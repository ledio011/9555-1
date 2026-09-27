using System;
using UnityEngine;

// Token: 0x02000817 RID: 2071
public class CityDanceSceneObj : MonoBehaviour
{
	// Token: 0x060031E8 RID: 12776 RVA: 0x000C341C File Offset: 0x000C161C
	private void Start()
	{
		for (int i = 0; i < this.NPCDancerRoot.Length; i++)
		{
			ActionData actionDataByName = DataManager.GetActionDataByName("baiRen_XD_tiaoWu_fuFei");
			this.NPCDancerRoot[i].animation.playAutomatically = false;
			AnimationState animationState = this.NPCDancerRoot[i].animation[this.NPCDancerRoot[i].animation.clip.name];
			animationState.speed = animationState.length / actionDataByName.AnimDurationTimeSecond;
			animationState.normalizedTime = Time.realtimeSinceStartup % actionDataByName.AnimDurationTimeSecond / actionDataByName.AnimDurationTimeSecond;
			this.NPCDancerRoot[i].animation.CrossFade(animationState.name);
		}
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x000C34D0 File Offset: 0x000C16D0
	private void OnEnable()
	{
		MapInfoData currentMapInofData = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		SoundData soundDataById = DataManager.GetSoundDataById(108);
		if (soundDataById != null)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlayBGMusic(soundDataById.Id, soundDataById.FadeOutTime, soundDataById.FadeInTime);
		}
	}

	// Token: 0x04002157 RID: 8535
	public GameObject[] NPCDancerRoot;
}
