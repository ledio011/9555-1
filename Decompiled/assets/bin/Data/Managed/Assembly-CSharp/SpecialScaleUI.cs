using System;
using UnityEngine;

// Token: 0x02000A4B RID: 2635
public class SpecialScaleUI : MonoBehaviour
{
	// Token: 0x06004CC9 RID: 19657 RVA: 0x001A0EEC File Offset: 0x0019F0EC
	private void Awake()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06004CCA RID: 19658 RVA: 0x001A0F1C File Offset: 0x0019F11C
	private void OnEnable()
	{
		this.ScreenSizeChanged();
	}

	// Token: 0x06004CCB RID: 19659 RVA: 0x001A0F24 File Offset: 0x0019F124
	private void ScreenSizeChanged()
	{
		base.transform.localScale = new Vector3((float)Screen.width / (float)Screen.height * 0.6f, Mathf.Clamp01((float)Screen.width / (float)Screen.height * 0.6f), 1f);
	}

	// Token: 0x06004CCC RID: 19660 RVA: 0x001A0F74 File Offset: 0x0019F174
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}
}
