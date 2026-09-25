using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A4D RID: 2637
public class TeamBroadCastRootLogic : SingletonUnity<TeamBroadCastRootLogic>
{
	// Token: 0x06004CD2 RID: 19666 RVA: 0x001A1054 File Offset: 0x0019F254
	private void OnEnable()
	{
		this.IsWorldFlag = true;
		this.IsNearbyFlag = true;
		this.IsGangFlag = true;
		this.WorldFlag.enabled = true;
		this.NearbyFlag.enabled = true;
		this.GangFlag.enabled = true;
	}

	// Token: 0x06004CD3 RID: 19667 RVA: 0x001A1090 File Offset: 0x0019F290
	public void OnClickWorldBtn()
	{
		if (this.IsWorldFlag)
		{
			this.WorldFlag.enabled = false;
		}
		else
		{
			this.WorldFlag.enabled = true;
		}
		this.IsWorldFlag = !this.IsWorldFlag;
	}

	// Token: 0x06004CD4 RID: 19668 RVA: 0x001A10CC File Offset: 0x0019F2CC
	public void OnClickNearbyBtn()
	{
		if (this.IsNearbyFlag)
		{
			this.NearbyFlag.enabled = false;
		}
		else
		{
			this.NearbyFlag.enabled = true;
		}
		this.IsNearbyFlag = !this.IsNearbyFlag;
	}

	// Token: 0x06004CD5 RID: 19669 RVA: 0x001A1108 File Offset: 0x0019F308
	public void OnClickGangBtn()
	{
		if (this.IsGangFlag)
		{
			this.GangFlag.enabled = false;
		}
		else
		{
			this.GangFlag.enabled = true;
		}
		this.IsGangFlag = !this.IsGangFlag;
	}

	// Token: 0x06004CD6 RID: 19670 RVA: 0x001A1144 File Offset: 0x0019F344
	public void OnClickBroadCast()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam())
		{
			this.InsertTeamLink(playerData.TeamInfo);
			if (Time.time - TeamBroadCastRootLogic.LastTime > 5f)
			{
				if (this.IsWorldFlag)
				{
					this.SendChatInfo(GameDefine.CHAT_CHANNEL_TYPE.WORLD);
				}
				if (this.IsNearbyFlag)
				{
					this.SendChatInfo(GameDefine.CHAT_CHANNEL_TYPE.NORMAL);
				}
				if (this.IsGangFlag && playerData.IsHaveGuild())
				{
					this.SendChatInfo(GameDefine.CHAT_CHANNEL_TYPE.GUILD);
				}
			}
		}
		this.OnClickCloseBtn();
	}

	// Token: 0x06004CD7 RID: 19671 RVA: 0x001A11D0 File Offset: 0x0019F3D0
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamBroadCastRoot);
	}

	// Token: 0x06004CD8 RID: 19672 RVA: 0x001A11E4 File Offset: 0x0019F3E4
	public void InsertTeamLink(Team teamInfo)
	{
		this.ChatInfo = string.Empty;
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
		this.ChatInfo = this.mLinkText;
	}

	// Token: 0x06004CD9 RID: 19673 RVA: 0x001A12A4 File Offset: 0x0019F4A4
	public void SendChatInfo(GameDefine.CHAT_CHANNEL_TYPE mCurChannelType)
	{
		string text = this.ChatInfo;
		if (string.IsNullOrEmpty(text))
		{
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
		if (this.mCurLinkType != GameDefine.CHAT_LINK_TYPE.INVALID && !text.Contains(this.mLinkText))
		{
			return;
		}
		TeamBroadCastRootLogic.LastTime = Time.time;
		chat.request request = new chat.request();
		request.chattype = (long)mCurChannelType;
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
		request.chatInfo = text;
		NetLogic.GetInstance().Send<Protocol.chat>(request, null);
	}

	// Token: 0x06004CDA RID: 19674 RVA: 0x001A13C8 File Offset: 0x0019F5C8
	private void OnDisable()
	{
		this.ClearLinkInfo();
	}

	// Token: 0x06004CDB RID: 19675 RVA: 0x001A13D0 File Offset: 0x0019F5D0
	private void ClearLinkInfo()
	{
		this.mLinkText = string.Empty;
		this.mCurLinkType = GameDefine.CHAT_LINK_TYPE.INVALID;
		this.mLinkLongData.Clear();
		this.mLinkStrData.Clear();
	}

	// Token: 0x04003A6B RID: 14955
	private bool IsWorldFlag;

	// Token: 0x04003A6C RID: 14956
	private bool IsNearbyFlag;

	// Token: 0x04003A6D RID: 14957
	private bool IsGangFlag;

	// Token: 0x04003A6E RID: 14958
	public UISprite WorldFlag;

	// Token: 0x04003A6F RID: 14959
	public UISprite NearbyFlag;

	// Token: 0x04003A70 RID: 14960
	public UISprite GangFlag;

	// Token: 0x04003A71 RID: 14961
	private string mLinkText;

	// Token: 0x04003A72 RID: 14962
	private GameDefine.CHAT_LINK_TYPE mCurLinkType = GameDefine.CHAT_LINK_TYPE.INVALID;

	// Token: 0x04003A73 RID: 14963
	private List<long> mLinkLongData = new List<long>();

	// Token: 0x04003A74 RID: 14964
	private List<string> mLinkStrData = new List<string>();

	// Token: 0x04003A75 RID: 14965
	private string ChatInfo = string.Empty;

	// Token: 0x04003A76 RID: 14966
	private int MAX_SEND_MESSAHE_COUNT = 256;

	// Token: 0x04003A77 RID: 14967
	private static float LastTime;
}
