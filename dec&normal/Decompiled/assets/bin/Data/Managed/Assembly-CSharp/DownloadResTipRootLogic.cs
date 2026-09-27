using System;
using System.Text;
using UnityEngine;

// Token: 0x02000906 RID: 2310
public class DownloadResTipRootLogic : SingletonUnity<DownloadResTipRootLogic>
{
	// Token: 0x17000F8A RID: 3978
	// (get) Token: 0x06003F38 RID: 16184 RVA: 0x0012593C File Offset: 0x00123B3C
	public float StartDownloadTime
	{
		get
		{
			return this.startDownloadTime;
		}
	}

	// Token: 0x17000F8B RID: 3979
	// (get) Token: 0x06003F39 RID: 16185 RVA: 0x00125944 File Offset: 0x00123B44
	public float DownloadProgress
	{
		get
		{
			return this.downloadProgress;
		}
	}

	// Token: 0x06003F3A RID: 16186 RVA: 0x0012594C File Offset: 0x00123B4C
	public void UpdateTutorialStep(int step, float percent)
	{
		if (step > 0 && step < this.statePercent.Length)
		{
			this.SetProgressLineVal(percent * (this.statePercent[step] - this.statePercent[step - 1]) + this.statePercent[step - 1]);
			if (this.mTutorialStep != step)
			{
				if (step >= this.targetWords.Length)
				{
					NGUITools.SetActive(this.StepTargetLabel.gameObject, false);
				}
				else
				{
					NGUITools.SetActive(this.StepTargetLabel.gameObject, true);
					this.StepTargetLabel.text = StrDictionary.GetDictionaryString(this.targetWords[step], new object[0]);
				}
			}
		}
		else
		{
			this.SetProgressLineVal(percent * this.statePercent[step]);
			if (this.mTutorialStep != step)
			{
				this.StepTargetLabel.text = StrDictionary.GetDictionaryString(this.targetWords[step], new object[0]);
			}
		}
		if (this.mTutorialStep < step)
		{
			for (int i = 0; i < this.StarPic.Length; i++)
			{
				if (i + 1 <= step)
				{
					NGUITools.SetActive(this.StarPic[i], true);
				}
				else
				{
					NGUITools.SetActive(this.StarPic[i], false);
				}
			}
		}
		this.mTutorialStep = step;
	}

	// Token: 0x06003F3B RID: 16187 RVA: 0x00125A88 File Offset: 0x00123C88
	public void UpdateTutorialStep(float percent)
	{
		this.SetProgressLineVal(percent);
		for (int i = 0; i < this.statePercent.Length - 1; i++)
		{
			if (percent + 1E-45f > this.statePercent[i])
			{
				if (!UnityVersionUtil.IsActive(this.StarPic[i]))
				{
					NGUITools.SetActive(this.StarPic[i], true);
				}
			}
			else if (UnityVersionUtil.IsActive(this.StarPic[i]))
			{
				NGUITools.SetActive(this.StarPic[i], false);
			}
		}
	}

	// Token: 0x06003F3C RID: 16188 RVA: 0x00125B10 File Offset: 0x00123D10
	public void OnClickDownLoadBtn()
	{
		this.StartDownload();
		if (SingletonDontDestoryUnity<GameManager>.Instance.IsNeedCountDownload)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "BackGroundDownloadStart");
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "StartTimes");
			LocalDataSaveManager.SetDownloadCountFinish();
		}
		this.startDownloadTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06003F3D RID: 16189 RVA: 0x00125B74 File Offset: 0x00123D74
	private new void Awake()
	{
		base.Awake();
		this.DownLoadFinishFlag = false;
		this.ProgressLinePic.width = UIWidgetControl.GetFitWidth(this.ProgressLinePic.width);
		this.ProgressLinePic.transform.localPosition = new Vector3((float)(-(float)this.ProgressLinePic.width / 2), 0f, 0f);
		this.mProgressLineWidth = this.ProgressLinePic.width;
		this.SetProgressLineWidth(0);
		if (this.updateHelper == null)
		{
			GameObject gameObject = new GameObject();
			UnityVersionUtil.SetActiveRecursive(gameObject, true);
			this.updateHelper = gameObject.AddComponent<FileUpdateHelper>();
		}
		PlayerData.downLoadFlag = 1;
		this.BeginDownload();
		this.downloadProgress = 0f;
		this.autoContinueFlag = true;
		this.UpdateTutorialStep(0, 0f);
	}

	// Token: 0x17000F8C RID: 3980
	// (get) Token: 0x06003F3E RID: 16190 RVA: 0x00125C44 File Offset: 0x00123E44
	public FileUpdateHelper UpdateHelper
	{
		get
		{
			return this.updateHelper;
		}
	}

	// Token: 0x06003F3F RID: 16191 RVA: 0x00125C4C File Offset: 0x00123E4C
	private void SetProgressLineWidth(int width)
	{
		this.ProgressLinePic.width = width;
		this.ProgressLineAnimaObj.transform.localPosition = new Vector3((float)width, 0f, 0f);
	}

	// Token: 0x06003F40 RID: 16192 RVA: 0x00125C88 File Offset: 0x00123E88
	private void BeginDownload()
	{
		if (GameSettingData.GetPhoneClass() != 0)
		{
			this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), true, false);
		}
	}

	// Token: 0x06003F41 RID: 16193 RVA: 0x00125CC4 File Offset: 0x00123EC4
	public void OnChangeUpdateStep(UPDATE_STEP newStep)
	{
		this.mLastUpdateStep = newStep;
		switch (newStep)
		{
		case UPDATE_STEP.CHECK_VERSION:
			this.downloadProgress = 0f;
			break;
		case UPDATE_STEP.GET_FILELIST:
			this.downloadProgress = 0f;
			break;
		case UPDATE_STEP.COMPARE_RES:
			this.downloadProgress = 0f;
			break;
		case UPDATE_STEP.CHECK_IS_DOWNLOAD:
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
			this.OnClickDownLoadBtn();
			break;
		case UPDATE_STEP.CHECK_RES:
			this.downloadProgress = 0.93f;
			break;
		case UPDATE_STEP.COPY_RES:
			this.downloadProgress = 0.93f;
			break;
		case UPDATE_STEP.CLEAR_CACHE:
			this.downloadProgress = 0.93f;
			SingletonDontDestoryUnity<GameManager>.Instance.ReImportData();
			SingletonDontDestoryUnity<GameManager>.Instance.ReImportAnima();
			break;
		case UPDATE_STEP.FINISH:
			if (this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS)
			{
				this.downloadProgress = 0.95f;
				this.DownloadFlurryCount(Time.realtimeSinceStartup - this.startDownloadTime);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "BackGroundDownloadFinish");
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "FinishTimes");
			}
			else if (this.autoContinueFlag)
			{
				this.autoContinueFlag = false;
				this.ReDownLoad();
			}
			break;
		}
	}

	// Token: 0x06003F42 RID: 16194 RVA: 0x00125E78 File Offset: 0x00124078
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

	// Token: 0x06003F43 RID: 16195 RVA: 0x00125FF8 File Offset: 0x001241F8
	private void CheckDownloadRes()
	{
		this.SetProgressLineVal(0f);
		this.SetProgressLineWidth(0);
		this.OnClickDownLoadBtn();
	}

	// Token: 0x06003F44 RID: 16196 RVA: 0x00126014 File Offset: 0x00124214
	private void OnClickExitGame()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		MessageBoxLogic.CloseBox();
		Application.Quit();
	}

	// Token: 0x06003F45 RID: 16197 RVA: 0x0012602C File Offset: 0x0012422C
	public void StartDownload()
	{
		this.updateHelper.DownloadFileList();
	}

	// Token: 0x06003F46 RID: 16198 RVA: 0x0012603C File Offset: 0x0012423C
	public void ContinueDownLoad()
	{
		this.updateHelper.ContinueDownload();
	}

	// Token: 0x06003F47 RID: 16199 RVA: 0x0012604C File Offset: 0x0012424C
	public void ReDownLoad()
	{
		this.downloadProgress = 0f;
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), true, false);
	}

	// Token: 0x06003F48 RID: 16200 RVA: 0x00126080 File Offset: 0x00124280
	private void SetProgressLineVal(float val)
	{
		if (val > this.curProgress)
		{
			this.mProgressTargetWidth = (int)(val * (float)this.mProgressLineWidth);
			this.curProgress = val;
		}
	}

	// Token: 0x06003F49 RID: 16201 RVA: 0x001260A8 File Offset: 0x001242A8
	private void Update()
	{
		if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES && this.mTutorialStep == 2)
		{
			this.downloadProgress = this.updateHelper.GetDownloadProgress() * 0.93f;
		}
		if (this.mLastUpdateStep == UPDATE_STEP.FINISH && this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS && DataManager.DataInitFinishFlag)
		{
			this.downloadProgress = 1f;
		}
		if (this.mTutorialStep == 2)
		{
			this.UpdateTutorialStep(2, this.downloadProgress);
		}
		if (this.ProgressLinePic.width < this.mProgressTargetWidth)
		{
			int num = this.ProgressLinePic.width + 10;
			if (num > this.mProgressTargetWidth)
			{
				num = this.mProgressTargetWidth;
			}
			this.SetProgressLineWidth(num);
			if (this.ProgressLinePic.width >= this.mProgressLineWidth && this.mLastUpdateStep == UPDATE_STEP.FINISH)
			{
				this.DownLoadFinishFlag = true;
				this.timeCount = Time.time;
			}
		}
	}

	// Token: 0x04002B03 RID: 11011
	public GameObject ProgressLineAnimaObj;

	// Token: 0x04002B04 RID: 11012
	public GameObject[] StarPic;

	// Token: 0x04002B05 RID: 11013
	public UILabel StepTargetLabel;

	// Token: 0x04002B06 RID: 11014
	private string[] targetWords = new string[]
	{
		"#{600067}",
		"#{600067}",
		"#{600068}"
	};

	// Token: 0x04002B07 RID: 11015
	private float startDownloadTime;

	// Token: 0x04002B08 RID: 11016
	private float downloadProgress;

	// Token: 0x04002B09 RID: 11017
	private float[] statePercent = new float[]
	{
		0.3f,
		0.65f,
		1f,
		1f
	};

	// Token: 0x04002B0A RID: 11018
	private int mTutorialStep = -1;

	// Token: 0x04002B0B RID: 11019
	public UISprite ProgressLinePic;

	// Token: 0x04002B0C RID: 11020
	private int mProgressLineWidth = -1;

	// Token: 0x04002B0D RID: 11021
	private int mProgressTargetWidth;

	// Token: 0x04002B0E RID: 11022
	private string mCurLocalVersion = string.Empty;

	// Token: 0x04002B0F RID: 11023
	private string mCurApkVersion = string.Empty;

	// Token: 0x04002B10 RID: 11024
	private FileUpdateHelper updateHelper;

	// Token: 0x04002B11 RID: 11025
	private UPDATE_STEP mLastUpdateStep;

	// Token: 0x04002B12 RID: 11026
	private float mNeedDownLoadSize;

	// Token: 0x04002B13 RID: 11027
	private string mDownLoadUnit;

	// Token: 0x04002B14 RID: 11028
	private bool autoContinueFlag = true;

	// Token: 0x04002B15 RID: 11029
	private float curProgress;

	// Token: 0x04002B16 RID: 11030
	private string[] TitleLocIdList = new string[]
	{
		"#{101511}",
		"#{101512}",
		"#{101516}",
		"#{101518}",
		"#{101519}"
	};

	// Token: 0x04002B17 RID: 11031
	private string[] TextLocIdList = new string[]
	{
		"#{101601}",
		"#{101603}",
		"#{101613}",
		"#{101615}",
		"#{101617}"
	};

	// Token: 0x04002B18 RID: 11032
	private float TurnPageTime = 8f;

	// Token: 0x04002B19 RID: 11033
	private int mCurIndex;

	// Token: 0x04002B1A RID: 11034
	private float mLastTurnPageTime;

	// Token: 0x04002B1B RID: 11035
	private StringBuilder percentSb = new StringBuilder(512);

	// Token: 0x04002B1C RID: 11036
	private string percentFormat = "{0}";

	// Token: 0x04002B1D RID: 11037
	private StringBuilder downloadSb = new StringBuilder(512);

	// Token: 0x04002B1E RID: 11038
	private string downloadFormat = "{0:N1}/{1:N1}{2}";

	// Token: 0x04002B1F RID: 11039
	public bool DownLoadFinishFlag;

	// Token: 0x04002B20 RID: 11040
	private float timeCount;
}
