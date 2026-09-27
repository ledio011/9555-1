using System;
using UnityEngine;

// Token: 0x0200093F RID: 2367
public class GameMenuRootLogic : SingletonUnity<GameMenuRootLogic>
{
	// Token: 0x060041D6 RID: 16854 RVA: 0x0013A934 File Offset: 0x00138B34
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060041D7 RID: 16855 RVA: 0x0013A940 File Offset: 0x00138B40
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060041D8 RID: 16856 RVA: 0x0013A960 File Offset: 0x00138B60
	public void Reset()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		NGUITools.SetActive(this.CarBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CAR));
		NGUITools.SetActive(this.TitleBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TITLE));
		NGUITools.SetActive(this.EnhanceBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE));
		NGUITools.SetActive(this.SkillBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL));
		NGUITools.SetActive(this.CharacterBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHARACTER));
		NGUITools.SetActive(this.BagBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.BAG));
		NGUITools.SetActive(this.SocialBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SOCIAL));
		NGUITools.SetActive(this.GuildBtnIcon.gameObject, playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD));
		this.VerticalGrid.Reposition();
		this.HorizontalGrid.Reposition();
	}

	// Token: 0x060041D9 RID: 16857 RVA: 0x0013AA68 File Offset: 0x00138C68
	public void OnClickGangBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, null, null);
	}

	// Token: 0x060041DA RID: 16858 RVA: 0x0013AA7C File Offset: 0x00138C7C
	public void OnClickSkillBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GameMenuSkillInfoRootUI, null, null);
	}

	// Token: 0x060041DB RID: 16859 RVA: 0x0013AA90 File Offset: 0x00138C90
	public void OnClickCharacterBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
		{
			if (isSuccess)
			{
				Debug.Log("!!!!!!!!!!!!!!!!!!!!!!! :: " + TutorialManager.CurStep);
				this.CheckTutorialEvent();
			}
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.Reset();
		}, null);
	}

	// Token: 0x060041DC RID: 16860 RVA: 0x0013AAB0 File Offset: 0x00138CB0
	public void OnClickBagBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
		{
			if (isSuccess)
			{
				Debug.Log("!!!!!!!!!!!!!!!!!!!!!!! :: " + TutorialManager.CurStep);
				this.CheckTutorialEvent();
			}
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickItemBackPackBtn();
		}, null);
	}

	// Token: 0x060041DD RID: 16861 RVA: 0x0013AAD0 File Offset: 0x00138CD0
	public void OnClickSocialBtn()
	{
		SingletonUnity<SocialUIRootLogic>.Instance.ShowSocialUI();
		SingletonUnity<SocialUIRootLogic>.Instance.OnClickMailBtn();
	}

	// Token: 0x060041DE RID: 16862 RVA: 0x0013AAE8 File Offset: 0x00138CE8
	public void OnClickTitleBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TitleUIRootLogic, null, null);
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x0013AAFC File Offset: 0x00138CFC
	public void OnClickEnhaceBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
		{
			SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
		}, null);
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x0013AB2C File Offset: 0x00138D2C
	public void OnClickCarBtn()
	{
	}

	// Token: 0x060041E1 RID: 16865 RVA: 0x0013AB30 File Offset: 0x00138D30
	public UISprite GetBtnIconByFunctionType(FUNCTION_TYPE type)
	{
		switch (type)
		{
		case FUNCTION_TYPE.CAR:
			return this.CarBtnIcon;
		case FUNCTION_TYPE.TITLE:
			return this.TitleBtnIcon;
		case FUNCTION_TYPE.ENHANCE:
			return this.EnhanceBtnIcon;
		case FUNCTION_TYPE.SKILL:
			return this.SkillBtnIcon;
		case FUNCTION_TYPE.CHARACTER:
			return this.CharacterBtnIcon;
		case FUNCTION_TYPE.BAG:
			return this.BagBtnIcon;
		case FUNCTION_TYPE.SOCIAL:
			return this.SocialBtnIcon;
		case FUNCTION_TYPE.GUILD:
			return this.GuildBtnIcon;
		default:
			return null;
		}
	}

	// Token: 0x04002DC8 RID: 11720
	public UIGrid VerticalGrid;

	// Token: 0x04002DC9 RID: 11721
	public UIGrid HorizontalGrid;

	// Token: 0x04002DCA RID: 11722
	public UISprite CharacterBtnIcon;

	// Token: 0x04002DCB RID: 11723
	public UISprite BagBtnIcon;

	// Token: 0x04002DCC RID: 11724
	public UISprite SocialBtnIcon;

	// Token: 0x04002DCD RID: 11725
	public UISprite GuildBtnIcon;

	// Token: 0x04002DCE RID: 11726
	public UISprite UnKnowBtnIcon;

	// Token: 0x04002DCF RID: 11727
	public UISprite CarBtnIcon;

	// Token: 0x04002DD0 RID: 11728
	public UISprite TitleBtnIcon;

	// Token: 0x04002DD1 RID: 11729
	public UISprite EnhanceBtnIcon;

	// Token: 0x04002DD2 RID: 11730
	public UISprite SkillBtnIcon;

	// Token: 0x04002DD3 RID: 11731
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;
}
