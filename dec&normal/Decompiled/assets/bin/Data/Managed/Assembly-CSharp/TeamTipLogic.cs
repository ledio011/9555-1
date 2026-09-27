using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A08 RID: 2568
public class TeamTipLogic : MonoBehaviour
{
	// Token: 0x060049A9 RID: 18857 RVA: 0x0017D0CC File Offset: 0x0017B2CC
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060049AA RID: 18858 RVA: 0x0017D0D8 File Offset: 0x0017B2D8
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060049AB RID: 18859 RVA: 0x0017D0F8 File Offset: 0x0017B2F8
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x060049AC RID: 18860 RVA: 0x0017D104 File Offset: 0x0017B304
	public void Reset()
	{
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam())
		{
			NGUITools.SetActive(this.NoTeamRoot, false);
			NGUITools.SetActive(this.HasTeamRoot, true);
			this.ResetTeamMember();
		}
		else
		{
			NGUITools.SetActive(this.NoTeamRoot, true);
			NGUITools.SetActive(this.HasTeamRoot, false);
		}
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x0017D174 File Offset: 0x0017B374
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

	// Token: 0x060049AE RID: 18862 RVA: 0x0017D25C File Offset: 0x0017B45C
	public void UpdateMemberInfo(TeamMember member)
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			TeamTipMemberLineLogic memberLineById = this.GetMemberLineById(member.ServerId);
			if (memberLineById != null)
			{
				memberLineById.Reset(member);
			}
		}
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x0017D29C File Offset: 0x0017B49C
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

	// Token: 0x060049B0 RID: 18864 RVA: 0x0017D320 File Offset: 0x0017B520
	public void OnClickCreateBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CreateTeamRoot, delegate
		{
			WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
			SingletonUnity<CreateTeamRootLogic>.Instance.Reset(true, string.Empty);
			SingletonUnity<CreateTeamRootLogic>.Instance.RefreshPage();
			if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_CREATE)
			{
				this.CheckTutorialEvent();
			}
		}, null);
	}

	// Token: 0x060049B1 RID: 18865 RVA: 0x0017D340 File Offset: 0x0017B540
	public void OnClickSearchBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SearchTeamRoot, delegate
		{
			SingletonUnity<SearchTeamRootLogic>.Instance.Reset(string.Empty);
			WaitResponseUIRootLogic.OpenWaitBox(145, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
		}, null);
	}

	// Token: 0x060049B2 RID: 18866 RVA: 0x0017D370 File Offset: 0x0017B570
	public void OnClickHasTeamBottomPic()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
	}

	// Token: 0x040036E5 RID: 14053
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040036E6 RID: 14054
	public GameObject NoTeamRoot;

	// Token: 0x040036E7 RID: 14055
	public GameObject HasTeamRoot;

	// Token: 0x040036E8 RID: 14056
	public List<TeamTipMemberLineLogic> TeamMemberLineList;

	// Token: 0x040036E9 RID: 14057
	public UISprite CreateTeamBtn;
}
