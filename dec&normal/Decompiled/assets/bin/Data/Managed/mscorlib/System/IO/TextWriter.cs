using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x02000144 RID: 324
	[ComVisible(true)]
	[Serializable]
	public abstract class TextWriter : IDisposable
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x00031100 File Offset: 0x0002F300
		protected TextWriter()
		{
			this.CoreNewLine = Environment.NewLine.ToCharArray();
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C9B RID: 3227
		public abstract Encoding Encoding { get; }

		// Token: 0x06000C9C RID: 3228 RVA: 0x00031124 File Offset: 0x0002F324
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00031130 File Offset: 0x0002F330
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00031140 File Offset: 0x0002F340
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00031150 File Offset: 0x0002F350
		public virtual void Flush()
		{
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00031154 File Offset: 0x0002F354
		internal static TextWriter Synchronized(TextWriter writer, bool neverClose)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer is null");
			}
			if (writer is SynchronizedWriter)
			{
				return writer;
			}
			return new SynchronizedWriter(writer, neverClose);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003117C File Offset: 0x0002F37C
		public virtual void Write(char value)
		{
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00031180 File Offset: 0x0002F380
		public virtual void Write(char[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00031194 File Offset: 0x0002F394
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000311A8 File Offset: 0x0002F3A8
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(format, arg));
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000311B8 File Offset: 0x0002F3B8
		public virtual void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || index > buffer.Length - count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				this.Write(buffer[index]);
				count--;
				index++;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00031230 File Offset: 0x0002F430
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(format, arg0, arg1));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00031240 File Offset: 0x0002F440
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(format, arg0, arg1, arg2));
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00031254 File Offset: 0x0002F454
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00031264 File Offset: 0x0002F464
		public virtual void WriteLine(string value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00031274 File Offset: 0x0002F474
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.Write(format, arg0, arg1);
			this.WriteLine();
		}

		// Token: 0x0400053D RID: 1341
		protected char[] CoreNewLine;

		// Token: 0x0400053E RID: 1342
		internal IFormatProvider internalFormatProvider;

		// Token: 0x0400053F RID: 1343
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x02000145 RID: 325
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00031290 File Offset: 0x0002F490
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x06000CAD RID: 3245 RVA: 0x00031298 File Offset: 0x0002F498
			public override void Write(string s)
			{
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x0003129C File Offset: 0x0002F49C
			public override void Write(char value)
			{
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x000312A0 File Offset: 0x0002F4A0
			public override void Write(char[] value, int index, int count)
			{
			}
		}
	}
}
