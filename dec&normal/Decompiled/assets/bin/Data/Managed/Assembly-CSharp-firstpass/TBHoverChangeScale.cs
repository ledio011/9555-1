using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class TBHoverChangeScale : MonoBehaviour
{
	// Token: 0x060001FB RID: 507 RVA: 0x00008A98 File Offset: 0x00006C98
	private void Start()
	{
		this.originalScale = base.transform.localScale;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00008AAC File Offset: 0x00006CAC
	private void OnFingerHover(FingerHoverEvent e)
	{
		if (e.Phase == FingerHoverPhase.Enter)
		{
			base.transform.localScale = this.hoverScaleFactor * this.originalScale;
		}
		else
		{
			base.transform.localScale = this.originalScale;
		}
	}

	// Token: 0x04000173 RID: 371
	public float hoverScaleFactor = 1.5f;

	// Token: 0x04000174 RID: 372
	private Vector3 originalScale = Vector3.one;
}
