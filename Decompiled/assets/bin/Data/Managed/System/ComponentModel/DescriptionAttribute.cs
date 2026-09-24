using System;

namespace System.ComponentModel
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000272C File Offset: 0x0000092C
		public DescriptionAttribute()
		{
			this.desc = string.Empty;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002740 File Offset: 0x00000940
		public DescriptionAttribute(string name)
		{
			this.desc = name;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000275C File Offset: 0x0000095C
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002764 File Offset: 0x00000964
		protected string DescriptionValue
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000276C File Offset: 0x0000096C
		public override bool Equals(object obj)
		{
			return obj is DescriptionAttribute && (obj == this || ((DescriptionAttribute)obj).Description == this.desc);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000279C File Offset: 0x0000099C
		public override int GetHashCode()
		{
			return this.desc.GetHashCode();
		}

		// Token: 0x04000025 RID: 37
		private string desc;

		// Token: 0x04000026 RID: 38
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
