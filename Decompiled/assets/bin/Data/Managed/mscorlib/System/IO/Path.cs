using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000133 RID: 307
	[ComVisible(true)]
	public static class Path
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x0002E3F8 File Offset: 0x0002C5F8
		static Path()
		{
			Path.DirectorySeparatorChar = MonoIO.DirectorySeparatorChar;
			Path.AltDirectorySeparatorChar = MonoIO.AltDirectorySeparatorChar;
			Path.PathSeparator = MonoIO.PathSeparator;
			Path.InvalidPathChars = Path.GetInvalidPathChars();
			Path.DirectorySeparatorStr = Path.DirectorySeparatorChar.ToString();
			Path.PathSeparatorChars = new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar,
				Path.VolumeSeparatorChar
			};
			Path.dirEqualsVolume = (Path.DirectorySeparatorChar == Path.VolumeSeparatorChar);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002E47C File Offset: 0x0002C67C
		public static string Combine(string path1, string path2)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path1.Length == 0)
			{
				return path2;
			}
			if (path2.Length == 0)
			{
				return path1;
			}
			if (path1.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (path2.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (Path.IsPathRooted(path2))
			{
				return path2;
			}
			char c = path1[path1.Length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorStr + path2;
			}
			return path1 + path2;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002E550 File Offset: 0x0002C750
		internal static string CleanPath(string s)
		{
			int length = s.Length;
			int num = 0;
			int num2 = 0;
			char c = s[0];
			if (length > 2 && c == '\\' && s[1] == '\\')
			{
				num2 = 2;
			}
			if (length == 1 && (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
			{
				return s;
			}
			for (int i = num2; i < length; i++)
			{
				char c2 = s[i];
				if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
				{
					if (i + 1 == length)
					{
						num++;
					}
					else
					{
						c2 = s[i + 1];
						if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
						{
							num++;
						}
					}
				}
			}
			if (num == 0)
			{
				return s;
			}
			char[] array = new char[length - num];
			if (num2 != 0)
			{
				array[0] = '\\';
				array[1] = '\\';
			}
			int j = num2;
			int num3 = num2;
			while (j < length && num3 < array.Length)
			{
				char c3 = s[j];
				if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
				{
					array[num3++] = c3;
				}
				else if (num3 + 1 != array.Length)
				{
					array[num3++] = Path.DirectorySeparatorChar;
					while (j < length - 1)
					{
						c3 = s[j + 1];
						if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
						{
							break;
						}
						j++;
					}
				}
				j++;
			}
			return new string(array);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002E704 File Offset: 0x0002C904
		public static string GetDirectoryName(string path)
		{
			if (path == string.Empty)
			{
				throw new ArgumentException("Invalid path");
			}
			if (path == null || Path.GetPathRoot(path) == path)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Argument string consists of whitespace characters only.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) > -1)
			{
				throw new ArgumentException("Path contains invalid characters");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num == 0)
			{
				num++;
			}
			if (num <= 0)
			{
				return string.Empty;
			}
			string text = path.Substring(0, num);
			int length = text.Length;
			if (length >= 2 && Path.DirectorySeparatorChar == '\\' && text[length - 1] == Path.VolumeSeparatorChar)
			{
				return text + Path.DirectorySeparatorChar;
			}
			return Path.CleanPath(text);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002E7EC File Offset: 0x0002C9EC
		public static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (num > -1 && num < path.Length - 1)
			{
				return path.Substring(num);
			}
			return string.Empty;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002E848 File Offset: 0x0002CA48
		public static string GetFileName(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num >= 0)
			{
				return path.Substring(num + 1);
			}
			return path;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
		public static string GetFullPath(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002E8BC File Offset: 0x0002CABC
		internal static string WindowsDriveAdjustment(string path)
		{
			if (path.Length < 2)
			{
				return path;
			}
			if (path[1] != ':' || !char.IsLetter(path[0]))
			{
				return path;
			}
			string currentDirectory = Directory.GetCurrentDirectory();
			if (path.Length == 2)
			{
				if (currentDirectory[0] == path[0])
				{
					path = currentDirectory;
				}
				else
				{
					path += '\\';
				}
			}
			else if (path[2] != Path.DirectorySeparatorChar && path[2] != Path.AltDirectorySeparatorChar)
			{
				if (currentDirectory[0] == path[0])
				{
					path = Path.Combine(currentDirectory, path.Substring(2, path.Length - 2));
				}
				else
				{
					path = path.Substring(0, 2) + Path.DirectorySeparatorStr + path.Substring(2, path.Length - 2);
				}
			}
			return path;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		internal static string InsecureGetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				string text = Locale.GetText("The specified path is not of a legal form (empty).");
				throw new ArgumentException(text);
			}
			if (Environment.IsRunningOnWindows)
			{
				path = Path.WindowsDriveAdjustment(path);
			}
			char c = path[path.Length - 1];
			if (path.Length >= 2 && Path.IsDsc(path[0]) && Path.IsDsc(path[1]))
			{
				if (path.Length == 2 || path.IndexOf(path[0], 2) < 0)
				{
					throw new ArgumentException("UNC paths should be of the form \\\\server\\share.");
				}
				if (path[0] != Path.DirectorySeparatorChar)
				{
					path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
				path = Path.CanonicalizePath(path);
			}
			else
			{
				if (!Path.IsPathRooted(path))
				{
					path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorStr + path;
				}
				else if (Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && Path.IsDsc(path[0]) && !Path.IsDsc(path[1]))
				{
					string currentDirectory = Directory.GetCurrentDirectory();
					if (currentDirectory[1] == Path.VolumeSeparatorChar)
					{
						path = currentDirectory.Substring(0, 2) + path;
					}
					else
					{
						path = currentDirectory.Substring(0, currentDirectory.IndexOf('\\', currentDirectory.IndexOf("\\\\") + 1));
					}
				}
				path = Path.CanonicalizePath(path);
			}
			if (Path.IsDsc(c) && path[path.Length - 1] != Path.DirectorySeparatorChar)
			{
				path += Path.DirectorySeparatorChar;
			}
			return path;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002EB80 File Offset: 0x0002CD80
		private static bool IsDsc(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002EB98 File Offset: 0x0002CD98
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("The specified path is not of a legal form.");
			}
			if (!Path.IsPathRooted(path))
			{
				return string.Empty;
			}
			if (Path.DirectorySeparatorChar == '/')
			{
				return (!Path.IsDsc(path[0])) ? string.Empty : Path.DirectorySeparatorStr;
			}
			int num = 2;
			if (path.Length == 1 && Path.IsDsc(path[0]))
			{
				return Path.DirectorySeparatorStr;
			}
			if (path.Length < 2)
			{
				return string.Empty;
			}
			if (Path.IsDsc(path[0]) && Path.IsDsc(path[1]))
			{
				while (num < path.Length && !Path.IsDsc(path[num]))
				{
					num++;
				}
				if (num < path.Length)
				{
					num++;
					while (num < path.Length && !Path.IsDsc(path[num]))
					{
						num++;
					}
				}
				return Path.DirectorySeparatorStr + Path.DirectorySeparatorStr + path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}
			if (Path.IsDsc(path[0]))
			{
				return Path.DirectorySeparatorStr;
			}
			if (path[1] == Path.VolumeSeparatorChar)
			{
				if (path.Length >= 3 && Path.IsDsc(path[2]))
				{
					num++;
				}
				return path.Substring(0, num);
			}
			return Directory.GetCurrentDirectory().Substring(0, 2);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002ED48 File Offset: 0x0002CF48
		public static bool IsPathRooted(string path)
		{
			if (path == null || path.Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			char c = path[0];
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || (!Path.dirEqualsVolume && path.Length > 1 && path[1] == Path.VolumeSeparatorChar);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002EDCC File Offset: 0x0002CFCC
		public static char[] GetInvalidFileNameChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'\0',
					'\u0001',
					'\u0002',
					'\u0003',
					'\u0004',
					'\u0005',
					'\u0006',
					'\a',
					'\b',
					'\t',
					'\n',
					'\v',
					'\f',
					'\r',
					'\u000e',
					'\u000f',
					'\u0010',
					'\u0011',
					'\u0012',
					'\u0013',
					'\u0014',
					'\u0015',
					'\u0016',
					'\u0017',
					'\u0018',
					'\u0019',
					'\u001a',
					'\u001b',
					'\u001c',
					'\u001d',
					'\u001e',
					'\u001f',
					'"',
					'<',
					'>',
					'|',
					':',
					'*',
					'?',
					'\\',
					'/'
				};
			}
			return new char[]
			{
				'\0',
				'/'
			};
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002EDF8 File Offset: 0x0002CFF8
		public static char[] GetInvalidPathChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'"',
					'<',
					'>',
					'|',
					'\0',
					'\u0001',
					'\u0002',
					'\u0003',
					'\u0004',
					'\u0005',
					'\u0006',
					'\a',
					'\b',
					'\t',
					'\n',
					'\v',
					'\f',
					'\r',
					'\u000e',
					'\u000f',
					'\u0010',
					'\u0011',
					'\u0012',
					'\u0013',
					'\u0014',
					'\u0015',
					'\u0016',
					'\u0017',
					'\u0018',
					'\u0019',
					'\u001a',
					'\u001b',
					'\u001c',
					'\u001d',
					'\u001e',
					'\u001f'
				};
			}
			return new char[1];
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002EE20 File Offset: 0x0002D020
		private static int findExtension(string path)
		{
			if (path != null)
			{
				int num = path.LastIndexOf('.');
				int num2 = path.LastIndexOfAny(Path.PathSeparatorChars);
				if (num > num2)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002EE54 File Offset: 0x0002D054
		private static string GetServerAndShare(string path)
		{
			int num = 2;
			while (num < path.Length && !Path.IsDsc(path[num]))
			{
				num++;
			}
			if (num < path.Length)
			{
				num++;
				while (num < path.Length && !Path.IsDsc(path[num]))
				{
					num++;
				}
			}
			return path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		private static bool SameRoot(string root, string path)
		{
			if (root.Length < 2 || path.Length < 2)
			{
				return false;
			}
			if (!Path.IsDsc(root[0]) || !Path.IsDsc(root[1]))
			{
				return root[0].Equals(path[0]) && path[1] == Path.VolumeSeparatorChar && (root.Length <= 2 || path.Length <= 2 || (Path.IsDsc(root[2]) && Path.IsDsc(path[2])));
			}
			if (!Path.IsDsc(path[0]) || !Path.IsDsc(path[1]))
			{
				return false;
			}
			string serverAndShare = Path.GetServerAndShare(root);
			string serverAndShare2 = Path.GetServerAndShare(path);
			return string.Compare(serverAndShare, serverAndShare2, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002EFD0 File Offset: 0x0002D1D0
		private static string CanonicalizePath(string path)
		{
			if (path == null)
			{
				return path;
			}
			if (Environment.IsRunningOnWindows)
			{
				path = path.Trim();
			}
			if (path.Length == 0)
			{
				return path;
			}
			string pathRoot = Path.GetPathRoot(path);
			string[] array = path.Split(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
			int num = 0;
			bool flag = Environment.IsRunningOnWindows && pathRoot.Length > 2 && Path.IsDsc(pathRoot[0]) && Path.IsDsc(pathRoot[1]);
			int num2 = (!flag) ? 0 : 3;
			for (int i = 0; i < array.Length; i++)
			{
				if (Environment.IsRunningOnWindows)
				{
					array[i] = array[i].TrimEnd(new char[0]);
				}
				if (!(array[i] == ".") && (i == 0 || array[i].Length != 0))
				{
					if (array[i] == "..")
					{
						if (num > num2)
						{
							num--;
						}
					}
					else
					{
						array[num++] = array[i];
					}
				}
			}
			if (num == 0 || (num == 1 && array[0] == string.Empty))
			{
				return pathRoot;
			}
			string text = string.Join(Path.DirectorySeparatorStr, array, 0, num);
			if (!Environment.IsRunningOnWindows)
			{
				return text;
			}
			if (flag)
			{
				text = Path.DirectorySeparatorStr + text;
			}
			if (!Path.SameRoot(pathRoot, text))
			{
				text = pathRoot + text;
			}
			if (flag)
			{
				return text;
			}
			if (!Path.IsDsc(path[0]) && Path.SameRoot(pathRoot, path))
			{
				if (text.Length <= 2 && !text.EndsWith(Path.DirectorySeparatorStr))
				{
					text += Path.DirectorySeparatorChar;
				}
				return text;
			}
			string currentDirectory = Directory.GetCurrentDirectory();
			if (currentDirectory.Length > 1 && currentDirectory[1] == Path.VolumeSeparatorChar)
			{
				if (text.Length == 0 || Path.IsDsc(text[0]))
				{
					text += '\\';
				}
				return currentDirectory.Substring(0, 2) + text;
			}
			if (Path.IsDsc(currentDirectory[currentDirectory.Length - 1]) && Path.IsDsc(text[0]))
			{
				return currentDirectory + text.Substring(1);
			}
			return currentDirectory + text;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002F270 File Offset: 0x0002D470
		internal static bool IsPathSubsetOf(string subset, string path)
		{
			if (subset.Length > path.Length)
			{
				return false;
			}
			int num = subset.LastIndexOfAny(Path.PathSeparatorChars);
			if (string.Compare(subset, 0, path, 0, num) != 0)
			{
				return false;
			}
			num++;
			int num2 = path.IndexOfAny(Path.PathSeparatorChars, num);
			if (num2 >= num)
			{
				return string.Compare(subset, num, path, num, path.Length - num2) == 0;
			}
			return subset.Length == path.Length && string.Compare(subset, num, path, num, subset.Length - num) == 0;
		}

		// Token: 0x040004F7 RID: 1271
		[Obsolete("see GetInvalidPathChars and GetInvalidFileNameChars methods.")]
		public static readonly char[] InvalidPathChars;

		// Token: 0x040004F8 RID: 1272
		public static readonly char AltDirectorySeparatorChar;

		// Token: 0x040004F9 RID: 1273
		public static readonly char DirectorySeparatorChar;

		// Token: 0x040004FA RID: 1274
		public static readonly char PathSeparator;

		// Token: 0x040004FB RID: 1275
		internal static readonly string DirectorySeparatorStr;

		// Token: 0x040004FC RID: 1276
		public static readonly char VolumeSeparatorChar = MonoIO.VolumeSeparatorChar;

		// Token: 0x040004FD RID: 1277
		internal static readonly char[] PathSeparatorChars;

		// Token: 0x040004FE RID: 1278
		private static readonly bool dirEqualsVolume;
	}
}
