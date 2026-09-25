using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008A8 RID: 2216
public class TestScreenBottomBtn : MonoBehaviour
{
	// Token: 0x06003BBF RID: 15295 RVA: 0x001049F0 File Offset: 0x00102BF0
	private void Start()
	{
		if (this.mEventListener == null)
		{
			this.mEventListener = base.gameObject.AddComponent<UIEventListener>();
		}
		UIEventListener uieventListener = this.mEventListener;
		uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBottomBtn));
		UIEventListener uieventListener2 = this.mEventListener;
		uieventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener2.onDrag, new UIEventListener.VectorDelegate(this.OnDragBottomBtn));
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x00104A70 File Offset: 0x00102C70
	public void OnPressBottomBtn(GameObject btn, bool isPress)
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			if (isPress)
			{
				Singleton<ObjManager>.Instance.MainPlayer.CameraController.IsCamCanUse = isPress;
			}
			else if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(this.DelaySet());
			}
		}
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x00104AD0 File Offset: 0x00102CD0
	private IEnumerator DelaySet()
	{
		yield return 1;
		Singleton<ObjManager>.Instance.MainPlayer.CameraController.IsCamCanUse = false;
		yield break;
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x00104AE4 File Offset: 0x00102CE4
	public void OnDragBottomBtn(GameObject btn, Vector2 delta)
	{
	}

	// Token: 0x0400271A RID: 10010
	private UIEventListener mEventListener;
}
