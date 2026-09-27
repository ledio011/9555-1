using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200035D RID: 861
	[ComVisible(true)]
	[Serializable]
	public sealed class FileCodeGroup : CodeGroup
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x0005E914 File Offset: 0x0005CB14
		public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access) : base(membershipCondition, null)
		{
			this.m_access = access;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005E928 File Offset: 0x0005CB28
		internal FileCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005E934 File Offset: 0x0005CB34
		public override CodeGroup Copy()
		{
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			fileCodeGroup.Name = base.Name;
			fileCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				fileCodeGroup.AddChild(codeGroup.Copy());
			}
			return fileCodeGroup;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005E9C8 File Offset: 0x0005CBC8
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					PolicyStatement policyStatement = codeGroup.Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2;
			if (base.PolicyStatement != null)
			{
				policyStatement2 = base.PolicyStatement.Copy();
			}
			else
			{
				policyStatement2 = PolicyStatement.Empty();
			}
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0005EAD4 File Offset: 0x0005CCD4
		public override bool Equals(object o)
		{
			return o is FileCodeGroup && this.m_access == ((FileCodeGroup)o).m_access && base.Equals((CodeGroup)o, false);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005EB08 File Offset: 0x0005CD08
		public override int GetHashCode()
		{
			return this.m_access.GetHashCode();
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0005EB1C File Offset: 0x0005CD1C
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			string text = e.Attribute("Access");
			if (text != null)
			{
				this.m_access = (FileIOPermissionAccess)((int)Enum.Parse(typeof(FileIOPermissionAccess), text, true));
			}
			else
			{
				this.m_access = FileIOPermissionAccess.NoAccess;
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0005EB64 File Offset: 0x0005CD64
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			element.AddAttribute("Access", this.m_access.ToString());
		}

		// Token: 0x04000E1A RID: 3610
		private FileIOPermissionAccess m_access;
	}
}
