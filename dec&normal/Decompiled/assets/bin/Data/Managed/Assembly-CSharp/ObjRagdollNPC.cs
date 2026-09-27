using System;
using SprotoType;
using UnityEngine;

// Token: 0x0200082D RID: 2093
public class ObjRagdollNPC : ObjNPC
{
	// Token: 0x17000EC3 RID: 3779
	// (get) Token: 0x060034EE RID: 13550 RVA: 0x000D54E8 File Offset: 0x000D36E8
	public bool IsRagdollEnable
	{
		get
		{
			return this.RagdollFlag;
		}
	}

	// Token: 0x17000EC4 RID: 3780
	// (get) Token: 0x060034EF RID: 13551 RVA: 0x000D54F0 File Offset: 0x000D36F0
	// (set) Token: 0x060034F0 RID: 13552 RVA: 0x000D54F8 File Offset: 0x000D36F8
	public bool IsEnterTrigger
	{
		get
		{
			return this.EnterTriggerFlag;
		}
		set
		{
			this.EnterTriggerFlag = value;
		}
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x000D5504 File Offset: 0x000D3704
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x060034F2 RID: 13554 RVA: 0x000D550C File Offset: 0x000D370C
	public override void ResetNpc(ObjInitNpcData initData)
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		base.Reset();
		this.ServerId = initData.mServerID;
		base.Position = initData.mPos;
		this.mTransform.forward = initData.mDir;
		this.mNpcData = initData.npcInfoData;
		base.BornPos = initData.mPos;
		this.AttributeData.Camp = (GameDefine.CAMP_TYPE)initData.npcInfoData.Group;
		this.mNPCFunctionType = (GameDefine.NPC_FUNCTION_TYPE)initData.npcInfoData.FunctionType;
		this.mNPCType = (GameDefine.NPC_TYPE)initData.npcInfoData.Type;
		this.AttributeData.HP = initData.HP;
		this.AttributeData.MaxHP = initData.MaxHP;
		this.AttributeData.Name = initData.npcInfoData.Name;
		this.AttributeData.CurATK = (float)initData.ATK;
		this.AttributeData.CurDEF = (float)initData.DEF;
		this.AttributeData.CurEXD = (float)initData.EXD / 10000f;
		this.AttributeData.CurEXR = (float)initData.EXR / 10000f;
		this.AttributeData.CurHIT = (float)initData.HIT;
		this.AttributeData.CurDGE = (float)initData.EVA;
		this.AttributeData.CurCRI = (float)initData.CRI;
		this.AttributeData.CurRES = (float)initData.RES;
		this.AttributeData.CurCRD = (float)initData.CRD / 10000f;
		this.AttributeData.CurCRR = (float)initData.CRR / 10000f;
		this.AttributeData.CurDEFA = initData.DEFA;
		this.AttributeData.CurDGEA = initData.DGEA;
		this.AttributeData.CurRESA = initData.RESA;
		this.AttributeData.CurHITA = initData.HITA;
		this.AttributeData.CurCRIA = initData.CRIA;
		this.AttributeData.CurAntiKnockDown = (float)initData.AntiKnockDown / 10000f;
		this.AttributeData.CurAntiStun = (float)initData.AntiStun / 10000f;
		this.AttributeData.CurSpeed = initData.npcInfoData.MoveSpeedMeter;
		this.AttributeData.WalkSpeed = initData.npcInfoData.WalkSpeedMeter;
		this.mDefaultDialogID = initData.npcInfoData.TalkGroup;
		base.AddDialogMission();
		base.InitNavMeshAgent();
		base.InitNPCHeadInfo();
		base.InitSkill(this.mNpcData.SkillList);
		if (this.mTransform.childCount > 0)
		{
			Transform child = this.mTransform.GetChild(0);
			child.localScale = Vector3.one * initData.npcInfoData.ModelScale;
		}
		CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
		if (component != null)
		{
			component.height = base.CurrentCharacterModelData.ModelHeight;
			component.center = Vector3.up * base.CurrentCharacterModelData.ModelHeight / 2f;
			component.radius = base.CurrentCharacterModelData.ModelRadius;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSingleCopyScene() || SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsLowPhoneManager())
		{
			if (!base.IsMissionNpc() && this.AttributeData.Camp != GameDefine.CAMP_TYPE.FUNCTION_NPC)
			{
				if (this.mAILogic == null)
				{
					this.mAILogic = base.gameObject.AddComponent<AILogic>();
				}
				if (this.mAILogic != null)
				{
					this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, initData.PathID);
				}
			}
			else
			{
				if (this.mAILogic != null)
				{
					Object.Destroy(this.mAILogic);
					this.mAILogic = null;
				}
				if (base.IsMissionNpc())
				{
					this.mNavMeshAgent.enabled = false;
					NavMeshHit navMeshHit;
					NavMesh.SamplePosition(initData.mPos, ref navMeshHit, 1f, -1);
					base.Position = navMeshHit.position;
				}
				else
				{
					this.mNavMeshAgent.enabled = true;
				}
			}
		}
		else if (this.mNpcData.AI.Equals("FollowAI"))
		{
			if (this.mAILogic == null)
			{
				this.mAILogic = base.gameObject.AddComponent<AILogic>();
			}
			if (this.mAILogic != null)
			{
				this.mAILogic.ResetAI(this.mNpcData.AI, this.mNpcData.AIID, initData.PathID);
			}
		}
		else
		{
			if (this.mAILogic != null)
			{
				Object.Destroy(this.mAILogic);
				this.mAILogic = null;
			}
			if (base.IsMissionNpc())
			{
				this.mNavMeshAgent.enabled = false;
				NavMeshHit navMeshHit2;
				NavMesh.SamplePosition(initData.mPos, ref navMeshHit2, 1f, -1);
				base.Position = navMeshHit2.position;
			}
			else
			{
				this.mNavMeshAgent.enabled = true;
			}
		}
		this.mIsNormalNpc = true;
		base.ShowMesh();
		this.RagdollFlag = false;
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x000D5A34 File Offset: 0x000D3C34
	public void RecycleSelf()
	{
		this.recycleHandle.Cancel();
		this.mAnimationLogic.EnableAnimationLogic();
		this.RagdollFlag = false;
		if (this.BodyCollider != null)
		{
			for (int i = 0; i < this.BodyCollider.Length; i++)
			{
				this.BodyCollider[i].enabled = false;
				this.BodyRigidbody[i].isKinematic = true;
				this.BodyRigidbody[i].detectCollisions = false;
				this.BodyPart[i].localPosition = this.BodyPos[i];
				this.BodyPart[i].localRotation = this.BodyRotation[i];
			}
		}
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		if (this.mAnimationLogic.AnimaObj != null)
		{
			if (this.mAnimationLogic.AnimaObj["idle"] == null)
			{
				this.mAnimationLogic.LoadAnim(DataManager.GetActionDataByName("idle"));
			}
			this.mAnimationLogic.AnimaObj.Play("idle");
			this.mAnimationLogic.AnimaObj["idle"].time = 0f;
			this.mAnimationLogic.AnimaObj.Sample();
		}
		Singleton<ObjManager>.Instance.RecycleRagdollNPC(this);
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x000D5B90 File Offset: 0x000D3D90
	public override void OnDie()
	{
		base.OnDie();
		this.recycleHandle.Cancel();
		vp_Timer.In(2.5f, delegate()
		{
			this.RecycleSelf();
		}, this.recycleHandle);
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x000D5BC0 File Offset: 0x000D3DC0
	private void OnTriggerEnter(Collider other)
	{
		if (base.IsMissionNpc())
		{
			return;
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerCar"))
		{
			this.IsEnterTrigger = true;
			if (NGUITools.GetRoot(other.gameObject).rigidbody.velocity.sqrMagnitude > 4f)
			{
				ObjPlayerCar objPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
				if (objPlayerCar != null)
				{
					this.mHitManSoundVolume = 0.2f + 0.8f * Mathf.Clamp01(objPlayerCar.CurSpeed / 60f);
					SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.mHitManSoundId, this.mHitManSoundVolume, null);
				}
				else
				{
					objPlayerCar = Singleton<ObjManager>.Instance.MainPlayer.CurPlayerCar;
				}
				int damageByCar = CharacterAttributeData.GetDamageByCar(objPlayerCar, Singleton<ObjManager>.Instance.MainPlayer, this);
				if (damageByCar > 0)
				{
					this.ChangeHPVal(this.AttributeData.HP - (long)damageByCar);
				}
				this.EnableRagdoll();
			}
		}
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x000D5CBC File Offset: 0x000D3EBC
	public void EnableRagdoll()
	{
		if (this.RagdollFlag)
		{
			return;
		}
		this.mAnimationLogic.DisableAnimationLogic();
		base.DisableNavMeshAgent();
		base.DisactiveTargetArriveFinish();
		if (base.SimpleShadow != null)
		{
			UnityVersionUtil.SetActiveRecursive(base.SimpleShadow, false);
		}
		this.RagdollFlag = true;
		for (int i = 0; i < this.BodyCollider.Length; i++)
		{
			this.BodyRigidbody[i].isKinematic = false;
			this.BodyCollider[i].enabled = true;
			this.BodyRigidbody[i].detectCollisions = true;
		}
		if (this.mHeadInfoLogic != null)
		{
			(this.mHeadInfoLogic as NPCHeadInfoLogic).HideHpLine();
		}
		base.IsDie = true;
		if (this.AttributeData.HP <= 0L && GameManager.OnLineState && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsCarScene())
		{
			if (GameManager.IsSupportCurDataVersion145())
			{
				impact_npc.request rpcReq = new impact_npc.request();
				NetLogic.GetInstance().Send<Protocol.impact_npc>(rpcReq, null);
			}
			this.recycleHandle.Cancel();
			vp_Timer.In(3f, delegate()
			{
				this.RecycleSelf();
			}, this.recycleHandle);
			return;
		}
	}

	// Token: 0x060034F7 RID: 13559 RVA: 0x000D5DF0 File Offset: 0x000D3FF0
	private void OnTriggerExit(Collider other)
	{
		if (base.IsMissionNpc())
		{
			return;
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerCar"))
		{
			this.IsEnterTrigger = false;
			vp_Timer.In(3f, delegate()
			{
				this.ResetGetUpSelf();
			}, null);
		}
	}

	// Token: 0x060034F8 RID: 13560 RVA: 0x000D5E44 File Offset: 0x000D4044
	public void ResetGetUpSelf()
	{
		if (!this.RagdollFlag || this.IsEnterTrigger || !base.IsDie || this.AttributeData.HP <= 0L)
		{
			return;
		}
		this.mAnimationLogic.EnableAnimationLogic();
		if (base.SimpleShadow != null)
		{
			UnityVersionUtil.SetActiveRecursive(base.SimpleShadow, true);
		}
		this.RagdollFlag = false;
		base.IsDie = false;
		if (this.BodyCollider != null)
		{
			if (this.BodyCollider.Length > 0)
			{
				base.transform.position = this.BodyCollider[0].transform.position;
			}
			for (int i = 0; i < this.BodyCollider.Length; i++)
			{
				this.BodyCollider[i].enabled = false;
				this.BodyRigidbody[i].isKinematic = true;
				this.BodyRigidbody[i].detectCollisions = false;
				this.BodyPart[i].localPosition = this.BodyPos[i];
				this.BodyPart[i].localRotation = this.BodyRotation[i];
			}
		}
		base.EnableNavMeshAgent();
		base.WalkMoveTo(this.preTargetPoint, 1f, new ObjCharacter.TargetArriveFinsh(this.MoveToNextCityPoint));
		if (this.mHeadInfoLogic != null)
		{
			(this.mHeadInfoLogic as NPCHeadInfoLogic).ShowHpLine();
		}
	}

	// Token: 0x060034F9 RID: 13561 RVA: 0x000D5FB4 File Offset: 0x000D41B4
	public override void ChangeHPVal(long newHP)
	{
		if (this.IsRagdollEnable)
		{
			return;
		}
		if (!base.IsDie)
		{
			if (newHP > this.AttributeData.HP && Time.time - this.mReceiveBiggerHpTimeCount < this.mReceiveBiggerHpTime)
			{
				return;
			}
			if (this.AttributeData.HP != newHP)
			{
				this.AttributeData.HP = newHP;
				this.UpdateHeadInfo();
			}
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
				if (!base.IsCityCaptureNpc())
				{
					float delay = 4f;
					if (this.dieDelayHandle == null)
					{
						this.dieDelayHandle = new vp_Timer.Handle();
					}
					vp_Timer.In(delay, delegate()
					{
						base.DelayRecycle();
					}, this.dieDelayHandle);
				}
			}
		}
		else if (newHP <= 0L && !base.IsCityCaptureNpc())
		{
			float delay2 = 10f;
			if (this.dieDelayHandle == null)
			{
				this.dieDelayHandle = new vp_Timer.Handle();
			}
			vp_Timer.In(delay2, delegate()
			{
				base.DelayRecycle();
			}, this.dieDelayHandle);
		}
	}

	// Token: 0x060034FA RID: 13562 RVA: 0x000D60C8 File Offset: 0x000D42C8
	public new virtual void UpdateHeadInfo()
	{
		if (this.mHeadInfoLogic != null)
		{
			this.mHeadInfoLogic.SetHpVal((float)this.AttributeData.HP / (float)this.AttributeData.MaxHP);
		}
	}

	// Token: 0x060034FB RID: 13563 RVA: 0x000D610C File Offset: 0x000D430C
	public static void InitRagdollObj(Transform root, ObjRagdollNPC npc)
	{
		if (!SingletonUnity<NPCRagdollData>.Exists)
		{
			Debug.Log("No Ragdoll Data");
			return;
		}
		RagdollInfoData[] ragdollData = SingletonUnity<NPCRagdollData>.Instance.RagdollData;
		PhysicMaterial ragdollMat = SingletonUnity<NPCRagdollData>.Instance.RagdollMat;
		npc.BodyRigidbody = new Rigidbody[ragdollData.Length];
		npc.BodyCollider = new Collider[ragdollData.Length];
		npc.BodyPos = new Vector3[ragdollData.Length];
		npc.BodyRotation = new Quaternion[ragdollData.Length];
		npc.BodyPart = new Transform[ragdollData.Length];
		for (int i = 0; i < ragdollData.Length; i++)
		{
			Transform transform = root.FindChild(ragdollData[i].Path);
			if (transform != null)
			{
				GameObject gameObject = transform.gameObject;
				npc.BodyPos[i] = transform.localPosition;
				npc.BodyRotation[i] = transform.localRotation;
				npc.BodyPart[i] = transform;
				Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
				rigidbody.mass = ragdollData[i].Mass;
				rigidbody.drag = ragdollData[i].Drag;
				rigidbody.angularDrag = ragdollData[i].AngularDrag;
				rigidbody.useGravity = ragdollData[i].UseGravity;
				rigidbody.isKinematic = true;
				rigidbody.detectCollisions = false;
				npc.BodyRigidbody[i] = rigidbody;
				if (ragdollData[i].ColliderType == 0)
				{
					CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
					capsuleCollider.center = ragdollData[i].ColliderCenter;
					capsuleCollider.radius = ragdollData[i].ColliderRadius;
					capsuleCollider.height = ragdollData[i].Height;
					capsuleCollider.direction = ragdollData[i].Direction;
					capsuleCollider.material = ragdollMat;
					capsuleCollider.enabled = false;
					npc.BodyCollider[i] = capsuleCollider;
				}
				else if (ragdollData[i].ColliderType == 1)
				{
					BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
					boxCollider.center = ragdollData[i].ColliderCenter;
					boxCollider.size = ragdollData[i].size;
					boxCollider.material = ragdollMat;
					boxCollider.enabled = false;
					npc.BodyCollider[i] = boxCollider;
				}
				else if (ragdollData[i].ColliderType == 2)
				{
					SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
					sphereCollider.center = ragdollData[i].ColliderCenter;
					sphereCollider.radius = ragdollData[i].ColliderRadius;
					sphereCollider.material = ragdollMat;
					sphereCollider.enabled = false;
					npc.BodyCollider[i] = sphereCollider;
				}
				if (ragdollData[i].HasJoint)
				{
					CharacterJoint characterJoint = gameObject.AddComponent<CharacterJoint>();
					characterJoint.connectedBody = root.FindChild(ragdollData[i].ConnectBodyPath).gameObject.rigidbody;
					characterJoint.anchor = ragdollData[i].Anchor;
					characterJoint.axis = ragdollData[i].Axis;
					characterJoint.swingAxis = ragdollData[i].SwingAxis;
					SoftJointLimit lowTwistLimit = default(SoftJointLimit);
					lowTwistLimit.limit = ragdollData[i].LowTwistLimit.Limit;
					lowTwistLimit.bounciness = ragdollData[i].LowTwistLimit.Bounciness;
					lowTwistLimit.spring = ragdollData[i].LowTwistLimit.Spring;
					lowTwistLimit.damper = ragdollData[i].LowTwistLimit.Damper;
					characterJoint.lowTwistLimit = lowTwistLimit;
					SoftJointLimit highTwistLimit = default(SoftJointLimit);
					highTwistLimit.limit = ragdollData[i].HighTwistLimit.Limit;
					highTwistLimit.bounciness = ragdollData[i].HighTwistLimit.Bounciness;
					highTwistLimit.spring = ragdollData[i].HighTwistLimit.Spring;
					highTwistLimit.damper = ragdollData[i].HighTwistLimit.Damper;
					characterJoint.highTwistLimit = highTwistLimit;
					SoftJointLimit swing1Limit = default(SoftJointLimit);
					swing1Limit.limit = ragdollData[i].Swing1Limit.Limit;
					swing1Limit.bounciness = ragdollData[i].Swing1Limit.Bounciness;
					swing1Limit.spring = ragdollData[i].Swing1Limit.Spring;
					swing1Limit.damper = ragdollData[i].Swing1Limit.Damper;
					characterJoint.swing1Limit = swing1Limit;
					SoftJointLimit swing2Limit = default(SoftJointLimit);
					swing2Limit.limit = ragdollData[i].Swing2Limit.Limit;
					swing2Limit.bounciness = ragdollData[i].Swing2Limit.Bounciness;
					swing2Limit.spring = ragdollData[i].Swing2Limit.Spring;
					swing2Limit.damper = ragdollData[i].Swing2Limit.Damper;
					characterJoint.swing2Limit = swing2Limit;
				}
			}
		}
	}

	// Token: 0x060034FC RID: 13564 RVA: 0x000D6590 File Offset: 0x000D4790
	public void ResetCityMove(CitySimController cityCtl, CityPathPointData targetPoint)
	{
		this.mIsNormalNpc = false;
		this.mCityCtl = cityCtl;
		this.TargetPathPoint = targetPoint;
		int num;
		if (CitySimController.IsForward(base.Position - targetPoint.PointPos, targetPoint.PointRight))
		{
			num = 1;
		}
		else
		{
			num = -1;
		}
		Vector3 pos = targetPoint.PointPos + (float)num * targetPoint.PointRight * Random.Range(targetPoint.MinWalkDis, targetPoint.MaxWalkDis);
		bool flag = SceneManager.IsInNavmeshArea(pos);
		int num2 = 0;
		while (!flag)
		{
			num2++;
			pos = targetPoint.PointPos + (float)num * targetPoint.PointRight * Random.Range(targetPoint.MinWalkDis - (float)num2, targetPoint.MaxWalkDis - (float)num2);
			flag = SceneManager.IsInNavmeshArea(pos);
		}
		this.preTargetPoint = pos;
		base.WalkMoveTo(pos, 5f, new ObjCharacter.TargetArriveFinsh(this.MoveToNextCityPoint));
	}

	// Token: 0x060034FD RID: 13565 RVA: 0x000D6684 File Offset: 0x000D4884
	public void ContinueMove()
	{
		if (this.mIsNormalNpc)
		{
			return;
		}
		base.WalkMoveTo(this.preTargetPoint, 1f, new ObjCharacter.TargetArriveFinsh(this.MoveToNextCityPoint));
	}

	// Token: 0x060034FE RID: 13566 RVA: 0x000D66B0 File Offset: 0x000D48B0
	public void MoveToNextCityPoint(ObjCharacter obj)
	{
		CityPathPointData targetPathPoint = this.TargetPathPoint;
		int num = -1;
		int num2;
		if (targetPathPoint.IsCross)
		{
			num2 = Random.Range(0, 4);
		}
		else
		{
			num2 = Random.Range(0, 2);
		}
		if (num2 == 0)
		{
			if (targetPathPoint.LinkPointIndex[0] != -1)
			{
				num = targetPathPoint.LinkPointIndex[0];
			}
			else
			{
				num = targetPathPoint.LinkPointIndex[1];
			}
			if (!this.mCityCtl.PointDataList[num].IsWalkable)
			{
				if (targetPathPoint.LinkPointIndex[2] != -1)
				{
					num = targetPathPoint.LinkPointIndex[2];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[3];
					if (num == -1)
					{
						num = targetPathPoint.LinkPointIndex[0];
					}
				}
			}
		}
		else if (num2 == 1)
		{
			if (targetPathPoint.LinkPointIndex[1] != -1)
			{
				num = targetPathPoint.LinkPointIndex[1];
			}
			else
			{
				num = targetPathPoint.LinkPointIndex[0];
			}
			if (!this.mCityCtl.PointDataList[num].IsWalkable)
			{
				if (targetPathPoint.LinkPointIndex[2] != -1)
				{
					num = targetPathPoint.LinkPointIndex[2];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[3];
					if (num == -1)
					{
						num = targetPathPoint.LinkPointIndex[1];
					}
				}
			}
		}
		else if (num2 == 2)
		{
			if (targetPathPoint.LinkPointIndex[2] != -1)
			{
				num = targetPathPoint.LinkPointIndex[2];
			}
			else if (targetPathPoint.LinkPointIndex[3] != -1)
			{
				num = targetPathPoint.LinkPointIndex[3];
			}
			else
			{
				num = targetPathPoint.LinkPointIndex[0];
			}
			if (!this.mCityCtl.PointDataList[num].IsWalkable)
			{
				if (targetPathPoint.LinkPointIndex[1] != -1)
				{
					num = targetPathPoint.LinkPointIndex[1];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[0];
				}
			}
		}
		else if (num2 == 3)
		{
			if (targetPathPoint.LinkPointIndex[3] != -1)
			{
				num = targetPathPoint.LinkPointIndex[3];
			}
			else if (targetPathPoint.LinkPointIndex[2] != -1)
			{
				num = targetPathPoint.LinkPointIndex[2];
			}
			else
			{
				num = targetPathPoint.LinkPointIndex[0];
			}
			if (!this.mCityCtl.PointDataList[num].IsWalkable)
			{
				if (targetPathPoint.LinkPointIndex[1] != -1)
				{
					num = targetPathPoint.LinkPointIndex[1];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[0];
				}
			}
		}
		if (this.PreTargetPoint != null && this.PreTargetPoint.IsCross && targetPathPoint.IsCross)
		{
			num = targetPathPoint.LinkPointIndex[0];
		}
		if (this.TargetPathPoint.IsCross && this.mCityCtl.PointDataList[num].IsCross && CitySimController.CurRoadState != ROAD_STATE.PERSON_PASS1 && CitySimController.CurRoadState != ROAD_STATE.PERSON_PASS2)
		{
			return;
		}
		this.PreTargetPoint = this.TargetPathPoint;
		this.TargetPathPoint = this.mCityCtl.PointDataList[num];
		Vector3 pos;
		if (CitySimController.IsForward(base.Position - this.TargetPathPoint.PointPos, this.TargetPathPoint.PointRight))
		{
			pos = this.TargetPathPoint.PointPos + this.TargetPathPoint.PointRight * Random.Range(this.TargetPathPoint.MinWalkDis, this.TargetPathPoint.MaxWalkDis);
			bool flag = SceneManager.IsInNavmeshArea(pos);
			int num3 = 0;
			while (!flag)
			{
				num3++;
				pos = this.TargetPathPoint.PointPos + this.TargetPathPoint.PointRight * Random.Range(this.TargetPathPoint.MinWalkDis - (float)num3, this.TargetPathPoint.MaxWalkDis - (float)num3);
				flag = SceneManager.IsInNavmeshArea(pos);
			}
		}
		else
		{
			pos = this.TargetPathPoint.PointPos - this.TargetPathPoint.PointRight * Random.Range(this.TargetPathPoint.MinWalkDis, this.TargetPathPoint.MaxWalkDis);
			bool flag2 = SceneManager.IsInNavmeshArea(pos);
			int num4 = 0;
			while (!flag2)
			{
				num4++;
				pos = this.TargetPathPoint.PointPos + this.TargetPathPoint.PointRight * Random.Range(this.TargetPathPoint.MinWalkDis - (float)num4, this.TargetPathPoint.MaxWalkDis - (float)num4);
				flag2 = SceneManager.IsInNavmeshArea(pos);
			}
		}
		this.preTargetPoint = pos;
		base.WalkMoveTo(pos, 5f, new ObjCharacter.TargetArriveFinsh(this.MoveToNextCityPoint));
	}

	// Token: 0x060034FF RID: 13567 RVA: 0x000D6B34 File Offset: 0x000D4D34
	public override void StopMove()
	{
		if (base.IsDie)
		{
			return;
		}
		base.StopMove();
	}

	// Token: 0x04002297 RID: 8855
	public Collider[] BodyCollider;

	// Token: 0x04002298 RID: 8856
	public Rigidbody[] BodyRigidbody;

	// Token: 0x04002299 RID: 8857
	public Vector3[] BodyPos;

	// Token: 0x0400229A RID: 8858
	public Quaternion[] BodyRotation;

	// Token: 0x0400229B RID: 8859
	public Transform[] BodyPart;

	// Token: 0x0400229C RID: 8860
	public CityPathPointData TargetPathPoint;

	// Token: 0x0400229D RID: 8861
	private CityPathPointData PreTargetPoint;

	// Token: 0x0400229E RID: 8862
	private bool RagdollFlag;

	// Token: 0x0400229F RID: 8863
	private bool EnterTriggerFlag;

	// Token: 0x040022A0 RID: 8864
	public int CheckIndex = -1;

	// Token: 0x040022A1 RID: 8865
	public CitySimController mCityCtl;

	// Token: 0x040022A2 RID: 8866
	private bool mIsNormalNpc = true;

	// Token: 0x040022A3 RID: 8867
	public vp_Timer.Handle recycleHandle = new vp_Timer.Handle();

	// Token: 0x040022A4 RID: 8868
	private Vector3 flyDir;

	// Token: 0x040022A5 RID: 8869
	private int mHitManSoundId = 40;

	// Token: 0x040022A6 RID: 8870
	private float mHitManSoundVolume = 1f;

	// Token: 0x040022A7 RID: 8871
	private Vector3 preTargetPoint = Vector3.zero;
}
