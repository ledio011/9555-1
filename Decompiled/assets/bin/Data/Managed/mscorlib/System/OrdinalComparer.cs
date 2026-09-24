using System;

namespace System
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	internal sealed class OrdinalComparer : StringComparer
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x00037E78 File Offset: 0x00036078
		public OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00037E88 File Offset: 0x00036088
		public override int Compare(string x, string y)
		{
			if (this._ignoreCase)
			{
				return string.CompareOrdinalCaseInsensitiveUnchecked(x, 0, int.MaxValue, y, 0, int.MaxValue);
			}
			return string.CompareOrdinalUnchecked(x, 0, int.MaxValue, y, 0, int.MaxValue);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00037EBC File Offset: 0x000360BC
		public override bool Equals(string x, string y)
		{
			if (this._ignoreCase)
			{
				return this.Compare(x, y) == 0;
			}
			return x == y;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00037EDC File Offset: 0x000360DC
		public override int GetHashCode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (this._ignoreCase)
			{
				return s.GetCaseInsensitiveHashCode();
			}
			return s.GetHashCode();
		}

		// Token: 0x040005CC RID: 1484
		private readonly bool _ignoreCase;
	}
}
