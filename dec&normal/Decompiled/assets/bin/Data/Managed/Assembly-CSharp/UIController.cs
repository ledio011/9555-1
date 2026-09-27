using System;
using UnityEngine;

// Token: 0x02000A5A RID: 2650
public class UIController : MonoBehaviour
{
	// Token: 0x06004D39 RID: 19769 RVA: 0x001A4848 File Offset: 0x001A2A48
	private void InitUI()
	{
		if (!SingletonUnity<UIManager>.Exists)
		{
			base.gameObject.AddComponent<UIManager>();
		}
	}

	// Token: 0x06004D3A RID: 19770 RVA: 0x001A4860 File Offset: 0x001A2A60
	private void Awake()
	{
		if (!UIController.InitScreenSizeFlag)
		{
			UIController.InitScreenSizeFlag = true;
			UIRoot component = base.gameObject.GetComponent<UIRoot>();
			UIController.ScreenHeight = (float)component.manualHeight;
			UIController.ScreenWidth = (float)Mathf.RoundToInt(UIController.ScreenHeight * ((float)Screen.width / (float)Screen.height));
			UIController.ScreenWidthScale = UIController.ScreenWidth / (float)Screen.width;
			UIController.ScreenHeightScale = UIController.ScreenHeight / (float)Screen.height;
		}
		this.InitUI();
	}

	// Token: 0x04003AFF RID: 15103
	public static bool InitScreenSizeFlag;

	// Token: 0x04003B00 RID: 15104
	public static float ScreenWidth;

	// Token: 0x04003B01 RID: 15105
	public static float ScreenHeight = 480f;

	// Token: 0x04003B02 RID: 15106
	public static float ScreenWidthScale;

	// Token: 0x04003B03 RID: 15107
	public static float ScreenHeightScale;
}
