using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000070 RID: 112
	[ComVisible(true)]
	[Serializable]
	public struct Boolean : IComparable<bool>, IEquatable<bool>, IComparable, IConvertible
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00011F68 File Offset: 0x00010168
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011F8C File Offset: 0x0001018C
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011F90 File Offset: 0x00010190
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011F9C File Offset: 0x0001019C
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011FA4 File Offset: 0x000101A4
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011FAC File Offset: 0x000101AC
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011FB8 File Offset: 0x000101B8
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011FC4 File Offset: 0x000101C4
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011FD0 File Offset: 0x000101D0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011FDC File Offset: 0x000101DC
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00011FE8 File Offset: 0x000101E8
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011FF4 File Offset: 0x000101F4
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012000 File Offset: 0x00010200
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001200C File Offset: 0x0001020C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012018 File Offset: 0x00010218
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012024 File Offset: 0x00010224
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException(Locale.GetText("Object is not a Boolean."));
			}
			bool flag = (bool)obj;
			if (this && !flag)
			{
				return 1;
			}
			return (this != flag) ? -1 : 0;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001207C File Offset: 0x0001027C
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is bool))
			{
				return false;
			}
			bool flag = (bool)obj;
			return (!this) ? (!flag) : flag;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000120B4 File Offset: 0x000102B4
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			return this ? 1 : -1;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000120D0 File Offset: 0x000102D0
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000120D8 File Offset: 0x000102D8
		public override int GetHashCode()
		{
			return (!this) ? 0 : 1;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000120E8 File Offset: 0x000102E8
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			value = value.Trim();
			if (string.Compare(value, bool.TrueString, true, CultureInfo.InvariantCulture) == 0)
			{
				return true;
			}
			if (string.Compare(value, bool.FalseString, true, CultureInfo.InvariantCulture) == 0)
			{
				return false;
			}
			throw new FormatException(Locale.GetText("Value is not equivalent to either TrueString or FalseString."));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00012150 File Offset: 0x00010350
		public static bool TryParse(string value, out bool result)
		{
			result = false;
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			if (string.Compare(value, bool.TrueString, true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				return true;
			}
			return string.Compare(value, bool.FalseString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000121A4 File Offset: 0x000103A4
		public override string ToString()
		{
			return (!this) ? bool.FalseString : bool.TrueString;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000121BC File Offset: 0x000103BC
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000121C0 File Offset: 0x000103C0
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x040001B1 RID: 433
		public static readonly string FalseString = "False";

		// Token: 0x040001B2 RID: 434
		public static readonly string TrueString = "True";

		// Token: 0x040001B3 RID: 435
		internal bool m_value;
	}
}
