using System;
using Sproto;
using SprotoType;

// Token: 0x02000248 RID: 584
public class notice_relife_player_handler
{
	// Token: 0x06001315 RID: 4885 RVA: 0x0007C54C File Offset: 0x0007A74C
	public static SprotoTypeBase notice_relife_player_request(SprotoTypeBase req)
	{
		notice_relife_player.request request = req as notice_relife_player.request;
		if (request != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RebirthUIRoot, delegate
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopDiamondBuyRoot);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopShopRoot);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
				SingletonUnity<RebirthUIRootLogic>.Instance.Reset(request);
			}, null);
		}
		return null;
	}
}
