using System;
using SprotoType;

// Token: 0x02000891 RID: 2193
public class PlayerSlotData
{
	// Token: 0x06003B54 RID: 15188 RVA: 0x00102E30 File Offset: 0x00101030
	public void UpdateSlotData(ret_slot_info.request request)
	{
		this.mFreeTimes = (int)request.slot_info.curNum;
		this.mSumTimes = (int)request.slot_info.sumNum;
		if (request.HasSlot_items)
		{
			this.mNotGetItemsNum = request.slot_items.Count;
		}
		else
		{
			this.mNotGetItemsNum = 0;
		}
		this.UpdateTips();
	}

	// Token: 0x06003B55 RID: 15189 RVA: 0x00102E90 File Offset: 0x00101090
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.CheckTips(), GameDefine.TIPS_TYPE.SLOT);
		}
	}

	// Token: 0x06003B56 RID: 15190 RVA: 0x00102EB0 File Offset: 0x001010B0
	public void SetInfo(int freetimes, int sum, int notget)
	{
		this.mFreeTimes = freetimes;
		this.mSumTimes = sum;
		this.mNotGetItemsNum = notget;
		this.UpdateTips();
	}

	// Token: 0x06003B57 RID: 15191 RVA: 0x00102ED0 File Offset: 0x001010D0
	public bool CheckTips()
	{
		return this.mFreeTimes > 0 || this.mNotGetItemsNum > 0;
	}

	// Token: 0x040026D4 RID: 9940
	private int mFreeTimes;

	// Token: 0x040026D5 RID: 9941
	private int mSumTimes;

	// Token: 0x040026D6 RID: 9942
	private int mNotGetItemsNum;
}
