using System;
using UnityEngine;

// Token: 0x020009BC RID: 2492
public class SmallPlayerInfoItem : MonoBehaviour
{
	// Token: 0x060046F7 RID: 18167 RVA: 0x00168E3C File Offset: 0x0016703C
	public void Reset(PROFESSION_TYPE profession, string name, int comboVal, int level, string btnName, string disableBtnName, bool isBtnEnable, long key, long guildId, string guildName, DelegateDefine.OneLongParamDelegate clickFunc = null)
	{
		this.Reset((int)profession, name, comboVal, level, btnName, disableBtnName, isBtnEnable, key, guildId, guildName, clickFunc);
	}

	// Token: 0x060046F8 RID: 18168 RVA: 0x00168E64 File Offset: 0x00167064
	public void Reset(int profession, string name, int comboVal, int level, string btnName, string disableBtnName, bool isBtnEnable, long key, long guildId, string guildName, DelegateDefine.OneLongParamDelegate clickFunc = null)
	{
		this.PlayerIcon.spriteName = GameDefine.Player_Icon_Small_Pic[profession];
		this.NameLabel.text = name;
		this.LevelLabel.text = string.Format("Lv.{0}", level);
		this.ComboValLabel.text = string.Format("{0}", comboVal);
		this.mKey = key;
		this.mProfession = (PROFESSION_TYPE)profession;
		this.mName = name;
		this.mComboVal = comboVal;
		this.mLevel = level;
		this.mGuildId = guildId;
		this.mGuildName = guildName;
		this.BtnCollider.enabled = isBtnEnable;
		if (!isBtnEnable)
		{
			this.BtnColor.isEnabled = false;
			this.BtnColor.SetState(UIButtonColor.State.Disabled, true);
			this.BtnLabel.text = disableBtnName;
		}
		else
		{
			this.BtnLabel.text = btnName;
			this.BtnColor.isEnabled = true;
			this.BtnColor.SetState(UIButtonColor.State.Normal, true);
		}
		this.mDisableBtnName = disableBtnName;
		this.onClickBtn = clickFunc;
	}

	// Token: 0x060046F9 RID: 18169 RVA: 0x00168F70 File Offset: 0x00167170
	public void OnClickBtn()
	{
		if (this.onClickBtn != null)
		{
			this.onClickBtn(this.mKey);
		}
		this.BtnCollider.enabled = false;
		this.BtnColor.isEnabled = false;
		this.BtnColor.SetState(UIButtonColor.State.Disabled, true);
		this.BtnLabel.text = this.mDisableBtnName;
	}

	// Token: 0x060046FA RID: 18170 RVA: 0x00168FD0 File Offset: 0x001671D0
	public void OnClickIcon()
	{
		TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
		selectTargetBasicInfo.ResetInfo(this.mKey, this.mLevel, this.mComboVal, this.mName, this.mProfession, 1, this.mGuildId, this.mGuildName, UICamera.currentTouch.pos);
		HitOtherPLayerLogic.ShowMenu(HitType.HitOtherPlayer, selectTargetBasicInfo);
	}

	// Token: 0x04003406 RID: 13318
	public UISprite PlayerIcon;

	// Token: 0x04003407 RID: 13319
	public UILabel NameLabel;

	// Token: 0x04003408 RID: 13320
	public UILabel LevelLabel;

	// Token: 0x04003409 RID: 13321
	public UILabel ComboValLabel;

	// Token: 0x0400340A RID: 13322
	public UILabel BtnLabel;

	// Token: 0x0400340B RID: 13323
	public UIButtonColor BtnColor;

	// Token: 0x0400340C RID: 13324
	public BoxCollider BtnCollider;

	// Token: 0x0400340D RID: 13325
	private long mKey;

	// Token: 0x0400340E RID: 13326
	private DelegateDefine.OneLongParamDelegate onClickBtn;

	// Token: 0x0400340F RID: 13327
	private string mDisableBtnName;

	// Token: 0x04003410 RID: 13328
	private PROFESSION_TYPE mProfession;

	// Token: 0x04003411 RID: 13329
	private string mName;

	// Token: 0x04003412 RID: 13330
	private int mComboVal;

	// Token: 0x04003413 RID: 13331
	private int mLevel;

	// Token: 0x04003414 RID: 13332
	private long mGuildId;

	// Token: 0x04003415 RID: 13333
	private string mGuildName;
}
