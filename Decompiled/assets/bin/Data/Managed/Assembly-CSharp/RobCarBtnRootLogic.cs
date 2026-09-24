using System;

// Token: 0x02000985 RID: 2437
public class RobCarBtnRootLogic : SingletonUnity<RobCarBtnRootLogic>
{
	// Token: 0x060044F0 RID: 17648 RVA: 0x001590E8 File Offset: 0x001572E8
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060044F1 RID: 17649 RVA: 0x001590F4 File Offset: 0x001572F4
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060044F2 RID: 17650 RVA: 0x00159114 File Offset: 0x00157314
	public void Reset(ObjFakeAICar mCurNearestCar)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.ROB_CAR_TIP))
		{
			TutorialManager.ShowTutorial(TUTORIAL_STEP.ROB_CAR_START);
			return;
		}
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager != null && sceneManager.CheckCarIsMissionCar(mCurNearestCar))
		{
			this.OnClickRobCarBtn();
		}
	}

	// Token: 0x060044F3 RID: 17651 RVA: 0x00159168 File Offset: 0x00157368
	public void OnClickRobCarBtn()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.ROB_CAR_START)
		{
			this.CheckTutorialEvent();
		}
		SingletonUnity<CitySimController>.Instance.RobCar(null);
	}

	// Token: 0x040031BE RID: 12734
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040031BF RID: 12735
	public UITexture RobCarBtn;
}
