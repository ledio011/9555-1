using System;
using UnityEngine;

// Token: 0x02000A66 RID: 2662
public class AcceleratedMotion : MonoBehaviour
{
	// Token: 0x17000FD1 RID: 4049
	// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x001A8B8C File Offset: 0x001A6D8C
	public bool Going
	{
		get
		{
			return this.mGoing;
		}
	}

	// Token: 0x06004DA7 RID: 19879 RVA: 0x001A8B94 File Offset: 0x001A6D94
	private void Start()
	{
		this.mTransform = base.gameObject.transform;
	}

	// Token: 0x06004DA8 RID: 19880 RVA: 0x001A8BA8 File Offset: 0x001A6DA8
	private void Update()
	{
		if (this.mGoing)
		{
			this.Play();
		}
	}

	// Token: 0x06004DA9 RID: 19881 RVA: 0x001A8BBC File Offset: 0x001A6DBC
	public void Init(Vector3 vecVelocity, Vector3 vecAcceleration, float fMotionTime)
	{
		this.mVelocity = vecVelocity;
		this.mOriginVelocity = vecVelocity;
		this.mAcceleration = vecAcceleration;
		this.mMotionTime = fMotionTime;
	}

	// Token: 0x06004DAA RID: 19882 RVA: 0x001A8BDC File Offset: 0x001A6DDC
	public void Go()
	{
		this.mGoing = true;
		this.mStartTime = Time.fixedTime;
	}

	// Token: 0x06004DAB RID: 19883 RVA: 0x001A8BF0 File Offset: 0x001A6DF0
	private void Play()
	{
		if (Time.fixedTime - this.mStartTime <= this.mMotionTime)
		{
			base.gameObject.transform.localPosition += this.mVelocity * Time.deltaTime;
			this.mVelocity += this.mAcceleration * Time.deltaTime;
		}
		else
		{
			this.mGoing = false;
			this.mStartTime = 0f;
			this.mVelocity = this.mOriginVelocity;
		}
	}

	// Token: 0x04003C5F RID: 15455
	public Vector3 mOriginPosition = new Vector3(0f, 0f, 0f);

	// Token: 0x04003C60 RID: 15456
	public Vector3 mVelocity = new Vector3(0f, 0f, 0f);

	// Token: 0x04003C61 RID: 15457
	public Vector3 mAcceleration = new Vector3(0f, 0f, 0f);

	// Token: 0x04003C62 RID: 15458
	private float mStartTime;

	// Token: 0x04003C63 RID: 15459
	private float mMotionTime;

	// Token: 0x04003C64 RID: 15460
	private bool mGoing;

	// Token: 0x04003C65 RID: 15461
	private Vector3 mOriginVelocity = new Vector3(0f, 0f, 0f);

	// Token: 0x04003C66 RID: 15462
	private Transform mTransform;
}
