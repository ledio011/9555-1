using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
[AddComponentMenu("FingerGestures/Gestures/Twist Recognizer")]
public class TwistRecognizer : ContinuousGestureRecognizer<TwistGesture>
{
	// Token: 0x0600018B RID: 395 RVA: 0x00006BEC File Offset: 0x00004DEC
	public override string GetDefaultEventMessageName()
	{
		return "OnTwist";
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600018C RID: 396 RVA: 0x00006BF4 File Offset: 0x00004DF4
	// (set) Token: 0x0600018D RID: 397 RVA: 0x00006BF8 File Offset: 0x00004DF8
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
				Debug.LogWarning("Twist only supports 2 fingers");
			}
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600018E RID: 398 RVA: 0x00006C10 File Offset: 0x00004E10
	public override bool SupportFingerClustering
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00006C14 File Offset: 0x00004E14
	public override GestureResetMode GetDefaultResetMode()
	{
		return GestureResetMode.NextFrame;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00006C18 File Offset: 0x00004E18
	protected override GameObject GetDefaultSelectionForSendMessage(TwistGesture gesture)
	{
		return gesture.StartSelection;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00006C20 File Offset: 0x00004E20
	protected override void Reset(TwistGesture gesture)
	{
		base.Reset(gesture);
		gesture.Pivot = null;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00006C30 File Offset: 0x00004E30
	private FingerGestures.Finger GetTwistPivot(FingerGestures.Finger finger0, FingerGestures.Finger finger1)
	{
		if (finger0.IsMoving == finger1.IsMoving)
		{
			return null;
		}
		FingerGestures.Finger finger2 = (!finger0.IsMoving) ? finger0 : finger1;
		if (finger2.DistanceFromStart > base.ToPixels(this.PivotMoveTolerance))
		{
			return null;
		}
		return finger2;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00006C80 File Offset: 0x00004E80
	protected override bool CanBegin(TwistGesture gesture, FingerGestures.IFingerList touches)
	{
		if (!base.CanBegin(gesture, touches))
		{
			return false;
		}
		FingerGestures.Finger finger = touches[0];
		FingerGestures.Finger finger2 = touches[1];
		if (this.Method == TwistMethod.Pivot)
		{
			if (!this.GetTwistPivot(finger, finger2))
			{
				return false;
			}
		}
		else
		{
			if (!FingerGestures.AllFingersMoving(finger, finger2))
			{
				return false;
			}
			if (!this.FingersMovedInOppositeDirections(finger, finger2))
			{
				return false;
			}
		}
		float f = TwistRecognizer.SignedAngularGap(finger, finger2, finger.StartPosition, finger2.StartPosition);
		return Mathf.Abs(f) >= this.MinRotation;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00006D18 File Offset: 0x00004F18
	protected override void OnBegin(TwistGesture gesture, FingerGestures.IFingerList touches)
	{
		FingerGestures.Finger finger = touches[0];
		FingerGestures.Finger finger2 = touches[1];
		if (this.Method == TwistMethod.Pivot)
		{
			gesture.Pivot = this.GetTwistPivot(finger, finger2);
			gesture.StartPosition = gesture.Pivot.StartPosition;
		}
		else
		{
			gesture.Pivot = null;
			gesture.StartPosition = 0.5f * (finger.Position + finger2.Position);
		}
		gesture.Position = gesture.StartPosition;
		gesture.TotalRotation = 0f;
		gesture.DeltaRotation = 0f;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00006DB0 File Offset: 0x00004FB0
	protected override GestureRecognitionState OnRecognize(TwistGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count == this.RequiredFingerCount)
		{
			FingerGestures.Finger finger = touches[0];
			FingerGestures.Finger finger2 = touches[1];
			if (this.Method == TwistMethod.Pivot)
			{
				if (gesture.Pivot == null)
				{
					Debug.LogWarning("Twist - pivot finger is null!", this);
					return GestureRecognitionState.Failed;
				}
				if (gesture.Pivot != finger && gesture.Pivot != finger2)
				{
					Debug.LogWarning("Twist - lost track of pivot finger!", this);
					return GestureRecognitionState.Failed;
				}
				gesture.Position = gesture.Pivot.Position;
			}
			else
			{
				gesture.Position = 0.5f * (finger.Position + finger2.Position);
			}
			gesture.DeltaRotation = TwistRecognizer.SignedAngularGap(finger, finger2, finger.PreviousPosition, finger2.PreviousPosition);
			if (Mathf.Abs(gesture.DeltaRotation) > 1E-45f)
			{
				gesture.TotalRotation += gesture.DeltaRotation;
				base.RaiseEvent(gesture);
			}
			return GestureRecognitionState.InProgress;
		}
		gesture.DeltaRotation = 0f;
		if (touches.Count < this.RequiredFingerCount)
		{
			return GestureRecognitionState.Ended;
		}
		return GestureRecognitionState.Failed;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00006EC8 File Offset: 0x000050C8
	private bool FingersMovedInOppositeDirections(FingerGestures.Finger finger0, FingerGestures.Finger finger1)
	{
		return FingerGestures.FingersMovedInOppositeDirections(finger0, finger1, this.MinDOT);
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00006ED8 File Offset: 0x000050D8
	private static float SignedAngularGap(FingerGestures.Finger finger0, FingerGestures.Finger finger1, Vector2 refPos0, Vector2 refPos1)
	{
		Vector2 normalized = (finger0.Position - finger1.Position).normalized;
		Vector2 normalized2 = (refPos0 - refPos1).normalized;
		return 57.29578f * FingerGestures.SignedAngle(normalized2, normalized);
	}

	// Token: 0x040000FB RID: 251
	public TwistMethod Method;

	// Token: 0x040000FC RID: 252
	public float MinDOT = -0.7f;

	// Token: 0x040000FD RID: 253
	public float MinRotation = 1f;

	// Token: 0x040000FE RID: 254
	public float PivotMoveTolerance = 0.5f;
}
