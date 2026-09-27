using System;
using UnityEngine;

// Token: 0x02000899 RID: 2201
public class LoadingUITest : MonoBehaviour
{
	// Token: 0x06003B82 RID: 15234 RVA: 0x00104160 File Offset: 0x00102360
	private void Awake()
	{
		this.updateHelper = base.gameObject.GetComponent<FileUpdateHelper>();
		if (this.updateHelper == null)
		{
			this.updateHelper = base.gameObject.AddComponent<FileUpdateHelper>();
		}
	}

	// Token: 0x06003B83 RID: 15235 RVA: 0x001041A0 File Offset: 0x001023A0
	private void Start()
	{
		UnityVersionUtil.SetActiveRecursive(this.CheckRootObj.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.ReDownloadRootObj.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.ContinueDownloadRootObj, false);
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), false, true);
		this.ProcessSlider.value = 0f;
	}

	// Token: 0x06003B84 RID: 15236 RVA: 0x00104210 File Offset: 0x00102410
	private void Update()
	{
		if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
		{
			this.SetSlider(0.3f + 0.6f * this.updateHelper.GetDownloadProgress());
		}
		if (this.ProcessSlider.value < this.targetPercent)
		{
			float num = this.ProcessSlider.value + Time.deltaTime;
			if (num > this.targetPercent)
			{
				num = this.targetPercent;
			}
			this.ProcessSlider.value = num;
			if (this.ProcessSlider.value > 0.99f)
			{
				Application.LoadLevel("Login");
			}
		}
		if (Input.GetKeyUp(27))
		{
			Application.Quit();
		}
	}

	// Token: 0x06003B85 RID: 15237 RVA: 0x001042C0 File Offset: 0x001024C0
	public void OnChangeUpdateStep(UPDATE_STEP newStep)
	{
		this.UpdateStateLabel.text = string.Empty + newStep;
		switch (newStep)
		{
		case UPDATE_STEP.CHECK_VERSION:
			this.SetSlider(0f);
			break;
		case UPDATE_STEP.GET_FILELIST:
			this.SetSlider(0.1f);
			break;
		case UPDATE_STEP.COMPARE_RES:
			this.SetSlider(0.2f);
			break;
		case UPDATE_STEP.CHECK_IS_DOWNLOAD:
			this.SetSlider(0.3f);
			this.DownloadSizeLabel.text = (float)this.updateHelper.NeedDownloadSize / 1024f / 1024f + "MB";
			UnityVersionUtil.SetActiveRecursive(this.CheckRootObj.gameObject, true);
			break;
		case UPDATE_STEP.CHECK_RES:
			this.SetSlider(0.9f);
			break;
		case UPDATE_STEP.COPY_RES:
			this.SetSlider(0.95f);
			break;
		case UPDATE_STEP.CLEAR_CACHE:
			this.SetSlider(0.98f);
			break;
		case UPDATE_STEP.FINISH:
			if (this.updateHelper.CurUpdateResult == UPDATE_RESULT.SUCCESS)
			{
				this.SetSlider(1f);
			}
			else if (this.mLastUpdateStep == UPDATE_STEP.DOWNLOAD_RES)
			{
				UnityVersionUtil.SetActiveRecursive(this.ContinueDownloadRootObj, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ReDownloadRootObj.gameObject, true);
			}
			this.UpdateResultLabel.text = string.Empty + newStep;
			break;
		}
		this.mLastUpdateStep = newStep;
	}

	// Token: 0x06003B86 RID: 15238 RVA: 0x00104448 File Offset: 0x00102648
	private void SetSlider(float percent)
	{
		this.targetPercent = percent;
	}

	// Token: 0x06003B87 RID: 15239 RVA: 0x00104454 File Offset: 0x00102654
	public void StartDownload()
	{
		UnityVersionUtil.SetActiveRecursive(this.CheckRootObj.gameObject, false);
		this.updateHelper.DownloadFileList();
	}

	// Token: 0x06003B88 RID: 15240 RVA: 0x00104474 File Offset: 0x00102674
	public void ReDownLoad()
	{
		this.ProcessSlider.value = 0f;
		this.updateHelper.StartCheckRes(this.updateHelper.mServerUrl, new FileUpdateHelper.OnChangeUpdateStepDelegate(this.OnChangeUpdateStep), false, true);
		UnityVersionUtil.SetActiveRecursive(this.ReDownloadRootObj.gameObject, false);
	}

	// Token: 0x06003B89 RID: 15241 RVA: 0x001044C8 File Offset: 0x001026C8
	public void ContinueDownLoad()
	{
		this.updateHelper.ContinueDownload();
		UnityVersionUtil.SetActiveRecursive(this.ContinueDownloadRootObj, false);
	}

	// Token: 0x04002700 RID: 9984
	private FileUpdateHelper updateHelper;

	// Token: 0x04002701 RID: 9985
	public UISlider ProcessSlider;

	// Token: 0x04002702 RID: 9986
	public UILabel UpdateResultLabel;

	// Token: 0x04002703 RID: 9987
	public UILabel UpdateStateLabel;

	// Token: 0x04002704 RID: 9988
	public GameObject CheckRootObj;

	// Token: 0x04002705 RID: 9989
	public UILabel DownloadSizeLabel;

	// Token: 0x04002706 RID: 9990
	public GameObject ReDownloadRootObj;

	// Token: 0x04002707 RID: 9991
	public GameObject ContinueDownloadRootObj;

	// Token: 0x04002708 RID: 9992
	public UILabel curDownloadFileNameLabel;

	// Token: 0x04002709 RID: 9993
	private UPDATE_STEP mLastUpdateStep = UPDATE_STEP.INVALID;

	// Token: 0x0400270A RID: 9994
	private float targetPercent;
}
