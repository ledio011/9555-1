using System;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200012F RID: 303
	internal sealed class MonoIO
	{
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002E054 File Offset: 0x0002C254
		public static Exception GetException(MonoIOError error)
		{
			if (error == MonoIOError.ERROR_ACCESS_DENIED)
			{
				return new UnauthorizedAccessException("Access to the path is denied.");
			}
			if (error != MonoIOError.ERROR_FILE_EXISTS)
			{
				return MonoIO.GetException(string.Empty, error);
			}
			string message = "Cannot create a file that already exist.";
			return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002E0A4 File Offset: 0x0002C2A4
		public static Exception GetException(string path, MonoIOError error)
		{
			switch (error)
			{
			case MonoIOError.ERROR_FILE_NOT_FOUND:
			{
				string message = string.Format("Could not find file \"{0}\"", path);
				return new IsolatedStorageException(message);
			}
			case MonoIOError.ERROR_PATH_NOT_FOUND:
			{
				string message = string.Format("Could not find a part of the path \"{0}\"", path);
				return new IsolatedStorageException(message);
			}
			case MonoIOError.ERROR_TOO_MANY_OPEN_FILES:
				return new IOException("Too many open files", (int)((MonoIOError)(-2147024896) | error));
			case MonoIOError.ERROR_ACCESS_DENIED:
			{
				string message = string.Format("Access to the path \"{0}\" is denied.", path);
				return new UnauthorizedAccessException(message);
			}
			case MonoIOError.ERROR_INVALID_HANDLE:
			{
				string message = string.Format("Invalid handle to path \"{0}\"", path);
				return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
			}
			default:
				switch (error)
				{
				case MonoIOError.ERROR_WRITE_FAULT:
				{
					string message = string.Format("Write fault on path {0}", path);
					return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
				}
				default:
					switch (error)
					{
					case MonoIOError.ERROR_INVALID_DRIVE:
					{
						string message = string.Format("Could not find the drive  '{0}'. The drive might not be ready or might not be mapped.", path);
						return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
					}
					default:
						switch (error)
						{
						case MonoIOError.ERROR_FILE_EXISTS:
						{
							string message = string.Format("Could not create file \"{0}\". File already exists.", path);
							return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
						}
						default:
							if (error == MonoIOError.ERROR_HANDLE_DISK_FULL)
							{
								string message = string.Format("Disk full. Path {0}", path);
								return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
							}
							if (error == MonoIOError.ERROR_INVALID_PARAMETER)
							{
								string message = string.Format("Invalid parameter", new object[0]);
								return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
							}
							if (error == MonoIOError.ERROR_DIR_NOT_EMPTY)
							{
								string message = string.Format("Directory {0} is not empty", path);
								return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
							}
							if (error == MonoIOError.ERROR_FILENAME_EXCED_RANGE)
							{
								string message = string.Format("Path is too long. Path: {0}", path);
								return new PathTooLongException(message);
							}
							if (error != MonoIOError.ERROR_ENCRYPTION_FAILED)
							{
								string message = string.Format("Win32 IO returned {0}. Path: {1}", error, path);
								return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
							}
							return new IOException("Encryption failed", (int)((MonoIOError)(-2147024896) | error));
						case MonoIOError.ERROR_CANNOT_MAKE:
						{
							string message = string.Format("Path {0} is a directory", path);
							return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
						}
						}
						break;
					case MonoIOError.ERROR_NOT_SAME_DEVICE:
					{
						string message = "Source and destination are not on the same device";
						return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
					}
					}
					break;
				case MonoIOError.ERROR_SHARING_VIOLATION:
				{
					string message = string.Format("Sharing violation on path {0}", path);
					return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
				}
				case MonoIOError.ERROR_LOCK_VIOLATION:
				{
					string message = string.Format("Lock violation on path {0}", path);
					return new IOException(message, (int)((MonoIOError)(-2147024896) | error));
				}
				}
				break;
			}
		}

		// Token: 0x06000BD6 RID: 3030
		[MethodImpl(4096)]
		public static extern bool CreateDirectory(string path, out MonoIOError error);

		// Token: 0x06000BD7 RID: 3031
		[MethodImpl(4096)]
		public static extern bool RemoveDirectory(string path, out MonoIOError error);

		// Token: 0x06000BD8 RID: 3032
		[MethodImpl(4096)]
		public static extern string[] GetFileSystemEntries(string path, string path_with_pattern, int attrs, int mask, out MonoIOError error);

		// Token: 0x06000BD9 RID: 3033
		[MethodImpl(4096)]
		public static extern string GetCurrentDirectory(out MonoIOError error);

		// Token: 0x06000BDA RID: 3034
		[MethodImpl(4096)]
		public static extern bool SetCurrentDirectory(string path, out MonoIOError error);

		// Token: 0x06000BDB RID: 3035
		[MethodImpl(4096)]
		public static extern bool CopyFile(string path, string dest, bool overwrite, out MonoIOError error);

		// Token: 0x06000BDC RID: 3036
		[MethodImpl(4096)]
		public static extern bool DeleteFile(string path, out MonoIOError error);

		// Token: 0x06000BDD RID: 3037
		[MethodImpl(4096)]
		public static extern FileAttributes GetFileAttributes(string path, out MonoIOError error);

		// Token: 0x06000BDE RID: 3038
		[MethodImpl(4096)]
		public static extern MonoFileType GetFileType(IntPtr handle, out MonoIOError error);

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002E2F8 File Offset: 0x0002C4F8
		public static bool Exists(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != MonoIO.InvalidFileAttributes;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002E31C File Offset: 0x0002C51C
		public static bool ExistsFile(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != MonoIO.InvalidFileAttributes && (fileAttributes & FileAttributes.Directory) == (FileAttributes)0;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002E34C File Offset: 0x0002C54C
		public static bool ExistsDirectory(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			if (error == MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				error = MonoIOError.ERROR_PATH_NOT_FOUND;
			}
			return fileAttributes != MonoIO.InvalidFileAttributes && (fileAttributes & FileAttributes.Directory) != (FileAttributes)0;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002E388 File Offset: 0x0002C588
		public static bool ExistsSymlink(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != MonoIO.InvalidFileAttributes && (fileAttributes & FileAttributes.ReparsePoint) != (FileAttributes)0;
		}

		// Token: 0x06000BE3 RID: 3043
		[MethodImpl(4096)]
		public static extern bool GetFileStat(string path, out MonoIOStat stat, out MonoIOError error);

		// Token: 0x06000BE4 RID: 3044
		[MethodImpl(4096)]
		public static extern IntPtr Open(string filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error);

		// Token: 0x06000BE5 RID: 3045
		[MethodImpl(4096)]
		public static extern bool Close(IntPtr handle, out MonoIOError error);

		// Token: 0x06000BE6 RID: 3046
		[MethodImpl(4096)]
		public static extern int Read(IntPtr handle, byte[] dest, int dest_offset, int count, out MonoIOError error);

		// Token: 0x06000BE7 RID: 3047
		[MethodImpl(4096)]
		public static extern int Write(IntPtr handle, [In] byte[] src, int src_offset, int count, out MonoIOError error);

		// Token: 0x06000BE8 RID: 3048
		[MethodImpl(4096)]
		public static extern long Seek(IntPtr handle, long offset, SeekOrigin origin, out MonoIOError error);

		// Token: 0x06000BE9 RID: 3049
		[MethodImpl(4096)]
		public static extern long GetLength(IntPtr handle, out MonoIOError error);

		// Token: 0x06000BEA RID: 3050
		[MethodImpl(4096)]
		public static extern bool SetLength(IntPtr handle, long length, out MonoIOError error);

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000BEB RID: 3051
		public static extern IntPtr ConsoleOutput { [MethodImpl(4096)] get; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000BEC RID: 3052
		public static extern IntPtr ConsoleInput { [MethodImpl(4096)] get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000BED RID: 3053
		public static extern IntPtr ConsoleError { [MethodImpl(4096)] get; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000BEE RID: 3054
		public static extern char VolumeSeparatorChar { [MethodImpl(4096)] get; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000BEF RID: 3055
		public static extern char DirectorySeparatorChar { [MethodImpl(4096)] get; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000BF0 RID: 3056
		public static extern char AltDirectorySeparatorChar { [MethodImpl(4096)] get; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000BF1 RID: 3057
		public static extern char PathSeparator { [MethodImpl(4096)] get; }

		// Token: 0x040004D6 RID: 1238
		public static readonly FileAttributes InvalidFileAttributes = (FileAttributes)(-1);

		// Token: 0x040004D7 RID: 1239
		public static readonly IntPtr InvalidHandle = (IntPtr)(-1L);
	}
}
