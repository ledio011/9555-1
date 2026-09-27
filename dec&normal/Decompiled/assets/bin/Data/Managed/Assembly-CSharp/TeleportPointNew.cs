using System;
using SprotoType;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class TeleportPointNew : MonoBehaviour
{
	// Token: 0x06000760 RID: 1888 RVA: 0x000332E4 File Offset: 0x000314E4
	private void Start()
	{
		this.mTeleportTransform = base.transform;
		if (this.mAutoSearchPathManager == null)
		{
			this.mAutoSearchPathManager = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		}
		this.SqrActiveRadius = (float)(this.ActiveRadius * this.ActiveRadius);
		this.ExitSqrActiveRadius = (float)((this.ActiveRadius + 1) * (this.ActiveRadius + 1));
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00033344 File Offset: 0x00031544
	private void GoNextScene()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange)
		{
			return;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
			return;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
		enter_new_map.request request = new enter_new_map.request();
		request.mapInfoId = this.mAutoSearchPathManager.TargetSceneId;
		NetLogic.GetInstance().Send<Protocol.enter_new_map>(request, null);
		this.mfLastInvaildTime = Time.time;
		this.mbValid = false;
		WaitResponseUIRootLogic.OpenWaitBox(106, 10f, 0f, null);
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x000333F0 File Offset: 0x000315F0
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
				this.mMainPlayerTransform = Singleton<ObjManager>.Instance.MainPlayer.transform;
			}
			if (null == this.mMainPlayerTransform)
			{
				return;
			}
		}
		if (null != Singleton<ObjManager>.Instance.MainPlayer)
		{
			this.sqrDis = (this.mMainPlayerTransform.position - this.mTeleportTransform.position).sqrMagnitude;
			if (this.sqrDis <= this.SqrActiveRadius)
			{
				if (!this.mInCircleFlag)
				{
					if (this.mAutoSearchPathManager.IsAutoMovingFlag)
					{
						this.GoNextScene();
					}
					else if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange && !UIManager.IsUnlockTutorialEnable() && !SingletonUnity<UIManager>.Instance.IsHideBaseUI && (!SingletonUnity<MapUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<MapUIRootLogic>.Instance.gameObject)))
					{
						SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
						if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
						{
							SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
						}
						else
						{
							SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MapUIRoot, delegate
							{
								SingletonUnity<MapUIRootLogic>.Instance.Reset();
								SingletonUnity<MapUIRootLogic>.Instance.ChangeToLocalMap(false);
							}, null);
						}
					}
					this.mInCircleFlag = true;
					this.mAutoSearchPathManager.IsInTelePortCircle = true;
				}
			}
			else if (this.sqrDis > this.ExitSqrActiveRadius)
			{
				this.mInCircleFlag = false;
				this.mAutoSearchPathManager.IsInTelePortCircle = false;
			}
		}
	}

	// Token: 0x0400064A RID: 1610
	public int ActiveRadius = 3;

	// Token: 0x0400064B RID: 1611
	private bool mbValid = true;

	// Token: 0x0400064C RID: 1612
	private float mfLastInvaildTime;

	// Token: 0x0400064D RID: 1613
	private Transform mMainPlayerTransform;

	// Token: 0x0400064E RID: 1614
	private Transform mTeleportTransform;

	// Token: 0x0400064F RID: 1615
	private AutoSearchPathManager mAutoSearchPathManager;

	// Token: 0x04000650 RID: 1616
	private float SqrActiveRadius;

	// Token: 0x04000651 RID: 1617
	private float ExitSqrActiveRadius;

	// Token: 0x04000652 RID: 1618
	private bool mInCircleFlag = true;

	// Token: 0x04000653 RID: 1619
	private float sqrDis;
}
