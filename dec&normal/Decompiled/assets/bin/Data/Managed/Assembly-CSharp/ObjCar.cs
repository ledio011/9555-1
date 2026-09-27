using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class ObjCar : ObjCharacter
{
	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0004A758 File Offset: 0x00048958
	public override float ModelRadius
	{
		get
		{
			return 3f;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0004A760 File Offset: 0x00048960
	public override float ModelHeight
	{
		get
		{
			return 1.5f;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0004A768 File Offset: 0x00048968
	public CarControl CarControl
	{
		get
		{
			return this.mCarControl;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0004A770 File Offset: 0x00048970
	// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0004A778 File Offset: 0x00048978
	public new string ModelId
	{
		get
		{
			return this.mModelId;
		}
		set
		{
			this.mModelId = value;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0004A784 File Offset: 0x00048984
	public float CurSpeed
	{
		get
		{
			return this.mCarControl.CurSpeed;
		}
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0004A794 File Offset: 0x00048994
	protected new void Init()
	{
		if (this.mCarControl == null)
		{
			this.mCarControl = base.gameObject.GetComponent<CarControl>();
			if (this.mCarControl == null)
			{
				this.mCarControl = base.gameObject.AddComponent<CarControl>();
			}
		}
		this.FLWheel = base.transform.FindChild("MeshRoot/FLWheel").gameObject.GetComponent<WheelSuspension>();
		this.FRWheel = base.transform.FindChild("MeshRoot/FRWheel").gameObject.GetComponent<WheelSuspension>();
		this.BLWheel = base.transform.FindChild("MeshRoot/BLWheel").gameObject.GetComponent<WheelSuspension>();
		this.BRWheel = base.transform.FindChild("MeshRoot/BRWheel").gameObject.GetComponent<WheelSuspension>();
		this.CarBodyRoot = base.transform.FindChild("MeshRoot/cheshen");
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0004A87C File Offset: 0x00048A7C
	public virtual void DisableCar()
	{
		if (!base.rigidbody.isKinematic)
		{
			base.rigidbody.velocity = Vector3.zero;
			base.rigidbody.angularVelocity = Vector3.zero;
		}
		base.rigidbody.useGravity = false;
		base.rigidbody.isKinematic = true;
		base.rigidbody.drag = 0.6f;
		base.rigidbody.angularDrag = 0.6f;
		this.mCarControl.enabled = false;
		this.FLWheel.enabled = false;
		this.FRWheel.enabled = false;
		this.BLWheel.enabled = false;
		this.BRWheel.enabled = false;
		this.InsidePlayer = null;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0004A934 File Offset: 0x00048B34
	public virtual void EnableCar(ObjCharacter insidePlayer)
	{
		base.rigidbody.isKinematic = false;
		base.rigidbody.useGravity = true;
		this.mCarControl.enabled = true;
		base.rigidbody.drag = 0f;
		base.rigidbody.angularDrag = 0f;
		this.FLWheel.enabled = true;
		this.FRWheel.enabled = true;
		this.BLWheel.enabled = true;
		this.BRWheel.enabled = true;
		this.InsidePlayer = insidePlayer;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0004A9BC File Offset: 0x00048BBC
	protected new void Reset()
	{
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0004A9C0 File Offset: 0x00048BC0
	public new void FaceToPub(Vector3 pos)
	{
		Vector3 vector = pos - base.Position;
		vector.y = 0f;
		if (vector != Vector3.zero)
		{
			base.CacheTransform.rotation = Quaternion.LookRotation(vector);
		}
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0004AA08 File Offset: 0x00048C08
	private void SetCarPath(CarPath path)
	{
		this.mPath = path;
		this.mCurPathIndex = 0;
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0004AA18 File Offset: 0x00048C18
	public CarPathPoint GetCurPathPoint()
	{
		if (this.mCurPathIndex < this.mPath.PathPointList.Count - 1)
		{
			for (int i = this.mCurPathIndex; i < this.mPath.PathPointList.Count; i++)
			{
				if (this.mPath.PathPointList[i].transform.InverseTransformPoint(base.Position).z * this.mPath.PathPointList[i + 1].transform.InverseTransformPoint(base.Position).z < 0f)
				{
					this.mCurPathIndex = i;
					return this.mPath.PathPointList[i];
				}
			}
			return null;
		}
		return this.mPath.PathPointList[this.mCurPathIndex];
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0004AAF8 File Offset: 0x00048CF8
	public int GetCurPathIndex()
	{
		if (this.mCurPathIndex < this.mPath.PathPointList.Count - 1)
		{
			for (int i = Mathf.Max(this.mCurPathIndex - 1, 0); i < this.mPath.PathPointList.Count; i++)
			{
				if (this.mPath.PathPointList[i].transform.InverseTransformPoint(base.Position).z * this.mPath.PathPointList[i + 1].transform.InverseTransformPoint(base.Position).z < 0f)
				{
					this.mCurPathIndex = i + 1;
					return this.mCurPathIndex;
				}
			}
			return this.mCurPathIndex;
		}
		return this.mCurPathIndex;
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0004ABCC File Offset: 0x00048DCC
	protected void FixedUpdate()
	{
		this.mCarControl.OnTheGroundFlag = (this.FLWheel.OnGround && this.FRWheel.OnGround && this.BLWheel.OnGround && this.BRWheel.OnGround);
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0004AC24 File Offset: 0x00048E24
	protected void Update()
	{
		if (this.mCarControl.CurSpeed < 10f && this.mStrikeDic.Count > 0)
		{
			List<GameObject> list = new List<GameObject>(this.mStrikeDic.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				this.DisableStrike(list[i]);
			}
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0004AC8C File Offset: 0x00048E8C
	private SceneManager CurSceneManager
	{
		get
		{
			if (this.mCurSceneManager == null)
			{
				this.mCurSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			}
			return this.mCurSceneManager;
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0004ACB0 File Offset: 0x00048EB0
	protected void EnableStrike(GameObject obj, Vector3 pos)
	{
		if (!GameSettingData.IsCarCopyEffectEnable[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		GameObject root = NGUITools.GetRoot(obj);
		if (!this.mStrikeDic.ContainsKey(root))
		{
			ParticleSystem strike = this.CurSceneManager.GetStrike();
			if (strike != null)
			{
				UnityVersionUtil.SetActiveRecursive(strike.gameObject, true);
				strike.transform.parent = this.MeshRoot.transform;
				strike.transform.position = pos;
				this.mStrikeDic.Add(root, strike);
				strike.Play();
			}
			else
			{
				Debug.Log("Strike Out Of Pool Count!!!!!!!!!!!!!!!!!");
			}
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0004AD50 File Offset: 0x00048F50
	protected void DisableStrike(GameObject obj)
	{
		GameObject root = NGUITools.GetRoot(obj);
		if (this.mStrikeDic.ContainsKey(root))
		{
			ParticleSystem particleSystem = this.mStrikeDic[root];
			UnityVersionUtil.SetActiveRecursive(particleSystem.gameObject, false);
			this.mStrikeDic.Remove(root);
			particleSystem.transform.parent = null;
			this.CurSceneManager.RecycleStrike(particleSystem);
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0004ADB4 File Offset: 0x00048FB4
	protected void ClearStrike()
	{
		List<ParticleSystem> list = new List<ParticleSystem>(this.mStrikeDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(list[i].gameObject, false);
			list[i].transform.parent = null;
			this.CurSceneManager.RecycleStrike(list[i]);
		}
		this.mStrikeDic.Clear();
	}

	// Token: 0x0400091D RID: 2333
	public float MaxSpeed = 40f;

	// Token: 0x0400091E RID: 2334
	public float MaxSteerAngle = 10f;

	// Token: 0x0400091F RID: 2335
	public float MaxAcceleration = 10f;

	// Token: 0x04000920 RID: 2336
	public float BrakeAcceleration = 40f;

	// Token: 0x04000921 RID: 2337
	protected CarControl mCarControl;

	// Token: 0x04000922 RID: 2338
	public GameObject MeshRoot;

	// Token: 0x04000923 RID: 2339
	public Transform CarBodyRoot;

	// Token: 0x04000924 RID: 2340
	private string mModelId;

	// Token: 0x04000925 RID: 2341
	public WheelSuspension FLWheel;

	// Token: 0x04000926 RID: 2342
	public WheelSuspension FRWheel;

	// Token: 0x04000927 RID: 2343
	public WheelSuspension BLWheel;

	// Token: 0x04000928 RID: 2344
	public WheelSuspension BRWheel;

	// Token: 0x04000929 RID: 2345
	protected Dictionary<GameObject, ParticleSystem> mStrikeDic = new Dictionary<GameObject, ParticleSystem>();

	// Token: 0x0400092A RID: 2346
	public ObjCharacter InsidePlayer;

	// Token: 0x0400092B RID: 2347
	protected CarPath mPath;

	// Token: 0x0400092C RID: 2348
	protected int mCurPathIndex;

	// Token: 0x0400092D RID: 2349
	private SceneManager mCurSceneManager;
}
