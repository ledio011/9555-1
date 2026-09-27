using System;
using Sproto;
using SprotoType;

// Token: 0x020002C2 RID: 706
public class syn_rank_pvp_data_handler
{
	// Token: 0x0600140C RID: 5132 RVA: 0x00081F5C File Offset: 0x0008015C
	public static SprotoTypeBase syn_rank_pvp_data_request(SprotoTypeBase req)
	{
		syn_rank_pvp_data.request request = req as syn_rank_pvp_data.request;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (request != null)
		{
			playerData.RankPVPData.UpdateRankPvPData(request);
			if (SingletonUnity<RankPVPUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RankPVPUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<RankPVPUIRootLogic>.Instance.ResetPlayerInfo();
			}
		}
		return null;
	}
}
