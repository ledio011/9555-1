using System;
using Sproto;
using SprotoType;

// Token: 0x02000268 RID: 616
public class ret_domin_info_handler
{
	// Token: 0x06001357 RID: 4951 RVA: 0x0007EE94 File Offset: 0x0007D094
	public static SprotoTypeBase ret_domin_info_request(SprotoTypeBase req)
	{
		ret_domin_info.request request = req as ret_domin_info.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.UpdateDominInfo(request);
		}
		return null;
	}
}
