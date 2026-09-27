using System;
using UnityEngine;

// Token: 0x02000A1B RID: 2587
public class RedScreenRootLogic : SingletonUnity<RedScreenRootLogic>
{
	// Token: 0x06004A90 RID: 19088 RVA: 0x00188274 File Offset: 0x00186474
	public static void EnableRedScreen()
	{
		if (!SingletonUnity<RedScreenRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<RedScreenRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RedScreenRoot, delegate
			{
				SingletonUnity<RedScreenRootLogic>.Instance.Reset();
			}, null);
		}
		if (GameManager.IsSupportCurDataVersion() && TutorialManager.IsTutorialCanShow() && SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.LOW_HP_USE_DRUG_TIP))
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (!mainPlayer.IsOpenAutoCombat && Time.time > RedScreenRootLogic.startTime)
			{
				TutorialManager.ShowTutorial(TUTORIAL_STEP.DRUG_USE_TIP_START);
				RedScreenRootLogic.startTime = Time.time + 5f;
			}
		}
	}

	// Token: 0x06004A91 RID: 19089 RVA: 0x00188338 File Offset: 0x00186538
	public static void DisableRedScreen()
	{
		if (SingletonUnity<RedScreenRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RedScreenRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RedScreenRoot);
		}
	}

	// Token: 0x06004A92 RID: 19090 RVA: 0x00188374 File Offset: 0x00186574
	public void Reset()
	{
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mMainPlayer == null || this.mMainPlayer.IsDie)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RedScreenRoot);
		}
	}

	// Token: 0x0400382D RID: 14381
	private static float startTime;

	// Token: 0x0400382E RID: 14382
	private ObjMainPlayer mMainPlayer;
}
