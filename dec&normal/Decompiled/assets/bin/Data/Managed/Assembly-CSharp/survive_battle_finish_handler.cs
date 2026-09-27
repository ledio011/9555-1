using System;
using Sproto;
using SprotoType;

// Token: 0x020002C0 RID: 704
public class survive_battle_finish_handler
{
	// Token: 0x06001408 RID: 5128 RVA: 0x00081EC0 File Offset: 0x000800C0
	public static SprotoTypeBase survive_battle_finish_request(SprotoTypeBase req)
	{
		survive_battle_finish.request request = req as survive_battle_finish.request;
		if (request != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SurviveBattleResultRoot, delegate
			{
				SingletonUnity<SurviveBattleResultRootLogic>.Instance.Reset(request);
			}, null);
		}
		return null;
	}
}
