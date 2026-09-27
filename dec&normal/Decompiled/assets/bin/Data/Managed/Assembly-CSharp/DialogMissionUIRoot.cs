using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000901 RID: 2305
public class DialogMissionUIRoot : SingletonUnity<DialogMissionUIRoot>
{
	// Token: 0x06003EFF RID: 16127 RVA: 0x001232FC File Offset: 0x001214FC
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06003F00 RID: 16128 RVA: 0x00123304 File Offset: 0x00121504
	public void OnClickAcceptBtn()
	{
		this.CloseDialog();
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID.Class == 4 || missionDataByID.Class == 5)
		{
			GameManager gameManager = SingletonDontDestoryUnity<GameManager>.Instance;
			ActivityData activityData = gameManager.PlayerData.ActivityData;
			activity_info activityInfoByType;
			if (missionDataByID.Class == 4)
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
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.mCurMissionID);
				if (missionDataByID.Class == 5 && gameManager.PlayerData.MainPlayerAttrData.PkMode == 0)
				{
					MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{102015}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
					{
						request_change_pk_mode.request request = new request_change_pk_mode.request();
						request.pk = 1L;
						NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request, null);
						gameManager.PlayerData.SetPKModeState(1);
					}, null, null, null);
				}
			}
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.mCurMissionID);
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMainMissionCheck(DataManager.GetMissionDataByID(this.mCurMissionID));
			if (!string.IsNullOrEmpty(this.mCurNpcId) && (string.IsNullOrEmpty(missionDataByID.Target) || !missionDataByID.Target.Equals(this.mCurNpcId)))
			{
				SingletonUnity<UIManager>.Instance.CheckReShowUI(UIInfo.DialogMissionUI);
			}
		}
	}

	// Token: 0x06003F01 RID: 16129 RVA: 0x001234AC File Offset: 0x001216AC
	public void OnClickCompleteBtn()
	{
		this.CloseDialog();
		SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.CompleteMission(this.mCurMissionID);
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (string.IsNullOrEmpty(missionDataByID.NextID))
		{
			SingletonUnity<UIManager>.Instance.CheckReShowUI(UIInfo.DialogMissionUI);
		}
	}

	// Token: 0x06003F02 RID: 16130 RVA: 0x00123504 File Offset: 0x00121704
	public void OnClickCancelBtn()
	{
		this.CloseDialog();
		if (this.mCurMissionType == MISSION_STATE.ACCEPTED)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AcceptMission(this.mCurMissionID);
		}
	}

	// Token: 0x06003F03 RID: 16131 RVA: 0x00123530 File Offset: 0x00121730
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
		else
		{
			update_misison_complete.request request3 = new update_misison_complete.request();
			request3.missionId = this.mCurMissionID;
			NetLogic.GetInstance().Send<Protocol.update_misison_complete>(request3, null);
		}
	}

	// Token: 0x06003F04 RID: 16132 RVA: 0x00123610 File Offset: 0x00121810
	private void CloseDialog()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DialogMissionUI);
		Singleton<DialogManager>.Instance.OnCloseDialog();
	}

	// Token: 0x06003F05 RID: 16133 RVA: 0x0012362C File Offset: 0x0012182C
	public void Reset()
	{
		this.mCurMissionType = MISSION_STATE.INVALID;
		this.TitleLabel.text = string.Empty;
		this.TextLabel.text = string.Empty;
		this.MissionTitleLable.text = string.Empty;
		this.DefaultNpcPic.enabled = false;
		this.NpcPic.enabled = true;
		UnityVersionUtil.SetActiveRecursive(this.RewardLineObj, true);
		this.mCurNpcId = string.Empty;
	}

	// Token: 0x06003F06 RID: 16134 RVA: 0x001236A0 File Offset: 0x001218A0
	public void ResetActMissionAcceptUI(string missionId)
	{
		this.Reset();
		this.NpcPic.enabled = false;
		this.DefaultNpcPic.enabled = true;
		this.mCurMissionID = missionId;
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID == null)
		{
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, true);
		UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
		if (missionDataByID.Class == 8)
		{
			TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
			this.MissionTitleLable.text = timeLimitMissionDataByID.MName;
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100324}", new object[0]);
			this.TextLabel.text = timeLimitMissionDataByID.MDesc;
		}
		else
		{
			this.MissionTitleLable.text = StrDictionary.GetDictionaryString(missionDataByID.TipDescribeID, new object[0]);
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100324}", new object[0]);
			this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.DescribeID, new object[0]);
		}
		ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.MISSION, playerData.Level, playerData.Profession, 0);
	}

	// Token: 0x06003F07 RID: 16135 RVA: 0x001237D4 File Offset: 0x001219D4
	public void ResetSideMissionAcceptUI(string missionId, NpcData npcData)
	{
		this.Reset();
		this.mCurMissionID = missionId;
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID == null)
		{
			return;
		}
		this.mCurNpcId = npcData.ID;
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, true);
		UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
		if (missionDataByID.Class == 8)
		{
			TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
			this.MissionTitleLable.text = timeLimitMissionDataByID.MDesc;
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100324}", new object[0]);
			this.TextLabel.text = timeLimitMissionDataByID.MDesc;
		}
		else
		{
			this.TitleLabel.text = npcData.MName;
			this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.AcceptDialog, new object[0]);
		}
		ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.ESCORT, playerData.Level, playerData.Profession, 0);
	}

	// Token: 0x06003F08 RID: 16136 RVA: 0x00123914 File Offset: 0x00121B14
	public void ResetMissionUI(string missionId, NpcData npcData, MISSION_STATE type)
	{
		this.Reset();
		this.mCurMissionID = missionId;
		MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionID);
		if (missionDataByID == null)
		{
			return;
		}
		this.mCurNpcId = npcData.ID;
		this.mCurMissionType = type;
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		if (this.mCurMissionType == MISSION_STATE.ACCEPTED)
		{
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, true);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
			this.TitleLabel.text = npcData.MName;
			this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.AcceptDialog, new object[0]);
			this.MissionTitleLable.text = missionDataByID.MTipDescribeID;
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.MISSION, 1, SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession, 0);
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		}
		else if (this.mCurMissionType == MISSION_STATE.COMPLETE)
		{
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, true);
			this.TitleLabel.text = npcData.MName;
			this.MissionTitleLable.text = missionDataByID.MTipDescribeID;
			this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.CompleteDialog, new object[0]);
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, this.mCurMissionID, REWARD_TYPE.MISSION, 1, SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession, 0);
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
			if (missionDataByID.Class == 3)
			{
				UnityVersionUtil.SetActiveRecursive(this.RewardLineObj, false);
			}
		}
	}

	// Token: 0x06003F09 RID: 16137 RVA: 0x00123ADC File Offset: 0x00121CDC
	public void ResetNormalDialogUI(string dialogId, NpcData npcData)
	{
		this.Reset();
		NPCDialogData npcdialogDataByID = DataManager.GetNPCDialogDataByID(dialogId);
		if (npcdialogDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.TitleLabel.gameObject, true);
			this.mCurNpcId = npcData.ID;
			this.TitleLabel.text = npcData.MName;
			this.TextLabel.text = StrDictionary.GetDictionaryString(npcdialogDataByID.Dialog, new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItem.gameObject, false);
			this.NpcFakeObjRoot.EnableFakeObjRoot();
			this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		}
	}

	// Token: 0x06003F0A RID: 16138 RVA: 0x00123BB0 File Offset: 0x00121DB0
	public void ResetMissionTargetDialogUI(string missionId, NpcData npcData)
	{
		this.Reset();
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID != null)
		{
			this.NpcFakeObjRoot.EnableFakeObjRoot();
			this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
			this.mCurNpcId = npcData.ID;
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
			this.mCurMissionID = missionId;
			UnityVersionUtil.SetActiveRecursive(this.AcceptBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.CompleteBtn, false);
			UnityVersionUtil.SetActiveRecursive(this.TitleLabel.gameObject, true);
			this.TitleLabel.text = npcData.MName;
			if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.MULTI_DELIVERY)
			{
				MultiDeliveryMissionData curMultiDeliveryTargetData = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMultiDeliveryTargetData(missionId);
				this.TextLabel.text = curMultiDeliveryTargetData.Dialog;
			}
			else
			{
				this.TextLabel.text = StrDictionary.GetDictionaryString(missionDataByID.TargetDialog, new object[0]);
			}
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItem.gameObject, false);
		}
	}

	// Token: 0x06003F0B RID: 16139 RVA: 0x00123CBC File Offset: 0x00121EBC
	private void OnEnable()
	{
		Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
	}

	// Token: 0x06003F0C RID: 16140 RVA: 0x00123CD0 File Offset: 0x00121ED0
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
		this.NpcFakeObj.DisableNpcAnimaHandle();
	}

	// Token: 0x06003F0D RID: 16141 RVA: 0x00123D10 File Offset: 0x00121F10
	protected override void OnDestroy()
	{
		this.NpcFakeObj.DestroyNpcFakeObj();
		base.OnDestroy();
	}

	// Token: 0x04002AB3 RID: 10931
	public UILabel TitleLabel;

	// Token: 0x04002AB4 RID: 10932
	public UILabel TextLabel;

	// Token: 0x04002AB5 RID: 10933
	public GameObject AcceptBtn;

	// Token: 0x04002AB6 RID: 10934
	public GameObject CompleteBtn;

	// Token: 0x04002AB7 RID: 10935
	public UILabel CompleteLabel;

	// Token: 0x04002AB8 RID: 10936
	public GameObject RewardLineObj;

	// Token: 0x04002AB9 RID: 10937
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002ABA RID: 10938
	public UILabel MissionTitleLable;

	// Token: 0x04002ABB RID: 10939
	private string mCurMissionID = string.Empty;

	// Token: 0x04002ABC RID: 10940
	private MISSION_STATE mCurMissionType = MISSION_STATE.INVALID;

	// Token: 0x04002ABD RID: 10941
	public TeamFakeObjPicRootLogic NpcFakeObjRoot;

	// Token: 0x04002ABE RID: 10942
	public FakeObjLogic NpcFakeObj = new FakeObjLogic();

	// Token: 0x04002ABF RID: 10943
	public UITexture NpcPic;

	// Token: 0x04002AC0 RID: 10944
	public UITexture DefaultNpcPic;

	// Token: 0x04002AC1 RID: 10945
	private string mCurNpcId = string.Empty;
}
