using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082B RID: 2091
public class ObjOtherPlayerPool
{
	// Token: 0x060034D1 RID: 13521 RVA: 0x000D4BDC File Offset: 0x000D2DDC
	public ObjOtherPlayerPool()
	{
		this.Reset();
	}

	// Token: 0x17000EBD RID: 3773
	// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000D4BF8 File Offset: 0x000D2DF8
	public List<ObjOtherPlayer> DisableOtherPlayerList
	{
		get
		{
			return this.mDisableOtherPlayerList;
		}
	}

	// Token: 0x17000EBE RID: 3774
	// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000D4C00 File Offset: 0x000D2E00
	// (set) Token: 0x060034D4 RID: 13524 RVA: 0x000D4C08 File Offset: 0x000D2E08
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

	// Token: 0x060034D5 RID: 13525 RVA: 0x000D4C14 File Offset: 0x000D2E14
	public void Reset()
	{
		for (int i = 0; i < this.mDisableOtherPlayerList.Count; i++)
		{
			Object.Destroy(this.mDisableOtherPlayerList[i]);
		}
		this.mDisableOtherPlayerList.Clear();
	}

	// Token: 0x060034D6 RID: 13526 RVA: 0x000D4C5C File Offset: 0x000D2E5C
	public void SetPoolMaxNum(int maxNum)
	{
		this.mMaxPoolNum = maxNum;
	}

	// Token: 0x060034D7 RID: 13527 RVA: 0x000D4C68 File Offset: 0x000D2E68
	public ObjOtherPlayer GetOtherPlayer(ObjInitPlayerData initData)
	{
		if (this.CheckInPool(initData.mServerID))
		{
			return this.GetInPool(initData.mServerID);
		}
		return null;
	}

	// Token: 0x060034D8 RID: 13528 RVA: 0x000D4C98 File Offset: 0x000D2E98
	public void RecycleOtherPlayer(ObjOtherPlayer objOtherPlayer)
	{
		UnityVersionUtil.SetActiveRecursive(objOtherPlayer.gameObject, false);
		if (this.mDisableOtherPlayerList.Count < this.MaxPoolNum)
		{
			this.mDisableOtherPlayerList.Add(objOtherPlayer);
		}
		else if (this.mDisableOtherPlayerList.Count > 0)
		{
			ObjOtherPlayer objOtherPlayer2 = this.mDisableOtherPlayerList[0];
			this.mDisableOtherPlayerList.RemoveAt(0);
			this.mDisableOtherPlayerList.Add(objOtherPlayer);
			objOtherPlayer2.RecycleUnloadModelBundle();
			Object.Destroy(objOtherPlayer2.gameObject);
		}
		else
		{
			objOtherPlayer.RecycleUnloadModelBundle();
			Object.Destroy(objOtherPlayer.gameObject);
		}
	}

	// Token: 0x060034D9 RID: 13529 RVA: 0x000D4D38 File Offset: 0x000D2F38
	private bool CheckInPool(long serverId)
	{
		for (int i = 0; i < this.mDisableOtherPlayerList.Count; i++)
		{
			if (this.mDisableOtherPlayerList[i].ServerId == serverId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060034DA RID: 13530 RVA: 0x000D4D7C File Offset: 0x000D2F7C
	private ObjOtherPlayer GetInPool(long serverId)
	{
		for (int i = 0; i < this.mDisableOtherPlayerList.Count; i++)
		{
			if (this.mDisableOtherPlayerList[i].ServerId == serverId)
			{
				ObjOtherPlayer result = this.mDisableOtherPlayerList[i];
				this.mDisableOtherPlayerList.RemoveAt(i);
				return result;
			}
		}
		return null;
	}

	// Token: 0x060034DB RID: 13531 RVA: 0x000D4DD8 File Offset: 0x000D2FD8
	public void Clear()
	{
		this.mDisableOtherPlayerList.Clear();
	}

	// Token: 0x060034DC RID: 13532 RVA: 0x000D4DE8 File Offset: 0x000D2FE8
	~ObjOtherPlayerPool()
	{
	}

	// Token: 0x0400228D RID: 8845
	private List<ObjOtherPlayer> mDisableOtherPlayerList = new List<ObjOtherPlayer>();

	// Token: 0x0400228E RID: 8846
	private int mMaxPoolNum;
}
