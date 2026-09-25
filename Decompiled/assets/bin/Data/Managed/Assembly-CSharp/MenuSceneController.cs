using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SprotoType;
using UnityEngine;

// Token: 0x020009F3 RID: 2547
public class MenuSceneController : SingletonUnity<MenuSceneController>
{
	// Token: 0x060048D1 RID: 18641 RVA: 0x00177038 File Offset: 0x00175238
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x060048D2 RID: 18642 RVA: 0x00177040 File Offset: 0x00175240
	private void Start()
	{
		this.CheckLoadUIState();
	}

	// Token: 0x060048D3 RID: 18643 RVA: 0x00177048 File Offset: 0x00175248
	private IEnumerator NextFrame()
	{
		while (!DataManager.initDoneFlag)
		{
			yield return null;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.HideFakeLoading();
		LoadingWindow.LoadScene(3001);
		yield break;
	}

	// Token: 0x060048D4 RID: 18644 RVA: 0x0017705C File Offset: 0x0017525C
	private void CheckLoadUIState()
	{
		if (GameSettingData.IsLowPhone)
		{
			DataManager.InitData(SingletonDontDestoryUnity<GameManager>.Instance);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LoadingUIRoot, null, null);
			if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(this.NextFrame());
			}
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.EnterLoginScene();
			PlayerData.CurLoginServerData = new ServerInfoData();
			if (GameSettingData.IsLocalTestServer)
			{
				this.SetTestServer();
			}
			else
			{
				PlayerData.CurLoginServerData.LoginIP = "s16.serv00.com";
				PlayerData.CurLoginServerData.LoginPort = 9777;
				PlayerData.CurLoginServerData.IsUseDns = true;
			}
			PlayerData.LocalDataVersion = MenuSceneController.GetDataVersion();
			this.InitUI();
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, new NetLogic.ConnectDelegate(this.ConnectSuccess), new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
		this.showAD();
		this.phoneStateFlurry(GameSettingData.IsLowPhone);
	}

	// Token: 0x060048D5 RID: 18645 RVA: 0x0017715C File Offset: 0x0017535C
	private void showAD()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.LocalFirstEnter)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.LocalFirstEnter = false;
			int adfreeFlag = LocalDataSaveManager.GetADFreeFlag();
			if (adfreeFlag == 0)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.ShowFullScreenSmall();
			}
			else if (adfreeFlag == -1)
			{
				LocalDataSaveManager.SetADFreeFlag(0);
			}
		}
	}

	// Token: 0x060048D6 RID: 18646 RVA: 0x001771AC File Offset: 0x001753AC
	private void phoneStateFlurry(bool islowphone)
	{
		if (LocalDataSaveManager.GetPhoneStateFlag() == 0)
		{
			if (islowphone)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("PhoneState", "state", "local");
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("PhoneState", "state", "connect");
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("PhoneState", "timezone", string.Format("time{0}", TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours));
			}
			LocalDataSaveManager.SetPhoneStateFlag();
		}
	}

	// Token: 0x060048D7 RID: 18647 RVA: 0x00177240 File Offset: 0x00175440
	private void ConnectLost()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
		{
			SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
		}, null);
	}

	// Token: 0x060048D8 RID: 18648 RVA: 0x00177260 File Offset: 0x00175460
	private void ConnectSuccess(bool isSuccess)
	{
		WaitResponseUIRootLogic.CloseBox();
		if (isSuccess)
		{
			if (SingletonUnity<AccountVersionCheckRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AccountVersionCheckRootLogic>.Instance.gameObject))
			{
				SingletonUnity<AccountVersionCheckRootLogic>.Instance.StartCheckVerifyAccount();
			}
			else
			{
				Debug.LogError("SingletonUnity<AccountVersionCheckRootLogic>.Exists == false!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
			{
				SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
			}, null);
			NoticeLogic.AddNotifyData("#{200065}", true, false);
		}
	}

	// Token: 0x060048D9 RID: 18649 RVA: 0x001772DC File Offset: 0x001754DC
	private void OnCancleClick()
	{
		Application.Quit();
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x001772E4 File Offset: 0x001754E4
	private void OnYesClick()
	{
		WaitResponseUIRootLogic.OpenWaitBox(4, -1f, 0f, null);
		SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, new NetLogic.ConnectDelegate(this.ConnectSuccess), new NetLogic.ConnectLostDelegate(this.ConnectLost));
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x00177338 File Offset: 0x00175538
	private void InitUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NotifyRootUI, null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, null, null);
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.WaitFrame());
		}
	}

	// Token: 0x060048DC RID: 18652 RVA: 0x00177384 File Offset: 0x00175584
	private IEnumerator WaitFrame()
	{
		yield return null;
		SingletonDontDestoryUnity<GameManager>.Instance.HideFakeLoading();
		yield break;
	}

	// Token: 0x060048DD RID: 18653 RVA: 0x00177398 File Offset: 0x00175598
	public void ShowChooseServer()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LoginRootUI, null, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CreateRoleRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChooseRoleRootUI);
	}

	// Token: 0x060048DE RID: 18654 RVA: 0x001773E0 File Offset: 0x001755E0
	public void ShowCreateRole(bool isNewAccount)
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChooseRoleRootUI);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CreateRoleRootUI, delegate
		{
			SingletonUnity<CreateRoleRootLogic>.Instance.Reset(isNewAccount);
		}, null);
	}

	// Token: 0x060048DF RID: 18655 RVA: 0x00177434 File Offset: 0x00175634
	public void ShowChooseRole(character_list.response chaList)
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginRootUI);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChooseRoleRootUI, delegate
		{
			SingletonUnity<ChooseRoleRootLogic>.Instance.Reset(chaList);
		}, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CreateRoleRootUI);
	}

	// Token: 0x060048E0 RID: 18656 RVA: 0x00177488 File Offset: 0x00175688
	private void OnClickExitGame()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		MessageBoxLogic.CloseBox();
		Application.Quit();
	}

	// Token: 0x060048E1 RID: 18657 RVA: 0x001774A0 File Offset: 0x001756A0
	private void OnClickNo()
	{
		MessageBoxLogic.CloseBox();
	}

	// Token: 0x060048E2 RID: 18658 RVA: 0x001774A8 File Offset: 0x001756A8
	private void ExitGame()
	{
		if (SingletonUnity<ExitGameRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ExitGameRoot>.Instance.gameObject))
		{
			SingletonUnity<ExitGameRoot>.Instance.OnClickNoBtn();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExitGameRoot, delegate
			{
				SingletonUnity<ExitGameRoot>.Instance.Reset();
			}, null);
		}
	}

	// Token: 0x060048E3 RID: 18659 RVA: 0x00177510 File Offset: 0x00175710
	private void Update()
	{
		BundleManager.LoadModelListUpdate(this);
		if (Input.GetKeyUp((KeyCode)27))
		{
			if (!SingletonUnity<UIManager>.Instance.BackUIFun())
			{
				this.ExitGame();
			}
		}
	}

	// Token: 0x060048E4 RID: 18660 RVA: 0x0017754C File Offset: 0x0017574C
	public GameObject CreatePlayerModelRoot(PROFESSION_TYPE profession)
	{
		return ResourcesManager.LoadAndInstantiate("TestModel/Model/" + profession.ToString() + "/ModelRoot") as GameObject;
	}

	// Token: 0x060048E5 RID: 18661 RVA: 0x00177580 File Offset: 0x00175780
	public void CreatePlayerModelVisual(GameObject modelRoot, string weaponId, string headId, string bodyId, string legId, MenuSceneController.OnLoadPartFinisheDel func)
	{
		this.LoadPlayerVisual(modelRoot, weaponId, headId, bodyId, legId, func);
	}

	// Token: 0x060048E6 RID: 18662 RVA: 0x00177594 File Offset: 0x00175794
	private void LoadPlayerVisual(GameObject playerRootObj, string partWeaponId, string partHeadId, string partBodyId, string partLegId, MenuSceneController.OnLoadPartFinisheDel func)
	{
		ModelData modeDataByID = DataManager.GetModeDataByID(partLegId);
		if (modeDataByID != null)
		{
			BundleManager.LoadModelInList(modeDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), func, playerRootObj, modeDataByID.ModelPath);
		}
		modeDataByID = DataManager.GetModeDataByID(partWeaponId);
		if (modeDataByID != null)
		{
			BundleManager.LoadModelInList(modeDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), func, playerRootObj, modeDataByID.ModelPath);
		}
		modeDataByID = DataManager.GetModeDataByID(partHeadId);
		if (modeDataByID != null)
		{
			BundleManager.LoadModelInList(modeDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), func, playerRootObj, modeDataByID.ModelPath);
		}
		modeDataByID = DataManager.GetModeDataByID(partBodyId);
		if (modeDataByID != null)
		{
			BundleManager.LoadModelInList(modeDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), func, playerRootObj, modeDataByID.ModelPath);
		}
	}

	// Token: 0x060048E7 RID: 18663 RVA: 0x00177670 File Offset: 0x00175870
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x060048E8 RID: 18664 RVA: 0x00177678 File Offset: 0x00175878
	private void OnLoadPlayerPartFinished(object objBundle, object param1 = null, object param2 = null)
	{
		MenuSceneController.OnLoadPartFinisheDel onLoadPartFinisheDel = param1 as MenuSceneController.OnLoadPartFinisheDel;
		GameObject gameObject = param2 as GameObject;
		GameObject gameObject2 = objBundle as GameObject;
		BundleManager.ResetShader(gameObject2.transform);
		gameObject2.layer = LayerMask.NameToLayer("ShadowCaster");
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		UnityVersionUtil.SetActiveRecursive(gameObject2.gameObject, false);
		BundleManager.RebuildBones(gameObject, gameObject2);
		if (onLoadPartFinisheDel != null)
		{
			onLoadPartFinisheDel(gameObject);
		}
	}

	// Token: 0x060048E9 RID: 18665 RVA: 0x001776F8 File Offset: 0x001758F8
	public static int GetDataVersion()
	{
		string text = string.Format("{0}/{1}", FileUpdateHelper.GetLocalVersionPath(), FileUpdateHelper.VersionFileName);
		string text2 = string.Format("{0}{1}/{2}", Application.streamingAssetsPath, FileUpdateHelper.ApkVersionFolderName, FileUpdateHelper.VersionFileName);
		string empty = string.Empty;
		if (File.Exists(text) && !MyFileUtil.GetStringFromFile(text, ref empty))
		{
			Log.ERROR_MSG("parse version fail");
		}
		string empty2 = string.Empty;
		if (File.Exists(text2) && !MyFileUtil.GetStringFromFile(text2, ref empty2))
		{
			Log.ERROR_MSG("parse version fail");
		}
		if (string.Compare(empty, empty2) > 0)
		{
			return int.Parse(empty);
		}
		if (string.IsNullOrEmpty(empty2))
		{
			return 0;
		}
		return int.Parse(empty2);
	}

	// Token: 0x060048EA RID: 18666 RVA: 0x001777AC File Offset: 0x001759AC
	public void SaveServerList(verfiy.response response)
	{
		this.GameServerList.Clear();
		this.UserServerList.Clear();
		this.user_Str = string.Empty;
		if (response.HasGame_server)
		{
			this.GameServerList = response.game_server;
		}
		if (!GameSettingData.IsLocalTestServer || (GameSettingData.IsLocalTestServer && GameSettingData.LocalTestServerID == 0))
		{
			if (response.HasUser_server)
			{
				this.SaveUseServer(response.user_server);
				if (this.UserServerList.Count > 0)
				{
					PlayerData.CurGameServerData = new game_server();
					PlayerData.CurGameServerData.serverId = this.UserServerList[0].serverId;
					PlayerData.CurGameServerData.serverIP = this.UserServerList[0].serverIP;
					PlayerData.CurGameServerData.serverPort = this.UserServerList[0].serverPort;
					PlayerData.CurGameServerData.serverName = this.UserServerList[0].serverName;
					PlayerData.CurGameServerData.serverState = this.UserServerList[0].serverState;
					if (this.UserServerList[0].HasNewServer)
					{
						PlayerData.CurGameServerData.newServer = this.UserServerList[0].newServer;
					}
				}
				else
				{
					PlayerData.CurGameServerData = null;
				}
			}
			else
			{
				this.DelTestServer(false);
				PlayerData.CurGameServerData = null;
			}
		}
	}

	// Token: 0x060048EB RID: 18667 RVA: 0x00177918 File Offset: 0x00175B18
	public void UpdateServerList(List<game_server> serverlist)
	{
		this.GameServerList.Clear();
		this.GameServerList = serverlist;
		if (!GameSettingData.IsLocalTestServer || (GameSettingData.IsLocalTestServer && GameSettingData.LocalTestServerID == 0))
		{
			this.updateUserServer();
			if (PlayerData.CurGameServerData != null)
			{
				for (int i = 0; i < this.GameServerList.Count; i++)
				{
					if (PlayerData.CurGameServerData.serverId == this.GameServerList[i].serverId)
					{
						PlayerData.CurGameServerData.serverId = this.GameServerList[i].serverId;
						PlayerData.CurGameServerData.serverIP = this.GameServerList[i].serverIP;
						PlayerData.CurGameServerData.serverPort = this.GameServerList[i].serverPort;
						PlayerData.CurGameServerData.serverName = this.GameServerList[i].serverName;
						PlayerData.CurGameServerData.serverState = this.GameServerList[i].serverState;
						if (this.GameServerList[i].HasNewServer)
						{
							PlayerData.CurGameServerData.newServer = this.GameServerList[i].newServer;
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x060048EC RID: 18668 RVA: 0x00177A60 File Offset: 0x00175C60
	public void ChooseRecommendServer()
	{
		string serverAreaName = SingletonDontDestoryUnity<GameManager>.Instance.ServerAreaName;
		int num = 0;
		if (!string.IsNullOrEmpty(serverAreaName))
		{
			string text = serverAreaName;
			if (text != null)
			{
				if (MenuSceneController.m_switch_map14 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
					dictionary.Add("Asia", 0);
					dictionary.Add("Australia", 0);
					dictionary.Add("Europe", 1);
					dictionary.Add("Africa", 1);
					dictionary.Add("North America", 2);
					dictionary.Add("South America", 2);
					dictionary.Add("Antarctica", 2);
					MenuSceneController.m_switch_map14 = dictionary;
				}
				int num2 = -1;
				if (MenuSceneController.m_switch_map14.TryGetValue(text, out num2))
				{
					if (num2 == 0)
					{
						num = 2;
					}
					else if (num2 == 1)
					{
						num = 1;
					}
					else if (num2 == 2)
					{
						num = 0;
					}
				}
			}
		}
		else
		{
			Debug.Log("AreaName: is null or empty");
		}
		List<game_server> nearTimezoneServer = this.getNearTimezoneServer();
		if (nearTimezoneServer.Count > 0)
		{
			// Manual Sort to avoid lambdas/delegates
			for (int i = 0; i < nearTimezoneServer.Count; i++)
			{
				for (int j = i + 1; j < nearTimezoneServer.Count; j++)
				{
					bool swap = false;
					if (nearTimezoneServer[i].serverRank > nearTimezoneServer[j].serverRank) swap = true;
					else if (nearTimezoneServer[i].serverRank == nearTimezoneServer[j].serverRank && nearTimezoneServer[i].serverId > nearTimezoneServer[j].serverId) swap = true;

					if (swap)
					{
						game_server temp = nearTimezoneServer[i];
						nearTimezoneServer[i] = nearTimezoneServer[j];
						nearTimezoneServer[j] = temp;
					}
				}
			}
			List<game_server> list = new List<game_server>();
			for (int i = nearTimezoneServer.Count - 1; i >= 0; i--)
			{
				if ((int)nearTimezoneServer[i].serverArea == num)
				{
					list.Add(nearTimezoneServer[i]);
					break;
				}
			}
			game_server recommendServer;
			if (list != null && list.Count > 0)
			{
				recommendServer = this.GetRecommendServer(list);
			}
			else
			{
				recommendServer = this.GetRecommendServer(nearTimezoneServer);
			}
			if (recommendServer != null)
			{
				PlayerData.CurGameServerData = new game_server();
				PlayerData.CurGameServerData.serverId = recommendServer.serverId;
				PlayerData.CurGameServerData.serverIP = recommendServer.serverIP;
				PlayerData.CurGameServerData.serverPort = recommendServer.serverPort;
				PlayerData.CurGameServerData.serverName = recommendServer.serverName;
				PlayerData.CurGameServerData.serverState = recommendServer.serverState;
				if (recommendServer.HasNewServer)
				{
					PlayerData.CurGameServerData.newServer = recommendServer.newServer;
				}
			}
			else
			{
				PlayerData.CurGameServerData = null;
			}
		}
		else
		{
			PlayerData.CurGameServerData = null;
		}
	}

	// Token: 0x060048ED RID: 18669 RVA: 0x00177C9C File Offset: 0x00175E9C
	public game_server GetRecommendServer(List<game_server> uselist)
	{
		List<game_server> list = new List<game_server>();
		int num = -1;
		for (int i = 0; i < uselist.Count; i++)
		{
			if ((long)num < uselist[i].serverRank)
			{
				num = (int)uselist[i].serverRank;
			}
		}
		int num2 = 0;
		for (int j = 0; j < uselist.Count; j++)
		{
			if (num == (int)uselist[j].serverRank)
			{
				list.Add(uselist[j]);
				num2 += (int)uselist[j].serverWeight;
			}
		}
		if (list == null || list.Count == 0)
		{
			return null;
		}
		game_server result;
		if (num2 == 0)
		{
			int num3 = UnityEngine.Random.Range(0, list.Count);
			result = list[num3];
		}
		else
		{
			int num4 = UnityEngine.Random.Range(0, num2);
			result = list[list.Count - 1];
			for (int k = 0; k < list.Count; k++)
			{
				if (num4 < (int)list[k].serverWeight)
				{
					result = list[k];
					break;
				}
				num4 -= (int)list[k].serverWeight;
			}
		}
		return result;
	}

	// Token: 0x060048EE RID: 18670 RVA: 0x00177DE0 File Offset: 0x00175FE0
	public List<game_server> getNearTimezoneServer()
	{
		List<game_server> list = new List<game_server>();
		for (int i = 0; i < this.GameServerList.Count; i++)
		{
			if (this.GameServerList[i].serverState != 3L)
			{
				list.Add(this.GameServerList[i]);
			}
		}
		int LocalTimezone = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
		for (int i = 0; i < list.Count; i++)
		{
			for (int j = i + 1; j < list.Count; j++)
			{
				int diffX = Mathf.Abs((int)list[i].serverTimeZone - LocalTimezone);
				int diffY = Mathf.Abs((int)list[j].serverTimeZone - LocalTimezone);
				if (diffX > diffY)
				{
					game_server temp = list[i];
					list[i] = list[j];
					list[j] = temp;
				}
			}
		}
		for (int k = list.Count - 1; k > 0; k--)
		{
			if (Mathf.Abs((float)(list[k].serverTimeZone - (long)LocalTimezone)) != Mathf.Abs((float)(list[0].serverTimeZone - (long)LocalTimezone)))
			{
				list.RemoveAt(k);
			}
		}
		return list;
	}

	// Token: 0x060048EF RID: 18671 RVA: 0x00177ECC File Offset: 0x001760CC
	public void SaveUseServer(string use_server)
	{
		bool ishave = false;
		if (!string.IsNullOrEmpty(use_server))
		{
			string[] array = use_server.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (int.Parse(array[i]) == 2)
				{
					ishave = true;
					break;
				}
			}
		}
		this.DelTestServer(ishave);
		if (!string.IsNullOrEmpty(use_server))
		{
			this.user_Str = use_server;
			string[] array2 = use_server.Split(new char[]
			{
				'#'
			});
			for (int j = 0; j < array2.Length; j++)
			{
				for (int k = 0; k < this.GameServerList.Count; k++)
				{
					if (this.GameServerList[k].serverId == (long)int.Parse(array2[j]))
					{
						this.UserServerList.Add(this.GameServerList[k]);
						break;
					}
				}
			}
		}
	}

	// Token: 0x060048F0 RID: 18672 RVA: 0x00177FC4 File Offset: 0x001761C4
	public void updateUserServer()
	{
		bool ishave = false;
		if (!string.IsNullOrEmpty(this.user_Str))
		{
			string[] array = this.user_Str.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (int.Parse(array[i]) == 2)
				{
					ishave = true;
					break;
				}
			}
		}
		this.DelTestServer(ishave);
		for (int j = 0; j < this.UserServerList.Count; j++)
		{
			for (int k = 0; k < this.GameServerList.Count; k++)
			{
				if (this.GameServerList[k].serverId == this.UserServerList[j].serverId)
				{
					this.UserServerList[j] = this.GameServerList[k];
					break;
				}
			}
		}
	}

	// Token: 0x060048F1 RID: 18673 RVA: 0x001780AC File Offset: 0x001762AC
	private void DelTestServer(bool ishave)
	{
		if (!ishave && this.GameServerList.Count > 1)
		{
			for (int i = this.GameServerList.Count - 1; i >= 0; i--)
			{
				if (this.GameServerList[i].serverId == 2L)
				{
					this.GameServerList.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x060048F2 RID: 18674 RVA: 0x00178114 File Offset: 0x00176314
	public List<game_server> GetAreaServerList(int areaid)
	{
		List<game_server> list = new List<game_server>();
		if (areaid == -1)
		{
			list = new List<game_server>(this.UserServerList);
		}
		else
		{
			for (int i = 0; i < this.GameServerList.Count; i++)
			{
				if ((int)this.GameServerList[i].serverArea == areaid)
				{
					list.Add(this.GameServerList[i]);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = j + 1; k < list.Count; k++)
				{
					if (list[j].serverId > list[k].serverId)
					{
						game_server game_server = list[j];
						list[j] = list[k];
						list[k] = game_server;
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060048F3 RID: 18675 RVA: 0x001781AC File Offset: 0x001763AC
	public void DataLoadFinish()
	{
		MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(GameDefine.SCENE_DEFINE.SCENE_MAIN_CITY);
		SoundData soundDataById = DataManager.GetSoundDataById(mapInfoDataByID.GetAudioID());
		if (soundDataById != null)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlayBGMusic(soundDataById.Id, soundDataById.FadeOutTime, soundDataById.FadeInTime);
		}
		for (int i = 0; i < this.mNpcObjModelNameList.Length; i++)
		{
			Transform transform = GameObject.Find(string.Format("DSJ_shangYeZhongXin_1/animtion/{0}", this.mNpcObjModelNameList[i])).transform;
			if (this.mNpcObjList[i] == null)
			{
				this.mNpcObjList[i] = new FakeObjLogic();
			}
			if (this.mNpcObjList[i].FakeObj == null)
			{
				this.mNpcObjList[i].InitAnimaFakeNpcObj(this.mNpcObjModelNameList[i], transform, "tiaowu");
			}
		}
	}

	// Token: 0x060048F4 RID: 18676 RVA: 0x00178274 File Offset: 0x00176474
	public void AddServerList()
	{
	}

	// Token: 0x060048F5 RID: 18677 RVA: 0x00178278 File Offset: 0x00176478
	public void SetTestServer()
	{
		switch (GameSettingData.LocalTestServerID)
		{
		case 0:
			PlayerData.CurLoginServerData.LoginIP = "ec2-52-91-30-19.compute-1.amazonaws.com";
			PlayerData.CurLoginServerData.LoginPort = 9777;
			PlayerData.CurLoginServerData.IsUseDns = true;
			break;
		case 1:
			PlayerData.CurLoginServerData.LoginIP = "s16.serv00.com";
			PlayerData.CurLoginServerData.LoginPort = 9777;
			PlayerData.CurLoginServerData.IsUseDns = false;
			PlayerData.CurGameServerData = new game_server();
			PlayerData.CurGameServerData.serverIP = "s16.serv00.com";
			PlayerData.CurGameServerData.serverPort = 15678L;
			PlayerData.CurGameServerData.serverName = "Liu";
			PlayerData.CurGameServerData.serverState = 0L;
			PlayerData.CurGameServerData.serverId = 1L;
			PlayerData.CurGameServerData.newServer = 1L;
			break;
		case 2:
			PlayerData.CurLoginServerData.LoginIP = "s16.serv00.com";
			PlayerData.CurLoginServerData.LoginPort = 9777;
			PlayerData.CurLoginServerData.IsUseDns = false;
			PlayerData.CurGameServerData = new game_server();
			PlayerData.CurGameServerData.serverIP = "s16.serv00.com";
			PlayerData.CurGameServerData.serverPort = 15678L;
			PlayerData.CurGameServerData.serverName = "Li";
			PlayerData.CurGameServerData.serverState = 0L;
			PlayerData.CurGameServerData.serverId = 1L;
			PlayerData.CurGameServerData.newServer = 0L;
			break;
		case 3:
			PlayerData.CurLoginServerData.LoginIP = "s16.serv00.com";
			PlayerData.CurLoginServerData.LoginPort = 9777;
			PlayerData.CurLoginServerData.IsUseDns = false;
			PlayerData.CurGameServerData = new game_server();
			PlayerData.CurGameServerData.serverIP = "s16.serv00.com";
			PlayerData.CurGameServerData.serverPort = 15678L;
			PlayerData.CurGameServerData.serverName = "Local Server";
			PlayerData.CurGameServerData.serverState = 0L;
			PlayerData.CurGameServerData.serverId = 1L;
			PlayerData.CurGameServerData.newServer = 0L;
			break;
		}
	}

	// Token: 0x04003616 RID: 13846
	public List<game_server> GameServerList = new List<game_server>();
	public List<game_server> UserServerList = new List<game_server>();
	public string user_Str = string.Empty;
	private FakeObjLogic[] mNpcObjList = new FakeObjLogic[2];
	private string[] mNpcObjModelNameList = new string[] { "NPC_Nv_004", "NPC_Nv_005" };
	public static Dictionary<string, int> m_switch_map14;

	// Token: 0x02000AFC RID: 2812
	// (Invoke) Token: 0x06005079 RID: 20601
	public delegate void OnLoadPartFinisheDel(GameObject obj);
}
