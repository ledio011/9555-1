using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000353 RID: 851
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
		public override bool IsSubsetOf(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				return this.zone == SecurityZone.NoZone;
			}
			return this.zone == SecurityZone.NoZone || this.zone == zoneIdentityPermission.zone;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0005D2FC File Offset: 0x0005B4FC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Zone");
			if (text == null)
			{
				this.zone = SecurityZone.NoZone;
			}
			else
			{
				this.zone = (SecurityZone)((int)Enum.Parse(typeof(SecurityZone), text));
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0005D350 File Offset: 0x0005B550
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", this.zone.ToString());
			}
			return securityElement;
		}

		// Token: 0x1700048C RID: 1164
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x0005D390 File Offset: 0x0005B590
		public SecurityZone SecurityZone
		{
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "SecurityZone");
				}
				this.zone = value;
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005D3E0 File Offset: 0x0005B5E0
		private ZoneIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ZoneIdentityPermission));
			}
			return zoneIdentityPermission;
		}

		// Token: 0x04000DDF RID: 3551
		private const int version = 1;

		// Token: 0x04000DE0 RID: 3552
		private SecurityZone zone;
	}
}
