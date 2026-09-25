using System;
using Sproto;
using SprotoType;

// Token: 0x020002AC RID: 684
public class ret_title_req_level_up_handler
{
	// Token: 0x060013DF RID: 5087 RVA: 0x000811D0 File Offset: 0x0007F3D0
	public static SprotoTypeBase ret_title_req_level_up_request(SprotoTypeBase req)
	{
		ret_title_req_level_up.request request = req as ret_title_req_level_up.request;
		if (request != null && request.HasTitle_exp && request.HasTitle_level && Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.AttributeData.CurTitleExp = (int)request.title_exp;
			Singleton<ObjManager>.Instance.MainPlayer.AttributeData.CurTitleLevel = (int)request.title_level;
			if (SingletonUnity<JSShengWangLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<JSShengWangLogic>.Instance.gameObject))
			{
				WaitResponseUIRootLogic.CloseBox();
				SingletonUnity<JSShengWangLogic>.Instance.Reset();
			}
			PlayerHeadInfoLogic playerHeadInfoLogic = Singleton<ObjManager>.Instance.MainPlayer.HeadInfoLogic as PlayerHeadInfoLogic;
			playerHeadInfoLogic.ChangePic(Singleton<ObjManager>.Instance.MainPlayer.AttributeData.CurTitleLevel);
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateTitleTips();
			}
			if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
			}
			NoticeLogic.AddNotifyData("#{101719}", true, false);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Achieve", "Title", string.Format("title_{0}", request.title_level));
		}
		return null;
	}
}
