using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class MovePathPoint : MonoBehaviour
{
	// Token: 0x06001176 RID: 4470 RVA: 0x0007125C File Offset: 0x0006F45C
	public void RegisterOnArrivePathPoint(MovePathPoint.OnArrivePointDelegate func)
	{
		this.OnArrivePoint = (MovePathPoint.OnArrivePointDelegate)Delegate.Combine(this.OnArrivePoint, func);
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x00071278 File Offset: 0x0006F478
	public void DeRegisterOnArrivePathPoint(MovePathPoint.OnArrivePointDelegate func)
	{
		this.OnArrivePoint = (MovePathPoint.OnArrivePointDelegate)Delegate.Remove(this.OnArrivePoint, func);
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00071294 File Offset: 0x0006F494
	private void Start()
	{
		this.mTeleportTransform = base.transform;
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x000712A4 File Offset: 0x0006F4A4
	private void FixedUpdate()
	{
		if (!this.mbValid)
		{
			if (Time.time - this.mfLastInvaildTime < 3f)
			{
				return;
			}
			this.mbValid = true;
		}
		if (null == this.mMainPlayerTransform)
		{
			if (null != Singleton<ObjManager>.Instance.MainPlayer)
			{
				this.mMainPlayerTransform = Singleton<ObjManager>.Instance.MainPlayer.CacheTransform;
			}
			if (null == this.mMainPlayerTransform)
			{
				return;
			}
		}
		if (null != Singleton<ObjManager>.Instance.MainPlayer && Vector3.Distance(this.mMainPlayerTransform.position, this.mTeleportTransform.position) <= this.ActiveRadius)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (!sceneManager.IsSurviveBattleScene())
			{
				UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
			}
			if (this.OnArrivePoint != null)
			{
				this.OnArrivePoint(base.transform.position);
			}
		}
	}

	// Token: 0x0400174C RID: 5964
	public float ActiveRadius = 3f;

	// Token: 0x0400174D RID: 5965
	private bool mbValid = true;

	// Token: 0x0400174E RID: 5966
	private float mfLastInvaildTime;

	// Token: 0x0400174F RID: 5967
	private Transform mMainPlayerTransform;

	// Token: 0x04001750 RID: 5968
	private Transform mTeleportTransform;

	// Token: 0x04001751 RID: 5969
	public MovePathPoint.OnArrivePointDelegate OnArrivePoint;

	// Token: 0x02000AD5 RID: 2773
	// (Invoke) Token: 0x06004FDD RID: 20445
	public delegate void OnArrivePointDelegate(Vector3 pos);
}
