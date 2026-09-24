using System;

// Token: 0x02000005 RID: 5
public abstract class ContinuousGestureRecognizer<T> : GestureRecognizerTS<T> where T : ContinuousGesture, new()
{
	// Token: 0x06000008 RID: 8 RVA: 0x000025E0 File Offset: 0x000007E0
	protected override void Reset(T gesture)
	{
		base.Reset(gesture);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000025EC File Offset: 0x000007EC
	protected override void OnStateChanged(Gesture sender)
	{
		base.OnStateChanged(sender);
		T gesture = (T)((object)sender);
		switch (gesture.State)
		{
		case GestureRecognitionState.Started:
			base.RaiseEvent(gesture);
			break;
		case GestureRecognitionState.Failed:
			if (gesture.PreviousState != GestureRecognitionState.Ready)
			{
				base.RaiseEvent(gesture);
			}
			break;
		case GestureRecognitionState.Ended:
			base.RaiseEvent(gesture);
			break;
		}
	}
}
