using System;
using Sproto;
using SprotoType;

// Token: 0x020002B4 RID: 692
public class sample_activity_result_handler
{
	// Token: 0x060013EF RID: 5103 RVA: 0x00081880 File Offset: 0x0007FA80
	public static SprotoTypeBase sample_activity_result_request(SprotoTypeBase req)
	{
		sample_activity_result.request request = req as sample_activity_result.request;
		if (request != null)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager != null && sceneManager.CurrentMapInofData != null && sceneManager.CurrentMapInofData.MapType == MAPTYPE.DOMIN_MAP)
			{
				if (request.win)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YouWinShowRoot, null, null);
					PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
					if (playerData.Domin_InfoDic.ContainsKey(request.id))
					{
						playerData.Domin_InfoDic[request.id].state = 1L;
					}
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YouLostShowRoot, null, null);
				}
			}
		}
		return null;
	}
}
