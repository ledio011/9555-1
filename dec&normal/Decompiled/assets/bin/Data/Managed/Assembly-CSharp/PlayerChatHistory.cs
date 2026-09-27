using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SprotoType;

// Token: 0x02000131 RID: 305
public class PlayerChatHistory
{
	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00053A38 File Offset: 0x00051C38
	public List<PlayerChatHistoryInfo> ChatHistoryList
	{
		get
		{
			return this.mChatHistoryList;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00053A40 File Offset: 0x00051C40
	public List<PlayerChatHistoryInfo>[] ChannelChatHistoryList
	{
		get
		{
			return this.mChannelChatHistoryList;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00053A48 File Offset: 0x00051C48
	// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00053A50 File Offset: 0x00051C50
	public bool NewPrivateChatFlag
	{
		get
		{
			return this.mNewPrivateChatFlag;
		}
		set
		{
			this.mNewPrivateChatFlag = value;
		}
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00053A5C File Offset: 0x00051C5C
	public void OnReceiveChatMessage(chat_item chatInfo)
	{
		if (chatInfo != null)
		{
			if (chatInfo.chattype >= (long)this.mChannelChatHistoryList.Length || chatInfo.chattype < 0L)
			{
				return;
			}
			List<PlayerChatHistoryInfo> list;
			PlayerChatHistoryInfo playerChatHistoryInfo2;
			checked
			{
				list = this.mChannelChatHistoryList[(int)((IntPtr)chatInfo.chattype)];
				if (list.Count >= this.ChannelHistoryMaxCount[(int)((IntPtr)chatInfo.chattype)])
				{
					PlayerChatHistoryInfo playerChatHistoryInfo = list[0];
					list.RemoveAt(0);
					this.mChatHistoryList.Remove(playerChatHistoryInfo);
				}
				playerChatHistoryInfo2 = new PlayerChatHistoryInfo();
				if (chatInfo.HasSenderId)
				{
					playerChatHistoryInfo2.SenderServerId = chatInfo.senderId;
				}
				if (chatInfo.HasSenderName)
				{
					playerChatHistoryInfo2.SenderName = chatInfo.senderName;
				}
				if (chatInfo.HasTellId)
				{
					playerChatHistoryInfo2.TellId = chatInfo.tellId;
				}
				if (chatInfo.HasTellName)
				{
					playerChatHistoryInfo2.TellName = chatInfo.tellName;
				}
				if (chatInfo.HasChatInfo)
				{
					playerChatHistoryInfo2.ChatInfo = chatInfo.chatInfo;
				}
				if (chatInfo.HasChatInfo2)
				{
					playerChatHistoryInfo2.ChatInfo2 = this.GetFormatStr(chatInfo.chatInfo2);
				}
				else
				{
					playerChatHistoryInfo2.ChatInfo2 = chatInfo.chatInfo;
				}
			}
			if (chatInfo.HasChattype)
			{
				playerChatHistoryInfo2.ChannelType = (GameDefine.CHAT_CHANNEL_TYPE)chatInfo.chattype;
			}
			if (chatInfo.HasLinktype)
			{
				playerChatHistoryInfo2.LinkType = (GameDefine.CHAT_LINK_TYPE)chatInfo.linktype;
			}
			if (chatInfo.HasIntdata)
			{
				for (int i = 0; i < chatInfo.intdata.Count; i++)
				{
					playerChatHistoryInfo2.LongData.Add(chatInfo.intdata[i]);
				}
			}
			if (chatInfo.HasStringdata)
			{
				for (int j = 0; j < chatInfo.stringdata.Count; j++)
				{
					playerChatHistoryInfo2.StringData.Add(chatInfo.stringdata[j]);
				}
			}
			if (chatInfo.HasSenderProfession)
			{
				playerChatHistoryInfo2.SenderProfession = (PROFESSION_TYPE)chatInfo.senderProfession;
			}
			if (chatInfo.HasLevel)
			{
				playerChatHistoryInfo2.Level = (int)chatInfo.level;
			}
			if (chatInfo.HasCombValue)
			{
				playerChatHistoryInfo2.ComboValue = (int)chatInfo.combValue;
			}
			if (chatInfo.HasGuildId)
			{
				playerChatHistoryInfo2.GuildId = chatInfo.guildId;
				playerChatHistoryInfo2.GuildName = chatInfo.guildName;
			}
			else
			{
				playerChatHistoryInfo2.GuildId = 0L;
				playerChatHistoryInfo2.GuildName = string.Empty;
			}
			list.Add(playerChatHistoryInfo2);
			this.ChatHistoryList.Add(playerChatHistoryInfo2);
			if (SingletonDontDestoryUnity<GameManager>.Exists)
			{
				if (playerChatHistoryInfo2.ChannelType == GameDefine.CHAT_CHANNEL_TYPE.PRIVATE)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.RecentSpeakers.OnReceiveMessage(playerChatHistoryInfo2);
					if (SingletonUnity<ChatUIRootLogic>.Exists)
					{
						SingletonUnity<ChatUIRootLogic>.Instance.CheckNewPrivateMessage();
					}
				}
				if (playerChatHistoryInfo2.ChannelType == SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChoosedChannelType && SingletonUnity<ChatUIRootLogic>.Exists)
				{
					SingletonUnity<ChatUIRootLogic>.Instance.OnReceiveMessage(playerChatHistoryInfo2);
				}
				if (SingletonUnity<ChatBaseRootLogic>.Exists)
				{
					SingletonUnity<ChatBaseRootLogic>.Instance.OnReceiveMessage();
				}
				if (SingletonUnity<FunctionBtnRootLogic>.Exists)
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.OnReceiveMessage();
				}
			}
		}
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00053D5C File Offset: 0x00051F5C
	public List<PlayerChatHistoryInfo> GetHistoryBySenderId(long targetId)
	{
		List<PlayerChatHistoryInfo> list = new List<PlayerChatHistoryInfo>();
		list.Clear();
		List<PlayerChatHistoryInfo> list2 = this.mChannelChatHistoryList[5];
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i].SenderServerId == targetId || list2[i].TellId == targetId)
			{
				list.Add(list2[i]);
			}
		}
		return list;
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00053DC8 File Offset: 0x00051FC8
	public void Reset()
	{
		this.mChatHistoryList.Clear();
		for (int i = 0; i < this.mChannelChatHistoryList.Length; i++)
		{
			if (this.mChannelChatHistoryList[i] == null)
			{
				this.mChannelChatHistoryList[i] = new List<PlayerChatHistoryInfo>();
			}
			this.mChannelChatHistoryList[i].Clear();
		}
		this.mInitOfflineChatFlag = false;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00053E28 File Offset: 0x00052028
	public void InitOfflineChat()
	{
		if (!this.mInitOfflineChatFlag)
		{
			this.mInitOfflineChatFlag = true;
			req_offline_chat.request rpcReq = new req_offline_chat.request();
			NetLogic.GetInstance().Send<Protocol.req_offline_chat>(rpcReq, null);
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00053E5C File Offset: 0x0005205C
	private string GetFormatStr(string str)
	{
		Regex regex = new Regex("(&#[^;]+;)|([:#] )");
		string result;
		try
		{
			str = regex.Replace(str, delegate(Match match)
			{
				string text = match.Value.ToString().Replace("&#", string.Empty).Replace(";", string.Empty).Replace("# ", "#").Replace(": ", ":");
				int num;
				if (int.TryParse(text, ref num))
				{
					return Convert.ToChar(num).ToString();
				}
				return text;
			});
			result = str;
		}
		catch (Exception ex)
		{
			result = str;
		}
		return result;
	}

	// Token: 0x04000A3D RID: 2621
	private List<PlayerChatHistoryInfo> mChatHistoryList = new List<PlayerChatHistoryInfo>();

	// Token: 0x04000A3E RID: 2622
	private List<PlayerChatHistoryInfo>[] mChannelChatHistoryList = new List<PlayerChatHistoryInfo>[8];

	// Token: 0x04000A3F RID: 2623
	private int[] ChannelHistoryMaxCount = new int[]
	{
		20,
		20,
		20,
		20,
		20,
		20
	};

	// Token: 0x04000A40 RID: 2624
	private bool mInitOfflineChatFlag;

	// Token: 0x04000A41 RID: 2625
	private bool mNewPrivateChatFlag;
}
