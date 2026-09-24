using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	// Token: 0x0200036C RID: 876
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IMembershipCondition
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x000603EC File Offset: 0x0005E5EC
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			this.blob = blob;
			this.name = name;
			if (version != null)
			{
				this.assemblyVersion = (Version)version.Clone();
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00060444 File Offset: 0x0005E644
		internal StrongNameMembershipCondition(SecurityElement e)
		{
			this.FromXml(e);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0006045C File Offset: 0x0005E65C
		internal StrongNameMembershipCondition()
		{
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x0006046C File Offset: 0x0005E66C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00060474 File Offset: 0x0005E674
		public Version Version
		{
			get
			{
				return this.assemblyVersion;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0006047C File Offset: 0x0005E67C
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.blob;
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00060484 File Offset: 0x0005E684
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				StrongName strongName = obj as StrongName;
				if (strongName != null)
				{
					return strongName.PublicKey.Equals(this.blob) && (this.name == null || !(this.name != strongName.Name)) && (!(this.assemblyVersion != null) || this.assemblyVersion.Equals(strongName.Version));
				}
			}
			return false;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0006052C File Offset: 0x0005E72C
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.blob, this.name, this.assemblyVersion);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00060548 File Offset: 0x0005E748
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition == null)
			{
				return false;
			}
			if (!strongNameMembershipCondition.PublicKey.Equals(this.PublicKey))
			{
				return false;
			}
			if (this.name != strongNameMembershipCondition.Name)
			{
				return false;
			}
			if (this.assemblyVersion != null)
			{
				return this.assemblyVersion.Equals(strongNameMembershipCondition.Version);
			}
			return strongNameMembershipCondition.Version == null;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000605C4 File Offset: 0x0005E7C4
		public override int GetHashCode()
		{
			return this.blob.GetHashCode();
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000605D4 File Offset: 0x0005E7D4
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000605E0 File Offset: 0x0005E7E0
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.blob = StrongNamePublicKeyBlob.FromString(e.Attribute("PublicKeyBlob"));
			this.name = e.Attribute("Name");
			string text = e.Attribute("AssemblyVersion");
			if (text == null)
			{
				this.assemblyVersion = null;
			}
			else
			{
				this.assemblyVersion = new Version(text);
			}
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00060658 File Offset: 0x0005E858
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("StrongName - ");
			stringBuilder.Append(this.blob);
			if (this.name != null)
			{
				stringBuilder.AppendFormat(" name = {0}", this.name);
			}
			if (this.assemblyVersion != null)
			{
				stringBuilder.AppendFormat(" version = {0}", this.assemblyVersion);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000606C4 File Offset: 0x0005E8C4
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000606D0 File Offset: 0x0005E8D0
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(StrongNameMembershipCondition), this.version);
			if (this.blob != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", this.blob.ToString());
			}
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.assemblyVersion != null)
			{
				string text = this.assemblyVersion.ToString();
				if (text != "0.0")
				{
					securityElement.AddAttribute("AssemblyVersion", text);
				}
			}
			return securityElement;
		}

		// Token: 0x04000E39 RID: 3641
		private readonly int version = 1;

		// Token: 0x04000E3A RID: 3642
		private StrongNamePublicKeyBlob blob;

		// Token: 0x04000E3B RID: 3643
		private string name;

		// Token: 0x04000E3C RID: 3644
		private Version assemblyVersion;
	}
}
