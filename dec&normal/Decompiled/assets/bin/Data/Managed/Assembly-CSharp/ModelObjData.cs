using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000914 RID: 2324
public class ModelObjData
{
	// Token: 0x17000F95 RID: 3989
	// (get) Token: 0x0600400E RID: 16398 RVA: 0x0012C4FC File Offset: 0x0012A6FC
	public string ModelId
	{
		get
		{
			if (this.ModelData != null)
			{
				return this.ModelData.ID;
			}
			return string.Empty;
		}
	}

	// Token: 0x17000F96 RID: 3990
	// (get) Token: 0x0600400F RID: 16399 RVA: 0x0012C51C File Offset: 0x0012A71C
	// (set) Token: 0x06004010 RID: 16400 RVA: 0x0012C53C File Offset: 0x0012A73C
	public string TargetModelId
	{
		get
		{
			if (this.TargetModelData != null)
			{
				return this.TargetModelData.ID;
			}
			return string.Empty;
		}
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.TargetModelData = DataManager.GetModeDataByID(value);
			}
			else
			{
				this.TargetModelData = null;
			}
		}
	}

	// Token: 0x06004011 RID: 16401 RVA: 0x0012C564 File Offset: 0x0012A764
	public void ClearCurEffect()
	{
		for (int i = 0; i < this.EffectList.Count; i++)
		{
			Object.Destroy(this.EffectList[i]);
		}
		this.EffectList.Clear();
	}

	// Token: 0x06004012 RID: 16402 RVA: 0x0012C5AC File Offset: 0x0012A7AC
	public void ClearEffect()
	{
		this.ClearCurEffect();
		this.StopLoadingEffect();
	}

	// Token: 0x06004013 RID: 16403 RVA: 0x0012C5BC File Offset: 0x0012A7BC
	public void StopLoadingEffect()
	{
		for (int i = 0; i < this.LoadingEffectDataList.Count; i++)
		{
			if (this.LoadingEffectDataList[i] != null)
			{
				this.LoadingEffectDataList[i].OnLoadFinished = null;
			}
		}
		this.LoadingEffectDataList.Clear();
	}

	// Token: 0x06004014 RID: 16404 RVA: 0x0012C614 File Offset: 0x0012A814
	public void UnloadCurModel()
	{
		if (this.ModelData != null)
		{
			if (this.ModelObj != null)
			{
				Object.Destroy(this.ModelObj);
				BundleManager.UnloadModel(this.ModelData.Name, this.ModelData.ModelPath, -1L, false);
				this.ModelObj = null;
			}
			this.ModelData = null;
		}
	}

	// Token: 0x06004015 RID: 16405 RVA: 0x0012C674 File Offset: 0x0012A874
	public void UnloadAllModel()
	{
		this.UnloadCurModel();
		this.StopLoadingMesh();
		this.ClearEffect();
	}

	// Token: 0x06004016 RID: 16406 RVA: 0x0012C688 File Offset: 0x0012A888
	public void StopLoadingMesh()
	{
		if (this.LoadingModelData != null)
		{
			this.LoadingModelData.OnLoadFinished = null;
			this.LoadingModelData = null;
			BundleManager.RemoveFromLoadModelList(this.LoadingModelId);
			this.LoadingModelId = -1L;
		}
		this.StopLoadingEffect();
	}

	// Token: 0x04002BDA RID: 11226
	public ModelData ModelData;

	// Token: 0x04002BDB RID: 11227
	public long LoadingModelId = -1L;

	// Token: 0x04002BDC RID: 11228
	public BundleManager.LoadModelData LoadingModelData;

	// Token: 0x04002BDD RID: 11229
	public GameObject ModelObj;

	// Token: 0x04002BDE RID: 11230
	public ModelData TargetModelData;

	// Token: 0x04002BDF RID: 11231
	public string CurEffectId = string.Empty;

	// Token: 0x04002BE0 RID: 11232
	public List<GameObject> EffectList = new List<GameObject>();

	// Token: 0x04002BE1 RID: 11233
	public List<BundleManager.LoadModelData> LoadingEffectDataList = new List<BundleManager.LoadModelData>();
}
