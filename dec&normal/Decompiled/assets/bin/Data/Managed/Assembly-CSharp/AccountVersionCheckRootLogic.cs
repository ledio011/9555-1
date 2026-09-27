using System;
using System.Text;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020009EC RID: 2540
public class AccountVersionCheckRootLogic : SingletonUnity<AccountVersionCheckRootLogic>
{
	// Token: 0x06004852 RID: 18514 RVA: 0x00172D84 File Offset: 0x00170F84
	private new void Awake()
	{
		base.Awake();
		this.curAutoReDownloadTimes = 0;
		NGUITools.SetActive(this.CheckAccountRoot, false);
		NGUITools.SetActive(this.CheckVersionRoot, false);
		NGUITools.SetActive(this.ConnectLabel.gameObject, true);
		this.ConnectLabel.text = StrDictionary.GetDictionaryString("#{102211}", new object[0]);
		this.ProgressLinePic.width = UIWidgetControl.GetFitWidth(this.ProgressLinePic.width);
		this.ProgressLinePic.transform.localPosition = new Vector3((float)(-(float)this.ProgressLinePic.width / 2), 0f, 0f);
		this.mProgressLineWidth = this.ProgressLinePic.width;
		this.SetProgressLineWidth(0);
		this.updateHelper = base.gameObject.GetComponent<FileUpdateHelper>();
		if (this.updateHelper == null)
		{
			this.updateHelper = base.gameObject.AddComponent<FileUpdateHelper>();
		}
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x00172E78 File Offset: 0x00171078
	private void SetProgressLineWidth(int width)
	{
		this.ProgressLinePic.width = width;
		this.LineAnimaObj.transform.localPosition = new Vector3((float)width, 0f, 0f);
	}

	// Token: 0x06004854 RID: 18516 RVA: 0x00172EB4 File Offset: 0x001710B4
	public void StartVerifyAccount()
	{
		if (AccountVersionCheckRootLogic.FaceBookState != 0)
		{
			this.ReLoginAndBindFacebook();
			AccountVersionCheckRootLogic.FaceBookState = 0;
			return;
		}
		string playerAccountId = PlayerData.GetPlayerAccountId();
		if (string.IsNullOrEmpty(playerAccountId))
		{
			this.ResetAccountPage();
		}
		else
		{
			verfiy.request request = new verfiy.request();
			request.id = playerAccountId;
			request.key = PlayerData.GetPlayerAccountKey();
			this.curVerifyId = playerAccountId;
			this.curVerifyKey = request.key;
			request.versionCode = GameSettingData.GameVersion;
			NetLogic.GetInstance().Send<Protocol.verfiy>(request, new RpcRspHandler(this.OnVerifyResponse));
		}
	}

	// Token: 0x06004855 RID: 18517 RVA: 0x00172F44 File Offset: 0x00171144
	public void OnClickCreateAccountBtn()
	{
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{201023}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			PlayerData.ClearAccount();
			WaitResponseUIRootLogic.OpenWaitBox(2, 0f, 0f, null);
			if (NetLogic.GetInstance().ConnectStatus == NetLogic.CONNECT_STATUS.CONNRCTED)
			{
				NetLogic.GetInstance().Send<Protocol.visitor>(null, new RpcRspHandler(this.VisitorResponse));
			}
			else
			{
				SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, delegate(bool success)
				{
					if (success)
					{
						NetLogic.GetInstance().Send<Protocol.visitor>(null, new RpcRspHandler(this.VisitorResponse));
					}
					else
					{
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
						{
							SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
						}, null);
						NoticeLogic.AddNotifyData("#{200065}", true, false);
					}
				}, new NetLogic.ConnectLostDelegate(this.ConnectLost));
			}
		}, delegate
		{
			this.ResetAccountPage();
		}, null, null);
	}

	// Token: 0x06004856 RID: 18518 RVA: 0x00172F90 File Offset: 0x00171190
	public void StartCheckVerifyAccount()
	{
		if (AccountVersionCheckRootLogic.FaceBookState != 0)
		{
			this.ReLoginAndBindFacebook();
			AccountVersionCheckRootLogic.FaceBookState = 0;
			return;
		}
		this.VerifyAccount();
	}

	// Token: 0x06004857 RID: 18519 RVA: 0x00172FB0 File Offset: 0x001711B0
	public void ReLoginVerfiyAccount()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		this.ResetAccountPage();
	}

	// Token: 0x06004858 RID: 18520 RVA: 0x00172FC4 File Offset: 0x001711C4
	public void ReLoginAndBindFacebook()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		this.ResetAccountPage();
		this.OnClickFaceBookBtn();
	}

	// Token: 0x06004859 RID: 18521 RVA: 0x00172FDC File Offset: 0x001711DC
	public static void CheckFaceBookBindTips(int type)
	{
		if (PlayerData.FaceBookBind > -1L)
		{
			return;
		}
		if (type == 0 && LocalDataSaveManager.GetFaceBookPopTips(type))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FaceBookTipUIRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<FaceBookTipUIRoot>.Instance.Reset(new DelegateDefine.NoParamDelegate(AccountVersionCheckRootLogic.ClickBindFaceBook), null);
			}, null);
			LocalDataSaveManager.SetFaceBookPopCount(0);
		}
		else if (type == 1 && LocalDataSaveManager.GetFaceBookPopTips(type) && LocalDataSaveManager.GetFaceBookNextPopDay(3))
		{
			if (LocalDataSaveManager.IsFirstEnterGame())
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FaceBookTipUIRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<FaceBookTipUIRoot>.Instance.Reset(new DelegateDefine.NoParamDelegate(AccountVersionCheckRootLogic.ClickBindFaceBook), null);
			}, null);
			LocalDataSaveManager.SetFaceBookPopCount(1);
			LocalDataSaveManager.SetFaceBookBindTime();
		}
	}

	// Token: 0x0600485A RID: 18522 RVA: 0x001730A0 File Offset: 0x001712A0
	public static void ClickBindFaceBook()
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (!Application.loadedLevelName.Equals(GameDefine.LOGIN_SCENE_NAME))
		{
			SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
			AccountVersionCheckRootLogic.FaceBookState = 1;
			LoadingWindow.LoadScene(0);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, delegate(bool bSuccess, object param)
			{
				SingletonUnity<AccountVersionCheckRootLogic>.Instance.ReLoginAndBindFacebook();
			}, null);
		}
	}

	// Token: 0x0600485B RID: 18523 RVA: 0x00173110 File Offset: 0x00171310
	public void OnVerifyResponse(SprotoTypeBase req)
	{
		WaitResponseUIRootLogic.CloseBox();
		verfiy.response response = req as verfiy.response;
		this.mNoticeStr = response.notice;
		this.mNoticeVersion = response.notice_version;
		PlayerData.loginType = 1;
		if (response.HasFacebook_bind || response.HasFacebook_bind1 || response.HasGoogle_bind)
		{
			if (response.HasFacebook_bind1)
			{
				PlayerData.FaceBookBind2 = response.facebook_bind1;
				PlayerData.FaceBookBind = 100L;
			}
			else if (response.HasFacebook_bind1)
			{
				PlayerData.FaceBookBind2 = response.facebook_bind.ToString();
				PlayerData.FaceBookBind = 100L;
			}
			if (response.HasGoogle_bind)
			{
				if (PlayerData.FaceBookBind == 100L)
				{
					PlayerData.FaceBookBind = 300L;
				}
				else
				{
					PlayerData.FaceBookBind = 200L;
				}
			}
		}
		else
		{
			PlayerData.FaceBookBind = -1L;
			PlayerData.FaceBookBind2 = string.Empty;
		}
		if (response.HasVersionCode && SingletonDontDestoryUnity<NetManager>.Instance.IsNeedQuitUpdate(response.versionCode, delegate
		{
			if (response.state == 0L)
			{
				PlayerData.session = response.session;
				PlayerData.loginType = 1;
				if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
				{
					PlayerData.ServerDataVersion = -1;
				}
				if (response.HasDownloadFlag)
				{
					PlayerData.downLoadFlag = (int)response.downloadFlag;
					this.ResetDownLoadPage(true);
				}
				else
				{
					PlayerData.downLoadFlag = 0;
					this.ResetDownLoadPage(true);
				}
				SingletonUnity<MenuSceneController>.Instance.SaveServerList(response);
				PlayerData.SavePlayerAccountId(this.curVerifyId);
				PlayerData.SavePlayerAccountKey(this.curVerifyKey);
			}
			else if (response.state == 2L)
			{
				PlayerData.session = response.session;
				PlayerData.loginType = 2;
				if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
				{
					PlayerData.ServerDataVersion = -1;
				}
				if (response.HasDownloadFlag)
				{
					PlayerData.downLoadFlag = (int)response.downloadFlag;
					this.ResetDownLoadPage(true);
				}
				else
				{
					PlayerData.downLoadFlag = 0;
					this.ResetDownLoadPage(true);
				}
				SingletonUnity<MenuSceneController>.Instance.SaveServerList(response);
				PlayerData.SavePlayerAccountId(this.curVerifyId);
				PlayerData.SavePlayerAccountKey(this.curVerifyKey);
			}
			else if (response.state == 3L)
			{
				NoticeLogic.AddNotifyData("#{200097}", true, false);
				this.ResetAccountPage();
			}
			else
			{
				MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{201022}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					this.ResetAccountPage();
				});
			}
		}))
		{
			return;
		}
		if (response.state == 0L)
		{
			PlayerData.session = response.session;
			PlayerData.loginType = 1;
			if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
			{
				PlayerData.ServerDataVersion = -1;
			}
			if (response.HasDownloadFlag)
			{
				PlayerData.downLoadFlag = (int)response.downloadFlag;
				this.ResetDownLoadPage(true);
			}
			else
			{
				PlayerData.downLoadFlag = 0;
				this.ResetDownLoadPage(true);
			}
			SingletonUnity<MenuSceneController>.Instance.SaveServerList(response);
			PlayerData.SavePlayerAccountId(this.curVerifyId);
			PlayerData.SavePlayerAccountKey(this.curVerifyKey);
		}
		else if (response.state == 2L)
		{
			PlayerData.session = response.session;
			PlayerData.loginType = 2;
			if (!int.TryParse(response.dataVersionCode, ref PlayerData.ServerDataVersion))
			{
				PlayerData.ServerDataVersion = -1;
			}
			if (response.HasDownloadFlag)
			{
				PlayerData.downLoadFlag = (int)response.downloadFlag;
				this.ResetDownLoadPage(true);
			}
			else
			{
				PlayerData.downLoadFlag = 0;
				this.ResetDownLoadPage(true);
			}
			SingletonUnity<MenuSceneController>.Instance.SaveServerList(response);
			PlayerData.SavePlayerAccountId(this.curVerifyId);
			PlayerData.SavePlayerAccountKey(this.curVerifyKey);
		}
		else if (response.state == 3L)
		{
			NoticeLogic.AddNotifyData("#{200097}", true, false);
			this.ResetAccountPage();
		}
		else
		{
			MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{201022}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
			{
				this.ResetAccountPage();
			});
		}
	}

	// Token: 0x0600485C RID: 18524 RVA: 0x00173428 File Offset: 0x00171628
	public void RefreshAccount()
	{
		if (PlayerData.FaceBookBind > -1L)
		{
			if (PlayerData.FaceBookBind == 100L)
			{
				this.FaceBookLabel.text = "Unbind";
				this.BindSpriteIcon.spriteName = "CZ_faceBook";
			}
			else
			{
				this.FaceBookLabel.text = "Unbind";
				this.BindSpriteIcon.spriteName = "CZ_google";
			}
		}
		else
		{
			this.BindSpriteIcon.spriteName = "CZ_zhuangHaoGuanLian";
			this.FaceBookLabel.text = "Bind";
		}
	}

	// Token: 0x0600485D RID: 18525 RVA: 0x001734B8 File Offset: 0x001716B8
	public void ResetAccountPage()
	{
		NGUITools.SetActive(this.ConnectLabel.gameObject, false);
		NGUITools.SetActive(this.CheckAccountRoot, true);
		NGUITools.SetActive(this.CheckVersionRoot, false);
		this.AccountInput.value = PlayerData.GetPlayerAccountId();
		this.PasswordInput.value = PlayerData.GetPlayerAccountKey();
		this.RefreshAccount();
	}

	// Token: 0x17000FBC RID: 4028
	// (get) Token: 0x0600485E RID: 18526 RVA: 0x00173514 File Offset: 0x00171714
	public bool IsVerifyDownload
	{
		get
		{
			return this.mIsVerifyDownload;
		}
	}

	// Token: 0x0600485F RID: 18527 RVA: 0x0017351C File Offset: 0x0017171C
	public void ResetDownLoadPage(bool isVerifyDownload)
	{
		int serverDataVersion = PlayerData.ServerDataVersion;
		if (serverDataVersion >= this.ChristmasVersionMin && serverDataVersion <= this.ChristmasVersionMax)
		{
			this.isAutoDownload = true;
			this.downloadFormat = "Downloading Christmas Resources: {0:N1}/{1:N1}{2}";
		}
		else
		{
			this.isAutoDownload = false;
			this.downloadFormat = "{0:N1}/{1:N1}{2}";
		}
		this.mIsVerifyDownload = isVerifyDownload;
		NGUITools.SetActive(this.ConnectLabel.gameObject, false);
		NGUITools.SetActive(this.CheckAccountRoot, false);
		NGUITools.SetActive(this.CheckVersionRoot, true);
		this.isDownloadData = false;
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), !isVerifyDownload, true);
	}

	// Token: 0x06004860 RID: 18528 RVA: 0x001735D0 File Offset: 0x001717D0
	public void OnChangeUpdateStep(UPDATE_STEP newStep)
	{
		switch (newStep)
		{
		case UPDATE_STEP.CHECK_VERSION:
			this.SetProgressLineVal(0.3f);
			this.CheckVersionInfoLabel.text = StrDictionary.GetDictionaryString("#{102203}", new object[0]);
			break;
		case UPDATE_STEP.GET_FILELIST:
			this.SetProgressLineVal(0.6f);
			this.CheckVersionInfoLabel.text = StrDictionary.GetDictionaryString("#{102204}", new object[0]);
			break;
		case UPDATE_STEP.COMPARE_RES:
			this.SetProgressLineVal(0.95f);
			this.CheckVersionInfoLabel.text = StrDictionary.GetDictionaryString("#{102205}", new object[0]);
			break;
		case UPDATE_STEP.CHECK_IS_DOWNLOAD:
			this.SetProgressLineVal(1f);
			if (this.updateHelper.NeedDownloadSize > 1048576L)
			{
				this.mNeedDownLoadSize = (float)this.updateHelper.NeedDownloadSize / 1024f / 1024f;
				this.mDownLoadUnit = "MB";
			}
			else
			{
				this.mNeedDownLoadSize = (float)this.updateHelper.NeedDownloadSize / 1024f;
				this.mDownLoadUnit = "KB";
			}
			break;
		case UPDATE_STEP.CHECK_RES:
			this.SetProgressLineWidth(0);
			this.SetProgressLineVal(0.3f);
			this.CheckVersionInfoLabel.text = StrDictionary.GetDictionaryString("#{102207}", new object[0]);
			break;
		case UPDATE_STEP.COPY_RES:
			this.SetProgressLineVal(0.6f);
			break;
		case UPDATE_STEP.CLEAR_CACHE:
			this.SetProgressLineVal(0.93f);
			this.isDownloadData = true;
			break;
		case UPDATE_STEP.FINISH:
			if (this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS)
			{
				if (this.mIsVerifyDownload)
				{
					if (!AnimationManager.initDownloadFlag)
					{
						AnimationManager.initDownloadAnimationData(SingletonDontDestoryUnity<GameManager>.Instance);
					}
					else if (this.isDownloadData)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.ReImportAnima();
					}
					if (!DataManager.initFlag)
					{
						DataManager.InitData(SingletonDontDestoryUnity<GameManager>.Instance);
					}
					else if (this.isDownloadData)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.ReImportData();
					}
				}
				else if (this.isDownloadData)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.ReImportData();
					SingletonDontDestoryUnity<GameManager>.Instance.ReImportAnima();
				}
				this.SetProgressLineVal(0.95f);
			}
			else
			{
				this.SetProgressLineVal(0.3f);
				if (this.curAutoReDownloadTimes < GameDefine.AutoReDownloadTimes)
				{
					this.curAutoReDownloadTimes++;
					this.isAutoDownload = true;
					this.ReDownLoad();
				}
				else
				{
					this.isAutoDownload = false;
					MessageBoxLogic.OpenOKCancelBox("#{100156}", "#{100127}", new MessageBoxLogic.OnYesClick(this.ReDownLoad), new MessageBoxLogic.OnCancelClick(this.OnClickExitGame), null, "#{100149}");
				}
			}
			break;
		}
		this.mLastUpdateStep = newStep;
	}

	// Token: 0x06004861 RID: 18529 RVA: 0x0017388C File Offset: 0x00171A8C
	private void OnClickExitGame()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		MessageBoxLogic.CloseBox();
		Application.Quit();
	}

	// Token: 0x06004862 RID: 18530 RVA: 0x001738A4 File Offset: 0x00171AA4
	public void StartDownload()
	{
		this.CheckVersionInfoLabel.text = string.Format(this.downloadFormat, 0.0, this.mNeedDownLoadSize, this.mDownLoadUnit);
		this.updateHelper.DownloadFileList();
	}

	// Token: 0x06004863 RID: 18531 RVA: 0x001738F4 File Offset: 0x00171AF4
	public void ContinueDownLoad()
	{
		this.updateHelper.ContinueDownload();
	}

	// Token: 0x06004864 RID: 18532 RVA: 0x00173904 File Offset: 0x00171B04
	public void ReDownLoad()
	{
		this.SetProgressLineWidth(0);
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), !this.mIsVerifyDownload, true);
	}

	// Token: 0x06004865 RID: 18533 RVA: 0x0017393C File Offset: 0x00171B3C
	private void SetProgressLineVal(float val)
	{
		this.mProgressTargetWidth = (int)(val * (float)this.mProgressLineWidth);
		this.curProgress = val;
	}

	// Token: 0x06004866 RID: 18534 RVA: 0x00173958 File Offset: 0x00171B58
	private void CheckDownloadRes()
	{
		this.CheckVersionInfoLabel.text = string.Empty;
		this.SetProgressLineVal(0f);
		this.SetProgressLineWidth(0);
		if (this.updateHelper.NeedDownloadSize > 1048576L)
		{
			this.mNeedDownLoadSize = (float)this.updateHelper.NeedDownloadSize / 1024f / 1024f;
			this.mDownLoadUnit = "MB";
		}
		else
		{
			this.mNeedDownLoadSize = (float)this.updateHelper.NeedDownloadSize / 1024f;
			this.mDownLoadUnit = "KB";
		}
		if (this.isAutoDownload)
		{
			this.StartDownload();
		}
		else
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{102209}", new object[]
			{
				string.Format("{0:N1}{1}", this.mNeedDownLoadSize, this.mDownLoadUnit)
			}), StrDictionary.GetDictionaryString("#{102210}", new object[0]), new MessageBoxLogic.OnYesClick(this.StartDownload), delegate
			{
				Application.Quit();
			}, null, null);
		}
	}

	// Token: 0x06004867 RID: 18535 RVA: 0x00173A74 File Offset: 0x00171C74
	private void Update()
	{
		if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
		{
			this.SetProgressLineVal(this.updateHelper.GetDownloadProgress());
		}
		if (this.mLastUpdateStep == UPDATE_STEP.FINISH && this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS && DataManager.DataInitFinishFlag)
		{
			this.SetProgressLineVal(1f);
		}
		if (this.ProgressLinePic.width < this.mProgressTargetWidth)
		{
			int num = this.ProgressLinePic.width + 10;
			if (num > this.mProgressTargetWidth)
			{
				num = this.mProgressTargetWidth;
			}
			this.SetProgressLineWidth(num);
			if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
			{
				this.downloadSb.Length = 0;
				this.downloadSb.AppendFormat(this.downloadFormat, this.mNeedDownLoadSize * this.curProgress, this.mNeedDownLoadSize, this.mDownLoadUnit);
				this.CheckVersionInfoLabel.text = this.downloadSb.ToString();
			}
			if (this.ProgressLinePic.width >= this.mProgressLineWidth)
			{
				if (this.mLastUpdateStep == UPDATE_STEP.FINISH)
				{
					if (this.mIsVerifyDownload)
					{
						SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.AccountVersionCheckRootUI);
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LoginRootUI, delegate
						{
							SingletonUnity<LoginRootLogic>.Instance.Reset(this.mNoticeStr, this.mNoticeVersion);
						}, null);
					}
					else
					{
						WaitResponseUIRootLogic.OpenWaitBox(0, 10f, 0f, null);
						SingletonDontDestoryUnity<NetManager>.Instance.CharacterListRequest();
					}
				}
				else if (this.mLastUpdateStep == UPDATE_STEP.CHECK_IS_DOWNLOAD)
				{
					this.CheckDownloadRes();
				}
			}
		}
	}

	// Token: 0x06004868 RID: 18536 RVA: 0x00173C00 File Offset: 0x00171E00
	public void VerifyAccount()
	{
		WaitResponseUIRootLogic.OpenWaitBox(2, 0f, 0f, null);
		if (NetLogic.GetInstance().ConnectStatus == NetLogic.CONNECT_STATUS.CONNRCTED)
		{
			string playerAccountId = PlayerData.GetPlayerAccountId();
			if (string.IsNullOrEmpty(playerAccountId))
			{
				NetLogic.GetInstance().Send<Protocol.visitor>(null, new RpcRspHandler(this.VisitorResponse));
			}
			else
			{
				verfiy.request request = new verfiy.request();
				request.id = playerAccountId;
				request.key = PlayerData.GetPlayerAccountKey();
				this.curVerifyId = playerAccountId;
				this.curVerifyKey = request.key;
				request.versionCode = GameSettingData.GameVersion;
				NetLogic.GetInstance().Send<Protocol.verfiy>(request, new RpcRspHandler(this.OnVerifyResponse));
			}
		}
		else
		{
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, new NetLogic.ConnectDelegate(this.ConnectSuccess), new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
	}

	// Token: 0x06004869 RID: 18537 RVA: 0x00173CE4 File Offset: 0x00171EE4
	public void OnClickShowPasswordBtn()
	{
		if (this.PasswordInput.inputType == UIInput.InputType.Password)
		{
			this.PasswordInput.inputType = UIInput.InputType.Standard;
		}
		else if (this.PasswordInput.inputType == UIInput.InputType.Standard)
		{
			this.PasswordInput.inputType = UIInput.InputType.Password;
		}
		else
		{
			this.PasswordInput.inputType = UIInput.InputType.Password;
		}
		this.PasswordInput.UpdateLabel();
	}

	// Token: 0x0600486A RID: 18538 RVA: 0x00173D4C File Offset: 0x00171F4C
	public void OnClickVisitorBtn()
	{
		WaitResponseUIRootLogic.OpenWaitBox(2, 0f, 0f, null);
		if (NetLogic.GetInstance().ConnectStatus == NetLogic.CONNECT_STATUS.CONNRCTED)
		{
			string value = this.AccountInput.value;
			string value2 = this.PasswordInput.value;
			if (string.IsNullOrEmpty(value))
			{
				WaitResponseUIRootLogic.CloseBox();
				MessageBoxLogic.OpenOKBox("#{201024}", "#{100127}", null);
				return;
			}
			if (string.IsNullOrEmpty(value2))
			{
				WaitResponseUIRootLogic.CloseBox();
				MessageBoxLogic.OpenOKBox("#{201025}", "#{100127}", null);
				return;
			}
			verfiy.request request = new verfiy.request();
			request.id = value;
			request.key = value2;
			this.curVerifyId = value;
			this.curVerifyKey = value2;
			request.versionCode = GameSettingData.GameVersion;
			NetLogic.GetInstance().Send<Protocol.verfiy>(request, new RpcRspHandler(this.OnVerifyResponse));
		}
		else
		{
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, new NetLogic.ConnectDelegate(this.ClickVisitorConnectSuccess), new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
	}

	// Token: 0x0600486B RID: 18539 RVA: 0x00173E54 File Offset: 0x00172054
	private void ClickVisitorConnectSuccess(bool isSuccess)
	{
		WaitResponseUIRootLogic.CloseBox();
		if (isSuccess)
		{
			if (AccountVersionCheckRootLogic.FaceBookState != 0)
			{
				this.ReLoginAndBindFacebook();
				AccountVersionCheckRootLogic.FaceBookState = 0;
				return;
			}
			string value = this.AccountInput.value;
			string value2 = this.PasswordInput.value;
			if (string.IsNullOrEmpty(value))
			{
				MessageBoxLogic.OpenOKBox("#{201024}", "#{100127}", null);
				return;
			}
			if (string.IsNullOrEmpty(value2))
			{
				MessageBoxLogic.OpenOKBox("#{201025}", "#{100127}", null);
				return;
			}
			WaitResponseUIRootLogic.OpenWaitBox(3, 10f, 0f, null);
			verfiy.request request = new verfiy.request();
			request.id = value;
			request.key = value2;
			this.curVerifyId = value;
			this.curVerifyKey = value2;
			request.versionCode = GameSettingData.GameVersion;
			NetLogic.GetInstance().Send<Protocol.verfiy>(request, new RpcRspHandler(this.OnVerifyResponse));
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

	// Token: 0x0600486C RID: 18540 RVA: 0x00173F58 File Offset: 0x00172158
	private void ConnectLost()
	{
		WaitResponseUIRootLogic.CloseBox();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TopMessageBoxUI, delegate
		{
			SingletonUnity<TopMessageBoxLogic>.Instance.OpenOKCancelBox("#{100142}", "#{100127}", new DelegateDefine.NoParamDelegate(this.OnYesClick), new DelegateDefine.NoParamDelegate(this.OnCancleClick), "#{100143}", "#{100144}");
		}, null);
	}

	// Token: 0x0600486D RID: 18541 RVA: 0x00173F7C File Offset: 0x0017217C
	private void ConnectSuccess(bool isSuccess)
	{
		WaitResponseUIRootLogic.CloseBox();
		if (isSuccess)
		{
			this.StartVerifyAccount();
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

	// Token: 0x0600486E RID: 18542 RVA: 0x00173FC8 File Offset: 0x001721C8
	private void OnCancleClick()
	{
		Application.Quit();
	}

	// Token: 0x0600486F RID: 18543 RVA: 0x00173FD0 File Offset: 0x001721D0
	private void OnYesClick()
	{
		WaitResponseUIRootLogic.OpenWaitBox(4, 0f, 0f, null);
		SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, new NetLogic.ConnectDelegate(this.ConnectSuccess), new NetLogic.ConnectLostDelegate(this.ConnectLost));
	}

	// Token: 0x06004870 RID: 18544 RVA: 0x00174024 File Offset: 0x00172224
	private void VisitorResponse(SprotoTypeBase rep)
	{
		visitor.response response = rep as visitor.response;
		if (response != null && response.state == 0L)
		{
			PlayerData.SavePlayerAccountId(response.id);
			PlayerData.SavePlayerAccountKey(response.key);
			this.StartVerifyAccount();
		}
		else
		{
			WaitResponseUIRootLogic.CloseBox();
			NetLogic.GetInstance().DisconnectServer();
		}
	}

	// Token: 0x06004871 RID: 18545 RVA: 0x0017407C File Offset: 0x0017227C
	public void OnClickFaceBookBtn()
	{
		if (PlayerData.FaceBookBind < 0L)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountBindUIRoot, null, null);
		}
		else if (PlayerData.FaceBookBind == 100L)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SignOutFacebook();
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SignOutGoogle();
		}
	}

	// Token: 0x06004872 RID: 18546 RVA: 0x001740D4 File Offset: 0x001722D4
	public void SignInCheck(string id, string token, int type = 0)
	{
		WaitResponseUIRootLogic.OpenWaitBox(5, -1f, 0f, null);
		if (PlayerData.FaceBookBind > -1L)
		{
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, delegate(bool success)
			{
				this.FaceBookUnlink(id, token, type);
			}, new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
		else
		{
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, delegate(bool success)
			{
				this.FaceBookSignInSuccess(id, token, type);
			}, new NetLogic.ConnectLostDelegate(this.ConnectLost));
		}
	}

	// Token: 0x06004873 RID: 18547 RVA: 0x00174194 File Offset: 0x00172394
	private void UseLocalAccount()
	{
		string faceBookId = PlayerData.FaceBookId;
		string facekBookToken = PlayerData.FacekBookToken;
		string playerAccountId = PlayerData.GetPlayerAccountId();
		string playerAccountKey = PlayerData.GetPlayerAccountKey();
		facebook_link.request request = new facebook_link.request();
		request.facebook_id = faceBookId;
		request.facebook_token = facekBookToken;
		request.id = playerAccountId;
		request.key = playerAccountKey;
		request.bindType = this.cutBindType;
		request.confirm = 1L;
		WaitResponseUIRootLogic.OpenWaitBox(5, 10f, 0f, null);
		NetLogic.GetInstance().Send<Protocol.facebook_link>(request, delegate(SprotoTypeBase rpc)
		{
			WaitResponseUIRootLogic.CloseBox();
			facebook_link.response response = rpc as facebook_link.response;
			if (response.state == 0L)
			{
				PlayerData.SavePlayerAccountId(response.id);
				PlayerData.SavePlayerAccountKey(response.key);
				this.StartVerifyAccount();
			}
			else
			{
				NoticeLogic.AddNotifyData("#{200093}", true, false);
			}
		});
	}

	// Token: 0x06004874 RID: 18548 RVA: 0x00174220 File Offset: 0x00172420
	private void FacebookLinkResponse(SprotoTypeBase rpcRsp)
	{
		WaitResponseUIRootLogic.CloseBox();
		facebook_link.response response = rpcRsp as facebook_link.response;
		if (response.state == 0L)
		{
			if (response.bindType == 0L)
			{
				if (PlayerData.FaceBookBind < 0L)
				{
					PlayerData.FaceBookBind = 100L;
				}
				else if (PlayerData.FaceBookBind == 200L)
				{
					PlayerData.FaceBookBind = 300L;
				}
			}
			else if (PlayerData.FaceBookBind < 0L)
			{
				PlayerData.FaceBookBind = 200L;
			}
			else if (PlayerData.FaceBookBind == 100L)
			{
				PlayerData.FaceBookBind = 300L;
			}
			PlayerData.SavePlayerAccountId(response.id);
			PlayerData.SavePlayerAccountKey(response.key);
			this.StartVerifyAccount();
		}
		else if (response.state == 1L)
		{
			if (response.bindType == 0L)
			{
				if (PlayerData.FaceBookBind < 0L)
				{
					PlayerData.FaceBookBind = 100L;
				}
				else if (PlayerData.FaceBookBind == 200L)
				{
					PlayerData.FaceBookBind = 300L;
				}
			}
			else if (PlayerData.FaceBookBind < 0L)
			{
				PlayerData.FaceBookBind = 200L;
			}
			else if (PlayerData.FaceBookBind == 100L)
			{
				PlayerData.FaceBookBind = 300L;
			}
			string serverId = response.id;
			string serverKey = response.key;
			this.cutBindType = response.bindType;
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{200068}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), new MessageBoxLogic.OnYesClick(this.UseLocalAccount), delegate
			{
				MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{200069}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					PlayerData.SavePlayerAccountId(serverId);
					PlayerData.SavePlayerAccountKey(serverKey);
					this.StartVerifyAccount();
				}, null, null, null);
			}, null, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200093}", true, false);
		}
	}

	// Token: 0x06004875 RID: 18549 RVA: 0x001743E4 File Offset: 0x001725E4
	private void FacebookUnLinkResponse(SprotoTypeBase rpcRsp)
	{
		facebook_unlink.response response = rpcRsp as facebook_unlink.response;
		WaitResponseUIRootLogic.CloseBox();
		if (response != null)
		{
			if (response.state == 0L)
			{
				if (response.bindType == 0L)
				{
					if (PlayerData.FaceBookBind == 100L)
					{
						PlayerData.FaceBookBind = -1L;
					}
					else if (PlayerData.FaceBookBind == 300L)
					{
						PlayerData.FaceBookBind = 200L;
					}
				}
				else if (PlayerData.FaceBookBind == 200L)
				{
					PlayerData.FaceBookBind = -1L;
				}
				else if (PlayerData.FaceBookBind == 300L)
				{
					PlayerData.FaceBookBind = 100L;
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{200094}", true, false);
			}
			this.RefreshAccount();
		}
	}

	// Token: 0x06004876 RID: 18550 RVA: 0x001744A0 File Offset: 0x001726A0
	public void FaceBookUnlink(string facebookId, string facebookToken, int type = 0)
	{
		string playerAccountId = PlayerData.GetPlayerAccountId();
		string playerAccountKey = PlayerData.GetPlayerAccountKey();
		facebook_unlink.request request = new facebook_unlink.request();
		request.id = playerAccountId;
		request.key = playerAccountKey;
		request.bindType = (long)type;
		WaitResponseUIRootLogic.OpenWaitBox(6, 10f, 0f, null);
		NetLogic.GetInstance().Send<Protocol.facebook_unlink>(request, new RpcRspHandler(this.FacebookUnLinkResponse));
	}

	// Token: 0x06004877 RID: 18551 RVA: 0x00174500 File Offset: 0x00172700
	public void FaceBookSignInSuccess(string facebookId, string facebookToken, int type = 0)
	{
		string playerAccountId = PlayerData.GetPlayerAccountId();
		string playerAccountKey = PlayerData.GetPlayerAccountKey();
		facebook_link.request request = new facebook_link.request();
		request.facebook_id = facebookId;
		request.facebook_token = facebookToken;
		request.bindType = (long)type;
		PlayerData.FaceBookId = facebookId;
		PlayerData.FacekBookToken = facebookToken;
		if (!string.IsNullOrEmpty(playerAccountId))
		{
			request.id = playerAccountId;
		}
		if (!string.IsNullOrEmpty(playerAccountKey))
		{
			request.key = playerAccountKey;
		}
		WaitResponseUIRootLogic.OpenWaitBox(5, 10f, 0f, null);
		NetLogic.GetInstance().Send<Protocol.facebook_link>(request, new RpcRspHandler(this.FacebookLinkResponse));
	}

	// Token: 0x040035AB RID: 13739
	public GameObject CheckAccountRoot;

	// Token: 0x040035AC RID: 13740
	public GameObject CheckVersionRoot;

	// Token: 0x040035AD RID: 13741
	public UISprite ProgressLinePic;

	// Token: 0x040035AE RID: 13742
	private int mProgressLineWidth = -1;

	// Token: 0x040035AF RID: 13743
	private int mProgressTargetWidth;

	// Token: 0x040035B0 RID: 13744
	public UILabel CheckVersionInfoLabel;

	// Token: 0x040035B1 RID: 13745
	public UILabel ConnectLabel;

	// Token: 0x040035B2 RID: 13746
	private string mCurLocalVersion = string.Empty;

	// Token: 0x040035B3 RID: 13747
	private string mCurApkVersion = string.Empty;

	// Token: 0x040035B4 RID: 13748
	private FileUpdateHelper updateHelper;

	// Token: 0x040035B5 RID: 13749
	private UPDATE_STEP mLastUpdateStep;

	// Token: 0x040035B6 RID: 13750
	public UILabel FaceBookLabel;

	// Token: 0x040035B7 RID: 13751
	public UISprite BindSpriteIcon;

	// Token: 0x040035B8 RID: 13752
	public static int FaceBookState;

	// Token: 0x040035B9 RID: 13753
	public GameObject FackbookBtn;

	// Token: 0x040035BA RID: 13754
	public GameObject VisitorBtn;

	// Token: 0x040035BB RID: 13755
	public GameObject LineAnimaObj;

	// Token: 0x040035BC RID: 13756
	private int curAutoReDownloadTimes;

	// Token: 0x040035BD RID: 13757
	public UIInput AccountInput;

	// Token: 0x040035BE RID: 13758
	public UIInput PasswordInput;

	// Token: 0x040035BF RID: 13759
	private string mNoticeStr;

	// Token: 0x040035C0 RID: 13760
	private string mNoticeVersion;

	// Token: 0x040035C1 RID: 13761
	private bool mIsVerifyDownload;

	// Token: 0x040035C2 RID: 13762
	private bool isAutoDownload;

	// Token: 0x040035C3 RID: 13763
	private float mNeedDownLoadSize;

	// Token: 0x040035C4 RID: 13764
	private string mDownLoadUnit;

	// Token: 0x040035C5 RID: 13765
	private bool isDownloadData;

	// Token: 0x040035C6 RID: 13766
	private float curProgress;

	// Token: 0x040035C7 RID: 13767
	private StringBuilder downloadSb = new StringBuilder(512);

	// Token: 0x040035C8 RID: 13768
	private string downloadFormat = "{0:N1}/{1:N1}{2}";

	// Token: 0x040035C9 RID: 13769
	private int ChristmasVersionMin = 132;

	// Token: 0x040035CA RID: 13770
	private int ChristmasVersionMax = 140;

	// Token: 0x040035CB RID: 13771
	private string curVerifyId = string.Empty;

	// Token: 0x040035CC RID: 13772
	private string curVerifyKey = string.Empty;

	// Token: 0x040035CD RID: 13773
	private long cutBindType;
}
