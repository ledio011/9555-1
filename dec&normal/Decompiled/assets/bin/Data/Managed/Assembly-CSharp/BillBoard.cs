using System;
using UnityEngine;

// Token: 0x02000838 RID: 2104
public class BillBoard : MonoBehaviour
{
	// Token: 0x17000EDC RID: 3804
	// (get) Token: 0x060035D8 RID: 13784 RVA: 0x000DC108 File Offset: 0x000DA308
	// (set) Token: 0x060035D9 RID: 13785 RVA: 0x000DC110 File Offset: 0x000DA310
	public GameObject BindObj
	{
		get
		{
			return this.mBindObj;
		}
		set
		{
			this.mBindObj = value;
			if (this.mBindObj != null)
			{
				this.mBindObjTrans = this.mBindObj.transform;
			}
		}
	}

	// Token: 0x17000EDD RID: 3805
	// (get) Token: 0x060035DB RID: 13787 RVA: 0x000DC178 File Offset: 0x000DA378
	// (set) Token: 0x060035DA RID: 13786 RVA: 0x000DC13C File Offset: 0x000DA33C
	public float DeltaHeight
	{
		get
		{
			return this.mDeltaHeight;
		}
		set
		{
			if (value != this.mDeltaHeight)
			{
				this.mDeltaHeight = value;
				this.mPosition = new Vector3(0f, this.mDeltaHeight, 0f);
			}
		}
	}

	// Token: 0x060035DC RID: 13788 RVA: 0x000DC180 File Offset: 0x000DA380
	private void Awake()
	{
		if (null == Camera.main)
		{
			return;
		}
		this.mCameraTransform = Camera.main.transform;
		Vector3 vector = this.mCameraTransform.rotation * Vector3.up;
		base.transform.LookAt(base.transform.position + this.mCameraTransform.rotation * Vector3.back, vector);
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x000DC1F8 File Offset: 0x000DA3F8
	private void OnEnable()
	{
		base.transform.rotation = Quaternion.identity;
		if (null == Camera.main)
		{
			return;
		}
		Vector3 vector = this.mCameraTransform.rotation * Vector3.up;
		base.transform.LookAt(base.transform.position + this.mCameraTransform.rotation * Vector3.back, vector);
		base.transform.position = new Vector3(0f, -20f, 0f);
	}

	// Token: 0x060035DE RID: 13790 RVA: 0x000DC28C File Offset: 0x000DA48C
	private void Start()
	{
		if (this.mBindObj != null)
		{
			this.mPosition = new Vector3(0f, this.mDeltaHeight, 0f);
			this.mBindObjTrans = this.mBindObj.transform;
		}
		this.mTransform = base.transform;
	}

	// Token: 0x060035DF RID: 13791 RVA: 0x000DC2E4 File Offset: 0x000DA4E4
	private void Update()
	{
		if (this.mCameraTransform == null)
		{
			if (Camera.main == null)
			{
				return;
			}
			this.mCameraTransform = Camera.main.transform;
			if (this.mCameraTransform == null)
			{
				return;
			}
		}
		if (null != this.mBindObj && null == this.mBindObjTrans)
		{
			this.mBindObjTrans = this.mBindObj.transform;
		}
		if (this.mBindObj && null != this.mTransform && null != this.mBindObjTrans)
		{
			this.mTransform.position = this.mBindObjTrans.position;
			this.mTransform.position += this.mPosition;
		}
		Vector3 vector = this.mCameraTransform.rotation * Vector3.up;
		base.transform.LookAt(base.transform.position + this.mCameraTransform.rotation * Vector3.back, vector);
		base.transform.Rotate(Vector3.up * 180f);
	}

	// Token: 0x04002327 RID: 8999
	private GameObject mBindObj;

	// Token: 0x04002328 RID: 9000
	private float mDeltaHeight = 2.25f;

	// Token: 0x04002329 RID: 9001
	private Transform mCameraTransform;

	// Token: 0x0400232A RID: 9002
	private Transform mTransform;

	// Token: 0x0400232B RID: 9003
	private Vector3 mPosition;

	// Token: 0x0400232C RID: 9004
	private Transform mBindObjTrans;
}
