using System;
using Sproto;
using SprotoType;

// Token: 0x020002C7 RID: 711
public class sync_dance_state_info_handler
{
	// Token: 0x06001416 RID: 5142 RVA: 0x00082530 File Offset: 0x00080730
	public static SprotoTypeBase sync_dance_state_info_request(SprotoTypeBase req)
	{
		sync_dance_state_info.request request = req as sync_dance_state_info.request;
		if (request != null)
		{
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ActivityData.SyncDanceStateInfo(request);
			if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.UpdateDanceInfo();
			}
			if (SingletonUnity<DanceBtnRootLogic>.Exists)
			{
				SingletonUnity<DanceBtnRootLogic>.Instance.UpdateCDTime();
			}
		}
		return null;
	}
}
