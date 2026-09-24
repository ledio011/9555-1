using System;

// Token: 0x020008CC RID: 2252
public class BlackScreenLogic : SingletonUnity<BlackScreenLogic>
{
	// Token: 0x06003CAC RID: 15532 RVA: 0x0010AF4C File Offset: 0x0010914C
	public static void Close(float duration = 0.5f, DelegateDefine.NoParamDelegate func = null)
	{
		if (SingletonUnity<BlackScreenLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<BlackScreenLogic>.Instance.gameObject))
		{
			SingletonUnity<BlackScreenLogic>.Instance.CloseScreen(duration, func);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BlackScreenRoot, delegate
			{
				SingletonUnity<BlackScreenLogic>.Instance.CloseScreen(duration, func);
			}, null);
		}
	}

	// Token: 0x06003CAD RID: 15533 RVA: 0x0010AFC4 File Offset: 0x001091C4
	public static void Open(float duration = 0.5f, DelegateDefine.NoParamDelegate func = null)
	{
		if (SingletonUnity<BlackScreenLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<BlackScreenLogic>.Instance.gameObject))
		{
			SingletonUnity<BlackScreenLogic>.Instance.OpenScreen(duration, func);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BlackScreenRoot, delegate
			{
				SingletonUnity<BlackScreenLogic>.Instance.OpenScreen(duration, func);
			}, null);
		}
	}

	// Token: 0x06003CAE RID: 15534 RVA: 0x0010B03C File Offset: 0x0010923C
	public void CloseScreen(float duration = 0.5f, DelegateDefine.NoParamDelegate func = null)
	{
		if (!UnityVersionUtil.IsActive(this.tweenAlph.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.tweenAlph.gameObject, true);
		}
		this.tweenAlph.duration = duration;
		this.tweenAlph.PlayForward();
		this.isOpen = true;
		this.onTweenFinished = func;
	}

	// Token: 0x06003CAF RID: 15535 RVA: 0x0010B094 File Offset: 0x00109294
	public void OpenScreen(float duration = 0.5f, DelegateDefine.NoParamDelegate func = null)
	{
		this.tweenAlph.duration = duration;
		this.tweenAlph.PlayReverse();
		this.isOpen = false;
		this.onTweenFinished = func;
	}

	// Token: 0x06003CB0 RID: 15536 RVA: 0x0010B0BC File Offset: 0x001092BC
	public void OnTweenFinished()
	{
		if (!this.isOpen)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BlackScreenRoot);
		}
		if (this.onTweenFinished != null)
		{
			this.onTweenFinished();
			this.onTweenFinished = null;
		}
	}

	// Token: 0x040027E7 RID: 10215
	public TweenAlpha tweenAlph;

	// Token: 0x040027E8 RID: 10216
	private bool isOpen;

	// Token: 0x040027E9 RID: 10217
	private DelegateDefine.NoParamDelegate onTweenFinished;
}
