using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000363 RID: 867
	[ComVisible(true)]
	public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x060019B5 RID: 6581
		bool Check(Evidence evidence);

		// Token: 0x060019B6 RID: 6582
		IMembershipCondition Copy();

		// Token: 0x060019B7 RID: 6583
		bool Equals(object obj);

		// Token: 0x060019B8 RID: 6584
		string ToString();
	}
}
