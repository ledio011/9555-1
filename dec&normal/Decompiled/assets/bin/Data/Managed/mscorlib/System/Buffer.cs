using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000072 RID: 114
	[ComVisible(true)]
	public static class Buffer
	{
		// Token: 0x06000378 RID: 888 RVA: 0x000121C8 File Offset: 0x000103C8
		public static int ByteLength(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = Buffer.ByteLengthInternal(array);
			if (num < 0)
			{
				throw new ArgumentException(Locale.GetText("Object must be an array of primitives."));
			}
			return num;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012208 File Offset: 0x00010408
		public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (dst == null)
			{
				throw new ArgumentNullException("dst");
			}
			if (srcOffset < 0)
			{
				throw new ArgumentOutOfRangeException("srcOffset", Locale.GetText("Non-negative number required."));
			}
			if (dstOffset < 0)
			{
				throw new ArgumentOutOfRangeException("dstOffset", Locale.GetText("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Locale.GetText("Non-negative number required."));
			}
			if (!Buffer.BlockCopyInternal(src, srcOffset, dst, dstOffset, count) && (srcOffset > Buffer.ByteLength(src) - count || dstOffset > Buffer.ByteLength(dst) - count))
			{
				throw new ArgumentException(Locale.GetText("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
		}

		// Token: 0x0600037A RID: 890
		[MethodImpl(4096)]
		private static extern int ByteLengthInternal(Array array);

		// Token: 0x0600037B RID: 891
		[MethodImpl(4096)]
		internal static extern bool BlockCopyInternal(Array src, int src_offset, Array dest, int dest_offset, int count);
	}
}
