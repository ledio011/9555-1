using System;
using Sproto;
using SprotoType;

// Token: 0x020002A3 RID: 675
public class ret_search_online_character_by_name_handler
{
	// Token: 0x060013CD RID: 5069 RVA: 0x00080E28 File Offset: 0x0007F028
	public static SprotoTypeBase ret_search_online_character_by_name_request(SprotoTypeBase req)
	{
		ret_search_online_character_by_name.request request = req as ret_search_online_character_by_name.request;
		if (request != null)
		{
			if (request.HasFriend_list && request.friend_list.Count > 0)
			{
				FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
				friendInfo.FilterSearchFriend(request.friend_list);
				if (SingletonUnity<SocialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<SocialUIRootLogic>.Instance.UpdateSocialInfo();
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100222}", true, false);
			}
		}
		return null;
	}
}
