using System;
using Sproto;
using SprotoType;

// Token: 0x020002A8 RID: 680
public class ret_slot_info_handler
{
	// Token: 0x060013D7 RID: 5079 RVA: 0x00081038 File Offset: 0x0007F238
	public static SprotoTypeBase ret_slot_info_request(SprotoTypeBase req)
	{
		ret_slot_info.request request = req as ret_slot_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
			instance.PlayerData.playerSlotData.UpdateSlotData(request);
			if (SingletonUnity<SlotUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SlotUIRootLogic>.Instance.Reset(request);
			}
		}
		return null;
	}
}
