using System;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x0200002D RID: 45
	public sealed class FileVersionInfo
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002CEC File Offset: 0x00000EEC
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002CF4 File Offset: 0x00000EF4
		private static void AppendFormat(StringBuilder sb, string format, params object[] args)
		{
			sb.AppendFormat(format, args);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002D00 File Offset: 0x00000F00
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			FileVersionInfo.AppendFormat(stringBuilder, "File:             {0}{1}", new object[]
			{
				this.FileName,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "InternalName:     {0}{1}", new object[]
			{
				this.internalname,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "OriginalFilename: {0}{1}", new object[]
			{
				this.originalfilename,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "FileVersion:      {0}{1}", new object[]
			{
				this.fileversion,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "FileDescription:  {0}{1}", new object[]
			{
				this.filedescription,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Product:          {0}{1}", new object[]
			{
				this.productname,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "ProductVersion:   {0}{1}", new object[]
			{
				this.productversion,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Debug:            {0}{1}", new object[]
			{
				this.isdebug,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Patched:          {0}{1}", new object[]
			{
				this.ispatched,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "PreRelease:       {0}{1}", new object[]
			{
				this.isprerelease,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "PrivateBuild:     {0}{1}", new object[]
			{
				this.isprivatebuild,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "SpecialBuild:     {0}{1}", new object[]
			{
				this.isspecialbuild,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Language          {0}{1}", new object[]
			{
				this.language,
				Environment.NewLine
			});
			return stringBuilder.ToString();
		}

		// Token: 0x04000055 RID: 85
		private string comments;

		// Token: 0x04000056 RID: 86
		private string companyname;

		// Token: 0x04000057 RID: 87
		private string filedescription;

		// Token: 0x04000058 RID: 88
		private string filename;

		// Token: 0x04000059 RID: 89
		private string fileversion;

		// Token: 0x0400005A RID: 90
		private string internalname;

		// Token: 0x0400005B RID: 91
		private string language;

		// Token: 0x0400005C RID: 92
		private string legalcopyright;

		// Token: 0x0400005D RID: 93
		private string legaltrademarks;

		// Token: 0x0400005E RID: 94
		private string originalfilename;

		// Token: 0x0400005F RID: 95
		private string privatebuild;

		// Token: 0x04000060 RID: 96
		private string productname;

		// Token: 0x04000061 RID: 97
		private string productversion;

		// Token: 0x04000062 RID: 98
		private string specialbuild;

		// Token: 0x04000063 RID: 99
		private bool isdebug;

		// Token: 0x04000064 RID: 100
		private bool ispatched;

		// Token: 0x04000065 RID: 101
		private bool isprerelease;

		// Token: 0x04000066 RID: 102
		private bool isprivatebuild;

		// Token: 0x04000067 RID: 103
		private bool isspecialbuild;

		// Token: 0x04000068 RID: 104
		private int filemajorpart;

		// Token: 0x04000069 RID: 105
		private int fileminorpart;

		// Token: 0x0400006A RID: 106
		private int filebuildpart;

		// Token: 0x0400006B RID: 107
		private int fileprivatepart;

		// Token: 0x0400006C RID: 108
		private int productmajorpart;

		// Token: 0x0400006D RID: 109
		private int productminorpart;

		// Token: 0x0400006E RID: 110
		private int productbuildpart;

		// Token: 0x0400006F RID: 111
		private int productprivatepart;
	}
}
