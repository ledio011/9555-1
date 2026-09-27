using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200095E RID: 2398
public class MultiRankSmallRootLogic : SingletonUnity<MultiRankSmallRootLogic>
{
	// Token: 0x0600431F RID: 17183 RVA: 0x0014AA78 File Offset: 0x00148C78
	public void EnableReset()
	{
		this.CurPage = 0;
		this.OnClickInfoBtn();
	}

	// Token: 0x06004320 RID: 17184 RVA: 0x0014AA88 File Offset: 0x00148C88
	public void OnClickInfoBtn()
	{
		if (this.CurPage != 1)
		{
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
			this.CurPage = 1;
			this.InfoSelectSp.enabled = true;
			this.teamSelectSp.enabled = false;
		}
	}

	// Token: 0x06004321 RID: 17185 RVA: 0x0014AAE4 File Offset: 0x00148CE4
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

	// Token: 0x06004322 RID: 17186 RVA: 0x0014AB68 File Offset: 0x00148D68
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

	// Token: 0x06004323 RID: 17187 RVA: 0x0014ABA0 File Offset: 0x00148DA0
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

	// Token: 0x06004324 RID: 17188 RVA: 0x0014AC88 File Offset: 0x00148E88
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

	// Token: 0x06004325 RID: 17189 RVA: 0x0014AD0C File Offset: 0x00148F0C
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

	// Token: 0x06004326 RID: 17190 RVA: 0x0014AD48 File Offset: 0x00148F48
	public void Reset(battle_info battleinfo)
	{
		if (this.CurPage != 1)
		{
			return;
		}
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100772}", new object[0]);
		this.ScoreLabel.text = StrDictionary.GetDictionaryString("#{100756}", new object[0]);
		this.curBattleInfo = battleinfo;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.DamageList = this.curBattleInfo.damage_list;
		int num = this.DamageList.Count - this.rankItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rankItemList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.RankGrid.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				DamageRankItemLogic component = gameObject.GetComponent<DamageRankItemLogic>();
				this.rankItemList.Add(component);
				if (this.rankItemList.Count < 10)
				{
					gameObject.gameObject.name = "0" + this.rankItemList.Count.ToString();
				}
				else
				{
					gameObject.gameObject.name = this.rankItemList.Count.ToString();
				}
			}
			this.RankGrid.Reposition();
		}
		for (int j = 0; j < this.rankItemList.Count; j++)
		{
			if (j < this.DamageList.Count)
			{
				NGUITools.SetActive(this.rankItemList[j].gameObject, true);
				this.rankItemList[j].updateItem(j + 1, this.DamageList[j]);
			}
			else
			{
				NGUITools.SetActive(this.rankItemList[j].gameObject, false);
			}
		}
		this.selfRankLabel.text = string.Format("NO.{0}", this.curBattleInfo.my_rank);
		this.selfNameLabel.text = playerData.MainPlayerAttrData.Name;
		this.selfDamageLabel.text = string.Format("{0}", this.curBattleInfo.my_damage);
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
			this.hidebtnAnima.ResetToBeginning();
			this.infoAnima.ResetToBeginning();
			this.hideflag = true;
			this.OnClickHideBtn();
			this.curAnchor.enabled = true;
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
		}
	}

	// Token: 0x06004327 RID: 17191 RVA: 0x0014B014 File Offset: 0x00149214
	public void Reset(battle_info battleinfo, GameDefine.ACTIVITY_TYPE type)
	{
		if (this.CurPage != 1)
		{
			return;
		}
		if (type != GameDefine.ACTIVITY_TYPE.BAR_FIGHT)
		{
			return;
		}
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100772}", new object[0]);
		this.ScoreLabel.text = StrDictionary.GetDictionaryString("#{100756}", new object[0]);
		this.curBattleInfo = battleinfo;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.DamageList = this.curBattleInfo.damage_list;
		this.DamageList.Sort((damage_list x, damage_list y) => (int)y.damage - (int)x.damage);
		for (int i = this.DamageList.Count - 1; i >= 6; i--)
		{
			this.DamageList.RemoveAt(i);
		}
		int num = -1;
		int num2 = -1;
		for (int j = 0; j < this.DamageList.Count; j++)
		{
			if (this.DamageList[j].id == PlayerData.MainPlayerServerId)
			{
				num2 = j + 1;
				num = (int)this.DamageList[j].damage;
				break;
			}
		}
		int num3 = this.DamageList.Count - this.rankItemList.Count;
		if (num3 > 0)
		{
			for (int k = 0; k < num3; k++)
			{
				GameObject gameObject = Object.Instantiate(this.rankItemList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.RankGrid.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				DamageRankItemLogic component = gameObject.GetComponent<DamageRankItemLogic>();
				this.rankItemList.Add(component);
				if (this.rankItemList.Count < 10)
				{
					gameObject.gameObject.name = "0" + this.rankItemList.Count.ToString();
				}
				else
				{
					gameObject.gameObject.name = this.rankItemList.Count.ToString();
				}
			}
			this.RankGrid.Reposition();
		}
		for (int l = 0; l < this.rankItemList.Count; l++)
		{
			if (l < this.DamageList.Count)
			{
				NGUITools.SetActive(this.rankItemList[l].gameObject, true);
				this.rankItemList[l].updateItem(l + 1, this.DamageList[l]);
			}
			else
			{
				NGUITools.SetActive(this.rankItemList[l].gameObject, false);
			}
		}
		this.selfRankLabel.text = string.Format("NO.{0}", num2);
		this.selfNameLabel.text = playerData.MainPlayerAttrData.Name;
		this.selfDamageLabel.text = string.Format("{0}", num);
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
			this.hidebtnAnima.ResetToBeginning();
			this.infoAnima.ResetToBeginning();
			this.hideflag = true;
			this.OnClickHideBtn();
			this.curAnchor.enabled = true;
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
		}
	}

	// Token: 0x06004328 RID: 17192 RVA: 0x0014B394 File Offset: 0x00149594
	public void Reset(ret_request_survive_top.request request)
	{
		if (this.CurPage != 1)
		{
			return;
		}
		this.TitleLabel.text = StrDictionary.GetDictionaryString("#{101596}", new object[0]);
		this.ScoreLabel.text = StrDictionary.GetDictionaryString("#{101597}", new object[0]);
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int num = request.score_infos.Count - this.rankItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rankItemList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.RankGrid.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				DamageRankItemLogic component = gameObject.GetComponent<DamageRankItemLogic>();
				this.rankItemList.Add(component);
				if (this.rankItemList.Count < 10)
				{
					gameObject.gameObject.name = "0" + this.rankItemList.Count.ToString();
				}
				else
				{
					gameObject.gameObject.name = this.rankItemList.Count.ToString();
				}
			}
			this.RankGrid.Reposition();
		}
		for (int j = 0; j < this.rankItemList.Count; j++)
		{
			if (j < request.score_infos.Count)
			{
				NGUITools.SetActive(this.rankItemList[j].gameObject, true);
				this.rankItemList[j].updateItem(j + 1, request.score_infos[j]);
			}
			else
			{
				NGUITools.SetActive(this.rankItemList[j].gameObject, false);
			}
		}
		this.selfRankLabel.text = string.Format("NO.{0}", request.my_rank);
		this.selfNameLabel.text = playerData.MainPlayerAttrData.Name;
		this.selfDamageLabel.text = string.Format("{0}", request.my_score);
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
			this.hidebtnAnima.ResetToBeginning();
			this.infoAnima.ResetToBeginning();
			this.hideflag = true;
			this.OnClickHideBtn();
			this.curAnchor.enabled = true;
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
		}
	}

	// Token: 0x06004329 RID: 17193 RVA: 0x0014B640 File Offset: 0x00149840
	public void OnClickHideBtn()
	{
		if (this.hideflag)
		{
			this.hidebtnAnima.PlayForward();
			this.infoAnima.PlayForward();
			this.hideflag = false;
		}
		else
		{
			this.hidebtnAnima.PlayReverse();
			this.infoAnima.PlayReverse();
			this.hideflag = true;
		}
	}

	// Token: 0x04002F9F RID: 12191
	public List<DamageRankItemLogic> rankItemList;

	// Token: 0x04002FA0 RID: 12192
	public List<damage_list> DamageList;

	// Token: 0x04002FA1 RID: 12193
	public UIGrid RankGrid;

	// Token: 0x04002FA2 RID: 12194
	public UILabel TitleLabel;

	// Token: 0x04002FA3 RID: 12195
	public UILabel ScoreLabel;

	// Token: 0x04002FA4 RID: 12196
	public UILabel selfRankLabel;

	// Token: 0x04002FA5 RID: 12197
	public UILabel selfNameLabel;

	// Token: 0x04002FA6 RID: 12198
	public UILabel selfDamageLabel;

	// Token: 0x04002FA7 RID: 12199
	private battle_info curBattleInfo;

	// Token: 0x04002FA8 RID: 12200
	public TweenRotation hidebtnAnima;

	// Token: 0x04002FA9 RID: 12201
	public TweenPosition infoAnima;

	// Token: 0x04002FAA RID: 12202
	private bool hideflag;

	// Token: 0x04002FAB RID: 12203
	public UIAnchor curAnchor;

	// Token: 0x04002FAC RID: 12204
	public GameObject InfoObj;

	// Token: 0x04002FAD RID: 12205
	public GameObject TeamObj;

	// Token: 0x04002FAE RID: 12206
	public List<TeamTipMemberLineLogic> TeamMemberLineList;

	// Token: 0x04002FAF RID: 12207
	public UISprite InfoSelectSp;

	// Token: 0x04002FB0 RID: 12208
	public UISprite teamSelectSp;

	// Token: 0x04002FB1 RID: 12209
	private int CurPage;
}
