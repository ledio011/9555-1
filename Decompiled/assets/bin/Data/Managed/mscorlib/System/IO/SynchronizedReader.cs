using System;

namespace System.IO
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	internal class SynchronizedReader : TextReader
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x00030B3C File Offset: 0x0002ED3C
		public SynchronizedReader(TextReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00030B4C File Offset: 0x0002ED4C
		public override void Close()
		{
			lock (this)
			{
				this.reader.Close();
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00030B88 File Offset: 0x0002ED88
		public override int Peek()
		{
			int result;
			lock (this)
			{
				result = this.reader.Peek();
			}
			return result;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00030BCC File Offset: 0x0002EDCC
		public override string ReadLine()
		{
			string result;
			lock (this)
			{
				result = this.reader.ReadLine();
			}
			return result;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00030C10 File Offset: 0x0002EE10
		public override string ReadToEnd()
		{
			string result;
			lock (this)
			{
				result = this.reader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00030C54 File Offset: 0x0002EE54
		public override int Read()
		{
			int result;
			lock (this)
			{
				result = this.reader.Read();
			}
			return result;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00030C98 File Offset: 0x0002EE98
		public override int Read(char[] buffer, int index, int count)
		{
			int result;
			lock (this)
			{
				result = this.reader.Read(buffer, index, count);
			}
			return result;
		}

		// Token: 0x04000539 RID: 1337
		private TextReader reader;
	}
}
