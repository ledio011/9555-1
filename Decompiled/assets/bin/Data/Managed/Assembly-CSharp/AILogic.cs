using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class AILogic : MonoBehaviour
{
	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0003186C File Offset: 0x0002FA6C
	// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00031874 File Offset: 0x0002FA74
	public ObjNPC Ownner
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

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00031880 File Offset: 0x0002FA80
	// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00031888 File Offset: 0x0002FA88
	public AISTATE AttackState
	{
		get
		{
			return this.mAttackState;
		}
		set
		{
			this.mAttackState = value;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00031894 File Offset: 0x0002FA94
	// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0003189C File Offset: 0x0002FA9C
	public float DelayMoveTimeCount
	{
		get
		{
			return this.mDelayMoveTimeCount;
		}
		set
		{
			this.mDelayMoveTimeCount = value;
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x060006E7 RID: 1767 RVA: 0x000318A8 File Offset: 0x0002FAA8
	// (set) Token: 0x060006E8 RID: 1768 RVA: 0x000318B0 File Offset: 0x0002FAB0
	public bool OutOffRangeFlag
	{
		get
		{
			return this.mOutOffRangeFlag;
		}
		set
		{
			this.mOutOffRangeFlag = value;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000318BC File Offset: 0x0002FABC
	// (set) Token: 0x060006EA RID: 1770 RVA: 0x000318C4 File Offset: 0x0002FAC4
	public bool ReturnBackFlag
	{
		get
		{
			return this.mReturnBackFlag;
		}
		set
		{
			this.mReturnBackFlag = value;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060006EB RID: 1771 RVA: 0x000318D0 File Offset: 0x0002FAD0
	// (set) Token: 0x060006EC RID: 1772 RVA: 0x000318D8 File Offset: 0x0002FAD8
	public float StartChaseTime
	{
		get
		{
			return this.mStartChaseTime;
		}
		set
		{
			this.mStartChaseTime = value;
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060006ED RID: 1773 RVA: 0x000318E4 File Offset: 0x0002FAE4
	// (set) Token: 0x060006EE RID: 1774 RVA: 0x000318EC File Offset: 0x0002FAEC
	public bool UpdateEnableFlag
	{
		get
		{
			return this.mUpdateEnableFlag;
		}
		set
		{
			this.mUpdateEnableFlag = value;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060006EF RID: 1775 RVA: 0x000318F8 File Offset: 0x0002FAF8
	// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00031900 File Offset: 0x0002FB00
	public bool EnableActionFlag
	{
		get
		{
			return this.mEnableActionFlag;
		}
		set
		{
			this.mEnableActionFlag = value;
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0003190C File Offset: 0x0002FB0C
	// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00031914 File Offset: 0x0002FB14
	public AIData AiData
	{
		get
		{
			return this.mAiData;
		}
		set
		{
			this.mAiData = value;
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00031920 File Offset: 0x0002FB20
	// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00031928 File Offset: 0x0002FB28
	public float AroundTime
	{
		get
		{
			return this.mAroundTime;
		}
		set
		{
			this.mAroundTime = value;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00031934 File Offset: 0x0002FB34
	// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0003193C File Offset: 0x0002FB3C
	public float EnterAroundStateTime
	{
		get
		{
			return this.mEnterAroundStateTime;
		}
		set
		{
			this.mEnterAroundStateTime = value;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00031948 File Offset: 0x0002FB48
	// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00031950 File Offset: 0x0002FB50
	public float ActionTime
	{
		get
		{
			return this.mActionTime;
		}
		set
		{
			this.mActionTime = value;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0003195C File Offset: 0x0002FB5C
	// (set) Token: 0x060006FA RID: 1786 RVA: 0x00031964 File Offset: 0x0002FB64
	public float EnterAttackStateTime
	{
		get
		{
			return this.mEnterAttackStateTime;
		}
		set
		{
			this.mEnterAttackStateTime = value;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x00031970 File Offset: 0x0002FB70
	// (set) Token: 0x060006FC RID: 1788 RVA: 0x00031978 File Offset: 0x0002FB78
	public string PathId
	{
		get
		{
			return this.mPathId;
		}
		set
		{
			this.mPathId = value;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060006FD RID: 1789 RVA: 0x00031984 File Offset: 0x0002FB84
	// (set) Token: 0x060006FE RID: 1790 RVA: 0x0003198C File Offset: 0x0002FB8C
	public List<NPCPathData> PathList
	{
		get
		{
			return this.mPathList;
		}
		set
		{
			this.mPathList = value;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060006FF RID: 1791 RVA: 0x00031998 File Offset: 0x0002FB98
	// (set) Token: 0x06000700 RID: 1792 RVA: 0x000319A0 File Offset: 0x0002FBA0
	public int CurPathIndex
	{
		get
		{
			return this.mCurPathIndex;
		}
		set
		{
			this.mCurPathIndex = value;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000701 RID: 1793 RVA: 0x000319AC File Offset: 0x0002FBAC
	// (set) Token: 0x06000702 RID: 1794 RVA: 0x000319B4 File Offset: 0x0002FBB4
	public string CurUseSkillID
	{
		get
		{
			return this.mCurUseSkillID;
		}
		set
		{
			this.mCurUseSkillID = value;
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x000319C0 File Offset: 0x0002FBC0
	public void InitAILogic()
	{
		this.mOwner = base.gameObject.GetComponent<ObjNPC>();
		if (this.mStateMachine == null)
		{
			this.mStateMachine = new StateMachine<AILogic>(this, 4);
		}
		this.mStateMachine.AddState(Singleton<PatrolState>.Instance, 0);
		this.mStateMachine.AddState(Singleton<DecisionState>.Instance, 3);
		this.RegisterEvent();
		this.mUpdateEnableFlag = true;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00031A28 File Offset: 0x0002FC28
	public void ResetAI(string ai, string aiID, string pathID)
	{
		this.mAiData = DataManager.GetAIDataByID(aiID);
		if (this.mStateMachine != null)
		{
			this.mUpdateEnableFlag = true;
		}
		else
		{
			this.InitAILogic();
		}
		this.mPathId = pathID;
		if (!string.IsNullOrEmpty(this.mPathId))
		{
			this.mPathList = DataManager.GetNPCPathDataListById(this.mPathId);
			this.mCurPathIndex = 0;
		}
		if (string.IsNullOrEmpty(ai))
		{
			this.mStateMachine.AddState(Singleton<global::AttackState>.Instance, 1);
			this.mAttackState = AISTATE.ATTACK_STATE;
			this.mStateMachine.AddState(Singleton<AroundState>.Instance, 2);
		}
		else if (ai.Equals("BlockAI"))
		{
			this.mStateMachine.AddState(Singleton<BlockAINoAttackState>.Instance, 1);
			this.mAttackState = AISTATE.ATTACK_STATE;
		}
		else if (ai.Equals("RandomSkill"))
		{
			this.mStateMachine.AddState(Singleton<global::AttackState>.Instance, 1);
			this.mAttackState = AISTATE.ATTACK_STATE;
			this.mStateMachine.AddState(Singleton<AroundState>.Instance, 2);
		}
		else if (ai.Equals("FollowAI"))
		{
			this.mStateMachine.AddState(Singleton<BlockAINoAttackState>.Instance, 1);
			this.mAttackState = AISTATE.ATTACK_STATE;
			this.mStateMachine.AddState(Singleton<FollowState>.Instance, 0);
		}
		this.ChangeState(0);
		this.mEnableActionFlag = true;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00031B78 File Offset: 0x0002FD78
	public bool IsHavePath()
	{
		return !string.IsNullOrEmpty(this.mPathId);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00031B88 File Offset: 0x0002FD88
	private IEnumerator UpdateSecond()
	{
		for (;;)
		{
			yield return new WaitForSeconds(0.2f);
			if (this.mStateMachine.CurrentState == null)
			{
				MonoBehaviour.print("mStateMachine.CurrentState == null");
				this.ChangeState(0);
			}
			else
			{
				this.mStateMachine.Update();
			}
		}
		yield break;
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000707 RID: 1799 RVA: 0x00031BA4 File Offset: 0x0002FDA4
	public SceneManager CurSceneManager
	{
		get
		{
			if (this.mSceneManager == null)
			{
				this.mSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			}
			return this.mSceneManager;
		}
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00031BC8 File Offset: 0x0002FDC8
	private void Update()
	{
		if (this.Ownner.IsDie)
		{
			return;
		}
		if (!this.CurSceneManager.IsLowPhoneManager() && !GameManager.OnLineState)
		{
			return;
		}
		if (!this.mEnableActionFlag)
		{
			return;
		}
		if (this.mUpdateEnableFlag)
		{
			this.mUpdateIntervalCount += Time.deltaTime;
			if (this.mUpdateIntervalCount > this.mUpdateInterval)
			{
				this.mUpdateIntervalCount = 0f;
				if (this.mStateMachine.CurrentState == null)
				{
					this.ChangeState(0);
				}
				else
				{
					this.mStateMachine.Update();
				}
			}
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00031C70 File Offset: 0x0002FE70
	public void ChangeState(int newId)
	{
		this.curState = (AISTATE)newId;
		this.mStateMachine.SetState(newId);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00031C88 File Offset: 0x0002FE88
	public void OnPatrolStateArriveTarget(ObjCharacter objCha)
	{
		this.mDelayMoveTimeCount = Time.time;
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00031C98 File Offset: 0x0002FE98
	public void OnStun(BuffInfoData buffInfoData)
	{
		this.mUpdateEnableFlag = false;
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00031CA4 File Offset: 0x0002FEA4
	public void OnStunDone(BuffInfoData buffInfoData)
	{
		this.mUpdateEnableFlag = true;
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00031CB0 File Offset: 0x0002FEB0
	public void OnKnockDown(BuffInfoData buffInfoData, ObjCharacter sender)
	{
		this.mUpdateEnableFlag = false;
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00031CBC File Offset: 0x0002FEBC
	public void OnKnockDownDone(BuffInfoData buffInfoData)
	{
		this.mUpdateEnableFlag = true;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00031CC8 File Offset: 0x0002FEC8
	public void OnSleep(BuffInfoData buffInfoData)
	{
		this.mUpdateEnableFlag = false;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00031CD4 File Offset: 0x0002FED4
	public void OnSleepDone(BuffInfoData buffInfoData)
	{
		this.mUpdateEnableFlag = true;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00031CE0 File Offset: 0x0002FEE0
	public void OnBeaton()
	{
		this.mStateMachine.Notify(0, new object[]
		{
			this
		});
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00031CF8 File Offset: 0x0002FEF8
	public void OnSkillFinished()
	{
		this.mStateMachine.Notify(1, new object[]
		{
			this
		});
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00031D10 File Offset: 0x0002FF10
	private void OnDestroy()
	{
		this.DeRegisterEvent();
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00031D18 File Offset: 0x0002FF18
	private void RegisterEvent()
	{
		this.mOwner.BuffLogic.RegisterOnStun(new BuffLogic.BuffDelegate(this.OnStun));
		this.mOwner.BuffLogic.RegisterOnStunDone(new BuffLogic.BuffDelegate(this.OnStunDone));
		this.mOwner.BuffLogic.RegisterOnSleep(new BuffLogic.BuffDelegate(this.OnSleep));
		this.mOwner.BuffLogic.RegisterOnSleepDone(new BuffLogic.BuffDelegate(this.OnSleepDone));
		this.mOwner.RegisterOnBeaton(new ObjCharacter.OnBeatonDelegate(this.OnBeaton));
		this.mOwner.RegisterOnSkillFinished(new ObjCharacter.OnSkillFinishedDelegate(this.OnSkillFinished));
		this.mOwner.BuffLogic.RegisterOnKnockDown(new BuffLogic.BuffSenderDelegate(this.OnKnockDown));
		this.mOwner.BuffLogic.RegisterOnKnockDownDone(new BuffLogic.BuffDelegate(this.OnKnockDownDone));
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x00031DFC File Offset: 0x0002FFFC
	private void DeRegisterEvent()
	{
		this.mOwner.BuffLogic.DeRegisterOnStun(new BuffLogic.BuffDelegate(this.OnStun));
		this.mOwner.BuffLogic.DeRegisterOnStunDone(new BuffLogic.BuffDelegate(this.OnStunDone));
		this.mOwner.BuffLogic.DeRegisterOnSleep(new BuffLogic.BuffDelegate(this.OnSleep));
		this.mOwner.BuffLogic.DeRegisterOnSleepDone(new BuffLogic.BuffDelegate(this.OnSleepDone));
		this.mOwner.DeRegisterOnBeaton(new ObjCharacter.OnBeatonDelegate(this.OnBeaton));
		this.mOwner.DeRegisterOnSkillFinished(new ObjCharacter.OnSkillFinishedDelegate(this.OnSkillFinished));
		this.mOwner.BuffLogic.DeRegisterOnKnockDown(new BuffLogic.BuffSenderDelegate(this.OnKnockDown));
		this.mOwner.BuffLogic.DeRegisterOnKnockDownDone(new BuffLogic.BuffDelegate(this.OnKnockDownDone));
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00031EE0 File Offset: 0x000300E0
	public void EnableAIAction()
	{
		this.mEnableActionFlag = true;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00031EEC File Offset: 0x000300EC
	public void DisableAIAction()
	{
		this.mEnableActionFlag = false;
	}

	// Token: 0x0400060F RID: 1551
	private ObjNPC mOwner;

	// Token: 0x04000610 RID: 1552
	public AISTATE curState;

	// Token: 0x04000611 RID: 1553
	private StateMachine<AILogic> mStateMachine;

	// Token: 0x04000612 RID: 1554
	private AISTATE mAttackState;

	// Token: 0x04000613 RID: 1555
	private float mDelayMoveTimeCount;

	// Token: 0x04000614 RID: 1556
	public bool mOutOffRangeFlag;

	// Token: 0x04000615 RID: 1557
	public bool mReturnBackFlag;

	// Token: 0x04000616 RID: 1558
	private float mStartChaseTime;

	// Token: 0x04000617 RID: 1559
	public float ChaseTime = 2f;

	// Token: 0x04000618 RID: 1560
	private float mUpdateIntervalCount;

	// Token: 0x04000619 RID: 1561
	private float mUpdateInterval = 0.2f;

	// Token: 0x0400061A RID: 1562
	private bool mUpdateEnableFlag;

	// Token: 0x0400061B RID: 1563
	private bool mEnableActionFlag;

	// Token: 0x0400061C RID: 1564
	private AIData mAiData;

	// Token: 0x0400061D RID: 1565
	private float mAroundTime;

	// Token: 0x0400061E RID: 1566
	private float mEnterAroundStateTime;

	// Token: 0x0400061F RID: 1567
	private float mActionTime;

	// Token: 0x04000620 RID: 1568
	private float mEnterAttackStateTime;

	// Token: 0x04000621 RID: 1569
	private string mPathId;

	// Token: 0x04000622 RID: 1570
	private List<NPCPathData> mPathList;

	// Token: 0x04000623 RID: 1571
	private int mCurPathIndex;

	// Token: 0x04000624 RID: 1572
	private string mCurUseSkillID;

	// Token: 0x04000625 RID: 1573
	private SceneManager mSceneManager;
}
