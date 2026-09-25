using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000358 RID: 856
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		// Token: 0x06001967 RID: 6503 RVA: 0x0005D89C File Offset: 0x0005BA9C
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy != null)
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0005D8EC File Offset: 0x0005BAEC
		internal CodeGroup(SecurityElement e, PolicyLevel level)
		{
			this.FromXml(e, level);
		}

		// Token: 0x06001969 RID: 6505
		public abstract CodeGroup Copy();

		// Token: 0x0600196A RID: 6506
		public abstract PolicyStatement Resolve(Evidence evidence);

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0005D908 File Offset: 0x0005BB08
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0005D910 File Offset: 0x0005BB10
		public PolicyStatement PolicyStatement
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0005D91C File Offset: 0x0005BB1C
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0005D924 File Offset: 0x0005BB24
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0005D930 File Offset: 0x0005BB30
		public IMembershipCondition MembershipCondition
		{
			get
			{
				return this.m_membershipCondition;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0005D938 File Offset: 0x0005BB38
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x0005D940 File Offset: 0x0005BB40
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0005D94C File Offset: 0x0005BB4C
		public IList Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0005D954 File Offset: 0x0005BB54
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.m_children.Add(group.Copy());
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0005D97C File Offset: 0x0005BB7C
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			return codeGroup != null && this.Equals(codeGroup, false);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0005D9A0 File Offset: 0x0005BBA0
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (cg.Name != this.Name)
			{
				return false;
			}
			if (cg.Description != this.Description)
			{
				return false;
			}
			if (!cg.MembershipCondition.Equals(this.m_membershipCondition))
			{
				return false;
			}
			if (compareChildren)
			{
				int count = cg.Children.Count;
				if (this.Children.Count != count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (!((CodeGroup)this.Children[i]).Equals((CodeGroup)cg.Children[i], false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005DA5C File Offset: 0x0005BC5C
		public override int GetHashCode()
		{
			int num = this.m_membershipCondition.GetHashCode();
			if (this.m_policy != null)
			{
				num += this.m_policy.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0005DA90 File Offset: 0x0005BC90
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			string text = e.Attribute("PermissionSetName");
			PermissionSet permissionSet;
			if (text != null && level != null)
			{
				permissionSet = level.GetNamedPermissionSet(text);
			}
			else
			{
				SecurityElement securityElement = e.SearchForChildByTag("PermissionSet");
				if (securityElement != null)
				{
					Type type = Type.GetType(securityElement.Attribute("class"));
					permissionSet = (PermissionSet)Activator.CreateInstance(type, true);
					permissionSet.FromXml(securityElement);
				}
				else
				{
					permissionSet = new PermissionSet(new PermissionSet(PermissionState.None));
				}
			}
			this.m_policy = new PolicyStatement(permissionSet);
			this.m_children.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2.Tag == "CodeGroup")
					{
						this.AddChild(CodeGroup.CreateFromXml(securityElement2, level));
					}
				}
			}
			this.m_membershipCondition = null;
			SecurityElement securityElement3 = e.SearchForChildByTag("IMembershipCondition");
			if (securityElement3 != null)
			{
				string text2 = securityElement3.Attribute("class");
				Type type2 = Type.GetType(text2);
				if (type2 == null)
				{
					type2 = Type.GetType("System.Security.Policy." + text2);
				}
				this.m_membershipCondition = (IMembershipCondition)Activator.CreateInstance(type2, true);
				this.m_membershipCondition.FromXml(securityElement3, level);
			}
			this.m_name = e.Attribute("Name");
			this.m_description = e.Attribute("Description");
			this.ParseXml(e, level);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005DC64 File Offset: 0x0005BE64
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005DC68 File Offset: 0x0005BE68
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005DC74 File Offset: 0x0005BE74
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Description != null)
			{
				securityElement.AddAttribute("Description", this.Description);
			}
			if (this.MembershipCondition != null)
			{
				securityElement.AddChild(this.MembershipCondition.ToXml());
			}
			if (this.PolicyStatement != null && this.PolicyStatement.PermissionSet != null)
			{
				securityElement.AddChild(this.PolicyStatement.PermissionSet.ToXml());
			}
			foreach (object obj in this.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				securityElement.AddChild(codeGroup.ToXml());
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005DD9C File Offset: 0x0005BF9C
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0005DDA0 File Offset: 0x0005BFA0
		internal static CodeGroup CreateFromXml(SecurityElement se, PolicyLevel level)
		{
			string text = se.Attribute("class");
			string text2 = text;
			int num = text2.IndexOf(",");
			if (num > 0)
			{
				text2 = text2.Substring(0, num);
			}
			num = text2.LastIndexOf(".");
			if (num > 0)
			{
				text2 = text2.Substring(num + 1);
			}
			string text3 = text2;
			switch (text3)
			{
			case "FileCodeGroup":
				return new FileCodeGroup(se, level);
			case "FirstMatchCodeGroup":
				return new FirstMatchCodeGroup(se, level);
			case "NetCodeGroup":
				return new NetCodeGroup(se, level);
			case "UnionCodeGroup":
				return new UnionCodeGroup(se, level);
			}
			Type type = Type.GetType(text);
			CodeGroup codeGroup = (CodeGroup)Activator.CreateInstance(type, true);
			codeGroup.FromXml(se, level);
			return codeGroup;
		}

		// Token: 0x04000DF0 RID: 3568
		private PolicyStatement m_policy;

		// Token: 0x04000DF1 RID: 3569
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04000DF2 RID: 3570
		private string m_description;

		// Token: 0x04000DF3 RID: 3571
		private string m_name;

		// Token: 0x04000DF4 RID: 3572
		private ArrayList m_children = new ArrayList();
	}
}
