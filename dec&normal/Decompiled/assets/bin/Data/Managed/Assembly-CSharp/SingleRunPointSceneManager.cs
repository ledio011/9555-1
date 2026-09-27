using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000888 RID: 2184
public class SingleRunPointSceneManager : SceneManager
{
	// Token: 0x06003AE0 RID: 15072 RVA: 0x000FFC74 File Offset: 0x000FDE74
	public override void Init(string id)
	{
		base.Init(id);
		this.mPathPointList = base.CurrentMapInofData.PlayerPathPointList;
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject;
		this.mMoveTargetPoint = gameObject.GetComponent<MovePathPoint>();
		UnityVersionUtil.SetActiveRecursive(this.mMoveTargetPoint.gameObject, false);
		this.mMoveTargetPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArriveMovePoint));
		this.mCurPointIndex = 0;
		this.mAnimaObj = null;
		if (!string.IsNullOrEmpty(this.mapInfoData.Param2))
		{
			this.mAnimaObj = (ResourcesManager.LoadAndInstantiate(string.Format("StartSceneAnima/{0}", this.mapInfoData.Param2)) as GameObject);
			UnityVersionUtil.SetActiveRecursive(this.mAnimaObj, false);
		}
		SingletonUnity<MyEvent>.Instance.Register("OnLoadingOver", this, "OnLoadingOver");
	}

	// Token: 0x06003AE1 RID: 15073 RVA: 0x000FFD48 File Offset: 0x000FDF48
	public override void OnLoadingOver()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnLoadingOver", this, "OnLoadingOver");
		base.LoadingFlag = false;
		if (base.IsCanShowCheckPopUI())
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckPopTipsUI();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ClearReshowUI();
		}
		if (this.mapInfoData.ID.Equals("312"))
		{
			TutorialManager.ShowTutorial(TUTORIAL_STEP.AUTO_FIGHT_CLICK);
		}
		else
		{
			NetLogic.GetInstance().Send<Protocol.map_ready>(null, null);
			LoadingWindow.isSendMapReady = true;
		}
	}

	// Token: 0x06003AE2 RID: 15074 RVA: 0x000FFDD8 File Offset: 0x000FDFD8
	public void OpenBlock()
	{
		if (this.mCurPointIndex < this.mPathPointList.Count)
		{
			this.mMoveTargetPoint.transform.position = this.mPathPointList[this.mCurPointIndex];
			UnityVersionUtil.SetActiveRecursive(this.mMoveTargetPoint.gameObject, true);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
			{
				SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMoveTargetPoint.gameObject, Singleton<ObjManager>.Instance.MainPlayer.gameObject);
			}, null);
			this.mCurPointIndex++;
		}
	}

	// Token: 0x06003AE3 RID: 15075 RVA: 0x000FFE58 File Offset: 0x000FE058
	private void OnArriveMovePoint(Vector3 pos)
	{
		if (this.mCurPointIndex == this.mPathPointList.Count && this.mAnimaObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mAnimaObj, true);
			this.CurAnimaCtl = this.mAnimaObj.GetComponent<SceneAnimationCtl>();
			this.CurAnimaCtl.RegisterOnFinished(delegate
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
				NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
				Singleton<ObjManager>.Instance.MainPlayer.CameraController.CurCamera.enabled = true;
				if (SingletonUnity<ScreenBottomBtn>.Exists)
				{
					SingletonUnity<ScreenBottomBtn>.Instance.LockBtn = false;
				}
				UICamera.mainCamera.depth = 0f;
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SkipBtnRoot);
			});
			Singleton<ObjManager>.Instance.MainPlayer.CameraController.CurCamera.enabled = false;
			if (SingletonUnity<ScreenBottomBtn>.Exists)
			{
				SingletonUnity<ScreenBottomBtn>.Instance.LockBtn = true;
			}
			UICamera.mainCamera.depth = 2f;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SkipBtnRoot, null, null);
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
		NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
	}

	// Token: 0x06003AE4 RID: 15076 RVA: 0x000FFF3C File Offset: 0x000FE13C
	public override void AutoFightAction()
	{
		if (UnityVersionUtil.IsActive(this.mMoveTargetPoint.gameObject))
		{
			Singleton<ObjManager>.Instance.MainPlayer.MoveTo(this.mMoveTargetPoint.transform.position, 1f, null);
		}
	}

	// Token: 0x04002684 RID: 9860
	private List<Vector3> mPathPointList = new List<Vector3>();

	// Token: 0x04002685 RID: 9861
	private MovePathPoint mMoveTargetPoint;

	// Token: 0x04002686 RID: 9862
	private int mCurPointIndex;

	// Token: 0x04002687 RID: 9863
	private GameObject mAnimaObj;

	// Token: 0x04002688 RID: 9864
	public SceneAnimationCtl CurAnimaCtl;
}
