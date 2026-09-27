using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B8 RID: 696
	internal class MethodCallDictionary : MethodDictionary
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0004C728 File Offset: 0x0004A928
		public MethodCallDictionary(IMethodMessage message) : base(message)
		{
			base.MethodKeys = MethodCallDictionary.InternalKeys;
		}

		// Token: 0x04000B2F RID: 2863
		public static string[] InternalKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__Args",
			"__CallContext"
		};
	}
}
