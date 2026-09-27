using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B1 RID: 689
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001588 RID: 5512
		int ArgCount { get; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001589 RID: 5513
		object[] Args { get; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600158A RID: 5514
		bool HasVarArgs { get; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600158B RID: 5515
		LogicalCallContext LogicalCallContext { get; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600158C RID: 5516
		MethodBase MethodBase { get; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600158D RID: 5517
		string MethodName { get; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600158E RID: 5518
		object MethodSignature { get; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600158F RID: 5519
		string TypeName { get; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001590 RID: 5520
		string Uri { get; }

		// Token: 0x06001591 RID: 5521
		object GetArg(int argNum);

		// Token: 0x06001592 RID: 5522
		string GetArgName(int index);
	}
}
