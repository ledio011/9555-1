using System;

// Token: 0x020008EF RID: 2287
public class CopyFunctionRootLogic : SingletonUnity<CopyFunctionRootLogic>
{
	// Token: 0x06003DE6 RID: 15846 RVA: 0x00117D64 File Offset: 0x00115F64
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003DE7 RID: 15847 RVA: 0x00117D70 File Offset: 0x00115F70
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06003DE8 RID: 15848 RVA: 0x00117D90 File Offset: 0x00115F90
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06003DE9 RID: 15849 RVA: 0x00117D9C File Offset: 0x00115F9C
	public void Reset(MAPTYPE curMapType, string MapName)
	{
		this.curmaptype = curMapType;
		this.MapNameLabel.text = StrDictionary.GetDictionaryString(MapName, new object[0]);
		if (curMapType == MAPTYPE.CAR_CHASE_COPY)
		{
			NGUITools.SetActive(this.AutoFightBtn.gameObject, false);
		}
		else if (curMapType == MAPTYPE.TUTORIAL_CAR)
		{
			NGUITools.SetActive(this.AutoFightBtn.gameObject, false);
			NGUITools.SetActive(this.ExitbtnSprite.gameObject, false);
		}
		else
		{
			this.UpdateAutoFightBtn();
		}
	}

	// Token: 0x06003DEA RID: 15850 RVA: 0x00117E1C File Offset: 0x0011601C
	public void OnClickLeaveCopyBtn()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.LeaveScene();
		if (this.curmaptype != MAPTYPE.INVALID)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("copyleave", "leavecopy", string.Format("maptype_{0}", (int)this.curmaptype));
		}
	}

	// Token: 0x06003DEB RID: 15851 RVA: 0x00117E70 File Offset: 0x00116070
	public void UpdateAutoFightBtn()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer == null)
		{
			return;
		}
		if (mainPlayer.IsOpenAutoCombat)
		{
			this.AutoBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_ZiDong_2";
			this.AutoEffect.alpha = 1f;
		}
		else
		{
			this.AutoBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_ZiDong";
			this.AutoEffect.alpha = 0f;
		}
		this.AutoFightBtn.ResetToBeginning();
		this.AutoFightBtn.enabled = mainPlayer.IsOpenAutoCombat;
	}

	// Token: 0x06003DEC RID: 15852 RVA: 0x00117F04 File Offset: 0x00116104
	public void OnClickAutoFightBtn()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.AUTO_FIGHT_CLICK)
		{
			this.CheckTutorialEvent();
		}
		if (!mainPlayer.IsOpenAutoCombat)
		{
			mainPlayer.EnterAutoCombat();
		}
		else
		{
			mainPlayer.LeveAutoCombat();
		}
		this.UpdateAutoFightBtn();
	}

	// Token: 0x04002989 RID: 10633
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400298A RID: 10634
	public TweenScale AutoFightBtn;

	// Token: 0x0400298B RID: 10635
	public UISprite AutoBtnSprite;

	// Token: 0x0400298C RID: 10636
	public UISprite ExitbtnSprite;

	// Token: 0x0400298D RID: 10637
	public UISprite AutoEffect;

	// Token: 0x0400298E RID: 10638
	public UILabel MapNameLabel;

	// Token: 0x0400298F RID: 10639
	private MAPTYPE curmaptype = MAPTYPE.INVALID;
}
