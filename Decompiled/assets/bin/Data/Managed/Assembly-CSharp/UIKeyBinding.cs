using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	// Token: 0x060001A1 RID: 417 RVA: 0x0000AFA0 File Offset: 0x000091A0
	private void Start()
	{
		UIInput component = base.GetComponent<UIInput>();
		this.mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000AFE4 File Offset: 0x000091E4
	private void OnSubmit()
	{
		if (UICamera.currentKey == this.keyCode && this.IsModifierActive())
		{
			this.mIgnoreUp = true;
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000B014 File Offset: 0x00009214
	private bool IsModifierActive()
	{
		if (this.modifier == UIKeyBinding.Modifier.None)
		{
			return true;
		}
		if (this.modifier == UIKeyBinding.Modifier.Alt)
		{
			if (Input.GetKey(308) || Input.GetKey(307))
			{
				return true;
			}
		}
		else if (this.modifier == UIKeyBinding.Modifier.Control)
		{
			if (Input.GetKey(306) || Input.GetKey(305))
			{
				return true;
			}
		}
		else if (this.modifier == UIKeyBinding.Modifier.Shift && (Input.GetKey(304) || Input.GetKey(303)))
		{
			return true;
		}
		return false;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000B0C0 File Offset: 0x000092C0
	private void Update()
	{
		if (this.keyCode == null || !this.IsModifierActive())
		{
			return;
		}
		if (this.action == UIKeyBinding.Action.PressAndClick)
		{
			if (UICamera.inputHasFocus)
			{
				return;
			}
			UICamera.currentTouch = UICamera.controller;
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			UICamera.currentTouch.current = base.gameObject;
			if (Input.GetKeyDown(this.keyCode))
			{
				UICamera.Notify(base.gameObject, "OnPress", true);
			}
			if (Input.GetKeyUp(this.keyCode))
			{
				UICamera.Notify(base.gameObject, "OnPress", false);
				UICamera.Notify(base.gameObject, "OnClick", null);
			}
			UICamera.currentTouch.current = null;
		}
		else if (this.action == UIKeyBinding.Action.Select && Input.GetKeyUp(this.keyCode))
		{
			if (this.mIsInput)
			{
				if (!this.mIgnoreUp && !UICamera.inputHasFocus)
				{
					UICamera.selectedObject = base.gameObject;
				}
				this.mIgnoreUp = false;
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x040001D3 RID: 467
	public KeyCode keyCode;

	// Token: 0x040001D4 RID: 468
	public UIKeyBinding.Modifier modifier;

	// Token: 0x040001D5 RID: 469
	public UIKeyBinding.Action action;

	// Token: 0x040001D6 RID: 470
	private bool mIgnoreUp;

	// Token: 0x040001D7 RID: 471
	private bool mIsInput;

	// Token: 0x0200005D RID: 93
	public enum Action
	{
		// Token: 0x040001D9 RID: 473
		PressAndClick,
		// Token: 0x040001DA RID: 474
		Select
	}

	// Token: 0x0200005E RID: 94
	public enum Modifier
	{
		// Token: 0x040001DC RID: 476
		None,
		// Token: 0x040001DD RID: 477
		Shift,
		// Token: 0x040001DE RID: 478
		Control,
		// Token: 0x040001DF RID: 479
		Alt
	}
}
