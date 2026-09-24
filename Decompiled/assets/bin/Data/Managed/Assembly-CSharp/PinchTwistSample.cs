using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class PinchTwistSample : SampleBase
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000025 RID: 37 RVA: 0x00002EF0 File Offset: 0x000010F0
	// (set) Token: 0x06000026 RID: 38 RVA: 0x00002EF8 File Offset: 0x000010F8
	private bool Rotating
	{
		get
		{
			return this.rotating;
		}
		set
		{
			if (this.rotating != value)
			{
				this.rotating = value;
				this.UpdateTargetMaterial();
			}
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000027 RID: 39 RVA: 0x00002F14 File Offset: 0x00001114
	// (set) Token: 0x06000028 RID: 40 RVA: 0x00002F1C File Offset: 0x0000111C
	private bool Pinching
	{
		get
		{
			return this.pinching;
		}
		set
		{
			if (this.pinching != value)
			{
				this.pinching = value;
				this.UpdateTargetMaterial();
			}
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002F38 File Offset: 0x00001138
	private void OnTwist(TwistGesture gesture)
	{
		if (gesture.Phase == 1)
		{
			base.UI.StatusText = "Twist gesture started";
			this.Rotating = true;
		}
		else if (gesture.Phase == 2)
		{
			if (this.Rotating)
			{
				base.UI.StatusText = "Rotation updated by " + gesture.DeltaRotation + " degrees";
				this.target.Rotate(0f, 0f, gesture.DeltaRotation);
			}
		}
		else if (this.Rotating)
		{
			base.UI.StatusText = "Rotation gesture ended. Total rotation: " + gesture.TotalRotation;
			this.Rotating = false;
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002FFC File Offset: 0x000011FC
	private void OnPinch(PinchGesture gesture)
	{
		if (gesture.Phase == 1)
		{
			this.Pinching = true;
		}
		else if (gesture.Phase == 2)
		{
			if (this.Pinching)
			{
				this.target.transform.localScale += FingerGesturesExtensions.Centimeters(gesture.Delta) * this.pinchScaleFactor * Vector3.one;
			}
		}
		else if (this.Pinching)
		{
			this.Pinching = false;
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003088 File Offset: 0x00001288
	private void UpdateTargetMaterial()
	{
		Material sharedMaterial;
		if (this.pinching && this.rotating)
		{
			sharedMaterial = this.pinchAndTwistMaterial;
		}
		else if (this.pinching)
		{
			sharedMaterial = this.pinchMaterial;
		}
		else if (this.rotating)
		{
			sharedMaterial = this.twistMaterial;
		}
		else
		{
			sharedMaterial = this.originalMaterial;
		}
		this.target.renderer.sharedMaterial = sharedMaterial;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003100 File Offset: 0x00001300
	protected override string GetHelpText()
	{
		return "This sample demonstrates how to use the two-fingers Pinch and Rotation gesture events to control the scale and orientation of a rectangle on the screen\r\n\r\n- Pinch: move two fingers closer or further apart to change the scale of the rectangle (mousewheel on desktop)\r\n- Rotation: twist two fingers in a circular motion to rotate the rectangle (CTRL+mousewheel on desktop)\r\n\r\n";
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003108 File Offset: 0x00001308
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = "Use two fingers anywhere on the screen to rotate and scale the green object.";
		this.originalMaterial = this.target.renderer.sharedMaterial;
	}

	// Token: 0x04000025 RID: 37
	public Transform target;

	// Token: 0x04000026 RID: 38
	public Material twistMaterial;

	// Token: 0x04000027 RID: 39
	public Material pinchMaterial;

	// Token: 0x04000028 RID: 40
	public Material pinchAndTwistMaterial;

	// Token: 0x04000029 RID: 41
	public float pinchScaleFactor = 0.02f;

	// Token: 0x0400002A RID: 42
	private bool rotating;

	// Token: 0x0400002B RID: 43
	private bool pinching;

	// Token: 0x0400002C RID: 44
	private Material originalMaterial;

	// Token: 0x02000008 RID: 8
	public enum InputMode
	{
		// Token: 0x0400002E RID: 46
		PinchOnly,
		// Token: 0x0400002F RID: 47
		TwistOnly,
		// Token: 0x04000030 RID: 48
		PinchAndTwist
	}
}
