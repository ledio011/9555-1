using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200031F RID: 799
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public struct SByte : IComparable<sbyte>, IEquatable<sbyte>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x00058460 File Offset: 0x00056660
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0005846C File Offset: 0x0005666C
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00058478 File Offset: 0x00056678
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00058484 File Offset: 0x00056684
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00058490 File Offset: 0x00056690
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0005849C File Offset: 0x0005669C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000584A8 File Offset: 0x000566A8
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000584B4 File Offset: 0x000566B4
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000584C0 File Offset: 0x000566C0
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000584CC File Offset: 0x000566CC
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000584D0 File Offset: 0x000566D0
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000584DC File Offset: 0x000566DC
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00058500 File Offset: 0x00056700
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0005850C File Offset: 0x0005670C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00058518 File Offset: 0x00056718
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00058524 File Offset: 0x00056724
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is sbyte))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.SByte."));
			}
			sbyte b = (sbyte)obj;
			if ((int)this == (int)b)
			{
				return 0;
			}
			if ((int)this > (int)b)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00058574 File Offset: 0x00056774
		public override bool Equals(object obj)
		{
			return obj is sbyte && (int)((sbyte)obj) == (int)this;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00058590 File Offset: 0x00056790
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00058598 File Offset: 0x00056798
		public int CompareTo(sbyte value)
		{
			if ((int)this == (int)value)
			{
				return 0;
			}
			if ((int)this > (int)value)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000585B4 File Offset: 0x000567B4
		public bool Equals(sbyte obj)
		{
			return (int)obj == (int)this;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000585C0 File Offset: 0x000567C0
		internal static bool Parse(string s, bool tryParse, out sbyte result, out Exception exc)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			result = 0;
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
			char c;
			for (i = 0; i < length; i++)
			{
				c = s[i];
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
			c = s[i];
			if (c == '+')
			{
				i++;
			}
			else if (c == '-')
			{
				flag = true;
				i++;
			}
			while (i < length)
			{
				c = s[i];
				if (c >= '0' && c <= '9')
				{
					if (tryParse)
					{
						int num2 = num * 10 - (int)(c - '0');
						if (num2 < -128)
						{
							return false;
						}
						num = (int)((sbyte)num2);
					}
					else
					{
						num = checked(num * 10 - (int)(c - '0'));
					}
					flag2 = true;
					i++;
				}
				else
				{
					if (char.IsWhiteSpace(c))
					{
						for (i++; i < length; i++)
						{
							if (!char.IsWhiteSpace(s[i]))
							{
								if (!tryParse)
								{
									exc = int.GetFormatException();
								}
								return false;
							}
						}
						break;
					}
					if (!tryParse)
					{
						exc = int.GetFormatException();
					}
					return false;
				}
			}
			if (!flag2)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			num = ((!flag) ? (-num) : num);
			if (num < -128 || num > 127)
			{
				if (!tryParse)
				{
					exc = new OverflowException();
				}
				return false;
			}
			result = (sbyte)num;
			return true;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005876C File Offset: 0x0005696C
		[CLSCompliant(false)]
		public static sbyte Parse(string s, IFormatProvider provider)
		{
			return sbyte.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00058778 File Offset: 0x00056978
		[CLSCompliant(false)]
		public static sbyte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			int num = int.Parse(s, style, provider);
			if (num > 127 || num < -128)
			{
				throw new OverflowException(Locale.GetText("Value too large or too small."));
			}
			return (sbyte)num;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000587B0 File Offset: 0x000569B0
		[CLSCompliant(false)]
		public static bool TryParse(string s, out sbyte result)
		{
			Exception ex;
			if (!sbyte.Parse(s, true, out result, out ex))
			{
				result = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000587D4 File Offset: 0x000569D4
		public override string ToString()
		{
			return NumberFormatter.NumberToString((int)this, null);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000587E0 File Offset: 0x000569E0
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString((int)this, provider);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x000587EC File Offset: 0x000569EC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000587F8 File Offset: 0x000569F8
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00058804 File Offset: 0x00056A04
		public TypeCode GetTypeCode()
		{
			return TypeCode.SByte;
		}

		// Token: 0x04000CA0 RID: 3232
		public const sbyte MinValue = -128;

		// Token: 0x04000CA1 RID: 3233
		public const sbyte MaxValue = 127;

		// Token: 0x04000CA2 RID: 3234
		internal sbyte m_value;
	}
}
