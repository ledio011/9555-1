using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003D2 RID: 978
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public struct UInt16 : IComparable<ushort>, IEquatable<ushort>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x0007097C File Offset: 0x0006EB7C
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00070988 File Offset: 0x0006EB88
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00070994 File Offset: 0x0006EB94
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000709A0 File Offset: 0x0006EBA0
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000709AC File Offset: 0x0006EBAC
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000709B8 File Offset: 0x0006EBB8
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000709C4 File Offset: 0x0006EBC4
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000709D0 File Offset: 0x0006EBD0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000709DC File Offset: 0x0006EBDC
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000709E8 File Offset: 0x0006EBE8
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000709F4 File Offset: 0x0006EBF4
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00070A00 File Offset: 0x0006EC00
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00070A24 File Offset: 0x0006EC24
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00070A28 File Offset: 0x0006EC28
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00070A34 File Offset: 0x0006EC34
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00070A40 File Offset: 0x0006EC40
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ushort))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.UInt16."));
			}
			return (int)(this - (ushort)value);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00070A70 File Offset: 0x0006EC70
		public override bool Equals(object obj)
		{
			return obj is ushort && (ushort)obj == this;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00070A8C File Offset: 0x0006EC8C
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00070A90 File Offset: 0x0006EC90
		public int CompareTo(ushort value)
		{
			return (int)(this - value);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00070A98 File Offset: 0x0006EC98
		public bool Equals(ushort obj)
		{
			return obj == this;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00070AA0 File Offset: 0x0006ECA0
		[CLSCompliant(false)]
		public static ushort Parse(string s, IFormatProvider provider)
		{
			return ushort.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00070AAC File Offset: 0x0006ECAC
		[CLSCompliant(false)]
		public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			uint num = uint.Parse(s, style, provider);
			if (num > 65535U)
			{
				throw new OverflowException(Locale.GetText("Value too large."));
			}
			return (ushort)num;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00070AE0 File Offset: 0x0006ECE0
		[CLSCompliant(false)]
		public static bool TryParse(string s, out ushort result)
		{
			return ushort.TryParse(s, NumberStyles.Integer, null, out result);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00070AEC File Offset: 0x0006ECEC
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			result = 0;
			uint num;
			if (!uint.TryParse(s, style, provider, out num))
			{
				return false;
			}
			if (num > 65535U)
			{
				return false;
			}
			result = (ushort)num;
			return true;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00070B20 File Offset: 0x0006ED20
		public override string ToString()
		{
			return NumberFormatter.NumberToString((int)this, null);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00070B2C File Offset: 0x0006ED2C
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString((int)this, provider);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00070B38 File Offset: 0x0006ED38
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00070B44 File Offset: 0x0006ED44
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00070B50 File Offset: 0x0006ED50
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		// Token: 0x04000FA5 RID: 4005
		public const ushort MaxValue = 65535;

		// Token: 0x04000FA6 RID: 4006
		public const ushort MinValue = 0;

		// Token: 0x04000FA7 RID: 4007
		internal ushort m_value;
	}
}
