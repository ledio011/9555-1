using System;
using SprotoType;

// Token: 0x020008FF RID: 2303
public class DialogManager : Singleton<DialogManager>
{
	// Token: 0x17000F89 RID: 3977
	// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x00122410 File Offset: 0x00120610
	public ObjNPC TargetNPC
	{
		get
		{
			return this.mTargetNPC;
		}
	}

	// Token: 0x06003EE9 RID: 16105 RVA: 0x00122418 File Offset: 0x00120618
	public void OnCloseDialog()
	{
	}

	// Token: 0x06003EEA RID: 16106 RVA: 0x0012241C File Offset: 0x0012061C
	public void ShowDialog(ObjNPC objNpc, string missionId)
	{
		this.mTargetNPC = objNpc;
		this.mTargetNPC.AnimationLogic.PlayTalkAnimation();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (this.mTargetNPC != null)
		{
			this.mTargetNPC.FaceToPub(mainPlayer.Position);
			if (!mainPlayer.IsLocalDrivingCar)
			{
				mainPlayer.FaceToPub(this.mTargetNPC.Position);
			}
		}
		if (this.mTargetNPC == null)
		{
			return;
		}
		if (!this.mTargetNPC.CheckInDialogRange())
		{
			return;
		}
		if (this.PopCompleteMission(missionId))
		{
			return;
		}
		if (this.PopTargetMission(missionId))
		{
			return;
		}
		if (this.PopAcceptableMission(missionId))
		{
			return;
		}
		if (this.PopOptionDialog())
		{
			return;
		}
		this.ShowNormalDialog(this.mTargetNPC.DefaultDialogID);
	}

	// Token: 0x06003EEB RID: 16107 RVA: 0x001224F0 File Offset: 0x001206F0
	private bool PopTargetMission(string targetMissionId)
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		if (!string.IsNullOrEmpty(targetMissionId))
		{
			if (!missionManager.IsMissionAccepted(targetMissionId))
			{
				return false;
			}
			MissionData missionDataByID = DataManager.GetMissionDataByID(targetMissionId);
			if (missionDataByID == null)
			{
				return false;
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level < missionDataByID.MinLv)
			{
				return false;
			}
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData = missionManager.GetCurMultiDeliveryTargetData(targetMissionId);
				if (!this.mTargetNPC.NPCDataID.Equals(curMultiDeliveryTargetData.TargetNpcId))
				{
					return false;
				}
			}
			else if (missionDataByID.Target != this.mTargetNPC.NPCDataID)
			{
				return false;
			}
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.STORY && missionManager.GetMissionState(targetMissionId) == MISSION_STATE.ACCEPTED)
			{
				StoryDialogRootLogic.ShowStory(missionDataByID.LogicID, this.mTargetNPC.NPCData);
				return true;
			}
			if (missionManager.GetMissionState(targetMissionId) == MISSION_STATE.ACCEPTED)
			{
				this.ShowMissionDialogUI(targetMissionId);
				return true;
			}
		}
		else
		{
			for (int i = 0; i < this.mTargetNPC.MissionIdList.Count; i++)
			{
				string text = this.mTargetNPC.MissionIdList[i];
				if (missionManager.IsMissionAccepted(text))
				{
					MissionData missionDataByID2 = DataManager.GetMissionDataByID(text);
					if (missionDataByID2 != null)
					{
						if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level >= missionDataByID2.MinLv)
						{
							if (missionDataByID2.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
							{
								MultiDeliveryMissionData curMultiDeliveryTargetData2 = missionManager.GetCurMultiDeliveryTargetData(text);
								if (!this.mTargetNPC.NPCDataID.Equals(curMultiDeliveryTargetData2.TargetNpcId))
								{
									goto IL_1F9;
								}
							}
							else if (missionDataByID2.Target != this.mTargetNPC.NPCDataID)
							{
								goto IL_1F9;
							}
							if (missionDataByID2.MissionLogicType == MISSION_LOGICTYPE.STORY && missionManager.GetMissionState(text) == MISSION_STATE.ACCEPTED)
							{
								StoryDialogRootLogic.ShowStory(missionDataByID2.LogicID, this.mTargetNPC.NPCData);
								return true;
							}
							if (missionManager.GetMissionState(text) == MISSION_STATE.ACCEPTED)
							{
								this.ShowMissionDialogUI(text);
								return true;
							}
						}
					}
				}
				IL_1F9:;
			}
		}
		return false;
	}

	// Token: 0x06003EEC RID: 16108 RVA: 0x00122714 File Offset: 0x00120914
	private bool PopCompleteMission(string targetMissionId)
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		if (!string.IsNullOrEmpty(targetMissionId))
		{
			if (this.mTargetNPC.MissionIdList.Contains(targetMissionId))
			{
				if (!missionManager.IsMissionAccepted(targetMissionId))
				{
					return false;
				}
				MissionData missionDataByID = DataManager.GetMissionDataByID(targetMissionId);
				if (missionDataByID == null)
				{
					return false;
				}
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level < missionDataByID.MinLv)
				{
					return false;
				}
				if (missionDataByID.Submit != this.mTargetNPC.NPCDataID)
				{
					return false;
				}
				if (missionManager.GetMissionState(targetMissionId) == MISSION_STATE.COMPLETE)
				{
					this.ShowMissionDialogUI(targetMissionId);
					return true;
				}
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.STORY)
				{
					StoryDialogRootLogic.ShowStory(missionDataByID.LogicID, this.mTargetNPC.NPCData);
					return true;
				}
			}
		}
		else
		{
			for (int i = 0; i < this.mTargetNPC.MissionIdList.Count; i++)
			{
				string text = this.mTargetNPC.MissionIdList[i];
				if (missionManager.IsMissionAccepted(text))
				{
					MissionData missionDataByID2 = DataManager.GetMissionDataByID(text);
					if (missionDataByID2 == null)
					{
						return false;
					}
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level < missionDataByID2.MinLv)
					{
						return false;
					}
					if (!(missionDataByID2.Submit != this.mTargetNPC.NPCDataID))
					{
						if (missionManager.GetMissionState(text) == MISSION_STATE.COMPLETE)
						{
							this.ShowMissionDialogUI(text);
							return true;
						}
						if (missionDataByID2.MissionLogicType == MISSION_LOGICTYPE.STORY)
						{
							StoryDialogRootLogic.ShowStory(missionDataByID2.LogicID, this.mTargetNPC.NPCData);
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003EED RID: 16109 RVA: 0x001228B0 File Offset: 0x00120AB0
	private bool PopAcceptableMission(string missionId)
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		if (!string.IsNullOrEmpty(missionId))
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
			if (missionDataByID == null)
			{
				return false;
			}
			if (missionDataByID.Accept != this.mTargetNPC.NPCDataID)
			{
				return false;
			}
			if (missionManager.IsMissionAcceptable(missionId))
			{
				this.ShowMissionDialogUI(missionId);
				return true;
			}
			if (missionDataByID.Class == 4 || missionDataByID.Class == 5)
			{
				if (missionManager.IsMissionAccepted(missionId))
				{
					this.ShowNormalDialogStr("#{102013}");
					return true;
				}
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (missionDataByID.Class == 4)
				{
					CurMission robboryMission = missionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.ROBBERY);
					if (robboryMission != null)
					{
						MessageBoxLogic.OpenOKCancelBox("#{201001}", "#{100127}", delegate
						{
							missionManager.AbandonMission(robboryMission.MissionId, false);
						}, null, null, null);
						return true;
					}
					if (playerData.Level < missionDataByID.MinLv)
					{
						this.ShowNormalDialogStr("#{102010}");
						return true;
					}
					activity_info activityInfoByType = playerData.ActivityData.GetActivityInfoByType(1);
					if (activityInfoByType == null || activityInfoByType.CurNum <= 0L)
					{
						this.ShowNormalDialogStr("#{102009}");
						return true;
					}
				}
				else if (missionDataByID.Class == 5)
				{
					CurMission escortMission = missionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.ESCORT);
					if (escortMission != null)
					{
						MessageBoxLogic.OpenOKCancelBox("#{201002}", "#{100127}", delegate
						{
							missionManager.AbandonMission(escortMission.MissionId, false);
						}, null, null, null);
						return true;
					}
					if (playerData.Level < missionDataByID.MinLv)
					{
						this.ShowNormalDialogStr("#{102012}");
						return true;
					}
					activity_info activityInfoByType2 = playerData.ActivityData.GetActivityInfoByType(2);
					if (activityInfoByType2 == null || activityInfoByType2.CurNum <= 0L)
					{
						this.ShowNormalDialogStr("#{102011}");
						return true;
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < this.mTargetNPC.MissionIdList.Count; i++)
			{
				string text = this.mTargetNPC.MissionIdList[i];
				MissionData missionDataByID2 = DataManager.GetMissionDataByID(text);
				if (missionDataByID2 != null)
				{
					if (!(missionDataByID2.Accept != this.mTargetNPC.NPCDataID))
					{
						if (missionManager.IsMissionAcceptable(text))
						{
							this.ShowMissionDialogUI(text);
							return true;
						}
						if (missionDataByID2.Class == 4 || missionDataByID2.Class == 5)
						{
							if (missionManager.IsMissionAccepted(text))
							{
								this.ShowNormalDialogStr("#{102013}");
								return true;
							}
							PlayerData playerData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
							if (missionDataByID2.Class == 4)
							{
								CurMission robboryMission = missionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.ROBBERY);
								if (robboryMission != null)
								{
									MessageBoxLogic.OpenOKCancelBox("#{201001}", "#{100127}", delegate
									{
										missionManager.AbandonMission(robboryMission.MissionId, false);
									}, null, null, null);
									return true;
								}
								if (playerData2.Level < missionDataByID2.MinLv)
								{
									this.ShowNormalDialogStr("#{102010}");
									return true;
								}
								activity_info activityInfoByType3 = playerData2.ActivityData.GetActivityInfoByType(1);
								if (activityInfoByType3 == null || activityInfoByType3.CurNum <= 0L)
								{
									this.ShowNormalDialogStr("#{102009}");
									return true;
								}
							}
							else if (missionDataByID2.Class == 5)
							{
								CurMission escortMission = missionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.ESCORT);
								if (escortMission != null)
								{
									MessageBoxLogic.OpenOKCancelBox("#{201002}", "#{100127}", delegate
									{
										missionManager.AbandonMission(escortMission.MissionId, false);
									}, null, null, null);
									return true;
								}
								if (playerData2.Level < missionDataByID2.MinLv)
								{
									this.ShowNormalDialogStr("#{102012}");
									return true;
								}
								activity_info activityInfoByType4 = playerData2.ActivityData.GetActivityInfoByType(2);
								if (activityInfoByType4 == null || activityInfoByType4.CurNum <= 0L)
								{
									this.ShowNormalDialogStr("#{102011}");
									return true;
								}
							}
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003EEE RID: 16110 RVA: 0x00122D00 File Offset: 0x00120F00
	public void ShowMissionDialogUI(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (this.mTargetNPC == null)
		{
			return;
		}
		if (!this.mTargetNPC.IsContainsMission(missionId))
		{
			return;
		}
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		bool flag = missionManager.IsMissionAccepted(missionId);
		MISSION_STATE missionState = missionManager.GetMissionState(missionId);
		if (flag && missionState != MISSION_STATE.FAIL)
		{
			if (missionState == MISSION_STATE.COMPLETE)
			{
				if (missionDataByID.Submit == this.mTargetNPC.NPCDataID)
				{
					this.ShowMissionDialogUI(missionId, MISSION_STATE.COMPLETE);
				}
			}
			else if (missionState == MISSION_STATE.ACCEPTED)
			{
				if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
				{
					MultiDeliveryMissionData curMultiDeliveryTargetData = missionManager.GetCurMultiDeliveryTargetData(missionId);
					if (this.mTargetNPC.NPCDataID.Equals(curMultiDeliveryTargetData.TargetNpcId))
					{
						this.ShowMissionTargetDialogUI(missionId);
					}
				}
				else if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.COPYSCENE_KILLMONSTER)
				{
					this.ShowEnterCopyDialogUI(missionId);
				}
				else if (missionDataByID.Target == this.mTargetNPC.NPCDataID)
				{
					this.ShowMissionTargetDialogUI(missionId);
				}
			}
		}
		else if (missionManager.IsMissionAcceptable(missionId) && missionDataByID.Accept == this.mTargetNPC.NPCDataID)
		{
			if (missionDataByID.Class == 4 || missionDataByID.Class == 5)
			{
				this.ShowSideMissionAcceptUI(missionId);
			}
			else
			{
				this.ShowMissionDialogUI(missionId, MISSION_STATE.ACCEPTED);
			}
		}
	}

	// Token: 0x06003EEF RID: 16111 RVA: 0x00122E68 File Offset: 0x00121068
	public void ShowEnterCopyDialogUI(string missionId)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OptionDialogUI, new UIManager.OnOpenUIDelegate(this.OnShowEnterMissionCopyDialogUI), missionId);
		}
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x00122EA0 File Offset: 0x001210A0
	private void OnShowEnterMissionCopyDialogUI(bool isSuccess, object misId)
	{
		if (!isSuccess)
		{
			return;
		}
		string id = (string)misId;
		MissionData missionDataByID = DataManager.GetMissionDataByID(id);
		NpcData npcDataByID = DataManager.GetNpcDataByID(missionDataByID.Target);
		if (SingletonUnity<OptionDialogUILogic>.Exists)
		{
			SingletonUnity<OptionDialogUILogic>.Instance.ResetOptionDialog(npcDataByID, missionDataByID.TargetDialog, "Yes", "No", missionDataByID.LogicID, delegate(string val)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
				enter_copy_scene.request request = new enter_copy_scene.request();
				request.mapInfoId = val;
				NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request, null);
			}, false);
		}
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x00122F18 File Offset: 0x00121118
	public void ShowMissionTargetDialogUI(string missionId)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogUI, new UIManager.OnOpenUIDelegate(this.OnShowMissionTargetDialogUI), missionId);
		}
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x00122F50 File Offset: 0x00121150
	private void OnShowMissionTargetDialogUI(bool isSuccess, object misId)
	{
		if (!isSuccess)
		{
			return;
		}
		string missionId = (string)misId;
		if (SingletonUnity<DialogUILogic>.Exists)
		{
			SingletonUnity<DialogUILogic>.Instance.ResetMissionTargetDialogUI(missionId, this.mTargetNPC.NPCData);
		}
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x00122F8C File Offset: 0x0012118C
	public void ShowSideMissionAcceptUI(string missionId)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			activity_info activityInfoByType;
			if (missionDataByID.Class == 4)
			{
				activityInfoByType = playerData.ActivityData.GetActivityInfoByType(1);
			}
			else
			{
				activityInfoByType = playerData.ActivityData.GetActivityInfoByType(2);
			}
			if (activityInfoByType != null)
			{
				if (activityInfoByType.State == 0L && SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() < activityInfoByType.Parm)
				{
					MessageBoxLogic.OpenOKCancelBox("#{200067}", "#{100127}", delegate
					{
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogMissionUI, delegate
						{
							SingletonUnity<DialogMissionUIRoot>.Instance.ResetSideMissionAcceptUI(missionId, this.mTargetNPC.NPCData);
						}, null);
					}, null, null, null);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogMissionUI, delegate
					{
						SingletonUnity<DialogMissionUIRoot>.Instance.ResetSideMissionAcceptUI(missionId, this.mTargetNPC.NPCData);
					}, null);
				}
			}
		}
	}

	// Token: 0x06003EF4 RID: 16116 RVA: 0x0012306C File Offset: 0x0012126C
	public void ShowMissionDialogUI(string missionId, MISSION_STATE type)
	{
		MissionUIInfo param = new MissionUIInfo(missionId, type);
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogMissionUI, new UIManager.OnOpenUIDelegate(this.OnShowMissionDialog), param);
		}
	}

	// Token: 0x06003EF5 RID: 16117 RVA: 0x001230AC File Offset: 0x001212AC
	private void OnShowMissionDialog(bool isSuccess, object info)
	{
		if (!isSuccess)
		{
			return;
		}
		MissionUIInfo missionUIInfo = (MissionUIInfo)info;
		if (SingletonUnity<DialogMissionUIRoot>.Exists && missionUIInfo != null)
		{
			SingletonUnity<DialogMissionUIRoot>.Instance.ResetMissionUI(missionUIInfo.MissionID, this.mTargetNPC.NPCData, missionUIInfo.uiType);
		}
	}

	// Token: 0x06003EF6 RID: 16118 RVA: 0x001230F8 File Offset: 0x001212F8
	private void ShowNormalDialogStr(string str)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogUI, delegate
			{
				if (SingletonUnity<DialogUILogic>.Exists)
				{
					SingletonUnity<DialogUILogic>.Instance.ResetNormalDialogByStr(str, this.mTargetNPC.NPCData);
				}
			}, null);
		}
	}

	// Token: 0x06003EF7 RID: 16119 RVA: 0x00123144 File Offset: 0x00121344
	private void ShowNormalDialog(string dialogId)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DialogUI, new UIManager.OnOpenUIDelegate(this.OnShowNormalDialogUI), dialogId);
		}
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x0012317C File Offset: 0x0012137C
	private void OnShowNormalDialogUI(bool isSuccess, object diaId)
	{
		if (!isSuccess)
		{
			return;
		}
		string dialogId = (string)diaId;
		if (SingletonUnity<DialogUILogic>.Exists)
		{
			SingletonUnity<DialogUILogic>.Instance.ResetNormalDialogUI(dialogId, this.mTargetNPC.NPCData);
		}
	}

	// Token: 0x06003EF9 RID: 16121 RVA: 0x001231B8 File Offset: 0x001213B8
	private bool PopOptionDialog()
	{
		NPCDialogData npcdialogDataByID = DataManager.GetNPCDialogDataByID(this.mTargetNPC.DefaultDialogID);
		if (npcdialogDataByID != null && !string.IsNullOrEmpty(npcdialogDataByID.OptionDialogID))
		{
			this.ShowOptionDialogUI(npcdialogDataByID.OptionDialogID);
			return true;
		}
		return false;
	}

	// Token: 0x06003EFA RID: 16122 RVA: 0x001231FC File Offset: 0x001213FC
	public void ShowOptionDialogUI(string optionDialogId)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OptionDialogUI, new UIManager.OnOpenUIDelegate(this.OnShowOptionDialog), optionDialogId);
		}
	}

	// Token: 0x06003EFB RID: 16123 RVA: 0x00123234 File Offset: 0x00121434
	private void OnShowOptionDialog(bool isSuccess, object optDialogId)
	{
		if (!isSuccess)
		{
			return;
		}
		if (SingletonUnity<OptionDialogUILogic>.Exists)
		{
			SingletonUnity<OptionDialogUILogic>.Instance.ResetOptionDialog(optDialogId as string, this.mTargetNPC.NPCData, false);
		}
	}

	// Token: 0x04002AAF RID: 10927
	private ObjNPC mTargetNPC;
}
