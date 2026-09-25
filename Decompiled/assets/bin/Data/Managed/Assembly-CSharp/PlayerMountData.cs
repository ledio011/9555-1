using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200011F RID: 287
public class PlayerMountData
{
	// Token: 0x06000A9E RID: 2718 RVA: 0x0004F584 File Offset: 0x0004D784
	public void UpdateMountInfo(ret_mount_info.request request)
	{
		this.mCurMountInfoList = new List<mount>(request.mount_info.Values);
		this.UpdateTips();
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0004F5A4 File Offset: 0x0004D7A4
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.CheckTips(), GameDefine.TIPS_TYPE.VEHICLE);
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
	public void SetMountState(string id)
	{
		for (int i = 0; i < this.mCurMountInfoList.Count; i++)
		{
			if (this.mCurMountInfoList[i].ID.Equals(id))
			{
				this.mCurMountInfoList[i].state = 1L;
				break;
			}
		}
		this.UpdateTips();
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0004F628 File Offset: 0x0004D828
	public bool CheckTips()
	{
		ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.EXCHANGE, false, PROFESSION_TYPE.INVALID);
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			return false;
		}
		for (int i = 0; i < this.mCurMountInfoList.Count; i++)
		{
			if (this.mCurMountInfoList[i].state == 0L)
			{
				for (int j = 0; j < targetTypeItem.Count; j++)
				{
					if (targetTypeItem[j].ItemData.Function.ToString().Equals(this.mCurMountInfoList[i].ID))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x040009A8 RID: 2472
	private List<mount> mCurMountInfoList = new List<mount>();
}
