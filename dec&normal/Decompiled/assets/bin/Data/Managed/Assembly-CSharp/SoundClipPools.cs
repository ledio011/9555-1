using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000893 RID: 2195
public class SoundClipPools
{
	// Token: 0x06003B5C RID: 15196 RVA: 0x00102F70 File Offset: 0x00101170
	public void GetSoundClip(int nSoundId, SoundClipPools.GetAudioClipDelegate delFun, SoundClipPools.SoundClipParam param)
	{
		if (nSoundId >= 0)
		{
			if (this.mAudioClipMap.ContainsKey(nSoundId))
			{
				this.mAudioClipMap[nSoundId].m_LastActiveTime = Time.realtimeSinceStartup;
				if (delFun != null)
				{
					delFun(this.mAudioClipMap[nSoundId], param);
				}
				return;
			}
			if (this.mAudioClipMap.Count > SoundManager.m_SFXChannelsCount)
			{
				this.RemoveLastUnUsedClip();
			}
			SoundData soundDataById = DataManager.GetSoundDataById(nSoundId);
			if (soundDataById == null)
			{
				Debug.LogError("sound id " + nSoundId.ToString() + " is null");
				if (delFun != null)
				{
					delFun(null, param);
				}
				return;
			}
			string fullPathName = soundDataById.FullPathName;
			if (string.IsNullOrEmpty(fullPathName))
			{
				if (delFun != null)
				{
					delFun(null, param);
				}
				return;
			}
			string name = soundDataById.Name;
			if (string.IsNullOrEmpty(name))
			{
				if (delFun != null)
				{
					delFun(null, param);
				}
				return;
			}
			AudioClip audioClip = ResourcesManager.Load(fullPathName + "/" + name) as AudioClip;
			if (audioClip != null)
			{
				this.OnLoadSound(fullPathName + name, audioClip, soundDataById, delFun, param);
				return;
			}
			if (UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<SoundManager>.Instance.gameObject))
			{
				SingletonDontDestoryUnity<SoundManager>.Instance.StartCoroutine(BundleManager.LoadSound(fullPathName, name, new BundleManager.LoadSoundFinish(this.OnLoadSound), soundDataById, delFun, param));
			}
		}
	}

	// Token: 0x06003B5D RID: 15197 RVA: 0x001030C4 File Offset: 0x001012C4
	private void OnLoadSound(string soundPath, AudioClip curAudioClip, object param1, object param2, object param3 = null)
	{
		SoundClip soundClip = new SoundClip();
		soundClip.AudioClip = curAudioClip;
		SoundClipPools.GetAudioClipDelegate getAudioClipDelegate = param2 as SoundClipPools.GetAudioClipDelegate;
		SoundClipPools.SoundClipParam param4 = param3 as SoundClipPools.SoundClipParam;
		SoundData soundData = param1 as SoundData;
		if (null == soundClip.AudioClip)
		{
			Debug.LogError("sound clip " + soundPath + " is null");
			if (getAudioClipDelegate != null)
			{
				getAudioClipDelegate(null, param4);
			}
			return;
		}
		if (!soundClip.AudioClip.isReadyToPlay)
		{
			Debug.LogError("Cann't decompress the sound resource " + soundPath);
			if (getAudioClipDelegate != null)
			{
				getAudioClipDelegate(null, param4);
			}
			return;
		}
		soundClip.m_LastActiveTime = Time.realtimeSinceStartup;
		soundClip.m_delay = soundData.Delay;
		soundClip.m_minDistance = soundData.MinDistance;
		soundClip.m_panLevel = soundData.PanLevel;
		soundClip.m_spread = soundData.Spread;
		soundClip.m_volume = soundData.Volume;
		soundClip.m_isLoop = (soundData.IsLoop == 1);
		soundClip.m_path = soundPath;
		soundClip.m_name = soundData.Name;
		soundClip.m_audio_id = soundData.Id;
		soundClip.m_curMaxPlayingCount = soundData.CurMaxPlayingCount;
		if (!this.mAudioClipMap.ContainsKey(soundData.Id))
		{
			this.mAudioClipMap.Add(soundData.Id, soundClip);
		}
		if (getAudioClipDelegate != null)
		{
			getAudioClipDelegate(soundClip, param4);
		}
	}

	// Token: 0x06003B5E RID: 15198 RVA: 0x00103210 File Offset: 0x00101410
	private void RemoveLastUnUsedClip()
	{
		float num = 100000000f;
		int num2 = -1;
		foreach (SoundClip soundClip in this.mAudioClipMap.Values)
		{
			if (num > soundClip.m_LastActiveTime)
			{
				num2 = soundClip.m_audio_id;
				num = soundClip.m_LastActiveTime;
			}
		}
		this.mAudioClipMap.Remove(num2);
	}

	// Token: 0x06003B5F RID: 15199 RVA: 0x001032A4 File Offset: 0x001014A4
	public void ForceRemoveClipByID(int uid)
	{
		if (uid != -1)
		{
			int num = -1;
			foreach (SoundClip soundClip in this.mAudioClipMap.Values)
			{
				if (soundClip.m_audio_id == uid)
				{
					num = soundClip.m_audio_id;
					break;
				}
			}
			this.mAudioClipMap.Remove(num);
		}
	}

	// Token: 0x040026E4 RID: 9956
	private Dictionary<int, SoundClip> mAudioClipMap = new Dictionary<int, SoundClip>();

	// Token: 0x02000894 RID: 2196
	public class SoundClipParam
	{
		// Token: 0x06003B60 RID: 15200 RVA: 0x00103338 File Offset: 0x00101538
		public SoundClipParam(float volume, SoundClipPools.OnPlaySoundDelegate func = null)
		{
			this.m_Volume = volume;
			this.m_FadeInTime = 0f;
			this.m_FadeOutTime = 0f;
			this.onPlaySound = func;
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x00103370 File Offset: 0x00101570
		public SoundClipParam(int clipId, float fadeOutTime, float fadeInTime, SoundClipPools.OnPlaySoundDelegate func = null)
		{
			this.m_Volume = 1f;
			this.m_FadeInTime = fadeInTime;
			this.m_FadeOutTime = fadeOutTime;
			this.m_clip_id = clipId;
			this.onPlaySound = func;
		}

		// Token: 0x040026E5 RID: 9957
		public float m_Volume;

		// Token: 0x040026E6 RID: 9958
		public float m_FadeOutTime;

		// Token: 0x040026E7 RID: 9959
		public float m_FadeInTime;

		// Token: 0x040026E8 RID: 9960
		public int m_clip_id;

		// Token: 0x040026E9 RID: 9961
		public SoundClipPools.OnPlaySoundDelegate onPlaySound;
	}

	// Token: 0x02000AE7 RID: 2791
	// (Invoke) Token: 0x06005025 RID: 20517
	public delegate void GetAudioClipDelegate(SoundClip soundClip, SoundClipPools.SoundClipParam param);

	// Token: 0x02000AE8 RID: 2792
	// (Invoke) Token: 0x06005029 RID: 20521
	public delegate void OnPlaySoundDelegate(AudioSource audioSource);
}
