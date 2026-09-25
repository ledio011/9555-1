using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082C RID: 2092
public class ObjPatrolNPC : ObjNPC
{
	// Token: 0x17000EBF RID: 3775
	// (get) Token: 0x060034DE RID: 13534 RVA: 0x000D4E34 File Offset: 0x000D3034
	// (set) Token: 0x060034DF RID: 13535 RVA: 0x000D4E3C File Offset: 0x000D303C
	public List<Vector3> PatrolPointList
	{
		get
		{
			return this.mPatrolPointList;
		}
		set
		{
			this.mPatrolPointList = value;
		}
	}

	// Token: 0x17000EC0 RID: 3776
	// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000D4E48 File Offset: 0x000D3048
	// (set) Token: 0x060034E1 RID: 13537 RVA: 0x000D4E50 File Offset: 0x000D3050
	public int CurPointIndex
	{
		get
		{
			return this.mCurPointIndex;
		}
		set
		{
			this.mCurPointIndex = value;
		}
	}

	// Token: 0x17000EC1 RID: 3777
	// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000D4E5C File Offset: 0x000D305C
	// (set) Token: 0x060034E3 RID: 13539 RVA: 0x000D4E64 File Offset: 0x000D3064
	public float SearchAngle
	{
		get
		{
			return this.mSearchAngle;
		}
		set
		{
			this.mSearchAngle = value;
		}
	}

	// Token: 0x17000EC2 RID: 3778
	// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000D4E70 File Offset: 0x000D3070
	// (set) Token: 0x060034E5 RID: 13541 RVA: 0x000D4E78 File Offset: 0x000D3078
	public float SearchDis
	{
		get
		{
			return this.mSearchDis;
		}
		set
		{
			this.mSearchDis = value;
		}
	}

	// Token: 0x060034E6 RID: 13542 RVA: 0x000D4E84 File Offset: 0x000D3084
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
		this.AttributeData.CurATK = (float)initData.npcInfoData.Atk;
		this.AttributeData.CurDEF = (float)initData.npcInfoData.Def;
		this.AttributeData.Name = initData.npcInfoData.Name;
		this.AttributeData.CurEXD = (float)this.mNpcData.EXD / 10000f;
		this.AttributeData.CurEXR = (float)this.mNpcData.EXR / 10000f;
		this.AttributeData.CurHIT = (float)this.mNpcData.HIT;
		this.AttributeData.CurDGE = (float)this.mNpcData.DGE;
		this.AttributeData.CurCRI = (float)this.mNpcData.CRI;
		this.AttributeData.CurRES = (float)this.mNpcData.RES;
		this.AttributeData.CurCRD = (float)this.mNpcData.CRD / 10000f;
		this.AttributeData.CurCRR = (float)this.mNpcData.CRR / 10000f;
		this.AttributeData.CurDEFA = this.mNpcData.DEFA;
		this.AttributeData.CurDGEA = initData.DGEA;
		this.AttributeData.CurRESA = initData.RESA;
		this.AttributeData.CurHITA = initData.HITA;
		this.AttributeData.CurCRIA = initData.CRIA;
		this.AttributeData.CurAntiKnockDown = (float)initData.AntiKnockDown / 10000f;
		this.AttributeData.CurAntiStun = (float)initData.AntiStun / 10000f;
		this.AttributeData.Level = initData.Level;
		this.AttributeData.CurSpeed = initData.npcInfoData.MoveSpeedMeter;
		this.AttributeData.WalkSpeed = initData.npcInfoData.WalkSpeedMeter;
		base.InitNavMeshAgent();
		if (this.mTransform.childCount > 0)
		{
			Transform child = this.mTransform.GetChild(0);
			child.localScale = Vector3.one * initData.npcInfoData.ModelScale;
		}
		this.mCurPointIndex = 1;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.mSearchDis = 5f;
		this.mSearchAngle = 60f;
		if (this.mCheckAreaObj == null)
		{
			this.mCheckAreaObj = new GameObject("CheckArea");
			this.mCheckAreaObj.transform.parent = base.CacheTransform;
			this.mCheckAreaObj.transform.localPosition = Vector3.up * 0.2f;
			this.mCheckAreaObj.transform.localRotation = Quaternion.identity;
			Light light = this.mCheckAreaObj.AddComponent<Light>();
			light.type = 0;
			light.range = this.mSearchDis;
			light.spotAngle = this.mSearchAngle;
			light.color = Color.white;
			light.cullingMask = 8388608;
			light.intensity = 8f;
			light.renderMode = 1;
		}
		this.mSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
	}

	// Token: 0x060034E7 RID: 13543 RVA: 0x000D5254 File Offset: 0x000D3454
	public void SetPathPoint(List<Vector3> pointList)
	{
		for (int i = 0; i < pointList.Count; i++)
		{
			this.mPatrolPointList.Add(pointList[i]);
		}
		base.WalkMoveTo(this.mPatrolPointList[this.mCurPointIndex], 1f, new ObjCharacter.TargetArriveFinsh(this.OnArrivePoint));
	}

	// Token: 0x060034E8 RID: 13544 RVA: 0x000D52B4 File Offset: 0x000D34B4
	private void OnArrivePoint(ObjCharacter objCha)
	{
		this.mCurPointIndex = (this.mCurPointIndex + 1) % this.mPatrolPointList.Count;
		base.WalkMoveTo(this.mPatrolPointList[this.mCurPointIndex], 1f, new ObjCharacter.TargetArriveFinsh(this.OnArrivePoint));
	}

	// Token: 0x060034E9 RID: 13545 RVA: 0x000D5304 File Offset: 0x000D3504
	public void ResetToPathBegin()
	{
		base.CacheTransform.position = this.mPatrolPointList[0];
		this.mCurPointIndex = 0;
		this.OnArrivePoint(this);
	}

	// Token: 0x060034EA RID: 13546 RVA: 0x000D5338 File Offset: 0x000D3538
	private void Update()
	{
		base.UpdateComponent();
		base.UpdateMove();
		this.mCheckTimeCount += Time.deltaTime;
		if (this.mCheckTimeCount > 0.1f)
		{
			this.mCheckTimeCount = 0f;
			this.CheckFindPlayer();
		}
	}

	// Token: 0x060034EB RID: 13547 RVA: 0x000D5384 File Offset: 0x000D3584
	private void CheckFindPlayer()
	{
		if (this.mMainPlayer != null)
		{
			if (AreaCheckTool.CheckInSector(this.mMainPlayer.Position, base.CacheTransform, this.mSearchDis, this.mSearchAngle))
			{
				this.mSceneManager.OnFindPlayer();
			}
		}
		else
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x000D53EC File Offset: 0x000D35EC
	public void RecycleSelf()
	{
		this.Recyle();
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
		Singleton<ObjManager>.Instance.RecyclePatrolNPC(this);
	}

	// Token: 0x0400228F RID: 8847
	private List<Vector3> mPatrolPointList = new List<Vector3>();

	// Token: 0x04002290 RID: 8848
	private int mCurPointIndex;

	// Token: 0x04002291 RID: 8849
	private float mSearchAngle;

	// Token: 0x04002292 RID: 8850
	private float mSearchDis;

	// Token: 0x04002293 RID: 8851
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002294 RID: 8852
	private GameObject mCheckAreaObj;

	// Token: 0x04002295 RID: 8853
	private SceneManager mSceneManager;

	// Token: 0x04002296 RID: 8854
	private float mCheckTimeCount;
}
