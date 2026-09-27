using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000389 RID: 905
	[ComVisible(true)]
	[Serializable]
	public struct Single : IComparable<float>, IEquatable<float>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06001A6F RID: 6767 RVA: 0x000622E8 File Offset: 0x000604E8
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000622F4 File Offset: 0x000604F4
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00062300 File Offset: 0x00060500
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0006230C File Offset: 0x0006050C
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00062318 File Offset: 0x00060518
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00062324 File Offset: 0x00060524
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00062330 File Offset: 0x00060530
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0006233C File Offset: 0x0006053C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00062348 File Offset: 0x00060548
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00062354 File Offset: 0x00060554
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00062360 File Offset: 0x00060560
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0006236C File Offset: 0x0006056C
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00062390 File Offset: 0x00060590
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0006239C File Offset: 0x0006059C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000623A8 File Offset: 0x000605A8
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000623B4 File Offset: 0x000605B4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Single."));
			}
			float num = (float)value;
			if (float.IsPositiveInfinity(this) && float.IsPositiveInfinity(num))
			{
				return 0;
			}
			if (float.IsNegativeInfinity(this) && float.IsNegativeInfinity(num))
			{
				return 0;
			}
			if (float.IsNaN(num))
			{
				if (float.IsNaN(this))
				{
					return 0;
				}
				return 1;
			}
			else if (float.IsNaN(this))
			{
				if (float.IsNaN(num))
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this == num)
				{
					return 0;
				}
				if (this > num)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00062468 File Offset: 0x00060668
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			if (float.IsNaN(num))
			{
				return float.IsNaN(this);
			}
			return num == this;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000624A4 File Offset: 0x000606A4
		public int CompareTo(float value)
		{
			if (float.IsPositiveInfinity(this) && float.IsPositiveInfinity(value))
			{
				return 0;
			}
			if (float.IsNegativeInfinity(this) && float.IsNegativeInfinity(value))
			{
				return 0;
			}
			if (float.IsNaN(value))
			{
				if (float.IsNaN(this))
				{
					return 0;
				}
				return 1;
			}
			else if (float.IsNaN(this))
			{
				if (float.IsNaN(value))
				{
					return 0;
				}
				return -1;
			}
			else
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
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00062530 File Offset: 0x00060730
		public bool Equals(float obj)
		{
			if (float.IsNaN(obj))
			{
				return float.IsNaN(this);
			}
			return obj == this;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0006254C File Offset: 0x0006074C
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00062560 File Offset: 0x00060760
		public static bool IsInfinity(float f)
		{
			return f == float.PositiveInfinity || f == float.NegativeInfinity;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00062578 File Offset: 0x00060778
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool IsNaN(float f)
		{
			return f != f;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00062584 File Offset: 0x00060784
		public static bool IsNegativeInfinity(float f)
		{
			return f < 0f && (f == float.NegativeInfinity || f == float.PositiveInfinity);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000625AC File Offset: 0x000607AC
		public static bool IsPositiveInfinity(float f)
		{
			return f > 0f && (f == float.NegativeInfinity || f == float.PositiveInfinity);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000625D4 File Offset: 0x000607D4
		public static float Parse(string s)
		{
			double num = double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, null);
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				throw new OverflowException();
			}
			return (float)num;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0006261C File Offset: 0x0006081C
		public static float Parse(string s, IFormatProvider provider)
		{
			double num = double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				throw new OverflowException();
			}
			return (float)num;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00062664 File Offset: 0x00060864
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			double num;
			Exception ex;
			if (!double.Parse(s, style, provider, true, out num, out ex))
			{
				result = 0f;
				return false;
			}
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				result = 0f;
				return false;
			}
			result = (float)num;
			return true;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000626C0 File Offset: 0x000608C0
		public static bool TryParse(string s, out float result)
		{
			return float.TryParse(s, NumberStyles.Any, null, out result);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000626D0 File Offset: 0x000608D0
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000626DC File Offset: 0x000608DC
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000626E8 File Offset: 0x000608E8
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000626F4 File Offset: 0x000608F4
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00062700 File Offset: 0x00060900
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		// Token: 0x04000E99 RID: 3737
		public const float Epsilon = 1E-45f;

		// Token: 0x04000E9A RID: 3738
		public const float MaxValue = 3.4028235E+38f;

		// Token: 0x04000E9B RID: 3739
		public const float MinValue = -3.4028235E+38f;

		// Token: 0x04000E9C RID: 3740
		public const float NaN = float.NaN;

		// Token: 0x04000E9D RID: 3741
		public const float PositiveInfinity = float.PositiveInfinity;

		// Token: 0x04000E9E RID: 3742
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x04000E9F RID: 3743
		private const double MaxValueEpsilon = 3.6147112457961776E+29;

		// Token: 0x04000EA0 RID: 3744
		internal float m_value;
	}
}
