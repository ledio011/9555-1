using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000331 RID: 817
	[ComVisible(true)]
	public interface ISecurityEncodable
	{
		// Token: 0x060018A9 RID: 6313
		void FromXml(SecurityElement e);

		// Token: 0x060018AA RID: 6314
		SecurityElement ToXml();
	}
}
