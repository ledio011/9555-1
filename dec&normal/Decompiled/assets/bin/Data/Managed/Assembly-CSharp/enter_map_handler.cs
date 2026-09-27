using System;
using Sproto;
using SprotoType;

// Token: 0x0200022E RID: 558
public class enter_map_handler
{
	// Token: 0x060012DD RID: 4829 RVA: 0x0007B3C4 File Offset: 0x000795C4
	public static SprotoTypeBase enter_map_request(SprotoTypeBase req)
	{
		enter_map.request request = req as enter_map.request;
		if (request != null)
		{
			NetLogic.GetInstance().CanProcessPack = false;
			int sceneDefine = int.Parse(request.mapInfoId);
			if (request.HasLine_count)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.LineCount = (int)request.line_count;
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex = (int)request.line_index;
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.LineCount = 1;
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex = 1;
			}
			LoadingWindow.LoadScene(sceneDefine);
		}
		return null;
	}
}
