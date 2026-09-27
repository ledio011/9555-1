using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200034D RID: 845
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x0600191D RID: 6429 RVA: 0x0005C734 File Offset: 0x0005A934
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this._state = PermissionState.None;
			this._list = new ArrayList();
			this._list.Add(new StrongNameIdentityPermission.SNIP(blob, name, version));
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005C7B0 File Offset: 0x0005A9B0
		public string Name
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).Name;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0005C7F0 File Offset: 0x0005A9F0
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).PublicKey;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005C830 File Offset: 0x0005AA30
		public Version Version
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).AssemblyVersion;
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0005C870 File Offset: 0x0005AA70
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._list.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement se = (SecurityElement)obj;
					this._list.Add(this.FromSecurityElement(se));
				}
			}
			else
			{
				this._list.Add(this.FromSecurityElement(e));
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0005C938 File Offset: 0x0005AB38
		private StrongNameIdentityPermission.SNIP FromSecurityElement(SecurityElement se)
		{
			string name = se.Attribute("Name");
			StrongNamePublicKeyBlob pk = StrongNamePublicKeyBlob.FromString(se.Attribute("PublicKeyBlob"));
			string text = se.Attribute("AssemblyVersion");
			Version version = (text != null) ? new Version(text) : null;
			return new StrongNameIdentityPermission.SNIP(pk, name, version);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0005C98C File Offset: 0x0005AB8C
		public override bool IsSubsetOf(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return strongNameIdentityPermission.IsUnrestricted();
			}
			if (strongNameIdentityPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				foreach (object obj2 in strongNameIdentityPermission._list)
				{
					StrongNameIdentityPermission.SNIP target2 = (StrongNameIdentityPermission.SNIP)obj2;
					if (!snip.IsSubsetOf(target2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0005CA98 File Offset: 0x0005AC98
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._list.Count > 1)
			{
				foreach (object obj in this._list)
				{
					StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
					SecurityElement securityElement2 = new SecurityElement("StrongName");
					this.ToSecurityElement(securityElement2, snip);
					securityElement.AddChild(securityElement2);
				}
			}
			else if (this._list.Count == 1)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)this._list[0];
				if (!this.IsEmpty(snip2))
				{
					this.ToSecurityElement(securityElement, snip2);
				}
			}
			return securityElement;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0005CB6C File Offset: 0x0005AD6C
		private void ToSecurityElement(SecurityElement se, StrongNameIdentityPermission.SNIP snip)
		{
			if (snip.PublicKey != null)
			{
				se.AddAttribute("PublicKeyBlob", snip.PublicKey.ToString());
			}
			if (snip.Name != null)
			{
				se.AddAttribute("Name", snip.Name);
			}
			if (snip.AssemblyVersion != null)
			{
				se.AddAttribute("AssemblyVersion", snip.AssemblyVersion.ToString());
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0005CBE4 File Offset: 0x0005ADE4
		private bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
		private bool IsEmpty(StrongNameIdentityPermission.SNIP snip)
		{
			return this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0005CC4C File Offset: 0x0005AE4C
		private bool IsEmpty()
		{
			return !this.IsUnrestricted() && this._list.Count <= 1 && this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0005CCC8 File Offset: 0x0005AEC8
		private StrongNameIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(StrongNameIdentityPermission));
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x04000DCB RID: 3531
		private const int version = 1;

		// Token: 0x04000DCC RID: 3532
		private static Version defaultVersion = new Version(0, 0);

		// Token: 0x04000DCD RID: 3533
		private PermissionState _state;

		// Token: 0x04000DCE RID: 3534
		private ArrayList _list;

		// Token: 0x0200034E RID: 846
		private struct SNIP
		{
			// Token: 0x0600192B RID: 6443 RVA: 0x0005CCFC File Offset: 0x0005AEFC
			internal SNIP(StrongNamePublicKeyBlob pk, string name, Version version)
			{
				this.PublicKey = pk;
				this.Name = name;
				this.AssemblyVersion = version;
			}

			// Token: 0x0600192C RID: 6444 RVA: 0x0005CD14 File Offset: 0x0005AF14
			internal bool IsNameSubsetOf(string target)
			{
				if (this.Name == null)
				{
					return target == null;
				}
				if (target == null)
				{
					return true;
				}
				int num = this.Name.LastIndexOf('*');
				if (num == 0)
				{
					return true;
				}
				if (num == -1)
				{
					num = this.Name.Length;
				}
				return string.Compare(this.Name, 0, target, 0, num, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x0600192D RID: 6445 RVA: 0x0005CD7C File Offset: 0x0005AF7C
			internal bool IsSubsetOf(StrongNameIdentityPermission.SNIP target)
			{
				return (this.PublicKey != null && this.PublicKey.Equals(target.PublicKey)) || (this.IsNameSubsetOf(target.Name) && (!(this.AssemblyVersion != null) || this.AssemblyVersion.Equals(target.AssemblyVersion)) && this.PublicKey == null && target.PublicKey == null);
			}

			// Token: 0x04000DCF RID: 3535
			public StrongNamePublicKeyBlob PublicKey;

			// Token: 0x04000DD0 RID: 3536
			public string Name;

			// Token: 0x04000DD1 RID: 3537
			public Version AssemblyVersion;
		}
	}
}
