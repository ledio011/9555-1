using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A32 RID: 2610
public class NewMissionUIRootLogic : SingletonUnity<NewMissionUIRootLogic>
{
	// Token: 0x06004C19 RID: 19481 RVA: 0x0019BF40 File Offset: 0x0019A140
	public void Reset(string targetMissionId)
	{
		this.curAcceptableMissionDataList.Clear();
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		this.allSideMissionList = DataManager.GetAllSideMissionList();
		if (this.allSideMissionList != null)
		{
			for (int i = 0; i < this.allSideMissionList.Count; i++)
			{
				if (missionManager.IsMissionAcceptable(this.allSideMissionList[i].ID))
				{
					this.curAcceptableMissionDataList.Add(this.allSideMissionList[i]);
				}
			}
		}
		this.curAcceptableMissionDataList.Sort((MissionData x, MissionData y) => x.ShowRank - y.ShowRank);
		this.curAcceptedMissionList.Clear();
		List<string> acceptedMissionList = this.GetAcceptedMissionList();
		for (int j = 0; j < acceptedMissionList.Count; j++)
		{
			this.curAcceptedMissionList.Add(DataManager.GetMissionDataByID(acceptedMissionList[j]));
		}
		int num = this.curAcceptableMissionDataList.Count + this.curAcceptedMissionList.Count - this.MissionLineList.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.MissionLineList[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D3}", this.MissionLineList.Count + 1);
				gameObject.transform.parent = this.MissionLineList[0].transform.parent;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				this.MissionLineList.Add(gameObject.GetComponent<NewMissionLineLogic>());
			}
		}
		int num2 = this.curAcceptableMissionDataList.Count + this.curAcceptedMissionList.Count;
		for (int l = 0; l < this.MissionLineList.Count; l++)
		{
			NGUITools.SetActive(this.MissionLineList[l].gameObject, false);
		}
		if (string.IsNullOrEmpty(targetMissionId))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.Reset(sceneManager.CurrentMapInofData.ID);
		}
		else
		{
			MISSION_STATE missionState = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetMissionState(targetMissionId);
			MissionData missionDataByID = DataManager.GetMissionDataByID(targetMissionId);
			string text = string.Empty;
			if (missionState == MISSION_STATE.INVALID)
			{
				text = missionDataByID.AcceptMapId;
			}
			else if (missionState == MISSION_STATE.ACCEPTED)
			{
				text = missionDataByID.TargetMapId;
			}
			else if (missionState == MISSION_STATE.COMPLETE)
			{
				text = missionDataByID.SubmitMapId;
			}
			if (string.IsNullOrEmpty(text))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
			}
			else
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(text);
			}
		}
		if (string.IsNullOrEmpty(targetMissionId))
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
		}
		this.mCurMissionLine = null;
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.DelayReposition());
		}
		this.mTargetMissionId = targetMissionId;
	}

	// Token: 0x06004C1A RID: 19482 RVA: 0x0019C280 File Offset: 0x0019A480
	public void ClickTargetMission(string missionId)
	{
		if (this.mCurMissionLine != null)
		{
			this.mCurMissionLine.CloseLine(true);
		}
		if (!string.IsNullOrEmpty(missionId))
		{
			for (int i = 0; i < this.MissionLineList.Count; i++)
			{
				if (UnityVersionUtil.IsActive(this.MissionLineList[i].gameObject) && this.MissionLineList[i].curMissionId.Equals(missionId))
				{
					this.MissionLineList[i].OnClickItemLine();
					this.MissionLineList[i].MyCenterOn.CenterOn(this.MissionLineList[i].transform);
				}
			}
		}
	}

	// Token: 0x06004C1B RID: 19483 RVA: 0x0019C340 File Offset: 0x0019A540
	private List<string> GetAcceptedMissionList()
	{
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		List<string> allMissionId = missionManager.GetAllMissionId();
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		for (int i = allMissionId.Count - 1; i >= 0; i--)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[i]);
			if (level < missionDataByID.DisplayLv)
			{
				allMissionId.RemoveAt(i);
			}
			else if (missionDataByID.Class == 1)
			{
				text = missionDataByID.ID;
			}
			else if (missionDataByID.Class == 0 || missionDataByID.Class == 3 || missionDataByID.Class == 6)
			{
				text2 = missionDataByID.ID;
			}
		}
		if (!string.IsNullOrEmpty(text2))
		{
			text3 = text2;
		}
		else if (!string.IsNullOrEmpty(text))
		{
			text3 = text;
		}
		allMissionId.Sort(delegate(string x, string y)
		{
			long missionChangeTime = missionManager.GetMissionChangeTime(x);
			long missionChangeTime2 = missionManager.GetMissionChangeTime(y);
			if (missionChangeTime != missionChangeTime2)
			{
				return (int)(missionChangeTime2 - missionChangeTime);
			}
			if (x.Length == y.Length)
			{
				return x.CompareTo(y);
			}
			return x.Length - y.Length;
		});
		List<string> list = new List<string>();
		for (int j = allMissionId.Count - 1; j >= 0; j--)
		{
			if (allMissionId[j].Equals(text) || allMissionId[j].Equals(text2))
			{
				allMissionId.RemoveAt(j);
			}
			else if (missionManager.GetMissionState(allMissionId[j]) == MISSION_STATE.COMPLETE)
			{
				list.Add(allMissionId[j]);
				allMissionId.RemoveAt(j);
			}
		}
		if (!string.IsNullOrEmpty(text2))
		{
			allMissionId.Insert(0, text2);
		}
		if (!string.IsNullOrEmpty(text))
		{
			allMissionId.Insert(0, text);
		}
		if (list.Count > 0)
		{
			int num;
			if (string.IsNullOrEmpty(text3))
			{
				num = 0;
			}
			else
			{
				num = allMissionId.IndexOf(text3) + 1;
			}
			for (int k = 0; k < list.Count; k++)
			{
				allMissionId.Insert(num, list[k]);
			}
		}
		return allMissionId;
	}

	// Token: 0x06004C1C RID: 19484 RVA: 0x0019C564 File Offset: 0x0019A764
	private IEnumerator DelayReposition()
	{
		yield return null;
		int allMisCount = this.curAcceptableMissionDataList.Count + this.curAcceptedMissionList.Count;
		for (int i = 0; i < this.MissionLineList.Count; i++)
		{
			if (i < allMisCount)
			{
				NGUITools.SetActive(this.MissionLineList[i].gameObject, true);
				if (i < this.curAcceptedMissionList.Count)
				{
					this.MissionLineList[i].ResetLine(this.curAcceptedMissionList[i], new NewMissionLineLogic.OnClickNewMissionLine(this.OnClickItemLine));
				}
				else
				{
					this.MissionLineList[i].ResetLine(this.curAcceptableMissionDataList[i - this.curAcceptedMissionList.Count], new NewMissionLineLogic.OnClickNewMissionLine(this.OnClickItemLine));
				}
			}
			else
			{
				NGUITools.SetActive(this.MissionLineList[i].gameObject, false);
			}
		}
		this.TableRoot.Reposition();
		this.ScrollView.ResetPosition();
		if (!string.IsNullOrEmpty(this.mTargetMissionId))
		{
			for (int j = 0; j < this.MissionLineList.Count; j++)
			{
				if (UnityVersionUtil.IsActive(this.MissionLineList[j].gameObject) && this.MissionLineList[j].curMissionId.Equals(this.mTargetMissionId))
				{
					this.MissionLineList[j].OnClickItemLine();
					this.MissionLineList[j].MyCenterOn.CenterOn(this.MissionLineList[j].transform);
				}
			}
		}
		this.MissionLineList[this.MissionLineList.Count - 1].InfoTween.updateTable = false;
		this.mTargetMissionId = string.Empty;
		yield break;
	}

	// Token: 0x06004C1D RID: 19485 RVA: 0x0019C580 File Offset: 0x0019A780
	public void OnClickItemLine(NewMissionLineLogic clickLine)
	{
		if (this.mCurMissionLine == clickLine)
		{
			this.mCurMissionLine = null;
			SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
			SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
		}
		else
		{
			if (this.mCurMissionLine != null)
			{
				this.mCurMissionLine.CloseLine(true);
			}
			this.mCurMissionLine = clickLine;
			this.mCurMissionLine.MyCenterOn.CenterOn(this.mCurMissionLine.transform);
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			if (missionManager.IsMissionAccepted(this.mCurMissionLine.curMissionId))
			{
				MISSION_STATE missionState = missionManager.GetMissionState(this.mCurMissionLine.CurMissionData.ID);
				if (missionState == MISSION_STATE.ACCEPTED)
				{
					if (this.mCurMissionLine.CurMissionData.MissionLogicType == MISSION_LOGICTYPE.SURVEY)
					{
						SurveyMissionData surveyMissionDataById = DataManager.GetSurveyMissionDataById(this.mCurMissionLine.CurMissionData.LogicID);
						SingletonUnity<NewMapUIRootLogic>.Instance.Reset(surveyMissionDataById.SceneID);
						SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(this.mCurMissionLine.curMissionId, true);
					}
					else if (string.IsNullOrEmpty(this.mCurMissionLine.CurMissionData.TargetMapId))
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
						SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
					}
					else
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.Reset(this.mCurMissionLine.CurMissionData.TargetMapId);
						SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(this.mCurMissionLine.curMissionId, true);
					}
				}
				else if (missionState == MISSION_STATE.COMPLETE)
				{
					if (string.IsNullOrEmpty(this.mCurMissionLine.CurMissionData.SubmitMapId))
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID);
						SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
					}
					else
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.Reset(this.mCurMissionLine.CurMissionData.SubmitMapId);
						SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(this.mCurMissionLine.curMissionId, true);
					}
				}
			}
			else
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.Reset(this.mCurMissionLine.CurMissionData.AcceptMapId);
				SingletonUnity<NewMapUIRootLogic>.Instance.ChooseActivityObj(this.mCurMissionLine.curMissionId, true);
			}
		}
	}

	// Token: 0x040039D5 RID: 14805
	public List<NewMissionLineLogic> MissionLineList = new List<NewMissionLineLogic>();

	// Token: 0x040039D6 RID: 14806
	public UITable TableRoot;

	// Token: 0x040039D7 RID: 14807
	public UIScrollView ScrollView;

	// Token: 0x040039D8 RID: 14808
	private List<MissionData> mCurMissionList = new List<MissionData>();

	// Token: 0x040039D9 RID: 14809
	private List<MissionData> allSideMissionList = new List<MissionData>();

	// Token: 0x040039DA RID: 14810
	private List<MissionData> curAcceptableMissionDataList = new List<MissionData>();

	// Token: 0x040039DB RID: 14811
	private List<MissionData> curAcceptedMissionList = new List<MissionData>();

	// Token: 0x040039DC RID: 14812
	private string mTargetMissionId = string.Empty;

	// Token: 0x040039DD RID: 14813
	private NewMissionLineLogic mCurMissionLine;
}
