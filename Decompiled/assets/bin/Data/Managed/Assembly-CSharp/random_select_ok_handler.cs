using System;
using Sproto;
using SprotoType;

// Token: 0x0200024D RID: 589
public class random_select_ok_handler
{
	// Token: 0x0600131F RID: 4895 RVA: 0x0007C804 File Offset: 0x0007AA04
	public static SprotoTypeBase random_select_ok_request(SprotoTypeBase req)
	{
		random_select_ok.request request = req as random_select_ok.request;
		if (request != null && (!SingletonUnity<TeamUIRootNewLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TeamUIRootNewLogic>.Instance.gameObject)) && !UIManager.IsUnlockTutorialEnable() && SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
		}
		return null;
	}
}
