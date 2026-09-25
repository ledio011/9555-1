using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010C RID: 268
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x17000232 RID: 562
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00015F8C File Offset: 0x0001418C
		public Sprite sprite
		{
			set
			{
				this.SetSprite_INTERNAL(value);
			}
		}

		// Token: 0x060009D4 RID: 2516
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void SetSprite_INTERNAL(Sprite sprite);
	}
}
