using System;
using Sproto;
using SprotoType;

// Token: 0x020002B5 RID: 693
public class sample_copy_result_handler
{
	// Token: 0x060013F1 RID: 5105 RVA: 0x0008193C File Offset: 0x0007FB3C
	public static SprotoTypeBase sample_copy_result_request(SprotoTypeBase req)
	{
		sample_copy_result.request request = req as sample_copy_result.request;
		if (request != null)
		{
			CountDownTimeLogic.CloseTime();
			SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CompleteMission();
			if (request.HasWin && !request.win)
			{
				LocalDataSaveManager.SetDiedFlag(1);
			}
		}
		return null;
	}
}
