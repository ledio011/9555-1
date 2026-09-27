using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x020003D3 RID: 979
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public struct UInt32 : IComparable<uint>, IEquatable<uint>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06001E34 RID: 7732 RVA: 0x00070B54 File Offset: 0x0006ED54
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00070B60 File Offset: 0x0006ED60
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00070B6C File Offset: 0x0006ED6C
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00070B78 File Offset: 0x0006ED78
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00070B84 File Offset: 0x0006ED84
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00070B90 File Offset: 0x0006ED90
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00070B9C File Offset: 0x0006ED9C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00070BA8 File Offset: 0x0006EDA8
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00070BB4 File Offset: 0x0006EDB4
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00070BC0 File Offset: 0x0006EDC0
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00070BCC File Offset: 0x0006EDCC
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00070BD8 File Offset: 0x0006EDD8
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00070BFC File Offset: 0x0006EDFC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00070C08 File Offset: 0x0006EE08
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00070C0C File Offset: 0x0006EE0C
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00070C18 File Offset: 0x0006EE18
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is uint))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.UInt32."));
			}
			uint num = (uint)value;
			if (this == num)
			{
				return 0;
			}
			return (this >= num) ? 1 : -1;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00070C68 File Offset: 0x0006EE68
		public override bool Equals(object obj)
		{
			return obj is uint && (uint)obj == this;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00070C84 File Offset: 0x0006EE84
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00070C88 File Offset: 0x0006EE88
		public int CompareTo(uint value)
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

		// Token: 0x06001E47 RID: 7751 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		public bool Equals(uint obj)
		{
			return obj == this;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00070CA8 File Offset: 0x0006EEA8
		internal static bool Parse(string s, bool tryParse, out uint result, out Exception exc)
		{
			uint num = 0U;
			bool flag = false;
			bool flag2 = false;
			result = 0U;
			exc = null;
			if (s == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("s");
				}
				return false;
			}
			int length = s.Length;
			int i;
			for (i = 0; i < length; i++)
			{
				char c = s[i];
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
			}
			if (i == length)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (s[i] == '+')
			{
				i++;
			}
			else if (s[i] == '-')
			{
				i++;
				flag2 = true;
			}
			while (i < length)
			{
				char c = s[i];
				if (c >= '0' && c <= '9')
				{
					uint num2 = (uint)(c - '0');
					if (num > 429496729U || (num == 429496729U && num2 > 5U))
					{
						if (!tryParse)
						{
							exc = new OverflowException(Locale.GetText("Value is too large"));
						}
						return false;
					}
					num = num * 10U + num2;
					flag = true;
				}
				else if (!int.ProcessTrailingWhitespace(tryParse, s, i, ref exc))
				{
					return false;
				}
				i++;
			}
			if (!flag)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (flag2 && num > 0U)
			{
				if (!tryParse)
				{
					exc = new OverflowException(Locale.GetText("Negative number"));
				}
				return false;
			}
			result = num;
			return true;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00070E1C File Offset: 0x0006F01C
		internal static bool Parse(string s, NumberStyles style, IFormatProvider provider, bool tryParse, out uint result, out Exception exc)
		{
			result = 0U;
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
			uint num2 = 0U;
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
					uint num4;
					if (char.IsDigit(c))
					{
						num4 = (uint)(c - '0');
					}
					else if (char.IsLower(c))
					{
						num4 = (uint)(c - 'a' + '\n');
					}
					else
					{
						num4 = (uint)(c - 'A' + '\n');
					}
					if (tryParse)
					{
						ulong num5 = (ulong)(num2 * 16U + num4);
						if (num5 > (ulong)-1)
						{
							return false;
						}
						num2 = (uint)num5;
					}
					else
					{
						num2 = checked(num2 * 16U + num4);
					}
				}
				else if (flag14)
				{
					num3++;
					if (s[num++] != '0')
					{
						goto Block_50;
					}
				}
				else
				{
					num3++;
					try
					{
						num2 = checked(num2 * 10U + (uint)(s[num++] - '0'));
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
			goto IL_435;
			Block_50:
			if (!tryParse)
			{
				exc = new OverflowException(Locale.GetText("Value too large or too small."));
			}
			return false;
			IL_435:
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
			if (flag11 && num2 > 0U)
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

		// Token: 0x06001E4A RID: 7754 RVA: 0x00071408 File Offset: 0x0006F608
		[CLSCompliant(false)]
		public static uint Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			uint result;
			Exception ex;
			if (!uint.Parse(s, style, provider, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0007142C File Offset: 0x0006F62C
		[CLSCompliant(false)]
		public static uint Parse(string s, IFormatProvider provider)
		{
			return uint.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00071438 File Offset: 0x0006F638
		[CLSCompliant(false)]
		public static bool TryParse(string s, out uint result)
		{
			Exception ex;
			if (!uint.Parse(s, true, out result, out ex))
			{
				result = 0U;
				return false;
			}
			return true;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0007145C File Offset: 0x0006F65C
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out uint result)
		{
			Exception ex;
			if (!uint.Parse(s, style, provider, true, out result, out ex))
			{
				result = 0U;
				return false;
			}
			return true;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00071480 File Offset: 0x0006F680
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0007148C File Offset: 0x0006F68C
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00071498 File Offset: 0x0006F698
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000714A4 File Offset: 0x0006F6A4
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000714B0 File Offset: 0x0006F6B0
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt32;
		}

		// Token: 0x04000FA8 RID: 4008
		public const uint MaxValue = 4294967295U;

		// Token: 0x04000FA9 RID: 4009
		public const uint MinValue = 0U;

		// Token: 0x04000FAA RID: 4010
		internal uint m_value;
	}
}
