using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Security
{
	// Token: 0x02000046 RID: 70
	internal class Uri
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000A1CC File Offset: 0x000083CC
		public Uri(string uriString) : this(uriString, false)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public Uri(string uriString, bool dontEscape)
		{
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.Parse();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public string AbsolutePath
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public bool IsFile
		{
			get
			{
				return this.scheme == Uri.UriSchemeFile;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public bool IsUnc
		{
			get
			{
				return this.isUnc;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000A3DC File Offset: 0x000085DC
		public string LocalPath
		{
			get
			{
				if (this.cachedLocalPath != null)
				{
					return this.cachedLocalPath;
				}
				if (!this.IsFile)
				{
					return this.AbsolutePath;
				}
				bool flag = this.path.Length > 3 && this.path[1] == ':' && (this.path[2] == '\\' || this.path[2] == '/');
				if (!this.IsUnc)
				{
					string text = this.Unescape(this.path);
					if (Path.DirectorySeparatorChar == '\\' || flag)
					{
						this.cachedLocalPath = text.Replace('/', '\\');
					}
					else
					{
						this.cachedLocalPath = text;
					}
				}
				else if (this.path.Length > 1 && this.path[1] == ':')
				{
					this.cachedLocalPath = this.Unescape(this.path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
				}
				else if (Path.DirectorySeparatorChar == '\\')
				{
					this.cachedLocalPath = "\\\\" + this.Unescape(this.host + this.path.Replace('/', '\\'));
				}
				else
				{
					this.cachedLocalPath = this.Unescape(this.path);
				}
				if (this.cachedLocalPath == string.Empty)
				{
					this.cachedLocalPath = Path.DirectorySeparatorChar.ToString();
				}
				return this.cachedLocalPath;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A574 File Offset: 0x00008774
		public override bool Equals(object comparant)
		{
			if (comparant == null)
			{
				return false;
			}
			Uri uri = comparant as Uri;
			if (uri == null)
			{
				string text = comparant as string;
				if (text == null)
				{
					return false;
				}
				uri = new Uri(text);
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			return this.scheme.ToLower(invariantCulture) == uri.scheme.ToLower(invariantCulture) && this.userinfo.ToLower(invariantCulture) == uri.userinfo.ToLower(invariantCulture) && this.host.ToLower(invariantCulture) == uri.host.ToLower(invariantCulture) && this.port == uri.port && this.path == uri.path && this.query.ToLower(invariantCulture) == uri.query.ToLower(invariantCulture);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A660 File Offset: 0x00008860
		public override int GetHashCode()
		{
			if (this.cachedHashCode == 0)
			{
				this.cachedHashCode = this.scheme.GetHashCode() + this.userinfo.GetHashCode() + this.host.GetHashCode() + this.port + this.path.GetHashCode() + this.query.GetHashCode();
			}
			return this.cachedHashCode;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public string GetLeftPart(UriPartial part)
		{
			switch (part)
			{
			case UriPartial.Scheme:
				return this.scheme + this.GetOpaqueWiseSchemeDelimiter();
			case UriPartial.Authority:
			{
				if (this.host == string.Empty || this.scheme == Uri.UriSchemeMailto || this.scheme == Uri.UriSchemeNews)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.scheme);
				stringBuilder.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder.Append(this.userinfo).Append('@');
				}
				stringBuilder.Append(this.host);
				int defaultPort = Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder.Append(':').Append(this.port);
				}
				return stringBuilder.ToString();
			}
			case UriPartial.Path:
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(this.scheme);
				stringBuilder2.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder2.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder2.Append(this.userinfo).Append('@');
				}
				stringBuilder2.Append(this.host);
				int defaultPort = Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder2.Append(':').Append(this.port);
				}
				stringBuilder2.Append(this.path);
				return stringBuilder2.ToString();
			}
			default:
				return null;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public static int FromHex(char digit)
		{
			if ('0' <= digit && digit <= '9')
			{
				return (int)(digit - '0');
			}
			if ('a' <= digit && digit <= 'f')
			{
				return (int)(digit - 'a' + '\n');
			}
			if ('A' <= digit && digit <= 'F')
			{
				return (int)(digit - 'A' + '\n');
			}
			throw new ArgumentException("digit");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A958 File Offset: 0x00008B58
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			return "%" + Uri.hexUpperChars[(int)((character & 'ð') >> 4)] + Uri.hexUpperChars[(int)(character & '\u000f')];
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		public static char HexUnescape(string pattern, ref int index)
		{
			if (pattern == null)
			{
				throw new ArgumentException("pattern");
			}
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = 0;
			int num2 = 0;
			while (index + 3 <= pattern.Length && pattern[index] == '%' && Uri.IsHexDigit(pattern[index + 1]) && Uri.IsHexDigit(pattern[index + 2]))
			{
				index++;
				int num3 = Uri.FromHex(pattern[index++]);
				int num4 = Uri.FromHex(pattern[index++]);
				int num5 = (num3 << 4) + num4;
				if (num == 0)
				{
					if (num5 < 192)
					{
						return (char)num5;
					}
					if (num5 < 224)
					{
						num2 = num5 - 192;
						num = 2;
					}
					else if (num5 < 240)
					{
						num2 = num5 - 224;
						num = 3;
					}
					else if (num5 < 248)
					{
						num2 = num5 - 240;
						num = 4;
					}
					else if (num5 < 251)
					{
						num2 = num5 - 248;
						num = 5;
					}
					else if (num5 < 254)
					{
						num2 = num5 - 252;
						num = 6;
					}
					num2 <<= (num - 1) * 6;
				}
				else
				{
					num2 += num5 - 128 << (num - 1) * 6;
				}
				num--;
				if (num <= 0)
				{
					IL_1A2:
					return (char)num2;
				}
			}
			if (num == 0)
			{
				return pattern[index++];
			}
			goto IL_1A2;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000AB64 File Offset: 0x00008D64
		public static bool IsHexDigit(char digit)
		{
			return ('0' <= digit && digit <= '9') || ('a' <= digit && digit <= 'f') || ('A' <= digit && digit <= 'F');
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public static bool IsHexEncoding(string pattern, int index)
		{
			return index + 3 <= pattern.Length && (pattern[index++] == '%' && Uri.IsHexDigit(pattern[index++])) && Uri.IsHexDigit(pattern[index]);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		public override string ToString()
		{
			if (this.cachedToString != null)
			{
				return this.cachedToString;
			}
			string str = (!this.query.StartsWith("?")) ? this.Unescape(this.query) : ('?' + this.Unescape(this.query.Substring(1)));
			this.cachedToString = this.Unescape(this.GetLeftPart(UriPartial.Path), true) + str + this.fragment;
			return this.cachedToString;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000AC80 File Offset: 0x00008E80
		protected static string EscapeString(string str)
		{
			return Uri.EscapeString(str, false, true, true);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000AC8C File Offset: 0x00008E8C
		internal static string EscapeString(string str, bool escapeReserved, bool escapeHex, bool escapeBrackets)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				if (Uri.IsHexEncoding(str, i))
				{
					stringBuilder.Append(str.Substring(i, 3));
					i += 2;
				}
				else
				{
					byte[] bytes = Encoding.UTF8.GetBytes(new char[]
					{
						str[i]
					});
					int num = bytes.Length;
					for (int j = 0; j < num; j++)
					{
						char c = (char)bytes[j];
						if (c <= ' ' || c >= '\u007f' || "<>%\"{}|\\^`".IndexOf(c) != -1 || (escapeHex && c == '#') || (escapeBrackets && (c == '[' || c == ']')) || (escapeReserved && ";/?:@&=+$,".IndexOf(c) != -1))
						{
							stringBuilder.Append(Uri.HexEscape(c));
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000ADAC File Offset: 0x00008FAC
		protected void Parse()
		{
			this.Parse(this.source);
			if (this.userEscaped)
			{
				return;
			}
			this.host = Uri.EscapeString(this.host, false, true, false);
			this.path = Uri.EscapeString(this.path);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000ADEC File Offset: 0x00008FEC
		protected string Unescape(string str)
		{
			return this.Unescape(str, false);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		internal string Unescape(string str, bool excludeSharp)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c == '%')
				{
					char c2 = Uri.HexUnescape(str, ref i);
					if (excludeSharp && c2 == '#')
					{
						stringBuilder.Append("%23");
					}
					else
					{
						stringBuilder.Append(c2);
					}
					i--;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000AE8C File Offset: 0x0000908C
		private void ParseAsWindowsUNC(string uriString)
		{
			this.scheme = Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.isUnc = true;
			uriString = uriString.TrimStart(new char[]
			{
				'\\'
			});
			int num = uriString.IndexOf('\\');
			if (num > 0)
			{
				this.path = uriString.Substring(num);
				this.host = uriString.Substring(0, num);
			}
			else
			{
				this.host = uriString;
				this.path = string.Empty;
			}
			this.path = this.path.Replace("\\", "/");
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000AF38 File Offset: 0x00009138
		private void ParseAsWindowsAbsoluteFilePath(string uriString)
		{
			if (uriString.Length > 2 && uriString[2] != '\\' && uriString[2] != '/')
			{
				throw new FormatException("Relative file path is not allowed.");
			}
			this.scheme = Uri.UriSchemeFile;
			this.host = string.Empty;
			this.port = -1;
			this.path = uriString.Replace("\\", "/");
			this.fragment = string.Empty;
			this.query = string.Empty;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000AFC4 File Offset: 0x000091C4
		private void ParseAsUnixAbsoluteFilePath(string uriString)
		{
			this.isUnixFilePath = true;
			this.scheme = Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.host = string.Empty;
			this.path = null;
			if (uriString.StartsWith("//"))
			{
				uriString = uriString.TrimStart(new char[]
				{
					'/'
				});
				this.path = '/' + uriString;
			}
			if (this.path == null)
			{
				this.path = uriString;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000B05C File Offset: 0x0000925C
		private void Parse(string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			int length = uriString.Length;
			if (length <= 1)
			{
				throw new FormatException();
			}
			int num = uriString.IndexOf(':');
			if (num < 0)
			{
				if (uriString[0] == '/')
				{
					this.ParseAsUnixAbsoluteFilePath(uriString);
				}
				else
				{
					if (!uriString.StartsWith("\\\\"))
					{
						throw new FormatException("URI scheme was not recognized, nor input string is not recognized as an absolute file path.");
					}
					this.ParseAsWindowsUNC(uriString);
				}
				return;
			}
			if (num == 1)
			{
				if (!char.IsLetter(uriString[0]))
				{
					throw new FormatException("URI scheme must start with alphabet character.");
				}
				this.ParseAsWindowsAbsoluteFilePath(uriString);
				return;
			}
			else
			{
				this.scheme = uriString.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
				if (!char.IsLetter(this.scheme[0]))
				{
					throw new FormatException("URI scheme must start with alphabet character.");
				}
				for (int i = 1; i < this.scheme.Length; i++)
				{
					if (!char.IsLetterOrDigit(this.scheme, i))
					{
						switch (this.scheme[i])
						{
						case '+':
						case '-':
						case '.':
							goto IL_132;
						}
						throw new FormatException("URI scheme must consist of one of alphabet, digits, '+', '-' or '.' character.");
					}
					IL_132:;
				}
				uriString = uriString.Substring(num + 1);
				num = uriString.IndexOf('#');
				if (!this.IsUnc && num != -1)
				{
					this.fragment = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
				}
				num = uriString.IndexOf('?');
				if (num != -1)
				{
					this.query = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
					if (!this.userEscaped)
					{
						this.query = Uri.EscapeString(this.query);
					}
				}
				bool flag = this.scheme == Uri.UriSchemeFile && uriString.StartsWith("///");
				if (uriString.StartsWith("//"))
				{
					if (uriString.StartsWith("////"))
					{
						flag = false;
					}
					uriString = uriString.TrimStart(new char[]
					{
						'/'
					});
					if (uriString.Length > 1 && uriString[1] == ':')
					{
						flag = false;
					}
				}
				else if (!Uri.IsPredefinedScheme(this.scheme))
				{
					this.path = uriString;
					this.isOpaquePart = true;
					return;
				}
				num = uriString.IndexOfAny(new char[]
				{
					'/'
				});
				if (flag)
				{
					num = -1;
				}
				if (num == -1)
				{
					if (this.scheme != Uri.UriSchemeMailto && this.scheme != Uri.UriSchemeNews && this.scheme != Uri.UriSchemeFile)
					{
						this.path = "/";
					}
				}
				else
				{
					this.path = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
				}
				num = uriString.IndexOf("@");
				if (num != -1)
				{
					this.userinfo = uriString.Substring(0, num);
					uriString = uriString.Remove(0, num + 1);
				}
				this.port = -1;
				num = uriString.LastIndexOf(":");
				if (flag)
				{
					num = -1;
				}
				if (num != -1 && num != uriString.Length - 1)
				{
					string text = uriString.Remove(0, num + 1);
					if (text.Length > 1 && text[text.Length - 1] != ']')
					{
						try
						{
							this.port = (int)uint.Parse(text, CultureInfo.InvariantCulture);
							uriString = uriString.Substring(0, num);
						}
						catch (Exception)
						{
							throw new FormatException("Invalid URI: invalid port number");
						}
					}
				}
				if (this.port == -1)
				{
					this.port = Uri.GetDefaultPort(this.scheme);
				}
				this.host = uriString;
				if (flag)
				{
					this.path = '/' + uriString;
					this.host = string.Empty;
				}
				else if (this.host.Length == 2 && this.host[1] == ':')
				{
					this.path = this.host + this.path;
					this.host = string.Empty;
				}
				else if (this.isUnixFilePath)
				{
					uriString = "//" + uriString;
					this.host = string.Empty;
				}
				else
				{
					if (this.host.Length == 0)
					{
						throw new FormatException("Invalid URI: The hostname could not be parsed");
					}
					if (this.scheme == Uri.UriSchemeFile)
					{
						this.isUnc = true;
					}
				}
				if (this.scheme != Uri.UriSchemeMailto && this.scheme != Uri.UriSchemeNews && this.scheme != Uri.UriSchemeFile && this.reduce)
				{
					this.path = Uri.Reduce(this.path);
				}
				return;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000B574 File Offset: 0x00009774
		private static string Reduce(string path)
		{
			path = path.Replace('\\', '/');
			string[] array = path.Split(new char[]
			{
				'/'
			});
			ArrayList arrayList = new ArrayList();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				if (text.Length != 0 && !(text == "."))
				{
					if (text == "..")
					{
						if (arrayList.Count == 0)
						{
							if (i != 1)
							{
								throw new Exception("Invalid path.");
							}
						}
						else
						{
							arrayList.RemoveAt(arrayList.Count - 1);
						}
					}
					else
					{
						arrayList.Add(text);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return "/";
			}
			arrayList.Insert(0, string.Empty);
			string text2 = string.Join("/", (string[])arrayList.ToArray(typeof(string)));
			if (path.EndsWith("/"))
			{
				text2 += '/';
			}
			return text2;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000B694 File Offset: 0x00009894
		internal static string GetSchemeDelimiter(string scheme)
		{
			for (int i = 0; i < Uri.schemes.Length; i++)
			{
				if (Uri.schemes[i].scheme == scheme)
				{
					return Uri.schemes[i].delimiter;
				}
			}
			return Uri.SchemeDelimiter;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000B6EC File Offset: 0x000098EC
		internal static int GetDefaultPort(string scheme)
		{
			for (int i = 0; i < Uri.schemes.Length; i++)
			{
				if (Uri.schemes[i].scheme == scheme)
				{
					return Uri.schemes[i].defaultPort;
				}
			}
			return -1;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000B740 File Offset: 0x00009940
		private string GetOpaqueWiseSchemeDelimiter()
		{
			if (this.isOpaquePart)
			{
				return ":";
			}
			return Uri.GetSchemeDelimiter(this.scheme);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000B760 File Offset: 0x00009960
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme != null)
			{
				if (Uri.<>f__switch$map17 == null)
				{
					Uri.<>f__switch$map17 = new Dictionary<string, int>(8)
					{
						{
							"http",
							0
						},
						{
							"https",
							0
						},
						{
							"file",
							0
						},
						{
							"ftp",
							0
						},
						{
							"nntp",
							0
						},
						{
							"gopher",
							0
						},
						{
							"mailto",
							0
						},
						{
							"news",
							0
						}
					};
				}
				int num;
				if (Uri.<>f__switch$map17.TryGetValue(scheme, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400010B RID: 267
		private bool isUnixFilePath;

		// Token: 0x0400010C RID: 268
		private string source;

		// Token: 0x0400010D RID: 269
		private string scheme = string.Empty;

		// Token: 0x0400010E RID: 270
		private string host = string.Empty;

		// Token: 0x0400010F RID: 271
		private int port = -1;

		// Token: 0x04000110 RID: 272
		private string path = string.Empty;

		// Token: 0x04000111 RID: 273
		private string query = string.Empty;

		// Token: 0x04000112 RID: 274
		private string fragment = string.Empty;

		// Token: 0x04000113 RID: 275
		private string userinfo = string.Empty;

		// Token: 0x04000114 RID: 276
		private bool isUnc;

		// Token: 0x04000115 RID: 277
		private bool isOpaquePart;

		// Token: 0x04000116 RID: 278
		private string[] segments;

		// Token: 0x04000117 RID: 279
		private bool userEscaped;

		// Token: 0x04000118 RID: 280
		private string cachedAbsoluteUri;

		// Token: 0x04000119 RID: 281
		private string cachedToString;

		// Token: 0x0400011A RID: 282
		private string cachedLocalPath;

		// Token: 0x0400011B RID: 283
		private int cachedHashCode;

		// Token: 0x0400011C RID: 284
		private bool reduce = true;

		// Token: 0x0400011D RID: 285
		private static readonly string hexUpperChars = "0123456789ABCDEF";

		// Token: 0x0400011E RID: 286
		public static readonly string SchemeDelimiter = "://";

		// Token: 0x0400011F RID: 287
		public static readonly string UriSchemeFile = "file";

		// Token: 0x04000120 RID: 288
		public static readonly string UriSchemeFtp = "ftp";

		// Token: 0x04000121 RID: 289
		public static readonly string UriSchemeGopher = "gopher";

		// Token: 0x04000122 RID: 290
		public static readonly string UriSchemeHttp = "http";

		// Token: 0x04000123 RID: 291
		public static readonly string UriSchemeHttps = "https";

		// Token: 0x04000124 RID: 292
		public static readonly string UriSchemeMailto = "mailto";

		// Token: 0x04000125 RID: 293
		public static readonly string UriSchemeNews = "news";

		// Token: 0x04000126 RID: 294
		public static readonly string UriSchemeNntp = "nntp";

		// Token: 0x04000127 RID: 295
		private static Uri.UriScheme[] schemes = new Uri.UriScheme[]
		{
			new Uri.UriScheme(Uri.UriSchemeHttp, Uri.SchemeDelimiter, 80),
			new Uri.UriScheme(Uri.UriSchemeHttps, Uri.SchemeDelimiter, 443),
			new Uri.UriScheme(Uri.UriSchemeFtp, Uri.SchemeDelimiter, 21),
			new Uri.UriScheme(Uri.UriSchemeFile, Uri.SchemeDelimiter, -1),
			new Uri.UriScheme(Uri.UriSchemeMailto, ":", 25),
			new Uri.UriScheme(Uri.UriSchemeNews, ":", -1),
			new Uri.UriScheme(Uri.UriSchemeNntp, Uri.SchemeDelimiter, 119),
			new Uri.UriScheme(Uri.UriSchemeGopher, Uri.SchemeDelimiter, 70)
		};

		// Token: 0x02000047 RID: 71
		private struct UriScheme
		{
			// Token: 0x06000133 RID: 307 RVA: 0x0000B80C File Offset: 0x00009A0C
			public UriScheme(string s, string d, int p)
			{
				this.scheme = s;
				this.delimiter = d;
				this.defaultPort = p;
			}

			// Token: 0x04000129 RID: 297
			public string scheme;

			// Token: 0x0400012A RID: 298
			public string delimiter;

			// Token: 0x0400012B RID: 299
			public int defaultPort;
		}
	}
}
