using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A43 RID: 2627
public class ScreenBottomBtn : SingletonUnity<ScreenBottomBtn>
{
	// Token: 0x06004C9E RID: 19614 RVA: 0x0019FA24 File Offset: 0x0019DC24
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004C9F RID: 19615 RVA: 0x0019FA30 File Offset: 0x0019DC30
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000FCE RID: 4046
	// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x0019FA50 File Offset: 0x0019DC50
	// (set) Token: 0x06004CA1 RID: 19617 RVA: 0x0019FA58 File Offset: 0x0019DC58
	public bool LockBtn
	{
		get
		{
			return this.mLockBtn;
		}
		set
		{
			this.mLockBtn = value;
		}
	}

	// Token: 0x06004CA2 RID: 19618 RVA: 0x0019FA64 File Offset: 0x0019DC64
	private void Start()
	{
		if (this.mEventListener == null)
		{
			this.mEventListener = base.gameObject.AddComponent<UIEventListener>();
		}
		UIEventListener uieventListener = this.mEventListener;
		uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBottomBtn));
		this.mLockBtn = false;
	}

	// Token: 0x06004CA3 RID: 19619 RVA: 0x0019FAC4 File Offset: 0x0019DCC4
	protected override void OnDestroy()
	{
		UIEventListener uieventListener = this.mEventListener;
		uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Remove(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBottomBtn));
		base.OnDestroy();
	}

	// Token: 0x06004CA4 RID: 19620 RVA: 0x0019FAF4 File Offset: 0x0019DCF4
	public void OnPressBottomBtn(GameObject btn, bool isPress)
	{
		if (this.mLockBtn)
		{
			return;
		}
		if (this.camCtl == null && Singleton<ObjManager>.Exists && Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			this.camCtl = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
		}
		if (this.camCtl != null)
		{
			if (isPress)
			{
				base.StopAllCoroutines();
				this.camCtl.IsCamCanUse = isPress;
			}
			else
			{
				if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
				{
					base.StartCoroutine(this.DelaySet());
				}
				if (TutorialManager.CurStep == TUTORIAL_STEP.MOVE_SCREEN)
				{
					this.CheckTutorialEvent();
				}
			}
		}
	}

	// Token: 0x06004CA5 RID: 19621 RVA: 0x0019FBB0 File Offset: 0x0019DDB0
	private IEnumerator DelaySet()
	{
		yield return null;
		if (this.camCtl != null)
		{
			this.camCtl.IsCamCanUse = false;
		}
		yield break;
	}

	// Token: 0x04003A48 RID: 14920
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003A49 RID: 14921
	private bool mLockBtn;

	// Token: 0x04003A4A RID: 14922
	private UIEventListener mEventListener;

	// Token: 0x04003A4B RID: 14923
	private CameraController camCtl;
}
