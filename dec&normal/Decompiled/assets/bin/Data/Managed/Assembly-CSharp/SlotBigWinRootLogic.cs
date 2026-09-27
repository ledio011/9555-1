using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000999 RID: 2457
public class SlotBigWinRootLogic : SingletonUnity<SlotBigWinRootLogic>
{
	// Token: 0x060045A3 RID: 17827 RVA: 0x0015EC10 File Offset: 0x0015CE10
	public void Reset(List<item> items, DelegateDefine.NoParamDelegate okfun = null)
	{
		this.mShowRewardFlag = true;
		this.mStartTime = Time.time;
		this.ShowRewardItem.ShowRewards(items);
		this.onClickOK = okfun;
		UnityVersionUtil.SetActiveRecursive(this.uiObj, false);
		this.InitTexture();
		vp_Timer.In(1f, delegate()
		{
			this.setbegin();
		}, null);
	}

	// Token: 0x060045A4 RID: 17828 RVA: 0x0015EC6C File Offset: 0x0015CE6C
	public void setbegin()
	{
		UnityVersionUtil.SetActiveRecursive(this.uiObj, true);
	}

	// Token: 0x060045A5 RID: 17829 RVA: 0x0015EC7C File Offset: 0x0015CE7C
	public void InitTexture()
	{
		List<string> list = new List<string>();
		if (this.BigWinTexture.mainTexture == null)
		{
			list.Add(GameDefine.SlotBigWin);
		}
		if (this.BigWinLineTexture.mainTexture == null)
		{
			list.Add(GameDefine.SlotBigWinBian);
		}
		if (list.Count == 0)
		{
			return;
		}
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060045A6 RID: 17830 RVA: 0x0015ED08 File Offset: 0x0015CF08
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			return;
		}
		if (retdic.ContainsKey(GameDefine.SlotBigWin))
		{
			this.BigWinTexture.mainTexture = retdic[GameDefine.SlotBigWin];
		}
		if (retdic.ContainsKey(GameDefine.SlotBigWinBian))
		{
			this.BigWinLineTexture.mainTexture = retdic[GameDefine.SlotBigWinBian];
		}
	}

	// Token: 0x060045A7 RID: 17831 RVA: 0x0015ED70 File Offset: 0x0015CF70
	private void Update()
	{
		if (this.mShowRewardFlag)
		{
			this.mTimeCount = Time.time - this.mStartTime;
			if (this.mTimeCount >= (float)this.mWaitCloseTime)
			{
				this.mShowRewardFlag = false;
			}
		}
	}

	// Token: 0x060045A8 RID: 17832 RVA: 0x0015EDB4 File Offset: 0x0015CFB4
	public void OnClickOKBtn()
	{
		if (this.mShowRewardFlag)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SlotBigWinRoot);
		if (this.onClickOK != null)
		{
			this.onClickOK();
		}
	}

	// Token: 0x0400329A RID: 12954
	public ShowRewardItems ShowRewardItem;

	// Token: 0x0400329B RID: 12955
	public GameObject uiObj;

	// Token: 0x0400329C RID: 12956
	public UITexture BigWinTexture;

	// Token: 0x0400329D RID: 12957
	public UITexture BigWinLineTexture;

	// Token: 0x0400329E RID: 12958
	private bool mShowRewardFlag;

	// Token: 0x0400329F RID: 12959
	private float mStartTime;

	// Token: 0x040032A0 RID: 12960
	private float mTimeCount;

	// Token: 0x040032A1 RID: 12961
	private int mWaitCloseTime = 1;

	// Token: 0x040032A2 RID: 12962
	private DelegateDefine.NoParamDelegate onClickOK;
}
