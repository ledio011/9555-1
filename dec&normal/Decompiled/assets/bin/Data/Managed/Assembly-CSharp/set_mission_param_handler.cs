using System;
using Sproto;
using SprotoType;

// Token: 0x020002B9 RID: 697
public class set_mission_param_handler
{
	// Token: 0x060013F9 RID: 5113 RVA: 0x00081B3C File Offset: 0x0007FD3C
	public static SprotoTypeBase set_mission_param_request(SprotoTypeBase req)
	{
		set_mission_param.request request = req as set_mission_param.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.SetMissionParam(request.missionId, (int)request.paramindex - 1, request.param);
		}
		return null;
	}
}
