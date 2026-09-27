using System;
using UnityEngine;

// Token: 0x02000A62 RID: 2658
[ExecuteInEditMode]
public class UIWidgetControl : MonoBehaviour
{
	// Token: 0x06004D92 RID: 19858 RVA: 0x001A872C File Offset: 0x001A692C
	private void Awake()
	{
		if (this.width == 800)
		{
			this.width = 804;
		}
		if (this.height == 480)
		{
			this.height = 484;
		}
		this.uiWidget = base.GetComponent<UIWidget>();
		if (this.uiWidget != null)
		{
			float num = (float)Screen.width / (float)Screen.height * 480f;
			int num2 = Mathf.FloorToInt(num * ((float)this.width / UIWidgetControl.originWidth));
			if (num2 != this.uiWidget.width)
			{
				this.uiWidget.width = num2;
				this.uiWidget.height = this.height;
			}
			BoxCollider component = base.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.size = new Vector3((float)num2, (float)this.height, 1f);
			}
		}
	}

	// Token: 0x06004D93 RID: 19859 RVA: 0x001A8810 File Offset: 0x001A6A10
	public static int GetFitWidth(int sourceWidth)
	{
		float num = (float)Screen.width / (float)Screen.height * 480f;
		return Mathf.FloorToInt(num * ((float)sourceWidth / UIWidgetControl.originWidth));
	}

	// Token: 0x06004D94 RID: 19860 RVA: 0x001A8840 File Offset: 0x001A6A40
	private void Start()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06004D95 RID: 19861 RVA: 0x001A8870 File Offset: 0x001A6A70
	private void Update()
	{
	}

	// Token: 0x06004D96 RID: 19862 RVA: 0x001A8874 File Offset: 0x001A6A74
	private void ScreenSizeChanged()
	{
		this.Awake();
	}

	// Token: 0x06004D97 RID: 19863 RVA: 0x001A887C File Offset: 0x001A6A7C
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x04003C52 RID: 15442
	private UIWidget uiWidget;

	// Token: 0x04003C53 RID: 15443
	public int width = 800;

	// Token: 0x04003C54 RID: 15444
	public int height = 480;

	// Token: 0x04003C55 RID: 15445
	private static float originWidth = 800f;
}
