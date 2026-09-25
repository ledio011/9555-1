using System;

// Token: 0x02000135 RID: 309
public class RecentSpeaker
{
	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00054484 File Offset: 0x00052684
	// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0005448C File Offset: 0x0005268C
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

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00054498 File Offset: 0x00052698
	// (set) Token: 0x06000B8E RID: 2958 RVA: 0x000544A0 File Offset: 0x000526A0
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

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000B8F RID: 2959 RVA: 0x000544AC File Offset: 0x000526AC
	// (set) Token: 0x06000B90 RID: 2960 RVA: 0x000544B4 File Offset: 0x000526B4
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

	// Token: 0x06000B91 RID: 2961 RVA: 0x000544C0 File Offset: 0x000526C0
	public void Reset(long serverId, string name, PROFESSION_TYPE profession)
	{
		this.mServerId = serverId;
		this.mName = name;
		this.mProfession = profession;
		this.NewMessageFlag = false;
	}

	// Token: 0x04000A5A RID: 2650
	private long mServerId;

	// Token: 0x04000A5B RID: 2651
	private string mName;

	// Token: 0x04000A5C RID: 2652
	private PROFESSION_TYPE mProfession;

	// Token: 0x04000A5D RID: 2653
	public bool NewMessageFlag;
}
