using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009D7 RID: 2519
public class GongNengQuLogic : SingletonUnity<GongNengQuLogic>
{
	// Token: 0x0600478D RID: 18317 RVA: 0x0016D070 File Offset: 0x0016B270
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x0600478E RID: 18318 RVA: 0x0016D07C File Offset: 0x0016B27C
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x0600478F RID: 18319 RVA: 0x0016D09C File Offset: 0x0016B29C
	private void UpdateViewState()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			CameraController cameraController = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
			if (cameraController != null)
			{
				if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FREE || CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED_2_FREE)
				{
					this.viewStateLabel.text = "3D";
				}
				else
				{
					this.viewStateLabel.text = "2.5D";
				}
			}
			else
			{
				Log.ERROR_MSG("GongNengQuLogic CameraController is NULL!");
			}
		}
		else
		{
			this.viewStateLabel.text = "3D";
		}
	}

	// Token: 0x06004790 RID: 18320 RVA: 0x0016D13C File Offset: 0x0016B33C
	public void ChangViewOnClick()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			CameraController cameraController = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
			if (cameraController != null)
			{
				if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FREE)
				{
					cameraController.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FIXED);
				}
				else if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED)
				{
					cameraController.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FREE);
				}
			}
		}
		this.UpdateViewState();
	}

	// Token: 0x06004791 RID: 18321 RVA: 0x0016D1A8 File Offset: 0x0016B3A8
	public void OnClickMenuBtn()
	{
	}

	// Token: 0x06004792 RID: 18322 RVA: 0x0016D1AC File Offset: 0x0016B3AC
	public void OnClickExitBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(108, 3f))
		{
			NoticeLogic.AddNotifyData("#{200066}", true, false);
			return;
		}
		MessageBoxLogic.OpenOKCancelBox("#{100319}", "#{101506}", new MessageBoxLogic.OnYesClick(this.ExitMission), null, null, null);
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x0016D1FC File Offset: 0x0016B3FC
	private void ExitMission()
	{
		leave_copy_scene.request rpcReq = new leave_copy_scene.request();
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(rpcReq, null);
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x0016D21C File Offset: 0x0016B41C
	private new void Awake()
	{
		base.Awake();
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x0016D224 File Offset: 0x0016B424
	private void Start()
	{
		this.UpdateViewState();
	}

	// Token: 0x06004796 RID: 18326 RVA: 0x0016D22C File Offset: 0x0016B42C
	private void OnEnable()
	{
		this.Reset();
	}

	// Token: 0x06004797 RID: 18327 RVA: 0x0016D234 File Offset: 0x0016B434
	public void Reset()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld())
		{
			UnityVersionUtil.SetActiveRecursive(this.ExitBtnRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.MapNameRoot, true);
			this.MapNameLabel.text = StrDictionary.GetDictionaryString(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.Name, new object[0]);
			this.ViewBtnRoot.transform.localPosition = new Vector3(-286f, -35f, 0f);
			this.MenuBtnRoot.transform.localPosition = new Vector3(-35f, -35f, 0f);
		}
		else if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			UnityVersionUtil.SetActiveRecursive(this.ExitBtnRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.MenuBtnRoot.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.MapNameRoot, false);
			this.ViewBtnRoot.transform.localPosition = new Vector3(-105f, -35f, 0f);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ExitBtnRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.MapNameRoot, false);
			this.ViewBtnRoot.transform.localPosition = new Vector3(-175f, -35f, 0f);
			this.MenuBtnRoot.transform.localPosition = new Vector3(-105f, -35f, 0f);
		}
	}

	// Token: 0x06004798 RID: 18328 RVA: 0x0016D3AC File Offset: 0x0016B5AC
	public void OnClickMapBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MapUIRoot, delegate
		{
			SingletonUnity<MapUIRootLogic>.Instance.Reset();
		}, null);
	}

	// Token: 0x040034CB RID: 13515
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040034CC RID: 13516
	public UILabel viewStateLabel;

	// Token: 0x040034CD RID: 13517
	public UILabel MapNameLabel;

	// Token: 0x040034CE RID: 13518
	public UISprite MenuBtnSprite;

	// Token: 0x040034CF RID: 13519
	public UIWidget ViewBtnRoot;

	// Token: 0x040034D0 RID: 13520
	public GameObject MapNameRoot;

	// Token: 0x040034D1 RID: 13521
	public GameObject MenuBtnRoot;

	// Token: 0x040034D2 RID: 13522
	public GameObject ExitBtnRoot;
}
