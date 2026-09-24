using System;
using Sproto;
using SprotoType;

// Token: 0x02000243 RID: 579
public class notice_add_friend_handler
{
	// Token: 0x0600130B RID: 4875 RVA: 0x0007C39C File Offset: 0x0007A59C
	public static SprotoTypeBase notice_add_friend_request(SprotoTypeBase req)
	{
		notice_add_friend.request request = req as notice_add_friend.request;
		if (request != null && request.HasFriend)
		{
			friend_info friend = request.friend;
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.AddApplyFriend(friend);
		}
		return null;
	}
}
