using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[AddComponentMenu("FingerGestures/Finger Events/Finger Down Detector")]
public class FingerDownDetector : FingerEventDetector<FingerDownEvent>
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x0600008F RID: 143 RVA: 0x00003D78 File Offset: 0x00001F78
	// (remove) Token: 0x06000090 RID: 144 RVA: 0x00003D94 File Offset: 0x00001F94
	public event FingerEventDetector<FingerDownEvent>.FingerEventHandler OnFingerDown;

	// Token: 0x06000091 RID: 145 RVA: 0x00003DB0 File Offset: 0x00001FB0
	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		if (finger.IsDown && !finger.WasDown)
		{
			FingerDownEvent @event = this.GetEvent(finger.Index);
			@event.Name = this.MessageName;
			base.UpdateSelection(@event);
			if (this.OnFingerDown != null)
			{
				this.OnFingerDown(@event);
			}
			base.TrySendMessage(@event);
		}
	}

	// Token: 0x0400005A RID: 90
	public string MessageName = "OnFingerDown";
}
