using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000852 RID: 2130
public class GuildInfoRootLogic : SingletonUnity<GuildInfoRootLogic>
{
	// Token: 0x06003730 RID: 14128 RVA: 0x000E2DF8 File Offset: 0x000E0FF8
	public void UpdateChangeTips()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.Notice.enabled = playerData.PlayerGuild.CanEditNotice();
		UnityVersionUtil.SetActiveRecursive(this.NoticeTips, playerData.PlayerGuild.CanEditNotice());
	}

	// Token: 0x06003731 RID: 14129 RVA: 0x000E2E3C File Offset: 0x000E103C
	public void UpdateDonate(Dictionary<string, donate_record> records)
	{
		List<donate_record> list = new List<donate_record>(records.Values);
		list.Sort((donate_record d1, donate_record d2) => d1.id.CompareTo(d2.id));
		for (int i = 0; i < list.Count; i++)
		{
			if (i < this.DonateItems.Length)
			{
				GuildDonateData guildDonateDataByID = DataManager.GetGuildDonateDataByID(list[i].id);
				this.DonateItems[i].Init(guildDonateDataByID, list[i]);
			}
			else
			{
				Debug.LogError("Donate max 3333333!!!");
			}
		}
	}

	// Token: 0x06003732 RID: 14130 RVA: 0x000E2ED4 File Offset: 0x000E10D4
	public void UpDateGuildInfo(Guild info)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.curInfo = info;
		this.GuildIcon.spriteName = GameDefine.GuildIcon[info.GuildIcon];
		this.Name.text = string.Format("{0}", info.GuilName);
		this.Bossname.text = string.Format("{0}", info.GuildChiefName);
		this.LevelLab.text = string.Format("LV:{0}", info.GuilLevel);
		this.MemberNum.text = string.Format("{0}/{1}", info.GuildMemberNum, info.GuildMaxPlayer);
		this.NoticeLab.text = string.Format("{0}", info.Notice);
		this.ExpLab.text = info.GuildExp.ToString();
		List<GuildLevelData> guildLevelDataList = DataManager.GetGuildLevelDataList();
		if (info.GuilLevel < guildLevelDataList.Count)
		{
			float num = 0f;
			if (info.GuilLevel > 1)
			{
				num = (float)DataManager.GetGuildLevelDataByLevel(info.GuilLevel - 2).GuildExp;
			}
			GuildLevelData guildLevelDataByLevel = DataManager.GetGuildLevelDataByLevel(info.GuilLevel - 1);
			float value = Mathf.Clamp01(((float)info.GuildExp - num) / ((float)guildLevelDataByLevel.GuildExp - num));
			this.expSlider.value = value;
		}
		else
		{
			this.expSlider.value = 1f;
		}
		this.ComboValueLab.text = info.GuildCombo.ToString();
		this.Notice.defaultText = info.Notice;
		this.JobLabel.text = StrDictionary.GetDictionaryString(GameDefine.GuildJobStr[(int)info.PlayerJob], new object[0]);
		this.ContributeLabel.text = playerData.GuildContribute.ToString();
		this.AllContributeLabel.text = info.GuildAllContribute.ToString();
		this.UpdateDonate(info.DonateRecord);
		this.UpdateChangeTips();
		this.ShowActiveInfo(this.curInfo.DisactiveState == 0);
	}

	// Token: 0x06003733 RID: 14131 RVA: 0x000E30F0 File Offset: 0x000E12F0
	private void ShowActiveInfo(bool isActive)
	{
		if (isActive)
		{
			NGUITools.SetActive(this.ActiveObj, true);
			this.ActiveLabel.color = new Color(1f, 0.73333335f, 0f);
		}
		else
		{
			NGUITools.SetActive(this.ActiveObj, false);
			this.ActiveLabel.color = Color.gray;
		}
	}

	// Token: 0x06003734 RID: 14132 RVA: 0x000E3150 File Offset: 0x000E1350
	public void OnClickTiShiBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{100798}", null, new object[0]);
		}, null);
	}

	// Token: 0x06003735 RID: 14133 RVA: 0x000E3180 File Offset: 0x000E1380
	public void OnClickChangeNoticeBtn()
	{
		if (!string.IsNullOrEmpty(this.Notice.value))
		{
			Singleton<ObjManager>.Instance.MainPlayer.ChangeGuildNotice(this.Notice.value);
		}
	}

	// Token: 0x0400244C RID: 9292
	public UISprite GuildIcon;

	// Token: 0x0400244D RID: 9293
	public UILabel Name;

	// Token: 0x0400244E RID: 9294
	public UILabel Bossname;

	// Token: 0x0400244F RID: 9295
	public UILabel LevelLab;

	// Token: 0x04002450 RID: 9296
	public UILabel MemberNum;

	// Token: 0x04002451 RID: 9297
	public UILabel NoticeLab;

	// Token: 0x04002452 RID: 9298
	public UILabel ComboValueLab;

	// Token: 0x04002453 RID: 9299
	public UILabel ExpLab;

	// Token: 0x04002454 RID: 9300
	public GameObject NoticeTips;

	// Token: 0x04002455 RID: 9301
	public Guild curInfo;

	// Token: 0x04002456 RID: 9302
	public UIInput Notice;

	// Token: 0x04002457 RID: 9303
	public UILabel JobLabel;

	// Token: 0x04002458 RID: 9304
	public UILabel ContributeLabel;

	// Token: 0x04002459 RID: 9305
	public UILabel AllContributeLabel;

	// Token: 0x0400245A RID: 9306
	public DonateItem[] DonateItems;

	// Token: 0x0400245B RID: 9307
	public UISlider expSlider;

	// Token: 0x0400245C RID: 9308
	public UILabel ActiveLabel;

	// Token: 0x0400245D RID: 9309
	public GameObject ActiveObj;
}
