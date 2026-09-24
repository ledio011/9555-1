using System;
using UnityEngine;

// Token: 0x020009DB RID: 2523
public class AutoComboInfoLogic : SingletonUnity<AutoComboInfoLogic>
{
	// Token: 0x060047B7 RID: 18359 RVA: 0x0016EB3C File Offset: 0x0016CD3C
	private void OnEnable()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCopyScene() && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			Vector3 localPosition = this.Offset.localPosition;
			localPosition.y = 60f;
			this.Offset.localPosition = localPosition;
			this.CurWidget.alpha = 0f;
		}
	}

	// Token: 0x060047B8 RID: 18360 RVA: 0x0016EBA8 File Offset: 0x0016CDA8
	public void Show(bool show)
	{
		if (this.isShow != show)
		{
			this.isShow = show;
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.ShowAutoCombo(show);
				this.CurWidget.alpha = 0f;
			}
			else if (this.isShow)
			{
				this.CurWidget.alpha = 1f;
			}
			else
			{
				this.CurWidget.alpha = 0f;
			}
		}
	}

	// Token: 0x04003512 RID: 13586
	private bool isShow = true;

	// Token: 0x04003513 RID: 13587
	public UIWidget CurWidget;

	// Token: 0x04003514 RID: 13588
	public Transform Offset;
}
