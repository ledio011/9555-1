using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
[RequireComponent(typeof(Camera))]
public class TweenOrthoSize : UITweener
{
	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000204B0 File Offset: 0x0001E6B0
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

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000204D8 File Offset: 0x0001E6D8
	// (set) Token: 0x060004A4 RID: 1188 RVA: 0x000204E0 File Offset: 0x0001E6E0
	[Obsolete("Use 'value' instead")]
	public float orthoSize
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

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000204EC File Offset: 0x0001E6EC
	// (set) Token: 0x060004A6 RID: 1190 RVA: 0x000204FC File Offset: 0x0001E6FC
	public float value
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002050C File Offset: 0x0001E70C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002052C File Offset: 0x0001E72C
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration);
		tweenOrthoSize.from = tweenOrthoSize.value;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00020574 File Offset: 0x0001E774
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00020584 File Offset: 0x0001E784
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000417 RID: 1047
	public float from = 1f;

	// Token: 0x04000418 RID: 1048
	public float to = 1f;

	// Token: 0x04000419 RID: 1049
	private Camera mCam;
}
