using System;
using UnityEngine;

// Token: 0x02000867 RID: 2151
public class SkillMotion : MonoBehaviour
{
	// Token: 0x17000F3E RID: 3902
	// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000E78DC File Offset: 0x000E5ADC
	// (set) Token: 0x060037E9 RID: 14313 RVA: 0x000E78E4 File Offset: 0x000E5AE4
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

	// Token: 0x17000F3F RID: 3903
	// (get) Token: 0x060037EA RID: 14314 RVA: 0x000E78F0 File Offset: 0x000E5AF0
	// (set) Token: 0x060037EB RID: 14315 RVA: 0x000E78F8 File Offset: 0x000E5AF8
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

	// Token: 0x17000F40 RID: 3904
	// (get) Token: 0x060037EC RID: 14316 RVA: 0x000E7904 File Offset: 0x000E5B04
	// (set) Token: 0x060037ED RID: 14317 RVA: 0x000E790C File Offset: 0x000E5B0C
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

	// Token: 0x17000F41 RID: 3905
	// (get) Token: 0x060037EE RID: 14318 RVA: 0x000E7918 File Offset: 0x000E5B18
	// (set) Token: 0x060037EF RID: 14319 RVA: 0x000E7920 File Offset: 0x000E5B20
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

	// Token: 0x17000F42 RID: 3906
	// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000E792C File Offset: 0x000E5B2C
	// (set) Token: 0x060037F1 RID: 14321 RVA: 0x000E7934 File Offset: 0x000E5B34
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

	// Token: 0x060037F2 RID: 14322 RVA: 0x000E7940 File Offset: 0x000E5B40
	public void Init(ObjCharacter objCharacter)
	{
		this.ownner = objCharacter;
	}

	// Token: 0x060037F3 RID: 14323 RVA: 0x000E794C File Offset: 0x000E5B4C
	public void SkillMoveTo(Vector3 targetPos, float time, float delay)
	{
	}

	// Token: 0x060037F4 RID: 14324 RVA: 0x000E7950 File Offset: 0x000E5B50
	public void ResetSkillMove(SkillData skillData, float delayTime, ObjCharacter target)
	{
		if (delayTime > 0f)
		{
			if (this.mNeedMoveFlag)
			{
				this.handle.Cancel();
			}
			vp_Timer.In(delayTime, delegate()
			{
				this.ResetSkillMove(skillData, target);
			}, this.handle);
		}
		else
		{
			this.ResetSkillMove(skillData, target);
		}
	}

	// Token: 0x060037F5 RID: 14325 RVA: 0x000E79C8 File Offset: 0x000E5BC8
	private void ResetSkillMove(SkillData skillData, ObjCharacter target)
	{
		this.mMoveStartPos = this.ownner.Position;
		float num;
		if (skillData.MoveAngle == -1)
		{
			num = (float)Random.Range(0, 360);
		}
		else
		{
			num = (float)skillData.MoveAngle;
		}
		if (skillData.AutoMoveFlag == 0 || target == null)
		{
			this.mMoveTargetPos = Quaternion.AngleAxis(num, Vector3.up) * this.ownner.CacheTransform.forward * skillData.MoveDistanceMeter + this.ownner.Position;
		}
		else
		{
			if (!(target != null))
			{
				return;
			}
			this.mMoveTargetPos = target.Position - (target.Position - this.ownner.Position).normalized * (target.ModelRadius + this.ownner.ModelRadius + 0.5f);
		}
		NavMeshHit navMeshHit;
		if (NavMesh.Raycast(this.mMoveStartPos, this.mMoveTargetPos, ref navMeshHit, this.ownner.NavMeshAgent.walkableMask))
		{
			this.mMoveTime = skillData.MoveTimeSecond * VectorXZ.Distance(navMeshHit.position, this.mMoveStartPos) / skillData.MoveDistanceMeter;
			this.mMoveTargetPos = navMeshHit.position;
		}
		else if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.mMoveStartPos, (this.mMoveTargetPos - this.mMoveStartPos).normalized, ref raycastHit, Vector3.Distance(this.mMoveTargetPos, this.mMoveStartPos), -2147483648))
			{
				this.mMoveTime = skillData.MoveTimeSecond * VectorXZ.Distance(raycastHit.point, this.mMoveStartPos) / skillData.MoveDistanceMeter;
				this.mMoveTargetPos = raycastHit.point;
			}
			else
			{
				this.mMoveTime = skillData.MoveTimeSecond;
			}
		}
		else
		{
			this.mMoveTime = skillData.MoveTimeSecond;
		}
		this.mMoveTargetPos = new Vector3(this.mMoveTargetPos.x, SceneManager.GetHitHeight(this.mMoveTargetPos), this.mMoveTargetPos.z);
		if (this.mMoveTime <= 0.1f)
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
	}

	// Token: 0x060037F6 RID: 14326 RVA: 0x000E7C5C File Offset: 0x000E5E5C
	private void Start()
	{
	}

	// Token: 0x060037F7 RID: 14327 RVA: 0x000E7C60 File Offset: 0x000E5E60
	private void Update()
	{
	}

	// Token: 0x060037F8 RID: 14328 RVA: 0x000E7C64 File Offset: 0x000E5E64
	public void UpdateSkillMotion()
	{
		if (this.NeedMoveFlag)
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

	// Token: 0x060037F9 RID: 14329 RVA: 0x000E7CDC File Offset: 0x000E5EDC
	public void BreakCurSkillMotion()
	{
		this.NeedMoveFlag = false;
		this.movePercent = 0f;
	}

	// Token: 0x040024EC RID: 9452
	private Vector3 mMoveStartPos;

	// Token: 0x040024ED RID: 9453
	private Vector3 mMoveTargetPos;

	// Token: 0x040024EE RID: 9454
	private float mMoveTime;

	// Token: 0x040024EF RID: 9455
	private float mMoveStartTime;

	// Token: 0x040024F0 RID: 9456
	private bool mNeedMoveFlag;

	// Token: 0x040024F1 RID: 9457
	private float movePercent;

	// Token: 0x040024F2 RID: 9458
	private ObjCharacter ownner;

	// Token: 0x040024F3 RID: 9459
	public SkillMotion.Finish MoveArrive;

	// Token: 0x040024F4 RID: 9460
	private vp_Timer.Handle handle = new vp_Timer.Handle();

	// Token: 0x02000AE4 RID: 2788
	// (Invoke) Token: 0x06005019 RID: 20505
	public delegate void Finish();
}
