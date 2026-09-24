using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A13 RID: 2579
public class GuildBattleRootLogic : SingletonUnity<GuildBattleRootLogic>
{
	// Token: 0x17000FC5 RID: 4037
	// (get) Token: 0x06004A29 RID: 18985 RVA: 0x00182D34 File Offset: 0x00180F34
	public guild_battle_info CurBattleInfo
	{
		get
		{
			return this.battleInfo;
		}
	}

	// Token: 0x06004A2A RID: 18986 RVA: 0x00182D3C File Offset: 0x00180F3C
	public void EnableReset()
	{
		this.mPlayerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.BattleRound1.EnableReset();
		this.BattleRound2.EnableReset();
		this.BattleRound3.EnableReset();
		NGUITools.SetActive(this.StartBtn.gameObject, false);
		NGUITools.SetActive(this.GambleBtn.gameObject, false);
		NGUITools.SetActive(this.ShowRewardItems.gameObject, false);
		this.ShowTimeLabel.text = string.Empty;
		this.IsShowTimeCount = false;
		NGUITools.SetActive(this.FirstRankObj.gameObject, false);
		NGUITools.SetActive(this.NoFirstObj.gameObject, true);
		this.mCanEnterBattle = false;
		this.ResetFinishBtn();
	}

	// Token: 0x06004A2B RID: 18987 RVA: 0x00182DF4 File Offset: 0x00180FF4
	private void InitBattleData()
	{
		this.battleInfo = new guild_battle_info();
		this.battleInfo.time = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() + 6000L;
		this.battleInfo.ID = "1501";
		this.battleInfo.state = 4L;
		Guild playerGuild = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild;
		this.battleInfo.battle_round_1 = new guild_battle_round();
		this.battleInfo.battle_round_1.battle_team = new Dictionary<long, guild_battle_team>();
		guild_battle_team guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = playerGuild.ServerId;
		guild_battle_team.guildName = playerGuild.GuilName;
		guild_battle_team.state = 3L;
		guild_battle_team.index = 1L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 222L;
		guild_battle_team.guildName = "222";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 2L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 333L;
		guild_battle_team.guildName = "333";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 3L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 444L;
		guild_battle_team.guildName = "444";
		guild_battle_team.state = 3L;
		guild_battle_team.index = 4L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 555L;
		guild_battle_team.guildName = "555";
		guild_battle_team.state = 3L;
		guild_battle_team.index = 5L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 666L;
		guild_battle_team.guildName = "666";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 6L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 777L;
		guild_battle_team.guildName = "777";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 7L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 888L;
		guild_battle_team.guildName = "888";
		guild_battle_team.state = 3L;
		guild_battle_team.index = 8L;
		this.battleInfo.battle_round_1.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		this.battleInfo.battle_round_2 = new guild_battle_round();
		this.battleInfo.battle_round_2.battle_team = new Dictionary<long, guild_battle_team>();
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = playerGuild.ServerId;
		guild_battle_team.guildName = playerGuild.GuilName;
		guild_battle_team.state = 3L;
		guild_battle_team.index = 1L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 222L;
		guild_battle_team.guildName = "222";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 2L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 333L;
		guild_battle_team.guildName = "333";
		guild_battle_team.state = 3L;
		guild_battle_team.index = 3L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 444L;
		guild_battle_team.guildName = "444";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 4L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 555L;
		guild_battle_team.guildName = "555";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 5L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 666L;
		guild_battle_team.guildName = "666";
		guild_battle_team.state = 3L;
		guild_battle_team.index = 6L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 777L;
		guild_battle_team.guildName = "777";
		guild_battle_team.state = 4L;
		guild_battle_team.index = 7L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 888L;
		guild_battle_team.guildName = "888";
		guild_battle_team.state = 3L;
		guild_battle_team.index = 8L;
		this.battleInfo.battle_round_2.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		this.battleInfo.battle_round_3 = new guild_battle_round();
		this.battleInfo.battle_round_3.battle_team = new Dictionary<long, guild_battle_team>();
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = playerGuild.ServerId;
		guild_battle_team.guildName = playerGuild.GuilName;
		guild_battle_team.state = 0L;
		guild_battle_team.index = 1L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 222L;
		guild_battle_team.guildName = "222";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 2L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 333L;
		guild_battle_team.guildName = "333";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 3L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 444L;
		guild_battle_team.guildName = "444";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 4L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 555L;
		guild_battle_team.guildName = "555";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 5L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 666L;
		guild_battle_team.guildName = "666";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 6L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 777L;
		guild_battle_team.guildName = "777";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 7L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
		guild_battle_team = new guild_battle_team();
		guild_battle_team.guildId = 888L;
		guild_battle_team.guildName = "888";
		guild_battle_team.state = 0L;
		guild_battle_team.index = 8L;
		this.battleInfo.battle_round_3.battle_team.Add(guild_battle_team.guildId, guild_battle_team);
	}

	// Token: 0x06004A2C RID: 18988 RVA: 0x001835A4 File Offset: 0x001817A4
	public void Reset(ret_guild_battle_info.request request)
	{
		this.battleInfo = request.battle_info;
		this.UpdatePage();
		this.requestStateFlag = false;
	}

	// Token: 0x06004A2D RID: 18989 RVA: 0x001835C0 File Offset: 0x001817C0
	public void UpdatePage()
	{
		this.mCurGuildBattleData = DataManager.GetGuildBattleDataById(this.battleInfo.ID);
		if (this.mCurGuildBattleData != null)
		{
			ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.mCurGuildBattleData.ShowRewardID);
			if (showRewardDataByID != null)
			{
				NGUITools.SetActive(this.ShowRewardItems.gameObject, true);
				this.ShowRewardItems.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
			}
			else
			{
				NGUITools.SetActive(this.ShowRewardItems.gameObject, false);
			}
		}
		this.ResetBtn();
		if (this.battleInfo.state == 0L || this.battleInfo.state == 2L || this.battleInfo.state == 4L)
		{
			this.IsShowTimeCount = false;
			string dictionaryString = StrDictionary.GetDictionaryString(GameDefine.RoundName[(int)(checked((IntPtr)(this.battleInfo.state / 2L)))], new object[0]);
			int num = 0;
			int num2 = 0;
			if (this.battleInfo.state == 0L)
			{
				num = this.mCurGuildBattleData.Week1;
				num2 = this.mCurGuildBattleData.StartTime1;
			}
			else if (this.battleInfo.state == 2L)
			{
				num = this.mCurGuildBattleData.Week2;
				num2 = this.mCurGuildBattleData.StartTime2;
			}
			else if (this.battleInfo.state == 4L)
			{
				num = this.mCurGuildBattleData.Week3;
				num2 = this.mCurGuildBattleData.StartTime3;
			}
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			int num3 = (num - 1 + TimeTools.GetOffsetDay((long)num2, playerCommonData.TimeOffset) + GameDefine.WEEK_NAME.Count) % GameDefine.WEEK_NAME.Count;
			TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)num2, playerCommonData.TimeOffset);
			TimeSpan localShowTime2 = TimeTools.GetLocalShowTime((long)(num2 + this.mCurGuildBattleData.DurationTime), playerCommonData.TimeOffset);
			string text = string.Format("{0} {1}-{2}", StrDictionary.GetDictionaryString(GameDefine.WEEK_NAME[num3], new object[0]), string.Format("{0:D2}:{1:D2}", localShowTime.Hours, localShowTime.Minutes), string.Format("{0:D2}:{1:D2}", localShowTime2.Hours, localShowTime2.Minutes));
			this.ShowTimeLabel.text = string.Format("{0} {1} {2}", dictionaryString, StrDictionary.GetDictionaryString("#{105085}", new object[0]), text);
		}
		else if (this.battleInfo.state == 1L || this.battleInfo.state == 3L || this.battleInfo.state == 5L)
		{
			this.IsShowTimeCount = true;
			string dictionaryString2 = StrDictionary.GetDictionaryString(GameDefine.RoundName[(int)(checked((IntPtr)(this.battleInfo.state / 2L)))], new object[0]);
			if (this.battleInfo.HasTime)
			{
				this.ShowTimeLabel.text = string.Format("{0} {1}", dictionaryString2, StrDictionary.GetDictionaryString("#{105055}", new object[]
				{
					TimeTools.GetDaySecondStr(this.battleInfo.time - this.mPlayerCommonData.GetCurServerTime())
				}));
			}
			else
			{
				this.ShowTimeLabel.text = string.Empty;
			}
		}
		else if (this.battleInfo.state < 0L || this.battleInfo.state >= 6L)
		{
			this.IsShowTimeCount = false;
			int week = this.mCurGuildBattleData.Week;
			int startTime = this.mCurGuildBattleData.StartTime;
			PlayerCommonData playerCommonData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			int num4 = (week - 1 + TimeTools.GetOffsetDay((long)startTime, playerCommonData2.TimeOffset) + GameDefine.WEEK_NAME.Count) % GameDefine.WEEK_NAME.Count;
			TimeSpan localShowTime3 = TimeTools.GetLocalShowTime((long)startTime, playerCommonData2.TimeOffset);
			this.ShowTimeLabel.text = string.Format("{0}:{1} {2:d2}:{3:d2}", new object[]
			{
				StrDictionary.GetDictionaryString("#{105076}", new object[0]),
				StrDictionary.GetDictionaryString(GameDefine.WEEK_NAME[num4], new object[0]),
				localShowTime3.Hours,
				localShowTime3.Minutes
			});
		}
		long targetGuildId = -1L;
		if (this.mCanEnterBattle)
		{
			targetGuildId = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId;
		}
		else if (this.battleInfo.HasGuildId)
		{
			targetGuildId = this.battleInfo.guildId;
		}
		if (this.battleInfo.HasBattle_round_1)
		{
			this.BattleRound1.Reset(this.battleInfo.battle_round_1, false, targetGuildId, (int)this.battleInfo.state, 1);
		}
		else
		{
			this.BattleRound1.Reset(null, false, targetGuildId, (int)this.battleInfo.state, 1);
		}
		if (this.battleInfo.HasBattle_round_2)
		{
			this.BattleRound2.Reset(this.battleInfo.battle_round_2, false, targetGuildId, (int)this.battleInfo.state, 2);
		}
		else
		{
			this.BattleRound2.Reset(null, false, targetGuildId, (int)this.battleInfo.state, 2);
		}
		if (this.battleInfo.HasBattle_round_3)
		{
			this.BattleRound3.Reset(this.battleInfo.battle_round_3, true, targetGuildId, (int)this.battleInfo.state, 3);
		}
		else
		{
			this.BattleRound3.Reset(null, true, targetGuildId, (int)this.battleInfo.state, 3);
		}
		if (this.battleInfo.state == 6L || this.battleInfo.state == -1L)
		{
			string text2 = string.Empty;
			if (this.battleInfo.HasChampionName)
			{
				text2 = this.battleInfo.championName;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				NGUITools.SetActive(this.FirstRankObj, true);
				NGUITools.SetActive(this.NoFirstObj, false);
				this.FirstRankLabel.text = text2;
			}
			else
			{
				NGUITools.SetActive(this.FirstRankObj, false);
				NGUITools.SetActive(this.NoFirstObj, true);
			}
		}
	}

	// Token: 0x06004A2E RID: 18990 RVA: 0x00183BD8 File Offset: 0x00181DD8
	private void ResetBtn()
	{
		if (this.battleInfo == null)
		{
			return;
		}
		this.mCanEnterBattle = false;
		if (this.battleInfo.state == 0L || this.battleInfo.state == 1L)
		{
			if (this.battleInfo.battle_round_1 != null)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
				{
					long serverId = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId;
					List<guild_battle_team> list = new List<guild_battle_team>(this.battleInfo.battle_round_1.battle_team.Values);
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].guildId == serverId)
						{
							this.mCanEnterBattle = true;
							SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsGuildBattleRedTeam = (list[i].index % 2L == 0L);
							break;
						}
					}
				}
				else
				{
					this.mCanEnterBattle = false;
				}
			}
		}
		else if (this.battleInfo.state == 2L || this.battleInfo.state == 3L)
		{
			if (this.battleInfo.battle_round_2 != null)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
				{
					long serverId2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId;
					List<guild_battle_team> list2 = new List<guild_battle_team>(this.battleInfo.battle_round_2.battle_team.Values);
					for (int j = 0; j < list2.Count; j++)
					{
						if (list2[j].guildId == serverId2)
						{
							this.mCanEnterBattle = true;
							SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsGuildBattleRedTeam = (list2[j].index % 2L == 0L);
							break;
						}
					}
				}
				else
				{
					this.mCanEnterBattle = false;
				}
			}
		}
		else if ((this.battleInfo.state == 4L || this.battleInfo.state == 5L) && this.battleInfo.battle_round_3 != null)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				long serverId3 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId;
				List<guild_battle_team> list3 = new List<guild_battle_team>(this.battleInfo.battle_round_3.battle_team.Values);
				for (int k = 0; k < list3.Count; k++)
				{
					if (list3[k].guildId == serverId3)
					{
						this.mCanEnterBattle = true;
						SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsGuildBattleRedTeam = (list3[k].index % 2L == 0L);
						break;
					}
				}
			}
			else
			{
				this.mCanEnterBattle = false;
			}
		}
		if (this.mCanEnterBattle)
		{
			this.ResetFightPage();
		}
		else
		{
			this.ResetGamblePage();
		}
	}

	// Token: 0x06004A2F RID: 18991 RVA: 0x00183EC4 File Offset: 0x001820C4
	private void ResetFinishBtn()
	{
		NGUITools.SetActive(this.StartBtn.gameObject, false);
		NGUITools.SetActive(this.GuildMemberBtn.gameObject, false);
		NGUITools.SetActive(this.GambleBtn.gameObject, false);
	}

	// Token: 0x06004A30 RID: 18992 RVA: 0x00183F04 File Offset: 0x00182104
	private void ResetGamblePage()
	{
		NGUITools.SetActive(this.StartBtn.gameObject, false);
		NGUITools.SetActive(this.GuildMemberBtn.gameObject, false);
		if (this.battleInfo.state >= 6L || this.battleInfo.state < 0L)
		{
			NGUITools.SetActive(this.GambleBtn.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.GambleBtn.gameObject, true);
		}
		if (this.battleInfo.HasGuildId || this.battleInfo.state == 6L)
		{
			this.GambleBtn.spriteName = "CZ_anNiu_2+";
			this.GambleLabel.text = StrDictionary.GetDictionaryString(StrDictionary.GetDictionaryString("#{105046}", new object[0]), new object[0]);
		}
		else
		{
			this.GambleBtn.spriteName = "CZ_anNiu_2";
			this.GambleLabel.text = StrDictionary.GetDictionaryString(StrDictionary.GetDictionaryString("#{105023}", new object[0]), new object[0]);
		}
	}

	// Token: 0x06004A31 RID: 18993 RVA: 0x00184014 File Offset: 0x00182214
	private void ResetFightPage()
	{
		NGUITools.SetActive(this.StartBtn.gameObject, true);
		NGUITools.SetActive(this.GuildMemberBtn.gameObject, true);
		NGUITools.SetActive(this.GambleBtn.gameObject, false);
		if (this.battleInfo.state == 6L)
		{
			this.StartBtn.spriteName = "CZ_anNiu_2+";
		}
		else
		{
			this.StartBtn.spriteName = "CZ_anNiu_2";
		}
	}

	// Token: 0x06004A32 RID: 18994 RVA: 0x0018408C File Offset: 0x0018228C
	public void OnClickStartBtn()
	{
		if (this.battleInfo.state == 1L || this.battleInfo.state == 3L || this.battleInfo.state == 5L)
		{
			enter_guild_battle.request rpcReq = new enter_guild_battle.request();
			WaitResponseUIRootLogic.OpenWaitBox(286, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_guild_battle>(rpcReq, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{105011}", true, false);
		}
	}

	// Token: 0x06004A33 RID: 18995 RVA: 0x00184108 File Offset: 0x00182308
	public void OnClickRankBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleRankRoot, delegate
		{
			SingletonUnity<GuildBattleRankRoot>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(287, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.req_guild_battle_rank>(null, null);
		}, null);
	}

	// Token: 0x06004A34 RID: 18996 RVA: 0x00184138 File Offset: 0x00182338
	public void OnClickMemberBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleMemberRoot, delegate
		{
			SingletonUnity<GuildBattleMemberRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(288, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.req_guild_battle_member>(null, null);
		}, null);
	}

	// Token: 0x06004A35 RID: 18997 RVA: 0x00184168 File Offset: 0x00182368
	public void OnClickRuleBtn()
	{
		if (this.mCurGuildBattleData != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.mCurGuildBattleData.MRule, null, new object[0]);
			}, null);
		}
	}

	// Token: 0x06004A36 RID: 18998 RVA: 0x00184194 File Offset: 0x00182394
	public void OnClickGambleBtn()
	{
		if (this.battleInfo.HasGuildId && this.battleInfo.guildId != -1L)
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{105068}", new object[0]), true, false);
			return;
		}
		if (this.battleInfo.state == 0L || this.battleInfo.state == 2L || this.battleInfo.state == 4L)
		{
			if (this.battleInfo.HasGuildId)
			{
				NoticeLogic.AddNotifyData("#{105068}", true, false);
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleGambleRoot, delegate
				{
					SingletonUnity<GuildBattleGambleRoot>.Instance.Reset();
					if (this.battleInfo.state == 0L)
					{
						SingletonUnity<GuildBattleGambleRoot>.Instance.RefreshInfo(this.battleInfo.battle_round_1);
					}
					else if (this.battleInfo.state == 2L)
					{
						SingletonUnity<GuildBattleGambleRoot>.Instance.RefreshInfo(this.battleInfo.battle_round_2);
					}
					else if (this.battleInfo.state == 4L)
					{
						SingletonUnity<GuildBattleGambleRoot>.Instance.RefreshInfo(this.battleInfo.battle_round_3);
					}
				}, null);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{105071}", true, false);
		}
	}

	// Token: 0x06004A37 RID: 18999 RVA: 0x00184260 File Offset: 0x00182460
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRoot);
		if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.CurPageIndex = -1;
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildActivityRootLogic, delegate
			{
				SingletonUnity<GuildActivityRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(195, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_guild_boss>(null, null);
			}, null);
		}
	}

	// Token: 0x06004A38 RID: 19000 RVA: 0x001842E4 File Offset: 0x001824E4
	private void Update()
	{
		if (this.battleInfo != null && Time.realtimeSinceStartup - this.lastCheckTime > 1f)
		{
			this.lastCheckTime = Time.realtimeSinceStartup;
			this.UpdateTimeLabel();
		}
	}

	// Token: 0x06004A39 RID: 19001 RVA: 0x00184324 File Offset: 0x00182524
	private void UpdateTimeLabel()
	{
		if (this.battleInfo != null && this.battleInfo.HasTime)
		{
			this.restTime = this.battleInfo.time - this.mPlayerCommonData.GetCurServerTime();
			if (this.restTime > 0L)
			{
				if (this.IsShowTimeCount)
				{
					string dictionaryString = StrDictionary.GetDictionaryString(GameDefine.RoundName[(int)(checked((IntPtr)(this.battleInfo.state / 2L)))], new object[0]);
					this.ShowTimeLabel.text = string.Format("{0} {1}", dictionaryString, StrDictionary.GetDictionaryString("#{105055}", new object[]
					{
						TimeTools.GetDaySecondStr(this.battleInfo.time - this.mPlayerCommonData.GetCurServerTime())
					}));
				}
			}
			else if (!this.requestStateFlag)
			{
				this.requestStateFlag = true;
				WaitResponseUIRootLogic.OpenWaitBox(285, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.req_guild_battle_info>(null, null);
			}
		}
	}

	// Token: 0x06004A3A RID: 19002 RVA: 0x00184420 File Offset: 0x00182620
	private string GetBattleTimeStr(long state, string timeStr)
	{
		if (state >= 0L && state <= 6L)
		{
			switch ((int)state)
			{
			case 0:
				return StrDictionary.GetDictionaryString("#{105056}", new object[]
				{
					timeStr
				});
			case 2:
				return StrDictionary.GetDictionaryString("#{105053}", new object[]
				{
					timeStr
				});
			case 4:
				return StrDictionary.GetDictionaryString("#{105054}", new object[]
				{
					timeStr
				});
			case 6:
				return StrDictionary.GetDictionaryString("#{105052}", new object[]
				{
					timeStr
				});
			}
		}
		return StrDictionary.GetDictionaryString("#{105055}", new object[]
		{
			timeStr
		});
	}

	// Token: 0x06004A3B RID: 19003 RVA: 0x001844D0 File Offset: 0x001826D0
	public bool IsPreparingState()
	{
		return this.battleInfo != null && (this.battleInfo.state == 0L || this.battleInfo.state == 2L || this.battleInfo.state == 4L);
	}

	// Token: 0x04003798 RID: 14232
	public GuildBattleRoundRoot BattleRound1;

	// Token: 0x04003799 RID: 14233
	public GuildBattleRoundRoot BattleRound2;

	// Token: 0x0400379A RID: 14234
	public GuildBattleRoundRoot BattleRound3;

	// Token: 0x0400379B RID: 14235
	public ShowRewardItems ShowRewardItems;

	// Token: 0x0400379C RID: 14236
	private GuildBattleData mCurGuildBattleData;

	// Token: 0x0400379D RID: 14237
	public UISprite GuildMemberBtn;

	// Token: 0x0400379E RID: 14238
	public UISprite StartBtn;

	// Token: 0x0400379F RID: 14239
	public UISprite GambleBtn;

	// Token: 0x040037A0 RID: 14240
	public UILabel GambleLabel;

	// Token: 0x040037A1 RID: 14241
	public UILabel ShowTimeLabel;

	// Token: 0x040037A2 RID: 14242
	private bool IsShowTimeCount;

	// Token: 0x040037A3 RID: 14243
	public GameObject FirstRankObj;

	// Token: 0x040037A4 RID: 14244
	public GameObject NoFirstObj;

	// Token: 0x040037A5 RID: 14245
	public UILabel FirstRankLabel;

	// Token: 0x040037A6 RID: 14246
	private guild_battle_info battleInfo;

	// Token: 0x040037A7 RID: 14247
	private bool mCanEnterBattle;

	// Token: 0x040037A8 RID: 14248
	private PlayerCommonData mPlayerCommonData;

	// Token: 0x040037A9 RID: 14249
	private float lastCheckTime;

	// Token: 0x040037AA RID: 14250
	private long restTime;

	// Token: 0x040037AB RID: 14251
	private bool requestStateFlag;
}
