using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000240 RID: 576
	[MonoTODO("Struct should be [StructLayout(LayoutKind.Sequential)] but will need to be reordered for that.")]
	[ComVisible(true)]
	public struct GCHandle
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x0004550C File Offset: 0x0004370C
		private GCHandle(object value, GCHandleType type)
		{
			if (type < GCHandleType.Weak || type > GCHandleType.Pinned)
			{
				type = GCHandleType.Normal;
			}
			this.handle = GCHandle.GetTargetHandle(value, 0, type);
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00045530 File Offset: 0x00043730
		public bool IsAllocated
		{
			get
			{
				return this.handle != 0;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00045540 File Offset: 0x00043740
		public object Target
		{
			get
			{
				if (!this.IsAllocated)
				{
					throw new InvalidOperationException(Locale.GetText("Handle is not allocated"));
				}
				return GCHandle.GetTarget(this.handle);
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00045568 File Offset: 0x00043768
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00045574 File Offset: 0x00043774
		public void Free()
		{
			GCHandle.FreeHandle(this.handle);
			this.handle = 0;
		}

		// Token: 0x06001344 RID: 4932
		[MethodImpl(4096)]
		private static extern object GetTarget(int handle);

		// Token: 0x06001345 RID: 4933
		[MethodImpl(4096)]
		private static extern int GetTargetHandle(object obj, int handle, GCHandleType type);

		// Token: 0x06001346 RID: 4934
		[MethodImpl(4096)]
		private static extern void FreeHandle(int handle);

		// Token: 0x06001347 RID: 4935 RVA: 0x00045588 File Offset: 0x00043788
		public override bool Equals(object o)
		{
			return o != null && o is GCHandle && this.handle == ((GCHandle)o).handle;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000455C0 File Offset: 0x000437C0
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x040009FD RID: 2557
		private int handle;
	}
}
