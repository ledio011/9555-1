using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000054 RID: 84
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x00009580 File Offset: 0x00007780
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1>)System.Delegate.Combine(this.Delegate, System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction) as UnityAction<T1>);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000095B8 File Offset: 0x000077B8
		public InvokableCall(UnityAction<T1> callback)
		{
			this.Delegate = (UnityAction<T1>)System.Delegate.Combine(this.Delegate, callback);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000095D8 File Offset: 0x000077D8
		public override void Invoke(object[] args)
		{
			if (args.Length != 1)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00009624 File Offset: 0x00007824
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}

		// Token: 0x040000A8 RID: 168
		private UnityAction<T1> Delegate;
	}
}
