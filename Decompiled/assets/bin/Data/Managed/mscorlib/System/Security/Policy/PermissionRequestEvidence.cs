using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000366 RID: 870
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionRequestEvidence : IBuiltInEvidence
	{
		// Token: 0x060019C6 RID: 6598 RVA: 0x0005F330 File Offset: 0x0005D530
		public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
		{
			if (request != null)
			{
				this.requested = new PermissionSet(request);
			}
			if (optional != null)
			{
				this.optional = new PermissionSet(optional);
			}
			if (denied != null)
			{
				this.denied = new PermissionSet(denied);
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0005F370 File Offset: 0x0005D570
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
			securityElement.AddAttribute("version", "1");
			if (this.requested != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Request");
				securityElement2.AddChild(this.requested.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.optional != null)
			{
				SecurityElement securityElement3 = new SecurityElement("Optional");
				securityElement3.AddChild(this.optional.ToXml());
				securityElement.AddChild(securityElement3);
			}
			if (this.denied != null)
			{
				SecurityElement securityElement4 = new SecurityElement("Denied");
				securityElement4.AddChild(this.denied.ToXml());
				securityElement.AddChild(securityElement4);
			}
			return securityElement.ToString();
		}

		// Token: 0x04000E24 RID: 3620
		private PermissionSet requested;

		// Token: 0x04000E25 RID: 3621
		private PermissionSet optional;

		// Token: 0x04000E26 RID: 3622
		private PermissionSet denied;
	}
}
