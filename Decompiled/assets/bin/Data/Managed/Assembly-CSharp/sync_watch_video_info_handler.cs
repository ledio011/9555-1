using System;
using Sproto;
using SprotoType;

// Token: 0x020002CE RID: 718
public class sync_watch_video_info_handler
{
	// Token: 0x06001424 RID: 5156 RVA: 0x00082BBC File Offset: 0x00080DBC
	public static SprotoTypeBase sync_watch_video_info_request(SprotoTypeBase req)
	{
		sync_watch_video_info.request request = req as sync_watch_video_info.request;
		if (request != null)
		{
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			if (request.HasCur_times)
			{
				instance.PlayerData.SetVideoTimes(request.cur_times);
			}
			if (request.HasMax_times)
			{
				instance.PlayerData.SetVideoMaxTimes(request.max_times);
			}
			if (request.HasEvery_time)
			{
				instance.PlayerData.SetVideoDiamond(request.every_time);
			}
		}
		return null;
	}
}
