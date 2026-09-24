using System;
using Sproto;
using SprotoType;

// Token: 0x0200027D RID: 637
public class ret_guild_req_list_handler
{
	// Token: 0x06001381 RID: 4993 RVA: 0x0007F9C8 File Offset: 0x0007DBC8
	public static SprotoTypeBase ret_guild_req_list_request(SprotoTypeBase req)
	{
		ret_guild_req_list.request request = req as ret_guild_req_list.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (request.HasApplyGuildId && mainPlayer != null)
			{
				mainPlayer.ApplyGuildIDList = request.applyGuildId;
			}
			else if (mainPlayer != null)
			{
				mainPlayer.ApplyGuildIDList.Clear();
			}
			if (request.HasLeave_time)
			{
				mainPlayer.LeaveGuildTime = request.leave_time;
			}
			if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewGuildUIRootLogic>.Instance.ShowHasNoGuildInfo(request);
			}
		}
		return null;
	}
}
