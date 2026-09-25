using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000915 RID: 2325
public class PlayerModelObjData
{
	// Token: 0x17000F97 RID: 3991
	// (get) Token: 0x06004018 RID: 16408 RVA: 0x0012C704 File Offset: 0x0012A904
	public ModelObjData[] PartModelObjData
	{
		get
		{
			return this.mPartModelObjData;
		}
	}

	// Token: 0x06004019 RID: 16409 RVA: 0x0012C70C File Offset: 0x0012A90C
	public void StopLoadMesh()
	{
		for (int i = 0; i < this.mPartModelObjData.Length; i++)
		{
			this.mPartModelObjData[i].StopLoadingMesh();
		}
	}

	// Token: 0x0600401A RID: 16410 RVA: 0x0012C740 File Offset: 0x0012A940
	public void SetTargetPartId(MODEL_TYPE type, string id)
	{
		this.mPartModelObjData[(int)type].TargetModelId = id;
	}

	// Token: 0x0600401B RID: 16411 RVA: 0x0012C750 File Offset: 0x0012A950
	public string GetTargetPartId(MODEL_TYPE type)
	{
		return this.mPartModelObjData[(int)type].TargetModelId;
	}

	// Token: 0x0600401C RID: 16412 RVA: 0x0012C760 File Offset: 0x0012A960
	public void LoadModel(MODEL_TYPE type, string targetId, GameObject fakeObjRoot, BundleManager.OnLoadModelFinish onLoadModelFinish, ref int needLoadPartNum, string layerStr)
	{
		bool flag;
		if (!this.mPartModelObjData[(int)type].TargetModelId.Equals(targetId))
		{
			if (!this.mPartModelObjData[(int)type].ModelId.Equals(targetId))
			{
				flag = true;
				needLoadPartNum++;
				this.mPartModelObjData[(int)type].StopLoadingMesh();
			}
			else
			{
				flag = false;
				this.mPartModelObjData[(int)type].StopLoadingMesh();
			}
			this.mPartModelObjData[(int)type].TargetModelId = targetId;
		}
		else
		{
			flag = false;
			if (!this.mPartModelObjData[(int)type].ModelId.Equals(targetId))
			{
				needLoadPartNum++;
			}
		}
		ModelData modeDataByID = DataManager.GetModeDataByID(targetId);
		if (flag && modeDataByID != null)
		{
			this.mPartModelObjData[(int)type].LoadingModelData = BundleManager.LoadModelInList(modeDataByID.Name, true, false, onLoadModelFinish, modeDataByID, fakeObjRoot, modeDataByID.ModelPath);
			this.mPartModelObjData[(int)type].LoadingModelId = ((this.mPartModelObjData[(int)type].LoadingModelData != null) ? this.mPartModelObjData[(int)type].LoadingModelData.ID : -1L);
		}
		this.CheckModelEffect(modeDataByID, fakeObjRoot, layerStr);
	}

	// Token: 0x0600401D RID: 16413 RVA: 0x0012C87C File Offset: 0x0012AA7C
	private void CheckModelEffect(ModelData modelData, GameObject player, string layerStr)
	{
		List<GameObject> effectList = this.mPartModelObjData[modelData.ModelType].EffectList;
		List<BundleManager.LoadModelData> loadingEffectDataList = this.mPartModelObjData[modelData.ModelType].LoadingEffectDataList;
		if (GameSettingData.IsShowFashionEffect[GameSettingData.GetPhoneClass()] && !string.IsNullOrEmpty(modelData.EffectId))
		{
			this.mPartModelObjData[modelData.ModelType].ClearEffect();
			List<FxEffInfoData> fxEffInfoDataListById = DataManager.GetFxEffInfoDataListById(modelData.EffectId);
			for (int i = 0; i < fxEffInfoDataListById.Count; i++)
			{
				ModelEffectData modelEffectData = new ModelEffectData(modelData, fxEffInfoDataListById[i], i == 0, layerStr, -1);
				string path = string.Format("{0}/{1}", fxEffInfoDataListById[i].EffFilePath, fxEffInfoDataListById[i].EffName);
				GameObject gameObject = ResourcesManager.LoadAndInstantiate(path) as GameObject;
				if (gameObject != null)
				{
					effectList.Add(gameObject);
					gameObject.transform.parent = TransformUtil.FindChildTransform(player.transform, modelEffectData.fxEffinfoData.EffLinkNode, false);
					gameObject.transform.localPosition = modelEffectData.fxEffinfoData.Position;
					gameObject.transform.localEulerAngles = modelEffectData.fxEffinfoData.Angel;
					NGUITools.SetLayer(gameObject, LayerMask.NameToLayer(layerStr));
				}
				else
				{
					modelEffectData.Index = loadingEffectDataList.Count;
					loadingEffectDataList.Add(BundleManager.LoadEffectInList(fxEffInfoDataListById[i].EffName, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartEffectFinished), modelEffectData, player));
				}
			}
		}
		else
		{
			this.mPartModelObjData[modelData.ModelType].ClearEffect();
		}
	}

	// Token: 0x0600401E RID: 16414 RVA: 0x0012CA14 File Offset: 0x0012AC14
	public void OnLoadPlayerPartEffectFinished(object objBundle, object param1 = null, object param2 = null)
	{
		GameObject gameObject = param2 as GameObject;
		if (gameObject == null)
		{
			return;
		}
		ModelEffectData modelEffectData = param1 as ModelEffectData;
		GameObject gameObject2 = objBundle as GameObject;
		List<GameObject> effectList = this.mPartModelObjData[modelEffectData.modelData.ModelType].EffectList;
		effectList.Add(gameObject2);
		gameObject2.transform.parent = TransformUtil.FindChildTransform(gameObject.transform, modelEffectData.fxEffinfoData.EffLinkNode, true);
		gameObject2.transform.localPosition = modelEffectData.fxEffinfoData.Position;
		gameObject2.transform.localEulerAngles = modelEffectData.fxEffinfoData.Angel;
		if (!string.IsNullOrEmpty(modelEffectData.LayerStr))
		{
			NGUITools.SetLayer(gameObject2, LayerMask.NameToLayer(modelEffectData.LayerStr));
		}
		List<BundleManager.LoadModelData> loadingEffectDataList = this.mPartModelObjData[modelEffectData.modelData.ModelType].LoadingEffectDataList;
		if (modelEffectData.Index >= 0 && modelEffectData.Index < loadingEffectDataList.Count)
		{
			loadingEffectDataList[modelEffectData.Index] = null;
			bool flag = true;
			for (int i = 0; i < loadingEffectDataList.Count; i++)
			{
				if (loadingEffectDataList[i] != null)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				loadingEffectDataList.Clear();
			}
		}
		if (UnityVersionUtil.IsActive(gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(gameObject2.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(gameObject2.gameObject, false);
		}
	}

	// Token: 0x0600401F RID: 16415 RVA: 0x0012CB84 File Offset: 0x0012AD84
	public void LoadModelFinished(MODEL_TYPE type, ModelData modelData, GameObject modelObj)
	{
		this.LoadModelFinished((int)type, modelData, modelObj);
	}

	// Token: 0x06004020 RID: 16416 RVA: 0x0012CB90 File Offset: 0x0012AD90
	public void LoadModelFinished(int type, ModelData modelData, GameObject modelObj)
	{
		this.mPartModelObjData[type].TargetModelId = string.Empty;
		this.mPartModelObjData[type].LoadingModelData = null;
		this.mPartModelObjData[type].LoadingModelId = -1L;
		this.mPartModelObjData[type].UnloadCurModel();
		this.mPartModelObjData[type].ModelData = modelData;
		this.mPartModelObjData[type].ModelObj = modelObj;
	}

	// Token: 0x06004021 RID: 16417 RVA: 0x0012CBF8 File Offset: 0x0012ADF8
	public void ActiveModelObj()
	{
		for (int i = 0; i < this.mPartModelObjData.Length; i++)
		{
			if (this.mPartModelObjData[i].ModelObj != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.mPartModelObjData[i].ModelObj, true);
			}
		}
	}

	// Token: 0x06004022 RID: 16418 RVA: 0x0012CC4C File Offset: 0x0012AE4C
	public void UnloadAllModel()
	{
		for (int i = 0; i < this.mPartModelObjData.Length; i++)
		{
			this.mPartModelObjData[i].UnloadAllModel();
		}
	}

	// Token: 0x04002BE2 RID: 11234
	private ModelObjData[] mPartModelObjData = new ModelObjData[]
	{
		new ModelObjData(),
		new ModelObjData(),
		new ModelObjData(),
		new ModelObjData()
	};
}
