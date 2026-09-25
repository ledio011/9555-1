using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x0600046C RID: 1132 RVA: 0x0001FB58 File Offset: 0x0001DD58
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001FB6C File Offset: 0x0001DD6C
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x040003F8 RID: 1016
	public Color color = Color.white;

	// Token: 0x040003F9 RID: 1017
	private UIWidget mWidget;
}
