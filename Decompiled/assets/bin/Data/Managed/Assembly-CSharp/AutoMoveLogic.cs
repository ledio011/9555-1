using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000836 RID: 2102
public class AutoMoveLogic : MonoBehaviour
{
	// Token: 0x17000EDB RID: 3803
	// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000DB20C File Offset: 0x000D940C
	private bool mIsMoving
	{
		get
		{
			return this.owner.IsMoving;
		}
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x000DB21C File Offset: 0x000D941C
	public void Init(ObjCharacter objOwner)
	{
		this.owner = objOwner;
		this.targetAngel = (this.currentAngle = MathUtil.Heading(this.owner.CacheTransform.forward));
		this.curIndex = 0;
		this.mMovePosList.Clear();
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x000DB268 File Offset: 0x000D9468
	public void Reset()
	{
		this.curIndex = 0;
		this.owner.StopMove();
		this.mMovePosList.Clear();
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x000DB288 File Offset: 0x000D9488
	public void AddMovePoint(MovePoint point)
	{
		if (this.owner.CurPlayerState == PLAYER_STATE.DANCE)
		{
			return;
		}
		if (this.mMovePosList.Count > 32)
		{
			this.mMovePosList.Clear();
		}
		if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
		{
			this.mMovePosList.Clear();
			this.mMovePosList.Add(point);
		}
		else
		{
			this.mMovePosList.Add(point);
		}
	}

	// Token: 0x060035A6 RID: 13734 RVA: 0x000DB2FC File Offset: 0x000D94FC
	public void PrintMoveList()
	{
		for (int i = 0; i < this.mMovePosList.Count; i++)
		{
			MonoBehaviour.print(string.Concat(new object[]
			{
				this.mMovePosList[i].Index,
				" : ",
				this.mMovePosList[i].xPos,
				",",
				this.mMovePosList[i].zPos
			}));
		}
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x000DB39C File Offset: 0x000D959C
	private void ReachPoint()
	{
		this.mLastPos = this.owner.Position;
		if (this.mMovePosList.Count <= 0)
		{
			return;
		}
		this.mMovePosList.RemoveAt(0);
		if (this.mMovePosList.Count <= 0)
		{
			this.StopMove();
		}
		else
		{
			this.MoveTo(this.mMovePosList[0]);
		}
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x000DB408 File Offset: 0x000D9608
	private void MoveTo(MovePoint point)
	{
		this.targetAngel = point.O;
		if (!this.owner.IsDie)
		{
			this.mObstacleCheckTime = Time.time;
			this.curIndex = point.Index;
			Vector3 pos;
			pos..ctor(point.xPos, this.owner.Position.y + 0.1f, point.zPos);
			if (this.owner.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
			{
				if (point.walk)
				{
					this.owner.WalkMoveTo(pos, 0f, null);
				}
				else
				{
					this.owner.MoveTo(pos, 0f, null);
				}
			}
			else
			{
				this.owner.MoveTo(pos, 0f, null);
			}
		}
		else
		{
			this.StopMove();
		}
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x000DB4E0 File Offset: 0x000D96E0
	public void StopMove()
	{
		this.owner.StopMove();
		this.Reset();
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x000DB4F4 File Offset: 0x000D96F4
	public void InterruptMove(MovePoint point)
	{
		int num = 0;
		if (num >= this.mMovePosList.Count)
		{
			return;
		}
		if (point.Index == this.mMovePosList[num].Index && num < this.mMovePosList.Count - 1)
		{
			this.mMovePosList.RemoveRange(num + 1, this.mMovePosList.Count - num - 1);
		}
		this.mMovePosList.RemoveAt(num);
		this.mMovePosList.Add(point);
		if (num == 0)
		{
			this.MoveTo(this.mMovePosList[num]);
		}
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x000DB59C File Offset: 0x000D979C
	private void ObstacleCheck()
	{
		if (Time.time - this.mObstacleCheckTime <= this.mObstacleCheckInterval)
		{
			return;
		}
		this.mObstacleCheckTime = Time.time;
		if (!this.mIsMoving || this.mMovePosList.Count <= 0)
		{
			return;
		}
		if (VectorXZ.Distance(this.mLastPos, this.owner.Position) < 0.1f)
		{
			Vector3 position;
			position..ctor(this.mMovePosList[0].xPos, 0f, this.mMovePosList[0].zPos);
			this.owner.CacheTransform.position = position;
			this.ReachPoint();
		}
		this.mLastPos = this.owner.Position;
	}

	// Token: 0x060035AC RID: 13740 RVA: 0x000DB674 File Offset: 0x000D9874
	private void UpdateAngel()
	{
		this.currentAngle = MathUtil.WrapDegrees(Mathf.Lerp(this.currentAngle, this.targetAngel, 10f * Time.deltaTime));
		this.owner.CacheTransform.forward = MathUtil.HeadingToVector3(this.currentAngle);
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x000DB6C4 File Offset: 0x000D98C4
	private bool IsReachPoint(MovePoint p)
	{
		VectorXZ a = new VectorXZ(p.xPos, p.zPos);
		VectorXZ b = new VectorXZ(this.owner.Position.x, this.owner.Position.z);
		float num = 0.5f;
		return VectorXZ.Distance(a, b) < num;
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x000DB72C File Offset: 0x000D992C
	private void CheckMove()
	{
		if (this.mMovePosList.Count <= 0)
		{
			return;
		}
		if (this.mIsMoving)
		{
			if (this.IsReachPoint(this.mMovePosList[0]))
			{
				this.ReachPoint();
				return;
			}
			this.MoveTo(this.mMovePosList[0]);
		}
		else
		{
			this.MoveTo(this.mMovePosList[0]);
		}
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x000DB7A0 File Offset: 0x000D99A0
	private void FixedUpdate()
	{
		this.ObstacleCheck();
		this.CheckMove();
	}

	// Token: 0x04002314 RID: 8980
	private List<MovePoint> mMovePosList = new List<MovePoint>();

	// Token: 0x04002315 RID: 8981
	private int curIndex;

	// Token: 0x04002316 RID: 8982
	private ObjCharacter owner;

	// Token: 0x04002317 RID: 8983
	private float currentAngle;

	// Token: 0x04002318 RID: 8984
	private float targetAngel;

	// Token: 0x04002319 RID: 8985
	private float mObstacleCheckInterval = 2f;

	// Token: 0x0400231A RID: 8986
	private float mObstacleCheckTime;

	// Token: 0x0400231B RID: 8987
	private Vector3 mLastPos = Vector3.zero;
}
