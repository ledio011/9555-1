using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x02000332 RID: 818
	[ComVisible(true)]
	public interface ISecurityPolicyEncodable
	{
		// Token: 0x060018AB RID: 6315
		void FromXml(SecurityElement e, PolicyLevel level);

		// Token: 0x060018AC RID: 6316
		SecurityElement ToXml(PolicyLevel level);
	}
}
