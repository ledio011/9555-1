using System;
using Sproto;
using SprotoType;

// Token: 0x020002BA RID: 698
public class set_mission_state_handler
{
	// Token: 0x060013FB RID: 5115 RVA: 0x00081B84 File Offset: 0x0007FD84
	public static SprotoTypeBase set_mission_state_request(SprotoTypeBase req)
	{
		set_mission_state.request request = req as set_mission_state.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.SetMissionState(request.missionId, (MISSION_STATE)request.missionstate, PlayerCommonData.GetServerTime());
		}
		return null;
	}
}
