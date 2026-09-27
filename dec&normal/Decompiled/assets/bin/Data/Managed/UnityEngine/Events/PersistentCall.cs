using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	internal class PersistentCall
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000979C File Offset: 0x0000799C
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000097A4 File Offset: 0x000079A4
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000097AC File Offset: 0x000079AC
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000097B4 File Offset: 0x000079B4
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000097BC File Offset: 0x000079BC
		public bool IsValid()
		{
			return this.target != null && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000097E0 File Offset: 0x000079E0
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			if (this.m_CallState == UnityEventCallState.Off || theEvent == null)
			{
				return null;
			}
			MethodInfo methodInfo = theEvent.FindMethod(this);
			if (methodInfo == null)
			{
				return null;
			}
			switch (this.m_Mode)
			{
			case PersistentListenerMode.EventDefined:
				return theEvent.GetDelegate(this.target, methodInfo);
			case PersistentListenerMode.Void:
				return new InvokableCall(this.target, methodInfo);
			case PersistentListenerMode.Object:
				return PersistentCall.GetObjectCall(this.target, methodInfo, this.m_Arguments);
			case PersistentListenerMode.Int:
				return new CachedInvokableCall<int>(this.target, methodInfo, this.m_Arguments.intArgument);
			case PersistentListenerMode.Float:
				return new CachedInvokableCall<float>(this.target, methodInfo, this.m_Arguments.floatArgument);
			case PersistentListenerMode.String:
				return new CachedInvokableCall<string>(this.target, methodInfo, this.m_Arguments.stringArgument);
			case PersistentListenerMode.Bool:
				return new CachedInvokableCall<bool>(this.target, methodInfo, this.m_Arguments.boolArgument);
			default:
				return null;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000098D0 File Offset: 0x00007AD0
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			if (!string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName))
			{
				type = (Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object));
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[]
			{
				type
			});
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			if (@object != null && !type.IsAssignableFrom(@object.GetType()))
			{
				@object = null;
			}
			return constructor.Invoke(new object[]
			{
				target,
				method,
				@object
			}) as BaseInvokableCall;
		}

		// Token: 0x040000AC RID: 172
		[SerializeField]
		[FormerlySerializedAs("instance")]
		private Object m_Target;

		// Token: 0x040000AD RID: 173
		[FormerlySerializedAs("methodName")]
		[SerializeField]
		private string m_MethodName;

		// Token: 0x040000AE RID: 174
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private PersistentListenerMode m_Mode;

		// Token: 0x040000AF RID: 175
		[FormerlySerializedAs("arguments")]
		[SerializeField]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x040000B0 RID: 176
		[FormerlySerializedAs("enabled")]
		[SerializeField]
		[FormerlySerializedAs("m_Enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
