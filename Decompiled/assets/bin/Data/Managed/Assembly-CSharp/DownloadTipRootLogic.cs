using System;
using UnityEngine;

// Token: 0x02000907 RID: 2311
public class DownloadTipRootLogic : SingletonUnity<DownloadTipRootLogic>
{
	// Token: 0x17000F8D RID: 3981
	// (get) Token: 0x06003F4B RID: 16203 RVA: 0x001261AC File Offset: 0x001243AC
	// (set) Token: 0x06003F4C RID: 16204 RVA: 0x001261B4 File Offset: 0x001243B4
	public FileUpdateHelper UpdateHelper
	{
		get
		{
			return this.updateHelper;
		}
		set
		{
			this.updateHelper = value;
		}
	}

	// Token: 0x06003F4D RID: 16205 RVA: 0x001261C0 File Offset: 0x001243C0
	public void Reset()
	{
		this.isBackGroundDownload = false;
		if (PlayerData.ServerDataVersion != PlayerData.LocalDataVersion)
		{
			PlayerData.downLoadFlag = 1;
			if (this.updateHelper == null)
			{
				GameObject gameObject = GameObject.Find("FileUpdateHelper");
				if (gameObject == null)
				{
					gameObject = new GameObject();
					gameObject.name = "FileUpdateHelper";
					UnityVersionUtil.SetActiveRecursive(gameObject, true);
					this.updateHelper = gameObject.AddComponent<FileUpdateHelper>();
				}
				else
				{
					this.updateHelper = gameObject.GetComponent<FileUpdateHelper>();
					if (this.updateHelper == null)
					{
						this.updateHelper = gameObject.AddComponent<FileUpdateHelper>();
					}
				}
			}
			this.BeginDownload();
			if (this.updateHelper.CurUpdateStape == UPDATE_STEP.FINISH && this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS)
			{
				UnityVersionUtil.SetActiveRecursive(this.FinishRoot, true);
				this.ShowTipsLabel(true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.FinishRoot, false);
				this.ShowTipsLabel(false);
			}
			this.autoContinueFlag = true;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.FinishRoot, true);
			this.ShowTipsLabel(true);
		}
	}

	// Token: 0x06003F4E RID: 16206 RVA: 0x001262D4 File Offset: 0x001244D4
	public void ShowTipsLabel(bool isFinish)
	{
		if (isFinish)
		{
			if (GameManager.IsSupportCurDataVersion167())
			{
				this.TipLabel.text = StrDictionary.GetDictionaryString("#{300802}", new object[0]);
			}
			else
			{
				this.TipLabel.text = "Complete";
			}
			this.DirAnima.enabled = false;
			this.DirAnima.transform.localPosition = new Vector3(this.DirAnima.transform.localPosition.x, 0f, this.DirAnima.transform.localPosition.z);
			this.BgAnima.enabled = true;
			this.BgAnima.PlayForward();
		}
		else
		{
			if (GameManager.IsSupportCurDataVersion167())
			{
				this.TipLabel.text = StrDictionary.GetDictionaryString("#{102201}", new object[0]);
			}
			else
			{
				this.TipLabel.text = "Download";
			}
			this.DirAnima.enabled = true;
			this.DirAnima.PlayForward();
			this.BgAnima.enabled = false;
			this.BgAnima.ResetToBeginning();
		}
		if (GameManager.IsSupportCurDataVersion167())
		{
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{102214}", new object[0]);
		}
		else
		{
			this.InfoLabel.text = "With New Car";
		}
	}

	// Token: 0x06003F4F RID: 16207 RVA: 0x00126438 File Offset: 0x00124638
	public void OnClickDownloadTipBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
	}

	// Token: 0x06003F50 RID: 16208 RVA: 0x0012644C File Offset: 0x0012464C
	public void BeginDownload()
	{
		if (GameSettingData.GetPhoneClass() != 0)
		{
			this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), true, false);
		}
	}

	// Token: 0x06003F51 RID: 16209 RVA: 0x00126488 File Offset: 0x00124688
	public void OnChangeUpdateStep(UPDATE_STEP newStep)
	{
		this.mCurUpdateStep = newStep;
		switch (this.mCurUpdateStep)
		{
		case UPDATE_STEP.CHECK_IS_DOWNLOAD:
			if (Application.internetReachability == 2)
			{
				this.updateHelper.DownloadFileList();
				if (SingletonUnity<DownLoadResRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DownLoadResRootLogic>.Instance.gameObject))
				{
					SingletonUnity<DownLoadResRootLogic>.Instance.OnChangeUpdateStep(UPDATE_STEP.DOWNLOAD_RES, true);
				}
				this.isBackGroundDownload = true;
				if (SingletonDontDestoryUnity<GameManager>.Instance.IsNeedCountDownload)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "BackGroundDownloadStart");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "StartTimes");
					LocalDataSaveManager.SetDownloadCountFinish();
					this.startDownloadTime = Time.realtimeSinceStartup;
				}
			}
			break;
		case UPDATE_STEP.CLEAR_CACHE:
			if (SingletonDontDestoryUnity<GameManager>.Instance.IsNeedCountDownloadFinish)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.IsNeedCountDownloadFinish = false;
				LocalDataSaveManager.SetDownloadFinishCountFinish();
				if (this.isBackGroundDownload)
				{
					this.DownloadFlurryCount(Time.realtimeSinceStartup - this.startDownloadTime);
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "BackGroundDownloadFinish");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "FinishTimes");
				}
				else
				{
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "RealDownloadFinish");
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tutorial", "NewDownload", "FinishTimes");
					this.DownloadFlurryCount(Time.realtimeSinceStartup - this.startDownloadTime);
				}
			}
			break;
		case UPDATE_STEP.FINISH:
			if (this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS)
			{
				this.ShowDownloadFinishTip();
			}
			else if (!SingletonUnity<DownLoadResRootLogic>.Exists && this.autoContinueFlag)
			{
				this.autoContinueFlag = false;
				this.ReDownLoad();
			}
			break;
		}
	}

	// Token: 0x06003F52 RID: 16210 RVA: 0x00126664 File Offset: 0x00124864
	public void ReDownLoad()
	{
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), true, false);
	}

	// Token: 0x06003F53 RID: 16211 RVA: 0x00126698 File Offset: 0x00124898
	public void ShowDownloadFinishTip()
	{
		UnityVersionUtil.SetActiveRecursive(this.FinishRoot, true);
		this.ShowTipsLabel(true);
		if (SingletonUnity<CitySimController>.Exists)
		{
			SingletonUnity<CitySimController>.Instance.OnDownloadFlashNPC();
		}
	}

	// Token: 0x06003F54 RID: 16212 RVA: 0x001266C4 File Offset: 0x001248C4
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

	// Token: 0x04002B21 RID: 11041
	public UILabel TipLabel;

	// Token: 0x04002B22 RID: 11042
	public UILabel InfoLabel;

	// Token: 0x04002B23 RID: 11043
	public GameObject FinishRoot;

	// Token: 0x04002B24 RID: 11044
	public TweenPosition DirAnima;

	// Token: 0x04002B25 RID: 11045
	public TweenAlpha BgAnima;

	// Token: 0x04002B26 RID: 11046
	private FileUpdateHelper updateHelper;

	// Token: 0x04002B27 RID: 11047
	private UPDATE_STEP mCurUpdateStep;

	// Token: 0x04002B28 RID: 11048
	private bool autoContinueFlag;

	// Token: 0x04002B29 RID: 11049
	private float startDownloadTime;

	// Token: 0x04002B2A RID: 11050
	private bool isBackGroundDownload;
}
