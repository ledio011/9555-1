using System;
using Sproto;
using SprotoType;

// Token: 0x02000221 RID: 545
public class ask_confirm_multi_copy_scene_handler
{
	// Token: 0x060012BC RID: 4796 RVA: 0x0007AB10 File Offset: 0x00078D10
	public static SprotoTypeBase ask_confirm_multi_copy_scene_request(SprotoTypeBase req)
	{
		ask_confirm_multi_copy_scene.request request = req as ask_confirm_multi_copy_scene.request;
		if (request != null)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			CopySceneData copySceneData = null;
			if (request.type1 == 1L)
			{
				copySceneData = DataManager.GetCopySceneDataById(request.id);
			}
			ask_confirm_multi_copy_scene_handler.mCurCopySession = request.session;
			ask_confirm_multi_copy_scene_handler.preTutorialStep = TUTORIAL_STEP.INVALID;
			if (UIManager.IsUnlockTutorialEnable())
			{
				if (TutorialManager.CurStep != TUTORIAL_STEP.JOIN_TEAM_START && TutorialManager.CurStep != TUTORIAL_STEP.JOIN_TEAM_SHOW_1 && TutorialManager.CurStep != TUTORIAL_STEP.JOIN_TEAM_CLICK_URGE && TutorialManager.CurStep != TUTORIAL_STEP.JOIN_TEAM_FINISH)
				{
					ask_confirm_multi_copy_scene_handler.OnClickCancelBtn();
					return null;
				}
				ask_confirm_multi_copy_scene_handler.preTutorialStep = TutorialManager.CurStep;
				TutorialManager.CloseTutorial();
			}
			Team teamInfo = playerData.TeamInfo;
			teamInfo.IsCheckingEnterCopy = true;
			teamInfo.ClearMemberEnterCopyState();
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager != null && !sceneManager.IsShowTeamScene() && copySceneData != null)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamPreparationRoot, delegate
				{
					SingletonUnity<TeamPreparationRootLogic>.Instance.Reset(ask_confirm_multi_copy_scene_handler.mCurCopySession, true);
				}, null);
			}
			if (SingletonUnity<TeamUIRootNewLogic>.Exists)
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
			}
		}
		return null;
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x0007AC40 File Offset: 0x00078E40
	private static void OnClickOkBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
		{
			ret_ask_confirm_multi_copy_scene.request request = new ret_ask_confirm_multi_copy_scene.request();
			request.state = 1L;
			request.session = ask_confirm_multi_copy_scene_handler.mCurCopySession;
			NetLogic.GetInstance().Send<Protocol.ret_ask_confirm_multi_copy_scene>(request, null);
		}
		else
		{
			ret_ask_confirm_multi_copy_scene.request request2 = new ret_ask_confirm_multi_copy_scene.request();
			request2.state = 0L;
			request2.session = ask_confirm_multi_copy_scene_handler.mCurCopySession;
			NetLogic.GetInstance().Send<Protocol.ret_ask_confirm_multi_copy_scene>(request2, null);
			Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
			teamInfo.SelfMember.IsReadyEnterCopy = true;
			if (SingletonUnity<TeamUIRootNewLogic>.Exists)
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
			}
		}
		if (ask_confirm_multi_copy_scene_handler.preTutorialStep != TUTORIAL_STEP.INVALID)
		{
			TutorialManager.ShowTutorial(ask_confirm_multi_copy_scene_handler.preTutorialStep);
		}
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x0007ACF8 File Offset: 0x00078EF8
	private static void OnClickCancelBtn()
	{
		ret_ask_confirm_multi_copy_scene.request request = new ret_ask_confirm_multi_copy_scene.request();
		request.state = 1L;
		request.session = ask_confirm_multi_copy_scene_handler.mCurCopySession;
		NetLogic.GetInstance().Send<Protocol.ret_ask_confirm_multi_copy_scene>(request, null);
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
		{
			Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
			teamInfo.SelfMember.IsReadyEnterCopy = false;
			teamInfo.IsCheckingEnterCopy = false;
			if (SingletonUnity<TeamUIRootNewLogic>.Exists)
			{
				SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
			}
		}
		if (ask_confirm_multi_copy_scene_handler.preTutorialStep != TUTORIAL_STEP.INVALID)
		{
			TutorialManager.ShowTutorial(ask_confirm_multi_copy_scene_handler.preTutorialStep);
		}
	}

	// Token: 0x040017DB RID: 6107
	private static long mCurCopySession;

	// Token: 0x040017DC RID: 6108
	private static TUTORIAL_STEP preTutorialStep;
}
