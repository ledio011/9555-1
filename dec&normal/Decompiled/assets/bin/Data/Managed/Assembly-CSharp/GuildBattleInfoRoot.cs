using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A0E RID: 2574
public class GuildBattleInfoRoot : SingletonUnity<GuildBattleInfoRoot>
{
	// Token: 0x060049FC RID: 18940 RVA: 0x00180C3C File Offset: 0x0017EE3C
	public void EnableReset()
	{
		this.CurPage = 0;
		this.isScoreRank = true;
		this.OnClickInfoBtn();
	}

	// Token: 0x060049FD RID: 18941 RVA: 0x00180C54 File Offset: 0x0017EE54
	public void OnClickInfoBtn()
	{
		if (this.CurPage != 1)
		{
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
			this.CurPage = 1;
			this.InfoSelectSp.enabled = true;
			this.teamSelectSp.enabled = false;
			for (int i = 0; i < this.infoLines.Count; i++)
			{
				NGUITools.SetActive(this.infoLines[i].gameObject, false);
			}
		}
	}

	// Token: 0x060049FE RID: 18942 RVA: 0x00180CE4 File Offset: 0x0017EEE4
	public void OnClickTeamBtn()
	{
		if (this.CurPage != 2)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (!playerData.IsHaveTeam())
			{
				NoticeLogic.AddNotifyData("#{103207}", true, false);
				return;
			}
			NGUITools.SetActive(this.InfoObj.gameObject, false);
			NGUITools.SetActive(this.TeamObj.gameObject, true);
			this.CurPage = 2;
			this.InfoSelectSp.enabled = false;
			this.teamSelectSp.enabled = true;
			this.ResetTeam();
		}
	}

	// Token: 0x060049FF RID: 18943 RVA: 0x00180D68 File Offset: 0x0017EF68
	public void ResetTeam()
	{
		if (this.CurPage != 2)
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam())
		{
			this.ResetTeamMember();
		}
	}

	// Token: 0x06004A00 RID: 18944 RVA: 0x00180DA0 File Offset: 0x0017EFA0
	public void ResetTeamMember()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsTeamLeader())
		{
			for (int i = 0; i < playerData.TeamInfo.TeamMembers.Length; i++)
			{
				this.TeamMemberLineList[i].Reset(playerData.TeamInfo.TeamMembers[i]);
			}
		}
		else
		{
			int num = 0;
			this.TeamMemberLineList[num].Reset(playerData.TeamInfo.TeamLeader);
			num++;
			for (int j = 0; j < playerData.TeamInfo.TeamMembers.Length; j++)
			{
				if (playerData.TeamInfo.TeamMembers[j].ServerId != PlayerData.MainPlayerServerId)
				{
					this.TeamMemberLineList[num].Reset(playerData.TeamInfo.TeamMembers[j]);
					num++;
				}
			}
		}
	}

	// Token: 0x06004A01 RID: 18945 RVA: 0x00180E88 File Offset: 0x0017F088
	private TeamTipMemberLineLogic GetMemberLineById(long serverId)
	{
		for (int i = 0; i < this.TeamMemberLineList.Count; i++)
		{
			if (this.TeamMemberLineList[i].CurMember != null && this.TeamMemberLineList[i].CurMember.IsValid() && this.TeamMemberLineList[i].CurMember.ServerId == serverId)
			{
				return this.TeamMemberLineList[i];
			}
		}
		return null;
	}

	// Token: 0x06004A02 RID: 18946 RVA: 0x00180F0C File Offset: 0x0017F10C
	public void UpdateMemberInfo(TeamMember member)
	{
		if (this.CurPage != 2)
		{
			return;
		}
		TeamTipMemberLineLogic memberLineById = this.GetMemberLineById(member.ServerId);
		if (memberLineById != null)
		{
			memberLineById.Reset(member);
		}
	}

	// Token: 0x06004A03 RID: 18947 RVA: 0x00180F48 File Offset: 0x0017F148
	public void UpdateInfo(guild_battle_score_info infodata)
	{
		if (this.CurPage != 1)
		{
			return;
		}
		this.curInfo = infodata;
		this.ItemsList.Clear();
		if (infodata.HasItem_info)
		{
			this.ItemsList = infodata.item_info;
		}
		this.EnemyScoreLabel.text = this.curInfo.score2.ToString();
		this.OurScoreLabel.text = this.curInfo.score1.ToString();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.selfNamelabel.text = playerData.MainPlayerAttrData.Name;
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
			this.leftisopen = false;
			this.leftAnchor.enabled = true;
			this.leftRotAni.ResetToBeginning();
			this.leftPosAni.ResetToBeginning();
			this.OnClickLefthidebtn();
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
		}
		this.RefershLeftInfo();
	}

	// Token: 0x06004A04 RID: 18948 RVA: 0x0018105C File Offset: 0x0017F25C
	private void RefershLeftInfo()
	{
		if (this.curInfo.score1 != 0L || this.curInfo.score2 != 0L)
		{
			this.BlueLineSlider.value = (float)this.curInfo.score1 / (float)(this.curInfo.score1 + this.curInfo.score2);
			this.RedLineSlider.value = (float)this.curInfo.score2 / (float)(this.curInfo.score1 + this.curInfo.score2);
		}
		else
		{
			this.BlueLineSlider.value = 0.5f;
			this.RedLineSlider.value = 0.5f;
		}
		if (this.isScoreRank)
		{
			this.ItemsList.Sort((guild_battle_item_info x, guild_battle_item_info y) => (int)y.score - (int)x.score);
			this.infoNamelabel.text = StrDictionary.GetDictionaryString("#{105021}", new object[0]);
			for (int i = 0; i < this.ItemsList.Count; i++)
			{
				if (this.ItemsList[i].id == PlayerData.MainPlayerServerId)
				{
					this.selfRanklabel.text = string.Format("NO.{0}", i + 1);
					this.SelfInfoLabel.text = this.ItemsList[i].score.ToString();
					break;
				}
			}
		}
		else
		{
			this.ItemsList.Sort((guild_battle_item_info x, guild_battle_item_info y) => (int)y.killNum - (int)x.killNum);
			this.infoNamelabel.text = StrDictionary.GetDictionaryString("#{105036}", new object[0]);
			for (int j = 0; j < this.ItemsList.Count; j++)
			{
				if (this.ItemsList[j].id == PlayerData.MainPlayerServerId)
				{
					this.selfRanklabel.text = string.Format("NO.{0}", j + 1);
					this.SelfInfoLabel.text = this.ItemsList[j].killNum.ToString();
					break;
				}
			}
		}
		int num = this.ShowTopNum - this.infoLines.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.infoLines[0].gameObject) as GameObject;
				GuildBattleInfoLine component = gameObject.GetComponent<GuildBattleInfoLine>();
				gameObject.name = string.Format("{0:D2}", this.infoLines.Count);
				gameObject.transform.parent = this.uiGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.infoLines.Add(component);
			}
		}
		for (int l = 0; l < this.infoLines.Count; l++)
		{
			if (l < this.ItemsList.Count)
			{
				NGUITools.SetActive(this.infoLines[l].gameObject, true);
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsGuildBattleRedTeam)
				{
					if (this.ItemsList[l].guildId == SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId)
					{
						this.infoLines[l].updateItem(this.ItemsList[l], l, this.isScoreRank, false);
					}
					else
					{
						this.infoLines[l].updateItem(this.ItemsList[l], l, this.isScoreRank, true);
					}
				}
				else if (this.ItemsList[l].guildId == SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId)
				{
					this.infoLines[l].updateItem(this.ItemsList[l], l, this.isScoreRank, true);
				}
				else
				{
					this.infoLines[l].updateItem(this.ItemsList[l], l, this.isScoreRank, false);
				}
			}
			else
			{
				NGUITools.SetActive(this.infoLines[l].gameObject, false);
			}
		}
		this.uiGrid.Reposition();
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsGuildBattleRedTeam)
		{
			this.selfNamelabel.color = this.redColor;
			this.selfRanklabel.color = this.redColor;
			this.SelfInfoLabel.color = this.redColor;
		}
		else
		{
			this.selfNamelabel.color = this.blueColor;
			this.selfRanklabel.color = this.blueColor;
			this.SelfInfoLabel.color = this.blueColor;
		}
	}

	// Token: 0x06004A05 RID: 18949 RVA: 0x00181564 File Offset: 0x0017F764
	public void OnClickScoreBtn()
	{
		this.selectPic.parent = this.ScoreBtn;
		this.selectPic.localPosition = Vector3.zero;
		this.selectPic.localScale = Vector3.one;
		this.isScoreRank = true;
		this.RefershLeftInfo();
	}

	// Token: 0x06004A06 RID: 18950 RVA: 0x001815B0 File Offset: 0x0017F7B0
	public void OnClickKillBtn()
	{
		this.selectPic.parent = this.KillBtn;
		this.selectPic.localPosition = Vector3.zero;
		this.selectPic.localScale = Vector3.one;
		this.isScoreRank = false;
		this.RefershLeftInfo();
	}

	// Token: 0x06004A07 RID: 18951 RVA: 0x001815FC File Offset: 0x0017F7FC
	public void OnClickLefthidebtn()
	{
		if (this.leftisopen)
		{
			this.leftRotAni.PlayReverse();
			this.leftPosAni.PlayReverse();
			this.leftisopen = false;
		}
		else
		{
			this.leftRotAni.PlayForward();
			this.leftPosAni.PlayForward();
			this.leftisopen = true;
		}
	}

	// Token: 0x04003748 RID: 14152
	public UILabel infoNamelabel;

	// Token: 0x04003749 RID: 14153
	public Transform ScoreBtn;

	// Token: 0x0400374A RID: 14154
	public Transform KillBtn;

	// Token: 0x0400374B RID: 14155
	public Transform selectPic;

	// Token: 0x0400374C RID: 14156
	private int ShowTopNum = 6;

	// Token: 0x0400374D RID: 14157
	public UIGrid uiGrid;

	// Token: 0x0400374E RID: 14158
	public List<GuildBattleInfoLine> infoLines;

	// Token: 0x0400374F RID: 14159
	public UILabel EnemyScoreLabel;

	// Token: 0x04003750 RID: 14160
	public UILabel OurScoreLabel;

	// Token: 0x04003751 RID: 14161
	public UILabel selfRanklabel;

	// Token: 0x04003752 RID: 14162
	public UILabel selfNamelabel;

	// Token: 0x04003753 RID: 14163
	public UILabel SelfInfoLabel;

	// Token: 0x04003754 RID: 14164
	public GameObject SliderObj;

	// Token: 0x04003755 RID: 14165
	public UISlider BlueLineSlider;

	// Token: 0x04003756 RID: 14166
	public UISlider RedLineSlider;

	// Token: 0x04003757 RID: 14167
	private guild_battle_score_info curInfo;

	// Token: 0x04003758 RID: 14168
	private bool isScoreRank = true;

	// Token: 0x04003759 RID: 14169
	private List<guild_battle_item_info> ItemsList = new List<guild_battle_item_info>();

	// Token: 0x0400375A RID: 14170
	public TweenRotation leftRotAni;

	// Token: 0x0400375B RID: 14171
	public TweenPosition leftPosAni;

	// Token: 0x0400375C RID: 14172
	private bool leftisopen;

	// Token: 0x0400375D RID: 14173
	public UIAnchor leftAnchor;

	// Token: 0x0400375E RID: 14174
	public GameObject InfoObj;

	// Token: 0x0400375F RID: 14175
	public GameObject TeamObj;

	// Token: 0x04003760 RID: 14176
	public List<TeamTipMemberLineLogic> TeamMemberLineList;

	// Token: 0x04003761 RID: 14177
	public UISprite InfoSelectSp;

	// Token: 0x04003762 RID: 14178
	public UISprite teamSelectSp;

	// Token: 0x04003763 RID: 14179
	private int CurPage;

	// Token: 0x04003764 RID: 14180
	private Color blueColor = new Color(0.3647059f, 0.7058824f, 1f);

	// Token: 0x04003765 RID: 14181
	private Color redColor = Color.red;
}
