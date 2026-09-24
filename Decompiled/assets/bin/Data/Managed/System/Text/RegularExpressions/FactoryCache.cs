using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000063 RID: 99
	internal class FactoryCache
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00008310 File Offset: 0x00006510
		public FactoryCache(int capacity)
		{
			this.capacity = capacity;
			this.factories = new Hashtable(capacity);
			this.mru_list = new MRUList();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008338 File Offset: 0x00006538
		public void Add(string pattern, RegexOptions options, IMachineFactory factory)
		{
			lock (this)
			{
				FactoryCache.Key key = new FactoryCache.Key(pattern, options);
				this.Cleanup();
				this.factories[key] = factory;
				this.mru_list.Use(key);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008390 File Offset: 0x00006590
		private void Cleanup()
		{
			while (this.factories.Count >= this.capacity && this.capacity > 0)
			{
				object obj = this.mru_list.Evict();
				if (obj != null)
				{
					this.factories.Remove((FactoryCache.Key)obj);
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000083E8 File Offset: 0x000065E8
		public IMachineFactory Lookup(string pattern, RegexOptions options)
		{
			lock (this)
			{
				FactoryCache.Key key = new FactoryCache.Key(pattern, options);
				if (this.factories.Contains(key))
				{
					this.mru_list.Use(key);
					return (IMachineFactory)this.factories[key];
				}
			}
			return null;
		}

		// Token: 0x0400097D RID: 2429
		private int capacity;

		// Token: 0x0400097E RID: 2430
		private Hashtable factories;

		// Token: 0x0400097F RID: 2431
		private MRUList mru_list;

		// Token: 0x02000064 RID: 100
		private class Key
		{
			// Token: 0x060001A5 RID: 421 RVA: 0x00008458 File Offset: 0x00006658
			public Key(string pattern, RegexOptions options)
			{
				this.pattern = pattern;
				this.options = options;
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x00008470 File Offset: 0x00006670
			public override int GetHashCode()
			{
				return this.pattern.GetHashCode() ^ (int)this.options;
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x00008484 File Offset: 0x00006684
			public override bool Equals(object o)
			{
				if (o == null || !(o is FactoryCache.Key))
				{
					return false;
				}
				FactoryCache.Key key = (FactoryCache.Key)o;
				return this.options == key.options && this.pattern.Equals(key.pattern);
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x000084D0 File Offset: 0x000066D0
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"('",
					this.pattern,
					"', [",
					this.options,
					"])"
				});
			}

			// Token: 0x04000980 RID: 2432
			public string pattern;

			// Token: 0x04000981 RID: 2433
			public RegexOptions options;
		}
	}
}
