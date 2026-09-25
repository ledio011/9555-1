using System;
using Sproto;
using SprotoType;

// Token: 0x020002CC RID: 716
public class sync_random_team_state_handler
{
	// Token: 0x06001420 RID: 5152 RVA: 0x00082AE4 File Offset: 0x00080CE4
	public static SprotoTypeBase sync_random_team_state_request(SprotoTypeBase req)
	{
		sync_random_team_state.request request = req as sync_random_team_state.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.TeamInfo.UpdateMatchState(request);
		}
		return null;
	}
}
