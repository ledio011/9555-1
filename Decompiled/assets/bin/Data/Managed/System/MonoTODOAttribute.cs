using System;

namespace System
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00003EFC File Offset: 0x000020FC
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F04 File Offset: 0x00002104
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x040007C9 RID: 1993
		private string comment;
	}
}
