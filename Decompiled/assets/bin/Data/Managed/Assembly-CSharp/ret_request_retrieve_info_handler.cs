using System;
using Sproto;
using SprotoType;

// Token: 0x02000298 RID: 664
public class ret_request_retrieve_info_handler
{
	// Token: 0x060013B7 RID: 5047 RVA: 0x00080728 File Offset: 0x0007E928
	public static SprotoTypeBase ret_request_retrieve_info_request(SprotoTypeBase req)
	{
		ret_request_retrieve_info.request request = req as ret_request_retrieve_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.SetRetrieve(request);
			if (SingletonUnity<RetrieveRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RetrieveRootLogic>.Instance.gameObject))
			{
				SingletonUnity<RetrieveRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
