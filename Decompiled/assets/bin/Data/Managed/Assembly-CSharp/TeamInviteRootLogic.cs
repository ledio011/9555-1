using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009C0 RID: 2496
public class TeamInviteRootLogic : SingletonUnity<TeamInviteRootLogic>
{
	// Token: 0x0600470B RID: 18187 RVA: 0x001695F8 File Offset: 0x001677F8
	private new void Awake()
	{
		base.Awake();
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.TwoColumPlayerInfoPage, new UIManager.OnLoadUIDelegate(this.OnLoadPlayerInfoPage), null);
		this.mCurPage = -1;
		this.OnClickGuildBtn();
	}

	// Token: 0x0600470C RID: 18188 RVA: 0x00169644 File Offset: 0x00167844
	private void OnLoadPlayerInfoPage(GameObject newObj, object param)
	{
		GameObject gameObject = Object.Instantiate(newObj) as GameObject;
		gameObject.transform.parent = this.RootOffSet;
		gameObject.transform.localPosition = new Vector3(0f, 15.81504f, 0f);
		gameObject.transform.localScale = Vector3.one;
		this.mPlayerInfoPage = gameObject.GetComponent<TwoColumnPlayerInfoPage>();
	}

	// Token: 0x0600470D RID: 18189 RVA: 0x001696AC File Offset: 0x001678AC
	public void ResetPage(int pageIndex)
	{
		List<PlayerInfoItemData> list = new List<PlayerInfoItemData>();
		this.mCurPage = pageIndex;
		if (this.mCurPage == 0)
		{
			if (this.mCurGuildMamberList.Count > 0)
			{
				for (int i = 0; i < this.mCurGuildMamberList.Count; i++)
				{
					if (this.mCurGuildMamberList[i].State != 0)
					{
						if (this.mCurGuildMamberList[i].ServerId != PlayerData.MainPlayerServerId)
						{
							list.Add(new PlayerInfoItemData
							{
								Profession = (int)this.mCurGuildMamberList[i].Profession,
								Name = this.mCurGuildMamberList[i].MemberName,
								Level = this.mCurGuildMamberList[i].Level,
								ComboVal = this.mCurGuildMamberList[i].ComboValue,
								Key = this.mCurGuildMamberList[i].ServerId,
								IsEnable = !this.mPlayerData.TeamInfo.InvitedPlyaer.Contains(this.mCurGuildMamberList[i].ServerId),
								GuildId = this.mPlayerData.PlayerGuild.ServerId,
								GuildName = this.mPlayerData.PlayerGuild.GuilName
							});
						}
					}
				}
			}
			this.ChooseObjTrans.localPosition = this.GuildBtnTrans.localPosition - Vector3.up * 5f;
		}
		else if (this.mCurPage == 1)
		{
			if (this.mCurNearByList.Count > 0)
			{
				for (int j = 0; j < this.mCurNearByList.Count; j++)
				{
					if (!this.mPlayerData.TeamInfo.isTeamMemberById(this.mCurNearByList[j].characterId))
					{
						list.Add(new PlayerInfoItemData
						{
							Profession = (int)this.mCurNearByList[j].profession,
							Name = this.mCurNearByList[j].name,
							Level = (int)this.mCurNearByList[j].level,
							ComboVal = (int)this.mCurNearByList[j].combValue,
							Key = this.mCurNearByList[j].characterId,
							IsEnable = !this.mPlayerData.TeamInfo.InvitedPlyaer.Contains(this.mCurNearByList[j].characterId),
							GuildId = this.mCurNearByList[j].guildId,
							GuildName = this.mCurNearByList[j].guildName
						});
					}
				}
			}
			this.ChooseObjTrans.localPosition = this.NearByBtnTrans.localPosition - Vector3.up * 5f;
		}
		else if (this.mCurPage == 2)
		{
			if (this.mCurFriendList.Count > 0)
			{
				for (int k = 0; k < this.mCurFriendList.Count; k++)
				{
					if (this.mCurFriendList[k].state != 0L)
					{
						list.Add(new PlayerInfoItemData
						{
							Profession = (int)this.mCurFriendList[k].profession,
							Name = this.mCurFriendList[k].name,
							Level = (int)this.mCurFriendList[k].level,
							ComboVal = (int)this.mCurFriendList[k].combValue,
							Key = this.mCurFriendList[k].friendId,
							IsEnable = !this.mPlayerData.TeamInfo.InvitedPlyaer.Contains(this.mCurFriendList[k].friendId),
							GuildId = this.mCurFriendList[k].guildId,
							GuildName = this.mCurFriendList[k].guildName
						});
					}
				}
			}
			this.ChooseObjTrans.localPosition = this.FriendBtnTrans.localPosition - Vector3.up * 5f;
		}
		this.mPlayerInfoPage.Reset(list, StrDictionary.GetDictionaryString("#{100815}", new object[0]), StrDictionary.GetDictionaryString("#{100814}", new object[0]), new DelegateDefine.OneLongParamDelegate(this.OnClickInviteBtn));
	}

	// Token: 0x0600470E RID: 18190 RVA: 0x00169B70 File Offset: 0x00167D70
	private void OnClickInviteBtn(long key)
	{
		req_invite_team.request request = new req_invite_team.request();
		request.characterid = key;
		request.goalId = this.mPlayerData.TeamInfo.TeamGoalData.ID;
		request.minLevel = (long)this.mPlayerData.TeamInfo.MinLimitLevel;
		request.maxLevel = (long)this.mPlayerData.TeamInfo.MaxLimitLevel;
		NetLogic.GetInstance().Send<Protocol.req_invite_team>(request, null);
		this.mPlayerData.TeamInfo.InvitedPlyaer.Add(request.characterid);
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x00169BFC File Offset: 0x00167DFC
	public void UpdateNearByPlayerList()
	{
		WaitResponseUIRootLogic.CloseBox();
		this.mCurNearByList = new List<friend_info>(this.mPlayerData.FriendInfo.MainPlayerRandomFriendDic.Values);
		this.ResetPage(1);
	}

	// Token: 0x06004710 RID: 18192 RVA: 0x00169C38 File Offset: 0x00167E38
	public void UpdateGuildMemberInfo()
	{
		this.mCurGuildMamberList = new List<GuildMember>(this.mPlayerData.PlayerGuild.GuildMemberList.Values);
		this.ResetPage(0);
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x00169C6C File Offset: 0x00167E6C
	public void UpdateFriendListInfo()
	{
		WaitResponseUIRootLogic.CloseBox();
		this.mCurFriendList = new List<friend_info>(this.mPlayerData.FriendInfo.MainPlayerFriendDic.Values);
		this.ResetPage(2);
	}

	// Token: 0x06004712 RID: 18194 RVA: 0x00169CA8 File Offset: 0x00167EA8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamInviteRoot);
	}

	// Token: 0x06004713 RID: 18195 RVA: 0x00169CBC File Offset: 0x00167EBC
	public void OnClickGuildBtn()
	{
		if (this.mPlayerData.IsHaveGuild())
		{
			if (this.mPlayerData.PlayerGuild.GuildMemberList != null && this.mPlayerData.PlayerGuild.GuildMemberList.Count > 0)
			{
				this.UpdateGuildMemberInfo();
			}
			Singleton<ObjManager>.Instance.MainPlayer.ApplyUpdataGuildMemberList();
			WaitResponseUIRootLogic.OpenWaitBox(170, 10f, 0f, null);
		}
		else
		{
			this.ResetPage(0);
		}
	}

	// Token: 0x06004714 RID: 18196 RVA: 0x00169D40 File Offset: 0x00167F40
	public void OnClickNearByBtn()
	{
		WaitResponseUIRootLogic.OpenWaitBox(159, 10f, 0f, null);
		req_random_online_character_list.request request = new req_random_online_character_list.request();
		request.characterId = PlayerData.MainPlayerServerId;
		NetLogic.GetInstance().Send<Protocol.req_random_online_character_list>(request, null);
	}

	// Token: 0x06004715 RID: 18197 RVA: 0x00169D80 File Offset: 0x00167F80
	public void OnClickFriendBtn()
	{
		WaitResponseUIRootLogic.OpenWaitBox(126, 10f, 0f, null);
		request_update_friend_useinfo.request request = new request_update_friend_useinfo.request();
		request.characterId = PlayerData.MainPlayerServerId;
		NetLogic.GetInstance().Send<Protocol.request_update_friend_useinfo>(request, null);
	}

	// Token: 0x0400341E RID: 13342
	public Transform ChooseObjTrans;

	// Token: 0x0400341F RID: 13343
	public Transform GuildBtnTrans;

	// Token: 0x04003420 RID: 13344
	public Transform NearByBtnTrans;

	// Token: 0x04003421 RID: 13345
	public Transform FriendBtnTrans;

	// Token: 0x04003422 RID: 13346
	public Transform RootOffSet;

	// Token: 0x04003423 RID: 13347
	private int mCurPage = -1;

	// Token: 0x04003424 RID: 13348
	private List<friend_info> mCurNearByList = new List<friend_info>();

	// Token: 0x04003425 RID: 13349
	private List<GuildMember> mCurGuildMamberList = new List<GuildMember>();

	// Token: 0x04003426 RID: 13350
	private List<friend_info> mCurFriendList = new List<friend_info>();

	// Token: 0x04003427 RID: 13351
	private PlayerData mPlayerData;

	// Token: 0x04003428 RID: 13352
	private TwoColumnPlayerInfoPage mPlayerInfoPage;
}
