using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000301 RID: 769
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface IFormatterConverter
	{
		// Token: 0x060017C5 RID: 6085
		object Convert(object value, Type type);

		// Token: 0x060017C6 RID: 6086
		bool ToBoolean(object value);

		// Token: 0x060017C7 RID: 6087
		short ToInt16(object value);

		// Token: 0x060017C8 RID: 6088
		int ToInt32(object value);

		// Token: 0x060017C9 RID: 6089
		long ToInt64(object value);

		// Token: 0x060017CA RID: 6090
		string ToString(object value);
	}
}
