using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008EB RID: 2283
public class TimerActivityTipsRootLogic : SingletonUnity<TimerActivityTipsRootLogic>
{
	// Token: 0x06003DCA RID: 15818 RVA: 0x001170A4 File Offset: 0x001152A4
	public void Reset()
	{
		this.mCurActivityDataList = DataManager.GetTimerActivityTipsDataList();
		this.InitTexture();
		this.ShowPage(0);
	}

	// Token: 0x06003DCB RID: 15819 RVA: 0x001170C0 File Offset: 0x001152C0
	private void ShowPage(int showIndex)
	{
		this.mCurPageIndex = showIndex;
		this.mCurActivityData = this.mCurActivityDataList[this.mCurPageIndex];
		if (this.mCurPageIndex < this.mTextureDic.Count)
		{
			this.ActivityPic.mainTexture = this.mTextureDic[this.mCurActivityData.PicName];
			this.ActivityPic.SetDimensions(this.ActivityPic.mainTexture.width, this.ActivityPic.mainTexture.height);
		}
		if (this.mCurActivityData.JumpType == -1)
		{
			NGUITools.SetActive(this.JumpBtnRoot, false);
		}
		else
		{
			NGUITools.SetActive(this.JumpBtnRoot, true);
		}
	}

	// Token: 0x06003DCC RID: 15820 RVA: 0x0011717C File Offset: 0x0011537C
	public void InitTexture()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.mCurActivityDataList.Count; i++)
		{
			if (!string.IsNullOrEmpty(this.mCurActivityDataList[i].PicName))
			{
				list.Add(this.mCurActivityDataList[i].PicName);
			}
		}
		if (list.Count > 0)
		{
			if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
			}
		}
		else
		{
			this.OnClickCloseBtn();
		}
	}

	// Token: 0x06003DCD RID: 15821 RVA: 0x00117220 File Offset: 0x00115420
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			this.OnClickCloseBtn();
			return;
		}
		this.mTextureDic = retdic;
		if (this.mCurActivityData != null)
		{
			this.ActivityPic.mainTexture = this.mTextureDic[this.mCurActivityData.PicName];
			this.ActivityPic.SetDimensions(this.ActivityPic.mainTexture.width, this.ActivityPic.mainTexture.height);
		}
		else
		{
			this.OnClickCloseBtn();
		}
	}

	// Token: 0x06003DCE RID: 15822 RVA: 0x001172A8 File Offset: 0x001154A8
	private bool IsHaveNextPage()
	{
		return this.mCurPageIndex + 1 < this.mCurActivityDataList.Count;
	}

	// Token: 0x06003DCF RID: 15823 RVA: 0x001172C8 File Offset: 0x001154C8
	public void OnClickCloseBtn()
	{
		if (this.IsHaveNextPage())
		{
			this.ShowPage(this.mCurPageIndex + 1);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TimerActivityTipsRoot);
			if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
			{
				SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
			}
		}
	}

	// Token: 0x06003DD0 RID: 15824 RVA: 0x0011732C File Offset: 0x0011552C
	public void OnClickJumpBtn()
	{
		if (this.mCurActivityData.JumpType == 1)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TimerActivityTipsRoot);
			if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
			{
				SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
			}
			if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
			{
				SingletonUnity<AutoPopUIRoot>.Instance.NextPop(true);
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn(GameDefine.SHOP_TAB_TYPE.FASHION, GameDefine.UIBACKTYPE.NOTHINTG, null);
			}, null);
		}
	}

	// Token: 0x06003DD1 RID: 15825 RVA: 0x001173D8 File Offset: 0x001155D8
	private void OnDisable()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.mCurActivityDataList.Count; i++)
		{
			list.Add(this.mCurActivityDataList[i].PicName);
		}
		BundleManager.UnloadTexture(list);
	}

	// Token: 0x04002965 RID: 10597
	public GameObject JumpBtnRoot;

	// Token: 0x04002966 RID: 10598
	public UILabel JumpBtnLabel;

	// Token: 0x04002967 RID: 10599
	public UITexture ActivityPic;

	// Token: 0x04002968 RID: 10600
	private int mCurPageIndex;

	// Token: 0x04002969 RID: 10601
	private List<TimerActivityTipsData> mCurActivityDataList = new List<TimerActivityTipsData>();

	// Token: 0x0400296A RID: 10602
	private TimerActivityTipsData mCurActivityData;

	// Token: 0x0400296B RID: 10603
	private Dictionary<string, Texture> mTextureDic = new Dictionary<string, Texture>();
}
