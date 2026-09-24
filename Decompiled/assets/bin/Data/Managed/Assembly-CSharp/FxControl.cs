using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class FxControl : MonoBehaviour
{
	// Token: 0x17000393 RID: 915
	// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0006D644 File Offset: 0x0006B844
	public long OwnerId
	{
		get
		{
			return this.mOwnerId;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0006D64C File Offset: 0x0006B84C
	public GameDefine.OBJ_TYPE OwnerType
	{
		get
		{
			return this.mOwnerType;
		}
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0006D654 File Offset: 0x0006B854
	public float GenerateTime
	{
		get
		{
			return this.mGenerateTime;
		}
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0006D65C File Offset: 0x0006B85C
	public string EffectName
	{
		get
		{
			return this.mEffectName;
		}
	}

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0006D664 File Offset: 0x0006B864
	public string EffectId
	{
		get
		{
			return this.mEffectId;
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x060010DA RID: 4314 RVA: 0x0006D66C File Offset: 0x0006B86C
	public float Duration
	{
		get
		{
			return this.mDuration;
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x060010DB RID: 4315 RVA: 0x0006D674 File Offset: 0x0006B874
	public Transform CacheTransform
	{
		get
		{
			if (this.mCacheTransform == null)
			{
				this.mCacheTransform = base.transform;
			}
			return this.mCacheTransform;
		}
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0006D69C File Offset: 0x0006B89C
	private void OnEnable()
	{
		for (int i = 0; i < this.arraySystem.Length; i++)
		{
			this.arraySystem[i].Clear();
		}
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0006D6D0 File Offset: 0x0006B8D0
	private void OnDisable()
	{
		for (int i = 0; i < this.arraySystem.Length; i++)
		{
			if (this.arraySystem[i] != null)
			{
				this.arraySystem[i].Clear();
			}
		}
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0006D718 File Offset: 0x0006B918
	private void PlayParticleSystem()
	{
		for (int i = 0; i < this.arraySystem.Length; i++)
		{
			this.arraySystem[i].Play();
		}
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0006D74C File Offset: 0x0006B94C
	public virtual void Play(Transform target)
	{
		this.Play();
		if (target != null)
		{
			if (this.mAutoMoveFx == null)
			{
				this.mAutoMoveFx = base.gameObject.AddComponent<AutoMoveFx>();
			}
			this.mAutoMoveFx.Reset(this.mDuration, target);
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0006D7A0 File Offset: 0x0006B9A0
	public virtual void Play(Vector3 target)
	{
		this.Play();
		if (this.mAutoMoveFx == null)
		{
			this.mAutoMoveFx = base.gameObject.AddComponent<AutoMoveFx>();
		}
		this.mAutoMoveFx.Reset(this.mDuration, target);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0006D7E8 File Offset: 0x0006B9E8
	public virtual void Play()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		this.PlayParticleSystem();
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x0006D7FC File Offset: 0x0006B9FC
	public virtual void Reset(EffectLogic effectLogic, FxEffInfoData fxEffInfoData, float duration, long ownerId, GameDefine.OBJ_TYPE ownerType)
	{
		this.mDuration = duration;
		this.mEffectName = string.Format("{0}", fxEffInfoData.EffName);
		this.mEffectId = fxEffInfoData.ID;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		this.mGenerateTime = Time.time;
		this.mOwnerId = ownerId;
		this.mOwnerType = ownerType;
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0006D85C File Offset: 0x0006BA5C
	private void Awake()
	{
		this.arraySystem = base.GetComponentsInChildren<ParticleSystem>();
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0006D86C File Offset: 0x0006BA6C
	private void Update()
	{
		this.mDuration -= Time.deltaTime;
		if (this.mDuration <= 0f)
		{
			this.mDuration = 2f;
			EffectLogic.RecyleEffect(this);
		}
	}

	// Token: 0x0400146B RID: 5227
	private long mOwnerId = -1L;

	// Token: 0x0400146C RID: 5228
	public GameDefine.OBJ_TYPE mOwnerType;

	// Token: 0x0400146D RID: 5229
	private float mGenerateTime;

	// Token: 0x0400146E RID: 5230
	private ParticleSystem[] arraySystem;

	// Token: 0x0400146F RID: 5231
	private string mEffectName;

	// Token: 0x04001470 RID: 5232
	private string mEffectId;

	// Token: 0x04001471 RID: 5233
	private float mDuration;

	// Token: 0x04001472 RID: 5234
	private Transform mCacheTransform;

	// Token: 0x04001473 RID: 5235
	private EffectLogic mCurEffectLogicHandle;

	// Token: 0x04001474 RID: 5236
	private AutoMoveFx mAutoMoveFx;
}
