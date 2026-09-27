using System;
using Sproto;
using SprotoType;

// Token: 0x0200021E RID: 542
public class apply_join_team_handler
{
	// Token: 0x060012B6 RID: 4790 RVA: 0x0007AA80 File Offset: 0x00078C80
	public static SprotoTypeBase apply_join_team_request(SprotoTypeBase req)
	{
		apply_join_team.request request = req as apply_join_team.request;
		if (request != null && request.HasMember)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.AddApplyMember(request.member);
		}
		return null;
	}
}
