using System;
using SprotoType;

// Token: 0x02000851 RID: 2129
[Serializable]
public class GuildInfo
{
	// Token: 0x06003713 RID: 14099 RVA: 0x000E2AC0 File Offset: 0x000E0CC0
	public GuildInfo()
	{
		this.ResetGuildInfo();
	}

	// Token: 0x06003714 RID: 14100 RVA: 0x000E2AD8 File Offset: 0x000E0CD8
	public GuildInfo(guild_info info)
	{
		this.mServerId = ((!info.HasGuildId) ? -1L : info.guildId);
		this.mGuildName = ((!info.HasGuildName) ? string.Empty : info.guildName);
		this.mGuildLevel = ((!info.HasGuildLevel) ? -1 : ((int)info.guildLevel));
		this.mGuildChiefName = ((!info.HasGuildChiefName) ? string.Empty : info.guildChiefName);
		this.CurApplyNum = ((!info.HasGuildApplyNum) ? -1 : ((int)info.guildApplyNum));
		this.MaxApplyNum = ((!info.HasGuildApplyMaxNum) ? -1 : ((int)info.guildApplyMaxNum));
		this.mCurMemberNum = ((!info.HasGuildMemberNum) ? -1 : ((int)info.guildMemberNum - this.CurApplyNum));
		this.mMaxMemberNum = (int)info.guildMaxPlayer;
		this.mNotice = ((!info.HasNotice) ? string.Empty : info.notice);
		this.mIsNeedAppro = info.isNeedAppro;
		this.mComboValue = ((!info.HasGuildCombo) ? -1 : ((int)info.guildCombo));
		this.mExp = ((!info.HasGuildExp) ? -1 : ((int)info.guildExp));
		this.mGuildIcon = ((!info.HasIcon) ? 0 : ((int)info.icon));
		this.mDisactiveState = ((!info.HasDisactiveState) ? 0 : ((int)info.disactiveState));
	}

	// Token: 0x17000F1C RID: 3868
	// (get) Token: 0x06003715 RID: 14101 RVA: 0x000E2C84 File Offset: 0x000E0E84
	// (set) Token: 0x06003716 RID: 14102 RVA: 0x000E2C8C File Offset: 0x000E0E8C
	public long ServerId
	{
		get
		{
			return this.mServerId;
		}
		set
		{
			this.mServerId = value;
		}
	}

	// Token: 0x17000F1D RID: 3869
	// (get) Token: 0x06003717 RID: 14103 RVA: 0x000E2C98 File Offset: 0x000E0E98
	// (set) Token: 0x06003718 RID: 14104 RVA: 0x000E2CA0 File Offset: 0x000E0EA0
	public string GuilName
	{
		get
		{
			return this.mGuildName;
		}
		set
		{
			this.mGuildName = value;
		}
	}

	// Token: 0x17000F1E RID: 3870
	// (get) Token: 0x06003719 RID: 14105 RVA: 0x000E2CAC File Offset: 0x000E0EAC
	// (set) Token: 0x0600371A RID: 14106 RVA: 0x000E2CB4 File Offset: 0x000E0EB4
	public int GuilLevel
	{
		get
		{
			return this.mGuildLevel;
		}
		set
		{
			this.mGuildLevel = value;
		}
	}

	// Token: 0x17000F1F RID: 3871
	// (get) Token: 0x0600371B RID: 14107 RVA: 0x000E2CC0 File Offset: 0x000E0EC0
	// (set) Token: 0x0600371C RID: 14108 RVA: 0x000E2CC8 File Offset: 0x000E0EC8
	public string GuildChiefName
	{
		get
		{
			return this.mGuildChiefName;
		}
		set
		{
			this.mGuildChiefName = value;
		}
	}

	// Token: 0x17000F20 RID: 3872
	// (get) Token: 0x0600371D RID: 14109 RVA: 0x000E2CD4 File Offset: 0x000E0ED4
	// (set) Token: 0x0600371E RID: 14110 RVA: 0x000E2CDC File Offset: 0x000E0EDC
	public int CurMemberNum
	{
		get
		{
			return this.mCurMemberNum;
		}
		set
		{
			this.mCurMemberNum = value;
		}
	}

	// Token: 0x17000F21 RID: 3873
	// (get) Token: 0x0600371F RID: 14111 RVA: 0x000E2CE8 File Offset: 0x000E0EE8
	// (set) Token: 0x06003720 RID: 14112 RVA: 0x000E2CF0 File Offset: 0x000E0EF0
	public int MaxMemberNum
	{
		get
		{
			return this.mMaxMemberNum;
		}
		set
		{
			this.mMaxMemberNum = value;
		}
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x000E2CFC File Offset: 0x000E0EFC
	public bool IsCanApply()
	{
		return this.CurApplyNum < this.MaxApplyNum;
	}

	// Token: 0x17000F22 RID: 3874
	// (get) Token: 0x06003722 RID: 14114 RVA: 0x000E2D0C File Offset: 0x000E0F0C
	// (set) Token: 0x06003723 RID: 14115 RVA: 0x000E2D14 File Offset: 0x000E0F14
	public string Notice
	{
		get
		{
			return this.mNotice;
		}
		set
		{
			this.mNotice = value;
		}
	}

	// Token: 0x17000F23 RID: 3875
	// (get) Token: 0x06003724 RID: 14116 RVA: 0x000E2D20 File Offset: 0x000E0F20
	// (set) Token: 0x06003725 RID: 14117 RVA: 0x000E2D28 File Offset: 0x000E0F28
	public bool IsNeedAppro
	{
		get
		{
			return this.mIsNeedAppro;
		}
		set
		{
			this.mIsNeedAppro = value;
		}
	}

	// Token: 0x17000F24 RID: 3876
	// (get) Token: 0x06003726 RID: 14118 RVA: 0x000E2D34 File Offset: 0x000E0F34
	// (set) Token: 0x06003727 RID: 14119 RVA: 0x000E2D3C File Offset: 0x000E0F3C
	public int DisactiveState
	{
		get
		{
			return this.mDisactiveState;
		}
		set
		{
			this.mDisactiveState = value;
		}
	}

	// Token: 0x17000F25 RID: 3877
	// (get) Token: 0x06003728 RID: 14120 RVA: 0x000E2D48 File Offset: 0x000E0F48
	// (set) Token: 0x06003729 RID: 14121 RVA: 0x000E2D50 File Offset: 0x000E0F50
	public int GuildIcon
	{
		get
		{
			return this.mGuildIcon;
		}
		set
		{
			this.mGuildIcon = value;
		}
	}

	// Token: 0x17000F26 RID: 3878
	// (get) Token: 0x0600372A RID: 14122 RVA: 0x000E2D5C File Offset: 0x000E0F5C
	// (set) Token: 0x0600372B RID: 14123 RVA: 0x000E2D64 File Offset: 0x000E0F64
	public int ComboValue
	{
		get
		{
			return this.mComboValue;
		}
		set
		{
			this.mComboValue = value;
		}
	}

	// Token: 0x17000F27 RID: 3879
	// (get) Token: 0x0600372C RID: 14124 RVA: 0x000E2D70 File Offset: 0x000E0F70
	// (set) Token: 0x0600372D RID: 14125 RVA: 0x000E2D78 File Offset: 0x000E0F78
	public int Exp
	{
		get
		{
			return this.mExp;
		}
		set
		{
			this.mExp = value;
		}
	}

	// Token: 0x0600372E RID: 14126 RVA: 0x000E2D84 File Offset: 0x000E0F84
	public void ResetGuildInfo()
	{
		this.mServerId = -1L;
		this.mGuildName = string.Empty;
		this.mGuildLevel = -1;
		this.mGuildChiefName = string.Empty;
		this.mCurMemberNum = -1;
		this.mMaxMemberNum = -1;
		this.CurApplyNum = -1;
		this.MaxApplyNum = -1;
		this.mGuildIcon = -1;
		this.mExp = 0;
		this.mComboValue = 0;
		this.mIsNeedAppro = false;
	}

	// Token: 0x0400243E RID: 9278
	private long mServerId = -1L;

	// Token: 0x0400243F RID: 9279
	private string mGuildName;

	// Token: 0x04002440 RID: 9280
	private int mGuildLevel;

	// Token: 0x04002441 RID: 9281
	private string mGuildChiefName;

	// Token: 0x04002442 RID: 9282
	private int mCurMemberNum;

	// Token: 0x04002443 RID: 9283
	private int mMaxMemberNum;

	// Token: 0x04002444 RID: 9284
	private int CurApplyNum;

	// Token: 0x04002445 RID: 9285
	private int MaxApplyNum;

	// Token: 0x04002446 RID: 9286
	private string mNotice;

	// Token: 0x04002447 RID: 9287
	private bool mIsNeedAppro;

	// Token: 0x04002448 RID: 9288
	private int mDisactiveState;

	// Token: 0x04002449 RID: 9289
	private int mGuildIcon;

	// Token: 0x0400244A RID: 9290
	private int mComboValue;

	// Token: 0x0400244B RID: 9291
	private int mExp;
}
