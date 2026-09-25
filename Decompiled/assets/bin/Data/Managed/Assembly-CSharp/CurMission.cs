using System;

// Token: 0x02000213 RID: 531
public class CurMission
{
	// Token: 0x06001282 RID: 4738 RVA: 0x000792EC File Offset: 0x000774EC
	public CurMission()
	{
		this.Reset();
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001283 RID: 4739 RVA: 0x0007932C File Offset: 0x0007752C
	// (set) Token: 0x06001284 RID: 4740 RVA: 0x00079334 File Offset: 0x00077534
	public string MissionId
	{
		get
		{
			return this.mMissionId;
		}
		set
		{
			this.mMissionId = value;
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001285 RID: 4741 RVA: 0x00079340 File Offset: 0x00077540
	// (set) Token: 0x06001286 RID: 4742 RVA: 0x00079348 File Offset: 0x00077548
	public MISSION_STATE MissionState
	{
		get
		{
			return this.mMissionState;
		}
		set
		{
			this.mMissionState = value;
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06001287 RID: 4743 RVA: 0x00079354 File Offset: 0x00077554
	// (set) Token: 0x06001288 RID: 4744 RVA: 0x0007935C File Offset: 0x0007755C
	public long[] MissionParam
	{
		get
		{
			return this.mMissionParam;
		}
		set
		{
			this.mMissionParam = value;
		}
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x00079368 File Offset: 0x00077568
	public void Reset()
	{
		this.MissionId = string.Empty;
		this.MissionState = MISSION_STATE.INVALID;
		for (int i = 0; i < this.mMissionParam.Length; i++)
		{
			this.mMissionParam[i] = 0L;
		}
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x000793AC File Offset: 0x000775AC
	public void SetParam(int paramIndex, long val)
	{
		if (paramIndex >= 0 && paramIndex < 8)
		{
			this.mMissionParam[paramIndex] = val;
		}
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x000793C8 File Offset: 0x000775C8
	public long GetParam(int paramIndex)
	{
		if (paramIndex >= 0 && paramIndex < 8)
		{
			return this.mMissionParam[paramIndex];
		}
		return -1L;
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x000793E4 File Offset: 0x000775E4
	public bool SetMissionState(MISSION_STATE state)
	{
		this.mMissionState = state;
		return true;
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x000793F0 File Offset: 0x000775F0
	public MISSION_STATE GetMissionState()
	{
		return this.mMissionState;
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x0600128E RID: 4750 RVA: 0x000793F8 File Offset: 0x000775F8
	// (set) Token: 0x0600128F RID: 4751 RVA: 0x00079400 File Offset: 0x00077600
	public long LastChangeTime
	{
		get
		{
			return this.mLastChangeTime;
		}
		set
		{
			this.mLastChangeTime = value;
		}
	}

	// Token: 0x040017C3 RID: 6083
	public const int MAX_MISSION_PARAM_NUM = 8;

	// Token: 0x040017C4 RID: 6084
	private string mMissionId = string.Empty;

	// Token: 0x040017C5 RID: 6085
	private MISSION_STATE mMissionState = MISSION_STATE.INVALID;

	// Token: 0x040017C6 RID: 6086
	private long[] mMissionParam = new long[8];

	// Token: 0x040017C7 RID: 6087
	private long mLastChangeTime = -1L;
}
