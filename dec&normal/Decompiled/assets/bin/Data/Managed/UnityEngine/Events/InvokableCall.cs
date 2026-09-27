using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000053 RID: 83
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00009504 File Offset: 0x00007704
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction)System.Delegate.Combine(this.Delegate, System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction) as UnityAction);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000953C File Offset: 0x0000773C
		public override void Invoke(object[] args)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate();
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000955C File Offset: 0x0000775C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}

		// Token: 0x040000A7 RID: 167
		private UnityAction Delegate;
	}
}
