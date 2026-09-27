using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000060 RID: 96
	[ComVisible(true)]
	[Serializable]
	public class ArgumentOutOfRangeException : ArgumentException
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000EB78 File Offset: 0x0000CD78
		public ArgumentOutOfRangeException() : base(Locale.GetText("Argument is out of range."))
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public ArgumentOutOfRangeException(string paramName) : base(Locale.GetText("Argument is out of range."), paramName)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public ArgumentOutOfRangeException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public ArgumentOutOfRangeException(string paramName, object actualValue, string message) : base(message, paramName)
		{
			this.actual_value = actualValue;
			base.HResult = -2146233086;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actual_value = info.GetString("ActualValue");
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this.actual_value == null)
				{
					return message;
				}
				return message + Environment.NewLine + this.actual_value;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ActualValue", this.actual_value);
		}

		// Token: 0x0400018A RID: 394
		private const int Result = -2146233086;

		// Token: 0x0400018B RID: 395
		private object actual_value;
	}
}
