using System;
using System.Collections.Generic;

// Token: 0x02000A02 RID: 2562
public class CopyTeamTipRoot : SingletonUnity<CopyTeamTipRoot>
{
	// Token: 0x06004950 RID: 18768 RVA: 0x0017ABC0 File Offset: 0x00178DC0
	public void Reset()
	{
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam())
		{
			this.ResetTeamMember();
		}
		else
		{
			NGUITools.SetActive(base.gameObject, false);
		}
	}

	// Token: 0x06004951 RID: 18769 RVA: 0x0017AC0C File Offset: 0x00178E0C
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

	// Token: 0x06004952 RID: 18770 RVA: 0x0017ACF4 File Offset: 0x00178EF4
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

	// Token: 0x06004953 RID: 18771 RVA: 0x0017AD78 File Offset: 0x00178F78
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

	// Token: 0x04003696 RID: 13974
	public List<TeamTipMemberLineLogic> TeamMemberLineList;
}
