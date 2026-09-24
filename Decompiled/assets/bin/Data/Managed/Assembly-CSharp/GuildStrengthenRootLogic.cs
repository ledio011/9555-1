using System;

// Token: 0x02000940 RID: 2368
public class GuildStrengthenRootLogic : SingletonUnity<GuildStrengthenRootLogic>
{
	// Token: 0x060041E6 RID: 16870 RVA: 0x0013AC34 File Offset: 0x00138E34
	public void Reset()
	{
		this.CurPage = -1;
		this.OnClickSkillBtn();
	}

	// Token: 0x060041E7 RID: 16871 RVA: 0x0013AC44 File Offset: 0x00138E44
	public void OnClickSkillBtn()
	{
		if (this.CurPage == 1)
		{
			return;
		}
		this.CurPage = 1;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildStarRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildSkillRootLogic, delegate
		{
			Singleton<ObjManager>.Instance.MainPlayer.ReqGuildSkill();
			WaitResponseUIRootLogic.OpenWaitBox(212, 10f, 0f, null);
		}, null);
		this.SetCurSelect();
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x0013ACA8 File Offset: 0x00138EA8
	public void OnClickStarBtn()
	{
		if (this.CurPage == 2)
		{
			return;
		}
		this.CurPage = 2;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildSkillRootLogic);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildStarRoot, delegate
		{
			SingletonUnity<GuildStarRootLogic>.Instance.EnableReset();
			WaitResponseUIRootLogic.OpenWaitBox(293, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.req_guild_star>(null, null);
		}, null);
		this.SetCurSelect();
	}

	// Token: 0x060041E9 RID: 16873 RVA: 0x0013AD0C File Offset: 0x00138F0C
	public void SetCurSelect()
	{
		if (this.CurPage == 1)
		{
			this.SkillBtn.spriteName = "CZ_huaDongBG_1";
			this.StarBtn.spriteName = "CZ_huaDongBG";
		}
		else if (this.CurPage == 2)
		{
			this.SkillBtn.spriteName = "CZ_huaDongBG";
			this.StarBtn.spriteName = "CZ_huaDongBG_1";
		}
	}

	// Token: 0x04002DD5 RID: 11733
	public UISprite SkillBtn;

	// Token: 0x04002DD6 RID: 11734
	public UISprite StarBtn;

	// Token: 0x04002DD7 RID: 11735
	private int CurPage = -1;
}
