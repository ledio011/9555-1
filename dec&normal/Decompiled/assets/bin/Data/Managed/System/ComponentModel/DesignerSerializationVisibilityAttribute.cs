using System;

namespace System.ComponentModel
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000028EC File Offset: 0x00000AEC
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility vis)
		{
			this.visibility = vis;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000292C File Offset: 0x00000B2C
		public DesignerSerializationVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002934 File Offset: 0x00000B34
		public override bool Equals(object obj)
		{
			return obj is DesignerSerializationVisibilityAttribute && (obj == this || ((DesignerSerializationVisibilityAttribute)obj).Visibility == this.visibility);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002960 File Offset: 0x00000B60
		public override int GetHashCode()
		{
			return this.visibility.GetHashCode();
		}

		// Token: 0x04000032 RID: 50
		private DesignerSerializationVisibility visibility;

		// Token: 0x04000033 RID: 51
		public static readonly DesignerSerializationVisibilityAttribute Default = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		// Token: 0x04000034 RID: 52
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		// Token: 0x04000035 RID: 53
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		// Token: 0x04000036 RID: 54
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);
	}
}
