using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000905 RID: 2309
public class DownLoadResRootLogic : SingletonUnity<DownLoadResRootLogic>
{
	// Token: 0x06003F21 RID: 16161 RVA: 0x00124864 File Offset: 0x00122A64
	public void OnClickDownLoadBtn()
	{
		this.StartDownload();
		NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
		NGUITools.SetActive(this.ProgressPercentLabel.gameObject, true);
		if (this.mIsFullDownload)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.IsNeedCountDownload)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "RealDownloadStart");
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "StartTimes");
				LocalDataSaveManager.SetDownloadCountFinish();
			}
			this.startDownloadTime = Time.realtimeSinceStartup;
		}
		else if (SingletonUnity<DownloadResTipRootLogic>.Exists)
		{
			this.startDownloadTime = SingletonUnity<DownloadResTipRootLogic>.Instance.StartDownloadTime;
		}
	}

	// Token: 0x06003F22 RID: 16162 RVA: 0x00124914 File Offset: 0x00122B14
	private new void Awake()
	{
		base.Awake();
		this.curAutoReDownloadTimes = 0;
		this.DownLoadFinishFlag = false;
		this.mIsAutoDownload = false;
		this.ProgressLinePic.transform.localPosition = new Vector3((float)(-(float)this.ProgressLinePic.width / 2), 0f, 0f);
		this.mProgressLineWidth = this.ProgressLinePic.width;
		this.SetProgressLineWidth(0);
		this.isNeedAddFunc = false;
		if (SingletonUnity<DownloadTipRootLogic>.Exists && SingletonUnity<DownloadTipRootLogic>.Instance.UpdateHelper != null)
		{
			this.updateHelper = SingletonUnity<DownloadTipRootLogic>.Instance.UpdateHelper;
			this.isNeedAddFunc = true;
		}
		else
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "FileUpdateHelper";
			UnityVersionUtil.SetActiveRecursive(gameObject, true);
			this.updateHelper = gameObject.AddComponent<FileUpdateHelper>();
			if (SingletonUnity<DownloadTipRootLogic>.Exists)
			{
				SingletonUnity<DownloadTipRootLogic>.Instance.UpdateHelper = this.updateHelper;
			}
		}
		DownloadRewardData downloadRewardDataBuyId = DataManager.GetDownloadRewardDataBuyId("1");
		List<string> list = new List<string>();
		list.Add(downloadRewardDataBuyId.ItemID1);
		list.Add(downloadRewardDataBuyId.ItemID2);
		list.Add(downloadRewardDataBuyId.ItemID3);
		list.Add(downloadRewardDataBuyId.ItemID4);
		List<int> list2 = new List<int>();
		list2.Add(downloadRewardDataBuyId.ItemCount1);
		list2.Add(downloadRewardDataBuyId.ItemCount2);
		list2.Add(downloadRewardDataBuyId.ItemCount3);
		list2.Add(downloadRewardDataBuyId.ItemCount4);
		List<int> list3 = new List<int>();
		list3.Add(downloadRewardDataBuyId.Quality1);
		list3.Add(downloadRewardDataBuyId.Quality2);
		list3.Add(downloadRewardDataBuyId.Quality3);
		list3.Add(downloadRewardDataBuyId.Quality4);
		this.RewardItems.ShowRewards(list, list3, list2);
		this.DownloadBtnLabel.text = StrDictionary.GetDictionaryString("#{102212}", new object[0]);
		this.TextLabel.pivot = UIWidget.Pivot.TopLeft;
		this.TextLabel.text = StrDictionary.GetDictionaryString("#{201013}", new object[0]);
		this.TipTextLabel.enabled = true;
		this.TipTextLabel.text = StrDictionary.GetDictionaryString("201014", new object[0]);
		NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
		NGUITools.SetActive(this.DownloadFinishBtnRoot.gameObject, false);
		NGUITools.SetActive(this.ProgressPercentLabel.gameObject, true);
		this.ProgressPercentLabel.text = "2";
		PlayerData.downLoadFlag = 1;
		UPDATE_STEP curUpdateStape = this.updateHelper.CurUpdateStape;
		UPDATE_RESULT curUpdateResult = this.updateHelper.CurUpdateResult;
		if (curUpdateStape == UPDATE_STEP.INVALID)
		{
			this.mIsFullDownload = true;
			this.BeginDownload();
		}
		else
		{
			this.mIsFullDownload = false;
			if (curUpdateResult != UPDATE_RESULT.INVALID)
			{
				if (curUpdateResult == UPDATE_RESULT.SUCCESS)
				{
					if (!this.updateHelper.IsNeedCopyRes)
					{
						this.updateHelper.IsNeedCopyRes = true;
						this.BeginDownload();
					}
					else
					{
						this.ResetDownloadSuccess();
					}
				}
				else
				{
					this.BeginDownload();
				}
			}
			else
			{
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
				if (curUpdateStape == UPDATE_STEP.CHECK_IS_DOWNLOAD)
				{
					this.mIsFullDownload = true;
				}
				this.OnChangeUpdateStep(curUpdateStape, true);
			}
		}
		this.updateHelper.IsNeedCopyRes = true;
	}

	// Token: 0x06003F23 RID: 16163 RVA: 0x00124C88 File Offset: 0x00122E88
	private void SetProgressLineWidth(int width)
	{
		this.ProgressLinePic.width = width;
		this.ProgressLineAnimaObj.transform.localPosition = new Vector3((float)width, 0f, 0f);
	}

	// Token: 0x06003F24 RID: 16164 RVA: 0x00124CC4 File Offset: 0x00122EC4
	private void BeginDownload()
	{
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), true, true);
		if (SingletonUnity<DownloadTipRootLogic>.Exists)
		{
			FileUpdateHelper fileUpdateHelper = this.updateHelper;
			fileUpdateHelper.OnChangeUpdateStep = (FileUpdateHelper.OnChangeUpdateStepDelegate)Delegate.Combine(fileUpdateHelper.OnChangeUpdateStep, new FileUpdateHelper.OnChangeUpdateStepDelegate(SingletonUnity<DownloadTipRootLogic>.Instance.OnChangeUpdateStep));
		}
	}

	// Token: 0x06003F25 RID: 16165 RVA: 0x00124D2C File Offset: 0x00122F2C
	public void OnChangeUpdateStep(UPDATE_STEP newStep)
	{
		if (base.gameObject == null || !UnityVersionUtil.IsActive(base.gameObject))
		{
			return;
		}
		this.OnChangeUpdateStep(newStep, false);
	}

	// Token: 0x06003F26 RID: 16166 RVA: 0x00124D64 File Offset: 0x00122F64
	public void OnChangeUpdateStep(UPDATE_STEP newStep, bool isForce)
	{
		switch (newStep)
		{
		case UPDATE_STEP.CHECK_VERSION:
			this.SetProgressLineVal(0.3f, isForce);
			this.ProgressInfoLabel.text = StrDictionary.GetDictionaryString("#{102203}", new object[0]);
			break;
		case UPDATE_STEP.GET_FILELIST:
			this.SetProgressLineVal(0.6f, isForce);
			this.ProgressInfoLabel.text = StrDictionary.GetDictionaryString("#{102204}", new object[0]);
			break;
		case UPDATE_STEP.COMPARE_RES:
			this.SetProgressLineVal(1f, isForce);
			this.ProgressInfoLabel.text = StrDictionary.GetDictionaryString("#{102205}", new object[0]);
			break;
		case UPDATE_STEP.CHECK_IS_DOWNLOAD:
			if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
			{
				return;
			}
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
			this.CheckDownloadRes();
			break;
		case UPDATE_STEP.DOWNLOAD_RES:
			NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
			if (isForce)
			{
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
				this.SetProgressLineVal(this.updateHelper.GetDownloadProgress(), isForce);
				this.downloadSb.Length = 0;
				this.downloadSb.AppendFormat(this.downloadFormat, this.mNeedDownLoadSize * this.curProgress, this.mNeedDownLoadSize, this.mDownLoadUnit);
				this.ProgressInfoLabel.text = this.downloadSb.ToString();
			}
			break;
		case UPDATE_STEP.CHECK_RES:
			NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
			this.SetProgressLineWidth(0);
			this.SetProgressLineVal(0.3f, isForce);
			this.ProgressInfoLabel.text = StrDictionary.GetDictionaryString("#{102207}", new object[0]);
			break;
		case UPDATE_STEP.COPY_RES:
			NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
			this.SetProgressLineVal(0.6f, isForce);
			break;
		case UPDATE_STEP.CLEAR_CACHE:
			NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
			this.SetProgressLineVal(0.93f, isForce);
			SingletonDontDestoryUnity<GameManager>.Instance.ReImportData();
			SingletonDontDestoryUnity<GameManager>.Instance.ReImportAnima();
			break;
		case UPDATE_STEP.FINISH:
			NGUITools.SetActive(this.DownloadBtnRoot.gameObject, false);
			if (this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS)
			{
				this.SetProgressLineVal(0.95f, isForce);
			}
			else
			{
				this.SetProgressLineVal(0.3f, isForce);
				if (this.curAutoReDownloadTimes < GameDefine.AutoReDownloadTimes)
				{
					this.curAutoReDownloadTimes++;
					this.mIsAutoDownload = true;
					this.ReDownLoad();
				}
				else
				{
					this.mIsAutoDownload = false;
					MessageBoxLogic.OpenOKCancelBox("#{100156}", "#{100127}", new MessageBoxLogic.OnYesClick(this.ReDownLoad), new MessageBoxLogic.OnCancelClick(this.OnClickExitGame), null, "#{100149}");
				}
			}
			break;
		default:
			Debug.Log("CURSTATE :: " + newStep);
			break;
		}
		this.mLastUpdateStep = newStep;
	}

	// Token: 0x06003F27 RID: 16167 RVA: 0x001250F8 File Offset: 0x001232F8
	private void DownloadFlurryCount(float downloadTime)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.IsNeedCountDownloadTime)
		{
			return;
		}
		if (downloadTime < 60f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "1Min");
		}
		else if (downloadTime < 120f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "2Min");
		}
		else if (downloadTime < 180f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "3Min");
		}
		else if (downloadTime < 240f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "4Min");
		}
		else if (downloadTime < 300f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "5Min");
		}
		else if (downloadTime < 420f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "7Min");
		}
		else if (downloadTime < 540f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "9Min");
		}
		else if (downloadTime < 660f)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "11Min");
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewFullDownloadTime", "11+Min");
		}
	}

	// Token: 0x06003F28 RID: 16168 RVA: 0x00125278 File Offset: 0x00123478
	private void CheckDownloadRes()
	{
		this.ProgressInfoLabel.text = string.Empty;
		this.SetProgressLineVal(0f, false);
		this.ProgressPercentLabel.text = "0";
		this.SetProgressLineWidth(0);
		if (SingletonDontDestoryUnity<GameManager>.Instance.IsAutoDownload)
		{
			this.OnClickDownLoadBtn();
		}
		else if (this.mIsAutoDownload)
		{
			this.OnClickDownLoadBtn();
		}
		else
		{
			NGUITools.SetActive(this.DownloadBtnRoot.gameObject, true);
			NGUITools.SetActive(this.ProgressPercentLabel.gameObject, false);
			this.DownloadBtnSizeLabel.text = string.Format("{0:N1}{1}", this.mNeedDownLoadSize, this.mDownLoadUnit);
		}
	}

	// Token: 0x06003F29 RID: 16169 RVA: 0x00125330 File Offset: 0x00123530
	private void OnClickExitGame()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		MessageBoxLogic.CloseBox();
		Application.Quit();
	}

	// Token: 0x06003F2A RID: 16170 RVA: 0x00125348 File Offset: 0x00123548
	public void StartDownload()
	{
		this.ProgressInfoLabel.text = string.Format("{0:N1}/{1:N1}{2}", 0.0, this.mNeedDownLoadSize, this.mDownLoadUnit);
		this.updateHelper.DownloadFileList();
	}

	// Token: 0x06003F2B RID: 16171 RVA: 0x00125394 File Offset: 0x00123594
	public void ContinueDownLoad()
	{
		this.updateHelper.ContinueDownload();
	}

	// Token: 0x06003F2C RID: 16172 RVA: 0x001253A4 File Offset: 0x001235A4
	public void ReDownLoad()
	{
		this.SetProgressLineWidth(0);
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), true, true);
		if (SingletonUnity<DownloadTipRootLogic>.Exists)
		{
			FileUpdateHelper fileUpdateHelper = this.updateHelper;
			fileUpdateHelper.OnChangeUpdateStep = (FileUpdateHelper.OnChangeUpdateStepDelegate)Delegate.Combine(fileUpdateHelper.OnChangeUpdateStep, new FileUpdateHelper.OnChangeUpdateStepDelegate(SingletonUnity<DownloadTipRootLogic>.Instance.OnChangeUpdateStep));
		}
	}

	// Token: 0x06003F2D RID: 16173 RVA: 0x00125414 File Offset: 0x00123614
	private void SetProgressLineVal(float val, bool isForce = false)
	{
		this.mProgressTargetWidth = (int)(val * (float)this.mProgressLineWidth);
		this.curProgress = val;
		if (isForce)
		{
			this.SetProgressLineWidth(this.mProgressTargetWidth);
			this.percentSb.AppendFormat(this.percentFormat, (int)((float)this.mProgressTargetWidth / (float)this.mProgressLineWidth * 100f));
			this.ProgressPercentLabel.text = this.percentSb.ToString();
		}
	}

	// Token: 0x06003F2E RID: 16174 RVA: 0x00125490 File Offset: 0x00123690
	public void OnClickTurnPageBtn()
	{
	}

	// Token: 0x06003F2F RID: 16175 RVA: 0x00125494 File Offset: 0x00123694
	private void TurnPage()
	{
	}

	// Token: 0x06003F30 RID: 16176 RVA: 0x00125498 File Offset: 0x00123698
	private void Update()
	{
		if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
		{
			this.SetProgressLineVal(this.updateHelper.GetDownloadProgress(), false);
		}
		if (this.mLastUpdateStep == UPDATE_STEP.FINISH && this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS && DataManager.DataInitFinishFlag)
		{
			this.SetProgressLineVal(1f, false);
		}
		if (this.ProgressLinePic.width < this.mProgressTargetWidth)
		{
			int num = this.ProgressLinePic.width + 10;
			if (num > this.mProgressTargetWidth)
			{
				num = this.mProgressTargetWidth;
			}
			this.SetProgressLineWidth(num);
			this.percentSb.Length = 0;
			if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
			{
				this.percentSb.AppendFormat(this.percentFormat, (int)((float)num / (float)this.mProgressLineWidth * 100f));
			}
			else
			{
				this.percentSb.AppendFormat(this.percentFormat, Mathf.Clamp((int)((float)num / (float)this.mProgressLineWidth * 100f), 2, 100));
			}
			this.ProgressPercentLabel.text = this.percentSb.ToString();
			if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
			{
				this.downloadSb.Length = 0;
				this.downloadSb.AppendFormat(this.downloadFormat, this.mNeedDownLoadSize * this.curProgress, this.mNeedDownLoadSize, this.mDownLoadUnit);
				this.ProgressInfoLabel.text = this.downloadSb.ToString();
			}
			if (this.ProgressLinePic.width >= this.mProgressLineWidth)
			{
				if (this.mLastUpdateStep == UPDATE_STEP.FINISH)
				{
					this.DownLoadFinishFlag = true;
					this.ResetDownloadSuccess();
					this.timeCount = Time.time;
				}
				else if (this.mLastUpdateStep == UPDATE_STEP.CHECK_IS_DOWNLOAD)
				{
					this.CheckDownloadRes();
				}
			}
		}
	}

	// Token: 0x06003F31 RID: 16177 RVA: 0x00125674 File Offset: 0x00123874
	public void ResetDownloadSuccess()
	{
		this.TipTextLabel.enabled = false;
		this.TextLabel.pivot = UIWidget.Pivot.Center;
		this.TextLabel.text = StrDictionary.GetDictionaryString("#{201016}", new object[0]);
		this.DownloadBtnLabel.text = StrDictionary.GetDictionaryString("#{201017}", new object[0]);
		NGUITools.SetActive(this.DownloadFinishBtnRoot, true);
		NGUITools.SetActive(this.ProgressRoot, true);
		this.ProgressPercentLabel.text = "100";
		NGUITools.SetActive(this.DownloadInfoLabelRoot, false);
		this.ProgressLinePic.width = this.mProgressLineWidth;
		NGUITools.SetActive(this.ProgressLineAnimaObj, false);
		NGUITools.SetActive(this.DownloadBtnRoot, false);
	}

	// Token: 0x06003F32 RID: 16178 RVA: 0x0012572C File Offset: 0x0012392C
	public void OnClickDownloadFinishBtn()
	{
		WaitResponseUIRootLogic.OpenWaitBox(270, 10f, 0f, null);
		NetLogic.GetInstance().Send<Protocol.download_finish>(null, null);
	}

	// Token: 0x06003F33 RID: 16179 RVA: 0x00125750 File Offset: 0x00123950
	public void OnPressCarModelPic(GameObject btn, bool ispress)
	{
		if (ispress)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.StopRotate();
		}
		else
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.PlayRotate();
		}
	}

	// Token: 0x06003F34 RID: 16180 RVA: 0x00125774 File Offset: 0x00123974
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DownLoadResRoot);
	}

	// Token: 0x06003F35 RID: 16181 RVA: 0x00125788 File Offset: 0x00123988
	private void OnEnable()
	{
		if (this.isNeedAddFunc)
		{
			FileUpdateHelper fileUpdateHelper = this.updateHelper;
			fileUpdateHelper.OnChangeUpdateStep = (FileUpdateHelper.OnChangeUpdateStepDelegate)Delegate.Combine(fileUpdateHelper.OnChangeUpdateStep, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep));
		}
	}

	// Token: 0x06003F36 RID: 16182 RVA: 0x001257C8 File Offset: 0x001239C8
	private void OnDisable()
	{
		this.updateHelper.OnChangeUpdateStep = null;
		if (SingletonUnity<DownloadTipRootLogic>.Exists)
		{
			FileUpdateHelper fileUpdateHelper = this.updateHelper;
			fileUpdateHelper.OnChangeUpdateStep = (FileUpdateHelper.OnChangeUpdateStepDelegate)Delegate.Combine(fileUpdateHelper.OnChangeUpdateStep, new FileUpdateHelper.OnChangeUpdateStepDelegate(SingletonUnity<DownloadTipRootLogic>.Instance.OnChangeUpdateStep));
		}
	}

	// Token: 0x04002AD9 RID: 10969
	public UILabel TitleLabel;

	// Token: 0x04002ADA RID: 10970
	public UILabel TextLabel;

	// Token: 0x04002ADB RID: 10971
	public UILabel TipTextLabel;

	// Token: 0x04002ADC RID: 10972
	public ShowRewardItems RewardItems;

	// Token: 0x04002ADD RID: 10973
	public UIButtonColor BtnColor;

	// Token: 0x04002ADE RID: 10974
	public UILabel DownloadBtnLabel;

	// Token: 0x04002ADF RID: 10975
	public UILabel DownloadBtnSizeLabel;

	// Token: 0x04002AE0 RID: 10976
	public UIEventListener RotateModelBtnListener;

	// Token: 0x04002AE1 RID: 10977
	public UILabel ProgressPercentLabel;

	// Token: 0x04002AE2 RID: 10978
	public GameObject ProgressLineAnimaObj;

	// Token: 0x04002AE3 RID: 10979
	private float startDownloadTime;

	// Token: 0x04002AE4 RID: 10980
	private int curAutoReDownloadTimes;

	// Token: 0x04002AE5 RID: 10981
	private bool mIsAutoDownload;

	// Token: 0x04002AE6 RID: 10982
	private bool mIsFullDownload;

	// Token: 0x04002AE7 RID: 10983
	public GameObject DownloadBtnRoot;

	// Token: 0x04002AE8 RID: 10984
	public GameObject DownloadFinishBtnRoot;

	// Token: 0x04002AE9 RID: 10985
	public GameObject ProgressRoot;

	// Token: 0x04002AEA RID: 10986
	public GameObject CloseBtnRoot;

	// Token: 0x04002AEB RID: 10987
	public GameObject DownloadInfoLabelRoot;

	// Token: 0x04002AEC RID: 10988
	private bool isNeedAddFunc;

	// Token: 0x04002AED RID: 10989
	public UISprite ProgressLinePic;

	// Token: 0x04002AEE RID: 10990
	private int mProgressLineWidth = -1;

	// Token: 0x04002AEF RID: 10991
	private int mProgressTargetWidth;

	// Token: 0x04002AF0 RID: 10992
	public UILabel ProgressInfoLabel;

	// Token: 0x04002AF1 RID: 10993
	private string mCurLocalVersion = string.Empty;

	// Token: 0x04002AF2 RID: 10994
	private string mCurApkVersion = string.Empty;

	// Token: 0x04002AF3 RID: 10995
	private FileUpdateHelper updateHelper;

	// Token: 0x04002AF4 RID: 10996
	private UPDATE_STEP mLastUpdateStep;

	// Token: 0x04002AF5 RID: 10997
	private float mNeedDownLoadSize;

	// Token: 0x04002AF6 RID: 10998
	private string mDownLoadUnit;

	// Token: 0x04002AF7 RID: 10999
	private float curProgress;

	// Token: 0x04002AF8 RID: 11000
	private string[] TitleLocIdList = new string[]
	{
		"#{101511}",
		"#{101512}",
		"#{101516}",
		"#{101518}",
		"#{101519}"
	};

	// Token: 0x04002AF9 RID: 11001
	private string[] TextLocIdList = new string[]
	{
		"#{101601}",
		"#{101603}",
		"#{101613}",
		"#{101615}",
		"#{101617}"
	};

	// Token: 0x04002AFA RID: 11002
	private float TurnPageTime = 8f;

	// Token: 0x04002AFB RID: 11003
	private int mCurIndex;

	// Token: 0x04002AFC RID: 11004
	private float mLastTurnPageTime;

	// Token: 0x04002AFD RID: 11005
	private StringBuilder percentSb = new StringBuilder(512);

	// Token: 0x04002AFE RID: 11006
	private string percentFormat = "{0}";

	// Token: 0x04002AFF RID: 11007
	private StringBuilder downloadSb = new StringBuilder(512);

	// Token: 0x04002B00 RID: 11008
	private string downloadFormat = "{0:N1}/{1:N1}{2}";

	// Token: 0x04002B01 RID: 11009
	public bool DownLoadFinishFlag;

	// Token: 0x04002B02 RID: 11010
	private float timeCount;
}
