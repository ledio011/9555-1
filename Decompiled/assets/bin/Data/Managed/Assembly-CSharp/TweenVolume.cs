using System;
using UnityEngine;

// Token: 0x020000AB RID: 171
[AddComponentMenu("NGUI/Tween/Tween Volume")]
[RequireComponent(typeof(AudioSource))]
public class TweenVolume : UITweener
{
	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00020CD0 File Offset: 0x0001EED0
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.audio;
				if (this.mSource == null)
				{
					this.mSource = base.GetComponent<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00020D40 File Offset: 0x0001EF40
	// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00020D48 File Offset: 0x0001EF48
	[Obsolete("Use 'value' instead")]
	public float volume
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

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00020D54 File Offset: 0x0001EF54
	// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00020D88 File Offset: 0x0001EF88
	public float value
	{
		get
		{
			return (!(this.audioSource != null)) ? 0f : this.mSource.volume;
		}
		set
		{
			if (this.audioSource != null)
			{
				this.mSource.volume = value;
			}
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00020DA8 File Offset: 0x0001EFA8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00020DF0 File Offset: 0x0001EFF0
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		return tweenVolume;
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00020E1C File Offset: 0x0001F01C
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00020E2C File Offset: 0x0001F02C
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0400042E RID: 1070
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x0400042F RID: 1071
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000430 RID: 1072
	private AudioSource mSource;
}
