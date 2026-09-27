using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000960 RID: 2400
public class OpenBoxRootLogic : SingletonUnity<OpenBoxRootLogic>
{
	// Token: 0x06004339 RID: 17209 RVA: 0x0014BD5C File Offset: 0x00149F5C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x0600433A RID: 17210 RVA: 0x0014BD68 File Offset: 0x00149F68
	private void CheckTutorialEvent()
	{
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x0600433B RID: 17211 RVA: 0x0014BDC0 File Offset: 0x00149FC0
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x0600433C RID: 17212 RVA: 0x0014BDCC File Offset: 0x00149FCC
	public void Reset()
	{
		NGUITools.SetActive(this.BackRoot, false);
		NGUITools.SetActive(this.TipRoot, false);
		this.OpenFlag = false;
		this.startTime = Time.time;
	}

	// Token: 0x0600433D RID: 17213 RVA: 0x0014BE04 File Offset: 0x0014A004
	public void ShowTipPage(ret_open_item_package.request request)
	{
		this.OpenFlag = true;
		this.mCurItems = request.items;
	}

	// Token: 0x0600433E RID: 17214 RVA: 0x0014BE1C File Offset: 0x0014A01C
	public void ShowTipPage(ret_commercail_reward.request request)
	{
		this.OpenFlag = true;
		this.mCurItems = request.items;
	}

	// Token: 0x0600433F RID: 17215 RVA: 0x0014BE34 File Offset: 0x0014A034
	private void Update()
	{
		if (this.OpenFlag && Time.time - this.startTime > 2f && !UnityVersionUtil.IsActive(this.BackRoot.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.OpenBoxEffect.gameObject, false);
			NGUITools.SetActive(this.BackRoot, true);
			NGUITools.SetActive(this.TipRoot, true);
			this.ShowRewardItem.ShowRewards(this.mCurItems);
		}
	}

	// Token: 0x06004340 RID: 17216 RVA: 0x0014BEB4 File Offset: 0x0014A0B4
	public void OnClickOkBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.OpenBoxRoot);
		if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_CONFIRM)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004341 RID: 17217 RVA: 0x0014BEE8 File Offset: 0x0014A0E8
	public void OnTweenScaleFinish()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_WAIT_OPEN_BOX)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x04002FC2 RID: 12226
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002FC3 RID: 12227
	public GameObject BackRoot;

	// Token: 0x04002FC4 RID: 12228
	public GameObject TipRoot;

	// Token: 0x04002FC5 RID: 12229
	public GameObject OpenBoxEffect;

	// Token: 0x04002FC6 RID: 12230
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002FC7 RID: 12231
	private float startTime;

	// Token: 0x04002FC8 RID: 12232
	private bool OpenFlag;

	// Token: 0x04002FC9 RID: 12233
	private List<item> mCurItems;

	// Token: 0x04002FCA RID: 12234
	public UIWidget OkBtnRoot;
}
