using System;
using Sproto;
using SprotoType;

// Token: 0x02000261 RID: 609
public class ret_complete_mission_handler
{
	// Token: 0x06001349 RID: 4937 RVA: 0x0007E960 File Offset: 0x0007CB60
	public static SprotoTypeBase ret_complete_mission_request(SprotoTypeBase req)
	{
		ret_complete_mission.request request = req as ret_complete_mission.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.CompleteMissionSuccess(request.missionId);
		}
		return null;
	}
}
