using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000142 RID: 322
	[ComVisible(true)]
	[Serializable]
	public abstract class TextReader : IDisposable
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x00031054 File Offset: 0x0002F254
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00031060 File Offset: 0x0002F260
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003106C File Offset: 0x0002F26C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003107C File Offset: 0x0002F27C
		public virtual int Peek()
		{
			return -1;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00031080 File Offset: 0x0002F280
		public virtual int Read()
		{
			return -1;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00031084 File Offset: 0x0002F284
		public virtual int Read([In] [Out] char[] buffer, int index, int count)
		{
			int i;
			for (i = 0; i < count; i++)
			{
				int num;
				if ((num = this.Read()) == -1)
				{
					return i;
				}
				buffer[index + i] = (char)num;
			}
			return i;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000310BC File Offset: 0x0002F2BC
		public virtual string ReadLine()
		{
			return string.Empty;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000310C4 File Offset: 0x0002F2C4
		public virtual string ReadToEnd()
		{
			return string.Empty;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000310CC File Offset: 0x0002F2CC
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader is null");
			}
			if (reader is SynchronizedReader)
			{
				return reader;
			}
			return new SynchronizedReader(reader);
		}

		// Token: 0x0400053C RID: 1340
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x02000143 RID: 323
		private class NullTextReader : TextReader
		{
			// Token: 0x06000C98 RID: 3224 RVA: 0x000310FC File Offset: 0x0002F2FC
			public override string ReadLine()
			{
				return null;
			}
		}
	}
}
