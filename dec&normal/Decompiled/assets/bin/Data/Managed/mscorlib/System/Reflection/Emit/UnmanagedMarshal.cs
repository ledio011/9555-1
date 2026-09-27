using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001C2 RID: 450
	[ComVisible(true)]
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
	[Serializable]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x0600110B RID: 4363 RVA: 0x00041FE8 File Offset: 0x000401E8
		internal MarshalAsAttribute ToMarshalAsAttribute()
		{
			MarshalAsAttribute marshalAsAttribute = new MarshalAsAttribute(this.t);
			marshalAsAttribute.ArraySubType = this.tbase;
			marshalAsAttribute.MarshalCookie = this.mcookie;
			marshalAsAttribute.MarshalType = this.marshaltype;
			marshalAsAttribute.MarshalTypeRef = this.marshaltyperef;
			if (this.count == -1)
			{
				marshalAsAttribute.SizeConst = 0;
			}
			else
			{
				marshalAsAttribute.SizeConst = this.count;
			}
			if (this.param_num == -1)
			{
				marshalAsAttribute.SizeParamIndex = 0;
			}
			else
			{
				marshalAsAttribute.SizeParamIndex = (short)this.param_num;
			}
			return marshalAsAttribute;
		}

		// Token: 0x04000886 RID: 2182
		private int count;

		// Token: 0x04000887 RID: 2183
		private UnmanagedType t;

		// Token: 0x04000888 RID: 2184
		private UnmanagedType tbase;

		// Token: 0x04000889 RID: 2185
		private string guid;

		// Token: 0x0400088A RID: 2186
		private string mcookie;

		// Token: 0x0400088B RID: 2187
		private string marshaltype;

		// Token: 0x0400088C RID: 2188
		private Type marshaltyperef;

		// Token: 0x0400088D RID: 2189
		private int param_num;

		// Token: 0x0400088E RID: 2190
		private bool has_size;
	}
}
