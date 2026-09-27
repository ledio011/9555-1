using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public struct ScreenRaycastData
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000088 RID: 136 RVA: 0x00003B30 File Offset: 0x00001D30
	public GameObject GameObject
	{
		get
		{
			if (this.Is2D)
			{
				return (!this.Hit2D.collider) ? null : this.Hit2D.collider.gameObject;
			}
			return (!this.Hit3D.collider) ? null : this.Hit3D.collider.gameObject;
		}
	}

	// Token: 0x04000052 RID: 82
	public bool Is2D;

	// Token: 0x04000053 RID: 83
	public RaycastHit Hit3D;

	// Token: 0x04000054 RID: 84
	public RaycastHit2D Hit2D;
}
