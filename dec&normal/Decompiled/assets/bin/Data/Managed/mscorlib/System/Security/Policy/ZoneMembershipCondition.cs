using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200036F RID: 879
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IMembershipCondition
	{
		// Token: 0x06001A04 RID: 6660 RVA: 0x000609A0 File Offset: 0x0005EBA0
		internal ZoneMembershipCondition()
		{
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000609B0 File Offset: 0x0005EBB0
		public ZoneMembershipCondition(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000609C8 File Offset: 0x0005EBC8
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x000609D0 File Offset: 0x0005EBD0
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(Locale.GetText("invalid zone"));
				}
				if (value == SecurityZone.NoZone)
				{
					throw new ArgumentException(Locale.GetText("NoZone isn't valid for membership condition"));
				}
				this.zone = value;
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00060A28 File Offset: 0x0005EC28
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Zone zone = obj as Zone;
				if (zone != null && zone.SecurityZone == this.zone)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00060A7C File Offset: 0x0005EC7C
		public IMembershipCondition Copy()
		{
			return new ZoneMembershipCondition(this.zone);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00060A8C File Offset: 0x0005EC8C
		public override bool Equals(object o)
		{
			ZoneMembershipCondition zoneMembershipCondition = o as ZoneMembershipCondition;
			return zoneMembershipCondition != null && zoneMembershipCondition.SecurityZone == this.zone;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00060AB8 File Offset: 0x0005ECB8
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00060AC4 File Offset: 0x0005ECC4
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Zone");
			if (text != null)
			{
				this.zone = (SecurityZone)((int)Enum.Parse(typeof(SecurityZone), text));
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00060B18 File Offset: 0x0005ED18
		public override int GetHashCode()
		{
			return this.zone.GetHashCode();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00060B2C File Offset: 0x0005ED2C
		public override string ToString()
		{
			return "Zone - " + this.zone;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00060B44 File Offset: 0x0005ED44
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00060B50 File Offset: 0x0005ED50
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(ZoneMembershipCondition), this.version);
			securityElement.AddAttribute("Zone", this.zone.ToString());
			return securityElement;
		}

		// Token: 0x04000E3E RID: 3646
		private readonly int version = 1;

		// Token: 0x04000E3F RID: 3647
		private SecurityZone zone;
	}
}
