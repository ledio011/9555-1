using System;
using Sproto;
using SprotoType;

// Token: 0x02000226 RID: 550
public class car_copy_result_handler
{
	// Token: 0x060012C9 RID: 4809 RVA: 0x0007AE90 File Offset: 0x00079090
	public static SprotoTypeBase car_copy_result_request(SprotoTypeBase req)
	{
		car_copy_result.request request = req as car_copy_result.request;
		if (request != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarRewardPageRootLogic, delegate
			{
				SingletonUnity<CarRewardPageRootLogic>.Instance.ResetCarRewardPageRoot(request);
			}, null);
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CompleteMission();
		}
		return null;
	}
}
