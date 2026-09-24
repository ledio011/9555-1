using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000150 RID: 336
	[ComVisible(true)]
	[Serializable]
	public class MissingFieldException : MissingMemberException
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x000320E8 File Offset: 0x000302E8
		public MissingFieldException() : base(Locale.GetText("Cannot find requested field."))
		{
			base.HResult = -2146233071;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00032108 File Offset: 0x00030308
		public MissingFieldException(string message) : base(message)
		{
			base.HResult = -2146233071;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0003211C File Offset: 0x0003031C
		protected MissingFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00032128 File Offset: 0x00030328
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				string text = Locale.GetText("Field '{0}.{1}' not found.");
				return string.Format(text, this.ClassName, this.MemberName);
			}
		}

		// Token: 0x0400055B RID: 1371
		private const int Result = -2146233071;
	}
}
