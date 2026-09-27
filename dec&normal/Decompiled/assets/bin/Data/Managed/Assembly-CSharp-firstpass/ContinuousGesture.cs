using System;

// Token: 0x02000004 RID: 4
public abstract class ContinuousGesture : Gesture
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000006 RID: 6 RVA: 0x000025A0 File Offset: 0x000007A0
	public ContinuousGesturePhase Phase
	{
		get
		{
			switch (base.State)
			{
			case GestureRecognitionState.Started:
				return ContinuousGesturePhase.Started;
			case GestureRecognitionState.InProgress:
				return ContinuousGesturePhase.Updated;
			case GestureRecognitionState.Failed:
			case GestureRecognitionState.Ended:
				return ContinuousGesturePhase.Ended;
			default:
				return ContinuousGesturePhase.None;
			}
		}
	}
}
