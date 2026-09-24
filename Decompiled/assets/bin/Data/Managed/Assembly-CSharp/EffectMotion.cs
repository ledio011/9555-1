using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000849 RID: 2121
public class EffectMotion : MonoBehaviour
{
	// Token: 0x0600368C RID: 13964 RVA: 0x000E0BF0 File Offset: 0x000DEDF0
	public void Init(ObjCharacter objCharacter)
	{
		this.ownner = objCharacter;
	}

	// Token: 0x0600368D RID: 13965 RVA: 0x000E0BFC File Offset: 0x000DEDFC
	public void AddPlayEffInfoMotionData(string effInfoId, float delayTime, Vector3 senderPos)
	{
		if (!this.ownner.IsDie)
		{
			this.PlayEffInfoMotionDataList.Add(new PlayEffInfoMotionData(effInfoId, delayTime, senderPos));
		}
	}

	// Token: 0x17000EFF RID: 3839
	// (get) Token: 0x0600368E RID: 13966 RVA: 0x000E0C24 File Offset: 0x000DEE24
	// (set) Token: 0x0600368F RID: 13967 RVA: 0x000E0C2C File Offset: 0x000DEE2C
	public Vector3 MoveStartPos
	{
		get
		{
			return this.mMoveStartPos;
		}
		set
		{
			this.mMoveStartPos = value;
		}
	}

	// Token: 0x17000F00 RID: 3840
	// (get) Token: 0x06003690 RID: 13968 RVA: 0x000E0C38 File Offset: 0x000DEE38
	// (set) Token: 0x06003691 RID: 13969 RVA: 0x000E0C40 File Offset: 0x000DEE40
	public Vector3 MoveTargetPos
	{
		get
		{
			return this.mMoveTargetPos;
		}
		set
		{
			this.mMoveTargetPos = value;
		}
	}

	// Token: 0x17000F01 RID: 3841
	// (get) Token: 0x06003692 RID: 13970 RVA: 0x000E0C4C File Offset: 0x000DEE4C
	// (set) Token: 0x06003693 RID: 13971 RVA: 0x000E0C54 File Offset: 0x000DEE54
	public float MoveTime
	{
		get
		{
			return this.mMoveTime;
		}
		set
		{
			this.mMoveTime = value;
		}
	}

	// Token: 0x17000F02 RID: 3842
	// (get) Token: 0x06003694 RID: 13972 RVA: 0x000E0C60 File Offset: 0x000DEE60
	// (set) Token: 0x06003695 RID: 13973 RVA: 0x000E0C68 File Offset: 0x000DEE68
	public float MoveStartTime
	{
		get
		{
			return this.mMoveStartTime;
		}
		set
		{
			this.mMoveStartTime = value;
		}
	}

	// Token: 0x17000F03 RID: 3843
	// (get) Token: 0x06003696 RID: 13974 RVA: 0x000E0C74 File Offset: 0x000DEE74
	// (set) Token: 0x06003697 RID: 13975 RVA: 0x000E0C7C File Offset: 0x000DEE7C
	public bool NeedMoveFlag
	{
		get
		{
			return this.mNeedMoveFlag;
		}
		set
		{
			this.mNeedMoveFlag = value;
		}
	}

	// Token: 0x17000F04 RID: 3844
	// (get) Token: 0x06003698 RID: 13976 RVA: 0x000E0C88 File Offset: 0x000DEE88
	// (set) Token: 0x06003699 RID: 13977 RVA: 0x000E0C90 File Offset: 0x000DEE90
	public bool MoveLockFlag
	{
		get
		{
			return this.mMoveLockFlag;
		}
		set
		{
			this.mMoveLockFlag = value;
		}
	}

	// Token: 0x0600369A RID: 13978 RVA: 0x000E0C9C File Offset: 0x000DEE9C
	private void Start()
	{
	}

	// Token: 0x0600369B RID: 13979 RVA: 0x000E0CA0 File Offset: 0x000DEEA0
	private void Update()
	{
		this.UpdateEffectMotion();
	}

	// Token: 0x0600369C RID: 13980 RVA: 0x000E0CA8 File Offset: 0x000DEEA8
	public void UpdateEffectMotion()
	{
		if (!this.mNeedMoveFlag)
		{
			if (this.PlayEffInfoMotionDataList.Count > 0)
			{
				for (int i = 0; i < this.PlayEffInfoMotionDataList.Count; i++)
				{
					this.PlayEffInfoMotionDataList[i].DelayTime -= Time.deltaTime;
				}
				if (this.PlayEffInfoMotionDataList[0].DelayTime <= 0f)
				{
					this.ResetMotion();
				}
			}
		}
		else
		{
			this.movePercent = (Time.time - this.mMoveStartTime) / this.mMoveTime;
			this.ownner.Position = Vector3.Lerp(this.mMoveStartPos, this.mMoveTargetPos, this.movePercent);
			if (this.movePercent >= 1f)
			{
				this.movePercent = 0f;
				this.NeedMoveFlag = false;
			}
		}
	}

	// Token: 0x0600369D RID: 13981 RVA: 0x000E0D90 File Offset: 0x000DEF90
	public void ResetMotion()
	{
		EffInfoData effInfoData = this.PlayEffInfoMotionDataList[0].EffInfoData;
		this.mNeedMoveFlag = true;
		this.mMoveStartPos = this.ownner.Position;
		if (effInfoData.MoveAngle == -1)
		{
			effInfoData.MoveAngle = Random.Range(0, 360);
		}
		if (effInfoData.ForceMove != 1)
		{
			this.ownner.FaceToPub(this.PlayEffInfoMotionDataList[0].SenderPos);
		}
		this.mMoveTargetPos = Quaternion.AngleAxis((float)effInfoData.MoveAngle, Vector3.up) * (this.ownner.CacheTransform.forward * -1f) * effInfoData.MoveDistanceMeter + this.ownner.Position;
		NavMeshHit navMeshHit;
		if (NavMesh.Raycast(this.mMoveStartPos, this.mMoveTargetPos, ref navMeshHit, 15))
		{
			this.mMoveTime = effInfoData.MoveTimeSecond * VectorXZ.Distance(navMeshHit.position, this.mMoveStartPos) / effInfoData.MoveDistanceMeter;
			this.mMoveTargetPos = navMeshHit.position;
		}
		else
		{
			this.mMoveTime = effInfoData.MoveTimeSecond;
		}
		if (this.MoveTime <= 0.1f)
		{
			this.NeedMoveFlag = false;
			this.movePercent = 0f;
		}
		else
		{
			this.NeedMoveFlag = true;
			this.movePercent = 0f;
			this.mMoveStartTime = Time.time;
		}
		this.PlayEffInfoMotionDataList.RemoveAt(0);
	}

	// Token: 0x0600369E RID: 13982 RVA: 0x000E0F18 File Offset: 0x000DF118
	public void BreakCurEffectMotion()
	{
		this.NeedMoveFlag = false;
		this.movePercent = 0f;
		this.PlayEffInfoMotionDataList.Clear();
	}

	// Token: 0x040023F5 RID: 9205
	private ObjCharacter ownner;

	// Token: 0x040023F6 RID: 9206
	public List<PlayEffInfoMotionData> PlayEffInfoMotionDataList = new List<PlayEffInfoMotionData>();

	// Token: 0x040023F7 RID: 9207
	private Vector3 mMoveStartPos;

	// Token: 0x040023F8 RID: 9208
	private Vector3 mMoveTargetPos;

	// Token: 0x040023F9 RID: 9209
	private float mMoveTime;

	// Token: 0x040023FA RID: 9210
	private float mMoveStartTime;

	// Token: 0x040023FB RID: 9211
	private bool mNeedMoveFlag;

	// Token: 0x040023FC RID: 9212
	private bool mMoveLockFlag;

	// Token: 0x040023FD RID: 9213
	private float moveLockTimeCount;

	// Token: 0x040023FE RID: 9214
	private float movePercent;
}
