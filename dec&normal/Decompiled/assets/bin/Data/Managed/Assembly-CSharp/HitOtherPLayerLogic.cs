using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020009E6 RID: 2534
public class HitOtherPLayerLogic : SingletonUnity<HitOtherPLayerLogic>
{
	// Token: 0x060047ED RID: 18413 RVA: 0x00170148 File Offset: 0x0016E348
	protected override void Awake()
	{
		base.Awake();
		this.Init();
	}

	// Token: 0x060047EE RID: 18414 RVA: 0x00170158 File Offset: 0x0016E358
	private void Init()
	{
		if (!this.FristInit)
		{
			this.FristInit = true;
			foreach (object obj in this.ErJiGrid.transform)
			{
				Transform transform = (Transform)obj;
				UIEventListener uieventListener = UIEventListener.Get(transform.gameObject);
				uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener.onClick, new UIEventListener.VoidDelegate(this.OnClickErJiJieMianItem));
			}
		}
	}

	// Token: 0x060047EF RID: 18415 RVA: 0x00170204 File Offset: 0x0016E404
	private void Start()
	{
		this.mMenuItemNum = 0;
		this.mMainCamera = Camera.main;
	}

	// Token: 0x060047F0 RID: 18416 RVA: 0x00170218 File Offset: 0x0016E418
	public static void ShowMenu(HitType hittype, TargetBasicInfo target)
	{
		HitOtherPLayerLogic.initParams.Clear();
		HitOtherPLayerLogic.initParams.Add(hittype);
		HitOtherPLayerLogic.initParams.Add(target);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.HitOtherPlayerRoot, new UIManager.OnOpenUIDelegate(HitOtherPLayerLogic.ShowUIOver), HitOtherPLayerLogic.initParams);
	}

	// Token: 0x060047F1 RID: 18417 RVA: 0x0017026C File Offset: 0x0016E46C
	private static void ShowUIOver(bool bSucces, object param)
	{
		if (bSucces)
		{
			List<object> list = param as List<object>;
			if (SingletonUnity<HitOtherPLayerLogic>.Exists && list != null)
			{
				SingletonUnity<HitOtherPLayerLogic>.Instance.ShowPopMenu((HitType)((int)list[0]), (TargetBasicInfo)list[1]);
			}
		}
	}

	// Token: 0x060047F2 RID: 18418 RVA: 0x001702B8 File Offset: 0x0016E4B8
	private void ShowPopMenu(HitType strMenuName, TargetBasicInfo target)
	{
		HitOtherPLayerLogic.mTargetBasicInfo = target;
		this.TargetServerId = target.ServerId;
		this.mMenuItemNum = 0;
		this.currHitType = strMenuName;
		if (this.mMainCamera == null)
		{
			this.mMainCamera = Camera.main;
		}
		this.SetOffsetPos();
		SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.PopMenuItem, new UIManager.OnLoadUIDelegate(this.LoadItemOver), strMenuName);
	}

	// Token: 0x060047F3 RID: 18419 RVA: 0x00170328 File Offset: 0x0016E528
	private void SetOffsetPos()
	{
		if (!this.ScreenFitInitFlag)
		{
			this.ScreenFitInitFlag = true;
			this.mHalfMenuWidth = this.mPopMenuOffset.transform.localScale.x * (float)(this.BottomPic.width / 2 + this.RightPic.width / 2);
			this.mHalfMenuHeight = this.mPopMenuOffset.transform.localScale.y * (float)this.BottomPic.height / 2f;
		}
		Vector2 vector;
		vector..ctor(HitOtherPLayerLogic.mTargetBasicInfo.MousePos.x * UIController.ScreenWidthScale, HitOtherPLayerLogic.mTargetBasicInfo.MousePos.y * UIController.ScreenHeightScale);
		float num = (vector.x >= UIController.ScreenWidth / 2f) ? (-this.mHalfMenuWidth) : this.mHalfMenuWidth;
		float num2 = (vector.y >= UIController.ScreenHeight / 2f) ? (-this.mHalfMenuHeight) : this.mHalfMenuHeight;
		Vector2 vector2;
		vector2..ctor(num, num2);
		vector += vector2;
		vector..ctor(Mathf.Clamp(vector.x, 50f, UIController.ScreenWidth - 50f), Mathf.Clamp(vector.y, 50f, UIController.ScreenHeight - 50f));
		this.mPopMenuOffset.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
	}

	// Token: 0x060047F4 RID: 18420 RVA: 0x001704B4 File Offset: 0x0016E6B4
	private void LoadItemOver(GameObject resobj, object param)
	{
		if (resobj == null)
		{
			return;
		}
		this.mResMenuItem = resobj;
		this.ShowTargetBasicInfo();
		for (int i = 0; i < this.PopMenuItemLogicListDisEnable.Count; i++)
		{
			NGUITools.SetActive(this.PopMenuItemLogicListDisEnable[i].gameObject, false);
		}
		if (this.mMenuItemNum > 0)
		{
			this.mMenuItemGrid.Reposition();
		}
	}

	// Token: 0x060047F5 RID: 18421 RVA: 0x00170528 File Offset: 0x0016E728
	private void ShowTargetBasicInfo()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ErJiJieMian, false);
			this.Icon.spriteName = GameDefine.Player_Icon_Small_Pic[(int)HitOtherPLayerLogic.mTargetBasicInfo.profession];
			this.Name.text = HitOtherPLayerLogic.mTargetBasicInfo.Name;
			this.Level.text = string.Format("Lv.{0}", HitOtherPLayerLogic.mTargetBasicInfo.Level.ToString());
			this.CombolLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), HitOtherPLayerLogic.mTargetBasicInfo.ComboValue.ToString());
			this.GuildNameLabel.text = HitOtherPLayerLogic.mTargetBasicInfo.GuildName;
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			this.AddMenuItem(0, StrDictionary.GetDictionaryString("#{100203}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuView));
			if (HitOtherPLayerLogic.mTargetBasicInfo.OnlineState == 1)
			{
				this.AddMenuItem(2, StrDictionary.GetDictionaryString("#{100218}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuChat));
				if ((playerData.IsHaveTeam() & !playerData.TeamInfo.isTeamMemberById(HitOtherPLayerLogic.mTargetBasicInfo.ServerId)) && !playerData.TeamInfo.IsFull())
				{
					this.AddMenuItem(3, StrDictionary.GetDictionaryString("#{100204}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuTeam));
				}
				else if (!playerData.IsHaveTeam())
				{
					this.AddMenuItem(3, StrDictionary.GetDictionaryString("#{100256}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuJoinTeam));
				}
				if (playerData.IsHaveGuild() && !HitOtherPLayerLogic.mTargetBasicInfo.isHaveGuild())
				{
					this.AddMenuItem(4, StrDictionary.GetDictionaryString("#{100205}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuInviteJoinGuild));
				}
			}
			if (!playerData.IsHaveGuild() && HitOtherPLayerLogic.mTargetBasicInfo.isHaveGuild())
			{
				this.AddMenuItem(4, StrDictionary.GetDictionaryString("#{100257}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuJoinGuild));
			}
			if (playerData.FriendInfo.GetFriendById(HitOtherPLayerLogic.mTargetBasicInfo.ServerId) == null)
			{
				this.AddMenuItem(5, StrDictionary.GetDictionaryString("#{100228}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuAddFriend));
			}
			else
			{
				this.AddMenuItem(5, StrDictionary.GetDictionaryString("#{100207}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuDel));
			}
			if (this.currHitType == HitType.HitTeamMemberIcon && playerData.IsHaveTeam() && playerData.IsTeamLeader())
			{
				this.AddMenuItem(6, StrDictionary.GetDictionaryString("#{100825}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopRemoveTeamMember));
			}
			if (this.currHitType == HitType.HitGuildMember)
			{
				if (playerData.PlayerGuild.CanKickedMember(this.TargetServerId))
				{
					this.AddMenuItem(6, StrDictionary.GetDictionaryString("#{100729}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopRemoveGuildMember));
				}
				if (playerData.PlayerGuild.CanChangeMemberJob(this.TargetServerId))
				{
					this.AddMenuItem(7, StrDictionary.GetDictionaryString("#{100728}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopChangeDuate));
				}
			}
			if (playerData.FriendInfo.GetEnemyById(HitOtherPLayerLogic.mTargetBasicInfo.ServerId) == null)
			{
				this.AddMenuItem(8, StrDictionary.GetDictionaryString("#{103305}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuAddEnemy));
			}
			else
			{
				this.AddMenuItem(8, StrDictionary.GetDictionaryString("#{103306}", new object[0]), new PopMenuItemLogic.MenuItemOnClicked(this.PopMenuDelEnemy));
			}
		}
	}

	// Token: 0x060047F6 RID: 18422 RVA: 0x001708E0 File Offset: 0x0016EAE0
	private void AddMenuItem(int Itemid, string strLabel, PopMenuItemLogic.MenuItemOnClicked func)
	{
		if (null == this.mResMenuItem)
		{
			return;
		}
		PopMenuItemLogic popMenuItemLogic;
		if (this.PopMenuItemLogicListDisEnable.Count <= 0)
		{
			popMenuItemLogic = this.SetParent(this.mResMenuItem, this.mMenuItemGrid, Itemid);
		}
		else
		{
			popMenuItemLogic = this.PopMenuItemLogicListDisEnable[this.PopMenuItemLogicListDisEnable.Count - 1];
			this.PopMenuItemLogicListDisEnable.RemoveAt(this.PopMenuItemLogicListDisEnable.Count - 1);
		}
		if (popMenuItemLogic != null)
		{
			popMenuItemLogic.InitMenuItem(strLabel, func);
			this.PopMenuItemLogicListEnable.Add(popMenuItemLogic);
			this.mMenuItemNum++;
		}
	}

	// Token: 0x060047F7 RID: 18423 RVA: 0x0017098C File Offset: 0x0016EB8C
	private PopMenuItemLogic SetParent(GameObject child, UIGrid parent, int Id)
	{
		if (child == null || parent == null)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate(child) as GameObject;
		parent.AddChild(gameObject.transform);
		gameObject.transform.localScale = Vector3.one;
		if (Id != -1)
		{
			gameObject.name = string.Format("MenuItem{0}", Id.ToString());
		}
		parent.Reposition();
		return gameObject.GetComponent<PopMenuItemLogic>();
	}

	// Token: 0x060047F8 RID: 18424 RVA: 0x00170A08 File Offset: 0x0016EC08
	private void PopRemoveTeamMember()
	{
		this.Close();
		team_kick.request request = new team_kick.request();
		request.characterId = HitOtherPLayerLogic.mTargetBasicInfo.ServerId;
		NetLogic.GetInstance().Send<Protocol.team_kick>(request, null);
	}

	// Token: 0x060047F9 RID: 18425 RVA: 0x00170A40 File Offset: 0x0016EC40
	private void PopRemoveGuildMember()
	{
		this.Close();
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{200112}", new object[]
		{
			HitOtherPLayerLogic.mTargetBasicInfo.Name
		}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnRemove), null, null, null);
	}

	// Token: 0x060047FA RID: 18426 RVA: 0x00170A94 File Offset: 0x0016EC94
	private void OnRemove()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.KickGuildMember(this.TargetServerId);
		}
		this.Close();
	}

	// Token: 0x060047FB RID: 18427 RVA: 0x00170ABC File Offset: 0x0016ECBC
	private void PopChangeDuate()
	{
		UnityVersionUtil.SetActiveRecursive(this.ErJiJieMian, true);
	}

	// Token: 0x060047FC RID: 18428 RVA: 0x00170ACC File Offset: 0x0016ECCC
	public void OnClickCloseErjiJieMian()
	{
		if (UnityVersionUtil.IsActive(this.ErJiJieMian.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.ErJiJieMian, false);
		}
		else
		{
			this.Close();
		}
	}

	// Token: 0x060047FD RID: 18429 RVA: 0x00170B08 File Offset: 0x0016ED08
	private void SetErJiJIeMianBtn(Guild_JOB job)
	{
		this.ErJiGrid.Reposition();
	}

	// Token: 0x060047FE RID: 18430 RVA: 0x00170B18 File Offset: 0x0016ED18
	private void PopMenuAddFriend()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL_APPLY))
			{
				FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
				if (friendInfo.IsCanAddFriend())
				{
					add_friend.request request = new add_friend.request();
					request.characterId = this.TargetServerId;
					NetLogic.GetInstance().Send<Protocol.add_friend>(request, null);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "apply_times");
				}
				else
				{
					NoticeLogic.AddNotifyData("#{100265}", true, false);
				}
			}
			else
			{
				int condition = DataManager.GetFunctionDataById(4062.ToString()).Condition;
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					condition
				}), true, false);
			}
		}
		this.Close();
	}

	// Token: 0x060047FF RID: 18431 RVA: 0x00170BF0 File Offset: 0x0016EDF0
	private void PopMenuAddEnemy()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL_APPLY))
			{
				FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
				if (friendInfo.IsCanAddEnemy())
				{
					add_friend.request request = new add_friend.request();
					request.characterId = this.TargetServerId;
					request.type = 1L;
					NetLogic.GetInstance().Send<Protocol.add_friend>(request, null);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Enemy", "add_times");
				}
				else
				{
					NoticeLogic.AddNotifyData("#{100265}", true, false);
				}
			}
			else
			{
				int condition = DataManager.GetFunctionDataById(4062.ToString()).Condition;
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					condition
				}), true, false);
			}
		}
		this.Close();
	}

	// Token: 0x06004800 RID: 18432 RVA: 0x00170CD0 File Offset: 0x0016EED0
	private void PopMenuTeam()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			req_invite_team.request request = new req_invite_team.request();
			request.characterid = this.TargetServerId;
			NetLogic.GetInstance().Send<Protocol.req_invite_team>(request, null);
		}
		this.Close();
	}

	// Token: 0x06004801 RID: 18433 RVA: 0x00170D0C File Offset: 0x0016EF0C
	private void PopMenuJoinGuild()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD))
			{
				guild_join.request request = new guild_join.request();
				request.guildId = HitOtherPLayerLogic.mTargetBasicInfo.GuildId;
				NetLogic.GetInstance().Send<Protocol.guild_join>(request, null);
			}
			else
			{
				int condition = DataManager.GetFunctionDataById(3017.ToString()).Condition;
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
				{
					condition
				}), true, false);
			}
		}
		this.Close();
	}

	// Token: 0x06004802 RID: 18434 RVA: 0x00170DA4 File Offset: 0x0016EFA4
	private void PopMenuInviteJoinGuild()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			guild_invite.request request = new guild_invite.request();
			request.id = HitOtherPLayerLogic.mTargetBasicInfo.ServerId;
			NetLogic.GetInstance().Send<Protocol.guild_invite>(request, null);
		}
		this.Close();
	}

	// Token: 0x06004803 RID: 18435 RVA: 0x00170DE4 File Offset: 0x0016EFE4
	private void PopMenuJoinTeam()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TEAM))
		{
			if (HitOtherPLayerLogic.mTargetBasicInfo != null)
			{
				req_other_team.request request = new req_other_team.request();
				request.id = HitOtherPLayerLogic.mTargetBasicInfo.ServerId;
				NetLogic.GetInstance().Send<Protocol.req_other_team>(request, null);
			}
			this.Close();
		}
		else
		{
			int condition = DataManager.GetFunctionDataById(3018.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
			{
				condition
			}), true, false);
		}
	}

	// Token: 0x06004804 RID: 18436 RVA: 0x00170E7C File Offset: 0x0016F07C
	private void PopMenuDel()
	{
		this.Close();
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100247}", new object[]
			{
				HitOtherPLayerLogic.mTargetBasicInfo.Name
			}), StrDictionary.GetDictionaryString("#{100244}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnConfirmClick), null, null, null);
		}
	}

	// Token: 0x06004805 RID: 18437 RVA: 0x00170EDC File Offset: 0x0016F0DC
	private void PopMenuDelEnemy()
	{
		this.Close();
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{103319}", new object[]
			{
				HitOtherPLayerLogic.mTargetBasicInfo.Name
			}), StrDictionary.GetDictionaryString("#{100244}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnConfirmClickDelEnemy), null, null, null);
		}
	}

	// Token: 0x06004806 RID: 18438 RVA: 0x00170F3C File Offset: 0x0016F13C
	private void OnConfirmClick()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			del_friend.request request = new del_friend.request();
			request.characterId = this.TargetServerId;
			NetLogic.GetInstance().Send<Protocol.del_friend>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "del_times");
		}
		this.Close();
	}

	// Token: 0x06004807 RID: 18439 RVA: 0x00170F90 File Offset: 0x0016F190
	private void OnConfirmClickDelEnemy()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			del_friend.request request = new del_friend.request();
			request.characterId = this.TargetServerId;
			request.type = 1L;
			NetLogic.GetInstance().Send<Protocol.del_friend>(request, null);
			FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
			friendInfo.RemoveEnemy(this.TargetServerId);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Enemy", "del_times");
		}
		this.Close();
	}

	// Token: 0x06004808 RID: 18440 RVA: 0x0017100C File Offset: 0x0016F20C
	private void PopMenuChat()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			ChatUIRootLogic.ResetPrivateChat(this.TargetServerId, HitOtherPLayerLogic.mTargetBasicInfo.Name, HitOtherPLayerLogic.mTargetBasicInfo.profession);
		}
		this.Close();
		if (SingletonUnity<SocialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SocialUIRootLogic>.Instance.OnClickCloseBtn();
		}
		if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickCloseBtn();
		}
		if (SingletonUnity<TeamUIRootNewLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject))
		{
			SingletonUnity<TeamUIRootNewLogic>.Instance.OnClickCloseBtn();
		}
	}

	// Token: 0x06004809 RID: 18441 RVA: 0x001710C0 File Offset: 0x0016F2C0
	private void OnChatRootShow(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			GameDefine.CHAT_CHANNEL_TYPE choosedChannelType = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ChoosedChannelType;
			SingletonUnity<ChatUIRootLogic>.Instance.Reset(choosedChannelType);
		}
	}

	// Token: 0x0600480A RID: 18442 RVA: 0x001710F0 File Offset: 0x0016F2F0
	private void PopMenuView()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			ask_character_info.request request = new ask_character_info.request();
			request.characterId = this.TargetServerId;
			NetLogic.GetInstance().Send<Protocol.ask_character_info>(request, new RpcRspHandler(this.Ret_Ask_Character_info));
		}
		this.Close();
	}

	// Token: 0x0600480B RID: 18443 RVA: 0x00171138 File Offset: 0x0016F338
	private void Ret_Ask_Character_info(SprotoTypeBase req)
	{
		if (UIManager.IsUnlockTutorialEnable())
		{
			return;
		}
		ask_character_info.response response = req as ask_character_info.response;
		if (response != null && response.HasCharacter)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OtherPlayerInfoUILogicRoot, new UIManager.OnOpenUIDelegate(this.OnOpenOtherPlayerInfoMenuRoot), response.character);
		}
	}

	// Token: 0x0600480C RID: 18444 RVA: 0x0017118C File Offset: 0x0016F38C
	private void OnOpenOtherPlayerInfoMenuRoot(bool success, object param)
	{
		character_look character_look = param as character_look;
		if (success && SingletonUnity<OtherPlayerInfoUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OtherPlayerInfoUILogic>.Instance.gameObject))
		{
			SingletonUnity<OtherPlayerInfoUILogic>.Instance.EnableReset();
			if (character_look != null)
			{
				List<GameItem> list = new List<GameItem>();
				if (character_look.HasEquip)
				{
					list = this.GetGameItemList(new List<gameitem>(character_look.equip.Values));
				}
				PROFESSION_TYPE type = (PROFESSION_TYPE)character_look.general.profession;
				string modeName = ServerToClientTools.GetModeName(character_look.visual.HeadId);
				CharacterAttributeData characterAttributeData = ServerToClientTools.attributeToCharacterAttributeData(character_look);
				characterAttributeData.Name = HitOtherPLayerLogic.mTargetBasicInfo.Name;
				SingletonUnity<OtherPlayerInfoUILogic>.Instance.ResetOtherPlayerInfo(list, type, modeName, characterAttributeData, character_look);
			}
		}
	}

	// Token: 0x0600480D RID: 18445 RVA: 0x00171244 File Offset: 0x0016F444
	private List<GameItem> GetGameItemList(List<gameitem> list)
	{
		List<GameItem> list2 = new List<GameItem>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null)
			{
				GameItem gameItem = ServerToClientTools.ServerGameItemToClientGameItem(list[i]);
				if (gameItem != null)
				{
					list2.Add(gameItem);
				}
			}
		}
		return list2;
	}

	// Token: 0x0600480E RID: 18446 RVA: 0x0017129C File Offset: 0x0016F49C
	private void PopMenuReport()
	{
		this.Close();
	}

	// Token: 0x0600480F RID: 18447 RVA: 0x001712A4 File Offset: 0x0016F4A4
	private void PopMenuFollow()
	{
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.FollowServerID = this.TargetServerId;
		}
		else
		{
			Singleton<ObjManager>.Instance.MainPlayer.FollowServerID = -1L;
		}
		this.Close();
	}

	// Token: 0x06004810 RID: 18448 RVA: 0x001712EC File Offset: 0x0016F4EC
	private void OnClickErJiJieMianItem(GameObject obj)
	{
		Guild_JOB guild_JOB = Guild_JOB.NO_JOB;
		string name = obj.name;
		if (name != null)
		{
			if (HitOtherPLayerLogic.<>f__switch$map13 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
				dictionary.Add("01_BossBtn", 0);
				dictionary.Add("04_MemberBtn", 1);
				dictionary.Add("03_ElderBtn", 2);
				dictionary.Add("02_ViceBtn", 3);
				HitOtherPLayerLogic.<>f__switch$map13 = dictionary;
			}
			int num;
			if (HitOtherPLayerLogic.<>f__switch$map13.TryGetValue(name, ref num))
			{
				switch (num)
				{
				case 0:
					MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100787}", new object[]
					{
						HitOtherPLayerLogic.mTargetBasicInfo.Name
					}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnSure), null, null, null);
					return;
				case 1:
					guild_JOB = Guild_JOB.JOB_Member;
					break;
				case 2:
					guild_JOB = Guild_JOB.JOB_Elder;
					break;
				case 3:
					guild_JOB = Guild_JOB.JOB_VicePresident;
					break;
				}
			}
		}
		if (HitOtherPLayerLogic.mTargetBasicInfo != null && guild_JOB != Guild_JOB.NO_JOB)
		{
			Singleton<ObjManager>.Instance.MainPlayer.ChangeMemberJob(this.TargetServerId, guild_JOB);
		}
		this.Close();
	}

	// Token: 0x06004811 RID: 18449 RVA: 0x00171400 File Offset: 0x0016F600
	private void OnSure()
	{
		this.Close();
		if (HitOtherPLayerLogic.mTargetBasicInfo != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.ChangeMemberJob(this.TargetServerId, Guild_JOB.JOB_Chief);
		}
	}

	// Token: 0x06004812 RID: 18450 RVA: 0x00171434 File Offset: 0x0016F634
	public void Close()
	{
		for (int i = this.PopMenuItemLogicListEnable.Count - 1; i >= 0; i--)
		{
			this.PopMenuItemLogicListDisEnable.Add(this.PopMenuItemLogicListEnable[i]);
			this.PopMenuItemLogicListEnable.RemoveAt(i);
		}
		this.mMenuItemNum = 0;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.HitOtherPlayerRoot);
	}

	// Token: 0x06004813 RID: 18451 RVA: 0x00171498 File Offset: 0x0016F698
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.Close();
		}
	}

	// Token: 0x0400355C RID: 13660
	private Camera mMainCamera;

	// Token: 0x0400355D RID: 13661
	public GameObject mPopMenuOffset;

	// Token: 0x0400355E RID: 13662
	public UIGrid mMenuItemGrid;

	// Token: 0x0400355F RID: 13663
	private int mMenuItemNum;

	// Token: 0x04003560 RID: 13664
	private GameObject mResMenuItem;

	// Token: 0x04003561 RID: 13665
	private long TargetServerId;

	// Token: 0x04003562 RID: 13666
	private static TargetBasicInfo mTargetBasicInfo;

	// Token: 0x04003563 RID: 13667
	private static Vector3 targetPos;

	// Token: 0x04003564 RID: 13668
	public UILabel Name;

	// Token: 0x04003565 RID: 13669
	public UILabel Level;

	// Token: 0x04003566 RID: 13670
	public UILabel CombolLabel;

	// Token: 0x04003567 RID: 13671
	public UILabel GuildNameLabel;

	// Token: 0x04003568 RID: 13672
	public UISprite Icon;

	// Token: 0x04003569 RID: 13673
	public GameObject ErJiJieMian;

	// Token: 0x0400356A RID: 13674
	public GameObject JiBenGongNeng;

	// Token: 0x0400356B RID: 13675
	public UIGrid ErJiGrid;

	// Token: 0x0400356C RID: 13676
	public UISprite BottomPic;

	// Token: 0x0400356D RID: 13677
	public UISprite RightPic;

	// Token: 0x0400356E RID: 13678
	private HitType currHitType;

	// Token: 0x0400356F RID: 13679
	private List<PopMenuItemLogic> PopMenuItemLogicListEnable = new List<PopMenuItemLogic>();

	// Token: 0x04003570 RID: 13680
	private List<PopMenuItemLogic> PopMenuItemLogicListDisEnable = new List<PopMenuItemLogic>();

	// Token: 0x04003571 RID: 13681
	private static List<object> initParams = new List<object>();

	// Token: 0x04003572 RID: 13682
	private bool ScreenFitInitFlag;

	// Token: 0x04003573 RID: 13683
	private float mHalfMenuWidth;

	// Token: 0x04003574 RID: 13684
	private float mHalfMenuHeight;

	// Token: 0x04003575 RID: 13685
	private bool FristInit;
}
