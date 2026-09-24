using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[AddComponentMenu("FingerGestures/Finger Events/Finger Hover Detector")]
public class FingerHoverDetector : FingerEventDetector<FingerHoverEvent>
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000096 RID: 150 RVA: 0x00003E44 File Offset: 0x00002044
	// (remove) Token: 0x06000097 RID: 151 RVA: 0x00003E60 File Offset: 0x00002060
	public event FingerEventDetector<FingerHoverEvent>.FingerEventHandler OnFingerHover;

	// Token: 0x06000098 RID: 152 RVA: 0x00003E7C File Offset: 0x0000207C
	protected override void Start()
	{
		base.Start();
		if (!this.Raycaster)
		{
			Debug.LogWarning("FingerHoverDetector component on " + base.name + " has no Raycaster set.");
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003EBC File Offset: 0x000020BC
	private bool FireEvent(FingerHoverEvent e, FingerHoverPhase phase)
	{
		e.Name = this.MessageName;
		e.Phase = phase;
		if (this.OnFingerHover != null)
		{
			this.OnFingerHover(e);
		}
		base.TrySendMessage(e);
		return true;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003EFC File Offset: 0x000020FC
	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		FingerHoverEvent @event = base.GetEvent(finger);
		GameObject previousSelection = @event.PreviousSelection;
		GameObject gameObject = (!finger.IsDown) ? null : base.PickObject(finger.Position);
		if (gameObject != previousSelection)
		{
			if (previousSelection)
			{
				this.FireEvent(@event, FingerHoverPhase.Exit);
			}
			if (gameObject)
			{
				@event.Selection = gameObject;
				@event.Raycast = base.Raycast;
				this.FireEvent(@event, FingerHoverPhase.Enter);
			}
		}
		@event.PreviousSelection = gameObject;
	}

	// Token: 0x04000062 RID: 98
	public string MessageName = "OnFingerHover";
}
