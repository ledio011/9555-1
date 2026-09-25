using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06000469 RID: 1129 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x040003F5 RID: 1013
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x040003F6 RID: 1014
	private UIWidget mWidget;

	// Token: 0x040003F7 RID: 1015
	private UIPanel mPanel;
}
