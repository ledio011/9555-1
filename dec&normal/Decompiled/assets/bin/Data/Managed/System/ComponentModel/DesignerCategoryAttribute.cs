using System;

namespace System.ComponentModel
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002854 File Offset: 0x00000A54
		public DesignerCategoryAttribute(string category)
		{
			this.category = category;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028A4 File Offset: 0x00000AA4
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000028AC File Offset: 0x00000AAC
		public override bool Equals(object obj)
		{
			return obj is DesignerCategoryAttribute && (obj == this || ((DesignerCategoryAttribute)obj).Category == this.category);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028DC File Offset: 0x00000ADC
		public override int GetHashCode()
		{
			return this.category.GetHashCode();
		}

		// Token: 0x04000029 RID: 41
		private string category;

		// Token: 0x0400002A RID: 42
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		// Token: 0x0400002B RID: 43
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		// Token: 0x0400002C RID: 44
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");

		// Token: 0x0400002D RID: 45
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute(string.Empty);
	}
}
