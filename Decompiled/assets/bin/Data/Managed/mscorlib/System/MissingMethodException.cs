using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000152 RID: 338
	[ComVisible(true)]
	[Serializable]
	public class MissingMethodException : MissingMemberException
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x00032290 File Offset: 0x00030490
		public MissingMethodException() : base(Locale.GetText("Cannot find the requested method."))
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000322B0 File Offset: 0x000304B0
		public MissingMethodException(string message) : base(message)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000322C4 File Offset: 0x000304C4
		protected MissingMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000322D0 File Offset: 0x000304D0
		public MissingMethodException(string className, string methodName) : base(className, methodName)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000322E8 File Offset: 0x000304E8
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				string text = Locale.GetText("Method not found: '{0}.{1}'.");
				return string.Format(text, this.ClassName, this.MemberName);
			}
		}

		// Token: 0x04000560 RID: 1376
		private const int Result = -2146233069;
	}
}
