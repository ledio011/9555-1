using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008BA RID: 2234
public class SceneAnimationCtl : MonoBehaviour
{
	// Token: 0x06003C39 RID: 15417 RVA: 0x001073DC File Offset: 0x001055DC
	private void Start()
	{
		this.Init();
		this.PlaySceneAnima();
	}

	// Token: 0x06003C3A RID: 15418 RVA: 0x001073EC File Offset: 0x001055EC
	public void RegisterOnFinished(DelegateDefine.NoParamDelegate func = null)
	{
		this.onFinished = func;
	}

	// Token: 0x06003C3B RID: 15419 RVA: 0x001073F8 File Offset: 0x001055F8
	private void Init()
	{
		this.mCurSceneIndex = 0;
		this.mSceneCount = this.SubAnimaList.Count;
		for (int i = 0; i < this.SubAnimaList.Count; i++)
		{
			this.SubAnimaList[i].Init(new DelegateDefine.NoParamDelegate(this.OnSceneAnimaFinished));
			UnityVersionUtil.SetActiveRecursive(this.SubAnimaList[i].gameObject, false);
		}
	}

	// Token: 0x06003C3C RID: 15420 RVA: 0x00107470 File Offset: 0x00105670
	private void OnSceneAnimaFinished()
	{
		this.mCurSceneIndex++;
		if (this.mCurSceneIndex < this.mSceneCount)
		{
			this.PlaySceneAnima();
		}
		else
		{
			if (this.onFinished != null)
			{
				this.onFinished();
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003C3D RID: 15421 RVA: 0x001074C8 File Offset: 0x001056C8
	private void PlaySceneAnima()
	{
		this.SubAnimaList[this.mCurSceneIndex].StartScene();
	}

	// Token: 0x06003C3E RID: 15422 RVA: 0x001074E0 File Offset: 0x001056E0
	public void SkipAnima()
	{
		if (this.onFinished != null)
		{
			this.onFinished();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400276A RID: 10090
	public List<SceneSubAnimationCtl> SubAnimaList;

	// Token: 0x0400276B RID: 10091
	private int mCurSceneIndex;

	// Token: 0x0400276C RID: 10092
	private int mSceneCount;

	// Token: 0x0400276D RID: 10093
	private DelegateDefine.NoParamDelegate onFinished;
}
