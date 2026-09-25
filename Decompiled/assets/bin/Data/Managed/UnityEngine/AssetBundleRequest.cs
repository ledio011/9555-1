using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[StructLayout(0)]
	public sealed class AssetBundleRequest : AsyncOperation
	{
		// Token: 0x04000028 RID: 40
		internal AssetBundle m_AssetBundle;

		// Token: 0x04000029 RID: 41
		internal string m_Path;

		// Token: 0x0400002A RID: 42
		internal Type m_Type;
	}
}
