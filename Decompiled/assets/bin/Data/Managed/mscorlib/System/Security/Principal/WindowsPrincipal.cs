using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000378 RID: 888
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : IPrincipal
	{
		// Token: 0x06001A21 RID: 6689 RVA: 0x00060F00 File Offset: 0x0005F100
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		// Token: 0x04000E59 RID: 3673
		private WindowsIdentity _identity;

		// Token: 0x04000E5A RID: 3674
		private string[] m_roles;
	}
}
