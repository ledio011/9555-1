using System;
using Sproto;
using SprotoType;

// Token: 0x02000225 RID: 549
public class cancel_apply_join_team_handler
{
	// Token: 0x060012C7 RID: 4807 RVA: 0x0007AE48 File Offset: 0x00079048
	public static SprotoTypeBase cancel_apply_join_team_request(SprotoTypeBase req)
	{
		cancel_apply_join_team.request request = req as cancel_apply_join_team.request;
		if (request != null && request.HasId)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.RemoveApplyMember(request.id);
		}
		return null;
	}
}
