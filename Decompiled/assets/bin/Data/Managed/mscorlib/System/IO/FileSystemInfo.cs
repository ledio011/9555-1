using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200012A RID: 298
	[ComVisible(true)]
	[Serializable]
	public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x0002D740 File Offset: 0x0002B940
		protected FileSystemInfo()
		{
			this.valid = false;
			this.FullPath = null;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002D758 File Offset: 0x0002B958
		protected FileSystemInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.FullPath = info.GetString("FullPath");
			this.OriginalPath = info.GetString("OriginalPath");
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002D794 File Offset: 0x0002B994
		[ComVisible(false)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("OriginalPath", this.OriginalPath, typeof(string));
			info.AddValue("FullPath", this.FullPath, typeof(string));
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BAF RID: 2991
		public abstract bool Exists { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002D7CC File Offset: 0x0002B9CC
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		internal void Refresh(bool force)
		{
			if (this.valid && !force)
			{
				return;
			}
			MonoIOError monoIOError;
			MonoIO.GetFileStat(this.FullName, out this.stat, out monoIOError);
			this.valid = true;
			this.InternalRefresh();
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002D814 File Offset: 0x0002BA14
		internal virtual void InternalRefresh()
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002D818 File Offset: 0x0002BA18
		internal void CheckPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
		}

		// Token: 0x040004C2 RID: 1218
		protected string FullPath;

		// Token: 0x040004C3 RID: 1219
		protected string OriginalPath;

		// Token: 0x040004C4 RID: 1220
		internal MonoIOStat stat;

		// Token: 0x040004C5 RID: 1221
		internal bool valid;
	}
}
