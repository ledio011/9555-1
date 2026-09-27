using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class TBHoverChangeMaterial : MonoBehaviour
{
	// Token: 0x060001F8 RID: 504 RVA: 0x00008A24 File Offset: 0x00006C24
	private void Start()
	{
		this.normalMaterial = base.renderer.sharedMaterial;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00008A38 File Offset: 0x00006C38
	private void OnFingerHover(FingerHoverEvent e)
	{
		if (e.Phase == FingerHoverPhase.Enter)
		{
			base.renderer.sharedMaterial = this.hoverMaterial;
		}
		else
		{
			base.renderer.sharedMaterial = this.normalMaterial;
		}
	}

	// Token: 0x04000171 RID: 369
	public Material hoverMaterial;

	// Token: 0x04000172 RID: 370
	private Material normalMaterial;
}
