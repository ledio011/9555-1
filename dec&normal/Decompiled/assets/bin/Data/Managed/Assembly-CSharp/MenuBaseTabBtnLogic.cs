using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009FC RID: 2556
public class MenuBaseTabBtnLogic : MonoBehaviour
{
	// Token: 0x0600492E RID: 18734 RVA: 0x00179A24 File Offset: 0x00177C24
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x0600492F RID: 18735 RVA: 0x00179A30 File Offset: 0x00177C30
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06004930 RID: 18736 RVA: 0x00179A50 File Offset: 0x00177C50
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06004931 RID: 18737 RVA: 0x00179A5C File Offset: 0x00177C5C
	public void Reset(MenuTabBtnInfo btnInfo)
	{
		this.mCurBtnInfo = btnInfo;
		if (this.mCurBtnInfo.IsIconBtn)
		{
			this.BtnIconSprite.spriteName = this.mCurBtnInfo.BtnName;
			this.BtnNameLabel.text = this.mCurBtnInfo.PageName;
			UISpriteData atlasSprite = this.BtnIconSprite.GetAtlasSprite();
			if (atlasSprite != null)
			{
				if (this.mCurBtnInfo.BtnName.Equals("CZ_left_Domain"))
				{
					this.BtnIconSprite.width = 32;
					this.BtnIconSprite.height = 32;
				}
				else
				{
					this.BtnIconSprite.width = atlasSprite.width;
					this.BtnIconSprite.height = atlasSprite.height;
				}
			}
			else
			{
				Debug.Log("No Pic In Atlas :: " + this.mCurBtnInfo.BtnName);
			}
		}
		else
		{
			if (this.BtnIconSprite != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.BtnIconSprite.gameObject, false);
			}
			UnityVersionUtil.SetActiveRecursive(this.BtnNameLabel.gameObject, true);
			this.BtnNameLabel.text = this.mCurBtnInfo.PageName;
		}
		if (this.mCurBtnInfo.FunctionType == FUNCTION_TYPE.COUNT)
		{
			this.mIsBtnEnable = true;
		}
		else if (GameManager.IsSupportCurDataVersion145())
		{
			this.mIsBtnEnable = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(this.mCurBtnInfo.FunctionType);
		}
		else if (this.mCurBtnInfo.FunctionType == FUNCTION_TYPE.GUILD_ACTIVITY)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				this.mIsBtnEnable = true;
			}
			else
			{
				this.mIsBtnEnable = false;
			}
		}
		else
		{
			this.mIsBtnEnable = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(this.mCurBtnInfo.FunctionType);
		}
		if (this.mIsBtnEnable)
		{
			if (this.BtnIconSprite != null)
			{
				this.BtnIconSprite.color = Color.white;
			}
			this.BtnNameLabel.color = Color.white;
		}
		else
		{
			if (this.BtnIconSprite != null)
			{
				this.BtnIconSprite.color = GameDefine.GrayColor;
			}
			this.BtnNameLabel.color = GameDefine.GrayColor;
			int functionType = (int)this.mCurBtnInfo.FunctionType;
			FunctionData functionDataById = DataManager.GetFunctionDataById(functionType.ToString());
			if (functionDataById != null)
			{
				this.unlockLevel = functionDataById.Condition;
			}
			else
			{
				this.unlockLevel = 1;
			}
		}
		List<string> menuTabBtnTipIdList = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList;
		int functionType2 = (int)this.mCurBtnInfo.FunctionType;
		if (menuTabBtnTipIdList.Contains(functionType2.ToString()))
		{
			FunctionTipsRootLogic.AddFunctionTips(base.gameObject, new Vector3((float)(this.BtnWidget.width / 2), (float)(-(float)this.BtnWidget.height / 2), 0f), -1f);
		}
		this.UpdateTips();
	}

	// Token: 0x06004932 RID: 18738 RVA: 0x00179D4C File Offset: 0x00177F4C
	public void UpdateTips()
	{
		this.isTips = false;
		if (this.TipsSp != null)
		{
			if (this.mCurBtnInfo.TipsFun != null)
			{
				this.isTips = this.mCurBtnInfo.TipsFun();
				this.TipsSp.enabled = this.isTips;
			}
			else
			{
				this.TipsSp.enabled = false;
			}
		}
	}

	// Token: 0x06004933 RID: 18739 RVA: 0x00179DBC File Offset: 0x00177FBC
	public void OnClickBtn()
	{
		List<string> menuTabBtnTipIdList = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList;
		int functionType = (int)this.mCurBtnInfo.FunctionType;
		if (menuTabBtnTipIdList.Contains(functionType.ToString()))
		{
			FunctionTipsRootLogic.RemoveFunctionTips(base.gameObject);
			List<string> menuTabBtnTipIdList2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList;
			int functionType2 = (int)this.mCurBtnInfo.FunctionType;
			menuTabBtnTipIdList2.Remove(functionType2.ToString());
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(this.mCurBtnInfo.FunctionType);
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateUnlockTips();
			}
		}
		if (this.mIsBtnEnable)
		{
			if (this.mCurBtnInfo.onClickBtn != null)
			{
				this.mCurBtnInfo.onClickBtn();
			}
		}
		else
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
			{
				this.unlockLevel
			}), true, false);
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.RANK_PVP_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.STRENGTH_STAR_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK_SKILL || TutorialManager.CurStep == TUTORIAL_STEP.TITLE_CLICK_TAP || TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_CLICK_DAILY || TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_CLICK_DAILY || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_CLICK_DAILY || TutorialManager.CurStep == TUTORIAL_STEP.TOWER_CLICK_DAILY || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_CLICK_TAB || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_CLICK_DAILY || TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_CLICK_TAB)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004934 RID: 18740 RVA: 0x00179FB0 File Offset: 0x001781B0
	private void OnEnable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.CheckBtnColor));
	}

	// Token: 0x06004935 RID: 18741 RVA: 0x00179FE0 File Offset: 0x001781E0
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.CheckBtnColor));
		FunctionTipsRootLogic.RemoveFunctionTips(base.gameObject);
		this.ClearTutorialEvent();
	}

	// Token: 0x06004936 RID: 18742 RVA: 0x0017A014 File Offset: 0x00178214
	private void CheckBtnColor()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(this.mCurBtnInfo.FunctionType))
		{
			if (this.BtnIconSprite != null)
			{
				this.BtnIconSprite.color = Color.white;
			}
			this.BtnNameLabel.color = Color.white;
		}
		else
		{
			if (this.BtnIconSprite != null)
			{
				this.BtnIconSprite.color = GameDefine.GrayColor;
			}
			this.BtnNameLabel.color = GameDefine.GrayColor;
		}
	}

	// Token: 0x04003661 RID: 13921
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003662 RID: 13922
	public UISprite BtnIconSprite;

	// Token: 0x04003663 RID: 13923
	public UILabel BtnNameLabel;

	// Token: 0x04003664 RID: 13924
	public UISprite BtnToggle;

	// Token: 0x04003665 RID: 13925
	public UIWidget BtnWidget;

	// Token: 0x04003666 RID: 13926
	public UISprite TipsSp;

	// Token: 0x04003667 RID: 13927
	public bool isTips;

	// Token: 0x04003668 RID: 13928
	private MenuTabBtnInfo mCurBtnInfo;

	// Token: 0x04003669 RID: 13929
	private bool mIsBtnEnable;

	// Token: 0x0400366A RID: 13930
	private int unlockLevel;
}
