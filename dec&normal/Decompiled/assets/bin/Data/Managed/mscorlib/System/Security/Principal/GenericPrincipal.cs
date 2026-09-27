using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : IPrincipal
	{
		// Token: 0x06001A13 RID: 6675 RVA: 0x00060BD0 File Offset: 0x0005EDD0
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
		}

		// Token: 0x04000E47 RID: 3655
		private IIdentity m_identity;

		// Token: 0x04000E48 RID: 3656
		private string[] m_roles;
	}
}
