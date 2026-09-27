using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000052 RID: 82
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x000094D0 File Offset: 0x000076D0
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument) : base(target, theFunction)
		{
			this.m_Arg1[0] = argument;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000094F4 File Offset: 0x000076F4
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x040000A6 RID: 166
		private readonly object[] m_Arg1 = new object[1];
	}
}
