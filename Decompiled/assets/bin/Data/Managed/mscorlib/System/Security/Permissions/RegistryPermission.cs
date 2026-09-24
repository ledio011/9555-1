using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x02000348 RID: 840
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x06001909 RID: 6409 RVA: 0x0005BEC8 File Offset: 0x0005A0C8
		public RegistryPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0005BF00 File Offset: 0x0005A100
		public string GetPathList(RegistryPermissionAccess access)
		{
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
			case RegistryPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(access, true);
				goto IL_6E;
			case RegistryPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case RegistryPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			case RegistryPermissionAccess.Create:
				return this.GetPathList(this.createList);
			}
			this.ThrowInvalidFlag(access, false);
			IL_6E:
			return null;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0005BF7C File Offset: 0x0005A17C
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
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
				return;
			}
			case RegistryPermissionAccess.Write:
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
				return;
			}
			case RegistryPermissionAccess.Create:
			{
				this.createList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value3 in array)
				{
					this.createList.Add(value3);
				}
				return;
			}
			case RegistryPermissionAccess.AllAccess:
			{
				this.createList.Clear();
				this.readList.Clear();
				this.writeList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value4 in array)
				{
					this.createList.Add(value4);
					this.readList.Add(value4);
					this.writeList.Add(value4);
				}
				return;
			}
			}
			this.ThrowInvalidFlag(access, false);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0005C150 File Offset: 0x0005A350
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Create");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Create, text);
			}
			string text2 = esd.Attribute("Read");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Read, text2);
			}
			string text3 = esd.Attribute("Write");
			if (text3 != null && text3.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Write, text3);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0005C200 File Offset: 0x0005A400
		public override bool IsSubsetOf(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return false;
			}
			if (registryPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.IsUnrestricted();
			}
			return registryPermission.IsUnrestricted() || (this.KeyIsSubsetOf(this.createList, registryPermission.createList) && this.KeyIsSubsetOf(this.readList, registryPermission.readList) && this.KeyIsSubsetOf(this.writeList, registryPermission.writeList));
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0005C29C File Offset: 0x0005A49C
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0005C2A8 File Offset: 0x0005A4A8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string pathList = this.GetPathList(RegistryPermissionAccess.Create);
				if (pathList != null)
				{
					securityElement.AddAttribute("Create", pathList);
				}
				pathList = this.GetPathList(RegistryPermissionAccess.Read);
				if (pathList != null)
				{
					securityElement.AddAttribute("Read", pathList);
				}
				pathList = this.GetPathList(RegistryPermissionAccess.Write);
				if (pathList != null)
				{
					securityElement.AddAttribute("Write", pathList);
				}
			}
			return securityElement;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0005C330 File Offset: 0x0005A530
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.createList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0005C370 File Offset: 0x0005A570
		private RegistryPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(RegistryPermission));
			}
			return registryPermission;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		internal void ThrowInvalidFlag(RegistryPermissionAccess flag, bool context)
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

		// Token: 0x06001913 RID: 6419 RVA: 0x0005C3EC File Offset: 0x0005A5EC
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

		// Token: 0x06001914 RID: 6420 RVA: 0x0005C4AC File Offset: 0x0005A6AC
		internal bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string value = (string)obj2;
					if (text.StartsWith(value))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000DA3 RID: 3491
		private const int version = 1;

		// Token: 0x04000DA4 RID: 3492
		private PermissionState _state;

		// Token: 0x04000DA5 RID: 3493
		private ArrayList createList;

		// Token: 0x04000DA6 RID: 3494
		private ArrayList readList;

		// Token: 0x04000DA7 RID: 3495
		private ArrayList writeList;
	}
}
