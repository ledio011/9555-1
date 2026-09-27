using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000895 RID: 2197
public class SoundManager : SingletonDontDestoryUnity<SoundManager>
{
	// Token: 0x17000F7D RID: 3965
	// (set) Token: 0x06003B64 RID: 15204 RVA: 0x00103410 File Offset: 0x00101610
	public float sfxVolume
	{
		set
		{
			SoundManager.m_sfxVolume = value;
		}
	}

	// Token: 0x17000F7E RID: 3966
	// (set) Token: 0x06003B65 RID: 15205 RVA: 0x00103418 File Offset: 0x00101618
	public float bgmVolume
	{
		set
		{
			SoundManager.m_bgmVolume = value;
			this.m_BGAudioSource.m_AudioSource.volume = this.m_CurBGVolume * SoundManager.m_bgmVolume;
		}
	}

	// Token: 0x17000F7F RID: 3967
	// (get) Token: 0x06003B66 RID: 15206 RVA: 0x00103448 File Offset: 0x00101648
	// (set) Token: 0x06003B67 RID: 15207 RVA: 0x00103450 File Offset: 0x00101650
	public bool EnableSFX
	{
		get
		{
			return SoundManager.m_EnableSFX;
		}
		set
		{
			SoundManager.m_EnableSFX = value;
		}
	}

	// Token: 0x17000F80 RID: 3968
	// (get) Token: 0x06003B68 RID: 15208 RVA: 0x00103458 File Offset: 0x00101658
	// (set) Token: 0x06003B69 RID: 15209 RVA: 0x00103460 File Offset: 0x00101660
	public bool EnableBGM
	{
		get
		{
			return SoundManager.m_EnableBGM;
		}
		set
		{
			if (SoundManager.m_EnableBGM && !value)
			{
				if (this.m_BGAudioSource != null && this.m_BGAudioSource.m_AudioSource.isPlaying)
				{
					this.m_BGAudioSource.m_AudioSource.Stop();
				}
			}
			else if (!SoundManager.m_EnableBGM && value && this.m_BGAudioSource != null)
			{
				this.PlayBGMWithFade(this.m_lastMusicID, 0.1f, 0f);
			}
			SoundManager.m_EnableBGM = value;
		}
	}

	// Token: 0x06003B6A RID: 15210 RVA: 0x001034EC File Offset: 0x001016EC
	protected override void Awake()
	{
		base.Awake();
		if (!SingletonDontDestoryUnity<SoundManager>.Exists || !base.IsInit)
		{
			return;
		}
		for (int i = 0; i < SoundManager.m_SFXChannelsCount; i++)
		{
			this.m_SFXChannel[i] = new SoundManager.MyAudioSource();
			this.m_SFXChannel[i].m_audioID = -1;
			this.m_SFXChannel[i].m_AudioSource = base.gameObject.AddComponent<AudioSource>();
		}
		this.m_BGAudioSource.m_AudioSource = base.gameObject.AddComponent<AudioSource>();
		this.m_BGAudioSource.m_audioID = -1;
		this.m_SoundClipPools = new SoundClipPools();
	}

	// Token: 0x06003B6B RID: 15211 RVA: 0x0010358C File Offset: 0x0010178C
	private void Start()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SystemMusic == 0 || GameSettingData.IsLowPhone)
		{
			this.EnableBGM = false;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SystemSoundEffect == 0 || GameSettingData.IsLowPhone)
		{
			this.EnableSFX = false;
		}
		this.sfxVolume = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SoundDragValue;
		this.bgmVolume = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MusicDragValue;
	}

	// Token: 0x06003B6C RID: 15212 RVA: 0x00103610 File Offset: 0x00101810
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06003B6D RID: 15213 RVA: 0x00103618 File Offset: 0x00101818
	private void FixedUpdate()
	{
		this.UpdateBGMusic();
	}

	// Token: 0x06003B6E RID: 15214 RVA: 0x00103620 File Offset: 0x00101820
	private void UpdateBGMusic()
	{
		if (this.m_fadeMode == SoundManager.FadeMode.FadeOut)
		{
			if (Mathf.Abs(this.m_fadeOutTime) < 0.001f)
			{
				return;
			}
			this.m_fadeOutTimer += Time.deltaTime;
			this.m_BGAudioSource.m_AudioSource.volume = (1f - this.m_fadeOutTimer / this.m_fadeOutTime) * this.m_CurBGVolume * SoundManager.m_bgmVolume;
			if (this.m_fadeOutTimer >= this.m_fadeOutTime)
			{
				int audioID = this.m_BGAudioSource.m_audioID;
				this.SetBGMAudioSource(ref this.m_BGAudioSource, this.m_NextSoundClip, 1f);
				this.m_SoundClipPools.ForceRemoveClipByID(audioID);
				this.m_CurBGVolume = this.m_NextSoundClip.m_volume;
				if (this.m_fadeInTime > 0f)
				{
					this.m_fadeMode = SoundManager.FadeMode.FadeIn;
					this.m_fadeOutTimer = 0f;
					this.m_BGAudioSource.m_AudioSource.volume = 0f;
				}
				else
				{
					this.m_fadeMode = SoundManager.FadeMode.None;
				}
				this.m_BGAudioSource.m_AudioSource.Play();
			}
		}
		else if (this.m_fadeMode == SoundManager.FadeMode.FadeIn)
		{
			if (Mathf.Abs(this.m_fadeInTime) < 0.001f)
			{
				return;
			}
			this.m_fadeInTimer += Time.deltaTime;
			this.m_BGAudioSource.m_AudioSource.volume = this.m_fadeInTimer / this.m_fadeInTime * this.m_CurBGVolume * SoundManager.m_bgmVolume;
			if (this.m_fadeInTimer >= this.m_fadeInTime)
			{
				this.m_fadeMode = SoundManager.FadeMode.None;
				this.m_fadeInTimer = 0f;
				this.m_BGAudioSource.m_AudioSource.volume = this.m_CurBGVolume * SoundManager.m_bgmVolume;
			}
		}
	}

	// Token: 0x06003B6F RID: 15215 RVA: 0x001037D8 File Offset: 0x001019D8
	private void PlayBGMWithFade(int nSoundclipId, float fadeOutTime, float fadeInTime)
	{
		this.m_SoundClipPools.GetSoundClip(nSoundclipId, new SoundClipPools.GetAudioClipDelegate(this.OnPlayBGMWithFade), new SoundClipPools.SoundClipParam(nSoundclipId, fadeOutTime, fadeInTime, null));
	}

	// Token: 0x06003B70 RID: 15216 RVA: 0x001037FC File Offset: 0x001019FC
	private void OnPlayBGMWithFade(SoundClip bgSoundClip, SoundClipPools.SoundClipParam param)
	{
		if (this.m_BGAudioSource != null && bgSoundClip != null)
		{
			this.m_lastMusicID = param.m_clip_id;
			if (this.m_BGAudioSource.m_AudioSource.isPlaying)
			{
				if (this.m_NextSoundClip != null && this.m_NextSoundClip.m_audio_id == bgSoundClip.m_audio_id)
				{
					return;
				}
				this.m_fadeOutTime = param.m_FadeOutTime;
				this.m_fadeInTime = param.m_FadeInTime;
				this.m_fadeOutTimer = 0f;
				this.m_fadeInTimer = 0f;
				if (this.m_fadeOutTime <= 0f)
				{
					int audioID = this.m_BGAudioSource.m_audioID;
					this.SetBGMAudioSource(ref this.m_BGAudioSource, bgSoundClip, 1f);
					this.m_SoundClipPools.ForceRemoveClipByID(audioID);
					this.m_CurBGVolume = bgSoundClip.m_volume;
					if (this.m_fadeInTime <= 0f)
					{
						this.m_BGAudioSource.m_AudioSource.Play();
						this.m_fadeMode = SoundManager.FadeMode.None;
					}
					else
					{
						this.m_BGAudioSource.m_AudioSource.volume = 0f;
						this.m_BGAudioSource.m_AudioSource.Play();
						this.m_fadeMode = SoundManager.FadeMode.FadeIn;
					}
				}
				else
				{
					this.m_NextSoundClip = bgSoundClip;
					this.m_fadeMode = SoundManager.FadeMode.FadeOut;
				}
			}
			else
			{
				this.m_fadeInTime = param.m_FadeInTime;
				this.m_fadeInTimer = 0f;
				int audioID2 = this.m_BGAudioSource.m_audioID;
				this.SetBGMAudioSource(ref this.m_BGAudioSource, bgSoundClip, 1f);
				this.m_SoundClipPools.ForceRemoveClipByID(audioID2);
				this.m_CurBGVolume = bgSoundClip.m_volume;
				if (this.m_fadeInTime <= 0f)
				{
					this.m_BGAudioSource.m_AudioSource.Play();
					this.m_fadeMode = SoundManager.FadeMode.None;
				}
				else
				{
					this.m_BGAudioSource.m_AudioSource.volume = 0f;
					this.m_BGAudioSource.m_AudioSource.Play();
					this.m_fadeMode = SoundManager.FadeMode.FadeIn;
				}
			}
		}
	}

	// Token: 0x06003B71 RID: 15217 RVA: 0x001039E8 File Offset: 0x00101BE8
	public void PlayBGMusic(int nClipID, float fadeOutTime, float fadeInTime)
	{
		if (AudioListener.volume == 0f)
		{
			return;
		}
		if (!GameSettingData.IsSoundCanPlay[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		if (nClipID < 0)
		{
			Debug.LogError("PlayBGM id < 0");
			return;
		}
		this.m_lastMusicID = nClipID;
		if (SoundManager.m_EnableBGM)
		{
			this.PlayBGMWithFade(nClipID, fadeOutTime, fadeInTime);
		}
	}

	// Token: 0x06003B72 RID: 15218 RVA: 0x00103A48 File Offset: 0x00101C48
	public void StopBGM(float _fadeTime)
	{
		if (SoundManager.m_EnableBGM && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.StopBGMWithFade(_fadeTime));
		}
	}

	// Token: 0x06003B73 RID: 15219 RVA: 0x00103A80 File Offset: 0x00101C80
	private IEnumerator StopBGMWithFade(float _fadeTime)
	{
		if (this.m_BGAudioSource != null && this.m_BGAudioSource.m_AudioSource.isPlaying)
		{
			float time = _fadeTime;
			while (time > 0f)
			{
				this.m_BGAudioSource.m_AudioSource.volume = time / this.m_fadeOutTime * SoundManager.m_bgmVolume;
				time -= Time.deltaTime;
				yield return null;
			}
			this.m_BGAudioSource.m_AudioSource.Stop();
		}
		yield break;
	}

	// Token: 0x06003B74 RID: 15220 RVA: 0x00103AAC File Offset: 0x00101CAC
	public void PlaySoundEffectAtPos2(int nSoundID, Vector3 playSoundPos, Vector3 listenerPos)
	{
		if (!GameSettingData.IsSoundCanPlay[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		if (nSoundID < 0)
		{
			return;
		}
		listenerPos.y = 0f;
		playSoundPos.y = 0f;
		float num = Vector3.Distance(listenerPos, playSoundPos);
		float num2 = 1f - num / 10f;
		if (num2 < 0.05f)
		{
			return;
		}
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		this.PlaySoundEffect(nSoundID, num2, null);
	}

	// Token: 0x06003B75 RID: 15221 RVA: 0x00103B34 File Offset: 0x00101D34
	public void PlaySoundEffect(int nSoundID, float volumeFactor = 1f, SoundClipPools.OnPlaySoundDelegate onPlaySound = null)
	{
		if (!GameSettingData.IsSoundCanPlay[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		if (AudioListener.volume == 0f)
		{
			return;
		}
		if (!SoundManager.m_EnableSFX)
		{
			return;
		}
		if (base.name == null)
		{
			Debug.LogError("PlaySoundEffect name is null");
			return;
		}
		base.name = base.name.Trim();
		this.m_SoundClipPools.GetSoundClip(nSoundID, new SoundClipPools.GetAudioClipDelegate(this.OnPlaySoundEffect), new SoundClipPools.SoundClipParam(volumeFactor, onPlaySound));
	}

	// Token: 0x06003B76 RID: 15222 RVA: 0x00103BB4 File Offset: 0x00101DB4
	private void OnPlaySoundEffect(SoundClip soundClip, SoundClipPools.SoundClipParam param)
	{
		if (soundClip == null)
		{
			Debug.LogError("soundClip is null");
			return;
		}
		this.PlaySoundEffect(soundClip, param.m_Volume, param.onPlaySound);
	}

	// Token: 0x06003B77 RID: 15223 RVA: 0x00103BE8 File Offset: 0x00101DE8
	private void PlaySoundEffect(SoundClip soundClip, float volumeFactor, SoundClipPools.OnPlaySoundDelegate onPlaySound = null)
	{
		if (SoundManager.m_EnableSFX && !string.IsNullOrEmpty(soundClip.m_path))
		{
			if (soundClip.AudioClip == null)
			{
				Debug.Log("PlaySoundEffect soundClip.Audioclip is null");
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			int num4 = -1;
			for (int i = 0; i < SoundManager.m_SFXChannelsCount; i++)
			{
				if (this.m_SFXChannel[i] == null)
				{
					return;
				}
				if (num3 == -1 && !this.m_SFXChannel[i].m_AudioSource.isPlaying)
				{
					num3 = i;
				}
				if (!(this.m_SFXChannel[i].m_AudioSource.clip == null))
				{
					if (this.m_SFXChannel[i].m_audioID == soundClip.m_audio_id)
					{
						if (num4 == -1 && !this.m_SFXChannel[i].m_AudioSource.isPlaying)
						{
							num4 = i;
						}
						if (this.m_SFXChannel[i].m_AudioSource.isPlaying)
						{
							num2++;
						}
						if (num2 >= soundClip.m_curMaxPlayingCount)
						{
							break;
						}
					}
					if (this.m_SFXChannel[num].m_AudioSource.priority < this.m_SFXChannel[i].m_AudioSource.priority)
					{
						num = i;
					}
				}
			}
			if (num2 < soundClip.m_curMaxPlayingCount)
			{
				int nValidIndex = -1;
				if (num4 != -1)
				{
					nValidIndex = num4;
				}
				else if (num3 != -1)
				{
					nValidIndex = num3;
				}
				else
				{
					nValidIndex = num;
				}
				if (nValidIndex >= 0 && nValidIndex < SoundManager.m_SFXChannelsCount)
				{
					this.m_SFXChannel[nValidIndex].m_AudioSource.Stop();
					this.SetSFXAudioSource(ref this.m_SFXChannel[nValidIndex], soundClip, volumeFactor);
					if (soundClip.m_delay > 1E-45f)
					{
						vp_Timer.In(soundClip.m_delay, delegate()
						{
							this.m_SFXChannel[nValidIndex].m_AudioSource.Play();
						}, null);
					}
					else
					{
						this.m_SFXChannel[nValidIndex].m_AudioSource.Play();
					}
					if (onPlaySound != null)
					{
						onPlaySound(this.m_SFXChannel[nValidIndex].m_AudioSource);
					}
					return;
				}
			}
		}
	}

	// Token: 0x06003B78 RID: 15224 RVA: 0x00103E44 File Offset: 0x00102044
	public void StopSoundEffect(int nSoundID)
	{
		if (this.m_SFXChannel == null)
		{
			return;
		}
		for (int i = 0; i < SoundManager.m_SFXChannelsCount; i++)
		{
			if (!(this.m_SFXChannel[i].m_AudioSource == null))
			{
				if (this.m_SFXChannel[i].m_audioID == nSoundID)
				{
					this.m_SFXChannel[i].m_AudioSource.Stop();
				}
			}
		}
	}

	// Token: 0x06003B79 RID: 15225 RVA: 0x00103EB8 File Offset: 0x001020B8
	public void StopAllSoundEffect()
	{
		if (this.m_SFXChannel != null)
		{
			for (int i = 0; i < SoundManager.m_SFXChannelsCount; i++)
			{
				if (this.m_SFXChannel[i] != null)
				{
					this.m_SFXChannel[i].m_AudioSource.Stop();
				}
			}
		}
	}

	// Token: 0x06003B7A RID: 15226 RVA: 0x00103F08 File Offset: 0x00102108
	private void SetSFXAudioSource(ref SoundManager.MyAudioSource audioSource, SoundClip clip, float volume)
	{
		if (clip == null)
		{
			return;
		}
		audioSource.m_AudioSource.clip = clip.AudioClip;
		audioSource.m_AudioSource.volume = clip.m_volume * volume * SoundManager.m_sfxVolume;
		audioSource.m_AudioSource.spread = clip.m_spread;
		audioSource.m_AudioSource.priority = clip.m_priority;
		audioSource.m_AudioSource.panLevel = clip.m_panLevel;
		audioSource.m_AudioSource.minDistance = clip.m_minDistance;
		audioSource.m_AudioSource.loop = clip.m_isLoop;
		audioSource.m_audioID = clip.m_audio_id;
		audioSource.m_AudioSource.pitch = 1f;
	}

	// Token: 0x06003B7B RID: 15227 RVA: 0x00103FC0 File Offset: 0x001021C0
	private void SetBGMAudioSource(ref SoundManager.MyAudioSource audioSource, SoundClip clip, float volume)
	{
		if (clip == null)
		{
			return;
		}
		audioSource.m_AudioSource.clip = clip.AudioClip;
		audioSource.m_AudioSource.volume = clip.m_volume * volume * SoundManager.m_bgmVolume;
		audioSource.m_AudioSource.spread = clip.m_spread;
		audioSource.m_AudioSource.priority = clip.m_priority;
		audioSource.m_AudioSource.panLevel = clip.m_panLevel;
		audioSource.m_AudioSource.minDistance = clip.m_minDistance;
		audioSource.m_AudioSource.loop = clip.m_isLoop;
		audioSource.m_audioID = clip.m_audio_id;
		audioSource.m_AudioSource.pitch = 1f;
	}

	// Token: 0x040026EA RID: 9962
	public SoundClipPools m_SoundClipPools;

	// Token: 0x040026EB RID: 9963
	private SoundManager.MyAudioSource m_BGAudioSource = new SoundManager.MyAudioSource();

	// Token: 0x040026EC RID: 9964
	private float m_CurBGVolume;

	// Token: 0x040026ED RID: 9965
	public static int m_SFXChannelsCount = 30;

	// Token: 0x040026EE RID: 9966
	private SoundManager.MyAudioSource[] m_SFXChannel = new SoundManager.MyAudioSource[SoundManager.m_SFXChannelsCount];

	// Token: 0x040026EF RID: 9967
	private SoundClip m_NextSoundClip;

	// Token: 0x040026F0 RID: 9968
	public static bool m_EnableBGM = true;

	// Token: 0x040026F1 RID: 9969
	public static bool m_EnableSFX = true;

	// Token: 0x040026F2 RID: 9970
	private int m_lastMusicID = -1;

	// Token: 0x040026F3 RID: 9971
	public static float m_sfxVolume = 1f;

	// Token: 0x040026F4 RID: 9972
	public static float m_bgmVolume = 1f;

	// Token: 0x040026F5 RID: 9973
	private SoundManager.FadeMode m_fadeMode;

	// Token: 0x040026F6 RID: 9974
	private float m_fadeOutTime;

	// Token: 0x040026F7 RID: 9975
	private float m_fadeOutTimer;

	// Token: 0x040026F8 RID: 9976
	private float m_fadeInTime;

	// Token: 0x040026F9 RID: 9977
	private float m_fadeInTimer;

	// Token: 0x02000896 RID: 2198
	public class MyAudioSource
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x00104078 File Offset: 0x00102278
		public MyAudioSource()
		{
			this.m_audioID = -1;
			this.m_AudioSource = null;
		}

		// Token: 0x040026FA RID: 9978
		public int m_audioID;

		// Token: 0x040026FB RID: 9979
		public AudioSource m_AudioSource;
	}

	// Token: 0x02000897 RID: 2199
	private enum FadeMode
	{
		// Token: 0x040026FD RID: 9981
		None,
		// Token: 0x040026FE RID: 9982
		FadeIn,
		// Token: 0x040026FF RID: 9983
		FadeOut
	}
}
