using System;
using Sproto;
using SprotoType;

// Token: 0x0200023F RID: 575
public class mail_update_handler
{
	// Token: 0x06001302 RID: 4866 RVA: 0x0007BD7C File Offset: 0x00079F7C
	public static SprotoTypeBase mail_update_request(SprotoTypeBase req)
	{
		mail_update.request request = req as mail_update.request;
		if (request != null)
		{
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.UpdateMailData(request);
		}
		return null;
	}
}
