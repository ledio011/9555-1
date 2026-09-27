using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class UISpriteFlowLight : UISprite
{
	// Token: 0x17000147 RID: 327
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x0002FA9C File Offset: 0x0002DC9C
	public override Material material
	{
		get
		{
			if (this.mTestMat != null)
			{
				return this.mTestMat;
			}
			return null;
		}
	}

	// Token: 0x040005CB RID: 1483
	public Material mTestMat;
}
