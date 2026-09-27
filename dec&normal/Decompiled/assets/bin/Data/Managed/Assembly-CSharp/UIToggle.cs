using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000076 RID: 118
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600025C RID: 604 RVA: 0x00010CD8 File Offset: 0x0000EED8
	// (set) Token: 0x0600025D RID: 605 RVA: 0x00010CE0 File Offset: 0x0000EEE0
	public bool value
	{
		get
		{
			return this.mIsActive;
		}
		set
		{
			if (this.group == 0 || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value);
			}
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600025E RID: 606 RVA: 0x00010D1C File Offset: 0x0000EF1C
	// (set) Token: 0x0600025F RID: 607 RVA: 0x00010D24 File Offset: 0x0000EF24
	[Obsolete("Use 'value' instead")]
	public bool isChecked
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00010D30 File Offset: 0x0000EF30
	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < UIToggle.list.size; i++)
		{
			UIToggle uitoggle = UIToggle.list[i];
			if (uitoggle != null && uitoggle.group == group && uitoggle.mIsActive)
			{
				return uitoggle;
			}
		}
		return null;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00010D8C File Offset: 0x0000EF8C
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00010D9C File Offset: 0x0000EF9C
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00010DAC File Offset: 0x0000EFAC
	private void Start()
	{
		if (this.startsChecked)
		{
			this.startsChecked = false;
			this.startsActive = true;
		}
		if (!Application.isPlaying)
		{
			if (this.checkSprite != null && this.activeSprite == null)
			{
				this.activeSprite = this.checkSprite;
				this.checkSprite = null;
			}
			if (this.checkAnimation != null && this.activeAnimation == null)
			{
				this.activeAnimation = this.checkAnimation;
				this.checkAnimation = null;
			}
			if (Application.isPlaying && this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!this.startsActive) ? 0f : 1f);
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				this.eventReceiver = null;
				this.functionName = null;
			}
		}
		else
		{
			this.mIsActive = !this.startsActive;
			this.mStarted = true;
			bool flag = this.instantTween;
			this.instantTween = true;
			this.Set(this.startsActive);
			this.instantTween = flag;
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00010EE0 File Offset: 0x0000F0E0
	private void OnClick()
	{
		if (base.enabled)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00010EFC File Offset: 0x0000F0FC
	private void Set(bool state)
	{
		if (!this.mStarted)
		{
			this.mIsActive = state;
			this.startsActive = state;
			if (this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!state) ? 0f : 1f);
			}
		}
		else if (this.mIsActive != state)
		{
			if (this.group != 0 && state)
			{
				int i = 0;
				int size = UIToggle.list.size;
				while (i < size)
				{
					UIToggle uitoggle = UIToggle.list[i];
					if (uitoggle != this && uitoggle.group == this.group)
					{
						uitoggle.Set(false);
					}
					if (UIToggle.list.size != size)
					{
						size = UIToggle.list.size;
						i = 0;
					}
					else
					{
						i++;
					}
				}
			}
			this.mIsActive = state;
			if (this.activeSprite != null)
			{
				if (this.instantTween)
				{
					this.activeSprite.alpha = ((!this.mIsActive) ? 0f : 1f);
				}
				else
				{
					TweenAlpha.Begin(this.activeSprite.gameObject, 0.15f, (!this.mIsActive) ? 0f : 1f);
				}
			}
			if (UIToggle.current == null)
			{
				UIToggle.current = this;
				if (EventDelegate.IsValid(this.onChange))
				{
					EventDelegate.Execute(this.onChange);
				}
				else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
				{
					this.eventReceiver.SendMessage(this.functionName, this.mIsActive, 1);
				}
				UIToggle.current = null;
			}
			if (this.activeAnimation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.activeAnimation, (!state) ? Direction.Reverse : Direction.Forward);
				if (this.instantTween)
				{
					activeAnimation.Finish();
				}
			}
		}
	}

	// Token: 0x040002A5 RID: 677
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x040002A6 RID: 678
	public static UIToggle current;

	// Token: 0x040002A7 RID: 679
	public int group;

	// Token: 0x040002A8 RID: 680
	public UIWidget activeSprite;

	// Token: 0x040002A9 RID: 681
	public Animation activeAnimation;

	// Token: 0x040002AA RID: 682
	public bool startsActive;

	// Token: 0x040002AB RID: 683
	public bool instantTween;

	// Token: 0x040002AC RID: 684
	public bool optionCanBeNone;

	// Token: 0x040002AD RID: 685
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040002AE RID: 686
	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	// Token: 0x040002AF RID: 687
	[SerializeField]
	[HideInInspector]
	private Animation checkAnimation;

	// Token: 0x040002B0 RID: 688
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x040002B1 RID: 689
	[SerializeField]
	[HideInInspector]
	private string functionName = "OnActivate";

	// Token: 0x040002B2 RID: 690
	[SerializeField]
	[HideInInspector]
	private bool startsChecked;

	// Token: 0x040002B3 RID: 691
	private bool mIsActive = true;

	// Token: 0x040002B4 RID: 692
	private bool mStarted;
}
