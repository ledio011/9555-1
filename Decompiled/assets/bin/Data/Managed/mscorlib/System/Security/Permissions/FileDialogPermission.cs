using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000338 RID: 824
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x0005AE24 File Offset: 0x00059024
		public FileDialogPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._access = FileDialogPermissionAccess.OpenSave;
			}
			else
			{
				this._access = FileDialogPermissionAccess.None;
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0005AE4C File Offset: 0x0005904C
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			this.Access = access;
		}

		// Token: 0x1700047C RID: 1148
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0005AE5C File Offset: 0x0005905C
		public FileDialogPermissionAccess Access
		{
			set
			{
				if (!Enum.IsDefined(typeof(FileDialogPermissionAccess), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "FileDialogPermissionAccess");
				}
				this._access = value;
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0005AEAC File Offset: 0x000590AC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._access = FileDialogPermissionAccess.OpenSave;
			}
			else
			{
				string text = esd.Attribute("Access");
				if (text == null)
				{
					this._access = FileDialogPermissionAccess.None;
				}
				else
				{
					this._access = (FileDialogPermissionAccess)((int)Enum.Parse(typeof(FileDialogPermissionAccess), text));
				}
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0005AF18 File Offset: 0x00059118
		public override bool IsSubsetOf(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			return fileDialogPermission != null && (this._access & fileDialogPermission._access) == this._access;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0005AF4C File Offset: 0x0005914C
		public bool IsUnrestricted()
		{
			return this._access == FileDialogPermissionAccess.OpenSave;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0005AF58 File Offset: 0x00059158
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			switch (this._access)
			{
			case FileDialogPermissionAccess.Open:
				securityElement.AddAttribute("Access", "Open");
				break;
			case FileDialogPermissionAccess.Save:
				securityElement.AddAttribute("Access", "Save");
				break;
			case FileDialogPermissionAccess.OpenSave:
				securityElement.AddAttribute("Unrestricted", "true");
				break;
			}
			return securityElement;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0005AFD0 File Offset: 0x000591D0
		private FileDialogPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileDialogPermission fileDialogPermission = target as FileDialogPermission;
			if (fileDialogPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileDialogPermission));
			}
			return fileDialogPermission;
		}

		// Token: 0x04000D5A RID: 3418
		private const int version = 1;

		// Token: 0x04000D5B RID: 3419
		private FileDialogPermissionAccess _access;
	}
}
