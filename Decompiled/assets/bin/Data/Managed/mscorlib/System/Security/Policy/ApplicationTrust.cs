using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	// Token: 0x02000356 RID: 854
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : ISecurityEncodable
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x0005D520 File Offset: 0x0005B720
		public ApplicationTrust()
		{
			this.fullTrustAssemblies = new List<StrongName>(0);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0005D534 File Offset: 0x0005B734
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this._defaultPolicy == null)
				{
					this._defaultPolicy = this.GetDefaultGrantSet();
				}
				return this._defaultPolicy;
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005D554 File Offset: 0x0005B754
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Tag != "ApplicationTrust")
			{
				throw new ArgumentException("element");
			}
			string text = element.Attribute("FullName");
			if (text != null)
			{
				this._appid = new ApplicationIdentity(text);
			}
			else
			{
				this._appid = null;
			}
			this._defaultPolicy = null;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = securityElement.Children[i] as SecurityElement;
					if (securityElement2.Tag == "PolicyStatement")
					{
						this.DefaultGrantSet.FromXml(securityElement2, null);
						break;
					}
				}
			}
			if (!bool.TryParse(element.Attribute("TrustedToRun"), out this._trustrun))
			{
				this._trustrun = false;
			}
			if (!bool.TryParse(element.Attribute("Persist"), out this._persist))
			{
				this._persist = false;
			}
			this._xtranfo = null;
			SecurityElement securityElement3 = element.SearchForChildByTag("ExtraInfo");
			if (securityElement3 != null)
			{
				text = securityElement3.Attribute("Data");
				if (text != null)
				{
					byte[] buffer = CryptoConvert.FromHex(text);
					using (MemoryStream memoryStream = new MemoryStream(buffer))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this._xtranfo = binaryFormatter.Deserialize(memoryStream);
					}
				}
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0005D6E8 File Offset: 0x0005B8E8
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this._appid != null)
			{
				securityElement.AddAttribute("FullName", this._appid.FullName);
			}
			if (this._trustrun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this._persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
			securityElement2.AddChild(this.DefaultGrantSet.ToXml());
			securityElement.AddChild(securityElement2);
			if (this._xtranfo != null)
			{
				byte[] input = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, this._xtranfo);
					input = memoryStream.ToArray();
				}
				SecurityElement securityElement3 = new SecurityElement("ExtraInfo");
				securityElement3.AddAttribute("Data", CryptoConvert.ToHex(input));
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005D804 File Offset: 0x0005BA04
		private PolicyStatement GetDefaultGrantSet()
		{
			PermissionSet permSet = new PermissionSet(PermissionState.None);
			return new PolicyStatement(permSet);
		}

		// Token: 0x04000DE4 RID: 3556
		private ApplicationIdentity _appid;

		// Token: 0x04000DE5 RID: 3557
		private PolicyStatement _defaultPolicy;

		// Token: 0x04000DE6 RID: 3558
		private object _xtranfo;

		// Token: 0x04000DE7 RID: 3559
		private bool _trustrun;

		// Token: 0x04000DE8 RID: 3560
		private bool _persist;

		// Token: 0x04000DE9 RID: 3561
		private IList<StrongName> fullTrustAssemblies;
	}
}
