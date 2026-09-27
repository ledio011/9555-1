using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009C3 RID: 2499
public class TeamPreparationRootLogic : SingletonUnity<TeamPreparationRootLogic>
{
	// Token: 0x17000FB2 RID: 4018
	// (get) Token: 0x06004723 RID: 18211 RVA: 0x0016A6DC File Offset: 0x001688DC
	public long Session
	{
		get
		{
			return this.mSession;
		}
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x0016A6E4 File Offset: 0x001688E4
	public void Reset()
	{
		this.Reset(this.mSession, false);
	}

	// Token: 0x06004725 RID: 18213 RVA: 0x0016A6F4 File Offset: 0x001688F4
	public void Reset(long session, bool refreshTime)
	{
		this.mSession = session;
		this.mTeamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
		if (this.mTeamInfo == null || this.mTeamInfo.TeamID == -1L)
		{
			return;
		}
		this.mTeamMemberList = this.mTeamInfo.TeamMembers;
		this.mTeamLeader = this.mTeamInfo.TeamLeader;
		this.ResetPlayerIcon(this.mTeamLeader, 0);
		for (int i = 0; i < this.mTeamMemberList.Length; i++)
		{
			if (this.mTeamMemberList[i] != null && this.mTeamMemberList[i].IsValid())
			{
				if (this.mTeamMemberList[i].IsRefuseEnterCopy)
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamPreparationRoot);
					return;
				}
				this.ResetPlayerIcon(this.mTeamMemberList[i], i + 1);
			}
			else
			{
				this.ResetPlayerIconEmpty(i + 1);
			}
		}
		this.mIsReady = false;
		if (this.mTeamLeader.ServerId == PlayerData.MainPlayerServerId)
		{
			this.mIsReady = this.mTeamLeader.IsReadyEnterCopy;
		}
		else
		{
			for (int j = 0; j < this.mTeamMemberList.Length; j++)
			{
				if (this.mTeamMemberList[j] != null && this.mTeamMemberList[j].IsValid() && this.mTeamMemberList[j].ServerId == PlayerData.MainPlayerServerId)
				{
					if (this.mTeamMemberList[j].IsReadyEnterCopy)
					{
						this.mIsReady = true;
					}
					else
					{
						this.mIsReady = false;
					}
				}
			}
		}
		if (refreshTime)
		{
			this.mTimeCount = this.mWaitTime;
		}
		this.ShowReady(this.mIsReady);
	}

	// Token: 0x06004726 RID: 18214 RVA: 0x0016A8A8 File Offset: 0x00168AA8
	public void ShowReady(bool isReady)
	{
		if (isReady)
		{
			this.WaitInfoLabel.enabled = false;
			this.ReadyInfoLabel.enabled = true;
			NGUITools.SetActive(this.ReadyBtnRoot, false);
			NGUITools.SetActive(this.CancelBtnRoot, false);
		}
		else
		{
			this.WaitInfoLabel.enabled = true;
			this.ReadyInfoLabel.enabled = false;
			NGUITools.SetActive(this.ReadyBtnRoot, true);
			NGUITools.SetActive(this.CancelBtnRoot, true);
			this.CancelBtnLabel.text = string.Format("{0}({1})", StrDictionary.GetDictionaryString("#{102069}", new object[0]), (int)this.mTimeCount);
		}
	}

	// Token: 0x06004727 RID: 18215 RVA: 0x0016A954 File Offset: 0x00168B54
	public void ResetPlayerIcon(TeamMember memberInfo, int index)
	{
		this.IconPicList[index].spriteName = GameDefine.Game_Player_Icon_pic[(int)memberInfo.Profession];
		this.IconPicList[index].width = 54;
		this.IconPicList[index].height = 63;
		this.NameLabelList[index].enabled = true;
		this.NameLabelList[index].text = memberInfo.Name;
		this.LevelLabelList[index].enabled = true;
		this.LevelLabelList[index].text = string.Format("Lv.{0}", memberInfo.Level);
		this.RemainTimesList[index].enabled = true;
		CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.mTeamInfo.TeamGoalData.CopyId);
		this.RemainTimesList[index].text = string.Format("{0}:{1}/{2}", StrDictionary.GetDictionaryString("#{100749}", new object[0]), memberInfo.CopyRestNum, copySceneDataById.MaxPlayNum);
		if (memberInfo.ServerId == PlayerData.MainPlayerServerId)
		{
			this.RemainNum = memberInfo.CopyRestNum;
		}
		if (memberInfo.IsReadyEnterCopy)
		{
			this.ReadyPicList[index].enabled = true;
		}
		else
		{
			this.ReadyPicList[index].enabled = false;
		}
	}

	// Token: 0x06004728 RID: 18216 RVA: 0x0016AA94 File Offset: 0x00168C94
	public void ResetPlayerIconEmpty(int index)
	{
		this.IconPicList[index].spriteName = "CZ_zuDui_wuRen";
		this.IconPicList[index].width = 60;
		this.IconPicList[index].height = 60;
		this.NameLabelList[index].enabled = false;
		this.LevelLabelList[index].enabled = false;
		this.ReadyPicList[index].enabled = false;
		this.RemainTimesList[index].enabled = false;
	}

	// Token: 0x06004729 RID: 18217 RVA: 0x0016AB0C File Offset: 0x00168D0C
	public void OnClickCancelBtn()
	{
		ret_ask_confirm_multi_copy_scene.request request = new ret_ask_confirm_multi_copy_scene.request();
		request.state = 1L;
		request.session = this.mSession;
		NetLogic.GetInstance().Send<Protocol.ret_ask_confirm_multi_copy_scene>(request, null);
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
		{
			leave_team.request request2 = new leave_team.request();
			request2.teamid = this.mTeamInfo.TeamID;
			request2.characterId = PlayerData.MainPlayerServerId;
			NetLogic.GetInstance().Send<Protocol.leave_team>(request2, null);
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamPreparationRoot);
	}

	// Token: 0x0600472A RID: 18218 RVA: 0x0016AB90 File Offset: 0x00168D90
	public void OnClickReadyBtn()
	{
		if (this.RemainNum <= 0)
		{
			MessageBoxLogic.OpenOKCancelBox("#{103206}", "#{100127}", delegate
			{
				this.AutoReady();
			}, null, null, null);
		}
		else
		{
			this.AutoReady();
		}
	}

	// Token: 0x0600472B RID: 18219 RVA: 0x0016ABC8 File Offset: 0x00168DC8
	public void AutoReady()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
		{
			ret_ask_confirm_multi_copy_scene.request request = new ret_ask_confirm_multi_copy_scene.request();
			request.state = 1L;
			request.session = this.mSession;
			NetLogic.GetInstance().Send<Protocol.ret_ask_confirm_multi_copy_scene>(request, null);
		}
		else
		{
			ret_ask_confirm_multi_copy_scene.request request2 = new ret_ask_confirm_multi_copy_scene.request();
			request2.state = 0L;
			request2.session = this.mSession;
			NetLogic.GetInstance().Send<Protocol.ret_ask_confirm_multi_copy_scene>(request2, null);
			Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
			teamInfo.SelfMember.IsReadyEnterCopy = true;
		}
		this.Reset(this.mSession, false);
	}

	// Token: 0x0600472C RID: 18220 RVA: 0x0016AC64 File Offset: 0x00168E64
	private void Update()
	{
		if (!this.mIsReady)
		{
			this.mTimeCount -= Time.deltaTime;
			this.flashInterval -= Time.deltaTime;
			if (this.flashInterval < 0f)
			{
				this.flashInterval += 1f;
				if (GameManager.OnLineState)
				{
					this.CancelBtnLabel.text = string.Format("{0}({1})", StrDictionary.GetDictionaryString("#{102069}", new object[0]), (int)this.mTimeCount);
					if (this.mTimeCount < 0f)
					{
						this.AutoReady();
					}
				}
			}
		}
	}

	// Token: 0x0600472D RID: 18221 RVA: 0x0016AD14 File Offset: 0x00168F14
	private void OnDisable()
	{
		Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
		if (teamInfo != null)
		{
			teamInfo.IsCheckingEnterCopy = false;
		}
	}

	// Token: 0x04003444 RID: 13380
	public UILabel[] NameLabelList;

	// Token: 0x04003445 RID: 13381
	public UISprite[] IconPicList;

	// Token: 0x04003446 RID: 13382
	public UILabel[] LevelLabelList;

	// Token: 0x04003447 RID: 13383
	public UISprite[] ReadyPicList;

	// Token: 0x04003448 RID: 13384
	public UILabel[] RemainTimesList;

	// Token: 0x04003449 RID: 13385
	public UILabel CancelBtnLabel;

	// Token: 0x0400344A RID: 13386
	public GameObject CancelBtnRoot;

	// Token: 0x0400344B RID: 13387
	public GameObject ReadyBtnRoot;

	// Token: 0x0400344C RID: 13388
	public UILabel WaitInfoLabel;

	// Token: 0x0400344D RID: 13389
	public UILabel ReadyInfoLabel;

	// Token: 0x0400344E RID: 13390
	private TeamMember[] mTeamMemberList;

	// Token: 0x0400344F RID: 13391
	private TeamMember mTeamLeader;

	// Token: 0x04003450 RID: 13392
	private Team mTeamInfo;

	// Token: 0x04003451 RID: 13393
	private float mWaitTime = 20f;

	// Token: 0x04003452 RID: 13394
	private float mTimeCount;

	// Token: 0x04003453 RID: 13395
	private long mSession;

	// Token: 0x04003454 RID: 13396
	private bool mIsReady;

	// Token: 0x04003455 RID: 13397
	private int RemainNum;

	// Token: 0x04003456 RID: 13398
	private float flashInterval = 1f;
}
