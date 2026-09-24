using System;
using Sproto;
using SprotoType;

// Token: 0x020002B1 RID: 689
public class ret_use_item_handler
{
	// Token: 0x060013E9 RID: 5097 RVA: 0x000815AC File Offset: 0x0007F7AC
	public static SprotoTypeBase ret_use_item_request(SprotoTypeBase req)
	{
		ret_use_item.request request = req as ret_use_item.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			long success = request.success;
			if (success == 1L)
			{
				ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.ITEM_BACKPACK);
				if (itemContainer != null)
				{
					GameItem item = itemContainer.GetItemByIndexId(request.indexId);
					item.StackNum--;
					if (item.StackNum < 1)
					{
						itemContainer.RemoveItem(item);
					}
					if (UIUpdateEvent.UpdateBackPackEvent != null)
					{
						UIUpdateEvent.UpdateBackPackEvent();
					}
					if (item.ItemData.Type == GameDefine.ITEM_TYPE.POTION)
					{
						if (SingletonUnity<PotionLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PotionLogic>.Instance.gameObject))
						{
							SingletonUnity<PotionLogic>.Instance.Reset();
						}
					}
					else if (item.ItemData.Type == GameDefine.ITEM_TYPE.EXCHANGE)
					{
						if (SingletonUnity<PlayerCarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject))
						{
							string text = item.ItemData.Function.ToString();
							GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
							instance.PlayerData.playerMountData.SetMountState(text);
							SingletonUnity<PlayerCarRootLogic>.Instance.CurMountInfoDic[text].state = 1L;
							SingletonUnity<PlayerCarRootLogic>.Instance.UpdateCarPage(item.ItemData.Function.ToString());
						}
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GetCarUIRoot, delegate
						{
							SingletonUnity<GetCarUIRootLogic>.Instance.Reset(item.ItemData.Function.ToString(), "#{101425}");
							if (SingletonUnity<PlayerCarRootLogic>.Exists && TutorialManager.CurStep == TUTORIAL_STEP.CAR_CLICK_GET)
							{
								SingletonUnity<PlayerCarRootLogic>.Instance.CheckTutorialEvent();
							}
						}, null);
					}
				}
			}
			else if (UIManager.IsUnlockTutorialEnable())
			{
				TutorialManager.CloseTutorial();
			}
		}
		return null;
	}
}
