using System;
using UnityEngine;

// Token: 0x02000A63 RID: 2659
public class WaitResponseUIRootLogic : SingletonUnity<WaitResponseUIRootLogic>
{
	// Token: 0x06004D99 RID: 19865 RVA: 0x001A88B4 File Offset: 0x001A6AB4
	public void Clear()
	{
		this.mWaitTime = 0f;
		this.mDealyTime = -1f;
	}

	// Token: 0x06004D9A RID: 19866 RVA: 0x001A88CC File Offset: 0x001A6ACC
	public void HideBox()
	{
		if (this.mDetailObject != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mDetailObject, false);
		}
	}

	// Token: 0x06004D9B RID: 19867 RVA: 0x001A88EC File Offset: 0x001A6AEC
	private void ShowBox()
	{
		if (this.mDetailObject != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mDetailObject, true);
		}
	}

	// Token: 0x06004D9C RID: 19868 RVA: 0x001A890C File Offset: 0x001A6B0C
	public void ResetBoxInfo(int type)
	{
	}

	// Token: 0x06004D9D RID: 19869 RVA: 0x001A8910 File Offset: 0x001A6B10
	public static void WaitTimeOut()
	{
		NoticeLogic.AddNotifyData("#{100786}", true, false);
	}

	// Token: 0x06004D9E RID: 19870 RVA: 0x001A8920 File Offset: 0x001A6B20
	public static void OpenWaitBox(int type, float duration = 10f, float delay = 0f, WaitResponseUIRootLogic.OnWaitTimeOut delWaitout = null)
	{
		WaitResponseUIRootLogic.CloseBox();
		WaitResponseUIRootLogic.WaitResponseBoxInfo param = new WaitResponseUIRootLogic.WaitResponseBoxInfo(type, delay, duration, delWaitout);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.WaitResponseRoot, new UIManager.OnOpenUIDelegate(WaitResponseUIRootLogic.OnOpenWaitBox), param);
	}

	// Token: 0x06004D9F RID: 19871 RVA: 0x001A8958 File Offset: 0x001A6B58
	private static void OnOpenWaitBox(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		WaitResponseUIRootLogic.WaitResponseBoxInfo waitResponseBoxInfo = obj as WaitResponseUIRootLogic.WaitResponseBoxInfo;
		if (waitResponseBoxInfo != null)
		{
			SingletonUnity<WaitResponseUIRootLogic>.Instance.Clear();
			SingletonUnity<WaitResponseUIRootLogic>.Instance.onWaitTimeOut = waitResponseBoxInfo.mDelWaitOut;
			if (waitResponseBoxInfo.mDelWaitOut == null)
			{
				SingletonUnity<WaitResponseUIRootLogic>.Instance.onWaitTimeOut = new WaitResponseUIRootLogic.OnWaitTimeOut(WaitResponseUIRootLogic.WaitTimeOut);
			}
			SingletonUnity<WaitResponseUIRootLogic>.Instance.mWaitTime = waitResponseBoxInfo.mDuration;
			SingletonUnity<WaitResponseUIRootLogic>.Instance.mDealyTime = waitResponseBoxInfo.mDelay;
			SingletonUnity<WaitResponseUIRootLogic>.Instance.ResetBoxInfo(waitResponseBoxInfo.mType);
			if (waitResponseBoxInfo.mDelay > 0f)
			{
				SingletonUnity<WaitResponseUIRootLogic>.Instance.HideBox();
			}
		}
	}

	// Token: 0x06004DA0 RID: 19872 RVA: 0x001A8A00 File Offset: 0x001A6C00
	public static void CloseBox()
	{
		if (SingletonUnity<WaitResponseUIRootLogic>.Exists)
		{
			SingletonUnity<WaitResponseUIRootLogic>.Instance.Clear();
		}
		if (SingletonUnity<UIManager>.Exists)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WaitResponseRoot);
		}
	}

	// Token: 0x06004DA1 RID: 19873 RVA: 0x001A8A3C File Offset: 0x001A6C3C
	private void Start()
	{
	}

	// Token: 0x06004DA2 RID: 19874 RVA: 0x001A8A40 File Offset: 0x001A6C40
	private void Update()
	{
		if (this.mDealyTime > 0f)
		{
			this.mDealyTime -= Time.deltaTime;
			if (this.mDealyTime <= 0f)
			{
				this.ShowBox();
			}
			return;
		}
		if (this.mWaitTime > 0f)
		{
			this.mWaitTime -= Time.deltaTime;
			if (this.mWaitTime <= 0f)
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WaitResponseRoot);
				if (this.onWaitTimeOut != null)
				{
					this.onWaitTimeOut();
				}
			}
		}
	}

	// Token: 0x04003C56 RID: 15446
	public GameObject mDetailObject;

	// Token: 0x04003C57 RID: 15447
	public float mWaitTime;

	// Token: 0x04003C58 RID: 15448
	public float mDealyTime;

	// Token: 0x04003C59 RID: 15449
	private WaitResponseUIRootLogic.OnWaitTimeOut onWaitTimeOut;

	// Token: 0x02000A64 RID: 2660
	private class WaitResponseBoxInfo
	{
		// Token: 0x06004DA3 RID: 19875 RVA: 0x001A8AE0 File Offset: 0x001A6CE0
		public WaitResponseBoxInfo(int type, float delay, float duration, WaitResponseUIRootLogic.OnWaitTimeOut delWaitOut)
		{
			this.mType = type;
			this.mDelay = delay;
			this.mDuration = duration;
			this.mDelWaitOut = delWaitOut;
		}

		// Token: 0x04003C5A RID: 15450
		public int mType;

		// Token: 0x04003C5B RID: 15451
		public string mText;

		// Token: 0x04003C5C RID: 15452
		public float mDelay;

		// Token: 0x04003C5D RID: 15453
		public float mDuration;

		// Token: 0x04003C5E RID: 15454
		public WaitResponseUIRootLogic.OnWaitTimeOut mDelWaitOut;
	}

	// Token: 0x02000B08 RID: 2824
	// (Invoke) Token: 0x060050A9 RID: 20649
	public delegate void OnWaitTimeOut();
}
