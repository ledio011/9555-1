using System;
using UnityEngine;

// Token: 0x02000892 RID: 2194
[Serializable]
public class SoundClip
{
	// Token: 0x17000F7C RID: 3964
	// (get) Token: 0x06003B59 RID: 15193 RVA: 0x00102F48 File Offset: 0x00101148
	// (set) Token: 0x06003B5A RID: 15194 RVA: 0x00102F50 File Offset: 0x00101150
	public AudioClip AudioClip
	{
		get
		{
			return this.m_AudioClip;
		}
		set
		{
			this.m_AudioClip = value;
		}
	}

	// Token: 0x040026D7 RID: 9943
	private AudioClip m_AudioClip;

	// Token: 0x040026D8 RID: 9944
	public int m_priority = 128;

	// Token: 0x040026D9 RID: 9945
	public string m_name = string.Empty;

	// Token: 0x040026DA RID: 9946
	public string m_path = string.Empty;

	// Token: 0x040026DB RID: 9947
	public float m_minDistance = 10f;

	// Token: 0x040026DC RID: 9948
	public float m_volume = 1f;

	// Token: 0x040026DD RID: 9949
	public float m_delay;

	// Token: 0x040026DE RID: 9950
	public float m_panLevel;

	// Token: 0x040026DF RID: 9951
	public float m_spread;

	// Token: 0x040026E0 RID: 9952
	public bool m_isLoop;

	// Token: 0x040026E1 RID: 9953
	public int m_curMaxPlayingCount = 1;

	// Token: 0x040026E2 RID: 9954
	public int m_audio_id = -1;

	// Token: 0x040026E3 RID: 9955
	public float m_LastActiveTime;
}
