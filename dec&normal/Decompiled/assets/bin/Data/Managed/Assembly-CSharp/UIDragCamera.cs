using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	// Token: 0x0600013B RID: 315 RVA: 0x00008794 File Offset: 0x00006994
	private void Awake()
	{
		if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000087C4 File Offset: 0x000069C4
	private void OnPress(bool isPressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000880C File Offset: 0x00006A0C
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00008854 File Offset: 0x00006A54
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x04000153 RID: 339
	public UIDraggableCamera draggableCamera;
}
