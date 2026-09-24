using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200093B RID: 2363
public class ExpBattleInfoRootLogic : SingletonUnity<ExpBattleInfoRootLogic>
{
	// Token: 0x060041B8 RID: 16824 RVA: 0x00139C88 File Offset: 0x00137E88
	public void EnableReset()
	{
		this.CurPage = 0;
		this.isEquipCopy = false;
		this.OnClickInfoBtn();
	}

	// Token: 0x060041B9 RID: 16825 RVA: 0x00139CA0 File Offset: 0x00137EA0
	public void ResetToTeam()
	{
		this.isEquipCopy = true;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.IsHaveTeam())
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ExpBattleInfoRoot);
			return;
		}
		NGUITools.SetActive(this.InfoObj.gameObject, false);
		NGUITools.SetActive(this.TeamObj.gameObject, true);
		this.CurPage = 2;
		this.InfoSelectSp.enabled = false;
		this.teamSelectSp.enabled = true;
		this.ResetTeam();
		this.infoAnima.ResetToBeginning();
		this.hideflag = true;
		this.OnClickHideBtn();
		this.curAnchor.enabled = true;
	}

	// Token: 0x060041BA RID: 16826 RVA: 0x00139D48 File Offset: 0x00137F48
	public void OnClickInfoBtn()
	{
		if (this.isEquipCopy)
		{
			return;
		}
		if (this.CurPage != 1)
		{
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
			this.CurPage = 1;
			this.InfoSelectSp.enabled = true;
			this.teamSelectSp.enabled = false;
		}
	}

	// Token: 0x060041BB RID: 16827 RVA: 0x00139DB0 File Offset: 0x00137FB0
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

	// Token: 0x060041BC RID: 16828 RVA: 0x00139E34 File Offset: 0x00138034
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

	// Token: 0x060041BD RID: 16829 RVA: 0x00139E6C File Offset: 0x0013806C
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

	// Token: 0x060041BE RID: 16830 RVA: 0x00139F54 File Offset: 0x00138154
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

	// Token: 0x060041BF RID: 16831 RVA: 0x00139FD8 File Offset: 0x001381D8
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

	// Token: 0x060041C0 RID: 16832 RVA: 0x0013A014 File Offset: 0x00138214
	public void UpdateInfo(notice_copy_scene_info.request request)
	{
		if (this.CurPage != 1)
		{
			return;
		}
		if (this.CurExpData == null || !this.CurExpData.ID.Equals(request.id))
		{
			this.CurExpData = DataManager.GetDailyExpDataById(request.id);
		}
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData != null && request.HasTime)
		{
			this.reamainTime = (float)(request.time - playerCommonData.GetCurServerTime());
		}
		if (this.reamainTime <= 0f)
		{
			this.TimeLabel.text = "00:00:00";
		}
		this.WaveLabel.text = string.Format("{0}/{1}", request.parm1, this.CurExpData.GroupCount);
		this.EnemyLabel.text = string.Format("{0}/{1}", request.parm2, this.CurExpData.GetWaveEnemyNum((int)request.index) * this.CurExpData.WaveCount);
		int num = this.CurExpData.GetWaveEnemyNum((int)request.index) * this.CurExpData.WaveCount * this.CurExpData.GroupCount;
		int num2 = 0;
		if (request.HasParm3)
		{
			num2 = (int)request.parm3;
		}
		this.AllKillLabel.text = string.Format("{0}/{1}", num2, num);
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			NGUITools.SetActive(base.gameObject, true);
			this.hidebtnAnima.ResetToBeginning();
			this.infoAnima.ResetToBeginning();
			this.hideflag = true;
			this.OnClickHideBtn();
			this.curAnchor.enabled = true;
			NGUITools.SetActive(this.InfoObj.gameObject, true);
			NGUITools.SetActive(this.TeamObj.gameObject, false);
		}
	}

	// Token: 0x060041C1 RID: 16833 RVA: 0x0013A1F8 File Offset: 0x001383F8
	private void Update()
	{
		if (this.reamainTime > 0f)
		{
			this.reamainTime -= Time.deltaTime;
			if (this.reamainTime < 0f)
			{
				this.reamainTime = 0f;
			}
			if (this.reamainTime <= 10f)
			{
				this.TimeLabel.color = Color.red;
			}
			else
			{
				this.TimeLabel.color = Color.white;
			}
			this.TimeLabel.text = string.Format("{0}", new TimeSpan(0, 0, (int)this.reamainTime));
		}
	}

	// Token: 0x060041C2 RID: 16834 RVA: 0x0013A2A0 File Offset: 0x001384A0
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

	// Token: 0x04002D9B RID: 11675
	public UILabel TimeLabel;

	// Token: 0x04002D9C RID: 11676
	public UILabel WaveLabel;

	// Token: 0x04002D9D RID: 11677
	public UILabel EnemyLabel;

	// Token: 0x04002D9E RID: 11678
	public UILabel AllKillLabel;

	// Token: 0x04002D9F RID: 11679
	private DailyExpData CurExpData;

	// Token: 0x04002DA0 RID: 11680
	private float reamainTime = -1f;

	// Token: 0x04002DA1 RID: 11681
	public TweenRotation hidebtnAnima;

	// Token: 0x04002DA2 RID: 11682
	public TweenPosition infoAnima;

	// Token: 0x04002DA3 RID: 11683
	private bool hideflag;

	// Token: 0x04002DA4 RID: 11684
	public UIAnchor curAnchor;

	// Token: 0x04002DA5 RID: 11685
	public GameObject InfoObj;

	// Token: 0x04002DA6 RID: 11686
	public GameObject TeamObj;

	// Token: 0x04002DA7 RID: 11687
	public List<TeamTipMemberLineLogic> TeamMemberLineList;

	// Token: 0x04002DA8 RID: 11688
	public UISprite InfoSelectSp;

	// Token: 0x04002DA9 RID: 11689
	public UISprite teamSelectSp;

	// Token: 0x04002DAA RID: 11690
	private int CurPage;

	// Token: 0x04002DAB RID: 11691
	private bool isEquipCopy;
}
