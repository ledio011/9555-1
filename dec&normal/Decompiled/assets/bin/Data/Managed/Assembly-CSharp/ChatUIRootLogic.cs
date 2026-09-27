using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class ChatUIRootLogic : SingletonUnity<ChatUIRootLogic>
{
	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00052B90 File Offset: 0x00050D90
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

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00052BB4 File Offset: 0x00050DB4
	// (set) Token: 0x06000B2C RID: 2860 RVA: 0x00052BBC File Offset: 0x00050DBC
	public GameDefine.CHAT_CHANNEL_TYPE CurChannelType
	{
		get
		{
			return this.mCurChannelType;
		}
		set
		{
			this.mCurChannelType = value;
		}
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00052BC8 File Offset: 0x00050DC8
	public static void ResetPrivateChat(long targetId, string targetName, PROFESSION_TYPE targetProfession)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		playerData.RecentSpeakers.Add(targetId, targetName, targetProfession);
		playerData.RecentSpeakers.SetLastSpeaker(targetId);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChatRoot, new UIManager.OnOpenUIDelegate(ChatUIRootLogic.OnChatUIShow), GameDefine.CHAT_CHANNEL_TYPE.PRIVATE);
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00052C1C File Offset: 0x00050E1C
	public static void OnChatUIShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			SingletonUnity<ChatUIRootLogic>.Instance.Reset((GameDefine.CHAT_CHANNEL_TYPE)((int)param));
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x00052C34 File Offset: 0x00050E34
	public void UpdateSendInfo()
	{
		string dictionaryString = StrDictionary.GetDictionaryString("#{100255}", new object[0]);
		if (this.mCurChannelType == GameDefine.CHAT_CHANNEL_TYPE.WORLD)
		{
			this.SpeakerItem = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack.GetItemByItemId2("5026");
			int num = 0;
			if (this.SpeakerItem != null && !this.SpeakerItem.IsEmpty())
			{
				num = this.SpeakerItem.StackNum;
			}
			if (num < 0)
			{
				num = 0;
			}
			this.SendLable.text = string.Format("{0}({1})", dictionaryString, num);
			this.SendBtn.isEnabled = (num > 0);
			if (num > 0)
			{
				this.ChatInput.defaultText = StrDictionary.GetDictionaryString("#{100254}", new object[0]);
			}
			else
			{
				this.ChatInput.defaultText = StrDictionary.GetDictionaryString("#{100298}", new object[0]);
			}
		}
		else
		{
			this.SendLable.text = dictionaryString;
			this.SendBtn.isEnabled = true;
			this.ChatInput.defaultText = StrDictionary.GetDictionaryString("#{100254}", new object[0]);
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00052D54 File Offset: 0x00050F54
	private void OnEnable()
	{
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateSendInfo));
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00052D84 File Offset: 0x00050F84
	private void OnDisable()
	{
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateSendInfo));
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00052DB4 File Offset: 0x00050FB4
	public void Reset(GameDefine.CHAT_CHANNEL_TYPE channelType)
	{
		this.SetCurChannel(channelType);
		this.UpdateTranslationState();
		this.ObjTweenPos.ResetToBeginning();
		this.ObjTweenPos.PlayForward();
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00052DDC File Offset: 0x00050FDC
	public static void ResetLinkChat(GameDefine.CHAT_LINK_TYPE linkType, GameDefine.CHAT_CHANNEL_TYPE channelType, object data)
	{
		object[] param = new object[]
		{
			linkType,
			channelType,
			data
		};
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChatRoot, new UIManager.OnOpenUIDelegate(ChatUIRootLogic.OnLinkChatUIShow), param);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00052E24 File Offset: 0x00051024
	public static void OnLinkChatUIShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			object[] array = param as object[];
			GameDefine.CHAT_LINK_TYPE chat_LINK_TYPE = (GameDefine.CHAT_LINK_TYPE)((int)array[0]);
			SingletonUnity<ChatUIRootLogic>.Instance.Reset((GameDefine.CHAT_CHANNEL_TYPE)((int)array[1]));
			GameDefine.CHAT_LINK_TYPE chat_LINK_TYPE2 = chat_LINK_TYPE;
			if (chat_LINK_TYPE2 != GameDefine.CHAT_LINK_TYPE.TEAM)
			{
				if (chat_LINK_TYPE2 == GameDefine.CHAT_LINK_TYPE.GUILD)
				{
					Guild guildInfo = array[2] as Guild;
					SingletonUnity<ChatUIRootLogic>.Instance.InsertGuildLink(guildInfo);
				}
			}
			else
			{
				Team teamInfo = array[2] as Team;
				SingletonUnity<ChatUIRootLogic>.Instance.InsertTeamLink(teamInfo);
			}
		}
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00052EA4 File Offset: 0x000510A4
	private void UpdateSelectChange(GameDefine.CHAT_CHANNEL_TYPE channelType)
	{
		for (int i = 0; i < this.ChannelIconSprite.Count; i++)
		{
			if (i != (int)channelType)
			{
				this.ChannelIconSprite[i].spriteName = "CZ_huaDongBG";
			}
			else
			{
				this.ChannelIconSprite[i].spriteName = "CZ_huaDongBG_1";
			}
		}
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00052F08 File Offset: 0x00051108
	private void SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE channelType)
	{
		this.mCurChannelType = channelType;
		this.mPlayerData.ChoosedChannelType = this.mCurChannelType;
		this.UpdateSelectChange(this.mCurChannelType);
		if (this.mCurChannelType == GameDefine.CHAT_CHANNEL_TYPE.SYSTEM)
		{
			UnityVersionUtil.SetActiveRecursive(this.ChatInput.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ChatInput.gameObject, true);
		}
		if (this.mCurChannelType == GameDefine.CHAT_CHANNEL_TYPE.PRIVATE)
		{
			NGUITools.SetActive(this.RecenSpeakerRoot.gameObject, true);
			this.RecenSpeakerRoot.Reset(this.mPlayerData.RecentSpeakers.RecentSpeakerList);
			this.ChatMessageRootLogic.ChatMessageRootPanel.baseClipRegion = new Vector4(0f, 0f, 300f, 370f);
			this.ChatMessageRootLogic.ChatMessageRootPanel.clipOffset = new Vector2(150f, 205f);
			this.ChatMessageRootLogic.ChatMessageRoot.transform.localPosition = Vector3.up * 20f;
			this.ChatMessageRootLogic.ScrollView.ResetPosition();
			this.OnClickRecentSpeaker(this.mPlayerData.RecentSpeakers.GetLastSpeaker());
		}
		else
		{
			NGUITools.SetActive(this.RecenSpeakerRoot.gameObject, false);
			this.ChatMessageRootLogic.ResetChatMessage(this.mPlayerData.CurChannelChatHistoryList, this.mCurChannelType);
			this.ChatMessageRootLogic.ChatMessageRootPanel.baseClipRegion = new Vector4(0f, 0f, 300f, 410f);
			this.ChatMessageRootLogic.ChatMessageRootPanel.clipOffset = new Vector2(150f, 205f);
			this.ChatMessageRootLogic.ChatMessageRoot.transform.localPosition = Vector3.zero;
			this.ChatMessageRootLogic.ScrollView.ResetPosition();
		}
		this.CheckNewPrivateMessage();
		this.UpdateSendInfo();
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x000530E8 File Offset: 0x000512E8
	public void OnClickChannelSystemBtn()
	{
		if (this.mCurChannelType != GameDefine.CHAT_CHANNEL_TYPE.SYSTEM)
		{
			this.SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE.SYSTEM);
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x000530FC File Offset: 0x000512FC
	public void OnClickChannelNormalBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene())
		{
			NoticeLogic.AddNotifyData("#{200089}", true, false);
		}
		else if (this.mCurChannelType != GameDefine.CHAT_CHANNEL_TYPE.NORMAL)
		{
			this.SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE.NORMAL);
		}
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00053144 File Offset: 0x00051344
	public void OnClickChannelWorldBtn()
	{
		if (this.mCurChannelType != GameDefine.CHAT_CHANNEL_TYPE.WORLD)
		{
			this.SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE.WORLD);
		}
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0005315C File Offset: 0x0005135C
	public void OnClickChannelTeamBtn()
	{
		if (!this.TeamSendCheck())
		{
			NoticeLogic.AddNotifyData("#{100280}", true, false);
			return;
		}
		if (this.mCurChannelType != GameDefine.CHAT_CHANNEL_TYPE.TEAM)
		{
			this.SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE.TEAM);
		}
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0005318C File Offset: 0x0005138C
	public void OnClickChannelGuildBtn()
	{
		if (!this.GuildSendCheck())
		{
			NoticeLogic.AddNotifyData("#{100281}", true, false);
			return;
		}
		if (this.mCurChannelType != GameDefine.CHAT_CHANNEL_TYPE.GUILD)
		{
			this.SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE.GUILD);
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x000531BC File Offset: 0x000513BC
	public void OnClickChannelPrivateBtn()
	{
		if (this.mCurChannelType != GameDefine.CHAT_CHANNEL_TYPE.PRIVATE)
		{
			this.SetCurChannel(GameDefine.CHAT_CHANNEL_TYPE.PRIVATE);
		}
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x000531D4 File Offset: 0x000513D4
	public void OnClickInputSendBtn()
	{
		if (!this.SendBtn.isEnabled)
		{
			return;
		}
		if (GameManager.IsSupportCurDataVersion177() && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHAT))
		{
			int condition = DataManager.GetFunctionDataById(4084.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
			{
				condition
			}), true, false);
			return;
		}
		string text = this.ChatInput.value;
		if (string.IsNullOrEmpty(text))
		{
			this.ClearLinkInfo();
			return;
		}
		if (this.mCurLinkType == GameDefine.CHAT_LINK_TYPE.INVALID)
		{
			text = text.Replace("\r", " ");
			text = text.Replace("\n", " ");
		}
		if (text.Length > this.MAX_SEND_MESSAHE_COUNT)
		{
			NoticeLogic.AddNotifyData("#{100279}", true, false);
			return;
		}
		if (!this.CanSendMessage())
		{
			this.ChatInput.value = string.Empty;
			this.ClearLinkInfo();
			return;
		}
		if (Time.time - this.mLastSendTime <= this.SEND_MESSAGE_TIME_INTERVAL)
		{
			NoticeLogic.AddNotifyData("#{100273}", true, false);
			return;
		}
		this.mLastSendTime = Time.time;
		if (this.mCurLinkType != GameDefine.CHAT_LINK_TYPE.INVALID && !text.Contains(this.mLinkText))
		{
			this.ChatInput.value = string.Empty;
			this.ClearLinkInfo();
			return;
		}
		chat.request request = new chat.request();
		request.chattype = (long)this.mCurChannelType;
		request.linktype = (long)this.mCurLinkType;
		if (this.mCurLinkType != GameDefine.CHAT_LINK_TYPE.INVALID)
		{
			if (this.mLinkLongData.Count != 0)
			{
				request.intdata = this.mLinkLongData;
			}
			if (this.mLinkStrData.Count != 0)
			{
				request.stringdata = this.mLinkStrData;
			}
			text = text.Replace(this.mLinkText, string.Format("[FF0000]{0}[-]", this.mLinkText));
		}
		if (this.mCurChannelType == GameDefine.CHAT_CHANNEL_TYPE.PRIVATE)
		{
			request.tellId = this.mPlayerData.RecentSpeakers.GetLastSpeaker().ServerId;
		}
		request.chatInfo = text;
		NetLogic.GetInstance().Send<Protocol.chat>(request, null);
		this.ChatInput.value = string.Empty;
		this.ClearLinkInfo();
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00053410 File Offset: 0x00051610
	private bool CanSendMessage()
	{
		switch (this.mCurChannelType)
		{
		case GameDefine.CHAT_CHANNEL_TYPE.NORMAL:
			return this.NormalSendCheck();
		case GameDefine.CHAT_CHANNEL_TYPE.WORLD:
			return this.WorldSendCheck();
		case GameDefine.CHAT_CHANNEL_TYPE.TEAM:
			return this.TeamSendCheck();
		case GameDefine.CHAT_CHANNEL_TYPE.GUILD:
			return this.GuildSendCheck();
		case GameDefine.CHAT_CHANNEL_TYPE.PRIVATE:
			return this.PrivateSendCheck();
		default:
			return false;
		}
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x0005346C File Offset: 0x0005166C
	private bool NormalSendCheck()
	{
		return true;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00053470 File Offset: 0x00051670
	private bool WorldSendCheck()
	{
		return this.mPlayerData.Level >= GameSettingData.MinPlayerLevelInWorldSpeak;
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0005348C File Offset: 0x0005168C
	private bool GuildSendCheck()
	{
		return this.mPlayerData.IsHaveGuild();
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0005349C File Offset: 0x0005169C
	private bool PrivateSendCheck()
	{
		return this.mPlayerData.RecentSpeakers.GetLastSpeaker() != null;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x000534B8 File Offset: 0x000516B8
	private bool TeamSendCheck()
	{
		return this.mPlayerData.IsHaveTeam();
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000534C8 File Offset: 0x000516C8
	public void OnClickCloseBtn()
	{
		this.ObjTweenPos.PlayReverse();
		vp_Timer.In(0.5f, delegate()
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChatRoot);
		}, null);
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00053500 File Offset: 0x00051700
	public void OnReceiveMessage(PlayerChatHistoryInfo chatInfo)
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.ChatMessageRootLogic.OnReceiveMessage(chatInfo);
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00053520 File Offset: 0x00051720
	public void OnClickRecentSpeaker(RecentSpeaker speakerInfo)
	{
		if (speakerInfo == null)
		{
			this.ChatMessageRootLogic.ResetChatMessage(null, GameDefine.CHAT_CHANNEL_TYPE.PRIVATE);
			this.RecenSpeakerRoot.OnClickBtn(null);
			return;
		}
		speakerInfo.NewMessageFlag = false;
		this.mPlayerData.RecentSpeakers.SetLastSpeaker(speakerInfo.ServerId);
		this.RecenSpeakerRoot.OnClickBtn(speakerInfo);
		this.ChatMessageRootLogic.ResetChatMessage(this.mPlayerData.ChatHistory.GetHistoryBySenderId(speakerInfo.ServerId), this.mCurChannelType);
		this.CheckNewPrivateMessage();
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x000535A4 File Offset: 0x000517A4
	public void CheckNewPrivateMessage()
	{
		if (this.mPlayerData.RecentSpeakers.HasNewMessage())
		{
			if (!UnityVersionUtil.IsActive(this.NewPrivateMessagePic.gameObject))
			{
				NGUITools.SetActive(this.NewPrivateMessagePic.gameObject, true);
			}
		}
		else if (UnityVersionUtil.IsActive(this.NewPrivateMessagePic.gameObject))
		{
			NGUITools.SetActive(this.NewPrivateMessagePic.gameObject, false);
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00053618 File Offset: 0x00051818
	public void InsertItemLink(GameItem item)
	{
		this.ChatInput.value = string.Empty;
		this.ClearLinkInfo();
		this.mCurLinkType = GameDefine.CHAT_LINK_TYPE.ITEM;
		this.mLinkStrData.Add(item.ItemId);
		this.mLinkText = string.Format("[{0}] ", item.ItemData.MName.Replace(" ", string.Empty));
		this.ChatInput.value = this.mLinkText;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00053690 File Offset: 0x00051890
	public void InsertEquipLink(GameItem item)
	{
		this.ChatInput.value = string.Empty;
		this.ClearLinkInfo();
		this.mCurLinkType = GameDefine.CHAT_LINK_TYPE.EQUIP;
		this.mLinkLongData.Add(item.IndexId);
		this.mLinkText = string.Format("[{0}] ", item.ItemData.MName.Replace(" ", string.Empty));
		this.ChatInput.value = this.mLinkText;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00053708 File Offset: 0x00051908
	public void InsertTeamLink(Team teamInfo)
	{
		this.ChatInput.value = string.Empty;
		this.ClearLinkInfo();
		this.mCurLinkType = GameDefine.CHAT_LINK_TYPE.TEAM;
		this.mLinkLongData.Add(teamInfo.TeamID);
		this.mLinkLongData.Add((long)teamInfo.MinLimitLevel);
		this.mLinkLongData.Add((long)teamInfo.MaxLimitLevel);
		string text = string.Empty;
		if (teamInfo.TeamGoalData.GoalType == 0)
		{
			text = teamInfo.TeamGoalData.MTitleName;
		}
		else
		{
			text = DataManager.GetCopySceneDataById(teamInfo.TeamGoalData.CopyId).MName;
		}
		this.mLinkText = StrDictionary.GetDictionaryString("#{100262}", new object[]
		{
			text
		});
		this.ChatInput.value = this.mLinkText;
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x000537D0 File Offset: 0x000519D0
	public void InsertGuildLink(Guild guildInfo)
	{
		this.ChatInput.value = string.Empty;
		this.ClearLinkInfo();
		this.mCurLinkType = GameDefine.CHAT_LINK_TYPE.GUILD;
		this.mLinkLongData.Add(guildInfo.ServerId);
		this.mLinkText = StrDictionary.GetDictionaryString("#{100261}", new object[]
		{
			guildInfo.GuilName
		});
		this.ChatInput.value = this.mLinkText;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x0005383C File Offset: 0x00051A3C
	private void ClearLinkInfo()
	{
		this.mLinkText = string.Empty;
		this.mCurLinkType = GameDefine.CHAT_LINK_TYPE.INVALID;
		this.mLinkLongData.Clear();
		this.mLinkStrData.Clear();
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00053874 File Offset: 0x00051A74
	private void Update()
	{
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00053878 File Offset: 0x00051A78
	public void OnClickTranslationBtn()
	{
		this.mPlayerData.IsNeedTranslation = !this.mPlayerData.IsNeedTranslation;
		LocalDataSaveManager.SetTranslationFlag(this.mPlayerData.IsNeedTranslation);
		this.UpdateTranslationState();
		this.SetCurChannel(this.mCurChannelType);
		if (SingletonUnity<ChatBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChatBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ChatBaseRootLogic>.Instance.UpdateMessage();
		}
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMessage();
		}
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x000538FC File Offset: 0x00051AFC
	private void UpdateTranslationState()
	{
		if (this.mPlayerData.IsNeedTranslation)
		{
			this.TranslationLabel.text = StrDictionary.GetDictionaryString("#{100291}", new object[0]);
		}
		else
		{
			this.TranslationLabel.text = StrDictionary.GetDictionaryString("#{100292}", new object[0]);
		}
	}

	// Token: 0x04000A25 RID: 2597
	public UIGrid ChannelGrid;

	// Token: 0x04000A26 RID: 2598
	public List<GameObject> ChannelBtn;

	// Token: 0x04000A27 RID: 2599
	public List<UISprite> ChannelIconSprite;

	// Token: 0x04000A28 RID: 2600
	public ChatMessageRootLogic ChatMessageRootLogic;

	// Token: 0x04000A29 RID: 2601
	public TweenPosition ObjTweenPos;

	// Token: 0x04000A2A RID: 2602
	public UIInput ChatInput;

	// Token: 0x04000A2B RID: 2603
	public RecentSpeakerUILogic RecenSpeakerRoot;

	// Token: 0x04000A2C RID: 2604
	public UISprite NewPrivateMessagePic;

	// Token: 0x04000A2D RID: 2605
	public UILabel TranslationLabel;

	// Token: 0x04000A2E RID: 2606
	public GameObject TranslationRoot;

	// Token: 0x04000A2F RID: 2607
	public UILabel SendLable;

	// Token: 0x04000A30 RID: 2608
	public UIButton SendBtn;

	// Token: 0x04000A31 RID: 2609
	private GameItem SpeakerItem;

	// Token: 0x04000A32 RID: 2610
	private PlayerData mPlayerDataCache;

	// Token: 0x04000A33 RID: 2611
	private GameDefine.CHAT_CHANNEL_TYPE mCurChannelType = GameDefine.CHAT_CHANNEL_TYPE.WORLD;

	// Token: 0x04000A34 RID: 2612
	private GameDefine.CHAT_LINK_TYPE mCurLinkType = GameDefine.CHAT_LINK_TYPE.INVALID;

	// Token: 0x04000A35 RID: 2613
	private List<long> mLinkLongData = new List<long>();

	// Token: 0x04000A36 RID: 2614
	private List<string> mLinkStrData = new List<string>();

	// Token: 0x04000A37 RID: 2615
	private string mLinkText;

	// Token: 0x04000A38 RID: 2616
	private float mLastSendTime;

	// Token: 0x04000A39 RID: 2617
	private float SEND_MESSAGE_TIME_INTERVAL = 5f;

	// Token: 0x04000A3A RID: 2618
	private int MAX_SEND_MESSAHE_COUNT = 256;
}
