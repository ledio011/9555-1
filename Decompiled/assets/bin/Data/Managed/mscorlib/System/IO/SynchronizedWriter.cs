using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	internal class SynchronizedWriter : TextWriter
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x00030CE0 File Offset: 0x0002EEE0
		public SynchronizedWriter(TextWriter writer, bool neverClose)
		{
			this.writer = writer;
			this.neverClose = neverClose;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00030CF8 File Offset: 0x0002EEF8
		public override void Close()
		{
			if (this.neverClose)
			{
				return;
			}
			lock (this)
			{
				this.writer.Close();
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00030D40 File Offset: 0x0002EF40
		public override void Flush()
		{
			lock (this)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00030D7C File Offset: 0x0002EF7C
		public override void Write(char value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00030DBC File Offset: 0x0002EFBC
		public override void Write(char[] value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00030DFC File Offset: 0x0002EFFC
		public override void Write(string value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00030E3C File Offset: 0x0002F03C
		public override void Write(string format, object[] value)
		{
			lock (this)
			{
				this.writer.Write(format, value);
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00030E7C File Offset: 0x0002F07C
		public override void Write(char[] buffer, int index, int count)
		{
			lock (this)
			{
				this.writer.Write(buffer, index, count);
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00030EBC File Offset: 0x0002F0BC
		public override void Write(string format, object arg0, object arg1)
		{
			lock (this)
			{
				this.writer.Write(format, arg0, arg1);
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00030EFC File Offset: 0x0002F0FC
		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			lock (this)
			{
				this.writer.Write(format, arg0, arg1, arg2);
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00030F40 File Offset: 0x0002F140
		public override void WriteLine()
		{
			lock (this)
			{
				this.writer.WriteLine();
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00030F7C File Offset: 0x0002F17C
		public override void WriteLine(string value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00030FBC File Offset: 0x0002F1BC
		public override void WriteLine(string format, object arg0, object arg1)
		{
			lock (this)
			{
				this.writer.WriteLine(format, arg0, arg1);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00030FFC File Offset: 0x0002F1FC
		public override Encoding Encoding
		{
			get
			{
				Encoding encoding;
				lock (this)
				{
					encoding = this.writer.Encoding;
				}
				return encoding;
			}
		}

		// Token: 0x0400053A RID: 1338
		private TextWriter writer;

		// Token: 0x0400053B RID: 1339
		private bool neverClose;
	}
}
