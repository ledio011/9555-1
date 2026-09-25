using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class invite_join_team_handler
{
	// Token: 0x060012F8 RID: 4856 RVA: 0x0007BB6C File Offset: 0x00079D6C
	public static SprotoTypeBase invite_join_team_request(SprotoTypeBase req)
	{
		invite_join_team.request request = req as invite_join_team.request;
		if (request != null)
		{
			invite_join_team_handler.characterId = request.member.id;
			invite_join_team_handler.teamId = request.teamid;
			if (UIManager.IsUnlockTutorialEnable())
			{
				invite_join_team_handler.DisagreeInvite();
				return null;
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsCanInviteTeam(invite_join_team_handler.teamId))
			{
				string goalid = string.Empty;
				if (request.HasGoalId)
				{
					goalid = request.goalId;
				}
				NewMessageUIRootLogic.AddteamInfo(new InviteTeamInfo(invite_join_team_handler.characterId, invite_join_team_handler.teamId, request.member.name, (long)Time.time, goalid));
			}
			else
			{
				invite_join_team_handler.DisagreeInvite();
			}
		}
		return null;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x0007BC14 File Offset: 0x00079E14
	public static void AgreeInvite()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TEAM))
		{
			ret_invite_join_team.request request = new ret_invite_join_team.request();
			request.ok = 1L;
			request.id = invite_join_team_handler.characterId;
			NetLogic.GetInstance().Send<Protocol.ret_invite_join_team>(request, null);
			if (invite_join_team_handler.teamId != -1L)
			{
				req_join_team.request request2 = new req_join_team.request();
				request2.teamid = invite_join_team_handler.teamId;
				request2.isapply = true;
				NetLogic.GetInstance().Send<Protocol.req_join_team>(request2, null);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{100276}", true, false);
		}
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x0007BCA0 File Offset: 0x00079EA0
	public static void DisagreeInvite()
	{
		ret_invite_join_team.request request = new ret_invite_join_team.request();
		request.ok = 0L;
		request.id = invite_join_team_handler.characterId;
		NetLogic.GetInstance().Send<Protocol.ret_invite_join_team>(request, null);
	}

	// Token: 0x040017E1 RID: 6113
	private static long characterId = -1L;

	// Token: 0x040017E2 RID: 6114
	private static long teamId = -1L;
}
