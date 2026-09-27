using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200038C RID: 908
	[ComVisible(true)]
	[Serializable]
	public abstract class StringComparer : IComparer<string>, IEqualityComparer<string>, IComparer, IEqualityComparer
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00066C14 File Offset: 0x00064E14
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return StringComparer.ordinalIgnoreCase;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00066C1C File Offset: 0x00064E1C
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable == null)
			{
				throw new ArgumentException();
			}
			return comparable.CompareTo(y);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00066C80 File Offset: 0x00064E80
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00066CD0 File Offset: 0x00064ED0
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			return (text != null) ? this.GetHashCode(text) : obj.GetHashCode();
		}

		// Token: 0x06001B58 RID: 7000
		public abstract int Compare(string x, string y);

		// Token: 0x06001B59 RID: 7001
		public abstract bool Equals(string x, string y);

		// Token: 0x06001B5A RID: 7002
		public abstract int GetHashCode(string obj);

		// Token: 0x04000EA5 RID: 3749
		private static StringComparer invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, true);

		// Token: 0x04000EA6 RID: 3750
		private static StringComparer invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, false);

		// Token: 0x04000EA7 RID: 3751
		private static StringComparer ordinalIgnoreCase = new OrdinalComparer(true);

		// Token: 0x04000EA8 RID: 3752
		private static StringComparer ordinal = new OrdinalComparer(false);
	}
}
