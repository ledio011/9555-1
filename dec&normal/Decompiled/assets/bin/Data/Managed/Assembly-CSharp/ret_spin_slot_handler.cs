using System;
using Sproto;
using SprotoType;

// Token: 0x020002AB RID: 683
public class ret_spin_slot_handler
{
	// Token: 0x060013DD RID: 5085 RVA: 0x00081184 File Offset: 0x0007F384
	public static SprotoTypeBase ret_spin_slot_request(SprotoTypeBase req)
	{
		ret_spin_slot.request request = req as ret_spin_slot.request;
		if (request != null && SingletonUnity<SlotUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SlotUIRootLogic>.Instance.UpdateResult(request);
		}
		return null;
	}
}
