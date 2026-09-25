using System;
using Sproto;
using SprotoType;

// Token: 0x02000224 RID: 548
public class be_deleted_friend_handler
{
	// Token: 0x060012C5 RID: 4805 RVA: 0x0007ADFC File Offset: 0x00078FFC
	public static SprotoTypeBase be_deleted_friend_request(SprotoTypeBase req)
	{
		be_deleted_friend.request request = req as be_deleted_friend.request;
		if (request != null && request.HasCharacterId)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.FriendInfo.RemoveFriend(request.characterId);
		}
		return null;
	}
}
