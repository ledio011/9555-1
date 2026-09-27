using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
[AddComponentMenu("FingerGestures/Toolbox/Pinch To Scale")]
public class TBPinchToScale : MonoBehaviour
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060001FE RID: 510 RVA: 0x00008B64 File Offset: 0x00006D64
	// (set) Token: 0x060001FF RID: 511 RVA: 0x00008B6C File Offset: 0x00006D6C
	public float ScaleAmount
	{
		get
		{
			return this.scaleAmount;
		}
		set
		{
			value = Mathf.Clamp(value, this.minScaleAmount, this.maxScaleAmount);
			if (value != this.scaleAmount)
			{
				this.scaleAmount = value;
				Vector3 localScale = this.scaleAmount * this.baseScale;
				localScale.x *= this.scaleWeights.x;
				localScale.y *= this.scaleWeights.y;
				localScale.z *= this.scaleWeights.z;
				base.transform.localScale = localScale;
			}
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000200 RID: 512 RVA: 0x00008C0C File Offset: 0x00006E0C
	// (set) Token: 0x06000201 RID: 513 RVA: 0x00008C14 File Offset: 0x00006E14
	public float IdealScaleAmount
	{
		get
		{
			return this.idealScaleAmount;
		}
		set
		{
			this.idealScaleAmount = Mathf.Clamp(value, this.minScaleAmount, this.maxScaleAmount);
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00008C30 File Offset: 0x00006E30
	private void Start()
	{
		this.baseScale = base.transform.localScale;
		this.IdealScaleAmount = this.ScaleAmount;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00008C5C File Offset: 0x00006E5C
	private void Update()
	{
		if (this.smoothingSpeed > 0f)
		{
			this.ScaleAmount = Mathf.Lerp(this.ScaleAmount, this.IdealScaleAmount, Time.deltaTime * this.smoothingSpeed);
		}
		else
		{
			this.ScaleAmount = this.IdealScaleAmount;
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00008CB0 File Offset: 0x00006EB0
	private void OnPinch(PinchGesture gesture)
	{
		this.IdealScaleAmount += this.sensitivity * gesture.Delta.Centimeters();
	}

	// Token: 0x04000175 RID: 373
	public Vector3 scaleWeights = Vector3.one;

	// Token: 0x04000176 RID: 374
	public float minScaleAmount = 0.5f;

	// Token: 0x04000177 RID: 375
	public float maxScaleAmount = 2f;

	// Token: 0x04000178 RID: 376
	public float sensitivity = 1f;

	// Token: 0x04000179 RID: 377
	public float smoothingSpeed = 12f;

	// Token: 0x0400017A RID: 378
	private float idealScaleAmount = 1f;

	// Token: 0x0400017B RID: 379
	private float scaleAmount = 1f;

	// Token: 0x0400017C RID: 380
	private Vector3 baseScale = Vector3.one;
}
