using System;

namespace System.ComponentModel
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000245C File Offset: 0x0000065C
		public BrowsableAttribute(bool browsable)
		{
			this.browsable = browsable;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002490 File Offset: 0x00000690
		public bool Browsable
		{
			get
			{
				return this.browsable;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002498 File Offset: 0x00000698
		public override bool Equals(object obj)
		{
			return obj is BrowsableAttribute && (obj == this || ((BrowsableAttribute)obj).Browsable == this.browsable);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024C4 File Offset: 0x000006C4
		public override int GetHashCode()
		{
			return this.browsable.GetHashCode();
		}

		// Token: 0x04000019 RID: 25
		private bool browsable;

		// Token: 0x0400001A RID: 26
		public static readonly BrowsableAttribute Default = new BrowsableAttribute(true);

		// Token: 0x0400001B RID: 27
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		// Token: 0x0400001C RID: 28
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);
	}
}
