using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class FGMouseInputProvider : FGInputProvider
{
	// Token: 0x0600019C RID: 412 RVA: 0x00006FE8 File Offset: 0x000051E8
	private void Start()
	{
		this.pinchDistance = this.initialPinchDistance;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00006FF8 File Offset: 0x000051F8
	private void Update()
	{
		bool flag = this.pinching || this.twisting;
		this.UpdatePinchEmulation();
		this.UpdateTwistEmulation();
		if (this.pinching || this.twisting)
		{
			if (!flag)
			{
				this.pivot = Input.mousePosition;
			}
			float f = 0f;
			float num = this.initialPinchDistance;
			if (this.pinching && this.twisting && Input.GetKey(this.twistAndPinchKey))
			{
				f = 0.017453292f * this.twistAngle;
				num = this.pinchDistance;
			}
			else if (this.twisting)
			{
				f = 0.017453292f * this.twistAngle;
			}
			else if (this.pinching)
			{
				num = this.pinchDistance;
			}
			float num2 = Mathf.Cos(f);
			float num3 = Mathf.Sin(f);
			this.pos[0].x = this.pivot.x - 0.5f * num * num2;
			this.pos[0].y = this.pivot.y - 0.5f * num * num3;
			this.pos[1].x = this.pivot.x + 0.5f * num * num2;
			this.pos[1].y = this.pivot.y + 0.5f * num * num3;
		}
		if (Input.GetKey(this.pivotKey))
		{
			if (Input.GetKeyDown(this.pivotKey))
			{
				this.pivot = Input.mousePosition;
			}
			if (!this.pivoting && Vector2.Distance(Input.mousePosition, this.pivot) > 50f)
			{
				this.pivoting = true;
			}
			if (this.pivoting)
			{
				this.pos[0] = this.pivot;
				this.pos[1] = Input.mousePosition;
			}
		}
		else
		{
			this.pivoting = false;
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00007220 File Offset: 0x00005420
	private void UpdatePinchEmulation()
	{
		float num = this.pinchAxisScale * Input.GetAxis(this.pinchAxis);
		if (Mathf.Abs(num) > 0.0001f)
		{
			if (!this.pinching)
			{
				this.pinching = true;
				this.pinchDistance = this.initialPinchDistance;
			}
			this.pinchResetTime = Time.time + this.pinchResetTimeDelay;
			this.pinchDistance = Mathf.Max(5f, this.pinchDistance + num);
		}
		else if (this.pinchResetTime <= Time.time)
		{
			this.pinching = false;
			this.pinchDistance = this.initialPinchDistance;
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000072C0 File Offset: 0x000054C0
	private void UpdateTwistEmulation()
	{
		float num = this.twistAxisScale * Input.GetAxis(this.twistAxis);
		if (this.twistKey != KeyCode.None && Input.GetKey(this.twistKey) && Mathf.Abs(num) > 0.0001f)
		{
			if (!this.twisting)
			{
				this.twisting = true;
				this.twistAngle = 0f;
			}
			this.twistResetTime = Time.time + this.twistResetTimeDelay;
			this.twistAngle += num;
		}
		else if (this.twistResetTime <= Time.time)
		{
			this.twisting = false;
			this.twistAngle = 0f;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007370 File Offset: 0x00005570
	public override int MaxSimultaneousFingers
	{
		get
		{
			return this.maxButtons;
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00007378 File Offset: 0x00005578
	public override void GetInputState(int fingerIndex, out bool down, out Vector2 position)
	{
		down = Input.GetMouseButton(fingerIndex);
		position = Input.mousePosition;
		if ((this.pivoting || this.pinching || this.twisting) && (fingerIndex == 0 || fingerIndex == 1))
		{
			down = true;
			position = this.pos[fingerIndex];
		}
	}

	// Token: 0x040000FF RID: 255
	public int maxButtons = 3;

	// Token: 0x04000100 RID: 256
	public string pinchAxis = "Mouse ScrollWheel";

	// Token: 0x04000101 RID: 257
	public float pinchAxisScale = 100f;

	// Token: 0x04000102 RID: 258
	public float pinchResetTimeDelay = 0.15f;

	// Token: 0x04000103 RID: 259
	public float initialPinchDistance = 150f;

	// Token: 0x04000104 RID: 260
	public string twistAxis = "Mouse ScrollWheel";

	// Token: 0x04000105 RID: 261
	public float twistAxisScale = 100f;

	// Token: 0x04000106 RID: 262
	public KeyCode twistKey = KeyCode.LeftControl;

	// Token: 0x04000107 RID: 263
	public float twistResetTimeDelay = 0.15f;

	// Token: 0x04000108 RID: 264
	public KeyCode pivotKey = KeyCode.LeftAlt;

	// Token: 0x04000109 RID: 265
	private bool pivoting;

	// Token: 0x0400010A RID: 266
	public KeyCode twistAndPinchKey = KeyCode.LeftShift;

	// Token: 0x0400010B RID: 267
	private Vector2 pivot = Vector2.zero;

	// Token: 0x0400010C RID: 268
	private Vector2[] pos = new Vector2[]
	{
		Vector2.zero,
		Vector2.zero
	};

	// Token: 0x0400010D RID: 269
	private bool pinching;

	// Token: 0x0400010E RID: 270
	private float pinchResetTime;

	// Token: 0x0400010F RID: 271
	private float pinchDistance;

	// Token: 0x04000110 RID: 272
	private bool twisting;

	// Token: 0x04000111 RID: 273
	private float twistAngle;

	// Token: 0x04000112 RID: 274
	private float twistResetTime;
}
