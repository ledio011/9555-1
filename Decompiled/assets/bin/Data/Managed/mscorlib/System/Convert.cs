using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace System
{
	// Token: 0x020000C1 RID: 193
	public static class Convert
	{
		// Token: 0x0600069F RID: 1695
		[MethodImpl(4096)]
		private static extern byte[] InternalFromBase64String(string str, bool allowWhitespaceOnly);

		// Token: 0x060006A0 RID: 1696 RVA: 0x00019FFC File Offset: 0x000181FC
		public static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return new byte[0];
			}
			return Convert.InternalFromBase64String(s, true);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001A028 File Offset: 0x00018228
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001A048 File Offset: 0x00018248
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (offset < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException("offset < 0 || length < 0");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset + length > array.Length");
			}
			byte[] bytes = ToBase64Transform.InternalTransformFinalBlock(inArray, offset, length);
			return new ASCIIEncoding().GetString(bytes);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001A0AC File Offset: 0x000182AC
		public static bool ToBoolean(byte value)
		{
			return value != 0;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001A0B8 File Offset: 0x000182B8
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001A0C8 File Offset: 0x000182C8
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001A0DC File Offset: 0x000182DC
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001A0EC File Offset: 0x000182EC
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001A0F8 File Offset: 0x000182F8
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001A104 File Offset: 0x00018304
		[CLSCompliant(false)]
		public static bool ToBoolean(sbyte value)
		{
			return (int)value != 0;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001A110 File Offset: 0x00018310
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001A11C File Offset: 0x0001831C
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001A12C File Offset: 0x0001832C
		[CLSCompliant(false)]
		public static bool ToBoolean(uint value)
		{
			return value != 0U;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001A138 File Offset: 0x00018338
		[CLSCompliant(false)]
		public static bool ToBoolean(ulong value)
		{
			return value != 0UL;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001A144 File Offset: 0x00018344
		[CLSCompliant(false)]
		public static bool ToBoolean(ushort value)
		{
			return value != 0;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001A150 File Offset: 0x00018350
		public static bool ToBoolean(object value)
		{
			return value != null && Convert.ToBoolean(value, null);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001A164 File Offset: 0x00018364
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001A17C File Offset: 0x0001837C
		public static byte ToByte(bool value)
		{
			return (!value) ? 0 : 1;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001A18C File Offset: 0x0001838C
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001A1AC File Offset: 0x000183AC
		public static byte ToByte(decimal value)
		{
			if (value > 255m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)Math.Round(value);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001A1FC File Offset: 0x000183FC
		public static byte ToByte(double value)
		{
			if (value > 255.0 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			if (double.IsNaN(value) || double.IsInfinity(value))
			{
				throw new OverflowException(Locale.GetText("Value is equal to Double.NaN, Double.PositiveInfinity, or Double.NegativeInfinity"));
			}
			return (byte)Math.Round(value);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001A264 File Offset: 0x00018464
		public static byte ToByte(float value)
		{
			if (value > 255f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.Minalue"));
			}
			if (float.IsNaN(value) || float.IsInfinity(value))
			{
				throw new OverflowException(Locale.GetText("Value is equal to Single.NaN, Single.PositiveInfinity, or Single.NegativeInfinity"));
			}
			return (byte)Math.Round((double)value);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001A2C8 File Offset: 0x000184C8
		public static byte ToByte(int value)
		{
			if (value > 255 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001A2F0 File Offset: 0x000184F0
		public static byte ToByte(long value)
		{
			if (value > 255L || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001A318 File Offset: 0x00018518
		[CLSCompliant(false)]
		public static byte ToByte(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than Byte.MinValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001A334 File Offset: 0x00018534
		public static byte ToByte(short value)
		{
			if (value > 255 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001A35C File Offset: 0x0001855C
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, provider);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001A370 File Offset: 0x00018570
		[CLSCompliant(false)]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001A390 File Offset: 0x00018590
		[CLSCompliant(false)]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001A3B0 File Offset: 0x000185B0
		[CLSCompliant(false)]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001A3D0 File Offset: 0x000185D0
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToByte(provider);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001A3E8 File Offset: 0x000185E8
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001A3EC File Offset: 0x000185EC
		public static char ToChar(int value)
		{
			if (value > 65535 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue or less than Char.MinValue"));
			}
			return (char)value;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001A414 File Offset: 0x00018614
		public static char ToChar(long value)
		{
			if (value > 65535L || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue or less than Char.MinValue"));
			}
			return (char)value;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001A43C File Offset: 0x0001863C
		public static char ToChar(float value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001A448 File Offset: 0x00018648
		[CLSCompliant(false)]
		public static char ToChar(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than Char.MinValue"));
			}
			return (char)value;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001A464 File Offset: 0x00018664
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than Char.MinValue"));
			}
			return (char)value;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001A480 File Offset: 0x00018680
		public static char ToChar(string value, IFormatProvider provider)
		{
			return char.Parse(value);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001A488 File Offset: 0x00018688
		[CLSCompliant(false)]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue"));
			}
			return (char)value;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001A4A8 File Offset: 0x000186A8
		[CLSCompliant(false)]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue"));
			}
			return (char)value;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001A4C8 File Offset: 0x000186C8
		[CLSCompliant(false)]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001A4CC File Offset: 0x000186CC
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return '\0';
			}
			return ((IConvertible)value).ToChar(provider);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001A4E4 File Offset: 0x000186E4
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return DateTime.MinValue;
			}
			return DateTime.Parse(value, provider);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001A4FC File Offset: 0x000186FC
		public static DateTime ToDateTime(short value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001A508 File Offset: 0x00018708
		public static DateTime ToDateTime(int value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001A514 File Offset: 0x00018714
		public static DateTime ToDateTime(long value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001A520 File Offset: 0x00018720
		public static DateTime ToDateTime(float value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001A52C File Offset: 0x0001872C
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return DateTime.MinValue;
			}
			return ((IConvertible)value).ToDateTime(provider);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001A548 File Offset: 0x00018748
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001A554 File Offset: 0x00018754
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001A560 File Offset: 0x00018760
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001A56C File Offset: 0x0001876C
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001A578 File Offset: 0x00018778
		public static decimal ToDecimal(bool value)
		{
			return (!value) ? 0 : 1;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001A58C File Offset: 0x0001878C
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001A594 File Offset: 0x00018794
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001A59C File Offset: 0x0001879C
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001A5A4 File Offset: 0x000187A4
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001A5AC File Offset: 0x000187AC
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001A5B4 File Offset: 0x000187B4
		[CLSCompliant(false)]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001A5BC File Offset: 0x000187BC
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001A5C4 File Offset: 0x000187C4
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, provider);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001A5DC File Offset: 0x000187DC
		[CLSCompliant(false)]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001A5E4 File Offset: 0x000187E4
		[CLSCompliant(false)]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001A5EC File Offset: 0x000187EC
		[CLSCompliant(false)]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001A5F4 File Offset: 0x000187F4
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return ((IConvertible)value).ToDecimal(provider);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001A610 File Offset: 0x00018810
		public static double ToDouble(bool value)
		{
			return (double)((!value) ? 0 : 1);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001A620 File Offset: 0x00018820
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001A624 File Offset: 0x00018824
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001A62C File Offset: 0x0001882C
		public static double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001A630 File Offset: 0x00018830
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001A634 File Offset: 0x00018834
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001A638 File Offset: 0x00018838
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001A63C File Offset: 0x0001883C
		[CLSCompliant(false)]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001A640 File Offset: 0x00018840
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001A644 File Offset: 0x00018844
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, provider);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001A660 File Offset: 0x00018860
		[CLSCompliant(false)]
		public static double ToDouble(uint value)
		{
			return value;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001A668 File Offset: 0x00018868
		[CLSCompliant(false)]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001A670 File Offset: 0x00018870
		[CLSCompliant(false)]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001A674 File Offset: 0x00018874
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return ((IConvertible)value).ToDouble(provider);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001A694 File Offset: 0x00018894
		public static short ToInt16(bool value)
		{
			return (!value) ? 0 : 1;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001A6A4 File Offset: 0x000188A4
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001A6A8 File Offset: 0x000188A8
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001A6C8 File Offset: 0x000188C8
		public static short ToInt16(decimal value)
		{
			if (value > 32767m || value < -32768m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)Math.Round(value);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001A71C File Offset: 0x0001891C
		public static short ToInt16(double value)
		{
			if (value > 32767.0 || value < -32768.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)Math.Round(value);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001A754 File Offset: 0x00018954
		public static short ToInt16(float value)
		{
			if (value > 32767f || value < -32768f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)Math.Round((double)value);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001A784 File Offset: 0x00018984
		public static short ToInt16(int value)
		{
			if (value > 32767 || value < -32768)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)value;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001A7B0 File Offset: 0x000189B0
		public static short ToInt16(long value)
		{
			if (value > 32767L || value < -32768L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)value;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001A7DC File Offset: 0x000189DC
		[CLSCompliant(false)]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001A7E0 File Offset: 0x000189E0
		public static short ToInt16(short value)
		{
			return value;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001A7E4 File Offset: 0x000189E4
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, provider);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001A7F8 File Offset: 0x000189F8
		[CLSCompliant(false)]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001A81C File Offset: 0x00018A1C
		[CLSCompliant(false)]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A83C File Offset: 0x00018A3C
		[CLSCompliant(false)]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A85C File Offset: 0x00018A5C
		public static short ToInt16(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToInt16(value, null);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A870 File Offset: 0x00018A70
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToInt16(provider);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001A888 File Offset: 0x00018A88
		public static int ToInt32(bool value)
		{
			return (!value) ? 0 : 1;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001A898 File Offset: 0x00018A98
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001A89C File Offset: 0x00018A9C
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		public static int ToInt32(decimal value)
		{
			if (value > 2147483647m || value < -2147483648m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return (int)Math.Round(value);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001A8F4 File Offset: 0x00018AF4
		public static int ToInt32(double value)
		{
			if (value > 2147483647.0 || value < -2147483648.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return checked((int)Math.Round(value));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001A92C File Offset: 0x00018B2C
		public static int ToInt32(float value)
		{
			if (value > 2.1474836E+09f || value < -2.1474836E+09f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return checked((int)Math.Round((double)value));
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001A95C File Offset: 0x00018B5C
		public static int ToInt32(long value)
		{
			if (value > 2147483647L || value < -2147483648L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return (int)value;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001A988 File Offset: 0x00018B88
		[CLSCompliant(false)]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001A98C File Offset: 0x00018B8C
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001A990 File Offset: 0x00018B90
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, provider);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		[CLSCompliant(false)]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue"));
			}
			return (int)value;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		[CLSCompliant(false)]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue"));
			}
			return (int)value;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		[CLSCompliant(false)]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001A9E8 File Offset: 0x00018BE8
		public static int ToInt32(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToInt32(value, null);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001A9FC File Offset: 0x00018BFC
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToInt32(provider);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001AA14 File Offset: 0x00018C14
		public static long ToInt64(bool value)
		{
			return (!value) ? 0L : 1L;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001AA24 File Offset: 0x00018C24
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001AA28 File Offset: 0x00018C28
		public static long ToInt64(char value)
		{
			return (long)value;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001AA2C File Offset: 0x00018C2C
		public static long ToInt64(decimal value)
		{
			if (value > 9223372036854775807m || value < -9223372036854775808m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue or less than Int64.MinValue"));
			}
			return (long)Math.Round(value);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001AA88 File Offset: 0x00018C88
		public static long ToInt64(double value)
		{
			if (value > 9.223372036854776E+18 || value < -9.223372036854776E+18)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue or less than Int64.MinValue"));
			}
			return (long)Math.Round(value);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		public static long ToInt64(float value)
		{
			if (value > 9.223372E+18f || value < -9.223372E+18f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue or less than Int64.MinValue"));
			}
			return (long)Math.Round((double)value);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		public static long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		[CLSCompliant(false)]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001AB00 File Offset: 0x00018D00
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, provider);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001AB14 File Offset: 0x00018D14
		[CLSCompliant(false)]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001AB18 File Offset: 0x00018D18
		[CLSCompliant(false)]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue"));
			}
			return (long)value;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001AB3C File Offset: 0x00018D3C
		[CLSCompliant(false)]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001AB40 File Offset: 0x00018D40
		public static long ToInt64(object value)
		{
			if (value == null)
			{
				return 0L;
			}
			return Convert.ToInt64(value, null);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001AB54 File Offset: 0x00018D54
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return ((IConvertible)value).ToInt64(provider);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001AB6C File Offset: 0x00018D6C
		[CLSCompliant(false)]
		public static sbyte ToSByte(bool value)
		{
			return (!value) ? 0 : 1;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001AB7C File Offset: 0x00018D7C
		[CLSCompliant(false)]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001AB98 File Offset: 0x00018D98
		[CLSCompliant(false)]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			if (value > 127m || value < -128m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)Math.Round(value);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001AC00 File Offset: 0x00018E00
		[CLSCompliant(false)]
		public static sbyte ToSByte(double value)
		{
			if (value > 127.0 || value < -128.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)Math.Round(value);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001AC38 File Offset: 0x00018E38
		[CLSCompliant(false)]
		public static sbyte ToSByte(float value)
		{
			if (value > 127f || value < -128f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.Minalue"));
			}
			return (sbyte)Math.Round((double)value);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001AC68 File Offset: 0x00018E68
		[CLSCompliant(false)]
		public static sbyte ToSByte(int value)
		{
			if (value > 127 || value < -128)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001AC8C File Offset: 0x00018E8C
		[CLSCompliant(false)]
		public static sbyte ToSByte(long value)
		{
			if (value > 127L || value < -128L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001ACB4 File Offset: 0x00018EB4
		[CLSCompliant(false)]
		public static sbyte ToSByte(short value)
		{
			if (value > 127 || value < -128)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return sbyte.Parse(value, provider);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		[CLSCompliant(false)]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001AD14 File Offset: 0x00018F14
		[CLSCompliant(false)]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001AD34 File Offset: 0x00018F34
		[CLSCompliant(false)]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001AD50 File Offset: 0x00018F50
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToSByte(provider);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001AD68 File Offset: 0x00018F68
		public static float ToSingle(bool value)
		{
			return (float)((!value) ? 0 : 1);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001AD78 File Offset: 0x00018F78
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001AD7C File Offset: 0x00018F7C
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001AD84 File Offset: 0x00018F84
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001AD88 File Offset: 0x00018F88
		public static float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001AD90 File Offset: 0x00018F90
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001AD94 File Offset: 0x00018F94
		[CLSCompliant(false)]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001AD98 File Offset: 0x00018F98
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001AD9C File Offset: 0x00018F9C
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, provider);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		[CLSCompliant(false)]
		public static float ToSingle(uint value)
		{
			return value;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001ADBC File Offset: 0x00018FBC
		[CLSCompliant(false)]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		[CLSCompliant(false)]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return ((IConvertible)value).ToSingle(provider);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		public static string ToString(object value, IFormatProvider provider)
		{
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToString(provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001AE1C File Offset: 0x0001901C
		[CLSCompliant(false)]
		public static ushort ToUInt16(bool value)
		{
			return (!value) ? 0 : 1;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001AE2C File Offset: 0x0001902C
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001AE30 File Offset: 0x00019030
		[CLSCompliant(false)]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001AE34 File Offset: 0x00019034
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			if (value > 65535m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)Math.Round(value);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001AE84 File Offset: 0x00019084
		[CLSCompliant(false)]
		public static ushort ToUInt16(double value)
		{
			if (value > 65535.0 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)Math.Round(value);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001AEBC File Offset: 0x000190BC
		[CLSCompliant(false)]
		public static ushort ToUInt16(float value)
		{
			if (value > 65535f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)Math.Round((double)value);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001AEEC File Offset: 0x000190EC
		[CLSCompliant(false)]
		public static ushort ToUInt16(int value)
		{
			if (value > 65535 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001AF14 File Offset: 0x00019114
		[CLSCompliant(false)]
		public static ushort ToUInt16(long value)
		{
			if (value > 65535L || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001AF3C File Offset: 0x0001913C
		[CLSCompliant(false)]
		public static ushort ToUInt16(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001AF58 File Offset: 0x00019158
		[CLSCompliant(false)]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001AF74 File Offset: 0x00019174
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, provider);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001AF88 File Offset: 0x00019188
		[CLSCompliant(false)]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue"));
			}
			return (ushort)value;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001AFA8 File Offset: 0x000191A8
		[CLSCompliant(false)]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue"));
			}
			return (ushort)value;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001AFC8 File Offset: 0x000191C8
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToUInt16(provider);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001AFE0 File Offset: 0x000191E0
		[CLSCompliant(false)]
		public static uint ToUInt32(bool value)
		{
			return (!value) ? 0U : 1U;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001AFF0 File Offset: 0x000191F0
		[CLSCompliant(false)]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001AFF4 File Offset: 0x000191F4
		[CLSCompliant(false)]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001AFF8 File Offset: 0x000191F8
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal value)
		{
			if (value > 4294967295m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)Math.Round(value);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001B048 File Offset: 0x00019248
		[CLSCompliant(false)]
		public static uint ToUInt32(double value)
		{
			if (value > 4294967295.0 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)Math.Round(value);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001B080 File Offset: 0x00019280
		[CLSCompliant(false)]
		public static uint ToUInt32(float value)
		{
			if (value > 4.2949673E+09f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)Math.Round((double)value);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001B0B0 File Offset: 0x000192B0
		[CLSCompliant(false)]
		public static uint ToUInt32(int value)
		{
			if ((long)value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001B0CC File Offset: 0x000192CC
		[CLSCompliant(false)]
		public static uint ToUInt32(long value)
		{
			if (value > (long)((ulong)-1) || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001B0F0 File Offset: 0x000192F0
		[CLSCompliant(false)]
		public static uint ToUInt32(sbyte value)
		{
			if ((long)value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001B110 File Offset: 0x00019310
		[CLSCompliant(false)]
		public static uint ToUInt32(short value)
		{
			if ((long)value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001B130 File Offset: 0x00019330
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, provider);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001B144 File Offset: 0x00019344
		[CLSCompliant(false)]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)-1)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue"));
			}
			return (uint)value;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001B160 File Offset: 0x00019360
		[CLSCompliant(false)]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001B164 File Offset: 0x00019364
		[CLSCompliant(false)]
		public static uint ToUInt32(object value)
		{
			if (value == null)
			{
				return 0U;
			}
			return Convert.ToUInt32(value, null);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001B178 File Offset: 0x00019378
		[CLSCompliant(false)]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return ((IConvertible)value).ToUInt32(provider);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001B190 File Offset: 0x00019390
		[CLSCompliant(false)]
		public static ulong ToUInt64(bool value)
		{
			return (ulong)((!value) ? 0L : 1L);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001B1A0 File Offset: 0x000193A0
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001B1A4 File Offset: 0x000193A4
		[CLSCompliant(false)]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001B1A8 File Offset: 0x000193A8
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal value)
		{
			if (value > 18446744073709551615m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt64.MaxValue or less than UInt64.MinValue"));
			}
			return (ulong)Math.Round(value);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001B1F8 File Offset: 0x000193F8
		[CLSCompliant(false)]
		public static ulong ToUInt64(double value)
		{
			if (value > 1.8446744073709552E+19 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt64.MaxValue or less than UInt64.MinValue"));
			}
			return (ulong)Math.Round(value);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001B230 File Offset: 0x00019430
		[CLSCompliant(false)]
		public static ulong ToUInt64(float value)
		{
			if (value > 1.8446744E+19f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt64.MaxValue or less than UInt64.MinValue"));
			}
			return (ulong)Math.Round((double)value);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001B260 File Offset: 0x00019460
		[CLSCompliant(false)]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt64.MinValue"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001B27C File Offset: 0x0001947C
		[CLSCompliant(false)]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt64.MinValue"));
			}
			return (ulong)value;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001B298 File Offset: 0x00019498
		[CLSCompliant(false)]
		public static ulong ToUInt64(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException("Value is less than UInt64.MinValue");
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001B2B0 File Offset: 0x000194B0
		[CLSCompliant(false)]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt64.MinValue"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001B2CC File Offset: 0x000194CC
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, provider);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001B2E0 File Offset: 0x000194E0
		[CLSCompliant(false)]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001B2E4 File Offset: 0x000194E4
		[CLSCompliant(false)]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001B2E8 File Offset: 0x000194E8
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return Convert.ToUInt64(value, null);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001B2FC File Offset: 0x000194FC
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ((IConvertible)value).ToUInt64(provider);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001B314 File Offset: 0x00019514
		public static object ChangeType(object value, Type conversionType)
		{
			if (value != null && conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			IFormatProvider provider;
			if (conversionType == typeof(DateTime))
			{
				provider = currentCulture.DateTimeFormat;
			}
			else
			{
				provider = currentCulture.NumberFormat;
			}
			return Convert.ToType(value, conversionType, provider, true);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001B36C File Offset: 0x0001956C
		internal static object ToType(object value, Type conversionType, IFormatProvider provider, bool try_target_to_type)
		{
			if (value == null)
			{
				if (conversionType != null && conversionType.IsValueType)
				{
					throw new InvalidCastException("Null object can not be converted to a value type.");
				}
				return null;
			}
			else
			{
				if (conversionType == null)
				{
					throw new InvalidCastException("Cannot cast to destination type.");
				}
				if (value.GetType() == conversionType)
				{
					return value;
				}
				if (value is IConvertible)
				{
					IConvertible convertible = (IConvertible)value;
					if (conversionType == Convert.conversionTable[0])
					{
						throw new ArgumentNullException();
					}
					if (conversionType == Convert.conversionTable[1])
					{
						return value;
					}
					if (conversionType == Convert.conversionTable[2])
					{
						throw new InvalidCastException("Cannot cast to DBNull, it's not IConvertible");
					}
					if (conversionType == Convert.conversionTable[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (conversionType == Convert.conversionTable[4])
					{
						return convertible.ToChar(provider);
					}
					if (conversionType == Convert.conversionTable[5])
					{
						return convertible.ToSByte(provider);
					}
					if (conversionType == Convert.conversionTable[6])
					{
						return convertible.ToByte(provider);
					}
					if (conversionType == Convert.conversionTable[7])
					{
						return convertible.ToInt16(provider);
					}
					if (conversionType == Convert.conversionTable[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (conversionType == Convert.conversionTable[9])
					{
						return convertible.ToInt32(provider);
					}
					if (conversionType == Convert.conversionTable[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (conversionType == Convert.conversionTable[11])
					{
						return convertible.ToInt64(provider);
					}
					if (conversionType == Convert.conversionTable[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (conversionType == Convert.conversionTable[13])
					{
						return convertible.ToSingle(provider);
					}
					if (conversionType == Convert.conversionTable[14])
					{
						return convertible.ToDouble(provider);
					}
					if (conversionType == Convert.conversionTable[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (conversionType == Convert.conversionTable[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (conversionType == Convert.conversionTable[18])
					{
						return convertible.ToString(provider);
					}
					if (try_target_to_type)
					{
						return convertible.ToType(conversionType, provider);
					}
				}
				throw new InvalidCastException(Locale.GetText("Value is not a convertible object: " + value.GetType().ToString() + " to " + conversionType.FullName));
			}
		}

		// Token: 0x0400026D RID: 621
		private const int MaxBytesPerLine = 57;

		// Token: 0x0400026E RID: 622
		public static readonly object DBNull = System.DBNull.Value;

		// Token: 0x0400026F RID: 623
		private static readonly Type[] conversionTable = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			null,
			typeof(string)
		};
	}
}
