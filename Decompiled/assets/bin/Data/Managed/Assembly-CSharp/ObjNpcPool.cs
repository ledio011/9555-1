using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000826 RID: 2086
public class ObjNpcPool
{
	// Token: 0x06003471 RID: 13425 RVA: 0x000D2C88 File Offset: 0x000D0E88
	public ObjNpcPool()
	{
		this.Reset();
	}

	// Token: 0x17000EA6 RID: 3750
	// (get) Token: 0x06003472 RID: 13426 RVA: 0x000D2CB8 File Offset: 0x000D0EB8
	public string ID
	{
		get
		{
			return this.mID;
		}
	}

	// Token: 0x17000EA7 RID: 3751
	// (get) Token: 0x06003473 RID: 13427 RVA: 0x000D2CC0 File Offset: 0x000D0EC0
	public List<ObjNPC> EnableNpcList
	{
		get
		{
			return this.mEnableNpcList;
		}
	}

	// Token: 0x17000EA8 RID: 3752
	// (get) Token: 0x06003474 RID: 13428 RVA: 0x000D2CC8 File Offset: 0x000D0EC8
	// (set) Token: 0x06003475 RID: 13429 RVA: 0x000D2CD0 File Offset: 0x000D0ED0
	public int MaxPoolNum
	{
		get
		{
			return this.mMaxPoolNum;
		}
		set
		{
			this.mMaxPoolNum = value;
		}
	}

	// Token: 0x17000EA9 RID: 3753
	// (get) Token: 0x06003476 RID: 13430 RVA: 0x000D2CDC File Offset: 0x000D0EDC
	public List<ObjNPC> DisableNpcList
	{
		get
		{
			return this.mDisableNpcList;
		}
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x000D2CE4 File Offset: 0x000D0EE4
	public void Reset()
	{
		for (int i = 0; i < this.mEnableNpcList.Count; i++)
		{
			Object.Destroy(this.mEnableNpcList[i]);
		}
		for (int j = 0; j < this.mDisableNpcList.Count; j++)
		{
			Object.Destroy(this.mDisableNpcList[j]);
		}
		this.mEnableNpcList.Clear();
		this.mDisableNpcList.Clear();
	}

	// Token: 0x06003478 RID: 13432 RVA: 0x000D2D64 File Offset: 0x000D0F64
	public void SetPoolMaxNum(int maxNum, string id)
	{
		this.mMaxPoolNum = maxNum;
		this.mID = id;
	}

	// Token: 0x06003479 RID: 13433 RVA: 0x000D2D74 File Offset: 0x000D0F74
	public void RefreshPoolMaxNum(int newPoolNum)
	{
		this.mMaxPoolNum = newPoolNum;
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x000D2D80 File Offset: 0x000D0F80
	public void GetNpc(ObjInitNpcData initData, ObjManager.OnGetNPC func, ObjManager.OnGetNPC onLoadModelFinished = null)
	{
		this.LastUseTime = Time.time;
		if (this.mDisableNpcList.Count > 0)
		{
			ObjNPC objNPC = this.mDisableNpcList[0];
			UnityVersionUtil.SetActiveRecursive(objNPC.gameObject, true);
			objNPC.ResetNpc(initData);
			this.mDisableNpcList.RemoveAt(0);
			this.mEnableNpcList.Add(objNPC);
			objNPC.gameObject.name = string.Concat(new object[]
			{
				"NPC",
				initData.mServerID,
				"_",
				initData.npcInfoData.ID
			});
			if (objNPC.MeshRoot != null)
			{
				if (onLoadModelFinished != null)
				{
					onLoadModelFinished(objNPC);
				}
			}
			else
			{
				ObjNpcPool.LoadNPCModelParam loadNPCModelParam = new ObjNpcPool.LoadNPCModelParam();
				loadNPCModelParam.InitData = initData;
				loadNPCModelParam.LoadFinishedFunc = onLoadModelFinished;
				objNPC.LoadingModelData = BundleManager.LoadModelInList(objNPC.CurrentCharacterModelData.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadNpcModelFinished), loadNPCModelParam, objNPC, null);
				objNPC.LoadingModelDataId = ((objNPC.LoadingModelData != null) ? objNPC.LoadingModelData.ID : -1L);
			}
			if (func != null)
			{
				func(objNPC);
			}
			return;
		}
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(initData.npcInfoData.Model);
		if (characterModelDataByID != null)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("TestModel/NPCRoot") as GameObject;
			if (gameObject != null)
			{
				gameObject.name = string.Concat(new object[]
				{
					"NPC",
					initData.mServerID,
					"_",
					initData.npcInfoData.ID
				});
				ObjNPC objNPC2 = gameObject.GetComponent<ObjNPC>();
				if (objNPC2 == null)
				{
					objNPC2 = gameObject.AddComponent<ObjNPC>();
				}
				objNPC2.InitInfo(characterModelDataByID);
				objNPC2.Init();
				UnityVersionUtil.SetActiveRecursive(objNPC2.gameObject, true);
				objNPC2.ResetNpc(initData);
				this.mEnableNpcList.Add(objNPC2);
				ObjNpcPool.LoadNPCModelParam loadNPCModelParam2 = new ObjNpcPool.LoadNPCModelParam();
				loadNPCModelParam2.InitData = initData;
				loadNPCModelParam2.LoadFinishedFunc = onLoadModelFinished;
				objNPC2.LoadingModelData = BundleManager.LoadModelInList(characterModelDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.OnLoadNpcModelFinished), loadNPCModelParam2, objNPC2, null);
				objNPC2.LoadingModelDataId = ((objNPC2.LoadingModelData != null) ? objNPC2.LoadingModelData.ID : -1L);
				if (func != null)
				{
					func(objNPC2);
				}
			}
		}
		else
		{
			Debug.Log("characterModelData == null " + initData.npcInfoData.Model);
		}
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x000D3004 File Offset: 0x000D1204
	public void OnLoadNpcModelFinished(object modelBundle, object param1, object param2)
	{
		ObjNpcPool.LoadNPCModelParam loadNPCModelParam = param1 as ObjNpcPool.LoadNPCModelParam;
		ObjNPC objNPC = param2 as ObjNPC;
		objNPC.LoadingModelData = null;
		objNPC.LoadingModelDataId = -1L;
		GameObject gameObject = modelBundle as GameObject;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		gameObject.gameObject.name = string.Format("MeshRoot", new object[0]);
		objNPC.MeshRoot = gameObject;
		Material meshMat = null;
		BundleManager.ResetShader(gameObject.transform, out meshMat);
		objNPC.MeshMat = meshMat;
		this.ReloadModel(objNPC.CacheTransform, gameObject);
		objNPC.LoadModelFinishInit();
		gameObject.transform.localScale = Vector3.one * loadNPCModelParam.InitData.npcInfoData.ModelScale;
		if (objNPC.enabled)
		{
			UnityVersionUtil.SetActiveRecursive(gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(gameObject, false);
		}
		if (loadNPCModelParam.LoadFinishedFunc != null)
		{
			loadNPCModelParam.LoadFinishedFunc(objNPC);
		}
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x000D30F4 File Offset: 0x000D12F4
	public void RecycleNpc(ObjNPC objNpc)
	{
		if (this.mDisableNpcList.Contains(objNpc))
		{
			return;
		}
		if (objNpc.dieDelayHandle != null)
		{
			objNpc.dieDelayHandle.Cancel();
		}
		this.mEnableNpcList.Remove(objNpc);
		if (objNpc.LoadingModelData != null)
		{
			objNpc.LoadingModelData.OnLoadFinished = null;
			objNpc.LoadingModelData = null;
			BundleManager.RemoveFromLoadModelList(objNpc.LoadingModelDataId);
			objNpc.LoadingModelDataId = -1L;
		}
		objNpc.OnRecycle();
		if (this.mDisableNpcList.Count >= this.mMaxPoolNum)
		{
			if (objNpc.MeshRoot != null)
			{
				BundleManager.UnloadModel(objNpc.CurrentCharacterModelData.ID, -1L, false);
			}
			Object.Destroy(objNpc.gameObject);
			return;
		}
		this.mDisableNpcList.Add(objNpc);
		UnityVersionUtil.SetActiveRecursive(objNpc.gameObject, false);
		if (objNpc.MeshRoot != null && objNpc.AnimationLogic.AnimaObj != null)
		{
			if (objNpc.NPCType == GameDefine.NPC_TYPE.BOSS)
			{
				if (objNpc.AnimationLogic.AnimaObj["idle_attack"] == null)
				{
					objNpc.AnimationLogic.LoadAnim(DataManager.GetActionDataByName("idle_attack"));
				}
				objNpc.AnimationLogic.AnimaObj.Play("idle_attack");
				objNpc.AnimationLogic.AnimaObj["idle_attack"].time = 0f;
				objNpc.AnimationLogic.AnimaObj.Sample();
			}
			else
			{
				if (objNpc.AnimationLogic.AnimaObj["idle"] == null)
				{
					objNpc.AnimationLogic.LoadAnim(DataManager.GetActionDataByName("idle"));
				}
				objNpc.AnimationLogic.AnimaObj.Play("idle");
				objNpc.AnimationLogic.AnimaObj["idle"].time = 0f;
				objNpc.AnimationLogic.AnimaObj.Sample();
			}
		}
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x000D32FC File Offset: 0x000D14FC
	public void ReloadModel(Transform root, GameObject obj)
	{
		obj.transform.parent = root;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x000D3338 File Offset: 0x000D1538
	public void DestroyClearPool()
	{
		for (int i = this.mDisableNpcList.Count - 1; i >= 0; i--)
		{
			if (this.mDisableNpcList[i].LoadingModelData != null)
			{
				this.mDisableNpcList[i].LoadingModelData.OnLoadFinished = null;
				this.mDisableNpcList[i].LoadingModelData = null;
				BundleManager.RemoveFromLoadModelList(this.mDisableNpcList[i].LoadingModelDataId);
				this.mDisableNpcList[i].LoadingModelDataId = -1L;
			}
			if (this.mDisableNpcList[i].MeshRoot != null)
			{
				BundleManager.UnloadModel(this.mDisableNpcList[i].CurrentCharacterModelData.ID, -1L, false);
			}
			Object.Destroy(this.mDisableNpcList[i].gameObject);
		}
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x000D341C File Offset: 0x000D161C
	public void Clear()
	{
		this.mEnableNpcList.Clear();
		this.mDisableNpcList.Clear();
	}

	// Token: 0x04002265 RID: 8805
	public float LastUseTime;

	// Token: 0x04002266 RID: 8806
	private string mID;

	// Token: 0x04002267 RID: 8807
	private List<ObjNPC> mEnableNpcList = new List<ObjNPC>();

	// Token: 0x04002268 RID: 8808
	private int mMaxPoolNum;

	// Token: 0x04002269 RID: 8809
	private List<ObjNPC> mDisableNpcList = new List<ObjNPC>();

	// Token: 0x0400226A RID: 8810
	private int CreatingNPCNum;

	// Token: 0x02000827 RID: 2087
	private class LoadNPCModelParam
	{
		// Token: 0x0400226B RID: 8811
		public ObjInitNpcData InitData;

		// Token: 0x0400226C RID: 8812
		public ObjManager.OnGetNPC LoadFinishedFunc;
	}
}
