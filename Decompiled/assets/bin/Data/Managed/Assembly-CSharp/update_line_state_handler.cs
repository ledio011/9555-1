using System;
using Sproto;
using SprotoType;

// Token: 0x020002D3 RID: 723
public class update_line_state_handler
{
	// Token: 0x0600142E RID: 5166 RVA: 0x000831B4 File Offset: 0x000813B4
	public static SprotoTypeBase update_line_state_request(SprotoTypeBase req)
	{
		update_line_state.request request = req as update_line_state.request;
		if (request != null && SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr == request.mapInfoId)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.LineCount = (int)request.line_count;
			if (request.HasLine_states)
			{
				playerData.LineStates = request.line_states;
			}
			if (SingletonUnity<MapLineInfoLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MapLineInfoLogic>.Instance.gameObject))
			{
				SingletonUnity<MapLineInfoLogic>.Instance.UpdateItems();
			}
		}
		return null;
	}
}
