using System;
using Sproto;
using SprotoType;

// Token: 0x0200028A RID: 650
public class ret_re_name_handler
{
	// Token: 0x0600139B RID: 5019 RVA: 0x00080024 File Offset: 0x0007E224
	public static SprotoTypeBase ret_re_name_request(SprotoTypeBase req)
	{
		ret_re_name.request request = req as ret_re_name.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasState && request.state == 1L && request.HasName)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.ReName(request.name);
				Singleton<ObjManager>.Instance.MainPlayer.reName(request.name);
			}
		}
		return null;
	}
}
