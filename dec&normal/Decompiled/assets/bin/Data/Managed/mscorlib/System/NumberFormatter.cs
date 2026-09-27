using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x0200016A RID: 362
	internal sealed class NumberFormatter
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0003417C File Offset: 0x0003237C
		public NumberFormatter(Thread current)
		{
			this._cbuf = new char[0];
			if (current == null)
			{
				return;
			}
			this._thread = current;
			this.CurrentCulture = this._thread.CurrentCulture;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000341B0 File Offset: 0x000323B0
		static NumberFormatter()
		{
			NumberFormatter.GetFormatterTables(out NumberFormatter.MantissaBitsTable, out NumberFormatter.TensExponentTable, out NumberFormatter.DigitLowerTable, out NumberFormatter.DigitUpperTable, out NumberFormatter.TenPowersList, out NumberFormatter.DecHexDigits);
		}

		// Token: 0x06000D8D RID: 3469
		[MethodImpl(4096)]
		private unsafe static extern void GetFormatterTables(out ulong* MantissaBitsTable, out int* TensExponentTable, out char* DigitLowerTable, out char* DigitUpperTable, out long* TenPowersList, out int* DecHexDigits);

		// Token: 0x06000D8E RID: 3470 RVA: 0x000341D8 File Offset: 0x000323D8
		private unsafe static long GetTenPowerOf(int i)
		{
			return NumberFormatter.TenPowersList[i];
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000341E4 File Offset: 0x000323E4
		private void InitDecHexDigits(uint value)
		{
			if (value >= 100000000U)
			{
				int num = (int)(value / 100000000U);
				value -= (uint)(100000000 * num);
				this._val2 = NumberFormatter.FastToDecHex(num);
			}
			this._val1 = NumberFormatter.ToDecHex((int)value);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00034228 File Offset: 0x00032428
		private void InitDecHexDigits(ulong value)
		{
			if (value >= 100000000UL)
			{
				long num = (long)(value / 100000000UL);
				value -= (ulong)(100000000L * num);
				if (num >= 100000000L)
				{
					int num2 = (int)(num / 100000000L);
					num -= (long)num2 * 100000000L;
					this._val3 = NumberFormatter.ToDecHex(num2);
				}
				if (num != 0L)
				{
					this._val2 = NumberFormatter.ToDecHex((int)num);
				}
			}
			if (value != 0UL)
			{
				this._val1 = NumberFormatter.ToDecHex((int)value);
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000342AC File Offset: 0x000324AC
		private void InitDecHexDigits(uint hi, ulong lo)
		{
			if (hi == 0U)
			{
				this.InitDecHexDigits(lo);
				return;
			}
			uint num = hi / 100000000U;
			ulong num2 = (ulong)(hi - num * 100000000U);
			ulong num3 = lo / 100000000UL;
			ulong num4 = lo - num3 * 100000000UL + num2 * 9551616UL;
			hi = num;
			lo = num3 + num2 * 184467440737UL;
			num3 = num4 / 100000000UL;
			num4 -= num3 * 100000000UL;
			lo += num3;
			this._val1 = NumberFormatter.ToDecHex((int)num4);
			num3 = lo / 100000000UL;
			num4 = lo - num3 * 100000000UL;
			lo = num3;
			if (hi != 0U)
			{
				lo += (ulong)hi * 184467440737UL;
				num4 += (ulong)hi * 9551616UL;
				num3 = num4 / 100000000UL;
				lo += num3;
				num4 -= num3 * 100000000UL;
			}
			this._val2 = NumberFormatter.ToDecHex((int)num4);
			if (lo >= 100000000UL)
			{
				num3 = lo / 100000000UL;
				lo -= num3 * 100000000UL;
				this._val4 = NumberFormatter.ToDecHex((int)num3);
			}
			this._val3 = NumberFormatter.ToDecHex((int)lo);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000343CC File Offset: 0x000325CC
		private unsafe static uint FastToDecHex(int val)
		{
			if (val < 100)
			{
				return (uint)NumberFormatter.DecHexDigits[val];
			}
			int num = val * 5243 >> 19;
			return (uint)(NumberFormatter.DecHexDigits[num] << 8 | NumberFormatter.DecHexDigits[val - num * 100]);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00034414 File Offset: 0x00032614
		private static uint ToDecHex(int val)
		{
			uint num = 0U;
			if (val >= 10000)
			{
				int num2 = val / 10000;
				val -= num2 * 10000;
				num = NumberFormatter.FastToDecHex(num2) << 16;
			}
			return num | NumberFormatter.FastToDecHex(val);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00034454 File Offset: 0x00032654
		private static int FastDecHexLen(int val)
		{
			if (val < 256)
			{
				if (val < 16)
				{
					return 1;
				}
				return 2;
			}
			else
			{
				if (val < 4096)
				{
					return 3;
				}
				return 4;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003447C File Offset: 0x0003267C
		private static int DecHexLen(uint val)
		{
			if (val < 65536U)
			{
				return NumberFormatter.FastDecHexLen((int)val);
			}
			return 4 + NumberFormatter.FastDecHexLen((int)(val >> 16));
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003449C File Offset: 0x0003269C
		private int DecHexLen()
		{
			if (this._val4 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val4) + 24;
			}
			if (this._val3 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val3) + 16;
			}
			if (this._val2 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val2) + 8;
			}
			if (this._val1 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val1);
			}
			return 0;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00034510 File Offset: 0x00032710
		private static int ScaleOrder(long hi)
		{
			for (int i = 18; i >= 0; i--)
			{
				if (hi >= NumberFormatter.GetTenPowerOf(i))
				{
					return i + 1;
				}
			}
			return 1;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00034544 File Offset: 0x00032744
		private int InitialFloatingPrecision()
		{
			if (this._specifier == 'R')
			{
				return this._defPrecision + 2;
			}
			if (this._precision < this._defPrecision)
			{
				return this._defPrecision;
			}
			if (this._specifier == 'G')
			{
				return Math.Min(this._defPrecision + 2, this._precision);
			}
			if (this._specifier == 'E')
			{
				return Math.Min(this._defPrecision + 2, this._precision + 1);
			}
			return this._defPrecision;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000345CC File Offset: 0x000327CC
		private static int ParsePrecision(string format)
		{
			int num = 0;
			for (int i = 1; i < format.Length; i++)
			{
				int num2 = (int)(format[i] - '0');
				num = num * 10 + num2;
				if (num2 < 0 || num2 > 9 || num > 99)
				{
					return -2;
				}
			}
			return num;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00034620 File Offset: 0x00032820
		private void Init(string format)
		{
			this._val1 = (this._val2 = (this._val3 = (this._val4 = 0U)));
			this._offset = 0;
			this._NaN = (this._infinity = false);
			this._isCustomFormat = false;
			this._specifierIsUpper = true;
			this._precision = -1;
			if (format == null || format.Length == 0)
			{
				this._specifier = 'G';
				return;
			}
			char c = format[0];
			if (c >= 'a' && c <= 'z')
			{
				c = c - 'a' + 'A';
				this._specifierIsUpper = false;
			}
			else if (c < 'A' || c > 'Z')
			{
				this._isCustomFormat = true;
				this._specifier = '0';
				return;
			}
			this._specifier = c;
			if (format.Length > 1)
			{
				this._precision = NumberFormatter.ParsePrecision(format);
				if (this._precision == -2)
				{
					this._isCustomFormat = true;
					this._specifier = '0';
					this._precision = -1;
				}
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00034724 File Offset: 0x00032924
		private void InitHex(ulong value)
		{
			int defPrecision = this._defPrecision;
			switch (defPrecision)
			{
			case 3:
				value = (ulong)((byte)value);
				break;
			default:
				if (defPrecision == 10)
				{
					value = (ulong)((uint)value);
				}
				break;
			case 5:
				value = (ulong)((ushort)value);
				break;
			}
			this._val1 = (uint)value;
			this._val2 = (uint)(value >> 32);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
			if (value == 0UL)
			{
				this._decPointPos = 1;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000347AC File Offset: 0x000329AC
		private void Init(string format, int value, int defPrecision)
		{
			this.Init(format);
			this._defPrecision = defPrecision;
			this._positive = (value >= 0);
			if (value == 0 || this._specifier == 'X')
			{
				this.InitHex((ulong)((long)value));
				return;
			}
			if (value < 0)
			{
				value = -value;
			}
			this.InitDecHexDigits((uint)value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00034818 File Offset: 0x00032A18
		private void Init(string format, uint value, int defPrecision)
		{
			this.Init(format);
			this._defPrecision = defPrecision;
			this._positive = true;
			if (value == 0U || this._specifier == 'X')
			{
				this.InitHex((ulong)value);
				return;
			}
			this.InitDecHexDigits(value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00034874 File Offset: 0x00032A74
		private void Init(string format, long value)
		{
			this.Init(format);
			this._defPrecision = 19;
			this._positive = (value >= 0L);
			if (value == 0L || this._specifier == 'X')
			{
				this.InitHex((ulong)value);
				return;
			}
			if (value < 0L)
			{
				value = -value;
			}
			this.InitDecHexDigits((ulong)value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000348E4 File Offset: 0x00032AE4
		private void Init(string format, ulong value)
		{
			this.Init(format);
			this._defPrecision = 20;
			this._positive = true;
			if (value == 0UL || this._specifier == 'X')
			{
				this.InitHex(value);
				return;
			}
			this.InitDecHexDigits(value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00034940 File Offset: 0x00032B40
		private unsafe void Init(string format, double value, int defPrecision)
		{
			this.Init(format);
			this._defPrecision = defPrecision;
			long num = BitConverter.DoubleToInt64Bits(value);
			this._positive = (num >= 0L);
			num &= long.MaxValue;
			if (num == 0L)
			{
				this._decPointPos = 1;
				this._digitsLen = 0;
				this._positive = true;
				return;
			}
			int num2 = (int)(num >> 52);
			long num3 = num & 4503599627370495L;
			if (num2 == 2047)
			{
				this._NaN = (num3 != 0L);
				this._infinity = (num3 == 0L);
				return;
			}
			int num4 = 0;
			if (num2 == 0)
			{
				num2 = 1;
				int num5 = NumberFormatter.ScaleOrder(num3);
				if (num5 < 15)
				{
					num4 = num5 - 15;
					num3 *= NumberFormatter.GetTenPowerOf(-num4);
				}
			}
			else
			{
				num3 = (num3 + 4503599627370495L + 1L) * 10L;
				num4 = -1;
			}
			ulong num6 = (ulong)((uint)num3);
			ulong num7 = (ulong)num3 >> 32;
			ulong num8 = NumberFormatter.MantissaBitsTable[num2];
			ulong num9 = num8 >> 32;
			num8 = (ulong)((uint)num8);
			ulong num10 = num7 * num8 + num6 * num9 + (num6 * num8 >> 32);
			long num11 = (long)(num7 * num9 + (num10 >> 32));
			while (num11 < 10000000000000000L)
			{
				num10 = (num10 & (ulong)-1) * 10UL;
				num11 = num11 * 10L + (long)(num10 >> 32);
				num4--;
			}
			if ((num10 & (ulong)-2147483648) != 0UL)
			{
				num11 += 1L;
			}
			int num12 = 17;
			this._decPointPos = NumberFormatter.TensExponentTable[num2] + num4 + num12;
			int num13 = this.InitialFloatingPrecision();
			if (num12 > num13)
			{
				long tenPowerOf = NumberFormatter.GetTenPowerOf(num12 - num13);
				num11 = (num11 + (tenPowerOf >> 1)) / tenPowerOf;
				num12 = num13;
			}
			if (num11 >= NumberFormatter.GetTenPowerOf(num12))
			{
				num12++;
				this._decPointPos++;
			}
			this.InitDecHexDigits((ulong)num11);
			this._offset = this.CountTrailingZeros();
			this._digitsLen = num12 - this._offset;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00034B30 File Offset: 0x00032D30
		private void Init(string format, decimal value)
		{
			this.Init(format);
			this._defPrecision = 100;
			int[] bits = decimal.GetBits(value);
			int num = (bits[3] & 2031616) >> 16;
			this._positive = (bits[3] >= 0);
			if (bits[0] == 0 && bits[1] == 0 && bits[2] == 0)
			{
				this._decPointPos = -num;
				this._positive = true;
				this._digitsLen = 0;
				return;
			}
			this.InitDecHexDigits((uint)bits[2], (ulong)((long)bits[1] << 32 | (long)((ulong)bits[0])));
			this._digitsLen = this.DecHexLen();
			this._decPointPos = this._digitsLen - num;
			if (this._precision != -1 || this._specifier != 'G')
			{
				this._offset = this.CountTrailingZeros();
				this._digitsLen -= this._offset;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00034C08 File Offset: 0x00032E08
		private void ResetCharBuf(int size)
		{
			this._ind = 0;
			if (this._cbuf.Length < size)
			{
				this._cbuf = new char[size];
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00034C2C File Offset: 0x00032E2C
		private void Resize(int len)
		{
			char[] array = new char[len];
			Array.Copy(this._cbuf, array, this._ind);
			this._cbuf = array;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00034C5C File Offset: 0x00032E5C
		private void Append(char c)
		{
			if (this._ind == this._cbuf.Length)
			{
				this.Resize(this._ind + 10);
			}
			this._cbuf[this._ind++] = c;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00034CA4 File Offset: 0x00032EA4
		private void Append(char c, int cnt)
		{
			if (this._ind + cnt > this._cbuf.Length)
			{
				this.Resize(this._ind + cnt + 10);
			}
			while (cnt-- > 0)
			{
				this._cbuf[this._ind++] = c;
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00034D04 File Offset: 0x00032F04
		private void Append(string s)
		{
			int length = s.Length;
			if (this._ind + length > this._cbuf.Length)
			{
				this.Resize(this._ind + length + 10);
			}
			for (int i = 0; i < length; i++)
			{
				this._cbuf[this._ind++] = s[i];
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00034D70 File Offset: 0x00032F70
		private NumberFormatInfo GetNumberFormatInstance(IFormatProvider fp)
		{
			if (this._nfi != null && fp == null)
			{
				return this._nfi;
			}
			return NumberFormatInfo.GetInstance(fp);
		}

		// Token: 0x170001F5 RID: 501
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00034D90 File Offset: 0x00032F90
		public CultureInfo CurrentCulture
		{
			set
			{
				if (value != null && value.IsReadOnly)
				{
					this._nfi = value.NumberFormat;
				}
				else
				{
					this._nfi = null;
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00034DBC File Offset: 0x00032FBC
		private int IntegerDigits
		{
			get
			{
				return (this._decPointPos <= 0) ? 1 : this._decPointPos;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00034DD8 File Offset: 0x00032FD8
		private int DecimalDigits
		{
			get
			{
				return (this._digitsLen <= this._decPointPos) ? 0 : (this._digitsLen - this._decPointPos);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00034E00 File Offset: 0x00033000
		private bool IsFloatingSource
		{
			get
			{
				return this._defPrecision == 15 || this._defPrecision == 7;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00034E1C File Offset: 0x0003301C
		private bool IsZero
		{
			get
			{
				return this._digitsLen == 0;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00034E28 File Offset: 0x00033028
		private bool IsZeroInteger
		{
			get
			{
				return this._digitsLen == 0 || this._decPointPos <= 0;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00034E44 File Offset: 0x00033044
		private void RoundPos(int pos)
		{
			this.RoundBits(this._digitsLen - pos);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00034E58 File Offset: 0x00033058
		private bool RoundDecimal(int decimals)
		{
			return this.RoundBits(this._digitsLen - this._decPointPos - decimals);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00034E70 File Offset: 0x00033070
		private bool RoundBits(int shift)
		{
			if (shift <= 0)
			{
				return false;
			}
			if (shift > this._digitsLen)
			{
				this._digitsLen = 0;
				this._decPointPos = 1;
				this._val1 = (this._val2 = (this._val3 = (this._val4 = 0U)));
				this._positive = true;
				return false;
			}
			shift += this._offset;
			this._digitsLen += this._offset;
			while (shift > 8)
			{
				this._val1 = this._val2;
				this._val2 = this._val3;
				this._val3 = this._val4;
				this._val4 = 0U;
				this._digitsLen -= 8;
				shift -= 8;
			}
			shift = shift - 1 << 2;
			uint num = this._val1 >> shift;
			uint num2 = num & 15U;
			this._val1 = (num ^ num2) << shift;
			bool result = false;
			if (num2 >= 5U)
			{
				this._val1 |= 2576980377U >> 28 - shift;
				this.AddOneToDecHex();
				int num3 = this.DecHexLen();
				result = (num3 != this._digitsLen);
				this._decPointPos = this._decPointPos + num3 - this._digitsLen;
				this._digitsLen = num3;
			}
			this.RemoveTrailingZeros();
			return result;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00034FC4 File Offset: 0x000331C4
		private void RemoveTrailingZeros()
		{
			this._offset = this.CountTrailingZeros();
			this._digitsLen -= this._offset;
			if (this._digitsLen == 0)
			{
				this._offset = 0;
				this._decPointPos = 1;
				this._positive = true;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00035010 File Offset: 0x00033210
		private void AddOneToDecHex()
		{
			if (this._val1 == 2576980377U)
			{
				this._val1 = 0U;
				if (this._val2 == 2576980377U)
				{
					this._val2 = 0U;
					if (this._val3 == 2576980377U)
					{
						this._val3 = 0U;
						this._val4 = NumberFormatter.AddOneToDecHex(this._val4);
					}
					else
					{
						this._val3 = NumberFormatter.AddOneToDecHex(this._val3);
					}
				}
				else
				{
					this._val2 = NumberFormatter.AddOneToDecHex(this._val2);
				}
			}
			else
			{
				this._val1 = NumberFormatter.AddOneToDecHex(this._val1);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000350B8 File Offset: 0x000332B8
		private static uint AddOneToDecHex(uint val)
		{
			if ((val & 65535U) == 39321U)
			{
				if ((val & 16777215U) == 10066329U)
				{
					if ((val & 268435455U) == 161061273U)
					{
						return val + 107374183U;
					}
					return val + 6710887U;
				}
				else
				{
					if ((val & 1048575U) == 629145U)
					{
						return val + 419431U;
					}
					return val + 26215U;
				}
			}
			else if ((val & 255U) == 153U)
			{
				if ((val & 4095U) == 2457U)
				{
					return val + 1639U;
				}
				return val + 103U;
			}
			else
			{
				if ((val & 15U) == 9U)
				{
					return val + 7U;
				}
				return val + 1U;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003516C File Offset: 0x0003336C
		private int CountTrailingZeros()
		{
			if (this._val1 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val1);
			}
			if (this._val2 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val2) + 8;
			}
			if (this._val3 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val3) + 16;
			}
			if (this._val4 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val4) + 24;
			}
			return this._digitsLen;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000351E4 File Offset: 0x000333E4
		private static int CountTrailingZeros(uint val)
		{
			if ((val & 65535U) == 0U)
			{
				if ((val & 16777215U) == 0U)
				{
					if ((val & 268435455U) == 0U)
					{
						return 7;
					}
					return 6;
				}
				else
				{
					if ((val & 1048575U) == 0U)
					{
						return 5;
					}
					return 4;
				}
			}
			else if ((val & 255U) == 0U)
			{
				if ((val & 4095U) == 0U)
				{
					return 3;
				}
				return 2;
			}
			else
			{
				if ((val & 15U) == 0U)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00035254 File Offset: 0x00033454
		private static NumberFormatter GetInstance()
		{
			NumberFormatter numberFormatter = NumberFormatter.threadNumberFormatter;
			NumberFormatter.threadNumberFormatter = null;
			if (numberFormatter == null)
			{
				return new NumberFormatter(Thread.CurrentThread);
			}
			return numberFormatter;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00035280 File Offset: 0x00033480
		private void Release()
		{
			NumberFormatter.threadNumberFormatter = this;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00035288 File Offset: 0x00033488
		internal static void SetThreadCurrentCulture(CultureInfo culture)
		{
			if (NumberFormatter.threadNumberFormatter != null)
			{
				NumberFormatter.threadNumberFormatter.CurrentCulture = culture;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000352A0 File Offset: 0x000334A0
		public static string NumberToString(string format, sbyte value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 3);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000352D0 File Offset: 0x000334D0
		public static string NumberToString(string format, byte value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 3);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000352FC File Offset: 0x000334FC
		public static string NumberToString(string format, ushort value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 5);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00035328 File Offset: 0x00033528
		public static string NumberToString(string format, short value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 5);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00035354 File Offset: 0x00033554
		public static string NumberToString(string format, uint value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value, 10);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00035384 File Offset: 0x00033584
		public static string NumberToString(string format, int value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value, 10);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000353B4 File Offset: 0x000335B4
		public static string NumberToString(string format, ulong value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000353E0 File Offset: 0x000335E0
		public static string NumberToString(string format, long value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003540C File Offset: 0x0003360C
		public static string NumberToString(string format, float value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (double)value, 7);
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else if (instance._specifier == 'R')
			{
				result = instance.FormatRoundtrip(value, numberFormatInstance);
			}
			else
			{
				result = instance.NumberToString(format, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000354A4 File Offset: 0x000336A4
		public static string NumberToString(string format, double value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value, 15);
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else if (instance._specifier == 'R')
			{
				result = instance.FormatRoundtrip(value, numberFormatInstance);
			}
			else
			{
				result = instance.NumberToString(format, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003553C File Offset: 0x0003373C
		public static string NumberToString(string format, decimal value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value);
			string result = instance.NumberToString(format, instance.GetNumberFormatInstance(fp));
			instance.Release();
			return result;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00035570 File Offset: 0x00033770
		public static string NumberToString(uint value, IFormatProvider fp)
		{
			if (value >= 100000000U)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString((int)value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000355A8 File Offset: 0x000337A8
		public static string NumberToString(int value, IFormatProvider fp)
		{
			if (value >= 100000000 || value <= -100000000)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString(value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000355EC File Offset: 0x000337EC
		public static string NumberToString(ulong value, IFormatProvider fp)
		{
			if (value >= 100000000UL)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString((int)value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00035628 File Offset: 0x00033828
		public static string NumberToString(long value, IFormatProvider fp)
		{
			if (value >= 100000000L || value <= -100000000L)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString((int)value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00035670 File Offset: 0x00033870
		public static string NumberToString(float value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(null, (double)value, 7);
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else
			{
				result = instance.FormatGeneral(-1, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000356EC File Offset: 0x000338EC
		public static string NumberToString(double value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			instance.Init(null, value, 15);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else
			{
				result = instance.FormatGeneral(-1, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00035768 File Offset: 0x00033968
		private string FastIntegerToString(int value, IFormatProvider fp)
		{
			if (value < 0)
			{
				string negativeSign = this.GetNumberFormatInstance(fp).NegativeSign;
				this.ResetCharBuf(8 + negativeSign.Length);
				value = -value;
				this.Append(negativeSign);
			}
			else
			{
				this.ResetCharBuf(8);
			}
			if (value >= 10000)
			{
				int num = value / 10000;
				this.FastAppendDigits(num, false);
				this.FastAppendDigits(value - num * 10000, true);
			}
			else
			{
				this.FastAppendDigits(value, false);
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000357F8 File Offset: 0x000339F8
		private string IntegerToString(string format, IFormatProvider fp)
		{
			NumberFormatInfo numberFormatInstance = this.GetNumberFormatInstance(fp);
			char specifier = this._specifier;
			switch (specifier)
			{
			case 'C':
				return this.FormatCurrency(this._precision, numberFormatInstance);
			case 'D':
				return this.FormatDecimal(this._precision, numberFormatInstance);
			case 'E':
				return this.FormatExponential(this._precision, numberFormatInstance);
			case 'F':
				return this.FormatFixedPoint(this._precision, numberFormatInstance);
			case 'G':
				if (this._precision <= 0)
				{
					return this.FormatDecimal(-1, numberFormatInstance);
				}
				return this.FormatGeneral(this._precision, numberFormatInstance);
			default:
				if (specifier == 'X')
				{
					return this.FormatHexadecimal(this._precision);
				}
				if (this._isCustomFormat)
				{
					return this.FormatCustom(format, numberFormatInstance);
				}
				throw new FormatException("The specified format '" + format + "' is invalid");
			case 'N':
				return this.FormatNumber(this._precision, numberFormatInstance);
			case 'P':
				return this.FormatPercent(this._precision, numberFormatInstance);
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00035910 File Offset: 0x00033B10
		private string NumberToString(string format, NumberFormatInfo nfi)
		{
			char specifier = this._specifier;
			switch (specifier)
			{
			case 'C':
				return this.FormatCurrency(this._precision, nfi);
			default:
				switch (specifier)
				{
				case 'N':
					return this.FormatNumber(this._precision, nfi);
				default:
					if (specifier != 'X')
					{
					}
					if (this._isCustomFormat)
					{
						return this.FormatCustom(format, nfi);
					}
					throw new FormatException("The specified format '" + format + "' is invalid");
				case 'P':
					return this.FormatPercent(this._precision, nfi);
				}
				break;
			case 'E':
				return this.FormatExponential(this._precision, nfi);
			case 'F':
				return this.FormatFixedPoint(this._precision, nfi);
			case 'G':
				return this.FormatGeneral(this._precision, nfi);
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000359E0 File Offset: 0x00033BE0
		public string FormatCurrency(int precision, NumberFormatInfo nfi)
		{
			precision = ((precision < 0) ? nfi.CurrencyDecimalDigits : precision);
			this.RoundDecimal(precision);
			this.ResetCharBuf(this.IntegerDigits * 2 + precision * 2 + 16);
			if (this._positive)
			{
				switch (nfi.CurrencyPositivePattern)
				{
				case 0:
					this.Append(nfi.CurrencySymbol);
					break;
				case 2:
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				}
			}
			else
			{
				switch (nfi.CurrencyNegativePattern)
				{
				case 0:
					this.Append('(');
					this.Append(nfi.CurrencySymbol);
					break;
				case 1:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.CurrencySymbol);
					break;
				case 2:
					this.Append(nfi.CurrencySymbol);
					this.Append(nfi.NegativeSign);
					break;
				case 3:
					this.Append(nfi.CurrencySymbol);
					break;
				case 4:
					this.Append('(');
					break;
				case 5:
					this.Append(nfi.NegativeSign);
					break;
				case 8:
					this.Append(nfi.NegativeSign);
					break;
				case 9:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				case 11:
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				case 12:
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					this.Append(nfi.NegativeSign);
					break;
				case 14:
					this.Append('(');
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				case 15:
					this.Append('(');
					break;
				}
			}
			this.AppendIntegerStringWithGroupSeparator(nfi.RawCurrencyGroupSizes, nfi.CurrencyGroupSeparator);
			if (precision > 0)
			{
				this.Append(nfi.CurrencyDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			if (this._positive)
			{
				switch (nfi.CurrencyPositivePattern)
				{
				case 1:
					this.Append(nfi.CurrencySymbol);
					break;
				case 3:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					break;
				}
			}
			else
			{
				switch (nfi.CurrencyNegativePattern)
				{
				case 0:
					this.Append(')');
					break;
				case 3:
					this.Append(nfi.NegativeSign);
					break;
				case 4:
					this.Append(nfi.CurrencySymbol);
					this.Append(')');
					break;
				case 5:
					this.Append(nfi.CurrencySymbol);
					break;
				case 6:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.CurrencySymbol);
					break;
				case 7:
					this.Append(nfi.CurrencySymbol);
					this.Append(nfi.NegativeSign);
					break;
				case 8:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					break;
				case 10:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					this.Append(nfi.NegativeSign);
					break;
				case 11:
					this.Append(nfi.NegativeSign);
					break;
				case 13:
					this.Append(nfi.NegativeSign);
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					break;
				case 14:
					this.Append(')');
					break;
				case 15:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					this.Append(')');
					break;
				}
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00035DFC File Offset: 0x00033FFC
		private string FormatDecimal(int precision, NumberFormatInfo nfi)
		{
			if (precision < this._digitsLen)
			{
				precision = this._digitsLen;
			}
			if (precision == 0)
			{
				return "0";
			}
			this.ResetCharBuf(precision + 1);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			this.AppendDigits(0, precision);
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00035E64 File Offset: 0x00034064
		private unsafe string FormatHexadecimal(int precision)
		{
			int i = Math.Max(precision, this._decPointPos);
			char* ptr = (!this._specifierIsUpper) ? NumberFormatter.DigitLowerTable : NumberFormatter.DigitUpperTable;
			this.ResetCharBuf(i);
			this._ind = i;
			ulong num = (ulong)this._val1 | (ulong)this._val2 << 32;
			while (i > 0)
			{
				this._cbuf[--i] = ptr[(num & 15UL) * 2UL / 2UL];
				num >>= 4;
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00035EF4 File Offset: 0x000340F4
		public string FormatFixedPoint(int precision, NumberFormatInfo nfi)
		{
			if (precision == -1)
			{
				precision = nfi.NumberDecimalDigits;
			}
			this.RoundDecimal(precision);
			this.ResetCharBuf(this.IntegerDigits + precision + 2);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			this.AppendIntegerString(this.IntegerDigits);
			if (precision > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00035F78 File Offset: 0x00034178
		private string FormatRoundtrip(double origval, NumberFormatInfo nfi)
		{
			NumberFormatter clone = this.GetClone();
			if (origval >= -1.79769313486231E+308 && origval <= 1.79769313486231E+308)
			{
				string text = this.FormatGeneral(this._defPrecision, nfi);
				if (origval == double.Parse(text, nfi))
				{
					return text;
				}
			}
			return clone.FormatGeneral(this._defPrecision + 2, nfi);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00035FD8 File Offset: 0x000341D8
		private string FormatRoundtrip(float origval, NumberFormatInfo nfi)
		{
			NumberFormatter clone = this.GetClone();
			string text = this.FormatGeneral(this._defPrecision, nfi);
			if (origval == float.Parse(text, nfi))
			{
				return text;
			}
			return clone.FormatGeneral(this._defPrecision + 2, nfi);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00036018 File Offset: 0x00034218
		private string FormatGeneral(int precision, NumberFormatInfo nfi)
		{
			bool flag;
			if (precision == -1)
			{
				flag = this.IsFloatingSource;
				precision = this._defPrecision;
			}
			else
			{
				flag = true;
				if (precision == 0)
				{
					precision = this._defPrecision;
				}
				this.RoundPos(precision);
			}
			int num = this._decPointPos;
			int digitsLen = this._digitsLen;
			int num2 = digitsLen - num;
			if ((num > precision || num <= -4) && flag)
			{
				return this.FormatExponential(digitsLen - 1, nfi, 2);
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num < 0)
			{
				num = 0;
			}
			this.ResetCharBuf(num2 + num + 3);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			if (num == 0)
			{
				this.Append('0');
			}
			else
			{
				this.AppendDigits(digitsLen - num, digitsLen);
			}
			if (num2 > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDigits(0, num2);
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00036108 File Offset: 0x00034308
		public string FormatNumber(int precision, NumberFormatInfo nfi)
		{
			precision = ((precision < 0) ? nfi.NumberDecimalDigits : precision);
			this.ResetCharBuf(this.IntegerDigits * 3 + precision);
			this.RoundDecimal(precision);
			if (!this._positive)
			{
				switch (nfi.NumberNegativePattern)
				{
				case 0:
					this.Append('(');
					break;
				case 1:
					this.Append(nfi.NegativeSign);
					break;
				case 2:
					this.Append(nfi.NegativeSign);
					this.Append(' ');
					break;
				}
			}
			this.AppendIntegerStringWithGroupSeparator(nfi.RawNumberGroupSizes, nfi.NumberGroupSeparator);
			if (precision > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			if (!this._positive)
			{
				switch (nfi.NumberNegativePattern)
				{
				case 0:
					this.Append(')');
					break;
				case 3:
					this.Append(nfi.NegativeSign);
					break;
				case 4:
					this.Append(' ');
					this.Append(nfi.NegativeSign);
					break;
				}
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00036248 File Offset: 0x00034448
		public string FormatPercent(int precision, NumberFormatInfo nfi)
		{
			precision = ((precision < 0) ? nfi.PercentDecimalDigits : precision);
			this.Multiply10(2);
			this.RoundDecimal(precision);
			this.ResetCharBuf(this.IntegerDigits * 2 + precision + 16);
			if (this._positive)
			{
				if (nfi.PercentPositivePattern == 2)
				{
					this.Append(nfi.PercentSymbol);
				}
			}
			else
			{
				switch (nfi.PercentNegativePattern)
				{
				case 0:
					this.Append(nfi.NegativeSign);
					break;
				case 1:
					this.Append(nfi.NegativeSign);
					break;
				case 2:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.PercentSymbol);
					break;
				}
			}
			this.AppendIntegerStringWithGroupSeparator(nfi.RawPercentGroupSizes, nfi.PercentGroupSeparator);
			if (precision > 0)
			{
				this.Append(nfi.PercentDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			if (this._positive)
			{
				int num = nfi.PercentPositivePattern;
				if (num != 0)
				{
					if (num == 1)
					{
						this.Append(nfi.PercentSymbol);
					}
				}
				else
				{
					this.Append(' ');
					this.Append(nfi.PercentSymbol);
				}
			}
			else
			{
				int num = nfi.PercentNegativePattern;
				if (num != 0)
				{
					if (num == 1)
					{
						this.Append(nfi.PercentSymbol);
					}
				}
				else
				{
					this.Append(' ');
					this.Append(nfi.PercentSymbol);
				}
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000363E8 File Offset: 0x000345E8
		public string FormatExponential(int precision, NumberFormatInfo nfi)
		{
			if (precision == -1)
			{
				precision = 6;
			}
			this.RoundPos(precision + 1);
			return this.FormatExponential(precision, nfi, 3);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00036408 File Offset: 0x00034608
		private string FormatExponential(int precision, NumberFormatInfo nfi, int expDigits)
		{
			int decPointPos = this._decPointPos;
			int digitsLen = this._digitsLen;
			int exponent = decPointPos - 1;
			int num = this._decPointPos = 1;
			this.ResetCharBuf(precision + 8);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			this.AppendOneDigit(digitsLen - 1);
			if (precision > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDigits(digitsLen - precision - 1, digitsLen - this._decPointPos);
			}
			this.AppendExponent(nfi, exponent, expDigits);
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003649C File Offset: 0x0003469C
		public string FormatCustom(string format, NumberFormatInfo nfi)
		{
			bool positive = this._positive;
			int offset = 0;
			int num = 0;
			NumberFormatter.CustomInfo.GetActiveSection(format, ref positive, this.IsZero, ref offset, ref num);
			if (num == 0)
			{
				return (!this._positive) ? nfi.NegativeSign : string.Empty;
			}
			this._positive = positive;
			NumberFormatter.CustomInfo customInfo = NumberFormatter.CustomInfo.Parse(format, offset, num, nfi);
			StringBuilder stringBuilder = new StringBuilder(customInfo.IntegerDigits * 2);
			StringBuilder stringBuilder2 = new StringBuilder(customInfo.DecimalDigits * 2);
			StringBuilder stringBuilder3 = (!customInfo.UseExponent) ? null : new StringBuilder(customInfo.ExponentDigits * 2);
			int num2 = 0;
			if (customInfo.Percents > 0)
			{
				this.Multiply10(2 * customInfo.Percents);
			}
			if (customInfo.Permilles > 0)
			{
				this.Multiply10(3 * customInfo.Permilles);
			}
			if (customInfo.DividePlaces > 0)
			{
				this.Divide10(customInfo.DividePlaces);
			}
			bool flag = true;
			if (customInfo.UseExponent && (customInfo.DecimalDigits > 0 || customInfo.IntegerDigits > 0))
			{
				if (!this.IsZero)
				{
					this.RoundPos(customInfo.DecimalDigits + customInfo.IntegerDigits);
					num2 -= this._decPointPos - customInfo.IntegerDigits;
					this._decPointPos = customInfo.IntegerDigits;
				}
				flag = (num2 <= 0);
				NumberFormatter.AppendNonNegativeNumber(stringBuilder3, (num2 >= 0) ? num2 : (-num2));
			}
			else
			{
				this.RoundDecimal(customInfo.DecimalDigits);
			}
			if (customInfo.IntegerDigits != 0 || !this.IsZeroInteger)
			{
				this.AppendIntegerString(this.IntegerDigits, stringBuilder);
			}
			this.AppendDecimalString(this.DecimalDigits, stringBuilder2);
			if (customInfo.UseExponent)
			{
				if (customInfo.DecimalDigits <= 0 && customInfo.IntegerDigits <= 0)
				{
					this._positive = true;
				}
				if (stringBuilder.Length < customInfo.IntegerDigits)
				{
					stringBuilder.Insert(0, "0", customInfo.IntegerDigits - stringBuilder.Length);
				}
				while (stringBuilder3.Length < customInfo.ExponentDigits - customInfo.ExponentTailSharpDigits)
				{
					stringBuilder3.Insert(0, '0');
				}
				if (flag && !customInfo.ExponentNegativeSignOnly)
				{
					stringBuilder3.Insert(0, nfi.PositiveSign);
				}
				else if (!flag)
				{
					stringBuilder3.Insert(0, nfi.NegativeSign);
				}
			}
			else
			{
				if (stringBuilder.Length < customInfo.IntegerDigits - customInfo.IntegerHeadSharpDigits)
				{
					stringBuilder.Insert(0, "0", customInfo.IntegerDigits - customInfo.IntegerHeadSharpDigits - stringBuilder.Length);
				}
				if (customInfo.IntegerDigits == customInfo.IntegerHeadSharpDigits && NumberFormatter.IsZeroOnly(stringBuilder))
				{
					stringBuilder.Remove(0, stringBuilder.Length);
				}
			}
			NumberFormatter.ZeroTrimEnd(stringBuilder2, true);
			while (stringBuilder2.Length < customInfo.DecimalDigits - customInfo.DecimalTailSharpDigits)
			{
				stringBuilder2.Append('0');
			}
			if (stringBuilder2.Length > customInfo.DecimalDigits)
			{
				stringBuilder2.Remove(customInfo.DecimalDigits, stringBuilder2.Length - customInfo.DecimalDigits);
			}
			return customInfo.Format(format, offset, num, nfi, this._positive, stringBuilder, stringBuilder2, stringBuilder3);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000367F0 File Offset: 0x000349F0
		private static void ZeroTrimEnd(StringBuilder sb, bool canEmpty)
		{
			int num = 0;
			int num2 = sb.Length - 1;
			while ((!canEmpty) ? (num2 > 0) : (num2 >= 0))
			{
				if (sb[num2] != '0')
				{
					break;
				}
				num++;
				num2--;
			}
			if (num > 0)
			{
				sb.Remove(sb.Length - num, num);
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003685C File Offset: 0x00034A5C
		private static bool IsZeroOnly(StringBuilder sb)
		{
			for (int i = 0; i < sb.Length; i++)
			{
				if (char.IsDigit(sb[i]) && sb[i] != '0')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000368A4 File Offset: 0x00034AA4
		private static void AppendNonNegativeNumber(StringBuilder sb, int v)
		{
			if (v < 0)
			{
				throw new ArgumentException();
			}
			int num = NumberFormatter.ScaleOrder((long)v) - 1;
			do
			{
				int num2 = v / (int)NumberFormatter.GetTenPowerOf(num);
				sb.Append((char)(48 | num2));
				v -= (int)NumberFormatter.GetTenPowerOf(num--) * num2;
			}
			while (num >= 0);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000368F8 File Offset: 0x00034AF8
		private void AppendIntegerString(int minLength, StringBuilder sb)
		{
			if (this._decPointPos <= 0)
			{
				sb.Append('0', minLength);
				return;
			}
			if (this._decPointPos < minLength)
			{
				sb.Append('0', minLength - this._decPointPos);
			}
			this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen, sb);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00036954 File Offset: 0x00034B54
		private void AppendIntegerString(int minLength)
		{
			if (this._decPointPos <= 0)
			{
				this.Append('0', minLength);
				return;
			}
			if (this._decPointPos < minLength)
			{
				this.Append('0', minLength - this._decPointPos);
			}
			this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000369AC File Offset: 0x00034BAC
		private void AppendDecimalString(int precision, StringBuilder sb)
		{
			this.AppendDigits(this._digitsLen - precision - this._decPointPos, this._digitsLen - this._decPointPos, sb);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000369D4 File Offset: 0x00034BD4
		private void AppendDecimalString(int precision)
		{
			this.AppendDigits(this._digitsLen - precision - this._decPointPos, this._digitsLen - this._decPointPos);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000369F8 File Offset: 0x00034BF8
		private void AppendIntegerStringWithGroupSeparator(int[] groups, string groupSeparator)
		{
			if (this.IsZeroInteger)
			{
				this.Append('0');
				return;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < groups.Length; i++)
			{
				num += groups[i];
				if (num > this._decPointPos)
				{
					break;
				}
				num2 = i;
			}
			if (groups.Length > 0 && num > 0)
			{
				int num3 = groups[num2];
				int num4 = (this._decPointPos <= num) ? 0 : (this._decPointPos - num);
				if (num3 == 0)
				{
					while (num2 >= 0 && groups[num2] == 0)
					{
						num2--;
					}
					num3 = ((num4 <= 0) ? groups[num2] : num4);
				}
				int num5;
				if (num4 == 0)
				{
					num5 = num3;
				}
				else
				{
					num2 += num4 / num3;
					num5 = num4 % num3;
					if (num5 == 0)
					{
						num5 = num3;
					}
					else
					{
						num2++;
					}
				}
				int num6 = 0;
				while (this._decPointPos - num6 > num5 && num5 != 0)
				{
					this.AppendDigits(this._digitsLen - num6 - num5, this._digitsLen - num6);
					num6 += num5;
					this.Append(groupSeparator);
					if (--num2 < groups.Length && num2 >= 0)
					{
						num3 = groups[num2];
					}
					num5 = num3;
				}
				this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen - num6);
			}
			else
			{
				this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen);
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00036B88 File Offset: 0x00034D88
		private void AppendExponent(NumberFormatInfo nfi, int exponent, int minDigits)
		{
			if (this._specifierIsUpper || this._specifier == 'R')
			{
				this.Append('E');
			}
			else
			{
				this.Append('e');
			}
			if (exponent >= 0)
			{
				this.Append(nfi.PositiveSign);
			}
			else
			{
				this.Append(nfi.NegativeSign);
				exponent = -exponent;
			}
			if (exponent == 0)
			{
				this.Append('0', minDigits);
			}
			else if (exponent < 10)
			{
				this.Append('0', minDigits - 1);
				this.Append((char)(48 | exponent));
			}
			else
			{
				uint num = NumberFormatter.FastToDecHex(exponent);
				if (exponent >= 100 || minDigits == 3)
				{
					this.Append((char)(48U | num >> 8));
				}
				this.Append((char)(48U | (num >> 4 & 15U)));
				this.Append((char)(48U | (num & 15U)));
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00036C64 File Offset: 0x00034E64
		private void AppendOneDigit(int start)
		{
			if (this._ind == this._cbuf.Length)
			{
				this.Resize(this._ind + 10);
			}
			start += this._offset;
			uint num;
			if (start < 0)
			{
				num = 0U;
			}
			else if (start < 8)
			{
				num = this._val1;
			}
			else if (start < 16)
			{
				num = this._val2;
			}
			else if (start < 24)
			{
				num = this._val3;
			}
			else if (start < 32)
			{
				num = this._val4;
			}
			else
			{
				num = 0U;
			}
			num >>= (start & 7) << 2;
			this._cbuf[this._ind++] = (char)(48U | (num & 15U));
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00036D28 File Offset: 0x00034F28
		private unsafe void FastAppendDigits(int val, bool force)
		{
			int ind = this._ind;
			int num2;
			if (force || val >= 100)
			{
				int num = val * 5243 >> 19;
				num2 = NumberFormatter.DecHexDigits[num];
				if (force || val >= 1000)
				{
					this._cbuf[ind++] = (char)(48 | num2 >> 4);
				}
				this._cbuf[ind++] = (char)(48 | (num2 & 15));
				num2 = NumberFormatter.DecHexDigits[val - num * 100];
			}
			else
			{
				num2 = NumberFormatter.DecHexDigits[val];
			}
			if (force || val >= 10)
			{
				this._cbuf[ind++] = (char)(48 | num2 >> 4);
			}
			this._cbuf[ind++] = (char)(48 | (num2 & 15));
			this._ind = ind;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00036DF4 File Offset: 0x00034FF4
		private void AppendDigits(int start, int end)
		{
			if (start >= end)
			{
				return;
			}
			int num = this._ind + (end - start);
			if (num > this._cbuf.Length)
			{
				this.Resize(num + 10);
			}
			this._ind = num;
			end += this._offset;
			start += this._offset;
			int num2 = start + 8 - (start & 7);
			for (;;)
			{
				uint num3;
				if (num2 == 8)
				{
					num3 = this._val1;
				}
				else if (num2 == 16)
				{
					num3 = this._val2;
				}
				else if (num2 == 24)
				{
					num3 = this._val3;
				}
				else if (num2 == 32)
				{
					num3 = this._val4;
				}
				else
				{
					num3 = 0U;
				}
				num3 >>= (start & 7) << 2;
				if (num2 > end)
				{
					num2 = end;
				}
				this._cbuf[--num] = (char)(48U | (num3 & 15U));
				switch (num2 - start)
				{
				case 1:
					goto IL_1C8;
				case 2:
					goto IL_1AB;
				case 3:
					goto IL_18E;
				case 4:
					goto IL_171;
				case 5:
					goto IL_154;
				case 6:
					goto IL_137;
				case 7:
					goto IL_11A;
				case 8:
					this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
					goto IL_11A;
				}
				IL_1D5:
				start = num2;
				num2 += 8;
				continue;
				IL_1C8:
				if (num2 == end)
				{
					break;
				}
				goto IL_1D5;
				IL_1AB:
				this._cbuf[--num] = (char)(48U | (num3 >> 4 & 15U));
				goto IL_1C8;
				IL_18E:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_1AB;
				IL_171:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_18E;
				IL_154:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_171;
				IL_137:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_154;
				IL_11A:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_137;
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00036FE4 File Offset: 0x000351E4
		private void AppendDigits(int start, int end, StringBuilder sb)
		{
			if (start >= end)
			{
				return;
			}
			int num = sb.Length + (end - start);
			sb.Length = num;
			end += this._offset;
			start += this._offset;
			int num2 = start + 8 - (start & 7);
			for (;;)
			{
				uint num3;
				if (num2 == 8)
				{
					num3 = this._val1;
				}
				else if (num2 == 16)
				{
					num3 = this._val2;
				}
				else if (num2 == 24)
				{
					num3 = this._val3;
				}
				else if (num2 == 32)
				{
					num3 = this._val4;
				}
				else
				{
					num3 = 0U;
				}
				num3 >>= (start & 7) << 2;
				if (num2 > end)
				{
					num2 = end;
				}
				sb[--num] = (char)(48U | (num3 & 15U));
				switch (num2 - start)
				{
				case 1:
					goto IL_1A8;
				case 2:
					goto IL_18C;
				case 3:
					goto IL_170;
				case 4:
					goto IL_154;
				case 5:
					goto IL_138;
				case 6:
					goto IL_11C;
				case 7:
					goto IL_100;
				case 8:
					sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
					goto IL_100;
				}
				IL_1B5:
				start = num2;
				num2 += 8;
				continue;
				IL_1A8:
				if (num2 == end)
				{
					break;
				}
				goto IL_1B5;
				IL_18C:
				sb[--num] = (char)(48U | (num3 >> 4 & 15U));
				goto IL_1A8;
				IL_170:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_18C;
				IL_154:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_170;
				IL_138:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_154;
				IL_11C:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_138;
				IL_100:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_11C;
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000371B4 File Offset: 0x000353B4
		private void Multiply10(int count)
		{
			if (count <= 0 || this._digitsLen == 0)
			{
				return;
			}
			this._decPointPos += count;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000371D8 File Offset: 0x000353D8
		private void Divide10(int count)
		{
			if (count <= 0 || this._digitsLen == 0)
			{
				return;
			}
			this._decPointPos -= count;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x000371FC File Offset: 0x000353FC
		private NumberFormatter GetClone()
		{
			return (NumberFormatter)base.MemberwiseClone();
		}

		// Token: 0x04000586 RID: 1414
		private const int DefaultExpPrecision = 6;

		// Token: 0x04000587 RID: 1415
		private const int HundredMillion = 100000000;

		// Token: 0x04000588 RID: 1416
		private const long SeventeenDigitsThreshold = 10000000000000000L;

		// Token: 0x04000589 RID: 1417
		private const ulong ULongDivHundredMillion = 184467440737UL;

		// Token: 0x0400058A RID: 1418
		private const ulong ULongModHundredMillion = 9551616UL;

		// Token: 0x0400058B RID: 1419
		private const int DoubleBitsExponentShift = 52;

		// Token: 0x0400058C RID: 1420
		private const int DoubleBitsExponentMask = 2047;

		// Token: 0x0400058D RID: 1421
		private const long DoubleBitsMantissaMask = 4503599627370495L;

		// Token: 0x0400058E RID: 1422
		private const int DecimalBitsScaleMask = 2031616;

		// Token: 0x0400058F RID: 1423
		private const int SingleDefPrecision = 7;

		// Token: 0x04000590 RID: 1424
		private const int DoubleDefPrecision = 15;

		// Token: 0x04000591 RID: 1425
		private const int Int8DefPrecision = 3;

		// Token: 0x04000592 RID: 1426
		private const int UInt8DefPrecision = 3;

		// Token: 0x04000593 RID: 1427
		private const int Int16DefPrecision = 5;

		// Token: 0x04000594 RID: 1428
		private const int UInt16DefPrecision = 5;

		// Token: 0x04000595 RID: 1429
		private const int Int32DefPrecision = 10;

		// Token: 0x04000596 RID: 1430
		private const int UInt32DefPrecision = 10;

		// Token: 0x04000597 RID: 1431
		private const int Int64DefPrecision = 19;

		// Token: 0x04000598 RID: 1432
		private const int UInt64DefPrecision = 20;

		// Token: 0x04000599 RID: 1433
		private const int DecimalDefPrecision = 100;

		// Token: 0x0400059A RID: 1434
		private const int TenPowersListLength = 19;

		// Token: 0x0400059B RID: 1435
		private const double MinRoundtripVal = -1.79769313486231E+308;

		// Token: 0x0400059C RID: 1436
		private const double MaxRoundtripVal = 1.79769313486231E+308;

		// Token: 0x0400059D RID: 1437
		private unsafe static readonly ulong* MantissaBitsTable;

		// Token: 0x0400059E RID: 1438
		private unsafe static readonly int* TensExponentTable;

		// Token: 0x0400059F RID: 1439
		private unsafe static readonly char* DigitLowerTable;

		// Token: 0x040005A0 RID: 1440
		private unsafe static readonly char* DigitUpperTable;

		// Token: 0x040005A1 RID: 1441
		private unsafe static readonly long* TenPowersList;

		// Token: 0x040005A2 RID: 1442
		private unsafe static readonly int* DecHexDigits;

		// Token: 0x040005A3 RID: 1443
		private Thread _thread;

		// Token: 0x040005A4 RID: 1444
		private NumberFormatInfo _nfi;

		// Token: 0x040005A5 RID: 1445
		private bool _NaN;

		// Token: 0x040005A6 RID: 1446
		private bool _infinity;

		// Token: 0x040005A7 RID: 1447
		private bool _isCustomFormat;

		// Token: 0x040005A8 RID: 1448
		private bool _specifierIsUpper;

		// Token: 0x040005A9 RID: 1449
		private bool _positive;

		// Token: 0x040005AA RID: 1450
		private char _specifier;

		// Token: 0x040005AB RID: 1451
		private int _precision;

		// Token: 0x040005AC RID: 1452
		private int _defPrecision;

		// Token: 0x040005AD RID: 1453
		private int _digitsLen;

		// Token: 0x040005AE RID: 1454
		private int _offset;

		// Token: 0x040005AF RID: 1455
		private int _decPointPos;

		// Token: 0x040005B0 RID: 1456
		private uint _val1;

		// Token: 0x040005B1 RID: 1457
		private uint _val2;

		// Token: 0x040005B2 RID: 1458
		private uint _val3;

		// Token: 0x040005B3 RID: 1459
		private uint _val4;

		// Token: 0x040005B4 RID: 1460
		private char[] _cbuf;

		// Token: 0x040005B5 RID: 1461
		private int _ind;

		// Token: 0x040005B6 RID: 1462
		[ThreadStatic]
		private static NumberFormatter threadNumberFormatter;

		// Token: 0x0200016B RID: 363
		private class CustomInfo
		{
			// Token: 0x06000DEA RID: 3562 RVA: 0x00037224 File Offset: 0x00035424
			public static void GetActiveSection(string format, ref bool positive, bool zero, ref int offset, ref int length)
			{
				int[] array = new int[3];
				int num = 0;
				int num2 = 0;
				char c = '\0';
				for (int i = 0; i < format.Length; i++)
				{
					char c2 = format[i];
					if (c2 == c || (c == '\0' && (c2 == '"' || c2 == '\'')))
					{
						if (c == '\0')
						{
							c = c2;
						}
						else
						{
							c = '\0';
						}
					}
					else if (c == '\0' && format[i] == ';' && (i == 0 || format[i - 1] != '\\'))
					{
						array[num++] = i - num2;
						num2 = i + 1;
						if (num == 3)
						{
							break;
						}
					}
				}
				if (num == 0)
				{
					offset = 0;
					length = format.Length;
					return;
				}
				if (num == 1)
				{
					if (positive || zero)
					{
						offset = 0;
						length = array[0];
						return;
					}
					if (array[0] + 1 < format.Length)
					{
						positive = true;
						offset = array[0] + 1;
						length = format.Length - offset;
						return;
					}
					offset = 0;
					length = array[0];
					return;
				}
				else if (num == 2)
				{
					if (zero)
					{
						offset = array[0] + array[1] + 2;
						length = format.Length - offset;
						return;
					}
					if (positive)
					{
						offset = 0;
						length = array[0];
						return;
					}
					if (array[1] > 0)
					{
						positive = true;
						offset = array[0] + 1;
						length = array[1];
						return;
					}
					offset = 0;
					length = array[0];
					return;
				}
				else
				{
					if (num != 3)
					{
						throw new ArgumentException();
					}
					if (zero)
					{
						offset = array[0] + array[1] + 2;
						length = array[2];
						return;
					}
					if (positive)
					{
						offset = 0;
						length = array[0];
						return;
					}
					if (array[1] > 0)
					{
						positive = true;
						offset = array[0] + 1;
						length = array[1];
						return;
					}
					offset = 0;
					length = array[0];
					return;
				}
			}

			// Token: 0x06000DEB RID: 3563 RVA: 0x000373F8 File Offset: 0x000355F8
			public static NumberFormatter.CustomInfo Parse(string format, int offset, int length, NumberFormatInfo nfi)
			{
				char c = '\0';
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = true;
				NumberFormatter.CustomInfo customInfo = new NumberFormatter.CustomInfo();
				int num = 0;
				int num2 = offset;
				while (num2 - offset < length)
				{
					char c2 = format[num2];
					if (c2 == c && c2 != '\0')
					{
						c = '\0';
					}
					else if (c == '\0')
					{
						if (flag3 && c2 != '\0' && c2 != '0' && c2 != '#')
						{
							flag3 = false;
							flag = (customInfo.DecimalPointPos < 0);
							flag2 = !flag;
							num2--;
						}
						else
						{
							char c3 = c2;
							switch (c3)
							{
							case '"':
							case '\'':
								if (c2 == '"' || c2 == '\'')
								{
									c = c2;
								}
								goto IL_311;
							case '#':
								if (flag4 && flag)
								{
									customInfo.IntegerHeadSharpDigits++;
								}
								else if (flag2)
								{
									customInfo.DecimalTailSharpDigits++;
								}
								else if (flag3)
								{
									customInfo.ExponentTailSharpDigits++;
								}
								break;
							default:
								switch (c3)
								{
								case ',':
									if (flag && customInfo.IntegerDigits > 0)
									{
										num++;
									}
									goto IL_311;
								default:
									if (c3 != 'E')
									{
										if (c3 == '\\')
										{
											num2++;
											goto IL_311;
										}
										if (c3 != 'e')
										{
											if (c3 != '‰')
											{
												goto IL_311;
											}
											customInfo.Permilles++;
											goto IL_311;
										}
									}
									if (customInfo.UseExponent)
									{
										goto IL_311;
									}
									customInfo.UseExponent = true;
									flag = false;
									flag2 = false;
									flag3 = true;
									if (num2 + 1 - offset < length)
									{
										char c4 = format[num2 + 1];
										if (c4 == '+')
										{
											customInfo.ExponentNegativeSignOnly = false;
										}
										if (c4 == '+' || c4 == '-')
										{
											num2++;
										}
										else if (c4 != '0' && c4 != '#')
										{
											customInfo.UseExponent = false;
											if (customInfo.DecimalPointPos < 0)
											{
												flag = true;
											}
										}
									}
									goto IL_311;
								case '.':
									flag = false;
									flag2 = true;
									flag3 = false;
									if (customInfo.DecimalPointPos == -1)
									{
										customInfo.DecimalPointPos = num2;
									}
									goto IL_311;
								case '0':
									break;
								}
								break;
							case '%':
								customInfo.Percents++;
								goto IL_311;
							}
							if (c2 != '#')
							{
								flag4 = false;
								if (flag2)
								{
									customInfo.DecimalTailSharpDigits = 0;
								}
								else if (flag3)
								{
									customInfo.ExponentTailSharpDigits = 0;
								}
							}
							if (customInfo.IntegerHeadPos == -1)
							{
								customInfo.IntegerHeadPos = num2;
							}
							if (flag)
							{
								customInfo.IntegerDigits++;
								if (num > 0)
								{
									customInfo.UseGroup = true;
								}
								num = 0;
							}
							else if (flag2)
							{
								customInfo.DecimalDigits++;
							}
							else if (flag3)
							{
								customInfo.ExponentDigits++;
							}
						}
					}
					IL_311:
					num2++;
				}
				if (customInfo.ExponentDigits == 0)
				{
					customInfo.UseExponent = false;
				}
				else
				{
					customInfo.IntegerHeadSharpDigits = 0;
				}
				if (customInfo.DecimalDigits == 0)
				{
					customInfo.DecimalPointPos = -1;
				}
				customInfo.DividePlaces += num * 3;
				return customInfo;
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x00037770 File Offset: 0x00035970
			public string Format(string format, int offset, int length, NumberFormatInfo nfi, bool positive, StringBuilder sb_int, StringBuilder sb_dec, StringBuilder sb_exp)
			{
				StringBuilder stringBuilder = new StringBuilder();
				char c = '\0';
				bool flag = true;
				bool flag2 = false;
				int num = 0;
				int i = 0;
				int num2 = 0;
				int[] rawNumberGroupSizes = nfi.RawNumberGroupSizes;
				string numberGroupSeparator = nfi.NumberGroupSeparator;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				if (this.UseGroup && rawNumberGroupSizes.Length > 0)
				{
					num3 = sb_int.Length;
					for (int j = 0; j < rawNumberGroupSizes.Length; j++)
					{
						num4 += rawNumberGroupSizes[j];
						if (num4 <= num3)
						{
							num5 = j;
						}
					}
					num7 = rawNumberGroupSizes[num5];
					int num8 = (num3 <= num4) ? 0 : (num3 - num4);
					if (num7 == 0)
					{
						while (num5 >= 0 && rawNumberGroupSizes[num5] == 0)
						{
							num5--;
						}
						num7 = ((num8 <= 0) ? rawNumberGroupSizes[num5] : num8);
					}
					if (num8 == 0)
					{
						num6 = num7;
					}
					else
					{
						num5 += num8 / num7;
						num6 = num8 % num7;
						if (num6 == 0)
						{
							num6 = num7;
						}
						else
						{
							num5++;
						}
					}
				}
				else
				{
					this.UseGroup = false;
				}
				int num9 = offset;
				while (num9 - offset < length)
				{
					char c2 = format[num9];
					if (c2 == c && c2 != '\0')
					{
						c = '\0';
					}
					else if (c != '\0')
					{
						stringBuilder.Append(c2);
					}
					else
					{
						char c3 = c2;
						switch (c3)
						{
						case '"':
						case '\'':
							if (c2 == '"' || c2 == '\'')
							{
								c = c2;
							}
							goto IL_474;
						case '#':
							break;
						default:
							switch (c3)
							{
							case ',':
								goto IL_474;
							default:
							{
								if (c3 != 'E')
								{
									if (c3 == '\\')
									{
										num9++;
										if (num9 - offset < length)
										{
											stringBuilder.Append(format[num9]);
										}
										goto IL_474;
									}
									if (c3 != 'e')
									{
										if (c3 != '‰')
										{
											stringBuilder.Append(c2);
											goto IL_474;
										}
										stringBuilder.Append(nfi.PerMilleSymbol);
										goto IL_474;
									}
								}
								if (sb_exp == null || !this.UseExponent)
								{
									stringBuilder.Append(c2);
									goto IL_474;
								}
								bool flag3 = true;
								bool flag4 = false;
								int num10 = num9 + 1;
								while (num10 - offset < length)
								{
									if (format[num10] == '0')
									{
										flag4 = true;
									}
									else if (num10 != num9 + 1 || (format[num10] != '+' && format[num10] != '-'))
									{
										if (!flag4)
										{
											flag3 = false;
										}
										break;
									}
									num10++;
								}
								if (flag3)
								{
									num9 = num10 - 1;
									flag = (this.DecimalPointPos < 0);
									flag2 = !flag;
									stringBuilder.Append(c2);
									stringBuilder.Append(sb_exp);
									sb_exp = null;
								}
								else
								{
									stringBuilder.Append(c2);
								}
								goto IL_474;
							}
							case '.':
								if (this.DecimalPointPos == num9)
								{
									if (this.DecimalDigits > 0)
									{
										while (i < sb_int.Length)
										{
											stringBuilder.Append(sb_int[i++]);
										}
									}
									if (sb_dec.Length > 0)
									{
										stringBuilder.Append(nfi.NumberDecimalSeparator);
									}
								}
								flag = false;
								flag2 = true;
								goto IL_474;
							case '0':
								break;
							}
							break;
						case '%':
							stringBuilder.Append(nfi.PercentSymbol);
							goto IL_474;
						}
						if (flag)
						{
							num++;
							if (this.IntegerDigits - num < sb_int.Length + i || c2 == '0')
							{
								while (this.IntegerDigits - num + i < sb_int.Length)
								{
									stringBuilder.Append(sb_int[i++]);
									if (this.UseGroup && --num3 > 0 && --num6 == 0)
									{
										stringBuilder.Append(numberGroupSeparator);
										if (--num5 < rawNumberGroupSizes.Length && num5 >= 0)
										{
											num7 = rawNumberGroupSizes[num5];
										}
										num6 = num7;
									}
								}
							}
						}
						else if (flag2)
						{
							if (num2 < sb_dec.Length)
							{
								stringBuilder.Append(sb_dec[num2++]);
							}
						}
						else
						{
							stringBuilder.Append(c2);
						}
					}
					IL_474:
					num9++;
				}
				if (!positive)
				{
					stringBuilder.Insert(0, nfi.NegativeSign);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040005B7 RID: 1463
			public bool UseGroup;

			// Token: 0x040005B8 RID: 1464
			public int DecimalDigits;

			// Token: 0x040005B9 RID: 1465
			public int DecimalPointPos = -1;

			// Token: 0x040005BA RID: 1466
			public int DecimalTailSharpDigits;

			// Token: 0x040005BB RID: 1467
			public int IntegerDigits;

			// Token: 0x040005BC RID: 1468
			public int IntegerHeadSharpDigits;

			// Token: 0x040005BD RID: 1469
			public int IntegerHeadPos;

			// Token: 0x040005BE RID: 1470
			public bool UseExponent;

			// Token: 0x040005BF RID: 1471
			public int ExponentDigits;

			// Token: 0x040005C0 RID: 1472
			public int ExponentTailSharpDigits;

			// Token: 0x040005C1 RID: 1473
			public bool ExponentNegativeSignOnly = true;

			// Token: 0x040005C2 RID: 1474
			public int DividePlaces;

			// Token: 0x040005C3 RID: 1475
			public int Percents;

			// Token: 0x040005C4 RID: 1476
			public int Permilles;
		}
	}
}
