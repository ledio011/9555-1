using System;
using UnityEngine;

// Token: 0x020009E3 RID: 2531
public class PlayerHeadInfoLogic : HeadInfoLogic
{
	// Token: 0x060047DD RID: 18397 RVA: 0x0016FA58 File Offset: 0x0016DC58
	public override void Init()
	{
		if (this.mHpLineLogic != null)
		{
			NGUITools.SetActive(this.mHpLineLogic.gameObject, false);
		}
	}

	// Token: 0x060047DE RID: 18398 RVA: 0x0016FA88 File Offset: 0x0016DC88
	public void Refresh(int titleLevel, string name, string guildName, bool isChampion)
	{
		this.mIsChampionGuild = isChampion;
		if (guildName != this.mGuildName)
		{
			if (string.IsNullOrEmpty(guildName))
			{
				this.GuildLabel.text = string.Empty;
				this.mIsChampionGuild = false;
			}
			else
			{
				this.GuildLabel.text = string.Format("<{0}>", guildName);
			}
			this.mGuildName = guildName;
		}
		if (this.mTitleLevel != titleLevel)
		{
			this.ChangePic(titleLevel);
			this.mTitleLevel = titleLevel;
		}
		this.UpdateChampionGuildPic(this.mIsChampionGuild);
		this.UpdateCampPic();
	}

	// Token: 0x060047DF RID: 18399 RVA: 0x0016FB20 File Offset: 0x0016DD20
	public void Reset(bool isMainPlayer, int titleLevel, string name, string guildName, GameDefine.CAMP_TYPE camp, bool ShowHpLine = false, bool isChampionGuild = false)
	{
		this.mCamp = camp;
		this.curValue = -1f;
		this.NameLabel.text = name;
		this.mIsChampionGuild = isChampionGuild;
		if (string.IsNullOrEmpty(guildName))
		{
			this.GuildLabel.text = string.Empty;
			this.mIsChampionGuild = false;
		}
		else
		{
			this.GuildLabel.text = string.Format("<{0}>", guildName);
		}
		this.mGuildName = guildName;
		if (isMainPlayer)
		{
			this.NameLabel.color = Color.white;
		}
		else
		{
			this.NameLabel.color = Color.green;
		}
		this.ChangePic(titleLevel);
		this.mTitleLevel = titleLevel;
		this.mShowHpLine = ShowHpLine;
		if (this.mHpLineLogic != null)
		{
			NGUITools.SetActive(this.mHpLineLogic.gameObject, false);
		}
		this.UpdateChampionGuildPic(this.mIsChampionGuild);
		this.UpdateCampPic();
	}

	// Token: 0x060047E0 RID: 18400 RVA: 0x0016FC10 File Offset: 0x0016DE10
	public void UpdateName(string name)
	{
		this.NameLabel.text = name;
	}

	// Token: 0x060047E1 RID: 18401 RVA: 0x0016FC20 File Offset: 0x0016DE20
	public void UpdateChampionGuildPic(bool isChampion)
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			if (isChampion)
			{
				NGUITools.SetActive(this.ChampionGuildPic.gameObject, true);
				this.ChampionGuildPic.width = this.GuildLabel.width + 110;
			}
			else
			{
				NGUITools.SetActive(this.ChampionGuildPic.gameObject, false);
			}
		}
		else
		{
			NGUITools.SetActive(this.ChampionGuildPic.gameObject, false);
		}
	}

	// Token: 0x060047E2 RID: 18402 RVA: 0x0016FC9C File Offset: 0x0016DE9C
	private void UpdateCampPic()
	{
		MapInfoData currentMapInofData = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		if (currentMapInofData.MapType == MAPTYPE.SURVIVE_BATTLE2 || currentMapInofData.MapType == MAPTYPE.GUILD_BATTLE)
		{
			NGUITools.SetActive(this.CampPic.gameObject, true);
			if (string.IsNullOrEmpty(this.mGuildName))
			{
				this.CampPic.transform.localPosition = Vector3.up * this.noGuildHeight;
			}
			else if (this.mIsChampionGuild)
			{
				this.CampPic.transform.localPosition = Vector3.up * this.championGuildHeight;
			}
			else
			{
				this.CampPic.transform.localPosition = Vector3.up * this.hasGuildHeight;
			}
			if (currentMapInofData.MapType == MAPTYPE.GUILD_BATTLE)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (playerData.PlayerGuild != null)
				{
					if (this.mGuildName.Equals(playerData.PlayerGuild.GuilName))
					{
						if (playerData.IsGuildBattleRedTeam)
						{
							this.CampPic.color = Color.red;
						}
						else
						{
							this.CampPic.color = Color.blue;
						}
					}
					else if (playerData.IsGuildBattleRedTeam)
					{
						this.CampPic.color = Color.blue;
					}
					else
					{
						this.CampPic.color = Color.red;
					}
				}
			}
			else if (this.mCamp == GameDefine.CAMP_TYPE.PLAYER_1)
			{
				this.CampPic.color = Color.green;
			}
			else
			{
				this.CampPic.color = Color.red;
			}
		}
		else
		{
			NGUITools.SetActive(this.CampPic.gameObject, false);
		}
	}

	// Token: 0x060047E3 RID: 18403 RVA: 0x0016FE58 File Offset: 0x0016E058
	public void ChangePic(int titleLevel)
	{
		if (titleLevel > 0)
		{
			this.TitlePic.spriteName = GameDefine.TianTi_TuBiao[titleLevel - 1];
			this.TitlePic.enabled = true;
			this.TitlePic2.enabled = true;
			UISpriteData atlasSprite = this.TitlePic.GetAtlasSprite();
			this.TitlePic.width = atlasSprite.width / 2;
			this.TitlePic.height = atlasSprite.height / 2;
			this.TitlePic.transform.localPosition = -Vector3.right * (float)(this.NameLabel.width / 2 + 20);
			this.TitlePic2.spriteName = this.TitlePic.spriteName;
			this.TitlePic2.width = this.TitlePic.width;
			this.TitlePic2.height = this.TitlePic.height;
			this.TitlePic2.transform.localPosition = Vector3.right * (float)(this.NameLabel.width / 2 + 20);
			return;
		}
		this.TitlePic.enabled = false;
		this.TitlePic2.enabled = false;
	}

	// Token: 0x060047E4 RID: 18404 RVA: 0x0016FF88 File Offset: 0x0016E188
	public override void SetHpVal(float val)
	{
		if (this.mShowHpLine && Mathf.Abs(val - this.curValue) > 1E-45f)
		{
			if (Mathf.Abs(this.curValue + 1f) > 1E-45f)
			{
				if (this.mShowHpLine)
				{
					if (this.mHpLineLogic != null)
					{
						this.ShowHpLine();
						if (this.mHpLineLogic != null)
						{
							this.mHpLineLogic.ChangeVal(val);
						}
					}
				}
				else if (this.mHpLineLogic != null)
				{
					NGUITools.SetActive(this.mHpLineLogic.gameObject, false);
				}
			}
			this.curValue = val;
		}
	}

	// Token: 0x060047E5 RID: 18405 RVA: 0x00170040 File Offset: 0x0016E240
	public void ShowHpLine()
	{
		NGUITools.SetActive(this.mHpLineLogic.gameObject, true);
		this.lastChangeTime = Time.time;
	}

	// Token: 0x060047E6 RID: 18406 RVA: 0x00170060 File Offset: 0x0016E260
	public void HideHpLine()
	{
		NGUITools.SetActive(this.mHpLineLogic.gameObject, false);
	}

	// Token: 0x060047E7 RID: 18407 RVA: 0x00170074 File Offset: 0x0016E274
	private void Update()
	{
		if (!this.mShowHpLine)
		{
			return;
		}
		if (UnityVersionUtil.IsActive(this.mHpLineLogic.gameObject) && Time.time - this.lastChangeTime > GameDefine.NPC_HP_LINE_SHOW_TIME)
		{
			this.HideHpLine();
		}
	}

	// Token: 0x04003539 RID: 13625
	public UILabel GuildLabel;

	// Token: 0x0400353A RID: 13626
	public UISprite TitlePic;

	// Token: 0x0400353B RID: 13627
	public UISprite TitlePic2;

	// Token: 0x0400353C RID: 13628
	public UISprite ChampionGuildPic;

	// Token: 0x0400353D RID: 13629
	public UISprite CampPic;

	// Token: 0x0400353E RID: 13630
	private string mGuildName = string.Empty;

	// Token: 0x0400353F RID: 13631
	private int mTitleLevel = -1;

	// Token: 0x04003540 RID: 13632
	private bool mShowHpLine;

	// Token: 0x04003541 RID: 13633
	private float curValue = -1f;

	// Token: 0x04003542 RID: 13634
	private bool mIsChampionGuild;

	// Token: 0x04003543 RID: 13635
	private float noGuildHeight = 25f;

	// Token: 0x04003544 RID: 13636
	private float hasGuildHeight = 50f;

	// Token: 0x04003545 RID: 13637
	private float championGuildHeight = 70f;

	// Token: 0x04003546 RID: 13638
	private GameDefine.CAMP_TYPE mCamp;

	// Token: 0x04003547 RID: 13639
	private float lastChangeTime;
}
