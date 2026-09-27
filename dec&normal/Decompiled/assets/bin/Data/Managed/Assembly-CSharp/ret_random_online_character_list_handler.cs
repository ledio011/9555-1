using System;
using Sproto;
using SprotoType;

// Token: 0x02000289 RID: 649
public class ret_random_online_character_list_handler
{
	// Token: 0x06001399 RID: 5017 RVA: 0x0007FF88 File Offset: 0x0007E188
	public static SprotoTypeBase ret_random_online_character_list_request(SprotoTypeBase req)
	{
		ret_random_online_character_list.request request = req as ret_random_online_character_list.request;
		if (request != null && request.HasFriend_list)
		{
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.FilterRandomFriendDic(request.friend_list);
			if (SingletonUnity<SocialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SocialUIRootLogic>.Instance.UpdateSocialInfo();
			}
			if (SingletonUnity<TeamInviteRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamInviteRootLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamInviteRootLogic>.Instance.UpdateNearByPlayerList();
			}
		}
		return null;
	}
}
