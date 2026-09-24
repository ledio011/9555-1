using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class BundleManager
{
	// Token: 0x060007C9 RID: 1993 RVA: 0x00037ACC File Offset: 0x00035CCC
	// Note: this type is marked as 'beforefieldinit'.
	static BundleManager()
	{
		List<string> list = new List<string>();
		list.Add("baiRen_XD");
		list.Add("heiRen_QJ");
		list.Add("nvRen_QS");
		BundleManager.AnimationStreamFileNameList = list;
		list = new List<string>();
		list.Add("baiRen_QJ");
		list.Add("baiRen_QS");
		list.Add("heiRen_XD");
		list.Add("heiRen_QS");
		list.Add("nvRen_XD");
		list.Add("nvRen_QJ");
		BundleManager.AnimationFileNameList = list;
		BundleManager.mAnimationBundleDic = new Dictionary<string, AssetBundle>();
		BundleManager.DownloadAnimationFlag = false;
		BundleManager.StreamAnimationFlag = false;
		BundleManager.DataRootPath = BundleManager.BundleRoot + "/Data";
		BundleManager.DataFileName = "Data.bundle";
		BundleManager.mDataBundle = null;
		BundleManager.mLoadingDataBundleFlag = false;
		BundleManager.mWaittingLoadDataFlag = false;
		BundleManager.SoundRootPath = BundleManager.BundleRoot + "/";
		BundleManager.TextureRootPath = BundleManager.BundleRoot + "/Items/Texture";
		BundleManager.MaxTextureNum = 20;
		BundleManager.mTextureDic = new Dictionary<string, BundleManager.TextureInfo>();
		BundleManager.mCurLoadingTextureList = new List<string>();
		BundleManager.mLoadTextureBundle = new List<string>();
		BundleManager.mWaitTextureDic = new Dictionary<string, List<BundleManager.LoadTextureData>>();
		BundleManager.NextLoadingTexture = null;
		BundleManager.ItemsRootPath = BundleManager.BundleRoot + "/Items/ShowModel";
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00037D9C File Offset: 0x00035F9C
	public static bool UnloadBundle(AssetBundle curBundle, bool flag)
	{
		if (BundleManager.IsCanUnloadBundle)
		{
			if (curBundle != null)
			{
				curBundle.Unload(flag);
			}
			return true;
		}
		return false;
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x060007CB RID: 1995 RVA: 0x00037DCC File Offset: 0x00035FCC
	public static Dictionary<string, AssetBundleData> ModelBundleCacheDic
	{
		get
		{
			return BundleManager.mModelBundleCacheDic;
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00037DD4 File Offset: 0x00035FD4
	public static void CleanmLoadModelList()
	{
		BundleManager.mLoadModelList.Clear();
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00037DE0 File Offset: 0x00035FE0
	public static BundleManager.LoadModelData LoadModelInList(string modelName, bool isNeedUnload, bool isDoNotCache = false, BundleManager.OnLoadModelFinish onFinished = null, object paramData1 = null, object paramData2 = null, string subPath = null)
	{
		object obj = ResourcesManager.LoadAndInstantiate("TestModel/" + modelName);
		if (obj != null)
		{
			if (onFinished != null)
			{
				onFinished(obj, paramData1, paramData2);
			}
			return null;
		}
		BundleManager.LoadModelData loadModelData = new BundleManager.LoadModelData(modelName, isNeedUnload, BundleManager.LoadModelData.BundleType.MODEL, isDoNotCache, onFinished, paramData1, paramData2, subPath);
		if (BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.ModelName) && BundleManager.mModelBundleCacheDic[loadModelData.ModelName].IsBundleValid())
		{
			if (onFinished != null)
			{
				BundleManager.mModelBundleCacheDic[loadModelData.ModelName].AddBundleUsingCount();
				obj = Object.Instantiate(BundleManager.mModelBundleCacheDic[loadModelData.ModelName].GetBundle().mainAsset);
				onFinished(obj, paramData1, paramData2);
			}
			return null;
		}
		if (!string.IsNullOrEmpty(loadModelData.SubPath) && !BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.SubPath))
		{
			BundleManager.mModelBundleCacheDic.Add(loadModelData.SubPath, new AssetBundleData(null, 0, loadModelData.SubPath, string.Empty, true));
		}
		if (!BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.ModelName))
		{
			BundleManager.mModelBundleCacheDic.Add(loadModelData.ModelName, new AssetBundleData(null, 0, loadModelData.ModelName, loadModelData.SubPath, false));
		}
		BundleManager.mModelBundleCacheDic[loadModelData.ModelName].AddBundleUsingCount();
		BundleManager.mLoadModelList.Add(loadModelData);
		return loadModelData;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00037F44 File Offset: 0x00036144
	public static void LoadModelListUpdate(MonoBehaviour mono)
	{
		if (mono == null)
		{
			return;
		}
		if (BundleManager.mLoadModelList.Count > 0)
		{
			if (BundleManager.mLoadModelList[0].bundleType == BundleManager.LoadModelData.BundleType.MODEL)
			{
				if (BundleManager.mModelCommonShaderBundle == null)
				{
					if (!BundleManager.ModelLoadingShaderFlag && UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
					{
						mono.StartCoroutine(BundleManager.LoadCommonShader());
					}
				}
				else
				{
					BundleManager.LoadModelData loadModelData = BundleManager.mLoadModelList[0];
					BundleManager.mLoadModelList.RemoveAt(0);
					if (!string.IsNullOrEmpty(loadModelData.SubPath))
					{
						if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
						{
							mono.StartCoroutine(BundleManager.LoadDependPicBundle(loadModelData, mono));
						}
					}
					else if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
					{
						mono.StartCoroutine(BundleManager.LoadModelFromList(loadModelData));
					}
				}
			}
			else if (BundleManager.mEffectCommonPicBundle == null)
			{
				if (!BundleManager.effectCommonFileLoadingFlag && UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
				{
					mono.StartCoroutine(BundleManager.LoadEffectCommonFile());
				}
			}
			else
			{
				BundleManager.LoadModelData curData = BundleManager.mLoadModelList[0];
				BundleManager.mLoadModelList.RemoveAt(0);
				if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
				{
					mono.StartCoroutine(BundleManager.LoadModelFromList(curData));
				}
			}
		}
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0003809C File Offset: 0x0003629C
	private static IEnumerator LoadModelFromList(BundleManager.LoadModelData curData)
	{
		string loadUrl = curData.LoadURL;
		if (!BundleManager.LoadModelFromCache(curData))
		{
			if (!BundleManager.mLoadingBundle.Contains(loadUrl))
			{
				BundleManager.mLoadingBundle.Add(loadUrl);
				WWW www = new WWW(loadUrl);
				yield return www;
				if (www.assetBundle != null)
				{
					BundleManager.mModelBundleCacheDic[curData.ModelName].SetBundle(www.assetBundle);
					if (curData.OnLoadFinished != null)
					{
						if (BundleManager.mWaitingBundleDic.ContainsKey(loadUrl))
						{
							for (int i = 0; i < BundleManager.mWaitingBundleDic[loadUrl].Count; i++)
							{
								BundleManager.LoadModelFromCache(BundleManager.mWaitingBundleDic[loadUrl][i]);
							}
							BundleManager.mWaitingBundleDic.Remove(loadUrl);
						}
						curData.OnLoadFinished(Object.Instantiate(www.assetBundle.mainAsset), curData.param1, curData.param2);
					}
					else
					{
						if (BundleManager.mWaitingBundleDic.ContainsKey(loadUrl) && BundleManager.mWaitingBundleDic.ContainsKey(loadUrl))
						{
							for (int j = 0; j < BundleManager.mWaitingBundleDic[loadUrl].Count; j++)
							{
								BundleManager.LoadModelFromCache(BundleManager.mWaitingBundleDic[loadUrl][j]);
							}
							BundleManager.mWaitingBundleDic.Remove(loadUrl);
						}
						if (BundleManager.mModelBundleCacheDic[curData.ModelName].UnLoadBundle())
						{
							BundleManager.mModelBundleCacheDic.Remove(curData.ModelName);
						}
					}
					if (BundleManager.mLoadingBundle.Contains(loadUrl))
					{
						BundleManager.mLoadingBundle.Remove(loadUrl);
					}
				}
				else
				{
					Log.ERROR_MSG("load bundle fail : " + curData.LoadURL);
				}
			}
			else
			{
				if (!BundleManager.mWaitingBundleDic.ContainsKey(loadUrl))
				{
					BundleManager.mWaitingBundleDic.Add(loadUrl, new List<BundleManager.LoadModelData>());
				}
				BundleManager.mWaitingBundleDic[loadUrl].Add(curData);
			}
		}
		yield break;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x000380C0 File Offset: 0x000362C0
	private static bool LoadModelFromCache(BundleManager.LoadModelData curData)
	{
		if (!BundleManager.mModelBundleCacheDic[curData.ModelName].IsBundleValid())
		{
			return false;
		}
		if (curData.OnLoadFinished != null)
		{
			Object objBundle = Object.Instantiate(BundleManager.mModelBundleCacheDic[curData.ModelName].GetBundle().mainAsset);
			curData.OnLoadFinished(objBundle, curData.param1, curData.param2);
			return true;
		}
		if (BundleManager.mModelBundleCacheDic[curData.ModelName].UnLoadBundle())
		{
			BundleManager.mModelBundleCacheDic.Remove(curData.ModelName);
		}
		return true;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0003815C File Offset: 0x0003635C
	private static IEnumerator LoadCommonShader()
	{
		BundleManager.ModelLoadingShaderFlag = true;
		WWW wwwCommonShader = new WWW(BundleManager.GetLocalUrl(BundleManager.BundleModelRootPath, "CommonShader.bundle"));
		yield return wwwCommonShader;
		if (wwwCommonShader.assetBundle != null)
		{
			wwwCommonShader.assetBundle.LoadAll();
			BundleManager.mModelCommonShaderBundle = wwwCommonShader.assetBundle;
		}
		else
		{
			Debug.Log("no shader LoadModelFromList");
		}
		yield break;
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00038170 File Offset: 0x00036370
	private static IEnumerator LoadDependPicBundle(BundleManager.LoadModelData curData, MonoBehaviour mono)
	{
		if (!BundleManager.mModelBundleCacheDic[curData.SubPath].IsBundleValid())
		{
			if (!BundleManager.mLoadingBundle.Contains(curData.SubPath))
			{
				BundleManager.mLoadingBundle.Add(curData.SubPath);
				BundleManager.sb.Length = 0;
				BundleManager.sb1.Length = 0;
				BundleManager.sb.AppendFormat("{0}{1}{2}", BundleManager.BundleModelRootPath, "/", curData.SubPath);
				BundleManager.sb1.AppendFormat("{0}{1}", curData.SubPath, ".bundle");
				string str = BundleManager.GetLocalUrl(BundleManager.sb.ToString(), BundleManager.sb1.ToString());
				WWW wwwDependPic = new WWW(str);
				yield return wwwDependPic;
				if (wwwDependPic.assetBundle != null)
				{
					wwwDependPic.assetBundle.LoadAll();
					BundleManager.mModelBundleCacheDic[curData.SubPath].SetBundle(wwwDependPic.assetBundle);
					BundleManager.mLoadingBundle.Remove(curData.SubPath);
					if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
					{
						mono.StartCoroutine(BundleManager.LoadModelFromList(curData));
					}
					if (BundleManager.mWaitingBundleDic.ContainsKey(curData.SubPath))
					{
						List<BundleManager.LoadModelData> waitingList = BundleManager.mWaitingBundleDic[curData.SubPath];
						for (int i = 0; i < waitingList.Count; i++)
						{
							if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
							{
								mono.StartCoroutine(BundleManager.LoadModelFromList(waitingList[i]));
							}
						}
						BundleManager.mWaitingBundleDic.Remove(curData.SubPath);
					}
				}
				else
				{
					Debug.Log("wwwDependPic.assetBundle == null");
				}
			}
			else
			{
				if (!BundleManager.mWaitingBundleDic.ContainsKey(curData.SubPath))
				{
					BundleManager.mWaitingBundleDic.Add(curData.SubPath, new List<BundleManager.LoadModelData>());
				}
				BundleManager.mWaitingBundleDic[curData.SubPath].Add(curData);
			}
		}
		else if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadModelFromList(curData));
		}
		yield break;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x000381A0 File Offset: 0x000363A0
	public static void UnloadModel(string modelId, long curLoadingDataId, bool isMainPlayerUnload)
	{
		if (!string.IsNullOrEmpty(modelId))
		{
			ModelData modeDataByID = DataManager.GetModeDataByID(modelId);
			if (modeDataByID != null)
			{
				BundleManager.UnloadModel(modeDataByID.Name, modeDataByID.ModelPath, curLoadingDataId, isMainPlayerUnload);
			}
			else
			{
				CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(modelId);
				BundleManager.UnloadModel(characterModelDataByID.Name, string.Empty, curLoadingDataId, false);
			}
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x000381F8 File Offset: 0x000363F8
	public static void UnloadModel(string modelName, string subPath, long loadingDataId, bool isMainPlayerPart)
	{
		if (BundleManager.mModelBundleCacheDic.ContainsKey(modelName) && BundleManager.mModelBundleCacheDic[modelName].IsBundleValid() && BundleManager.mModelBundleCacheDic[modelName].UnLoadBundle())
		{
			BundleManager.mModelBundleCacheDic.Remove(modelName);
		}
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0003824C File Offset: 0x0003644C
	public static void RemoveFromLoadModelList(long loadingDataId)
	{
		if (loadingDataId != -1L)
		{
			for (int i = 0; i < BundleManager.mLoadModelList.Count; i++)
			{
				if (BundleManager.mLoadModelList[i].ID == loadingDataId)
				{
					BundleManager.LoadModelData loadModelData = BundleManager.mLoadModelList[i];
					BundleManager.mLoadModelList.RemoveAt(i);
					if (BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.ModelName) && BundleManager.mModelBundleCacheDic[loadModelData.ModelName].UnLoadBundle())
					{
						BundleManager.mModelBundleCacheDic.Remove(loadModelData.ModelName);
					}
					return;
				}
			}
		}
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x000382EC File Offset: 0x000364EC
	public static void ClearCacheModelBundle(bool isClearAll = false)
	{
		List<KeyValuePair<string, AssetBundleData>> list = new List<KeyValuePair<string, AssetBundleData>>(BundleManager.mModelBundleCacheDic);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Value.Clear(isClearAll))
			{
				BundleManager.mModelBundleCacheDic.Remove(list[i].Key);
			}
		}
		BundleManager.mLoadModelList.Clear();
		BundleManager.mLoadingBundle.Clear();
		BundleManager.mWaitingBundleDic.Clear();
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00038378 File Offset: 0x00036578
	public static void ClearMainPlayerBundleFlag(string weaponId, string headId, string bodyId, string legId, string carId)
	{
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(weaponId))
		{
			ModelData modeDataByID = DataManager.GetModeDataByID(weaponId);
			if (modeDataByID != null)
			{
				list.Add(modeDataByID.Name);
				if (!string.IsNullOrEmpty(modeDataByID.ModelPath))
				{
					list.Add(modeDataByID.ModelPath);
				}
			}
		}
		if (!string.IsNullOrEmpty(headId))
		{
			ModelData modeDataByID2 = DataManager.GetModeDataByID(headId);
			list.Add(modeDataByID2.Name);
			if (!string.IsNullOrEmpty(modeDataByID2.ModelPath) && !list.Contains(modeDataByID2.ModelPath))
			{
				list.Add(modeDataByID2.ModelPath);
			}
		}
		if (!string.IsNullOrEmpty(bodyId))
		{
			ModelData modeDataByID3 = DataManager.GetModeDataByID(bodyId);
			list.Add(modeDataByID3.Name);
			if (!string.IsNullOrEmpty(modeDataByID3.ModelPath) && !list.Contains(modeDataByID3.ModelPath))
			{
				list.Add(modeDataByID3.ModelPath);
			}
		}
		if (!string.IsNullOrEmpty(legId))
		{
			ModelData modeDataByID4 = DataManager.GetModeDataByID(legId);
			list.Add(modeDataByID4.Name);
			if (!string.IsNullOrEmpty(modeDataByID4.ModelPath) && !list.Contains(modeDataByID4.ModelPath))
			{
				list.Add(modeDataByID4.ModelPath);
			}
		}
		if (!string.IsNullOrEmpty(carId))
		{
			ModelData modeDataByID5 = DataManager.GetModeDataByID(DataManager.GetMountDataById(carId).ModelId);
			list.Add(modeDataByID5.Name);
			if (!string.IsNullOrEmpty(modeDataByID5.ModelPath) && !list.Contains(modeDataByID5.ModelPath))
			{
				list.Add(modeDataByID5.ModelPath);
			}
		}
		List<string> list2 = new List<string>(BundleManager.mModelBundleCacheDic.Keys);
		for (int i = list2.Count - 1; i >= 0; i--)
		{
			if (!list.Contains(list2[i]) && BundleManager.mModelBundleCacheDic.ContainsKey(list2[i]) && !BundleManager.mModelBundleCacheDic[list2[i]].IsDependObj && BundleManager.mModelBundleCacheDic[list2[i]].UsingCount <= 0 && BundleManager.mModelBundleCacheDic[list2[i]].UnLoadBundle())
			{
				BundleManager.mModelBundleCacheDic.Remove(list2[i]);
			}
		}
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x000385D4 File Offset: 0x000367D4
	public static void UnloadCommonShaderBundle()
	{
		if (BundleManager.UnloadBundle(BundleManager.mModelCommonShaderBundle, true))
		{
			BundleManager.mModelCommonShaderBundle = null;
		}
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x000385EC File Offset: 0x000367EC
	public static void AddOutLineMaterial(GameObject obj)
	{
		SkinnedMeshRenderer component = obj.GetComponent<SkinnedMeshRenderer>();
		Material[] array = new Material[component.materials.Length + 1];
		for (int i = 0; i < component.materials.Length; i++)
		{
			if (component.materials[i].name.Contains("XRay"))
			{
				return;
			}
			array[i] = component.materials[i];
		}
		Object @object = ResourcesManager.Load("Material/XRay");
		if (null != @object)
		{
			array[component.materials.Length] = (Object.Instantiate(@object) as Material);
		}
		component.materials = array;
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00038684 File Offset: 0x00036884
	public static void ResetParticleShader(Transform obj)
	{
		ParticleSystem component = obj.GetComponent<ParticleSystem>();
		if (component != null)
		{
			Material material = component.renderer.material;
			string name = material.shader.name;
			if (!string.IsNullOrEmpty(name))
			{
				Shader shader = Shader.Find(name);
				if (shader != null)
				{
					material.shader = shader;
				}
				else
				{
					Debug.Log("unable to refresh shader: " + name + " in material " + material.name);
				}
			}
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			BundleManager.ResetParticleShader(obj.transform.GetChild(i));
		}
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00038730 File Offset: 0x00036930
	public static void ResetAllShader(Transform obj)
	{
		if (obj.renderer != null && obj.renderer.material != null)
		{
			Material material = obj.renderer.material;
			string text = material.shader.name;
			if (!string.IsNullOrEmpty(text))
			{
				if (GameSettingData.IsLowPhone && text.Contains("Outline_"))
				{
					text = "Mobile/Diffuse";
				}
				Shader shader = Shader.Find(text);
				if (shader != null)
				{
					material.shader = shader;
				}
				else
				{
					Debug.Log("unable to refresh shader: " + text + " in material " + material.name);
				}
			}
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			BundleManager.ResetAllShader(obj.transform.GetChild(i));
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0003880C File Offset: 0x00036A0C
	public static bool ResetShader(Transform obj)
	{
		if (obj.renderer != null && obj.renderer.material != null)
		{
			Material material = obj.renderer.material;
			string text = material.shader.name;
			if (!string.IsNullOrEmpty(text))
			{
				if (GameSettingData.IsLowPhone && text.Contains("Outline_"))
				{
					text = "Mobile/Diffuse";
				}
				Shader shader = Shader.Find(text);
				if (shader != null)
				{
					material.shader = shader;
				}
				else
				{
					Debug.Log("unable to refresh shader: " + text + " in material " + material.name);
				}
			}
			return true;
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			if (BundleManager.ResetShader(obj.transform.GetChild(i)))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x000388F0 File Offset: 0x00036AF0
	public static bool ResetShader(Transform obj, out Material sm)
	{
		if (obj.renderer != null && obj.renderer.material != null)
		{
			sm = obj.renderer.material;
			string text = sm.shader.name;
			if (!string.IsNullOrEmpty(text))
			{
				if (GameSettingData.IsLowPhone && text.Contains("Outline_"))
				{
					text = "Mobile/Diffuse";
				}
				Shader shader = Shader.Find(text);
				if (shader != null)
				{
					sm.shader = shader;
				}
				else
				{
					Debug.Log("unable to refresh shader: " + text + " in material " + sm.name);
				}
			}
			return true;
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			if (BundleManager.ResetShader(obj.transform.GetChild(i), out sm))
			{
				return true;
			}
		}
		sm = null;
		return false;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x000389DC File Offset: 0x00036BDC
	public static void RebuildBones(ObjCharacter parent, GameObject skineObject)
	{
		BoneSave component = skineObject.GetComponent<BoneSave>();
		List<string> list = component.list;
		List<Transform> list2 = new List<Transform>();
		if (parent.BonesTrsDict == null)
		{
			parent.BonesTrsDict = parent.GetComponentInChildren<TransDicts>().TransDict;
		}
		for (int i = 0; i < list.Count; i++)
		{
			Transform transform = parent.BonesTrsDict[list[i]];
			if (transform != null)
			{
				list2.Add(transform);
			}
			else
			{
				Log.DEBUG_MSG("Find bone erro=" + list[i]);
			}
		}
		SkinnedMeshRenderer component2 = skineObject.GetComponent<SkinnedMeshRenderer>();
		component2.bones = list2.ToArray();
		component2.enabled = true;
		component2.updateWhenOffscreen = true;
		parent.AnimationLogic.AnimaObj.cullingType = 0;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00038AAC File Offset: 0x00036CAC
	public static void RebuildBones(GameObject parent, GameObject skineObject)
	{
		BoneSave component = skineObject.GetComponent<BoneSave>();
		List<string> list = component.list;
		List<Transform> list2 = new List<Transform>();
		Dictionary<string, Transform> transDict;
		if (!parent.gameObject.activeSelf)
		{
			UnityVersionUtil.SetActiveRecursive(parent.gameObject, true);
			TransDicts transDicts = parent.GetComponent<TransDicts>();
			if (transDicts == null)
			{
				transDicts = parent.GetComponentInChildren<TransDicts>();
			}
			transDict = transDicts.TransDict;
			UnityVersionUtil.SetActiveRecursive(parent.gameObject, false);
		}
		else
		{
			TransDicts transDicts2 = parent.GetComponent<TransDicts>();
			if (transDicts2 == null)
			{
				transDicts2 = parent.GetComponentInChildren<TransDicts>();
			}
			transDict = transDicts2.TransDict;
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (transDict.ContainsKey(list[i]))
			{
				Transform transform = transDict[list[i]];
				if (transform != null)
				{
					list2.Add(transform);
				}
				else
				{
					Log.DEBUG_MSG("Find bone erro=" + list[i]);
				}
			}
		}
		SkinnedMeshRenderer component2 = skineObject.GetComponent<SkinnedMeshRenderer>();
		component2.bones = list2.ToArray();
		component2.enabled = true;
		parent.animation.cullingType = 0;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00038BE0 File Offset: 0x00036DE0
	public static string GetDataLocalUrl(string folderPath, string fileName)
	{
		if (folderPath.EndsWith("/"))
		{
			folderPath = folderPath.Substring(0, folderPath.Length - 1);
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			FileUpdateHelper.GetLocalPathRoot(),
			folderPath,
			"/",
			fileName
		});
		string text = BundleManager.sb.ToString();
		if (PlayerData.downLoadFlag == 1 && File.Exists(text))
		{
			BundleManager.sb.Length = 0;
			return BundleManager.sb.AppendFormat("{0}{1}", "file:///", text).ToString();
		}
		BundleManager.sb.Length = 0;
		return BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			Application.streamingAssetsPath,
			folderPath,
			"/",
			fileName
		}).ToString();
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00038CD8 File Offset: 0x00036ED8
	public static string GetLocalUrl(string folderPath, string fileName)
	{
		if (folderPath.EndsWith("/"))
		{
			folderPath = folderPath.Substring(0, folderPath.Length - 1);
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			FileUpdateHelper.GetLocalPathRoot(),
			folderPath,
			"/",
			fileName
		});
		string text = BundleManager.sb.ToString();
		if (File.Exists(text))
		{
			BundleManager.sb.Length = 0;
			return BundleManager.sb.AppendFormat("{0}{1}", "file:///", text).ToString();
		}
		BundleManager.sb.Length = 0;
		return BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			Application.streamingAssetsPath,
			folderPath,
			"/",
			fileName
		}).ToString();
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00038DC4 File Offset: 0x00036FC4
	public static string GetLocalCheckPath(string folderPath, string fileName)
	{
		if (folderPath.EndsWith("/"))
		{
			folderPath = folderPath.Substring(0, folderPath.Length - 1);
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			FileUpdateHelper.GetLocalPathRoot(),
			folderPath,
			"/",
			fileName
		});
		return BundleManager.sb.ToString();
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00038E44 File Offset: 0x00037044
	public static string GetLocalStreamUrl(string folderPath, string fileName)
	{
		if (folderPath.EndsWith("/"))
		{
			folderPath = folderPath.Substring(0, folderPath.Length - 1);
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			FileUpdateHelper.GetLocalPathRoot(),
			folderPath,
			"/",
			fileName
		});
		string text = BundleManager.sb.ToString();
		BundleManager.sb.Length = 0;
		return BundleManager.sb.AppendFormat("{0}{1}{2}{3}", new object[]
		{
			Application.streamingAssetsPath,
			folderPath,
			"/",
			fileName
		}).ToString();
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00038F00 File Offset: 0x00037100
	public static IEnumerator LoadScene(string sceneName, BundleManager.OnLoadSceneFinish onLoadFinish)
	{
		if (BundleManager.mCommonSceneBundle == null)
		{
			BundleManager.sb.Length = 0;
			BundleManager.sb1.Length = 0;
			string commonLoadPath = BundleManager.GetLocalUrl(BundleManager.sb.AppendFormat("{0}{1}", BundleManager.BundleRoot, "/Scene").ToString(), BundleManager.sb1.AppendFormat("CommonPrefab{0}", ".bundle").ToString());
			WWW commonWWW = new WWW(commonLoadPath);
			yield return commonWWW;
			BundleManager.mCommonSceneBundle = commonWWW.assetBundle;
		}
		if (BundleManager.mSceneBundleCacheDic.ContainsKey(sceneName))
		{
			BundleManager.mCacheSceneList.Remove(sceneName);
			BundleManager.mCacheSceneList.Add(sceneName);
			BundleManager.mCommonSceneBundle.LoadAll();
			BundleManager.mSceneBundleCacheDic[sceneName].LoadAll();
			if (onLoadFinish != null)
			{
				onLoadFinish(true, sceneName, BundleManager.mSceneBundleCacheDic[sceneName]);
			}
			yield break;
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		string loadPath = BundleManager.GetLocalUrl(BundleManager.sb.AppendFormat("{0}{1}", BundleManager.BundleRoot, "/Scene").ToString(), BundleManager.sb1.AppendFormat("{0}{1}", sceneName, ".bundle").ToString());
		WWW www = new WWW(loadPath);
		yield return www;
		bool isSuccess = false;
		AssetBundle assetBundle = null;
		if (string.IsNullOrEmpty(www.error))
		{
			if (BundleManager.mCacheSceneList.Count >= GameSettingData.MaxSceneCache[GameSettingData.GetPhoneClass()])
			{
				BundleManager.UnloadBundle(BundleManager.mSceneBundleCacheDic[BundleManager.mCacheSceneList[0]], true);
				BundleManager.mSceneBundleCacheDic.Remove(BundleManager.mCacheSceneList[0]);
				BundleManager.mCacheSceneList.RemoveAt(0);
			}
			BundleManager.mCommonSceneBundle.LoadAll();
			assetBundle = www.assetBundle;
			assetBundle.LoadAll();
			BundleManager.mCacheSceneList.Add(sceneName);
			BundleManager.mSceneBundleCacheDic.Add(sceneName, assetBundle);
			isSuccess = true;
		}
		else
		{
			Debug.Log(www.error);
		}
		if (onLoadFinish != null)
		{
			onLoadFinish(isSuccess, sceneName, assetBundle);
		}
		yield break;
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00038F30 File Offset: 0x00037130
	public static IEnumerator LoadSceneActivityObj(string ActObjName, SceneComponentData data, BundleManager.OnLoadActObjFinish onLoadFinish)
	{
		if (BundleManager.mCommonActObjBundle == null)
		{
			BundleManager.sb.Length = 0;
			BundleManager.sb1.Length = 0;
			string commonLoadPath = BundleManager.GetLocalUrl(BundleManager.CommonRootPath, "CommonobjPrefab.bundle");
			WWW commonWWW = new WWW(commonLoadPath);
			yield return commonWWW;
			BundleManager.mCommonActObjBundle = commonWWW.assetBundle;
		}
		if (BundleManager.mActivityBundleCacheDic.ContainsKey(ActObjName))
		{
			BundleManager.mCacheActObjList.Remove(ActObjName);
			BundleManager.mCacheActObjList.Add(ActObjName);
			BundleManager.mCommonActObjBundle.LoadAll();
			BundleManager.mActivityBundleCacheDic[ActObjName].LoadAll();
			if (onLoadFinish != null)
			{
				onLoadFinish(ActObjName, data, BundleManager.mActivityBundleCacheDic[ActObjName].mainAsset);
			}
			yield break;
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		string loadPath = BundleManager.GetLocalUrl(BundleManager.ActObjRootPath, ActObjName + ".bundle");
		WWW www = new WWW(loadPath);
		yield return www;
		AssetBundle assetBundle = null;
		if (string.IsNullOrEmpty(www.error))
		{
			BundleManager.mCommonActObjBundle.LoadAll();
			assetBundle = www.assetBundle;
			assetBundle.LoadAll();
			BundleManager.mCacheActObjList.Add(ActObjName);
			BundleManager.mActivityBundleCacheDic.Add(ActObjName, assetBundle);
			if (onLoadFinish != null)
			{
				onLoadFinish(ActObjName, data, assetBundle.mainAsset);
			}
		}
		else
		{
			Debug.Log(www.error);
		}
		yield break;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00038F70 File Offset: 0x00037170
	public static void UnloadCacheActObj()
	{
		for (int i = BundleManager.mCacheActObjList.Count - 1; i >= 0; i--)
		{
			BundleManager.UnloadBundle(BundleManager.mActivityBundleCacheDic[BundleManager.mCacheActObjList[i]], true);
			BundleManager.mActivityBundleCacheDic.Remove(BundleManager.mCacheActObjList[i]);
			BundleManager.mCacheActObjList.RemoveAt(i);
		}
		if (BundleManager.UnloadBundle(BundleManager.mCommonActObjBundle, true))
		{
			BundleManager.mCommonActObjBundle = null;
		}
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00038FF0 File Offset: 0x000371F0
	public static IEnumerator LoadSceneAnima(string sceneAnimaName, BundleManager.OnLoadSceneFinish onLoadFinish)
	{
		if (BundleManager.mCommonSceneBundle == null)
		{
			BundleManager.sb.Length = 0;
			BundleManager.sb1.Length = 0;
			string commonLoadPath = BundleManager.GetLocalUrl(BundleManager.sb.AppendFormat("{0}{1}", BundleManager.BundleRoot, "/Scene").ToString(), BundleManager.sb1.AppendFormat("CommonPrefab{0}", ".bundle").ToString());
			WWW commonWWW = new WWW(commonLoadPath);
			yield return commonWWW;
			BundleManager.mCommonSceneBundle = commonWWW.assetBundle;
		}
		BundleManager.sb.Length = 0;
		BundleManager.sb1.Length = 0;
		string loadPath = BundleManager.GetLocalUrl(BundleManager.sb.AppendFormat("{0}{1}", BundleManager.BundleRoot, "/StartSceneAnima").ToString(), BundleManager.sb1.AppendFormat("{0}{1}", sceneAnimaName, ".bundle").ToString());
		WWW www = new WWW(loadPath);
		yield return www;
		bool isSuccess = false;
		AssetBundle assetBundle = null;
		if (string.IsNullOrEmpty(www.error))
		{
			BundleManager.mCommonSceneBundle.LoadAll();
			assetBundle = www.assetBundle;
			assetBundle.LoadAll();
			isSuccess = true;
			if (onLoadFinish != null)
			{
				onLoadFinish(isSuccess, sceneAnimaName, assetBundle);
			}
			assetBundle.Unload(false);
		}
		else if (onLoadFinish != null)
		{
			onLoadFinish(isSuccess, sceneAnimaName, assetBundle);
		}
		yield break;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00039020 File Offset: 0x00037220
	public static BundleManager.LoadModelData LoadEffectInList(string effectName, bool isNeedUnload, bool isDoNotCache = false, BundleManager.OnLoadModelFinish onFinished = null, object paramData1 = null, object paramData2 = null)
	{
		BundleManager.LoadModelData loadModelData = new BundleManager.LoadModelData(effectName, isNeedUnload, BundleManager.LoadModelData.BundleType.EFFECT, isDoNotCache, onFinished, paramData1, paramData2, null);
		if (BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.ModelName) && BundleManager.mModelBundleCacheDic[loadModelData.ModelName].IsBundleValid())
		{
			if (onFinished != null)
			{
				BundleManager.mModelBundleCacheDic[loadModelData.ModelName].AddBundleUsingCount();
				Object objBundle = Object.Instantiate(BundleManager.mModelBundleCacheDic[loadModelData.ModelName].GetBundle().mainAsset);
				onFinished(objBundle, paramData1, paramData2);
			}
			return null;
		}
		if (!string.IsNullOrEmpty(loadModelData.SubPath) && !BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.SubPath))
		{
			BundleManager.mModelBundleCacheDic.Add(loadModelData.SubPath, new AssetBundleData(null, 0, loadModelData.SubPath, string.Empty, true));
		}
		if (!BundleManager.mModelBundleCacheDic.ContainsKey(loadModelData.ModelName))
		{
			BundleManager.mModelBundleCacheDic.Add(loadModelData.ModelName, new AssetBundleData(null, 0, loadModelData.ModelName, loadModelData.SubPath, false));
		}
		BundleManager.mModelBundleCacheDic[loadModelData.ModelName].AddBundleUsingCount();
		BundleManager.mLoadModelList.Add(loadModelData);
		return loadModelData;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00039158 File Offset: 0x00037358
	private static IEnumerator LoadEffectCommonFile()
	{
		BundleManager.effectCommonFileLoadingFlag = true;
		WWW wwwEffectCommonShader = new WWW(BundleManager.GetLocalUrl(BundleManager.BundleEffectRootPath, "EffectCommonShader.bundle"));
		yield return wwwEffectCommonShader;
		if (wwwEffectCommonShader.assetBundle != null)
		{
			wwwEffectCommonShader.assetBundle.LoadAll();
			BundleManager.mEffectCommonShaderBundle = wwwEffectCommonShader.assetBundle;
		}
		else
		{
			Debug.Log("no shader LoadModelFromList");
		}
		WWW wwwCommonPic = new WWW(BundleManager.GetLocalUrl(BundleManager.BundleEffectRootPath, "CommonPic.bundle"));
		yield return wwwCommonPic;
		if (wwwCommonPic.assetBundle != null)
		{
			wwwCommonPic.assetBundle.LoadAll();
			BundleManager.mEffectCommonPicBundle = wwwCommonPic.assetBundle;
		}
		else
		{
			Debug.Log("no shader LoadModelFromList");
		}
		yield break;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0003916C File Offset: 0x0003736C
	public static IEnumerator LoadDownloadAnimationBundle(BundleManager.OnLoadAnimationFinishedDelegate finishfun)
	{
		if (!BundleManager.DownloadAnimationFlag)
		{
			BundleManager.DownloadAnimationFlag = true;
			for (int i = 0; i < BundleManager.AnimationFileNameList.Count; i++)
			{
				if (BundleManager.mAnimationBundleDic.ContainsKey(BundleManager.AnimationFileNameList[i]) && BundleManager.UnloadBundle(BundleManager.mAnimationBundleDic[BundleManager.AnimationFileNameList[i]], false))
				{
					BundleManager.mAnimationBundleDic.Remove(BundleManager.AnimationFileNameList[i]);
				}
				if (!BundleManager.mAnimationBundleDic.ContainsKey(BundleManager.AnimationFileNameList[i]))
				{
					WWW wwwAnima = new WWW(BundleManager.GetLocalUrl(BundleManager.AnimationRootPath, BundleManager.AnimationFileNameList[i] + ".Bundle"));
					yield return wwwAnima;
					if (string.IsNullOrEmpty(wwwAnima.error))
					{
						if (wwwAnima.assetBundle != null)
						{
							BundleManager.mAnimationBundleDic.Add(BundleManager.AnimationFileNameList[i], wwwAnima.assetBundle);
						}
						else
						{
							Debug.Log("no animation bundle" + BundleManager.AnimationFileNameList[i]);
						}
					}
					else
					{
						Debug.Log(wwwAnima.error);
					}
				}
			}
			if (finishfun != null)
			{
				finishfun();
			}
			BundleManager.DownloadAnimationFlag = false;
		}
		yield break;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00039190 File Offset: 0x00037390
	public static IEnumerator LoadStreamAnimationBundle(BundleManager.OnLoadAnimationFinishedDelegate finishfun)
	{
		if (!BundleManager.StreamAnimationFlag)
		{
			BundleManager.StreamAnimationFlag = true;
			for (int i = 0; i < BundleManager.AnimationStreamFileNameList.Count; i++)
			{
				if (BundleManager.mAnimationBundleDic.ContainsKey(BundleManager.AnimationStreamFileNameList[i]) && BundleManager.UnloadBundle(BundleManager.mAnimationBundleDic[BundleManager.AnimationStreamFileNameList[i]], false))
				{
					BundleManager.mAnimationBundleDic.Remove(BundleManager.AnimationStreamFileNameList[i]);
				}
				if (!BundleManager.mAnimationBundleDic.ContainsKey(BundleManager.AnimationStreamFileNameList[i]))
				{
					WWW wwwAnima = new WWW(BundleManager.GetLocalUrl(BundleManager.AnimationRootPath, BundleManager.AnimationStreamFileNameList[i] + ".bundle"));
					yield return wwwAnima;
					if (string.IsNullOrEmpty(wwwAnima.error))
					{
						if (wwwAnima.assetBundle != null)
						{
							BundleManager.mAnimationBundleDic.Add(BundleManager.AnimationStreamFileNameList[i], wwwAnima.assetBundle);
						}
						else
						{
							Debug.Log("no animation bundle" + BundleManager.AnimationStreamFileNameList[i]);
						}
					}
					else
					{
						Debug.Log(wwwAnima.error);
					}
				}
			}
			if (finishfun != null)
			{
				finishfun();
			}
			BundleManager.StreamAnimationFlag = false;
		}
		yield break;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x000391B4 File Offset: 0x000373B4
	public static Object LoadAnimation(string BundleName, string AnimationName)
	{
		if (BundleManager.mAnimationBundleDic.ContainsKey(BundleName) && BundleManager.mAnimationBundleDic[BundleName] != null)
		{
			return BundleManager.mAnimationBundleDic[BundleName].Load(AnimationName, typeof(Object));
		}
		return null;
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x00039208 File Offset: 0x00037408
	public static AssetBundle DataBundle
	{
		get
		{
			return BundleManager.mDataBundle;
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00039210 File Offset: 0x00037410
	public static IEnumerator LoadData(BundleManager.OnLoadDataFinishedDelegate onFinished)
	{
		BundleManager.mWaittingLoadDataFlag = false;
		if (BundleManager.mLoadingDataBundleFlag)
		{
			BundleManager.mWaittingLoadDataFlag = true;
		}
		else
		{
			BundleManager.mLoadingDataBundleFlag = true;
			WWW wwwData = new WWW(BundleManager.GetDataLocalUrl(BundleManager.DataRootPath, BundleManager.DataFileName));
			yield return wwwData;
			if (BundleManager.mWaittingLoadDataFlag)
			{
				BundleManager.mWaittingLoadDataFlag = false;
			}
			BundleManager.mLoadingDataBundleFlag = false;
			if (wwwData.assetBundle != null)
			{
				BundleManager.mDataBundle = wwwData.assetBundle;
				if (onFinished != null)
				{
					onFinished();
				}
			}
			else
			{
				Debug.Log("no shader LoadModelFromList");
			}
		}
		yield break;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00039234 File Offset: 0x00037434
	public static object LoadTable(string fileName)
	{
		if (BundleManager.mDataBundle == null)
		{
			Debug.Log("mDataBundle == null :: " + fileName);
			return null;
		}
		return BundleManager.mDataBundle.Load(fileName, typeof(object));
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0003927C File Offset: 0x0003747C
	public static void UnloadDataBundle()
	{
		if (BundleManager.UnloadBundle(BundleManager.mDataBundle, true))
		{
			BundleManager.mDataBundle = null;
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00039294 File Offset: 0x00037494
	public static IEnumerator LoadSound(string soundPath, string name, BundleManager.LoadSoundFinish delFinish, object param1 = null, object param2 = null, object param3 = null)
	{
		WWW wwwSound = new WWW(BundleManager.GetLocalUrl(BundleManager.SoundRootPath + soundPath, name + ".bundle"));
		yield return wwwSound;
		AudioClip retObj = null;
		if (string.IsNullOrEmpty(wwwSound.error))
		{
			if (wwwSound.assetBundle != null)
			{
				retObj = (wwwSound.assetBundle.mainAsset as AudioClip);
				if (delFinish != null)
				{
					delFinish(soundPath + name, retObj, param1, param2, param3);
				}
				BundleManager.UnloadBundle(wwwSound.assetBundle, false);
			}
			else if (delFinish != null)
			{
				delFinish(soundPath + name, null, param1, param2, param3);
			}
		}
		else
		{
			Debug.Log(wwwSound.error);
		}
		yield break;
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00039300 File Offset: 0x00037500
	public static IEnumerator LoadTexture(string name, BundleManager.LoadTextureFinish delFinish)
	{
		if (BundleManager.mTextureDic.ContainsKey(name))
		{
			BundleManager.mTextureDic[name].useTimes++;
			BundleManager.mTextureDic[name].m_LastActiveTime = Time.realtimeSinceStartup;
			if (delFinish != null)
			{
				delFinish(name, BundleManager.mTextureDic[name].mTexture);
			}
		}
		else
		{
			if (BundleManager.mCurLoadingTextureList.Contains(name))
			{
				yield break;
			}
			BundleManager.mCurLoadingTextureList.Add(name);
			WWW wwwTexture = new WWW(BundleManager.GetLocalUrl(BundleManager.TextureRootPath, name + ".bundle"));
			yield return wwwTexture;
			BundleManager.mCurLoadingTextureList.Remove(name);
			Texture retObj = null;
			if (string.IsNullOrEmpty(wwwTexture.error))
			{
				if (wwwTexture.assetBundle != null)
				{
					retObj = (wwwTexture.assetBundle.mainAsset as Texture);
					BundleManager.TextureInfo newtexture = new BundleManager.TextureInfo(name, retObj, 1, Time.realtimeSinceStartup);
					BundleManager.mTextureDic.Add(name, newtexture);
					BundleManager.UnloadBundle(wwwTexture.assetBundle, false);
					BundleManager.CheckTextureCache();
				}
				else
				{
					Debug.Log("bundle no texture! name:" + name);
				}
			}
			else
			{
				Debug.Log(wwwTexture.error);
			}
			if (delFinish != null)
			{
				delFinish(name, retObj);
			}
		}
		yield break;
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00039330 File Offset: 0x00037530
	public static IEnumerator LoadTexture(List<string> namelist, BundleManager.LoadTextureDicFinish delFun)
	{
		Dictionary<string, Texture> retDic = new Dictionary<string, Texture>();
		for (int i = 0; i < namelist.Count; i++)
		{
			if (BundleManager.mTextureDic.ContainsKey(namelist[i]))
			{
				BundleManager.mTextureDic[namelist[i]].useTimes++;
				BundleManager.mTextureDic[namelist[i]].m_LastActiveTime = Time.realtimeSinceStartup;
				retDic.Add(namelist[i], BundleManager.mTextureDic[namelist[i]].mTexture);
			}
			else
			{
				WWW wwwTexture = new WWW(BundleManager.GetLocalUrl(BundleManager.TextureRootPath, namelist[i] + ".bundle"));
				yield return wwwTexture;
				Texture retObj = null;
				if (string.IsNullOrEmpty(wwwTexture.error))
				{
					if (wwwTexture.assetBundle != null)
					{
						retObj = (wwwTexture.assetBundle.mainAsset as Texture);
						BundleManager.TextureInfo newtexture = new BundleManager.TextureInfo(namelist[i], retObj, 1, Time.realtimeSinceStartup);
						BundleManager.mTextureDic.Add(namelist[i], newtexture);
						retDic.Add(namelist[i], retObj);
						BundleManager.UnloadBundle(wwwTexture.assetBundle, false);
						BundleManager.CheckTextureCache();
					}
					else
					{
						Debug.Log("bundle no texture! name:" + namelist[i]);
					}
				}
				else
				{
					Debug.Log(wwwTexture.error);
				}
			}
		}
		if (delFun != null)
		{
			delFun(retDic);
		}
		yield break;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00039360 File Offset: 0x00037560
	public static IEnumerator LoadWaitTexture(List<string> namelist, BundleManager.LoadTextureFinish delFun)
	{
		for (int i = 0; i < namelist.Count; i++)
		{
			BundleManager.LoadTextureData curtexturedata = new BundleManager.LoadTextureData(namelist[i], delFun);
			if (BundleManager.mTextureDic.ContainsKey(namelist[i]))
			{
				BundleManager.mTextureDic[namelist[i]].useTimes++;
				BundleManager.mTextureDic[namelist[i]].m_LastActiveTime = Time.realtimeSinceStartup;
				if (delFun != null)
				{
					delFun(namelist[i], BundleManager.mTextureDic[namelist[i]].mTexture);
				}
			}
			else if (!BundleManager.mLoadTextureBundle.Contains(namelist[i]))
			{
				BundleManager.mLoadTextureBundle.Add(namelist[i]);
				WWW wwwTexture = new WWW(BundleManager.GetLocalUrl(BundleManager.TextureRootPath, namelist[i] + ".bundle"));
				yield return wwwTexture;
				Texture retObj = null;
				if (string.IsNullOrEmpty(wwwTexture.error))
				{
					if (wwwTexture.assetBundle != null)
					{
						retObj = (wwwTexture.assetBundle.mainAsset as Texture);
						BundleManager.TextureInfo newtexture = new BundleManager.TextureInfo(namelist[i], retObj, 1, Time.realtimeSinceStartup);
						BundleManager.mTextureDic.Add(namelist[i], newtexture);
						if (curtexturedata.OnLoadFinished != null)
						{
							if (BundleManager.mWaitTextureDic.ContainsKey(namelist[i]))
							{
								for (int j = 0; j < BundleManager.mWaitTextureDic[namelist[i]].Count; j++)
								{
									BundleManager.LoadTextureFromCache(BundleManager.mWaitTextureDic[namelist[i]][j]);
								}
								BundleManager.mWaitTextureDic.Remove(namelist[i]);
							}
							curtexturedata.OnLoadFinished(namelist[i], retObj);
						}
						else if (BundleManager.mWaitTextureDic.ContainsKey(namelist[i]))
						{
							for (int k = 0; k < BundleManager.mWaitTextureDic[namelist[i]].Count; k++)
							{
								BundleManager.LoadTextureFromCache(BundleManager.mWaitTextureDic[namelist[i]][k]);
							}
							BundleManager.mWaitTextureDic.Remove(namelist[i]);
						}
						BundleManager.UnloadBundle(wwwTexture.assetBundle, false);
						BundleManager.CheckTextureCache();
						if (BundleManager.mLoadTextureBundle.Contains(namelist[i]))
						{
							BundleManager.mLoadTextureBundle.Remove(namelist[i]);
						}
					}
					else
					{
						Debug.Log("bundle no texture! name:" + namelist[i]);
					}
				}
				else
				{
					Debug.Log(wwwTexture.error);
				}
			}
			else
			{
				if (!BundleManager.mWaitTextureDic.ContainsKey(namelist[i]))
				{
					BundleManager.mWaitTextureDic.Add(namelist[i], new List<BundleManager.LoadTextureData>());
				}
				BundleManager.mWaitTextureDic[namelist[i]].Add(curtexturedata);
			}
		}
		yield break;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00039390 File Offset: 0x00037590
	private static void LoadTextureFromCache(BundleManager.LoadTextureData infodata)
	{
		if (BundleManager.mTextureDic.ContainsKey(infodata.TextureName))
		{
			BundleManager.mTextureDic[infodata.TextureName].useTimes++;
			BundleManager.mTextureDic[infodata.TextureName].m_LastActiveTime = Time.realtimeSinceStartup;
			if (infodata.OnLoadFinished != null)
			{
				infodata.OnLoadFinished(infodata.TextureName, BundleManager.mTextureDic[infodata.TextureName].mTexture);
			}
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0003941C File Offset: 0x0003761C
	private static void CheckTextureCache()
	{
		if (BundleManager.mTextureDic.Count >= BundleManager.MaxTextureNum)
		{
			float num = 100000000f;
			string text = string.Empty;
			foreach (BundleManager.TextureInfo textureInfo in BundleManager.mTextureDic.Values)
			{
				if (num > textureInfo.m_LastActiveTime)
				{
					text = textureInfo.name;
					num = textureInfo.m_LastActiveTime;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				BundleManager.mTextureDic.Remove(text);
			}
		}
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000394D0 File Offset: 0x000376D0
	public static void ClearCacheTexture()
	{
		BundleManager.mCurLoadingTextureList.Clear();
		BundleManager.mTextureDic.Clear();
		BundleManager.mWaitTextureDic.Clear();
		BundleManager.mLoadTextureBundle.Clear();
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00039508 File Offset: 0x00037708
	public static void UnloadTexture(string name)
	{
		if (BundleManager.mTextureDic.ContainsKey(name))
		{
			BundleManager.mTextureDic.Remove(name);
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00039528 File Offset: 0x00037728
	public static void UnloadTexture(List<string> names)
	{
		for (int i = 0; i < names.Count; i++)
		{
			BundleManager.UnloadTexture(names[i]);
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00039558 File Offset: 0x00037758
	public static void StartLoadingUItexture(MonoBehaviour mono, string name)
	{
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(BundleManager.LoadLoadingTexture(name));
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00039578 File Offset: 0x00037778
	public static IEnumerator LoadLoadingTexture(string name)
	{
		if (!(BundleManager.NextLoadingTexture != null) || !BundleManager.NextLoadingTexture.name.Equals(name))
		{
			string check_path = BundleManager.GetLocalCheckPath(BundleManager.TextureRootPath, name + ".bundle");
			if (File.Exists(check_path))
			{
				string urlstr = BundleManager.GetLocalUrl(BundleManager.TextureRootPath, name + ".bundle");
				WWW wwwTexture = new WWW(urlstr);
				yield return wwwTexture;
				Texture retObj = null;
				if (string.IsNullOrEmpty(wwwTexture.error))
				{
					if (wwwTexture.assetBundle != null)
					{
						retObj = (wwwTexture.assetBundle.mainAsset as Texture);
						BundleManager.NextLoadingTexture = retObj;
						BundleManager.UnloadBundle(wwwTexture.assetBundle, false);
					}
					else
					{
						Debug.Log("bundle no texture! name:" + name);
					}
				}
				else
				{
					Debug.Log(wwwTexture.error);
				}
			}
			else
			{
				Debug.Log("bundle no exit texture! name:" + name);
			}
		}
		yield break;
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0003959C File Offset: 0x0003779C
	public static Texture GetNextLoadingTexture()
	{
		return BundleManager.NextLoadingTexture;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000395A4 File Offset: 0x000377A4
	public static IEnumerator LoadItem(string name, BundleManager.LoaditemsFinish delFinish, object param1 = null, object param2 = null)
	{
		WWW wwwTexture = new WWW(BundleManager.GetLocalUrl(BundleManager.ItemsRootPath, name + ".bundle"));
		yield return wwwTexture;
		Object retObj = null;
		if (string.IsNullOrEmpty(wwwTexture.error))
		{
			if (wwwTexture.assetBundle != null)
			{
				retObj = wwwTexture.assetBundle.mainAsset;
				if (delFinish != null)
				{
					delFinish(name, retObj, param1, param2);
				}
				BundleManager.UnloadBundle(wwwTexture.assetBundle, false);
			}
			else if (delFinish != null)
			{
				delFinish(name, null, param1, param2);
			}
		}
		else
		{
			Debug.Log(wwwTexture.error);
		}
		yield break;
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000395F0 File Offset: 0x000377F0
	public static void PrintCurCacheDic()
	{
		Debug.Log("================================");
		List<AssetBundleData> list = new List<AssetBundleData>(BundleManager.mModelBundleCacheDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].IsBundleValid())
			{
				if (list[i].Bundle.mainAsset != null)
				{
					Debug.Log(string.Concat(new object[]
					{
						"name :: ",
						list[i].Bundle.mainAsset.name,
						" :: count :: ",
						list[i].UsingCount
					}));
				}
				else
				{
					Debug.Log(list[i].SelfURL + " :: " + list[i].UsingCount);
				}
			}
			else
			{
				Debug.Log(string.Concat(new object[]
				{
					"Bundle Not Valid :: ",
					list[i].SelfURL,
					" :: ",
					list[i].UsingCount
				}));
			}
		}
		Debug.Log("==============end===============");
	}

	// Token: 0x040006BE RID: 1726
	protected const string Separator = "/";

	// Token: 0x040006BF RID: 1727
	protected const string BundleSuffix = ".bundle";

	// Token: 0x040006C0 RID: 1728
	protected const string CommonShader = "CommonShader.bundle";

	// Token: 0x040006C1 RID: 1729
	protected const string FilePrefix = "file:///";

	// Token: 0x040006C2 RID: 1730
	protected const string SceneRoot = "/Scene";

	// Token: 0x040006C3 RID: 1731
	protected const string SceneAnimaRoot = "/StartSceneAnima";

	// Token: 0x040006C4 RID: 1732
	private const string EffectCommonShader = "EffectCommonShader.bundle";

	// Token: 0x040006C5 RID: 1733
	private const string CommonPic = "CommonPic.bundle";

	// Token: 0x040006C6 RID: 1734
	public static bool IsCanUnloadBundle = true;

	// Token: 0x040006C7 RID: 1735
	public static string BundleRoot = "/Bundle";

	// Token: 0x040006C8 RID: 1736
	public static string BundleModelRootPath = BundleManager.BundleRoot + "/Model";

	// Token: 0x040006C9 RID: 1737
	public static StringBuilder sb = new StringBuilder(512);

	// Token: 0x040006CA RID: 1738
	public static StringBuilder sb1 = new StringBuilder(512);

	// Token: 0x040006CB RID: 1739
	public static bool ModelLoadingShaderFlag = false;

	// Token: 0x040006CC RID: 1740
	private static AssetBundle mModelCommonShaderBundle = null;

	// Token: 0x040006CD RID: 1741
	private static List<BundleManager.LoadModelData> mLoadModelList = new List<BundleManager.LoadModelData>();

	// Token: 0x040006CE RID: 1742
	private static List<string> mLoadingBundle = new List<string>();

	// Token: 0x040006CF RID: 1743
	private static Dictionary<string, List<BundleManager.LoadModelData>> mWaitingBundleDic = new Dictionary<string, List<BundleManager.LoadModelData>>();

	// Token: 0x040006D0 RID: 1744
	private static Dictionary<string, AssetBundleData> mModelBundleCacheDic = new Dictionary<string, AssetBundleData>();

	// Token: 0x040006D1 RID: 1745
	public static string FontUIName = "Font.bundle";

	// Token: 0x040006D2 RID: 1746
	public static string CommonUIName = "Common.bundle";

	// Token: 0x040006D3 RID: 1747
	public static string UIPathRoot = "/UI";

	// Token: 0x040006D4 RID: 1748
	public static string CommonUIPath = "/UI/Common";

	// Token: 0x040006D5 RID: 1749
	public static string GameUIPath = "/UI/Common/GameUI";

	// Token: 0x040006D6 RID: 1750
	public static string MenuUIPath = "/UI/Common/MenuUI";

	// Token: 0x040006D7 RID: 1751
	private static AssetBundle mFontUIBundle = null;

	// Token: 0x040006D8 RID: 1752
	private static AssetBundle mCommonUIBundle = null;

	// Token: 0x040006D9 RID: 1753
	private static List<string> mGameUIBundleList = new List<string>();

	// Token: 0x040006DA RID: 1754
	private static List<string> mMenuUIBundleList = new List<string>();

	// Token: 0x040006DB RID: 1755
	private static List<string> mCommonUIBundleList = new List<string>();

	// Token: 0x040006DC RID: 1756
	private static Dictionary<string, AssetBundleData> mUICacheBundleDic = new Dictionary<string, AssetBundleData>();

	// Token: 0x040006DD RID: 1757
	private static List<string> loadingUIBundleList = new List<string>();

	// Token: 0x040006DE RID: 1758
	private static Dictionary<string, int> waitingUIBundleNum = new Dictionary<string, int>();

	// Token: 0x040006DF RID: 1759
	private static Dictionary<string, AssetBundle> mSceneBundleCacheDic = new Dictionary<string, AssetBundle>();

	// Token: 0x040006E0 RID: 1760
	private static List<string> mCacheSceneList = new List<string>();

	// Token: 0x040006E1 RID: 1761
	private static AssetBundle mCommonSceneBundle = null;

	// Token: 0x040006E2 RID: 1762
	public static string CommonRootPath = BundleManager.BundleRoot + "/Activity/CommonObj";

	// Token: 0x040006E3 RID: 1763
	public static string ActObjRootPath = BundleManager.BundleRoot + "/Activity/Obj";

	// Token: 0x040006E4 RID: 1764
	private static Dictionary<string, AssetBundle> mActivityBundleCacheDic = new Dictionary<string, AssetBundle>();

	// Token: 0x040006E5 RID: 1765
	private static List<string> mCacheActObjList = new List<string>();

	// Token: 0x040006E6 RID: 1766
	private static AssetBundle mCommonActObjBundle = null;

	// Token: 0x040006E7 RID: 1767
	private static string BundleEffectRootPath = BundleManager.BundleRoot + "/Effect";

	// Token: 0x040006E8 RID: 1768
	private static bool effectCommonFileLoadingFlag = false;

	// Token: 0x040006E9 RID: 1769
	private static AssetBundle mEffectCommonShaderBundle = null;

	// Token: 0x040006EA RID: 1770
	private static AssetBundle mEffectCommonPicBundle = null;

	// Token: 0x040006EB RID: 1771
	private static string AnimationRootPath = BundleManager.BundleRoot + "/Animation";

	// Token: 0x040006EC RID: 1772
	private static List<string> AnimationStreamFileNameList;

	// Token: 0x040006ED RID: 1773
	private static List<string> AnimationFileNameList;

	// Token: 0x040006EE RID: 1774
	private static Dictionary<string, AssetBundle> mAnimationBundleDic;

	// Token: 0x040006EF RID: 1775
	private static bool DownloadAnimationFlag;

	// Token: 0x040006F0 RID: 1776
	private static bool StreamAnimationFlag;

	// Token: 0x040006F1 RID: 1777
	private static string DataRootPath;

	// Token: 0x040006F2 RID: 1778
	private static string DataFileName;

	// Token: 0x040006F3 RID: 1779
	private static AssetBundle mDataBundle;

	// Token: 0x040006F4 RID: 1780
	private static bool mLoadingDataBundleFlag;

	// Token: 0x040006F5 RID: 1781
	private static bool mWaittingLoadDataFlag;

	// Token: 0x040006F6 RID: 1782
	private static string SoundRootPath;

	// Token: 0x040006F7 RID: 1783
	private static string TextureRootPath;

	// Token: 0x040006F8 RID: 1784
	public static int MaxTextureNum;

	// Token: 0x040006F9 RID: 1785
	public static Dictionary<string, BundleManager.TextureInfo> mTextureDic;

	// Token: 0x040006FA RID: 1786
	public static List<string> mCurLoadingTextureList;

	// Token: 0x040006FB RID: 1787
	public static List<string> mLoadTextureBundle;

	// Token: 0x040006FC RID: 1788
	public static Dictionary<string, List<BundleManager.LoadTextureData>> mWaitTextureDic;

	// Token: 0x040006FD RID: 1789
	public static Texture NextLoadingTexture;

	// Token: 0x040006FE RID: 1790
	private static string ItemsRootPath;

	// Token: 0x020000F3 RID: 243
	public class LoadModelData
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x0003972C File Offset: 0x0003792C
		public LoadModelData(string modelName, bool isNeedUnload, BundleManager.LoadModelData.BundleType bType = BundleManager.LoadModelData.BundleType.MODEL, bool isDoNotCache = false, BundleManager.OnLoadModelFinish onFinished = null, object paramData1 = null, object paramData2 = null, string sPath = null)
		{
			this.ModelName = modelName;
			this.OnLoadFinished = onFinished;
			this.param1 = paramData1;
			this.param2 = paramData2;
			this.SubPath = sPath;
			this.DoNotCache = isDoNotCache;
			this.bundleType = bType;
			this.IsNeedUnload = isNeedUnload;
			this.ID = UUID.GenUUID();
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x000397B0 File Offset: 0x000379B0
		public string LoadURL
		{
			get
			{
				if (string.IsNullOrEmpty(this.mLoadURL))
				{
					this.mLoadURL = this.GetLoadUrl();
				}
				return this.mLoadURL;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000397E0 File Offset: 0x000379E0
		private string GetLoadUrl()
		{
			BundleManager.sb.Length = 0;
			BundleManager.sb1.Length = 0;
			string fileName = BundleManager.sb.AppendFormat("{0}{1}", this.ModelName, ".bundle").ToString();
			if (this.bundleType == BundleManager.LoadModelData.BundleType.MODEL)
			{
				return BundleManager.GetLocalUrl(BundleManager.sb1.AppendFormat("{0}{1}{2}", BundleManager.BundleModelRootPath, "/", this.SubPath).ToString(), fileName);
			}
			return BundleManager.GetLocalUrl(BundleManager.BundleEffectRootPath, fileName);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00039864 File Offset: 0x00037A64
		public static string GetLoadUrl(string modelName, string subPath)
		{
			BundleManager.sb.Length = 0;
			BundleManager.sb1.Length = 0;
			string fileName = BundleManager.sb.AppendFormat("{0}{1}", modelName, ".bundle").ToString();
			return BundleManager.GetLocalUrl(BundleManager.sb1.AppendFormat("{0}{1}{2}", BundleManager.BundleModelRootPath, "/", subPath).ToString(), fileName);
		}

		// Token: 0x040006FF RID: 1791
		public string ModelName = string.Empty;

		// Token: 0x04000700 RID: 1792
		public BundleManager.OnLoadModelFinish OnLoadFinished;

		// Token: 0x04000701 RID: 1793
		public object param1;

		// Token: 0x04000702 RID: 1794
		public object param2;

		// Token: 0x04000703 RID: 1795
		public string SubPath = string.Empty;

		// Token: 0x04000704 RID: 1796
		public bool DoNotCache;

		// Token: 0x04000705 RID: 1797
		public BundleManager.LoadModelData.BundleType bundleType;

		// Token: 0x04000706 RID: 1798
		public bool IsNeedUnload = true;

		// Token: 0x04000707 RID: 1799
		public long ID;

		// Token: 0x04000708 RID: 1800
		private string mLoadURL = string.Empty;

		// Token: 0x020000F4 RID: 244
		public enum BundleType
		{
			// Token: 0x0400070A RID: 1802
			MODEL,
			// Token: 0x0400070B RID: 1803
			EFFECT
		}
	}

	// Token: 0x020000F5 RID: 245
	public class TextureInfo
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x000398C8 File Offset: 0x00037AC8
		public TextureInfo(string Name, Texture curTex, int use = 0, float time = 0f)
		{
			this.name = Name;
			this.mTexture = curTex;
			this.useTimes = use;
			this.m_LastActiveTime = time;
		}

		// Token: 0x0400070C RID: 1804
		public string name;

		// Token: 0x0400070D RID: 1805
		public Texture mTexture;

		// Token: 0x0400070E RID: 1806
		public int useTimes;

		// Token: 0x0400070F RID: 1807
		public float m_LastActiveTime;
	}

	// Token: 0x020000F6 RID: 246
	public class LoadTextureData
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x000398F0 File Offset: 0x00037AF0
		public LoadTextureData(string texname, BundleManager.LoadTextureFinish onFinished = null)
		{
			this.TextureName = texname;
			this.OnLoadFinished = onFinished;
		}

		// Token: 0x04000710 RID: 1808
		public string TextureName = string.Empty;

		// Token: 0x04000711 RID: 1809
		public BundleManager.LoadTextureFinish OnLoadFinished;
	}

	// Token: 0x02000AAD RID: 2733
	// (Invoke) Token: 0x06004F3D RID: 20285
	public delegate void OnLoadModelFinish(object objBundle, object param1 = null, object param2 = null);

	// Token: 0x02000AAE RID: 2734
	// (Invoke) Token: 0x06004F41 RID: 20289
	public delegate void LoadUIBundleFinish(UIPathData uiData, GameObject retObj, UIManager.OnOpenUIDelegate param1, object param2);

	// Token: 0x02000AAF RID: 2735
	// (Invoke) Token: 0x06004F45 RID: 20293
	public delegate void OnLoadSceneFinish(bool isSuccess, string sceneName, AssetBundle sceneBundle);

	// Token: 0x02000AB0 RID: 2736
	// (Invoke) Token: 0x06004F49 RID: 20297
	public delegate void OnLoadActObjFinish(string name, SceneComponentData data, Object obj);

	// Token: 0x02000AB1 RID: 2737
	// (Invoke) Token: 0x06004F4D RID: 20301
	public delegate void OnLoadAnimationFinishedDelegate();

	// Token: 0x02000AB2 RID: 2738
	// (Invoke) Token: 0x06004F51 RID: 20305
	public delegate void OnLoadDataFinishedDelegate();

	// Token: 0x02000AB3 RID: 2739
	// (Invoke) Token: 0x06004F55 RID: 20309
	public delegate void LoadSoundFinish(string modelName, AudioClip audioClip, object param1, object param2, object param3 = null);

	// Token: 0x02000AB4 RID: 2740
	// (Invoke) Token: 0x06004F59 RID: 20313
	public delegate void LoadTextureFinish(string name, Texture textureObj);

	// Token: 0x02000AB5 RID: 2741
	// (Invoke) Token: 0x06004F5D RID: 20317
	public delegate void LoadTextureDicFinish(Dictionary<string, Texture> DicTex);

	// Token: 0x02000AB6 RID: 2742
	// (Invoke) Token: 0x06004F61 RID: 20321
	public delegate void LoaditemsFinish(string name, Object obj, object param1 = null, object param2 = null);
}
