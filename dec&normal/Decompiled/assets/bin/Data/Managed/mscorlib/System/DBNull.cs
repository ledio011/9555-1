using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000CC RID: 204
	[ComVisible(true)]
	[Serializable]
	public sealed class DBNull : IConvertible, ISerializable
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		private DBNull()
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001F100 File Offset: 0x0001D300
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001F108 File Offset: 0x0001D308
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001F110 File Offset: 0x0001D310
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001F118 File Offset: 0x0001D318
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001F120 File Offset: 0x0001D320
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001F128 File Offset: 0x0001D328
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001F130 File Offset: 0x0001D330
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001F138 File Offset: 0x0001D338
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001F140 File Offset: 0x0001D340
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001F148 File Offset: 0x0001D348
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001F150 File Offset: 0x0001D350
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001F158 File Offset: 0x0001D358
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == typeof(string))
			{
				return string.Empty;
			}
			if (targetType == typeof(DBNull))
			{
				return this;
			}
			throw new InvalidCastException();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001F188 File Offset: 0x0001D388
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001F190 File Offset: 0x0001D390
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001F198 File Offset: 0x0001D398
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetDBNullData(this, info, context);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001F1AC File Offset: 0x0001D3AC
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001F1B0 File Offset: 0x0001D3B0
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		// Token: 0x040002A8 RID: 680
		public static readonly DBNull Value = new DBNull();
	}
}
