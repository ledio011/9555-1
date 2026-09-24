using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A44 RID: 2628
public class ScreenBottomBtnSmall : MonoBehaviour
{
	// Token: 0x17000FCF RID: 4047
	// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x0019FBD4 File Offset: 0x0019DDD4
	// (set) Token: 0x06004CA8 RID: 19624 RVA: 0x0019FBDC File Offset: 0x0019DDDC
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

	// Token: 0x06004CA9 RID: 19625 RVA: 0x0019FBE8 File Offset: 0x0019DDE8
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

	// Token: 0x06004CAA RID: 19626 RVA: 0x0019FC48 File Offset: 0x0019DE48
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
			else if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(this.DelaySet());
			}
		}
	}

	// Token: 0x06004CAB RID: 19627 RVA: 0x0019FCF4 File Offset: 0x0019DEF4
	private IEnumerator DelaySet()
	{
		yield return null;
		if (this.camCtl != null)
		{
			this.camCtl.IsCamCanUse = false;
		}
		yield break;
	}

	// Token: 0x06004CAC RID: 19628 RVA: 0x0019FD10 File Offset: 0x0019DF10
	private void OnDisable()
	{
		if (this.camCtl != null)
		{
			this.camCtl.IsCamCanUse = false;
		}
	}

	// Token: 0x04003A4C RID: 14924
	private bool mLockBtn;

	// Token: 0x04003A4D RID: 14925
	private UIEventListener mEventListener;

	// Token: 0x04003A4E RID: 14926
	private CameraController camCtl;
}
