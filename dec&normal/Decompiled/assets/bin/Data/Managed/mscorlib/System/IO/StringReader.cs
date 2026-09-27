using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200013E RID: 318
	[ComVisible(true)]
	[Serializable]
	public class StringReader : TextReader
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0003074C File Offset: 0x0002E94C
		public StringReader(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			this.source = s;
			this.nextChar = 0;
			this.sourceLength = s.Length;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00030780 File Offset: 0x0002E980
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0003078C File Offset: 0x0002E98C
		protected override void Dispose(bool disposing)
		{
			this.source = null;
			base.Dispose(disposing);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0003079C File Offset: 0x0002E99C
		public override int Peek()
		{
			this.CheckObjectDisposedException();
			if (this.nextChar >= this.sourceLength)
			{
				return -1;
			}
			return (int)this.source[this.nextChar];
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000307C8 File Offset: 0x0002E9C8
		public override int Read()
		{
			this.CheckObjectDisposedException();
			if (this.nextChar >= this.sourceLength)
			{
				return -1;
			}
			return (int)this.source[this.nextChar++];
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003080C File Offset: 0x0002EA0C
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			this.CheckObjectDisposedException();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException();
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num;
			if (this.nextChar > this.sourceLength - count)
			{
				num = this.sourceLength - this.nextChar;
			}
			else
			{
				num = count;
			}
			this.source.CopyTo(this.nextChar, buffer, index, num);
			this.nextChar += num;
			return num;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000308A0 File Offset: 0x0002EAA0
		public override string ReadLine()
		{
			this.CheckObjectDisposedException();
			if (this.nextChar >= this.source.Length)
			{
				return null;
			}
			int num = this.source.IndexOf('\r', this.nextChar);
			int num2 = this.source.IndexOf('\n', this.nextChar);
			bool flag = false;
			int num3;
			if (num == -1)
			{
				if (num2 == -1)
				{
					return this.ReadToEnd();
				}
				num3 = num2;
			}
			else if (num2 == -1)
			{
				num3 = num;
			}
			else
			{
				num3 = ((num <= num2) ? num : num2);
				flag = (num + 1 == num2);
			}
			string result = this.source.Substring(this.nextChar, num3 - this.nextChar);
			this.nextChar = num3 + ((!flag) ? 1 : 2);
			return result;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00030968 File Offset: 0x0002EB68
		public override string ReadToEnd()
		{
			this.CheckObjectDisposedException();
			string result = this.source.Substring(this.nextChar, this.sourceLength - this.nextChar);
			this.nextChar = this.sourceLength;
			return result;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000309A8 File Offset: 0x0002EBA8
		private void CheckObjectDisposedException()
		{
			if (this.source == null)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot read from a closed StringReader"));
			}
		}

		// Token: 0x04000534 RID: 1332
		private string source;

		// Token: 0x04000535 RID: 1333
		private int nextChar;

		// Token: 0x04000536 RID: 1334
		private int sourceLength;
	}
}
