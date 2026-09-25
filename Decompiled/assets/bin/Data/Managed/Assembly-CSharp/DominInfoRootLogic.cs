using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A25 RID: 2597
public class DominInfoRootLogic : SingletonUnity<DominInfoRootLogic>
{
	// Token: 0x06004B07 RID: 19207 RVA: 0x0018C790 File Offset: 0x0018A990
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004B08 RID: 19208 RVA: 0x0018C79C File Offset: 0x0018A99C
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004B09 RID: 19209 RVA: 0x0018C7CC File Offset: 0x0018A9CC
	public void FlashPage()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.Domin_InfoDic.ContainsKey(this.curDominInfo.id))
		{
			domin_info domin_info = playerData.Domin_InfoDic[this.curDominInfo.id];
			if (playerData.Domin_CharacterDic.ContainsKey(domin_info.serverId))
			{
				this.Reset(domin_info, playerData.Domin_CharacterDic[domin_info.serverId]);
			}
			else
			{
				this.Reset(domin_info, null);
			}
		}
	}

	// Token: 0x06004B0A RID: 19210 RVA: 0x0018C854 File Offset: 0x0018AA54
	public void Reset(domin_info info, character_look chaLook)
	{
		this.curDominInfo = info;
		this.curLookInfo = chaLook;
		this.curDominData = DataManager.GetDominDataByID(info.id);
		this.CurFakeObjRoot.CreateModelPic();
		this.CurFakeObjRoot.EnableFakeObjRoot();
		this.PlayerModelPic.mainTexture = this.CurFakeObjRoot.ModelPic;
		UnityVersionUtil.SetActiveRecursive(this.PlayerModelPic.gameObject, true);
		this.ZoneNameLabel.text = StrDictionary.GetDictionaryString(this.curDominData.ZoneName, new object[0]);
		this.BuildingNameLabel.text = StrDictionary.GetDictionaryString(this.curDominData.BuildingName, new object[0]);
		if (this.curLookInfo != null)
		{
			this.PlayerNameLabel.text = this.curLookInfo.general.name;
			this.PlayerLevelLabel.text = "Lv." + this.curLookInfo.attribute_other.level;
			if (this.curLookInfo.attribute_other.HasGuildId)
			{
				this.PlayerGuildLabel.text = this.curLookInfo.attribute_other.guildName;
			}
			else
			{
				this.PlayerGuildLabel.text = "----";
			}
			this.PlayerPowerLabel.text = this.curLookInfo.attribute_other.combValue.ToString();
			this.PlayerProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[(int)(checked((IntPtr)this.curLookInfo.general.profession))], new object[0]);
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				if ((int)this.curLookInfo.general.profession == 0)
				{
					this.curLookInfo.visual.HeadId = GameDefine.XD_NormalModel[1];
					this.curLookInfo.visual.LegId = GameDefine.XD_NormalModel[3];
					this.curLookInfo.visual.BodyId = GameDefine.XD_NormalModel[2];
					this.curLookInfo.visual.WeaponId = GameDefine.XD_NormalModel[0];
				}
				else if ((int)this.curLookInfo.general.profession == 1)
				{
					this.curLookInfo.visual.HeadId = GameDefine.QJ_NormalModel[1];
					this.curLookInfo.visual.LegId = GameDefine.QJ_NormalModel[3];
					this.curLookInfo.visual.BodyId = GameDefine.QJ_NormalModel[2];
					this.curLookInfo.visual.WeaponId = GameDefine.QJ_NormalModel[0];
				}
				else
				{
					this.curLookInfo.visual.HeadId = GameDefine.NQS_NormalModel[1];
					this.curLookInfo.visual.LegId = GameDefine.NQS_NormalModel[3];
					this.curLookInfo.visual.BodyId = GameDefine.NQS_NormalModel[2];
					this.curLookInfo.visual.WeaponId = GameDefine.NQS_NormalModel[0];
				}
			}
			this.CurFakeObj.InitFakeObject(this.curLookInfo.visual, (int)this.curLookInfo.general.profession, this.CurFakeObjRoot.MeshRoot, null, "FakeObj");
			this.PlayerIconPic.spriteName = GameDefine.Player_Icon_Small_Pic[(int)(checked((IntPtr)this.curLookInfo.general.profession))];
		}
		else
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			this.PlayerNameLabel.text = playerData.MainPlayerAttrData.Name;
			this.PlayerLevelLabel.text = "Lv." + playerData.Level;
			this.PlayerGuildLabel.text = ((!playerData.IsHaveGuild()) ? "----" : playerData.PlayerGuild.GuilName);
			this.PlayerPowerLabel.text = playerData.MainPlayerAttrData.ComboValue.ToString();
			this.PlayerProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[(int)playerData.Profession], new object[0]);
			this.CurFakeObj.InitFakeObject(playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId, playerData.Profession, this.CurFakeObjRoot.MeshRoot, null, "FakeObj");
			this.PlayerIconPic.spriteName = GameDefine.Player_Icon_Small_Pic[(int)playerData.Profession];
		}
		if (this.curDominInfo.state == 0L)
		{
			NGUITools.SetActive(this.ChallengesRoot, true);
			this.Reward1Label.text = StrDictionary.GetDictionaryString("#{103008}", new object[0]);
			this.Reward1Label.color = Color.red;
			this.Reward2Label.text = StrDictionary.GetDictionaryString("#{103008}", new object[0]);
			this.Reward2Label.color = Color.red;
			this.Reward1Btn.spriteName = GameDefine.BtnIcon[2];
			this.Reward2Btn.spriteName = GameDefine.BtnIcon[2];
			NGUITools.SetActive(this.WinPicRoot, false);
		}
		else
		{
			NGUITools.SetActive(this.ChallengesRoot, false);
			this.Reward1Label.text = string.Empty;
			this.Reward2Label.text = string.Empty;
			this.Reward1Btn.spriteName = GameDefine.BtnIcon[1];
			this.Reward2Btn.spriteName = GameDefine.BtnIcon[1];
			NGUITools.SetActive(this.WinPicRoot, true);
		}
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		list.Add(this.curDominData.Resources1);
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curDominData.Resources1);
		list2.Add(itemDataByID.Quality);
		list3.Add(this.curDominData.Total1);
		this.RewardItems1.ShowRewards(list, list2, list3);
		list.Clear();
		list.Add(this.curDominData.Resources2);
		itemDataByID = DataManager.GetItemDataByID(this.curDominData.Resources2);
		list2.Clear();
		list2.Add(itemDataByID.Quality);
		list3.Clear();
		list3.Add(this.curDominData.Total2);
		this.RewardItems2.ShowRewards(list, list2, list3);
		this.UpdatePercentLabel();
		SingletonUnity<NewMapUIRootLogic>.Instance.Reset("11");
		SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(this.curDominInfo.id, false);
	}

	// Token: 0x06004B0B RID: 19211 RVA: 0x0018CE94 File Offset: 0x0018B094
	private void UpdatePercentLabel()
	{
		long num = this.curDominInfo.res_count1;
		if (this.curDominInfo.state == 1L)
		{
			num += (long)Mathf.CeilToInt((float)(PlayerCommonData.GetServerTime() - this.curDominInfo.res_time1) / this.curDominData.Speed1_Second);
		}
		if (num > (long)this.curDominData.Total1)
		{
			num = (long)this.curDominData.Total1;
		}
		float percent = (float)num / (float)this.curDominData.Total1;
		this.SetPercentLabel(percent, this.Reward1PercentLabel, this.Reward1PercentLine, this.Reward1PercentBottomLine);
		if (num < (long)this.curDominData.Total1)
		{
			if (UnityVersionUtil.IsActive(this.Reward1ClaimRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward1ClaimRoot, false);
			}
			if (!UnityVersionUtil.IsActive(this.Reward1GetNowRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward1GetNowRoot, true);
			}
			if (this.curDominInfo.state == 1L)
			{
				float num2 = (float)(this.curDominInfo.res_end_time1 - PlayerCommonData.GetServerTime());
				float percent2 = 1f - Mathf.Clamp01(num2 / (float)(this.curDominInfo.res_end_time1 - this.curDominInfo.res_time1));
				this.SetPercentLabel(percent2, this.Reward1PercentLabel, this.Reward1PercentLine, this.Reward1PercentBottomLine);
				this.Reward1TimeLabel.text = TimeTools.GetHourMinSecStr((long)((int)num2));
			}
			else
			{
				float num3 = (float)((long)this.curDominData.Total1 - num) * this.curDominData.Speed1_Second;
				this.Reward1TimeLabel.text = TimeTools.GetHourMinSecStr((long)((int)num3));
			}
		}
		else
		{
			if (!UnityVersionUtil.IsActive(this.Reward1ClaimRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward1ClaimRoot, true);
			}
			if (UnityVersionUtil.IsActive(this.Reward1GetNowRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward1GetNowRoot, false);
			}
		}
		long num4 = this.curDominInfo.res_count2;
		if (this.curDominInfo.state == 1L)
		{
			num4 += (long)Mathf.CeilToInt((float)(PlayerCommonData.GetServerTime() - this.curDominInfo.res_time2) / this.curDominData.Speed2_Second);
		}
		if (num4 > (long)this.curDominData.Total2)
		{
			num4 = (long)this.curDominData.Total2;
		}
		float percent3 = (float)num4 / (float)this.curDominData.Total2;
		this.SetPercentLabel(percent3, this.Reward2PercentLabel, this.Reward2PercentLine, this.Reward2PercentBottomLine);
		if (num4 < (long)this.curDominData.Total2)
		{
			if (UnityVersionUtil.IsActive(this.Reward2ClaimRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward2ClaimRoot, false);
			}
			if (!UnityVersionUtil.IsActive(this.Reward2GetNowRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward2GetNowRoot, true);
			}
			if (this.curDominInfo.state == 1L)
			{
				float num5 = (float)(this.curDominInfo.res_end_time2 - PlayerCommonData.GetServerTime());
				float percent4 = 1f - Mathf.Clamp01(num5 / (float)(this.curDominInfo.res_end_time2 - this.curDominInfo.res_time2));
				this.SetPercentLabel(percent4, this.Reward2PercentLabel, this.Reward2PercentLine, this.Reward2PercentBottomLine);
				this.Reward2TimeLabel.text = TimeTools.GetHourMinSecStr((long)((int)num5));
			}
			else
			{
				float num6 = (float)((long)this.curDominData.Total2 - num4) * this.curDominData.Speed2_Second;
				this.Reward2TimeLabel.text = TimeTools.GetHourMinSecStr((long)((int)num6));
			}
		}
		else
		{
			if (!UnityVersionUtil.IsActive(this.Reward2ClaimRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward2ClaimRoot, true);
			}
			if (UnityVersionUtil.IsActive(this.Reward2GetNowRoot.gameObject))
			{
				NGUITools.SetActive(this.Reward2GetNowRoot, false);
			}
		}
		if (this.curDominInfo.state == 0L)
		{
			this.SetPercentLabel(0f, this.PowerPercentLabel, this.PowerPercentLine, this.PowerPercentBottomLine);
		}
		else
		{
			float num7 = (float)(this.curDominInfo.end_time - PlayerCommonData.GetServerTime());
			if (num7 < 0f)
			{
				num7 = 0f;
			}
			float num8 = 1f - (float)(PlayerCommonData.GetServerTime() - this.curDominInfo.donmin_time) / (float)(this.curDominInfo.end_time - this.curDominInfo.donmin_time);
			this.SetPercentLabel(num8, this.PowerPercentLabel, this.PowerPercentLine, this.PowerPercentBottomLine);
			this.PowerPercentLabel.text = string.Format("{0}%({1})", (int)(num8 * 100f), TimeTools.GetHourMinSecStr((long)((int)num7)));
		}
	}

	// Token: 0x06004B0C RID: 19212 RVA: 0x0018D32C File Offset: 0x0018B52C
	private void SetPercentLabel(float percent, UILabel label, UISprite topPic, UISprite bottomPic)
	{
		if (percent > 1f)
		{
			percent = 1f;
		}
		if (percent > 1E-45f)
		{
			if (!topPic.enabled)
			{
				topPic.enabled = true;
			}
			topPic.width = (int)((float)bottomPic.width * percent);
		}
		else
		{
			topPic.enabled = false;
		}
		label.text = string.Format("{0}%", (int)(percent * 100f));
	}

	// Token: 0x06004B0D RID: 19213 RVA: 0x0018D3A4 File Offset: 0x0018B5A4
	private void Update()
	{
		if (this.curDominInfo != null && this.curDominInfo.state != 0L)
		{
			this.timeCount += Time.deltaTime;
			if (this.timeCount > 1f)
			{
				this.timeCount = 0f;
				this.UpdatePercentLabel();
			}
		}
	}

	// Token: 0x06004B0E RID: 19214 RVA: 0x0018D400 File Offset: 0x0018B600
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
		this.UnLoadFakeObj();
	}

	// Token: 0x06004B0F RID: 19215 RVA: 0x0018D420 File Offset: 0x0018B620
	public void UnLoadFakeObj()
	{
		if (this.CurFakeObj != null)
		{
			this.CurFakeObj.DestroyFakeObj();
			this.CurFakeObj = null;
		}
		else
		{
			Debug.Log("mPlayerModelVisual == null");
		}
		if (this.CurFakeObjRoot != null)
		{
			this.CurFakeObjRoot.DisableFakeObjRoot();
		}
	}

	// Token: 0x06004B10 RID: 19216 RVA: 0x0018D478 File Offset: 0x0018B678
	public void OnClickChallengeBtn()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			if (this.curDominInfo != null && this.curDominInfo.state != 1L)
			{
				enter_domin_pk_scene.request request = new enter_domin_pk_scene.request();
				request.id = this.curDominInfo.id;
				NetLogic.GetInstance().Send<Protocol.enter_domin_pk_scene>(request, null);
			}
		}
		else
		{
			SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
		}
	}

	// Token: 0x06004B11 RID: 19217 RVA: 0x0018D510 File Offset: 0x0018B710
	public void OnClickReward1Btn()
	{
		if (this.curDominInfo.state == 1L)
		{
			if (UnityVersionUtil.IsActive(this.Reward1ClaimRoot))
			{
				require_domin_rewards.request request = new require_domin_rewards.request();
				request.id = this.curDominInfo.id;
				request.index = 0L;
				request.cost = false;
				NetLogic.GetInstance().Send<Protocol.require_domin_rewards>(request, null);
				this.curDominInfo.res_time1 = PlayerCommonData.GetServerTime();
				this.curDominInfo.res_count1 = 0L;
			}
			else
			{
				long num = (long)(this.curDominData.Total1 - Mathf.FloorToInt((float)(PlayerCommonData.GetServerTime() - this.curDominInfo.res_time1) / this.curDominData.Speed1_Second)) - this.curDominInfo.res_count1;
				long costVal = (long)Mathf.CeilToInt((float)(num * (long)this.curDominData.Price1) / 10000f);
				ShowItemsRootLogic.ShowYestOrNoBtn(DataManager.GetItemDataByID(this.curDominData.Resources1), this.curDominData.Total1, "#{100127}", StrDictionary.GetDictionaryString("#{103018}", new object[]
				{
					GameMoneyHelper.GetMoneyValStr((int)costVal, this.curDominData.PriceType1)
				}), delegate
				{
					if (GameMoneyHelper.BeforeCheckBuyTop(this.curDominData.PriceType1, (int)costVal))
					{
						require_domin_rewards.request request2 = new require_domin_rewards.request();
						request2.id = this.curDominInfo.id;
						request2.index = 0L;
						request2.cost = true;
						NetLogic.GetInstance().Send<Protocol.require_domin_rewards>(request2, null);
						this.curDominInfo.res_time1 = PlayerCommonData.GetServerTime();
						this.curDominInfo.res_count1 = 0L;
					}
				}, null, new object[0]);
			}
		}
	}

	// Token: 0x06004B12 RID: 19218 RVA: 0x0018D660 File Offset: 0x0018B860
	public void OnClickReward2Btn()
	{
		if (this.curDominInfo.state == 1L)
		{
			if (UnityVersionUtil.IsActive(this.Reward2ClaimRoot))
			{
				require_domin_rewards.request request = new require_domin_rewards.request();
				request.id = this.curDominInfo.id;
				request.index = 1L;
				request.cost = false;
				NetLogic.GetInstance().Send<Protocol.require_domin_rewards>(request, null);
				this.curDominInfo.res_time2 = PlayerCommonData.GetServerTime();
				this.curDominInfo.res_count2 = 0L;
			}
			else
			{
				long num = (long)(this.curDominData.Total2 - Mathf.FloorToInt((float)(PlayerCommonData.GetServerTime() - this.curDominInfo.res_time2) / this.curDominData.Speed2_Second)) - this.curDominInfo.res_count2;
				long costVal = (long)Mathf.CeilToInt((float)(num * (long)this.curDominData.Price2) / 10000f);
				ShowItemsRootLogic.ShowYestOrNoBtn(DataManager.GetItemDataByID(this.curDominData.Resources2), this.curDominData.Total2, "#{100127}", StrDictionary.GetDictionaryString("#{103018}", new object[]
				{
					GameMoneyHelper.GetMoneyValStr((int)costVal, this.curDominData.PriceType2)
				}), delegate
				{
					if (GameMoneyHelper.BeforeCheckBuyTop(this.curDominData.PriceType2, (int)costVal))
					{
						require_domin_rewards.request request2 = new require_domin_rewards.request();
						request2.id = this.curDominInfo.id;
						request2.index = 1L;
						request2.cost = true;
						NetLogic.GetInstance().Send<Protocol.require_domin_rewards>(request2, null);
						this.curDominInfo.res_time2 = PlayerCommonData.GetServerTime();
						this.curDominInfo.res_count2 = 0L;
					}
				}, null, new object[0]);
			}
		}
	}

	// Token: 0x06004B13 RID: 19219 RVA: 0x0018D7B0 File Offset: 0x0018B9B0
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominInfoRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DominPageRoot, delegate
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FlashDominRootPage();
		}, null);
	}

	// Token: 0x0400389E RID: 14494
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400389F RID: 14495
	public UILabel ZoneNameLabel;

	// Token: 0x040038A0 RID: 14496
	public UILabel BuildingNameLabel;

	// Token: 0x040038A1 RID: 14497
	public GameObject ChallengesRoot;

	// Token: 0x040038A2 RID: 14498
	public UILabel PlayerNameLabel;

	// Token: 0x040038A3 RID: 14499
	public UILabel PlayerLevelLabel;

	// Token: 0x040038A4 RID: 14500
	public UILabel PlayerGuildLabel;

	// Token: 0x040038A5 RID: 14501
	public UILabel PlayerPowerLabel;

	// Token: 0x040038A6 RID: 14502
	public UILabel PlayerProfessionLabel;

	// Token: 0x040038A7 RID: 14503
	public UITexture PlayerModelPic;

	// Token: 0x040038A8 RID: 14504
	public UISprite Reward1Btn;

	// Token: 0x040038A9 RID: 14505
	public UISprite Reward2Btn;

	// Token: 0x040038AA RID: 14506
	public GameObject Reward1GetNowRoot;

	// Token: 0x040038AB RID: 14507
	public GameObject Reward1ClaimRoot;

	// Token: 0x040038AC RID: 14508
	public GameObject Reward2GetNowRoot;

	// Token: 0x040038AD RID: 14509
	public GameObject Reward2ClaimRoot;

	// Token: 0x040038AE RID: 14510
	public UILabel Reward1TimeLabel;

	// Token: 0x040038AF RID: 14511
	public UILabel Reward2TimeLabel;

	// Token: 0x040038B0 RID: 14512
	public UILabel GetNowTimeLabel;

	// Token: 0x040038B1 RID: 14513
	public UILabel Reward1Label;

	// Token: 0x040038B2 RID: 14514
	public UILabel Reward2Label;

	// Token: 0x040038B3 RID: 14515
	public UILabel Reward1PercentLabel;

	// Token: 0x040038B4 RID: 14516
	public UILabel Reward2PercentLabel;

	// Token: 0x040038B5 RID: 14517
	public UISprite Reward1PercentLine;

	// Token: 0x040038B6 RID: 14518
	public UISprite Reward1PercentBottomLine;

	// Token: 0x040038B7 RID: 14519
	public UISprite Reward2PercentLine;

	// Token: 0x040038B8 RID: 14520
	public UISprite Reward2PercentBottomLine;

	// Token: 0x040038B9 RID: 14521
	public ShowRewardItems RewardItems1;

	// Token: 0x040038BA RID: 14522
	public ShowRewardItems RewardItems2;

	// Token: 0x040038BB RID: 14523
	public UISprite PlayerIconPic;

	// Token: 0x040038BC RID: 14524
	public UILabel PowerPercentLabel;

	// Token: 0x040038BD RID: 14525
	public UISprite PowerPercentLine;

	// Token: 0x040038BE RID: 14526
	public UISprite PowerPercentBottomLine;

	// Token: 0x040038BF RID: 14527
	public TeamFakeObjPicRootLogic CurFakeObjRoot;

	// Token: 0x040038C0 RID: 14528
	public FakeObjLogic CurFakeObj;

	// Token: 0x040038C1 RID: 14529
	public GameObject WinPicRoot;

	// Token: 0x040038C2 RID: 14530
	private domin_info curDominInfo;

	// Token: 0x040038C3 RID: 14531
	private character_look curLookInfo;

	// Token: 0x040038C4 RID: 14532
	private DominData curDominData;

	// Token: 0x040038C5 RID: 14533
	private float timeCount;
}
