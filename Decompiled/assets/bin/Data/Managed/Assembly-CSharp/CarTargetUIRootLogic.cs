using System;
using System.Text;
using UnityEngine;

// Token: 0x020008D3 RID: 2259
public class CarTargetUIRootLogic : SingletonUnity<CarTargetUIRootLogic>
{
	// Token: 0x06003CDC RID: 15580 RVA: 0x0010C91C File Offset: 0x0010AB1C
	private new void Awake()
	{
		base.Awake();
		this.screenWidth = Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
		this.mMainCamera = Camera.main;
		this.targetTransform = this.targetPic.transform;
	}

	// Token: 0x06003CDD RID: 15581 RVA: 0x0010C96C File Offset: 0x0010AB6C
	private void Update()
	{
		if (this.mSourceObj == null)
		{
			return;
		}
		if (UnityVersionUtil.IsActive(this.mTargetObj))
		{
			if (!UnityVersionUtil.IsActive(this.targetPic.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, true);
			}
			else
			{
				Vector3 vector = this.mMainCamera.WorldToViewportPoint(this.tartgetPos + Vector3.up * 0.5f);
				Vector3 zero = Vector3.zero;
				if (vector.z > 0f)
				{
					zero..ctor(Mathf.Clamp(vector.x * (float)this.screenWidth, 30f, (float)(this.screenWidth - 30)), Mathf.Clamp(vector.y * 480f, 10f, 470f), 0f);
				}
				else
				{
					vector.x = 1f - vector.x;
					zero..ctor(Mathf.Clamp(vector.x * (float)this.screenWidth, 30f, (float)(this.screenWidth - 30)), 10f, 0f);
				}
				this.ArrowPic.transform.localPosition = zero;
				if (zero.x >= (float)(this.screenWidth - 30))
				{
					this.ArrowPic.transform.localEulerAngles = Vector3.zero;
					this.targetPic.transform.localPosition = zero + Vector3.left * 45f;
					this.DisLabel.transform.localPosition = this.targetTransform.localPosition + Vector3.up * 30f;
				}
				else if (zero.x <= 30f)
				{
					this.ArrowPic.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
					this.targetPic.transform.localPosition = zero + Vector3.right * 45f;
					this.DisLabel.transform.localPosition = this.targetTransform.localPosition + Vector3.up * 30f;
				}
				else if (zero.y >= 410f)
				{
					this.ArrowPic.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
					this.targetPic.transform.localPosition = zero + Vector3.down * 45f;
					this.DisLabel.transform.localPosition = this.targetTransform.localPosition + Vector3.down * 30f;
				}
				else
				{
					this.ArrowPic.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
					this.targetPic.transform.localPosition = zero + Vector3.up * 45f;
					this.DisLabel.transform.localPosition = this.targetTransform.localPosition + Vector3.up * 30f;
				}
				this.sb.Length = 0;
				this.sb.AppendFormat("{0}m", (int)Vector3.Distance(this.sourcesTransform.position, this.tartgetPos));
				this.DisLabel.text = this.sb.ToString();
			}
		}
		else if (UnityVersionUtil.IsActive(this.targetPic.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, false);
		}
	}

	// Token: 0x06003CDE RID: 15582 RVA: 0x0010CD40 File Offset: 0x0010AF40
	public void Reset(GameObject targetObj, GameObject sourceObj)
	{
		this.mTargetObj = targetObj;
		this.tartgetPos = targetObj.transform.position;
		this.mSourceObj = sourceObj;
		if (this.mSourceObj != null)
		{
			this.sourcesTransform = sourceObj.transform;
		}
		this.Update();
	}

	// Token: 0x06003CDF RID: 15583 RVA: 0x0010CD90 File Offset: 0x0010AF90
	private void OnEnable()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.onScreenResize));
	}

	// Token: 0x06003CE0 RID: 15584 RVA: 0x0010CDC0 File Offset: 0x0010AFC0
	private void OnDisable()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.onScreenResize));
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x0010CDF0 File Offset: 0x0010AFF0
	private void onScreenResize()
	{
		this.screenWidth = Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
	}

	// Token: 0x04002834 RID: 10292
	public UISprite targetPic;

	// Token: 0x04002835 RID: 10293
	public UISprite ArrowPic;

	// Token: 0x04002836 RID: 10294
	public UILabel DisLabel;

	// Token: 0x04002837 RID: 10295
	public GameObject mTargetObj;

	// Token: 0x04002838 RID: 10296
	private int screenWidth;

	// Token: 0x04002839 RID: 10297
	private Camera mMainCamera;

	// Token: 0x0400283A RID: 10298
	private GameObject mSourceObj;

	// Token: 0x0400283B RID: 10299
	private Vector3 tartgetPos;

	// Token: 0x0400283C RID: 10300
	private bool isNeedUpdate;

	// Token: 0x0400283D RID: 10301
	private Transform targetTransform;

	// Token: 0x0400283E RID: 10302
	private Transform sourcesTransform;

	// Token: 0x0400283F RID: 10303
	private StringBuilder sb = new StringBuilder();
}
