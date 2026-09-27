using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000126 RID: 294
public class ChatBaseRootLogic : SingletonUnity<ChatBaseRootLogic>
{
	// Token: 0x06000AC8 RID: 2760 RVA: 0x00050B08 File Offset: 0x0004ED08
	private void Start()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChatHistory.InitOfflineChat();
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00050B44 File Offset: 0x0004ED44
	private List<PlayerChatHistoryInfo> mHistoryList
	{
		get
		{
			if (this.mHistoryListCache == null)
			{
				this.mHistoryListCache = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChatHistory.ChatHistoryList;
			}
			return this.mHistoryListCache;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00050B7C File Offset: 0x0004ED7C
	private PlayerData mPlayerData
	{
		get
		{
			if (this.mPlayerDataCache == null)
			{
				this.mPlayerDataCache = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			}
			return this.mPlayerDataCache;
		}
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x00050BA0 File Offset: 0x0004EDA0
	public void OnReceiveMessage()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.UpdateMessage();
		}
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00050BB8 File Offset: 0x0004EDB8
	public void UpdateMessage()
	{
		if (this.mHistoryList.Count == 0)
		{
			return;
		}
		this.textLabel.UpdateNGUIText();
		this.outPutStr.Length = 0;
		int num = 0;
		string text = null;
		for (int i = this.mHistoryList.Count - 1; i >= 0; i--)
		{
			if (this.IsAcceptChannel(this.mHistoryList[i].ChannelType))
			{
				NGUIText.WrapText(this.FormatStr(this.mHistoryList[i]), out text);
				if (text.get_Chars(text.Length - 1) != '\n')
				{
					text = string.Format("{0}\n", text);
				}
				num += this.GetLineCount(text);
				if (num > this.MaxLineCount)
				{
					int num2 = num - this.MaxLineCount;
					if (num2 > 0)
					{
						text = text.Substring(this.GetIndexOfCount(text, '\n', num2) + 1);
						this.outPutStr.Insert(0, text);
					}
					break;
				}
				this.outPutStr.Insert(0, text);
				if (num == this.MaxLineCount)
				{
					break;
				}
			}
		}
		if (this.outPutStr.Length > 0)
		{
			this.outPutStr.Length = this.outPutStr.Length - 1;
		}
		this.textLabel.text = this.outPutStr.ToString();
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x00050D1C File Offset: 0x0004EF1C
	private int GetIndexOfCount(string str, char val, int count)
	{
		for (int i = 0; i < str.Length; i++)
		{
			if (str.get_Chars(i) == val)
			{
				count--;
				if (count <= 0)
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00050D5C File Offset: 0x0004EF5C
	private int GetLineCount(string str)
	{
		int num = 0;
		for (int i = 0; i < str.Length; i++)
		{
			if (str.get_Chars(i) == '\n')
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00050D98 File Offset: 0x0004EF98
	public string FormatStr(PlayerChatHistoryInfo info)
	{
		this.tempStr.Length = 0;
		if (this.mPlayerData.IsNeedTranslation)
		{
			this.tempStr.AppendFormat("{0} [8FF4F3]{1}[-]: {2}\n", StrDictionary.GetDictionaryString(GameDefine.CHANNEL_PRE_WORD[(int)info.ChannelType], new object[0]), info.SenderName, info.ChatInfo2);
		}
		else
		{
			this.tempStr.AppendFormat("{0} [8FF4F3]{1}[-]: {2}\n", StrDictionary.GetDictionaryString(GameDefine.CHANNEL_PRE_WORD[(int)info.ChannelType], new object[0]), info.SenderName, info.ChatInfo);
		}
		return this.tempStr.ToString();
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00050E3C File Offset: 0x0004F03C
	private bool IsAcceptChannel(GameDefine.CHAT_CHANNEL_TYPE type)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return playerData == null || playerData.IsAcceptChannel(type);
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x00050E64 File Offset: 0x0004F064
	public void SetChannelAccept(GameDefine.CHAT_CHANNEL_TYPE type, bool IsAccept)
	{
		this.AcceptChannelType[(int)type] = IsAccept;
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x00050E70 File Offset: 0x0004F070
	public void OnClickOpenChatBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChatRoot, new UIManager.OnOpenUIDelegate(this.OnChatRootShow), null);
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00050E90 File Offset: 0x0004F090
	private void OnChatRootShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			SingletonUnity<ChatUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChoosedChannelType);
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00050EBC File Offset: 0x0004F0BC
	public void OnClickBackPackBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, null, null);
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00050ED0 File Offset: 0x0004F0D0
	public void OnClickDanceBtn()
	{
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00050ED4 File Offset: 0x0004F0D4
	public void OnClickMailBtn()
	{
		SingletonUnity<SocialUIRootLogic>.Instance.ShowSocialUI();
		SingletonUnity<SocialUIRootLogic>.Instance.OnClickMailBtn();
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x00050EEC File Offset: 0x0004F0EC
	public void OnClickSocialBtn()
	{
		SingletonUnity<SocialUIRootLogic>.Instance.ShowSocialUI();
		SingletonUnity<SocialUIRootLogic>.Instance.SelectFriendInfobtn();
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00050F04 File Offset: 0x0004F104
	public void ShowOrCloseTips(bool show, GameDefine.TIPS_TYPE type)
	{
	}

	// Token: 0x040009E3 RID: 2531
	public int MaxLineCount = 2;

	// Token: 0x040009E4 RID: 2532
	private bool[] AcceptChannelType = new bool[]
	{
		true,
		true,
		true,
		true,
		true,
		true,
		true,
		true
	};

	// Token: 0x040009E5 RID: 2533
	public UILabel textLabel;

	// Token: 0x040009E6 RID: 2534
	private List<PlayerChatHistoryInfo> mHistoryListCache;

	// Token: 0x040009E7 RID: 2535
	private PlayerData mPlayerDataCache;

	// Token: 0x040009E8 RID: 2536
	private StringBuilder outPutStr = new StringBuilder();

	// Token: 0x040009E9 RID: 2537
	private StringBuilder tempStr = new StringBuilder();
}
