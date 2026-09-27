using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	public sealed class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000242 RID: 578
		public extern AssetBundle assetBundle { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000243 RID: 579
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern void DisableCompatibilityChecks();
	}
}
