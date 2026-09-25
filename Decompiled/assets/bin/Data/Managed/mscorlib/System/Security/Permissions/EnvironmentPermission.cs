using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x02000336 RID: 822
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x0005A8D0 File Offset: 0x00058AD0
		public EnvironmentPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005A8FC File Offset: 0x00058AFC
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.SetPathList(flag, pathList);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005A924 File Offset: 0x00058B24
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Read");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Read, text);
			}
			string text2 = esd.Attribute("Write");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Write, text2);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0005A9A0 File Offset: 0x00058BA0
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
			case EnvironmentPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(flag, true);
				break;
			case EnvironmentPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case EnvironmentPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
			return null;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005AA00 File Offset: 0x00058C00
		public override bool IsSubsetOf(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return false;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.IsUnrestricted();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.readList)
			{
				string item = (string)obj;
				if (!environmentPermission.readList.Contains(item))
				{
					return false;
				}
			}
			foreach (object obj2 in this.writeList)
			{
				string item2 = (string)obj2;
				if (!environmentPermission.writeList.Contains(item2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0005AB18 File Offset: 0x00058D18
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005AB24 File Offset: 0x00058D24
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
			{
				this.readList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value in array)
				{
					this.readList.Add(value);
				}
				break;
			}
			case EnvironmentPermissionAccess.Write:
			{
				this.writeList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value2 in array)
				{
					this.writeList.Add(value2);
				}
				break;
			}
			case EnvironmentPermissionAccess.AllAccess:
			{
				this.readList.Clear();
				this.writeList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value3 in array)
				{
					this.readList.Add(value3);
					this.writeList.Add(value3);
				}
				break;
			}
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0005AC7C File Offset: 0x00058E7C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string pathList = this.GetPathList(EnvironmentPermissionAccess.Read);
				if (pathList != null)
				{
					securityElement.AddAttribute("Read", pathList);
				}
				pathList = this.GetPathList(EnvironmentPermissionAccess.Write);
				if (pathList != null)
				{
					securityElement.AddAttribute("Write", pathList);
				}
			}
			return securityElement;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0005ACE8 File Offset: 0x00058EE8
		private EnvironmentPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			EnvironmentPermission environmentPermission = target as EnvironmentPermission;
			if (environmentPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(EnvironmentPermission));
			}
			return environmentPermission;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005AD1C File Offset: 0x00058F1C
		internal void ThrowInvalidFlag(EnvironmentPermissionAccess flag, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, flag), "flag");
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0005AD64 File Offset: 0x00058F64
		private string GetPathList(ArrayList list)
		{
			if (this.IsUnrestricted())
			{
				return string.Empty;
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				string value = (string)obj;
				stringBuilder.Append(value);
				stringBuilder.Append(";");
			}
			string text = stringBuilder.ToString();
			int length = text.Length;
			if (length > 0)
			{
				return text.Substring(0, length - 1);
			}
			return string.Empty;
		}

		// Token: 0x04000D51 RID: 3409
		private const int version = 1;

		// Token: 0x04000D52 RID: 3410
		private PermissionState _state;

		// Token: 0x04000D53 RID: 3411
		private ArrayList readList;

		// Token: 0x04000D54 RID: 3412
		private ArrayList writeList;
	}
}
