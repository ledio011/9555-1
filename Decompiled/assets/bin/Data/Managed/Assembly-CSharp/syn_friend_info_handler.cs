using System;
using Sproto;
using SprotoType;

// Token: 0x020002C1 RID: 705
public class syn_friend_info_handler
{
	// Token: 0x0600140A RID: 5130 RVA: 0x00081F10 File Offset: 0x00080110
	public static SprotoTypeBase syn_friend_info_request(SprotoTypeBase req)
	{
		syn_friend_info.request request = req as syn_friend_info.request;
		if (request != null && request.HasFriend)
		{
			friend_info friend = request.friend;
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.UpdateFriendInfo(friend);
		}
		return null;
	}
}
