using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		// Token: 0x0600034D RID: 845 RVA: 0x00011C10 File Offset: 0x0000FE10
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this.valid_on = validOn;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00011C28 File Offset: 0x0000FE28
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00011C30 File Offset: 0x0000FE30
		public bool AllowMultiple
		{
			get
			{
				return this.allow_multiple;
			}
			set
			{
				this.allow_multiple = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00011C3C File Offset: 0x0000FE3C
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00011C44 File Offset: 0x0000FE44
		public bool Inherited
		{
			get
			{
				return this.inherited;
			}
			set
			{
				this.inherited = value;
			}
		}

		// Token: 0x040001AC RID: 428
		private AttributeTargets valid_on;

		// Token: 0x040001AD RID: 429
		private bool allow_multiple;

		// Token: 0x040001AE RID: 430
		private bool inherited = true;
	}
}
