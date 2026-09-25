using System;
using UnityEngine;

// Token: 0x020009AC RID: 2476
public class MailItemLogic : MonoBehaviour
{
	// Token: 0x06004640 RID: 17984 RVA: 0x00163624 File Offset: 0x00161824
	public void InitSelct()
	{
		this.bkSprite.spriteName = "CZ_huaDongBG";
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x00163638 File Offset: 0x00161838
	public void UpdateSelect(long selectid)
	{
		if (selectid != this.curMailid)
		{
			this.bkSprite.spriteName = "CZ_huaDongBG";
		}
		else
		{
			this.bkSprite.spriteName = "CZ_huaDongBG_1";
		}
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x0016366C File Offset: 0x0016186C
	public void UpdateMail(FriendInfo.Mail mail, MailItemLogic.OnClick func)
	{
		this.onClick = func;
		this.curMail = mail;
		this.curMailid = mail.key;
		this.mailNameLabel.text = StrDictionary.GetServerDictionaryString(mail.title);
		if (this.curMail.read)
		{
			this.MailStateLabel.enabled = true;
			this.mailFlag.spriteName = "CZ_youJian_YiLingQu";
			this.mailFlag.MakePixelPerfect();
			this.colorAlphwi.alpha = 0.6f;
		}
		else
		{
			this.MailStateLabel.enabled = false;
			this.mailFlag.spriteName = "CZ_youJian_WeiLingQu";
			this.mailFlag.MakePixelPerfect();
			this.colorAlphwi.alpha = 1f;
		}
		long curServerTime = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
		if (this.curMail.expireday >= 0L)
		{
			this.TimeLabel.text = string.Format("{0}", Mathf.Max((float)(this.curMail.expireday - (long)TimeTools.GetPassDays(this.curMail.time, curServerTime)), 0f));
		}
		else
		{
			this.TimeLabel.text = string.Format("{0}", Mathf.Max(30 - TimeTools.GetPassDays(this.curMail.time, curServerTime), 0));
		}
		this.ItemFlagSprite.enabled = (this.curMail.IsHaveItem() && !this.curMail.IsGetItem());
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x001637F8 File Offset: 0x001619F8
	public void OnOnClickItemBtn()
	{
		if (this.onClick != null)
		{
			this.onClick(this.curMailid);
		}
	}

	// Token: 0x04003348 RID: 13128
	public UISprite bkSprite;

	// Token: 0x04003349 RID: 13129
	public UILabel mailNameLabel;

	// Token: 0x0400334A RID: 13130
	public UISprite ItemFlagSprite;

	// Token: 0x0400334B RID: 13131
	public UILabel TimeLabel;

	// Token: 0x0400334C RID: 13132
	public UILabel MailStateLabel;

	// Token: 0x0400334D RID: 13133
	private MailItemLogic.OnClick onClick;

	// Token: 0x0400334E RID: 13134
	private long curMailid = -1L;

	// Token: 0x0400334F RID: 13135
	private FriendInfo.Mail curMail;

	// Token: 0x04003350 RID: 13136
	public UISprite mailFlag;

	// Token: 0x04003351 RID: 13137
	public UIWidget colorAlphwi;

	// Token: 0x02000AFB RID: 2811
	// (Invoke) Token: 0x06005075 RID: 20597
	public delegate void OnClick(long mailid);
}
