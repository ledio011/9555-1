using System;
using Sproto;
using SprotoType;

// Token: 0x02000284 RID: 644
public class ret_mount_use_color_handler
{
	// Token: 0x0600138F RID: 5007 RVA: 0x0007FDC4 File Offset: 0x0007DFC4
	public static SprotoTypeBase ret_mount_use_color_request(SprotoTypeBase req)
	{
		ret_mount_use_color.request request = req as ret_mount_use_color.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<PlayerCarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PlayerCarRootLogic>.Instance.buyColorSuccess(request);
			}
			if (Singleton<ObjManager>.Instance.MainPlayer != null)
			{
				Singleton<ObjManager>.Instance.MainPlayer.ChangeMountColor(request.mountId, request.colorId);
			}
		}
		return null;
	}
}
