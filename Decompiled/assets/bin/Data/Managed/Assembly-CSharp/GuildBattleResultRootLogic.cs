using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A12 RID: 2578
public class GuildBattleResultRootLogic : SingletonUnity<GuildBattleResultRootLogic>
{
	// Token: 0x06004A20 RID: 18976 RVA: 0x00182628 File Offset: 0x00180828
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06004A21 RID: 18977 RVA: 0x00182670 File Offset: 0x00180870
	public void RefreshInfo(guild_battle_finish_info.request curinfo)
	{
		this.infoDatas.Clear();
		this.guildBattleInfo = null;
		if (curinfo.HasGuild_battle_score_info)
		{
			this.guildBattleInfo = curinfo.guild_battle_score_info;
			if (this.guildBattleInfo.HasItem_info)
			{
				this.infoDatas = this.guildBattleInfo.item_info;
			}
			this.infoDatas.Sort((guild_battle_item_info x, guild_battle_item_info y) => (int)y.score - (int)x.score);
		}
		if (this.guildBattleInfo != null && this.guildBattleInfo.HasGuildIcon1 && this.guildBattleInfo.guildIcon1 >= 0L && this.guildBattleInfo.guildIcon1 < (long)GameDefine.GuildIcon.Length)
		{
			this.LeftGuildFlag.enabled = true;
			this.LeftGuildFlag.spriteName = GameDefine.GuildIcon[(int)(checked((IntPtr)this.guildBattleInfo.guildIcon1))];
		}
		else
		{
			this.LeftGuildFlag.enabled = false;
		}
		if (this.guildBattleInfo != null && this.guildBattleInfo.HasGuildIcon2 && this.guildBattleInfo.guildIcon2 >= 0L && this.guildBattleInfo.guildIcon2 < (long)GameDefine.GuildIcon.Length)
		{
			this.RightGuildFlag.enabled = true;
			this.RightGuildFlag.spriteName = GameDefine.GuildIcon[(int)(checked((IntPtr)this.guildBattleInfo.guildIcon2))];
		}
		else
		{
			this.RightGuildFlag.enabled = false;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsGuildBattleRedTeam)
		{
			if (this.guildBattleInfo != null && this.guildBattleInfo.HasWin)
			{
				this.isWin = (this.guildBattleInfo.win != 1L);
			}
			else
			{
				this.isWin = false;
			}
		}
		else if (this.guildBattleInfo != null && this.guildBattleInfo.HasWin)
		{
			this.isWin = (this.guildBattleInfo.win == 1L);
		}
		else
		{
			this.isWin = false;
		}
		NGUITools.SetActive(this.VictoryRoot, this.isWin);
		NGUITools.SetActive(this.FailRoot, !this.isWin);
		this.OnClickResultBtn();
	}

	// Token: 0x06004A22 RID: 18978 RVA: 0x001828AC File Offset: 0x00180AAC
	public void OnClickResultBtn()
	{
		this.resultBtn.spriteName = "CZ_huaDongBG_1";
		this.scoreBtn.spriteName = "CZ_huaDongBG";
		NGUITools.SetActive(this.ResultRoot, true);
		NGUITools.SetActive(this.ScoreRoot, false);
		if (this.isWin)
		{
			this.BattleInfoLabel.text = StrDictionary.GetDictionaryString("#{105027}", new object[0]);
		}
		else
		{
			this.BattleInfoLabel.text = StrDictionary.GetDictionaryString("#{105028}", new object[0]);
		}
		if (this.guildBattleInfo != null)
		{
			if (this.guildBattleInfo.HasGuildName1)
			{
				this.LeftGuildName.text = this.guildBattleInfo.guildName1;
				this.LeftRankLabel.text = string.Format("{0}: {1}", StrDictionary.GetDictionaryString("#{105021}", new object[0]), this.guildBattleInfo.score1);
			}
			else
			{
				this.LeftGuildName.text = string.Empty;
				this.LeftRankLabel.text = string.Empty;
			}
			if (this.guildBattleInfo.HasGuildName2)
			{
				this.RightGuildName.text = this.guildBattleInfo.guildName2;
				this.RightRankLabel.text = string.Format("{0}: {1}", StrDictionary.GetDictionaryString("#{105021}", new object[0]), this.guildBattleInfo.score2);
			}
			else
			{
				this.RightGuildName.text = string.Empty;
				this.RightRankLabel.text = string.Empty;
			}
			for (int i = 0; i < this.TopLabel.Length; i++)
			{
				if (i < this.infoDatas.Count)
				{
					NGUITools.SetActive(this.TopObj[i], true);
					this.TopLabel[i].text = this.infoDatas[i].name;
				}
				else
				{
					NGUITools.SetActive(this.TopObj[i], false);
					this.TopLabel[i].text = string.Empty;
				}
			}
		}
	}

	// Token: 0x06004A23 RID: 18979 RVA: 0x00182AC0 File Offset: 0x00180CC0
	public void OnClickScoreBtn()
	{
		this.resultBtn.spriteName = "CZ_huaDongBG";
		this.scoreBtn.spriteName = "CZ_huaDongBG_1";
		NGUITools.SetActive(this.ResultRoot, false);
		NGUITools.SetActive(this.ScoreRoot, true);
		int num = Mathf.Min(this.infoDatas.Count, this.lineMinCount) - this.ScoreLines.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.ScoreLines[0].gameObject) as GameObject;
				GuildBattleScoreLine component = gameObject.GetComponent<GuildBattleScoreLine>();
				gameObject.name = string.Format("{0:D2}", this.ScoreLines.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.ScoreLines.Add(component);
			}
		}
		for (int j = 0; j < this.ScoreLines.Count; j++)
		{
			if (j < this.infoDatas.Count)
			{
				NGUITools.SetActive(this.ScoreLines[j].gameObject, true);
				this.ScoreLines[j].UpdateInfo(this.infoDatas[j], j);
			}
			else
			{
				NGUITools.SetActive(this.ScoreLines[j].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.ScoreLines.Count;
		this.WrapContentBottomWidget.height = this.ScoreLines.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
	}

	// Token: 0x06004A24 RID: 18980 RVA: 0x00182CAC File Offset: 0x00180EAC
	public void OnClickExitBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x06004A25 RID: 18981 RVA: 0x00182CBC File Offset: 0x00180EBC
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		GuildBattleScoreLine itemLogic = this.ScoreLines[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x06004A26 RID: 18982 RVA: 0x00182CE4 File Offset: 0x00180EE4
	private void ResetItemLine(GuildBattleScoreLine itemLogic, int idx)
	{
		if (idx < this.infoDatas.Count)
		{
			itemLogic.UpdateInfo(this.infoDatas[idx], idx);
		}
	}

	// Token: 0x04003780 RID: 14208
	public GameObject VictoryRoot;

	// Token: 0x04003781 RID: 14209
	public GameObject FailRoot;

	// Token: 0x04003782 RID: 14210
	public GameObject ResultRoot;

	// Token: 0x04003783 RID: 14211
	public GameObject ScoreRoot;

	// Token: 0x04003784 RID: 14212
	public UILabel BattleInfoLabel;

	// Token: 0x04003785 RID: 14213
	public UILabel LeftGuildName;

	// Token: 0x04003786 RID: 14214
	public UILabel LeftRankLabel;

	// Token: 0x04003787 RID: 14215
	public UILabel RightGuildName;

	// Token: 0x04003788 RID: 14216
	public UILabel RightRankLabel;

	// Token: 0x04003789 RID: 14217
	public GameObject[] TopObj;

	// Token: 0x0400378A RID: 14218
	public UILabel[] TopLabel;

	// Token: 0x0400378B RID: 14219
	public UISprite resultBtn;

	// Token: 0x0400378C RID: 14220
	public UISprite scoreBtn;

	// Token: 0x0400378D RID: 14221
	public List<GuildBattleScoreLine> ScoreLines;

	// Token: 0x0400378E RID: 14222
	private List<guild_battle_item_info> infoDatas = new List<guild_battle_item_info>();

	// Token: 0x0400378F RID: 14223
	private guild_battle_score_info guildBattleInfo;

	// Token: 0x04003790 RID: 14224
	public UIWrapContentNew uiWrapContent;

	// Token: 0x04003791 RID: 14225
	private int lineMinCount = 6;

	// Token: 0x04003792 RID: 14226
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04003793 RID: 14227
	public UIScrollView uiScrollView;

	// Token: 0x04003794 RID: 14228
	public UISprite LeftGuildFlag;

	// Token: 0x04003795 RID: 14229
	public UISprite RightGuildFlag;

	// Token: 0x04003796 RID: 14230
	private bool isWin;
}
