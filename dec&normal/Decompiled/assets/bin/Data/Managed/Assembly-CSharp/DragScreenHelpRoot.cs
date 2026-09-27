using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000A4E RID: 2638
public class DragScreenHelpRoot : SingletonUnity<DragScreenHelpRoot>
{
	// Token: 0x06004CDD RID: 19677 RVA: 0x001A1410 File Offset: 0x0019F610
	private void Start()
	{
		this.screenWidth = Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
	}

	// Token: 0x06004CDE RID: 19678 RVA: 0x001A1430 File Offset: 0x0019F630
	public void SetTargetObj(GameObject target)
	{
		this.mTargetObj = target;
		NGUITools.SetActive(this.HandObj, false);
		this.mMainCamera = Camera.main;
	}

	// Token: 0x06004CDF RID: 19679 RVA: 0x001A1450 File Offset: 0x0019F650
	private void Update()
	{
		if (this.mTargetObj == null)
		{
			return;
		}
		Vector3 vector = this.mMainCamera.WorldToViewportPoint(this.mTargetObj.transform.position + Vector3.up * 0.5f);
		Vector3 zero = Vector3.zero;
		if (vector.z > 0f)
		{
			zero..ctor(Mathf.Clamp(vector.x * (float)this.screenWidth, 30f, (float)(this.screenWidth - 30)), Mathf.Clamp(vector.y * 480f, 10f, 470f), 0f);
		}
		else
		{
			vector.x = 1f - vector.x;
			zero..ctor(Mathf.Clamp(vector.x * (float)this.screenWidth, 30f, (float)(this.screenWidth - 30)), 10f, 0f);
		}
		if (zero.x >= (float)(this.screenWidth - 30))
		{
			this.DragRight();
		}
		else if (zero.x <= 30f)
		{
			this.DragLeft();
		}
		else
		{
			this.DisableTip();
		}
	}

	// Token: 0x06004CE0 RID: 19680 RVA: 0x001A1594 File Offset: 0x0019F794
	private void DragRight()
	{
		if (!UnityVersionUtil.IsActive(this.HandObj))
		{
			NGUITools.SetActive(this.HandObj, true);
		}
		if (this.mCurDirection != SCREEN_DIRECTION.RIGHT)
		{
			this.mCurDirection = SCREEN_DIRECTION.RIGHT;
			TweenExtensions.Kill(DragScreenHelpRoot.mTutorialTweener, false);
			this.HandObj.transform.localPosition = new Vector3(0f, 50f, 0f);
			DragScreenHelpRoot.mTutorialTweener = TweenSettingsExtensions.SetLoops<Tweener>(ShortcutExtensions.DOLocalMove(this.HandObj.transform, new Vector3(200f, 50f, 0f), 1f, false), int.MaxValue);
		}
	}

	// Token: 0x06004CE1 RID: 19681 RVA: 0x001A1638 File Offset: 0x0019F838
	private void DragLeft()
	{
		if (!UnityVersionUtil.IsActive(this.HandObj))
		{
			NGUITools.SetActive(this.HandObj, true);
		}
		if (this.mCurDirection != SCREEN_DIRECTION.LEFT)
		{
			this.mCurDirection = SCREEN_DIRECTION.LEFT;
			TweenExtensions.Kill(DragScreenHelpRoot.mTutorialTweener, false);
			this.HandObj.transform.localPosition = new Vector3(0f, 50f, 0f);
			DragScreenHelpRoot.mTutorialTweener = TweenSettingsExtensions.SetLoops<Tweener>(ShortcutExtensions.DOLocalMove(this.HandObj.transform, new Vector3(-200f, 50f, 0f), 1f, false), int.MaxValue);
		}
	}

	// Token: 0x06004CE2 RID: 19682 RVA: 0x001A16DC File Offset: 0x0019F8DC
	private void DisableTip()
	{
		if (UnityVersionUtil.IsActive(this.HandObj))
		{
			NGUITools.SetActive(this.HandObj, false);
		}
	}

	// Token: 0x04003A78 RID: 14968
	public GameObject HandObj;

	// Token: 0x04003A79 RID: 14969
	private GameObject mTargetObj;

	// Token: 0x04003A7A RID: 14970
	private Camera mMainCamera;

	// Token: 0x04003A7B RID: 14971
	private int screenWidth;

	// Token: 0x04003A7C RID: 14972
	private static Tweener mTutorialTweener;

	// Token: 0x04003A7D RID: 14973
	private SCREEN_DIRECTION mCurDirection;
}
