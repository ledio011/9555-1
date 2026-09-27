using System;
using System.IO;

namespace Sproto
{
	// Token: 0x0200080F RID: 2063
	public class SprotoPack
	{
		// Token: 0x06003188 RID: 12680 RVA: 0x000C1728 File Offset: 0x000BF928
		public SprotoPack()
		{
			this.buffer = new MemoryStream();
			this.tmp = new byte[8];
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000C1748 File Offset: 0x000BF948
		private void write_ff(byte[] src, int offset, long pos, int n)
		{
			int num = n + 7 & -8;
			long position = this.buffer.Position;
			this.buffer.Seek(pos, 0);
			this.buffer.WriteByte(byte.MaxValue);
			this.buffer.WriteByte((byte)(num / 8 - 1));
			this.buffer.Write(src, offset, n);
			for (int i = 0; i < num - n; i++)
			{
				this.buffer.WriteByte(0);
			}
			this.buffer.Seek(position, 0);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000C17D8 File Offset: 0x000BF9D8
		private int pack_seg(byte[] src, long offset, int ff_n)
		{
			byte b = 0;
			int num = 0;
			long position = this.buffer.Position;
			this.buffer.Seek(1L, 1);
			for (int i = 0; i < 8; i++)
			{
				if (src[(int)(checked((IntPtr)(unchecked(offset + (long)i))))] != 0)
				{
					num++;
					b |= (byte)(1 << i);
					this.buffer.WriteByte(src[(int)(checked((IntPtr)(unchecked(offset + (long)i))))]);
				}
			}
			if ((num == 7 || num == 6) && ff_n > 0)
			{
				num = 8;
			}
			if (num != 8)
			{
				this.buffer.Seek(position, 0);
				this.buffer.WriteByte(b);
				this.buffer.Seek(position, 0);
				return num + 1;
			}
			if (ff_n > 0)
			{
				this.buffer.Seek(position, 0);
				return 8;
			}
			this.buffer.Seek(position, 0);
			return 10;
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000C18B4 File Offset: 0x000BFAB4
		public byte[] pack(byte[] data, byte[] sends, ref int len)
		{
			this.clear();
			int num = len;
			byte[] array = null;
			int num2 = 0;
			long pos = 0L;
			int num3 = 0;
			byte[] array2 = data;
			for (int i = 0; i < num; i += 8)
			{
				int num4 = i;
				int num5 = i + 8 - num;
				if (num5 > 0)
				{
					for (int j = 0; j < 8 - num5; j++)
					{
						this.tmp[j] = array2[i + j];
					}
					for (int k = 0; k < num5; k++)
					{
						this.tmp[7 - k] = 0;
					}
					array2 = this.tmp;
					num4 = 0;
				}
				int num6 = this.pack_seg(array2, (long)num4, num3);
				if (num6 == 10)
				{
					array = array2;
					num2 = num4;
					pos = this.buffer.Position;
					num3 = 1;
				}
				else if (num6 == 8 && num3 > 0)
				{
					num3++;
					if (num3 == 256)
					{
						this.write_ff(array, num2, pos, 2048);
						num3 = 0;
					}
				}
				else if (num3 > 0)
				{
					this.write_ff(array, num2, pos, num3 * 8);
					num3 = 0;
				}
				this.buffer.Seek((long)num6, 1);
			}
			if (num3 == 1)
			{
				this.write_ff(array, num2, pos, 8);
			}
			else if (num3 > 1)
			{
				int num7 = (array != data) ? array.Length : num;
				this.write_ff(array, num2, pos, num7 - num2);
			}
			long num8 = (long)((num + 2047) / 2048 * 2 + num + 2);
			if (num8 < this.buffer.Position)
			{
				SprotoTypeSize.error("packing error, return size=" + this.buffer.Position);
			}
			if ((long)sends.Length < this.buffer.Position)
			{
				int num9 = (int)this.buffer.Position;
				sends = new byte[num9 * 2];
			}
			len = (int)this.buffer.Position;
			this.buffer.Seek(0L, 0);
			this.buffer.Read(sends, 0, len);
			return sends;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000C1AD0 File Offset: 0x000BFCD0
		public byte[] unpack(byte[] data, ref int len)
		{
			this.clear();
			int i = len;
			int num = len;
			while (i > 0)
			{
				byte b = data[len - i];
				i--;
				if (b == 255)
				{
					if (i < 0)
					{
						SprotoTypeSize.error("invalid unpack stream.");
					}
					int num2 = (int)((data[len - i] + 1) * 8);
					if (i < num2 + 1)
					{
						SprotoTypeSize.error("invalid unpack stream.");
					}
					this.buffer.Write(data, len - i + 1, num2);
					i -= num2 + 1;
				}
				else
				{
					for (int j = 0; j < 8; j++)
					{
						int num3 = b >> j & 1;
						if (num3 == 1)
						{
							if (i < 0)
							{
								SprotoTypeSize.error("invalid unpack stream.");
							}
							this.buffer.WriteByte(data[len - i]);
							i--;
						}
						else
						{
							this.buffer.WriteByte(0);
						}
					}
				}
			}
			len = (int)this.buffer.Position;
			this.buffer.Seek(0L, 0);
			this.buffer.Read(data, 0, len);
			return data;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000C1BE8 File Offset: 0x000BFDE8
		private void clear()
		{
			this.buffer.Seek(0L, 0);
			for (int i = 0; i < this.tmp.Length; i++)
			{
				this.tmp[i] = 0;
			}
		}

		// Token: 0x04002138 RID: 8504
		private MemoryStream buffer;

		// Token: 0x04002139 RID: 8505
		private byte[] tmp;
	}
}
