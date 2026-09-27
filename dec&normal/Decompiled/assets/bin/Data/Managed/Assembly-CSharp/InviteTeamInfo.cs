using System;

// Token: 0x02000869 RID: 2153
public class InviteTeamInfo
{
	// Token: 0x06003830 RID: 14384 RVA: 0x000E8FF8 File Offset: 0x000E71F8
	public InviteTeamInfo(long chaid, long teamid, string name, long time, string goalid)
	{
		this.characterId = chaid;
		this.teamId = teamid;
		this.inviteTime = time;
		this.inviteName = name;
		this.IsUrgeFlag = false;
		this.TeamGoalId = goalid;
	}

	// Token: 0x06003831 RID: 14385 RVA: 0x000E9068 File Offset: 0x000E7268
	public InviteTeamInfo(bool isurge, string name, long memberid, long time)
	{
		this.IsUrgeFlag = isurge;
		this.characterId = memberid;
		this.inviteName = name;
		this.inviteTime = time;
	}

	// Token: 0x04002504 RID: 9476
	public long characterId = -1L;

	// Token: 0x04002505 RID: 9477
	public long teamId = -1L;

	// Token: 0x04002506 RID: 9478
	public long inviteTime = -1L;

	// Token: 0x04002507 RID: 9479
	public string inviteName = string.Empty;

	// Token: 0x04002508 RID: 9480
	public bool IsUrgeFlag;

	// Token: 0x04002509 RID: 9481
	public string TeamGoalId = string.Empty;
}
