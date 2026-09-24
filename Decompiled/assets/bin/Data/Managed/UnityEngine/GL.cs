using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200006B RID: 107
	public sealed class GL
	{
		// Token: 0x060004BA RID: 1210
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x060004BB RID: 1211
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void TexCoord2(float x, float y);

		// Token: 0x060004BC RID: 1212
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void MultiTexCoord2(int unit, float x, float y);

		// Token: 0x060004BD RID: 1213
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Begin(int mode);

		// Token: 0x060004BE RID: 1214
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void End();

		// Token: 0x060004BF RID: 1215
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void LoadOrtho();

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.INTERNAL_CALL_LoadProjectionMatrix(ref mat);
		}

		// Token: 0x060004C1 RID: 1217
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_LoadProjectionMatrix(ref Matrix4x4 mat);

		// Token: 0x060004C2 RID: 1218
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void LoadIdentity();

		// Token: 0x060004C3 RID: 1219
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void PushMatrix();

		// Token: 0x060004C4 RID: 1220
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void PopMatrix();

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000A3BC File Offset: 0x000085BC
		[ExcludeFromDocs]
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			float depth = 1f;
			GL.Clear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.Internal_Clear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000A3E4 File Offset: 0x000085E4
		private static void Internal_Clear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.INTERNAL_CALL_Internal_Clear(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x060004C8 RID: 1224
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_Clear(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x060004C9 RID: 1225
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ClearWithSkybox(bool clearDepth, Camera camera);

		// Token: 0x040000F7 RID: 247
		public const int TRIANGLES = 4;

		// Token: 0x040000F8 RID: 248
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x040000F9 RID: 249
		public const int QUADS = 7;

		// Token: 0x040000FA RID: 250
		public const int LINES = 1;
	}
}
