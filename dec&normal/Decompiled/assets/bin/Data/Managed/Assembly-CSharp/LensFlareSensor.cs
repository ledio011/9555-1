using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class LensFlareSensor : MonoBehaviour
{
	// Token: 0x060009EA RID: 2538 RVA: 0x00047F88 File Offset: 0x00046188
	public void SetMaxBrightness(float bright)
	{
		this.maxBrightness = bright;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x00047F94 File Offset: 0x00046194
	private void Start()
	{
		if (this.cam == null)
		{
			this.cam = Camera.main;
		}
		this.flare = base.GetComponent<LensFlare>();
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x00047FCC File Offset: 0x000461CC
	private void FixedUpdate()
	{
		if (this.cam == null)
		{
			this.cam = Camera.main;
			if (this.cam == null)
			{
				return;
			}
		}
		Vector3 vector = this.cam.transform.position - base.transform.position;
		float magnitude = vector.magnitude;
		float num = this.decayDistance.Evaluate(magnitude / this.maxDistance);
		float num2 = 1f;
		if (this.angleValid)
		{
			float num3 = Vector3.Angle(vector, base.transform.forward);
			num2 = this.decayAngle.Evaluate(num3 / this.maxAngle);
		}
		float num4 = num * num2;
		if (this.flare != null)
		{
			this.flare.brightness = this.maxBrightness * num4;
		}
		else if (base.light != null)
		{
			base.light.intensity = this.maxBrightness * num4;
		}
	}

	// Token: 0x040008AE RID: 2222
	public Camera cam;

	// Token: 0x040008AF RID: 2223
	public float maxDistance;

	// Token: 0x040008B0 RID: 2224
	public AnimationCurve decayDistance;

	// Token: 0x040008B1 RID: 2225
	public bool angleValid;

	// Token: 0x040008B2 RID: 2226
	public float maxAngle;

	// Token: 0x040008B3 RID: 2227
	public AnimationCurve decayAngle;

	// Token: 0x040008B4 RID: 2228
	private LensFlare flare;

	// Token: 0x040008B5 RID: 2229
	public float maxBrightness = 3f;
}
