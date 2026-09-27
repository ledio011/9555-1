using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x06000171 RID: 369 RVA: 0x0000A0D8 File Offset: 0x000082D8
	private void OnHover(bool isOver)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (isOver)
		{
			EventDelegate.Execute(this.onHoverOver);
		}
		else
		{
			EventDelegate.Execute(this.onHoverOut);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000A124 File Offset: 0x00008324
	private void OnPress(bool pressed)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (pressed)
		{
			EventDelegate.Execute(this.onPress);
		}
		else
		{
			EventDelegate.Execute(this.onRelease);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000A170 File Offset: 0x00008370
	private void OnSelect(bool selected)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (selected)
		{
			EventDelegate.Execute(this.onSelect);
		}
		else
		{
			EventDelegate.Execute(this.onDeselect);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000A1BC File Offset: 0x000083BC
	private void OnClick()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000A1F4 File Offset: 0x000083F4
	private void OnDoubleClick()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDoubleClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000A22C File Offset: 0x0000842C
	private void OnDragOver(GameObject go)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOver);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000A264 File Offset: 0x00008464
	private void OnDragOut(GameObject go)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOut);
		UIEventTrigger.current = null;
	}

	// Token: 0x0400019E RID: 414
	public static UIEventTrigger current;

	// Token: 0x0400019F RID: 415
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x040001A0 RID: 416
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x040001A1 RID: 417
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x040001A2 RID: 418
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x040001A3 RID: 419
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x040001A4 RID: 420
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x040001A5 RID: 421
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x040001A6 RID: 422
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x040001A7 RID: 423
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x040001A8 RID: 424
	public List<EventDelegate> onDragOut = new List<EventDelegate>();
}
