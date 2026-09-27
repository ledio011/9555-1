using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200084F RID: 2127
public class GuildCityItemLogic : MonoBehaviour
{
	// Token: 0x0600370A RID: 14090 RVA: 0x000E2280 File Offset: 0x000E0480
	public void UpdateInfo(guild_map_info curdata)
	{
		this.CurInfo = curdata;
		this.CurCaptureData = DataManager.GetGuildCaptureDataByID(this.CurInfo.id);
		this.CurMapData = DataManager.GetMapInfoDataByID(this.CurCaptureData.MapID);
		this.CityIcon.spriteName = this.CurMapData.MapIcon;
		this.CityLabel.text = StrDictionary.GetDictionaryString(this.CurMapData.Name, new object[0]);
		this.AttLabel.text = string.Format("{0} {1}", GameDefine.GetAttributeName_S(this.CurCaptureData.Basestatus), GameDefine.GetAttributeValueStr(this.CurCaptureData.Basestatus, this.CurCaptureData.BSValue));
		if (this.CurInfo.HasGuildId && this.CurInfo.guildId != 0L)
		{
			this.GangIcon.enabled = true;
			this.GangLabel.enabled = true;
			this.GangIcon.spriteName = GameDefine.GuildIcon[(int)(checked((IntPtr)this.CurInfo.guildIcon))];
			this.GangLabel.text = this.CurInfo.guildName;
		}
		else
		{
			this.GangIcon.enabled = false;
			this.GangLabel.enabled = false;
		}
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(this.CurCaptureData.ShowRewardID);
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardRoot.gameObject, false);
		}
		if (this.CurInfo.state == 1L)
		{
			NGUITools.SetActive(this.BtnSp.gameObject, true);
			this.BtnSp.spriteName = GameDefine.BtnIcon[0];
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{106036}", new object[0]);
			this.CompleteFlag.enabled = false;
		}
		else if (this.CurInfo.HasGuildId && this.CurInfo.guildId != 0L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId == this.CurInfo.guildId)
		{
			if (this.CurInfo.HasRequireState)
			{
				if (this.CurInfo.requireState == 1L)
				{
					NGUITools.SetActive(this.BtnSp.gameObject, true);
					this.BtnSp.spriteName = GameDefine.BtnIcon[0];
					this.BtnLabel.text = StrDictionary.GetDictionaryString("#{103006}", new object[0]);
					this.CompleteFlag.enabled = false;
				}
				else if (this.CurInfo.requireState == 2L)
				{
					NGUITools.SetActive(this.BtnSp.gameObject, false);
					this.CompleteFlag.enabled = true;
				}
				else
				{
					NGUITools.SetActive(this.BtnSp.gameObject, true);
					this.BtnSp.spriteName = GameDefine.BtnIcon[2];
					this.BtnLabel.text = StrDictionary.GetDictionaryString("#{103006}", new object[0]);
					this.CompleteFlag.enabled = false;
				}
			}
			else
			{
				NGUITools.SetActive(this.BtnSp.gameObject, true);
				this.BtnSp.spriteName = GameDefine.BtnIcon[2];
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{103006}", new object[0]);
				this.CompleteFlag.enabled = false;
			}
		}
		else
		{
			NGUITools.SetActive(this.BtnSp.gameObject, true);
			this.BtnSp.spriteName = GameDefine.BtnIcon[2];
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{103006}", new object[0]);
			this.CompleteFlag.enabled = false;
		}
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x000E2658 File Offset: 0x000E0858
	public bool UpdateRewardInfo(guild_map_info updateinfo)
	{
		if (this.CurInfo.id.Equals(updateinfo.id))
		{
			this.UpdateInfo(updateinfo);
			return true;
		}
		return false;
	}

	// Token: 0x0600370C RID: 14092 RVA: 0x000E268C File Offset: 0x000E088C
	public void OnClickBtn()
	{
		if (this.CurInfo.state == 1L)
		{
			if (this.CurMapData != null && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(this.CurMapData.OpenLv))
			{
				NoticeLogic.AddNotifyData2Client(false, "#{100154}", false, new object[]
				{
					this.CurMapData.OpenLv
				});
				return;
			}
			MessageBoxLogic.OpenOKCancelBox("#{106032}", "#{100127}", delegate
			{
				MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
				if (this.CurCaptureData != null)
				{
					enter_guild_city_scene.request request2 = new enter_guild_city_scene.request();
					request2.id = this.CurCaptureData.ID;
					NetLogic.GetInstance().Send<Protocol.enter_guild_city_scene>(request2, null);
				}
				SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickCloseBtn();
			}, null, null, null);
		}
		else if (this.CurInfo.HasGuildId && this.CurInfo.guildId != 0L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId == this.CurInfo.guildId)
		{
			if (this.CurInfo.HasRequireState && this.CurInfo.requireState == 1L)
			{
				request_guild_map_reward.request request = new request_guild_map_reward.request();
				request.id = this.CurInfo.id;
				NetLogic.GetInstance().Send<Protocol.request_guild_map_reward>(request, null);
				return;
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{106037}", true, false);
		}
	}

	// Token: 0x0600370D RID: 14093 RVA: 0x000E27BC File Offset: 0x000E09BC
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardRoot.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x0400242F RID: 9263
	public UISprite CityIcon;

	// Token: 0x04002430 RID: 9264
	public UILabel CityLabel;

	// Token: 0x04002431 RID: 9265
	public UILabel AttLabel;

	// Token: 0x04002432 RID: 9266
	public UISprite GangIcon;

	// Token: 0x04002433 RID: 9267
	public UILabel GangLabel;

	// Token: 0x04002434 RID: 9268
	public ShowRewardItems ShowRewardRoot;

	// Token: 0x04002435 RID: 9269
	public UISprite BtnSp;

	// Token: 0x04002436 RID: 9270
	public UILabel BtnLabel;

	// Token: 0x04002437 RID: 9271
	public UISprite CompleteFlag;

	// Token: 0x04002438 RID: 9272
	private guild_map_info CurInfo;

	// Token: 0x04002439 RID: 9273
	private GuildCaptureData CurCaptureData;

	// Token: 0x0400243A RID: 9274
	private MapInfoData CurMapData;
}
