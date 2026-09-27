using System;
using UnityEngine;

// Token: 0x0200083F RID: 2111
public class InputController : SingletonUnity<InputController>
{
	// Token: 0x17000EE7 RID: 3815
	// (get) Token: 0x06003626 RID: 13862 RVA: 0x000DE584 File Offset: 0x000DC784
	public PinchRecognizer PinchRecognizer
	{
		get
		{
			return this.mPinchRecognizer;
		}
	}

	// Token: 0x17000EE8 RID: 3816
	// (get) Token: 0x06003627 RID: 13863 RVA: 0x000DE58C File Offset: 0x000DC78C
	public DragRecognizer DragRecognizer
	{
		get
		{
			return this.mDragRecognizer;
		}
	}

	// Token: 0x17000EE9 RID: 3817
	// (get) Token: 0x06003628 RID: 13864 RVA: 0x000DE594 File Offset: 0x000DC794
	public LongPressRecognizer LongPressRecognizer
	{
		get
		{
			return this.mLongPressRecognizer;
		}
	}

	// Token: 0x17000EEA RID: 3818
	// (get) Token: 0x06003629 RID: 13865 RVA: 0x000DE59C File Offset: 0x000DC79C
	public SwipeRecognizer SwipeRecognizer
	{
		get
		{
			return this.mSwipeRecognizer;
		}
	}

	// Token: 0x17000EEB RID: 3819
	// (get) Token: 0x0600362A RID: 13866 RVA: 0x000DE5A4 File Offset: 0x000DC7A4
	public TapRecognizer TapRecognizer
	{
		get
		{
			return this.mTapRecognizer;
		}
	}

	// Token: 0x17000EEC RID: 3820
	// (get) Token: 0x0600362B RID: 13867 RVA: 0x000DE5AC File Offset: 0x000DC7AC
	public TwistRecognizer TwistRecognizer
	{
		get
		{
			return this.mTwistRecognizer;
		}
	}

	// Token: 0x17000EED RID: 3821
	// (get) Token: 0x0600362C RID: 13868 RVA: 0x000DE5B4 File Offset: 0x000DC7B4
	public FingerDownDetector FingerDownDetector
	{
		get
		{
			return this.mFingerDownDetector;
		}
	}

	// Token: 0x17000EEE RID: 3822
	// (get) Token: 0x0600362D RID: 13869 RVA: 0x000DE5BC File Offset: 0x000DC7BC
	public FingerHoverDetector FingerHoverDetector
	{
		get
		{
			return this.mFingerHoverDetector;
		}
	}

	// Token: 0x17000EEF RID: 3823
	// (get) Token: 0x0600362E RID: 13870 RVA: 0x000DE5C4 File Offset: 0x000DC7C4
	public FingerMotionDetector FingerMotionDetector
	{
		get
		{
			return this.mFingerMotionDetector;
		}
	}

	// Token: 0x17000EF0 RID: 3824
	// (get) Token: 0x0600362F RID: 13871 RVA: 0x000DE5CC File Offset: 0x000DC7CC
	public FingerUpDetector FingerUpDetector
	{
		get
		{
			return this.mFingerUpDetector;
		}
	}

	// Token: 0x17000EF1 RID: 3825
	// (get) Token: 0x06003630 RID: 13872 RVA: 0x000DE5D4 File Offset: 0x000DC7D4
	public ScreenRaycaster ScreenRaycaster
	{
		get
		{
			return this.mScreenRaycaster;
		}
	}

	// Token: 0x17000EF2 RID: 3826
	// (get) Token: 0x06003631 RID: 13873 RVA: 0x000DE5DC File Offset: 0x000DC7DC
	public ObjMainPlayer MainPlayer
	{
		get
		{
			if (this.mMainPlayer == null)
			{
				this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			}
			return this.mMainPlayer;
		}
	}

	// Token: 0x06003632 RID: 13874 RVA: 0x000DE608 File Offset: 0x000DC808
	private new void Awake()
	{
		base.Awake();
		this.Init();
	}

	// Token: 0x06003633 RID: 13875 RVA: 0x000DE618 File Offset: 0x000DC818
	private void Init()
	{
		if (this.mPinchRecognizer == null)
		{
			this.mPinchRecognizer = base.gameObject.AddComponent<PinchRecognizer>();
			this.mPinchRecognizer.OnGesture += new GestureRecognizerTS<PinchGesture>.GestureEventHandler(this.OnPinch);
			this.mPinchRecognizer.UseSendMessage = false;
		}
		if (this.mDragRecognizer == null)
		{
			this.mDragRecognizer = base.gameObject.AddComponent<DragRecognizer>();
			this.mDragRecognizer.OnGesture += new GestureRecognizerTS<DragGesture>.GestureEventHandler(this.OnDrag);
			this.mDragRecognizer.UseSendMessage = false;
			this.mDragRecognizer.MaxSimultaneousGestures = 2;
		}
		if (this.mLongPressRecognizer == null)
		{
			this.mLongPressRecognizer = base.gameObject.AddComponent<LongPressRecognizer>();
			this.mLongPressRecognizer.OnGesture += new GestureRecognizerTS<LongPressGesture>.GestureEventHandler(this.OnLongPress);
		}
		if (this.mSwipeRecognizer == null)
		{
			this.mSwipeRecognizer = base.gameObject.AddComponent<SwipeRecognizer>();
			this.mSwipeRecognizer.OnGesture += new GestureRecognizerTS<SwipeGesture>.GestureEventHandler(this.OnSwipe);
		}
		if (this.mTapRecognizer == null)
		{
			this.mTapRecognizer = base.gameObject.AddComponent<TapRecognizer>();
			this.mTapRecognizer.OnGesture += new GestureRecognizerTS<TapGesture>.GestureEventHandler(this.OnTap);
		}
		if (this.mTwistRecognizer == null)
		{
			this.mTwistRecognizer = base.gameObject.AddComponent<TwistRecognizer>();
			this.mTwistRecognizer.OnGesture += new GestureRecognizerTS<TwistGesture>.GestureEventHandler(this.OnTwist);
		}
		if (this.mFingerDownDetector == null)
		{
			this.mFingerDownDetector = base.gameObject.AddComponent<FingerDownDetector>();
			this.mFingerDownDetector.OnFingerDown += new FingerEventDetector<FingerDownEvent>.FingerEventHandler(this.OnFingerDown);
		}
		if (this.mFingerHoverDetector == null)
		{
			this.mFingerHoverDetector = base.gameObject.AddComponent<FingerHoverDetector>();
			this.mFingerHoverDetector.OnFingerHover += new FingerEventDetector<FingerHoverEvent>.FingerEventHandler(this.OnFingerHover);
		}
		if (this.mFingerMotionDetector == null)
		{
			this.mFingerMotionDetector = base.gameObject.AddComponent<FingerMotionDetector>();
			this.mFingerMotionDetector.OnFingerMove += new FingerEventDetector<FingerMotionEvent>.FingerEventHandler(this.OnFingerMove);
			this.mFingerMotionDetector.OnFingerStationary += new FingerEventDetector<FingerMotionEvent>.FingerEventHandler(this.OnFingerStationary);
		}
		if (this.mFingerUpDetector == null)
		{
			this.mFingerUpDetector = base.gameObject.AddComponent<FingerUpDetector>();
			this.mFingerUpDetector.OnFingerUp += new FingerEventDetector<FingerUpEvent>.FingerEventHandler(this.OnFingerUp);
		}
	}

	// Token: 0x06003634 RID: 13876 RVA: 0x000DE89C File Offset: 0x000DCA9C
	private void Start()
	{
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		this.mainCamera = Camera.main;
	}

	// Token: 0x06003635 RID: 13877 RVA: 0x000DE8D8 File Offset: 0x000DCAD8
	public void OnPinch(PinchGesture gesture)
	{
		if (this.MainPlayer != null)
		{
			this.MainPlayer.CameraController.OnPinchCamera(gesture);
		}
	}

	// Token: 0x06003636 RID: 13878 RVA: 0x000DE908 File Offset: 0x000DCB08
	public void OnDrag(DragGesture gesture)
	{
		if (gesture.Phase == 1 && this.dragFingerIndex == -1)
		{
			if (this.MainPlayer != null && this.MainPlayer.CameraController.IsCamCanUse)
			{
				this.dragFingerIndex = gesture.Fingers[0].Index;
			}
		}
		else if (gesture.Fingers[0].Index == this.dragFingerIndex)
		{
			if (gesture.Phase == 2)
			{
				if (this.MainPlayer != null)
				{
					this.MainPlayer.CameraController.OnDragCamera(gesture);
				}
			}
			else
			{
				this.dragFingerIndex = -1;
			}
		}
	}

	// Token: 0x06003637 RID: 13879 RVA: 0x000DE9C8 File Offset: 0x000DCBC8
	public void OnLongPress(LongPressGesture gesture)
	{
	}

	// Token: 0x06003638 RID: 13880 RVA: 0x000DE9CC File Offset: 0x000DCBCC
	public void OnSwipe(SwipeGesture gesture)
	{
	}

	// Token: 0x06003639 RID: 13881 RVA: 0x000DE9D0 File Offset: 0x000DCBD0
	public void OnTap(TapGesture gesture)
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
			if (this.mainCamera == null)
			{
				return;
			}
		}
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (this.mMainPlayer == null)
			{
				return;
			}
		}
		if (this.mMainPlayer.CameraController.IsCamCanUse)
		{
			Ray ray = this.mainCamera.ScreenPointToRay(gesture.Position);
			this.CastScreenRay(ray, gesture);
		}
	}

	// Token: 0x0600363A RID: 13882 RVA: 0x000DEA74 File Offset: 0x000DCC74
	public static bool IsMouseOverUI(Vector2 pos)
	{
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 inPos = UICamera.currentCamera.ScreenToWorldPoint(pos);
		GameObject gameObject = (!UICamera.Raycast(inPos, out raycastHit)) ? null : UICamera.lastHit.collider.gameObject;
		return gameObject != null;
	}

	// Token: 0x0600363B RID: 13883 RVA: 0x000DEAD0 File Offset: 0x000DCCD0
	private void CastScreenRay(Ray ray, TapGesture gesture)
	{
		int num = 67108864;
		num = ~num;
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(ray, ref raycastHit, 50f, num))
		{
			GameObject root = NGUITools.GetRoot(raycastHit.collider.gameObject);
			ObjCharacter component = root.GetComponent<ObjCharacter>();
			if (component != null)
			{
				if (component.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
				{
					Ray ray2;
					ray2..ctor(raycastHit.point + ray.direction * 0.01f, ray.direction);
					this.CastScreenRay(ray2, gesture);
					return;
				}
				this.mMainPlayer.SelectTarget(component);
				if (component.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
				{
					ObjOtherPlayer objOtherPlayer = component as ObjOtherPlayer;
					TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
					selectTargetBasicInfo.ResetInfo(objOtherPlayer.ServerId, objOtherPlayer.AttributeData.Level, objOtherPlayer.AttributeData.ComboValue, objOtherPlayer.AttributeData.Name, objOtherPlayer.Profession, 1, objOtherPlayer.AttributeData.GuildId, objOtherPlayer.AttributeData.GuildName, gesture.Position);
					HitOtherPLayerLogic.ShowMenu(HitType.HitOtherPlayer, selectTargetBasicInfo);
					CampTool.TipsCanSelectAttackPlayer(this.mMainPlayer, objOtherPlayer);
				}
			}
		}
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x000DEC08 File Offset: 0x000DCE08
	public void OnTwist(TwistGesture gesture)
	{
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x000DEC0C File Offset: 0x000DCE0C
	public void OnFingerDown(FingerDownEvent e)
	{
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x000DEC10 File Offset: 0x000DCE10
	public void OnFingerHover(FingerHoverEvent e)
	{
	}

	// Token: 0x0600363F RID: 13887 RVA: 0x000DEC14 File Offset: 0x000DCE14
	public void OnFingerMove(FingerMotionEvent e)
	{
	}

	// Token: 0x06003640 RID: 13888 RVA: 0x000DEC18 File Offset: 0x000DCE18
	public void OnFingerStationary(FingerMotionEvent e)
	{
	}

	// Token: 0x06003641 RID: 13889 RVA: 0x000DEC1C File Offset: 0x000DCE1C
	public void OnFingerUp(FingerUpEvent e)
	{
	}

	// Token: 0x0400239A RID: 9114
	private PinchRecognizer mPinchRecognizer;

	// Token: 0x0400239B RID: 9115
	private DragRecognizer mDragRecognizer;

	// Token: 0x0400239C RID: 9116
	private LongPressRecognizer mLongPressRecognizer;

	// Token: 0x0400239D RID: 9117
	private SwipeRecognizer mSwipeRecognizer;

	// Token: 0x0400239E RID: 9118
	private TapRecognizer mTapRecognizer;

	// Token: 0x0400239F RID: 9119
	private TwistRecognizer mTwistRecognizer;

	// Token: 0x040023A0 RID: 9120
	private FingerDownDetector mFingerDownDetector;

	// Token: 0x040023A1 RID: 9121
	private FingerHoverDetector mFingerHoverDetector;

	// Token: 0x040023A2 RID: 9122
	private FingerMotionDetector mFingerMotionDetector;

	// Token: 0x040023A3 RID: 9123
	private FingerUpDetector mFingerUpDetector;

	// Token: 0x040023A4 RID: 9124
	private ScreenRaycaster mScreenRaycaster;

	// Token: 0x040023A5 RID: 9125
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040023A6 RID: 9126
	private Camera mainCamera;

	// Token: 0x040023A7 RID: 9127
	private int dragFingerIndex = -1;
}
