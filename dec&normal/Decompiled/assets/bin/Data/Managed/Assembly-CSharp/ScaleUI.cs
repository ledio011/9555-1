using System;
using UnityEngine;

// Token: 0x02000A42 RID: 2626
public class ScaleUI : MonoBehaviour
{
	// Token: 0x06004C99 RID: 19609 RVA: 0x0019F978 File Offset: 0x0019DB78
	private void Awake()
	{
	}

	// Token: 0x06004C9A RID: 19610 RVA: 0x0019F97C File Offset: 0x0019DB7C
	private void OnEnable()
	{
		this.ChangeScale();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ChangeScale));
	}

	// Token: 0x06004C9B RID: 19611 RVA: 0x0019F9B0 File Offset: 0x0019DBB0
	private void ChangeScale()
	{
		base.transform.localScale = Vector3.one * Mathf.Clamp01((float)Screen.width / (float)Screen.height * 0.6f);
	}

	// Token: 0x06004C9C RID: 19612 RVA: 0x0019F9EC File Offset: 0x0019DBEC
	private void OnDisable()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ChangeScale));
	}
}
