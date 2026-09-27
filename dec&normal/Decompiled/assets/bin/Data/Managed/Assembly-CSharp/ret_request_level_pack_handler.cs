using System;
using Sproto;
using SprotoType;

// Token: 0x02000296 RID: 662
public class ret_request_level_pack_handler
{
	// Token: 0x060013B3 RID: 5043 RVA: 0x00080660 File Offset: 0x0007E860
	public static SprotoTypeBase ret_request_level_pack_request(SprotoTypeBase req)
	{
		ret_request_level_pack.request request = req as ret_request_level_pack.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.InitLevelPack(request);
			if (SingletonUnity<LevelPackRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<LevelPackRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<LevelPackRewardRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
