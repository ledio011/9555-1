using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[AddComponentMenu("FingerGestures/Gestures/Pinch Recognizer")]
public class PinchRecognizer : ContinuousGestureRecognizer<PinchGesture>
{
	// Token: 0x06000136 RID: 310 RVA: 0x000055D8 File Offset: 0x000037D8
	public override string GetDefaultEventMessageName()
	{
		return "OnPinch";
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000137 RID: 311 RVA: 0x000055E0 File Offset: 0x000037E0
	// (set) Token: 0x06000138 RID: 312 RVA: 0x000055E4 File Offset: 0x000037E4
	public override int RequiredFingerCount
	{
		get
		{
			return 2;
		}
		set
		{
			if (Application.isPlaying)
			{
				Debug.LogWarning("Pinch only supports 2 fingers");
			}
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000139 RID: 313 RVA: 0x000055FC File Offset: 0x000037FC
	public override bool SupportFingerClustering
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00005600 File Offset: 0x00003800
	protected override GameObject GetDefaultSelectionForSendMessage(PinchGesture gesture)
	{
		return gesture.StartSelection;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00005608 File Offset: 0x00003808
	public override GestureResetMode GetDefaultResetMode()
	{
		return GestureResetMode.NextFrame;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000560C File Offset: 0x0000380C
	protected override bool CanBegin(PinchGesture gesture, FingerGestures.IFingerList touches)
	{
		if (!base.CanBegin(gesture, touches))
		{
			return false;
		}
		FingerGestures.Finger finger = touches[0];
		FingerGestures.Finger finger2 = touches[1];
		if (!FingerGestures.AllFingersMoving(finger, finger2))
		{
			return false;
		}
		if (!this.FingersMovedInOppositeDirections(finger, finger2))
		{
			return false;
		}
		float num = Vector2.SqrMagnitude(finger.StartPosition - finger2.StartPosition);
		float num2 = Vector2.SqrMagnitude(finger.Position - finger2.Position);
		return Mathf.Abs(num - num2) >= base.ToSqrPixels(this.MinDistance);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000056A0 File Offset: 0x000038A0
	protected override void OnBegin(PinchGesture gesture, FingerGestures.IFingerList touches)
	{
		FingerGestures.Finger finger = touches[0];
		FingerGestures.Finger finger2 = touches[1];
		gesture.StartPosition = 0.5f * (finger.StartPosition + finger2.StartPosition);
		gesture.Position = 0.5f * (finger.Position + finger2.Position);
		float num = Vector2.Distance(finger.PreviousPosition, finger2.PreviousPosition);
		float num2 = Vector2.Distance(finger.Position, finger2.Position);
		gesture.Delta = num2 - num;
		gesture.Gap = num2;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00005734 File Offset: 0x00003934
	protected override GestureRecognitionState OnRecognize(PinchGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			gesture.Delta = 0f;
			if (touches.Count < this.RequiredFingerCount)
			{
				return GestureRecognitionState.Ended;
			}
			return GestureRecognitionState.Failed;
		}
		else
		{
			FingerGestures.Finger finger = touches[0];
			FingerGestures.Finger finger2 = touches[1];
			gesture.Position = 0.5f * (finger.Position + finger2.Position);
			if (!FingerGestures.AllFingersMoving(finger, finger2))
			{
				return GestureRecognitionState.InProgress;
			}
			float num = Vector2.Distance(finger.Position, finger2.Position);
			float num2 = num - gesture.Gap;
			gesture.Gap = num;
			if (Mathf.Abs(num2) > 0.001f)
			{
				if (!this.FingersMovedInOppositeDirections(finger, finger2))
				{
					return GestureRecognitionState.InProgress;
				}
				gesture.Delta = num2;
				base.RaiseEvent(gesture);
			}
			return GestureRecognitionState.InProgress;
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00005804 File Offset: 0x00003A04
	private bool FingersMovedInOppositeDirections(FingerGestures.Finger finger0, FingerGestures.Finger finger1)
	{
		return FingerGestures.FingersMovedInOppositeDirections(finger0, finger1, this.MinDOT);
	}

	// Token: 0x040000C7 RID: 199
	public float MinDOT = -0.7f;

	// Token: 0x040000C8 RID: 200
	public float MinDistance = 0.25f;
}
