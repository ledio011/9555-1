using System;

namespace Sproto
{
	// Token: 0x02000814 RID: 2068
	public class SprotoTypeReader
	{
		// Token: 0x060031C0 RID: 12736 RVA: 0x000C2930 File Offset: 0x000C0B30
		public SprotoTypeReader(byte[] buffer, int offset, int size)
		{
			this.Init(buffer, offset, size);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000C2944 File Offset: 0x000C0B44
		public SprotoTypeReader()
		{
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000C294C File Offset: 0x000C0B4C
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000C2954 File Offset: 0x000C0B54
		public int Position
		{
			get
			{
				return this.pos - this.begin;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000C2964 File Offset: 0x000C0B64
		public int Offset
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000C296C File Offset: 0x000C0B6C
		public int Length
		{
			get
			{
				return this.size - this.begin;
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000C297C File Offset: 0x000C0B7C
		public void Init(byte[] buffer, int offset, int size)
		{
			this.begin = offset;
			this.pos = offset;
			this.buffer = buffer;
			this.size = offset + size;
			this.check();
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000C29B0 File Offset: 0x000C0BB0
		private void check()
		{
			if (this.pos > this.size || this.begin > this.pos)
			{
				SprotoTypeSize.error("invalid pos.");
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000C29EC File Offset: 0x000C0BEC
		public byte ReadByte()
		{
			this.check();
			return this.buffer[this.pos++];
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000C2A18 File Offset: 0x000C0C18
		public void Seek(int offset)
		{
			this.pos = this.begin + offset;
			this.check();
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000C2A30 File Offset: 0x000C0C30
		public void Read(byte[] data, int offset, int size)
		{
			int num = this.pos;
			this.pos += size;
			this.check();
			for (int i = num; i < this.pos; i++)
			{
				data[offset + i - num] = this.buffer[i];
			}
		}

		// Token: 0x04002148 RID: 8520
		private byte[] buffer;

		// Token: 0x04002149 RID: 8521
		private int begin;

		// Token: 0x0400214A RID: 8522
		private int pos;

		// Token: 0x0400214B RID: 8523
		private int size;
	}
}
