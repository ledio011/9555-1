using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020002D7 RID: 727
	[ComVisible(true)]
	public sealed class RemotingServices
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x0004F934 File Offset: 0x0004DB34
		private RemotingServices()
		{
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0004F93C File Offset: 0x0004DB3C
		static RemotingServices()
		{
			RemotingSurrogateSelector selector = new RemotingSurrogateSelector();
			StreamingContext context = new StreamingContext(StreamingContextStates.Remoting, null);
			RemotingServices._serializationFormatter = new BinaryFormatter(selector, context);
			RemotingServices._deserializationFormatter = new BinaryFormatter(null, context);
			RemotingServices._serializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			RemotingServices._deserializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			RemotingServices.RegisterInternalChannels();
			RemotingServices.app_id = Guid.NewGuid().ToString().Replace('-', '_') + "/";
			RemotingServices.CreateWellKnownServerIdentity(typeof(RemoteActivator), "RemoteActivationService.rem", WellKnownObjectMode.Singleton);
			RemotingServices.FieldSetterMethod = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.NonPublic);
			RemotingServices.FieldGetterMethod = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x060016B8 RID: 5816
		[MethodImpl(4096)]
		internal static extern object InternalExecute(MethodBase method, object obj, object[] parameters, out object[] out_args);

		// Token: 0x060016B9 RID: 5817
		[MethodImpl(4096)]
		internal static extern MethodBase GetVirtualMethod(Type type, MethodBase method);

		// Token: 0x060016BA RID: 5818
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern bool IsTransparentProxy(object proxy);

		// Token: 0x060016BB RID: 5819 RVA: 0x0004FA14 File Offset: 0x0004DC14
		internal static IMethodReturnMessage InternalExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			Type type = target.GetType();
			MethodBase methodBase;
			if (reqMsg.MethodBase.DeclaringType == type || reqMsg.MethodBase == RemotingServices.FieldSetterMethod || reqMsg.MethodBase == RemotingServices.FieldGetterMethod)
			{
				methodBase = reqMsg.MethodBase;
			}
			else
			{
				methodBase = RemotingServices.GetVirtualMethod(type, reqMsg.MethodBase);
				if (methodBase == null)
				{
					throw new RemotingException(string.Format("Cannot resolve method {0}:{1}", type, reqMsg.MethodName));
				}
			}
			if (reqMsg.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = reqMsg.MethodBase.GetGenericArguments();
				methodBase = ((MethodInfo)methodBase).MakeGenericMethod(genericArguments);
			}
			object oldContext = CallContext.SetCurrentCallContext(reqMsg.LogicalCallContext);
			ReturnMessage result;
			try
			{
				object[] array;
				object ret = RemotingServices.InternalExecute(methodBase, target, reqMsg.Args, out array);
				ParameterInfo[] parameters = methodBase.GetParameters();
				object[] array2 = new object[parameters.Length];
				int outArgsCount = 0;
				int num = 0;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					if (parameterInfo.IsOut && !parameterInfo.ParameterType.IsByRef)
					{
						array2[outArgsCount++] = reqMsg.GetArg(parameterInfo.Position);
					}
					else if (parameterInfo.ParameterType.IsByRef)
					{
						array2[outArgsCount++] = array[num++];
					}
					else
					{
						array2[outArgsCount++] = null;
					}
				}
				result = new ReturnMessage(ret, array2, outArgsCount, CallContext.CreateLogicalCallContext(true), reqMsg);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, reqMsg);
			}
			CallContext.RestoreCallContext(oldContext);
			return result;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004FBC4 File Offset: 0x0004DDC4
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (RemotingServices.IsTransparentProxy(target))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(target);
				return (IMethodReturnMessage)realProxy.Invoke(reqMsg);
			}
			return RemotingServices.InternalExecuteMessage(target, reqMsg);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0004FBF8 File Offset: 0x0004DDF8
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url)
		{
			ObjRef objRef = new ObjRef(classToProxy, url, null);
			return RemotingServices.GetRemoteObject(objRef, classToProxy);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0004FC18 File Offset: 0x0004DE18
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			ObjRef objRef = new ObjRef(classToProxy, url, data);
			return RemotingServices.GetRemoteObject(objRef, classToProxy);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0004FC38 File Offset: 0x0004DE38
		public static bool Disconnect(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			ServerIdentity serverIdentity;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				if (!realProxy.GetProxiedType().IsContextful || !(realProxy.ObjectIdentity is ServerIdentity))
				{
					throw new ArgumentException("The obj parameter is a proxy.");
				}
				serverIdentity = (realProxy.ObjectIdentity as ServerIdentity);
			}
			else
			{
				serverIdentity = obj.ObjectIdentity;
				obj.ObjectIdentity = null;
			}
			if (serverIdentity == null || !serverIdentity.IsConnected)
			{
				return false;
			}
			LifetimeServices.StopTrackingLifetime(serverIdentity);
			RemotingServices.DisposeIdentity(serverIdentity);
			TrackingServices.NotifyDisconnectedObject(obj);
			return true;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0004FCE0 File Offset: 0x0004DEE0
		public static Type GetServerTypeForUri(string URI)
		{
			ServerIdentity serverIdentity = RemotingServices.GetIdentityForUri(URI) as ServerIdentity;
			if (serverIdentity == null)
			{
				return null;
			}
			return serverIdentity.ObjectType;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004FD08 File Offset: 0x0004DF08
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			Identity objectIdentity = RemotingServices.GetObjectIdentity(obj);
			if (objectIdentity is ClientIdentity)
			{
				return ((ClientIdentity)objectIdentity).TargetUri;
			}
			if (objectIdentity != null)
			{
				return objectIdentity.ObjectUri;
			}
			return null;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004FD44 File Offset: 0x0004DF44
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.Unmarshal(objectRef, true);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004FD50 File Offset: 0x0004DF50
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			Type type = (!fRefine) ? typeof(MarshalByRefObject) : objectRef.ServerType;
			if (type == null)
			{
				type = typeof(MarshalByRefObject);
			}
			if (objectRef.IsReferenceToWellKnow)
			{
				object remoteObject = RemotingServices.GetRemoteObject(objectRef, type);
				TrackingServices.NotifyUnmarshaledObject(remoteObject, objectRef);
				return remoteObject;
			}
			object obj;
			if (type.IsContextful)
			{
				ProxyAttribute proxyAttribute = (ProxyAttribute)Attribute.GetCustomAttribute(type, typeof(ProxyAttribute), true);
				if (proxyAttribute != null)
				{
					obj = proxyAttribute.CreateProxy(objectRef, type, null, null).GetTransparentProxy();
					TrackingServices.NotifyUnmarshaledObject(obj, objectRef);
					return obj;
				}
			}
			obj = RemotingServices.GetProxyForRemoteObject(objectRef, type);
			TrackingServices.NotifyUnmarshaledObject(obj, objectRef);
			return obj;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.Marshal(Obj, null, null);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0004FE04 File Offset: 0x0004E004
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.Marshal(Obj, URI, null);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0004FE10 File Offset: 0x0004E010
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				Identity objectIdentity = realProxy.ObjectIdentity;
				if (objectIdentity != null)
				{
					if (realProxy.GetProxiedType().IsContextful && !objectIdentity.IsConnected)
					{
						ClientActivatedIdentity clientActivatedIdentity = (ClientActivatedIdentity)objectIdentity;
						if (ObjURI == null)
						{
							ObjURI = RemotingServices.NewUri();
						}
						clientActivatedIdentity.ObjectUri = ObjURI;
						RemotingServices.RegisterServerIdentity(clientActivatedIdentity);
						clientActivatedIdentity.StartTrackingLifetime((ILease)Obj.InitializeLifetimeService());
						return clientActivatedIdentity.CreateObjRef(RequestedType);
					}
					if (ObjURI != null)
					{
						throw new RemotingException("It is not possible marshal a proxy of a remote object.");
					}
					ObjRef objRef = realProxy.ObjectIdentity.CreateObjRef(RequestedType);
					TrackingServices.NotifyMarshaledObject(Obj, objRef);
					return objRef;
				}
			}
			if (RequestedType == null)
			{
				RequestedType = Obj.GetType();
			}
			if (ObjURI == null)
			{
				if (Obj.ObjectIdentity == null)
				{
					ObjURI = RemotingServices.NewUri();
					RemotingServices.CreateClientActivatedServerIdentity(Obj, RequestedType, ObjURI);
				}
			}
			else
			{
				ClientActivatedIdentity clientActivatedIdentity2 = RemotingServices.GetIdentityForUri("/" + ObjURI) as ClientActivatedIdentity;
				if (clientActivatedIdentity2 == null || Obj != clientActivatedIdentity2.GetServerObject())
				{
					RemotingServices.CreateClientActivatedServerIdentity(Obj, RequestedType, ObjURI);
				}
			}
			ObjRef objRef2;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				objRef2 = RemotingServices.GetRealProxy(Obj).ObjectIdentity.CreateObjRef(RequestedType);
			}
			else
			{
				objRef2 = Obj.CreateObjRef(RequestedType);
			}
			TrackingServices.NotifyMarshaledObject(Obj, objRef2);
			return objRef2;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0004FF58 File Offset: 0x0004E158
		private static string NewUri()
		{
			int num = Interlocked.Increment(ref RemotingServices.next_id);
			return string.Concat(new object[]
			{
				RemotingServices.app_id,
				Environment.TickCount.ToString("x"),
				"_",
				num,
				".rem"
			});
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004FFB4 File Offset: 0x0004E1B4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static RealProxy GetRealProxy(object proxy)
		{
			if (!RemotingServices.IsTransparentProxy(proxy))
			{
				throw new RemotingException("Cannot get the real proxy from an object that is not a transparent proxy.");
			}
			return ((TransparentProxy)proxy)._rp;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004FFD8 File Offset: 0x0004E1D8
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			Type type = Type.GetType(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException("Type '" + msg.TypeName + "' not found.");
			}
			return RemotingServices.GetMethodBaseFromName(type, msg.MethodName, (Type[])msg.MethodSignature);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0005002C File Offset: 0x0004E22C
		internal static MethodBase GetMethodBaseFromName(Type type, string methodName, Type[] signature)
		{
			if (type.IsInterface)
			{
				return RemotingServices.FindInterfaceMethod(type, methodName, signature);
			}
			MethodBase method;
			if (signature == null)
			{
				method = type.GetMethod(methodName, RemotingServices.methodBindings);
			}
			else
			{
				method = type.GetMethod(methodName, RemotingServices.methodBindings, null, signature, null);
			}
			if (method != null)
			{
				return method;
			}
			if (methodName == "FieldSetter")
			{
				return RemotingServices.FieldSetterMethod;
			}
			if (methodName == "FieldGetter")
			{
				return RemotingServices.FieldGetterMethod;
			}
			if (signature == null)
			{
				return type.GetConstructor(RemotingServices.methodBindings, null, Type.EmptyTypes, null);
			}
			return type.GetConstructor(RemotingServices.methodBindings, null, signature, null);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000500D4 File Offset: 0x0004E2D4
		private static MethodBase FindInterfaceMethod(Type type, string methodName, Type[] signature)
		{
			MethodBase methodBase;
			if (signature == null)
			{
				methodBase = type.GetMethod(methodName, RemotingServices.methodBindings);
			}
			else
			{
				methodBase = type.GetMethod(methodName, RemotingServices.methodBindings, null, signature, null);
			}
			if (methodBase != null)
			{
				return methodBase;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				methodBase = RemotingServices.FindInterfaceMethod(type2, methodName, signature);
				if (methodBase != null)
				{
					return methodBase;
				}
			}
			return null;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00050144 File Offset: 0x0004E344
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			ObjRef objRef = RemotingServices.Marshal((MarshalByRefObject)obj);
			objRef.GetObjectData(info, context);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00050178 File Offset: 0x0004E378
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			Identity objectIdentity = RemotingServices.GetObjectIdentity(obj);
			if (objectIdentity == null)
			{
				return null;
			}
			return objectIdentity.CreateObjRef(null);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0005019C File Offset: 0x0004E39C
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.GetLifetimeService();
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000501AC File Offset: 0x0004E3AC
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				return ((ClientIdentity)RemotingServices.GetRealProxy(obj).ObjectIdentity).EnvoySink;
			}
			throw new ArgumentException("obj must be a proxy.", "obj");
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000501E0 File Offset: 0x0004E3E0
		[MonoTODO]
		[Obsolete("It existed for only internal use in .NET and unimplemented in mono")]
		[Conditional("REMOTING_PERF")]
		public static void LogRemotingStage(int stage)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000501E8 File Offset: 0x0004E3E8
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000501F0 File Offset: 0x0004E3F0
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			MonoType monoType = (MonoType)msg.MethodBase.DeclaringType;
			return monoType.GetMethodsByName(msg.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, false, monoType).Length > 1;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00050224 File Offset: 0x0004E424
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			MarshalByRefObject marshalByRefObject = tp as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			Identity objectIdentity = RemotingServices.GetObjectIdentity(marshalByRefObject);
			return objectIdentity is ClientIdentity;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00050250 File Offset: 0x0004E450
		public static bool IsObjectOutOfContext(object tp)
		{
			MarshalByRefObject marshalByRefObject = tp as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			Identity objectIdentity = RemotingServices.GetObjectIdentity(marshalByRefObject);
			if (objectIdentity == null)
			{
				return false;
			}
			ServerIdentity serverIdentity = objectIdentity as ServerIdentity;
			return serverIdentity == null || serverIdentity.Context != Thread.CurrentContext;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0005029C File Offset: 0x0004E49C
		public static bool IsOneWay(MethodBase method)
		{
			return method.IsDefined(typeof(OneWayAttribute), false);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000502B0 File Offset: 0x0004E4B0
		internal static bool IsAsyncMessage(IMessage msg)
		{
			return msg is MonoMethodMessage && (((MonoMethodMessage)msg).IsAsync || RemotingServices.IsOneWay(((MonoMethodMessage)msg).MethodBase));
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000502EC File Offset: 0x0004E4EC
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity objectIdentity = realProxy.ObjectIdentity;
				if (objectIdentity != null && !(objectIdentity is ServerIdentity) && !realProxy.GetProxiedType().IsContextful)
				{
					throw new RemotingException("SetObjectUriForMarshal method should only be called for MarshalByRefObjects that exist in the current AppDomain.");
				}
			}
			RemotingServices.Marshal(obj, uri);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00050348 File Offset: 0x0004E548
		internal static object CreateClientProxy(ActivatedClientTypeEntry entry, object[] activationAttributes)
		{
			if (entry.ContextAttributes != null || activationAttributes != null)
			{
				ArrayList arrayList = new ArrayList();
				if (entry.ContextAttributes != null)
				{
					arrayList.AddRange(entry.ContextAttributes);
				}
				if (activationAttributes != null)
				{
					arrayList.AddRange(activationAttributes);
				}
				return RemotingServices.CreateClientProxy(entry.ObjectType, entry.ApplicationUrl, arrayList.ToArray());
			}
			return RemotingServices.CreateClientProxy(entry.ObjectType, entry.ApplicationUrl, null);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000503BC File Offset: 0x0004E5BC
		internal static object CreateClientProxy(Type objectType, string url, object[] activationAttributes)
		{
			string text = url;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			text += "RemoteActivationService.rem";
			string text2;
			RemotingServices.GetClientChannelSinkChain(text, null, out text2);
			RemotingProxy remotingProxy = new RemotingProxy(objectType, text, activationAttributes);
			return remotingProxy.GetTransparentProxy();
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0005040C File Offset: 0x0004E60C
		internal static object CreateClientProxy(WellKnownClientTypeEntry entry)
		{
			return RemotingServices.Connect(entry.ObjectType, entry.ObjectUrl, null);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00050420 File Offset: 0x0004E620
		internal static object CreateClientProxyForContextBound(Type type, object[] activationAttributes)
		{
			if (type.IsContextful)
			{
				ProxyAttribute proxyAttribute = (ProxyAttribute)Attribute.GetCustomAttribute(type, typeof(ProxyAttribute), true);
				if (proxyAttribute != null)
				{
					return proxyAttribute.CreateInstance(type);
				}
			}
			RemotingProxy remotingProxy = new RemotingProxy(type, ChannelServices.CrossContextUrl, activationAttributes);
			return remotingProxy.GetTransparentProxy();
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00050470 File Offset: 0x0004E670
		internal static Identity GetIdentityForUri(string uri)
		{
			string text = RemotingServices.GetNormalizedUri(uri);
			Hashtable obj = RemotingServices.uri_hash;
			Identity result;
			lock (obj)
			{
				Identity identity = (Identity)RemotingServices.uri_hash[text];
				if (identity == null)
				{
					text = RemotingServices.RemoveAppNameFromUri(uri);
					if (text != null)
					{
						identity = (Identity)RemotingServices.uri_hash[text];
					}
				}
				result = identity;
			}
			return result;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000504EC File Offset: 0x0004E6EC
		private static string RemoveAppNameFromUri(string uri)
		{
			string text = RemotingConfiguration.ApplicationName;
			if (text == null)
			{
				return null;
			}
			text = "/" + text + "/";
			if (uri.StartsWith(text))
			{
				return uri.Substring(text.Length);
			}
			return null;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00050534 File Offset: 0x0004E734
		internal static Identity GetObjectIdentity(MarshalByRefObject obj)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				return RemotingServices.GetRealProxy(obj).ObjectIdentity;
			}
			return obj.ObjectIdentity;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00050554 File Offset: 0x0004E754
		internal static ClientIdentity GetOrCreateClientIdentity(ObjRef objRef, Type proxyType, out object clientProxy)
		{
			object channelData = (objRef.ChannelInfo == null) ? null : objRef.ChannelInfo.ChannelData;
			string uri;
			IMessageSink clientChannelSinkChain = RemotingServices.GetClientChannelSinkChain(objRef.URI, channelData, out uri);
			if (uri == null)
			{
				uri = objRef.URI;
			}
			Hashtable obj = RemotingServices.uri_hash;
			ClientIdentity result;
			lock (obj)
			{
				clientProxy = null;
				string normalizedUri = RemotingServices.GetNormalizedUri(objRef.URI);
				ClientIdentity clientIdentity = RemotingServices.uri_hash[normalizedUri] as ClientIdentity;
				if (clientIdentity != null)
				{
					clientProxy = clientIdentity.ClientProxy;
					if (clientProxy != null)
					{
						return clientIdentity;
					}
					RemotingServices.DisposeIdentity(clientIdentity);
				}
				clientIdentity = new ClientIdentity(uri, objRef);
				clientIdentity.ChannelSink = clientChannelSinkChain;
				RemotingServices.uri_hash[normalizedUri] = clientIdentity;
				if (proxyType != null)
				{
					RemotingProxy remotingProxy = new RemotingProxy(proxyType, clientIdentity);
					CrossAppDomainSink crossAppDomainSink = clientChannelSinkChain as CrossAppDomainSink;
					if (crossAppDomainSink != null)
					{
						remotingProxy.SetTargetDomain(crossAppDomainSink.TargetDomainId);
					}
					clientProxy = remotingProxy.GetTransparentProxy();
					clientIdentity.ClientProxy = (MarshalByRefObject)clientProxy;
				}
				result = clientIdentity;
			}
			return result;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0005067C File Offset: 0x0004E87C
		private static IMessageSink GetClientChannelSinkChain(string url, object channelData, out string objectUri)
		{
			IMessageSink messageSink = ChannelServices.CreateClientChannelSinkChain(url, channelData, out objectUri);
			if (messageSink != null)
			{
				return messageSink;
			}
			if (url != null)
			{
				string message = string.Format("Cannot create channel sink to connect to URL {0}. An appropriate channel has probably not been registered.", url);
				throw new RemotingException(message);
			}
			string message2 = string.Format("Cannot create channel sink to connect to the remote object. An appropriate channel has probably not been registered.", url);
			throw new RemotingException(message2);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000506C8 File Offset: 0x0004E8C8
		internal static ClientActivatedIdentity CreateContextBoundObjectIdentity(Type objectType)
		{
			return new ClientActivatedIdentity(null, objectType)
			{
				ChannelSink = ChannelServices.CrossContextChannel
			};
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000506EC File Offset: 0x0004E8EC
		internal static ClientActivatedIdentity CreateClientActivatedServerIdentity(MarshalByRefObject realObject, Type objectType, string objectUri)
		{
			ClientActivatedIdentity clientActivatedIdentity = new ClientActivatedIdentity(objectUri, objectType);
			clientActivatedIdentity.AttachServerObject(realObject, Context.DefaultContext);
			RemotingServices.RegisterServerIdentity(clientActivatedIdentity);
			clientActivatedIdentity.StartTrackingLifetime((ILease)realObject.InitializeLifetimeService());
			return clientActivatedIdentity;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00050728 File Offset: 0x0004E928
		internal static ServerIdentity CreateWellKnownServerIdentity(Type objectType, string objectUri, WellKnownObjectMode mode)
		{
			ServerIdentity serverIdentity;
			if (mode == WellKnownObjectMode.SingleCall)
			{
				serverIdentity = new SingleCallIdentity(objectUri, Context.DefaultContext, objectType);
			}
			else
			{
				serverIdentity = new SingletonIdentity(objectUri, Context.DefaultContext, objectType);
			}
			RemotingServices.RegisterServerIdentity(serverIdentity);
			return serverIdentity;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00050764 File Offset: 0x0004E964
		private static void RegisterServerIdentity(ServerIdentity identity)
		{
			Hashtable obj = RemotingServices.uri_hash;
			lock (obj)
			{
				if (RemotingServices.uri_hash.ContainsKey(identity.ObjectUri))
				{
					throw new RemotingException("Uri already in use: " + identity.ObjectUri + ".");
				}
				RemotingServices.uri_hash[identity.ObjectUri] = identity;
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000507DC File Offset: 0x0004E9DC
		internal static object GetProxyForRemoteObject(ObjRef objref, Type classToProxy)
		{
			ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(objref.URI) as ClientActivatedIdentity;
			if (clientActivatedIdentity != null)
			{
				return clientActivatedIdentity.GetServerObject();
			}
			return RemotingServices.GetRemoteObject(objref, classToProxy);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00050810 File Offset: 0x0004EA10
		internal static object GetRemoteObject(ObjRef objRef, Type proxyType)
		{
			object result;
			RemotingServices.GetOrCreateClientIdentity(objRef, proxyType, out result);
			return result;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00050828 File Offset: 0x0004EA28
		internal static object GetServerObject(string uri)
		{
			ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(uri) as ClientActivatedIdentity;
			if (clientActivatedIdentity == null)
			{
				throw new RemotingException("Server for uri '" + uri + "' not found");
			}
			return clientActivatedIdentity.GetServerObject();
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00050864 File Offset: 0x0004EA64
		internal static byte[] SerializeCallData(object obj)
		{
			LogicalCallContext logicalCallContext = CallContext.CreateLogicalCallContext(false);
			if (logicalCallContext != null)
			{
				obj = new RemotingServices.CACD
				{
					d = obj,
					c = logicalCallContext
				};
			}
			if (obj == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			RemotingServices._serializationFormatter.Serialize(memoryStream, obj);
			return memoryStream.ToArray();
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000508B8 File Offset: 0x0004EAB8
		internal static object DeserializeCallData(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			MemoryStream serializationStream = new MemoryStream(array);
			object obj = RemotingServices._deserializationFormatter.Deserialize(serializationStream);
			if (obj is RemotingServices.CACD)
			{
				RemotingServices.CACD cacd = (RemotingServices.CACD)obj;
				obj = cacd.d;
				CallContext.UpdateCurrentCallContext((LogicalCallContext)cacd.c);
			}
			return obj;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005090C File Offset: 0x0004EB0C
		internal static byte[] SerializeExceptionData(Exception ex)
		{
			byte[] result;
			try
			{
				int num = 4;
				do
				{
					try
					{
						MemoryStream memoryStream = new MemoryStream();
						RemotingServices._serializationFormatter.Serialize(memoryStream, ex);
						return memoryStream.ToArray();
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException)
						{
							Thread.ResetAbort();
							num = 5;
							ex = ex2;
						}
						else if (num == 2)
						{
							ex = new Exception();
							ex.SetMessage(ex2.Message);
							ex.SetStackTrace(ex2.StackTrace);
						}
						else
						{
							ex = ex2;
						}
					}
					num--;
				}
				while (num > 0);
				result = null;
			}
			catch (Exception ex3)
			{
				byte[] array = RemotingServices.SerializeExceptionData(ex3);
				Thread.ResetAbort();
				result = array;
			}
			return result;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000509E0 File Offset: 0x0004EBE0
		internal static object GetDomainProxy(AppDomain domain)
		{
			byte[] array = null;
			Context currentContext = Thread.CurrentContext;
			try
			{
				array = (byte[])AppDomain.InvokeInDomain(domain, typeof(AppDomain).GetMethod("GetMarshalledDomainObjRef", BindingFlags.Instance | BindingFlags.NonPublic), domain, null);
			}
			finally
			{
				AppDomain.InternalSetContext(currentContext);
			}
			byte[] array2 = new byte[array.Length];
			array.CopyTo(array2, 0);
			MemoryStream mem = new MemoryStream(array2);
			ObjRef objectRef = (ObjRef)CADSerializer.DeserializeObject(mem);
			return (AppDomain)RemotingServices.Unmarshal(objectRef);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00050A68 File Offset: 0x0004EC68
		private static void RegisterInternalChannels()
		{
			CrossAppDomainChannel.RegisterCrossAppDomainChannel();
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00050A70 File Offset: 0x0004EC70
		internal static void DisposeIdentity(Identity ident)
		{
			Hashtable obj = RemotingServices.uri_hash;
			lock (obj)
			{
				if (!ident.Disposed)
				{
					ClientIdentity clientIdentity = ident as ClientIdentity;
					if (clientIdentity != null)
					{
						RemotingServices.uri_hash.Remove(RemotingServices.GetNormalizedUri(clientIdentity.TargetUri));
					}
					else
					{
						RemotingServices.uri_hash.Remove(ident.ObjectUri);
					}
					ident.Disposed = true;
				}
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00050AF0 File Offset: 0x0004ECF0
		internal static Identity GetMessageTargetIdentity(IMessage msg)
		{
			if (msg is IInternalMessage)
			{
				return ((IInternalMessage)msg).TargetIdentity;
			}
			Hashtable obj = RemotingServices.uri_hash;
			Identity result;
			lock (obj)
			{
				string normalizedUri = RemotingServices.GetNormalizedUri(((IMethodMessage)msg).Uri);
				result = (RemotingServices.uri_hash[normalizedUri] as ServerIdentity);
			}
			return result;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00050B64 File Offset: 0x0004ED64
		internal static void SetMessageTargetIdentity(IMessage msg, Identity ident)
		{
			if (msg is IInternalMessage)
			{
				((IInternalMessage)msg).TargetIdentity = ident;
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00050B80 File Offset: 0x0004ED80
		internal static bool UpdateOutArgObject(ParameterInfo pi, object local, object remote)
		{
			if (pi.ParameterType.IsArray && ((Array)local).Rank == 1)
			{
				Array array = (Array)local;
				if (array.Rank == 1)
				{
					Array.Copy((Array)remote, array, array.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00050BD8 File Offset: 0x0004EDD8
		private static string GetNormalizedUri(string uri)
		{
			if (uri.StartsWith("/"))
			{
				return uri.Substring(1);
			}
			return uri;
		}

		// Token: 0x04000BBE RID: 3006
		private static Hashtable uri_hash = new Hashtable();

		// Token: 0x04000BBF RID: 3007
		private static BinaryFormatter _serializationFormatter;

		// Token: 0x04000BC0 RID: 3008
		private static BinaryFormatter _deserializationFormatter;

		// Token: 0x04000BC1 RID: 3009
		internal static string app_id;

		// Token: 0x04000BC2 RID: 3010
		private static int next_id = 1;

		// Token: 0x04000BC3 RID: 3011
		private static readonly BindingFlags methodBindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000BC4 RID: 3012
		private static readonly MethodInfo FieldSetterMethod;

		// Token: 0x04000BC5 RID: 3013
		private static readonly MethodInfo FieldGetterMethod;

		// Token: 0x020002D8 RID: 728
		[Serializable]
		private class CACD
		{
			// Token: 0x04000BC6 RID: 3014
			public object d;

			// Token: 0x04000BC7 RID: 3015
			public object c;
		}
	}
}
