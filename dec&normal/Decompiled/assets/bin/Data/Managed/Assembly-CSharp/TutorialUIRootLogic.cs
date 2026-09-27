using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A55 RID: 2645
public class TutorialUIRootLogic : SingletonUnity<TutorialUIRootLogic>
{
	// Token: 0x06004D1E RID: 19742 RVA: 0x001A3D2C File Offset: 0x001A1F2C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent, float waitTime = -1f)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
		this.mTutorialStartTime = Time.realtimeSinceStartup;
		this.mTutorialWaitTime = waitTime;
	}

	// Token: 0x06004D1F RID: 19743 RVA: 0x001A3D48 File Offset: 0x001A1F48
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null && Time.realtimeSinceStartup - this.mTutorialStartTime > this.mTutorialWaitTime)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004D20 RID: 19744 RVA: 0x001A3D8C File Offset: 0x001A1F8C
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06004D21 RID: 19745 RVA: 0x001A3D98 File Offset: 0x001A1F98
	public static void ShowWindow(GameObject centerObj, int width, int height, string tipText, SCREEN_DIRECTION tipTextPos, float duration, bool isTipCircleEnable = false, bool isMaskEnable = false, bool isMaskShow = true, UIWidget.Pivot pivot = UIWidget.Pivot.Center)
	{
		if (centerObj == null)
		{
			Debug.Log("Center Obj == null");
			return;
		}
		TutorialUIRootLogic.TutorialUIInfo param = new TutorialUIRootLogic.TutorialUIInfo(centerObj, width, height, tipText, tipTextPos, duration, isTipCircleEnable, isMaskEnable, isMaskShow, pivot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TutorialUIRoot, new UIManager.OnOpenUIDelegate(TutorialUIRootLogic.OnTutorialUIRootOpen), param);
	}

	// Token: 0x06004D22 RID: 19746 RVA: 0x001A3DF0 File Offset: 0x001A1FF0
	public static void ShowWindow(Vector3 centerPos, int width, int height, string tipText, SCREEN_DIRECTION tipTextPos, bool isTipCircleEnable = false, bool isMaskEnable = false, bool isMaskShow = true)
	{
		TutorialUIRootLogic.TutorialUIInfo param = new TutorialUIRootLogic.TutorialUIInfo(centerPos, width, height, tipText, tipTextPos, isTipCircleEnable, isMaskEnable, isMaskShow, UIWidget.Pivot.Top);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TutorialUIRoot, new UIManager.OnOpenUIDelegate(TutorialUIRootLogic.OnTutorialUIRootOpen), param);
	}

	// Token: 0x06004D23 RID: 19747 RVA: 0x001A3E2C File Offset: 0x001A202C
	public static void ShowWindow(GameObject centerObj, List<UIWidget> showObjList, int width, int height, string tipText, SCREEN_DIRECTION tipTextPos, bool isTipCircleEnable = false, bool isMaskEnable = false, bool isMaskShow = true, bool isShowHand = true, bool isShowCircle = true)
	{
		TutorialUIRootLogic.TutorialUIChangePannelInfo param = new TutorialUIRootLogic.TutorialUIChangePannelInfo(centerObj, showObjList, width, height, tipText, tipTextPos, isTipCircleEnable, isMaskEnable, isMaskShow, isShowHand, isShowCircle);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TutorialUIRoot, new UIManager.OnOpenUIDelegate(TutorialUIRootLogic.OnTutorialUIRootOpenChangePannel), param);
	}

	// Token: 0x06004D24 RID: 19748 RVA: 0x001A3E70 File Offset: 0x001A2070
	private static void OnTutorialUIRootOpenChangePannel(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			TutorialUIRootLogic.TutorialUIChangePannelInfo tutorialUIChangePannelInfo = param as TutorialUIRootLogic.TutorialUIChangePannelInfo;
			if (tutorialUIChangePannelInfo != null && SingletonUnity<TutorialUIRootLogic>.Exists)
			{
				SingletonUnity<TutorialUIRootLogic>.Instance.Reset(tutorialUIChangePannelInfo);
			}
		}
	}

	// Token: 0x06004D25 RID: 19749 RVA: 0x001A3EA8 File Offset: 0x001A20A8
	private static void OnTutorialUIRootOpen(bool isSuccess, object param)
	{
		if (isSuccess)
		{
			TutorialUIRootLogic.TutorialUIInfo tutorialUIInfo = param as TutorialUIRootLogic.TutorialUIInfo;
			if (tutorialUIInfo != null && SingletonUnity<TutorialUIRootLogic>.Exists)
			{
				SingletonUnity<TutorialUIRootLogic>.Instance.Reset(tutorialUIInfo);
			}
		}
	}

	// Token: 0x06004D26 RID: 19750 RVA: 0x001A3EE0 File Offset: 0x001A20E0
	private IEnumerator DelayReset()
	{
		yield return null;
		UIAnchor targetAnchor = NGUITools.FindInParents<UIAnchor>(this.info.CenterObj);
		if (targetAnchor != null)
		{
			this.AnchorRoot.side = targetAnchor.side;
		}
		else
		{
			this.AnchorRoot.side = UIAnchor.Side.Center;
		}
		this.AnchorRoot.ScreenSizeChanged();
		this.ChangePannelFlag = true;
		this.HandTipTweenS.enabled = true;
		this.HandTipTweenS.transform.localPosition = Vector3.zero;
		UnityVersionUtil.SetActiveRecursive(this.RootOffsetTrans.gameObject, true);
		this.RootOffsetTrans.transform.position = this.info.CenterObj.transform.position;
		this.mHighLightObjList = this.info.HighLightObjList;
		if (this.mHighLightObjList != null && this.mHighLightObjList.Count != 0)
		{
			for (int i = 0; i < this.mHighLightObjList.Count; i++)
			{
				this.mPreTransParentList.Add(this.mHighLightObjList[i].transform.parent);
				this.mPreLocalPosition.Add(this.mHighLightObjList[i].transform.localPosition);
				this.mHighLightObjList[i].transform.parent = this.HightLightObjRoot;
				NGUITools.MarkParentAsChanged(this.mHighLightObjList[i].gameObject);
			}
		}
		if (!this.info.IsTipCircleEnable)
		{
			UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.HandTipTweenS.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.HandTipTweenS.gameObject, true);
		}
		if (!this.info.IsMaskEnable)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.RightMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TopMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.BottomMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CenterMaskObj.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.RightMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TopMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.BottomMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CenterMaskObj.gameObject, true);
			if (this.info.IsMaskShow)
			{
				this.CenterMaskObj.alpha = 0.4f;
			}
			else
			{
				this.CenterMaskObj.alpha = 0.01f;
			}
		}
		if (string.IsNullOrEmpty(this.info.TipText))
		{
			UnityVersionUtil.SetActiveRecursive(this.TipTextLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TipTextBottomSprite.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TipTextLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.TipTextBottomSprite.gameObject, true);
			this.TipTextLabel.text = this.info.TipText;
			this.TipTextBottomSprite.width = this.TipTextLabel.width + 84;
			this.TipTextBottomSprite.height = this.TipTextLabel.height + 20;
			int width = this.TipTextBottomSprite.width;
			int height = this.TipTextBottomSprite.height;
			int XoffSet = 0;
			int YoffSet = 0;
			int Distance = 20;
			switch (this.info.TipTextPos)
			{
			case SCREEN_DIRECTION.TOP:
				YoffSet = Distance + height / 2 + this.info.Height / 2;
				break;
			case SCREEN_DIRECTION.BOTTOM:
				YoffSet = -(Distance + height / 2 + this.info.Height / 2);
				break;
			case SCREEN_DIRECTION.LEFT:
				XoffSet = -(Distance + width / 2 + this.info.Width / 2);
				break;
			case SCREEN_DIRECTION.RIGHT:
				XoffSet = Distance + width / 2 + this.info.Width / 2;
				break;
			case SCREEN_DIRECTION.TOP_LEFT:
				YoffSet = Distance + height / 2 + this.info.Height / 2;
				XoffSet = -(Distance + width / 2 + this.info.Width / 2);
				break;
			case SCREEN_DIRECTION.BOTTOM_LEFT:
				YoffSet = -(Distance + height / 2 + this.info.Height / 2);
				XoffSet = -(Distance + width / 2 + this.info.Width / 2);
				break;
			case SCREEN_DIRECTION.TOP_RIGHT:
				YoffSet = Distance + height / 2 + this.info.Height / 2;
				XoffSet = Distance + width / 2 + this.info.Width / 2;
				break;
			case SCREEN_DIRECTION.BOTTOM_RIGHT:
				YoffSet = -(Distance + height / 2 + this.info.Height / 2);
				XoffSet = Distance + width / 2 + this.info.Width / 2;
				break;
			}
			this.TipTextBottomSprite.transform.localPosition = new Vector3((float)XoffSet, (float)YoffSet, 0f);
			if (this.info.TipTextPos == SCREEN_DIRECTION.LEFT)
			{
				this.SetPicLeft();
			}
			else if (this.info.TipTextPos == SCREEN_DIRECTION.RIGHT)
			{
				this.SetPicRight();
			}
			else if (this.TipTextBottomSprite.transform.position.x > this.CenterRoot.transform.position.x)
			{
				this.SetPicLeft();
			}
			else
			{
				this.SetPicRight();
			}
		}
		if (this.info.IsShowHand)
		{
			UnityVersionUtil.SetActiveRecursive(this.HandTipTweenS.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.HandTipTweenS.gameObject, false);
		}
		if (this.info.IsShowCircle)
		{
			UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, false);
		}
		if (this.mHighLightObjList != null && this.mHighLightObjList.Count != 0 && SingletonUnity<FunctionTipsRootLogic>.Exists)
		{
			for (int j = 0; j < this.mHighLightObjList.Count; j++)
			{
				if (SingletonUnity<FunctionTipsRootLogic>.Instance.IsHaveTipsCircle(this.mHighLightObjList[j].gameObject))
				{
					UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, false);
					break;
				}
			}
		}
		this.info = null;
		yield break;
	}

	// Token: 0x06004D27 RID: 19751 RVA: 0x001A3EFC File Offset: 0x001A20FC
	public void Reset(TutorialUIRootLogic.TutorialUIChangePannelInfo infos)
	{
		if (SingletonUnity<MissionTeamTipLogic>.Exists && SingletonUnity<MissionTeamTipLogic>.Instance.gameObject.active)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.CloseHandTip();
		}
		this.info = infos;
		UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.HandTipTweenS.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.TipTextLabel.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.TipTextBottomSprite.gameObject, false);
		if (!this.info.IsMaskEnable)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.RightMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TopMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.BottomMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CenterMaskObj.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.RightMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TopMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.BottomMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CenterMaskObj.gameObject, true);
			if (this.info.IsMaskShow)
			{
				this.CenterMaskObj.alpha = 0.4f;
			}
			else
			{
				this.CenterMaskObj.alpha = 0.01f;
			}
		}
		this.mOnClickTutorialBtn = null;
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.DelayReset());
		}
	}

	// Token: 0x06004D28 RID: 19752 RVA: 0x001A409C File Offset: 0x001A229C
	private void SetPicRight()
	{
		this.TipPic.transform.localPosition = new Vector3((float)(this.TipTextBottomSprite.width / 2), (float)(-(float)this.TipTextBottomSprite.height / 2), 0f);
		this.TipPic.transform.localScale = new Vector3(1f, 1f, 1f);
		this.TipTextLabel.transform.localPosition = new Vector3((float)(-(float)this.TipTextBottomSprite.width / 2 + 10), 0f, 0f);
	}

	// Token: 0x06004D29 RID: 19753 RVA: 0x001A4138 File Offset: 0x001A2338
	private void SetPicLeft()
	{
		this.TipPic.transform.localPosition = new Vector3((float)(-(float)this.TipTextBottomSprite.width / 2), (float)(-(float)this.TipTextBottomSprite.height / 2), 0f);
		this.TipPic.transform.localScale = new Vector3(-1f, 1f, 1f);
		this.TipTextLabel.transform.localPosition = new Vector3((float)(this.TipTextBottomSprite.width / 2 - 10 - this.TipTextLabel.width), 0f, 0f);
	}

	// Token: 0x06004D2A RID: 19754 RVA: 0x001A41E0 File Offset: 0x001A23E0
	public void Reset(TutorialUIRootLogic.TutorialUIInfo info)
	{
		if (SingletonUnity<MissionTeamTipLogic>.Exists && SingletonUnity<MissionTeamTipLogic>.Instance.gameObject.active)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.CloseHandTip();
		}
		this.infoo = info;
		UIAnchor uianchor = NGUITools.FindInParents<UIAnchor>(info.CenterObj);
		if (uianchor != null)
		{
			this.AnchorRoot.side = uianchor.side;
		}
		else
		{
			this.AnchorRoot.side = UIAnchor.Side.Center;
		}
		this.AnchorRoot.ScreenSizeChanged();
		if (info.CenterObj == null)
		{
			this.RootOffsetTrans.transform.localPosition = info.CenterPos;
		}
		else if (info.Pivot == UIWidget.Pivot.Center)
		{
			this.RootOffsetTrans.transform.position = info.CenterObj.transform.position;
		}
		else if (info.Pivot == UIWidget.Pivot.Top)
		{
			this.RootOffsetTrans.transform.position = info.CenterObj.transform.position - new Vector3(0f, (float)(info.Height / 2) * this.RootOffsetTrans.transform.lossyScale.y, 0f);
		}
		this.HandTipTweenS.enabled = true;
		this.HandTipTweenS.transform.localPosition = Vector3.zero;
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.NoChangeDelayReset());
		}
	}

	// Token: 0x06004D2B RID: 19755 RVA: 0x001A4364 File Offset: 0x001A2564
	private IEnumerator NoChangeDelayReset()
	{
		yield return null;
		TutorialUIRootLogic.TutorialUIInfo info = this.infoo;
		UIAnchor targetAnchor = NGUITools.FindInParents<UIAnchor>(info.CenterObj);
		if (targetAnchor != null)
		{
			this.AnchorRoot.side = targetAnchor.side;
		}
		else
		{
			this.AnchorRoot.side = UIAnchor.Side.Center;
		}
		this.AnchorRoot.ScreenSizeChanged();
		if (info.CenterObj == null)
		{
			this.RootOffsetTrans.transform.localPosition = info.CenterPos;
		}
		else if (info.Pivot == UIWidget.Pivot.Center)
		{
			this.RootOffsetTrans.transform.position = info.CenterObj.transform.position;
		}
		else if (info.Pivot == UIWidget.Pivot.Top)
		{
			this.RootOffsetTrans.transform.position = info.CenterObj.transform.position - new Vector3(0f, (float)(info.Height / 2) * this.RootOffsetTrans.transform.lossyScale.y, 0f);
		}
		this.ChangePannelFlag = false;
		UnityVersionUtil.SetActiveRecursive(this.RootOffsetTrans.gameObject, true);
		if (!info.IsTipCircleEnable)
		{
			UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TipCircleSprite.gameObject, true);
		}
		if (!info.IsMaskEnable)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.RightMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TopMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.BottomMaskTrans.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CenterMaskObj.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftMaskTrans.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.RightMaskTrans.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.TopMaskTrans.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.BottomMaskTrans.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.CenterMaskObj.gameObject, false);
			if (info.IsMaskShow)
			{
				for (int i = 0; i < this.MaskPic.Count; i++)
				{
					this.MaskPic[i].color = new Color(1f, 1f, 1f, 0.3f);
				}
			}
			else
			{
				for (int j = 0; j < this.MaskPic.Count; j++)
				{
					this.MaskPic[j].color = new Color(1f, 1f, 1f, 0.01f);
				}
			}
			int xOffset = this.MaskSprite.width / 2;
			int yOffset = this.MaskSprite.height / 2;
			int xFit = this.MaskSprite.width % 2;
			int yFit = this.MaskSprite.height % 2;
			this.LeftMaskTrans.transform.localPosition = new Vector3((float)(-(float)xOffset - info.Width / 2), (float)(-(float)yOffset + info.Height / 2 - yFit), 0f);
			this.RightMaskTrans.transform.localPosition = new Vector3((float)(xOffset + info.Width / 2), (float)(yOffset - info.Height / 2 + yFit), 0f);
			this.TopMaskTrans.transform.localPosition = new Vector3((float)(-(float)xOffset + info.Width / 2 - xFit), (float)(yOffset + info.Height / 2), 0f);
			this.BottomMaskTrans.transform.localPosition = new Vector3((float)(xOffset - info.Width / 2 + xFit), (float)(-(float)yOffset - info.Height / 2), 0f);
		}
		if (string.IsNullOrEmpty(info.TipText))
		{
			UnityVersionUtil.SetActiveRecursive(this.TipTextLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.TipTextBottomSprite.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TipTextLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.TipTextBottomSprite.gameObject, true);
			this.TipTextLabel.width = this.mTipLabelLength;
			this.TipTextLabel.text = info.TipText;
			if (this.TipTextLabel.printedSize.x < (float)this.mTipLabelLength)
			{
				this.TipTextLabel.width = Mathf.CeilToInt(this.TipTextLabel.printedSize.x);
			}
			else
			{
				this.TipTextLabel.width = this.mTipLabelLength;
			}
			this.TipTextBottomSprite.width = this.TipTextLabel.width + 84;
			this.TipTextBottomSprite.height = this.TipTextLabel.height + 20;
			int width = this.TipTextBottomSprite.width;
			int height = this.TipTextBottomSprite.height;
			int XoffSet = 0;
			int YoffSet = 0;
			int Distance = 20;
			switch (info.TipTextPos)
			{
			case SCREEN_DIRECTION.TOP:
				YoffSet = Distance + height / 2 + info.Height / 2;
				break;
			case SCREEN_DIRECTION.BOTTOM:
				YoffSet = -(Distance + height / 2 + info.Height / 2);
				break;
			case SCREEN_DIRECTION.LEFT:
				XoffSet = -(Distance + width / 2 + info.Width / 2);
				break;
			case SCREEN_DIRECTION.RIGHT:
				XoffSet = Distance + width / 2 + info.Width / 2;
				break;
			case SCREEN_DIRECTION.TOP_LEFT:
				YoffSet = Distance + height / 2 + info.Height / 2;
				XoffSet = -(Distance + width / 2 + info.Width / 2);
				break;
			case SCREEN_DIRECTION.BOTTOM_LEFT:
				YoffSet = -(Distance + height / 2 + info.Height / 2);
				XoffSet = -(Distance + width / 2 + info.Width / 2);
				break;
			case SCREEN_DIRECTION.TOP_RIGHT:
				YoffSet = Distance + height / 2 + info.Height / 2;
				XoffSet = Distance + width / 2 + info.Width / 2;
				break;
			case SCREEN_DIRECTION.BOTTOM_RIGHT:
				YoffSet = -(Distance + height / 2 + info.Height / 2);
				XoffSet = Distance + width / 2 + info.Width / 2;
				break;
			}
			this.TipTextBottomSprite.transform.localPosition = new Vector3((float)XoffSet, (float)YoffSet, 0f);
			if (info.TipTextPos == SCREEN_DIRECTION.LEFT)
			{
				this.SetPicLeft();
			}
			else if (info.TipTextPos == SCREEN_DIRECTION.RIGHT)
			{
				this.SetPicRight();
			}
			else if (this.TipTextBottomSprite.transform.position.x > this.CenterRoot.transform.position.x)
			{
				this.SetPicLeft();
			}
			else
			{
				this.SetPicRight();
			}
		}
		this.mOnClickTutorialBtn = null;
		yield break;
	}

	// Token: 0x06004D2C RID: 19756 RVA: 0x001A4380 File Offset: 0x001A2580
	public void CloseCheck()
	{
		if (this.mHighLightObjList != null && this.mHighLightObjList.Count > 0 && this.ChangePannelFlag && this.mHighLightObjList != null && this.mHighLightObjList.Count != 0)
		{
			for (int i = 0; i < this.mHighLightObjList.Count; i++)
			{
				this.mHighLightObjList[i].transform.parent = this.mPreTransParentList[i];
				this.mHighLightObjList[i].transform.localPosition = this.mPreLocalPosition[i];
				if (!this.mPreTransParentList[i].gameObject.active)
				{
					NGUITools.SetActive(this.mHighLightObjList[i].gameObject, false);
				}
				NGUITools.MarkParentAsChanged(this.mHighLightObjList[i].gameObject);
			}
			this.mHighLightObjList.Clear();
			this.mPreTransParentList.Clear();
			this.mPreLocalPosition.Clear();
		}
	}

	// Token: 0x06004D2D RID: 19757 RVA: 0x001A4498 File Offset: 0x001A2698
	private void OnEnable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
		}
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06004D2E RID: 19758 RVA: 0x001A44EC File Offset: 0x001A26EC
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
		this.info = null;
		this.infoo = null;
	}

	// Token: 0x06004D2F RID: 19759 RVA: 0x001A454C File Offset: 0x001A274C
	private void ScreenSizeChanged()
	{
		if (this.infoo != null && this.infoo.IsMaskEnable)
		{
			int num = this.MaskSprite.width / 2;
			int num2 = this.MaskSprite.height / 2;
			int num3 = this.MaskSprite.width % 2;
			int num4 = this.MaskSprite.height % 2;
			this.LeftMaskTrans.transform.localPosition = new Vector3((float)(-(float)num - this.infoo.Width / 2), (float)(-(float)num2 + this.infoo.Height / 2 - num4), 0f);
			this.RightMaskTrans.transform.localPosition = new Vector3((float)(num + this.infoo.Width / 2), (float)(num2 - this.infoo.Height / 2 + num4), 0f);
			this.TopMaskTrans.transform.localPosition = new Vector3((float)(-(float)num + this.infoo.Width / 2 - num3), (float)(num2 + this.infoo.Height / 2), 0f);
			this.BottomMaskTrans.transform.localPosition = new Vector3((float)(num - this.infoo.Width / 2 + num3), (float)(-(float)num2 - this.infoo.Height / 2), 0f);
		}
	}

	// Token: 0x06004D30 RID: 19760 RVA: 0x001A46A0 File Offset: 0x001A28A0
	public static void CloseWindow()
	{
		if (SingletonUnity<TutorialUIRootLogic>.Exists && SingletonUnity<TutorialUIRootLogic>.Instance.gameObject.active)
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TutorialUIRoot);
		}
	}

	// Token: 0x04003AC5 RID: 15045
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003AC6 RID: 15046
	private float mTutorialStartTime;

	// Token: 0x04003AC7 RID: 15047
	private float mTutorialWaitTime = -1f;

	// Token: 0x04003AC8 RID: 15048
	public UIPanel TutorialPannel;

	// Token: 0x04003AC9 RID: 15049
	public Transform RootOffsetTrans;

	// Token: 0x04003ACA RID: 15050
	public Transform LeftMaskTrans;

	// Token: 0x04003ACB RID: 15051
	public Transform RightMaskTrans;

	// Token: 0x04003ACC RID: 15052
	public Transform TopMaskTrans;

	// Token: 0x04003ACD RID: 15053
	public Transform BottomMaskTrans;

	// Token: 0x04003ACE RID: 15054
	public List<UISprite> MaskPic;

	// Token: 0x04003ACF RID: 15055
	public UISprite CenterMaskObj;

	// Token: 0x04003AD0 RID: 15056
	public UISprite MaskSprite;

	// Token: 0x04003AD1 RID: 15057
	public TweenScale HandTipTweenS;

	// Token: 0x04003AD2 RID: 15058
	public UILabel TipTextLabel;

	// Token: 0x04003AD3 RID: 15059
	public UISprite TipTextBottomSprite;

	// Token: 0x04003AD4 RID: 15060
	public UITexture TipPic;

	// Token: 0x04003AD5 RID: 15061
	public UISprite TipCircleSprite;

	// Token: 0x04003AD6 RID: 15062
	private bool ChangePannelFlag;

	// Token: 0x04003AD7 RID: 15063
	private List<UIWidget> mHighLightObjList = new List<UIWidget>();

	// Token: 0x04003AD8 RID: 15064
	private List<Transform> mPreTransParentList = new List<Transform>();

	// Token: 0x04003AD9 RID: 15065
	private List<Vector3> mPreLocalPosition = new List<Vector3>();

	// Token: 0x04003ADA RID: 15066
	public Transform HightLightObjRoot;

	// Token: 0x04003ADB RID: 15067
	public Transform CenterRoot;

	// Token: 0x04003ADC RID: 15068
	public UIAnchor AnchorRoot;

	// Token: 0x04003ADD RID: 15069
	private TutorialUIRootLogic.TutorialUIChangePannelInfo info;

	// Token: 0x04003ADE RID: 15070
	private int mTipLabelLength = 286;

	// Token: 0x04003ADF RID: 15071
	private TutorialUIRootLogic.TutorialUIInfo infoo;

	// Token: 0x02000A56 RID: 2646
	public class TutorialUIInfo
	{
		// Token: 0x06004D31 RID: 19761 RVA: 0x001A46E4 File Offset: 0x001A28E4
		public TutorialUIInfo(GameObject centerObj, int width, int height, string tipText, SCREEN_DIRECTION tipTextPos, float duration, bool isTipCircleEnable, bool isMaskEnable, bool isMaskShow = true, UIWidget.Pivot pivot = UIWidget.Pivot.Top)
		{
			this.CenterObj = centerObj;
			this.CenterPos = this.CenterObj.transform.position;
			this.Width = width;
			this.Height = height;
			this.TipText = tipText;
			this.TipTextPos = tipTextPos;
			this.IsTipCircleEnable = isTipCircleEnable;
			this.IsMaskEnable = isMaskEnable;
			this.IsMaskShow = isMaskShow;
			this.Pivot = pivot;
			this.Duration = duration;
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x001A475C File Offset: 0x001A295C
		public TutorialUIInfo(Vector3 centerPos, int width, int height, string tipText, SCREEN_DIRECTION tipTextPos, bool isTipCircleEnable, bool isMaskEnable, bool isMaskShow = true, UIWidget.Pivot pivot = UIWidget.Pivot.Top)
		{
			this.CenterObj = null;
			this.CenterPos = centerPos;
			this.Width = width;
			this.Height = height;
			this.TipText = tipText;
			this.TipTextPos = tipTextPos;
			this.IsTipCircleEnable = isTipCircleEnable;
			this.IsMaskEnable = isMaskEnable;
			this.IsMaskShow = isMaskShow;
			this.Pivot = pivot;
		}

		// Token: 0x04003AE0 RID: 15072
		public GameObject CenterObj;

		// Token: 0x04003AE1 RID: 15073
		public Vector3 CenterPos;

		// Token: 0x04003AE2 RID: 15074
		public int Width;

		// Token: 0x04003AE3 RID: 15075
		public int Height;

		// Token: 0x04003AE4 RID: 15076
		public string TipText;

		// Token: 0x04003AE5 RID: 15077
		public SCREEN_DIRECTION TipTextPos;

		// Token: 0x04003AE6 RID: 15078
		public bool IsTipCircleEnable;

		// Token: 0x04003AE7 RID: 15079
		public bool IsMaskEnable;

		// Token: 0x04003AE8 RID: 15080
		public bool IsMaskShow;

		// Token: 0x04003AE9 RID: 15081
		public UIWidget.Pivot Pivot;

		// Token: 0x04003AEA RID: 15082
		public float Duration;
	}

	// Token: 0x02000A57 RID: 2647
	public class TutorialUIChangePannelInfo
	{
		// Token: 0x06004D33 RID: 19763 RVA: 0x001A47BC File Offset: 0x001A29BC
		public TutorialUIChangePannelInfo(GameObject centerObj, List<UIWidget> objList, int width, int height, string tipText, SCREEN_DIRECTION tipTextPos, bool isTipCircleEnable, bool isMaskEnable, bool isMaskShow, bool isShowHand, bool isShowCircle)
		{
			this.CenterObj = centerObj;
			this.HighLightObjList = objList;
			this.Width = width;
			this.Height = height;
			this.TipText = tipText;
			this.TipTextPos = tipTextPos;
			this.IsTipCircleEnable = isTipCircleEnable;
			this.IsMaskEnable = isMaskEnable;
			this.IsMaskShow = isMaskShow;
			this.IsShowHand = isShowHand;
			this.IsShowCircle = isShowCircle;
		}

		// Token: 0x04003AEB RID: 15083
		public GameObject CenterObj;

		// Token: 0x04003AEC RID: 15084
		public List<UIWidget> HighLightObjList;

		// Token: 0x04003AED RID: 15085
		public int Width;

		// Token: 0x04003AEE RID: 15086
		public int Height;

		// Token: 0x04003AEF RID: 15087
		public string TipText;

		// Token: 0x04003AF0 RID: 15088
		public SCREEN_DIRECTION TipTextPos;

		// Token: 0x04003AF1 RID: 15089
		public bool IsTipCircleEnable;

		// Token: 0x04003AF2 RID: 15090
		public bool IsMaskEnable;

		// Token: 0x04003AF3 RID: 15091
		public bool IsMaskShow;

		// Token: 0x04003AF4 RID: 15092
		public bool IsShowHand;

		// Token: 0x04003AF5 RID: 15093
		public bool IsShowCircle;
	}
}
