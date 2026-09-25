using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A7B RID: 2683
public class UIStayEvent : MonoBehaviour
{
	// Token: 0x06004E0C RID: 19980 RVA: 0x001AACC0 File Offset: 0x001A8EC0
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.time = Time.realtimeSinceStartup + 1f;
			this.startTime = this.time;
		}
		else
		{
			this.click = (this.startTime < Time.realtimeSinceStartup);
			this.time = -1f;
		}
	}

	// Token: 0x06004E0D RID: 19981 RVA: 0x001AAD14 File Offset: 0x001A8F14
	private void OnClick()
	{
		if (!this.click)
		{
			EventDelegate.Execute(this.onClick);
		}
	}

	// Token: 0x06004E0E RID: 19982 RVA: 0x001AAD2C File Offset: 0x001A8F2C
	private void Update()
	{
		if (this.time > 0f && Time.realtimeSinceStartup > this.time)
		{
			EventDelegate.Execute(this.onStay);
			this.time = Time.realtimeSinceStartup + 0.2f;
		}
	}

	// Token: 0x04003C9B RID: 15515
	public static UIEventTrigger current;

	// Token: 0x04003C9C RID: 15516
	public List<EventDelegate> onStay = new List<EventDelegate>();

	// Token: 0x04003C9D RID: 15517
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04003C9E RID: 15518
	private bool press;

	// Token: 0x04003C9F RID: 15519
	private float time = -1f;

	// Token: 0x04003CA0 RID: 15520
	public bool click;

	// Token: 0x04003CA1 RID: 15521
	private float startTime;
}
