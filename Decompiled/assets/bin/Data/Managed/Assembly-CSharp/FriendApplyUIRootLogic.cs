using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x020009A9 RID: 2473
public class FriendApplyUIRootLogic : SingletonUnity<FriendApplyUIRootLogic>
{
	// Token: 0x06004627 RID: 17959 RVA: 0x00162B50 File Offset: 0x00160D50
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06004628 RID: 17960 RVA: 0x00162B58 File Offset: 0x00160D58
	public void UpdateFriendList()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<friend_info> list = new List<friend_info>(playerData.FriendInfo.ApplyFriendDic.Values);
		list = FriendInfo.SortFrientList(list);
		List<PlayerInfoItemData> list2 = new List<PlayerInfoItemData>();
		for (int i = 0; i < list.Count; i++)
		{
			list2.Add(new PlayerInfoItemData
			{
				Profession = (int)list[i].profession,
				Name = list[i].name,
				Level = (int)list[i].level,
				ComboVal = (int)list[i].combValue,
				Key = list[i].friendId,
				IsEnable = true,
				GuildId = list[i].guildId,
				GuildName = list[i].guildName
			});
		}
		this.mPlayerInfoPage.Reset(list2, StrDictionary.GetDictionaryString("#{100219}", new object[0]), StrDictionary.GetDictionaryString("#{100219}", new object[0]), new DelegateDefine.OneLongParamDelegate(this.OnClickAcceptFriend));
	}

	// Token: 0x06004629 RID: 17961 RVA: 0x00162C84 File Offset: 0x00160E84
	public void OnClickAcceptFriend(long key)
	{
		approve_resverve_friend.request request = new approve_resverve_friend.request();
		request.characterId = key;
		request.isAgree = 1L;
		NetLogic.GetInstance().Send<Protocol.approve_resverve_friend>(request, null);
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		friendInfo.RemoveApply(key);
		if (SingletonUnity<SocialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SocialUIRootLogic>.Instance.UpdateSocialInfo();
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "accept_times");
	}

	// Token: 0x0600462A RID: 17962 RVA: 0x00162D0C File Offset: 0x00160F0C
	public void OnClickAddAll()
	{
		MessageBoxLogic.OpenOKCancelBox("#{100224}", "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickAllAgree), new MessageBoxLogic.OnCancelClick(this.OnNoClick), null, null);
	}

	// Token: 0x0600462B RID: 17963 RVA: 0x00162D38 File Offset: 0x00160F38
	public void OnClikcRefuseAll()
	{
		MessageBoxLogic.OpenOKCancelBox("#{100223}", "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickAllRefuse), new MessageBoxLogic.OnCancelClick(this.OnNoClick), null, null);
	}

	// Token: 0x0600462C RID: 17964 RVA: 0x00162D64 File Offset: 0x00160F64
	private void OnClickAllAgree()
	{
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		Dictionary<long, friend_info> applyFriendDic = friendInfo.ApplyFriendDic;
		if (applyFriendDic.Count + friendInfo.FriendCount >= GameDefine.MAX_FRIENT_COUNT)
		{
			NoticeLogic.AddNotifyData("#{100227}", true, false);
			return;
		}
		approve_resverve_friend.request request = new approve_resverve_friend.request();
		foreach (KeyValuePair<long, friend_info> keyValuePair in applyFriendDic)
		{
			request.characterId = keyValuePair.Value.friendId;
			request.isAgree = 1L;
			NetLogic.GetInstance().Send<Protocol.approve_resverve_friend>(request, null);
		}
		applyFriendDic.Clear();
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo.RefreshUI();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "accept_times");
	}

	// Token: 0x0600462D RID: 17965 RVA: 0x00162E5C File Offset: 0x0016105C
	private void OnNoClick()
	{
	}

	// Token: 0x0600462E RID: 17966 RVA: 0x00162E60 File Offset: 0x00161060
	public void OnClickAllRefuse()
	{
		Dictionary<long, friend_info> applyFriendDic = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo.ApplyFriendDic;
		approve_resverve_friend.request request = new approve_resverve_friend.request();
		foreach (KeyValuePair<long, friend_info> keyValuePair in applyFriendDic)
		{
			request.characterId = keyValuePair.Value.friendId;
			request.isAgree = 0L;
			NetLogic.GetInstance().Send<Protocol.approve_resverve_friend>(request, null);
		}
		applyFriendDic.Clear();
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo.RefreshUI();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "refuse_times");
	}

	// Token: 0x0600462F RID: 17967 RVA: 0x00162F30 File Offset: 0x00161130
	public void OnClickAddFriend()
	{
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (friendInfo.FriendCount >= GameDefine.MAX_FRIENT_COUNT)
		{
			NoticeLogic.AddNotifyData("#{100227}", true, false);
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FriendAddUILogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<FriendAddUILogic>.Instance.Reset();
			NetLogic.GetInstance().Send<Protocol.req_random_online_character_list>(null, null);
		}, null);
	}

	// Token: 0x04003335 RID: 13109
	public TwoColumnPlayerInfoPage mPlayerInfoPage;
}
