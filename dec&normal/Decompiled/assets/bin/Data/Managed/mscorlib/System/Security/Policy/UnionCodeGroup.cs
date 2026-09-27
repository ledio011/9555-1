using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200036D RID: 877
	[ComVisible(true)]
	[Serializable]
	public sealed class UnionCodeGroup : CodeGroup
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x0006076C File Offset: 0x0005E96C
		public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy) : base(membershipCondition, policy)
		{
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00060778 File Offset: 0x0005E978
		internal UnionCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00060784 File Offset: 0x0005E984
		public override CodeGroup Copy()
		{
			return this.Copy(true);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00060790 File Offset: 0x0005E990
		internal CodeGroup Copy(bool childs)
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(base.MembershipCondition, base.PolicyStatement);
			unionCodeGroup.Name = base.Name;
			unionCodeGroup.Description = base.Description;
			if (childs)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					unionCodeGroup.AddChild(codeGroup.Copy());
				}
			}
			return unionCodeGroup;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0006082C File Offset: 0x0005EA2C
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
			PermissionSet permissionSet = base.PolicyStatement.PermissionSet.Copy();
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
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}
	}
}
