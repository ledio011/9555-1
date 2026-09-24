using System;
using UnityEngine;

// Token: 0x020008D1 RID: 2257
public class CarControllerRootLogic : SingletonUnity<CarControllerRootLogic>
{
	// Token: 0x06003CCA RID: 15562 RVA: 0x0010C0E8 File Offset: 0x0010A2E8
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003CCB RID: 15563 RVA: 0x0010C0F4 File Offset: 0x0010A2F4
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06003CCC RID: 15564 RVA: 0x0010C114 File Offset: 0x0010A314
	private new void Awake()
	{
		base.Awake();
		UIEventListener accelBtnListener = this.AccelBtnListener;
		accelBtnListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(accelBtnListener.onPress, new UIEventListener.BoolDelegate(this.OnPressAccBtn));
		UIEventListener brakeBtnListener = this.BrakeBtnListener;
		brakeBtnListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(brakeBtnListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBrakeBtn));
		UIEventListener leftBtnListener = this.LeftBtnListener;
		leftBtnListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(leftBtnListener.onPress, new UIEventListener.BoolDelegate(this.OnPressLeftBtn));
		UIEventListener rightBtnListener = this.RightBtnListener;
		rightBtnListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(rightBtnListener.onPress, new UIEventListener.BoolDelegate(this.OnPressRightBtn));
		this.mCarSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
	}

	// Token: 0x06003CCD RID: 15565 RVA: 0x0010C1D4 File Offset: 0x0010A3D4
	private void OnEnable()
	{
		if (this.mCarSceneManager.IsTutorialScene())
		{
			this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
		}
		else
		{
			this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
		}
	}

	// Token: 0x06003CCE RID: 15566 RVA: 0x0010C21C File Offset: 0x0010A41C
	public void Reset()
	{
		this.mIsUsingGravity = (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.StreetRacingMode == 0);
		if (this.mIsUsingGravity)
		{
			NGUITools.SetActive(this.LeftBtnListener.gameObject, false);
			NGUITools.SetActive(this.RightBtnListener.gameObject, false);
			this.AccelBtnListener.transform.localPosition = new Vector3(-105f, 95f, 0f);
			this.BrakeBtnListener.transform.parent = this.LeftAnchorRoot;
			this.BrakeBtnListener.transform.localPosition = new Vector3(105f, 95f, 0f);
		}
		else
		{
			NGUITools.SetActive(this.LeftBtnListener.gameObject, true);
			NGUITools.SetActive(this.RightBtnListener.gameObject, true);
			this.AccelBtnListener.transform.localPosition = new Vector3(-64.99997f, 141.79f, 0f);
			this.BrakeBtnListener.transform.parent = this.RightAnchorRoot;
			this.BrakeBtnListener.transform.localPosition = new Vector3(-185f, 60f, 0f);
		}
	}

	// Token: 0x06003CCF RID: 15567 RVA: 0x0010C354 File Offset: 0x0010A554
	public void OnPressLeftBtn(GameObject obj, bool isPress)
	{
		if (!this.mCarSceneManager.IsMissionStart)
		{
			return;
		}
		if (this.mPlayerCar == null)
		{
			if (this.mCarSceneManager.IsTutorialScene())
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
			}
			else
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
			}
			if (this.mPlayerCar == null)
			{
				return;
			}
		}
		this.mPlayerCar.OnPressLeftBtn(isPress);
	}

	// Token: 0x06003CD0 RID: 15568 RVA: 0x0010C3DC File Offset: 0x0010A5DC
	public void OnPressRightBtn(GameObject obj, bool isPress)
	{
		if (!this.mCarSceneManager.IsMissionStart)
		{
			return;
		}
		if (this.mPlayerCar == null)
		{
			if (this.mCarSceneManager.IsTutorialScene())
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
			}
			else
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
			}
			if (this.mPlayerCar == null)
			{
				return;
			}
		}
		this.mPlayerCar.OnPressRightBtn(isPress);
	}

	// Token: 0x06003CD1 RID: 15569 RVA: 0x0010C464 File Offset: 0x0010A664
	public void OnPressAccBtn(GameObject obj, bool isPress)
	{
		if (!this.mCarSceneManager.IsMissionStart)
		{
			return;
		}
		if (this.mPlayerCar == null)
		{
			if (this.mCarSceneManager.IsTutorialScene())
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
			}
			else
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
			}
			if (this.mPlayerCar == null)
			{
				return;
			}
		}
		this.mPlayerCar.OnPressAccelBtn(isPress);
	}

	// Token: 0x06003CD2 RID: 15570 RVA: 0x0010C4EC File Offset: 0x0010A6EC
	public void OnPressBrakeBtn(GameObject obj, bool isPress)
	{
		if (!this.mCarSceneManager.IsMissionStart)
		{
			return;
		}
		if (this.mPlayerCar == null)
		{
			if (this.mCarSceneManager.IsTutorialScene())
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
			}
			else
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
			}
			if (this.mPlayerCar == null)
			{
				return;
			}
		}
		this.mPlayerCar.OnPressBrakeBtn(isPress);
	}

	// Token: 0x06003CD3 RID: 15571 RVA: 0x0010C574 File Offset: 0x0010A774
	public void OnClickResetBtn()
	{
		if (!this.mCarSceneManager.IsMissionStart)
		{
			return;
		}
		if (Time.time - this.lastClickTime < 1f)
		{
			return;
		}
		this.lastClickTime = Time.time;
		if (this.mCarSceneManager.CurrentMapInofData.MapType == MAPTYPE.CAR_CHASE_COPY)
		{
			(this.mCarSceneManager as CarChaseSceneManager).ResetPlayerCarToLastPoint();
		}
		else if (this.mCarSceneManager.IsTutorialScene())
		{
			this.ResetPlayerCarPos();
		}
	}

	// Token: 0x06003CD4 RID: 15572 RVA: 0x0010C5F8 File Offset: 0x0010A7F8
	public void ResetPlayerCarPos()
	{
		if (this.mPlayerCar == null)
		{
			if (this.mCarSceneManager.IsTutorialScene())
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
			}
			else
			{
				this.mPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
			}
			if (this.mPlayerCar == null)
			{
				return;
			}
		}
		this.mPlayerCar.rigidbody.velocity = Vector3.zero;
		this.mPlayerCar.rigidbody.angularVelocity = Vector3.zero;
		bool flag = false;
		float hitHeight = SceneManager.GetHitHeight(this.mPlayerCar.transform.position, out flag);
		if (flag)
		{
			this.mPlayerCar.transform.position = new Vector3(this.mPlayerCar.transform.position.x, hitHeight, this.mPlayerCar.transform.position.z) + Vector3.up;
		}
		else
		{
			Vector3 birthPosVector = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.BirthPosVector3;
			this.mPlayerCar.transform.position = new Vector3(birthPosVector.x, SceneManager.GetHitHeight(birthPosVector), birthPosVector.z) + Vector3.up;
		}
		this.mPlayerCar.transform.eulerAngles = new Vector3(0f, this.mPlayerCar.transform.eulerAngles.y, 0f);
	}

	// Token: 0x06003CD5 RID: 15573 RVA: 0x0010C784 File Offset: 0x0010A984
	public void TutorialShowAllBtn()
	{
		NGUITools.SetActive(this.ResetBtnRoot, true);
		NGUITools.SetActive(this.LeftBtnListener.gameObject, true);
		NGUITools.SetActive(this.RightBtnListener.gameObject, true);
		NGUITools.SetActive(this.BrakeBtnListener.gameObject, true);
	}

	// Token: 0x04002822 RID: 10274
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002823 RID: 10275
	public UIEventListener AccelBtnListener;

	// Token: 0x04002824 RID: 10276
	public UIEventListener BrakeBtnListener;

	// Token: 0x04002825 RID: 10277
	public UIEventListener LeftBtnListener;

	// Token: 0x04002826 RID: 10278
	public UIEventListener RightBtnListener;

	// Token: 0x04002827 RID: 10279
	public Transform LeftAnchorRoot;

	// Token: 0x04002828 RID: 10280
	public Transform RightAnchorRoot;

	// Token: 0x04002829 RID: 10281
	public GameObject ResetBtnRoot;

	// Token: 0x0400282A RID: 10282
	private SceneManager mCarSceneManager;

	// Token: 0x0400282B RID: 10283
	private ObjPlayerCar mPlayerCar;

	// Token: 0x0400282C RID: 10284
	private bool mIsUsingGravity;

	// Token: 0x0400282D RID: 10285
	private float lastClickTime;
}
