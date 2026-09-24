using System;
using System.Collections.Generic;

// Token: 0x02000134 RID: 308
public class RecentSpeakerRecord
{
	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000541B4 File Offset: 0x000523B4
	public List<RecentSpeaker> RecentSpeakerList
	{
		get
		{
			return this.mRecentSpeakerList;
		}
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000541BC File Offset: 0x000523BC
	public void Add(long serverId, string name, PROFESSION_TYPE profession)
	{
		if (this.IsExist(serverId))
		{
			return;
		}
		if (this.mRecentSpeakerList.Count >= GameSettingData.MaxRecentSpeakerNum)
		{
			this.mRecentSpeakerList.RemoveAt(0);
		}
		RecentSpeaker recentSpeaker = new RecentSpeaker();
		recentSpeaker.Reset(serverId, name, profession);
		this.mRecentSpeakerList.Add(recentSpeaker);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00054214 File Offset: 0x00052414
	public bool IsExist(long serverId)
	{
		for (int i = 0; i < this.mRecentSpeakerList.Count; i++)
		{
			if (this.mRecentSpeakerList[i].ServerId == serverId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00054258 File Offset: 0x00052458
	public void Reset()
	{
		this.mRecentSpeakerList.Clear();
		this.mLastSpeaker = null;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0005426C File Offset: 0x0005246C
	public void SetLastSpeaker(long speakerId)
	{
		for (int i = 0; i < this.mRecentSpeakerList.Count; i++)
		{
			if (this.mRecentSpeakerList[i].ServerId == speakerId)
			{
				this.mLastSpeaker = this.mRecentSpeakerList[i];
				return;
			}
		}
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x000542C0 File Offset: 0x000524C0
	public RecentSpeaker GetLastSpeaker()
	{
		if (this.mLastSpeaker == null && this.mRecentSpeakerList.Count > 0)
		{
			this.mLastSpeaker = this.mRecentSpeakerList[this.mRecentSpeakerList.Count - 1];
		}
		return this.mLastSpeaker;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00054310 File Offset: 0x00052510
	public RecentSpeaker GetRecentSpeaker(long serverId)
	{
		for (int i = 0; i < this.mRecentSpeakerList.Count; i++)
		{
			if (this.mRecentSpeakerList[i].ServerId == serverId)
			{
				return this.mRecentSpeakerList[i];
			}
		}
		return null;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00054360 File Offset: 0x00052560
	public void OnReceiveMessage(PlayerChatHistoryInfo curChatInfo)
	{
		if (Singleton<ObjManager>.Exists && curChatInfo.SenderServerId != Singleton<ObjManager>.Instance.MainPlayer.ServerId)
		{
			if (!this.IsExist(curChatInfo.SenderServerId))
			{
				this.Add(curChatInfo.SenderServerId, curChatInfo.SenderName, curChatInfo.SenderProfession);
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChoosedChannelType != GameDefine.CHAT_CHANNEL_TYPE.PRIVATE || curChatInfo.SenderServerId != this.GetLastSpeaker().ServerId)
			{
				RecentSpeaker recentSpeaker = this.GetRecentSpeaker(curChatInfo.SenderServerId);
				recentSpeaker.NewMessageFlag = true;
			}
			if (SingletonUnity<ChatUIRootLogic>.Exists && SingletonUnity<ChatUIRootLogic>.Instance.CurChannelType == GameDefine.CHAT_CHANNEL_TYPE.PRIVATE)
			{
				SingletonUnity<ChatUIRootLogic>.Instance.RecenSpeakerRoot.Reset(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.RecentSpeakers.RecentSpeakerList);
			}
		}
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00054438 File Offset: 0x00052638
	public bool HasNewMessage()
	{
		for (int i = 0; i < this.mRecentSpeakerList.Count; i++)
		{
			if (this.mRecentSpeakerList[i].NewMessageFlag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000A58 RID: 2648
	private List<RecentSpeaker> mRecentSpeakerList = new List<RecentSpeaker>();

	// Token: 0x04000A59 RID: 2649
	private RecentSpeaker mLastSpeaker;
}
