using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x020001FA RID: 506
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(true)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x000451C4 File Offset: 0x000433C4
		public SatelliteContractVersionAttribute(string version)
		{
			this.ver = new Version(version);
		}

		// Token: 0x0400098D RID: 2445
		private Version ver;
	}
}
