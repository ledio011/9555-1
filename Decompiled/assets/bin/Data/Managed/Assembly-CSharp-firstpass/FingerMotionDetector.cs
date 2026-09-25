using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
[AddComponentMenu("FingerGestures/Finger Events/Finger Motion Detector")]
public class FingerMotionDetector : FingerEventDetector<FingerMotionEvent>
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x060000A2 RID: 162 RVA: 0x00004010 File Offset: 0x00002210
	// (remove) Token: 0x060000A3 RID: 163 RVA: 0x0000402C File Offset: 0x0000222C
	public event FingerEventDetector<FingerMotionEvent>.FingerEventHandler OnFingerMove;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060000A4 RID: 164 RVA: 0x00004048 File Offset: 0x00002248
	// (remove) Token: 0x060000A5 RID: 165 RVA: 0x00004064 File Offset: 0x00002264
	public event FingerEventDetector<FingerMotionEvent>.FingerEventHandler OnFingerStationary;

	// Token: 0x060000A6 RID: 166 RVA: 0x00004080 File Offset: 0x00002280
	private bool FireEvent(FingerMotionEvent e, FingerMotionDetector.EventType eventType, FingerMotionPhase phase, Vector2 position, bool updateSelection)
	{
		if ((!this.TrackMove && eventType == FingerMotionDetector.EventType.Move) || (!this.TrackStationary && eventType == FingerMotionDetector.EventType.Stationary))
		{
			return false;
		}
		e.Phase = phase;
		e.Position = position;
		if (e.Phase == FingerMotionPhase.Started)
		{
			e.StartTime = Time.time;
		}
		if (updateSelection)
		{
			base.UpdateSelection(e);
		}
		if (eventType == FingerMotionDetector.EventType.Move)
		{
			e.Name = this.MoveMessageName;
			if (this.OnFingerMove != null)
			{
				this.OnFingerMove(e);
			}
			base.TrySendMessage(e);
		}
		else
		{
			if (eventType != FingerMotionDetector.EventType.Stationary)
			{
				Debug.LogWarning("Unhandled FingerMotionDetector event type: " + eventType);
				return false;
			}
			e.Name = this.StationaryMessageName;
			if (this.OnFingerStationary != null)
			{
				this.OnFingerStationary(e);
			}
			base.TrySendMessage(e);
		}
		return true;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000416C File Offset: 0x0000236C
	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		FingerMotionEvent @event = base.GetEvent(finger);
		bool flag = false;
		if (finger.Phase != finger.PreviousPhase)
		{
			FingerGestures.FingerPhase fingerPhase = finger.PreviousPhase;
			if (fingerPhase != FingerGestures.FingerPhase.Moving)
			{
				if (fingerPhase == FingerGestures.FingerPhase.Stationary)
				{
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Ended, finger.PreviousPosition, !flag);
				}
			}
			else
			{
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Ended, finger.Position, !flag);
			}
			fingerPhase = finger.Phase;
			if (fingerPhase != FingerGestures.FingerPhase.Moving)
			{
				if (fingerPhase == FingerGestures.FingerPhase.Stationary)
				{
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Started, finger.Position, !flag);
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Updated, finger.Position, !flag);
				}
			}
			else
			{
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Started, finger.PreviousPosition, !flag);
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Updated, finger.Position, !flag);
			}
		}
		else
		{
			FingerGestures.FingerPhase fingerPhase = finger.Phase;
			if (fingerPhase != FingerGestures.FingerPhase.Moving)
			{
				if (fingerPhase == FingerGestures.FingerPhase.Stationary)
				{
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Updated, finger.Position, !flag);
				}
			}
			else
			{
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Updated, finger.Position, !flag);
			}
		}
	}

	// Token: 0x0400006C RID: 108
	public string MoveMessageName = "OnFingerMove";

	// Token: 0x0400006D RID: 109
	public string StationaryMessageName = "OnFingerStationary";

	// Token: 0x0400006E RID: 110
	public bool TrackMove = true;

	// Token: 0x0400006F RID: 111
	public bool TrackStationary = true;

	// Token: 0x0200001E RID: 30
	private enum EventType
	{
		// Token: 0x04000073 RID: 115
		Move,
		// Token: 0x04000074 RID: 116
		Stationary
	}
}
