using System;
using Sproto;
using SprotoType;

// Token: 0x020002CD RID: 717
public class sync_skill_info_handler
{
	// Token: 0x06001422 RID: 5154 RVA: 0x00082B20 File Offset: 0x00080D20
	public static SprotoTypeBase sync_skill_info_request(SprotoTypeBase req)
	{
		sync_skill_info.request request = req as sync_skill_info.request;
		sync_skill_info.response response = new sync_skill_info.response();
		if (request != null)
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (mainPlayer != null)
			{
				mainPlayer.UpdateSkillList(request.skill_dict);
			}
			if (SingletonUnity<SkillInfoRootLogic>.Instance != null)
			{
				SingletonUnity<SkillInfoRootLogic>.Instance.SyncPage();
			}
			if (SingletonUnity<JueseJiNengQuLogic>.Exists)
			{
				SingletonUnity<JueseJiNengQuLogic>.Instance.UpdateIcon();
			}
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateSkillTips();
			}
			response.isLevelUp = request.isLevelUp;
		}
		return response;
	}
}
