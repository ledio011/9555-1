using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000902 RID: 2306
public class DialogUILogic : SingletonUnity<DialogUILogic>
{
	// Token: 0x06003F0F RID: 16143 RVA: 0x00123D4C File Offset: 0x00121F4C
	public void OnClickAcceptBtn()
	{
		this.CloseDialog();
		MissionData curMissionData = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (curMissionData.Class == 4 || curMissionData.Class == 5)
		{
			GameManager gameManager = SingletonDontDestoryUnity<GameManager>.Instance;
			ActivityData activityData = gameManager.PlayerData.ActivityData;
			activity_info activityInfoByType;
			if (curMissionData.Class == 4)
			{
				activityInfoByType = activityData.GetActivityInfoByType(1);
			}
			else
			{
				activityInfoByType = activityData.GetActivityInfoByType(2);
			}
			if (activityInfoByType.State == 1L && gameManager.PlayerCommonData.GetCurServerTime() < activityInfoByType.Parm)
			{
				gameManager.MissionManager.AcceptMission(this.mCurMissionID);
			}
			else if (activityInfoByType.State == 0L && gameManager.PlayerCommonData.GetCurServerTime() >= activityInfoByType.Parm)
			{
				gameManager.MissionManager.AcceptMission(this.mCurMissionID);
			}
			else
			{
				MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101572}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.mCurMissionID);
					if (curMissionData.Class == 5 && gameManager.PlayerData.MainPlayerAttrData.PkMode == 0)
					{
						vp_Timer.In(Time.deltaTime * 2f, delegate()
						{
							MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{102015}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
							{
								request_change_pk_mode.request request = new request_change_pk_mode.request();
								request.pk = 1L;
								NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request, null);
								gameManager.PlayerData.SetPKModeState(1);
							}, null, null, null);
						}, null);
					}
				}, null, null, null);
			}
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.mCurMissionID);
		}
	}

	// Token: 0x06003F10 RID: 16144 RVA: 0x00123ECC File Offset: 0x001220CC
	public void OnClickCompleteBtn()
	{
		this.CloseDialog();
		SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.CompleteMission(this.mCurMissionID);
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x00123EEC File Offset: 0x001220EC
	public void OnClickCancelBtn()
	{
		this.CloseDialog();
		if (this.mCurMissionType == MISSION_STATE.ACCEPTED)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.mCurMissionID);
		}
	}

	// Token: 0x06003F12 RID: 16146 RVA: 0x00123F18 File Offset: 0x00122118
	public void OnClickOkBtn()
	{
		this.CloseDialog();
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			List<MultiDeliveryMissionData> multiDeliveryMissionDataListById = DataManager.GetMultiDeliveryMissionDataListById(missionDataByID.LogicID);
			int num = (int)missionManager.GetMissionParam(this.mCurMissionID, 0);
			if (num == multiDeliveryMissionDataListById.Count - 1)
			{
				update_misison_complete.request request = new update_misison_complete.request();
				request.missionId = this.mCurMissionID;
				NetLogic.GetInstance().Send<Protocol.update_misison_complete>(request, null);
			}
			else
			{
				update_misison_parm.request request2 = new update_misison_parm.request();
				request2.missionId = this.mCurMissionID;
				request2.paramType = 1L;
				request2.paramValue = 1L;
				NetLogic.GetInstance().Send<Protocol.update_misison_parm>(request2, null);
			}
		}
	}

	// Token: 0x06003F13 RID: 16147 RVA: 0x00123FD8 File Offset: 0x001221D8
	private void CloseDialog()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DialogUI);
		Singleton<DialogManager>.Instance.OnCloseDialog();
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x00123FF4 File Offset: 0x001221F4
	public void Reset()
	{
		this.mCurMissionType = MISSION_STATE.INVALID;
		this.TitleLabel.text = string.Empty;
		this.TextLabel.text = string.Empty;
	}

	// Token: 0x06003F15 RID: 16149 RVA: 0x00124020 File Offset: 0x00122220
	public void ResetSideMissionAcceptUI(string missionId, NpcData npcData)
	{
		this.Reset();
		this.mCurMissionID = missionId;
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID == null)
		{
			return;
		}
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, true);
		UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.OkBtn, false);
		this.CancelBtn.transform.localPosition = new Vector3(180f, -34f, 0f);
		UnityVersionUtil.SetActiveRecursive(this.CancelBtn, true);
		this.ShowRewardItem.transform.localPosition = new Vector3(90f, -198f, 0f);
		this.TitleLabel.text = npcData.MName;
		this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.AcceptDialog, new object[0]);
		ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.ESCORT, playerData.Level, playerData.Profession, 0);
	}

	// Token: 0x06003F16 RID: 16150 RVA: 0x0012415C File Offset: 0x0012235C
	public void ResetMissionUI(string missionId, NpcData npcData, MISSION_STATE type)
	{
		this.Reset();
		this.mCurMissionID = missionId;
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID == null)
		{
			return;
		}
		this.mCurMissionType = type;
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		if (this.mCurMissionType == MISSION_STATE.ACCEPTED)
		{
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, true);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.OkBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CancelBtn, false);
			this.TitleLabel.text = npcData.MName;
			this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.AcceptDialog, new object[0]);
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.MISSION, 1, SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession, 0);
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		}
		else if (this.mCurMissionType == MISSION_STATE.COMPLETE)
		{
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.OkBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, true);
			UnityVersionUtil.SetActiveRecursive(this.CancelBtn, false);
			this.TitleLabel.text = npcData.MName;
			this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.CompleteDialog, new object[0]);
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.MISSION, 1, SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession, 0);
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		}
	}

	// Token: 0x06003F17 RID: 16151 RVA: 0x00124308 File Offset: 0x00122508
	public void ResetNormalDialogUI(string dialogId, NpcData npcData)
	{
		this.Reset();
		NPCDialogData npcdialogDataByID = DataManager.GetNPCDialogDataByID(dialogId);
		if (npcdialogDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.OkBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CancelBtn, true);
			UnityVersionUtil.SetActiveRecursive(this.TitleLabel.gameObject, true);
			this.TitleLabel.text = npcData.MName;
			this.TextLabel.text = StrDictionary.GetDictionaryString(npcdialogDataByID.Dialog, new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItem.gameObject, false);
			this.NpcFakeObjRoot.EnableFakeObjRoot();
			this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		}
	}

	// Token: 0x06003F18 RID: 16152 RVA: 0x001243E8 File Offset: 0x001225E8
	public void ResetNormalDialogByStr(string str, NpcData npcData)
	{
		this.Reset();
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.OkBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.CancelBtn, true);
		UnityVersionUtil.SetActiveRecursive(this.TitleLabel.gameObject, true);
		this.TitleLabel.text = npcData.MName;
		this.TextLabel.text = StrDictionary.GetDictionaryString(str, new object[0]);
		UnityVersionUtil.SetActiveRecursive(this.ShowRewardItem.gameObject, false);
	}

	// Token: 0x06003F19 RID: 16153 RVA: 0x001244B4 File Offset: 0x001226B4
	public void ResetMissionTargetDialogUI(string missionId, NpcData npcData)
	{
		this.Reset();
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID != null)
		{
			this.NpcFakeObjRoot.EnableFakeObjRoot();
			this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
			this.mCurMissionID = missionId;
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.OkBtn, true);
			UnityVersionUtil.SetActiveRecursive(this.CancelBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.TitleLabel.gameObject, true);
			this.TitleLabel.text = npcData.MName;
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMultiDeliveryTargetData(missionId);
				this.TextLabel.text = curMultiDeliveryTargetData.Dialog;
			}
			else if (!string.IsNullOrEmpty(missionDataByID.TargetDialog))
			{
				this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.TargetDialog, new object[0]);
			}
			else
			{
				NPCDialogData npcdialogDataByID = DataManager.GetNPCDialogDataByID(npcData.TalkGroup);
				this.TextLabel.text = StrDictionary.GetDictionaryString(npcdialogDataByID.Dialog, new object[0]);
			}
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItem.gameObject, false);
		}
	}

	// Token: 0x06003F1A RID: 16154 RVA: 0x0012460C File Offset: 0x0012280C
	private void OnEnable()
	{
		Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
	}

	// Token: 0x06003F1B RID: 16155 RVA: 0x00124620 File Offset: 0x00122820
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
		this.NpcFakeObj.DisableNpcAnimaHandle();
	}

	// Token: 0x06003F1C RID: 16156 RVA: 0x00124660 File Offset: 0x00122860
	protected override void OnDestroy()
	{
		this.NpcFakeObj.DestroyNpcFakeObj();
		base.OnDestroy();
	}

	// Token: 0x04002AC2 RID: 10946
	public UILabel TitleLabel;

	// Token: 0x04002AC3 RID: 10947
	public UILabel TextLabel;

	// Token: 0x04002AC4 RID: 10948
	public GameObject AcceptBtn;

	// Token: 0x04002AC5 RID: 10949
	public GameObject CompleteBtn;

	// Token: 0x04002AC6 RID: 10950
	public GameObject CancelBtn;

	// Token: 0x04002AC7 RID: 10951
	public GameObject OkBtn;

	// Token: 0x04002AC8 RID: 10952
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002AC9 RID: 10953
	private string mCurMissionID = string.Empty;

	// Token: 0x04002ACA RID: 10954
	private MISSION_STATE mCurMissionType = MISSION_STATE.INVALID;

	// Token: 0x04002ACB RID: 10955
	public TeamFakeObjPicRootLogic NpcFakeObjRoot;

	// Token: 0x04002ACC RID: 10956
	public FakeObjLogic NpcFakeObj = new FakeObjLogic();

	// Token: 0x04002ACD RID: 10957
	public UITexture NpcPic;
}
