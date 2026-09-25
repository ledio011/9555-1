using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Mono.Globalization.Unicode;

namespace System.Globalization
{
	// Token: 0x020000F4 RID: 244
	[ComVisible(true)]
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0002484C File Offset: 0x00022A4C
		private CompareInfo()
		{
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00024854 File Offset: 0x00022A54
		internal CompareInfo(CultureInfo ci)
		{
			this.culture = ci.LCID;
			if (CompareInfo.UseManagedCollation)
			{
				object obj = CompareInfo.monitor;
				lock (obj)
				{
					if (CompareInfo.collators == null)
					{
						CompareInfo.collators = new Hashtable();
					}
					this.collator = (SimpleCollator)CompareInfo.collators[ci.LCID];
					if (this.collator == null)
					{
						this.collator = new SimpleCollator(ci);
						CompareInfo.collators[ci.LCID] = this.collator;
					}
				}
			}
			else
			{
				this.icu_name = ci.IcuName;
				this.construct_compareinfo(this.icu_name);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002495C File Offset: 0x00022B5C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (CompareInfo.UseManagedCollation)
			{
				this.collator = new SimpleCollator(new CultureInfo(this.culture));
			}
			else
			{
				try
				{
					this.construct_compareinfo(this.icu_name);
				}
				catch
				{
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000249B8 File Offset: 0x00022BB8
		internal static bool UseManagedCollation
		{
			get
			{
				return CompareInfo.useManagedCollation;
			}
		}

		// Token: 0x06000977 RID: 2423
		[MethodImpl(4096)]
		private extern void construct_compareinfo(string locale);

		// Token: 0x06000978 RID: 2424
		[MethodImpl(4096)]
		private extern void free_internal_collator();

		// Token: 0x06000979 RID: 2425
		[MethodImpl(4096)]
		private extern int internal_compare(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options);

		// Token: 0x0600097A RID: 2426
		[MethodImpl(4096)]
		private extern void assign_sortkey(object key, string source, CompareOptions options);

		// Token: 0x0600097B RID: 2427
		[MethodImpl(4096)]
		private extern int internal_index(string source, int sindex, int count, string value, CompareOptions options, bool first);

		// Token: 0x0600097C RID: 2428 RVA: 0x000249C0 File Offset: 0x00022BC0
		~CompareInfo()
		{
			this.free_internal_collator();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000249F0 File Offset: 0x00022BF0
		private int internal_compare_managed(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return this.collator.Compare(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00024A14 File Offset: 0x00022C14
		private int internal_compare_switch(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return (!CompareInfo.UseManagedCollation) ? this.internal_compare(str1, offset1, length1, str2, offset2, length2, options) : this.internal_compare_managed(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00024A54 File Offset: 0x00022C54
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00024A60 File Offset: 0x00022C60
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (string1 == null)
			{
				if (string2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (string2 == null)
				{
					return 1;
				}
				if (string1.Length == 0 && string2.Length == 0)
				{
					return 0;
				}
				return this.internal_compare_switch(string1, 0, string1.Length, string2, 0, string2.Length, options);
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00024ACC File Offset: 0x00022CCC
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (string1 == null)
			{
				if (string2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (string2 == null)
				{
					return 1;
				}
				if ((string1.Length == 0 || offset1 == string1.Length || length1 == 0) && (string2.Length == 0 || offset2 == string2.Length || length2 == 0))
				{
					return 0;
				}
				if (offset1 < 0 || length1 < 0 || offset2 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException("Offsets and lengths must not be less than zero");
				}
				if (offset1 > string1.Length)
				{
					throw new ArgumentOutOfRangeException("Offset1 is greater than or equal to the length of string1");
				}
				if (offset2 > string2.Length)
				{
					throw new ArgumentOutOfRangeException("Offset2 is greater than or equal to the length of string2");
				}
				if (length1 > string1.Length - offset1)
				{
					throw new ArgumentOutOfRangeException("Length1 is greater than the number of characters from offset1 to the end of string1");
				}
				if (length2 > string2.Length - offset2)
				{
					throw new ArgumentOutOfRangeException("Length2 is greater than the number of characters from offset2 to the end of string2");
				}
				return this.internal_compare_switch(string1, offset1, length1, string2, offset2, length2, options);
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00024BEC File Offset: 0x00022DEC
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && compareInfo.culture == this.culture;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00024C18 File Offset: 0x00022E18
		public override int GetHashCode()
		{
			return this.LCID;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00024C20 File Offset: 0x00022E20
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase || options == CompareOptions.Ordinal)
			{
				throw new ArgumentException("Now allowed CompareOptions.", "options");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.collator.GetSortKey(source, options);
			}
			SortKey sortKey = new SortKey(this.culture, source, options);
			this.assign_sortkey(sortKey, source, options);
			return sortKey;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00024C8C File Offset: 0x00022E8C
		public virtual int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00024C9C File Offset: 0x00022E9C
		private int internal_index_managed(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			return (!first) ? this.collator.LastIndexOf(s1, s2, sindex, count, opt) : this.collator.IndexOf(s1, s2, sindex, count, opt);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00024CD0 File Offset: 0x00022ED0
		private int internal_index_switch(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			return (!CompareInfo.UseManagedCollation || (first && opt == CompareOptions.Ordinal)) ? this.internal_index(s1, sindex, count, s2, opt, first) : this.internal_index_managed(s1, sindex, count, s2, opt, first);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00024D20 File Offset: 0x00022F20
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || source.Length - startIndex < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (value.Length == 0)
			{
				return startIndex;
			}
			if (count == 0)
			{
				return -1;
			}
			return this.internal_index_switch(source, startIndex, count, value, options, true);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00024DC4 File Offset: 0x00022FC4
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.collator.IsPrefix(source, prefix, options);
			}
			return source.Length >= prefix.Length && this.Compare(source, 0, prefix.Length, prefix, 0, prefix.Length, options) == 0;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00024E3C File Offset: 0x0002303C
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (suffix == null)
			{
				throw new ArgumentNullException("suffix");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.collator.IsSuffix(source, suffix, options);
			}
			return source.Length >= suffix.Length && this.Compare(source, source.Length - suffix.Length, suffix.Length, suffix, 0, suffix.Length, options) == 0;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00024EC0 File Offset: 0x000230C0
		public virtual int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00024ED0 File Offset: 0x000230D0
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex - count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (count == 0)
			{
				return -1;
			}
			if (value.Length == 0)
			{
				return 0;
			}
			return this.internal_index_switch(source, startIndex, count, value, options, false);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00024F70 File Offset: 0x00023170
		public override string ToString()
		{
			return "CompareInfo - " + this.culture;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00024F88 File Offset: 0x00023188
		public int LCID
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x04000337 RID: 823
		private const CompareOptions ValidCompareOptions_NoStringSort = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase;

		// Token: 0x04000338 RID: 824
		private const CompareOptions ValidCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase;

		// Token: 0x04000339 RID: 825
		private static readonly bool useManagedCollation = Environment.internalGetEnvironmentVariable("MONO_DISABLE_MANAGED_COLLATION") != "yes" && MSCompatUnicodeTable.IsReady;

		// Token: 0x0400033A RID: 826
		private int culture;

		// Token: 0x0400033B RID: 827
		[NonSerialized]
		private string icu_name;

		// Token: 0x0400033C RID: 828
		private int win32LCID;

		// Token: 0x0400033D RID: 829
		private string m_name;

		// Token: 0x0400033E RID: 830
		[NonSerialized]
		private SimpleCollator collator;

		// Token: 0x0400033F RID: 831
		private static Hashtable collators;

		// Token: 0x04000340 RID: 832
		[NonSerialized]
		private static object monitor = new object();
	}
}
