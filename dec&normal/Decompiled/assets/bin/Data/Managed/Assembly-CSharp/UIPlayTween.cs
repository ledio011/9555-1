using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000064 RID: 100
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Tween")]
public class UIPlayTween : MonoBehaviour
{
	// Token: 0x060001CC RID: 460 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
	private void Awake()
	{
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000BF24 File Offset: 0x0000A124
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000BF58 File Offset: 0x0000A158
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

	// Token: 0x060001CF RID: 463 RVA: 0x0000C01C File Offset: 0x0000A21C
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000C054 File Offset: 0x0000A254
	private void OnHover(bool isOver)
	{
		if (base.enabled && (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver)))
		{
			this.mActivated = (isOver && this.trigger == Trigger.OnHover);
			this.Play(isOver);
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000C0BC File Offset: 0x0000A2BC
	private void OnDragOut()
	{
		if (base.enabled && this.mActivated)
		{
			this.mActivated = false;
			this.Play(false);
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.mActivated = (isPressed && this.trigger == Trigger.OnPress);
			this.Play(isPressed);
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000C158 File Offset: 0x0000A358
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000C178 File Offset: 0x0000A378
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000C19C File Offset: 0x0000A39C
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.mActivated = (isSelected && this.trigger == Trigger.OnSelect);
			this.Play(isSelected);
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000C208 File Offset: 0x0000A408
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value);
		}
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000C284 File Offset: 0x0000A484
	private void Update()
	{
		if (this.disableWhenFinished != DisableCondition.DoNotDisable && this.mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (uitweener.enabled)
					{
						flag = false;
						break;
					}
					if (uitweener.direction != (Direction)this.disableWhenFinished)
					{
						flag2 = false;
					}
				}
				i++;
			}
			if (flag)
			{
				if (flag2)
				{
					NGUITools.SetActive(this.tweenTarget, false);
				}
				this.mTweens = null;
			}
		}
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000C330 File Offset: 0x0000A530
	public void Play(bool forward)
	{
		this.mActive = 0;
		GameObject gameObject = (!(this.tweenTarget == null)) ? this.tweenTarget : base.gameObject;
		if (!NGUITools.GetActive(gameObject))
		{
			if (this.ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = ((!this.includeChildren) ? gameObject.GetComponents<UITweener>() : gameObject.GetComponentsInChildren<UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != DisableCondition.DoNotDisable)
			{
				NGUITools.SetActive(this.tweenTarget, false);
			}
		}
		else
		{
			bool flag = false;
			if (this.playDirection == Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (!flag && !NGUITools.GetActive(gameObject))
					{
						flag = true;
						NGUITools.SetActive(gameObject, true);
					}
					this.mActive++;
					if (this.playDirection == Direction.Toggle)
					{
						EventDelegate.Add(uitweener.onFinished, new EventDelegate.Callback(this.OnFinished), true);
						uitweener.Toggle();
					}
					else
					{
						if (this.resetOnPlay || (this.resetIfDisabled && !uitweener.enabled))
						{
							uitweener.ResetToBeginning();
						}
						EventDelegate.Add(uitweener.onFinished, new EventDelegate.Callback(this.OnFinished), true);
						uitweener.Play(forward);
					}
				}
				i++;
			}
		}
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
	private void OnFinished()
	{
		if (--this.mActive == 0 && UIPlayTween.current == null)
		{
			UIPlayTween.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, 1);
			}
			this.eventReceiver = null;
			UIPlayTween.current = null;
		}
	}

	// Token: 0x04000209 RID: 521
	public static UIPlayTween current;

	// Token: 0x0400020A RID: 522
	public GameObject tweenTarget;

	// Token: 0x0400020B RID: 523
	public int tweenGroup;

	// Token: 0x0400020C RID: 524
	public Trigger trigger;

	// Token: 0x0400020D RID: 525
	public Direction playDirection = Direction.Forward;

	// Token: 0x0400020E RID: 526
	public bool resetOnPlay;

	// Token: 0x0400020F RID: 527
	public bool resetIfDisabled;

	// Token: 0x04000210 RID: 528
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04000211 RID: 529
	public DisableCondition disableWhenFinished;

	// Token: 0x04000212 RID: 530
	public bool includeChildren;

	// Token: 0x04000213 RID: 531
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000214 RID: 532
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000215 RID: 533
	[SerializeField]
	[HideInInspector]
	private string callWhenFinished;

	// Token: 0x04000216 RID: 534
	private UITweener[] mTweens;

	// Token: 0x04000217 RID: 535
	private bool mStarted;

	// Token: 0x04000218 RID: 536
	private int mActive;

	// Token: 0x04000219 RID: 537
	private bool mActivated;
}
