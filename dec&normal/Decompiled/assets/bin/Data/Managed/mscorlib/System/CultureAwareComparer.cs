using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	internal sealed class CultureAwareComparer : StringComparer
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001B5CC File Offset: 0x000197CC
		public CultureAwareComparer(CultureInfo ci, bool ignore_case)
		{
			this._compareInfo = ci.CompareInfo;
			this._ignoreCase = ignore_case;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001B5E8 File Offset: 0x000197E8
		public override int Compare(string x, string y)
		{
			CompareOptions options = (!this._ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase;
			return this._compareInfo.Compare(x, y, options);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001B618 File Offset: 0x00019818
		public override bool Equals(string x, string y)
		{
			return this.Compare(x, y) == 0;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001B628 File Offset: 0x00019828
		public override int GetHashCode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			CompareOptions options = (!this._ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase;
			return this._compareInfo.GetSortKey(s, options).GetHashCode();
		}

		// Token: 0x04000270 RID: 624
		private readonly bool _ignoreCase;

		// Token: 0x04000271 RID: 625
		private readonly CompareInfo _compareInfo;
	}
}
