using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000128 RID: 296
	[StructLayout(0)]
	public class TrackedReference
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x000198E4 File Offset: 0x00017AE4
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000198F4 File Offset: 0x00017AF4
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00019904 File Offset: 0x00017B04
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			if (y == null && x == null)
			{
				return true;
			}
			if (y == null)
			{
				return x.m_Ptr == IntPtr.Zero;
			}
			if (x == null)
			{
				return y.m_Ptr == IntPtr.Zero;
			}
			return x.m_Ptr == y.m_Ptr;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00019964 File Offset: 0x00017B64
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00019970 File Offset: 0x00017B70
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x040004CE RID: 1230
		[NotRenamed]
		internal IntPtr m_Ptr;
	}
}
