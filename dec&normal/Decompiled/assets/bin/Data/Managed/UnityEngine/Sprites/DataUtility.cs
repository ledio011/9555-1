using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Sprites
{
	// Token: 0x0200010D RID: 269
	public sealed class DataUtility
	{
		// Token: 0x060009D5 RID: 2517
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Vector4 GetInnerUV(Sprite sprite);

		// Token: 0x060009D6 RID: 2518
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Vector4 GetOuterUV(Sprite sprite);

		// Token: 0x060009D7 RID: 2519
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Vector4 GetPadding(Sprite sprite);

		// Token: 0x060009D8 RID: 2520 RVA: 0x00015F98 File Offset: 0x00014198
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 result;
			DataUtility.Internal_GetMinSize(sprite, out result);
			return result;
		}

		// Token: 0x060009D9 RID: 2521
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_GetMinSize(Sprite sprite, out Vector2 output);
	}
}
