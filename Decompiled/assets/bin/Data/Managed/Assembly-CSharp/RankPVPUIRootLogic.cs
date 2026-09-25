using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000980 RID: 2432
public class RankPVPUIRootLogic : SingletonUnity<RankPVPUIRootLogic>
{
	// Token: 0x060044B4 RID: 17588 RVA: 0x00157654 File Offset: 0x00155854
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060044B5 RID: 17589 RVA: 0x00157660 File Offset: 0x00155860
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x00157680 File Offset: 0x00155880
	private void OnEnable()
	{
		this.ResetPlayerInfo();
	}

	// Token: 0x060044B7 RID: 17591 RVA: 0x00157688 File Offset: 0x00155888
	private void OnDisable()
	{
		this.OnClickBackBtn();
		if (TutorialManager.CurStep == TUTORIAL_STEP.RANK_PVP_CHOOSE)
		{
			FunctionTipsRootLogic.ClearHandTip();
		}
	}

	// Token: 0x060044B8 RID: 17592 RVA: 0x001576A4 File Offset: 0x001558A4
	public void EnableReset()
	{
		for (int i = 0; i < this.PVPPlayers.Length; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.PVPPlayers[i].gameObject, false);
		}
	}

	// Token: 0x060044B9 RID: 17593 RVA: 0x001576E0 File Offset: 0x001558E0
	public void ResetOtherPlayer(ret_request_random_rank_pvp_opponent.request request)
	{
		this.mInitFakeObjNum = this.PVPPlayers.Length;
		this.mInitFakeObjCount = 0;
		for (int i = 0; i < this.PVPPlayers.Length; i++)
		{
			if (request.HasCharacters && request.HasRankPos)
			{
				if (i < request.characters.Count && i < request.rankPos.Count)
				{
					UnityVersionUtil.SetActiveRecursive(this.PVPPlayers[i].gameObject, true);
					this.PVPPlayers[i].UpdateInfo(request.characters[i], (int)request.rankPos[i], new RankPVPPlayerLogic.clickfightfun(this.OnClickFightBtn), new DelegateDefine.NoParamDelegate(this.OnInitFakeObjDone));
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.PVPPlayers[i].gameObject, false);
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.PVPPlayers[i].gameObject, false);
			}
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.RANK_PVP_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x060044BA RID: 17594 RVA: 0x001577E8 File Offset: 0x001559E8
	private void OnInitFakeObjDone()
	{
		this.mInitFakeObjCount++;
		if (this.mInitFakeObjCount >= this.mInitFakeObjNum)
		{
			this.mEnableReChooseBtnFlag = true;
		}
	}

	// Token: 0x060044BB RID: 17595 RVA: 0x0015781C File Offset: 0x00155A1C
	public void ResetPlayerInfo()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.PlayerIconSprite.spriteName = GameDefine.Game_Player_Icon_pic[(int)playerData.Profession];
		this.PlayerNameLabel.text = playerData.MainPlayerAttrData.Name;
		this.PlayerLevelLabel.text = string.Format("Lv.{0}", playerData.MainPlayerAttrData.Level);
		this.PlayerComboValueLabel.text = playerData.MainPlayerAttrData.ComboValue.ToString();
		this.PlayerRankLabel.text = playerData.RankPVPData.GetRankPosStr();
		this.PlayerBestRankLabel.text = playerData.RankPVPData.GetBestRankPosStr();
		this.RestFightNumLabel.text = string.Format("{0}:{1}/{2}", StrDictionary.GetDictionaryString("#{101007}", new object[0]), playerData.RankPVPData.RankTimes, 10);
	}

	// Token: 0x060044BC RID: 17596 RVA: 0x0015790C File Offset: 0x00155B0C
	public void OnClickFightBtn(long fightid)
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.RANK_PVP_CHOOSE)
		{
			this.CheckTutorialEvent();
		}
		this.SelectFightPerson(fightid);
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x00157928 File Offset: 0x00155B28
	private void SelectFightPerson(long characterId)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.RankPVPData.RankTimes > 0)
		{
			WaitResponseUIRootLogic.OpenWaitBox(135, 10f, 0f, null);
			select_pk_character.request request = new select_pk_character.request();
			request.characterId = characterId;
			NetLogic.GetInstance().Send<Protocol.select_pk_character>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.RankPVPData.SyncTimesPVPlocal();
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("PVP", "PVP", "pvptimes");
		}
		else
		{
			this.tempChaId = characterId;
			MessageBoxLogic.OpenOKCancelBox("#{101020}", "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickOKBtn), null, null, null);
		}
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x001579D4 File Offset: 0x00155BD4
	private void OnClickOKBtn()
	{
		select_pk_character.request request = new select_pk_character.request();
		request.characterId = this.tempChaId;
		NetLogic.GetInstance().Send<Protocol.select_pk_character>(request, null);
	}

	// Token: 0x060044BF RID: 17599 RVA: 0x00157A00 File Offset: 0x00155C00
	public void OnClickChangePersonBtn()
	{
		if (this.mEnableReChooseBtnFlag)
		{
			this.mEnableReChooseBtnFlag = false;
			WaitResponseUIRootLogic.OpenWaitBox(133, 10f, 0f, null);
			request_random_rank_pvp_opponent.request rpcReq = new request_random_rank_pvp_opponent.request();
			NetLogic.GetInstance().Send<Protocol.request_random_rank_pvp_opponent>(rpcReq, null);
		}
	}

	// Token: 0x060044C0 RID: 17600 RVA: 0x00157A48 File Offset: 0x00155C48
	public void OnClickRewardBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RankPvpShowRewardRoot, delegate
		{
			SingletonUnity<RankPVPRewardRootLogic>.Instance.Reset();
		}, null);
	}

	// Token: 0x060044C1 RID: 17601 RVA: 0x00157A78 File Offset: 0x00155C78
	public void OnClickRecordBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PVPLogUIRoot, delegate
		{
			SingletonUnity<PVPLogUIRootLogic>.Instance.Reset("#{101005}", null);
		}, null);
	}

	// Token: 0x060044C2 RID: 17602 RVA: 0x00157AA8 File Offset: 0x00155CA8
	public void OnClickRankListBtn()
	{
		this.OnClickBackBtn();
		SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerRankInfoRoot, delegate
		{
			SingletonUnity<PlayerRankInfoRootLogic>.Instance.ResetToPVP();
		}, null);
	}

	// Token: 0x060044C3 RID: 17603 RVA: 0x00157AF4 File Offset: 0x00155CF4
	public void OnClickBackBtn()
	{
		for (int i = 0; i < this.PVPPlayers.Length; i++)
		{
			if (this.PVPPlayers[i] != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.PVPPlayers[i].gameObject, false);
			}
		}
	}

	// Token: 0x060044C4 RID: 17604 RVA: 0x00157B40 File Offset: 0x00155D40
	public void OnClickTishiBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{101622}", null, new object[]
			{
				TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
			});
		}, null);
	}

	// Token: 0x0400316D RID: 12653
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400316E RID: 12654
	public UILabel PlayerNameLabel;

	// Token: 0x0400316F RID: 12655
	public UILabel PlayerLevelLabel;

	// Token: 0x04003170 RID: 12656
	public UILabel PlayerComboValueLabel;

	// Token: 0x04003171 RID: 12657
	public UILabel PlayerRankLabel;

	// Token: 0x04003172 RID: 12658
	public UILabel PlayerBestRankLabel;

	// Token: 0x04003173 RID: 12659
	public UILabel RestFightNumLabel;

	// Token: 0x04003174 RID: 12660
	public UISprite PlayerIconSprite;

	// Token: 0x04003175 RID: 12661
	private long[] OtherPlayerIndex = new long[3];

	// Token: 0x04003176 RID: 12662
	public GameObject[] FightBtnList;

	// Token: 0x04003177 RID: 12663
	public RankPVPPlayerLogic[] PVPPlayers;

	// Token: 0x04003178 RID: 12664
	private int mInitFakeObjNum;

	// Token: 0x04003179 RID: 12665
	private int mInitFakeObjCount;

	// Token: 0x0400317A RID: 12666
	private bool mEnableReChooseBtnFlag;

	// Token: 0x0400317B RID: 12667
	private long tempChaId;
}
