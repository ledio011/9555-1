using System;
using UnityEngine;

// Token: 0x02000856 RID: 2134
public class GuildListLineItemLogic : MonoBehaviour
{
	// Token: 0x0600374C RID: 14156 RVA: 0x000E3B1C File Offset: 0x000E1D1C
	public void InitGuildInfo(GuildInfo info, int index)
	{
		this.curGuildInfo = info;
		this.NameLab.text = info.GuilName;
		this.GuildIcon.spriteName = GameDefine.GuildIcon[info.GuildIcon];
		this.BossNameLab.text = info.GuildChiefName;
		this.LvLabLab.text = info.GuilLevel.ToString();
		this.MemberNumLab.text = string.Format("{0}/{1}", info.CurMemberNum, info.MaxMemberNum);
		this.ComboLabel.text = info.ComboValue.ToString();
		this.DkpLabel.text = info.Exp.ToString();
		if (Singleton<ObjManager>.Instance.MainPlayer.ApplyGuildIDList.Contains(this.curGuildInfo.ServerId))
		{
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100744}", new object[0]);
			this.CanApply = false;
		}
		else
		{
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100770}", new object[0]);
			this.CanApply = true;
		}
	}

	// Token: 0x0600374D RID: 14157 RVA: 0x000E3C4C File Offset: 0x000E1E4C
	public void OnClickApplyBtn()
	{
		if (this.CanApply && Singleton<ObjManager>.Instance.MainPlayer.JoinGuild(this.curGuildInfo.ServerId))
		{
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100744}", new object[0]);
		}
	}

	// Token: 0x04002474 RID: 9332
	public GuildInfo curGuildInfo;

	// Token: 0x04002475 RID: 9333
	public UILabel NameLab;

	// Token: 0x04002476 RID: 9334
	public UILabel IdLab;

	// Token: 0x04002477 RID: 9335
	public UILabel BossNameLab;

	// Token: 0x04002478 RID: 9336
	public UILabel LvLabLab;

	// Token: 0x04002479 RID: 9337
	public UILabel MemberNumLab;

	// Token: 0x0400247A RID: 9338
	public UISprite GuildIcon;

	// Token: 0x0400247B RID: 9339
	public bool IsNeedApply;

	// Token: 0x0400247C RID: 9340
	public UILabel ApplyBtnLabel;

	// Token: 0x0400247D RID: 9341
	public UILabel DkpLabel;

	// Token: 0x0400247E RID: 9342
	public UILabel ComboLabel;

	// Token: 0x0400247F RID: 9343
	public UIButton ApplyBtn;

	// Token: 0x04002480 RID: 9344
	private bool CanApply = true;
}
