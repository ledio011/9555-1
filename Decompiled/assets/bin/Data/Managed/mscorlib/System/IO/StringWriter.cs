using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200013F RID: 319
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x000309CC File Offset: 0x0002EBCC
		public StringWriter() : this(new StringBuilder())
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000309DC File Offset: 0x0002EBDC
		public StringWriter(StringBuilder sb) : this(sb, null)
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000309E8 File Offset: 0x0002EBE8
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb");
			}
			this.internalString = sb;
			this.internalFormatProvider = formatProvider;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00030A10 File Offset: 0x0002EC10
		public override Encoding Encoding
		{
			get
			{
				return Encoding.Unicode;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00030A18 File Offset: 0x0002EC18
		public override void Close()
		{
			this.Dispose(true);
			this.disposed = true;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00030A28 File Offset: 0x0002EC28
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.disposed = true;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00030A38 File Offset: 0x0002EC38
		public override string ToString()
		{
			return this.internalString.ToString();
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00030A48 File Offset: 0x0002EC48
		public override void Write(char value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot write to a closed StringWriter"));
			}
			this.internalString.Append(value);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00030A78 File Offset: 0x0002EC78
		public override void Write(string value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot write to a closed StringWriter"));
			}
			this.internalString.Append(value);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00030AA8 File Offset: 0x0002ECA8
		public override void Write(char[] buffer, int index, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot write to a closed StringWriter"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > buffer.Length - count)
			{
				throw new ArgumentException("index + count > buffer.Length");
			}
			this.internalString.Append(buffer, index, count);
		}

		// Token: 0x04000537 RID: 1335
		private StringBuilder internalString;

		// Token: 0x04000538 RID: 1336
		private bool disposed;
	}
}
