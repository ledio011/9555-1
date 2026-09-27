using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
[AddComponentMenu("FingerGestures/Gestures/Tap Recognizer")]
public class TapRecognizer : DiscreteGestureRecognizer<TapGesture>
{
	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600017A RID: 378 RVA: 0x000068CC File Offset: 0x00004ACC
	private bool IsMultiTap
	{
		get
		{
			return this.RequiredTaps > 1;
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x000068D8 File Offset: 0x00004AD8
	private bool HasTimedOut(TapGesture gesture)
	{
		return (this.MaxDuration > 0f && gesture.ElapsedTime > this.MaxDuration) || (this.IsMultiTap && this.MaxDelayBetweenTaps > 0f && Time.time - gesture.LastTapTime > this.MaxDelayBetweenTaps);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00006940 File Offset: 0x00004B40
	protected override void Reset(TapGesture gesture)
	{
		gesture.Taps = 0;
		gesture.Down = false;
		gesture.WasDown = false;
		base.Reset(gesture);
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600017D RID: 381 RVA: 0x00006960 File Offset: 0x00004B60
	public override bool SupportFingerClustering
	{
		get
		{
			return !this.IsMultiTap && base.SupportFingerClustering;
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00006978 File Offset: 0x00004B78
	private GestureRecognitionState RecognizeSingleTap(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			if (touches.Count == 0)
			{
				return GestureRecognitionState.Ended;
			}
			return GestureRecognitionState.Failed;
		}
		else
		{
			if (this.HasTimedOut(gesture))
			{
				return GestureRecognitionState.Failed;
			}
			float num = Vector3.SqrMagnitude(touches.GetAveragePosition() - gesture.StartPosition);
			if (num >= base.ToSqrPixels(this.MoveTolerance))
			{
				return GestureRecognitionState.Failed;
			}
			return GestureRecognitionState.InProgress;
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000069E4 File Offset: 0x00004BE4
	private GestureRecognitionState RecognizeMultiTap(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.WasDown = gesture.Down;
		gesture.Down = false;
		if (touches.Count == this.RequiredFingerCount)
		{
			gesture.Down = true;
			gesture.LastDownTime = Time.time;
		}
		else if (touches.Count == 0)
		{
			gesture.Down = false;
		}
		else if (touches.Count < this.RequiredFingerCount)
		{
			if (Time.time - gesture.LastDownTime > 0.25f)
			{
				return GestureRecognitionState.Failed;
			}
		}
		else if (!base.Young(touches))
		{
			return GestureRecognitionState.Failed;
		}
		if (this.HasTimedOut(gesture))
		{
			return GestureRecognitionState.Failed;
		}
		if (gesture.Down)
		{
			float num = Vector3.SqrMagnitude(touches.GetAveragePosition() - gesture.StartPosition);
			if (num >= base.ToSqrPixels(this.MoveTolerance))
			{
				return GestureRecognitionState.FailAndRetry;
			}
		}
		if (gesture.WasDown != gesture.Down && !gesture.Down)
		{
			gesture.Taps++;
			gesture.LastTapTime = Time.time;
			if (gesture.Taps >= this.RequiredTaps)
			{
				return GestureRecognitionState.Ended;
			}
		}
		return GestureRecognitionState.InProgress;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00006B14 File Offset: 0x00004D14
	public override string GetDefaultEventMessageName()
	{
		return "OnTap";
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00006B1C File Offset: 0x00004D1C
	protected override void OnBegin(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.Position = touches.GetAveragePosition();
		gesture.StartPosition = gesture.Position;
		gesture.LastTapTime = Time.time;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00006B4C File Offset: 0x00004D4C
	protected override GestureRecognitionState OnRecognize(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		return (!this.IsMultiTap) ? this.RecognizeSingleTap(gesture, touches) : this.RecognizeMultiTap(gesture, touches);
	}

	// Token: 0x040000F1 RID: 241
	public int RequiredTaps = 1;

	// Token: 0x040000F2 RID: 242
	public float MoveTolerance = 0.5f;

	// Token: 0x040000F3 RID: 243
	public float MaxDuration;

	// Token: 0x040000F4 RID: 244
	public float MaxDelayBetweenTaps = 0.5f;
}
