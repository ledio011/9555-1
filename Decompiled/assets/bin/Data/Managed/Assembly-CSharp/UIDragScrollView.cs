using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	// Token: 0x0600015F RID: 351 RVA: 0x000098B4 File Offset: 0x00007AB4
	private void OnEnable()
	{
		this.mTrans = base.transform;
		if (this.scrollView == null && this.draggablePanel != null)
		{
			this.scrollView = this.draggablePanel;
			this.draggablePanel = null;
		}
		if (this.mStarted && (this.mAutoFind || this.mScroll == null))
		{
			this.FindScrollView();
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00009930 File Offset: 0x00007B30
	private void Start()
	{
		this.mStarted = true;
		this.FindScrollView();
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00009940 File Offset: 0x00007B40
	private void FindScrollView()
	{
		UIScrollView uiscrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
		if (this.scrollView == null)
		{
			this.scrollView = uiscrollView;
			this.mAutoFind = true;
		}
		else if (this.scrollView == uiscrollView)
		{
			this.mAutoFind = true;
		}
		this.mScroll = this.scrollView;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000099A4 File Offset: 0x00007BA4
	private void OnPress(bool pressed)
	{
		if (this.mAutoFind && this.mScroll != this.scrollView)
		{
			this.mScroll = this.scrollView;
			this.mAutoFind = false;
		}
		if (this.scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.scrollView.Press(pressed);
			if (!pressed && this.mAutoFind)
			{
				this.scrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
				this.mScroll = this.scrollView;
			}
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00009A4C File Offset: 0x00007C4C
	private void OnDrag(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Drag();
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00009A80 File Offset: 0x00007C80
	private void OnScroll(float delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Scroll(delta);
		}
	}

	// Token: 0x0400018A RID: 394
	public UIScrollView scrollView;

	// Token: 0x0400018B RID: 395
	[SerializeField]
	[HideInInspector]
	private UIScrollView draggablePanel;

	// Token: 0x0400018C RID: 396
	private Transform mTrans;

	// Token: 0x0400018D RID: 397
	private UIScrollView mScroll;

	// Token: 0x0400018E RID: 398
	private bool mAutoFind;

	// Token: 0x0400018F RID: 399
	private bool mStarted;
}
