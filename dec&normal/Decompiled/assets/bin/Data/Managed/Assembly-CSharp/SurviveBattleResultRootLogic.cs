using System;
using SprotoType;

// Token: 0x020009B8 RID: 2488
public class SurviveBattleResultRootLogic : SingletonUnity<SurviveBattleResultRootLogic>
{
	// Token: 0x060046C7 RID: 18119 RVA: 0x00167340 File Offset: 0x00165540
	public void Reset(survive_battle_finish.request request)
	{
		this.ResultLabel.text = StrDictionary.GetServerDictionaryString(request.info);
	}

	// Token: 0x060046C8 RID: 18120 RVA: 0x00167358 File Offset: 0x00165558
	public void OnClickOkBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x060046C9 RID: 18121 RVA: 0x00167368 File Offset: 0x00165568
	private void OnEnable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
		}
	}

	// Token: 0x060046CA RID: 18122 RVA: 0x0016739C File Offset: 0x0016559C
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
	}

	// Token: 0x040033D9 RID: 13273
	public UILabel ResultLabel;
}
