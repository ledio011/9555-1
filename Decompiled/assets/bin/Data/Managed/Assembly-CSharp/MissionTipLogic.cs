using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A07 RID: 2567
public class MissionTipLogic : MonoBehaviour
{
	// Token: 0x06004995 RID: 18837 RVA: 0x0017C788 File Offset: 0x0017A988
	public void CheckMainMissionHandTip()
	{
		this.CloseHandTip();
	}

	// Token: 0x06004996 RID: 18838 RVA: 0x0017C79C File Offset: 0x0017A99C
	public void CloseHandTip()
	{
		NGUITools.SetActive(this.HandTipRoot, false);
		this.lastShowHandTipTime = Time.time;
	}

	// Token: 0x06004997 RID: 18839 RVA: 0x0017C7B8 File Offset: 0x0017A9B8
	public void ShowMainMissionTip()
	{
		if (TutorialManager.CurStep != TUTORIAL_STEP.MAIN_MISSION_START)
		{
			if ((!SingletonUnity<TutorialUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject)) && !FunctionTipsRootLogic.IsHandTipEnable())
			{
				MissionTipLineLogic targetMissionLine = this.GetTargetMissionLine(1);
				if (targetMissionLine != null)
				{
					this.lastShowHandTipTime = Time.time;
					NGUITools.SetActive(this.HandTipRoot, true);
					this.HandTipRoot.transform.position = targetMissionLine.transform.position - (Vector3.up * 30f - Vector3.right * 50f) * targetMissionLine.transform.lossyScale.y;
				}
				else
				{
					NGUITools.SetActive(this.HandTipRoot, false);
				}
			}
			else
			{
				NGUITools.SetActive(this.HandTipRoot, false);
			}
		}
	}

	// Token: 0x06004998 RID: 18840 RVA: 0x0017C8A4 File Offset: 0x0017AAA4
	public void ShowSideMissionTip(string misId)
	{
		if (TutorialManager.CurStep != TUTORIAL_STEP.MAIN_MISSION_START)
		{
			if ((!SingletonUnity<TutorialUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject)) && !FunctionTipsRootLogic.IsHandTipEnable())
			{
				MissionTipLineLogic targetMissionLineById = this.GetTargetMissionLineById(misId);
				if (targetMissionLineById != null)
				{
					this.lastShowHandTipTime = Time.time;
					NGUITools.SetActive(this.HandTipRoot, true);
					this.HandTipRoot.transform.position = targetMissionLineById.transform.position - (Vector3.up * 30f - Vector3.right * 50f) * targetMissionLineById.transform.lossyScale.y;
				}
				else
				{
					NGUITools.SetActive(this.HandTipRoot, false);
				}
			}
			else
			{
				NGUITools.SetActive(this.HandTipRoot, false);
			}
		}
	}

	// Token: 0x06004999 RID: 18841 RVA: 0x0017C990 File Offset: 0x0017AB90
	public void UpdateLastShowHandTipTime()
	{
		this.lastShowHandTipTime = Time.time;
	}

	// Token: 0x0600499A RID: 18842 RVA: 0x0017C9A0 File Offset: 0x0017ABA0
	private void Update()
	{
		if (Time.time - this.lastShowHandTipTime > this.CheckHandTipInterval)
		{
			this.lastShowHandTipTime = Time.time;
			if (UnityVersionUtil.IsActive(this.HandTipRoot))
			{
				this.CloseHandTip();
			}
		}
	}

	// Token: 0x0600499B RID: 18843 RVA: 0x0017C9E8 File Offset: 0x0017ABE8
	private void Awake()
	{
		this.uiWrapContent.onInitializeItem = new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem);
	}

	// Token: 0x0600499C RID: 18844 RVA: 0x0017CA04 File Offset: 0x0017AC04
	public void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		realIndex = Mathf.Abs(realIndex);
		if (this.mCurMissionIdList.Count > realIndex)
		{
			this.MissionTipLineList[index].Reset(this.mCurMissionIdList[Mathf.Abs(realIndex)]);
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.DAILY_MISSION_CLICK_START)
		{
			FunctionTipsRootLogic.ClearHandTip();
		}
	}

	// Token: 0x0600499D RID: 18845 RVA: 0x0017CA60 File Offset: 0x0017AC60
	public void Reset()
	{
		SingletonUnity<MissionTeamTipLogic>.Instance.TipScrollView.ResetPosition();
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		this.mCurMissionIdList = missionManager.GetAllMissionId();
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		for (int i = this.mCurMissionIdList.Count - 1; i >= 0; i--)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurMissionIdList[i]);
			if (level < missionDataByID.DisplayLv)
			{
				this.mCurMissionIdList.RemoveAt(i);
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
		this.BottomWidget.height = this.LineHeight * this.mCurMissionIdList.Count;
		int num = this.mCurMissionIdList.Count - this.MissionTipLineList.Count;
		for (int j = 0; j < this.MissionTipLineList.Count; j++)
		{
			NGUITools.SetActive(this.MissionTipLineList[j].gameObject, true);
		}
		if (num < 0)
		{
			for (int k = 0; k > num; k--)
			{
				NGUITools.SetActive(this.MissionTipLineList[this.MissionTipLineList.Count + k - 1].gameObject, false);
			}
		}
		this.mCurMissionIdList.Sort(delegate(string x, string y)
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
		for (int l = this.mCurMissionIdList.Count - 1; l >= 0; l--)
		{
			if (this.mCurMissionIdList[l].Equals(text) || this.mCurMissionIdList[l].Equals(text2))
			{
				this.mCurMissionIdList.RemoveAt(l);
			}
			else if (missionManager.GetMissionState(this.mCurMissionIdList[l]) == MISSION_STATE.COMPLETE)
			{
				list.Add(this.mCurMissionIdList[l]);
				this.mCurMissionIdList.RemoveAt(l);
			}
		}
		if (!string.IsNullOrEmpty(text2))
		{
			this.mCurMissionIdList.Insert(0, text2);
		}
		if (!string.IsNullOrEmpty(text))
		{
			this.mCurMissionIdList.Insert(0, text);
		}
		if (list.Count > 0)
		{
			int num2;
			if (string.IsNullOrEmpty(text3))
			{
				num2 = 0;
			}
			else
			{
				num2 = this.mCurMissionIdList.IndexOf(text3) + 1;
			}
			for (int m = 0; m < list.Count; m++)
			{
				this.mCurMissionIdList.Insert(num2, list[m]);
			}
		}
		this.uiWrapContent.minIndex = -(this.mCurMissionIdList.Count - 1);
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.CheckMainMissionHandTip();
	}

	// Token: 0x0600499E RID: 18846 RVA: 0x0017CDBC File Offset: 0x0017AFBC
	public void UpdateMission(string missionId)
	{
		for (int i = 0; i < this.MissionTipLineList.Count; i++)
		{
			if (this.MissionTipLineList[i].MissionId.Equals(missionId))
			{
				this.MissionTipLineList[i].Reset(missionId);
				return;
			}
		}
	}

	// Token: 0x0600499F RID: 18847 RVA: 0x0017CE14 File Offset: 0x0017B014
	public MissionTipLineLogic GetDailyMissionLine()
	{
		for (int i = 0; i < this.MissionTipLineList.Count; i++)
		{
			if (!string.IsNullOrEmpty(this.MissionTipLineList[i].MissionId))
			{
				if (UnityVersionUtil.IsActive(this.MissionTipLineList[i].gameObject))
				{
					MissionData missionDataByID = DataManager.GetMissionDataByID(this.MissionTipLineList[i].MissionId);
					if (missionDataByID.Class == 0 || missionDataByID.Class == 3 || missionDataByID.Class == 6)
					{
						return this.MissionTipLineList[i];
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060049A0 RID: 18848 RVA: 0x0017CEC8 File Offset: 0x0017B0C8
	public MissionTipLineLogic GetTargetMissionLine(int missionClass)
	{
		for (int i = 0; i < this.MissionTipLineList.Count; i++)
		{
			if (!string.IsNullOrEmpty(this.MissionTipLineList[i].MissionId))
			{
				if (UnityVersionUtil.IsActive(this.MissionTipLineList[i].gameObject))
				{
					MissionData missionDataByID = DataManager.GetMissionDataByID(this.MissionTipLineList[i].MissionId);
					if (missionDataByID.Class == missionClass)
					{
						return this.MissionTipLineList[i];
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060049A1 RID: 18849 RVA: 0x0017CF64 File Offset: 0x0017B164
	public MissionTipLineLogic GetTargetMissionLineById(string misId)
	{
		for (int i = 0; i < this.MissionTipLineList.Count; i++)
		{
			if (!string.IsNullOrEmpty(this.MissionTipLineList[i].MissionId))
			{
				if (UnityVersionUtil.IsActive(this.MissionTipLineList[i].gameObject))
				{
					if (this.MissionTipLineList[i].MissionId.Equals(misId))
					{
						return this.MissionTipLineList[i];
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060049A2 RID: 18850 RVA: 0x0017CFF8 File Offset: 0x0017B1F8
	public bool IsHaveMainLineMission()
	{
		return !(this.GetTargetMissionLine(1) == null);
	}

	// Token: 0x060049A3 RID: 18851 RVA: 0x0017D010 File Offset: 0x0017B210
	public bool IsHaveDailyMission()
	{
		return !(this.GetDailyMissionLine() == null);
	}

	// Token: 0x060049A4 RID: 18852 RVA: 0x0017D028 File Offset: 0x0017B228
	private void OnReshowBase()
	{
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.SetPanelDirty());
		}
	}

	// Token: 0x060049A5 RID: 18853 RVA: 0x0017D048 File Offset: 0x0017B248
	private IEnumerator SetPanelDirty()
	{
		yield return null;
		this.TipPanel.SetDirty();
		yield break;
	}

	// Token: 0x060049A6 RID: 18854 RVA: 0x0017D064 File Offset: 0x0017B264
	private void OnEnable()
	{
		UIUpdateEvent.OnReshowBase = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.OnReshowBase, new UIUpdateEvent.UpdateNoParamEvent(this.OnReshowBase));
	}

	// Token: 0x060049A7 RID: 18855 RVA: 0x0017D094 File Offset: 0x0017B294
	private void OnDisable()
	{
		UIUpdateEvent.OnReshowBase = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.OnReshowBase, new UIUpdateEvent.UpdateNoParamEvent(this.OnReshowBase));
	}

	// Token: 0x040036D6 RID: 14038
	public int LineHeight = 56;

	// Token: 0x040036D7 RID: 14039
	public MissionTipLineLogic TipLinePrefab;

	// Token: 0x040036D8 RID: 14040
	public List<MissionTipLineLogic> MissionTipLineList;

	// Token: 0x040036D9 RID: 14041
	private List<string> mCurMissionIdList = new List<string>();

	// Token: 0x040036DA RID: 14042
	public UIWrapContentNew uiWrapContent;

	// Token: 0x040036DB RID: 14043
	public UIWidget BottomWidget;

	// Token: 0x040036DC RID: 14044
	public GameObject HandTipRoot;

	// Token: 0x040036DD RID: 14045
	public UIPanel TipPanel;

	// Token: 0x040036DE RID: 14046
	private float lastShowHandTipTime = -9f;

	// Token: 0x040036DF RID: 14047
	private int missionTipMaxId = 18;

	// Token: 0x040036E0 RID: 14048
	private int NewMissionminId = 1000;

	// Token: 0x040036E1 RID: 14049
	private int NewMissionmaxId = 2000;

	// Token: 0x040036E2 RID: 14050
	private float CheckHandTipInterval = 10f;

	// Token: 0x040036E3 RID: 14051
	private MissionManager misManager;

	// Token: 0x040036E4 RID: 14052
	private bool flag;
}
