using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000179 RID: 377 RVA: 0x0000A2A4 File Offset: 0x000084A4
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, 1);
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000A2DC File Offset: 0x000084DC
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, 1);
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000A314 File Offset: 0x00008514
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", 1);
		}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000A344 File Offset: 0x00008544
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", 1);
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000A374 File Offset: 0x00008574
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, 1);
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000A3AC File Offset: 0x000085AC
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, 1);
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000A3E4 File Offset: 0x000085E4
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, 1);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000A420 File Offset: 0x00008620
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", 1);
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000A450 File Offset: 0x00008650
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, 1);
		}
	}

	// Token: 0x040001A9 RID: 425
	public GameObject target;

	// Token: 0x040001AA RID: 426
	public bool onHover;

	// Token: 0x040001AB RID: 427
	public bool onPress;

	// Token: 0x040001AC RID: 428
	public bool onClick;

	// Token: 0x040001AD RID: 429
	public bool onDoubleClick;

	// Token: 0x040001AE RID: 430
	public bool onSelect;

	// Token: 0x040001AF RID: 431
	public bool onDrag;

	// Token: 0x040001B0 RID: 432
	public bool onDrop;

	// Token: 0x040001B1 RID: 433
	public bool onSubmit;

	// Token: 0x040001B2 RID: 434
	public bool onScroll;
}
