using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[AddComponentMenu("FingerGestures/Gestures/Long Press Recognizer")]
public class LongPressRecognizer : DiscreteGestureRecognizer<LongPressGesture>
{
	// Token: 0x0600012D RID: 301 RVA: 0x00005508 File Offset: 0x00003708
	public override string GetDefaultEventMessageName()
	{
		return "OnLongPress";
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00005510 File Offset: 0x00003710
	protected override void OnBegin(LongPressGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.Position = touches.GetAveragePosition();
		gesture.StartPosition = gesture.Position;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00005538 File Offset: 0x00003738
	protected override GestureRecognitionState OnRecognize(LongPressGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			return GestureRecognitionState.Failed;
		}
		if (gesture.ElapsedTime >= this.Duration)
		{
			return GestureRecognitionState.Ended;
		}
		if (touches.GetAverageDistanceFromStart() > base.ToPixels(this.MoveTolerance))
		{
			return GestureRecognitionState.Failed;
		}
		return GestureRecognitionState.InProgress;
	}

	// Token: 0x040000C3 RID: 195
	public float Duration = 1f;

	// Token: 0x040000C4 RID: 196
	public float MoveTolerance = 0.5f;
}
