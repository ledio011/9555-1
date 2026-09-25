using System;
using Sproto;
using SprotoType;

// Token: 0x02000254 RID: 596
public class req_invite_team_result_handler
{
	// Token: 0x0600132E RID: 4910 RVA: 0x0007CA3C File Offset: 0x0007AC3C
	public static SprotoTypeBase req_invite_team_result_request(SprotoTypeBase req)
	{
		req_invite_team_result.request request = req as req_invite_team_result.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.TeamInfo.RemoveInvitedPerson(request.id);
		}
		return null;
	}
}
