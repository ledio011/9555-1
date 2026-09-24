using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000B2 RID: 178
	public sealed class Mesh : Object
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x00012E00 File Offset: 0x00011000
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x0600079D RID: 1949
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x0600079E RID: 1950
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Clear([DefaultValue("true")] bool keepVertexLayout);

		// Token: 0x0600079F RID: 1951 RVA: 0x00012E10 File Offset: 0x00011010
		[ExcludeFromDocs]
		public void Clear()
		{
			bool keepVertexLayout = true;
			this.Clear(keepVertexLayout);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007A0 RID: 1952
		public extern bool isReadable { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007A1 RID: 1953
		internal extern bool canAccess { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007A2 RID: 1954
		// (set) Token: 0x060007A3 RID: 1955
		public extern Vector3[] vertices { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007A4 RID: 1956
		// (set) Token: 0x060007A5 RID: 1957
		public extern Vector3[] normals { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007A6 RID: 1958
		// (set) Token: 0x060007A7 RID: 1959
		public extern Vector4[] tangents { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007A8 RID: 1960
		// (set) Token: 0x060007A9 RID: 1961
		public extern Vector2[] uv { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007AA RID: 1962
		// (set) Token: 0x060007AB RID: 1963
		public extern Vector2[] uv2 { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00012E28 File Offset: 0x00011028
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00012E30 File Offset: 0x00011030
		public Vector2[] uv1
		{
			get
			{
				return this.uv2;
			}
			set
			{
				this.uv2 = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00012E3C File Offset: 0x0001103C
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00012E54 File Offset: 0x00011054
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_bounds(ref value);
			}
		}

		// Token: 0x060007B0 RID: 1968
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x060007B1 RID: 1969
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_bounds(ref Bounds value);

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007B2 RID: 1970
		// (set) Token: 0x060007B3 RID: 1971
		public extern Color[] colors { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007B4 RID: 1972
		// (set) Token: 0x060007B5 RID: 1973
		public extern Color32[] colors32 { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060007B6 RID: 1974
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RecalculateBounds();

		// Token: 0x060007B7 RID: 1975
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RecalculateNormals();

		// Token: 0x060007B8 RID: 1976
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Optimize();

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007B9 RID: 1977
		// (set) Token: 0x060007BA RID: 1978
		public extern int[] triangles { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060007BB RID: 1979
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int[] GetTriangles(int submesh);

		// Token: 0x060007BC RID: 1980
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetTriangles(int[] triangles, int submesh);

		// Token: 0x060007BD RID: 1981
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int[] GetIndices(int submesh);

		// Token: 0x060007BE RID: 1982
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetIndices(int[] indices, MeshTopology topology, int submesh);

		// Token: 0x060007BF RID: 1983
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern MeshTopology GetTopology(int submesh);

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007C0 RID: 1984
		public extern int vertexCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007C1 RID: 1985
		// (set) Token: 0x060007C2 RID: 1986
		public extern int subMeshCount { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060007C3 RID: 1987
		[WrapperlessIcall]
		[Obsolete("Use SetTriangles instead. Internally this function will convert the triangle strip to a list of triangles anyway.")]
		[MethodImpl(4096)]
		public extern void SetTriangleStrip(int[] triangles, int submesh);

		// Token: 0x060007C4 RID: 1988
		[Obsolete("Use GetTriangles instead. Internally this function converts a list of triangles to a strip, so it might be slow, it might be a mess.")]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int[] GetTriangleStrip(int submesh);

		// Token: 0x060007C5 RID: 1989
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CombineMeshes(CombineInstance[] combine, [DefaultValue("true")] bool mergeSubMeshes, [DefaultValue("true")] bool useMatrices);

		// Token: 0x060007C6 RID: 1990 RVA: 0x00012E60 File Offset: 0x00011060
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			bool useMatrices = true;
			this.CombineMeshes(combine, mergeSubMeshes, useMatrices);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00012E78 File Offset: 0x00011078
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			bool useMatrices = true;
			bool mergeSubMeshes = true;
			this.CombineMeshes(combine, mergeSubMeshes, useMatrices);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007C8 RID: 1992
		// (set) Token: 0x060007C9 RID: 1993
		public extern BoneWeight[] boneWeights { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007CA RID: 1994
		// (set) Token: 0x060007CB RID: 1995
		public extern Matrix4x4[] bindposes { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060007CC RID: 1996
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void MarkDynamic();

		// Token: 0x060007CD RID: 1997
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void UploadMeshData(bool markNoLogerReadable);

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007CE RID: 1998
		public extern int blendShapeCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060007CF RID: 1999
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern string GetBlendShapeName(int index);

		// Token: 0x060007D0 RID: 2000
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetBlendShapeIndex(string blendShapeName);
	}
}
