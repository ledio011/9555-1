using System;

namespace UnityEngine.UI
{
	// Token: 0x02000054 RID: 84
	public interface ICanvasElement
	{
		// Token: 0x06000247 RID: 583
		void Rebuild(CanvasUpdate executing);

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000248 RID: 584
		Transform transform { get; }

		// Token: 0x06000249 RID: 585
		bool IsDestroyed();
	}
}
