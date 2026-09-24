using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002BC RID: 700
	internal class MethodReturnDictionary : MethodDictionary
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x0004D748 File Offset: 0x0004B948
		public MethodReturnDictionary(IMethodReturnMessage message) : base(message)
		{
			if (message.Exception == null)
			{
				base.MethodKeys = MethodReturnDictionary.InternalReturnKeys;
			}
			else
			{
				base.MethodKeys = MethodReturnDictionary.InternalExceptionKeys;
			}
		}

		// Token: 0x04000B49 RID: 2889
		public static string[] InternalReturnKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__OutArgs",
			"__Return",
			"__CallContext"
		};

		// Token: 0x04000B4A RID: 2890
		public static string[] InternalExceptionKeys = new string[]
		{
			"__CallContext"
		};
	}
}
