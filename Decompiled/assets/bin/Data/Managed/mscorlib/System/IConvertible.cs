using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200010C RID: 268
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface IConvertible
	{
		// Token: 0x06000A83 RID: 2691
		TypeCode GetTypeCode();

		// Token: 0x06000A84 RID: 2692
		bool ToBoolean(IFormatProvider provider);

		// Token: 0x06000A85 RID: 2693
		byte ToByte(IFormatProvider provider);

		// Token: 0x06000A86 RID: 2694
		char ToChar(IFormatProvider provider);

		// Token: 0x06000A87 RID: 2695
		DateTime ToDateTime(IFormatProvider provider);

		// Token: 0x06000A88 RID: 2696
		decimal ToDecimal(IFormatProvider provider);

		// Token: 0x06000A89 RID: 2697
		double ToDouble(IFormatProvider provider);

		// Token: 0x06000A8A RID: 2698
		short ToInt16(IFormatProvider provider);

		// Token: 0x06000A8B RID: 2699
		int ToInt32(IFormatProvider provider);

		// Token: 0x06000A8C RID: 2700
		long ToInt64(IFormatProvider provider);

		// Token: 0x06000A8D RID: 2701
		sbyte ToSByte(IFormatProvider provider);

		// Token: 0x06000A8E RID: 2702
		float ToSingle(IFormatProvider provider);

		// Token: 0x06000A8F RID: 2703
		string ToString(IFormatProvider provider);

		// Token: 0x06000A90 RID: 2704
		object ToType(Type conversionType, IFormatProvider provider);

		// Token: 0x06000A91 RID: 2705
		ushort ToUInt16(IFormatProvider provider);

		// Token: 0x06000A92 RID: 2706
		uint ToUInt32(IFormatProvider provider);

		// Token: 0x06000A93 RID: 2707
		ulong ToUInt64(IFormatProvider provider);
	}
}
