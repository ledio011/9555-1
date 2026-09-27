using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A0B RID: 2571
public class GuildActivityRootLogic : SingletonUnity<GuildActivityRootLogic>
{
	// Token: 0x060049CE RID: 18894 RVA: 0x0017EA50 File Offset: 0x0017CC50
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060049CF RID: 18895 RVA: 0x0017EA5C File Offset: 0x0017CC5C
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x17000FC4 RID: 4036
	// (get) Token: 0x060049D0 RID: 18896 RVA: 0x0017EA8C File Offset: 0x0017CC8C
	public int CurChoosedIndex
	{
		get
		{
			return this.mCurChoosedIndex;
		}
	}

	// Token: 0x060049D1 RID: 18897 RVA: 0x0017EA94 File Offset: 0x0017CC94
	public void EnableReset()
	{
		for (int i = 0; i < this.guildBossLines.Count; i++)
		{
			NGUITools.SetActive(this.guildBossLines[i].gameObject, false);
		}
		NGUITools.SetActive(this.ShowRewardItemScripts.gameObject, false);
		this.curBossInfo = null;
		this.curBossData = null;
		this.GuildBattleInfo = null;
		this.TargetTypeId = -1;
		this.mCurChoosedIndex = -1;
	}

	// Token: 0x060049D2 RID: 18898 RVA: 0x0017EB08 File Offset: 0x0017CD08
	public void RefershBossInfo(ret_request_guild_boss.request request)
	{
		if (request.HasGuild_boss)
		{
			this.guildBossInfos = new List<guild_boss>(request.guild_boss.Values);
			this.guildBossInfos.Sort((guild_boss x, guild_boss y) => int.Parse(x.id) - int.Parse(y.id));
		}
		int num = Mathf.Min(this.guildBossInfos.Count, 1);
		if (request.HasGuild_battle_info)
		{
			num++;
			this.mGuildBattleIndex = this.GUILD_BATTLE_INDEX;
		}
		else
		{
			this.mGuildBattleIndex = -1;
		}
		if (request.HasDance_state_info)
		{
			num++;
		}
		if (request.HasGuild_map_info)
		{
			num++;
		}
		if (!request.HasGuild_boss)
		{
			for (int i = 0; i < this.guildBossLines.Count; i++)
			{
				NGUITools.SetActive(this.guildBossLines[i].gameObject, false);
			}
		}
		int num2 = num - this.guildBossLines.Count;
		int count = this.guildBossLines.Count;
		if (num2 > 0)
		{
			for (int j = 0; j < num2; j++)
			{
				GameObject gameObject = Object.Instantiate(this.guildBossLines[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D2}", count + j);
				GuildBossLineLogic component = gameObject.GetComponent<GuildBossLineLogic>();
				if (component != null)
				{
					component.transform.parent = this.guildBossLines[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.guildBossLines.Add(component);
				}
			}
		}
		for (int k = 0; k < this.guildBossLines.Count; k++)
		{
			if (k < num)
			{
				NGUITools.SetActive(this.guildBossLines[k].gameObject, true);
			}
			else
			{
				NGUITools.SetActive(this.guildBossLines[k].gameObject, false);
			}
		}
		int num3 = 0;
		if (request.HasGuild_battle_info)
		{
			for (int l = 1; l < num; l++)
			{
				this.guildBossLines[l].ResetItem(this.guildBossInfos, new DelegateDefine.ThirdIntParamDelegate(this.OnClickItemBtn), l);
			}
			this.GuildBattleInfo = request.guild_battle_info;
			this.mCurGuildBattleData = DataManager.GetGuildBattleDataById(this.GuildBattleInfo.ID);
			this.guildBossLines[this.mGuildBattleIndex].ResetItem(this.mCurGuildBattleData.ID, new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildBattle), this.mGuildBattleIndex, this.GuildBattleInfo);
			if (this.TargetTypeId == 9)
			{
				num3 = this.mGuildBattleIndex;
				this.TargetTypeId = 0;
			}
			else if (this.TargetTypeId == 6 && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				num3 = 1;
				this.TargetTypeId = 0;
			}
		}
		else
		{
			for (int m = 0; m < num; m++)
			{
				this.guildBossLines[m].ResetItem(this.guildBossInfos, new DelegateDefine.ThirdIntParamDelegate(this.OnClickItemBtn), m);
			}
		}
		if (request.HasDance_state_info)
		{
			this.GuildDanceInfo = request.dance_state_info;
			this.mCurGuildDanceData = DataManager.GetCityDanceDataById(this.GuildDanceInfo.ID);
			this.guildBossLines[this.GUILD_DANCE_INDEX].ResetItem(new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildDance), this.GUILD_DANCE_INDEX, this.GuildDanceInfo);
			if (this.TargetTypeId == 16)
			{
				num3 = this.GUILD_DANCE_INDEX;
				this.TargetTypeId = 0;
			}
		}
		if (request.HasGuild_map_info)
		{
			this.GuildCityList = new List<guild_map_info>(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.CurGuildCityDataDic.Values);
			this.isOpenGuildCity = false;
			if (this.GuildCityList != null && this.GuildCityList.Count > 0)
			{
				for (int n = 0; n < this.GuildCityList.Count; n++)
				{
					if (this.GuildCityList[n].state == 1L)
					{
						this.isOpenGuildCity = true;
						break;
					}
				}
				this.GuildCityIndex = num - 1;
				this.guildBossLines[this.GuildCityIndex].ResetItem(new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildCity), this.GuildCityIndex, this.GuildCityList);
				if (this.TargetTypeId == 18)
				{
					num3 = this.GuildCityIndex;
					this.TargetTypeId = 0;
				}
			}
		}
		this.mCurChoosedIndex = -1;
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		if (num > 0)
		{
			this.guildBossLines[num3].OnClickItemBtn();
		}
	}

	// Token: 0x060049D3 RID: 18899 RVA: 0x0017EFF0 File Offset: 0x0017D1F0
	public void OnClickChangeTimeBtn()
	{
		if (this.GuildDanceInfo.state == 1L)
		{
			NoticeLogic.AddNotifyData("#{105098}", true, false);
			return;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.isGuildChief())
		{
			NoticeLogic.AddNotifyData("#{105097}", true, false);
			return;
		}
		if (this.ReamainTime > 0L)
		{
			NoticeLogic.AddNotifyData("#{105096}", true, false);
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChangeTimeRoot, delegate
		{
			SingletonUnity<ChangeTimeRootLogic>.Instance.ResetGuildDance(this.mCurGuildDanceData.StartTimes, (int)this.GuildDanceInfo.parm - 1, StrDictionary.GetDictionaryString(this.mCurGuildDanceData.Name, new object[0]), this.mCurGuildDanceData.DurationTime);
		}, null);
	}

	// Token: 0x060049D4 RID: 18900 RVA: 0x0017F074 File Offset: 0x0017D274
	public void OnClickGuildDance(int index, int subIndex, int isenable)
	{
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		string text = string.Empty;
		string text2 = string.Empty;
		NGUITools.SetActive(this.RankRootObj, false);
		NGUITools.SetActive(this.RewardObj, true);
		if (this.GuildDanceInfo.state != 1L)
		{
			this.canStartFlag = 2;
			NGUITools.SetActive(this.startBtnSp.gameObject, false);
			NGUITools.SetActive(this.ChangeTimeBtn.gameObject, true);
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.canStartFlag = 0;
			NGUITools.SetActive(this.startBtnSp.gameObject, true);
			NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		if (this.GuildDanceInfo.HasReset_time)
		{
			this.ReamainTime = this.GuildDanceInfo.reset_time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
		}
		else
		{
			this.ReamainTime = -1L;
		}
		if (this.ReamainTime > 0L)
		{
			this.NextTimeLabel.text = StrDictionary.GetDictionaryString("#{105095}", new object[]
			{
				TimeTools.GetFullTime(this.ReamainTime)
			});
			this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[2];
			this.NextTimeName.enabled = true;
		}
		else
		{
			this.NextTimeLabel.text = string.Empty;
			this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[1];
			this.NextTimeName.enabled = false;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.mCurGuildDanceData.ShowRewardId);
		text = StrDictionary.GetDictionaryString(this.mCurGuildDanceData.Description, new object[0]);
		text2 = StrDictionary.GetDictionaryString(this.mCurGuildDanceData.Name, new object[0]);
		this.DescLabel.text = text;
		this.NameLabel.text = text2;
		for (int i = 0; i < this.rankLabels.Count; i++)
		{
			this.rankLabels[i].text = string.Empty;
		}
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, false);
		}
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.mCurGuildDanceData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.mCurGuildDanceData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurGuildDanceData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060049D5 RID: 18901 RVA: 0x0017F3DC File Offset: 0x0017D5DC
	public void OnClickGuildCity(int index, int subIndex, int isenable)
	{
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		string text = string.Empty;
		string text2 = string.Empty;
		NGUITools.SetActive(this.RankRootObj, false);
		NGUITools.SetActive(this.RewardObj, false);
		NGUITools.SetActive(this.startBtnSp.gameObject, true);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		this.CurGuildCityData = DataManager.GetGuildCaptureDataByID(this.GuildCityList[0].id);
		if (!this.isOpenGuildCity)
		{
			this.canStartFlag = 2;
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.canStartFlag = 0;
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		text = StrDictionary.GetDictionaryString(this.CurGuildCityData.Description, new object[0]);
		text2 = StrDictionary.GetDictionaryString(this.CurGuildCityData.Name, new object[0]);
		this.DescLabel.text = text;
		this.NameLabel.text = text2;
		for (int i = 0; i < this.rankLabels.Count; i++)
		{
			this.rankLabels[i].text = string.Empty;
		}
		UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, false);
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.CurGuildCityData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.CurGuildCityData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.CurGuildCityData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060049D6 RID: 18902 RVA: 0x0017F620 File Offset: 0x0017D820
	public void UpdateGuildBossInfo(ret_request_guild_boss.request request)
	{
		if (request.HasGuild_boss)
		{
			this.guildBossInfos = new List<guild_boss>(request.guild_boss.Values);
			this.guildBossInfos.Sort((guild_boss x, guild_boss y) => int.Parse(x.id) - int.Parse(y.id));
		}
		if (request.HasGuild_battle_info)
		{
			this.GuildBattleInfo = request.guild_battle_info;
		}
		if (request.HasDance_state_info)
		{
			this.GuildDanceInfo = request.dance_state_info;
			this.mCurGuildDanceData = DataManager.GetCityDanceDataById(this.GuildDanceInfo.ID);
			this.guildBossLines[this.GUILD_DANCE_INDEX].ResetItem(new DelegateDefine.ThirdIntParamDelegate(this.OnClickGuildDance), this.GUILD_DANCE_INDEX, this.GuildDanceInfo);
		}
		if (this.mCurChoosedIndex != this.GUILD_DANCE_INDEX)
		{
			return;
		}
		if (this.GuildDanceInfo.state != 1L)
		{
			this.canStartFlag = 2;
			NGUITools.SetActive(this.startBtnSp.gameObject, false);
			NGUITools.SetActive(this.ChangeTimeBtn.gameObject, true);
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.canStartFlag = 0;
			NGUITools.SetActive(this.startBtnSp.gameObject, true);
			NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		if (this.GuildDanceInfo.HasReset_time)
		{
			this.ReamainTime = this.GuildDanceInfo.reset_time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
		}
		else
		{
			this.ReamainTime = -1L;
		}
		if (this.ReamainTime > 0L)
		{
			this.NextTimeLabel.text = StrDictionary.GetDictionaryString("#{105095}", new object[]
			{
				TimeTools.GetFullTime(this.ReamainTime)
			});
			this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[2];
			this.NextTimeName.enabled = true;
		}
		else
		{
			this.NextTimeLabel.text = string.Empty;
			this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[1];
			this.NextTimeName.enabled = false;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
	}

	// Token: 0x060049D7 RID: 18903 RVA: 0x0017F874 File Offset: 0x0017DA74
	private void Update()
	{
		if (this.ReamainTime > 0L)
		{
			this.tempCountTime += Time.deltaTime;
			if (this.tempCountTime >= 1f)
			{
				this.ReamainTime -= 1L;
				this.tempCountTime -= 1f;
				this.NextTimeLabel.text = StrDictionary.GetDictionaryString("#{105095}", new object[]
				{
					TimeTools.GetFullTime(this.ReamainTime)
				});
			}
			if (this.ReamainTime <= 0L)
			{
				this.tempCountTime = 0f;
				this.NextTimeLabel.text = string.Empty;
				this.ChangeTimeBtn.spriteName = GameDefine.BtnIcon[1];
				this.NextTimeName.enabled = false;
			}
		}
	}

	// Token: 0x060049D8 RID: 18904 RVA: 0x0017F940 File Offset: 0x0017DB40
	public void OnClickGuildBattle(int index, int subIndex, int isenable)
	{
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		string text = string.Empty;
		string text2 = string.Empty;
		NGUITools.SetActive(this.RankRootObj, false);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.startBtnSp.gameObject, true);
		NGUITools.SetActive(this.RewardObj, true);
		if (this.GuildBattleInfo != null && this.GuildBattleInfo.state == -2L)
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.mCurGuildBattleData.ShowRewardID);
		text = this.mCurGuildBattleData.MDesc;
		text2 = this.mCurGuildBattleData.MName;
		this.DescLabel.text = text;
		this.NameLabel.text = text2;
		for (int i = 0; i < this.rankLabels.Count; i++)
		{
			this.rankLabels[i].text = string.Empty;
		}
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, false);
		}
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.mCurGuildBattleData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.mCurGuildBattleData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.mCurGuildBattleData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
		this.canStartFlag = 0;
		if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_CLICK_START)
		{
			FunctionTipsRootLogic.ClearHandTip();
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060049D9 RID: 18905 RVA: 0x0017FBC8 File Offset: 0x0017DDC8
	public void OnClickItemBtn(int index, int subIndex, int isenable)
	{
		if (this.mCurChoosedIndex == index && this.mCurChoosedSubIndex == subIndex)
		{
			return;
		}
		this.mCurChoosedIndex = index;
		this.mCurChoosedSubIndex = subIndex;
		this.sublineisEnable = isenable;
		NGUITools.SetActive(this.RankRootObj, true);
		NGUITools.SetActive(this.ChangeTimeBtn.gameObject, false);
		NGUITools.SetActive(this.RewardObj, true);
		this.curBossInfo = this.guildBossInfos[this.mCurChoosedSubIndex];
		ShowRewardData showRewardData = null;
		string text = string.Empty;
		string text2 = string.Empty;
		this.curBossData = DataManager.GetGuildBossDataByID(this.curBossInfo.id);
		int id = 2;
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			id = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.GuilLevel;
		}
		if (DataManager.GetAdaptDataByID(id).DorpKeyDic.ContainsKey(this.curBossData.ShowRewardID))
		{
			string id2 = DataManager.GetAdaptDataByID(id).DorpKeyDic[this.curBossData.ShowRewardID];
			showRewardData = DataManager.GetShowRewardDataByID(id2);
		}
		text = StrDictionary.GetDictionaryString(this.curBossData.Desc, new object[0]);
		text2 = StrDictionary.GetDictionaryString("#{101534}", new object[0]);
		this.DescLabel.text = text;
		this.NameLabel.text = text2;
		for (int i = 0; i < this.rankLabels.Count; i++)
		{
			if (this.curBossInfo.HasSort_item && i < this.curBossInfo.sort_item.Count)
			{
				this.rankLabels[i].text = string.Format("{0}", this.curBossInfo.sort_item[i].name);
			}
			else
			{
				this.rankLabels[i].text = string.Format("{0}", "- - - -");
			}
		}
		if (showRewardData != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, false);
		}
		if (this.curBossInfo.state == 0L)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
			this.canStartFlag = 2;
		}
		else if (this.curBossInfo.state == 1L)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2";
			this.canStartFlag = 0;
		}
		else if (this.curBossInfo.state == 2L)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
			this.canStartFlag = 3;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
		}
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.curBossData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.curBossData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.curBossData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060049DA RID: 18906 RVA: 0x0017FF84 File Offset: 0x0017E184
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBG.mainTexture = tex;
	}

	// Token: 0x060049DB RID: 18907 RVA: 0x0017FF94 File Offset: 0x0017E194
	public void UpdateSelectItem()
	{
		for (int i = 0; i < this.guildBossLines.Count; i++)
		{
			this.guildBossLines[i].RefreshSelect(this.mCurChoosedIndex, this.mCurChoosedSubIndex);
		}
	}

	// Token: 0x060049DC RID: 18908 RVA: 0x0017FFDC File Offset: 0x0017E1DC
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardItemScripts.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x060049DD RID: 18909 RVA: 0x0017FFEC File Offset: 0x0017E1EC
	public void OnClickOpenBtn()
	{
		if (this.curBossInfo == null)
		{
			return;
		}
		if (this.canOpenFlag == 0)
		{
			WaitResponseUIRootLogic.OpenWaitBox(233, 10f, 0f, null);
			open_guild_boss.request request = new open_guild_boss.request();
			request.id = this.curBossInfo.id;
			NetLogic.GetInstance().Send<Protocol.open_guild_boss>(request, null);
			return;
		}
		switch (this.canOpenFlag)
		{
		case 1:
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			break;
		case 2:
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
			break;
		case 3:
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101540}", new object[0]), true, false);
			break;
		}
	}

	// Token: 0x060049DE RID: 18910 RVA: 0x001800C4 File Offset: 0x0017E2C4
	public void refershOpenBtn(guild_boss openboss)
	{
	}

	// Token: 0x060049DF RID: 18911 RVA: 0x001800C8 File Offset: 0x0017E2C8
	public void OnClickStartBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
			return;
		}
		if (this.mCurChoosedIndex == this.mGuildBattleIndex)
		{
			if (this.GuildBattleInfo != null && this.GuildBattleInfo.state == -2L)
			{
				NoticeLogic.AddNotifyData("#{105077}", true, false);
				return;
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildActivityRootLogic);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleRoot, delegate
			{
				SingletonUnity<GuildBattleRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(285, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.req_guild_battle_info>(null, null);
			}, null);
		}
		else if (this.mCurChoosedIndex == this.GUILD_DANCE_INDEX)
		{
			if (this.canStartFlag != 0)
			{
				switch (this.canStartFlag)
				{
				case 1:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
					break;
				case 2:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
					break;
				case 3:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102061}", new object[0]), true, false);
					break;
				}
				return;
			}
			SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickCloseBtn();
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.mCurGuildDanceData.MapId, Vector3.right * -5f, AUTO_SEARCH_PARTH_FINISHEVENT.CITY_DANCE, null);
		}
		else if (this.mCurChoosedIndex == this.GuildCityIndex)
		{
			if (this.canStartFlag != 0)
			{
				switch (this.canStartFlag)
				{
				case 1:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
					break;
				case 2:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
					break;
				case 3:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102061}", new object[0]), true, false);
					break;
				}
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.WorldMapRoot, delegate
			{
				WaitResponseUIRootLogic.OpenWaitBox(319, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_guild_map_info>(null, null);
				SingletonUnity<WorldMapRoot>.Instance.EnableReset(true);
			}, null);
		}
		else
		{
			if (this.curBossInfo == null)
			{
				return;
			}
			if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_CLICK_START)
			{
				this.CheckTutorialEvent();
			}
			if (this.canStartFlag != 0)
			{
				switch (this.canStartFlag)
				{
				case 1:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
					break;
				case 2:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
					break;
				case 3:
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102061}", new object[0]), true, false);
					break;
				}
				return;
			}
			enter_guild_boss_scene.request request = new enter_guild_boss_scene.request();
			request.id = this.curBossInfo.id;
			WaitResponseUIRootLogic.OpenWaitBox(194, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_guild_boss_scene>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("guildboss", string.Format("guildboss_{0}", this.curBossInfo.id), "starttimes");
		}
	}

	// Token: 0x060049E0 RID: 18912 RVA: 0x00180404 File Offset: 0x0017E604
	public void OnClicktishiBtn()
	{
		if (this.mCurChoosedIndex == this.mGuildBattleIndex)
		{
			if (this.mCurGuildBattleData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
				{
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurGuildBattleData.MRule, null, new object[0]);
				}, null);
			}
		}
		else if (this.mCurChoosedIndex == this.GUILD_DANCE_INDEX)
		{
			if (this.mCurGuildDanceData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
				{
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurGuildDanceData.Rule, null, new object[0]);
				}, null);
			}
		}
		else if (this.mCurChoosedIndex == this.GuildCityIndex)
		{
			if (this.CurGuildCityData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
				{
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.CurGuildCityData.Rule, null, new object[0]);
				}, null);
			}
		}
		else if (this.curBossData != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.curBossData.Rule, null, new object[0]);
			}, null);
		}
	}

	// Token: 0x060049E1 RID: 18913 RVA: 0x001804F0 File Offset: 0x0017E6F0
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_BOSS_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x04003710 RID: 14096
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003711 RID: 14097
	public UIScrollView uiScrollView;

	// Token: 0x04003712 RID: 14098
	public List<GuildBossLineLogic> guildBossLines = new List<GuildBossLineLogic>();

	// Token: 0x04003713 RID: 14099
	private List<guild_boss> guildBossInfos = new List<guild_boss>();

	// Token: 0x04003714 RID: 14100
	private guild_boss curBossInfo;

	// Token: 0x04003715 RID: 14101
	public ShowRewardItems ShowRewardItemScripts;

	// Token: 0x04003716 RID: 14102
	private GuildBattleData mCurGuildBattleData;

	// Token: 0x04003717 RID: 14103
	private CityDanceData mCurGuildDanceData;

	// Token: 0x04003718 RID: 14104
	public UILabel DescLabel;

	// Token: 0x04003719 RID: 14105
	public UILabel NameLabel;

	// Token: 0x0400371A RID: 14106
	public UITable RootTable;

	// Token: 0x0400371B RID: 14107
	private int mGuildBattleIndex = -1;

	// Token: 0x0400371C RID: 14108
	private int GUILD_BATTLE_INDEX;

	// Token: 0x0400371D RID: 14109
	private int GUILD_DANCE_INDEX = 2;

	// Token: 0x0400371E RID: 14110
	private int mCurChoosedIndex = -1;

	// Token: 0x0400371F RID: 14111
	private int mCurChoosedSubIndex = -1;

	// Token: 0x04003720 RID: 14112
	public UISprite startBtnSp;

	// Token: 0x04003721 RID: 14113
	public UISprite ChangeTimeBtn;

	// Token: 0x04003722 RID: 14114
	private long ReamainTime;

	// Token: 0x04003723 RID: 14115
	public UILabel NextTimeLabel;

	// Token: 0x04003724 RID: 14116
	public UILabel NextTimeName;

	// Token: 0x04003725 RID: 14117
	private int canStartFlag;

	// Token: 0x04003726 RID: 14118
	private int canOpenFlag;

	// Token: 0x04003727 RID: 14119
	public List<UILabel> rankLabels;

	// Token: 0x04003728 RID: 14120
	public GameObject RankRootObj;

	// Token: 0x04003729 RID: 14121
	public GameObject RewardObj;

	// Token: 0x0400372A RID: 14122
	private int sublineisEnable;

	// Token: 0x0400372B RID: 14123
	private GuildBossData curBossData;

	// Token: 0x0400372C RID: 14124
	public UITexture CopyBG;

	// Token: 0x0400372D RID: 14125
	public int TargetTypeId;

	// Token: 0x0400372E RID: 14126
	private guild_battle_info GuildBattleInfo;

	// Token: 0x0400372F RID: 14127
	private dance_state_info GuildDanceInfo;

	// Token: 0x04003730 RID: 14128
	private List<guild_map_info> GuildCityList;

	// Token: 0x04003731 RID: 14129
	private GuildCaptureData CurGuildCityData;

	// Token: 0x04003732 RID: 14130
	private bool isOpenGuildCity;

	// Token: 0x04003733 RID: 14131
	private int GuildCityIndex;

	// Token: 0x04003734 RID: 14132
	private float tempCountTime;
}
