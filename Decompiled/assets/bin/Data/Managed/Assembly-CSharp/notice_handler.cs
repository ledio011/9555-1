using System;
using Sproto;
using SprotoType;

// Token: 0x02000246 RID: 582
public class notice_handler
{
	// Token: 0x06001311 RID: 4881 RVA: 0x0007C4D0 File Offset: 0x0007A6D0
	public static SprotoTypeBase notice_request(SprotoTypeBase req)
	{
		notice.request request = req as notice.request;
		if (request != null)
		{
			NoticeLogic.AddNotifyData(request.notice, request.repeate, false);
		}
		return null;
	}
}
