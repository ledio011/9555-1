using System;
using Sproto;
using SprotoType;

// Token: 0x0200029D RID: 669
public class ret_request_update_friend_useinfo_handler
{
	// Token: 0x060013C1 RID: 5057 RVA: 0x00080964 File Offset: 0x0007EB64
	public static SprotoTypeBase ret_request_update_friend_useinfo_request(SprotoTypeBase req)
	{
		ret_request_update_friend_useinfo.request request = req as ret_request_update_friend_useinfo.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (request.HasFriend_list)
			{
				FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
				if (request.HasType && request.type == 1L)
				{
					friendInfo.FilterEnemy(request.friend_list);
				}
				else
				{
					friendInfo.FilterFriend(request.friend_list);
				}
				if (SingletonUnity<TeamInviteRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamInviteRootLogic>.Instance.gameObject))
				{
					SingletonUnity<TeamInviteRootLogic>.Instance.UpdateFriendListInfo();
				}
				if (SingletonUnity<EnemyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EnemyUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<EnemyUIRootLogic>.Instance.UpdateEnemyList();
				}
			}
		}
		return null;
	}
}
