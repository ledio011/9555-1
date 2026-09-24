using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200020C RID: 524
public class GameManager : SingletonDontDestoryUnity<GameManager>
{
	// Token: 0x170003BA RID: 954
	// (get) Token: 0x060011B9 RID: 4537 RVA: 0x000724D4 File Offset: 0x000706D4
	// (set) Token: 0x060011BA RID: 4538 RVA: 0x000724DC File Offset: 0x000706DC
	public SceneManager SceneManager
	{
		get
		{
			return this.mSceneManager;
		}
		private set
		{
			this.mSceneManager = value;
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x060011BB RID: 4539 RVA: 0x000724E8 File Offset: 0x000706E8
	// (set) Token: 0x060011BC RID: 4540 RVA: 0x000724F0 File Offset: 0x000706F0
	public PlayerData PlayerData
	{
		get
		{
			return this.mPlayerData;
		}
		set
		{
			this.mPlayerData = value;
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x060011BD RID: 4541 RVA: 0x000724FC File Offset: 0x000706FC
	// (set) Token: 0x060011BE RID: 4542 RVA: 0x00072504 File Offset: 0x00070704
	public PlayerCommonData PlayerCommonData
	{
		get
		{
			return this.mPlayerCommonData;
		}
		set
		{
			this.mPlayerCommonData = value;
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x060011BF RID: 4543 RVA: 0x00072510 File Offset: 0x00070710
	// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00072518 File Offset: 0x00070718
	public MissionManager MissionManager
	{
		get
		{
			return this.mMissionManager;
		}
		set
		{
			this.mMissionManager = value;
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00072524 File Offset: 0x00070724
	// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0007252C File Offset: 0x0007072C
	public int RunningMapId
	{
		get
		{
			return this.mRunningMapId;
		}
		set
		{
			this.mRunningMapId = value;
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00072538 File Offset: 0x00070738
	public string RunningMapIdStr
	{
		get
		{
			return this.mRunningMapId.ToString();
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00072548 File Offset: 0x00070748
	// (set) Token: 0x060011C5 RID: 4549 RVA: 0x00072550 File Offset: 0x00070750
	public static bool OnLineState
	{
		get
		{
			return GameManager.mOnlineState;
		}
		set
		{
			GameManager.mOnlineState = value;
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00072558 File Offset: 0x00070758
	// (set) Token: 0x060011C7 RID: 4551 RVA: 0x00072560 File Offset: 0x00070760
	public bool FirstEnterGame
	{
		get
		{
			return this.mFirstEnterGame;
		}
		set
		{
			this.mFirstEnterGame = value;
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0007256C File Offset: 0x0007076C
	// (set) Token: 0x060011C9 RID: 4553 RVA: 0x00072574 File Offset: 0x00070774
	public bool IsShowMainMissionTip
	{
		get
		{
			return this.mIsShowMainMissionTip;
		}
		set
		{
			this.mIsShowMainMissionTip = value;
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x060011CA RID: 4554 RVA: 0x00072580 File Offset: 0x00070780
	// (set) Token: 0x060011CB RID: 4555 RVA: 0x00072588 File Offset: 0x00070788
	public bool LocalFirstEnter
	{
		get
		{
			return this.mLocalFirstEnter;
		}
		set
		{
			this.mLocalFirstEnter = value;
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x060011CC RID: 4556 RVA: 0x00072594 File Offset: 0x00070794
	// (set) Token: 0x060011CD RID: 4557 RVA: 0x0007259C File Offset: 0x0007079C
	public static bool IsSceneReady
	{
		get
		{
			return GameManager.mIsSceneReady;
		}
		set
		{
			GameManager.mIsSceneReady = value;
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x060011CE RID: 4558 RVA: 0x000725A4 File Offset: 0x000707A4
	// (set) Token: 0x060011CF RID: 4559 RVA: 0x000725AC File Offset: 0x000707AC
	public AutoSearchPathManager AutoSearchPath
	{
		get
		{
			return this.mAutoSearchPath;
		}
		set
		{
			this.mAutoSearchPath = value;
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x060011D0 RID: 4560 RVA: 0x000725B8 File Offset: 0x000707B8
	public SendServerCheckManager SendServerCheckManager
	{
		get
		{
			return this.mSendServerCheckManager;
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x060011D1 RID: 4561 RVA: 0x000725C0 File Offset: 0x000707C0
	// (set) Token: 0x060011D2 RID: 4562 RVA: 0x000725C8 File Offset: 0x000707C8
	public string ServerAreaName
	{
		get
		{
			return this.serverAreaName;
		}
		set
		{
			this.serverAreaName = value;
		}
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000725D4 File Offset: 0x000707D4
	private void Init()
	{
		DataManager.LoadBaseLocalization();
		this.mPlayerData = new PlayerData();
		this.mPlayerCommonData = new PlayerCommonData();
		this.mMissionManager = new MissionManager();
		this.mPlayerData.ResetPlayerData();
		this.mPlayerCommonData.ClearData();
		this.IsAutoDownload = LocalDataSaveManager.GetAutoDownloadFlag();
		this.IsNeedCountDownload = LocalDataSaveManager.IsNeedCountDownload();
		this.IsNeedCountDownloadTime = this.IsNeedCountDownload;
		this.IsNeedCountDownloadFinish = LocalDataSaveManager.IsNeedCountDownloadFinish();
		if (!GameSettingData.IsLowPhone)
		{
			AnimationManager.initStreamAnimationData(this);
		}
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0007265C File Offset: 0x0007085C
	public void ReImportData()
	{
		DataManager.ReImportData(this);
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x00072664 File Offset: 0x00070864
	public void ReImportAnima()
	{
		AnimationManager.ReImportDownloadAnimationData(this);
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0007266C File Offset: 0x0007086C
	public void EnterLoginScene()
	{
		this.mSceneManager = null;
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x00072678 File Offset: 0x00070878
	public void LoadScenneManager()
	{
		MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(this.RunningMapIdStr);
		if (mapInfoDataByID != null)
		{
			MAPTYPE mapType = mapInfoDataByID.MapType;
			switch (mapType)
			{
			case MAPTYPE.SINGLE_KILL_MONSTER_COPY:
				this.mSceneManager = new KillBossLocalSceneManager();
				break;
			case MAPTYPE.MUTIPLE_KILL_MONSTER_COPY:
				this.mSceneManager = new MultiKillBossSceneManager();
				break;
			case MAPTYPE.RANK_PVP:
				this.mSceneManager = new RankPVPLocalSceneManager();
				break;
			case MAPTYPE.REAL_PVP:
				this.mSceneManager = new RealPVPSceneManager();
				break;
			case MAPTYPE.TUTORIAL_SCENE:
				break;
			case MAPTYPE.CAR_CHASE_COPY:
				this.mSceneManager = new CarChaseSceneManager();
				break;
			case MAPTYPE.SNEAKING_COPY:
				this.mSceneManager = new SneakingSceneManager();
				break;
			case MAPTYPE.CLAMBING_TOWER:
				this.mSceneManager = new TowerSceneManager();
				break;
			case MAPTYPE.TUTORIAL_CAR:
				this.mSceneManager = new NewTutorialSceneManager();
				break;
			case MAPTYPE.CASH_DAILY_COPY:
				this.mSceneManager = new CashSneakingSceneManager();
				break;
			case MAPTYPE.EXP_DAILY_COPY:
			case MAPTYPE.SINGLE_EXP_DAILY_COPY:
				this.mSceneManager = new EXPSceneManager();
				break;
			case MAPTYPE.SINGLE_RUN_POINT_COPY:
				this.mSceneManager = new SingleRunPointSceneManager();
				break;
			case MAPTYPE.BAR_FIGHT_COPY:
				this.mSceneManager = new BarFightSceneManager();
				break;
			case MAPTYPE.WILD_BOSS_COPY:
				this.mSceneManager = new WildBossSceneManager();
				break;
			case MAPTYPE.EQUIP_COPY:
				this.mSceneManager = new EquipCopySceneManager();
				break;
			case MAPTYPE.GUILD_BOSS_COPY:
				this.mSceneManager = new GuildBossSceneManager();
				break;
			case MAPTYPE.SURVIVE_BATTLE1:
				this.mSceneManager = new SurvivalBattle1SceneManager();
				break;
			case MAPTYPE.SURVIVE_BATTLE2:
				this.mSceneManager = new SurvivalBattle2SceneManager();
				break;
			case MAPTYPE.SCUFFLE_AREA_1:
			case MAPTYPE.SCUFFLE_AREA_2:
				this.mSceneManager = new ScuffleAreaSceneManager();
				break;
			case MAPTYPE.MULTI_TOWER_COPY:
				this.mSceneManager = new MultiTowerCopySceneManager();
				break;
			case MAPTYPE.GUILD_BATTLE:
				this.mSceneManager = new GuildBattleSceneManager();
				break;
			case MAPTYPE.DOMIN_MAP:
				this.mSceneManager = new DominSceneManager();
				break;
			default:
				if (mapType != MAPTYPE.ANIMA_EDITOR)
				{
					this.mSceneManager = new SceneManager();
				}
				else
				{
					this.mSceneManager = new AnimationEditorManager();
				}
				break;
			case MAPTYPE.SHOP_COPY:
				this.mSceneManager = new ShopCopySceneManager();
				break;
			case MAPTYPE.LOW_PHONE:
				this.mSceneManager = new LowPhoneSceneManager();
				break;
			}
			this.mSceneManager.Init(this.RunningMapIdStr);
		}
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x00072924 File Offset: 0x00070B24
	public void LoadNextLoadingTexture()
	{
		if (GameSettingData.GetPhoneClass() != 0)
		{
			List<LoadingUIData> loadingUIDataList = DataManager.GetLoadingUIDataList();
			for (int i = loadingUIDataList.Count - 1; i >= 0; i--)
			{
				if (loadingUIDataList[i].ShowFlag == 0 || this.mPlayerData.Level < loadingUIDataList[i].MinLevel || this.mPlayerData.Level >= loadingUIDataList[i].MaxLevel || (!string.IsNullOrEmpty(loadingUIDataList[i].StartTime) && !TimeTools.IsTimeRange(loadingUIDataList[i].StartTimeList, loadingUIDataList[i].EndTimeList)))
				{
					loadingUIDataList.RemoveAt(i);
				}
			}
			if (loadingUIDataList.Count > 0)
			{
				string loadindex = LocalDataSaveManager.GetLoadindex();
				int num = 0;
				for (int j = 0; j < loadingUIDataList.Count; j++)
				{
					if (loadingUIDataList[j].ID.Equals(loadindex))
					{
						num = j;
						break;
					}
				}
				BundleManager.StartLoadingUItexture(this, loadingUIDataList[num].Name);
				num = (num + 1) % loadingUIDataList.Count;
				LocalDataSaveManager.SetLoadindex(loadingUIDataList[num].ID);
			}
		}
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x00072A60 File Offset: 0x00070C60
	private void OnDisable()
	{
		this.LowPhoneSaveData();
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x00072A68 File Offset: 0x00070C68
	public void LowPhoneSaveData()
	{
		if (GameSettingData.IsLowPhone && this.mSceneManager != null)
		{
			LowPhoneSceneManager lowPhoneSceneManager = this.mSceneManager as LowPhoneSceneManager;
			if (lowPhoneSceneManager != null)
			{
				lowPhoneSceneManager.SaveData();
			}
		}
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x00072AA4 File Offset: 0x00070CA4
	protected override void Awake()
	{
		base.Awake();
		if (!SingletonDontDestoryUnity<GameManager>.Exists || !base.IsInit)
		{
			return;
		}
		this.Init();
		this.InitAndroid();
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x00072ADC File Offset: 0x00070CDC
	private void Update()
	{
		if (this.mSceneManager != null)
		{
			this.mSceneManager.Update();
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x00072AF4 File Offset: 0x00070CF4
	private void Start()
	{
		base.InvokeRepeating("CheckSendServerRefresh", 10f, 60f);
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x00072B0C File Offset: 0x00070D0C
	public bool CanSendToServer(int eventId, float Interval = 0.5f)
	{
		return this.mSendServerCheckManager.CanSendToServer(eventId, Interval);
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x00072B1C File Offset: 0x00070D1C
	public void RegisterCheckEvent(int eventId)
	{
		this.mSendServerCheckManager.RegisterCheckEvent(eventId, 0.5f);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x00072B30 File Offset: 0x00070D30
	private void CheckSendServerRefresh()
	{
		if (this.sendServerRefreshTime > 0L && Time.realtimeSinceStartup - this.senderServerRefreshStartCheckTime > (float)this.sendServerRefreshTime)
		{
			this.SendServerRefreshInfo();
		}
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x00072B60 File Offset: 0x00070D60
	public void SetNextServerRefreshTime()
	{
		this.sendServerRefreshTime = this.PlayerCommonData.GetResetDiffTime() + (long)Random.Range(0, 240);
		this.senderServerRefreshStartCheckTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x00072B98 File Offset: 0x00070D98
	public void SendServerRefreshInfo()
	{
		if (this.SceneManager != null && !Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
		{
			NetLogic.GetInstance().Send<Protocol.ask_copyscenes_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_activity_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_rank_pvp_data>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_tower_copy_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_30_day_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_buy>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_sign_week_info>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_active>(null, null);
			NetLogic.GetInstance().Send<Protocol.request_daily_mission>(null, null);
			this.ClearServerRefresh();
		}
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00072C44 File Offset: 0x00070E44
	public void ClearServerRefresh()
	{
		this.sendServerRefreshTime = -1L;
		this.senderServerRefreshStartCheckTime = -1f;
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x00072C5C File Offset: 0x00070E5C
	private void InitAndroid()
	{
		Debug.Log("**************Platform Unity Init:" + Debug.isDebugBuild);
		if (Debug.isDebugBuild)
		{
			GameManager.currentActivity = null;
			return;
		}
		if (GameManager.currentActivity == null)
		{
			GameManager.jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			GameManager.currentActivity = GameManager.jc.GetStatic<AndroidJavaObject>("currentActivity");
		}
		Debug.Log("**************Platform Unity InitX:" + GameManager.currentActivity);
		GameManager.cancleAllNotification();
		this.serverAreaName = this.GetServerArea();
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x00072CE8 File Offset: 0x00070EE8
	public bool IsFacebookEnable()
	{
		return this.GetSDKVersion() >= 9;
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x00072CFC File Offset: 0x00070EFC
	public int GetSDKVersion()
	{
		if (GameManager.currentActivity != null)
		{
			return GameManager.currentActivity.Call<int>("internalGetSDKVersion", new object[0]);
		}
		return -1;
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x00072D20 File Offset: 0x00070F20
	public void SignInFacebook()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalFacebookSignIn", new object[0]);
		}
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x00072D44 File Offset: 0x00070F44
	public void SignOutFacebook()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalFacebookSignOut", new object[0]);
		}
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x00072D68 File Offset: 0x00070F68
	public void SignInGoogle()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("googleSignIn", new object[0]);
		}
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x00072D8C File Offset: 0x00070F8C
	public void SignOutGoogle()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("googleSignOut", new object[0]);
		}
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x00072DB0 File Offset: 0x00070FB0
	public void SignInFacebookSuccess(string data)
	{
		if (string.IsNullOrEmpty(data))
		{
			return;
		}
		if (!data.Contains("@"))
		{
			return;
		}
		string[] array = data.Split(new char[]
		{
			'@'
		});
		if (array.Length != 2)
		{
			return;
		}
		string id = array[0];
		string token = array[1];
		if (SingletonUnity<AccountVersionCheckRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AccountVersionCheckRootLogic>.Instance.gameObject))
		{
			SingletonUnity<AccountVersionCheckRootLogic>.Instance.SignInCheck(id, token, 0);
		}
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x00072E2C File Offset: 0x0007102C
	public void SignInGoogleSuccess(string data)
	{
		if (string.IsNullOrEmpty(data))
		{
			return;
		}
		if (!data.Contains("@"))
		{
			return;
		}
		string[] array = data.Split(new char[]
		{
			'@'
		});
		if (array.Length != 2)
		{
			return;
		}
		string id = array[0];
		string token = array[1];
		if (SingletonUnity<AccountVersionCheckRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AccountVersionCheckRootLogic>.Instance.gameObject))
		{
			SingletonUnity<AccountVersionCheckRootLogic>.Instance.SignInCheck(id, token, 1);
		}
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x00072EA8 File Offset: 0x000710A8
	public void SignOutGoogleSuccess(string data)
	{
		if (SingletonUnity<AccountVersionCheckRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AccountVersionCheckRootLogic>.Instance.gameObject))
		{
			SingletonUnity<AccountVersionCheckRootLogic>.Instance.SignInCheck(string.Empty, string.Empty, 1);
		}
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x00072EE8 File Offset: 0x000710E8
	public void SignOutFacebookSuccess(string data)
	{
		if (SingletonUnity<AccountVersionCheckRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AccountVersionCheckRootLogic>.Instance.gameObject))
		{
			SingletonUnity<AccountVersionCheckRootLogic>.Instance.SignInCheck(string.Empty, string.Empty, 0);
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x00072F28 File Offset: 0x00071128
	public void CreateBilling()
	{
		Debug.Log("CreateBilling");
		if (GameManager.IsInitBiling)
		{
			return;
		}
		Debug.Log("Platform creteBilling");
		string text = string.Empty;
		Dictionary<string, PurchaseData> purchaseData = DataManager.GetPurchaseData();
		foreach (KeyValuePair<string, PurchaseData> keyValuePair in purchaseData)
		{
			text = text + keyValuePair.Value.ProductId + "#";
		}
		if (text.Length > 0)
		{
			text = text.Remove(text.Length - 1);
			string text2 = string.Format("{0}", PlayerData.MainPlayerServerId);
			if (GameManager.currentActivity != null)
			{
				GameManager.currentActivity.Call("internalCreateBilling", new object[]
				{
					text,
					text2
				});
			}
			GameManager.IsInitBiling = true;
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x00073024 File Offset: 0x00071224
	public void PurchaseInfoToServerToVerify(string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return;
		}
		string[] array = str.Split(new char[]
		{
			'#'
		});
		if (array.Length < 4)
		{
			return;
		}
		string productId = array[0];
		string packageName = array[1];
		string token = array[2];
		string payload = array[3];
		check_purchase.request request = new check_purchase.request();
		request.productId = productId;
		request.packageName = packageName;
		request.token = token;
		request.payload = payload;
		NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x000730A0 File Offset: 0x000712A0
	public void HideFullScreenSmallFromAndroid()
	{
		if (SingletonUnity<ExitGameRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ExitGameRoot>.Instance.gameObject))
		{
			SingletonUnity<ExitGameRoot>.Instance.CloseAd();
		}
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x000730D8 File Offset: 0x000712D8
	public string GetServerArea()
	{
		if (GameManager.currentActivity != null)
		{
			return GameManager.currentActivity.Call<string>("GetCountryServerArea", new object[0]);
		}
		return null;
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000730FC File Offset: 0x000712FC
	public void Billing(string sku)
	{
		string text = string.Format("{0}#{1}", sku, PlayerData.MainPlayerServerId);
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalBilling", new object[]
			{
				text
			});
		}
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x00073144 File Offset: 0x00071344
	public void UpdateComsumed(ret_commercail_reward.request request)
	{
		if (request.state == 0L || request.state == 2L)
		{
			switch ((int)request.type)
			{
			case 1:
			case 3:
			case 4:
			case 6:
			case 7:
				if (request.HasProductId && this.mPlayerCommonData.CheckDollorBuy(request.productId))
				{
					this.ComsumedPurchase(request.productId);
					this.mPlayerCommonData.UpdateAddFree(request.productId);
					AccountVersionCheckRootLogic.CheckFaceBookBindTips(0);
				}
				break;
			}
		}
		else if (request.state == 1L)
		{
			NoticeLogic.AddNotifyData("#{101233}", true, false);
		}
		else if (request.state == 3L)
		{
		}
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x00073218 File Offset: 0x00071418
	public void ComsumedPurchase(string sku)
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalComsumedPurchase", new object[]
			{
				sku
			});
		}
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x00073240 File Offset: 0x00071440
	public void QueryInventory()
	{
		string text = string.Format("{0}", PlayerData.MainPlayerServerId);
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalQueryInventory", new object[]
			{
				text
			});
		}
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x00073288 File Offset: 0x00071488
	public void ShowFeatureView()
	{
		Debug.Log("Platform ShowFeatureView >_<");
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalShowFeatureView", new object[0]);
		}
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x000732B4 File Offset: 0x000714B4
	public void HideFeatureView()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalHideFeatureView", new object[0]);
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000732D8 File Offset: 0x000714D8
	public bool IsFullScreenSmallReady()
	{
		Debug.Log("Platform IsFullScreenSmallReady >_<");
		return GameManager.currentActivity != null && GameManager.currentActivity.Call<bool>("internalIsFulScreenSmallReady", new object[0]);
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x00073308 File Offset: 0x00071508
	public bool IsFullScreenSmallShowing()
	{
		Debug.Log("Platform IsFullScreenSmallShowing >_<");
		return GameManager.currentActivity != null && GameManager.currentActivity.Call<bool>("internalIsFullScreenSmallShowing", new object[0]);
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x00073338 File Offset: 0x00071538
	public void ShowFullScreenExitSmall()
	{
		Debug.Log("Platform ShowFullScreenExitSmall >_<");
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalShowFullScreenExitSmall", new object[0]);
		}
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x00073364 File Offset: 0x00071564
	public void ShowFullScreenSmall()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalShowFullScreenSmall", new object[0]);
		}
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x00073388 File Offset: 0x00071588
	public void HideFullScreenSmall()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalHideFullScreenSmall", new object[0]);
		}
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000733AC File Offset: 0x000715AC
	public void HideFakeLoading()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalHideFakeLoading", new object[0]);
		}
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000733D0 File Offset: 0x000715D0
	public void ShowMoreGames()
	{
		Debug.Log("Platform ShowMoreGames >_<");
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalMoreGames", new object[0]);
		}
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000733FC File Offset: 0x000715FC
	public int GetBatteryState()
	{
		if (GameManager.currentActivity != null)
		{
			return GameManager.currentActivity.Call<int>("internalGetBatteryState", new object[0]);
		}
		return 50;
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0007342C File Offset: 0x0007162C
	public void Rating()
	{
		Debug.Log("Platform Rating >_<");
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalRating", new object[0]);
		}
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x00073458 File Offset: 0x00071658
	public void FlurryLogEvent(string eventName)
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("flurryLogEvent", new object[]
			{
				eventName
			});
		}
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x00073480 File Offset: 0x00071680
	public void FlurryLogEventMap(string title, string name, string value)
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("flurryLogEventMap", new object[]
			{
				title,
				name,
				value
			});
		}
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x000734B0 File Offset: 0x000716B0
	public void FlurryLogEventMap(string title, string name, string value, string value2)
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("flurryLogEventMap_2", new object[]
			{
				title,
				name,
				value,
				value2
			});
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000734F0 File Offset: 0x000716F0
	public static bool IsEnglishLanguage()
	{
		return GameManager.currentActivity != null && GameManager.currentActivity.Call<bool>("IsEnglishLanguage", new object[0]);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x00073514 File Offset: 0x00071714
	public static void setNotification(int id, int day, long delay, string message)
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalSetNotification", new object[]
			{
				id,
				day,
				delay,
				StrDictionary.GetDictionaryString(GameDefine.GameName, new object[0]),
				message
			});
		}
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x00073574 File Offset: 0x00071774
	public static void cancleNotification(int id)
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalCancelNotification", new object[]
			{
				id
			});
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000735AC File Offset: 0x000717AC
	public static void cancleAllNotification()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.NotifyList = DataManager.GetNotifyDataList();
			for (int i = 0; i < (GameManager.NotifyList.Count + 1) * 6; i++)
			{
				GameManager.cancleNotification(i);
			}
		}
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x000735F4 File Offset: 0x000717F4
	public static int GetNotifyTime(int id)
	{
		string timeOffset = LocalDataSaveManager.GetTimeOffset();
		long num = long.Parse(timeOffset);
		if (id == 0)
		{
			return GameDefine.NotifyTime1;
		}
		if (id <= 0)
		{
			return -1;
		}
		if (num == -1L)
		{
			return -1;
		}
		TimeSpan localShowTime = TimeTools.GetLocalShowTime((long)GameManager.NotifyList[id - 1].NotifyTime, num);
		return localShowTime.Hours * 60 + localShowTime.Minutes;
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0007365C File Offset: 0x0007185C
	public static void SetNotify()
	{
		if (GameManager.currentActivity == null)
		{
			return;
		}
		GameManager.NotifyList = DataManager.GetNotifyDataList();
		int num = GameManager.NotifyList.Count + 1;
		int num2 = 6;
		int num3 = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
		string text = string.Empty;
		for (int i = 0; i < num * num2; i++)
		{
			long num4 = (long)GameManager.GetNotifyTime(i % num);
			text = string.Empty;
			if (i != 0 || LocalDataSaveManager.GetRewardFlag() != 0)
			{
				if (num4 != -1L)
				{
					if (i < num)
					{
						if ((long)num3 < num4)
						{
							text = GameManager.GetNotifyMessage(i % num);
							if (!string.IsNullOrEmpty(text))
							{
								GameManager.setNotification(i, i / num, num4, text);
							}
						}
					}
					else
					{
						text = GameManager.GetNotifyMessage(i % num);
						if (!string.IsNullOrEmpty(text))
						{
							GameManager.setNotification(i, i / num, num4, text);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x00073764 File Offset: 0x00071964
	public static string GetNotifyMessage(int time)
	{
		string timeOffset = LocalDataSaveManager.GetTimeOffset();
		long offsetTime = long.Parse(timeOffset);
		TimeSpan timeSpan = default(TimeSpan);
		if (time == 0)
		{
			int num = -1;
			if (SingletonDontDestoryUnity<GameManager>.Instance != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData != null)
			{
				num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.GetNextWeekRewardDay();
			}
			if (num == 2)
			{
				if (GameManager.IsSupportCurDataVersion())
				{
					return StrDictionary.GetDictionaryString("#{200406}", new object[0]);
				}
				return "Login now, you can get a powerful weapon for free!";
			}
			else
			{
				if (num != 3)
				{
					return StrDictionary.GetDictionaryString("#{200402}", new object[0]);
				}
				if (GameManager.IsSupportCurDataVersion())
				{
					return StrDictionary.GetDictionaryString("#{200407}", new object[0]);
				}
				return "Now login, you can get brand-new vehicles for free!";
			}
		}
		else
		{
			if (time > 0)
			{
				timeSpan = TimeTools.GetLocalShowTime((long)GameManager.NotifyList[time - 1].ShowTime, offsetTime);
				return StrDictionary.GetDictionaryString(GameManager.NotifyList[time - 1].InfoStr, new object[]
				{
					string.Format("{0:d2}:{1:d2}", timeSpan.Hours, timeSpan.Minutes)
				});
			}
			return null;
		}
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x00073888 File Offset: 0x00071A88
	private void OnApplicationQuit()
	{
		GameManager.SetNotify();
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x00073890 File Offset: 0x00071A90
	private void OnApplicationFocus(bool isfocus)
	{
		if (isfocus)
		{
			if (GameManager.islostfocus)
			{
				GameManager.cancleAllNotification();
			}
		}
		else
		{
			GameManager.SetNotify();
			GameManager.islostfocus = true;
		}
		this.LowPhoneSaveData();
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x000738C0 File Offset: 0x00071AC0
	public void VideoAdsReadyCallBack()
	{
		if (this.OnUnityAdsStateChange != null)
		{
			this.OnUnityAdsStateChange(true);
		}
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000738DC File Offset: 0x00071ADC
	public void VideoAdsSkipCallBack(string adsVideoType)
	{
		if (this.OnUnityAdsFinished != null)
		{
			this.OnUnityAdsFinished(false);
			this.FlurryLogEventMap("VideoAds", "Skip", adsVideoType);
		}
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x00073914 File Offset: 0x00071B14
	public void VideoAdsClosedCallBack(string adsVideoType)
	{
		if (this.OnUnityAdsFinished != null)
		{
			this.OnUnityAdsFinished(true);
			this.FlurryLogEventMap("VideoAds", "Finish", adsVideoType);
		}
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0007394C File Offset: 0x00071B4C
	public void InterstitialAdCallBack(string adstr)
	{
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001212 RID: 4626 RVA: 0x00073950 File Offset: 0x00071B50
	public bool IsUnityAdsReady
	{
		get
		{
			return GameManager.currentActivity != null && GameManager.currentActivity.Call<bool>("internalIsVideoAdsReady", new object[0]);
		}
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x00073974 File Offset: 0x00071B74
	public void ShowUnityAds()
	{
		if (GameManager.currentActivity != null)
		{
			GameManager.currentActivity.Call("internalShowVideoAds", new object[0]);
		}
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x00073998 File Offset: 0x00071B98
	public static bool IsSupportCurDataVersion()
	{
		return PlayerData.LocalDataVersion > GameManager.supportMinVersion;
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000739AC File Offset: 0x00071BAC
	public static bool IsSupportCurDataVersion47()
	{
		return PlayerData.LocalDataVersion > GameManager.supportMinVersion47;
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000739C0 File Offset: 0x00071BC0
	public static bool IsSupportCurDataVersion56()
	{
		return PlayerData.LocalDataVersion >= GameManager.supportMinVersion56;
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000739D4 File Offset: 0x00071BD4
	public static bool IsSupportCurDataVersion77()
	{
		return PlayerData.LocalDataVersion >= GameManager.supportMinVersion77;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000739E8 File Offset: 0x00071BE8
	public static bool IsSupportCurDataVersion137()
	{
		return PlayerData.LocalDataVersion >= GameManager.supportMinVersion137;
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000739FC File Offset: 0x00071BFC
	public static bool IsSupportCurDataVersion145()
	{
		return GameSettingData.IsLocalTestServer || PlayerData.LocalDataVersion >= GameManager.supportMinVersion145;
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x00073A1C File Offset: 0x00071C1C
	public static bool IsSupportCurDataVersion157()
	{
		return GameSettingData.IsLocalTestServer || PlayerData.LocalDataVersion >= GameManager.supportMinVersion157;
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x00073A3C File Offset: 0x00071C3C
	public static bool IsSupportCurDataVersion167()
	{
		return GameSettingData.IsLocalTestServer || PlayerData.LocalDataVersion >= GameManager.supportMinVersion167;
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x00073A5C File Offset: 0x00071C5C
	public static bool IsSupportCurDataVersion177()
	{
		return GameSettingData.IsLocalTestServer || PlayerData.LocalDataVersion >= GameManager.supportMinVersion177;
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x00073A7C File Offset: 0x00071C7C
	public static bool IsSupportCurDataVersion184()
	{
		return GameSettingData.IsLocalTestServer || PlayerData.LocalDataVersion >= GameManager.supportMinVersion184;
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x00073A9C File Offset: 0x00071C9C
	public static bool IsSupportCurDataVersion200()
	{
		return GameSettingData.IsLocalTestServer || PlayerData.LocalDataVersion >= GameManager.supportMinVersion200;
	}

	// Token: 0x04001783 RID: 6019
	private SceneManager mSceneManager;

	// Token: 0x04001784 RID: 6020
	private PlayerData mPlayerData;

	// Token: 0x04001785 RID: 6021
	private PlayerCommonData mPlayerCommonData;

	// Token: 0x04001786 RID: 6022
	private MissionManager mMissionManager;

	// Token: 0x04001787 RID: 6023
	private int mRunningMapId;

	// Token: 0x04001788 RID: 6024
	private static bool mOnlineState = false;

	// Token: 0x04001789 RID: 6025
	private bool mFirstEnterGame;

	// Token: 0x0400178A RID: 6026
	private bool mIsShowMainMissionTip;

	// Token: 0x0400178B RID: 6027
	private bool mLocalFirstEnter = true;

	// Token: 0x0400178C RID: 6028
	private static bool mIsSceneReady = false;

	// Token: 0x0400178D RID: 6029
	private AutoSearchPathManager mAutoSearchPath = new AutoSearchPathManager();

	// Token: 0x0400178E RID: 6030
	private SendServerCheckManager mSendServerCheckManager = new SendServerCheckManager();

	// Token: 0x0400178F RID: 6031
	private string serverAreaName = string.Empty;

	// Token: 0x04001790 RID: 6032
	public bool IsDayFlag;

	// Token: 0x04001791 RID: 6033
	public bool IsAutoDownload;

	// Token: 0x04001792 RID: 6034
	public bool IsNeedCountDownload;

	// Token: 0x04001793 RID: 6035
	public bool IsNeedCountDownloadTime;

	// Token: 0x04001794 RID: 6036
	public bool IsNeedCountDownloadFinish;

	// Token: 0x04001795 RID: 6037
	private long sendServerRefreshTime = -1L;

	// Token: 0x04001796 RID: 6038
	private float senderServerRefreshStartCheckTime = -1f;

	// Token: 0x04001797 RID: 6039
	public static AndroidJavaClass jc;

	// Token: 0x04001798 RID: 6040
	public static AndroidJavaObject currentActivity;

	// Token: 0x04001799 RID: 6041
	public static bool IsInitBiling = false;

	// Token: 0x0400179A RID: 6042
	private static List<NotifyData> NotifyList = new List<NotifyData>();

	// Token: 0x0400179B RID: 6043
	private static bool islostfocus = false;

	// Token: 0x0400179C RID: 6044
	public GameManager.Void_Bool_Delegate OnUnityAdsFinished;

	// Token: 0x0400179D RID: 6045
	public GameManager.Void_Bool_Delegate OnUnityAdsStateChange;

	// Token: 0x0400179E RID: 6046
	private static int supportMinVersion = 42;

	// Token: 0x0400179F RID: 6047
	private static int supportMinVersion47 = 47;

	// Token: 0x040017A0 RID: 6048
	private static int supportMinVersion56 = 56;

	// Token: 0x040017A1 RID: 6049
	private static int supportMinVersion77 = 77;

	// Token: 0x040017A2 RID: 6050
	private static int supportMinVersion137 = 137;

	// Token: 0x040017A3 RID: 6051
	private static int supportMinVersion145 = 145;

	// Token: 0x040017A4 RID: 6052
	private static int supportMinVersion157 = 157;

	// Token: 0x040017A5 RID: 6053
	private static int supportMinVersion167 = 167;

	// Token: 0x040017A6 RID: 6054
	private static int supportMinVersion177 = 177;

	// Token: 0x040017A7 RID: 6055
	private static int supportMinVersion184 = 184;

	// Token: 0x040017A8 RID: 6056
	private static int supportMinVersion200 = 200;

	// Token: 0x02000AD6 RID: 2774
	// (Invoke) Token: 0x06004FE1 RID: 20449
	public delegate void Void_Bool_Delegate(bool value);
}
