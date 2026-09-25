using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.IO
{
	// Token: 0x02000123 RID: 291
	[ComVisible(true)]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x0002C19C File Offset: 0x0002A39C
		public FileNotFoundException() : base(Locale.GetText("Unable to find the specified file."))
		{
			base.HResult = -2146232799;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		public FileNotFoundException(string message) : base(message)
		{
			base.HResult = -2146232799;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002C1D0 File Offset: 0x0002A3D0
		public FileNotFoundException(string message, string fileName) : base(message)
		{
			base.HResult = -2146232799;
			this.fileName = fileName;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = info.GetString("FileNotFound_FileName");
			this.fusionLog = info.GetString("FileNotFound_FusionLog");
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002C218 File Offset: 0x0002A418
		public override string Message
		{
			get
			{
				if (this.message == null && this.fileName != null)
				{
					return string.Format(CultureInfo.CurrentCulture, "Could not load file or assembly '{0}' or one of its dependencies. The system cannot find the file specified.", new object[]
					{
						this.fileName
					});
				}
				return this.message;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002C264 File Offset: 0x0002A464
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this.fileName);
			info.AddValue("FileNotFound_FusionLog", this.fusionLog);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002C290 File Offset: 0x0002A490
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.GetType().FullName);
			stringBuilder.AppendFormat(": {0}", this.Message);
			if (this.fileName != null && this.fileName.Length > 0)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.AppendFormat("File name: '{0}'", this.fileName);
			}
			if (this.InnerException != null)
			{
				stringBuilder.AppendFormat(" ---> {0}", this.InnerException);
			}
			if (this.StackTrace != null)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(this.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000493 RID: 1171
		private const int Result = -2146232799;

		// Token: 0x04000494 RID: 1172
		private string fileName;

		// Token: 0x04000495 RID: 1173
		private string fusionLog;
	}
}
