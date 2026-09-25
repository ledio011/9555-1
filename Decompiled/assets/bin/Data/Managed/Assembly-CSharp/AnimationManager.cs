using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
public class AnimationManager
{
	// Token: 0x0600118C RID: 4492 RVA: 0x00071CC8 File Offset: 0x0006FEC8
	public static void initStreamAnimationData(MonoBehaviour mono)
	{
		if (AnimationManager.initStreamFlag)
		{
			return;
		}
		AnimationManager.initStreamFlag = true;
		AnimationManager.initStreamDoneFlag = false;
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadStreamAnimationBundle(new BundleManager.OnLoadAnimationFinishedDelegate(AnimationManager.StreamAnimationFinish)));
		}
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x00071D14 File Offset: 0x0006FF14
	private static void StreamAnimationFinish()
	{
		AnimationManager.initStreamDoneFlag = true;
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x00071D1C File Offset: 0x0006FF1C
	public static void initDownloadAnimationData(MonoBehaviour mono)
	{
		if (AnimationManager.initDownloadFlag)
		{
			return;
		}
		AnimationManager.initDownloadFlag = true;
		AnimationManager.initDownloadDoneFlag = false;
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadDownloadAnimationBundle(new BundleManager.OnLoadAnimationFinishedDelegate(AnimationManager.DownloadAnimationFinish)));
		}
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x00071D68 File Offset: 0x0006FF68
	private static void DownloadAnimationFinish()
	{
		AnimationManager.initDownloadDoneFlag = true;
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x00071D70 File Offset: 0x0006FF70
	public static void ReImportDownloadAnimationData(MonoBehaviour mono)
	{
		AnimationManager.initDownloadFlag = true;
		AnimationManager.initDownloadDoneFlag = false;
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadDownloadAnimationBundle(new BundleManager.OnLoadAnimationFinishedDelegate(AnimationManager.DownloadAnimationFinish)));
		}
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x00071DB4 File Offset: 0x0006FFB4
	public static Object LoadAnimation(string modelname, string actionname)
	{
		string path = string.Format("Animation/{0}/{1}", modelname, actionname);
		Object @object = ResourcesManager.LoadAnimation(path);
		if (@object == null)
		{
			@object = BundleManager.LoadAnimation(modelname, actionname);
		}
		return @object;
	}

	// Token: 0x04001769 RID: 5993
	public static bool initStreamFlag;

	// Token: 0x0400176A RID: 5994
	public static bool initStreamDoneFlag;

	// Token: 0x0400176B RID: 5995
	public static bool initDownloadFlag;

	// Token: 0x0400176C RID: 5996
	public static bool initDownloadDoneFlag;
}
