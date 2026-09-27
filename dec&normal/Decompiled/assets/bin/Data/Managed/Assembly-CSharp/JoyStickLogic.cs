using System;
using UnityEngine;

// Token: 0x020009E7 RID: 2535
public class JoyStickLogic : SingletonUnity<JoyStickLogic>
{
	// Token: 0x06004815 RID: 18453 RVA: 0x001714E4 File Offset: 0x0016F6E4
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004816 RID: 18454 RVA: 0x001714F0 File Offset: 0x0016F6F0
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000FB9 RID: 4025
	// (get) Token: 0x06004817 RID: 18455 RVA: 0x00171510 File Offset: 0x0016F710
	public int TouchID
	{
		get
		{
			return this.mTouchID;
		}
	}

	// Token: 0x06004818 RID: 18456 RVA: 0x00171518 File Offset: 0x0016F718
	private void Start()
	{
	}

	// Token: 0x06004819 RID: 18457 RVA: 0x0017151C File Offset: 0x0016F71C
	private void OnEnable()
	{
		this.HidePic();
		this.mPressed = false;
		base.transform.localPosition = Vector3.zero;
	}

	// Token: 0x0600481A RID: 18458 RVA: 0x0017153C File Offset: 0x0016F73C
	public void HidePic()
	{
		int limitlevel = 10;
		FunctionData functionDataById = DataManager.GetFunctionDataById(4086.ToString());
		if (functionDataById != null)
		{
			limitlevel = functionDataById.Condition;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(limitlevel))
		{
			UnityVersionUtil.SetActiveRecursive(this.insideCircle.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.outSideCircle.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.insideCircle.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.outSideCircle.gameObject, true);
			this.outSideCircle.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x0600481B RID: 18459 RVA: 0x001715E0 File Offset: 0x0016F7E0
	public void ShowPic()
	{
		UnityVersionUtil.SetActiveRecursive(this.insideCircle.gameObject, true);
		UnityVersionUtil.SetActiveRecursive(this.outSideCircle.gameObject, true);
	}

	// Token: 0x17000FBA RID: 4026
	// (get) Token: 0x0600481C RID: 18460 RVA: 0x00171610 File Offset: 0x0016F810
	public bool JoyStickUse
	{
		get
		{
			return this.mPressed;
		}
	}

	// Token: 0x0600481D RID: 18461 RVA: 0x00171618 File Offset: 0x0016F818
	private void Update()
	{
		this.UpdateControler();
	}

	// Token: 0x0600481E RID: 18462 RVA: 0x00171620 File Offset: 0x0016F820
	private void StartMove()
	{
		if (this.tweenPosition == null)
		{
			this.tweenPosition = base.GetComponent<TweenPosition>();
		}
		this.tweenPosition.enabled = false;
		this.IconSprite.alpha = 1f;
		if (TutorialManager.CurStep == TUTORIAL_STEP.JOYSTICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x0600481F RID: 18463 RVA: 0x00171678 File Offset: 0x0016F878
	private void EndMove()
	{
		if (this.tweenPosition == null)
		{
			this.tweenPosition = base.GetComponent<TweenPosition>();
		}
		this.tweenPosition.from = base.transform.localPosition;
		this.tweenPosition.ResetToBeginning();
		this.tweenPosition.PlayForward();
		this.IconSprite.alpha = 0.5f;
	}

	// Token: 0x06004820 RID: 18464 RVA: 0x001716E0 File Offset: 0x0016F8E0
	private void OnPress(bool pressed)
	{
		if (this.thirdPersonController == null)
		{
			return;
		}
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (pressed)
			{
				if (!this.mPressed)
				{
					this.mTouchID = UICamera.currentTouchID;
					this.mPressed = true;
					Transform transform = UICamera.currentCamera.transform;
					this.mPlane = new Plane(transform.rotation * Vector3.back, UICamera.lastHit.point);
					Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
					float num = 0f;
					if (this.mPlane.Raycast(ray, ref num))
					{
						Vector3 point = ray.GetPoint(num);
						base.transform.position = point;
						this.outSideCircle.transform.position = point;
						this.mCenterPosition = base.transform.localPosition;
						this.mTouchOffset = point - base.transform.position;
						this.StartMove();
					}
					this.thirdPersonController.IsJoyStickPress = true;
					this.ShowPic();
				}
			}
			else if (this.mPressed && this.mTouchID == UICamera.currentTouchID)
			{
				this.thirdPersonController.IsJoyStickPress = false;
				this.mPressed = false;
				this.mTouchID = -1;
				this.EndMove();
				this.HidePic();
			}
		}
	}

	// Token: 0x06004821 RID: 18465 RVA: 0x0017186C File Offset: 0x0016FA6C
	private void OnDrag(Vector2 delta)
	{
		if (this.mPressed && this.mTouchID == UICamera.currentTouchID && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float num = 0f;
			if (this.mPlane.Raycast(ray, ref num))
			{
				Vector3 point = ray.GetPoint(num);
				this.UpdatePosition(point);
			}
		}
	}

	// Token: 0x06004822 RID: 18466 RVA: 0x00171900 File Offset: 0x0016FB00
	private void UpdatePosition(Vector3 pos)
	{
		Vector3 vector = pos - this.mTouchOffset;
		Vector3 vector2 = base.transform.parent.transform.worldToLocalMatrix.MultiplyPoint(vector);
		float num = Vector3.SqrMagnitude(vector2 - this.mCenterPosition);
		if (num > this.MaxRadius * this.MaxRadius)
		{
			float num2 = Mathf.Sqrt(num);
			vector2 = Vector3.Lerp(this.mCenterPosition, vector2, this.MaxRadius / num2);
		}
		base.transform.localPosition = vector2;
	}

	// Token: 0x06004823 RID: 18467 RVA: 0x00171988 File Offset: 0x0016FB88
	private void UpdateControler()
	{
		if (this.thirdPersonController == null)
		{
			if (!(Singleton<ObjManager>.Instance.MainPlayer != null))
			{
				return;
			}
			this.thirdPersonController = Singleton<ObjManager>.Instance.MainPlayer.ThirdPersonController;
		}
		if (this.thirdPersonController.IsJoyStickPress)
		{
			this.thirdPersonController.HorizonRaw = (base.transform.localPosition.y - this.mCenterPosition.y) / this.MaxRadius;
			this.thirdPersonController.VerticalRaw = (base.transform.localPosition.x - this.mCenterPosition.x) / this.MaxRadius;
		}
	}

	// Token: 0x06004824 RID: 18468 RVA: 0x00171A48 File Offset: 0x0016FC48
	private void OnDisable()
	{
		if (this.thirdPersonController != null)
		{
			this.thirdPersonController.IsJoyStickPress = false;
			this.thirdPersonController.HorizonRaw = 0f;
			this.thirdPersonController.VerticalRaw = 0f;
		}
	}

	// Token: 0x06004825 RID: 18469 RVA: 0x00171A88 File Offset: 0x0016FC88
	public void MoveOutScreen()
	{
		base.transform.localPosition = Vector3.zero;
		if (this.thirdPersonController != null)
		{
			this.thirdPersonController.IsJoyStickPress = false;
			this.thirdPersonController.IsMoving = false;
			this.thirdPersonController.HorizonRaw = 0f;
			this.thirdPersonController.VerticalRaw = 0f;
		}
		this.mPressed = false;
		this.mTouchID = -1;
		this.HidePic();
	}

	// Token: 0x04003577 RID: 13687
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003578 RID: 13688
	private Vector3 mCenterPosition = Vector3.zero;

	// Token: 0x04003579 RID: 13689
	public float MaxRadius = 100f;

	// Token: 0x0400357A RID: 13690
	private Vector3 mTouchOffset = Vector3.zero;

	// Token: 0x0400357B RID: 13691
	public GameObject insideCircle;

	// Token: 0x0400357C RID: 13692
	public GameObject outSideCircle;

	// Token: 0x0400357D RID: 13693
	private int mTouchID = -1;

	// Token: 0x0400357E RID: 13694
	private bool mPressed;

	// Token: 0x0400357F RID: 13695
	private Plane mPlane;

	// Token: 0x04003580 RID: 13696
	public UISprite IconSprite;

	// Token: 0x04003581 RID: 13697
	private TweenPosition tweenPosition;

	// Token: 0x04003582 RID: 13698
	private ThirdPersonController thirdPersonController;
}
