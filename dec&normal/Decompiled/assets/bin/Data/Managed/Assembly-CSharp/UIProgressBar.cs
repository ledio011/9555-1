using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000067 RID: 103
[AddComponentMenu("NGUI/Interaction/NGUI Progress Bar")]
[ExecuteInEditMode]
public class UIProgressBar : UIWidgetContainer
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060001FB RID: 507 RVA: 0x0000D774 File Offset: 0x0000B974
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060001FC RID: 508 RVA: 0x0000D79C File Offset: 0x0000B99C
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060001FD RID: 509 RVA: 0x0000D7CC File Offset: 0x0000B9CC
	// (set) Token: 0x060001FE RID: 510 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
	public UIWidget foregroundWidget
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060001FF RID: 511 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
	// (set) Token: 0x06000200 RID: 512 RVA: 0x0000D800 File Offset: 0x0000BA00
	public UIWidget backgroundWidget
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000201 RID: 513 RVA: 0x0000D824 File Offset: 0x0000BA24
	// (set) Token: 0x06000202 RID: 514 RVA: 0x0000D82C File Offset: 0x0000BA2C
	public UIProgressBar.FillDirection fillDirection
	{
		get
		{
			return this.mFill;
		}
		set
		{
			if (this.mFill != value)
			{
				this.mFill = value;
				this.ForceUpdate();
			}
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000203 RID: 515 RVA: 0x0000D848 File Offset: 0x0000BA48
	// (set) Token: 0x06000204 RID: 516 RVA: 0x0000D888 File Offset: 0x0000BA88
	public float value
	{
		get
		{
			if (this.numberOfSteps > 1)
			{
				return Mathf.Round(this.mValue * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
			}
			return this.mValue;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mValue != num)
			{
				float value2 = this.value;
				this.mValue = num;
				if (value2 != this.value)
				{
					this.ForceUpdate();
					if (UIProgressBar.current == null && NGUITools.GetActive(this) && EventDelegate.IsValid(this.onChange))
					{
						UIProgressBar.current = this;
						EventDelegate.Execute(this.onChange);
						UIProgressBar.current = null;
					}
				}
			}
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000205 RID: 517 RVA: 0x0000D90C File Offset: 0x0000BB0C
	// (set) Token: 0x06000206 RID: 518 RVA: 0x0000D958 File Offset: 0x0000BB58
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 1f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				if (this.mFG.collider != null)
				{
					this.mFG.collider.enabled = (this.mFG.alpha > 0.001f);
				}
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				if (this.mBG.collider != null)
				{
					this.mBG.collider.enabled = (this.mBG.alpha > 0.001f);
				}
			}
			if (this.thumb != null)
			{
				UIWidget component = this.thumb.GetComponent<UIWidget>();
				if (component != null)
				{
					component.alpha = value;
					if (component.collider != null)
					{
						component.collider.enabled = (component.alpha > 0.001f);
					}
				}
			}
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000207 RID: 519 RVA: 0x0000DA68 File Offset: 0x0000BC68
	protected bool isHorizontal
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.LeftToRight || this.mFill == UIProgressBar.FillDirection.RightToLeft;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000208 RID: 520 RVA: 0x0000DA84 File Offset: 0x0000BC84
	protected bool isInverted
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.RightToLeft || this.mFill == UIProgressBar.FillDirection.TopToBottom;
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
	protected void Start()
	{
		this.Upgrade();
		if (Application.isPlaying)
		{
			if (this.mFG == null)
			{
				Debug.LogWarning("Progress bar needs a foreground widget to work with", this);
				base.enabled = false;
				return;
			}
			if (this.mBG != null)
			{
				this.mBG.autoResizeBoxCollider = true;
			}
			this.OnStart();
			if (UIProgressBar.current == null && this.onChange != null)
			{
				UIProgressBar.current = this;
				EventDelegate.Execute(this.onChange);
				UIProgressBar.current = null;
			}
		}
		this.ForceUpdate();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000DB3C File Offset: 0x0000BD3C
	protected virtual void Upgrade()
	{
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000DB40 File Offset: 0x0000BD40
	protected virtual void OnStart()
	{
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000DB44 File Offset: 0x0000BD44
	protected void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000DB58 File Offset: 0x0000BD58
	protected void OnValidate()
	{
		if (NGUITools.GetActive(this))
		{
			this.Upgrade();
			this.mIsDirty = true;
			float num = Mathf.Clamp01(this.mValue);
			if (this.mValue != num)
			{
				this.mValue = num;
			}
			if (this.numberOfSteps < 0)
			{
				this.numberOfSteps = 0;
			}
			else if (this.numberOfSteps > 20)
			{
				this.numberOfSteps = 20;
			}
			this.ForceUpdate();
		}
		else
		{
			float num2 = Mathf.Clamp01(this.mValue);
			if (this.mValue != num2)
			{
				this.mValue = num2;
			}
			if (this.numberOfSteps < 0)
			{
				this.numberOfSteps = 0;
			}
			else if (this.numberOfSteps > 20)
			{
				this.numberOfSteps = 20;
			}
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000DC20 File Offset: 0x0000BE20
	protected float ScreenToValue(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane;
		plane..ctor(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			return this.value;
		}
		return this.LocalToValue(cachedTransform.InverseTransformPoint(ray.GetPoint(num)));
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000DC94 File Offset: 0x0000BE94
	protected virtual float LocalToValue(Vector2 localPos)
	{
		if (!(this.mFG != null))
		{
			return this.value;
		}
		Vector3[] localCorners = this.mFG.localCorners;
		Vector3 vector = localCorners[2] - localCorners[0];
		if (this.isHorizontal)
		{
			float num = (localPos.x - localCorners[0].x) / vector.x;
			return (!this.isInverted) ? num : (1f - num);
		}
		float num2 = (localPos.y - localCorners[0].y) / vector.y;
		return (!this.isInverted) ? num2 : (1f - num2);
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000DD5C File Offset: 0x0000BF5C
	public virtual void ForceUpdate()
	{
		this.mIsDirty = false;
		if (this.mFG != null)
		{
			UISprite uisprite = this.mFG as UISprite;
			if (this.isHorizontal)
			{
				if (uisprite != null && uisprite.type == UISprite.Type.Filled)
				{
					uisprite.fillDirection = UISprite.FillDirection.Horizontal;
					uisprite.invert = this.isInverted;
					uisprite.fillAmount = this.value;
				}
				else
				{
					this.mFG.drawRegion = ((!this.isInverted) ? new Vector4(0f, 0f, this.value, 1f) : new Vector4(1f - this.value, 0f, 1f, 1f));
				}
			}
			else if (uisprite != null && uisprite.type == UISprite.Type.Filled)
			{
				uisprite.fillDirection = UISprite.FillDirection.Vertical;
				uisprite.invert = this.isInverted;
				uisprite.fillAmount = this.value;
			}
			else
			{
				this.mFG.drawRegion = ((!this.isInverted) ? new Vector4(0f, 0f, 1f, this.value) : new Vector4(0f, 1f - this.value, 1f, 1f));
			}
		}
		if (this.thumb != null && (this.mFG != null || this.mBG != null))
		{
			Vector3[] array = (!(this.mFG != null)) ? this.mBG.localCorners : this.mFG.localCorners;
			Vector4 vector = (!(this.mFG != null)) ? this.mBG.border : this.mFG.border;
			Vector3[] array2 = array;
			int num = 0;
			array2[num].x = array2[num].x + vector.x;
			Vector3[] array3 = array;
			int num2 = 1;
			array3[num2].x = array3[num2].x + vector.x;
			Vector3[] array4 = array;
			int num3 = 2;
			array4[num3].x = array4[num3].x - vector.z;
			Vector3[] array5 = array;
			int num4 = 3;
			array5[num4].x = array5[num4].x - vector.z;
			Vector3[] array6 = array;
			int num5 = 0;
			array6[num5].y = array6[num5].y + vector.y;
			Vector3[] array7 = array;
			int num6 = 1;
			array7[num6].y = array7[num6].y - vector.w;
			Vector3[] array8 = array;
			int num7 = 2;
			array8[num7].y = array8[num7].y - vector.w;
			Vector3[] array9 = array;
			int num8 = 3;
			array9[num8].y = array9[num8].y + vector.y;
			Transform transform = (!(this.mFG != null)) ? this.mBG.cachedTransform : this.mFG.cachedTransform;
			for (int i = 0; i < 4; i++)
			{
				array[i] = transform.TransformPoint(array[i]);
			}
			if (this.isHorizontal)
			{
				Vector3 vector2 = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 vector3 = Vector3.Lerp(array[2], array[3], 0.5f);
				this.SetThumbPosition(Vector3.Lerp(vector2, vector3, (!this.isInverted) ? this.value : (1f - this.value)));
			}
			else
			{
				Vector3 vector4 = Vector3.Lerp(array[0], array[3], 0.5f);
				Vector3 vector5 = Vector3.Lerp(array[1], array[2], 0.5f);
				this.SetThumbPosition(Vector3.Lerp(vector4, vector5, (!this.isInverted) ? this.value : (1f - this.value)));
			}
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000E184 File Offset: 0x0000C384
	protected void SetThumbPosition(Vector3 worldPos)
	{
		Transform parent = this.thumb.parent;
		if (parent != null)
		{
			worldPos = parent.InverseTransformPoint(worldPos);
			worldPos.x = Mathf.Round(worldPos.x);
			worldPos.y = Mathf.Round(worldPos.y);
			worldPos.z = 0f;
			if (Vector3.Distance(this.thumb.localPosition, worldPos) > 0.001f)
			{
				this.thumb.localPosition = worldPos;
			}
		}
		else if (Vector3.Distance(this.thumb.position, worldPos) > 1E-05f)
		{
			this.thumb.position = worldPos;
		}
	}

	// Token: 0x0400023F RID: 575
	public static UIProgressBar current;

	// Token: 0x04000240 RID: 576
	public UIProgressBar.OnDragFinished onDragFinished;

	// Token: 0x04000241 RID: 577
	public Transform thumb;

	// Token: 0x04000242 RID: 578
	[HideInInspector]
	[SerializeField]
	protected UIWidget mBG;

	// Token: 0x04000243 RID: 579
	[SerializeField]
	[HideInInspector]
	protected UIWidget mFG;

	// Token: 0x04000244 RID: 580
	[HideInInspector]
	[SerializeField]
	protected float mValue = 1f;

	// Token: 0x04000245 RID: 581
	[HideInInspector]
	[SerializeField]
	protected UIProgressBar.FillDirection mFill;

	// Token: 0x04000246 RID: 582
	protected Transform mTrans;

	// Token: 0x04000247 RID: 583
	protected bool mIsDirty;

	// Token: 0x04000248 RID: 584
	protected Camera mCam;

	// Token: 0x04000249 RID: 585
	protected float mOffset;

	// Token: 0x0400024A RID: 586
	public int numberOfSteps;

	// Token: 0x0400024B RID: 587
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x02000068 RID: 104
	public enum FillDirection
	{
		// Token: 0x0400024D RID: 589
		LeftToRight,
		// Token: 0x0400024E RID: 590
		RightToLeft,
		// Token: 0x0400024F RID: 591
		BottomToTop,
		// Token: 0x04000250 RID: 592
		TopToBottom
	}

	// Token: 0x02000A97 RID: 2711
	// (Invoke) Token: 0x06004EE5 RID: 20197
	public delegate void OnDragFinished();
}
