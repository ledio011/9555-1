using System;
using Sproto;
using SprotoType;

// Token: 0x020002CF RID: 719
public class tiantti_result_handler
{
	// Token: 0x06001426 RID: 5158 RVA: 0x00082C3C File Offset: 0x00080E3C
	public static SprotoTypeBase tiantti_result_request(SprotoTypeBase req)
	{
		tiantti_result.request request = req as tiantti_result.request;
		if (request != null)
		{
			if (request.win)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyMissionShowRoot, delegate
				{
					SingletonUnity<CopyMissionShowRootLogic>.Instance.ResetRankPvP(request);
				}, null);
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.RankPVPData.UpdateRankPvpInfo(request);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("PVP", "PVP", "success");
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CopyFailShowRoot, delegate
				{
					SingletonUnity<CopyFailShowRootLogic>.Instance.ResetRankPvp(request.items);
				}, null);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("PVP", "PVP", "failure");
			}
			CountDownTimeLogic.CloseTime();
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CompleteMission();
		}
		return null;
	}
}
