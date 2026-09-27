using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200093C RID: 2364
public class ExpLineRootLogic : SingletonUnity<ExpLineRootLogic>
{
	// Token: 0x060041C4 RID: 16836 RVA: 0x0013A314 File Offset: 0x00138514
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060041C5 RID: 16837 RVA: 0x0013A320 File Offset: 0x00138520
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060041C6 RID: 16838 RVA: 0x0013A340 File Offset: 0x00138540
	public static void UpdateExp()
	{
		if (SingletonUnity<ExpLineRootLogic>.Exists)
		{
			SingletonUnity<ExpLineRootLogic>.Instance.UpdateExpVal();
		}
	}

	// Token: 0x060041C7 RID: 16839 RVA: 0x0013A358 File Offset: 0x00138558
	private void Start()
	{
		this.UpdateExpVal();
		this.startTime = 300f;
		this.startTime2 = 300f;
		this.startTime3 = 10f;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.CurrentMapInofData.MapType == MAPTYPE.CAR_CHASE_COPY)
		{
			UnityVersionUtil.SetActiveRecursive(this.ViewBtnPic.gameObject, false);
		}
		this.UpdateViewType();
	}

	// Token: 0x060041C8 RID: 16840 RVA: 0x0013A3C0 File Offset: 0x001385C0
	public void UpdateViewType()
	{
		if (this.mPlayerData == null)
		{
			this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		if (this.mPlayerData.ViewType == CameraController.CAMERAVIEWSTATE.FIXED || this.mPlayerData.ViewType == CameraController.CAMERAVIEWSTATE.FIXED_3D_2_FIXED)
		{
			this.ViewLabel.text = "2.5D";
		}
		else if (this.mPlayerData.ViewType == CameraController.CAMERAVIEWSTATE.FREE || this.mPlayerData.ViewType == CameraController.CAMERAVIEWSTATE.FIXED_3D_2_FREE)
		{
			this.ViewLabel.text = "3D";
		}
	}

	// Token: 0x060041C9 RID: 16841 RVA: 0x0013A450 File Offset: 0x00138650
	public void UpdateExpVal()
	{
		if (this.mPlayerData == null)
		{
			this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		if (this.mCurLevel != this.mPlayerData.Level)
		{
			this.mTargetExp = DataManager.GetLevelDataByLevel(this.mPlayerData.Level).Exp;
			this.mCurLevel = this.mPlayerData.Level;
		}
		this.mCurPercent = Mathf.Clamp01((float)this.mPlayerData.MainPlayerAttrData.CurEXP / (float)this.mTargetExp);
		this.ExpSprite.width = (int)((float)Screen.width * this.mCurPercent);
		this.TopPic.width = (int)((float)this.BottomPic.width * this.mCurPercent);
		this.mTempStr.Length = 0;
		this.ExpLabel.text = this.mTempStr.AppendFormat("Exp ({0:0.0}%)", this.mCurPercent * 100f).ToString();
	}

	// Token: 0x060041CA RID: 16842 RVA: 0x0013A554 File Offset: 0x00138754
	private void UpdateDelayTime()
	{
	}

	// Token: 0x060041CB RID: 16843 RVA: 0x0013A558 File Offset: 0x00138758
	private int GetBatteryLevel()
	{
		int result = 50;
		try
		{
			string text = File.ReadAllText("/sys/class/power_supply/battery/capacity");
			result = int.Parse(text);
		}
		catch (Exception ex)
		{
			result = SingletonDontDestoryUnity<GameManager>.Instance.GetBatteryState();
		}
		return result;
	}

	// Token: 0x060041CC RID: 16844 RVA: 0x0013A5B0 File Offset: 0x001387B0
	public void OnClickChangeViewBtn()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			CameraController cameraController = Singleton<ObjManager>.Instance.MainPlayer.CameraController;
			if (cameraController != null)
			{
				if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FREE)
				{
					cameraController.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FIXED);
					this.UpdateViewType();
				}
				else if (CameraController.CurrentViewState == CameraController.CAMERAVIEWSTATE.FIXED)
				{
					cameraController.ChangeCameraView(CameraController.CAMERAVIEWSTATE.FREE);
					this.UpdateViewType();
				}
			}
			if (TutorialManager.CurStep == TUTORIAL_STEP.SWITCH_VIEW)
			{
				this.CheckTutorialEvent();
			}
		}
	}

	// Token: 0x04002DAC RID: 11692
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002DAD RID: 11693
	public UILabel ExpLabel;

	// Token: 0x04002DAE RID: 11694
	public UISprite ExpSprite;

	// Token: 0x04002DAF RID: 11695
	public UISprite BottomPic;

	// Token: 0x04002DB0 RID: 11696
	public UISprite TopPic;

	// Token: 0x04002DB1 RID: 11697
	public UISprite ViewBtnPic;

	// Token: 0x04002DB2 RID: 11698
	public UILabel ViewLabel;

	// Token: 0x04002DB3 RID: 11699
	private PlayerData mPlayerData;

	// Token: 0x04002DB4 RID: 11700
	private int mCurLevel = -1;

	// Token: 0x04002DB5 RID: 11701
	private long mTargetExp;

	// Token: 0x04002DB6 RID: 11702
	private float mCurPercent;

	// Token: 0x04002DB7 RID: 11703
	private StringBuilder mTempStr = new StringBuilder();

	// Token: 0x04002DB8 RID: 11704
	private float startTime;

	// Token: 0x04002DB9 RID: 11705
	private float startTime2;

	// Token: 0x04002DBA RID: 11706
	private float startTime3;
}
