using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200007D RID: 125
	[ComVisible(true)]
	[Serializable]
	public class CaseInsensitiveComparer : IComparer
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0001404C File Offset: 0x0001224C
		public CaseInsensitiveComparer()
		{
			this.culture = CultureInfo.CurrentCulture;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00014060 File Offset: 0x00012260
		private CaseInsensitiveComparer(bool invariant)
		{
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00014080 File Offset: 0x00012280
		public static CaseInsensitiveComparer Default
		{
			get
			{
				return CaseInsensitiveComparer.defaultComparer;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00014088 File Offset: 0x00012288
		public static CaseInsensitiveComparer DefaultInvariant
		{
			get
			{
				return CaseInsensitiveComparer.defaultInvariantComparer;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00014090 File Offset: 0x00012290
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text == null || text2 == null)
			{
				return Comparer.Default.Compare(a, b);
			}
			if (this.culture != null)
			{
				return this.culture.CompareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
			}
			return CultureInfo.InvariantCulture.CompareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
		}

		// Token: 0x040001DA RID: 474
		private static CaseInsensitiveComparer defaultComparer = new CaseInsensitiveComparer();

		// Token: 0x040001DB RID: 475
		private static CaseInsensitiveComparer defaultInvariantComparer = new CaseInsensitiveComparer(true);

		// Token: 0x040001DC RID: 476
		private CultureInfo culture;
	}
}
