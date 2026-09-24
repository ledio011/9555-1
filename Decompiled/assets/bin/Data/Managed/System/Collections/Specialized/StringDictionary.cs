using System;
using System.Globalization;
using System.Reflection;

namespace System.Collections.Specialized
{
	// Token: 0x02000006 RID: 6
	[DefaultMember("Item")]
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002390 File Offset: 0x00000590
		public StringDictionary()
		{
			this.contents = new Hashtable();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023A4 File Offset: 0x000005A4
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023B4 File Offset: 0x000005B4
		public virtual ICollection Keys
		{
			get
			{
				return this.contents.Keys;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023C4 File Offset: 0x000005C4
		public virtual ICollection Values
		{
			get
			{
				return this.contents.Values;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023D4 File Offset: 0x000005D4
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLower(CultureInfo.InvariantCulture), value);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002400 File Offset: 0x00000600
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		// Token: 0x0400000B RID: 11
		private Hashtable contents;
	}
}
