using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000147 RID: 327
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00031538 File Offset: 0x0002F738
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding) : base(stream, encoding)
		{
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00031544 File Offset: 0x0002F744
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00031574 File Offset: 0x0002F774
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000315A8 File Offset: 0x0002F7A8
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000315D8 File Offset: 0x0002F7D8
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00031608 File Offset: 0x0002F808
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
