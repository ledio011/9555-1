using System;

// Token: 0x02000007 RID: 7
public abstract class DiscreteGestureRecognizer<T> : GestureRecognizerTS<T> where T : DiscreteGesture, new()
{
	// Token: 0x0600000C RID: 12 RVA: 0x00002678 File Offset: 0x00000878
	protected override void OnStateChanged(Gesture sender)
	{
		base.OnStateChanged(sender);
		T gesture = (T)((object)sender);
		if (gesture.State == GestureRecognitionState.Ended)
		{
			base.RaiseEvent(gesture);
		}
	}
}
