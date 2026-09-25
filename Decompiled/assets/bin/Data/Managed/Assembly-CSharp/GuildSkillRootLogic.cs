using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009DA RID: 2522
public class GuildSkillRootLogic : SingletonUnity<GuildSkillRootLogic>
{
	// Token: 0x060047AE RID: 18350 RVA: 0x0016E4B8 File Offset: 0x0016C6B8
	private void OnEnable()
	{
		this.curSelectObj = null;
	}

	// Token: 0x060047AF RID: 18351 RVA: 0x0016E4C4 File Offset: 0x0016C6C4
	public void UpdateGuildSKill()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.contributeLabel.text = playerData.GuildContribute.ToString();
		this.guildLevelLabel.text = string.Format("Lv.{0}", playerData.PlayerGuild.GuilLevel.ToString());
		Dictionary<long, guild_skill> guildSkills = playerData.PlayerGuild.GuildSkills;
		this.mSkills = guildSkills;
		List<guild_skill> list = new List<guild_skill>(guildSkills.Values);
		list.Sort((guild_skill a, guild_skill b) => (int)a.skillType - (int)b.skillType);
		int num = list.Count - this.skillItems.Count;
		int count = this.skillItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.skillItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("jiNeng_{0:D2}", count + i);
				this.skllGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.skillItems.Add(gameObject.GetComponent<GuildSkillItem>());
			}
		}
		for (int j = 0; j < this.skillItems.Count; j++)
		{
			NGUITools.SetActive(this.skillItems[j].gameObject, j < list.Count);
		}
		this.skllGrid.Reposition();
		int num2 = 0;
		for (int k = 0; k < list.Count; k++)
		{
			if (this.curSelectObj == null)
			{
				this.curSkill = list[k];
				this.curSelectObj = this.skillItems[num2].gameObject;
			}
			this.skillItems[num2++].Init(list[k]);
		}
		this.UpdateSelect(this.curSkill, this.curSelectObj);
	}

	// Token: 0x060047B0 RID: 18352 RVA: 0x0016E6E0 File Offset: 0x0016C8E0
	public void UpdateSelect(guild_skill skillType, GameObject obj)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.selectSprite.transform.position = obj.transform.position;
		if (skillType.level > 0L)
		{
			this.curSelectSkillData = DataManager.GetGuildSkillDataByTypeLevel((int)skillType.skillType, (int)skillType.level - 1);
		}
		else
		{
			this.curSelectSkillData = null;
		}
		GuildSkillData guildSkillDataByTypeLevel = DataManager.GetGuildSkillDataByTypeLevel((int)skillType.skillType, (int)skillType.level);
		this.nextSkillData = guildSkillDataByTypeLevel;
		List<GuildSkillData> guildSkillDataListByType = DataManager.GetGuildSkillDataListByType((int)skillType.skillType);
		if (playerData.PlayerGuild.GuilLevel >= guildSkillDataListByType[0].LevelLimit)
		{
			if (guildSkillDataByTypeLevel != null)
			{
				this.CostContributeLabel.text = GameMoneyHelper.GetMoneyValStr(guildSkillDataByTypeLevel.SkillCost, GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE);
			}
			this.SkillInfoIconSprite.spriteName = guildSkillDataListByType[0].icon;
			this.SkillInfoIconSprite.MakePixelPerfect();
			this.SkillNameLabel.text = GameDefine.GetBoldStr(StrDictionary.GetDictionaryString(guildSkillDataListByType[0].name, new object[0]));
			this.SKillLevelLabel.text = skillType.level.ToString();
			this.SkillLevelLabel2.text = string.Format("{0}/{1}", skillType.level, guildSkillDataListByType.Count);
			if (this.curSelectSkillData != null)
			{
				this.SkillAttributeIcon.spriteName = GameDefine.GetAttributeIcon(this.curSelectSkillData.Parm1);
				this.SKillAttributeValue.text = GameDefine.GetAttributeValueStr(this.curSelectSkillData.Parm1, this.curSelectSkillData.Parm2) + GameDefine.GetAttributeName_S(this.curSelectSkillData.Parm1);
			}
			else
			{
				this.SkillAttributeIcon.spriteName = GameDefine.GetAttributeIcon(guildSkillDataByTypeLevel.Parm1);
				this.SKillAttributeValue.text = GameDefine.GetAttributeValueStr(guildSkillDataByTypeLevel.Parm1, 0) + GameDefine.GetAttributeName_S(guildSkillDataByTypeLevel.Parm1);
			}
			if (guildSkillDataByTypeLevel != null)
			{
				NGUITools.SetActive(this.upgradeBtn, true);
				NGUITools.SetActive(this.NextSKillAttributeIcon.gameObject, true);
				this.NextSKillAttributeIcon.spriteName = GameDefine.GetAttributeIcon(guildSkillDataByTypeLevel.Parm1);
				this.NextSKillAttributeValue.text = GameDefine.GetAttributeValueStr(guildSkillDataByTypeLevel.Parm1, guildSkillDataByTypeLevel.Parm2) + GameDefine.GetAttributeName_S(guildSkillDataByTypeLevel.Parm1);
			}
			else
			{
				this.NextSKillAttributeValue.text = string.Empty;
				NGUITools.SetActive(this.NextSKillAttributeIcon.gameObject, false);
				NGUITools.SetActive(this.upgradeBtn, false);
			}
			if (skillType.level >= (long)playerData.PlayerGuild.GuilLevel)
			{
				this.UpgradeBtnSprite.spriteName = "CZ_anNiu_2+";
			}
			else
			{
				this.UpgradeBtnSprite.spriteName = "CZ_anNiu_2";
			}
		}
		else
		{
			this.UpgradeBtnSprite.spriteName = "CZ_anNiu_2+";
		}
	}

	// Token: 0x060047B1 RID: 18353 RVA: 0x0016E9C0 File Offset: 0x0016CBC0
	public void OnClickItem(guild_skill skill, GameObject obj)
	{
		this.curSkill = skill;
		this.curSelectObj = obj;
		this.UpdateSelect(this.curSkill, this.curSelectObj);
	}

	// Token: 0x060047B2 RID: 18354 RVA: 0x0016E9F0 File Offset: 0x0016CBF0
	public void OnClickUpgrade()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.curSkill.level >= (long)playerData.PlayerGuild.GuilLevel)
		{
			NoticeLogic.AddNotifyData("#{101144}", true, false);
			return;
		}
		if (!GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE, this.nextSkillData.SkillCost))
		{
			return;
		}
		if (!GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.nextSkillData.SkillCost))
		{
			return;
		}
		guild_skill_level.request request = new guild_skill_level.request();
		request.guildSkillType = this.curSkill.skillType;
		NetLogic.GetInstance().Send<Protocol.guild_skill_level>(request, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Guild", string.Format("Skill_{0}", this.curSkill.skillType), string.Format("skilllevel_{0}", this.curSkill.level + 1L));
	}

	// Token: 0x060047B3 RID: 18355 RVA: 0x0016EAC8 File Offset: 0x0016CCC8
	public void OnClickTips()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{100791}", null, new object[0]);
		}, null);
	}

	// Token: 0x040034F9 RID: 13561
	public UILabel contributeLabel;

	// Token: 0x040034FA RID: 13562
	public UILabel guildLevelLabel;

	// Token: 0x040034FB RID: 13563
	public UISprite selectSprite;

	// Token: 0x040034FC RID: 13564
	public List<GuildSkillItem> skillItems = new List<GuildSkillItem>();

	// Token: 0x040034FD RID: 13565
	public UIGrid skllGrid;

	// Token: 0x040034FE RID: 13566
	private Dictionary<long, guild_skill> mSkills;

	// Token: 0x040034FF RID: 13567
	private GameObject curSelectObj;

	// Token: 0x04003500 RID: 13568
	private guild_skill curSkill;

	// Token: 0x04003501 RID: 13569
	public GameObject SkillInfo;

	// Token: 0x04003502 RID: 13570
	public UILabel SkillNameLabel;

	// Token: 0x04003503 RID: 13571
	public UILabel SKillLevelLabel;

	// Token: 0x04003504 RID: 13572
	public UILabel SkillLevelLabel2;

	// Token: 0x04003505 RID: 13573
	public UILabel SKillAttributeValue;

	// Token: 0x04003506 RID: 13574
	public UISprite SkillAttributeIcon;

	// Token: 0x04003507 RID: 13575
	public UISprite SkillInfoIconSprite;

	// Token: 0x04003508 RID: 13576
	public UILabel NextSKillAttributeValue;

	// Token: 0x04003509 RID: 13577
	public UISprite NextSKillAttributeIcon;

	// Token: 0x0400350A RID: 13578
	public UILabel CostContributeLabel;

	// Token: 0x0400350B RID: 13579
	public UILabel CostMoneyLabel;

	// Token: 0x0400350C RID: 13580
	public GameObject upgradeBtn;

	// Token: 0x0400350D RID: 13581
	public UISprite UpgradeBtnSprite;

	// Token: 0x0400350E RID: 13582
	private GuildSkillData curSelectSkillData;

	// Token: 0x0400350F RID: 13583
	private GuildSkillData nextSkillData;
}
