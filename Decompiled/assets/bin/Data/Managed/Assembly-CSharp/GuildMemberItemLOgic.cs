using System;
using UnityEngine;

// Token: 0x0200085C RID: 2140
public class GuildMemberItemLOgic : MonoBehaviour
{
	// Token: 0x06003774 RID: 14196 RVA: 0x000E4188 File Offset: 0x000E2388
	public void InitGuildMemberInfo(GuildMember info)
	{
		this.CurMember = info;
		this.NameLab.text = info.MemberName;
		this.DuitesLab.text = StrDictionary.GetDictionaryString(GameDefine.GuildJobStr[(int)info.Job], new object[0]);
		this.LevelLab.text = string.Format("{0}", info.Level);
		this.Icon.spriteName = GameDefine.Profession_PicName[(int)info.Profession];
		this.ContributeLab.text = info.ComboValue.ToString();
		this.AllContributeLab.text = info.AllContribute.ToString();
		if (info.State == 1)
		{
			this.StateLab.text = StrDictionary.GetDictionaryString("#{100714}", new object[0]);
			this.BkSprite.spriteName = "CZ_huaDongBG";
			this.StateLab.color = Color.green;
		}
		else
		{
			this.StateLab.color = Color.white;
			this.SetStateTime();
			this.BkSprite.spriteName = "CZ_huaDongBG_2";
		}
	}

	// Token: 0x06003775 RID: 14197 RVA: 0x000E42AC File Offset: 0x000E24AC
	public void OnClickGuildItem()
	{
		if (this.CurMember.ServerId != Singleton<ObjManager>.Instance.MainPlayer.ServerId)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			TargetBasicInfo selectTargetBasicInfo = playerData.SelectTargetBasicInfo;
			selectTargetBasicInfo.ResetInfo(this.CurMember.ServerId, this.CurMember.Level, this.CurMember.ComboValue, this.CurMember.MemberName, this.CurMember.Profession, this.CurMember.State, playerData.PlayerGuild.ServerId, playerData.PlayerGuild.GuilName, UICamera.currentTouch.pos);
			HitOtherPLayerLogic.ShowMenu(HitType.HitGuildMember, selectTargetBasicInfo);
		}
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x000E435C File Offset: 0x000E255C
	private void SetStateTime()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		int num = (int)((playerCommonData.GetCurServerTime() - this.CurMember.LastLogout) / 3600L);
		if (num <= 1)
		{
			this.StateLab.text = "<1H";
		}
		else if (num <= 24)
		{
			this.StateLab.text = string.Format("{0}H", num);
		}
		else
		{
			this.StateLab.text = string.Format("{0}D", num / 24);
		}
	}

	// Token: 0x04002499 RID: 9369
	public GuildMember CurMember;

	// Token: 0x0400249A RID: 9370
	public UILabel NameLab;

	// Token: 0x0400249B RID: 9371
	public UILabel DuitesLab;

	// Token: 0x0400249C RID: 9372
	public UILabel LevelLab;

	// Token: 0x0400249D RID: 9373
	public UILabel ContributeLab;

	// Token: 0x0400249E RID: 9374
	public UILabel AllContributeLab;

	// Token: 0x0400249F RID: 9375
	public UILabel StateLab;

	// Token: 0x040024A0 RID: 9376
	public UISprite Icon;

	// Token: 0x040024A1 RID: 9377
	public UISprite BkSprite;

	// Token: 0x040024A2 RID: 9378
	public int CellHeight = 45;
}
