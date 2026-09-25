using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200036B RID: 875
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : IBuiltInEvidence, IIdentityPermissionFactory
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x000602D0 File Offset: 0x0005E4D0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x000602D8 File Offset: 0x0005E4D8
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.publickey;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x000602E0 File Offset: 0x0005E4E0
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000602E8 File Offset: 0x0005E4E8
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.publickey, this.name, this.version);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00060304 File Offset: 0x0005E504
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && !(this.name != strongName.Name) && this.Version.Equals(strongName.Version) && this.PublicKey.Equals(strongName.PublicKey);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00060364 File Offset: 0x0005E564
		public override int GetHashCode()
		{
			return this.publickey.GetHashCode();
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00060374 File Offset: 0x0005E574
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(typeof(StrongName).Name);
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Key", this.publickey.ToString());
			securityElement.AddAttribute("Name", this.name);
			securityElement.AddAttribute("Version", this.version.ToString());
			return securityElement.ToString();
		}

		// Token: 0x04000E36 RID: 3638
		private StrongNamePublicKeyBlob publickey;

		// Token: 0x04000E37 RID: 3639
		private string name;

		// Token: 0x04000E38 RID: 3640
		private Version version;
	}
}
