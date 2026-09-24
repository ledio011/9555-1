using System;
using System.Reflection;

namespace System.Collections.Specialized
{
	// Token: 0x02000005 RID: 5
	[DefaultMember("Item")]
	internal class ProcessStringDictionary : StringDictionary, IEnumerable
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022F0 File Offset: 0x000004F0
		public ProcessStringDictionary()
		{
			IHashCodeProvider hcp = null;
			IComparer comparer = null;
			int platform = (int)Environment.OSVersion.Platform;
			if (platform != 4 && platform != 128)
			{
				hcp = CaseInsensitiveHashCodeProvider.DefaultInvariant;
				comparer = CaseInsensitiveComparer.DefaultInvariant;
			}
			this.table = new Hashtable(hcp, comparer);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002340 File Offset: 0x00000540
		public override int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002350 File Offset: 0x00000550
		public override ICollection Keys
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002360 File Offset: 0x00000560
		public override ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002370 File Offset: 0x00000570
		public override void Add(string key, string value)
		{
			this.table.Add(key, value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002380 File Offset: 0x00000580
		public override IEnumerator GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x0400000A RID: 10
		private Hashtable table;
	}
}
