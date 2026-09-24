using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x020001DB RID: 475
public class FriendInfo
{
	// Token: 0x0600110E RID: 4366 RVA: 0x0006E054 File Offset: 0x0006C254
	public FriendInfo()
	{
		this.Init();
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x0600110F RID: 4367 RVA: 0x0006E0C4 File Offset: 0x0006C2C4
	public HaoYouRoot_TYPE OpenType
	{
		get
		{
			return this.mOpenType;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001110 RID: 4368 RVA: 0x0006E0CC File Offset: 0x0006C2CC
	// (set) Token: 0x06001111 RID: 4369 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
	public int FriendCount
	{
		get
		{
			return this.mFriendCount;
		}
		set
		{
			this.mFriendCount = value;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001112 RID: 4370 RVA: 0x0006E0E0 File Offset: 0x0006C2E0
	// (set) Token: 0x06001113 RID: 4371 RVA: 0x0006E0E8 File Offset: 0x0006C2E8
	public int EnemyCount
	{
		get
		{
			return this.mEnemyCount;
		}
		set
		{
			this.mEnemyCount = value;
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001114 RID: 4372 RVA: 0x0006E0F4 File Offset: 0x0006C2F4
	// (set) Token: 0x06001115 RID: 4373 RVA: 0x0006E0FC File Offset: 0x0006C2FC
	public int FriendOnlineCount
	{
		get
		{
			return this.mFriendOnlineCount;
		}
		set
		{
			this.mFriendOnlineCount = value;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001116 RID: 4374 RVA: 0x0006E108 File Offset: 0x0006C308
	// (set) Token: 0x06001117 RID: 4375 RVA: 0x0006E110 File Offset: 0x0006C310
	public Dictionary<long, friend_info> MainPlayerFriendDic
	{
		get
		{
			return this.mMainPlayerFriendDic;
		}
		set
		{
			this.mMainPlayerFriendDic = value;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x06001118 RID: 4376 RVA: 0x0006E11C File Offset: 0x0006C31C
	// (set) Token: 0x06001119 RID: 4377 RVA: 0x0006E124 File Offset: 0x0006C324
	public Dictionary<long, friend_info> MainPlayerEnemyDic
	{
		get
		{
			return this.mMainPlayerEnemyDic;
		}
		set
		{
			this.mMainPlayerEnemyDic = value;
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0006E130 File Offset: 0x0006C330
	public void SetFlagValue(int index, bool setOrClear)
	{
		if (setOrClear)
		{
			this.tipsflag |= 1 << index;
		}
		else
		{
			this.tipsflag &= ~(1 << index);
		}
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0006E170 File Offset: 0x0006C370
	public int GetFlagValue(int index)
	{
		return this.tipsflag >> index & 1;
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0006E180 File Offset: 0x0006C380
	public bool GetTips()
	{
		return this.tipsflag > 0;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0006E18C File Offset: 0x0006C38C
	public void FilterFriend(Dictionary<long, friend_info> dict)
	{
		this.mMainPlayerFriendDic.Clear();
		this.ApplyFriendDic.Clear();
		foreach (KeyValuePair<long, friend_info> keyValuePair in dict)
		{
			if (keyValuePair.Value.friendType == 2L)
			{
				this.ApplyFriendDic.Add(keyValuePair.Key, keyValuePair.Value);
			}
			else if (keyValuePair.Value.friendType != 1L)
			{
				this.mMainPlayerFriendDic.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		this.GetFriendCount();
		this.RefreshUI();
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0006E268 File Offset: 0x0006C468
	public void FilterEnemy(Dictionary<long, friend_info> dict)
	{
		this.mMainPlayerEnemyDic.Clear();
		foreach (KeyValuePair<long, friend_info> keyValuePair in dict)
		{
			if (keyValuePair.Value.friendType == 6L)
			{
				long friendId = keyValuePair.Value.friendId;
				keyValuePair.Value.friendId = keyValuePair.Value.timeInfo;
				keyValuePair.Value.timeInfo = friendId;
				this.mMainPlayerEnemyDic.Add(keyValuePair.Value.friendId, keyValuePair.Value);
			}
		}
		this.GetEnemyCount();
		this.RefreshUI();
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0006E33C File Offset: 0x0006C53C
	public static List<friend_info> SortFrientList(List<friend_info> list)
	{
		list.Sort(delegate(friend_info x, friend_info y)
		{
			if (x.state == y.state)
			{
				return -(int)(x.combValue - y.combValue);
			}
			return -(int)(x.state - y.state);
		});
		return list;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0006E370 File Offset: 0x0006C570
	public static List<friend_info> SortEnemyList(List<friend_info> list)
	{
		list.Sort(delegate(friend_info x, friend_info y)
		{
			if (x.state == y.state)
			{
				return -(int)(x.friendScore - y.friendScore);
			}
			return -(int)(x.state - y.state);
		});
		return list;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0006E3A4 File Offset: 0x0006C5A4
	public HaoYouRoot_TYPE GetOpenType()
	{
		int flagValue = this.GetFlagValue(3);
		if (flagValue > 0)
		{
			return HaoYouRoot_TYPE.MAIL_LIST;
		}
		flagValue = this.GetFlagValue(2);
		if (flagValue > 0)
		{
			return HaoYouRoot_TYPE.APPLY_FRIEND;
		}
		return HaoYouRoot_TYPE.MAIL_LIST;
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0006E3D4 File Offset: 0x0006C5D4
	public bool IsAlreadyFriend(long id)
	{
		return this.mMainPlayerFriendDic.ContainsKey(id);
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0006E3E4 File Offset: 0x0006C5E4
	public bool IsAlreadyEnemy(long id)
	{
		return this.mMainPlayerEnemyDic.ContainsKey(id);
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0006E3F4 File Offset: 0x0006C5F4
	public bool RemoveFriend(long id)
	{
		bool flag = this.mMainPlayerFriendDic.Remove(id);
		if (flag)
		{
			this.mFriendCount--;
			this.RefreshUI();
		}
		return flag;
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0006E42C File Offset: 0x0006C62C
	public bool RemoveEnemy(long id)
	{
		bool flag = this.mMainPlayerEnemyDic.Remove(id);
		if (flag)
		{
			this.mEnemyCount--;
			this.RefreshUI();
		}
		return flag;
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x0006E464 File Offset: 0x0006C664
	public void GetFriendCount()
	{
		this.mFriendCount = 0;
		foreach (KeyValuePair<long, friend_info> keyValuePair in this.mMainPlayerFriendDic)
		{
			this.mFriendCount++;
		}
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0006E4D8 File Offset: 0x0006C6D8
	public void GetEnemyCount()
	{
		this.mEnemyCount = 0;
		foreach (KeyValuePair<long, friend_info> keyValuePair in this.mMainPlayerEnemyDic)
		{
			this.mEnemyCount++;
		}
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0006E54C File Offset: 0x0006C74C
	public void AddFriend(friend_info friend)
	{
		if (friend.friendType == 6L)
		{
			long friendId = friend.friendId;
			friend.friendId = friend.timeInfo;
			friend.timeInfo = friendId;
			this.AddEnemy(friend);
		}
		else
		{
			if (this.mMainPlayerFriendDic.ContainsKey(friend.friendId))
			{
				this.mMainPlayerFriendDic[friend.friendId] = friend;
			}
			else
			{
				this.mMainPlayerFriendDic.Add(friend.friendId, friend);
			}
			this.GetFriendCount();
			this.RefreshUI();
		}
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0006E5D8 File Offset: 0x0006C7D8
	public void AddEnemy(friend_info enemyid)
	{
		if (this.mMainPlayerEnemyDic.ContainsKey(enemyid.friendId))
		{
			this.mMainPlayerEnemyDic[enemyid.friendId] = enemyid;
		}
		else
		{
			this.mMainPlayerEnemyDic.Add(enemyid.friendId, enemyid);
		}
		this.GetEnemyCount();
		this.RefreshUI();
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0006E630 File Offset: 0x0006C830
	public void AddApplyFriend(friend_info friend)
	{
		if (this.ApplyFriendDic.ContainsKey(friend.friendId))
		{
			this.ApplyFriendDic[friend.friendId] = friend;
		}
		else
		{
			this.ApplyFriendDic.Add(friend.friendId, friend);
		}
		this.RefreshUI();
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0006E684 File Offset: 0x0006C884
	public bool RemoveApply(long id)
	{
		bool flag = this.ApplyFriendDic.Remove(id);
		if (flag)
		{
			this.RefreshUI();
		}
		return flag;
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0006E6AC File Offset: 0x0006C8AC
	public void RefreshUI()
	{
		if (SingletonUnity<SocialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SocialUIRootLogic>.Instance.UpdateSocialInfo();
		}
		this.SetFlagValue(2, this.IshavefriendApply());
		this.SetFlagValue(3, this.IsHaveMailTips());
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.IsHaveMailTips(), GameDefine.TIPS_TYPE.MAIL);
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.IshavefriendApply() || this.IsHaveMailTips(), GameDefine.TIPS_TYPE.SOCIAL);
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x0006E760 File Offset: 0x0006C960
	public bool IshavefriendApply()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL_APPLY) && this.ApplyFriendDic.Count > 0;
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0006E798 File Offset: 0x0006C998
	public void UpdateFriendInfo(friend_info friend)
	{
		if (this.IsAlreadyFriend(friend.friendId))
		{
			this.RemoveFriend(friend.friendId);
		}
		this.mMainPlayerFriendDic.Add(friend.friendId, friend);
		this.GetFriendCount();
		this.RefreshUI();
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x0006E7E4 File Offset: 0x0006C9E4
	public void UpdateEnemyInfo(friend_info enemy)
	{
		if (this.IsAlreadyEnemy(enemy.friendId))
		{
			this.RemoveEnemy(enemy.friendId);
		}
		this.mMainPlayerEnemyDic.Add(enemy.friendId, enemy);
		this.GetEnemyCount();
		this.RefreshUI();
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06001130 RID: 4400 RVA: 0x0006E830 File Offset: 0x0006CA30
	// (set) Token: 0x06001131 RID: 4401 RVA: 0x0006E838 File Offset: 0x0006CA38
	public Dictionary<long, friend_info> MainPlayerRandomFriendDic
	{
		get
		{
			return this.mMainPlayerRandomFriendDic;
		}
		set
		{
			this.mMainPlayerRandomFriendDic = value;
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x0006E844 File Offset: 0x0006CA44
	public void FilterRandomFriendDic(List<friend_info> list)
	{
		this.mMainPlayerRandomFriendDic.Clear();
		foreach (friend_info friend_info in list)
		{
			if (friend_info.friendType != 1L)
			{
				this.mMainPlayerRandomFriendDic.Add(friend_info.characterId, friend_info);
			}
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0006E8C8 File Offset: 0x0006CAC8
	public void FilterSearchFriend(List<friend_info> list)
	{
		this.mMainPlayerSearchFriendDic.Clear();
		foreach (friend_info friend_info in list)
		{
			if (friend_info.friendType != 1L)
			{
				this.mMainPlayerSearchFriendDic.Add(friend_info.characterId, friend_info);
			}
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x06001134 RID: 4404 RVA: 0x0006E94C File Offset: 0x0006CB4C
	// (set) Token: 0x06001135 RID: 4405 RVA: 0x0006E954 File Offset: 0x0006CB54
	public Dictionary<long, friend_info> MainPlayerSearchFriendDic
	{
		get
		{
			return this.mMainPlayerSearchFriendDic;
		}
		set
		{
			this.mMainPlayerSearchFriendDic = value;
		}
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0006E960 File Offset: 0x0006CB60
	public friend_info GetFriendById(long id)
	{
		if (this.mMainPlayerFriendDic.ContainsKey(id))
		{
			return this.mMainPlayerFriendDic[id];
		}
		return null;
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0006E984 File Offset: 0x0006CB84
	public friend_info GetEnemyById(long id)
	{
		if (this.mMainPlayerEnemyDic.ContainsKey(id))
		{
			return this.mMainPlayerEnemyDic[id];
		}
		return null;
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0006E9A8 File Offset: 0x0006CBA8
	public bool IsCanAddFriend()
	{
		return this.FriendCount < GameDefine.MAX_FRIENT_COUNT;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0006E9B8 File Offset: 0x0006CBB8
	public bool IsCanAddEnemy()
	{
		return this.EnemyCount < GameDefine.MAX_ENEMY_COUNT;
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0006E9C8 File Offset: 0x0006CBC8
	public void Init()
	{
		this.mFriendCount = 0;
		this.UserMailDic.Clear();
		this.UserMailList.Clear();
		this.MailListSortFlag = false;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0006E9FC File Offset: 0x0006CBFC
	public void SortMailList()
	{
		if (this.MailListSortFlag)
		{
			return;
		}
		this.MailListSortFlag = true;
		this.UserMailList.Sort(delegate(FriendInfo.Mail x, FriendInfo.Mail y)
		{
			if (x.read == y.read)
			{
				return (int)(y.time - x.time);
			}
			if (x.read)
			{
				return 1;
			}
			return -1;
		});
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0006EA3C File Offset: 0x0006CC3C
	public void ClearMailData()
	{
		this.UserMailDic.Clear();
		this.UserMailList.Clear();
		this.MailListSortFlag = false;
		this.RefreshUI();
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0006EA64 File Offset: 0x0006CC64
	public void UpdateMailData(mail_update.request data)
	{
		this.curUpdateType = FriendInfo.MailUpdateType.UPDATE;
		FriendInfo.Mail mail;
		if (this.UserMailDic.ContainsKey(data.mailId))
		{
			mail = this.UserMailDic[data.mailId];
			this.curUpdateType = FriendInfo.MailUpdateType.UPDATE;
		}
		else
		{
			mail = new FriendInfo.Mail();
			this.curUpdateType = FriendInfo.MailUpdateType.ADD;
		}
		mail.key = data.mailId;
		mail.senderType = (FriendInfo.MailSenderType)data.sendertype;
		mail.text = data.context;
		mail.time = data.sortTime;
		mail.title = ((!data.HasTitle) ? "no title!!!" : data.title);
		mail.read = ((int)data.readTime > 0);
		mail.expireday = ((!data.HasExpireday) ? -1L : data.expireday);
		if (data.HasItems)
		{
			mail.items = data.items;
		}
		else
		{
			mail.items = null;
		}
		mail.mailstate = (int)data.mailState;
		this.UserMailDic[mail.key] = mail;
		if (this.curUpdateType == FriendInfo.MailUpdateType.UPDATE)
		{
			this.MailListSortFlag = false;
			for (int i = 0; i < this.UserMailList.Count; i++)
			{
				if (mail.key == this.UserMailList[i].key)
				{
					this.UserMailList[i] = mail;
					break;
				}
			}
		}
		else if (this.curUpdateType == FriendInfo.MailUpdateType.ADD)
		{
			this.UserMailList.Insert(0, mail);
		}
		this.RefreshUI();
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0006EC00 File Offset: 0x0006CE00
	public void DelMail(long key)
	{
		if (this.UserMailDic.ContainsKey(key))
		{
			this.UserMailDic.Remove(key);
			for (int i = 0; i < this.UserMailList.Count; i++)
			{
				if (key == this.UserMailList[i].key)
				{
					this.UserMailList.RemoveAt(i);
					break;
				}
			}
		}
		this.RefreshUI();
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0006EC78 File Offset: 0x0006CE78
	public void ReadMail(long key)
	{
		if (this.UserMailDic.ContainsKey(key))
		{
			this.UserMailDic[key].read = true;
			if (this.UserMailDic[key].mailstate == 0)
			{
				this.UserMailDic[key].mailstate = 1;
			}
			this.MailListSortFlag = false;
			for (int i = 0; i < this.UserMailList.Count; i++)
			{
				if (key == this.UserMailList[i].key)
				{
					this.UserMailList[i] = this.UserMailDic[key];
					break;
				}
			}
			this.RefreshUI();
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0006ED30 File Offset: 0x0006CF30
	public void GetMailItem(long key)
	{
		if (this.UserMailDic.ContainsKey(key))
		{
			this.UserMailDic[key].getItem = true;
			this.UserMailDic[key].mailstate = 3;
			for (int i = 0; i < this.UserMailList.Count; i++)
			{
				if (key == this.UserMailList[i].key)
				{
					this.UserMailList[i] = this.UserMailDic[key];
					break;
				}
			}
			this.MailListSortFlag = false;
			this.RefreshUI();
		}
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0006EDD0 File Offset: 0x0006CFD0
	public bool IsHaveMail()
	{
		return this.UserMailList.Count != 0;
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x0006EDE4 File Offset: 0x0006CFE4
	public bool IsHaveMailTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL_MAIL))
		{
			return false;
		}
		foreach (KeyValuePair<long, FriendInfo.Mail> keyValuePair in this.UserMailDic)
		{
			if (!keyValuePair.Value.read)
			{
				return true;
			}
			if (keyValuePair.Value.IsHaveItem() && !keyValuePair.Value.IsGetItem())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400148C RID: 5260
	private HaoYouRoot_TYPE mOpenType;

	// Token: 0x0400148D RID: 5261
	private int mFriendCount;

	// Token: 0x0400148E RID: 5262
	private int mEnemyCount;

	// Token: 0x0400148F RID: 5263
	private int mFriendOnlineCount;

	// Token: 0x04001490 RID: 5264
	private int tipsflag;

	// Token: 0x04001491 RID: 5265
	private Dictionary<long, friend_info> mMainPlayerFriendDic = new Dictionary<long, friend_info>();

	// Token: 0x04001492 RID: 5266
	private Dictionary<long, friend_info> mMainPlayerEnemyDic = new Dictionary<long, friend_info>();

	// Token: 0x04001493 RID: 5267
	public Dictionary<long, friend_info> ApplyFriendDic = new Dictionary<long, friend_info>();

	// Token: 0x04001494 RID: 5268
	private Dictionary<long, friend_info> mMainPlayerRandomFriendDic = new Dictionary<long, friend_info>();

	// Token: 0x04001495 RID: 5269
	private Dictionary<long, friend_info> mMainPlayerSearchFriendDic = new Dictionary<long, friend_info>();

	// Token: 0x04001496 RID: 5270
	public Dictionary<long, FriendInfo.Mail> UserMailDic = new Dictionary<long, FriendInfo.Mail>();

	// Token: 0x04001497 RID: 5271
	public List<FriendInfo.Mail> UserMailList = new List<FriendInfo.Mail>();

	// Token: 0x04001498 RID: 5272
	public bool MailListSortFlag;

	// Token: 0x04001499 RID: 5273
	public FriendInfo.MailUpdateType curUpdateType = FriendInfo.MailUpdateType.ADD;

	// Token: 0x020001DC RID: 476
	public enum FriendType
	{
		// Token: 0x0400149E RID: 5278
		NORMAL,
		// Token: 0x0400149F RID: 5279
		APPLY,
		// Token: 0x040014A0 RID: 5280
		APPLIED,
		// Token: 0x040014A1 RID: 5281
		ACCEPT,
		// Token: 0x040014A2 RID: 5282
		REFUSED,
		// Token: 0x040014A3 RID: 5283
		BEDELETED,
		// Token: 0x040014A4 RID: 5284
		ENEMY
	}

	// Token: 0x020001DD RID: 477
	public enum MailState
	{
		// Token: 0x040014A6 RID: 5286
		UNREAD,
		// Token: 0x040014A7 RID: 5287
		READ,
		// Token: 0x040014A8 RID: 5288
		UNGETITEM,
		// Token: 0x040014A9 RID: 5289
		GETITEM
	}

	// Token: 0x020001DE RID: 478
	public enum MailUpdateType
	{
		// Token: 0x040014AB RID: 5291
		UPDATE,
		// Token: 0x040014AC RID: 5292
		ADD,
		// Token: 0x040014AD RID: 5293
		DEL
	}

	// Token: 0x020001DF RID: 479
	public enum MailSenderType
	{
		// Token: 0x040014AF RID: 5295
		SYS,
		// Token: 0x040014B0 RID: 5296
		CONSIGN_CANCEL,
		// Token: 0x040014B1 RID: 5297
		CONSIGN_TIME,
		// Token: 0x040014B2 RID: 5298
		CONSIGN_BUY,
		// Token: 0x040014B3 RID: 5299
		CONSIGN_FINISH,
		// Token: 0x040014B4 RID: 5300
		USER
	}

	// Token: 0x020001E0 RID: 480
	public class Mail
	{
		// Token: 0x06001147 RID: 4423 RVA: 0x0006EF68 File Offset: 0x0006D168
		public bool IsGetItem()
		{
			return this.mailstate == 3;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0006EF7C File Offset: 0x0006D17C
		public bool IsHaveItem()
		{
			return this.items != null && this.items.Count != 0;
		}

		// Token: 0x040014B5 RID: 5301
		public FriendInfo.MailSenderType senderType;

		// Token: 0x040014B6 RID: 5302
		public long key;

		// Token: 0x040014B7 RID: 5303
		public string title;

		// Token: 0x040014B8 RID: 5304
		public long time;

		// Token: 0x040014B9 RID: 5305
		public string text;

		// Token: 0x040014BA RID: 5306
		public Dictionary<string, item> items;

		// Token: 0x040014BB RID: 5307
		public bool read;

		// Token: 0x040014BC RID: 5308
		public bool getItem;

		// Token: 0x040014BD RID: 5309
		public int mailstate;

		// Token: 0x040014BE RID: 5310
		public long expireday;
	}
}
