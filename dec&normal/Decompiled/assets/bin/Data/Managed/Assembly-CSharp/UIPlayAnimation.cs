using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000061 RID: 97
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000B668 File Offset: 0x00009868
	private bool dualState
	{
		get
		{
			return this.trigger == Trigger.OnPress || this.trigger == Trigger.OnHover;
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000B684 File Offset: 0x00009884
	private void Awake()
	{
		UIButton component = base.GetComponent<UIButton>();
		if (component != null)
		{
			this.dragHighlight = component.dragHighlight;
		}
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000B6E0 File Offset: 0x000098E0
	private void Start()
	{
		this.mStarted = true;
		if (this.target == null && this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (this.animator != null)
		{
			if (this.animator.enabled)
			{
				this.animator.enabled = false;
			}
			return;
		}
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null && this.target.enabled)
		{
			this.target.enabled = false;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000B79C File Offset: 0x0000999C
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (this.trigger == Trigger.OnPress || this.trigger == Trigger.OnPressTrue)
			{
				this.mActivated = (UICamera.currentTouch.pressed == base.gameObject);
			}
			if (this.trigger == Trigger.OnHover || this.trigger == Trigger.OnHoverTrue)
			{
				this.mActivated = (UICamera.currentTouch.current == base.gameObject);
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000B860 File Offset: 0x00009A60
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000B898 File Offset: 0x00009A98
	private void OnHover(bool isOver)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
		{
			this.Play(isOver, this.dualState);
		}
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000B8F0 File Offset: 0x00009AF0
	private void OnPress(bool isPressed)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed))
		{
			this.Play(isPressed, this.dualState);
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000B948 File Offset: 0x00009B48
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000B968 File Offset: 0x00009B68
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000B998 File Offset: 0x00009B98
	private void OnSelect(bool isSelected)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected))
		{
			this.Play(isSelected, this.dualState);
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000B9F4 File Offset: 0x00009BF4
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value, this.dualState);
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000BA78 File Offset: 0x00009C78
	private void OnDragOver()
	{
		if (base.enabled && this.dualState)
		{
			if (UICamera.currentTouch.dragged == base.gameObject)
			{
				this.Play(true, true);
			}
			else if (this.dragHighlight && this.trigger == Trigger.OnPress)
			{
				this.Play(true, true);
			}
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000BAE4 File Offset: 0x00009CE4
	private void OnDragOut()
	{
		if (base.enabled && this.dualState && UICamera.hoveredObject != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000BB24 File Offset: 0x00009D24
	private void OnDrop(GameObject go)
	{
		if (base.enabled && this.trigger == Trigger.OnPress && UICamera.currentTouch.dragged != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000BB6C File Offset: 0x00009D6C
	public void Play(bool forward)
	{
		this.Play(forward, true);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000BB78 File Offset: 0x00009D78
	public void Play(bool forward, bool onlyIfDifferent)
	{
		if (this.target || this.animator)
		{
			if (onlyIfDifferent)
			{
				if (this.mActivated == forward)
				{
					return;
				}
				this.mActivated = forward;
			}
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			Direction direction = (Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = (!this.target) ? ActiveAnimation.Play(this.animator, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished) : ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation != null)
			{
				if (this.resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < this.onFinished.Count; i++)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
			}
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000BCA4 File Offset: 0x00009EA4
	private void OnFinished()
	{
		if (UIPlayAnimation.current == null)
		{
			UIPlayAnimation.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, 1);
			}
			this.eventReceiver = null;
			UIPlayAnimation.current = null;
		}
	}

	// Token: 0x040001ED RID: 493
	public static UIPlayAnimation current;

	// Token: 0x040001EE RID: 494
	public Animation target;

	// Token: 0x040001EF RID: 495
	public Animator animator;

	// Token: 0x040001F0 RID: 496
	public string clipName;

	// Token: 0x040001F1 RID: 497
	public Trigger trigger;

	// Token: 0x040001F2 RID: 498
	public Direction playDirection = Direction.Forward;

	// Token: 0x040001F3 RID: 499
	public bool resetOnPlay;

	// Token: 0x040001F4 RID: 500
	public bool clearSelection;

	// Token: 0x040001F5 RID: 501
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040001F6 RID: 502
	public DisableCondition disableWhenFinished;

	// Token: 0x040001F7 RID: 503
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x040001F8 RID: 504
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x040001F9 RID: 505
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x040001FA RID: 506
	private bool mStarted;

	// Token: 0x040001FB RID: 507
	private bool mActivated;

	// Token: 0x040001FC RID: 508
	private bool dragHighlight;
}
