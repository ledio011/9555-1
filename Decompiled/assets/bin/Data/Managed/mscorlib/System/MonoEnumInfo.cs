using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000157 RID: 343
	internal struct MonoEnumInfo
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x00032A58 File Offset: 0x00030C58
		private MonoEnumInfo(MonoEnumInfo other)
		{
			this.utype = other.utype;
			this.values = other.values;
			this.names = other.names;
			this.name_hash = other.name_hash;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00032A90 File Offset: 0x00030C90
		static MonoEnumInfo()
		{
			MonoEnumInfo.global_cache_monitor = new object();
			MonoEnumInfo.global_cache = new Hashtable();
		}

		// Token: 0x06000D15 RID: 3349
		[MethodImpl(4096)]
		private static extern void get_enum_info(Type enumType, out MonoEnumInfo info);

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00032AD0 File Offset: 0x00030CD0
		private static Hashtable Cache
		{
			get
			{
				if (MonoEnumInfo.cache == null)
				{
					MonoEnumInfo.cache = new Hashtable();
				}
				return MonoEnumInfo.cache;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00032AEC File Offset: 0x00030CEC
		internal static void GetInfo(Type enumType, out MonoEnumInfo info)
		{
			if (MonoEnumInfo.Cache.ContainsKey(enumType))
			{
				info = (MonoEnumInfo)MonoEnumInfo.cache[enumType];
				return;
			}
			object obj = MonoEnumInfo.global_cache_monitor;
			lock (obj)
			{
				if (MonoEnumInfo.global_cache.ContainsKey(enumType))
				{
					object obj2 = MonoEnumInfo.global_cache[enumType];
					MonoEnumInfo.cache[enumType] = obj2;
					info = (MonoEnumInfo)obj2;
					return;
				}
			}
			MonoEnumInfo.get_enum_info(enumType, out info);
			IComparer comparer = null;
			if (!(info.values is byte[]) && !(info.values is ushort[]) && !(info.values is uint[]) && !(info.values is ulong[]))
			{
				if (info.values is int[])
				{
					comparer = MonoEnumInfo.int_comparer;
				}
				else if (info.values is short[])
				{
					comparer = MonoEnumInfo.short_comparer;
				}
				else if (info.values is sbyte[])
				{
					comparer = MonoEnumInfo.sbyte_comparer;
				}
				else if (info.values is long[])
				{
					comparer = MonoEnumInfo.long_comparer;
				}
			}
			Array.Sort(info.values, info.names, comparer);
			if (info.names.Length > 50)
			{
				info.name_hash = new Hashtable(info.names.Length);
				for (int i = 0; i < info.names.Length; i++)
				{
					info.name_hash[info.names[i]] = i;
				}
			}
			MonoEnumInfo monoEnumInfo = new MonoEnumInfo(info);
			object obj3 = MonoEnumInfo.global_cache_monitor;
			lock (obj3)
			{
				MonoEnumInfo.global_cache[enumType] = monoEnumInfo;
			}
		}

		// Token: 0x0400056D RID: 1389
		internal Type utype;

		// Token: 0x0400056E RID: 1390
		internal Array values;

		// Token: 0x0400056F RID: 1391
		internal string[] names;

		// Token: 0x04000570 RID: 1392
		internal Hashtable name_hash;

		// Token: 0x04000571 RID: 1393
		[ThreadStatic]
		private static Hashtable cache;

		// Token: 0x04000572 RID: 1394
		private static Hashtable global_cache;

		// Token: 0x04000573 RID: 1395
		private static object global_cache_monitor;

		// Token: 0x04000574 RID: 1396
		internal static MonoEnumInfo.SByteComparer sbyte_comparer = new MonoEnumInfo.SByteComparer();

		// Token: 0x04000575 RID: 1397
		internal static MonoEnumInfo.ShortComparer short_comparer = new MonoEnumInfo.ShortComparer();

		// Token: 0x04000576 RID: 1398
		internal static MonoEnumInfo.IntComparer int_comparer = new MonoEnumInfo.IntComparer();

		// Token: 0x04000577 RID: 1399
		internal static MonoEnumInfo.LongComparer long_comparer = new MonoEnumInfo.LongComparer();

		// Token: 0x02000158 RID: 344
		internal class IntComparer : IComparer<int>, IComparer
		{
			// Token: 0x06000D19 RID: 3353 RVA: 0x00032CE8 File Offset: 0x00030EE8
			public int Compare(object x, object y)
			{
				int num = (int)x;
				int num2 = (int)y;
				if (num == num2)
				{
					return 0;
				}
				if (num < num2)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x06000D1A RID: 3354 RVA: 0x00032D18 File Offset: 0x00030F18
			public int Compare(int ix, int iy)
			{
				if (ix == iy)
				{
					return 0;
				}
				if (ix < iy)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x02000159 RID: 345
		internal class LongComparer : IComparer<long>, IComparer
		{
			// Token: 0x06000D1C RID: 3356 RVA: 0x00032D38 File Offset: 0x00030F38
			public int Compare(object x, object y)
			{
				long num = (long)x;
				long num2 = (long)y;
				if (num == num2)
				{
					return 0;
				}
				if (num < num2)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x00032D68 File Offset: 0x00030F68
			public int Compare(long ix, long iy)
			{
				if (ix == iy)
				{
					return 0;
				}
				if (ix < iy)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x0200015A RID: 346
		internal class SByteComparer : IComparer<sbyte>, IComparer
		{
			// Token: 0x06000D1F RID: 3359 RVA: 0x00032D88 File Offset: 0x00030F88
			public int Compare(object x, object y)
			{
				sbyte b = (sbyte)x;
				sbyte b2 = (sbyte)y;
				return (int)((byte)b - (byte)b2);
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x00032DA8 File Offset: 0x00030FA8
			public int Compare(sbyte ix, sbyte iy)
			{
				return (int)((byte)ix - (byte)iy);
			}
		}

		// Token: 0x0200015B RID: 347
		internal class ShortComparer : IComparer<short>, IComparer
		{
			// Token: 0x06000D22 RID: 3362 RVA: 0x00032DB8 File Offset: 0x00030FB8
			public int Compare(object x, object y)
			{
				short num = (short)x;
				short num2 = (short)y;
				return (int)((ushort)num - (ushort)num2);
			}

			// Token: 0x06000D23 RID: 3363 RVA: 0x00032DD8 File Offset: 0x00030FD8
			public int Compare(short ix, short iy)
			{
				return (int)((ushort)ix - (ushort)iy);
			}
		}
	}
}
