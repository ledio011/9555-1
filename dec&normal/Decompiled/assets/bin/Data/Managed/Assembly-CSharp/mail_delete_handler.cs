using System;
using Sproto;
using SprotoType;

// Token: 0x0200023E RID: 574
public class mail_delete_handler
{
	// Token: 0x06001300 RID: 4864 RVA: 0x0007BD30 File Offset: 0x00079F30
	public static SprotoTypeBase mail_delete_request(SprotoTypeBase req)
	{
		mail_delete.request request = req as mail_delete.request;
		if (request != null && request.HasMailId)
		{
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.DelMail(request.mailId);
		}
		return null;
	}
}
