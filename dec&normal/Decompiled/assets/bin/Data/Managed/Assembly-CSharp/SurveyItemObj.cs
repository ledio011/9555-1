using System;
using UnityEngine;

// Token: 0x02000831 RID: 2097
public class SurveyItemObj : MonoBehaviour
{
	// Token: 0x17000ED0 RID: 3792
	// (get) Token: 0x06003556 RID: 13654 RVA: 0x000D91E8 File Offset: 0x000D73E8
	// (set) Token: 0x06003557 RID: 13655 RVA: 0x000D91F0 File Offset: 0x000D73F0
	public BundleManager.LoadModelData LoadingModelData
	{
		get
		{
			return this.mLoadingModelData;
		}
		set
		{
			this.mLoadingModelData = value;
		}
	}

	// Token: 0x17000ED1 RID: 3793
	// (get) Token: 0x06003558 RID: 13656 RVA: 0x000D91FC File Offset: 0x000D73FC
	// (set) Token: 0x06003559 RID: 13657 RVA: 0x000D9204 File Offset: 0x000D7404
	public long LoadingModelDataId
	{
		get
		{
			return this.mLoadingModelDataId;
		}
		set
		{
			this.mLoadingModelDataId = value;
		}
	}

	// Token: 0x17000ED2 RID: 3794
	// (get) Token: 0x0600355A RID: 13658 RVA: 0x000D9210 File Offset: 0x000D7410
	// (set) Token: 0x0600355B RID: 13659 RVA: 0x000D9218 File Offset: 0x000D7418
	public GameObject MeshRoot
	{
		get
		{
			return this.mMeshRoot;
		}
		set
		{
			this.mMeshRoot = value;
		}
	}

	// Token: 0x17000ED3 RID: 3795
	// (get) Token: 0x0600355C RID: 13660 RVA: 0x000D9224 File Offset: 0x000D7424
	// (set) Token: 0x0600355D RID: 13661 RVA: 0x000D922C File Offset: 0x000D742C
	public GameObject EffectRoot
	{
		get
		{
			return this.mEffectRoot;
		}
		set
		{
			this.mEffectRoot = value;
		}
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x000D9238 File Offset: 0x000D7438
	public bool IsEnable()
	{
		return this.mRestNum > 0;
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x000D924C File Offset: 0x000D744C
	public void Reset(SurveyMissionData surveyMissionData)
	{
		this.SurveyMissionData = surveyMissionData;
		if (surveyMissionData.SceneID.Equals("11"))
		{
			this.SqrActiveRadius = 8f;
			this.ExitSqrActiveRadius = 10f;
		}
		else
		{
			this.SqrActiveRadius = 3f;
			this.ExitSqrActiveRadius = 3.5f;
		}
		base.transform.position = new Vector3(surveyMissionData.PosX, SceneManager.GetHitHeight(surveyMissionData.PosX, surveyMissionData.PosZ), surveyMissionData.PosZ);
		base.transform.forward = MathUtil.HeadingToVector3(surveyMissionData.PosO);
		this.mRestNum = this.SurveyMissionData.Count;
		this.mTransform = base.transform;
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x000D9308 File Offset: 0x000D7508
	public void CollectOne()
	{
		this.mRestNum--;
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x000D9318 File Offset: 0x000D7518
	public void Refresh()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		this.mRestNum = this.SurveyMissionData.Count;
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x000D9338 File Offset: 0x000D7538
	public void PlayEffect()
	{
		UnityVersionUtil.SetActiveRecursive(this.EffectRoot, false);
		Animation animation = this.MeshRoot.GetComponent<Animation>();
		if (animation == null)
		{
			animation = this.MeshRoot.GetComponentInChildren<Animation>();
		}
		if (animation != null)
		{
			animation.Play();
		}
		ParticleSystem[] componentsInChildren = this.MeshRoot.GetComponentsInChildren<ParticleSystem>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
		}
	}

	// Token: 0x06003563 RID: 13667 RVA: 0x000D93C0 File Offset: 0x000D75C0
	private void Update()
	{
		if (this.mRestNum <= 0)
		{
			if (this.mInCircleFlag)
			{
				if (SingletonUnity<SurveyItemBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SurveyItemBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SurveyItemBtnRoot);
				}
				this.mInCircleFlag = false;
			}
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
		this.sqrDis = (this.mMainPlayerTransform.position - this.mTransform.position).sqrMagnitude;
		if (this.sqrDis <= this.SqrActiveRadius)
		{
			if (!this.mInCircleFlag)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SurveyItemBtnRoot, delegate
				{
					SingletonUnity<SurveyItemBtnRootLogic>.Instance.Reset(this);
				}, null);
				this.mInCircleFlag = true;
			}
		}
		else if (this.sqrDis > this.ExitSqrActiveRadius && this.mInCircleFlag)
		{
			if (SingletonUnity<SurveyItemBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SurveyItemBtnRootLogic>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SurveyItemBtnRoot);
			}
			this.mInCircleFlag = false;
		}
	}

	// Token: 0x06003564 RID: 13668 RVA: 0x000D9524 File Offset: 0x000D7724
	private void OnDisable()
	{
		if (this.mInCircleFlag && SingletonUnity<SurveyItemBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SurveyItemBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SurveyItemBtnRoot);
		}
	}

	// Token: 0x040022CB RID: 8907
	public SurveyMissionData SurveyMissionData;

	// Token: 0x040022CC RID: 8908
	private BundleManager.LoadModelData mLoadingModelData;

	// Token: 0x040022CD RID: 8909
	private long mLoadingModelDataId;

	// Token: 0x040022CE RID: 8910
	private GameObject mMeshRoot;

	// Token: 0x040022CF RID: 8911
	private GameObject mEffectRoot;

	// Token: 0x040022D0 RID: 8912
	private int mRestNum;

	// Token: 0x040022D1 RID: 8913
	private Transform mMainPlayerTransform;

	// Token: 0x040022D2 RID: 8914
	private float sqrDis;

	// Token: 0x040022D3 RID: 8915
	private float SqrActiveRadius = 3f;

	// Token: 0x040022D4 RID: 8916
	private float ExitSqrActiveRadius = 3.5f;

	// Token: 0x040022D5 RID: 8917
	private Transform mTransform;

	// Token: 0x040022D6 RID: 8918
	private bool mInCircleFlag;
}
