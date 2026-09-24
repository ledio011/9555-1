using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200011F RID: 287
	[ComVisible(true)]
	public static class File
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x0002BD14 File Offset: 0x00029F14
		public static void Copy(string sourceFileName, string destFileName)
		{
			File.Copy(sourceFileName, destFileName, false);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002BD20 File Offset: 0x00029F20
		public static void Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (sourceFileName.Trim().Length == 0 || sourceFileName.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("The file name is not valid.");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destFileName");
			}
			if (destFileName.Trim().Length == 0 || destFileName.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("The file name is not valid.");
			}
			MonoIOError error;
			if (!MonoIO.Exists(sourceFileName, out error))
			{
				throw new FileNotFoundException(Locale.GetText("{0} does not exist", new object[]
				{
					sourceFileName
				}), sourceFileName);
			}
			if ((File.GetAttributes(sourceFileName) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				throw new ArgumentException(Locale.GetText("{0} is a directory", new object[]
				{
					sourceFileName
				}));
			}
			if (MonoIO.Exists(destFileName, out error))
			{
				if ((File.GetAttributes(destFileName) & FileAttributes.Directory) == FileAttributes.Directory)
				{
					throw new ArgumentException(Locale.GetText("{0} is a directory", new object[]
					{
						destFileName
					}));
				}
				if (!overwrite)
				{
					throw new IOException(Locale.GetText("{0} already exists", new object[]
					{
						destFileName
					}));
				}
			}
			string directoryName = Path.GetDirectoryName(destFileName);
			if (directoryName != string.Empty && !Directory.Exists(directoryName))
			{
				throw new DirectoryNotFoundException(Locale.GetText("Destination directory not found: {0}", new object[]
				{
					directoryName
				}));
			}
			if (!MonoIO.CopyFile(sourceFileName, destFileName, overwrite, out error))
			{
				string text = Locale.GetText("{0}\" or \"{1}", new object[]
				{
					sourceFileName,
					destFileName
				});
				throw MonoIO.GetException(text, error);
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		public static FileStream Create(string path)
		{
			return File.Create(path, 8192);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002BF04 File Offset: 0x0002A104
		public static FileStream Create(string path, int bufferSize)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002BF10 File Offset: 0x0002A110
		public static void Delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0 || path.IndexOfAny(Path.InvalidPathChars) >= 0)
			{
				throw new ArgumentException("path");
			}
			if (Directory.Exists(path))
			{
				throw new UnauthorizedAccessException(Locale.GetText("{0} is a directory", new object[]
				{
					path
				}));
			}
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName != string.Empty && !Directory.Exists(directoryName))
			{
				throw new DirectoryNotFoundException(Locale.GetText("Could not find a part of the path \"{0}\".", new object[]
				{
					path
				}));
			}
			MonoIOError monoIOError;
			if (!MonoIO.DeleteFile(path, out monoIOError) && monoIOError != MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		public static bool Exists(string path)
		{
			MonoIOError monoIOError;
			return path != null && path.Trim().Length != 0 && path.IndexOfAny(Path.InvalidPathChars) < 0 && MonoIO.ExistsFile(path, out monoIOError);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002C018 File Offset: 0x0002A218
		public static FileAttributes GetAttributes(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Path is empty"));
			}
			if (path.IndexOfAny(Path.InvalidPathChars) >= 0)
			{
				throw new ArgumentException(Locale.GetText("Path contains invalid chars"));
			}
			MonoIOError monoIOError;
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
			return fileAttributes;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002C090 File Offset: 0x0002A290
		public static FileStream OpenRead(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002C09C File Offset: 0x0002A29C
		public static StreamReader OpenText(string path)
		{
			return new StreamReader(path);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
		public static byte[] ReadAllBytes(string path)
		{
			byte[] result;
			using (FileStream fileStream = File.OpenRead(path))
			{
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException("Reading more than 2GB with this call is not supported");
				}
				int num = 0;
				int i = (int)length;
				byte[] array = new byte[length];
				while (i > 0)
				{
					int num2 = fileStream.Read(array, num, i);
					if (num2 == 0)
					{
						throw new IOException("Unexpected end of stream");
					}
					num += num2;
					i -= num2;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002C148 File Offset: 0x0002A348
		public static string ReadAllText(string path)
		{
			return File.ReadAllText(path, Encoding.UTF8Unmarked);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002C158 File Offset: 0x0002A358
		public static string ReadAllText(string path, Encoding encoding)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x04000478 RID: 1144
		private static DateTime? defaultLocalFileTime;
	}
}
