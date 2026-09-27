using System;
using UnityEngine;

// Token: 0x0200084D RID: 2125
public class GuildApplyListItemLogic : MonoBehaviour
{
	// Token: 0x06003701 RID: 14081 RVA: 0x000E1FF0 File Offset: 0x000E01F0
	public void InitApplyListItem(GuildMember info)
	{
		this.curMember = info;
		this.Icon.spriteName = GameDefine.Player_Icon_Small_Pic[(int)info.Profession];
		this.Name.text = info.MemberName;
		this.Level.text = string.Format("Lv.{0}", info.Level);
		this.ComboValue.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), info.ComboValue);
	}

	// Token: 0x06003702 RID: 14082 RVA: 0x000E207C File Offset: 0x000E027C
	public void Agree()
	{
		if (this.curMember != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.AgreeJoinGuild(this.curMember.ServerId);
		}
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x000E20A4 File Offset: 0x000E02A4
	public void DisAgree()
	{
		if (this.curMember != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.DisAgreeJoinGuild(this.curMember.ServerId);
		}
	}

	// Token: 0x04002428 RID: 9256
	public UISprite Icon;

	// Token: 0x04002429 RID: 9257
	public UILabel Name;

	// Token: 0x0400242A RID: 9258
	public UILabel Level;

	// Token: 0x0400242B RID: 9259
	public UILabel ComboValue;

	// Token: 0x0400242C RID: 9260
	public GuildMember curMember;
}
