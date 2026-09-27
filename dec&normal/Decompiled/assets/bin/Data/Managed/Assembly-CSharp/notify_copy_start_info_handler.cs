using System;
using Sproto;
using SprotoType;

// Token: 0x0200024B RID: 587
public class notify_copy_start_info_handler
{
	// Token: 0x0600131B RID: 4891 RVA: 0x0007C70C File Offset: 0x0007A90C
	public static SprotoTypeBase notify_copy_start_info_request(SprotoTypeBase req)
	{
		notify_copy_start_info.request request = req as notify_copy_start_info.request;
		if (request != null && request.HasEnd_time)
		{
			long end_time = request.end_time;
			long type = (!request.HasType) ? 0L : request.type;
			if (SingletonUnity<CountDownTimeLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CountDownTimeLogic>.Instance.gameObject))
			{
				SingletonUnity<CountDownTimeLogic>.Instance.SetReamainTime(end_time, type);
			}
		}
		return null;
	}
}
