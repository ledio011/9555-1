using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public abstract class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x00009A88 File Offset: 0x00007C88
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00009A98 File Offset: 0x00007C98
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00009AAC File Offset: 0x00007CAC
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0)
			});
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00009AC8 File Offset: 0x00007CC8
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00009AD4 File Offset: 0x00007CD4
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00009ADC File Offset: 0x00007CDC
		public void Invoke(T0 arg0)
		{
			this.m_InvokeArray[0] = arg0;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x040000BB RID: 187
		private readonly object[] m_InvokeArray = new object[1];
	}
}
