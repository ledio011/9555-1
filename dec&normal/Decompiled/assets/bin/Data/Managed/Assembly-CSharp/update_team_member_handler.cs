using System;
using Sproto;
using SprotoType;

// Token: 0x020002D7 RID: 727
public class update_team_member_handler
{
	// Token: 0x06001436 RID: 5174 RVA: 0x00083318 File Offset: 0x00081518
	public static SprotoTypeBase update_team_member_request(SprotoTypeBase req)
	{
		update_team_member.request request = req as update_team_member.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.UpdateTeamMemberInfo(request);
			if (SingletonUnity<TeamUIRootNewLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
			}
			if (SingletonUnity<TeamPreparationRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamPreparationRootLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamPreparationRootLogic>.Instance.Reset();
			}
		}
		return null;
	}
}
