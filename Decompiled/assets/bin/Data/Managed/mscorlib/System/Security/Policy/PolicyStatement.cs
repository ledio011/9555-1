using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000369 RID: 873
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x060019D7 RID: 6615 RVA: 0x000600D4 File Offset: 0x0005E2D4
		public PolicyStatement(PermissionSet permSet) : this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000600E0 File Offset: 0x0005E2E0
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet != null)
			{
				this.perms = permSet.Copy();
				this.perms.SetReadOnly(true);
			}
			this.attrs = attributes;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00060110 File Offset: 0x0005E310
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0006013C File Offset: 0x0005E33C
		public PermissionSet PermissionSet
		{
			get
			{
				if (this.perms == null)
				{
					this.perms = new PermissionSet(PermissionState.None);
					this.perms.SetReadOnly(true);
				}
				return this.perms;
			}
			set
			{
				this.perms = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00060148 File Offset: 0x0005E348
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00060150 File Offset: 0x0005E350
		public PolicyStatement Copy()
		{
			return new PolicyStatement(this.perms, this.attrs);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00060164 File Offset: 0x0005E364
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00060170 File Offset: 0x0005E370
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PolicyStatement")
			{
				throw new ArgumentException(Locale.GetText("Invalid tag."));
			}
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.attrs = (PolicyStatementAttribute)((int)Enum.Parse(typeof(PolicyStatementAttribute), text));
			}
			SecurityElement et2 = et.SearchForChildByTag("PermissionSet");
			this.PermissionSet.FromXml(et2);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000601F8 File Offset: 0x0005E3F8
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00060204 File Offset: 0x0005E404
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.attrs != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", this.attrs.ToString());
			}
			securityElement.AddChild(this.PermissionSet.ToXml());
			return securityElement;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00060264 File Offset: 0x0005E464
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.PermissionSet.Equals(obj) && this.attrs == policyStatement.attrs;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000602AC File Offset: 0x0005E4AC
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.PermissionSet.GetHashCode() ^ (int)this.attrs;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000602C0 File Offset: 0x0005E4C0
		internal static PolicyStatement Empty()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04000E2F RID: 3631
		private PermissionSet perms;

		// Token: 0x04000E30 RID: 3632
		private PolicyStatementAttribute attrs;
	}
}
