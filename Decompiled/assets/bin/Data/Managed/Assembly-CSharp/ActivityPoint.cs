using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class ActivityPoint : MonoBehaviour
{
	// Token: 0x06000721 RID: 1825 RVA: 0x00031F54 File Offset: 0x00030154
	private void Awake()
	{
		this.mTransform = base.transform;
		this.SqrActiveRadius = (float)(this.ActiveRadius * this.ActiveRadius);
		this.ExitSqrActiveRadius = ((float)this.ActiveRadius + 0.5f) * ((float)this.ActiveRadius + 0.5f);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00031FA4 File Offset: 0x000301A4
	public void Reset(ActivityMapData curinfo, Transform parenttra, bool isenable)
	{
		this.IsEnable = isenable;
		this.CurActData = curinfo;
		this.ID = this.CurActData.ID;
		if (this.CurActData.IsShowDoorFlag())
		{
			this.ActiveRadius = 3;
			this.SqrActiveRadius = (float)(this.ActiveRadius * this.ActiveRadius);
			this.ExitSqrActiveRadius = ((float)this.ActiveRadius + 0.5f) * ((float)this.ActiveRadius + 0.5f);
		}
		else
		{
			this.InitTexture();
			this.ActiveRadius = 4;
			this.SqrActiveRadius = (float)(this.ActiveRadius * this.ActiveRadius);
			this.ExitSqrActiveRadius = ((float)this.ActiveRadius + 0.5f) * ((float)this.ActiveRadius + 0.5f);
		}
		this.mTransform.parent = parenttra;
		this.mTransform.localPosition = this.CurActData.Position;
		this.mTransform.name = this.ID;
		this.IsExitFlag = false;
		this.OnArrivePoint = null;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x000320A4 File Offset: 0x000302A4
	public void ResetExit(ActivityPoint.OnArrivePointDelegate func = null)
	{
		this.CurActData = null;
		this.IsExitFlag = true;
		this.OnArrivePoint = func;
		this.ActiveRadius = 2;
		this.SqrActiveRadius = (float)(this.ActiveRadius * this.ActiveRadius);
		this.ExitSqrActiveRadius = ((float)this.ActiveRadius + 0.5f) * ((float)this.ActiveRadius + 0.5f);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00032104 File Offset: 0x00030304
	public void InitTexture()
	{
		UnityVersionUtil.SetActiveRecursive(this.PicObj, false);
		if (this.LockObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.LockObj, false);
		}
		this.PicMat = this.PicObj.gameObject.renderer.material;
		if (this.LockObj != null)
		{
			this.LockPicMat = this.LockObj.gameObject.renderer.material;
		}
		else
		{
			this.LockPicMat = null;
		}
		if (this.PicMat == null)
		{
			return;
		}
		this.textureList.Clear();
		if (!string.IsNullOrEmpty(this.CurActData.Icon) && (this.PicMat.mainTexture == null || !this.PicMat.mainTexture.name.Equals(this.CurActData.Icon)))
		{
			this.textureList.Add(this.CurActData.Icon);
		}
		if (this.LockPicMat != null && (this.LockPicMat.mainTexture == null || this.LockPicMat.mainTexture.name.Equals("CZ_effect_suo")))
		{
			this.textureList.Add("CZ_effect_suo");
		}
		if (this.textureList.Count == 0)
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MapActivityManager != null && UnityVersionUtil.IsactiveInHierarchy(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MapActivityManager.gameObject))
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MapActivityManager.StartCoroutine(BundleManager.LoadWaitTexture(this.textureList, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x000322EC File Offset: 0x000304EC
	private void TextureLoadFinish(string name, Texture curtex)
	{
		if (curtex == null)
		{
			return;
		}
		if (this.CurActData.Icon.Equals(name) && this.PicObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.PicObj, true);
		}
		if ("CZ_effect_suo".Equals(name) && this.LockObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.LockObj, true);
		}
		if (this.PicMat != null && this.CurActData.Icon.Equals(name))
		{
			this.PicMat.SetTexture("_MainTex", curtex);
		}
		if (this.LockPicMat != null && "CZ_effect_suo".Equals(name))
		{
			this.LockPicMat.SetTexture("_MainTex", curtex);
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x000323D0 File Offset: 0x000305D0
	private void FixedUpdate()
	{
		if (!this.IsExitFlag && this.CurActData == null)
		{
			return;
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
			this.sqrDis = (this.mMainPlayerTransform.position - this.mTransform.position).sqrMagnitude;
			if (this.sqrDis <= this.SqrActiveRadius)
			{
				if (!this.mInCircleFlag)
				{
					if (this.IsExitFlag)
					{
						if (this.OnArrivePoint != null)
						{
							this.OnArrivePoint();
						}
					}
					else if (this.CurActData.ActivityType == GameDefine.ACTIVITY_TYPE.SHOP_GATE)
					{
						if (!this.CurActData.IsUnlock)
						{
							SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ActivityTipstRoot, delegate
							{
								SingletonUnity<ActivityTipsRootLogic>.Instance.ShowInfo(this.CurActData);
							}, null);
						}
						else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload && !string.IsNullOrEmpty(this.CurActData.TargetMapID))
						{
							SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
							enter_empty_scene.request request = new enter_empty_scene.request();
							request.mapInfoId = this.CurActData.TargetMapID;
							NetLogic.GetInstance().Send<Protocol.enter_empty_scene>(request, null);
						}
					}
					else
					{
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ActivityTipstRoot, delegate
						{
							SingletonUnity<ActivityTipsRootLogic>.Instance.ShowInfo(this.CurActData);
						}, null);
					}
					this.mInCircleFlag = true;
				}
			}
			else if (this.sqrDis > this.ExitSqrActiveRadius)
			{
				if (this.mInCircleFlag && SingletonUnity<ActivityTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityTipsRootLogic>.Instance.gameObject))
				{
					SingletonUnity<ActivityTipsRootLogic>.Instance.CloseUI();
				}
				this.mInCircleFlag = false;
			}
		}
	}

	// Token: 0x04000630 RID: 1584
	public GameObject PicObj;

	// Token: 0x04000631 RID: 1585
	public GameObject LockObj;

	// Token: 0x04000632 RID: 1586
	public ActivityMapData CurActData;

	// Token: 0x04000633 RID: 1587
	private Material PicMat;

	// Token: 0x04000634 RID: 1588
	private Material LockPicMat;

	// Token: 0x04000635 RID: 1589
	public string ID;

	// Token: 0x04000636 RID: 1590
	private Transform mTransform;

	// Token: 0x04000637 RID: 1591
	private float SqrActiveRadius;

	// Token: 0x04000638 RID: 1592
	private float ExitSqrActiveRadius;

	// Token: 0x04000639 RID: 1593
	private int ActiveRadius = 4;

	// Token: 0x0400063A RID: 1594
	private Transform mMainPlayerTransform;

	// Token: 0x0400063B RID: 1595
	private bool mInCircleFlag;

	// Token: 0x0400063C RID: 1596
	private List<string> textureList = new List<string>();

	// Token: 0x0400063D RID: 1597
	public bool IsEnable;

	// Token: 0x0400063E RID: 1598
	private bool IsExitFlag;

	// Token: 0x0400063F RID: 1599
	public ActivityPoint.OnArrivePointDelegate OnArrivePoint;

	// Token: 0x04000640 RID: 1600
	private float sqrDis;

	// Token: 0x02000AAC RID: 2732
	// (Invoke) Token: 0x06004F39 RID: 20281
	public delegate void OnArrivePointDelegate();
}
