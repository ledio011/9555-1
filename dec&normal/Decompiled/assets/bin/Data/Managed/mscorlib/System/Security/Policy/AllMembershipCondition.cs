using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000355 RID: 853
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IMembershipCondition
	{
		// Token: 0x06001956 RID: 6486 RVA: 0x0005D494 File Offset: 0x0005B694
		public bool Check(Evidence evidence)
		{
			return true;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0005D498 File Offset: 0x0005B698
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0005D4A0 File Offset: 0x0005B6A0
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005D4AC File Offset: 0x0005B6AC
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005D4B8 File Offset: 0x0005B6B8
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005D4D4 File Offset: 0x0005B6D4
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0005D4E8 File Offset: 0x0005B6E8
		public override string ToString()
		{
			return "All code";
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005D4F0 File Offset: 0x0005B6F0
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0005D4FC File Offset: 0x0005B6FC
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(AllMembershipCondition), this.version);
		}

		// Token: 0x04000DE3 RID: 3555
		private readonly int version = 1;
	}
}
