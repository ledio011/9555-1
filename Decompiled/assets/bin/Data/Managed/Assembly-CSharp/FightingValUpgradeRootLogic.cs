using System;
using UnityEngine;

// Token: 0x0200093E RID: 2366
public class FightingValUpgradeRootLogic : SingletonUnity<FightingValUpgradeRootLogic>
{
	// Token: 0x060041D2 RID: 16850 RVA: 0x0013A6F0 File Offset: 0x001388F0
	public static void ShowFinghtingValUpgradeRoot(long preVal, long targetVal)
	{
		if (SingletonUnity<FightingValUpgradeRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FightingValUpgradeRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FightingValUpgradeRootLogic>.Instance.Reset(preVal, targetVal);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FightingValUpgradeRoot, delegate
			{
				SingletonUnity<FightingValUpgradeRootLogic>.Instance.Reset(preVal, targetVal);
			}, null);
		}
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x0013A768 File Offset: 0x00138968
	public void Reset(long preVal, long targetVal)
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(17, 1f, null);
		this.mPreVal = preVal;
		this.mTargetVal = targetVal;
		this.FightingValLabel.text = this.mPreVal.ToString();
		this.ChangeValLabel.text = (this.mTargetVal - this.mPreVal).ToString();
		this.mStartTime = Time.time;
		this.mChangePercent = 0f;
		for (int i = 0; i < this.TweenList.Length; i++)
		{
			this.TweenList[i].enabled = true;
			this.TweenList[i].ResetToBeginning();
			this.TweenList[i].PlayForward();
		}
		this.ParticleEffect.Clear();
		this.ParticleEffect.Play(true);
	}

	// Token: 0x060041D4 RID: 16852 RVA: 0x0013A83C File Offset: 0x00138A3C
	private void Update()
	{
		if (Time.time > this.mStartTime + this.mLastTime)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FightingValUpgradeRoot);
			return;
		}
		this.mChangePercent = (Time.time - this.mStartTime) / this.mChangeTime;
		if (this.mChangePercent >= 1f)
		{
			if (this.TweenList[0].enabled)
			{
				for (int i = 0; i < this.TweenList.Length; i++)
				{
					this.TweenList[i].enabled = false;
				}
				this.FightingValLabel.text = this.mTargetVal.ToString();
				this.FightingValLabel.transform.localScale = Vector3.one;
			}
			return;
		}
		this.FightingValLabel.text = ((int)Mathf.Lerp((float)this.mPreVal, (float)this.mTargetVal, this.mChangePercent)).ToString();
	}

	// Token: 0x04002DBE RID: 11710
	public UILabel FightingValLabel;

	// Token: 0x04002DBF RID: 11711
	public UILabel ChangeValLabel;

	// Token: 0x04002DC0 RID: 11712
	public UITweener[] TweenList;

	// Token: 0x04002DC1 RID: 11713
	public ParticleSystem ParticleEffect;

	// Token: 0x04002DC2 RID: 11714
	private long mPreVal;

	// Token: 0x04002DC3 RID: 11715
	private long mTargetVal;

	// Token: 0x04002DC4 RID: 11716
	private float mChangeTime = 1f;

	// Token: 0x04002DC5 RID: 11717
	private float mLastTime = 2f;

	// Token: 0x04002DC6 RID: 11718
	private float mStartTime;

	// Token: 0x04002DC7 RID: 11719
	private float mChangePercent;
}
