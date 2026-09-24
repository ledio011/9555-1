using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x06000072 RID: 114 RVA: 0x0000422C File Offset: 0x0000242C
	private void Awake()
	{
		UICursor.instance = this;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004234 File Offset: 0x00002434
	private void OnDestroy()
	{
		UICursor.instance = null;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000423C File Offset: 0x0000243C
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mSprite != null)
		{
			this.mAtlas = this.mSprite.atlas;
			this.mSpriteName = this.mSprite.spriteName;
			if (this.mSprite.depth < 100)
			{
				this.mSprite.depth = 100;
			}
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000042DC File Offset: 0x000024DC
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.isOrthoGraphic)
			{
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			this.mTrans.localPosition = mousePosition;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00004404 File Offset: 0x00002604
	public static void Clear()
	{
		if (UICursor.instance != null && UICursor.instance.mSprite != null)
		{
			UICursor.Set(UICursor.instance.mAtlas, UICursor.instance.mSpriteName);
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004450 File Offset: 0x00002650
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.instance != null && UICursor.instance.mSprite)
		{
			UICursor.instance.mSprite.atlas = atlas;
			UICursor.instance.mSprite.spriteName = sprite;
			UICursor.instance.mSprite.MakePixelPerfect();
			UICursor.instance.Update();
		}
	}

	// Token: 0x04000067 RID: 103
	public static UICursor instance;

	// Token: 0x04000068 RID: 104
	public Camera uiCamera;

	// Token: 0x04000069 RID: 105
	private Transform mTrans;

	// Token: 0x0400006A RID: 106
	private UISprite mSprite;

	// Token: 0x0400006B RID: 107
	private UIAtlas mAtlas;

	// Token: 0x0400006C RID: 108
	private string mSpriteName;
}
