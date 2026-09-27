using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200005E RID: 94
	[ComVisible(true)]
	[Serializable]
	public class ArgumentException : SystemException
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000EA18 File Offset: 0x0000CC18
		public ArgumentException() : base(Locale.GetText("Value does not fall within the expected range."))
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000EA38 File Offset: 0x0000CC38
		public ArgumentException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public ArgumentException(string message, string paramName) : base(message)
		{
			this.param_name = paramName;
			base.HResult = -2147024809;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000EA68 File Offset: 0x0000CC68
		public ArgumentException(string message, string paramName, Exception innerException) : base(message, innerException)
		{
			this.param_name = paramName;
			base.HResult = -2147024809;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000EA84 File Offset: 0x0000CC84
		protected ArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.param_name = info.GetString("ParamName");
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		public virtual string ParamName
		{
			get
			{
				return this.param_name;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public override string Message
		{
			get
			{
				if (this.ParamName != null && this.ParamName.Length != 0)
				{
					return base.Message + Environment.NewLine + Locale.GetText("Parameter name: ") + this.ParamName;
				}
				return base.Message;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this.ParamName);
		}

		// Token: 0x04000187 RID: 391
		private const int Result = -2147024809;

		// Token: 0x04000188 RID: 392
		private string param_name;
	}
}
