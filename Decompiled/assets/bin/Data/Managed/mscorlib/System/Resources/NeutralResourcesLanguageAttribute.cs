using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x020001F9 RID: 505
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(true)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x000451A4 File Offset: 0x000433A4
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("culture is null");
			}
			this.culture = cultureName;
		}

		// Token: 0x0400098B RID: 2443
		private string culture;

		// Token: 0x0400098C RID: 2444
		private UltimateResourceFallbackLocation loc;
	}
}
