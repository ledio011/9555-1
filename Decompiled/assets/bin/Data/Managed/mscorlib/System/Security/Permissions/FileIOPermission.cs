using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200033A RID: 826
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x060018CF RID: 6351 RVA: 0x0005B004 File Offset: 0x00059204
		public FileIOPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.m_Unrestricted = true;
				this.m_AllFilesAccess = FileIOPermissionAccess.AllAccess;
				this.m_AllLocalFilesAccess = FileIOPermissionAccess.AllAccess;
			}
			this.CreateLists();
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0005B038 File Offset: 0x00059238
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CreateLists();
			this.AddPathList(access, path);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0005B078 File Offset: 0x00059278
		internal void CreateLists()
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.appendList = new ArrayList();
			this.pathList = new ArrayList();
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0005B0A8 File Offset: 0x000592A8
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_AllFilesAccess;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0005B0B0 File Offset: 0x000592B0
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_AllLocalFilesAccess;
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0005B0B8 File Offset: 0x000592B8
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.AddPathInternal(access, path);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0005B0DC File Offset: 0x000592DC
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			foreach (string path in pathList)
			{
				this.AddPathInternal(access, path);
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0005B124 File Offset: 0x00059324
		internal void AddPathInternal(FileIOPermissionAccess access, string path)
		{
			path = Path.InsecureGetFullPath(path);
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Add(path);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Add(path);
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0005B194 File Offset: 0x00059394
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.m_Unrestricted = true;
			}
			else
			{
				this.m_Unrestricted = false;
				string text = esd.Attribute("Read");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.Read, array);
				}
				text = esd.Attribute("Write");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.Write, array);
				}
				text = esd.Attribute("Append");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.Append, array);
				}
				text = esd.Attribute("PathDiscovery");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.PathDiscovery, array);
				}
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0005B280 File Offset: 0x00059480
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			ArrayList arrayList = new ArrayList();
			switch (access)
			{
			case FileIOPermissionAccess.NoAccess:
				goto IL_9D;
			case FileIOPermissionAccess.Read:
				arrayList.AddRange(this.readList);
				goto IL_9D;
			case FileIOPermissionAccess.Write:
				arrayList.AddRange(this.writeList);
				goto IL_9D;
			case FileIOPermissionAccess.Append:
				arrayList.AddRange(this.appendList);
				goto IL_9D;
			case FileIOPermissionAccess.PathDiscovery:
				arrayList.AddRange(this.pathList);
				goto IL_9D;
			}
			FileIOPermission.ThrowInvalidFlag(access, false);
			IL_9D:
			return (arrayList.Count <= 0) ? null : ((string[])arrayList.ToArray(typeof(string)));
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005B354 File Offset: 0x00059554
		public override bool IsSubsetOf(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return false;
			}
			if (fileIOPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.IsUnrestricted();
			}
			return fileIOPermission.IsUnrestricted() || ((this.m_AllFilesAccess & fileIOPermission.AllFiles) == this.m_AllFilesAccess && (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles) == this.m_AllLocalFilesAccess && FileIOPermission.KeyIsSubsetOf(this.appendList, fileIOPermission.appendList) && FileIOPermission.KeyIsSubsetOf(this.readList, fileIOPermission.readList) && FileIOPermission.KeyIsSubsetOf(this.writeList, fileIOPermission.writeList) && FileIOPermission.KeyIsSubsetOf(this.pathList, fileIOPermission.pathList));
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005B438 File Offset: 0x00059638
		public bool IsUnrestricted()
		{
			return this.m_Unrestricted;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0005B440 File Offset: 0x00059640
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string[] array = this.GetPathList(FileIOPermissionAccess.Append);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("Append", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Read);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("Read", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Write);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("Write", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.PathDiscovery);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("PathDiscovery", string.Join(";", array));
				}
			}
			return securityElement;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005B52C File Offset: 0x0005972C
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005B530 File Offset: 0x00059730
		[ComVisible(false)]
		[MonoTODO("(2.0)")]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005B538 File Offset: 0x00059738
		private bool IsEmpty()
		{
			return !this.m_Unrestricted && this.appendList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0 && this.pathList.Count == 0;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005B594 File Offset: 0x00059794
		private static FileIOPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileIOPermission));
			}
			return fileIOPermission;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005B5C8 File Offset: 0x000597C8
		internal static void ThrowInvalidFlag(FileIOPermissionAccess access, bool context)
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
			throw new ArgumentException(string.Format(text, access), "access");
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005B610 File Offset: 0x00059810
		internal static void ThrowIfInvalidPath(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName != null && directoryName.LastIndexOfAny(FileIOPermission.BadPathNameCharacters) >= 0)
			{
				string message = string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), path);
				throw new ArgumentException(message, "path");
			}
			string fileName = Path.GetFileName(path);
			if (fileName != null && fileName.LastIndexOfAny(FileIOPermission.BadFileNameCharacters) >= 0)
			{
				string message2 = string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), path);
				throw new ArgumentException(message2, "path");
			}
			if (!Path.IsPathRooted(path))
			{
				string text = Locale.GetText("Absolute path information is required.");
				throw new ArgumentException(text, "path");
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005B6B8 File Offset: 0x000598B8
		internal static void ThrowIfInvalidPath(string[] paths)
		{
			foreach (string path in paths)
			{
				FileIOPermission.ThrowIfInvalidPath(path);
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005B6E8 File Offset: 0x000598E8
		internal static bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string path = (string)obj;
				foreach (object obj2 in target)
				{
					string subset = (string)obj2;
					if (Path.IsPathSubsetOf(subset, path))
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

		// Token: 0x04000D61 RID: 3425
		private const int version = 1;

		// Token: 0x04000D62 RID: 3426
		private static char[] BadPathNameCharacters = Path.GetInvalidPathChars();

		// Token: 0x04000D63 RID: 3427
		private static char[] BadFileNameCharacters = Path.GetInvalidFileNameChars();

		// Token: 0x04000D64 RID: 3428
		private bool m_Unrestricted;

		// Token: 0x04000D65 RID: 3429
		private FileIOPermissionAccess m_AllFilesAccess;

		// Token: 0x04000D66 RID: 3430
		private FileIOPermissionAccess m_AllLocalFilesAccess;

		// Token: 0x04000D67 RID: 3431
		private ArrayList readList;

		// Token: 0x04000D68 RID: 3432
		private ArrayList writeList;

		// Token: 0x04000D69 RID: 3433
		private ArrayList appendList;

		// Token: 0x04000D6A RID: 3434
		private ArrayList pathList;
	}
}
