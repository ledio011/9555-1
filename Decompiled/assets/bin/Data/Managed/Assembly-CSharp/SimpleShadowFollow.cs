using System;
using UnityEngine;

// Token: 0x02000840 RID: 2112
public class SimpleShadowFollow : MonoBehaviour
{
	// Token: 0x17000EF3 RID: 3827
	// (get) Token: 0x06003643 RID: 13891 RVA: 0x000DEC28 File Offset: 0x000DCE28
	// (set) Token: 0x06003644 RID: 13892 RVA: 0x000DEC30 File Offset: 0x000DCE30
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

	// Token: 0x17000EF4 RID: 3828
	// (get) Token: 0x06003646 RID: 13894 RVA: 0x000DEC98 File Offset: 0x000DCE98
	// (set) Token: 0x06003645 RID: 13893 RVA: 0x000DEC5C File Offset: 0x000DCE5C
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

	// Token: 0x06003647 RID: 13895 RVA: 0x000DECA0 File Offset: 0x000DCEA0
	private void OnEnable()
	{
		base.transform.rotation = Quaternion.identity;
		base.transform.position = new Vector3(0f, -20f, 0f);
	}

	// Token: 0x06003648 RID: 13896 RVA: 0x000DECDC File Offset: 0x000DCEDC
	private void Start()
	{
		if (this.mBindObj != null)
		{
			this.mPosition = new Vector3(0f, this.mDeltaHeight, 0f);
			this.mBindObjTrans = this.mBindObj.transform;
		}
		this.mTransform = base.transform;
	}

	// Token: 0x06003649 RID: 13897 RVA: 0x000DED34 File Offset: 0x000DCF34
	private void Update()
	{
		if (null != this.mBindObj && null == this.mBindObjTrans)
		{
			this.mBindObjTrans = this.mBindObj.transform;
		}
		if (this.mBindObj && null != this.mTransform && null != this.mBindObjTrans)
		{
			this.mTransform.position = this.mBindObjTrans.position;
			this.mTransform.position += this.mPosition;
			this.mTransform.rotation = this.mBindObjTrans.rotation;
		}
	}

	// Token: 0x040023A8 RID: 9128
	private GameObject mBindObj;

	// Token: 0x040023A9 RID: 9129
	private float mDeltaHeight;

	// Token: 0x040023AA RID: 9130
	private Transform mTransform;

	// Token: 0x040023AB RID: 9131
	private Vector3 mPosition;

	// Token: 0x040023AC RID: 9132
	private Transform mBindObjTrans;
}
