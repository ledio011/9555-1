using System;
using UnityEngine;

// Token: 0x020008C9 RID: 2249
public class UISpriteKeepInPanel : MonoBehaviour
{
	// Token: 0x06003C95 RID: 15509 RVA: 0x0010A074 File Offset: 0x00108274
	private void Start()
	{
		this.mRootPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		this.mCacheTransform = base.transform;
		this.mDefaultPos = this.mCacheTransform.localPosition;
		this.bounds = NGUIMath.CalculateRelativeWidgetBounds(this.mCacheTransform, this.mCacheTransform);
	}

	// Token: 0x06003C96 RID: 15510 RVA: 0x0010A0C8 File Offset: 0x001082C8
	private void Update()
	{
		if (this.mRootPanel != null)
		{
			this.mCacheTransform.localPosition = this.mRootPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max) + this.mDefaultPos;
		}
	}

	// Token: 0x040027D4 RID: 10196
	public UIPanel mRootPanel;

	// Token: 0x040027D5 RID: 10197
	public Vector3 mDefaultPos;

	// Token: 0x040027D6 RID: 10198
	private Transform mCacheTransform;

	// Token: 0x040027D7 RID: 10199
	public UIWidget widget;

	// Token: 0x040027D8 RID: 10200
	private Bounds bounds;
}
