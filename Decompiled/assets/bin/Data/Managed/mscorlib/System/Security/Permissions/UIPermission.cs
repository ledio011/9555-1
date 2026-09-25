using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000350 RID: 848
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x0005CFD0 File Offset: 0x0005B1D0
		public UIPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._clipboard = UIPermissionClipboard.AllClipboard;
				this._window = UIPermissionWindow.AllWindows;
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0005CFF4 File Offset: 0x0005B1F4
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
			this.Window = windowFlag;
		}

		// Token: 0x1700048A RID: 1162
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0005D00C File Offset: 0x0005B20C
		public UIPermissionClipboard Clipboard
		{
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionClipboard), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "UIPermissionClipboard");
				}
				this._clipboard = value;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x0005D05C File Offset: 0x0005B25C
		public UIPermissionWindow Window
		{
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionWindow), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "UIPermissionWindow");
				}
				this._window = value;
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0005D0AC File Offset: 0x0005B2AC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._window = UIPermissionWindow.AllWindows;
				this._clipboard = UIPermissionClipboard.AllClipboard;
			}
			else
			{
				string text = esd.Attribute("Window");
				if (text == null)
				{
					this._window = UIPermissionWindow.NoWindows;
				}
				else
				{
					this._window = (UIPermissionWindow)((int)Enum.Parse(typeof(UIPermissionWindow), text));
				}
				string text2 = esd.Attribute("Clipboard");
				if (text2 == null)
				{
					this._clipboard = UIPermissionClipboard.NoClipboard;
				}
				else
				{
					this._clipboard = (UIPermissionClipboard)((int)Enum.Parse(typeof(UIPermissionClipboard), text2));
				}
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0005D158 File Offset: 0x0005B358
		public override bool IsSubsetOf(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.IsEmpty(this._window, this._clipboard);
			}
			return uipermission.IsUnrestricted() || (this._window <= uipermission._window && this._clipboard <= uipermission._clipboard);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0005D1B8 File Offset: 0x0005B3B8
		public bool IsUnrestricted()
		{
			return this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0005D1D4 File Offset: 0x0005B3D4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				if (this._window != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", this._window.ToString());
				}
				if (this._clipboard != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", this._clipboard.ToString());
				}
			}
			return securityElement;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0005D264 File Offset: 0x0005B464
		private bool IsEmpty(UIPermissionWindow w, UIPermissionClipboard c)
		{
			return w == UIPermissionWindow.NoWindows && c == UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0005D274 File Offset: 0x0005B474
		private UIPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UIPermission uipermission = target as UIPermission;
			if (uipermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UIPermission));
			}
			return uipermission;
		}

		// Token: 0x04000DD3 RID: 3539
		private const int version = 1;

		// Token: 0x04000DD4 RID: 3540
		private UIPermissionWindow _window;

		// Token: 0x04000DD5 RID: 3541
		private UIPermissionClipboard _clipboard;
	}
}
