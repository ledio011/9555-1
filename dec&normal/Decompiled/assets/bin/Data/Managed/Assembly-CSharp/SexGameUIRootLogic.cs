using System;
using System.Text;
using SprotoType;
using UnityEngine;

// Token: 0x02000988 RID: 2440
public class SexGameUIRootLogic : SingletonUnity<SexGameUIRootLogic>
{
	// Token: 0x060044FD RID: 17661 RVA: 0x00159604 File Offset: 0x00157804
	public void Reset(bool isMan, string actId, NpcData npcData)
	{
		this.mCurScores = 0f;
		this.mCurScoresRate = 1f;
		this.mCurRestTime = this.TimeLimit;
		this.mCurGameVal = 0f;
		this.PicLength = (float)this.LinePic.height;
		this.CZPosPic.transform.localPosition = new Vector3(0f, this.PicLength * (this.MinCZVal / this.MaxGameVal), 0f);
		this.CZPosPic.width = Mathf.RoundToInt(this.PicLength * (this.MaxCZVal - this.MinCZVal) / this.MaxGameVal);
		this.BottomPosObj.localPosition = new Vector3(-8f, this.PicLength * (this.MinCZVal / this.MaxGameVal), 0f);
		NGUITools.SetActive(this.TouchRoot.gameObject, false);
		NGUITools.SetActive(this.ComboRoot.gameObject, false);
		NGUITools.SetActive(this.ScoresResultRoot, false);
		this.mFinishFlag = false;
		this.mStartFlag = false;
		this.mPauseFlag = false;
		this.SexGameCamPos = GameObject.Find("DSJ_zhuCheng/SexGamCamPos").transform;
		this.SexGameCamPos.animation.Play();
		this.SexGameParticleEffect = GameObject.Find("DSJ_zhuCheng").transform.FindChild("SexGameParticleEffect").gameObject.GetComponent<ParticleSystem>();
		UnityVersionUtil.SetActiveRecursive(this.SexGameParticleEffect.gameObject, true);
		this.SexGameParticleEffect.startSpeed = 5f;
		this.SexGameParticleEffect.emissionRate = 10f;
		Singleton<ObjManager>.Instance.MainPlayer.CameraController.LerpToTargetLocalZero(this.SexGameCamPos, 1f, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarMissionStartTimeCountRoot, delegate
		{
			SingletonUnity<CarMissionStartTimeCountRoot>.Instance.Reset(Time.time, new DelegateDefine.NoParamDelegate(this.StartGame));
		}, null);
		this.mIsMan = isMan;
		this.activityId = actId;
		this.TimeLabel.text = TimeTools.GetMinuteSecondStr(Mathf.FloorToInt(this.mCurRestTime));
		this.curNpcData = npcData;
		this.clickCount = 0;
		this.screenWidth = (float)Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
	}

	// Token: 0x060044FE RID: 17662 RVA: 0x00159834 File Offset: 0x00157A34
	private void StartGame()
	{
		this.mStartFlag = true;
	}

	// Token: 0x060044FF RID: 17663 RVA: 0x00159840 File Offset: 0x00157A40
	public void PauseGame()
	{
		this.mPauseFlag = true;
	}

	// Token: 0x06004500 RID: 17664 RVA: 0x0015984C File Offset: 0x00157A4C
	public void ResumeGame()
	{
		this.mPauseFlag = false;
	}

	// Token: 0x06004501 RID: 17665 RVA: 0x00159858 File Offset: 0x00157A58
	public void OnClickExitBtn()
	{
		if (this.mFinishFlag)
		{
			return;
		}
		this.PauseGame();
		MessageBoxLogic.OpenOKCancelBox("#{102001}", "#{100127}", delegate
		{
			if (SingletonUnity<CarMissionStartTimeCountRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CarMissionStartTimeCountRoot>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarMissionStartTimeCountRoot);
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SexGameUIRoot);
			Singleton<ObjManager>.Instance.MainPlayer.CameraController.LerpBackToPlayer(1f, null);
			UnityVersionUtil.SetActiveRecursive(this.SexGameParticleEffect.gameObject, false);
			StoryDialogRootLogic.ShowStory("107", this.curNpcData);
		}, delegate
		{
			this.ResumeGame();
		}, null, null);
	}

	// Token: 0x06004502 RID: 17666 RVA: 0x001598A0 File Offset: 0x00157AA0
	public void OnClickScreen()
	{
		if (this.mFinishFlag || !this.mStartFlag || this.mPauseFlag)
		{
			return;
		}
		this.clickCount++;
		this.ShowTouch();
		this.mCurGameVal += this.GameAddVal;
		this.mCurGameVal = ((this.mCurGameVal <= this.MaxGameVal) ? this.mCurGameVal : this.MaxGameVal);
		if (this.mCurGameVal > this.MinScoreCZVal && this.mCurGameVal < this.MaxScoreCZVal)
		{
			if (this.mCurGameVal > this.MinCZVal && this.mCurGameVal < this.MaxCZVal)
			{
				this.mCurComboVal++;
				this.ShowCombo();
				this.mCurScoresRate = 1f + this.ScoresAddRate * (float)(this.mCurComboVal / this.ComboAddRateNum);
				this.mCurScoresRate = ((this.mCurScoresRate <= this.MaxScoresAddRate) ? this.mCurScoresRate : this.MaxScoresAddRate);
				this.mCurScores += this.AddScores * this.mCurScoresRate;
				if (this.clickCount >= this.SoundClickNum)
				{
					this.clickCount = 0;
					this.PlayComboSound();
				}
			}
			else
			{
				this.HideCombo();
				this.mCurScores += this.AddScores;
				if (this.clickCount >= this.SoundClickNum)
				{
					this.clickCount = 0;
					this.PlayNormalSound();
				}
			}
		}
		else
		{
			this.HideCombo();
		}
		this.changeValTime = 1f;
	}

	// Token: 0x06004503 RID: 17667 RVA: 0x00159A4C File Offset: 0x00157C4C
	private void PlayNormalSound()
	{
		if (this.mIsMan)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.NormalManSound, 1f, null);
		}
		else
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.NormalWomenSound, 1f, null);
		}
	}

	// Token: 0x06004504 RID: 17668 RVA: 0x00159A98 File Offset: 0x00157C98
	private void PlayComboSound()
	{
		if (this.mIsMan)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.ComboManSound, 1f, null);
		}
		else
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.ComboWomenSound, 1f, null);
		}
	}

	// Token: 0x06004505 RID: 17669 RVA: 0x00159AE4 File Offset: 0x00157CE4
	private void ShowCombo()
	{
		NGUITools.SetActive(this.ComboRoot.gameObject, true);
		this.ComboRoot.ResetToBeginning();
		this.ComboRoot.PlayForward();
		this.ComboSubObj.ResetToBeginning();
		this.ComboSubObj.PlayForward();
		this.ComboLabel.text = string.Format("[i]{0}[/i]", this.mCurComboVal);
		this.SexGameParticleEffect.startSpeed = -5f;
		this.SexGameParticleEffect.emissionRate = 30f;
		this.clickStartTime = float.MaxValue;
	}

	// Token: 0x06004506 RID: 17670 RVA: 0x00159B7C File Offset: 0x00157D7C
	public void DisableCombat()
	{
		NGUITools.SetActive(this.ComboRoot.gameObject, false);
	}

	// Token: 0x06004507 RID: 17671 RVA: 0x00159B90 File Offset: 0x00157D90
	private void HideCombo()
	{
		this.mCurComboVal = 0;
		NGUITools.SetActive(this.ComboRoot.gameObject, false);
		this.clickStartTime = Time.time;
		this.SexGameParticleEffect.startSpeed = 0f;
		this.SexGameParticleEffect.emissionRate = 10f;
	}

	// Token: 0x06004508 RID: 17672 RVA: 0x00159BE0 File Offset: 0x00157DE0
	private void ShowTouch()
	{
		NGUITools.SetActive(this.TouchRoot.gameObject, true);
		this.TouchRoot.ResetToBeginning();
		this.TouchRoot.PlayForward();
		this.TouchRoot.transform.localPosition = new Vector3(UICamera.lastTouchPosition.x / (float)Screen.width * this.screenWidth, UICamera.lastTouchPosition.y / (float)Screen.height * 480f, 0f);
	}

	// Token: 0x06004509 RID: 17673 RVA: 0x00159C60 File Offset: 0x00157E60
	private void Update()
	{
		if (!this.mStartFlag)
		{
			return;
		}
		if (this.mPauseFlag)
		{
			return;
		}
		this.mCurRestTime -= Time.deltaTime;
		if (this.mCurRestTime > 0f)
		{
			this.TimeLabel.text = TimeTools.GetMinuteSecondStr(Mathf.FloorToInt(this.mCurRestTime));
			if (this.changeValTime > 0f)
			{
				this.changeValTime -= Time.deltaTime * 3f;
				this.mCurShowScores = (float)Mathf.FloorToInt(Mathf.Lerp(this.mCurScores, this.mCurShowScores, this.changeValTime));
				this.mScoresSb.Length = 0;
				this.mScoresSb.AppendFormat("{0}", this.mCurShowScores);
				this.ScoresLabel.text = this.mScoresSb.ToString();
			}
			else
			{
				this.changeValTime = 0f;
			}
			this.mCurShowGameVal = (float)Mathf.FloorToInt(Mathf.Lerp(this.mCurGameVal, this.mCurShowGameVal, this.changeValTime));
			this.curLinePercent = this.mCurShowGameVal / this.MaxGameVal;
			this.LinePanel.SetRect(0f, this.curLinePercent * this.PicLength / 2f, 75f, this.curLinePercent * this.PicLength);
			this.mCurGameVal -= Time.deltaTime * this.GameReduceVal;
			if (this.mCurGameVal < 0f)
			{
				this.mCurGameVal = 0f;
			}
			if (this.mCurGameVal < this.MinCZVal && this.SexGameParticleEffect.startSpeed < 0f)
			{
				this.HideCombo();
			}
		}
		else if (!this.mFinishFlag)
		{
			this.mFinishFlag = true;
			this.FinishGame();
		}
		if (Time.time - this.clickStartTime > 0.3f)
		{
			this.SexGameParticleEffect.startSpeed = 5f;
			this.SexGameParticleEffect.emissionRate = 10f;
			this.clickStartTime = float.MaxValue;
		}
	}

	// Token: 0x0600450A RID: 17674 RVA: 0x00159E84 File Offset: 0x00158084
	public void FinishGame()
	{
		NGUITools.SetActive(this.ScoresResultRoot, true);
		NGUITools.SetActive(this.ComboRoot.gameObject, false);
		this.ScoresResultLabel.text = string.Format("[i]{0}[/i]", this.mCurScores);
		update_sex_mini_score.request request = new update_sex_mini_score.request();
		request.id = this.activityId;
		request.score = (long)this.mCurScores;
		NetLogic.GetInstance().Send<Protocol.update_sex_mini_score>(request, null);
		vp_Timer.In(2f, delegate()
		{
			if (SingletonUnity<CarMissionStartTimeCountRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CarMissionStartTimeCountRoot>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarMissionStartTimeCountRoot);
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SexGameUIRoot);
			Singleton<ObjManager>.Instance.MainPlayer.CameraController.LerpBackToPlayer(1f, null);
			UnityVersionUtil.SetActiveRecursive(this.SexGameParticleEffect.gameObject, false);
			StoryDialogRootLogic.ShowStory("107", this.curNpcData);
		}, null);
		this.SexGameCamPos.animation.Stop();
		activity_info activity_info = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.CurActivityDataDic[this.activityId];
		activity_info.CurNum += 1L;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_8", "finish");
	}

	// Token: 0x040031CB RID: 12747
	public UIPanel LinePanel;

	// Token: 0x040031CC RID: 12748
	public UILabel ScoresLabel;

	// Token: 0x040031CD RID: 12749
	public UILabel TimeLabel;

	// Token: 0x040031CE RID: 12750
	public UILabel ComboLabel;

	// Token: 0x040031CF RID: 12751
	public TweenScale ComboRoot;

	// Token: 0x040031D0 RID: 12752
	public TweenPosition ComboSubObj;

	// Token: 0x040031D1 RID: 12753
	public TweenAlpha TouchRoot;

	// Token: 0x040031D2 RID: 12754
	public UISprite LinePic;

	// Token: 0x040031D3 RID: 12755
	public UISprite CZPosPic;

	// Token: 0x040031D4 RID: 12756
	public GameObject ScoresResultRoot;

	// Token: 0x040031D5 RID: 12757
	public UILabel ScoresResultLabel;

	// Token: 0x040031D6 RID: 12758
	public Transform BottomPosObj;

	// Token: 0x040031D7 RID: 12759
	public float TimeLimit;

	// Token: 0x040031D8 RID: 12760
	public float AddScores;

	// Token: 0x040031D9 RID: 12761
	public float MaxScoresAddRate;

	// Token: 0x040031DA RID: 12762
	public float ScoresAddRate;

	// Token: 0x040031DB RID: 12763
	public int ComboAddRateNum;

	// Token: 0x040031DC RID: 12764
	public float MaxGameVal;

	// Token: 0x040031DD RID: 12765
	public float GameAddVal;

	// Token: 0x040031DE RID: 12766
	public float GameReduceVal;

	// Token: 0x040031DF RID: 12767
	public float MinCZVal;

	// Token: 0x040031E0 RID: 12768
	public float MaxCZVal;

	// Token: 0x040031E1 RID: 12769
	public float MinScoreCZVal;

	// Token: 0x040031E2 RID: 12770
	public float MaxScoreCZVal;

	// Token: 0x040031E3 RID: 12771
	public int NormalManSound;

	// Token: 0x040031E4 RID: 12772
	public int ComboManSound;

	// Token: 0x040031E5 RID: 12773
	public int NormalWomenSound;

	// Token: 0x040031E6 RID: 12774
	public int ComboWomenSound;

	// Token: 0x040031E7 RID: 12775
	public int SoundClickNum;

	// Token: 0x040031E8 RID: 12776
	private float mCurScores;

	// Token: 0x040031E9 RID: 12777
	private float mCurScoresRate;

	// Token: 0x040031EA RID: 12778
	private float mCurRestTime;

	// Token: 0x040031EB RID: 12779
	private float mCurGameVal;

	// Token: 0x040031EC RID: 12780
	private int mCurComboVal;

	// Token: 0x040031ED RID: 12781
	private float PicLength;

	// Token: 0x040031EE RID: 12782
	private Transform SexGameCamPos;

	// Token: 0x040031EF RID: 12783
	private ParticleSystem SexGameParticleEffect;

	// Token: 0x040031F0 RID: 12784
	private bool mIsMan;

	// Token: 0x040031F1 RID: 12785
	private string activityId;

	// Token: 0x040031F2 RID: 12786
	private NpcData curNpcData;

	// Token: 0x040031F3 RID: 12787
	private int clickCount;

	// Token: 0x040031F4 RID: 12788
	private float screenWidth;

	// Token: 0x040031F5 RID: 12789
	private float mCurShowScores;

	// Token: 0x040031F6 RID: 12790
	private float mCurShowGameVal;

	// Token: 0x040031F7 RID: 12791
	private float changeValTime;

	// Token: 0x040031F8 RID: 12792
	private StringBuilder mScoresSb = new StringBuilder(512);

	// Token: 0x040031F9 RID: 12793
	private StringBuilder mTimeSb = new StringBuilder(512);

	// Token: 0x040031FA RID: 12794
	private float curLinePercent;

	// Token: 0x040031FB RID: 12795
	private bool mFinishFlag;

	// Token: 0x040031FC RID: 12796
	private bool mStartFlag;

	// Token: 0x040031FD RID: 12797
	private bool mPauseFlag;

	// Token: 0x040031FE RID: 12798
	private float clickStartTime;
}
