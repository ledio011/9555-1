using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class NetManager : SingletonDontDestoryUnity<NetManager>
{
	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001463 RID: 5219 RVA: 0x00083F8C File Offset: 0x0008218C
	public int NetDelayTime
	{
		get
		{
			return this.netTime;
		}
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x00083F94 File Offset: 0x00082194
	protected override void Awake()
	{
		base.Awake();
		if (!SingletonDontDestoryUnity<NetManager>.Exists || !base.IsInit)
		{
			return;
		}
		NetLogic.GetInstance();
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x00083FC4 File Offset: 0x000821C4
	public void LeaveGame()
	{
		NetLogic.GetInstance().Send<Protocol.leave_game>(null, null);
		NetLogic.GetInstance().SendLast();
		NetLogic.GetInstance().DisconnectServer();
		this.connectGameServer = -1;
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x00083FF8 File Offset: 0x000821F8
	private void OnApplicationQuit()
	{
		this.LeaveGame();
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x00084000 File Offset: 0x00082200
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x00084008 File Offset: 0x00082208
	public void ConnectToServer(string ip, int nport, NetLogic.ConnectDelegate delConnect = null, NetLogic.ConnectLostDelegate delConnectLost = null)
	{
		if (delConnect != null)
		{
			NetLogic.SetConnectDelegate(delConnect);
		}
		else
		{
			NetLogic.SetConnectDelegate(new NetLogic.ConnectDelegate(this.ConnectSuccess));
		}
		if (delConnectLost != null)
		{
			NetLogic.SetConnectLostDelegate(delConnectLost);
		}
		else
		{
			NetLogic.SetConnectLostDelegate(new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
		NetLogic.GetInstance().ConnectToServer(ip, nport, 500);
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x0008406C File Offset: 0x0008226C
	private void ConnectSuccess(bool isSuccess)
	{
		if (isSuccess)
		{
			this.sendTime2 = 0L;
			this.LoginRequest((long)PlayerData.loginType);
		}
		else
		{
			WaitResponseUIRootLogic.CloseBox();
			NoticeLogic.AddNotifyData("#{200065}", true, false);
		}
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x000840AC File Offset: 0x000822AC
	public void QueueFinish()
	{
		this.LoginRequest((long)PlayerData.loginType);
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x000840BC File Offset: 0x000822BC
	private void LoginRequest(long logintype)
	{
		login.request request = new login.request();
		request.session = PlayerData.session;
		request.id = PlayerData.GetPlayerAccountId();
		request.logintype = logintype;
		request.version = GameSettingData.GameVersion;
		request.time = (long)((int)(Time.realtimeSinceStartup * 100f));
		if (PlayerData.CurGameServerData != null)
		{
			request.serverId = PlayerData.CurGameServerData.serverId;
		}
		request.unityVersion = GameSettingData.UnityVersion;
		NetLogic.GetInstance().Send<Protocol.login>(request, new RpcRspHandler(this.LoginResponse));
		NetLogic.GetInstance().StartReconnecting();
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x00084150 File Offset: 0x00082350
	public void PickResponse(SprotoTypeBase req)
	{
		character_pick.response response = req as character_pick.response;
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (response != null && response.HasErrno)
		{
			if (response.errno == 0L)
			{
				WaitResponseUIRootLogic.CloseBox();
				NoticeLogic.AddNotifyData("#{100153}", true, false);
				this.LeaveGame();
				if (!Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
				{
					LoadingWindow.LoadScene(0);
				}
			}
			else if (response.errno == 2L)
			{
				WaitResponseUIRootLogic.CloseBox();
				NoticeLogic.AddNotifyData("#{100174}", true, false);
				this.LeaveGame();
				if (!Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
				{
					LoadingWindow.LoadScene(0);
				}
			}
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000841FC File Offset: 0x000823FC
	private void LoginResponse(SprotoTypeBase req)
	{
		NetLogic.GetInstance().FinishReconnecting();
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		login.response response = req as login.response;
		bool isFinishDownload = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload;
		if (response != null)
		{
			if (response.type > 1L)
			{
				GameManager.OnLineState = true;
				this.connectGameServer = 0;
				instance.PlayerCommonData.ServerLevel = (int)response.serverLevel;
				if (Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
				{
					if (SingletonUnity<CreateRoleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CreateRoleRootLogic>.Instance.gameObject))
					{
						WaitResponseUIRootLogic.CloseBox();
					}
					else
					{
						if (response.HasVersionCode && this.IsNeedQuitUpdate(response.versionCode, delegate
						{
							if (PlayerData.downLoadFlag == 1 && int.Parse(response.dataVersionCode) != PlayerData.LocalDataVersion)
							{
								WaitResponseUIRootLogic.CloseBox();
								SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginRootUI);
								if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
								{
									PlayerData.ServerDataVersion = -1;
								}
								SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, delegate
								{
									SingletonUnity<AccountVersionCheckRootLogic>.Instance.ResetDownLoadPage(false);
								}, null);
							}
							else
							{
								if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
								{
									PlayerData.ServerDataVersion = -1;
								}
								this.CharacterListRequest();
							}
						}))
						{
							return;
						}
						if (PlayerData.downLoadFlag == 1 && int.Parse(response.dataVersionCode) != PlayerData.LocalDataVersion)
						{
							WaitResponseUIRootLogic.CloseBox();
							SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginRootUI);
							if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
							{
								PlayerData.ServerDataVersion = -1;
							}
							SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, delegate
							{
								SingletonUnity<AccountVersionCheckRootLogic>.Instance.ResetDownLoadPage(false);
							}, null);
						}
						else
						{
							if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
							{
								PlayerData.ServerDataVersion = -1;
							}
							this.CharacterListRequest();
						}
					}
				}
				else
				{
					WaitResponseUIRootLogic.CloseBox();
					if (isFinishDownload && !this.IsVersionSame(response))
					{
						NoticeLogic.AddNotifyData("#{200064}", true, false);
						this.LeaveGame();
						LoadingWindow.LoadScene(0);
						this.connectGameServer = -1;
						return;
					}
					SingletonDontDestoryUnity<GameManager>.Instance.QueryInventory();
					SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.OnReconnectSuccess();
					NoticeLogic.AddNotifyData("#{100146}", true, false);
					if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene())
					{
						Singleton<ObjManager>.Instance.RecycleAllNPC();
						Singleton<ObjManager>.Instance.RecycleAllOtherPlayer();
						if (SingletonUnity<SurveyProgressLineLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SurveyProgressLineLogic>.Instance.gameObject))
						{
							SingletonUnity<SurveyProgressLineLogic>.Instance.StopSurveyItem();
						}
					}
					refresh_online_state.request request = new refresh_online_state.request();
					request.id = PlayerData.MainPlayerServerId;
					if (Singleton<ObjManager>.Instance.MainPlayer == null)
					{
						request.type = 1L;
						if (!LoadingWindow.isSendMapReady)
						{
							NetLogic.GetInstance().Send<Protocol.map_ready>(null, null);
							LoadingWindow.isSendMapReady = true;
						}
					}
					else
					{
						request.type = 0L;
						if (!LoadingWindow.isSendMapReady)
						{
							NetLogic.GetInstance().Send<Protocol.map_ready>(null, null);
							LoadingWindow.isSendMapReady = true;
						}
					}
					request.mapId = SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr;
					NetLogic.GetInstance().Send<Protocol.refresh_online_state>(request, null);
				}
			}
			else if (response.type > 0L)
			{
				this.connectGameServer = 0;
				instance.PlayerCommonData.ServerLevel = (int)response.serverLevel;
				GameManager.OnLineState = true;
				if (Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
				{
					if (SingletonUnity<CreateRoleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CreateRoleRootLogic>.Instance.gameObject))
					{
						WaitResponseUIRootLogic.CloseBox();
					}
					else
					{
						if (response.HasVersionCode && this.IsNeedQuitUpdate(response.versionCode, delegate
						{
							if (PlayerData.downLoadFlag == 1 && int.Parse(response.dataVersionCode) != PlayerData.LocalDataVersion)
							{
								WaitResponseUIRootLogic.CloseBox();
								SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginRootUI);
								if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
								{
									PlayerData.ServerDataVersion = -1;
								}
								SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, delegate
								{
									SingletonUnity<AccountVersionCheckRootLogic>.Instance.ResetDownLoadPage(false);
								}, null);
							}
							else
							{
								if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
								{
									PlayerData.ServerDataVersion = -1;
								}
								this.CharacterListRequest();
							}
						}))
						{
							return;
						}
						if (PlayerData.downLoadFlag == 1 && int.Parse(response.dataVersionCode) != PlayerData.LocalDataVersion)
						{
							WaitResponseUIRootLogic.CloseBox();
							SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoginRootUI);
							if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
							{
								PlayerData.ServerDataVersion = -1;
							}
							SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, delegate
							{
								SingletonUnity<AccountVersionCheckRootLogic>.Instance.ResetDownLoadPage(false);
							}, null);
						}
						else
						{
							if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
							{
								PlayerData.ServerDataVersion = -1;
							}
							this.CharacterListRequest();
						}
					}
				}
				else
				{
					if (isFinishDownload && !this.IsVersionSame(response))
					{
						NoticeLogic.AddNotifyData("#{200064}", true, false);
						this.LeaveGame();
						LoadingWindow.LoadScene(0);
						this.connectGameServer = -1;
						return;
					}
					SingletonDontDestoryUnity<GameManager>.Instance.QueryInventory();
					bool isTutorialFinish = instance.PlayerData.IsTutorialFinish;
					instance.PlayerData.ResetPlayerData();
					instance.PlayerCommonData.ClearData();
					character_pick.request request2 = new character_pick.request();
					request2.id = PlayerData.MainPlayerServerId;
					NetLogic.GetInstance().Send<Protocol.character_pick>(request2, new RpcRspHandler(this.PickResponse));
					instance.FirstEnterGame = true;
					instance.IsShowMainMissionTip = true;
					NetLogic.GetInstance().StartReconnecting();
					instance.PlayerData.IsTutorialFinish = isTutorialFinish;
				}
			}
			else
			{
				WaitResponseUIRootLogic.CloseBox();
				NoticeLogic.AddNotifyData("#{200064}", true, false);
				this.LeaveGame();
				LoadingWindow.LoadScene(0);
				this.connectGameServer = -1;
			}
		}
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x00084740 File Offset: 0x00082940
	private bool IsVersionSame(login.response response)
	{
		string[] array = response.versionCode.Split(new char[]
		{
			'.'
		});
		string[] array2 = GameSettingData.GameVersion.Split(new char[]
		{
			'.'
		});
		int[] array3 = new int[array.Length];
		int[] array4 = new int[array2.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array3[i] = int.Parse(array[i]);
			array4[i] = int.Parse(array2[i]);
		}
		for (int j = 0; j < 2; j++)
		{
			if (array3[j] > array4[j])
			{
				return false;
			}
		}
		return array3[2] <= array4[2] && int.Parse(response.dataVersionCode) <= PlayerData.LocalDataVersion;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x00084808 File Offset: 0x00082A08
	public void CharacterListRequest()
	{
		NetLogic.GetInstance().Send<Protocol.character_list>(null, new RpcRspHandler(this.CharacterListResponse));
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x00084824 File Offset: 0x00082A24
	public void GameCheck()
	{
		NetLogic.GetInstance().Send<Protocol.game_check>(null, null);
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x00084834 File Offset: 0x00082A34
	private void CharacterListResponse(SprotoTypeBase req)
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.AccountVersionCheckRootUI);
		character_list.response response = req as character_list.response;
		WaitResponseUIRootLogic.CloseBox();
		if (response != null)
		{
			if (response.character.Count == 0)
			{
				SingletonUnity<MenuSceneController>.Instance.ShowCreateRole(true);
			}
			else
			{
				SingletonUnity<MenuSceneController>.Instance.ShowChooseRole(response);
			}
		}
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x00084890 File Offset: 0x00082A90
	public void CheckConnectLost()
	{
		if (NetLogic.GetInstance().ConnectStatus == NetLogic.CONNECT_STATUS.DISCONNECTED && SingletonUnity<UIManager>.Exists)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
			{
				SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
			}, null);
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000848D8 File Offset: 0x00082AD8
	private void CheckConnectTimeOut()
	{
		if (NetManager.checkTime < 0f)
		{
			return;
		}
		if (!NetLogic.GetInstance().CanProcessPack)
		{
			NetManager.checkTime = 0f;
			return;
		}
		NetManager.checkTime += Time.deltaTime;
		if (NetManager.checkTime <= 12f)
		{
			return;
		}
		if (NetLogic.GetInstance().ReConnectingFlag)
		{
			NetLogic.GetInstance().FinishReconnecting();
			if (!Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
			{
				NoticeLogic.AddNotifyData("#{200064}", true, false);
				this.LeaveGame();
				LoadingWindow.LoadScene(0);
				this.connectGameServer = -1;
				return;
			}
		}
		if (SingletonUnity<TopMessageBoxLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TopMessageBoxLogic>.Instance.gameObject))
		{
			return;
		}
		if (SingletonUnity<UIManager>.Exists)
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
			{
				SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleTimeClick), "#{100143}", "#{100144}");
			}, null);
		}
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000849C8 File Offset: 0x00082BC8
	public void OnCancleTimeClick()
	{
		NetManager.checkTime = -1f;
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		LoadingWindow.LoadScene(0);
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000849E4 File Offset: 0x00082BE4
	private void OnYesTimeClick()
	{
		NetManager.checkTime = -1f;
		WaitResponseUIRootLogic.OpenWaitBox(0, 60f, 0f, null);
		this.ReConnectToServer(new NetLogic.ConnectDelegate(this.ReConnectSuccess), null);
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x00084A20 File Offset: 0x00082C20
	public void ConnectLost()
	{
		GameManager.OnLineState = false;
		this.connectGameServer = -1;
		if (NetLogic.GetInstance().ReConnectingFlag)
		{
			NetLogic.GetInstance().FinishReconnecting();
			if (!Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
			{
				NoticeLogic.AddNotifyData("#{200064}", true, false);
				this.LeaveGame();
				LoadingWindow.LoadScene(0);
				this.connectGameServer = -1;
				return;
			}
		}
		if (SingletonUnity<UIManager>.Exists)
		{
			TutorialManager.CloseTutorial();
			WaitResponseUIRootLogic.CloseBox();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
			{
				SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
			}, null);
		}
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x00084AB8 File Offset: 0x00082CB8
	private void OnYesClick()
	{
		if (Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME) && ((SingletonUnity<CreateRoleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CreateRoleRootLogic>.Instance.gameObject)) || (SingletonUnity<ChooseRoleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChooseRoleRootLogic>.Instance.gameObject))))
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonUnity<MenuSceneController>.Instance.ShowChooseServer();
			return;
		}
		WaitResponseUIRootLogic.OpenWaitBox(0, -1f, 0f, null);
		this.ReConnectToServer(new NetLogic.ConnectDelegate(this.ReConnectSuccess), null);
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x00084B4C File Offset: 0x00082D4C
	private void ReConnectSuccess(bool isSuccess)
	{
		if (isSuccess)
		{
			this.netTime = 0;
			this.sendTime2 = 0L;
			this.sendTime1 = DateTime.UtcNow.Ticks;
			NetManager.checkTime = -1f;
			this.LoginRequest(0L);
		}
		else
		{
			WaitResponseUIRootLogic.CloseBox();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
			{
				SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
			}, null);
		}
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x00084BBC File Offset: 0x00082DBC
	private void OnCancleClick()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		LoadingWindow.LoadScene(0);
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x00084BD0 File Offset: 0x00082DD0
	public void ReConnectToServer(NetLogic.ConnectDelegate delConnect = null, NetLogic.ConnectLostDelegate delConnectLost = null)
	{
		if (delConnect != null)
		{
			NetLogic.SetConnectDelegate(delConnect);
		}
		else
		{
			NetLogic.SetConnectDelegate(new NetLogic.ConnectDelegate(this.ConnectSuccess));
		}
		if (delConnectLost != null)
		{
			NetLogic.SetConnectLostDelegate(delConnectLost);
		}
		else
		{
			NetLogic.SetConnectLostDelegate(new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
		NetLogic.GetInstance().ReConnectToServer();
		this.connectGameServer = -1;
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x00084C34 File Offset: 0x00082E34
	private void HeaerBeat()
	{
		heart_beat.request request = new heart_beat.request();
		this.sendTime2 = DateTime.UtcNow.Ticks;
		request.time = this.sendTime2;
		this.sendTime1 = this.sendTime2;
		NetManager.checkTime = 0f;
		request.time2 = (long)((int)(Time.realtimeSinceStartup * 100f));
		NetLogic.GetInstance().Send<Protocol.heart_beat>(request, new RpcRspHandler(this.HeaerBeatResponse));
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x00084CA8 File Offset: 0x00082EA8
	private void HeaerBeatResponse(SprotoTypeBase req)
	{
		heart_beat.response response = req as heart_beat.response;
		this.sendTime2 = 0L;
		NetManager.checkTime = -1f;
		if (response != null)
		{
			this.netTime = (int)((DateTime.UtcNow.Ticks - response.time) / 100000L);
		}
		if (response.HasServerTime)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ServerTime = response.serverTime;
		}
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x00084D18 File Offset: 0x00082F18
	private void Update()
	{
		NetLogic.GetInstance().Update();
		if (NetLogic.GetInstance().ConnectStatus != NetLogic.CONNECT_STATUS.CONNRCTED)
		{
			return;
		}
		if (this.connectGameServer > -1)
		{
			this.curTime += Time.deltaTime;
			if (this.curTime > 15f)
			{
				this.curTime = 0f;
				this.HeaerBeat();
			}
			if (this.sendTime2 > 0L)
			{
				this.netTime = (int)((DateTime.UtcNow.Ticks - this.sendTime1) / 100000L);
			}
			this.CheckConnectTimeOut();
		}
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x00084DB4 File Offset: 0x00082FB4
	private void OnApplicationFocus(bool isfocus)
	{
		NetManager.checkTime = -1f;
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x00084DC0 File Offset: 0x00082FC0
	public bool IsNeedQuitUpdate(string serverVersionCode, MessageBoxLogic.OnCancelClick onContinueGame)
	{
		if (!string.IsNullOrEmpty(serverVersionCode) && !this.lastCheckVersion.Equals(serverVersionCode))
		{
			this.lastCheckVersion = serverVersionCode;
			string[] array = serverVersionCode.Split(new char[]
			{
				'.'
			});
			string[] array2 = GameSettingData.GameVersion.Split(new char[]
			{
				'.'
			});
			int[] array3 = new int[array.Length];
			int[] array4 = new int[array2.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array3[i] = int.Parse(array[i]);
				array4[i] = int.Parse(array2[i]);
			}
			for (int j = 0; j < 2; j++)
			{
				if (array3[j] > array4[j])
				{
					WaitResponseUIRootLogic.CloseBox();
					MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{100179}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
					{
						SingletonDontDestoryUnity<GameManager>.Instance.Rating();
						Application.Quit();
					});
					return true;
				}
			}
			if (array3[2] > array4[2])
			{
				WaitResponseUIRootLogic.CloseBox();
				MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100178}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					SingletonDontDestoryUnity<GameManager>.Instance.Rating();
					Application.Quit();
				}, onContinueGame, null, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x00084F20 File Offset: 0x00083120
	public void ClearLastCheckNeedQuitUpdateVersion()
	{
		this.lastCheckVersion = string.Empty;
	}

	// Token: 0x04001810 RID: 6160
	private const int MAX_HEART_BEAT_SECOND = 15;

	// Token: 0x04001811 RID: 6161
	private int connectGameServer = -1;

	// Token: 0x04001812 RID: 6162
	private float curTime = 15f;

	// Token: 0x04001813 RID: 6163
	private int netTime = -1;

	// Token: 0x04001814 RID: 6164
	private long sendTime2;

	// Token: 0x04001815 RID: 6165
	private long sendTime1;

	// Token: 0x04001816 RID: 6166
	private static float checkTime = -1f;

	// Token: 0x04001817 RID: 6167
	private string lastCheckVersion = string.Empty;
}
