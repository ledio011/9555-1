using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
[AddComponentMenu("FingerGestures/Gestures/Swipe Recognizer")]
public class SwipeRecognizer : DiscreteGestureRecognizer<SwipeGesture>
{
	// Token: 0x06000171 RID: 369 RVA: 0x0000664C File Offset: 0x0000484C
	public override string GetDefaultEventMessageName()
	{
		return "OnSwipe";
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00006654 File Offset: 0x00004854
	protected override bool CanBegin(SwipeGesture gesture, FingerGestures.IFingerList touches)
	{
		return base.CanBegin(gesture, touches) && touches.GetAverageDistanceFromStart() >= 0.5f && touches.AllMoving() && touches.MovingInSameDirection(0.35f);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000066A4 File Offset: 0x000048A4
	protected override void OnBegin(SwipeGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.StartPosition = touches.GetAverageStartPosition();
		gesture.Position = touches.GetAveragePosition();
		gesture.Move = Vector3.zero;
		gesture.MoveCounter = 0;
		gesture.Deviation = 0f;
		gesture.Direction = FingerGestures.SwipeDirection.None;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000066F4 File Offset: 0x000048F4
	protected override GestureRecognitionState OnRecognize(SwipeGesture gesture, FingerGestures.IFingerList touches)
	{
		float num = base.ToPixels(this.MinDistance);
		float num2 = base.ToPixels(this.MaxDistance);
		if (touches.Count != this.RequiredFingerCount)
		{
			if (touches.Count > this.RequiredFingerCount)
			{
				return GestureRecognitionState.Failed;
			}
			if (gesture.Move.magnitude < Mathf.Max(1f, num))
			{
				return GestureRecognitionState.Failed;
			}
			gesture.Direction = FingerGestures.GetSwipeDirection(gesture.Move);
			return GestureRecognitionState.Ended;
		}
		else
		{
			Vector2 move = gesture.Move;
			gesture.Position = touches.GetAveragePosition();
			gesture.Move = gesture.Position - gesture.StartPosition;
			float magnitude = gesture.Move.magnitude;
			if (num2 > num && magnitude > num2)
			{
				return GestureRecognitionState.Failed;
			}
			if (gesture.ElapsedTime > 0f)
			{
				gesture.Velocity = magnitude / gesture.ElapsedTime;
			}
			else
			{
				gesture.Velocity = 0f;
			}
			if (gesture.MoveCounter > 2 && gesture.Velocity < base.ToPixels(this.MinVelocity))
			{
				return GestureRecognitionState.Failed;
			}
			if (magnitude > 50f && gesture.MoveCounter > 2)
			{
				gesture.Deviation += 57.29578f * FingerGestures.SignedAngle(move, gesture.Move);
				if (Mathf.Abs(gesture.Deviation) > this.MaxDeviation)
				{
					return GestureRecognitionState.Failed;
				}
			}
			gesture.MoveCounter++;
			return GestureRecognitionState.InProgress;
		}
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00006870 File Offset: 0x00004A70
	public bool IsValidDirection(FingerGestures.SwipeDirection dir)
	{
		return dir != FingerGestures.SwipeDirection.None && (this.ValidDirections & dir) == dir;
	}

	// Token: 0x040000E7 RID: 231
	private FingerGestures.SwipeDirection ValidDirections = FingerGestures.SwipeDirection.All;

	// Token: 0x040000E8 RID: 232
	public float MinDistance = 0.5f;

	// Token: 0x040000E9 RID: 233
	public float MaxDistance;

	// Token: 0x040000EA RID: 234
	public float MinVelocity = 5f;

	// Token: 0x040000EB RID: 235
	public float MaxDeviation = 25f;
}
