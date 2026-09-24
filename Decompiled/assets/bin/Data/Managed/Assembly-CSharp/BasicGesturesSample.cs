using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class BasicGesturesSample : SampleBase
{
	// Token: 0x0600001D RID: 29 RVA: 0x00002C08 File Offset: 0x00000E08
	private void OnSwipe(SwipeGesture gesture)
	{
		GameObject startSelection = gesture.StartSelection;
		if (startSelection == this.swipeObject)
		{
			base.UI.StatusText = string.Concat(new object[]
			{
				"Swiped ",
				gesture.Direction,
				" with finger ",
				gesture.Fingers[0],
				" (velocity:",
				gesture.Velocity,
				", distance: ",
				gesture.Move.magnitude,
				" )"
			});
			Debug.Log(base.UI.StatusText);
			SwipeParticlesEmitter componentInChildren = startSelection.GetComponentInChildren<SwipeParticlesEmitter>();
			if (componentInChildren)
			{
				componentInChildren.Emit(gesture.Direction, gesture.Velocity);
			}
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002CE4 File Offset: 0x00000EE4
	private void OnTap(TapGesture gesture)
	{
		if (gesture.Selection == this.tapObject)
		{
			this.SpawnParticles(this.tapObject);
			base.UI.StatusText = "Tapped with finger " + gesture.Fingers[0];
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002D34 File Offset: 0x00000F34
	private void OnDoubleTap(TapGesture gesture)
	{
		if (gesture.Selection == this.doubleTapObject)
		{
			this.SpawnParticles(this.doubleTapObject);
			base.UI.StatusText = "Double-Tapped with finger " + gesture.Fingers[0];
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002D84 File Offset: 0x00000F84
	private void OnLongPress(LongPressGesture gesture)
	{
		if (gesture.Selection == this.longPressObject)
		{
			this.SpawnParticles(this.longPressObject);
			base.UI.StatusText = "Performed a long-press with finger " + gesture.Fingers[0];
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002DD4 File Offset: 0x00000FD4
	private void OnDrag(DragGesture gesture)
	{
		FingerGestures.Finger finger = gesture.Fingers[0];
		if (gesture.Phase == 1)
		{
			if (gesture.Selection != this.dragObject)
			{
				return;
			}
			base.UI.StatusText = "Started dragging with finger " + finger;
			this.dragFingerIndex = finger.Index;
			this.SpawnParticles(this.dragObject);
		}
		else if (finger.Index == this.dragFingerIndex)
		{
			if (gesture.Phase == 2)
			{
				this.dragObject.transform.position = SampleBase.GetWorldPos(gesture.Position);
			}
			else
			{
				base.UI.StatusText = "Stopped dragging with finger " + finger;
				this.dragFingerIndex = -1;
				this.SpawnParticles(this.dragObject);
			}
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002EAC File Offset: 0x000010AC
	protected override string GetHelpText()
	{
		return "This sample demonstrates some of the supported single-finger gestures:\r\n\r\n- Drag: press the red sphere and move your finger to drag it around  \r\n\r\n- LongPress: keep your finger pressed on the cyan sphere for a few seconds\r\n\r\n- Tap: press & release the purple sphere \r\n\r\n- Double Tap: quickly press & release the green sphere twice in a row\r\n\r\n- Swipe: press the yellow sphere and move your finger in one of the four cardinal directions, then release. The speed of the motion is taken into account.";
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002EB4 File Offset: 0x000010B4
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x0400001F RID: 31
	private int dragFingerIndex = -1;

	// Token: 0x04000020 RID: 32
	public GameObject longPressObject;

	// Token: 0x04000021 RID: 33
	public GameObject tapObject;

	// Token: 0x04000022 RID: 34
	public GameObject doubleTapObject;

	// Token: 0x04000023 RID: 35
	public GameObject swipeObject;

	// Token: 0x04000024 RID: 36
	public GameObject dragObject;
}
