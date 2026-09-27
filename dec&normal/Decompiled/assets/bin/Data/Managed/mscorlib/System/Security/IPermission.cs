using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000330 RID: 816
	[ComVisible(true)]
	public interface IPermission : ISecurityEncodable
	{
		// Token: 0x060018A7 RID: 6311
		void Demand();

		// Token: 0x060018A8 RID: 6312
		bool IsSubsetOf(IPermission target);
	}
}
