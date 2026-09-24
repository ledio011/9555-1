using System;
using Sproto;
using SprotoType;

// Token: 0x020002C6 RID: 710
public class sync_copyscenes_info_handler
{
	// Token: 0x06001414 RID: 5140 RVA: 0x0008246C File Offset: 0x0008066C
	public static SprotoTypeBase sync_copyscenes_info_request(SprotoTypeBase req)
	{
		sync_copyscenes_info.request request = req as sync_copyscenes_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.SyncCopyInfo(request.copyscenes);
			if (SingletonUnity<CreateTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CreateTeamRootLogic>.Instance.gameObject))
			{
				SingletonUnity<CreateTeamRootLogic>.Instance.RefreshPage();
			}
			else if (SingletonUnity<SearchTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SearchTeamRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SearchTeamRootLogic>.Instance.Init();
			}
			else if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewDailyCopyUIRootLogic>.Instance.RefreshCopyPage();
			}
		}
		return null;
	}
}
