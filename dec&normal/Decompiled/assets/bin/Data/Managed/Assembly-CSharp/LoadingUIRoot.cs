using System;
using System.Collections;
using UnityEngine;

// Token: 0x020009EA RID: 2538
public class LoadingUIRoot : SingletonUnity<LoadingUIRoot>
{
	// Token: 0x06004840 RID: 18496 RVA: 0x001726F8 File Offset: 0x001708F8
	private void OnEnable()
	{
		Texture nextLoadingTexture = BundleManager.GetNextLoadingTexture();
		if (nextLoadingTexture != null && !this.LoadTexture.mainTexture.name.Equals(nextLoadingTexture.name))
		{
			this.LoadTexture.mainTexture = nextLoadingTexture;
		}
	}

	// Token: 0x06004841 RID: 18497 RVA: 0x00172744 File Offset: 0x00170944
	private void SetProgressLineWidth(int width)
	{
		this.ProgressLine.width = width;
		this.ProressLineAnimaObj.transform.localPosition = new Vector3((float)width, 0f, 0f);
	}

	// Token: 0x06004842 RID: 18498 RVA: 0x00172780 File Offset: 0x00170980
	private void Start()
	{
		if (LoadingWindow.BlackLoading)
		{
			UnityVersionUtil.SetActiveRecursive(this.BlackTopPic.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.PicLoadingRoot.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.BlackTopPic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.PicLoadingRoot.gameObject, true);
		}
		this.ProgressLine.transform.localPosition = new Vector3((float)(-(float)this.BottomLine.width / 2), 0f, 0f);
		this.mTargetProgress = 1f;
		this.mCurProgress = LoadingWindow.LoadingProgress;
		this.SetTipStr();
		this.mMapReadyFlag = false;
	}

	// Token: 0x06004843 RID: 18499 RVA: 0x00172834 File Offset: 0x00170A34
	private void Update()
	{
		if (this.mCurProgress <= this.mTargetProgress)
		{
			this.mCurProgress += LoadingWindow.LoadingSpeed;
			if (!GameManager.IsSceneReady)
			{
				if (this.mCurProgress > 0.9f)
				{
					this.mCurProgress = 0.9f;
				}
			}
			else if (this.mCurProgress > 0.9f && !this.mMapReadyFlag && SingletonUnity<MyEvent>.Exists)
			{
				this.mMapReadyFlag = true;
				if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
				{
					base.StartCoroutine(this.OnLoadingOver());
				}
			}
			if (LoadingWindow.BlackLoading)
			{
				this.BlackTopPic.alpha = 1f - this.mCurProgress;
			}
			else
			{
				this.SetProgressLineWidth((int)((float)this.BottomLine.width * this.mCurProgress));
			}
			if (this.mCurProgress >= 1f)
			{
				LoadingUIRoot.TipsIndex = -1;
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LoadingUIRoot);
				if (LoadingWindow.BlackLoading)
				{
					LoadingWindow.BlackLoading = false;
				}
				SingletonDontDestoryUnity<GameManager>.Instance.LoadNextLoadingTexture();
			}
		}
	}

	// Token: 0x06004844 RID: 18500 RVA: 0x00172958 File Offset: 0x00170B58
	private IEnumerator OnLoadingOver()
	{
		yield return null;
		SingletonUnity<MyEvent>.Instance.Fire("OnLoadingOver", new object[0]);
		yield break;
	}

	// Token: 0x06004845 RID: 18501 RVA: 0x0017296C File Offset: 0x00170B6C
	public void SetProgress(float progress)
	{
		this.mTargetProgress = progress;
	}

	// Token: 0x06004846 RID: 18502 RVA: 0x00172978 File Offset: 0x00170B78
	public void SetTipStr()
	{
		if (GameSettingData.IsLowPhone)
		{
			return;
		}
		if (LoadingUIRoot.TipsIndex == -1)
		{
			LoadingUIRoot.TipsIndex = Random.Range(0, GameDefine.LoadingTips.Length);
		}
		string keystr = GameDefine.LoadingTips[LoadingUIRoot.TipsIndex];
		this.TipsLabel.text = StrDictionary.GetDictionaryString(keystr, new object[0]);
	}

	// Token: 0x04003597 RID: 13719
	public UILabel TipsLabel;

	// Token: 0x04003598 RID: 13720
	public UISprite ProgressLine;

	// Token: 0x04003599 RID: 13721
	public UISprite BottomLine;

	// Token: 0x0400359A RID: 13722
	private float mCurProgress;

	// Token: 0x0400359B RID: 13723
	private float mTargetProgress;

	// Token: 0x0400359C RID: 13724
	public UISprite BlackTopPic;

	// Token: 0x0400359D RID: 13725
	public GameObject PicLoadingRoot;

	// Token: 0x0400359E RID: 13726
	public static int TipsIndex = -1;

	// Token: 0x0400359F RID: 13727
	public UITexture LoadTexture;

	// Token: 0x040035A0 RID: 13728
	private bool mMapReadyFlag;

	// Token: 0x040035A1 RID: 13729
	public GameObject ProressLineAnimaObj;
}
