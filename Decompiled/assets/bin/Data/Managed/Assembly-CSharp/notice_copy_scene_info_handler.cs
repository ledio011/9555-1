using System;
using Sproto;
using SprotoType;

// Token: 0x02000244 RID: 580
public class notice_copy_scene_info_handler
{
	// Token: 0x0600130D RID: 4877 RVA: 0x0007C3E8 File Offset: 0x0007A5E8
	public static SprotoTypeBase notice_copy_scene_info_request(SprotoTypeBase req)
	{
		notice_copy_scene_info.request request = req as notice_copy_scene_info.request;
		if (request != null)
		{
			if (!(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager is EXPSceneManager))
			{
				return null;
			}
			if (request.HasId)
			{
				if (SingletonUnity<ExpBattleInfoRootLogic>.Exists)
				{
					SingletonUnity<ExpBattleInfoRootLogic>.Instance.UpdateInfo(request);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExpBattleInfoRoot, delegate
					{
						SingletonUnity<ExpBattleInfoRootLogic>.Instance.EnableReset();
						SingletonUnity<ExpBattleInfoRootLogic>.Instance.UpdateInfo(request);
					}, null);
				}
			}
		}
		return null;
	}
}
