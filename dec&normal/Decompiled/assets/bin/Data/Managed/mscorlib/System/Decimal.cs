using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x020000CD RID: 205
	[ComVisible(true)]
	[Serializable]
	public struct Decimal : IComparable<decimal>, IEquatable<decimal>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			this.lo = (uint)lo;
			this.mid = (uint)mid;
			this.hi = (uint)hi;
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("scale must be between 0 and 28"));
			}
			this.flags = (uint)scale;
			this.flags <<= 16;
			if (isNegative)
			{
				this.flags |= 2147483648U;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001F22C File Offset: 0x0001D42C
		public Decimal(int value)
		{
			this.hi = (this.mid = 0U);
			if (value < 0)
			{
				this.flags = 2147483648U;
				this.lo = (uint)(~value + 1);
			}
			else
			{
				this.flags = 0U;
				this.lo = (uint)value;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001F278 File Offset: 0x0001D478
		[CLSCompliant(false)]
		public Decimal(uint value)
		{
			this.lo = value;
			this.flags = (this.hi = (this.mid = 0U));
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		public Decimal(long value)
		{
			this.hi = 0U;
			if (value < 0L)
			{
				this.flags = 2147483648U;
				ulong num = (ulong)(~value + 1L);
				this.lo = (uint)num;
				this.mid = (uint)(num >> 32);
			}
			else
			{
				this.flags = 0U;
				this.lo = (uint)value;
				this.mid = (uint)((ulong)value >> 32);
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001F30C File Offset: 0x0001D50C
		[CLSCompliant(false)]
		public Decimal(ulong value)
		{
			this.flags = (this.hi = 0U);
			this.lo = (uint)value;
			this.mid = (uint)(value >> 32);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001F33C File Offset: 0x0001D53C
		public Decimal(float value)
		{
			if (value > 7.9228163E+28f || value < -7.9228163E+28f || float.IsNaN(value) || float.IsNegativeInfinity(value) || float.IsPositiveInfinity(value))
			{
				throw new OverflowException(Locale.GetText("Value {0} is greater than Decimal.MaxValue or less than Decimal.MinValue", new object[]
				{
					value
				}));
			}
			decimal num = decimal.Parse(value.ToString(CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture);
			this.flags = num.flags;
			this.hi = num.hi;
			this.lo = num.lo;
			this.mid = num.mid;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		public Decimal(double value)
		{
			if (value > 7.922816251426434E+28 || value < -7.922816251426434E+28 || double.IsNaN(value) || double.IsNegativeInfinity(value) || double.IsPositiveInfinity(value))
			{
				throw new OverflowException(Locale.GetText("Value {0} is greater than Decimal.MaxValue or less than Decimal.MinValue", new object[]
				{
					value
				}));
			}
			decimal num = decimal.Parse(value.ToString(CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture);
			this.flags = num.flags;
			this.hi = num.hi;
			this.lo = num.lo;
			this.mid = num.mid;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001F524 File Offset: 0x0001D724
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001F534 File Offset: 0x0001D734
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001F544 File Offset: 0x0001D744
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001F54C File Offset: 0x0001D74C
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001F554 File Offset: 0x0001D754
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001F55C File Offset: 0x0001D75C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001F56C File Offset: 0x0001D76C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001F57C File Offset: 0x0001D77C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001F58C File Offset: 0x0001D78C
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001F59C File Offset: 0x0001D79C
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001F5BC File Offset: 0x0001D7BC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001F5EC File Offset: 0x0001D7EC
		public static int[] GetBits(decimal d)
		{
			return new int[]
			{
				(int)d.lo,
				(int)d.mid,
				(int)d.hi,
				(int)d.flags
			};
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001F61C File Offset: 0x0001D81C
		public static decimal Add(decimal d1, decimal d2)
		{
			if (decimal.decimalIncr(ref d1, ref d2) == 0)
			{
				return d1;
			}
			throw new OverflowException(Locale.GetText("Overflow on adding decimal number"));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001F640 File Offset: 0x0001D840
		public static decimal Subtract(decimal d1, decimal d2)
		{
			d2.flags ^= 2147483648U;
			int num = decimal.decimalIncr(ref d1, ref d2);
			if (num == 0)
			{
				return d1;
			}
			throw new OverflowException(Locale.GetText("Overflow on subtracting decimal numbers (" + num + ")"));
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001F694 File Offset: 0x0001D894
		public override int GetHashCode()
		{
			return (int)(this.flags ^ this.hi ^ this.lo ^ this.mid);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		private static ulong u64(decimal value)
		{
			decimal.decimalFloorAndTrunc(ref value, 0);
			ulong result;
			if (decimal.decimal2UInt64(ref value, out result) != 0)
			{
				throw new OverflowException();
			}
			return result;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		private static long s64(decimal value)
		{
			decimal.decimalFloorAndTrunc(ref value, 0);
			long result;
			if (decimal.decimal2Int64(ref value, out result) != 0)
			{
				throw new OverflowException();
			}
			return result;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001F70C File Offset: 0x0001D90C
		public static bool Equals(decimal d1, decimal d2)
		{
			return decimal.Compare(d1, d2) == 0;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001F718 File Offset: 0x0001D918
		public override bool Equals(object value)
		{
			return value is decimal && decimal.Equals((decimal)value, this);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001F738 File Offset: 0x0001D938
		private bool IsZero()
		{
			return this.hi == 0U && this.lo == 0U && this.mid == 0U;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001F75C File Offset: 0x0001D95C
		public static decimal Floor(decimal d)
		{
			decimal.decimalFloorAndTrunc(ref d, 1);
			return d;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001F768 File Offset: 0x0001D968
		public static decimal Multiply(decimal d1, decimal d2)
		{
			if (d1.IsZero() || d2.IsZero())
			{
				return 0m;
			}
			if (decimal.decimalMult(ref d1, ref d2) != 0)
			{
				throw new OverflowException();
			}
			return d1;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001F7A0 File Offset: 0x0001D9A0
		public static decimal Divide(decimal d1, decimal d2)
		{
			if (d2.IsZero())
			{
				throw new DivideByZeroException();
			}
			if (d1.IsZero())
			{
				return 0m;
			}
			d1.flags ^= 2147483648U;
			d1.flags ^= 2147483648U;
			decimal result;
			if (decimal.decimalDiv(out result, ref d1, ref d2) != 0)
			{
				throw new OverflowException();
			}
			return result;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001F810 File Offset: 0x0001DA10
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.decimalCompare(ref d1, ref d2);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001F81C File Offset: 0x0001DA1C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Decimal"));
			}
			return decimal.Compare(this, (decimal)value);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001F854 File Offset: 0x0001DA54
		public int CompareTo(decimal value)
		{
			return decimal.Compare(this, value);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001F864 File Offset: 0x0001DA64
		public bool Equals(decimal value)
		{
			return decimal.Equals(value, this);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001F874 File Offset: 0x0001DA74
		public static decimal Parse(string s, IFormatProvider provider)
		{
			return decimal.Parse(s, NumberStyles.Number, provider);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001F880 File Offset: 0x0001DA80
		private static void ThrowAtPos(int pos)
		{
			throw new FormatException(string.Format(Locale.GetText("Invalid character at position {0}"), pos));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001F89C File Offset: 0x0001DA9C
		private static void ThrowInvalidExp()
		{
			throw new FormatException(Locale.GetText("Invalid exponent"));
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		private static string stripStyles(string s, NumberStyles style, NumberFormatInfo nfi, out int decPos, out bool isNegative, out bool expFlag, out int exp, bool throwex)
		{
			isNegative = false;
			expFlag = false;
			exp = 0;
			decPos = -1;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = (style & NumberStyles.AllowLeadingWhite) != NumberStyles.None;
			bool flag5 = (style & NumberStyles.AllowTrailingWhite) != NumberStyles.None;
			bool flag6 = (style & NumberStyles.AllowLeadingSign) != NumberStyles.None;
			bool flag7 = (style & NumberStyles.AllowTrailingSign) != NumberStyles.None;
			bool flag8 = (style & NumberStyles.AllowParentheses) != NumberStyles.None;
			bool flag9 = (style & NumberStyles.AllowThousands) != NumberStyles.None;
			bool flag10 = (style & NumberStyles.AllowDecimalPoint) != NumberStyles.None;
			bool flag11 = (style & NumberStyles.AllowExponent) != NumberStyles.None;
			bool flag12 = false;
			if ((style & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
			{
				int num = s.IndexOf(nfi.CurrencySymbol);
				if (num >= 0)
				{
					s = s.Remove(num, nfi.CurrencySymbol.Length);
					flag12 = true;
				}
			}
			string text = (!flag12) ? nfi.NumberDecimalSeparator : nfi.CurrencyDecimalSeparator;
			string text2 = (!flag12) ? nfi.NumberGroupSeparator : nfi.CurrencyGroupSeparator;
			int i = 0;
			int length = s.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			while (i < length)
			{
				char c = s[i];
				if (char.IsDigit(c))
				{
					break;
				}
				if (flag4 && char.IsWhiteSpace(c))
				{
					i++;
				}
				else if (flag8 && c == '(' && !flag && !flag2)
				{
					flag2 = true;
					flag = true;
					isNegative = true;
					i++;
				}
				else if (flag6 && c == nfi.NegativeSign[0] && !flag)
				{
					int length2 = nfi.NegativeSign.Length;
					if (length2 == 1 || s.IndexOf(nfi.NegativeSign, i, length2) == i)
					{
						flag = true;
						isNegative = true;
						i += length2;
					}
				}
				else if (flag6 && c == nfi.PositiveSign[0] && !flag)
				{
					int length3 = nfi.PositiveSign.Length;
					if (length3 == 1 || s.IndexOf(nfi.PositiveSign, i, length3) == i)
					{
						flag = true;
						i += length3;
					}
				}
				else
				{
					if (flag10 && c == text[0])
					{
						int length4 = text.Length;
						if (length4 != 1 && s.IndexOf(text, i, length4) != i)
						{
							if (!throwex)
							{
								return null;
							}
							decimal.ThrowAtPos(i);
						}
						break;
					}
					if (!throwex)
					{
						return null;
					}
					decimal.ThrowAtPos(i);
				}
			}
			if (i == length)
			{
				if (throwex)
				{
					throw new FormatException(Locale.GetText("No digits found"));
				}
				return null;
			}
			else
			{
				while (i < length)
				{
					char c2 = s[i];
					if (char.IsDigit(c2))
					{
						stringBuilder.Append(c2);
						i++;
					}
					else if (flag9 && c2 == text2[0])
					{
						int length5 = text2.Length;
						if (length5 != 1 && s.IndexOf(text2, i, length5) != i)
						{
							if (!throwex)
							{
								return null;
							}
							decimal.ThrowAtPos(i);
						}
						i += length5;
					}
					else
					{
						if (!flag10 || c2 != text[0] || flag3)
						{
							break;
						}
						int length6 = text.Length;
						if (length6 == 1 || s.IndexOf(text, i, length6) == i)
						{
							decPos = stringBuilder.Length;
							flag3 = true;
							i += length6;
						}
					}
				}
				if (i < length)
				{
					char c3 = s[i];
					if (flag11 && char.ToUpperInvariant(c3) == 'E')
					{
						expFlag = true;
						i++;
						if (i >= length)
						{
							if (!throwex)
							{
								return null;
							}
							decimal.ThrowInvalidExp();
						}
						c3 = s[i];
						bool flag13 = false;
						if (c3 == nfi.PositiveSign[0])
						{
							int length7 = nfi.PositiveSign.Length;
							if (length7 == 1 || s.IndexOf(nfi.PositiveSign, i, length7) == i)
							{
								i += length7;
								if (i >= length)
								{
									if (!throwex)
									{
										return null;
									}
									decimal.ThrowInvalidExp();
								}
							}
						}
						else if (c3 == nfi.NegativeSign[0])
						{
							int length8 = nfi.NegativeSign.Length;
							if (length8 == 1 || s.IndexOf(nfi.NegativeSign, i, length8) == i)
							{
								i += length8;
								if (i >= length)
								{
									if (!throwex)
									{
										return null;
									}
									decimal.ThrowInvalidExp();
								}
								flag13 = true;
							}
						}
						c3 = s[i];
						if (!char.IsDigit(c3))
						{
							if (!throwex)
							{
								return null;
							}
							decimal.ThrowInvalidExp();
						}
						exp = (int)(c3 - '0');
						i++;
						while (i < length && char.IsDigit(s[i]))
						{
							exp *= 10;
							exp += (int)(s[i] - '0');
							i++;
						}
						if (flag13)
						{
							exp *= -1;
						}
					}
				}
				while (i < length)
				{
					char c4 = s[i];
					if (flag5 && char.IsWhiteSpace(c4))
					{
						i++;
					}
					else if (flag8 && c4 == ')' && flag2)
					{
						flag2 = false;
						i++;
					}
					else if (flag7 && c4 == nfi.NegativeSign[0] && !flag)
					{
						int length9 = nfi.NegativeSign.Length;
						if (length9 == 1 || s.IndexOf(nfi.NegativeSign, i, length9) == i)
						{
							flag = true;
							isNegative = true;
							i += length9;
						}
					}
					else if (flag7 && c4 == nfi.PositiveSign[0] && !flag)
					{
						int length10 = nfi.PositiveSign.Length;
						if (length10 == 1 || s.IndexOf(nfi.PositiveSign, i, length10) == i)
						{
							flag = true;
							i += length10;
						}
					}
					else
					{
						if (!throwex)
						{
							return null;
						}
						decimal.ThrowAtPos(i);
					}
				}
				if (!flag2)
				{
					if (!flag3)
					{
						decPos = stringBuilder.Length;
					}
					return stringBuilder.ToString();
				}
				if (throwex)
				{
					throw new FormatException(Locale.GetText("Closing Parentheses not found"));
				}
				return null;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001FF68 File Offset: 0x0001E168
		public static decimal Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException("Decimal.TryParse does not accept AllowHexSpecifier", "style");
			}
			decimal result;
			decimal.PerformParse(s, style, provider, out result, true);
			return result;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		private static bool PerformParse(string s, NumberStyles style, IFormatProvider provider, out decimal res, bool throwex)
		{
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			int num;
			bool flag;
			bool flag2;
			int exp;
			s = decimal.stripStyles(s, style, instance, out num, out flag, out flag2, out exp, throwex);
			if (s == null)
			{
				res = 0m;
				return false;
			}
			if (num < 0)
			{
				if (throwex)
				{
					throw new Exception(Locale.GetText("Error in System.Decimal.Parse"));
				}
				res = 0m;
				return false;
			}
			else
			{
				int length = s.Length;
				int num2 = 0;
				while (num2 < num && s[num2] == '0')
				{
					num2++;
				}
				if (num2 > 1 && length > 1)
				{
					s = s.Substring(num2, length - num2);
					num -= num2;
				}
				int num3 = (num != 0) ? 28 : 27;
				length = s.Length;
				if (length >= num3 + 1 && string.Compare(s, 0, "79228162514264337593543950335", 0, num3 + 1, false, CultureInfo.InvariantCulture) <= 0)
				{
					num3++;
				}
				if (length > num3 && num < length)
				{
					int num4 = (int)(s[num3] - '0');
					s = s.Substring(0, num3);
					bool flag3 = false;
					if (num4 > 5)
					{
						flag3 = true;
					}
					else if (num4 == 5)
					{
						if (flag)
						{
							flag3 = true;
						}
						else
						{
							int num5 = (int)(s[num3 - 1] - '0');
							flag3 = ((num5 & 1) == 1);
						}
					}
					if (flag3)
					{
						char[] array = s.ToCharArray();
						int i = num3 - 1;
						while (i >= 0)
						{
							int num6 = (int)(array[i] - '0');
							if (array[i] != '9')
							{
								array[i] = (char)(num6 + 49);
								break;
							}
							array[i--] = '0';
						}
						if (i == -1 && array[0] == '0')
						{
							num++;
							s = "1".PadRight(num, '0');
						}
						else
						{
							s = new string(array);
						}
					}
				}
				decimal num7;
				if (decimal.string2decimal(out num7, s, (uint)num, 0) != 0)
				{
					if (throwex)
					{
						throw new OverflowException();
					}
					res = 0m;
					return false;
				}
				else
				{
					if (!flag2 || decimal.decimalSetExponent(ref num7, exp) == 0)
					{
						if (flag)
						{
							num7.flags ^= 2147483648U;
						}
						res = num7;
						return true;
					}
					if (throwex)
					{
						throw new OverflowException();
					}
					res = 0m;
					return false;
				}
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00020220 File Offset: 0x0001E420
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00020224 File Offset: 0x0001E424
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00020234 File Offset: 0x0001E434
		public override string ToString()
		{
			return this.ToString("G", null);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00020244 File Offset: 0x0001E444
		public string ToString(IFormatProvider provider)
		{
			return this.ToString("G", provider);
		}

		// Token: 0x06000833 RID: 2099
		[MethodImpl(4096)]
		private static extern int decimal2UInt64(ref decimal val, out ulong result);

		// Token: 0x06000834 RID: 2100
		[MethodImpl(4096)]
		private static extern int decimal2Int64(ref decimal val, out long result);

		// Token: 0x06000835 RID: 2101
		[MethodImpl(4096)]
		private static extern int decimalIncr(ref decimal d1, ref decimal d2);

		// Token: 0x06000836 RID: 2102
		[MethodImpl(4096)]
		internal static extern int string2decimal(out decimal val, string sDigits, uint decPos, int sign);

		// Token: 0x06000837 RID: 2103
		[MethodImpl(4096)]
		internal static extern int decimalSetExponent(ref decimal val, int exp);

		// Token: 0x06000838 RID: 2104
		[MethodImpl(4096)]
		private static extern double decimal2double(ref decimal val);

		// Token: 0x06000839 RID: 2105
		[MethodImpl(4096)]
		private static extern void decimalFloorAndTrunc(ref decimal val, int floorFlag);

		// Token: 0x0600083A RID: 2106
		[MethodImpl(4096)]
		private static extern int decimalMult(ref decimal pd1, ref decimal pd2);

		// Token: 0x0600083B RID: 2107
		[MethodImpl(4096)]
		private static extern int decimalDiv(out decimal pc, ref decimal pa, ref decimal pb);

		// Token: 0x0600083C RID: 2108
		[MethodImpl(4096)]
		private static extern int decimalCompare(ref decimal d1, ref decimal d2);

		// Token: 0x0600083D RID: 2109 RVA: 0x00020254 File Offset: 0x0001E454
		public static decimal operator +(decimal d1, decimal d2)
		{
			return decimal.Add(d1, d2);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00020260 File Offset: 0x0001E460
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00020270 File Offset: 0x0001E470
		public static decimal operator -(decimal d1, decimal d2)
		{
			return decimal.Subtract(d1, d2);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002027C File Offset: 0x0001E47C
		public static decimal operator *(decimal d1, decimal d2)
		{
			return decimal.Multiply(d1, d2);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00020288 File Offset: 0x0001E488
		public static decimal operator /(decimal d1, decimal d2)
		{
			return decimal.Divide(d1, d2);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00020294 File Offset: 0x0001E494
		public static explicit operator byte(decimal value)
		{
			ulong num = decimal.u64(value);
			return checked((byte)num);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000202AC File Offset: 0x0001E4AC
		[CLSCompliant(false)]
		public static explicit operator sbyte(decimal value)
		{
			long num = decimal.s64(value);
			return checked((sbyte)num);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000202C4 File Offset: 0x0001E4C4
		public static explicit operator short(decimal value)
		{
			long num = decimal.s64(value);
			return checked((short)num);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000202DC File Offset: 0x0001E4DC
		[CLSCompliant(false)]
		public static explicit operator ushort(decimal value)
		{
			ulong num = decimal.u64(value);
			return checked((ushort)num);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public static explicit operator int(decimal value)
		{
			long num = decimal.s64(value);
			return checked((int)num);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002030C File Offset: 0x0001E50C
		[CLSCompliant(false)]
		public static explicit operator uint(decimal value)
		{
			ulong num = decimal.u64(value);
			return checked((uint)num);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00020324 File Offset: 0x0001E524
		public static explicit operator long(decimal value)
		{
			return decimal.s64(value);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002032C File Offset: 0x0001E52C
		[CLSCompliant(false)]
		public static explicit operator ulong(decimal value)
		{
			return decimal.u64(value);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00020334 File Offset: 0x0001E534
		public static implicit operator decimal(byte value)
		{
			return new decimal((int)value);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002033C File Offset: 0x0001E53C
		[CLSCompliant(false)]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00020348 File Offset: 0x0001E548
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00020350 File Offset: 0x0001E550
		[CLSCompliant(false)]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((int)value);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00020358 File Offset: 0x0001E558
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00020360 File Offset: 0x0001E560
		[CLSCompliant(false)]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00020368 File Offset: 0x0001E568
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00020370 File Offset: 0x0001E570
		[CLSCompliant(false)]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00020378 File Offset: 0x0001E578
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00020380 File Offset: 0x0001E580
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00020388 File Offset: 0x0001E588
		public static explicit operator float(decimal value)
		{
			return (float)((double)value);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00020394 File Offset: 0x0001E594
		public static explicit operator double(decimal value)
		{
			return decimal.decimal2double(ref value);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000203A0 File Offset: 0x0001E5A0
		public static bool operator !=(decimal d1, decimal d2)
		{
			return !decimal.Equals(d1, d2);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000203AC File Offset: 0x0001E5AC
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.Equals(d1, d2);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000203B8 File Offset: 0x0001E5B8
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.Compare(d1, d2) > 0;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000203C4 File Offset: 0x0001E5C4
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.Compare(d1, d2) < 0;
		}

		// Token: 0x040002A9 RID: 681
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x040002AA RID: 682
		public const decimal MaxValue = 79228162514264337593543950335m;

		// Token: 0x040002AB RID: 683
		public const decimal MinusOne = -1m;

		// Token: 0x040002AC RID: 684
		public const decimal One = 1m;

		// Token: 0x040002AD RID: 685
		public const decimal Zero = 0m;

		// Token: 0x040002AE RID: 686
		private const int DECIMAL_DIVIDE_BY_ZERO = 5;

		// Token: 0x040002AF RID: 687
		private const uint MAX_SCALE = 28U;

		// Token: 0x040002B0 RID: 688
		private const int iMAX_SCALE = 28;

		// Token: 0x040002B1 RID: 689
		private const uint SIGN_FLAG = 2147483648U;

		// Token: 0x040002B2 RID: 690
		private const uint SCALE_MASK = 16711680U;

		// Token: 0x040002B3 RID: 691
		private const int SCALE_SHIFT = 16;

		// Token: 0x040002B4 RID: 692
		private const uint RESERVED_SS32_BITS = 2130771967U;

		// Token: 0x040002B5 RID: 693
		private static readonly decimal MaxValueDiv10 = 7922816251426433759354395033.5m;

		// Token: 0x040002B6 RID: 694
		private uint flags;

		// Token: 0x040002B7 RID: 695
		private uint hi;

		// Token: 0x040002B8 RID: 696
		private uint lo;

		// Token: 0x040002B9 RID: 697
		private uint mid;
	}
}
