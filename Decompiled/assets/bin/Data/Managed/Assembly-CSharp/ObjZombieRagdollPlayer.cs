using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000830 RID: 2096
public class ObjZombieRagdollPlayer : ObjZombiePlayer
{
	// Token: 0x06003537 RID: 13623 RVA: 0x000D7FF0 File Offset: 0x000D61F0
	public ObjZombieRagdollPlayer()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL;
	}

	// Token: 0x17000ECC RID: 3788
	// (get) Token: 0x06003538 RID: 13624 RVA: 0x000D8068 File Offset: 0x000D6268
	public bool IsRagdollEnable
	{
		get
		{
			return this.RagdollFlag;
		}
	}

	// Token: 0x17000ECD RID: 3789
	// (get) Token: 0x06003539 RID: 13625 RVA: 0x000D8070 File Offset: 0x000D6270
	public string ActivityId
	{
		get
		{
			return this.mActivityId;
		}
	}

	// Token: 0x17000ECE RID: 3790
	// (get) Token: 0x0600353A RID: 13626 RVA: 0x000D8078 File Offset: 0x000D6278
	public bool IsMissionNpc
	{
		get
		{
			return this.mIsMissionNpc;
		}
	}

	// Token: 0x17000ECF RID: 3791
	// (get) Token: 0x0600353B RID: 13627 RVA: 0x000D8080 File Offset: 0x000D6280
	public string NpcId
	{
		get
		{
			return this.npcId;
		}
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x000D8088 File Offset: 0x000D6288
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x0600353D RID: 13629 RVA: 0x000D8090 File Offset: 0x000D6290
	public void ResetObjZombieRagdollPlayer(ObjInitPlayerData initData)
	{
		base.ResetZombiePlayer(initData);
		this.RagdollFlag = false;
		CapsuleCollider component = base.gameObject.GetComponent<CapsuleCollider>();
		component.isTrigger = true;
		this.originalPos = initData.mPos;
		this.mAIData = DataManager.GetAIDataByID(initData.AIID);
		this.npcId = initData.NpcId;
		this.mActivityId = DataManager.GetNpcDataByID(this.npcId).TalkGroup;
		if (string.IsNullOrEmpty(this.mActivityId))
		{
			this.mIsMissionNpc = false;
			this.AttributeData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
		}
		else
		{
			this.mIsMissionNpc = true;
			this.AttributeData.Camp = GameDefine.CAMP_TYPE.FUNCTION_NPC;
		}
	}

	// Token: 0x0600353E RID: 13630 RVA: 0x000D8138 File Offset: 0x000D6338
	public void EnableRagdoll()
	{
		if (this.RagdollFlag)
		{
			return;
		}
		this.mAnimationLogic.DisableAnimationLogic();
		base.DisableNavMeshAgent();
		base.DisactiveTargetArriveFinish();
		base.DisactiveHeadInfo();
		this.RagdollFlag = true;
		for (int i = 0; i < this.BodyCollider.Length; i++)
		{
			this.BodyRigidbody[i].isKinematic = false;
			this.BodyCollider[i].enabled = true;
			this.BodyRigidbody[i].detectCollisions = true;
		}
		base.IsDie = true;
		this.recycleHandle.Cancel();
		vp_Timer.In(3f, delegate()
		{
			this.RecycleSelf();
		}, this.recycleHandle);
		this.SendServerDieAction();
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x000D81EC File Offset: 0x000D63EC
	public void RecycleSelf()
	{
		this.recycleHandle.Cancel();
		this.walkHandle.Cancel();
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		Singleton<ObjManager>.Instance.RecycleZombieRagdollPlayer(this);
	}

	// Token: 0x06003540 RID: 13632 RVA: 0x000D8228 File Offset: 0x000D6428
	public override void OnDie()
	{
		base.OnDie();
		this.recycleHandle.Cancel();
		vp_Timer.In(2.5f, delegate()
		{
			this.RecycleSelf();
		}, this.recycleHandle);
		this.SendServerDieAction();
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x000D8260 File Offset: 0x000D6460
	private void SendServerDieAction()
	{
		local_npc_die.request request = new local_npc_die.request();
		request.npcid = this.npcId;
		request.x = (long)(base.Position.x * 100f);
		request.z = (long)(base.Position.z * 100f);
		request.type = 0L;
		if (this.IsRagdollEnable && SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.IMPACT_NPC))
		{
			request.type = 3L;
		}
		NetLogic.GetInstance().Send<Protocol.local_npc_die>(request, null);
	}

	// Token: 0x06003542 RID: 13634 RVA: 0x000D82F4 File Offset: 0x000D64F4
	private void OnTriggerEnter(Collider other)
	{
		if (this.IsMissionNpc)
		{
			return;
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerCar") && NGUITools.GetRoot(other.gameObject).rigidbody.velocity.sqrMagnitude > 4f)
		{
			this.EnableRagdoll();
			ObjPlayerCar mainPlayerCar = Singleton<ObjManager>.Instance.MainPlayerCar;
			if (mainPlayerCar != null)
			{
				this.mHitManSoundVolume = 0.2f + 0.8f * Mathf.Clamp01(mainPlayerCar.CurSpeed / 60f);
				SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.mHitManSoundId, this.mHitManSoundVolume, null);
			}
		}
	}

	// Token: 0x06003543 RID: 13635 RVA: 0x000D83A8 File Offset: 0x000D65A8
	public static void InitRagdollObj(Transform root, ObjZombieRagdollPlayer npc)
	{
		GameObject gameObject = GameObject.Find("PlayerRagdollData");
		if (gameObject == null)
		{
			Debug.Log("No Player Ragdoll Data");
			return;
		}
		PlayerRagdollData component = gameObject.GetComponent<PlayerRagdollData>();
		RagdollInfoData[] ragdollData = component.RagdollData;
		PhysicMaterial ragdollMat = component.RagdollMat;
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
				GameObject gameObject2 = transform.gameObject;
				npc.BodyPos[i] = transform.localPosition;
				npc.BodyRotation[i] = transform.localRotation;
				npc.BodyPart[i] = transform;
				Rigidbody rigidbody = gameObject2.AddComponent<Rigidbody>();
				rigidbody.mass = ragdollData[i].Mass;
				rigidbody.drag = ragdollData[i].Drag;
				rigidbody.angularDrag = ragdollData[i].AngularDrag;
				rigidbody.useGravity = ragdollData[i].UseGravity;
				rigidbody.isKinematic = true;
				rigidbody.detectCollisions = false;
				npc.BodyRigidbody[i] = rigidbody;
				if (ragdollData[i].ColliderType == 0)
				{
					CapsuleCollider capsuleCollider = gameObject2.AddComponent<CapsuleCollider>();
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
					BoxCollider boxCollider = gameObject2.AddComponent<BoxCollider>();
					boxCollider.center = ragdollData[i].ColliderCenter;
					boxCollider.size = ragdollData[i].size;
					boxCollider.material = ragdollMat;
					boxCollider.enabled = false;
					npc.BodyCollider[i] = boxCollider;
				}
				else if (ragdollData[i].ColliderType == 2)
				{
					SphereCollider sphereCollider = gameObject2.AddComponent<SphereCollider>();
					sphereCollider.center = ragdollData[i].ColliderCenter;
					sphereCollider.radius = ragdollData[i].ColliderRadius;
					sphereCollider.material = ragdollMat;
					sphereCollider.enabled = false;
					npc.BodyCollider[i] = sphereCollider;
				}
				if (ragdollData[i].HasJoint)
				{
					CharacterJoint characterJoint = gameObject2.AddComponent<CharacterJoint>();
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
			else
			{
				Debug.Log(root.gameObject.name + " :: Can't find :: " + ragdollData[i].Path);
			}
		}
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x000D886C File Offset: 0x000D6A6C
	public void ResetCityMove(CitySimController cityCtl, CityPathPointData targetPoint)
	{
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

	// Token: 0x06003545 RID: 13637 RVA: 0x000D8958 File Offset: 0x000D6B58
	public void ContinueMove()
	{
		base.WalkMoveTo(this.preTargetPoint, 1f, new ObjCharacter.TargetArriveFinsh(this.MoveToNextCityPoint));
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x000D8978 File Offset: 0x000D6B78
	public void MoveToNextCityPoint(ObjCharacter obj)
	{
		if (this.TargetPathPoint == null)
		{
			return;
		}
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
			else
			{
				num = targetPathPoint.LinkPointIndex[3];
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
			else
			{
				num = targetPathPoint.LinkPointIndex[2];
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
		if (this.TargetPathPoint.IsCross && this.mCityCtl.PointDataList[num].IsCross && CitySimController.CurRoadState != ROAD_STATE.PERSON_PASS1 && CitySimController.CurRoadState != ROAD_STATE.PERSON_PASS2)
		{
			return;
		}
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

	// Token: 0x06003547 RID: 13639 RVA: 0x000D8D94 File Offset: 0x000D6F94
	public override void StopMove()
	{
		if (base.IsDie)
		{
			return;
		}
		base.StopMove();
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x000D8DA8 File Offset: 0x000D6FA8
	private void Update()
	{
		base.UpdateComponent();
		base.UpdateMove();
		base.UpdateSkillCD();
		base.UpdateHoldTime();
		base.UpdateComboTime();
		base.SkillLogic.UpdateSkill();
		this.ZombieAICheck();
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x000D8DE4 File Offset: 0x000D6FE4
	public override void OnBeaton()
	{
		base.OnBeaton();
		this.lastHitTime = Time.time;
		this.EnterAttackChase();
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x000D8E00 File Offset: 0x000D7000
	private void ZombieAICheck()
	{
		if (this.IsMissionNpc)
		{
			return;
		}
		if (base.IsDie)
		{
			return;
		}
		if (this.mIsAutoFight)
		{
			if (Time.time - this.lastHitTime > 3f)
			{
				this.LeaveAttackChase();
			}
			else
			{
				base.AutoFight();
			}
		}
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x000D8E58 File Offset: 0x000D7058
	private void EnterAttackChase()
	{
		if (this.IsMissionNpc)
		{
			return;
		}
		this.mIsAutoFight = true;
		this.walkHandle.Cancel();
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x000D8E78 File Offset: 0x000D7078
	private void LeaveAttackChase()
	{
		if (!base.SkillLogic.IsUsingSkill)
		{
			this.mIsAutoFight = false;
			this.ReturnOriginalPos();
		}
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x000D8E98 File Offset: 0x000D7098
	private void ReturnOriginalPos()
	{
		base.WalkMoveTo(this.originalPos, 1f, new ObjCharacter.TargetArriveFinsh(this.PatrolMove));
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x000D8EB8 File Offset: 0x000D70B8
	public void PatrolMove(ObjCharacter cha)
	{
		Vector3 mTarget = Vector3.zero;
		float num = Random.Range(this.mAIData.PatrolDistanceMeter * -1f, this.mAIData.PatrolDistanceMeter);
		float num2 = Random.Range(this.mAIData.PatrolDistanceMeter * -1f, this.mAIData.PatrolDistanceMeter);
		Vector3 vector = this.originalPos + new Vector3(num, 0f, num2);
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(vector, ref navMeshHit, 0.1f, 15))
		{
			mTarget = vector;
		}
		else
		{
			NavMeshHit navMeshHit2 = default(NavMeshHit);
			NavMesh.Raycast(this.originalPos, vector, ref navMeshHit2, base.NavMeshAgent.walkableMask);
			vector = navMeshHit2.position;
			mTarget = vector;
		}
		vp_Timer.In(Random.Range(0.5f, 3f), delegate()
		{
			this.WalkMoveTo(mTarget, 1f, new ObjCharacter.TargetArriveFinsh(this.PatrolMove));
		}, this.walkHandle);
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x000D8FBC File Offset: 0x000D71BC
	public override void ChangeHPEffect(long newHP, GameDefine.DAMAGEBOARD_TYPE type)
	{
		if (!base.IsDie)
		{
			this.OnBeaton();
			long num = this.AttributeData.HP - newHP;
			if (num > 0L)
			{
				base.UpdateDamgeBoard(type, num);
			}
			else
			{
				base.UpdateDamgeBoard(type, num);
			}
			if (newHP < 0L)
			{
				newHP = 0L;
			}
			this.AttributeData.HP = newHP;
			this.UpdateHeadInfo();
			if (this.AttributeData.HP <= 0L)
			{
				this.OnDie();
			}
			this.mReceiveBiggerHpTimeCount = Time.time;
		}
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x000D9048 File Offset: 0x000D7248
	public void ShowAcitvityDialog()
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OptionDialogUI, new UIManager.OnOpenUIDelegate(this.OnShowOptionDialog), null);
		}
		base.FaceToPub(Singleton<ObjManager>.Instance.MainPlayer.Position);
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x000D9098 File Offset: 0x000D7298
	private void OnShowOptionDialog(bool isSuccess, object param)
	{
		if (!isSuccess)
		{
			return;
		}
		if (SingletonUnity<OptionDialogUILogic>.Exists)
		{
			SingletonUnity<OptionDialogUILogic>.Instance.ResetOptionDialog(base.PartObjId[0], base.PartObjId[1], base.PartObjId[2], base.PartObjId[3], StrDictionary.GetDictionaryString("#{102098}", new object[0]), "Yes", "No", this.mActivityId, delegate(string val)
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (playerData.IsFinishDownload)
				{
					DominData dominDataByID = DataManager.GetDominDataByID(val);
					if (dominDataByID.IsOpen == 0)
					{
						NoticeLogic.AddNotifyData("#{103019}", true, false);
					}
					else if (playerData.Level < dominDataByID.LevelMin)
					{
						NoticeLogic.AddNotifyData("#{103009}", true, false);
					}
					else
					{
						enter_domin_pk_scene.request request = new enter_domin_pk_scene.request();
						request.id = val;
						NetLogic.GetInstance().Send<Protocol.enter_domin_pk_scene>(request, null);
					}
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
				}
			}, false);
		}
	}

	// Token: 0x040022B4 RID: 8884
	public Collider[] BodyCollider;

	// Token: 0x040022B5 RID: 8885
	public Rigidbody[] BodyRigidbody;

	// Token: 0x040022B6 RID: 8886
	public Vector3[] BodyPos;

	// Token: 0x040022B7 RID: 8887
	public Quaternion[] BodyRotation;

	// Token: 0x040022B8 RID: 8888
	public Transform[] BodyPart;

	// Token: 0x040022B9 RID: 8889
	public CityPathPointData TargetPathPoint;

	// Token: 0x040022BA RID: 8890
	public GameObject FlagObj;

	// Token: 0x040022BB RID: 8891
	private bool RagdollFlag;

	// Token: 0x040022BC RID: 8892
	public int CheckIndex = -1;

	// Token: 0x040022BD RID: 8893
	public CitySimController mCityCtl;

	// Token: 0x040022BE RID: 8894
	private string mActivityId = string.Empty;

	// Token: 0x040022BF RID: 8895
	private bool mIsMissionNpc;

	// Token: 0x040022C0 RID: 8896
	private AIData mAIData;

	// Token: 0x040022C1 RID: 8897
	private string npcId = string.Empty;

	// Token: 0x040022C2 RID: 8898
	private Vector3 originalPos = Vector3.zero;

	// Token: 0x040022C3 RID: 8899
	public vp_Timer.Handle recycleHandle = new vp_Timer.Handle();

	// Token: 0x040022C4 RID: 8900
	private Vector3 flyDir;

	// Token: 0x040022C5 RID: 8901
	private int mHitManSoundId = 40;

	// Token: 0x040022C6 RID: 8902
	private float mHitManSoundVolume = 1f;

	// Token: 0x040022C7 RID: 8903
	private Vector3 preTargetPoint = Vector3.zero;

	// Token: 0x040022C8 RID: 8904
	private float lastHitTime;

	// Token: 0x040022C9 RID: 8905
	private vp_Timer.Handle walkHandle = new vp_Timer.Handle();
}
