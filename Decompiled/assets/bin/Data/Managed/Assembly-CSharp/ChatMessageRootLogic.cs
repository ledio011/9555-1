using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class ChatMessageRootLogic : MonoBehaviour
{
	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00051ED8 File Offset: 0x000500D8
	private List<ChatMessageLineLogic> EnableMsgList
	{
		get
		{
			return this.mChatMSGLinePool.EnableMsgList;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00051EE8 File Offset: 0x000500E8
	private PlayerData mPlayerData
	{
		get
		{
			if (this.mCachePlayerData == null)
			{
				this.mCachePlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			}
			return this.mCachePlayerData;
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x00051F0C File Offset: 0x0005010C
	private void Awake()
	{
		UIEventListener chatBottomEventListener = this.ChatBottomEventListener;
		chatBottomEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(chatBottomEventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDrag));
		this.mChatMSGLinePool.Reset(this.MSGLinePrefab);
		ChatMSGLinePool chatMSGLinePool = this.mChatMSGLinePool;
		chatMSGLinePool.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(chatMSGLinePool.onDrag, new UIEventListener.VectorDelegate(this.OnDrag));
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00051F78 File Offset: 0x00050178
	public void AddMessage(PlayerChatHistoryInfo info, bool AddInHead)
	{
		ChatMessageLineLogic chatMessageLineLogic = this.mChatMSGLinePool.AddItem(AddInHead);
		chatMessageLineLogic.Reset(info);
		if (this.NeedResetPosFlag)
		{
			this.ResetMSGLinePos();
		}
		else if (AddInHead)
		{
			if (this.mChatMSGLinePool.EnableMsgList.Count > 1)
			{
				chatMessageLineLogic.gameObject.transform.localPosition = this.mChatMSGLinePool.GetPreNewestMSGPOS() - Vector3.up * (float)chatMessageLineLogic.RootWidget.height;
			}
			else
			{
				chatMessageLineLogic.gameObject.transform.localPosition = Vector3.zero;
			}
		}
		else if (this.mChatMSGLinePool.GetPreLatestMSGLine() != null)
		{
			chatMessageLineLogic.gameObject.transform.localPosition = this.mChatMSGLinePool.GetPreLastMSGPOS() + Vector3.up * (float)this.mChatMSGLinePool.GetPreLatestMSGLine().RootWidget.height;
		}
		else
		{
			Debug.Log("Not Enough MSGLine");
		}
		this.ScrollView.UpdateScrollbars(true);
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00052094 File Offset: 0x00050294
	public void ResetMSGLinePos()
	{
		if (this.EnableMsgList.Count > 0)
		{
			int num = 0;
			for (int i = 0; i < this.EnableMsgList.Count; i++)
			{
				ChatMessageLineLogic chatMessageLineLogic = this.EnableMsgList[this.EnableMsgList.Count - i - 1];
				chatMessageLineLogic.transform.localPosition = Vector3.up * (float)num;
				num += chatMessageLineLogic.RootWidget.height;
			}
			this.ScrollView.ResetPosition();
		}
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00052120 File Offset: 0x00050320
	public void ResetChatMessage(List<PlayerChatHistoryInfo> chatList, GameDefine.CHAT_CHANNEL_TYPE channelType)
	{
		this.mCurChannelType = channelType;
		UnityVersionUtil.SetActiveRecursive(this.MSGLinePrefab.gameObject, false);
		this.mCurHistoryInfoList.Clear();
		this.mChatMSGLinePool.Reset(null);
		UnityVersionUtil.SetActiveRecursive(this.NewMessageTipObj, false);
		if (chatList != null && chatList.Count > 0)
		{
			int num = Mathf.Max(chatList.Count - 10, 0);
			this.NeedResetPosFlag = false;
			for (int i = 0; i < chatList.Count; i++)
			{
				this.mCurHistoryInfoList.Add(chatList[i]);
				if (i >= num)
				{
					this.AddMessage(chatList[i], true);
				}
			}
			this.NeedResetPosFlag = true;
			this.mNewMessageCount = 0;
			this.ResetMSGLinePos();
		}
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x000521E4 File Offset: 0x000503E4
	public void OnReceiveMessage(PlayerChatHistoryInfo chatInfo)
	{
		if (this.mCurChannelType == GameDefine.CHAT_CHANNEL_TYPE.PRIVATE)
		{
			if (this.mPlayerData.RecentSpeakers.GetLastSpeaker() != null)
			{
				if (chatInfo.TellId != this.mPlayerData.RecentSpeakers.GetLastSpeaker().ServerId && chatInfo.SenderServerId != this.mPlayerData.RecentSpeakers.GetLastSpeaker().ServerId)
				{
					return;
				}
			}
			else
			{
				Debug.LogError("mPlayerData.RecentSpeakers.GetLastSpeaker() == null");
			}
		}
		this.mCurHistoryInfoList.Add(chatInfo);
		if (this.NeedResetPosFlag)
		{
			if (UnityVersionUtil.IsActive(this.NewMessageTipObj))
			{
				UnityVersionUtil.SetActiveRecursive(this.NewMessageTipObj, false);
			}
			this.ShowNewMessage();
		}
		else
		{
			this.mNewMessageCount++;
			if (!UnityVersionUtil.IsActive(this.NewMessageTipObj))
			{
				UnityVersionUtil.SetActiveRecursive(this.NewMessageTipObj, true);
			}
		}
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x000522CC File Offset: 0x000504CC
	private void ShowNewMessage()
	{
		this.mChatMSGLinePool.Reset(null);
		int num = Mathf.Max(this.mCurHistoryInfoList.Count - 10, 0);
		this.NeedResetPosFlag = false;
		for (int i = 0; i < this.mCurHistoryInfoList.Count; i++)
		{
			if (i >= num)
			{
				this.AddMessage(this.mCurHistoryInfoList[i], true);
			}
		}
		this.NeedResetPosFlag = true;
		this.mNewMessageCount = 0;
		this.ResetMSGLinePos();
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0005234C File Offset: 0x0005054C
	public void Clear()
	{
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00052350 File Offset: 0x00050550
	private int mLatestIndex
	{
		get
		{
			if (this.mChatMSGLinePool.GetLatestMSGLine() != null)
			{
				return this.mCurHistoryInfoList.IndexOf(this.mChatMSGLinePool.GetLatestMSGLine().CurInfo);
			}
			return -1;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00052390 File Offset: 0x00050590
	private int mNewestIndex
	{
		get
		{
			if (this.mChatMSGLinePool.GetNewestMSGLine() != null)
			{
				return this.mCurHistoryInfoList.IndexOf(this.mChatMSGLinePool.GetNewestMSGLine().CurInfo);
			}
			return -1;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000523D0 File Offset: 0x000505D0
	private int mCurCanAddPreCount
	{
		get
		{
			return Mathf.Min(this.mLatestIndex, 3);
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000523E0 File Offset: 0x000505E0
	private int curCanAddNewestCount
	{
		get
		{
			return Mathf.Min(this.mCurHistoryInfoList.Count - 1 - this.mNewestIndex, 3);
		}
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x000523FC File Offset: 0x000505FC
	public void Preview()
	{
		this.NeedResetPosFlag = false;
		if (this.mLatestIndex > 0)
		{
			this.AddMessage(this.mCurHistoryInfoList[this.mLatestIndex - 1], false);
		}
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00052438 File Offset: 0x00050638
	public void Forward()
	{
		this.NeedResetPosFlag = false;
		if (this.mNewestIndex < this.mCurHistoryInfoList.Count - 1)
		{
			this.AddMessage(this.mCurHistoryInfoList[this.mNewestIndex + 1], true);
		}
		else
		{
			this.NeedResetPosFlag = true;
			this.mNewMessageCount = 0;
			if (UnityVersionUtil.IsActive(this.NewMessageTipObj))
			{
				UnityVersionUtil.SetActiveRecursive(this.NewMessageTipObj, false);
			}
		}
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x000524B0 File Offset: 0x000506B0
	private void OnDrag(GameObject obj, Vector2 delta)
	{
		if (delta.y > 0f)
		{
			if (this.ScrollBar.value > 0.55f)
			{
				this.Forward();
			}
		}
		else if (this.ScrollBar.value < 0.45f)
		{
			this.Preview();
		}
	}

	// Token: 0x04000A06 RID: 2566
	private ChatMSGLinePool mChatMSGLinePool = new ChatMSGLinePool();

	// Token: 0x04000A07 RID: 2567
	private List<PlayerChatHistoryInfo> mCurHistoryInfoList = new List<PlayerChatHistoryInfo>();

	// Token: 0x04000A08 RID: 2568
	public UIPanel ChatMessageRootPanel;

	// Token: 0x04000A09 RID: 2569
	public Transform ChatMessageRoot;

	// Token: 0x04000A0A RID: 2570
	public UIScrollView ScrollView;

	// Token: 0x04000A0B RID: 2571
	public bool NeedResetPosFlag;

	// Token: 0x04000A0C RID: 2572
	public ChatMessageLineLogic MSGLinePrefab;

	// Token: 0x04000A0D RID: 2573
	public UIScrollBar ScrollBar;

	// Token: 0x04000A0E RID: 2574
	public UIEventListener ChatBottomEventListener;

	// Token: 0x04000A0F RID: 2575
	public GameObject NewMessageTipObj;

	// Token: 0x04000A10 RID: 2576
	public GameDefine.CHAT_CHANNEL_TYPE mCurChannelType;

	// Token: 0x04000A11 RID: 2577
	private PlayerData mCachePlayerData;

	// Token: 0x04000A12 RID: 2578
	private int mNewMessageCount;
}
