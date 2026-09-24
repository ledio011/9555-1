using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000031 RID: 49
	public struct ColorOptions : IPlugOptions
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000057C8 File Offset: 0x000039C8
		public void Reset()
		{
			this.alphaOnly = false;
		}

		// Token: 0x040000ED RID: 237
		public bool alphaOnly;
	}
}
