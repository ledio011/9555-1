using System;

namespace System
{
	// Token: 0x0200015F RID: 351
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x00032DF8 File Offset: 0x00030FF8
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00032E00 File Offset: 0x00031000
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x0400057A RID: 1402
		private string comment;
	}
}
