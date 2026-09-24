using System;

namespace System.Resources
{
	// Token: 0x020001F8 RID: 504
	internal class NameOrId
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0004514C File Offset: 0x0004334C
		public bool IsName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004515C File Offset: 0x0004335C
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00045164 File Offset: 0x00043364
		public override string ToString()
		{
			if (this.name != null)
			{
				return "Name(" + this.name + ")";
			}
			return "Id(" + this.id + ")";
		}

		// Token: 0x04000989 RID: 2441
		private string name;

		// Token: 0x0400098A RID: 2442
		private int id;
	}
}
