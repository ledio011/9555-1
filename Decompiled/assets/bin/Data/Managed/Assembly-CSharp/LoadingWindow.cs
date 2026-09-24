using System;
using System.Collections;
using UnityEngine;

// Token: 0x020009EB RID: 2539
public class LoadingWindow : MonoBehaviour
{
	// Token: 0x06004849 RID: 18505 RVA: 0x00172A1C File Offset: 0x00170C1C
	public static void LoadScene(int sceneDefine)
	{
		LoadingWindow.isSendMapReady = false;
		if (SingletonUnity<UIManager>.Exists)
		{
			SingletonUnity<UIManager>.Instance.UICheckChangeScene();
		}
		NetLogic.GetInstance().FinishReconnecting();
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null)
		{
			LoadingWindow.preSceneId = SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr;
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CloseActivityObj();
			MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(LoadingWindow.preSceneId);
			if (mapInfoDataByID.MapType == MAPTYPE.TUTORIAL_CAR)
			{
				NewTutorialSceneManager newTutorialSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager;
				if (newTutorialSceneManager != null)
				{
					UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(newTutorialSceneManager.CheckLockArea));
				}
			}
		}
		LoadingWindow.nextSceneName = DataManager.GetMapInfoDataByID(sceneDefine.ToString()).SceneName;
		if (sceneDefine == 11 && !GameManager.IsSupportCurDataVersion200())
		{
			LoadingWindow.nextSceneName = "DSJ_GTA";
		}
		SingletonDontDestoryUnity<GameManager>.Instance.RunningMapId = sceneDefine;
		LoadingWindow.ChangeSceneClearAction();
		BundleManager.UnloadCacheActObj();
		BundleManager.IsCanUnloadBundle = false;
		Application.LoadLevel("LoadingScene");
	}

	// Token: 0x0600484A RID: 18506 RVA: 0x00172B20 File Offset: 0x00170D20
	public static void ChangeSceneClearAction()
	{
		vp_Timer.DestroyAll();
		LoadingWindow.UnloadResource();
		Singleton<ObjManager>.Instance.ClearAll();
	}

	// Token: 0x0600484B RID: 18507 RVA: 0x00172B38 File Offset: 0x00170D38
	private static void UnloadResource()
	{
		EffectLogic.Clear();
	}

	// Token: 0x0600484C RID: 18508 RVA: 0x00172B40 File Offset: 0x00170D40
	private void Start()
	{
		BundleManager.IsCanUnloadBundle = true;
		this.mLoadProgress = 0f;
		this.mUILoadProgress = 0f;
		BundleManager.UnloadCacheActObj();
		BundleManager.ClearCacheTexture();
		if (SingletonDontDestoryUnity<GameManager>.Instance.RunningMapId == 0)
		{
			BundleManager.ClearCacheModelBundle(true);
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.ResetPlayerData();
			instance.PlayerCommonData.ClearData();
			instance.ClearServerRefresh();
			DataManager.initFlag = false;
			AnimationManager.initDownloadFlag = false;
			SingletonDontDestoryUnity<NetManager>.Instance.ClearLastCheckNeedQuitUpdateVersion();
			this.mLoadAsync = Application.LoadLevelAsync(LoadingWindow.nextSceneName);
		}
		else
		{
			BundleManager.ClearCacheModelBundle(false);
			MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr);
			if (mapInfoDataByID.DirectLoad == 1)
			{
				this.mLoadAsync = Application.LoadLevelAsync(LoadingWindow.nextSceneName);
			}
			else if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(this.LoadScene(LoadingWindow.nextSceneName));
			}
		}
	}

	// Token: 0x0600484D RID: 18509 RVA: 0x00172C30 File Offset: 0x00170E30
	private void Update()
	{
		if (this.mLoadAsync != null)
		{
			this.mLoadProgress = this.mLoadAsync.progress;
		}
		if (this.mUILoadProgress < this.mLoadProgress)
		{
			this.mUILoadProgress += LoadingWindow.LoadingSpeed;
			LoadingWindow.LoadingProgress = this.mUILoadProgress;
		}
		if (SingletonUnity<LoadingUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<LoadingUIRoot>.Instance.gameObject))
		{
			SingletonUnity<LoadingUIRoot>.Instance.SetProgress(this.mUILoadProgress);
		}
	}

	// Token: 0x0600484E RID: 18510 RVA: 0x00172CB8 File Offset: 0x00170EB8
	private IEnumerator LoadScene(string nextScene)
	{
		yield return base.StartCoroutine(BundleManager.LoadScene(nextScene, new BundleManager.OnLoadSceneFinish(this.OnLoadSceneFinish)));
		yield break;
	}

	// Token: 0x0600484F RID: 18511 RVA: 0x00172CE4 File Offset: 0x00170EE4
	private void OnLoadSceneFinish(bool isSuccess, string sceneName, AssetBundle sceneBundle)
	{
		if (!isSuccess)
		{
			this.LoadScene(LoadingWindow.preSceneId);
			return;
		}
		this.mLoadAsync = Application.LoadLevelAsync(sceneName);
	}

	// Token: 0x040035A2 RID: 13730
	public static float LoadingProgress = 0f;

	// Token: 0x040035A3 RID: 13731
	public static float LoadingSpeed = 0.02f;

	// Token: 0x040035A4 RID: 13732
	public static bool BlackLoading = false;

	// Token: 0x040035A5 RID: 13733
	private static string nextSceneName = string.Empty;

	// Token: 0x040035A6 RID: 13734
	public static string preSceneId = string.Empty;

	// Token: 0x040035A7 RID: 13735
	public static bool isSendMapReady = false;

	// Token: 0x040035A8 RID: 13736
	private AsyncOperation mLoadAsync;

	// Token: 0x040035A9 RID: 13737
	private float mLoadProgress;

	// Token: 0x040035AA RID: 13738
	private float mUILoadProgress;
}
