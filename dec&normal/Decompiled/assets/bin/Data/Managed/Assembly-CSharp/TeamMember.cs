using System;
using SprotoType;

// Token: 0x0200086A RID: 2154
public class TeamMember
{
	// Token: 0x06003833 RID: 14387 RVA: 0x000E90D8 File Offset: 0x000E72D8
	public void Init()
	{
		this.mServerId = -1L;
		this.mTeamJob = -1;
	}

	// Token: 0x17000F51 RID: 3921
	// (get) Token: 0x06003834 RID: 14388 RVA: 0x000E90EC File Offset: 0x000E72EC
	// (set) Token: 0x06003835 RID: 14389 RVA: 0x000E90F4 File Offset: 0x000E72F4
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

	// Token: 0x17000F52 RID: 3922
	// (get) Token: 0x06003836 RID: 14390 RVA: 0x000E9100 File Offset: 0x000E7300
	// (set) Token: 0x06003837 RID: 14391 RVA: 0x000E9108 File Offset: 0x000E7308
	public int TeamJob
	{
		get
		{
			return this.mTeamJob;
		}
		set
		{
			this.mTeamJob = value;
		}
	}

	// Token: 0x17000F53 RID: 3923
	// (get) Token: 0x06003838 RID: 14392 RVA: 0x000E9114 File Offset: 0x000E7314
	// (set) Token: 0x06003839 RID: 14393 RVA: 0x000E911C File Offset: 0x000E731C
	public long TeamId
	{
		get
		{
			return this.mTeamId;
		}
		set
		{
			this.mTeamId = value;
		}
	}

	// Token: 0x17000F54 RID: 3924
	// (get) Token: 0x0600383A RID: 14394 RVA: 0x000E9128 File Offset: 0x000E7328
	// (set) Token: 0x0600383B RID: 14395 RVA: 0x000E9130 File Offset: 0x000E7330
	public string Name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
		}
	}

	// Token: 0x17000F55 RID: 3925
	// (get) Token: 0x0600383C RID: 14396 RVA: 0x000E913C File Offset: 0x000E733C
	// (set) Token: 0x0600383D RID: 14397 RVA: 0x000E9144 File Offset: 0x000E7344
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

	// Token: 0x17000F56 RID: 3926
	// (get) Token: 0x0600383E RID: 14398 RVA: 0x000E9150 File Offset: 0x000E7350
	// (set) Token: 0x0600383F RID: 14399 RVA: 0x000E9158 File Offset: 0x000E7358
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

	// Token: 0x17000F57 RID: 3927
	// (get) Token: 0x06003840 RID: 14400 RVA: 0x000E9164 File Offset: 0x000E7364
	// (set) Token: 0x06003841 RID: 14401 RVA: 0x000E916C File Offset: 0x000E736C
	public int CombValue
	{
		get
		{
			return this.mCombValue;
		}
		set
		{
			this.mCombValue = value;
		}
	}

	// Token: 0x17000F58 RID: 3928
	// (get) Token: 0x06003842 RID: 14402 RVA: 0x000E9178 File Offset: 0x000E7378
	// (set) Token: 0x06003843 RID: 14403 RVA: 0x000E9180 File Offset: 0x000E7380
	public string MapInfoId
	{
		get
		{
			return this.mMapInfoId;
		}
		set
		{
			this.mMapInfoId = value;
		}
	}

	// Token: 0x17000F59 RID: 3929
	// (get) Token: 0x06003844 RID: 14404 RVA: 0x000E918C File Offset: 0x000E738C
	// (set) Token: 0x06003845 RID: 14405 RVA: 0x000E9194 File Offset: 0x000E7394
	public int ServerLineIndex
	{
		get
		{
			return this.mServerLineIndex;
		}
		set
		{
			this.mServerLineIndex = value;
		}
	}

	// Token: 0x17000F5A RID: 3930
	// (get) Token: 0x06003846 RID: 14406 RVA: 0x000E91A0 File Offset: 0x000E73A0
	// (set) Token: 0x06003847 RID: 14407 RVA: 0x000E91A8 File Offset: 0x000E73A8
	public int MemberType
	{
		get
		{
			return this.mMemberType;
		}
		set
		{
			this.mMemberType = value;
		}
	}

	// Token: 0x17000F5B RID: 3931
	// (get) Token: 0x06003848 RID: 14408 RVA: 0x000E91B4 File Offset: 0x000E73B4
	// (set) Token: 0x06003849 RID: 14409 RVA: 0x000E91BC File Offset: 0x000E73BC
	public int HP
	{
		get
		{
			return this.mHP;
		}
		set
		{
			this.mHP = value;
		}
	}

	// Token: 0x17000F5C RID: 3932
	// (get) Token: 0x0600384A RID: 14410 RVA: 0x000E91C8 File Offset: 0x000E73C8
	// (set) Token: 0x0600384B RID: 14411 RVA: 0x000E91D0 File Offset: 0x000E73D0
	public int MaxHP
	{
		get
		{
			return this.mMaxHP;
		}
		set
		{
			this.mMaxHP = value;
		}
	}

	// Token: 0x17000F5D RID: 3933
	// (get) Token: 0x0600384C RID: 14412 RVA: 0x000E91DC File Offset: 0x000E73DC
	// (set) Token: 0x0600384D RID: 14413 RVA: 0x000E91E4 File Offset: 0x000E73E4
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

	// Token: 0x17000F5E RID: 3934
	// (get) Token: 0x0600384E RID: 14414 RVA: 0x000E91F0 File Offset: 0x000E73F0
	// (set) Token: 0x0600384F RID: 14415 RVA: 0x000E91F8 File Offset: 0x000E73F8
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

	// Token: 0x06003850 RID: 14416 RVA: 0x000E9204 File Offset: 0x000E7404
	public bool IsValid()
	{
		return this.mServerId != -1L;
	}

	// Token: 0x17000F5F RID: 3935
	// (get) Token: 0x06003851 RID: 14417 RVA: 0x000E9214 File Offset: 0x000E7414
	// (set) Token: 0x06003852 RID: 14418 RVA: 0x000E921C File Offset: 0x000E741C
	public characterVisual Visual
	{
		get
		{
			return this.visual;
		}
		set
		{
			this.visual = value;
		}
	}

	// Token: 0x17000F60 RID: 3936
	// (get) Token: 0x06003853 RID: 14419 RVA: 0x000E9228 File Offset: 0x000E7428
	// (set) Token: 0x06003854 RID: 14420 RVA: 0x000E9230 File Offset: 0x000E7430
	public bool IsReadyEnterCopy
	{
		get
		{
			return this.mIsReadyEnterCopy;
		}
		set
		{
			this.mIsReadyEnterCopy = value;
		}
	}

	// Token: 0x17000F61 RID: 3937
	// (get) Token: 0x06003855 RID: 14421 RVA: 0x000E923C File Offset: 0x000E743C
	// (set) Token: 0x06003856 RID: 14422 RVA: 0x000E9244 File Offset: 0x000E7444
	public bool IsRefuseEnterCopy
	{
		get
		{
			return this.mIsRefuseEnterCopy;
		}
		set
		{
			this.mIsRefuseEnterCopy = value;
		}
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x000E9250 File Offset: 0x000E7450
	public void ClearEnterCopyState()
	{
		this.mIsReadyEnterCopy = false;
		this.mIsRefuseEnterCopy = false;
	}

	// Token: 0x17000F62 RID: 3938
	// (get) Token: 0x06003858 RID: 14424 RVA: 0x000E9260 File Offset: 0x000E7460
	// (set) Token: 0x06003859 RID: 14425 RVA: 0x000E9268 File Offset: 0x000E7468
	public int CopyRestNum
	{
		get
		{
			return this.mCopyRestNum;
		}
		set
		{
			this.mCopyRestNum = value;
		}
	}

	// Token: 0x0400250A RID: 9482
	private long mServerId;

	// Token: 0x0400250B RID: 9483
	private int mTeamJob;

	// Token: 0x0400250C RID: 9484
	private long mTeamId;

	// Token: 0x0400250D RID: 9485
	private string mName;

	// Token: 0x0400250E RID: 9486
	private int mLevel;

	// Token: 0x0400250F RID: 9487
	private PROFESSION_TYPE mProfession;

	// Token: 0x04002510 RID: 9488
	private int mCombValue;

	// Token: 0x04002511 RID: 9489
	private string mMapInfoId;

	// Token: 0x04002512 RID: 9490
	private int mServerLineIndex;

	// Token: 0x04002513 RID: 9491
	private int mMemberType;

	// Token: 0x04002514 RID: 9492
	private int mHP;

	// Token: 0x04002515 RID: 9493
	private int mMaxHP;

	// Token: 0x04002516 RID: 9494
	private string mGuildName;

	// Token: 0x04002517 RID: 9495
	private long mGuildId;

	// Token: 0x04002518 RID: 9496
	private characterVisual visual;

	// Token: 0x04002519 RID: 9497
	private bool mIsReadyEnterCopy;

	// Token: 0x0400251A RID: 9498
	private bool mIsRefuseEnterCopy;

	// Token: 0x0400251B RID: 9499
	private int mCopyRestNum = -1;
}
