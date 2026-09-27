using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x0200084B RID: 2123
public class CreateGuildRootLogic : SingletonUnity<CreateGuildRootLogic>
{
	// Token: 0x060036A7 RID: 13991 RVA: 0x000E10B4 File Offset: 0x000DF2B4
	private void Init()
	{
		this.DiamondLabel.text = string.Format("x{0}", 49);
		this.CashLabel.text = string.Format("x{0}", 499);
	}

	// Token: 0x060036A8 RID: 13992 RVA: 0x000E10F4 File Offset: 0x000DF2F4
	private void Start()
	{
		this.CenterOnChild.RegisterCenterOnEvent(new UICenterOnChild.CenterOnChildDelegate(this.OnCenterOnIcon));
	}

	// Token: 0x060036A9 RID: 13993 RVA: 0x000E1110 File Offset: 0x000DF310
	private void OnEnable()
	{
		this.CenterOnChild.CenterOn(this.IconList[this.mCurIconIndex]);
		this.Init();
	}

	// Token: 0x060036AA RID: 13994 RVA: 0x000E1140 File Offset: 0x000DF340
	private void OnCenterOnIcon(Transform target)
	{
		for (int i = 0; i < this.IconList.Count; i++)
		{
			if (this.IconList[i] == target)
			{
				this.mCurIconIndex = i;
				return;
			}
		}
	}

	// Token: 0x060036AB RID: 13995 RVA: 0x000E1188 File Offset: 0x000DF388
	private bool NoticeStrCheck(string str)
	{
		return string.IsNullOrEmpty(str) || str.Length <= this.MaxNoticeCount;
	}

	// Token: 0x060036AC RID: 13996 RVA: 0x000E11AC File Offset: 0x000DF3AC
	public void OnClickCreateBtnRight()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_COST)
		{
			TutorialManager.MoveNext(false);
			return;
		}
		string text = this.InputGangName.value;
		string text2 = this.InputNotice.value;
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Trim();
		}
		if (string.IsNullOrEmpty(text2))
		{
			text2 = this.InputNotice.defaultText;
		}
		if (string.IsNullOrEmpty(text))
		{
			MessageBoxLogic.OpenOKBox("#{200108}", "#{100127}", null);
			return;
		}
		if (!this.StrIsTrue(text))
		{
			MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{200109}", new object[]
			{
				this.MinNameCharNum,
				this.MaxNameCharNum
			}), "#{100127}", null);
			return;
		}
		if (!this.NoticeStrCheck(text2))
		{
			MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{200120}", new object[]
			{
				this.MaxNoticeCount
			}), "#{100127}", null);
			return;
		}
		Singleton<ObjManager>.Instance.MainPlayer.CreatGuild(text, text2, (long)this.mCurIconIndex, GameDefine.MONEY_TYPE.GOLD);
	}

	// Token: 0x060036AD RID: 13997 RVA: 0x000E12C0 File Offset: 0x000DF4C0
	public void OnClickCreatBtnLeft()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.GUILD_COST)
		{
			TutorialManager.MoveNext(false);
			return;
		}
		string text = this.InputGangName.value;
		string text2 = this.InputNotice.value;
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Trim();
		}
		if (string.IsNullOrEmpty(text2))
		{
			text2 = this.InputNotice.defaultText;
		}
		if (string.IsNullOrEmpty(text))
		{
			MessageBoxLogic.OpenOKBox("#{200108}", "#{100127}", null);
			return;
		}
		if (!this.StrIsTrue(text))
		{
			MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{200109}", new object[]
			{
				this.MinNameCharNum,
				this.MaxNameCharNum
			}), "#{100127}", null);
			return;
		}
		if (!this.NoticeStrCheck(text2))
		{
			MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{200120}", new object[]
			{
				this.MaxNoticeCount
			}), "#{100127}", null);
			return;
		}
		Singleton<ObjManager>.Instance.MainPlayer.CreatGuild(text, text2, (long)this.mCurIconIndex, GameDefine.MONEY_TYPE.DIAMOND);
	}

	// Token: 0x060036AE RID: 13998 RVA: 0x000E13D4 File Offset: 0x000DF5D4
	private bool StrIsTrue(string str)
	{
		string text = "^[a-zA-Z0-9]{1}([a-zA-Z0-9]|[ _]){4,14}$";
		return Regex.IsMatch(str, text);
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x000E13F8 File Offset: 0x000DF5F8
	public void OnClickLeftBtn()
	{
		this.mCurIconIndex = (this.mCurIconIndex + this.IconList.Count - 1) % this.IconList.Count;
		this.CenterOnChild.CenterOn(this.IconList[this.mCurIconIndex]);
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x000E1448 File Offset: 0x000DF648
	public void OnClickRightBtn()
	{
		this.mCurIconIndex = (this.mCurIconIndex + 1) % this.IconList.Count;
		this.CenterOnChild.CenterOn(this.IconList[this.mCurIconIndex]);
	}

	// Token: 0x04002405 RID: 9221
	public List<Transform> IconList;

	// Token: 0x04002406 RID: 9222
	public UICenterOnChild CenterOnChild;

	// Token: 0x04002407 RID: 9223
	private int mCurIconIndex;

	// Token: 0x04002408 RID: 9224
	public UIInput InputGangName;

	// Token: 0x04002409 RID: 9225
	public UIInput InputNotice;

	// Token: 0x0400240A RID: 9226
	private int MaxNoticeCount = 150;

	// Token: 0x0400240B RID: 9227
	public UILabel DiamondLabel;

	// Token: 0x0400240C RID: 9228
	public UILabel CashLabel;

	// Token: 0x0400240D RID: 9229
	public UISprite DiamondCreateBtn;

	// Token: 0x0400240E RID: 9230
	public UISprite CashCreateBtn;

	// Token: 0x0400240F RID: 9231
	private int MinNameCharNum = 5;

	// Token: 0x04002410 RID: 9232
	private int MaxNameCharNum = 15;
}
