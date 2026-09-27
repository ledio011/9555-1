using System;
using Sproto;
using SprotoType;

// Token: 0x020002B6 RID: 694
public class send_daily_mission_handler
{
	// Token: 0x060013F3 RID: 5107 RVA: 0x00081990 File Offset: 0x0007FB90
	public static SprotoTypeBase send_daily_mission_request(SprotoTypeBase req)
	{
		send_daily_mission.request request = req as send_daily_mission.request;
		if (request != null)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			missionManager.AcceptMissionSucess(request);
		}
		return null;
	}
}
