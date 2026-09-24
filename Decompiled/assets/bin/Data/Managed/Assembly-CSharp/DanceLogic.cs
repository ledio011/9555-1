using System;
using UnityEngine;

// Token: 0x02000845 RID: 2117
public class DanceLogic : MonoBehaviour
{
	// Token: 0x17000EF9 RID: 3833
	// (get) Token: 0x06003662 RID: 13922 RVA: 0x000DFA88 File Offset: 0x000DDC88
	// (set) Token: 0x06003663 RID: 13923 RVA: 0x000DFA90 File Offset: 0x000DDC90
	public bool DancingFlag
	{
		get
		{
			return this.mDancingFlag;
		}
		set
		{
			this.mDancingFlag = value;
		}
	}

	// Token: 0x17000EFA RID: 3834
	// (get) Token: 0x06003664 RID: 13924 RVA: 0x000DFA9C File Offset: 0x000DDC9C
	// (set) Token: 0x06003665 RID: 13925 RVA: 0x000DFAA4 File Offset: 0x000DDCA4
	public DanceData CurDanceData
	{
		get
		{
			return this.mCurDanceData;
		}
		set
		{
			this.mCurDanceData = value;
		}
	}

	// Token: 0x17000EFB RID: 3835
	// (get) Token: 0x06003666 RID: 13926 RVA: 0x000DFAB0 File Offset: 0x000DDCB0
	// (set) Token: 0x06003667 RID: 13927 RVA: 0x000DFAB8 File Offset: 0x000DDCB8
	public ObjOtherPlayer Owner
	{
		get
		{
			return this.mOwner;
		}
		set
		{
			this.mOwner = value;
		}
	}

	// Token: 0x17000EFC RID: 3836
	// (get) Token: 0x06003668 RID: 13928 RVA: 0x000DFAC4 File Offset: 0x000DDCC4
	// (set) Token: 0x06003669 RID: 13929 RVA: 0x000DFACC File Offset: 0x000DDCCC
	public ActionData CurActionData
	{
		get
		{
			return this.mCurActionData;
		}
		set
		{
			this.mCurActionData = value;
		}
	}

	// Token: 0x0600366A RID: 13930 RVA: 0x000DFAD8 File Offset: 0x000DDCD8
	public void Reset(ObjOtherPlayer owner)
	{
		this.mOwner = owner;
	}

	// Token: 0x0600366B RID: 13931 RVA: 0x000DFAE4 File Offset: 0x000DDCE4
	public void StartDance(string danceId)
	{
		this.mCurDanceData = DataManager.GetDanceDataById(danceId);
		string actionName = this.mOwner.GetActionName(this.mCurDanceData.ActionName);
		this.mCurActionData = DataManager.GetActionDataByName(actionName);
		float startTime = Time.realtimeSinceStartup % this.mCurActionData.AnimDurationTimeSecond / this.mCurActionData.AnimDurationTimeSecond;
		this.mOwner.AnimationLogic.PlayAnimation(this.mCurActionData, null, -1f, startTime);
		if (!string.IsNullOrEmpty(this.mCurActionData.FxEffID))
		{
			this.mOwner.EffectLogic.AddPlayeBufEffInfoData(this.mCurActionData.FxEffID, 0f, float.MaxValue, this.mOwner.Position);
		}
	}

	// Token: 0x0600366C RID: 13932 RVA: 0x000DFBA0 File Offset: 0x000DDDA0
	public void StopDance()
	{
		this.mOwner.EffectLogic.BreakEffect(this.mCurActionData.FxEffID);
	}

	// Token: 0x040023E0 RID: 9184
	private bool mDancingFlag;

	// Token: 0x040023E1 RID: 9185
	private DanceData mCurDanceData;

	// Token: 0x040023E2 RID: 9186
	private ObjOtherPlayer mOwner;

	// Token: 0x040023E3 RID: 9187
	private ActionData mCurActionData;
}
