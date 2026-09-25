using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[AddComponentMenu("FingerGestures/Finger Events/Finger Up Detector")]
public class FingerUpDetector : FingerEventDetector<FingerUpEvent>
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060000AC RID: 172 RVA: 0x000042E8 File Offset: 0x000024E8
	// (remove) Token: 0x060000AD RID: 173 RVA: 0x00004304 File Offset: 0x00002504
	public event FingerEventDetector<FingerUpEvent>.FingerEventHandler OnFingerUp;

	// Token: 0x060000AE RID: 174 RVA: 0x00004320 File Offset: 0x00002520
	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		if (!finger.IsDown && finger.WasDown)
		{
			FingerUpEvent @event = base.GetEvent(finger);
			@event.Name = this.MessageName;
			@event.TimeHeldDown = Mathf.Max(0f, Time.time - finger.StarTime);
			base.UpdateSelection(@event);
			if (this.OnFingerUp != null)
			{
				this.OnFingerUp(@event);
				return;
			}
			base.TrySendMessage(@event);
		}
	}

	// Token: 0x04000076 RID: 118
	public string MessageName = "OnFingerUp";
}
