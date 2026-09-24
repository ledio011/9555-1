using System;
using System.Collections.Generic;
using System.Text;
using SprotoType;
using UnityEngine;

// Token: 0x02000919 RID: 2329
public class FunctionBtnRootLogic : SingletonUnity<FunctionBtnRootLogic>
{
	// Token: 0x17000F98 RID: 3992
	// (get) Token: 0x0600403F RID: 16447 RVA: 0x0012D99C File Offset: 0x0012BB9C
	public bool IsOpenRightBtn
	{
		get
		{
			return this.isOpenRightBtn;
		}
	}

	// Token: 0x17000F99 RID: 3993
	// (get) Token: 0x06004040 RID: 16448 RVA: 0x0012D9A4 File Offset: 0x0012BBA4
	public bool IsOpenLeftBtn
	{
		get
		{
			return this.isOpenLeftBtn;
		}
	}

	// Token: 0x06004041 RID: 16449 RVA: 0x0012D9AC File Offset: 0x0012BBAC
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004042 RID: 16450 RVA: 0x0012D9B8 File Offset: 0x0012BBB8
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06004043 RID: 16451 RVA: 0x0012D9D8 File Offset: 0x0012BBD8
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x17000F9A RID: 3994
	// (get) Token: 0x06004044 RID: 16452 RVA: 0x0012D9E4 File Offset: 0x0012BBE4
	private List<PlayerChatHistoryInfo> mHistoryList
	{
		get
		{
			if (this.mHistoryListCache == null)
			{
				this.mHistoryListCache = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChatHistory.ChatHistoryList;
			}
			return this.mHistoryListCache;
		}
	}

	// Token: 0x06004045 RID: 16453 RVA: 0x0012DA1C File Offset: 0x0012BC1C
	private void Start()
	{
		this.UpdateAutoBtn();
		this.mCurSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChatHistory.InitOfflineChat();
		this.missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
	}

	// Token: 0x06004046 RID: 16454 RVA: 0x0012DA84 File Offset: 0x0012BC84
	public void Reset()
	{
		this.refershBtn();
		this.ResetRightBtn();
		this.isOpenRightBtn = true;
		this.MountCDSprite.fillAmount = 0f;
		this.UpdateMessage();
		this.ResetChatState();
		this.ShowAutoCombo(this.mMainPlayer.GetAutoCombatState());
	}

	// Token: 0x06004047 RID: 16455 RVA: 0x0012DAD4 File Offset: 0x0012BCD4
	public void OnReceiveMessage()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.UpdateMessage();
		}
	}

	// Token: 0x06004048 RID: 16456 RVA: 0x0012DAEC File Offset: 0x0012BCEC
	public void UpdateMessage()
	{
		if (this.mHistoryList.Count == 0)
		{
			return;
		}
		this.textLabel.UpdateNGUIText();
		this.outPutStr.Length = 0;
		int num = 0;
		string text = null;
		for (int i = this.mHistoryList.Count - 1; i >= 0; i--)
		{
			if (this.IsAcceptChannel(this.mHistoryList[i].ChannelType))
			{
				NGUIText.WrapText(this.FormatStr(this.mHistoryList[i]), out text);
				if (text.get_Chars(text.Length - 1) != '\n')
				{
					text = string.Format("{0}\n", text);
				}
				num += this.GetLineCount(text);
				if (num > this.MaxLineCount)
				{
					int num2 = num - this.MaxLineCount;
					if (num2 > 0)
					{
						text = text.Substring(this.GetIndexOfCount(text, '\n', num2) + 1);
						this.outPutStr.Insert(0, text);
					}
					break;
				}
				this.outPutStr.Insert(0, text);
				if (num == this.MaxLineCount)
				{
					break;
				}
			}
		}
		if (this.outPutStr.Length > 0)
		{
			this.outPutStr.Length = this.outPutStr.Length - 1;
		}
		this.textLabel.text = this.outPutStr.ToString();
	}

	// Token: 0x06004049 RID: 16457 RVA: 0x0012DC50 File Offset: 0x0012BE50
	private int GetIndexOfCount(string str, char val, int count)
	{
		for (int i = 0; i < str.Length; i++)
		{
			if (str.get_Chars(i) == val)
			{
				count--;
				if (count <= 0)
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x0600404A RID: 16458 RVA: 0x0012DC90 File Offset: 0x0012BE90
	private int GetLineCount(string str)
	{
		int num = 0;
		for (int i = 0; i < str.Length; i++)
		{
			if (str.get_Chars(i) == '\n')
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600404B RID: 16459 RVA: 0x0012DCCC File Offset: 0x0012BECC
	public string FormatStr(PlayerChatHistoryInfo info)
	{
		this.tempStr.Length = 0;
		if (this.mPlayerData.IsNeedTranslation)
		{
			this.tempStr.AppendFormat("{0} [8FF4F3]{1}[-]: {2}\n", StrDictionary.GetDictionaryString(GameDefine.CHANNEL_PRE_WORD[(int)info.ChannelType], new object[0]), info.SenderName, info.ChatInfo2);
		}
		else
		{
			this.tempStr.AppendFormat("{0} [8FF4F3]{1}[-]: {2}\n", StrDictionary.GetDictionaryString(GameDefine.CHANNEL_PRE_WORD[(int)info.ChannelType], new object[0]), info.SenderName, info.ChatInfo);
		}
		return this.tempStr.ToString();
	}

	// Token: 0x0600404C RID: 16460 RVA: 0x0012DD70 File Offset: 0x0012BF70
	private bool IsAcceptChannel(GameDefine.CHAT_CHANNEL_TYPE type)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return playerData == null || playerData.IsAcceptChannel(type);
	}

	// Token: 0x0600404D RID: 16461 RVA: 0x0012DD98 File Offset: 0x0012BF98
	public void SetChannelAccept(GameDefine.CHAT_CHANNEL_TYPE type, bool IsAccept)
	{
		this.AcceptChannelType[(int)type] = IsAccept;
	}

	// Token: 0x0600404E RID: 16462 RVA: 0x0012DDA4 File Offset: 0x0012BFA4
	public void OnClickOpenChatBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChatRoot, new UIManager.OnOpenUIDelegate(this.OnChatRootShow), null);
	}

	// Token: 0x0600404F RID: 16463 RVA: 0x0012DDC4 File Offset: 0x0012BFC4
	public void HideShowCarUI()
	{
		NGUITools.SetActive(this.AutoBtnSprite.gameObject, false);
		NGUITools.SetActive(this.ActionBtnIcon.gameObject, false);
	}

	// Token: 0x06004050 RID: 16464 RVA: 0x0012DDF4 File Offset: 0x0012BFF4
	private void OnChatRootShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			SingletonUnity<ChatUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChoosedChannelType);
		}
	}

	// Token: 0x06004051 RID: 16465 RVA: 0x0012DE20 File Offset: 0x0012C020
	private void CheckFunctionBtn(UISprite btnIcon, FUNCTION_TYPE funcType)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(funcType))
		{
			NGUITools.SetActive(btnIcon.gameObject, true);
			btnIcon.alpha = 1f;
		}
		else
		{
			NGUITools.SetActive(btnIcon.gameObject, false);
		}
	}

	// Token: 0x06004052 RID: 16466 RVA: 0x0012DE6C File Offset: 0x0012C06C
	public void OnClickOpenActivity()
	{
		switch (this.TipsActivityType)
		{
		case GameDefine.ACTIVITY_TYPE.CITY_DANCE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.CITY_DANCE, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.BAR_FIGHT:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.BAR_FIGHT, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.WILD_BOSS:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.WILD_BOSS, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BOSS:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
			}, null);
			break;
		case GameDefine.ACTIVITY_TYPE.GUILD_BATTLE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE, null, false);
			}, null);
			break;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "PopActivityBtn");
	}

	// Token: 0x06004053 RID: 16467 RVA: 0x0012DFF0 File Offset: 0x0012C1F0
	public void refershBtn()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			NGUITools.SetActive(this.MountCarBtn.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.MountCarBtn.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CAR));
		}
		this.ResetLeftBtn();
		this.isOpenLeftBtn = true;
		this.LeftBtnRotationTween.ResetToBeginning();
		this.ShowAutoBtn();
		NGUITools.SetActive(this.StrongerIcon.gameObject, false);
		this.UpdateAutoBtn();
		this.DisableEscortBtn();
		this.UpdateTips();
		this.UpdateUnlockTips();
		this.EnableBtnColor(this.TopBtnColorList);
		this.EnableBtnColor(this.RightBtnColorList);
	}

	// Token: 0x06004054 RID: 16468 RVA: 0x0012E0AC File Offset: 0x0012C2AC
	public void UpdateUnlockTips()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.MenuTabBtnTipIdList.Contains(4012.ToString()) || playerCommonData.MenuTabBtnTipIdList.Contains(4013.ToString()) || playerCommonData.MenuTabBtnTipIdList.Contains(3022.ToString()))
		{
			FunctionTipsRootLogic.AddFunctionTips(this.GiftFuncBtn.gameObject, Vector3.zero, -1f);
		}
		else
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.GiftFuncBtn.gameObject);
		}
		if (playerCommonData.MenuTabBtnTipIdList.Contains(4043.ToString()))
		{
			FunctionTipsRootLogic.AddFunctionTips(this.EnhanceFuncBtn.gameObject, Vector3.zero, -1f);
		}
		else
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.EnhanceFuncBtn.gameObject);
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			if (playerCommonData.MenuTabBtnTipIdList.Contains(4061.ToString()) || playerCommonData.MenuTabBtnTipIdList.Contains(4062.ToString()))
			{
				FunctionTipsRootLogic.AddFunctionTips(this.SocialFuncBtn.gameObject, Vector3.zero, -1f);
			}
			else
			{
				FunctionTipsRootLogic.RemoveFunctionTips(this.SocialFuncBtn.gameObject);
			}
		}
		if (playerCommonData.MenuTabBtnTipIdList.Contains(3021.ToString()))
		{
			FunctionTipsRootLogic.AddFunctionTips(this.AutoBtnSprite.gameObject, Vector3.zero, -1f);
		}
		else
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.AutoBtnSprite.gameObject);
		}
	}

	// Token: 0x06004055 RID: 16469 RVA: 0x0012E264 File Offset: 0x0012C464
	public void HideBigSaleBtn()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		playerCommonData.Big_PackFlag = false;
		NGUITools.SetActive(this.BigSaleFuncBtn.gameObject, false);
		this.LeftBtnGride2.Reposition();
	}

	// Token: 0x06004056 RID: 16470 RVA: 0x0012E2A0 File Offset: 0x0012C4A0
	public void HideMysterySaleBtn()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.Push != -1L && (playerCommonData.Push & 16L) != 0L)
		{
			playerCommonData.Push -= 16L;
		}
		NGUITools.SetActive(this.MysteryFuncBtn.gameObject, false);
		this.LeftBtnGride2.Reposition();
	}

	// Token: 0x06004057 RID: 16471 RVA: 0x0012E300 File Offset: 0x0012C500
	public void HideFirstSaleBtn()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		playerCommonData.First_PackFlag = false;
		NGUITools.SetActive(this.FirstSaleFuncBtn.gameObject, false);
		this.LeftBtnGride2.Reposition();
	}

	// Token: 0x06004058 RID: 16472 RVA: 0x0012E33C File Offset: 0x0012C53C
	public void UpdateMenu(bool resetNow = false)
	{
		this.ResetRightBtn();
		this.EnableBtnColor(this.RightBtnColorList);
	}

	// Token: 0x06004059 RID: 16473 RVA: 0x0012E350 File Offset: 0x0012C550
	public void ResetRightBtn()
	{
		this.CarFuncBtn.SetState(FUNCTION_TYPE.CAR, true);
		this.ShopFuncBtn.SetState(FUNCTION_TYPE.SHOP, false);
		this.EnhanceFuncBtn.SetState(FUNCTION_TYPE.ENHANCE, true);
		this.BagFuncBtn.SetState(FUNCTION_TYPE.BAG, true);
		this.GuildFuncBtn.SetState(FUNCTION_TYPE.GUILD, true);
		this.ShowDownLoadLaterBtn();
		this.bottomGrid.Reposition();
		this.ChatGrid.Reposition();
	}

	// Token: 0x0600405A RID: 16474 RVA: 0x0012E3D0 File Offset: 0x0012C5D0
	public void ResetLeftBtn()
	{
		this.RankFuncBtn.SetState(FUNCTION_TYPE.RANK, true);
		this.SlotFuncBtn.SetState(FUNCTION_TYPE.LOTTO, false);
		this.GiftFuncBtn.SetState(FUNCTION_TYPE.GIFT, false);
		this.BigSaleFuncBtn.SetState(FUNCTION_TYPE.BIGSALES, false);
		this.FirstSaleFuncBtn.SetState(FUNCTION_TYPE.FIRST_PAY, false);
		this.MysteryFuncBtn.SetState(FUNCTION_TYPE.MYSTERYSHOP, false);
		this.CheckLeftBtn();
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (!playerCommonData.Big_PackFlag)
		{
			NGUITools.SetActive(this.BigSaleFuncBtn.gameObject, false);
		}
		if (!playerCommonData.First_PackFlag)
		{
			NGUITools.SetActive(this.FirstSaleFuncBtn.gameObject, false);
		}
		if (playerCommonData.Push != -1L && (playerCommonData.Push & 16L) == 0L)
		{
			NGUITools.SetActive(this.MysteryFuncBtn.gameObject, false);
		}
		this.LeftBtnGride1.Reposition();
		this.LeftBtnGride2.Reposition();
	}

	// Token: 0x0600405B RID: 16475 RVA: 0x0012E4D0 File Offset: 0x0012C6D0
	public void ShowDownLoadLaterBtn()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.SocialFuncBtn.SetState(FUNCTION_TYPE.SOCIAL, false);
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				NGUITools.SetActive(this.ActionBtnIcon.gameObject, true);
			}
			else
			{
				NGUITools.SetActive(this.ActionBtnIcon.gameObject, false);
			}
			this.bottomGrid.Reposition();
			this.ChatGrid.Reposition();
		}
	}

	// Token: 0x0600405C RID: 16476 RVA: 0x0012E550 File Offset: 0x0012C750
	public void CheckLeftBtn()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_ACTIVITY) || playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT) || playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.FIRST_PAY) || playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.BIGSALES) || playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.MYSTERYSHOP) || playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.LOTTO) || playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP))
		{
			this.LeftBtnSp.alpha = 1f;
		}
		else
		{
			this.LeftBtnSp.alpha = 0f;
		}
	}

	// Token: 0x0600405D RID: 16477 RVA: 0x0012E600 File Offset: 0x0012C800
	public void ShowAutoCombo(bool isshow)
	{
		if (this.isShowAutoCombo != isshow)
		{
			this.isShowAutoCombo = isshow;
			if (this.isShowAutoCombo)
			{
				this.AutoComboWi.alpha = 1f;
			}
			else
			{
				this.AutoComboWi.alpha = 0f;
			}
		}
	}

	// Token: 0x0600405E RID: 16478 RVA: 0x0012E650 File Offset: 0x0012C850
	public void ResetChatState()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetChatShow == 1)
		{
			this.ChatOffsetAnima.ResetToBeginning();
			this.ChatOffsetAnima.enabled = false;
			this.IsOpenChatPage = true;
		}
		else
		{
			this.ChatOffsetAnima.PlayForward();
			this.ChatOffsetAnima.transform.localPosition = this.ChatOffsetAnima.to;
			this.ChatOffsetAnima.enabled = false;
			this.IsOpenChatPage = false;
		}
		this.ChatFuncBtn.SetDirState(this.IsOpenChatPage);
	}

	// Token: 0x0600405F RID: 16479 RVA: 0x0012E6E0 File Offset: 0x0012C8E0
	public void OnClickChatBtn()
	{
		if (this.IsOpenChatPage)
		{
			this.ChatOffsetAnima.PlayForward();
			this.ChatFuncBtn.ShowDirAnima(true);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetChatShow(0);
		}
		else
		{
			this.ChatOffsetAnima.PlayReverse();
			this.ChatFuncBtn.ShowDirAnima(false);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetChatShow(1);
		}
		this.IsOpenChatPage = !this.IsOpenChatPage;
	}

	// Token: 0x06004060 RID: 16480 RVA: 0x0012E75C File Offset: 0x0012C95C
	public void OnClickSocialDance()
	{
		if (SingletonUnity<SocialDanceUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialDanceUIRoot>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SocialDanceRoot);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SocialDanceRoot, delegate(bool bSuccess, object param)
			{
				if (bSuccess)
				{
					SingletonUnity<SocialDanceUIRoot>.Instance.Reset();
				}
			}, null);
		}
	}

	// Token: 0x06004061 RID: 16481 RVA: 0x0012E7C8 File Offset: 0x0012C9C8
	public void EnableDanceBtn()
	{
		if (!SingletonUnity<DanceBtnRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<DanceBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DanceBtnRoot, delegate
			{
				SingletonUnity<DanceBtnRootLogic>.Instance.Reset();
				this.mEnterDanceAreaTime = Time.time;
				this.ShowAutoBtn();
				if (SingletonUnity<MissionTeamTipLogic>.Exists)
				{
					SingletonUnity<MissionTeamTipLogic>.Instance.ChangeToDanceMission(true);
				}
			}, null);
		}
		if (SingletonUnity<PotionLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PotionLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PotionObjRoot);
		}
	}

	// Token: 0x06004062 RID: 16482 RVA: 0x0012E83C File Offset: 0x0012CA3C
	public void DisableDanceBtn()
	{
		if (SingletonUnity<DanceBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DanceBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DanceBtnRoot);
			this.ShowAutoBtn();
			if (SingletonUnity<MissionTeamTipLogic>.Exists)
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.ChangeToDanceMission(false);
			}
		}
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsCanUsePotion() && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PotionObjRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<PotionLogic>.Instance.Reset();
			}, null);
		}
	}

	// Token: 0x06004063 RID: 16483 RVA: 0x0012E8E8 File Offset: 0x0012CAE8
	public void EnableEscortBtn()
	{
		if (!UnityVersionUtil.IsActive(this.FollowEscortBtnScale.gameObject))
		{
			NGUITools.SetActive(this.FollowEscortBtnScale.gameObject, true);
			this.DynamicBtnGride.Reposition();
			this.ShowAutoBtn();
			this.UpdateFollowEscortBtn();
			if (this.mMainPlayer.IsOpenAutoCombat)
			{
				this.mMainPlayer.LeveAutoCombat();
			}
		}
	}

	// Token: 0x06004064 RID: 16484 RVA: 0x0012E950 File Offset: 0x0012CB50
	public void DisableEscortBtn()
	{
		if (UnityVersionUtil.IsActive(this.FollowEscortBtnScale.gameObject))
		{
			NGUITools.SetActive(this.FollowEscortBtnScale.gameObject, false);
			this.DynamicBtnGride.Reposition();
			this.ShowAutoBtn();
		}
	}

	// Token: 0x06004065 RID: 16485 RVA: 0x0012E994 File Offset: 0x0012CB94
	public void OnClickMenuBtn()
	{
		this.UpdateMenu(false);
	}

	// Token: 0x06004066 RID: 16486 RVA: 0x0012E9A0 File Offset: 0x0012CBA0
	public void OnClickActivityBtn()
	{
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
			if (TutorialManager.CurStep == TUTORIAL_STEP.EXP_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.CAR_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.GOLD_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.TOWER_START || TutorialManager.CurStep == TUTORIAL_STEP.SCUFFLE_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_START || TutorialManager.CurStep == TUTORIAL_STEP.EQUIP_COPY_START || TutorialManager.CurStep == TUTORIAL_STEP.BAR_FIGHT_START || TutorialManager.CurStep == TUTORIAL_STEP.ESCORT_START || TutorialManager.CurStep == TUTORIAL_STEP.ROBBORY_START || TutorialManager.CurStep == TUTORIAL_STEP.SURVIVAL_BATTLE_START || TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_START || TutorialManager.CurStep == TUTORIAL_STEP.RANK_PVP_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
	}

	// Token: 0x06004067 RID: 16487 RVA: 0x0012E9F4 File Offset: 0x0012CBF4
	public void OnClickAuctionBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ConsignUIRoot, new UIManager.OnOpenUIDelegate(this.OnShowConsignRoot), null);
	}

	// Token: 0x06004068 RID: 16488 RVA: 0x0012EA14 File Offset: 0x0012CC14
	private void OnShowConsignRoot(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			SingletonUnity<ConsignRootLogic>.Instance.Reset();
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.SELL_ITEM_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004069 RID: 16489 RVA: 0x0012EA3C File Offset: 0x0012CC3C
	public void OnClickGiftBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.GIFT))
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList.Contains(3022.ToString()))
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.AutoBtnSprite.gameObject);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList.Remove(3022.ToString());
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.GIFT_TIP);
			this.UpdateUnlockTips();
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CommercialUIRoot, delegate
		{
			SingletonUnity<CommercialUIRootLogic>.Instance.Reset();
		}, null);
	}

	// Token: 0x0600406A RID: 16490 RVA: 0x0012EAFC File Offset: 0x0012CCFC
	public void OnClickDailyActivity()
	{
	}

	// Token: 0x0600406B RID: 16491 RVA: 0x0012EB00 File Offset: 0x0012CD00
	public void OnClickStrongerBtn()
	{
	}

	// Token: 0x0600406C RID: 16492 RVA: 0x0012EB04 File Offset: 0x0012CD04
	public void OnClickLottoBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.LOTTO))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotUIRoot, delegate
		{
			SingletonUnity<SlotUIRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(242, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
			if (TutorialManager.CurStep == TUTORIAL_STEP.SLOT_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
	}

	// Token: 0x0600406D RID: 16493 RVA: 0x0012EB34 File Offset: 0x0012CD34
	public void OnClickRankBnt()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.RANK))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.Reset();
		}, null);
	}

	// Token: 0x0600406E RID: 16494 RVA: 0x0012EB80 File Offset: 0x0012CD80
	public void OnClickSalesBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.FIRST_PAY))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FirstBuyRoot, delegate
		{
			SingletonUnity<FirstBuyRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(259, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_first_buy>(null, null);
		}, null);
	}

	// Token: 0x0600406F RID: 16495 RVA: 0x0012EBCC File Offset: 0x0012CDCC
	public void OnClickBigSalesBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.BIGSALES))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BigPackRoot, delegate
		{
			SingletonUnity<BigPackRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(260, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_big_pack>(null, null);
		}, null);
	}

	// Token: 0x06004070 RID: 16496 RVA: 0x0012EC18 File Offset: 0x0012CE18
	public void OnClickMysteryShopBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.MYSTERYSHOP))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MysteryShopRoot, delegate
		{
			SingletonUnity<MysteryShopRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(274, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_special_big_pack>(null, null);
		}, null);
	}

	// Token: 0x06004071 RID: 16497 RVA: 0x0012EC64 File Offset: 0x0012CE64
	public void OnClickSevenDayBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SignWeekRoot, delegate
		{
			SingletonUnity<SignWeekRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(253, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_week_info>(null, null);
		}, null);
	}

	// Token: 0x06004072 RID: 16498 RVA: 0x0012EC94 File Offset: 0x0012CE94
	public void OnClickShopBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.SHOP))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_START)
			{
				if (GameManager.IsSupportCurDataVersion145())
				{
					SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn(GameDefine.SHOP_TAB_TYPE.ITEM, GameDefine.UIBACKTYPE.NOTHINTG, "9999");
					this.CheckTutorialEvent();
				}
				else
				{
					SingletonUnity<ShopUIRootLogic>.Instance.Reset();
				}
			}
			else
			{
				SingletonUnity<ShopUIRootLogic>.Instance.Reset();
			}
		}, null);
	}

	// Token: 0x06004073 RID: 16499 RVA: 0x0012ECC4 File Offset: 0x0012CEC4
	public void OnClickCoinShopBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExchangeShopRoot, delegate
		{
			SingletonUnity<ExchangeShopRootLogic>.Instance.Reset();
		}, null);
	}

	// Token: 0x06004074 RID: 16500 RVA: 0x0012ECF4 File Offset: 0x0012CEF4
	public void OnClickMailBtn()
	{
		SingletonUnity<SocialUIRootLogic>.Instance.ShowSocialUI();
		SingletonUnity<SocialUIRootLogic>.Instance.OnClickMailBtn();
	}

	// Token: 0x06004075 RID: 16501 RVA: 0x0012ED0C File Offset: 0x0012CF0C
	public void UpdateFollowEscortBtn()
	{
		if (!UnityVersionUtil.IsActive(this.FollowEscortBtnScale.gameObject))
		{
			return;
		}
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (this.mMainPlayer.IsTeamFollowState())
		{
			this.FollowEscortBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_ZiDongGenSui_2";
			this.FollowEffect.alpha = 1f;
		}
		else
		{
			this.FollowEscortBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_ZiDongGenSui_1";
			this.FollowEffect.alpha = 0f;
		}
		this.FollowEscortBtnScale.ResetToBeginning();
		this.FollowEscortBtnScale.enabled = this.mMainPlayer.IsTeamFollowState();
	}

	// Token: 0x06004076 RID: 16502 RVA: 0x0012EDB8 File Offset: 0x0012CFB8
	public void UpdateAutoBtn()
	{
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (this.mMainPlayer.IsOpenAutoCombat)
		{
			this.AutoBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_ZiDong_2";
			this.AutoEffect.alpha = 1f;
		}
		else
		{
			this.AutoBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_ZiDong";
			this.AutoEffect.alpha = 0f;
		}
		this.AutoBtnScale.ResetToBeginning();
		this.AutoBtnScale.enabled = this.mMainPlayer.IsOpenAutoCombat;
	}

	// Token: 0x06004077 RID: 16503 RVA: 0x0012EE70 File Offset: 0x0012D070
	public void OnClickAutoCombo()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList.Contains(3021.ToString()))
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.AutoBtnSprite.gameObject);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList.Remove(3021.ToString());
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.AUTO_FIGHT_TIP);
			this.UpdateUnlockTips();
		}
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (!this.mMainPlayer.IsOpenAutoCombat)
		{
			this.mMainPlayer.EnterAutoCombat();
		}
		else
		{
			this.mMainPlayer.LeveAutoCombat();
		}
		this.UpdateAutoBtn();
	}

	// Token: 0x06004078 RID: 16504 RVA: 0x0012EF34 File Offset: 0x0012D134
	public void OnClickLeftArrow()
	{
		this.OnClickLeftArrowBtn(false);
	}

	// Token: 0x06004079 RID: 16505 RVA: 0x0012EF40 File Offset: 0x0012D140
	public void OnClickLeftArrowBtn(bool resetNow = false)
	{
		if (this.isOpenLeftBtn)
		{
			this.LeftBtnRotationTween.PlayForward();
			this.LeftBtnGride1.CloseGrid();
			this.LeftBtnGride2.CloseGrid();
			this.DisableBtnColor(this.TopBtnColorList);
		}
		else
		{
			this.LeftBtnRotationTween.PlayReverse();
			this.ResetLeftBtn();
			if (!resetNow)
			{
				this.LeftBtnGride1.OpenGrid();
				this.LeftBtnGride2.OpenGrid();
			}
			this.EnableBtnColor(this.TopBtnColorList);
		}
		this.isOpenLeftBtn = !this.isOpenLeftBtn;
	}

	// Token: 0x0600407A RID: 16506 RVA: 0x0012EFD8 File Offset: 0x0012D1D8
	public void OnClickGangBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.GUILD))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, delegate
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "GuildBtn");
	}

	// Token: 0x0600407B RID: 16507 RVA: 0x0012F02C File Offset: 0x0012D22C
	public void OnClickSkillBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.SKILL))
		{
			return;
		}
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GameMenuSkillInfoRootUI, delegate
		{
			SingletonUnity<SkillInfoRootLogic>.Instance.Reset();
			if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_START || TutorialManager.CurStep == TUTORIAL_STEP.SKILL_DRAG_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "SkillBtn");
	}

	// Token: 0x0600407C RID: 16508 RVA: 0x0012F0A8 File Offset: 0x0012D2A8
	public void OnClickCharacterBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
		{
			if (isSuccess)
			{
			}
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickEquipBackPackBtn();
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "CharacterBtn");
	}

	// Token: 0x0600407D RID: 16509 RVA: 0x0012F0FC File Offset: 0x0012D2FC
	public void OnClickBagBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.BAG))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_START)
			{
				this.CheckTutorialEvent();
			}
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickEquipBackPackBtn();
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "ItemBtn");
	}

	// Token: 0x0600407E RID: 16510 RVA: 0x0012F150 File Offset: 0x0012D350
	public void OnClickSocialBtn()
	{
		SingletonUnity<SocialUIRootLogic>.Instance.ShowSocialUI();
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL_FRIEND))
		{
			if (!SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap())
			{
				SingletonUnity<SocialUIRootLogic>.Instance.SelectFriendInfobtn();
			}
		}
		else
		{
			SingletonUnity<SocialUIRootLogic>.Instance.OnClickMailBtn();
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "SocialBtn");
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x0012F1C4 File Offset: 0x0012D3C4
	public void OnClickTitleBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.TITLE))
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TitleUIRootLogic, delegate
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.TITLE_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "TitleBtn");
	}

	// Token: 0x06004080 RID: 16512 RVA: 0x0012F218 File Offset: 0x0012D418
	public void OnClickEnhaceBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.ENHANCE))
		{
			return;
		}
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<EquipStrengthenUIRootLogic>.Instance.Reset();
			if (TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_START || TutorialManager.CurStep == TUTORIAL_STEP.STRENGTH_STAR_START || TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_ALL_START || TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_START || TutorialManager.CurStep == TUTORIAL_STEP.TITLE_START)
			{
				this.CheckTutorialEvent();
			}
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "EnhanceBtn");
	}

	// Token: 0x06004081 RID: 16513 RVA: 0x0012F294 File Offset: 0x0012D494
	public void OnClickEscortFollowBtn()
	{
		ObjManager instance = Singleton<ObjManager>.Instance;
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		AutoSearchPathManager autoSearchPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		CurMission escortMission = missionManager.GetEscortMission();
		string empty = string.Empty;
		Vector3 escortNpcPos = missionManager.GetEscortNpcPos(out empty);
		if (!this.mCurSceneManager.CurrentMapInofData.ID.Equals(empty))
		{
			autoSearchPath.FindPath(new AutoSearchPathPoint(empty, escortNpcPos), AUTO_SEARCH_PARTH_FINISHEVENT.MISSION, escortMission.MissionId);
			if (autoSearchPath.CurPath != null && autoSearchPath.CurPath.PathPointList.Count > 0)
			{
				this.mMainPlayer.MoveTo(autoSearchPath.CurPath.PathPointList[0].PosX, autoSearchPath.CurPath.PathPointList[0].PosZ, 1f, null);
			}
			return;
		}
		if (!this.mMainPlayer.IsTeamFollowState())
		{
			long npcId = escortMission.GetParam(1);
			if (instance.ObjDict.ContainsKey(npcId))
			{
				this.mMainPlayer.EnterFollowTarget(npcId);
			}
			else
			{
				this.mMainPlayer.MoveTo(escortNpcPos, 1f, delegate(ObjCharacter A_1)
				{
					this.mMainPlayer.EnterFollowTarget(npcId);
					this.UpdateFollowEscortBtn();
				});
			}
		}
		else
		{
			this.mMainPlayer.LeaveTeamFollow();
		}
		this.UpdateFollowEscortBtn();
		if (!this.mMainPlayer.IsOpenAutoCombat)
		{
			this.OnClickAutoCombo();
		}
	}

	// Token: 0x06004082 RID: 16514 RVA: 0x0012F40C File Offset: 0x0012D60C
	public bool CheckDanceLevel()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		copyscene_info copyinfoByType = playerData.CopyInfoData.GetCopyinfoByType(26);
		string text = null;
		if (copyinfoByType != null)
		{
			text = copyinfoByType.ID;
		}
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(text);
		return playerData.CheckLevel(cityDanceDataById.UnlockLevel);
	}

	// Token: 0x06004083 RID: 16515 RVA: 0x0012F46C File Offset: 0x0012D66C
	public void OnClickCarBtn()
	{
		if (!this.isUnlockFun(FUNCTION_TYPE.CAR))
		{
			return;
		}
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerCarRoot, delegate
		{
			SingletonUnity<PlayerCarRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(235, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.request_mount_info>(null, null);
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("MainUI", "BtnClick", "CarBtn");
	}

	// Token: 0x06004084 RID: 16516 RVA: 0x0012F4F8 File Offset: 0x0012D6F8
	public bool isUnlockFun(FUNCTION_TYPE functype)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(functype))
		{
			return true;
		}
		int num = (int)functype;
		int condition = DataManager.GetFunctionDataById(num.ToString()).Condition;
		NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
		{
			condition
		}), true, false);
		return false;
	}

	// Token: 0x06004085 RID: 16517 RVA: 0x0012F554 File Offset: 0x0012D754
	private void UpdateMountTime()
	{
		if (this.mMountTime > 0f)
		{
			this.mMountTime -= Time.deltaTime;
			if (this.mMountTime <= 0f)
			{
				this.mMountTime = -1f;
			}
			this.MountCDSprite.fillAmount = Mathf.Clamp01(this.mMountTime / 3f);
		}
	}

	// Token: 0x06004086 RID: 16518 RVA: 0x0012F5BC File Offset: 0x0012D7BC
	public void OnClickMountBtn()
	{
		if (this.mMainPlayer == null || this.mMainPlayer.SkillLogic.IsUsingSkill || this.mMainPlayer.IsDie || this.mMainPlayer.IsStun())
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_CLICK_MOUNT)
		{
			this.CheckTutorialEvent();
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CAR))
		{
			NoticeLogic.AddNotifyData("#{100642}", true, false);
			return;
		}
		if (this.mMountTime > 0f)
		{
			return;
		}
		this.mMountTime = 3f;
		if (this.mMainPlayer.IsDrivingMount())
		{
			this.mMainPlayer.SendServerDisMountCar();
		}
		else if (string.IsNullOrEmpty(this.mMainPlayer.MountId))
		{
			this.OnClickCarBtn();
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101557}", new object[0]), true, false);
		}
		else
		{
			this.mMainPlayer.SendServerMountCar();
		}
	}

	// Token: 0x17000F9B RID: 3995
	// (set) Token: 0x06004087 RID: 16519 RVA: 0x0012F6C8 File Offset: 0x0012D8C8
	public float LastSendServerTimeCheck
	{
		set
		{
			this.mLastSendServerTimeCheck = value;
		}
	}

	// Token: 0x06004088 RID: 16520 RVA: 0x0012F6D4 File Offset: 0x0012D8D4
	private void Update()
	{
		this.UpdateMountTime();
		if (this.mCurSceneManager.IsCiytDanceSceneActive())
		{
			if (this.mCurSceneManager.IsInGatherArea(this.mMainPlayer.Position))
			{
				this.EnableDanceBtn();
				if (Time.time - this.mEnterDanceAreaTime > (float)this.DANCE_WAIT_TIME)
				{
					if (this.mMainPlayer.CurPlayerState != PLAYER_STATE.DANCE)
					{
						if (this.mMainPlayer.IsMoving || this.mMainPlayer.SkillLogic.IsUsingSkill)
						{
							this.mEnterDanceAreaTime = Time.time;
						}
						else
						{
							if (!this.CheckDanceLevel())
							{
								return;
							}
							if (Time.time - this.mLastSendServerTimeCheck > this.mSendSerTimeInterval)
							{
								if (this.mPlayerData.PlayerDanceData.IsHaveDanceData() && this.mPlayerData.ActivityData.IsCanDance())
								{
									if (this.mPlayerData.PlayerDanceData.IsSpecialDanceDataEnable())
									{
										use_dance.request request = new use_dance.request();
										request.id = this.mPlayerData.PlayerDanceData.CurSpecialDanceData.ID;
										NetLogic.GetInstance().Send<Protocol.use_dance>(request, null);
										this.mLastSendServerTimeCheck = Time.time;
									}
									else
									{
										use_dance.request request2 = new use_dance.request();
										request2.id = this.mPlayerData.PlayerDanceData.CurNormalDanceData.ID;
										NetLogic.GetInstance().Send<Protocol.use_dance>(request2, null);
										this.mLastSendServerTimeCheck = Time.time;
									}
								}
								else
								{
									this.mLastSendServerTimeCheck = Time.time;
								}
							}
						}
					}
					else
					{
						this.mEnterDanceAreaTime = Time.time;
					}
				}
			}
			else
			{
				this.DisableDanceBtn();
			}
		}
		if (UnityVersionUtil.IsActive(this.FollowEscortBtnScale.gameObject) && this.FollowEscortBtnScale.enabled && !this.mMainPlayer.IsTeamFollowState())
		{
			this.UpdateFollowEscortBtn();
		}
		if (this.mCurSceneManager.isHaveSaftyArea)
		{
			if (this.mCurSceneManager.IsInSafeArea(this.mMainPlayer.Position))
			{
				if (!this.isInsafeArea)
				{
					this.isInsafeArea = true;
					NoticeLogic.AddNotifyData("#{102028}", true, false);
					this.mMainPlayer.SelectTarget(null);
				}
			}
			else if (this.isInsafeArea)
			{
				this.isInsafeArea = false;
				NoticeLogic.AddNotifyData("#{102029}", true, false);
			}
		}
		this.UpdateFollowFlag();
	}

	// Token: 0x06004089 RID: 16521 RVA: 0x0012F934 File Offset: 0x0012DB34
	public void UpdateFollowFlag()
	{
		if (this.missionManager.IsInEscortMission())
		{
			string empty = string.Empty;
			Vector3 escortNpcPos = this.missionManager.GetEscortNpcPos(out empty);
			if (this.mCurSceneManager.CurrentMapInofData.ID.Equals(empty))
			{
				if (!UnityVersionUtil.IsActive(this.FollowEscortBtnScale.gameObject))
				{
					this.EnableEscortBtn();
				}
			}
			else
			{
				this.DisableEscortBtn();
			}
		}
		else
		{
			this.DisableEscortBtn();
		}
	}

	// Token: 0x0600408A RID: 16522 RVA: 0x0012F9B8 File Offset: 0x0012DBB8
	public void ShowAutoBtn()
	{
		if (UnityVersionUtil.IsActive(this.FollowEscortBtnScale.gameObject) || (SingletonUnity<DanceBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DanceBtnRootLogic>.Instance.gameObject)))
		{
			if (UnityVersionUtil.IsActive(this.AutoBtnSprite.gameObject))
			{
				NGUITools.SetActive(this.AutoBtnSprite.gameObject, false);
			}
		}
		else
		{
			this.CheckFunctionBtn(this.AutoBtnSprite, FUNCTION_TYPE.AUTO_FIGHT);
			this.UpdateAutoBtn();
		}
	}

	// Token: 0x0600408B RID: 16523 RVA: 0x0012FA3C File Offset: 0x0012DC3C
	public UISprite GetBtnIconByFunctionType(FUNCTION_TYPE type)
	{
		switch (type)
		{
		case FUNCTION_TYPE.RANK:
			return this.RankFuncBtn.IconSp;
		case FUNCTION_TYPE.GIFT:
			return this.GiftFuncBtn.IconSp;
		case FUNCTION_TYPE.SHOP:
			return this.ShopFuncBtn.IconSp;
		case FUNCTION_TYPE.LOTTO:
			return this.SlotFuncBtn.IconSp;
		case FUNCTION_TYPE.FIRST_PAY:
			return this.FirstSaleFuncBtn.IconSp;
		case FUNCTION_TYPE.BIGSALES:
			return this.BigSaleFuncBtn.IconSp;
		case FUNCTION_TYPE.CAR:
			return this.CarFuncBtn.IconSp;
		case FUNCTION_TYPE.ENHANCE:
			return this.EnhanceFuncBtn.IconSp;
		case FUNCTION_TYPE.BAG:
			return this.BagFuncBtn.IconSp;
		case FUNCTION_TYPE.SOCIAL:
			return this.SocialFuncBtn.IconSp;
		case FUNCTION_TYPE.GUILD:
			return this.GuildFuncBtn.IconSp;
		case FUNCTION_TYPE.MYSTERYSHOP:
			return this.MysteryFuncBtn.IconSp;
		}
		return null;
	}

	// Token: 0x0600408C RID: 16524 RVA: 0x0012FB30 File Offset: 0x0012DD30
	public bool CheckTitleTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TITLE_TITLE))
		{
			return false;
		}
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int curTitleExp = this.mPlayerData.MainPlayerAttrData.CurTitleExp;
		int curTitleLevel = this.mPlayerData.MainPlayerAttrData.CurTitleLevel;
		if (curTitleLevel < 10 && curTitleLevel >= 0)
		{
			TitleData titleDateById = DataManager.GetTitleDateById(curTitleLevel.ToString());
			return curTitleExp >= titleDateById.EXP;
		}
		return false;
	}

	// Token: 0x0600408D RID: 16525 RVA: 0x0012FBB8 File Offset: 0x0012DDB8
	public void UpdateTitleTips()
	{
		this.UpdateEnhanceTipsFlag();
	}

	// Token: 0x0600408E RID: 16526 RVA: 0x0012FBC0 File Offset: 0x0012DDC0
	public void OnClickMessageFlag()
	{
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<GameDefine.ACTIVITY_TYPE> messagelist = new List<GameDefine.ACTIVITY_TYPE>();
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE);
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.BAR_FIGHT))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.BAR_FIGHT);
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.WILD_BOSS))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.WILD_BOSS);
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_BOSS))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.GUILD_BOSS);
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE);
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_DANCE))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.GUILD_DANCE);
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE))
		{
			messagelist.Add(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewMessageUIRoot, delegate
		{
			SingletonUnity<NewMessageUIRootLogic>.Instance.ResetInfo(messagelist);
		}, null);
		if (TutorialManager.CurStep == TUTORIAL_STEP.FUNCTION_TIP_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x0600408F RID: 16527 RVA: 0x0012FD14 File Offset: 0x0012DF14
	public void OnClickCityDamageBtn()
	{
		guild_map_info curinfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.GetGuildMapInfo(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
		if (curinfo != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CityDamageRoot, delegate
			{
				WaitResponseUIRootLogic.OpenWaitBox(318, 10f, 0f, null);
				request_guild_map_domine_top.request request = new request_guild_map_domine_top.request();
				request.id = curinfo.id;
				NetLogic.GetInstance().Send<Protocol.request_guild_map_domine_top>(request, null);
				SingletonUnity<CityDamageRootLogic>.Instance.EnableReset();
			}, null);
		}
	}

	// Token: 0x06004090 RID: 16528 RVA: 0x0012FD7C File Offset: 0x0012DF7C
	public void UpdateMessageTips()
	{
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.BAR_FIGHT))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.WILD_BOSS))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_BOSS))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_DANCE))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE))
		{
			this.ShowMessageTips(true);
			return;
		}
		if (this.mPlayerData.ActivityData.IsHaveMissionTimeOut())
		{
			this.ShowMessageTips(true);
			return;
		}
		if (NewMessageUIRootLogic.IsHaveTeamMessageInfo())
		{
			this.ShowMessageTips(true);
			return;
		}
		this.ShowMessageTips(false);
	}

	// Token: 0x06004091 RID: 16529 RVA: 0x0012FEA4 File Offset: 0x0012E0A4
	private void ShowMessageTips(bool istrue)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			istrue = false;
		}
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			NGUITools.SetActive(this.MessageObj, istrue);
		}
		if (istrue && !SingletonUnity<UIManager>.Instance.IsHideBaseUI)
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			if (playerCommonData.IsTutorialCanShow(FUNCTION_TYPE.TIPBTN_TUTORIAL_TIP))
			{
				playerCommonData.CheckShowUnlockFunction();
			}
		}
	}

	// Token: 0x06004092 RID: 16530 RVA: 0x0012FF1C File Offset: 0x0012E11C
	private bool IsHaveActivityTips()
	{
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return this.mPlayerData.ActivityData.IsHaveActTips() || this.mPlayerData.CopyInfoData.IsHaveDailyCopyTips() || this.mPlayerData.RankPVPData.IsHavePVPTips() || this.mPlayerData.TowerData.IsHaveTowerTips();
	}

	// Token: 0x06004093 RID: 16531 RVA: 0x0012FF8C File Offset: 0x0012E18C
	public void UpdateEnhanceTipsFlag()
	{
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CheckTips(this.mPlayerData.IsHaveEnhanceandRefineTips() || this.CheckSkillUpdateTips() || this.CheckTitleTips(), GameDefine.TIPS_TYPE.ENHANCE);
	}

	// Token: 0x06004094 RID: 16532 RVA: 0x0012FFD8 File Offset: 0x0012E1D8
	public void UpdateTips()
	{
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.UpdateMessageTips();
		this.CheckTips(this.mPlayerData.FriendInfo.IsHaveMailTips(), GameDefine.TIPS_TYPE.MAIL);
		this.CheckTips(this.mPlayerData.FriendInfo.IshavefriendApply() || this.mPlayerData.FriendInfo.IsHaveMailTips(), GameDefine.TIPS_TYPE.SOCIAL);
		this.CheckTips(this.mPlayerData.ActivityData.IsHaveGuildTips(), GameDefine.TIPS_TYPE.GUILD);
		this.CheckTips(this.mPlayerData.playerSlotData.CheckTips(), GameDefine.TIPS_TYPE.SLOT);
		this.CheckTips(this.mPlayerData.playerMountData.CheckTips(), GameDefine.TIPS_TYPE.VEHICLE);
		this.CheckTips(this.CheckTitleTips(), GameDefine.TIPS_TYPE.ACHIEVEMNT);
		this.CheckTips(this.mPlayerData.IsHaveItemTips(), GameDefine.TIPS_TYPE.ITEMS);
		this.UpdateEnhanceTipsFlag();
		this.CheckTips(this.CheckSkillUpdateTips(), GameDefine.TIPS_TYPE.SKILL);
		this.CheckTips(this.mPlayerData.welfareData.isHaveWelfareTips(), GameDefine.TIPS_TYPE.WELFARE);
		this.CheckTips(this.mPlayerData.welfareData.HaveDailyActivityTips(), GameDefine.TIPS_TYPE.DAILYACT);
		this.CheckTips(this.mPlayerData.welfareData.HaveVipTips(), GameDefine.TIPS_TYPE.SHOP);
		this.UpdateCityDamageFlag();
	}

	// Token: 0x06004095 RID: 16533 RVA: 0x0013010C File Offset: 0x0012E30C
	public void CheckTips(bool istrue, GameDefine.TIPS_TYPE typetips)
	{
		switch (typetips)
		{
		case GameDefine.TIPS_TYPE.SOCIAL:
			this.SocialFuncBtn.SetTips(istrue, FUNCTION_TYPE.SOCIAL);
			break;
		case GameDefine.TIPS_TYPE.GUILD:
			this.GuildFuncBtn.SetTips(istrue, FUNCTION_TYPE.GUILD);
			break;
		case GameDefine.TIPS_TYPE.SLOT:
			this.SlotFuncBtn.SetTips(istrue, FUNCTION_TYPE.LOTTO);
			break;
		case GameDefine.TIPS_TYPE.VEHICLE:
			this.CarFuncBtn.SetTips(istrue, FUNCTION_TYPE.CAR);
			break;
		case GameDefine.TIPS_TYPE.ITEMS:
			this.BagFuncBtn.SetTips(istrue, FUNCTION_TYPE.BAG);
			break;
		case GameDefine.TIPS_TYPE.ENHANCE:
			this.EnhanceFuncBtn.SetTips(istrue, FUNCTION_TYPE.ENHANCE);
			break;
		case GameDefine.TIPS_TYPE.WELFARE:
			this.GiftFuncBtn.SetTips(istrue, FUNCTION_TYPE.GIFT);
			break;
		case GameDefine.TIPS_TYPE.MAIL:
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL_MAIL))
			{
				if (istrue)
				{
					this.SocialFuncBtn.IconSp.spriteName = "CZ_zhuJieMianAnNiu_youJian";
					this.SocialFuncBtn.BtnLabel.text = StrDictionary.GetDictionaryString("#{100215}", new object[0]);
				}
				else
				{
					this.SocialFuncBtn.IconSp.spriteName = "CZ_zhuJieMianAnNiu_haoYou";
					this.SocialFuncBtn.BtnLabel.text = StrDictionary.GetDictionaryString("#{100115}", new object[0]);
				}
			}
			else
			{
				this.SocialFuncBtn.IconSp.spriteName = "CZ_zhuJieMianAnNiu_haoYou";
				this.SocialFuncBtn.BtnLabel.text = StrDictionary.GetDictionaryString("#{100115}", new object[0]);
			}
			break;
		case GameDefine.TIPS_TYPE.SHOP:
			this.ShopFuncBtn.SetTips(istrue, FUNCTION_TYPE.SHOP);
			break;
		}
	}

	// Token: 0x06004096 RID: 16534 RVA: 0x001302E4 File Offset: 0x0012E4E4
	public void CheckRightDirTips()
	{
	}

	// Token: 0x06004097 RID: 16535 RVA: 0x001302E8 File Offset: 0x0012E4E8
	public void UpdateCityDamageFlag()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsOpenCityCapture(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID))
		{
			NGUITools.SetActive(this.CityDamageFlag.gameObject, true);
		}
		else
		{
			NGUITools.SetActive(this.CityDamageFlag.gameObject, false);
		}
	}

	// Token: 0x06004098 RID: 16536 RVA: 0x0013034C File Offset: 0x0012E54C
	public void UpdateSkillTips()
	{
		this.UpdateEnhanceTipsFlag();
		if (SingletonUnity<EquipStrengthenUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EquipStrengthenUIRootLogic>.Instance.gameObject) && SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
	}

	// Token: 0x06004099 RID: 16537 RVA: 0x001303A8 File Offset: 0x0012E5A8
	public bool CheckSkillUpdateTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL))
		{
			return false;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		for (int i = 0; i < mainPlayer.CharacterSkillData.Count; i++)
		{
			CharacterSkillData characterSkillData = mainPlayer.CharacterSkillData[i];
			if (characterSkillData != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(characterSkillData.UnlockLevel))
			{
				int index = characterSkillData.Index;
				if (index >= 4 && index <= 6)
				{
					SkillData skillDataById = DataManager.GetSkillDataById(characterSkillData.ID);
					SkillupgradeData skillupgradeDataByLevel = DataManager.GetSkillupgradeDataByLevel(characterSkillData.Level + 1);
					if (skillDataById != null && skillDataById.IsUpgrade != 0 && skillupgradeDataByLevel != null)
					{
						if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level > characterSkillData.Level + 1 && GameMoneyHelper.GetMoneyNum(skillupgradeDataByLevel.PriceType) >= (long)skillupgradeDataByLevel.PriceValue)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600409A RID: 16538 RVA: 0x001304B0 File Offset: 0x0012E6B0
	private void EnableBtnColor(List<UIButtonColor> btnList)
	{
		for (int i = 0; i < btnList.Count; i++)
		{
			if (btnList[i] != null)
			{
				btnList[i].enabled = true;
			}
		}
	}

	// Token: 0x0600409B RID: 16539 RVA: 0x001304F4 File Offset: 0x0012E6F4
	private void DisableBtnColor(List<UIButtonColor> btnList)
	{
		for (int i = 0; i < btnList.Count; i++)
		{
			if (btnList[i] != null)
			{
				btnList[i].enabled = false;
			}
		}
	}

	// Token: 0x04002BF9 RID: 11257
	private const float MAX_MOUNT_CD = 3f;

	// Token: 0x04002BFA RID: 11258
	public TweenRotation LeftBtnRotationTween;

	// Token: 0x04002BFB RID: 11259
	public UISprite LeftBtnSp;

	// Token: 0x04002BFC RID: 11260
	public UIGridNew LeftBtnGride1;

	// Token: 0x04002BFD RID: 11261
	public UISprite ActivityBtnIcon;

	// Token: 0x04002BFE RID: 11262
	public UISprite StrongerIcon;

	// Token: 0x04002BFF RID: 11263
	public UIGridNew LeftBtnGride2;

	// Token: 0x04002C00 RID: 11264
	public UIGrid bottomGrid;

	// Token: 0x04002C01 RID: 11265
	public UIGrid ChatGrid;

	// Token: 0x04002C02 RID: 11266
	public UISprite CharacterBtnIcon;

	// Token: 0x04002C03 RID: 11267
	public UISprite ActionBtnIcon;

	// Token: 0x04002C04 RID: 11268
	public UISprite AutoBtnSprite;

	// Token: 0x04002C05 RID: 11269
	public UITweener AutoBtnScale;

	// Token: 0x04002C06 RID: 11270
	public UISprite FollowEscortBtnSprite;

	// Token: 0x04002C07 RID: 11271
	public UITweener FollowEscortBtnScale;

	// Token: 0x04002C08 RID: 11272
	public UIGrid DynamicBtnGride;

	// Token: 0x04002C09 RID: 11273
	public UISprite FollowEffect;

	// Token: 0x04002C0A RID: 11274
	public UISprite DanceEffect;

	// Token: 0x04002C0B RID: 11275
	public UISprite AutoEffect;

	// Token: 0x04002C0C RID: 11276
	private bool IsOpenChatPage;

	// Token: 0x04002C0D RID: 11277
	public Transform ChatOffsetTra;

	// Token: 0x04002C0E RID: 11278
	public TweenPosition ChatOffsetAnima;

	// Token: 0x04002C0F RID: 11279
	public UIWidget AutoComboWi;

	// Token: 0x04002C10 RID: 11280
	public MainFuncBtnLogic CarFuncBtn;

	// Token: 0x04002C11 RID: 11281
	public MainFuncBtnLogic ShopFuncBtn;

	// Token: 0x04002C12 RID: 11282
	public MainFuncBtnLogic SkillFuncBtn;

	// Token: 0x04002C13 RID: 11283
	public MainFuncBtnLogic EnhanceFuncBtn;

	// Token: 0x04002C14 RID: 11284
	public MainFuncBtnLogic BagFuncBtn;

	// Token: 0x04002C15 RID: 11285
	public MainFuncBtnLogic GuildFuncBtn;

	// Token: 0x04002C16 RID: 11286
	public MainFuncBtnLogic TitleFuncBtn;

	// Token: 0x04002C17 RID: 11287
	public MainFuncBtnLogic SocialFuncBtn;

	// Token: 0x04002C18 RID: 11288
	public MainFuncBtnLogic ChatFuncBtn;

	// Token: 0x04002C19 RID: 11289
	public MainFuncBtnLogic RankFuncBtn;

	// Token: 0x04002C1A RID: 11290
	public MainFuncBtnLogic SlotFuncBtn;

	// Token: 0x04002C1B RID: 11291
	public MainFuncBtnLogic GiftFuncBtn;

	// Token: 0x04002C1C RID: 11292
	public MainFuncBtnLogic BigSaleFuncBtn;

	// Token: 0x04002C1D RID: 11293
	public MainFuncBtnLogic FirstSaleFuncBtn;

	// Token: 0x04002C1E RID: 11294
	public MainFuncBtnLogic MysteryFuncBtn;

	// Token: 0x04002C1F RID: 11295
	public UISprite MountCarBtn;

	// Token: 0x04002C20 RID: 11296
	public UISprite MountCDSprite;

	// Token: 0x04002C21 RID: 11297
	public UISprite activityIconTips;

	// Token: 0x04002C22 RID: 11298
	public GameObject MessageObj;

	// Token: 0x04002C23 RID: 11299
	public UISprite CityDamageFlag;

	// Token: 0x04002C24 RID: 11300
	private GameDefine.ACTIVITY_TYPE TipsActivityType = GameDefine.ACTIVITY_TYPE.INVALID;

	// Token: 0x04002C25 RID: 11301
	private bool isOpenRightBtn;

	// Token: 0x04002C26 RID: 11302
	private bool isOpenLeftBtn = true;

	// Token: 0x04002C27 RID: 11303
	private float mEnterDanceAreaTime;

	// Token: 0x04002C28 RID: 11304
	private int DANCE_WAIT_TIME = 20;

	// Token: 0x04002C29 RID: 11305
	private SceneManager mCurSceneManager;

	// Token: 0x04002C2A RID: 11306
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002C2B RID: 11307
	private PlayerData mPlayerData;

	// Token: 0x04002C2C RID: 11308
	private MissionManager missionManager;

	// Token: 0x04002C2D RID: 11309
	private bool isInsafeArea;

	// Token: 0x04002C2E RID: 11310
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002C2F RID: 11311
	private float mMountTime = -1f;

	// Token: 0x04002C30 RID: 11312
	public int MaxLineCount = 2;

	// Token: 0x04002C31 RID: 11313
	private bool[] AcceptChannelType = new bool[]
	{
		true,
		true,
		true,
		true,
		true,
		true,
		true,
		true
	};

	// Token: 0x04002C32 RID: 11314
	public UILabel textLabel;

	// Token: 0x04002C33 RID: 11315
	private List<PlayerChatHistoryInfo> mHistoryListCache;

	// Token: 0x04002C34 RID: 11316
	private StringBuilder outPutStr = new StringBuilder();

	// Token: 0x04002C35 RID: 11317
	private StringBuilder tempStr = new StringBuilder();

	// Token: 0x04002C36 RID: 11318
	private bool isShowAutoCombo = true;

	// Token: 0x04002C37 RID: 11319
	private float mLastSendServerTimeCheck;

	// Token: 0x04002C38 RID: 11320
	private float mSendSerTimeInterval = 5f;

	// Token: 0x04002C39 RID: 11321
	private float nearDistance = 4f;

	// Token: 0x04002C3A RID: 11322
	private float farDistance = 10f;

	// Token: 0x04002C3B RID: 11323
	public List<UIButtonColor> RightBtnColorList;

	// Token: 0x04002C3C RID: 11324
	public List<UIButtonColor> TopBtnColorList;
}
