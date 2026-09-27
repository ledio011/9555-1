using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A4F RID: 2639
public class FunctionTipsRootLogic : SingletonUnity<FunctionTipsRootLogic>
{
	// Token: 0x06004CE4 RID: 19684 RVA: 0x001A173C File Offset: 0x0019F93C
	private new void Awake()
	{
		base.Awake();
		NGUITools.SetActive(this.HandTipsObj.gameObject, false);
		NGUITools.SetActive(this.TipsObj.gameObject, false);
		for (int i = 0; i < this.DisableTipCircleData.Count; i++)
		{
			NGUITools.SetActive(this.DisableTipCircleData[i].gameObject, false);
		}
		for (int j = 0; j < this.DisableHandTipCircleData.Count; j++)
		{
			NGUITools.SetActive(this.DisableHandTipCircleData[j].gameObject, false);
		}
		if (Camera.main != null)
		{
			this.mainCamera = Camera.main;
		}
	}

	// Token: 0x06004CE5 RID: 19685 RVA: 0x001A17F4 File Offset: 0x0019F9F4
	public static void AddFunctionTips(GameObject targetObj, Vector3 offset, float duration = -1f)
	{
		if (SingletonUnity<FunctionTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionTipsRootLogic>.Instance.AddTips(targetObj, offset, duration);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FunctionTipsRoot, delegate
			{
				SingletonUnity<FunctionTipsRootLogic>.Instance.AddTips(targetObj, offset, duration);
			}, null);
		}
	}

	// Token: 0x06004CE6 RID: 19686 RVA: 0x001A1878 File Offset: 0x0019FA78
	public static void AddFunctionHandTips(GameObject targetObj, Vector3 offset, TUTORIAL_STEP step, bool isChild, string tips, SCREEN_DIRECTION tipPos, Vector3 tipOffset, float duration = -1f, Transform parentObj = null, bool isWorldObj = false, bool isShowHand = true)
	{
		if (SingletonUnity<FunctionTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionTipsRootLogic>.Instance.AddHandTips(targetObj, offset, step, isChild, tips, tipPos, tipOffset, duration, parentObj, isWorldObj, isShowHand);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FunctionTipsRoot, delegate
			{
				SingletonUnity<FunctionTipsRootLogic>.Instance.AddHandTips(targetObj, offset, step, isChild, tips, tipPos, tipOffset, duration, parentObj, isWorldObj, isShowHand);
			}, null);
		}
	}

	// Token: 0x06004CE7 RID: 19687 RVA: 0x001A196C File Offset: 0x0019FB6C
	public static bool IsHandTipEnable()
	{
		return SingletonUnity<FunctionTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionTipsRootLogic>.Instance.gameObject) && SingletonUnity<FunctionTipsRootLogic>.Instance.EnableHandTipCircleData.Count > 0;
	}

	// Token: 0x06004CE8 RID: 19688 RVA: 0x001A19B4 File Offset: 0x0019FBB4
	public static void RemoveFunctionTips(GameObject targetObj)
	{
		if (SingletonUnity<FunctionTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionTipsRootLogic>.Instance.RemoveTips(targetObj);
		}
	}

	// Token: 0x06004CE9 RID: 19689 RVA: 0x001A19EC File Offset: 0x0019FBEC
	public static void ClearHandTip()
	{
		if (SingletonUnity<FunctionTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionTipsRootLogic>.Instance.ClearHandTips();
		}
	}

	// Token: 0x06004CEA RID: 19690 RVA: 0x001A1A24 File Offset: 0x0019FC24
	public void AddTips(GameObject targetObj, Vector3 offset, float duration = -1f)
	{
		for (int i = 0; i < this.EnableTipCircleData.Count; i++)
		{
			if (targetObj == this.EnableTipCircleData[i].TargetObj)
			{
				if (duration > 0f)
				{
					this.EnableTipCircleData[i].TargetTime = Time.time + duration;
				}
				return;
			}
		}
		TipObj tipObj = null;
		if (this.DisableTipCircleData.Count > 0)
		{
			tipObj = this.DisableTipCircleData[0];
			this.DisableTipCircleData.RemoveAt(0);
		}
		if (tipObj == null || tipObj.gameObject == null)
		{
			GameObject gameObject = Object.Instantiate(this.TipsObj.gameObject) as GameObject;
			tipObj = gameObject.GetComponent<TipObj>();
			gameObject.transform.parent = this.HandTipsObj.transform.parent;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
		}
		tipObj.Reset(targetObj, offset, TUTORIAL_STEP.INVALID, duration, true, null, false);
		this.EnableTipCircleData.Add(tipObj);
	}

	// Token: 0x06004CEB RID: 19691 RVA: 0x001A1B48 File Offset: 0x0019FD48
	public bool IsHaveTipsCircle(GameObject checktarget)
	{
		for (int i = 0; i < this.EnableTipCircleData.Count; i++)
		{
			if (checktarget == this.EnableTipCircleData[i].TargetObj)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004CEC RID: 19692 RVA: 0x001A1B90 File Offset: 0x0019FD90
	public void AddHandTips(GameObject targetObj, Vector3 offset, TUTORIAL_STEP step, bool isChild, string tips, SCREEN_DIRECTION tipPos, Vector3 tipOffset, float duration = -1f, Transform parentObj = null, bool isWorldObj = false, bool isShowHand = true)
	{
		if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.CloseHandTip();
		}
		for (int i = 0; i < this.EnableHandTipCircleData.Count; i++)
		{
			if (targetObj == this.EnableHandTipCircleData[i].TargetObj)
			{
				if (duration > 0f)
				{
					this.EnableHandTipCircleData[i].TargetTime = Time.time + duration;
				}
				return;
			}
		}
		TipObj tipObj = null;
		if (this.DisableHandTipCircleData.Count > 0)
		{
			tipObj = this.DisableHandTipCircleData[0];
			this.DisableHandTipCircleData.RemoveAt(0);
		}
		if (tipObj == null || tipObj.gameObject == null)
		{
			GameObject gameObject = Object.Instantiate(this.HandTipsObj.gameObject) as GameObject;
			tipObj = gameObject.GetComponent<TipObj>();
			gameObject.transform.parent = this.HandTipsObj.transform.parent;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
		}
		tipObj.Reset(targetObj, offset, tips, tipPos, tipOffset, step, duration, isChild, parentObj, isWorldObj, isShowHand);
		tipObj.transform.localScale = Vector3.one;
		this.EnableHandTipCircleData.Add(tipObj);
	}

	// Token: 0x06004CED RID: 19693 RVA: 0x001A1D00 File Offset: 0x0019FF00
	public void ClearHandTips()
	{
		if (this.EnableHandTipCircleData.Count > 0)
		{
			for (int i = this.EnableHandTipCircleData.Count - 1; i >= 0; i--)
			{
				this.RemoveTips(this.EnableHandTipCircleData[i]);
			}
		}
	}

	// Token: 0x06004CEE RID: 19694 RVA: 0x001A1D50 File Offset: 0x0019FF50
	public void RemoveTips(TipObj tipData)
	{
		tipData.transform.parent = base.transform;
		tipData.transform.localPosition = Vector3.zero;
		NGUITools.SetActive(tipData.gameObject, false);
		tipData.TargetObj = null;
		tipData.TipText = string.Empty;
		if (this.EnableTipCircleData.Contains(tipData))
		{
			this.EnableTipCircleData.Remove(tipData);
			if (tipData != null && tipData.gameObject != null)
			{
				this.DisableTipCircleData.Add(tipData);
			}
		}
		else if (this.EnableHandTipCircleData.Contains(tipData))
		{
			this.EnableHandTipCircleData.Remove(tipData);
			if (tipData != null && tipData.gameObject != null)
			{
				this.DisableHandTipCircleData.Add(tipData);
			}
		}
		TutorialManager.ClearTutorialEvent(tipData.TutorialStep);
	}

	// Token: 0x06004CEF RID: 19695 RVA: 0x001A1E3C File Offset: 0x001A003C
	public void RemoveTips(GameObject targetObj)
	{
		TipObj tipObj = null;
		for (int i = this.EnableTipCircleData.Count - 1; i >= 0; i--)
		{
			if (this.EnableTipCircleData[i] == null)
			{
				this.EnableTipCircleData.RemoveAt(i);
			}
			else if (this.EnableTipCircleData[i].TargetObj == null || this.EnableTipCircleData[i].TargetObj == targetObj)
			{
				tipObj = this.EnableTipCircleData[i];
				this.EnableTipCircleData.RemoveAt(i);
				this.DisableTipCircleData.Add(tipObj);
				tipObj.transform.parent = base.transform;
				tipObj.transform.localPosition = Vector3.zero;
				NGUITools.SetActive(tipObj.gameObject, false);
				if (tipObj.TempRoot != null)
				{
					GameObject tempRoot = tipObj.TempRoot;
					tipObj.TempRoot = null;
					Object.Destroy(tempRoot);
				}
			}
		}
		for (int j = this.EnableHandTipCircleData.Count - 1; j >= 0; j--)
		{
			if (this.EnableHandTipCircleData[j] == null)
			{
				this.EnableHandTipCircleData.RemoveAt(j);
			}
			else if (this.EnableHandTipCircleData[j].TargetObj == null || this.EnableHandTipCircleData[j].TargetObj == targetObj)
			{
				tipObj = this.EnableHandTipCircleData[j];
				this.EnableHandTipCircleData.RemoveAt(j);
				this.DisableHandTipCircleData.Add(tipObj);
				tipObj.transform.parent = base.transform;
				tipObj.transform.localPosition = Vector3.zero;
				NGUITools.SetActive(tipObj.gameObject, false);
				tipObj.TipText = string.Empty;
				if (tipObj.TempRoot != null)
				{
					GameObject tempRoot2 = tipObj.TempRoot;
					tipObj.TempRoot = null;
					Object.Destroy(tempRoot2);
				}
			}
		}
		if (tipObj != null)
		{
			TutorialManager.ClearTutorialEvent(tipObj.TutorialStep);
		}
	}

	// Token: 0x06004CF0 RID: 19696 RVA: 0x001A205C File Offset: 0x001A025C
	private void Update()
	{
		for (int i = this.EnableTipCircleData.Count - 1; i >= 0; i--)
		{
			if (this.EnableTipCircleData[i].Duration > 0f && Time.time > this.EnableTipCircleData[i].TargetTime)
			{
				this.RemoveTips(this.EnableTipCircleData[i]);
			}
		}
		for (int j = this.EnableHandTipCircleData.Count - 1; j >= 0; j--)
		{
			if (!this.EnableHandTipCircleData[j].IsWorldObj)
			{
				if (!this.EnableHandTipCircleData[j].IsChild)
				{
					if (UnityVersionUtil.IsActive(this.EnableHandTipCircleData[j].gameObject) != UnityVersionUtil.IsActive(this.EnableHandTipCircleData[j].TargetObj))
					{
						UnityVersionUtil.SetActiveRecursive(this.EnableHandTipCircleData[j].gameObject, UnityVersionUtil.IsActive(this.EnableHandTipCircleData[j].TargetObj));
					}
					this.EnableHandTipCircleData[j].transform.position = this.EnableHandTipCircleData[j].TargetObj.transform.position + this.EnableHandTipCircleData[j].OffsetPos;
				}
			}
			else if (this.mainCamera != null)
			{
				Vector3 vector = this.mainCamera.WorldToViewportPoint(this.EnableHandTipCircleData[j].TargetObj.transform.position + Vector3.up * 1.1f);
				float num = (float)Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
				Vector3 vector2;
				vector2..ctor(vector.x * num - num / 2f, vector.y * 480f - 240f, 0f);
				this.EnableHandTipCircleData[j].transform.localPosition = vector2 + this.EnableHandTipCircleData[j].OffsetPos;
			}
			if (this.EnableHandTipCircleData[j].Duration > 0f && Time.time > this.EnableHandTipCircleData[j].TargetTime)
			{
				this.RemoveTips(this.EnableHandTipCircleData[j]);
			}
		}
	}

	// Token: 0x04003A7E RID: 14974
	public TipObj TipsObj;

	// Token: 0x04003A7F RID: 14975
	public TipObj HandTipsObj;

	// Token: 0x04003A80 RID: 14976
	public List<TipObj> EnableTipCircleData = new List<TipObj>();

	// Token: 0x04003A81 RID: 14977
	public List<TipObj> DisableTipCircleData = new List<TipObj>();

	// Token: 0x04003A82 RID: 14978
	public List<TipObj> EnableHandTipCircleData = new List<TipObj>();

	// Token: 0x04003A83 RID: 14979
	public List<TipObj> DisableHandTipCircleData = new List<TipObj>();

	// Token: 0x04003A84 RID: 14980
	public Transform CenterObj;

	// Token: 0x04003A85 RID: 14981
	private Camera mainCamera;
}
