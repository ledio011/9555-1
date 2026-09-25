using System;
using Sproto;
using SprotoType;

// Token: 0x02000257 RID: 599
public class ret_accept_mission_handler
{
	// Token: 0x06001334 RID: 4916 RVA: 0x0007CAE4 File Offset: 0x0007ACE4
	public static SprotoTypeBase ret_accept_mission_request(SprotoTypeBase req)
	{
		ret_accept_mission.request request = req as ret_accept_mission.request;
		if (request != null)
		{
			if (GameManager.IsSupportCurDataVersion184())
			{
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMissionSuccess(request.mission, PlayerCommonData.GetServerTime(), true);
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMissionSuccess(request.missionId, PlayerCommonData.GetServerTime(), true);
			}
		}
		return null;
	}
}
