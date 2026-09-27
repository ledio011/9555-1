using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200011B RID: 283
	[ComVisible(true)]
	public static class Directory
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x0002B674 File Offset: 0x00029874
		public static DirectoryInfo CreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Path contains invalid chars");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Only blank characters in path");
			}
			if (File.Exists(path))
			{
				throw new IOException("Cannot create " + path + " because a file with the same name already exists.");
			}
			if (path == ":")
			{
				throw new ArgumentException("Only ':' In path");
			}
			return Directory.CreateDirectoriesInternal(path);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002B724 File Offset: 0x00029924
		private static DirectoryInfo CreateDirectoriesInternal(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path, true);
			if (directoryInfo.Parent != null && !directoryInfo.Parent.Exists)
			{
				directoryInfo.Parent.Create();
			}
			MonoIOError monoIOError;
			if (!MonoIO.CreateDirectory(path, out monoIOError) && monoIOError != MonoIOError.ERROR_ALREADY_EXISTS && monoIOError != MonoIOError.ERROR_FILE_EXISTS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
			return directoryInfo;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002B788 File Offset: 0x00029988
		public static void Delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Path contains invalid chars");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Only blank characters in path");
			}
			if (path == ":")
			{
				throw new NotSupportedException("Only ':' In path");
			}
			MonoIOError monoIOError;
			bool flag;
			if (MonoIO.ExistsSymlink(path, out monoIOError))
			{
				flag = MonoIO.DeleteFile(path, out monoIOError);
			}
			else
			{
				flag = MonoIO.RemoveDirectory(path, out monoIOError);
			}
			if (flag)
			{
				return;
			}
			if (monoIOError != MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
			if (File.Exists(path))
			{
				throw new IOException("Directory does not exist, but a file of the same name exist.");
			}
			throw new DirectoryNotFoundException("Directory does not exist.");
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002B868 File Offset: 0x00029A68
		public static bool Exists(string path)
		{
			MonoIOError monoIOError;
			return path != null && MonoIO.ExistsDirectory(path, out monoIOError);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002B888 File Offset: 0x00029A88
		public static string GetCurrentDirectory()
		{
			MonoIOError monoIOError;
			string currentDirectory = MonoIO.GetCurrentDirectory(out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			return currentDirectory;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002B8AC File Offset: 0x00029AAC
		public static string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path, "*");
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public static string[] GetDirectories(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, FileAttributes.Directory, FileAttributes.Directory);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002B8CC File Offset: 0x00029ACC
		public static string[] GetFiles(string path)
		{
			return Directory.GetFiles(path, "*");
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002B8DC File Offset: 0x00029ADC
		public static string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, FileAttributes.Directory, (FileAttributes)0);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		public static void SetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("path string must not be an empty string or whitespace string");
			}
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException("Directory \"" + path + "\" not found.");
			}
			MonoIOError monoIOError;
			MonoIO.SetCurrentDirectory(path, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002B95C File Offset: 0x00029B5C
		private static string[] GetFileSystemEntries(string path, string searchPattern, FileAttributes mask, FileAttributes attrs)
		{
			if (path == null || searchPattern == null)
			{
				throw new ArgumentNullException();
			}
			if (searchPattern.Length == 0)
			{
				return new string[0];
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("The Path does not have a valid format");
			}
			string path2 = Path.Combine(path, searchPattern);
			string directoryName = Path.GetDirectoryName(path2);
			if (directoryName.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Path contains invalid characters");
			}
			MonoIOError monoIOError;
			if (directoryName.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				if (path.IndexOfAny(SearchPattern.InvalidChars) == -1)
				{
					throw new ArgumentException("Path contains invalid characters", "path");
				}
				throw new ArgumentException("Pattern contains invalid characters", "pattern");
			}
			else if (!MonoIO.ExistsDirectory(directoryName, out monoIOError))
			{
				MonoIOError monoIOError2;
				if (monoIOError == MonoIOError.ERROR_SUCCESS && MonoIO.ExistsFile(directoryName, out monoIOError2))
				{
					return new string[]
					{
						directoryName
					};
				}
				if (monoIOError != MonoIOError.ERROR_PATH_NOT_FOUND)
				{
					throw MonoIO.GetException(directoryName, monoIOError);
				}
				if (directoryName.IndexOfAny(SearchPattern.WildcardChars) == -1)
				{
					throw new DirectoryNotFoundException("Directory '" + directoryName + "' not found.");
				}
				if (path.IndexOfAny(SearchPattern.WildcardChars) == -1)
				{
					throw new ArgumentException("Pattern is invalid", "searchPattern");
				}
				throw new ArgumentException("Path is invalid", "path");
			}
			else
			{
				string path_with_pattern = Path.Combine(directoryName, searchPattern);
				string[] fileSystemEntries = MonoIO.GetFileSystemEntries(path, path_with_pattern, (int)attrs, (int)mask, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(directoryName, monoIOError);
				}
				return fileSystemEntries;
			}
		}
	}
}
