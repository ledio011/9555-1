using System;
using Sproto;
using SprotoType;

// Token: 0x020002B2 RID: 690
public class ret_watch_video_info_handler
{
	// Token: 0x060013EB RID: 5099 RVA: 0x00081770 File Offset: 0x0007F970
	public static SprotoTypeBase ret_watch_video_info_request(SprotoTypeBase req)
	{
		ret_watch_video_info.request request = req as ret_watch_video_info.request;
		if (request != null)
		{
			if (request.HasState && request.state == 1L)
			{
				NoticeLogic.AddNotifyData("Video rewards have been released!", true, false);
			}
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
			if (SingletonDontDestoryUnity<GameManager>.Instance.OnUnityAdsStateChange != null)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.OnUnityAdsStateChange(SingletonDontDestoryUnity<GameManager>.Instance.IsUnityAdsReady);
			}
		}
		return null;
	}
}
