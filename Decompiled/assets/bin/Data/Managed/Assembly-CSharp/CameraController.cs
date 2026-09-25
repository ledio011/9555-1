using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200083A RID: 2106
public class CameraController : MonoBehaviour
{
	// Token: 0x17000EDE RID: 3806
	// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000DC780 File Offset: 0x000DA980
	public static CameraController.CAMERAVIEWSTATE CurrentViewState
	{
		get
		{
			return CameraController.mCurrentViewState;
		}
	}

	// Token: 0x17000EDF RID: 3807
	// (get) Token: 0x060035EA RID: 13802 RVA: 0x000DC788 File Offset: 0x000DA988
	// (set) Token: 0x060035EB RID: 13803 RVA: 0x000DC790 File Offset: 0x000DA990
	public bool IsCamCanUse
	{
		get
		{
			return this.isCamCanUse;
		}
		set
		{
			this.isCamCanUse = value;
		}
	}

	// Token: 0x17000EE0 RID: 3808
	// (get) Token: 0x060035EC RID: 13804 RVA: 0x000DC79C File Offset: 0x000DA99C
	// (set) Token: 0x060035ED RID: 13805 RVA: 0x000DC7A4 File Offset: 0x000DA9A4
	public float IdealDistance25
	{
		get
		{
			return this.idealDistance25;
		}
		set
		{
			this.idealDistance25 = Mathf.Clamp(value, this.minDistance25, this.maxDistance25);
		}
	}

	// Token: 0x17000EE1 RID: 3809
	// (get) Token: 0x060035EE RID: 13806 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
	// (set) Token: 0x060035EF RID: 13807 RVA: 0x000DC7C8 File Offset: 0x000DA9C8
	public float IdealDistance30
	{
		get
		{
			return this.idealDistance30;
		}
		set
		{
			this.idealDistance30 = Mathf.Clamp(value, this.minDistance30, this.maxDistance30);
		}
	}

	// Token: 0x17000EE2 RID: 3810
	// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000DC7E4 File Offset: 0x000DA9E4
	public float Yaw
	{
		get
		{
			return this.yaw;
		}
	}

	// Token: 0x17000EE3 RID: 3811
	// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000DC7EC File Offset: 0x000DA9EC
	// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000DC7F4 File Offset: 0x000DA9F4
	public float IdealYaw
	{
		get
		{
			return this.idealYaw;
		}
		set
		{
			this.idealYaw = ((!this.clampYawAngle) ? value : CameraController.ClampAngle(value, this.minYaw, this.maxYaw));
		}
	}

	// Token: 0x17000EE4 RID: 3812
	// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000DC820 File Offset: 0x000DAA20
	public float Pitch
	{
		get
		{
			return this.pitch;
		}
	}

	// Token: 0x17000EE5 RID: 3813
	// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000DC828 File Offset: 0x000DAA28
	// (set) Token: 0x060035F5 RID: 13813 RVA: 0x000DC830 File Offset: 0x000DAA30
	public float IdealPitch
	{
		get
		{
			return this.idealPitch;
		}
		set
		{
			this.idealPitch = ((!this.clampPitchAngle) ? value : CameraController.ClampAngle(value, this.minPitch, this.maxPitch));
		}
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x000DC85C File Offset: 0x000DAA5C
	public void Init(CameraController.CAMERAVIEWSTATE initState = CameraController.CAMERAVIEWSTATE.FREE)
	{
		if (this.mInitFlag)
		{
			return;
		}
		this.mInitFlag = true;
		CameraController.mCurrentViewState = initState;
		if (this.mPlayerTransform == null)
		{
			this.mPlayerTransform = Singleton<ObjManager>.Instance.MainPlayer.CacheTransform;
		}
		if (this.mCameraRootTransform == null)
		{
			this.mCameraTransform = Camera.mainCamera.transform;
			this.mCameraRootTransform = this.mCameraTransform.parent;
		}
		this.InitCameraView();
	}

	// Token: 0x17000EE6 RID: 3814
	// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000DC8E0 File Offset: 0x000DAAE0
	public bool InitFlag
	{
		get
		{
			return this.mInitFlag;
		}
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x000DC8E8 File Offset: 0x000DAAE8
	private void Awake()
	{
		this.CurCamera = Camera.main;
		if (this.mPlayerData == null)
		{
			this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		int num = this.CurCamera.cullingMask;
		num &= -4194305;
		this.CurCamera.cullingMask = (num & -2097153);
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x000DC944 File Offset: 0x000DAB44
	private void Start()
	{
		this.Init(this.mPlayerData.ViewType);
	}

	// Token: 0x060035FA RID: 13818 RVA: 0x000DC958 File Offset: 0x000DAB58
	private void Update()
	{
		this.ColliderDistanceCheck();
		switch (CameraController.mCurrentViewState)
		{
		case CameraController.CAMERAVIEWSTATE.FIXED:
			this.Update2_5_Camera();
			break;
		case CameraController.CAMERAVIEWSTATE.FREE:
			this.Update3_0_Camera();
			break;
		case CameraController.CAMERAVIEWSTATE.FIXED_2_FREE:
			this.Fixed2Free();
			break;
		case CameraController.CAMERAVIEWSTATE.FREE_2_FIXED:
			this.Free2Fixed();
			break;
		case CameraController.CAMERAVIEWSTATE.FIXED_3D:
			this.UpdateFixed3DCamera();
			break;
		}
		if (this.CheckRock())
		{
			return;
		}
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x000DC9D4 File Offset: 0x000DABD4
	public void UpdateNow()
	{
		CameraController.CAMERAVIEWSTATE cameraviewstate = CameraController.mCurrentViewState;
		if (cameraviewstate != CameraController.CAMERAVIEWSTATE.FIXED)
		{
			if (cameraviewstate == CameraController.CAMERAVIEWSTATE.FREE)
			{
				this.Init30Camera(false);
			}
		}
		else
		{
			this.Init25Camera(false);
		}
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x000DCA14 File Offset: 0x000DAC14
	private void ColliderDistanceCheck()
	{
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector2 = this.mCameraRootTransform.position - vector;
		if (Physics.Raycast(new Ray(vector, vector2), ref this.rayCastHitInfo, this.preCamDistance, 1 << this.WALL_LAYER_VAL))
		{
			this.hitFlag = true;
			this.tempDistance = this.rayCastHitInfo.distance;
			if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED || CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FREE_2_FIXED)
			{
				this.mScale = Mathf.Clamp01(1f - (this.tempDistance - this.minDistance25) / (this.maxDistance25 - this.minDistance25));
			}
			else if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FREE || CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED_2_FREE)
			{
				this.mScale = Mathf.Clamp01(1f - (this.tempDistance - this.minDistance30) / (this.maxDistance30 - this.minDistance30));
			}
		}
		else if (this.hitFlag)
		{
			this.hitFlag = false;
			this.mScale = this.lastScale;
		}
		else
		{
			this.preCamDistance = this.distance;
			this.lastScale = this.mScale;
		}
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x000DCB54 File Offset: 0x000DAD54
	public void ChangeToCarView()
	{
		if (CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FREE)
		{
			this.mScale = 0f;
			this.maxDistance30 = 15f;
			this.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FREE);
			if (SingletonUnity<ExpLineRootLogic>.Exists)
			{
				SingletonUnity<ExpLineRootLogic>.Instance.UpdateViewType();
			}
		}
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x000DCBA0 File Offset: 0x000DADA0
	public void BackToNormalView()
	{
		this.maxDistance30 = 12f;
		if (CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FREE)
		{
			this.mScale = this.mLastViewDistance;
		}
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x000DCBD0 File Offset: 0x000DADD0
	public void LerpToTarget(Transform targetTrans, float durationTime, CameraController.OnLerpComplete onComplete = null)
	{
		base.enabled = false;
		Vector3 position = targetTrans.position;
		this.OnBeforeCamLerp();
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(base.transform, position, durationTime, false), 10), delegate()
		{
			if (onComplete != null)
			{
				onComplete();
			}
		}), 0);
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DORotate(base.transform, targetTrans.eulerAngles, durationTime, 0), 10), 0);
	}

	// Token: 0x06003600 RID: 13824 RVA: 0x000DCC58 File Offset: 0x000DAE58
	public void LerpToTargetLocalZero(Transform targetTrans, float durationTime, CameraController.OnLerpComplete onComplete = null)
	{
		base.enabled = false;
		this.OnBeforeCamLerp();
		base.transform.parent = targetTrans;
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(base.transform, Vector3.zero, durationTime, false), 10), delegate()
		{
			if (onComplete != null)
			{
				onComplete();
			}
		}), 0);
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalRotate(base.transform, Vector3.zero, durationTime, 0), 10), 0);
	}

	// Token: 0x06003601 RID: 13825 RVA: 0x000DCCE8 File Offset: 0x000DAEE8
	public void LerpToTargetPos(Vector3 targetPos, Vector3 lookTargetPos, float durationTime)
	{
		base.enabled = false;
		lookTargetPos += Vector3.up * this.LookHeight;
		Vector3 normalized = (lookTargetPos - targetPos).normalized;
		this.enableCamHandle.Cancel();
		this.OnBeforeCamLerp();
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(base.transform, targetPos, durationTime, false), 9), 0);
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DORotate(base.transform, Quaternion.LookRotation(normalized).eulerAngles, durationTime, 0), 9), 0);
	}

	// Token: 0x06003602 RID: 13826 RVA: 0x000DCD84 File Offset: 0x000DAF84
	public void LerpBackToPlayer(float durationTime, CameraController.OnLerpComplete onComplete = null)
	{
		if (!base.enabled)
		{
			base.transform.parent = null;
			Time.timeScale = 1f;
			this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
			Vector3 vector = this.mPlayerTransform.position + this.panOffset;
			Vector3 vector2 = vector - this.distance * (this.targetQuation * Vector3.forward);
			Vector3 normalized = (vector - vector2).normalized;
			this.enableCamHandle.Cancel();
			this.OnBeforeCamLerp();
			TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(base.transform, vector2, durationTime, false), 10), delegate()
			{
				if (onComplete != null)
				{
					onComplete();
				}
			}), 0);
			TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DORotate(base.transform, Quaternion.LookRotation(normalized).eulerAngles, durationTime, 0), 10), 0);
			vp_Timer.In(durationTime, delegate()
			{
				this.enabled = true;
			}, this.enableCamHandle);
		}
	}

	// Token: 0x06003603 RID: 13827 RVA: 0x000DCEB8 File Offset: 0x000DB0B8
	public void BackToPlayer(float durationTime)
	{
		if (!base.enabled)
		{
			vp_Timer.In(durationTime, delegate()
			{
				this.IdealYaw = this.mPlayerTransform.eulerAngles.y;
				this.UpdateNow();
				base.enabled = true;
			}, this.enableCamHandle);
		}
	}

	// Token: 0x06003604 RID: 13828 RVA: 0x000DCEE0 File Offset: 0x000DB0E0
	public void LookAtGameObject(Transform trs, float durationTime, CameraController.OnCamMoveOverDelegate moveOver = null)
	{
		base.enabled = false;
		Vector3 vector = trs.position - trs.forward * 6f + Vector3.up * 2f;
		this.OnBeforeCamLerp();
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnUpdate<Tweener>(TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(base.transform, vector, durationTime, false), 9), new TweenCallback(this.OnMoveOver)), delegate()
		{
			this.transform.LookAt(trs.position);
		}), 0);
	}

	// Token: 0x06003605 RID: 13829 RVA: 0x000DCF8C File Offset: 0x000DB18C
	private void OnBeforeCamLerp()
	{
		this.enableCamHandle.Cancel();
		DOTween.Kill(0, false);
	}

	// Token: 0x06003606 RID: 13830 RVA: 0x000DCFA8 File Offset: 0x000DB1A8
	private void OnDestroy()
	{
		this.enableCamHandle.Cancel();
	}

	// Token: 0x06003607 RID: 13831 RVA: 0x000DCFB8 File Offset: 0x000DB1B8
	public void OnMoveOver()
	{
		if (this.onMoveOver != null)
		{
			this.onMoveOver();
		}
		this.onMoveOver = null;
	}

	// Token: 0x06003608 RID: 13832 RVA: 0x000DCFD8 File Offset: 0x000DB1D8
	public void LookBackToPlayer(float durationTime)
	{
		Time.timeScale = 1f;
		this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		Vector3 lookAtPos = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector = lookAtPos - this.distance * (this.targetQuation * Vector3.forward);
		this.OnBeforeCamLerp();
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnUpdate<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(base.transform, vector, durationTime, false), 9), delegate()
		{
			this.transform.LookAt(lookAtPos);
		}), 0);
		vp_Timer.In(durationTime, delegate()
		{
			this.enabled = true;
		}, null);
	}

	// Token: 0x06003609 RID: 13833 RVA: 0x000DD0A8 File Offset: 0x000DB2A8
	public void BossDieCameraEffect(ObjCharacter objCharacter)
	{
		base.enabled = false;
		Transform trs = objCharacter.transform;
		BossCameraController bossCameraController = this.mCameraRootTransform.gameObject.AddComponent<BossCameraController>();
		bossCameraController.Init(objCharacter);
		Time.timeScale = 0.3f;
		Vector3 normalized = (this.mPlayerTransform.position - trs.position).normalized;
		Vector3 vector = trs.position + normalized * 2f + Vector3.up * 3f;
		float num = 1.5f;
		this.OnBeforeCamLerp();
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnUpdate<Tweener>(TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(TweenSettingsExtensions.SetUpdate<Tweener>(ShortcutExtensions.DOMove(base.transform, vector, num, false), true), 1), new TweenCallback(bossCameraController.CameraArrived)), delegate()
		{
			this.transform.LookAt(trs.position);
		}), 0);
	}

	// Token: 0x0600360A RID: 13834 RVA: 0x000DD1A8 File Offset: 0x000DB3A8
	public void FinishCopyCameraEffect(float durationTime, DelegateDefine.NoParamDelegate func)
	{
		base.enabled = false;
		Vector3 vector = this.mPlayerTransform.position + this.mPlayerTransform.forward * this.moveForwardDis + this.mPlayerTransform.up * this.moveUp + this.mPlayerTransform.right * this.moveRight;
		Vector3 PlayerLookPos = this.mPlayerTransform.position + this.mPlayerTransform.up * this.LookUp + this.mPlayerTransform.right * this.LookRight;
		this.OnBeforeCamLerp();
		TweenSettingsExtensions.SetId<Tweener>(TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.OnUpdate<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(base.transform, vector, durationTime, false), 9), delegate()
		{
			this.transform.LookAt(PlayerLookPos);
		}), delegate()
		{
			if (func != null)
			{
				func();
			}
		}), 0);
	}

	// Token: 0x0600360B RID: 13835 RVA: 0x000DD2C0 File Offset: 0x000DB4C0
	private void Fixed2Free()
	{
		this.currentTime += Time.deltaTime;
		if (this.currentTime >= this.time)
		{
			CameraController.mCurrentViewState = CameraController.CAMERAVIEWSTATE.FREE;
			this.currentTime = this.time;
		}
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		this.IdealDistance30 = Mathf.Lerp(this.maxDistance30, this.minDistance30, this.mScale);
		this.distance = this.IdealDistance30;
		Vector3 vector2 = vector - this.IdealDistance30 * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		this.mCameraRootTransform.localPosition = Vector3.Lerp(this.orginalPosition, this.targetPosition, this.currentTime / this.time);
		this.mCameraRootTransform.rotation = Quaternion.Slerp(this.orginalQuation, this.targetQuation, this.currentTime / this.time);
	}

	// Token: 0x0600360C RID: 13836 RVA: 0x000DD3C0 File Offset: 0x000DB5C0
	private void Free2Fixed()
	{
		this.currentTime += Time.deltaTime;
		if (this.currentTime >= this.time)
		{
			CameraController.mCurrentViewState = CameraController.CAMERAVIEWSTATE.FIXED;
			this.currentTime = this.time;
		}
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		this.IdealDistance25 = Mathf.Lerp(this.maxDistance25, this.minDistance25, this.mScale);
		this.distance = this.IdealDistance25;
		Vector3 vector2 = vector - this.IdealDistance25 * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		this.mCameraRootTransform.localPosition = Vector3.Lerp(this.orginalPosition, this.targetPosition, this.currentTime / this.time);
		this.mCameraRootTransform.rotation = Quaternion.Slerp(this.orginalQuation, this.targetQuation, this.currentTime / this.time);
	}

	// Token: 0x0600360D RID: 13837 RVA: 0x000DD4C0 File Offset: 0x000DB6C0
	private void InitCameraView()
	{
		this.mCameraTransform.localPosition = Vector3.zero;
		this.mCameraTransform.localRotation = Quaternion.identity;
		this.idealPanOffset = new Vector3(0f, 1.5f, 0f);
		if (this.mPlayerData.IsServerRidingMount)
		{
			this.IdealYaw = this.mPlayerTransform.eulerAngles.y;
			this.IdealPitch = 15f;
			this.ChangeToCarView();
		}
		else if (CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED)
		{
			this.mScale = 0f;
			Vector3 eulerAngles = this.mCameraRootTransform.eulerAngles;
			this.IdealDistance30 = this.initialDistance25;
			this.IdealYaw = eulerAngles.y;
			this.IdealPitch = eulerAngles.x;
			this.IdealYaw = this.mPlayerTransform.eulerAngles.y;
			this.Init25Camera(true);
		}
		else if (CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FREE)
		{
			this.mLastViewDistance = LocalDataSaveManager.GetLastViewDistance();
			this.mScale = this.mLastViewDistance;
			Vector3 eulerAngles2 = this.mCameraRootTransform.eulerAngles;
			this.IdealDistance30 = this.initialDistance30;
			this.IdealYaw = this.mPlayerTransform.eulerAngles.y;
			this.IdealPitch = 15f;
			this.Init30Camera(true);
		}
		else if (CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED_3D)
		{
			this.IdealYaw = this.mPlayerTransform.eulerAngles.y;
			this.IdealPitch = 15f;
			Singleton<ObjManager>.Instance.MainPlayer.HideHeadInfo();
		}
	}

	// Token: 0x0600360E RID: 13838 RVA: 0x000DD65C File Offset: 0x000DB85C
	public void ChangeCameraView(CameraController.CAMERAVIEWSTATE viewState)
	{
		if (viewState != CameraController.mCurrentViewState)
		{
			if (viewState == CameraController.CAMERAVIEWSTATE.FREE)
			{
				if (Singleton<ObjManager>.Instance.MainPlayer.IsDrivingMount())
				{
					this.mScale = 0f;
				}
				else
				{
					this.mScale = this.mLastViewDistance;
				}
				Vector3 eulerAngles = this.mCameraRootTransform.eulerAngles;
				this.IdealDistance30 = this.initialDistance30;
				this.IdealYaw = eulerAngles.y;
				this.IdealPitch = 15f;
				this.orginalPosition = this.mCameraRootTransform.localPosition;
				this.orginalQuation = this.mCameraRootTransform.rotation;
				CameraController.mCurrentViewState = CameraController.CAMERAVIEWSTATE.FREE;
				this.mPlayerData.ViewType = viewState;
				LocalDataSaveManager.SetCameraViewType(PlayerData.MainPlayerServerId, viewState);
				Singleton<ObjManager>.Instance.MainPlayer.ShowHeadInfo();
			}
			else if (viewState == CameraController.CAMERAVIEWSTATE.FIXED)
			{
				this.mScale = 0f;
				this.IdealDistance25 = this.initialDistance25;
				Vector3 eulerAngles2 = this.mCameraRootTransform.eulerAngles;
				this.IdealYaw = eulerAngles2.y;
				this.IdealPitch = eulerAngles2.x;
				this.orginalPosition = this.mCameraRootTransform.localPosition;
				this.orginalQuation = this.mCameraRootTransform.rotation;
				CameraController.mCurrentViewState = CameraController.CAMERAVIEWSTATE.FIXED;
				this.mPlayerData.ViewType = viewState;
				LocalDataSaveManager.SetCameraViewType(PlayerData.MainPlayerServerId, viewState);
				Singleton<ObjManager>.Instance.MainPlayer.ShowHeadInfo();
			}
			else if (viewState == CameraController.CAMERAVIEWSTATE.FIXED_3D)
			{
				CameraController.mCurrentViewState = CameraController.CAMERAVIEWSTATE.FIXED_3D;
				this.mPlayerData.ViewType = viewState;
				LocalDataSaveManager.SetCameraViewType(PlayerData.MainPlayerServerId, viewState);
				Singleton<ObjManager>.Instance.MainPlayer.HideHeadInfo();
			}
		}
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager != null)
		{
			sceneManager.UpdateDamgeBoadScale();
		}
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x000DD810 File Offset: 0x000DBA10
	private void Init25Camera(bool isForce = true)
	{
		this.IdealDistance25 = Mathf.Lerp(this.maxDistance25, this.minDistance25, this.mScale);
		this.distance = this.IdealDistance25;
		this.yaw = this.IdealYaw;
		this.pitch = this.view2_5Angle;
		this.panOffset = this.idealPanOffset;
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector2 = vector - this.distance * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		if (isForce)
		{
			this.mCameraRootTransform.position = this.targetPosition;
			this.mCameraRootTransform.rotation = this.targetQuation;
		}
	}

	// Token: 0x06003610 RID: 13840 RVA: 0x000DD904 File Offset: 0x000DBB04
	private void Update2_5_Camera()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		this.IdealDistance25 = Mathf.Lerp(this.maxDistance25, this.minDistance25, this.mScale);
		if (this.smoothMotion)
		{
			this.distance = Mathf.Lerp(this.distance, this.IdealDistance25, Time.deltaTime * this.smoothZoomSpeed);
			this.yaw = Mathf.LerpAngle(this.yaw, this.IdealYaw, Time.deltaTime * this.smoothOrbitSpeed);
			this.pitch = Mathf.LerpAngle(this.pitch, this.view2_5Angle, Time.deltaTime * this.smoothOrbitSpeed);
		}
		else
		{
			this.distance = this.IdealDistance25;
			this.yaw = this.IdealYaw;
			this.pitch = this.view2_5Angle;
		}
		if (this.smoothPanning)
		{
			this.panOffset = Vector3.Lerp(this.panOffset, this.idealPanOffset, Time.deltaTime * this.smoothPanningSpeed);
		}
		else
		{
			this.panOffset = this.idealPanOffset;
		}
		this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		this.mCameraRootTransform.rotation = this.targetQuation;
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector2 = vector - this.distance * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		this.mCameraRootTransform.position = this.targetPosition;
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x000DDAA0 File Offset: 0x000DBCA0
	private void Init30Camera(bool isForce = true)
	{
		this.IdealDistance30 = Mathf.Lerp(this.maxDistance30, this.minDistance30, this.mScale);
		this.distance = this.IdealDistance30;
		this.yaw = this.IdealYaw;
		this.pitch = this.IdealPitch;
		this.panOffset = this.idealPanOffset;
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector2 = vector - this.distance * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		if (isForce)
		{
			this.mCameraRootTransform.position = this.targetPosition;
			this.mCameraRootTransform.rotation = this.targetQuation;
		}
	}

	// Token: 0x06003612 RID: 13842 RVA: 0x000DDB94 File Offset: 0x000DBD94
	private void Update3_0_Camera()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		this.IdealDistance30 = Mathf.Lerp(this.maxDistance30, this.minDistance30, this.mScale);
		if (this.smoothMotion)
		{
			this.distance = Mathf.Lerp(this.distance, this.IdealDistance30, Time.deltaTime * this.smoothZoomSpeed);
			this.yaw = Mathf.LerpAngle(this.yaw, this.IdealYaw, Time.deltaTime * this.smoothOrbitSpeed);
			this.pitch = Mathf.LerpAngle(this.pitch, this.IdealPitch, Time.deltaTime * this.smoothOrbitSpeed);
		}
		else
		{
			this.distance = this.IdealDistance30;
			this.yaw = this.IdealYaw;
			this.pitch = this.IdealPitch;
		}
		if (this.smoothPanning)
		{
			this.panOffset = Vector3.Lerp(this.panOffset, this.idealPanOffset, Time.deltaTime * this.smoothPanningSpeed);
		}
		else
		{
			this.panOffset = this.idealPanOffset;
		}
		this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		this.mCameraRootTransform.rotation = this.targetQuation;
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector2 = vector - this.distance * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		this.mCameraRootTransform.position = this.targetPosition;
	}

	// Token: 0x06003613 RID: 13843 RVA: 0x000DDD30 File Offset: 0x000DBF30
	private void UpdateFixed3DCamera()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer == null)
		{
			return;
		}
		if (this.smoothMotion)
		{
			this.distance = Mathf.Lerp(this.distance, this.fixed3DDistance, Time.deltaTime * this.smoothZoomSpeed);
			this.yaw = Mathf.LerpAngle(this.yaw, this.IdealYaw, Time.deltaTime * this.smoothOrbitSpeed);
			this.pitch = Mathf.LerpAngle(this.pitch, this.IdealPitch, Time.deltaTime * this.smoothOrbitSpeed);
		}
		else
		{
			this.distance = this.IdealDistance30;
			this.yaw = this.IdealYaw;
			this.pitch = this.IdealPitch;
		}
		if (this.smoothPanning)
		{
			this.panOffset = Vector3.Lerp(this.panOffset, this.idealPanOffset, Time.deltaTime * this.smoothPanningSpeed);
		}
		else
		{
			this.panOffset = this.idealPanOffset;
		}
		this.targetQuation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		this.mCameraRootTransform.rotation = this.targetQuation;
		Vector3 vector = this.mPlayerTransform.position + this.panOffset;
		Vector3 vector2 = vector - this.distance * (this.targetQuation * Vector3.forward);
		this.targetPosition = vector2;
		this.mCameraRootTransform.position = this.targetPosition;
	}

	// Token: 0x06003614 RID: 13844 RVA: 0x000DDEB0 File Offset: 0x000DC0B0
	public void OnPinchCamera(PinchGesture gesture)
	{
		if (!SingletonUnity<JoyStickLogic>.Exists)
		{
			return;
		}
		if (!this.isCamCanUse || SingletonUnity<JoyStickLogic>.Instance.JoyStickUse || CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED || CameraController.mCurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED_3D)
		{
			return;
		}
		if (gesture.Phase == 2)
		{
			float num = Mathf.Abs(gesture.Delta);
			if (num > this.mPinchMax)
			{
				num = this.mPinchMax;
			}
			float num2 = num * gesture.Delta / Mathf.Abs(gesture.Delta);
			this.mScale += num2 * (0.5f + 0.5f * this.mScale) / this.mPinchSpeed;
			if (this.mScale > 1f)
			{
				this.mScale = 1f;
			}
			if (this.mScale < 0f)
			{
				this.mScale = 0f;
			}
		}
		else if (gesture.Phase == 3)
		{
			this.mLastViewDistance = this.mScale;
			LocalDataSaveManager.SetLastViewDistance(this.mLastViewDistance);
		}
	}

	// Token: 0x06003615 RID: 13845 RVA: 0x000DDFC0 File Offset: 0x000DC1C0
	public void OnDragCamera(DragGesture gesture)
	{
		if (!this.isCamCanUse || !base.enabled)
		{
			return;
		}
		this.IdealYaw += FingerGesturesExtensions.Centimeters(gesture.DeltaMove.x) * this.yawSensitivity;
		this.IdealPitch -= FingerGesturesExtensions.Centimeters(gesture.DeltaMove.y) * this.pitchSensitivity;
	}

	// Token: 0x06003616 RID: 13846 RVA: 0x000DE034 File Offset: 0x000DC234
	public void OnDragCamera(Vector2 delta)
	{
		if (!this.isCamCanUse)
		{
			return;
		}
		this.IdealYaw += FingerGesturesExtensions.Centimeters(delta.x) * this.yawSensitivity;
		this.IdealPitch -= FingerGesturesExtensions.Centimeters(delta.y) * this.pitchSensitivity;
	}

	// Token: 0x06003617 RID: 13847 RVA: 0x000DE090 File Offset: 0x000DC290
	public void StartDeathEffect()
	{
	}

	// Token: 0x06003618 RID: 13848 RVA: 0x000DE094 File Offset: 0x000DC294
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x06003619 RID: 13849 RVA: 0x000DE0D4 File Offset: 0x000DC2D4
	public void ClearByRockId(string rockId)
	{
		for (int i = 0; i < this.mCamRockInfoList.Count; i++)
		{
			CamRockInfo camRockInfo = this.mCamRockInfoList[i];
			if (camRockInfo.CamRockId == rockId)
			{
				camRockInfo.Init();
				this.mCamRockInfoList[i] = camRockInfo;
			}
		}
	}

	// Token: 0x0600361A RID: 13850 RVA: 0x000DE130 File Offset: 0x000DC330
	public void AddCamRock(string rockId)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Screen_Vibrating == 0f)
		{
			return;
		}
		CamRockData camRockDataByID = DataManager.GetCamRockDataByID(rockId);
		if (Random.Range(0, 100) > camRockDataByID.RockRate)
		{
			return;
		}
		if (camRockDataByID != null)
		{
			camRockDataByID.Init();
			CamRockInfo camRockInfo = new CamRockInfo();
			camRockInfo.Init();
			camRockInfo.CamRockId = rockId;
			camRockInfo.NeedRockTime = camRockDataByID.NeedRockTimeSecond;
			camRockInfo.DelayTime = camRockDataByID.DelayTimeSecond;
			camRockInfo.XPosCurve = camRockDataByID.XPosCurve;
			camRockInfo.YPosCurve = camRockDataByID.YPosCurve;
			camRockInfo.ZPosCurve = camRockDataByID.ZPosCurve;
			camRockInfo.XRotCurve = camRockDataByID.XRotCurve;
			camRockInfo.YRotCurve = camRockDataByID.YRotCurve;
			camRockInfo.ZRotCurve = camRockDataByID.ZRotCurve;
			camRockInfo.WRotCurve = camRockDataByID.WRotCurve;
			this.mCamRockInfoList.Add(camRockInfo);
		}
	}

	// Token: 0x0600361B RID: 13851 RVA: 0x000DE20C File Offset: 0x000DC40C
	private bool CheckRock()
	{
		bool result = false;
		if (this.mCamRockInfoList.Count > 0)
		{
			this.mTempRockPos = Vector3.zero;
			this.mTempRockRot = Quaternion.identity;
			for (int i = 0; i < this.mCamRockInfoList.Count; i++)
			{
				CamRockInfo camRockInfo = this.mCamRockInfoList[i];
				if (camRockInfo.IsValid())
				{
					if (camRockInfo.DelayTime > 0f)
					{
						camRockInfo.DelayTime -= Time.deltaTime;
					}
					else if (camRockInfo.RockTime > camRockInfo.NeedRockTime)
					{
						camRockInfo.Init();
					}
					else
					{
						camRockInfo.RockTime += Time.deltaTime;
						this.RockCam(camRockInfo);
						result = true;
					}
				}
			}
			this.mCameraTransform.localPosition = this.mTempRockPos;
			this.mCameraTransform.localRotation = this.mTempRockRot;
			for (int j = this.mCamRockInfoList.Count - 1; j >= 0; j--)
			{
				if (!this.mCamRockInfoList[j].IsValid())
				{
					this.mCamRockInfoList.RemoveAt(j);
				}
			}
		}
		return result;
	}

	// Token: 0x0600361C RID: 13852 RVA: 0x000DE340 File Offset: 0x000DC540
	private void RockCam(CamRockInfo rockInfo)
	{
		float num = rockInfo.XPosCurve.Evaluate(rockInfo.RockTime);
		float num2 = rockInfo.YPosCurve.Evaluate(rockInfo.RockTime);
		float num3 = rockInfo.ZPosCurve.Evaluate(rockInfo.RockTime);
		this.mTempRockPos += new Vector3(num, num2, num3);
		float num4 = rockInfo.XRotCurve.Evaluate(rockInfo.RockTime);
		float num5 = rockInfo.YRotCurve.Evaluate(rockInfo.RockTime);
		float num6 = rockInfo.ZRotCurve.Evaluate(rockInfo.RockTime);
		float num7 = rockInfo.WRotCurve.Evaluate(rockInfo.RockTime);
		this.mTempRockRot *= new Quaternion(num4, num5, num6, num7);
	}

	// Token: 0x0600361D RID: 13853 RVA: 0x000DE404 File Offset: 0x000DC604
	public void ClearCamRock()
	{
		this.mCamRockInfoList.Clear();
		this.mTempRockPos = Vector3.zero;
		this.mTempRockRot = Quaternion.identity;
		this.mCameraTransform.localPosition = Vector3.zero;
		this.mCameraTransform.localRotation = Quaternion.identity;
	}

	// Token: 0x0600361E RID: 13854 RVA: 0x000DE454 File Offset: 0x000DC654
	public void DisableCamera()
	{
		base.enabled = false;
	}

	// Token: 0x04002335 RID: 9013
	public Vector3 DialogRelativePos = new Vector3(1.7f, 1.5f, 1.7f);

	// Token: 0x04002336 RID: 9014
	public float mPinchSpeed = 100f;

	// Token: 0x04002337 RID: 9015
	public float mScale = 0.3f;

	// Token: 0x04002338 RID: 9016
	public float mPinchMax = 10f;

	// Token: 0x04002339 RID: 9017
	public float view2_5Angle = 30f;

	// Token: 0x0400233A RID: 9018
	public float mChangeSpeed = 6f;

	// Token: 0x0400233B RID: 9019
	private Transform mPlayerTransform;

	// Token: 0x0400233C RID: 9020
	private Transform mCameraRootTransform;

	// Token: 0x0400233D RID: 9021
	private Transform mCameraTransform;

	// Token: 0x0400233E RID: 9022
	private static CameraController.CAMERAVIEWSTATE mCurrentViewState = CameraController.CAMERAVIEWSTATE.FREE;

	// Token: 0x0400233F RID: 9023
	private bool isCamCanUse;

	// Token: 0x04002340 RID: 9024
	public float initialDistance30 = 15f;

	// Token: 0x04002341 RID: 9025
	public float initialDistance25 = 6f;

	// Token: 0x04002342 RID: 9026
	public float minDistance25 = 0.5f;

	// Token: 0x04002343 RID: 9027
	public float maxDistance25 = 15f;

	// Token: 0x04002344 RID: 9028
	public float minDistance30 = 1.5f;

	// Token: 0x04002345 RID: 9029
	public float maxDistance30 = 12f;

	// Token: 0x04002346 RID: 9030
	public float fixed3DDistance = 5f;

	// Token: 0x04002347 RID: 9031
	public float yawSensitivity = 25f;

	// Token: 0x04002348 RID: 9032
	public float pitchSensitivity = 25f;

	// Token: 0x04002349 RID: 9033
	public bool clampYawAngle;

	// Token: 0x0400234A RID: 9034
	public float minYaw = -75f;

	// Token: 0x0400234B RID: 9035
	public float maxYaw = 75f;

	// Token: 0x0400234C RID: 9036
	public bool clampPitchAngle = true;

	// Token: 0x0400234D RID: 9037
	public float minPitch = -3f;

	// Token: 0x0400234E RID: 9038
	public float maxPitch = 70f;

	// Token: 0x0400234F RID: 9039
	public bool allowPinchZoom = true;

	// Token: 0x04002350 RID: 9040
	public float pinchZoomSensitivity = 5f;

	// Token: 0x04002351 RID: 9041
	public bool smoothMotion = true;

	// Token: 0x04002352 RID: 9042
	public float smoothZoomSpeed = 5f;

	// Token: 0x04002353 RID: 9043
	public float smoothOrbitSpeed = 10f;

	// Token: 0x04002354 RID: 9044
	public bool smoothPanning = true;

	// Token: 0x04002355 RID: 9045
	public float smoothPanningSpeed = 12f;

	// Token: 0x04002356 RID: 9046
	private float distance = 10f;

	// Token: 0x04002357 RID: 9047
	private float yaw;

	// Token: 0x04002358 RID: 9048
	private float pitch;

	// Token: 0x04002359 RID: 9049
	private float idealDistance25;

	// Token: 0x0400235A RID: 9050
	private float idealDistance30;

	// Token: 0x0400235B RID: 9051
	private float idealYaw;

	// Token: 0x0400235C RID: 9052
	private float idealPitch;

	// Token: 0x0400235D RID: 9053
	public Vector3 idealPanOffset = Vector3.zero;

	// Token: 0x0400235E RID: 9054
	private Vector3 panOffset = Vector3.zero;

	// Token: 0x0400235F RID: 9055
	private Quaternion targetQuation;

	// Token: 0x04002360 RID: 9056
	private Vector3 targetPosition;

	// Token: 0x04002361 RID: 9057
	private Quaternion orginalQuation;

	// Token: 0x04002362 RID: 9058
	private Vector3 orginalPosition;

	// Token: 0x04002363 RID: 9059
	private float time = 1f;

	// Token: 0x04002364 RID: 9060
	private float changeTime = 0.5f;

	// Token: 0x04002365 RID: 9061
	private float currentTime;

	// Token: 0x04002366 RID: 9062
	private float mLastViewDistance = 0.7f;

	// Token: 0x04002367 RID: 9063
	private bool mInitFlag;

	// Token: 0x04002368 RID: 9064
	private PlayerData mPlayerData;

	// Token: 0x04002369 RID: 9065
	public Camera CurCamera;

	// Token: 0x0400236A RID: 9066
	private RaycastHit rayCastHitInfo;

	// Token: 0x0400236B RID: 9067
	private float tempDistance;

	// Token: 0x0400236C RID: 9068
	private float lastScale;

	// Token: 0x0400236D RID: 9069
	private float tempScale;

	// Token: 0x0400236E RID: 9070
	private bool hitFlag;

	// Token: 0x0400236F RID: 9071
	private float preCamDistance = 3f;

	// Token: 0x04002370 RID: 9072
	private int WALL_LAYER_VAL = 26;

	// Token: 0x04002371 RID: 9073
	public float LookHeight = 1.2f;

	// Token: 0x04002372 RID: 9074
	private vp_Timer.Handle enableCamHandle = new vp_Timer.Handle();

	// Token: 0x04002373 RID: 9075
	private CameraController.OnCamMoveOverDelegate onMoveOver;

	// Token: 0x04002374 RID: 9076
	public float moveForwardDis = 2f;

	// Token: 0x04002375 RID: 9077
	public float moveUp = 1.5f;

	// Token: 0x04002376 RID: 9078
	public float moveRight = 1f;

	// Token: 0x04002377 RID: 9079
	public float LookUp = 1.3f;

	// Token: 0x04002378 RID: 9080
	public float LookRight = -1f;

	// Token: 0x04002379 RID: 9081
	private List<CamRockInfo> mCamRockInfoList = new List<CamRockInfo>();

	// Token: 0x0400237A RID: 9082
	private Vector3 mTempRockPos;

	// Token: 0x0400237B RID: 9083
	private Quaternion mTempRockRot;

	// Token: 0x0200083B RID: 2107
	public enum CAMERAVIEWSTATE
	{
		// Token: 0x0400237D RID: 9085
		FIXED,
		// Token: 0x0400237E RID: 9086
		FREE,
		// Token: 0x0400237F RID: 9087
		FIXED_2_FREE,
		// Token: 0x04002380 RID: 9088
		FREE_2_FIXED,
		// Token: 0x04002381 RID: 9089
		FIXED_3D,
		// Token: 0x04002382 RID: 9090
		FREE_2_FIXED_3D,
		// Token: 0x04002383 RID: 9091
		FIXED_3D_2_FREE,
		// Token: 0x04002384 RID: 9092
		FEXED_2_FIXED_3D,
		// Token: 0x04002385 RID: 9093
		FIXED_3D_2_FIXED
	}

	// Token: 0x02000AE2 RID: 2786
	// (Invoke) Token: 0x06005011 RID: 20497
	public delegate void OnCamMoveOverDelegate();

	// Token: 0x02000AE3 RID: 2787
	// (Invoke) Token: 0x06005015 RID: 20501
	public delegate void OnLerpComplete();
}
