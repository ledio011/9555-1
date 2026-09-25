using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000A0C RID: 2572
public class GuildBattleGambleRoot : SingletonUnity<GuildBattleGambleRoot>
{
	// Token: 0x060049EC RID: 18924 RVA: 0x00180720 File Offset: 0x0017E920
	public void Reset()
	{
		this.VoteId = -1;
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{105041}", new object[0]);
	}

	// Token: 0x060049ED RID: 18925 RVA: 0x00180750 File Offset: 0x0017E950
	public void RefreshInfo(guild_battle_round roundInfo)
	{
		this.CurRoundInfo = roundInfo;
		for (int i = 0; i < this.GuildNameLabelList.Length; i++)
		{
			this.GuildNameLabelList[i].text = StrDictionary.GetDictionaryString("#{105070}", new object[0]);
			NGUITools.SetActive(this.VoteBtnList[i].gameObject, false);
		}
		if (roundInfo != null && roundInfo.HasBattle_team)
		{
			List<guild_battle_team> list = new List<guild_battle_team>(roundInfo.battle_team.Values);
			this.guildIdList = new long[list.Count];
			for (int j = 0; j < list.Count; j++)
			{
				checked
				{
					if (string.IsNullOrEmpty(list[j].guildName))
					{
						this.GuildNameLabelList[(int)((IntPtr)(unchecked(list[j].index - 1L)))].text = StrDictionary.GetDictionaryString("#{105070}", new object[0]);
						NGUITools.SetActive(this.VoteBtnList[(int)((IntPtr)(unchecked(list[j].index - 1L)))].gameObject, false);
					}
					else
					{
						this.GuildNameLabelList[(int)((IntPtr)(unchecked(list[j].index - 1L)))].text = list[j].guildName;
						this.guildIdList[(int)((IntPtr)(unchecked(list[j].index - 1L)))] = list[j].guildId;
						NGUITools.SetActive(this.VoteBtnList[(int)((IntPtr)(unchecked(list[j].index - 1L)))].gameObject, true);
					}
				}
			}
		}
	}

	// Token: 0x060049EE RID: 18926 RVA: 0x001808D0 File Offset: 0x0017EAD0
	public void OnClickVoteBtn0()
	{
		this.OnClickVote(0);
	}

	// Token: 0x060049EF RID: 18927 RVA: 0x001808DC File Offset: 0x0017EADC
	public void OnClickVoteBtn1()
	{
		this.OnClickVote(1);
	}

	// Token: 0x060049F0 RID: 18928 RVA: 0x001808E8 File Offset: 0x0017EAE8
	public void OnClickVoteBtn2()
	{
		this.OnClickVote(2);
	}

	// Token: 0x060049F1 RID: 18929 RVA: 0x001808F4 File Offset: 0x0017EAF4
	public void OnClickVoteBtn3()
	{
		this.OnClickVote(3);
	}

	// Token: 0x060049F2 RID: 18930 RVA: 0x00180900 File Offset: 0x0017EB00
	public void OnClickVoteBtn4()
	{
		this.OnClickVote(4);
	}

	// Token: 0x060049F3 RID: 18931 RVA: 0x0018090C File Offset: 0x0017EB0C
	public void OnClickVoteBtn5()
	{
		this.OnClickVote(5);
	}

	// Token: 0x060049F4 RID: 18932 RVA: 0x00180918 File Offset: 0x0017EB18
	public void OnClickVoteBtn6()
	{
		this.OnClickVote(6);
	}

	// Token: 0x060049F5 RID: 18933 RVA: 0x00180924 File Offset: 0x0017EB24
	public void OnClickVoteBtn7()
	{
		this.OnClickVote(7);
	}

	// Token: 0x060049F6 RID: 18934 RVA: 0x00180930 File Offset: 0x0017EB30
	public void OnClickVote(int index)
	{
		this.VoteId = index;
		for (int i = 0; i < this.VoteBtnList.Length; i++)
		{
			if (i == index)
			{
				this.VoteBtnList[i].spriteName = GameDefine.BtnIcon[0];
				this.VoteLabelList[i].text = StrDictionary.GetDictionaryString("#{105046}", new object[0]);
			}
			else
			{
				this.VoteBtnList[i].spriteName = GameDefine.BtnIcon[1];
				this.VoteLabelList[i].text = StrDictionary.GetDictionaryString("#{105042}", new object[0]);
			}
		}
		this.curVotedGuildId = this.guildIdList[index];
		this.curGuildName = this.GuildNameLabelList[index].text;
	}

	// Token: 0x060049F7 RID: 18935 RVA: 0x001809EC File Offset: 0x0017EBEC
	public void OnClickConfirmBtn()
	{
		if (this.VoteId != -1 || this.CurRoundInfo != null)
		{
			GuildBattleData battleData = DataManager.GetGuildBattleDataById(SingletonUnity<GuildBattleRootLogic>.Instance.CurBattleInfo.ID);
			if (this.curVotedGuildId > 0L)
			{
				MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{105044}", new object[]
				{
					GameMoneyHelper.GetMoneyValStr(battleData.GuessCost, battleData.GuessID),
					this.curGuildName
				}), "#{100127}", delegate
				{
					if (GameMoneyHelper.BeforeCheckBuy(GameDefine.ITEM_ID_MONEYTYPR[battleData.GuessID], battleData.GuessCost))
					{
						guild_battle_guess.request request = new guild_battle_guess.request();
						request.guildId = this.curVotedGuildId;
						NetLogic.GetInstance().Send<Protocol.guild_battle_guess>(request, null);
						SingletonUnity<GuildBattleRootLogic>.Instance.CurBattleInfo.guildId = this.curVotedGuildId;
						SingletonUnity<GuildBattleRootLogic>.Instance.UpdatePage();
						SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleGambleRoot);
					}
				}, null, null, null);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{105069}", true, false);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{105069}", true, false);
		}
	}

	// Token: 0x060049F8 RID: 18936 RVA: 0x00180AB8 File Offset: 0x0017ECB8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleGambleRoot);
	}

	// Token: 0x04003739 RID: 14137
	public UILabel TitleLabel;

	// Token: 0x0400373A RID: 14138
	public UILabel[] GuildNameLabelList;

	// Token: 0x0400373B RID: 14139
	public UISprite[] VoteBtnList;

	// Token: 0x0400373C RID: 14140
	public UILabel[] VoteLabelList;

	// Token: 0x0400373D RID: 14141
	public UISprite ConfirmBtn;

	// Token: 0x0400373E RID: 14142
	private int VoteId = -1;

	// Token: 0x0400373F RID: 14143
	private guild_battle_round CurRoundInfo;

	// Token: 0x04003740 RID: 14144
	private long curVotedGuildId = -1L;

	// Token: 0x04003741 RID: 14145
	private string curGuildName = string.Empty;

	// Token: 0x04003742 RID: 14146
	private long[] guildIdList;
}
