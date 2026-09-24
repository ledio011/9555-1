using System;
using System.Collections.Generic;

// Token: 0x02000828 RID: 2088
public class ObjNpcPoolGroup
{
	// Token: 0x17000EAA RID: 3754
	// (get) Token: 0x06003482 RID: 13442 RVA: 0x000D3468 File Offset: 0x000D1668
	public Dictionary<string, ObjNpcPool> ObjNpcPoolDict
	{
		get
		{
			return this.mObjNpcPoolDict;
		}
	}

	// Token: 0x17000EAB RID: 3755
	// (get) Token: 0x06003483 RID: 13443 RVA: 0x000D3470 File Offset: 0x000D1670
	public List<ObjNPC> EnableNpcList
	{
		get
		{
			return this.mEnableNpcList;
		}
	}

	// Token: 0x06003484 RID: 13444 RVA: 0x000D3478 File Offset: 0x000D1678
	public void RegisterOnNpcRecycle(ObjNpcPoolGroup.OnNpcRecycle func)
	{
		this.onNpcRecycle = (ObjNpcPoolGroup.OnNpcRecycle)Delegate.Combine(this.onNpcRecycle, func);
	}

	// Token: 0x06003485 RID: 13445 RVA: 0x000D3494 File Offset: 0x000D1694
	public void DeRegisterOnNpcRecycle(ObjNpcPoolGroup.OnNpcRecycle func)
	{
		if (this.onNpcRecycle != null)
		{
			this.onNpcRecycle = (ObjNpcPoolGroup.OnNpcRecycle)Delegate.Remove(this.onNpcRecycle, func);
		}
	}

	// Token: 0x06003486 RID: 13446 RVA: 0x000D34C4 File Offset: 0x000D16C4
	public void Reset(int poolGroupMaxNum, int poolMaxNum)
	{
		this.mPoolGroupMaxNum = poolGroupMaxNum;
		this.mPoolMaxNum = poolMaxNum;
	}

	// Token: 0x06003487 RID: 13447 RVA: 0x000D34D4 File Offset: 0x000D16D4
	public void RefreshPool(int poolGroupMaxNum, int poolMaxNum)
	{
		this.mPoolGroupMaxNum = poolGroupMaxNum;
		this.mPoolMaxNum = poolMaxNum;
		List<ObjNpcPool> list = new List<ObjNpcPool>(this.ObjNpcPoolDict.Values);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].RefreshPoolMaxNum(this.mPoolMaxNum);
		}
	}

	// Token: 0x06003488 RID: 13448 RVA: 0x000D352C File Offset: 0x000D172C
	public void GetNpc(ObjInitNpcData initData, ObjManager.OnGetNPC func, ObjManager.OnGetNPC onLoadModelDone = null)
	{
		ObjNpcPool objNpcPool = null;
		this.mObjNpcPoolDict.TryGetValue(initData.npcInfoData.Model, ref objNpcPool);
		func = (ObjManager.OnGetNPC)Delegate.Combine(func, new ObjManager.OnGetNPC(this.OnGetNpc));
		if (objNpcPool != null)
		{
			objNpcPool.GetNpc(initData, func, onLoadModelDone);
		}
		else
		{
			ObjNpcPool objNpcPool2 = new ObjNpcPool();
			objNpcPool2.SetPoolMaxNum(this.mPoolMaxNum, initData.npcInfoData.Model);
			this.mObjNpcPoolDict.Add(initData.npcInfoData.Model, objNpcPool2);
			this.mObjNpcPoolList.Add(objNpcPool2);
			objNpcPool2.GetNpc(initData, func, onLoadModelDone);
		}
	}

	// Token: 0x06003489 RID: 13449 RVA: 0x000D35CC File Offset: 0x000D17CC
	public void OnGetNpc(ObjNPC npc)
	{
		if (npc != null)
		{
			this.mEnableNpcList.Add(npc);
		}
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x000D35E8 File Offset: 0x000D17E8
	public void RecycleNpc(ObjNPC objNpc)
	{
		if (this.onNpcRecycle != null)
		{
			this.onNpcRecycle(objNpc);
		}
		objNpc.IsDie = true;
		ObjNpcPool objNpcPool;
		this.mObjNpcPoolDict.TryGetValue(objNpc.ModelId, ref objNpcPool);
		this.mEnableNpcList.Remove(objNpc);
		if (objNpcPool != null)
		{
			objNpcPool.RecycleNpc(objNpc);
		}
		if (this.mObjNpcPoolDict.Count > this.mPoolGroupMaxNum)
		{
			ObjNpcPool objNpcPool2 = null;
			float num = float.MaxValue;
			for (int i = 0; i < this.mObjNpcPoolList.Count; i++)
			{
				if (this.mObjNpcPoolList[i].EnableNpcList.Count <= 0 && num > this.mObjNpcPoolList[i].LastUseTime)
				{
					num = this.mObjNpcPoolList[i].LastUseTime;
					objNpcPool2 = this.mObjNpcPoolList[i];
				}
			}
			if (objNpcPool2 != null)
			{
				objNpcPool2.DestroyClearPool();
				this.mObjNpcPoolDict.Remove(objNpcPool2.ID);
				this.mObjNpcPoolList.Remove(objNpcPool2);
			}
		}
	}

	// Token: 0x0600348B RID: 13451 RVA: 0x000D36F8 File Offset: 0x000D18F8
	public bool CheckNpcClear(ObjNPC objNpc)
	{
		for (int i = 0; i < this.mEnableNpcList.Count; i++)
		{
			if (this.mEnableNpcList[i].AttributeData.Camp != GameDefine.CAMP_TYPE.FUNCTION_NPC && !this.mEnableNpcList[i].IsMissionNpc())
			{
				if (!this.mEnableNpcList[i].IsDie)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600348C RID: 13452 RVA: 0x000D3774 File Offset: 0x000D1974
	public void ClearNPC()
	{
		List<ObjNpcPool> list = new List<ObjNpcPool>(this.mObjNpcPoolDict.Values);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].Clear();
		}
		this.mEnableNpcList.Clear();
	}

	// Token: 0x0400226D RID: 8813
	private Dictionary<string, ObjNpcPool> mObjNpcPoolDict = new Dictionary<string, ObjNpcPool>();

	// Token: 0x0400226E RID: 8814
	private List<ObjNPC> mEnableNpcList = new List<ObjNPC>();

	// Token: 0x0400226F RID: 8815
	public ObjNpcPoolGroup.OnNpcRecycle onNpcRecycle;

	// Token: 0x04002270 RID: 8816
	private List<ObjNpcPool> mObjNpcPoolList = new List<ObjNpcPool>();

	// Token: 0x04002271 RID: 8817
	private int mPoolGroupMaxNum;

	// Token: 0x04002272 RID: 8818
	private int mPoolMaxNum;

	// Token: 0x02000ADE RID: 2782
	// (Invoke) Token: 0x06005001 RID: 20481
	public delegate void OnNpcRecycle(ObjNPC npc);
}
