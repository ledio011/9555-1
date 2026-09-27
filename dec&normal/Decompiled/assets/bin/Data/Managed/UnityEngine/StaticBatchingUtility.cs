using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010F RID: 271
	public sealed class StaticBatchingUtility
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x00016644 File Offset: 0x00014844
		public static void Combine(GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot);
		}

		// Token: 0x060009E4 RID: 2532
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		// Token: 0x060009E5 RID: 2533
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, [Writable] Mesh combinedMesh);
	}
}
