using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000267 RID: 615
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x00047A20 File Offset: 0x00045C20
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00047A58 File Offset: 0x00045C58
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink result;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					result = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink;
					result = crossAppDomainSink;
				}
			}
			return result;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00047AE4 File Offset: 0x00045CE4
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00047AEC File Offset: 0x00045CEC
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage result = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage retmsg = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					MemoryStream memoryStream = CADSerializer.SerializeMessage(msgRequest);
					array2 = memoryStream.GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[]
					{
						array2,
						cadmethodCallMessage
					});
					array = processMessageRes.arrResponse;
					retmsg = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					MemoryStream mem = new MemoryStream(array);
					result = CADSerializer.DeserializeMessage(mem, msgRequest as IMethodCallMessage);
				}
				else
				{
					result = new MethodResponse(msgRequest as IMethodCallMessage, retmsg);
				}
			}
			catch (Exception e)
			{
				try
				{
					result = new ReturnMessage(e, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return result;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00047BE8 File Offset: 0x00045DE8
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest state = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendAsyncMessage), state);
			return null;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00047C14 File Offset: 0x00045E14
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage msg = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(msg);
		}

		// Token: 0x04000A89 RID: 2697
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x04000A8A RID: 2698
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000A8B RID: 2699
		private int _domainID;

		// Token: 0x02000268 RID: 616
		private struct ProcessMessageRes
		{
			// Token: 0x04000A8C RID: 2700
			public byte[] arrResponse;

			// Token: 0x04000A8D RID: 2701
			public CADMethodReturnMessage cadMrm;
		}
	}
}
