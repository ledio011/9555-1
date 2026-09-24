using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000208 RID: 520
	public static class RuntimeHelpers
	{
		// Token: 0x06001291 RID: 4753
		[MethodImpl(4096)]
		private static extern void InitializeArray(Array array, IntPtr fldHandle);

		// Token: 0x06001292 RID: 4754 RVA: 0x000452CC File Offset: 0x000434CC
		public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle)
		{
			if (array == null || fldHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			RuntimeHelpers.InitializeArray(array, fldHandle.Value);
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001293 RID: 4755
		public static extern int OffsetToStringData { [MethodImpl(4096)] get; }
	}
}
