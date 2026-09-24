using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x02000099 RID: 153
	[System.ComponentModel.TypeConverter(typeof(System.UriTypeConverter))]
	[Serializable]
	public class Uri : ISerializable
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public Uri(string uriString) : this(uriString, false)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000FA04 File Offset: 0x0000DC04
		protected Uri(SerializationInfo serializationInfo, StreamingContext streamingContext) : this(serializationInfo.GetString("AbsoluteUri"), true)
		{
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000FA18 File Offset: 0x0000DC18
		[Obsolete]
		public Uri(string uriString, bool dontEscape)
		{
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.ParseUri(System.UriKind.Absolute);
			if (!this.isAbsoluteUri)
			{
				throw new System.UriFormatException("Invalid URI: The format of the URI could not be determined: " + uriString);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000FC28 File Offset: 0x0000DE28
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AbsoluteUri", this.AbsoluteUri);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		public string AbsoluteUri
		{
			get
			{
				this.EnsureAbsoluteUri();
				if (this.cachedAbsoluteUri == null)
				{
					this.cachedAbsoluteUri = this.GetLeftPart(System.UriPartial.Path);
					if (this.query.Length > 0)
					{
						this.cachedAbsoluteUri += this.query;
					}
					if (this.fragment.Length > 0)
					{
						this.cachedAbsoluteUri += this.fragment;
					}
				}
				return this.cachedAbsoluteUri;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public bool IsUnc
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.isUnc;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		public string Scheme
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.scheme;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public bool IsAbsoluteUri
		{
			get
			{
				return this.isAbsoluteUri;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public static System.UriHostNameType CheckHostName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return System.UriHostNameType.Unknown;
			}
			if (System.Uri.IsIPv4Address(name))
			{
				return System.UriHostNameType.IPv4;
			}
			if (System.Uri.IsDomainAddress(name))
			{
				return System.UriHostNameType.Dns;
			}
			IPv6Address pv6Address;
			if (IPv6Address.TryParse(name, out pv6Address))
			{
				return System.UriHostNameType.IPv6;
			}
			return System.UriHostNameType.Unknown;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000FD34 File Offset: 0x0000DF34
		internal static bool IsIPv4Address(string name)
		{
			string[] array = name.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				if (array[i].Length == 0)
				{
					return false;
				}
				uint num;
				if (!uint.TryParse(array[i], out num))
				{
					return false;
				}
				if (num > 255U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		internal static bool IsDomainAddress(string name)
		{
			int length = name.Length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = name[i];
				if (num == 0)
				{
					if (!char.IsLetterOrDigit(c))
					{
						return false;
					}
				}
				else if (c == '.')
				{
					num = 0;
				}
				else if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					return false;
				}
				if (++num == 64)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FE24 File Offset: 0x0000E024
		public static bool CheckSchemeName(string schemeName)
		{
			if (schemeName == null || schemeName.Length == 0)
			{
				return false;
			}
			if (!System.Uri.IsAlpha(schemeName[0]))
			{
				return false;
			}
			int length = schemeName.Length;
			for (int i = 1; i < length; i++)
			{
				char c = schemeName[i];
				if (!char.IsDigit(c) && !System.Uri.IsAlpha(c) && c != '.' && c != '+' && c != '-')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		private static bool IsAlpha(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		public override bool Equals(object comparant)
		{
			if (comparant == null)
			{
				return false;
			}
			System.Uri uri = comparant as System.Uri;
			if (uri == null)
			{
				string text = comparant as string;
				if (text == null)
				{
					return false;
				}
				uri = new System.Uri(text);
			}
			return this.InternalEquals(uri);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000FF24 File Offset: 0x0000E124
		private bool InternalEquals(System.Uri uri)
		{
			if (this.isAbsoluteUri != uri.isAbsoluteUri)
			{
				return false;
			}
			if (!this.isAbsoluteUri)
			{
				return this.source == uri.source;
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			return this.scheme.ToLower(invariantCulture) == uri.scheme.ToLower(invariantCulture) && this.host.ToLower(invariantCulture) == uri.host.ToLower(invariantCulture) && this.port == uri.port && this.query == uri.query && this.path == uri.path;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public override int GetHashCode()
		{
			if (this.cachedHashCode == 0)
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				if (this.isAbsoluteUri)
				{
					this.cachedHashCode = (this.scheme.ToLower(invariantCulture).GetHashCode() ^ this.host.ToLower(invariantCulture).GetHashCode() ^ this.port ^ this.query.GetHashCode() ^ this.path.GetHashCode());
				}
				else
				{
					this.cachedHashCode = this.source.GetHashCode();
				}
			}
			return this.cachedHashCode;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010078 File Offset: 0x0000E278
		public string GetLeftPart(System.UriPartial part)
		{
			this.EnsureAbsoluteUri();
			switch (part)
			{
			case System.UriPartial.Scheme:
				return this.scheme + this.GetOpaqueWiseSchemeDelimiter();
			case System.UriPartial.Authority:
			{
				if (this.scheme == System.Uri.UriSchemeMailto || this.scheme == System.Uri.UriSchemeNews)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.scheme);
				stringBuilder.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && System.Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder.Append(this.userinfo).Append('@');
				}
				stringBuilder.Append(this.host);
				int defaultPort = System.Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder.Append(':').Append(this.port);
				}
				return stringBuilder.ToString();
			}
			case System.UriPartial.Path:
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(this.scheme);
				stringBuilder2.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && System.Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder2.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder2.Append(this.userinfo).Append('@');
				}
				stringBuilder2.Append(this.host);
				int defaultPort = System.Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder2.Append(':').Append(this.port);
				}
				if (this.path.Length > 0)
				{
					string text = this.Scheme;
					if (text != null)
					{
						if (System.Uri.<>f__switch$map14 == null)
						{
							System.Uri.<>f__switch$map14 = new Dictionary<string, int>(2)
							{
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
						if (System.Uri.<>f__switch$map14.TryGetValue(text, out num))
						{
							if (num == 0)
							{
								stringBuilder2.Append(this.path);
								goto IL_2A6;
							}
						}
					}
					stringBuilder2.Append(System.Uri.Reduce(this.path, System.Uri.CompactEscaped(this.Scheme)));
				}
				IL_2A6:
				return stringBuilder2.ToString();
			}
			default:
				return null;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010334 File Offset: 0x0000E534
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

		// Token: 0x0600034B RID: 843 RVA: 0x00010390 File Offset: 0x0000E590
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			return "%" + System.Uri.hexUpperChars[(int)((character & 'ð') >> 4)] + System.Uri.hexUpperChars[(int)(character & '\u000f')];
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000103E8 File Offset: 0x0000E5E8
		public static bool IsHexDigit(char digit)
		{
			return ('0' <= digit && digit <= '9') || ('a' <= digit && digit <= 'f') || ('A' <= digit && digit <= 'F');
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010420 File Offset: 0x0000E620
		public static bool IsHexEncoding(string pattern, int index)
		{
			return index + 3 <= pattern.Length && (pattern[index++] == '%' && System.Uri.IsHexDigit(pattern[index++])) && System.Uri.IsHexDigit(pattern[index]);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00010478 File Offset: 0x0000E678
		private void AppendQueryAndFragment(ref string result)
		{
			if (this.query.Length > 0)
			{
				string str = (this.query[0] != '?') ? System.Uri.Unescape(this.query, false) : ('?' + System.Uri.Unescape(this.query.Substring(1), false));
				result += str;
			}
			if (this.fragment.Length > 0)
			{
				result += this.fragment;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010504 File Offset: 0x0000E704
		public override string ToString()
		{
			if (this.cachedToString != null)
			{
				return this.cachedToString;
			}
			if (this.isAbsoluteUri)
			{
				this.cachedToString = System.Uri.Unescape(this.GetLeftPart(System.UriPartial.Path), true);
			}
			else
			{
				this.cachedToString = this.Unescape(this.path);
			}
			this.AppendQueryAndFragment(ref this.cachedToString);
			return this.cachedToString;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001056C File Offset: 0x0000E76C
		[Obsolete]
		protected static string EscapeString(string str)
		{
			return System.Uri.EscapeString(str, false, true, true);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010578 File Offset: 0x0000E778
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
				if (System.Uri.IsHexEncoding(str, i))
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
							stringBuilder.Append(System.Uri.HexEscape(c));
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

		// Token: 0x06000352 RID: 850 RVA: 0x00010698 File Offset: 0x0000E898
		private void ParseUri(System.UriKind kind)
		{
			this.Parse(kind, this.source);
			if (this.userEscaped)
			{
				return;
			}
			this.host = System.Uri.EscapeString(this.host, false, true, false);
			if (this.host.Length > 1 && this.host[0] != '[' && this.host[this.host.Length - 1] != ']')
			{
				this.host = this.host.ToLower(CultureInfo.InvariantCulture);
			}
			if (this.path.Length > 0)
			{
				this.path = System.Uri.EscapeString(this.path);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010750 File Offset: 0x0000E950
		[Obsolete]
		protected virtual string Unescape(string str)
		{
			return System.Uri.Unescape(str, false);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001075C File Offset: 0x0000E95C
		internal static string Unescape(string str, bool excludeSpecial)
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
					char c3;
					char c2 = System.Uri.HexUnescapeMultiByte(str, ref i, out c3);
					if (excludeSpecial && c2 == '#')
					{
						stringBuilder.Append("%23");
					}
					else if (excludeSpecial && c2 == '%')
					{
						stringBuilder.Append("%25");
					}
					else if (excludeSpecial && c2 == '?')
					{
						stringBuilder.Append("%3F");
					}
					else
					{
						stringBuilder.Append(c2);
						if (c3 != '\0')
						{
							stringBuilder.Append(c3);
						}
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

		// Token: 0x06000355 RID: 853 RVA: 0x00010840 File Offset: 0x0000EA40
		private void ParseAsWindowsUNC(string uriString)
		{
			this.scheme = System.Uri.UriSchemeFile;
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

		// Token: 0x06000356 RID: 854 RVA: 0x000108EC File Offset: 0x0000EAEC
		private string ParseAsWindowsAbsoluteFilePath(string uriString)
		{
			if (uriString.Length > 2 && uriString[2] != '\\' && uriString[2] != '/')
			{
				return "Relative file path is not allowed.";
			}
			this.scheme = System.Uri.UriSchemeFile;
			this.host = string.Empty;
			this.port = -1;
			this.path = uriString.Replace("\\", "/");
			this.fragment = string.Empty;
			this.query = string.Empty;
			return null;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010974 File Offset: 0x0000EB74
		private void ParseAsUnixAbsoluteFilePath(string uriString)
		{
			this.isUnixFilePath = true;
			this.scheme = System.Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.host = string.Empty;
			this.path = null;
			if (uriString.Length >= 2 && uriString[0] == '/' && uriString[1] == '/')
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

		// Token: 0x06000358 RID: 856 RVA: 0x00010A24 File Offset: 0x0000EC24
		private void Parse(System.UriKind kind, string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			string text = this.ParseNoExceptions(kind, uriString);
			if (text != null)
			{
				throw new System.UriFormatException(text);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010A58 File Offset: 0x0000EC58
		private string ParseNoExceptions(System.UriKind kind, string uriString)
		{
			uriString = uriString.Trim();
			int length = uriString.Length;
			if (length == 0 && (kind == System.UriKind.Relative || kind == System.UriKind.RelativeOrAbsolute))
			{
				this.isAbsoluteUri = false;
				return null;
			}
			if (length <= 1 && kind != System.UriKind.Relative)
			{
				return "Absolute URI is too short";
			}
			int num = uriString.IndexOf(':');
			if (num == 0)
			{
				return "Invalid URI: The format of the URI could not be determined.";
			}
			if (num < 0)
			{
				if (uriString[0] == '/' && Path.DirectorySeparatorChar == '/')
				{
					this.ParseAsUnixAbsoluteFilePath(uriString);
					if (kind == System.UriKind.Relative)
					{
						this.isAbsoluteUri = false;
					}
				}
				else if (uriString.Length >= 2 && uriString[0] == '\\' && uriString[1] == '\\')
				{
					this.ParseAsWindowsUNC(uriString);
				}
				else
				{
					this.isAbsoluteUri = false;
					this.path = uriString;
				}
				return null;
			}
			if (num == 1)
			{
				if (!System.Uri.IsAlpha(uriString[0]))
				{
					return "URI scheme must start with a letter.";
				}
				string text = this.ParseAsWindowsAbsoluteFilePath(uriString);
				if (text != null)
				{
					return text;
				}
				return null;
			}
			else
			{
				this.scheme = uriString.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
				if (!System.Uri.CheckSchemeName(this.scheme))
				{
					return Locale.GetText("URI scheme must start with a letter and must consist of one of alphabet, digits, '+', '-' or '.' character.");
				}
				int num2 = num + 1;
				int num3 = uriString.Length;
				num = uriString.IndexOf('#', num2);
				if (!this.IsUnc && num != -1)
				{
					if (this.userEscaped)
					{
						this.fragment = uriString.Substring(num);
					}
					else
					{
						this.fragment = "#" + System.Uri.EscapeString(uriString.Substring(num + 1));
					}
					num3 = num;
				}
				num = uriString.IndexOf('?', num2, num3 - num2);
				if (num != -1)
				{
					this.query = uriString.Substring(num, num3 - num);
					num3 = num;
					if (!this.userEscaped)
					{
						this.query = System.Uri.EscapeString(this.query);
					}
				}
				if (System.Uri.IsPredefinedScheme(this.scheme) && this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews && (num3 - num2 < 2 || (num3 - num2 >= 2 && uriString[num2] == '/' && uriString[num2 + 1] != '/')))
				{
					return "Invalid URI: The Authority/Host could not be parsed.";
				}
				bool flag = num3 - num2 >= 2 && uriString[num2] == '/' && uriString[num2 + 1] == '/';
				bool flag2 = this.scheme == System.Uri.UriSchemeFile && flag && (num3 - num2 == 2 || uriString[num2 + 2] == '/');
				bool flag3 = false;
				if (flag)
				{
					if (kind == System.UriKind.Relative)
					{
						return "Absolute URI when we expected a relative one";
					}
					if (this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews)
					{
						num2 += 2;
					}
					if (this.scheme == System.Uri.UriSchemeFile)
					{
						int num4 = 2;
						for (int i = num2; i < num3; i++)
						{
							if (uriString[i] != '/')
							{
								break;
							}
							num4++;
						}
						if (num4 >= 4)
						{
							flag2 = false;
							while (num2 < num3 && uriString[num2] == '/')
							{
								num2++;
							}
						}
						else if (num4 >= 3)
						{
							num2++;
						}
					}
					if (num3 - num2 > 1 && uriString[num2 + 1] == ':')
					{
						flag2 = false;
						flag3 = true;
					}
				}
				else if (!System.Uri.IsPredefinedScheme(this.scheme))
				{
					this.path = uriString.Substring(num2, num3 - num2);
					this.isOpaquePart = true;
					return null;
				}
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = uriString.IndexOf('/', num2, num3 - num2);
					if (num == -1 && flag3)
					{
						num = uriString.IndexOf('\\', num2, num3 - num2);
					}
				}
				if (num == -1)
				{
					if (this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews)
					{
						this.path = "/";
					}
				}
				else
				{
					this.path = uriString.Substring(num, num3 - num);
					num3 = num;
				}
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = uriString.IndexOf('@', num2, num3 - num2);
				}
				if (num != -1)
				{
					this.userinfo = uriString.Substring(num2, num - num2);
					num2 = num + 1;
				}
				this.port = -1;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = uriString.LastIndexOf(':', num3 - 1, num3 - num2);
				}
				if (num != -1 && num != num3 - 1)
				{
					string text2 = uriString.Substring(num + 1, num3 - (num + 1));
					if (text2.Length > 0 && text2[text2.Length - 1] != ']')
					{
						if (!int.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out this.port) || this.port < 0 || this.port > 65535)
						{
							return "Invalid URI: Invalid port number";
						}
						num3 = num;
					}
					else if (this.port == -1)
					{
						this.port = System.Uri.GetDefaultPort(this.scheme);
					}
				}
				else if (this.port == -1)
				{
					this.port = System.Uri.GetDefaultPort(this.scheme);
				}
				uriString = uriString.Substring(num2, num3 - num2);
				this.host = uriString;
				if (flag2)
				{
					this.path = System.Uri.Reduce('/' + uriString, true);
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
				else if (this.scheme == System.Uri.UriSchemeFile)
				{
					this.isUnc = true;
				}
				else if (this.scheme == System.Uri.UriSchemeNews)
				{
					if (this.host.Length > 0)
					{
						this.path = this.host;
						this.host = string.Empty;
					}
				}
				else if (this.host.Length == 0 && (this.scheme == System.Uri.UriSchemeHttp || this.scheme == System.Uri.UriSchemeGopher || this.scheme == System.Uri.UriSchemeNntp || this.scheme == System.Uri.UriSchemeHttps || this.scheme == System.Uri.UriSchemeFtp))
				{
					return "Invalid URI: The hostname could not be parsed";
				}
				bool flag4 = this.host.Length > 0 && System.Uri.CheckHostName(this.host) == System.UriHostNameType.Unknown;
				if (!flag4 && this.host.Length > 1 && this.host[0] == '[' && this.host[this.host.Length - 1] == ']')
				{
					IPv6Address pv6Address;
					if (IPv6Address.TryParse(this.host, out pv6Address))
					{
						this.host = "[" + pv6Address.ToString(true) + "]";
					}
					else
					{
						flag4 = true;
					}
				}
				if (flag4 && (this.Parser is DefaultUriParser || this.Parser == null))
				{
					return Locale.GetText("Invalid URI: The hostname could not be parsed. (" + this.host + ")");
				}
				System.UriFormatException ex = null;
				if (this.Parser != null)
				{
					this.Parser.InitializeAndValidate(this, out ex);
				}
				if (ex != null)
				{
					return ex.Message;
				}
				if (this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews && this.scheme != System.Uri.UriSchemeFile)
				{
					this.path = System.Uri.Reduce(this.path, System.Uri.CompactEscaped(this.scheme));
				}
				return null;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000112EC File Offset: 0x0000F4EC
		private static bool CompactEscaped(string scheme)
		{
			if (scheme != null)
			{
				if (System.Uri.<>f__switch$map15 == null)
				{
					System.Uri.<>f__switch$map15 = new Dictionary<string, int>(5)
					{
						{
							"file",
							0
						},
						{
							"http",
							0
						},
						{
							"https",
							0
						},
						{
							"net.pipe",
							0
						},
						{
							"net.tcp",
							0
						}
					};
				}
				int num;
				if (System.Uri.<>f__switch$map15.TryGetValue(scheme, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011374 File Offset: 0x0000F574
		private static string Reduce(string path, bool compact_escaped)
		{
			if (path == "/")
			{
				return path;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (compact_escaped)
			{
				for (int i = 0; i < path.Length; i++)
				{
					char c = path[i];
					char c2 = c;
					if (c2 != '%')
					{
						if (c2 != '\\')
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append('/');
						}
					}
					else if (i < path.Length - 2)
					{
						char c3 = path[i + 1];
						char c4 = char.ToUpper(path[i + 2]);
						if ((c3 == '2' && c4 == 'F') || (c3 == '5' && c4 == 'C'))
						{
							stringBuilder.Append('/');
							i += 2;
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				path = stringBuilder.ToString();
			}
			else
			{
				path = path.Replace('\\', '/');
			}
			ArrayList arrayList = new ArrayList();
			int j = 0;
			while (j < path.Length)
			{
				int num = path.IndexOf('/', j);
				if (num == -1)
				{
					num = path.Length;
				}
				string text = path.Substring(j, num - j);
				j = num + 1;
				if (text.Length != 0 && !(text == "."))
				{
					if (text == "..")
					{
						int count = arrayList.Count;
						if (count != 0)
						{
							arrayList.RemoveAt(count - 1);
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
			stringBuilder.Length = 0;
			if (path[0] == '/')
			{
				stringBuilder.Append('/');
			}
			bool flag = true;
			foreach (object obj in arrayList)
			{
				string value = (string)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append('/');
				}
				stringBuilder.Append(value);
			}
			if (path.EndsWith("/"))
			{
				stringBuilder.Append('/');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000115F8 File Offset: 0x0000F7F8
		private static char HexUnescapeMultiByte(string pattern, ref int index, out char surrogate)
		{
			surrogate = '\0';
			if (pattern == null)
			{
				throw new ArgumentException("pattern");
			}
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (!System.Uri.IsHexEncoding(pattern, index))
			{
				return pattern[index++];
			}
			int num = index++;
			int num2 = System.Uri.FromHex(pattern[index++]);
			int num3 = System.Uri.FromHex(pattern[index++]);
			int num4 = num2;
			int num5 = 0;
			while ((num4 & 8) == 8)
			{
				num5++;
				num4 <<= 1;
			}
			if (num5 <= 1)
			{
				return (char)(num2 << 4 | num3);
			}
			byte[] array = new byte[num5];
			bool flag = false;
			array[0] = (byte)(num2 << 4 | num3);
			for (int i = 1; i < num5; i++)
			{
				if (!System.Uri.IsHexEncoding(pattern, index++))
				{
					flag = true;
					break;
				}
				int num6 = System.Uri.FromHex(pattern[index++]);
				if ((num6 & 12) != 8)
				{
					flag = true;
					break;
				}
				int num7 = System.Uri.FromHex(pattern[index++]);
				array[i] = (byte)(num6 << 4 | num7);
			}
			if (flag)
			{
				index = num + 3;
				return (char)array[0];
			}
			byte b = byte.MaxValue;
			b = (byte)(b >> num5 + 1);
			int num8 = (int)(array[0] & b);
			for (int j = 1; j < num5; j++)
			{
				num8 <<= 6;
				num8 |= (int)(array[j] & 63);
			}
			if (num8 <= 65535)
			{
				return (char)num8;
			}
			num8 -= 65536;
			surrogate = (char)((num8 & 1023) | 56320);
			return (char)(num8 >> 10 | 55296);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000117EC File Offset: 0x0000F9EC
		internal static string GetSchemeDelimiter(string scheme)
		{
			for (int i = 0; i < System.Uri.schemes.Length; i++)
			{
				if (System.Uri.schemes[i].scheme == scheme)
				{
					return System.Uri.schemes[i].delimiter;
				}
			}
			return System.Uri.SchemeDelimiter;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011844 File Offset: 0x0000FA44
		internal static int GetDefaultPort(string scheme)
		{
			System.UriParser uriParser = System.UriParser.GetParser(scheme);
			if (uriParser == null)
			{
				return -1;
			}
			return uriParser.DefaultPort;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011868 File Offset: 0x0000FA68
		private string GetOpaqueWiseSchemeDelimiter()
		{
			if (this.isOpaquePart)
			{
				return ":";
			}
			return System.Uri.GetSchemeDelimiter(this.scheme);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011888 File Offset: 0x0000FA88
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme != null)
			{
				if (System.Uri.<>f__switch$map16 == null)
				{
					System.Uri.<>f__switch$map16 = new Dictionary<string, int>(10)
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
						},
						{
							"net.pipe",
							0
						},
						{
							"net.tcp",
							0
						}
					};
				}
				int num;
				if (System.Uri.<>f__switch$map16.TryGetValue(scheme, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00011950 File Offset: 0x0000FB50
		private System.UriParser Parser
		{
			get
			{
				if (this.parser == null)
				{
					this.parser = System.UriParser.GetParser(this.Scheme);
					if (this.parser == null)
					{
						this.parser = new DefaultUriParser("*");
					}
				}
				return this.parser;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011990 File Offset: 0x0000FB90
		private void EnsureAbsoluteUri()
		{
			if (!this.IsAbsoluteUri)
			{
				throw new InvalidOperationException("This operation is not supported for a relative URI.");
			}
		}

		// Token: 0x04000A36 RID: 2614
		private const int MaxUriLength = 32766;

		// Token: 0x04000A37 RID: 2615
		private bool isUnixFilePath;

		// Token: 0x04000A38 RID: 2616
		private string source;

		// Token: 0x04000A39 RID: 2617
		private string scheme = string.Empty;

		// Token: 0x04000A3A RID: 2618
		private string host = string.Empty;

		// Token: 0x04000A3B RID: 2619
		private int port = -1;

		// Token: 0x04000A3C RID: 2620
		private string path = string.Empty;

		// Token: 0x04000A3D RID: 2621
		private string query = string.Empty;

		// Token: 0x04000A3E RID: 2622
		private string fragment = string.Empty;

		// Token: 0x04000A3F RID: 2623
		private string userinfo = string.Empty;

		// Token: 0x04000A40 RID: 2624
		private bool isUnc;

		// Token: 0x04000A41 RID: 2625
		private bool isOpaquePart;

		// Token: 0x04000A42 RID: 2626
		private bool isAbsoluteUri = true;

		// Token: 0x04000A43 RID: 2627
		private string[] segments;

		// Token: 0x04000A44 RID: 2628
		private bool userEscaped;

		// Token: 0x04000A45 RID: 2629
		private string cachedAbsoluteUri;

		// Token: 0x04000A46 RID: 2630
		private string cachedToString;

		// Token: 0x04000A47 RID: 2631
		private string cachedLocalPath;

		// Token: 0x04000A48 RID: 2632
		private int cachedHashCode;

		// Token: 0x04000A49 RID: 2633
		private static readonly string hexUpperChars = "0123456789ABCDEF";

		// Token: 0x04000A4A RID: 2634
		public static readonly string SchemeDelimiter = "://";

		// Token: 0x04000A4B RID: 2635
		public static readonly string UriSchemeFile = "file";

		// Token: 0x04000A4C RID: 2636
		public static readonly string UriSchemeFtp = "ftp";

		// Token: 0x04000A4D RID: 2637
		public static readonly string UriSchemeGopher = "gopher";

		// Token: 0x04000A4E RID: 2638
		public static readonly string UriSchemeHttp = "http";

		// Token: 0x04000A4F RID: 2639
		public static readonly string UriSchemeHttps = "https";

		// Token: 0x04000A50 RID: 2640
		public static readonly string UriSchemeMailto = "mailto";

		// Token: 0x04000A51 RID: 2641
		public static readonly string UriSchemeNews = "news";

		// Token: 0x04000A52 RID: 2642
		public static readonly string UriSchemeNntp = "nntp";

		// Token: 0x04000A53 RID: 2643
		public static readonly string UriSchemeNetPipe = "net.pipe";

		// Token: 0x04000A54 RID: 2644
		public static readonly string UriSchemeNetTcp = "net.tcp";

		// Token: 0x04000A55 RID: 2645
		private static System.Uri.UriScheme[] schemes = new System.Uri.UriScheme[]
		{
			new System.Uri.UriScheme(System.Uri.UriSchemeHttp, System.Uri.SchemeDelimiter, 80),
			new System.Uri.UriScheme(System.Uri.UriSchemeHttps, System.Uri.SchemeDelimiter, 443),
			new System.Uri.UriScheme(System.Uri.UriSchemeFtp, System.Uri.SchemeDelimiter, 21),
			new System.Uri.UriScheme(System.Uri.UriSchemeFile, System.Uri.SchemeDelimiter, -1),
			new System.Uri.UriScheme(System.Uri.UriSchemeMailto, ":", 25),
			new System.Uri.UriScheme(System.Uri.UriSchemeNews, ":", 119),
			new System.Uri.UriScheme(System.Uri.UriSchemeNntp, System.Uri.SchemeDelimiter, 119),
			new System.Uri.UriScheme(System.Uri.UriSchemeGopher, System.Uri.SchemeDelimiter, 70)
		};

		// Token: 0x04000A56 RID: 2646
		[NonSerialized]
		private System.UriParser parser;

		// Token: 0x0200009A RID: 154
		private struct UriScheme
		{
			// Token: 0x06000363 RID: 867 RVA: 0x000119A8 File Offset: 0x0000FBA8
			public UriScheme(string s, string d, int p)
			{
				this.scheme = s;
				this.delimiter = d;
				this.defaultPort = p;
			}

			// Token: 0x04000A5C RID: 2652
			public string scheme;

			// Token: 0x04000A5D RID: 2653
			public string delimiter;

			// Token: 0x04000A5E RID: 2654
			public int defaultPort;
		}
	}
}
