using System;
using Sproto;
using SprotoType;

// Token: 0x02000258 RID: 600
public class ret_add_friend_handler
{
	// Token: 0x06001336 RID: 4918 RVA: 0x0007CB50 File Offset: 0x0007AD50
	public static SprotoTypeBase ret_add_friend_request(SprotoTypeBase req)
	{
		ret_add_friend.request request = req as ret_add_friend.request;
		if (request != null && request.HasFriend)
		{
			friend_info friend = request.friend;
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.AddFriend(friend);
		}
		return null;
	}
}
