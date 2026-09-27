using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x000076C0 File Offset: 0x000058C0
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000076CC File Offset: 0x000058CC
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x000076EC File Offset: 0x000058EC
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00007724 File Offset: 0x00005924
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000775C File Offset: 0x0000595C
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00007784 File Offset: 0x00005984
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000077A4 File Offset: 0x000059A4
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000077C4 File Offset: 0x000059C4
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(this.functionName, base.gameObject, 1);
				i++;
			}
		}
		else
		{
			this.target.SendMessage(this.functionName, base.gameObject, 1);
		}
	}

	// Token: 0x04000127 RID: 295
	public GameObject target;

	// Token: 0x04000128 RID: 296
	public string functionName;

	// Token: 0x04000129 RID: 297
	public UIButtonMessage.Trigger trigger;

	// Token: 0x0400012A RID: 298
	public bool includeChildren;

	// Token: 0x0400012B RID: 299
	private bool mStarted;

	// Token: 0x02000045 RID: 69
	public enum Trigger
	{
		// Token: 0x0400012D RID: 301
		OnClick,
		// Token: 0x0400012E RID: 302
		OnMouseOver,
		// Token: 0x0400012F RID: 303
		OnMouseOut,
		// Token: 0x04000130 RID: 304
		OnPress,
		// Token: 0x04000131 RID: 305
		OnRelease,
		// Token: 0x04000132 RID: 306
		OnDoubleClick
	}
}
