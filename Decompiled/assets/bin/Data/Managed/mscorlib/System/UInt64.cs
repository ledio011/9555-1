using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x020003D4 RID: 980
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public struct UInt64 : IComparable<ulong>, IEquatable<ulong>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x000714B4 File Offset: 0x0006F6B4
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000714C0 File Offset: 0x0006F6C0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000714CC File Offset: 0x0006F6CC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000714D8 File Offset: 0x0006F6D8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000714E4 File Offset: 0x0006F6E4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000714F0 File Offset: 0x0006F6F0
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000714FC File Offset: 0x0006F6FC
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00071508 File Offset: 0x0006F708
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00071514 File Offset: 0x0006F714
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00071520 File Offset: 0x0006F720
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0007152C File Offset: 0x0006F72C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00071538 File Offset: 0x0006F738
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0007155C File Offset: 0x0006F75C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00071568 File Offset: 0x0006F768
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00071574 File Offset: 0x0006F774
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00071578 File Offset: 0x0006F778
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.UInt64."));
			}
			ulong num = (ulong)value;
			if (this == num)
			{
				return 0;
			}
			return (this >= num) ? 1 : -1;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000715C8 File Offset: 0x0006F7C8
		public override bool Equals(object obj)
		{
			return obj is ulong && (ulong)obj == this;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000715E4 File Offset: 0x0006F7E4
		public override int GetHashCode()
		{
			return (int)(this & (ulong)-1) ^ (int)(this >> 32);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000715F4 File Offset: 0x0006F7F4
		public int CompareTo(ulong value)
		{
			if (this == value)
			{
				return 0;
			}
			if (this > value)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0007160C File Offset: 0x0006F80C
		public bool Equals(ulong obj)
		{
			return obj == this;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00071614 File Offset: 0x0006F814
		[CLSCompliant(false)]
		public static ulong Parse(string s, IFormatProvider provider)
		{
			return ulong.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00071620 File Offset: 0x0006F820
		internal static bool Parse(string s, NumberStyles style, IFormatProvider provider, bool tryParse, out ulong result, out Exception exc)
		{
			result = 0UL;
			exc = null;
			if (s == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("s");
				}
				return false;
			}
			if (s.Length == 0)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			NumberFormatInfo numberFormatInfo = null;
			if (provider != null)
			{
				Type typeFromHandle = typeof(NumberFormatInfo);
				numberFormatInfo = (NumberFormatInfo)provider.GetFormat(typeFromHandle);
			}
			if (numberFormatInfo == null)
			{
				numberFormatInfo = Thread.CurrentThread.CurrentCulture.NumberFormat;
			}
			if (!int.CheckStyle(style, tryParse, ref exc))
			{
				return false;
			}
			bool flag = (style & NumberStyles.AllowCurrencySymbol) != NumberStyles.None;
			bool flag2 = (style & NumberStyles.AllowHexSpecifier) != NumberStyles.None;
			bool flag3 = (style & NumberStyles.AllowThousands) != NumberStyles.None;
			bool flag4 = (style & NumberStyles.AllowDecimalPoint) != NumberStyles.None;
			bool flag5 = (style & NumberStyles.AllowParentheses) != NumberStyles.None;
			bool flag6 = (style & NumberStyles.AllowTrailingSign) != NumberStyles.None;
			bool flag7 = (style & NumberStyles.AllowLeadingSign) != NumberStyles.None;
			bool flag8 = (style & NumberStyles.AllowTrailingWhite) != NumberStyles.None;
			bool flag9 = (style & NumberStyles.AllowLeadingWhite) != NumberStyles.None;
			int num = 0;
			if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
			{
				return false;
			}
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			if (flag5 && s[num] == '(')
			{
				flag10 = true;
				flag12 = true;
				flag11 = true;
				num++;
				if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
				{
					return false;
				}
				if (s.Substring(num, numberFormatInfo.NegativeSign.Length) == numberFormatInfo.NegativeSign)
				{
					if (!tryParse)
					{
						exc = int.GetFormatException();
					}
					return false;
				}
				if (s.Substring(num, numberFormatInfo.PositiveSign.Length) == numberFormatInfo.PositiveSign)
				{
					if (!tryParse)
					{
						exc = int.GetFormatException();
					}
					return false;
				}
			}
			if (flag7 && !flag12)
			{
				int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
				if (flag12)
				{
					if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
						if (flag13 && flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			if (flag && !flag13)
			{
				int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
				if (flag13)
				{
					if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag13 && !flag12 && flag7)
					{
						int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
						if (flag12 && flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			ulong num2 = 0UL;
			int num3 = 0;
			bool flag14 = false;
			do
			{
				if (!int.ValidDigit(s[num], flag2))
				{
					if (!flag3 || !int.FindOther(ref num, s, numberFormatInfo.NumberGroupSeparator))
					{
						if (flag14 || !flag4 || !int.FindOther(ref num, s, numberFormatInfo.NumberDecimalSeparator))
						{
							break;
						}
						flag14 = true;
					}
				}
				else if (flag2)
				{
					num3++;
					char c = s[num++];
					ulong num4;
					if (char.IsDigit(c))
					{
						num4 = (ulong)((long)(c - '0'));
					}
					else if (char.IsLower(c))
					{
						num4 = (ulong)((long)(c - 'a' + '\n'));
					}
					else
					{
						num4 = (ulong)((long)(c - 'A' + '\n'));
					}
					if (tryParse)
					{
						bool flag15 = num2 > 65535UL;
						num2 = num2 * 16UL + num4;
						if (flag15 && num2 < 16UL)
						{
							return false;
						}
					}
					else
					{
						num2 = checked(num2 * 16UL + num4);
					}
				}
				else if (flag14)
				{
					num3++;
					if (s[num++] != '0')
					{
						goto Block_51;
					}
				}
				else
				{
					num3++;
					try
					{
						num2 = checked(num2 * 10UL + (ulong)(s[num++] - '0'));
					}
					catch (OverflowException)
					{
						if (!tryParse)
						{
							exc = new OverflowException(Locale.GetText("Value too large or too small."));
						}
						return false;
					}
				}
			}
			while (num < s.Length);
			goto IL_44B;
			Block_51:
			if (!tryParse)
			{
				exc = new OverflowException(Locale.GetText("Value too large or too small."));
			}
			return false;
			IL_44B:
			if (num3 == 0)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (flag6 && !flag12)
			{
				int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
				if (flag12)
				{
					if (flag8 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
					}
				}
			}
			if (flag && !flag13)
			{
				int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
				if (flag13)
				{
					if (flag8 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (!flag12 && flag6)
					{
						int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
					}
				}
			}
			if (flag8 && num < s.Length && !int.JumpOverWhite(ref num, s, false, tryParse, ref exc))
			{
				return false;
			}
			if (flag10)
			{
				if (num >= s.Length || s[num++] != ')')
				{
					if (!tryParse)
					{
						exc = int.GetFormatException();
					}
					return false;
				}
				if (flag8 && num < s.Length && !int.JumpOverWhite(ref num, s, false, tryParse, ref exc))
				{
					return false;
				}
			}
			if (num < s.Length && s[num] != '\0')
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (flag11 && num2 > 0UL)
			{
				if (!tryParse)
				{
					exc = new OverflowException(Locale.GetText("Negative number"));
				}
				return false;
			}
			result = num2;
			return true;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x00071C24 File Offset: 0x0006FE24
		[CLSCompliant(false)]
		public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			ulong result;
			Exception ex;
			if (!ulong.Parse(s, style, provider, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00071C48 File Offset: 0x0006FE48
		[CLSCompliant(false)]
		public static bool TryParse(string s, out ulong result)
		{
			Exception ex;
			if (!ulong.Parse(s, NumberStyles.Integer, null, true, out result, out ex))
			{
				result = 0UL;
				return false;
			}
			return true;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00071C70 File Offset: 0x0006FE70
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00071C7C File Offset: 0x0006FE7C
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00071C88 File Offset: 0x0006FE88
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00071C94 File Offset: 0x0006FE94
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00071CA0 File Offset: 0x0006FEA0
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		// Token: 0x04000FAB RID: 4011
		public const ulong MaxValue = 18446744073709551615UL;

		// Token: 0x04000FAC RID: 4012
		public const ulong MinValue = 0UL;

		// Token: 0x04000FAD RID: 4013
		internal ulong m_value;
	}
}
