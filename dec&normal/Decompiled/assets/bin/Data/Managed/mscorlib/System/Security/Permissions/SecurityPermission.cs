using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200034B RID: 843
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x0005C57C File Offset: 0x0005A77C
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.Flags = flag;
		}

		// Token: 0x17000486 RID: 1158
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0005C58C File Offset: 0x0005A78C
		public SecurityPermissionFlag Flags
		{
			set
			{
				if ((value & SecurityPermissionFlag.AllFlags) != value)
				{
					string message = string.Format(Locale.GetText("Invalid flags {0}"), value);
					throw new ArgumentException(message, "SecurityPermissionFlag");
				}
				this.flags = value;
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0005C5D0 File Offset: 0x0005A7D0
		public bool IsUnrestricted()
		{
			return this.flags == SecurityPermissionFlag.AllFlags;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005C5E0 File Offset: 0x0005A7E0
		public override bool IsSubsetOf(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.IsEmpty();
			}
			return securityPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this.flags & ~securityPermission.flags) == SecurityPermissionFlag.NoFlags);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005C630 File Offset: 0x0005A830
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = SecurityPermissionFlag.AllFlags;
			}
			else
			{
				string text = esd.Attribute("Flags");
				if (text == null)
				{
					this.flags = SecurityPermissionFlag.NoFlags;
				}
				else
				{
					this.flags = (SecurityPermissionFlag)((int)Enum.Parse(typeof(SecurityPermissionFlag), text));
				}
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0005C6A0 File Offset: 0x0005A8A0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Flags", this.flags.ToString());
			}
			return securityElement;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005C6F4 File Offset: 0x0005A8F4
		private bool IsEmpty()
		{
			return this.flags == SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0005C700 File Offset: 0x0005A900
		private SecurityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SecurityPermission));
			}
			return securityPermission;
		}

		// Token: 0x04000DB8 RID: 3512
		private const int version = 1;

		// Token: 0x04000DB9 RID: 3513
		private SecurityPermissionFlag flags;
	}
}
