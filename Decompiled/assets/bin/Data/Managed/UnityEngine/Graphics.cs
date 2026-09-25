using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200006F RID: 111
	public sealed class Graphics
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0000A430 File Offset: 0x00008630
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000A43C File Offset: 0x0000863C
		private static void Internal_DrawMeshNow2(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.INTERNAL_CALL_Internal_DrawMeshNow2(mesh, ref matrix, materialIndex);
		}

		// Token: 0x060004D6 RID: 1238
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_DrawMeshNow2(Mesh mesh, ref Matrix4x4 matrix, int materialIndex);

		// Token: 0x060004D7 RID: 1239
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void DrawTexture(ref InternalDrawTextureArguments arguments);

		// Token: 0x060004D8 RID: 1240
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Blit(Texture source, RenderTexture dest);

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000A448 File Offset: 0x00008648
		[ExcludeFromDocs]
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			int pass = -1;
			Graphics.Blit(source, dest, mat, pass);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000A460 File Offset: 0x00008660
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial(source, dest, mat, pass, true);
		}

		// Token: 0x060004DB RID: 1243
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMaterial(Texture source, RenderTexture dest, Material mat, int pass, bool setRT);

		// Token: 0x060004DC RID: 1244 RVA: 0x0000A46C File Offset: 0x0000866C
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			Graphics.Internal_BlitMultiTap(source, dest, mat, offsets);
		}

		// Token: 0x060004DD RID: 1245
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMultiTap(Texture source, RenderTexture dest, Material mat, Vector2[] offsets);
	}
}
