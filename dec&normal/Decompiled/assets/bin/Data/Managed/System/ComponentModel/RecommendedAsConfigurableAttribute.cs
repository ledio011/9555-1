using System;

namespace System.ComponentModel
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use SettingsBindableAttribute instead of RecommendedAsConfigurableAttribute")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002B50 File Offset: 0x00000D50
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.recommendedAsConfigurable = recommendedAsConfigurable;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002B84 File Offset: 0x00000D84
		public bool RecommendedAsConfigurable
		{
			get
			{
				return this.recommendedAsConfigurable;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002B8C File Offset: 0x00000D8C
		public override bool Equals(object obj)
		{
			return obj is RecommendedAsConfigurableAttribute && ((RecommendedAsConfigurableAttribute)obj).RecommendedAsConfigurable == this.recommendedAsConfigurable;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public override int GetHashCode()
		{
			return this.recommendedAsConfigurable.GetHashCode();
		}

		// Token: 0x0400004C RID: 76
		private bool recommendedAsConfigurable;

		// Token: 0x0400004D RID: 77
		public static readonly RecommendedAsConfigurableAttribute Default = new RecommendedAsConfigurableAttribute(false);

		// Token: 0x0400004E RID: 78
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		// Token: 0x0400004F RID: 79
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);
	}
}
