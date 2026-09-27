using System;
using System.Collections.Generic;

// Token: 0x02000132 RID: 306
public class PlayerChatHistoryInfo
{
	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00053F70 File Offset: 0x00052170
	// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00053F78 File Offset: 0x00052178
	public GameDefine.CHAT_CHANNEL_TYPE ChannelType
	{
		get
		{
			return this.mChannelType;
		}
		set
		{
			this.mChannelType = value;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00053F84 File Offset: 0x00052184
	// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00053F8C File Offset: 0x0005218C
	public long SenderServerId
	{
		get
		{
			return this.mSenderServerId;
		}
		set
		{
			this.mSenderServerId = value;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00053F98 File Offset: 0x00052198
	// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00053FA0 File Offset: 0x000521A0
	public string SenderName
	{
		get
		{
			return this.mSenderName;
		}
		set
		{
			this.mSenderName = value;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00053FAC File Offset: 0x000521AC
	// (set) Token: 0x06000B68 RID: 2920 RVA: 0x00053FB4 File Offset: 0x000521B4
	public long TellId
	{
		get
		{
			return this.mTellId;
		}
		set
		{
			this.mTellId = value;
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00053FC0 File Offset: 0x000521C0
	// (set) Token: 0x06000B6A RID: 2922 RVA: 0x00053FC8 File Offset: 0x000521C8
	public string TellName
	{
		get
		{
			return this.mTellName;
		}
		set
		{
			this.mTellName = value;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00053FD4 File Offset: 0x000521D4
	// (set) Token: 0x06000B6C RID: 2924 RVA: 0x00053FDC File Offset: 0x000521DC
	public string ChatInfo
	{
		get
		{
			return this.mChatInfo;
		}
		set
		{
			this.mChatInfo = value;
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00053FE8 File Offset: 0x000521E8
	// (set) Token: 0x06000B6E RID: 2926 RVA: 0x00053FF0 File Offset: 0x000521F0
	public string ChatInfo2
	{
		get
		{
			return this.mChatInfo2;
		}
		set
		{
			this.mChatInfo2 = value;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00053FFC File Offset: 0x000521FC
	// (set) Token: 0x06000B70 RID: 2928 RVA: 0x00054004 File Offset: 0x00052204
	public GameDefine.CHAT_LINK_TYPE LinkType
	{
		get
		{
			return this.mLinkType;
		}
		set
		{
			this.mLinkType = value;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00054010 File Offset: 0x00052210
	// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00054018 File Offset: 0x00052218
	public PROFESSION_TYPE SenderProfession
	{
		get
		{
			return this.mSenderProfession;
		}
		set
		{
			this.mSenderProfession = value;
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00054024 File Offset: 0x00052224
	// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0005402C File Offset: 0x0005222C
	public int Level
	{
		get
		{
			return this.mLevel;
		}
		set
		{
			this.mLevel = value;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00054038 File Offset: 0x00052238
	// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00054040 File Offset: 0x00052240
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

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0005404C File Offset: 0x0005224C
	// (set) Token: 0x06000B78 RID: 2936 RVA: 0x00054054 File Offset: 0x00052254
	public long GuildId
	{
		get
		{
			return this.mGuildId;
		}
		set
		{
			this.mGuildId = value;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00054060 File Offset: 0x00052260
	// (set) Token: 0x06000B7A RID: 2938 RVA: 0x00054068 File Offset: 0x00052268
	public string GuildName
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

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00054074 File Offset: 0x00052274
	public List<long> LongData
	{
		get
		{
			return this.mLongData;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0005407C File Offset: 0x0005227C
	public List<string> StringData
	{
		get
		{
			return this.mStringData;
		}
	}

	// Token: 0x04000A43 RID: 2627
	private GameDefine.CHAT_CHANNEL_TYPE mChannelType;

	// Token: 0x04000A44 RID: 2628
	private long mSenderServerId;

	// Token: 0x04000A45 RID: 2629
	private string mSenderName;

	// Token: 0x04000A46 RID: 2630
	private long mTellId;

	// Token: 0x04000A47 RID: 2631
	private string mTellName;

	// Token: 0x04000A48 RID: 2632
	private string mChatInfo;

	// Token: 0x04000A49 RID: 2633
	private string mChatInfo2 = string.Empty;

	// Token: 0x04000A4A RID: 2634
	private GameDefine.CHAT_LINK_TYPE mLinkType;

	// Token: 0x04000A4B RID: 2635
	private PROFESSION_TYPE mSenderProfession;

	// Token: 0x04000A4C RID: 2636
	private int mLevel;

	// Token: 0x04000A4D RID: 2637
	private int mComboValue;

	// Token: 0x04000A4E RID: 2638
	private long mGuildId;

	// Token: 0x04000A4F RID: 2639
	private string mGuildName;

	// Token: 0x04000A50 RID: 2640
	private List<long> mLongData = new List<long>();

	// Token: 0x04000A51 RID: 2641
	private List<string> mStringData = new List<string>();
}
