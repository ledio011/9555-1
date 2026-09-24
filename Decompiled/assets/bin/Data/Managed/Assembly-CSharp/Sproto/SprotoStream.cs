using System;
using System.IO;

namespace Sproto
{
	// Token: 0x02000810 RID: 2064
	public class SprotoStream
	{
		// Token: 0x0600318E RID: 12686 RVA: 0x000C1C28 File Offset: 0x000BFE28
		public SprotoStream()
		{
			this.size = 128;
			this.pos = 0;
			this.buffer = new byte[this.size];
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000C1C54 File Offset: 0x000BFE54
		public int Position
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000C1C5C File Offset: 0x000BFE5C
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000C1C64 File Offset: 0x000BFE64
		private void _expand(int sz = 0)
		{
			if (this.size - this.pos < sz)
			{
				long num = (long)this.size;
				while (this.size - this.pos < sz)
				{
					this.size *= 2;
				}
				if (this.size >= SprotoTypeSize.encode_max_size)
				{
					SprotoTypeSize.error("object is too large (>" + SprotoTypeSize.encode_max_size + ")");
				}
				byte[] array = new byte[this.size];
				for (long num2 = 0L; num2 < num; num2 += 1L)
				{
					checked
					{
						array[(int)((IntPtr)num2)] = this.buffer[(int)((IntPtr)num2)];
					}
				}
				this.buffer = array;
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000C1D14 File Offset: 0x000BFF14
		public void WriteByte(byte v)
		{
			this._expand(1);
			this.buffer[this.pos++] = v;
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000C1D44 File Offset: 0x000BFF44
		public void Write(byte[] data, int offset, int count)
		{
			this._expand(count);
			for (int i = 0; i < count; i++)
			{
				this.buffer[this.pos++] = data[offset + i];
			}
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000C1D88 File Offset: 0x000BFF88
		public int Seek(int offset, SeekOrigin loc)
		{
			switch (loc)
			{
			case 0:
				this.pos = offset;
				break;
			case 1:
				this.pos += offset;
				break;
			case 2:
				this.pos = this.size + offset;
				break;
			}
			this._expand(0);
			return this.pos;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000C1DF0 File Offset: 0x000BFFF0
		public void Read(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				buffer[offset + i] = this.buffer[this.pos++];
			}
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000C1E2C File Offset: 0x000C002C
		public void MoveUp(int position, int up_count)
		{
			if (up_count <= 0)
			{
				return;
			}
			long num = (long)(this.pos - position);
			int num2 = 0;
			while ((long)num2 < num)
			{
				this.buffer[position - up_count + num2] = this.buffer[position + num2];
				num2++;
			}
			this.pos -= up_count;
		}

		// Token: 0x17000E2E RID: 3630
		public byte this[int i]
		{
			get
			{
				if (i < 0 || i >= this.size)
				{
					throw new Exception("invalid idx:" + i + "@get");
				}
				return this.buffer[i];
			}
			set
			{
				if (i < 0 || i >= this.size)
				{
					throw new Exception("invalid idx:" + i + "@set");
				}
				this.buffer[i] = value;
			}
		}

		// Token: 0x0400213A RID: 8506
		private int size;

		// Token: 0x0400213B RID: 8507
		private int pos;

		// Token: 0x0400213C RID: 8508
		private byte[] buffer;
	}
}
