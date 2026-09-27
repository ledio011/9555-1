using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System
{
	// Token: 0x0200009E RID: 158
	public abstract class UriParser
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00011A2C File Offset: 0x0000FC2C
		protected internal virtual void InitializeAndValidate(System.Uri uri, out System.UriFormatException parsingError)
		{
			if (uri.Scheme != this.scheme_name && this.scheme_name != "*")
			{
				parsingError = new System.UriFormatException("The argument Uri's scheme does not match");
			}
			else
			{
				parsingError = null;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011A78 File Offset: 0x0000FC78
		[MonoTODO]
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		// Token: 0x170000B8 RID: 184
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00011A7C File Offset: 0x0000FC7C
		internal string SchemeName
		{
			set
			{
				this.scheme_name = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011A88 File Offset: 0x0000FC88
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00011A90 File Offset: 0x0000FC90
		internal int DefaultPort
		{
			get
			{
				return this.default_port;
			}
			set
			{
				this.default_port = value;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011A9C File Offset: 0x0000FC9C
		private static void CreateDefaults()
		{
			if (System.UriParser.table != null)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeFile, -1);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeFtp, 21);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeGopher, 70);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeHttp, 80);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeHttps, 443);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeMailto, 25);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNetPipe, -1);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNetTcp, -1);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNews, 119);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNntp, 119);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), "ldap", 389);
			object obj = System.UriParser.lock_object;
			lock (obj)
			{
				if (System.UriParser.table == null)
				{
					System.UriParser.table = hashtable;
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		private static void InternalRegister(Hashtable table, System.UriParser uriParser, string schemeName, int defaultPort)
		{
			uriParser.SchemeName = schemeName;
			uriParser.DefaultPort = defaultPort;
			if (uriParser is System.GenericUriParser)
			{
				table.Add(schemeName, uriParser);
			}
			else
			{
				table.Add(schemeName, new DefaultUriParser
				{
					SchemeName = schemeName,
					DefaultPort = defaultPort
				});
			}
			uriParser.OnRegister(schemeName, defaultPort);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011C1C File Offset: 0x0000FE1C
		internal static System.UriParser GetParser(string schemeName)
		{
			if (schemeName == null)
			{
				return null;
			}
			System.UriParser.CreateDefaults();
			string key = schemeName.ToLower(CultureInfo.InvariantCulture);
			return (System.UriParser)System.UriParser.table[key];
		}

		// Token: 0x04000A69 RID: 2665
		private static object lock_object = new object();

		// Token: 0x04000A6A RID: 2666
		private static Hashtable table;

		// Token: 0x04000A6B RID: 2667
		internal string scheme_name;

		// Token: 0x04000A6C RID: 2668
		private int default_port;

		// Token: 0x04000A6D RID: 2669
		private static readonly System.Text.RegularExpressions.Regex uri_regex = new System.Text.RegularExpressions.Regex("^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\?([^#]*))?(#(.*))?");

		// Token: 0x04000A6E RID: 2670
		private static readonly System.Text.RegularExpressions.Regex auth_regex = new System.Text.RegularExpressions.Regex("^(([^@]+)@)?(.*?)(:([0-9]+))?$");
	}
}
