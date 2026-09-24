using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003CF RID: 975
	[ComVisible(true)]
	[CLSCompliant(false)]
	public struct TypedReference
	{
		// Token: 0x06001E08 RID: 7688 RVA: 0x0007071C File Offset: 0x0006E91C
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Locale.GetText("This operation is not supported for this type."));
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00070730 File Offset: 0x0006E930
		public override int GetHashCode()
		{
			if (this.type.Value == IntPtr.Zero)
			{
				return 0;
			}
			return Type.GetTypeFromHandle(this.type).GetHashCode();
		}

		// Token: 0x06001E0A RID: 7690
		[MethodImpl(4096)]
		public static extern object ToObject(TypedReference value);

		// Token: 0x04000F9E RID: 3998
		private RuntimeTypeHandle type;

		// Token: 0x04000F9F RID: 3999
		private IntPtr value;

		// Token: 0x04000FA0 RID: 4000
		private IntPtr klass;
	}
}
