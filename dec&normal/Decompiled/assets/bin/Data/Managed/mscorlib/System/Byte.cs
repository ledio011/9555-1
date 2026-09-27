using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000073 RID: 115
	[ComVisible(true)]
	[Serializable]
	public struct Byte : IComparable<byte>, IEquatable<byte>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x0600037C RID: 892 RVA: 0x000122CC File Offset: 0x000104CC
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000122F0 File Offset: 0x000104F0
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000122FC File Offset: 0x000104FC
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00012300 File Offset: 0x00010500
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001230C File Offset: 0x0001050C
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012314 File Offset: 0x00010514
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00012320 File Offset: 0x00010520
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001232C File Offset: 0x0001052C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00012338 File Offset: 0x00010538
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00012344 File Offset: 0x00010544
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00012350 File Offset: 0x00010550
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001235C File Offset: 0x0001055C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00012368 File Offset: 0x00010568
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00012374 File Offset: 0x00010574
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00012380 File Offset: 0x00010580
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001238C File Offset: 0x0001058C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is byte))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Byte."));
			}
			byte b = (byte)value;
			if (this == b)
			{
				return 0;
			}
			if (this > b)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000123D8 File Offset: 0x000105D8
		public override bool Equals(object obj)
		{
			return obj is byte && (byte)obj == this;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000123F4 File Offset: 0x000105F4
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000123F8 File Offset: 0x000105F8
		public int CompareTo(byte value)
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

		// Token: 0x0600038F RID: 911 RVA: 0x00012410 File Offset: 0x00010610
		public bool Equals(byte obj)
		{
			return this == obj;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00012418 File Offset: 0x00010618
		public static byte Parse(string s, IFormatProvider provider)
		{
			return byte.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012424 File Offset: 0x00010624
		public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			uint num = uint.Parse(s, style, provider);
			if (num > 255U)
			{
				throw new OverflowException(Locale.GetText("Value too large."));
			}
			return (byte)num;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00012458 File Offset: 0x00010658
		public static bool TryParse(string s, out byte result)
		{
			return byte.TryParse(s, NumberStyles.Integer, null, out result);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00012464 File Offset: 0x00010664
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			result = 0;
			uint num;
			if (!uint.TryParse(s, style, provider, out num))
			{
				return false;
			}
			if (num > 255U)
			{
				return false;
			}
			result = (byte)num;
			return true;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00012498 File Offset: 0x00010698
		public override string ToString()
		{
			return NumberFormatter.NumberToString((int)this, null);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000124A4 File Offset: 0x000106A4
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000124B0 File Offset: 0x000106B0
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString((int)this, provider);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000124BC File Offset: 0x000106BC
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000124C8 File Offset: 0x000106C8
		public TypeCode GetTypeCode()
		{
			return TypeCode.Byte;
		}

		// Token: 0x040001B6 RID: 438
		public const byte MinValue = 0;

		// Token: 0x040001B7 RID: 439
		public const byte MaxValue = 255;

		// Token: 0x040001B8 RID: 440
		internal byte m_value;
	}
}
