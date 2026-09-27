using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009B9 RID: 2489
public class ApplyLineLogic : MonoBehaviour
{
	// Token: 0x060046CC RID: 18124 RVA: 0x001673D8 File Offset: 0x001655D8
	public void InitApplyListItem(teammember info)
	{
		this.curMember = info;
		this.Icon.spriteName = GameDefine.Game_Player_Icon_pic[(int)info.profession];
		this.Name.text = info.name;
		this.Level.text = string.Format("Lv.{0}", info.level);
		this.ComboValue.text = string.Format("{0}", info.combValue);
	}

	// Token: 0x060046CD RID: 18125 RVA: 0x00167458 File Offset: 0x00165658
	public void OnClickAcceptBtn()
	{
		Debug.Log("OnClickAcceptBtn");
		apply_join_result.request request = new apply_join_result.request();
		request.characterId = this.curMember.id;
		request.isAgree = 1L;
		NetLogic.GetInstance().Send<Protocol.apply_join_result>(request, null);
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.RemoveApplyMember(this.curMember.id);
	}

	// Token: 0x060046CE RID: 18126 RVA: 0x001674BC File Offset: 0x001656BC
	public void OnClickRejectBtn()
	{
		apply_join_result.request request = new apply_join_result.request();
		request.characterId = this.curMember.id;
		request.isAgree = 0L;
		NetLogic.GetInstance().Send<Protocol.apply_join_result>(request, null);
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.RemoveApplyMember(this.curMember.id);
	}

	// Token: 0x040033DA RID: 13274
	public UISprite Icon;

	// Token: 0x040033DB RID: 13275
	public UILabel Name;

	// Token: 0x040033DC RID: 13276
	public UILabel Level;

	// Token: 0x040033DD RID: 13277
	public UILabel ComboValue;

	// Token: 0x040033DE RID: 13278
	public teammember curMember;
}
