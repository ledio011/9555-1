using System;
using Sproto;
using SprotoType;

// Token: 0x0200021D RID: 541
public class apply_join_state_handler
{
	// Token: 0x060012B4 RID: 4788 RVA: 0x0007AA2C File Offset: 0x00078C2C
	public static SprotoTypeBase apply_join_state_request(SprotoTypeBase req)
	{
		apply_join_state.request request = req as apply_join_state.request;
		if (request != null && request.HasTeamid && request.isAgree == 0L)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.RemoveApplyTeam(request.teamid);
		}
		return null;
	}
}
