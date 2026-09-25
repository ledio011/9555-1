using System;

// Token: 0x02000998 RID: 2456
public class SkipBtnRootLogic : SingletonUnity<SkipBtnRootLogic>
{
	// Token: 0x060045A1 RID: 17825 RVA: 0x0015EB94 File Offset: 0x0015CD94
	public void OnClickSkipBtn()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		switch (sceneManager.CurrentMapInofData.MapType)
		{
		case MAPTYPE.TUTORIAL_CAR:
			(sceneManager as NewTutorialSceneManager).curAnimaCtl.SkipAnima();
			break;
		case MAPTYPE.SINGLE_RUN_POINT_COPY:
			(sceneManager as SingleRunPointSceneManager).CurAnimaCtl.SkipAnima();
			break;
		}
	}
}
