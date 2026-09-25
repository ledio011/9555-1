using System;
using Sproto;
using SprotoType;

// Token: 0x02000256 RID: 598
public class ret_abandon_mission_handler
{
	// Token: 0x06001332 RID: 4914 RVA: 0x0007CAA0 File Offset: 0x0007ACA0
	public static SprotoTypeBase ret_abandon_mission_request(SprotoTypeBase req)
	{
		ret_abandon_mission.request request = req as ret_abandon_mission.request;
		if (request != null && request.HasMissionId)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AbandonMissionSuccess(request.missionId);
		}
		return null;
	}
}
