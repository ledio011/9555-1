using System;
using UnityEngine;

// Token: 0x020009B7 RID: 2487
public class SurviveBattleFloorInfoRootLogic : SingletonUnity<SurviveBattleFloorInfoRootLogic>
{
	// Token: 0x060046C1 RID: 18113 RVA: 0x0016719C File Offset: 0x0016539C
	public void Reset(int floorid, int scores)
	{
		this.EffectPic.enabled = false;
		if (floorid == 0)
		{
			this.TypeLabel.text = StrDictionary.GetDictionaryString("#{101583}", new object[0]);
			this.BtnPic.spriteName = "CZ_tuBiao_Up";
			this.BtnSprite.color = new Color(1f, 0.85882354f, 0f, 1f);
		}
		else
		{
			this.TypeLabel.text = StrDictionary.GetDictionaryString("#{101584}", new object[0]);
			this.BtnPic.spriteName = "CZ_tuBiao_Down";
			this.BtnSprite.color = Color.white;
		}
		this.ScoreLabel.text = scores.ToString();
		this.DisableHandTip();
	}

	// Token: 0x060046C2 RID: 18114 RVA: 0x00167264 File Offset: 0x00165464
	public void UpdateBtnEnable(bool isEnable)
	{
		this.TwPosition.enabled = isEnable;
		if (isEnable)
		{
			this.BtnSprite.color = new Color(1f, 0.85882354f, 0f, 1f);
			this.EffectPic.enabled = true;
			this.EnableHandTip();
		}
		else
		{
			this.BtnSprite.color = Color.white;
			this.EffectPic.enabled = false;
			this.DisableHandTip();
		}
	}

	// Token: 0x060046C3 RID: 18115 RVA: 0x001672E0 File Offset: 0x001654E0
	public void EnableHandTip()
	{
		if (!UnityVersionUtil.IsActive(this.HandTipRoot))
		{
			NGUITools.SetActive(this.HandTipRoot, true);
		}
	}

	// Token: 0x060046C4 RID: 18116 RVA: 0x00167300 File Offset: 0x00165500
	public void DisableHandTip()
	{
		if (UnityVersionUtil.IsActive(this.HandTipRoot))
		{
			NGUITools.SetActive(this.HandTipRoot, false);
		}
	}

	// Token: 0x060046C5 RID: 18117 RVA: 0x00167320 File Offset: 0x00165520
	public void OnClickBtn()
	{
		(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as SurvivalBattleSceneManager).MoveToNextFloor();
	}

	// Token: 0x040033D2 RID: 13266
	public UILabel TypeLabel;

	// Token: 0x040033D3 RID: 13267
	public UILabel ScoreLabel;

	// Token: 0x040033D4 RID: 13268
	public UISprite BtnPic;

	// Token: 0x040033D5 RID: 13269
	public TweenPosition TwPosition;

	// Token: 0x040033D6 RID: 13270
	public UISprite BtnSprite;

	// Token: 0x040033D7 RID: 13271
	public UITexture EffectPic;

	// Token: 0x040033D8 RID: 13272
	public GameObject HandTipRoot;
}
