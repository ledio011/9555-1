using System;
using SprotoType;

// Token: 0x0200085A RID: 2138
public class GuildMember
{
	// Token: 0x0600375A RID: 14170 RVA: 0x000E3F4C File Offset: 0x000E214C
	public GuildMember()
	{
		this.Init();
	}

	// Token: 0x0600375B RID: 14171 RVA: 0x000E3F5C File Offset: 0x000E215C
	public GuildMember(guild_member_info info)
	{
		this.mServerId = info.characterId;
		this.mMemberName = info.name;
		this.mJob = ((!info.HasJob) ? Guild_JOB.NO_JOB : ((Guild_JOB)info.job));
		this.mLevel = ((!info.HasLevel) ? 0 : ((int)info.level));
		this.mProfession = ((!info.HasProfession) ? PROFESSION_TYPE.INVALID : ((PROFESSION_TYPE)info.profession));
		this.mState = ((!info.HasState) ? 0 : ((int)info.state));
		this.mVip = ((!info.HasVip) ? -1 : ((int)info.vip));
		this.mContribute = ((!info.HasContribute) ? 0 : ((int)info.contribute));
		this.mComboValue = ((!info.HasCombValue) ? 0 : ((int)info.combValue));
		this.mLastLogout = ((!info.HasLastLogout) ? -1L : info.lastLogout);
		this.mAllContribute = ((!info.HasAll_contribute) ? 0 : ((int)info.all_contribute));
	}

	// Token: 0x0600375C RID: 14172 RVA: 0x000E4098 File Offset: 0x000E2298
	public void Init()
	{
	}

	// Token: 0x17000F29 RID: 3881
	// (get) Token: 0x0600375D RID: 14173 RVA: 0x000E409C File Offset: 0x000E229C
	// (set) Token: 0x0600375E RID: 14174 RVA: 0x000E40A4 File Offset: 0x000E22A4
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

	// Token: 0x17000F2A RID: 3882
	// (get) Token: 0x0600375F RID: 14175 RVA: 0x000E40B0 File Offset: 0x000E22B0
	// (set) Token: 0x06003760 RID: 14176 RVA: 0x000E40B8 File Offset: 0x000E22B8
	public string MemberName
	{
		get
		{
			return this.mMemberName;
		}
		set
		{
			this.mMemberName = value;
		}
	}

	// Token: 0x17000F2B RID: 3883
	// (get) Token: 0x06003761 RID: 14177 RVA: 0x000E40C4 File Offset: 0x000E22C4
	// (set) Token: 0x06003762 RID: 14178 RVA: 0x000E40CC File Offset: 0x000E22CC
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

	// Token: 0x17000F2C RID: 3884
	// (get) Token: 0x06003763 RID: 14179 RVA: 0x000E40D8 File Offset: 0x000E22D8
	// (set) Token: 0x06003764 RID: 14180 RVA: 0x000E40E0 File Offset: 0x000E22E0
	public int VIP
	{
		get
		{
			return this.mVip;
		}
		set
		{
			this.mVip = value;
		}
	}

	// Token: 0x17000F2D RID: 3885
	// (get) Token: 0x06003765 RID: 14181 RVA: 0x000E40EC File Offset: 0x000E22EC
	// (set) Token: 0x06003766 RID: 14182 RVA: 0x000E40F4 File Offset: 0x000E22F4
	public PROFESSION_TYPE Profession
	{
		get
		{
			return this.mProfession;
		}
		set
		{
			this.mProfession = value;
		}
	}

	// Token: 0x17000F2E RID: 3886
	// (get) Token: 0x06003767 RID: 14183 RVA: 0x000E4100 File Offset: 0x000E2300
	// (set) Token: 0x06003768 RID: 14184 RVA: 0x000E4108 File Offset: 0x000E2308
	public int Contribute
	{
		get
		{
			return this.mContribute;
		}
		set
		{
			this.mContribute = value;
		}
	}

	// Token: 0x17000F2F RID: 3887
	// (get) Token: 0x06003769 RID: 14185 RVA: 0x000E4114 File Offset: 0x000E2314
	// (set) Token: 0x0600376A RID: 14186 RVA: 0x000E411C File Offset: 0x000E231C
	public int AllContribute
	{
		get
		{
			return this.mAllContribute;
		}
		set
		{
			this.mAllContribute = value;
		}
	}

	// Token: 0x17000F30 RID: 3888
	// (get) Token: 0x0600376B RID: 14187 RVA: 0x000E4128 File Offset: 0x000E2328
	// (set) Token: 0x0600376C RID: 14188 RVA: 0x000E4130 File Offset: 0x000E2330
	public Guild_JOB Job
	{
		get
		{
			return this.mJob;
		}
		set
		{
			this.mJob = value;
		}
	}

	// Token: 0x17000F31 RID: 3889
	// (get) Token: 0x0600376D RID: 14189 RVA: 0x000E413C File Offset: 0x000E233C
	// (set) Token: 0x0600376E RID: 14190 RVA: 0x000E4144 File Offset: 0x000E2344
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

	// Token: 0x17000F32 RID: 3890
	// (get) Token: 0x0600376F RID: 14191 RVA: 0x000E4150 File Offset: 0x000E2350
	// (set) Token: 0x06003770 RID: 14192 RVA: 0x000E4158 File Offset: 0x000E2358
	public int State
	{
		get
		{
			return this.mState;
		}
		set
		{
			this.mState = value;
		}
	}

	// Token: 0x17000F33 RID: 3891
	// (get) Token: 0x06003771 RID: 14193 RVA: 0x000E4164 File Offset: 0x000E2364
	// (set) Token: 0x06003772 RID: 14194 RVA: 0x000E416C File Offset: 0x000E236C
	public long LastLogout
	{
		get
		{
			return this.mLastLogout;
		}
		set
		{
			this.mLastLogout = value;
		}
	}

	// Token: 0x04002487 RID: 9351
	private long mServerId;

	// Token: 0x04002488 RID: 9352
	private string mMemberName;

	// Token: 0x04002489 RID: 9353
	private int mLevel;

	// Token: 0x0400248A RID: 9354
	private int mVip;

	// Token: 0x0400248B RID: 9355
	private PROFESSION_TYPE mProfession;

	// Token: 0x0400248C RID: 9356
	private int mContribute;

	// Token: 0x0400248D RID: 9357
	private int mAllContribute;

	// Token: 0x0400248E RID: 9358
	private Guild_JOB mJob;

	// Token: 0x0400248F RID: 9359
	private int mComboValue;

	// Token: 0x04002490 RID: 9360
	public int mState;

	// Token: 0x04002491 RID: 9361
	private long mLastLogout;
}
