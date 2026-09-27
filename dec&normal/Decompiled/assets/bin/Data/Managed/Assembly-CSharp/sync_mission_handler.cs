using System;
using Sproto;
using SprotoType;

// Token: 0x020002CB RID: 715
public class sync_mission_handler
{
	// Token: 0x0600141E RID: 5150 RVA: 0x00082AA0 File Offset: 0x00080CA0
	public static SprotoTypeBase sync_mission_request(SprotoTypeBase req)
	{
		sync_mission.request request = req as sync_mission.request;
		if (request != null && SingletonDontDestoryUnity<GameManager>.Instance.MissionManager != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.SyncMissionList(request);
		}
		return null;
	}
}
