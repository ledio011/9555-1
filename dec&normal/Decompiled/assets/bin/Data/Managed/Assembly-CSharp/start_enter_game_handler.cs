using System;
using Sproto;
using SprotoType;

// Token: 0x020002BE RID: 702
public class start_enter_game_handler
{
	// Token: 0x06001403 RID: 5123 RVA: 0x00081D34 File Offset: 0x0007FF34
	public static SprotoTypeBase start_enter_game_request(SprotoTypeBase req)
	{
		start_enter_game.request request = req as start_enter_game.request;
		if (request != null && request.state == 1L)
		{
			WaitResponseUIRootLogic.CloseBox();
			if (SingletonUnity<DownLoadResRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DownLoadResRootLogic>.Instance.gameObject))
			{
				SingletonUnity<DownLoadResRootLogic>.Instance.OnClickCloseBtn();
			}
			if (SingletonUnity<DownloadTipRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DownloadTipRootLogic>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DownLoadTipRoot);
			}
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload = true;
			if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCanUsePotion() && !Singleton<ObjManager>.Instance.MainPlayer.IsLocalDrivingCar)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PotionObjRoot, delegate
				{
					SingletonUnity<PotionLogic>.Instance.Reset();
				}, null);
			}
			if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.refershBtn();
				SingletonUnity<FunctionBtnRootLogic>.Instance.ResetRightBtn();
			}
		}
		return null;
	}
}
