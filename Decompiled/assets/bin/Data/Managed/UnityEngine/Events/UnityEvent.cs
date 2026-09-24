using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00009A48 File Offset: 0x00007C48
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[0]);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00009A58 File Offset: 0x00007C58
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00009A64 File Offset: 0x00007C64
		public void Invoke()
		{
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x040000BA RID: 186
		private readonly object[] m_InvokeArray = new object[0];
	}
}
