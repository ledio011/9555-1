using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class ChatMessageLineLogic : MonoBehaviour
{
	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00050F9C File Offset: 0x0004F19C
	public PlayerChatHistoryInfo CurInfo
	{
		get
		{
			return this.mCurInfo;
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00050FA4 File Offset: 0x0004F1A4
	private void Awake()
	{
		UIEventListener messageListener = this.MessageListener;
		messageListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(messageListener.onClick, new UIEventListener.VoidDelegate(this.OnClickMessage));
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00050FD0 File Offset: 0x0004F1D0
	public void Reset(PlayerChatHistoryInfo info)
	{
		this.mCurInfo = info;
		if (this.mCurInfo.ChannelType == GameDefine.CHAT_CHANNEL_TYPE.SYSTEM)
		{
			UnityVersionUtil.SetActiveRecursive(this.PlayerIcon.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.MessageSprite.gameObject, true);
			this.PlayerIcon.spriteName = GameDefine.Player_Icon_Small_Pic[3];
			this.PlayerName.color = Color.white;
			this.PlayerName.text = StrDictionary.GetDictionaryString("#{100248}", new object[0]);
			this.GuildName.text = string.Empty;
			this.MessageLabel.width = 183;
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsNeedTranslation)
			{
				this.MessageLabel.text = NGUIText.StripSymbols(this.mCurInfo.ChatInfo2);
			}
			else
			{
				this.MessageLabel.text = NGUIText.StripSymbols(this.mCurInfo.ChatInfo);
			}
			if (this.mCurInfo.LinkType != GameDefine.CHAT_LINK_TYPE.INVALID)
			{
				this.MessageLabel.color = Color.blue;
			}
			else
			{
				this.MessageLabel.color = Color.black;
			}
			this.MessageSprite.height = this.MessageLabel.height + 15;
			this.MessageCollider.size = this.MessageSprite.localSize;
			this.MessageCollider.center = new Vector3(this.MessageCollider.size.x / 2f, -this.MessageCollider.size.y / 2f, 0f);
			this.PlayerIcon.transform.localPosition = new Vector3(38f, (float)(this.MessageSprite.height - 20), 0f);
			this.PlayerName.transform.localPosition = new Vector3(81f, (float)(this.MessageSprite.height + 5), 0f);
			this.GuildName.transform.localPosition = new Vector3(142f, (float)(this.MessageSprite.height + 5), 0f);
			this.MessageLabel.transform.localPosition = new Vector3(90f, (float)(this.MessageSprite.height - 10), 0f);
			this.MessageSprite.transform.localPosition = new Vector3(68f, (float)this.MessageSprite.height, 0f);
			this.MessageSprite.transform.localScale = Vector3.one;
			this.RootWidget.height = this.MessageSprite.height + 25;
			this.LevelLabel.text = string.Empty;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.PlayerIcon.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.MessageSprite.gameObject, true);
			this.PlayerIcon.spriteName = GameDefine.Player_Icon_Small_Pic[(int)this.mCurInfo.SenderProfession];
			this.PlayerName.color = Color.white;
			this.PlayerName.text = this.mCurInfo.SenderName;
			this.LevelLabel.text = string.Format("Lv.{0}", this.mCurInfo.Level);
			if (string.IsNullOrEmpty(this.mCurInfo.GuildName))
			{
				this.GuildName.text = string.Empty;
			}
			else
			{
				this.GuildName.text = string.Format("[{0}]", this.mCurInfo.GuildName);
			}
			this.MessageLabel.width = 183;
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsNeedTranslation)
			{
				this.MessageLabel.text = NGUIText.StripSymbols(this.mCurInfo.ChatInfo2);
			}
			else
			{
				this.MessageLabel.text = NGUIText.StripSymbols(this.mCurInfo.ChatInfo);
			}
			if (this.mCurInfo.LinkType != GameDefine.CHAT_LINK_TYPE.INVALID)
			{
				this.MessageLabel.color = Color.blue;
			}
			else
			{
				this.MessageLabel.color = Color.black;
			}
			this.MessageSprite.height = this.MessageLabel.height + 15;
			this.MessageCollider.size = this.MessageSprite.localSize;
			this.MessageCollider.center = new Vector3(this.MessageCollider.size.x / 2f, -this.MessageCollider.size.y / 2f, 0f);
			if (this.IsMainPlayer(this.mCurInfo.SenderServerId))
			{
				this.PlayerIcon.transform.localPosition = new Vector3(260f, (float)(this.MessageSprite.height - 20), 0f);
				this.PlayerName.transform.localPosition = new Vector3(25f, (float)(this.MessageSprite.height + 5), 0f);
				this.GuildName.transform.localPosition = new Vector3(142f, (float)(this.MessageSprite.height + 5), 0f);
				this.MessageLabel.transform.localPosition = new Vector3(28f, (float)(this.MessageSprite.height - 10), 0f);
				this.MessageSprite.transform.localPosition = new Vector3(230f, (float)this.MessageSprite.height, 0f);
				this.MessageSprite.transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				this.PlayerIcon.transform.localPosition = new Vector3(38f, (float)(this.MessageSprite.height - 20), 0f);
				this.PlayerName.transform.localPosition = new Vector3(81f, (float)(this.MessageSprite.height + 5), 0f);
				this.GuildName.transform.localPosition = new Vector3(190f, (float)(this.MessageSprite.height + 5), 0f);
				this.MessageLabel.transform.localPosition = new Vector3(90f, (float)(this.MessageSprite.height - 10), 0f);
				this.MessageSprite.transform.localPosition = new Vector3(68f, (float)this.MessageSprite.height, 0f);
				this.MessageSprite.transform.localScale = Vector3.one;
			}
			this.RootWidget.height = this.MessageSprite.height + 25;
		}
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x000516AC File Offset: 0x0004F8AC
	private bool IsMainPlayer(long serverId)
	{
		return serverId == Singleton<ObjManager>.Instance.MainPlayer.ServerId;
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x000516C8 File Offset: 0x0004F8C8
	private void OnDragMessage(Vector2 delta)
	{
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x000516CC File Offset: 0x0004F8CC
	public void OnClickPlayerIcon()
	{
		if (!this.IsMainPlayer(this.mCurInfo.SenderServerId) && this.mCurInfo.ChannelType != GameDefine.CHAT_CHANNEL_TYPE.SYSTEM)
		{
			TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
			selectTargetBasicInfo.ResetInfo(this.mCurInfo.SenderServerId, this.mCurInfo.Level, this.mCurInfo.ComboValue, this.mCurInfo.SenderName, this.mCurInfo.SenderProfession, 1, this.mCurInfo.GuildId, this.mCurInfo.GuildName, UICamera.currentTouch.pos);
			HitOtherPLayerLogic.ShowMenu(HitType.HitChatListItemIcon, selectTargetBasicInfo);
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00051774 File Offset: 0x0004F974
	public void OnClickMessage(GameObject obj)
	{
		GameDefine.CHAT_LINK_TYPE linkType = this.mCurInfo.LinkType;
		switch (linkType + 1)
		{
		case GameDefine.CHAT_LINK_TYPE.ITEM:
			return;
		case GameDefine.CHAT_LINK_TYPE.EQUIP:
			this.OnClickItemLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.TEAM:
			this.OnClickEquipLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.GUILD:
			this.OnClickTeamLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.GUILD_BOSS:
			this.OnClickGuildLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.ESCORT:
			this.OnClickGuildBossLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.DANCE:
			this.OnClickEscortLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.BAR_FIGHT:
			this.OnClickDanceLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.WILD_BOSS:
			this.OnClickBarFightLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.SURVIVE:
			this.OnClickWildBossLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.ATTACK_ESCORT:
			this.OnClickSurviveLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.GUILD_DANCE:
			this.OnClickAttackEscortLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.FIRST_GUILD_DANCE:
			this.OnClickGuildDanceLink();
			return;
		case GameDefine.CHAT_LINK_TYPE.GUILD_DONMINE:
			return;
		case GameDefine.CHAT_LINK_TYPE.GUILD_DONMINE_RES:
			this.OnClickGuildCityLink();
			return;
		case (GameDefine.CHAT_LINK_TYPE)15:
			return;
		default:
			return;
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00051838 File Offset: 0x0004FA38
	private void OnClickGuildCityLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD_ACTIVITY))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_DONMINE, null, false);
		}, null);
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0005189C File Offset: 0x0004FA9C
	private void OnClickGuildDanceLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD_ACTIVITY))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_DANCE, null, false);
		}, null);
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x00051900 File Offset: 0x0004FB00
	private void OnClickItemLink()
	{
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x00051904 File Offset: 0x0004FB04
	private void OnClickEquipLink()
	{
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x00051908 File Offset: 0x0004FB08
	private void OnClickTeamLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TEAM))
		{
			NoticeLogic.AddNotifyData("#{100276}", true, false);
			return;
		}
		int minlevel = (int)this.mCurInfo.LongData[1];
		int maxlevel = (int)this.mCurInfo.LongData[2];
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minlevel, maxlevel))
		{
			NoticeLogic.AddNotifyData("#{101539}", true, false);
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
		{
			NoticeLogic.AddNotifyData("#{100277}", true, false);
			return;
		}
		long num = this.mCurInfo.LongData[0];
		NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100788}", new object[0]), true, false);
		req_join_team.request request = new req_join_team.request();
		request.teamid = num;
		request.isapply = false;
		NetLogic.GetInstance().Send<Protocol.req_join_team>(request, null);
		team team = new team();
		team.id = num;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.AddApplyTeam(team);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00051A20 File Offset: 0x0004FC20
	private void OnClickGuildLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD))
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100785}", new object[0]), true, false);
			return;
		}
		NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100788}", new object[0]), true, false);
		Singleton<ObjManager>.Instance.MainPlayer.JoinGuild(this.mCurInfo.LongData[0]);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00051AB0 File Offset: 0x0004FCB0
	private void OnClickGuildBossLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD_ACTIVITY))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, null, false);
		}, null);
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00051B14 File Offset: 0x0004FD14
	private void OnClickEscortLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ESCORT, null, false);
		}, null);
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00051B78 File Offset: 0x0004FD78
	private void OnClickAttackEscortLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT, null, false);
		}, null);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x00051BDC File Offset: 0x0004FDDC
	private void OnClickDanceLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.CITY_DANCE, null, false);
		}, null);
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00051C40 File Offset: 0x0004FE40
	private void OnClickSurviveLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
		}, null);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00051CA4 File Offset: 0x0004FEA4
	private void OnClickBarFightLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.BAR_FIGHT, null, false);
		}, null);
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00051D08 File Offset: 0x0004FF08
	private void OnClickWildBossLink()
	{
		if (!this.IsCanGoto())
		{
			return;
		}
		if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_WORLDBOSS))
		{
			return;
		}
		SingletonUnity<ChatUIRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
		{
			SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.WILD_BOSS, null, false);
		}, null);
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00051D6C File Offset: 0x0004FF6C
	public bool IsCanGoto()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager != null && (sceneManager.IsBigWorld() || sceneManager.IsTutorialScene()))
		{
			return true;
		}
		NoticeLogic.AddNotifyData("#{102042}", true, false);
		return false;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00051DB0 File Offset: 0x0004FFB0
	public bool CheckUnlockFunction(FUNCTION_TYPE type)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.IsFunctionUnlock(type))
		{
			return true;
		}
		NoticeLogic.AddNotifyData("#{101539}", true, false);
		return false;
	}

	// Token: 0x040009F3 RID: 2547
	public UIWidget RootWidget;

	// Token: 0x040009F4 RID: 2548
	public UISprite PlayerIcon;

	// Token: 0x040009F5 RID: 2549
	public UILabel PlayerName;

	// Token: 0x040009F6 RID: 2550
	public UILabel GuildName;

	// Token: 0x040009F7 RID: 2551
	public UILabel MessageLabel;

	// Token: 0x040009F8 RID: 2552
	public UISprite MessageSprite;

	// Token: 0x040009F9 RID: 2553
	public BoxCollider MessageCollider;

	// Token: 0x040009FA RID: 2554
	public UIEventListener MessageListener;

	// Token: 0x040009FB RID: 2555
	public UILabel LevelLabel;

	// Token: 0x040009FC RID: 2556
	private PlayerChatHistoryInfo mCurInfo;
}
