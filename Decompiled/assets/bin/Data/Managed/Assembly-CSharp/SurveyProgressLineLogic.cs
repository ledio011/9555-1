using System;
using UnityEngine;

// Token: 0x020009B6 RID: 2486
public class SurveyProgressLineLogic : SingletonUnity<SurveyProgressLineLogic>
{
	// Token: 0x060046BD RID: 18109 RVA: 0x00166FB4 File Offset: 0x001651B4
	public void Reset(SurveyMissionData surveyMissionData, int needNum)
	{
		this.mUsingTime = 0f;
		this.ProgressSlider.value = 0f;
		this.mCurCount = 0;
		this.mSurveyTime = surveyMissionData.SurveyTimeSecond;
		this.mSurveyCount = surveyMissionData.Count;
		this.mNeedSurveyCount = needNum;
		this.TextLabel.text = StrDictionary.GetDictionaryString(surveyMissionData.Text, new object[0]);
		this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.mainPlayer.IsTalking = true;
		if (this.mainPlayer.IsDrivingMount())
		{
			this.mainPlayer.SendServerDisMountCar();
		}
		this.mainPlayer.AnimationLogic.ForcePlayAnimation("caiJi", null, -1f, 0f);
	}

	// Token: 0x060046BE RID: 18110 RVA: 0x00167078 File Offset: 0x00165278
	private void Update()
	{
		this.mUsingTime += Time.deltaTime;
		if (this.mUsingTime < this.mSurveyTime)
		{
			this.mSurveyPercent = this.mUsingTime / this.mSurveyTime;
			this.ProgressSlider.value = this.mSurveyPercent;
		}
		else
		{
			this.mCurCount++;
			Singleton<SurveyItemManager>.Instance.FinishSurveyItem();
			if (this.mCurCount >= this.mSurveyCount || this.mCurCount >= this.mNeedSurveyCount)
			{
				this.StopSurveyItem();
			}
			else
			{
				this.mUsingTime = 0f;
				this.ProgressSlider.value = 0f;
				this.mainPlayer.AnimationLogic.ForcePlayAnimation("caiJi", null, -1f, 0f);
			}
		}
	}

	// Token: 0x060046BF RID: 18111 RVA: 0x00167154 File Offset: 0x00165354
	public void StopSurveyItem()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SurveyProgressLine);
		this.mainPlayer.IsTalking = false;
		Singleton<SurveyItemManager>.Instance.CompleteSurveyItem();
		this.mainPlayer.CurAnimationState = GameDefine.ANIMATIONSTATE.IDLE;
	}

	// Token: 0x040033C9 RID: 13257
	public UISlider ProgressSlider;

	// Token: 0x040033CA RID: 13258
	public UILabel TextLabel;

	// Token: 0x040033CB RID: 13259
	private float mSurveyTime;

	// Token: 0x040033CC RID: 13260
	private float mUsingTime;

	// Token: 0x040033CD RID: 13261
	private float mSurveyPercent;

	// Token: 0x040033CE RID: 13262
	private int mCurCount;

	// Token: 0x040033CF RID: 13263
	private int mSurveyCount;

	// Token: 0x040033D0 RID: 13264
	private int mNeedSurveyCount;

	// Token: 0x040033D1 RID: 13265
	private ObjMainPlayer mainPlayer;
}
