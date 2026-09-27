using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200036E RID: 878
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : IBuiltInEvidence, IIdentityPermissionFactory
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00060904 File Offset: 0x0005EB04
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0006090C File Offset: 0x0005EB0C
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0006091C File Offset: 0x0005EB1C
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && zone.zone == this.zone;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00060948 File Offset: 0x0005EB48
		public override int GetHashCode()
		{
			return (int)this.zone;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00060950 File Offset: 0x0005EB50
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Zone", this.zone.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x04000E3D RID: 3645
		private SecurityZone zone;
	}
}
