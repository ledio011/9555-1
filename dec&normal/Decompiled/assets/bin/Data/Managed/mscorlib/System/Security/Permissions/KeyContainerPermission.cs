using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000341 RID: 833
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x0005BAA4 File Offset: 0x00059CA4
		public KeyContainerPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0005BAC4 File Offset: 0x00059CC4
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
			else
			{
				this._flags = (KeyContainerPermissionFlags)((int)Enum.Parse(typeof(KeyContainerPermissionFlags), securityElement.Attribute("Flags")));
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0005BB20 File Offset: 0x00059D20
		[MonoTODO("(2.0)")]
		public override bool IsSubsetOf(IPermission target)
		{
			return false;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0005BB24 File Offset: 0x00059D24
		public bool IsUnrestricted()
		{
			return this._flags == KeyContainerPermissionFlags.AllFlags;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0005BB34 File Offset: 0x00059D34
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x04000D86 RID: 3462
		private const int version = 1;

		// Token: 0x04000D87 RID: 3463
		private KeyContainerPermissionAccessEntryCollection _accessEntries;

		// Token: 0x04000D88 RID: 3464
		private KeyContainerPermissionFlags _flags;
	}
}
