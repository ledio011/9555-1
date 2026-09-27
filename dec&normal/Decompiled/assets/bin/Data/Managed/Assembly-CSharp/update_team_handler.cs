using System;
using Sproto;
using SprotoType;

// Token: 0x020002D6 RID: 726
public class update_team_handler
{
	// Token: 0x06001434 RID: 5172 RVA: 0x000832DC File Offset: 0x000814DC
	public static SprotoTypeBase update_team_request(SprotoTypeBase req)
	{
		update_team.request request = req as update_team.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.TeamInfo.UpdateTeamInfo(request);
		}
		return null;
	}
}
