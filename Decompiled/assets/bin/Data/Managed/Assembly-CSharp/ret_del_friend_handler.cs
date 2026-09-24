using System;
using Sproto;
using SprotoType;

// Token: 0x02000267 RID: 615
public class ret_del_friend_handler
{
	// Token: 0x06001355 RID: 4949 RVA: 0x0007EE48 File Offset: 0x0007D048
	public static SprotoTypeBase ret_del_friend_request(SprotoTypeBase req)
	{
		ret_del_friend.request request = req as ret_del_friend.request;
		if (request != null && request.HasCharacterId)
		{
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.RemoveFriend(request.characterId);
		}
		return null;
	}
}
