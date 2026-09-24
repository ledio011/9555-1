using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
[RequireComponent(typeof(Camera))]
public class TweenFOV : UITweener
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x0600048A RID: 1162 RVA: 0x00020208 File Offset: 0x0001E408
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x0600048B RID: 1163 RVA: 0x00020230 File Offset: 0x0001E430
	// (set) Token: 0x0600048C RID: 1164 RVA: 0x00020238 File Offset: 0x0001E438
	[Obsolete("Use 'value' instead")]
	public float fov
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x0600048D RID: 1165 RVA: 0x00020244 File Offset: 0x0001E444
	// (set) Token: 0x0600048E RID: 1166 RVA: 0x00020254 File Offset: 0x0001E454
	public float value
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00020264 File Offset: 0x0001E464
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00020284 File Offset: 0x0001E484
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration);
		tweenFOV.from = tweenFOV.value;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x000202CC File Offset: 0x0001E4CC
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x000202DC File Offset: 0x0001E4DC
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x000202EC File Offset: 0x0001E4EC
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x000202FC File Offset: 0x0001E4FC
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x0400040F RID: 1039
	public float from = 45f;

	// Token: 0x04000410 RID: 1040
	public float to = 45f;

	// Token: 0x04000411 RID: 1041
	private Camera mCam;
}
