using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000357 RID: 855
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		// Token: 0x06001965 RID: 6501 RVA: 0x0005D844 File Offset: 0x0005BA44
		public override bool Equals(object o)
		{
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this._scheme == codeConnectAccess._scheme && this._port == codeConnectAccess._port;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005D888 File Offset: 0x0005BA88
		public override int GetHashCode()
		{
			return this._scheme.GetHashCode() ^ this._port;
		}

		// Token: 0x04000DEA RID: 3562
		public static readonly string AnyScheme = "*";

		// Token: 0x04000DEB RID: 3563
		public static readonly int DefaultPort = -3;

		// Token: 0x04000DEC RID: 3564
		public static readonly int OriginPort = -4;

		// Token: 0x04000DED RID: 3565
		public static readonly string OriginScheme = "$origin";

		// Token: 0x04000DEE RID: 3566
		private string _scheme;

		// Token: 0x04000DEF RID: 3567
		private int _port;
	}
}
