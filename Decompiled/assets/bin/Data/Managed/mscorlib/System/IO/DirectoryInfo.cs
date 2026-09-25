using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200011C RID: 284
	[ComVisible(true)]
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0002BAD0 File Offset: 0x00029CD0
		public DirectoryInfo(string path) : this(path, false)
		{
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002BADC File Offset: 0x00029CDC
		internal DirectoryInfo(string path, bool simpleOriginalPath)
		{
			base.CheckPath(path);
			this.FullPath = Path.GetFullPath(path);
			if (simpleOriginalPath)
			{
				this.OriginalPath = Path.GetFileName(path);
			}
			else
			{
				this.OriginalPath = path;
			}
			this.Initialize();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002BB1C File Offset: 0x00029D1C
		private DirectoryInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Initialize();
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002BB2C File Offset: 0x00029D2C
		private void Initialize()
		{
			int num = this.FullPath.Length - 1;
			if (num > 1 && this.FullPath[num] == Path.DirectorySeparatorChar)
			{
				num--;
			}
			int num2 = this.FullPath.LastIndexOf(Path.DirectorySeparatorChar, num);
			if (num2 == -1 || (num2 == 0 && num == 0))
			{
				this.current = this.FullPath;
				this.parent = null;
			}
			else
			{
				this.current = this.FullPath.Substring(num2 + 1, num - num2);
				if (num2 == 0 && !Environment.IsRunningOnWindows)
				{
					this.parent = Path.DirectorySeparatorStr;
				}
				else
				{
					this.parent = this.FullPath.Substring(0, num2);
				}
				if (Environment.IsRunningOnWindows && this.parent.Length == 2 && this.parent[1] == ':' && char.IsLetter(this.parent[0]))
				{
					this.parent += Path.DirectorySeparatorChar;
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002BC50 File Offset: 0x00029E50
		public override bool Exists
		{
			get
			{
				base.Refresh(false);
				return this.stat.Attributes != MonoIO.InvalidFileAttributes && (this.stat.Attributes & FileAttributes.Directory) != (FileAttributes)0;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002BC88 File Offset: 0x00029E88
		public DirectoryInfo Parent
		{
			get
			{
				if (this.parent == null || this.parent.Length == 0)
				{
					return null;
				}
				return new DirectoryInfo(this.parent);
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002BCB4 File Offset: 0x00029EB4
		public void Create()
		{
			Directory.CreateDirectory(this.FullPath);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002BCC4 File Offset: 0x00029EC4
		public override string ToString()
		{
			return this.OriginalPath;
		}

		// Token: 0x04000476 RID: 1142
		private string current;

		// Token: 0x04000477 RID: 1143
		private string parent;
	}
}
