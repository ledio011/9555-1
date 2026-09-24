using System;
using System.Globalization;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200003E RID: 62
	internal class SortKeyBuffer
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000089F8 File Offset: 0x00006BF8
		public SortKeyBuffer(int lcid)
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008A00 File Offset: 0x00006C00
		public void Reset()
		{
			this.l1 = (this.l2 = (this.l3 = (this.l4s = (this.l4t = (this.l4k = (this.l4w = (this.l5 = 0)))))));
			this.frenchSorted = false;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008A5C File Offset: 0x00006C5C
		internal void Initialize(CompareOptions options, int lcid, string s, bool frenchSort)
		{
			this.source = s;
			this.lcid = lcid;
			this.options = options;
			int length = s.Length;
			this.processLevel2 = ((options & CompareOptions.IgnoreNonSpace) == CompareOptions.None);
			this.frenchSort = frenchSort;
			if (this.l1b == null || this.l1b.Length < length)
			{
				this.l1b = new byte[length * 2 + 10];
			}
			if (this.processLevel2 && (this.l2b == null || this.l2b.Length < length))
			{
				this.l2b = new byte[length + 10];
			}
			if (this.l3b == null || this.l3b.Length < length)
			{
				this.l3b = new byte[length + 10];
			}
			if (this.l4sb == null)
			{
				this.l4sb = new byte[10];
			}
			if (this.l4tb == null)
			{
				this.l4tb = new byte[10];
			}
			if (this.l4kb == null)
			{
				this.l4kb = new byte[10];
			}
			if (this.l4wb == null)
			{
				this.l4wb = new byte[10];
			}
			if (this.l5b == null)
			{
				this.l5b = new byte[10];
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008B98 File Offset: 0x00006D98
		internal void AppendCJKExtension(byte lv1msb, byte lv1lsb)
		{
			this.AppendBufferPrimitive(254, ref this.l1b, ref this.l1);
			this.AppendBufferPrimitive(byte.MaxValue, ref this.l1b, ref this.l1);
			this.AppendBufferPrimitive(lv1msb, ref this.l1b, ref this.l1);
			this.AppendBufferPrimitive(lv1lsb, ref this.l1b, ref this.l1);
			if (this.processLevel2)
			{
				this.AppendBufferPrimitive(2, ref this.l2b, ref this.l2);
			}
			this.AppendBufferPrimitive(2, ref this.l3b, ref this.l3);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008C2C File Offset: 0x00006E2C
		internal void AppendKana(byte category, byte lv1, byte lv2, byte lv3, bool isSmallKana, byte markType, bool isKatakana, bool isHalfWidth)
		{
			this.AppendNormal(category, lv1, lv2, lv3);
			this.AppendBufferPrimitive((!isSmallKana) ? 228 : 196, ref this.l4sb, ref this.l4s);
			this.AppendBufferPrimitive(markType, ref this.l4tb, ref this.l4t);
			this.AppendBufferPrimitive((!isKatakana) ? 228 : 196, ref this.l4kb, ref this.l4k);
			this.AppendBufferPrimitive((!isHalfWidth) ? 228 : 196, ref this.l4wb, ref this.l4w);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008CD4 File Offset: 0x00006ED4
		internal void AppendNormal(byte category, byte lv1, byte lv2, byte lv3)
		{
			if (lv2 == 0)
			{
				lv2 = 2;
			}
			if (lv3 == 0)
			{
				lv3 = 2;
			}
			if (category == 6 && (this.options & CompareOptions.StringSort) == CompareOptions.None)
			{
				this.AppendLevel5(category, lv1);
				return;
			}
			if (this.processLevel2 && category == 1 && this.l1 > 0)
			{
				lv2 += this.l2b[--this.l2];
				lv3 = this.l3b[--this.l3];
			}
			if (category != 1)
			{
				this.AppendBufferPrimitive(category, ref this.l1b, ref this.l1);
				this.AppendBufferPrimitive(lv1, ref this.l1b, ref this.l1);
			}
			if (this.processLevel2)
			{
				this.AppendBufferPrimitive(lv2, ref this.l2b, ref this.l2);
			}
			this.AppendBufferPrimitive(lv3, ref this.l3b, ref this.l3);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008DCC File Offset: 0x00006FCC
		private void AppendLevel5(byte category, byte lv1)
		{
			int num = (this.l2 + 1) % 8192;
			this.AppendBufferPrimitive((byte)(num / 64 + 128), ref this.l5b, ref this.l5);
			this.AppendBufferPrimitive((byte)(num % 64 * 4 + 3), ref this.l5b, ref this.l5);
			this.AppendBufferPrimitive(category, ref this.l5b, ref this.l5);
			this.AppendBufferPrimitive(lv1, ref this.l5b, ref this.l5);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008E48 File Offset: 0x00007048
		private void AppendBufferPrimitive(byte value, ref byte[] buf, ref int bidx)
		{
			buf[bidx++] = value;
			if (bidx == buf.Length)
			{
				byte[] array = new byte[bidx * 2];
				Array.Copy(buf, array, buf.Length);
				buf = array;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008E88 File Offset: 0x00007088
		public SortKey GetResultAndReset()
		{
			SortKey result = this.GetResult();
			this.Reset();
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008EA4 File Offset: 0x000070A4
		private int GetOptimizedLength(byte[] data, int len, byte defaultValue)
		{
			int num = -1;
			for (int i = 0; i < len; i++)
			{
				if (data[i] != defaultValue)
				{
					num = i;
				}
			}
			return num + 1;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008ED4 File Offset: 0x000070D4
		public SortKey GetResult()
		{
			if (this.frenchSort && !this.frenchSorted && this.l2b != null)
			{
				int i;
				for (i = 0; i < this.l2b.Length; i++)
				{
					if (this.l2b[i] == 0)
					{
						break;
					}
				}
				Array.Reverse(this.l2b, 0, i);
				this.frenchSorted = true;
			}
			this.l2 = this.GetOptimizedLength(this.l2b, this.l2, 2);
			this.l3 = this.GetOptimizedLength(this.l3b, this.l3, 2);
			bool flag = this.l4s > 0;
			this.l4s = this.GetOptimizedLength(this.l4sb, this.l4s, 228);
			this.l4t = this.GetOptimizedLength(this.l4tb, this.l4t, 3);
			this.l4k = this.GetOptimizedLength(this.l4kb, this.l4k, 228);
			this.l4w = this.GetOptimizedLength(this.l4wb, this.l4w, 228);
			this.l5 = this.GetOptimizedLength(this.l5b, this.l5, 2);
			int num = this.l1 + this.l2 + this.l3 + this.l5 + 5;
			int num2 = this.l4s + this.l4t + this.l4k + this.l4w;
			if (flag)
			{
				num += num2 + 4;
			}
			byte[] array = new byte[num];
			Array.Copy(this.l1b, array, this.l1);
			array[this.l1] = 1;
			int num3 = this.l1 + 1;
			if (this.l2 > 0)
			{
				Array.Copy(this.l2b, 0, array, num3, this.l2);
			}
			num3 += this.l2;
			array[num3++] = 1;
			if (this.l3 > 0)
			{
				Array.Copy(this.l3b, 0, array, num3, this.l3);
			}
			num3 += this.l3;
			array[num3++] = 1;
			if (flag)
			{
				Array.Copy(this.l4sb, 0, array, num3, this.l4s);
				num3 += this.l4s;
				array[num3++] = byte.MaxValue;
				Array.Copy(this.l4tb, 0, array, num3, this.l4t);
				num3 += this.l4t;
				array[num3++] = 2;
				Array.Copy(this.l4kb, 0, array, num3, this.l4k);
				num3 += this.l4k;
				array[num3++] = byte.MaxValue;
				Array.Copy(this.l4wb, 0, array, num3, this.l4w);
				num3 += this.l4w;
				array[num3++] = byte.MaxValue;
			}
			array[num3++] = 1;
			if (this.l5 > 0)
			{
				Array.Copy(this.l5b, 0, array, num3, this.l5);
			}
			num3 += this.l5;
			array[num3++] = 0;
			return new SortKey(this.lcid, this.source, array, this.options, this.l1, this.l2, this.l3, this.l4s, this.l4t, this.l4k, this.l4w, this.l5);
		}

		// Token: 0x040000E8 RID: 232
		private int l1;

		// Token: 0x040000E9 RID: 233
		private int l2;

		// Token: 0x040000EA RID: 234
		private int l3;

		// Token: 0x040000EB RID: 235
		private int l4s;

		// Token: 0x040000EC RID: 236
		private int l4t;

		// Token: 0x040000ED RID: 237
		private int l4k;

		// Token: 0x040000EE RID: 238
		private int l4w;

		// Token: 0x040000EF RID: 239
		private int l5;

		// Token: 0x040000F0 RID: 240
		private byte[] l1b;

		// Token: 0x040000F1 RID: 241
		private byte[] l2b;

		// Token: 0x040000F2 RID: 242
		private byte[] l3b;

		// Token: 0x040000F3 RID: 243
		private byte[] l4sb;

		// Token: 0x040000F4 RID: 244
		private byte[] l4tb;

		// Token: 0x040000F5 RID: 245
		private byte[] l4kb;

		// Token: 0x040000F6 RID: 246
		private byte[] l4wb;

		// Token: 0x040000F7 RID: 247
		private byte[] l5b;

		// Token: 0x040000F8 RID: 248
		private string source;

		// Token: 0x040000F9 RID: 249
		private bool processLevel2;

		// Token: 0x040000FA RID: 250
		private bool frenchSort;

		// Token: 0x040000FB RID: 251
		private bool frenchSorted;

		// Token: 0x040000FC RID: 252
		private int lcid;

		// Token: 0x040000FD RID: 253
		private CompareOptions options;
	}
}
