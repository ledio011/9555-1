using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[AddComponentMenu("FingerGestures/Gestures/Drag Recognizer")]
public class DragRecognizer : ContinuousGestureRecognizer<DragGesture>
{
	// Token: 0x06000126 RID: 294 RVA: 0x00005338 File Offset: 0x00003538
	public override string GetDefaultEventMessageName()
	{
		return "OnDrag";
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00005340 File Offset: 0x00003540
	protected override GameObject GetDefaultSelectionForSendMessage(DragGesture gesture)
	{
		return gesture.StartSelection;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00005348 File Offset: 0x00003548
	protected override bool CanBegin(DragGesture gesture, FingerGestures.IFingerList touches)
	{
		return base.CanBegin(gesture, touches) && touches.GetAverageDistanceFromStart() >= base.ToPixels(this.MoveTolerance) && touches.AllMoving() && (this.RequiredFingerCount < 2 || !this.ApplySameDirectionConstraint || touches.MovingInSameDirection(0.35f));
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000053B4 File Offset: 0x000035B4
	protected override void OnBegin(DragGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.Position = touches.GetAveragePosition();
		gesture.StartPosition = touches.GetAverageStartPosition();
		gesture.DeltaMove = gesture.Position - gesture.StartPosition;
		gesture.LastDelta = Vector2.zero;
		gesture.LastPos = gesture.Position;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00005408 File Offset: 0x00003608
	protected override GestureRecognitionState OnRecognize(DragGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			if (touches.Count < this.RequiredFingerCount)
			{
				return GestureRecognitionState.Ended;
			}
			return GestureRecognitionState.Failed;
		}
		else
		{
			if (this.RequiredFingerCount >= 2 && this.ApplySameDirectionConstraint && touches.AllMoving() && !touches.MovingInSameDirection(0.35f))
			{
				return GestureRecognitionState.Failed;
			}
			gesture.Position = touches.GetAveragePosition();
			gesture.LastDelta = gesture.DeltaMove;
			gesture.DeltaMove = gesture.Position - gesture.LastPos;
			if (gesture.DeltaMove.sqrMagnitude > 0f || gesture.LastDelta.sqrMagnitude > 0f)
			{
				gesture.LastPos = gesture.Position;
			}
			base.RaiseEvent(gesture);
			return GestureRecognitionState.InProgress;
		}
	}

	// Token: 0x040000C1 RID: 193
	public float MoveTolerance = 0.25f;

	// Token: 0x040000C2 RID: 194
	public bool ApplySameDirectionConstraint;
}
