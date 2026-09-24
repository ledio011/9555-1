using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[AddComponentMenu("NGUI/UI/NGUI Panel")]
[ExecuteInEditMode]
public class UIPanel : UIRect
{
	// Token: 0x17000118 RID: 280
	// (get) Token: 0x0600061D RID: 1565 RVA: 0x00029838 File Offset: 0x00027A38
	public static int nextUnusedDepth
	{
		get
		{
			int num = int.MinValue;
			for (int i = 0; i < UIPanel.list.size; i++)
			{
				num = Mathf.Max(num, UIPanel.list[i].depth);
			}
			return (num != int.MinValue) ? (num + 1) : 0;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x00029894 File Offset: 0x00027A94
	public override bool canBeAnchored
	{
		get
		{
			return this.mClipping != UIDrawCall.Clipping.None;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x0600061F RID: 1567 RVA: 0x000298A4 File Offset: 0x00027AA4
	// (set) Token: 0x06000620 RID: 1568 RVA: 0x000298AC File Offset: 0x00027AAC
	public override float alpha
	{
		get
		{
			return this.mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mAlpha != num)
			{
				this.mAlphaFrameID = -1;
				this.mResized = true;
				this.mAlpha = num;
				this.SetDirty();
			}
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x000298E8 File Offset: 0x00027AE8
	// (set) Token: 0x06000622 RID: 1570 RVA: 0x000298F0 File Offset: 0x00027AF0
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				UIPanel.list.Sort(new BetterList<UIPanel>.CompareFunc(UIPanel.CompareFunc));
			}
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000623 RID: 1571 RVA: 0x0002991C File Offset: 0x00027B1C
	// (set) Token: 0x06000624 RID: 1572 RVA: 0x00029924 File Offset: 0x00027B24
	public int sortingOrder
	{
		get
		{
			return this.mSortingOrder;
		}
		set
		{
			if (this.mSortingOrder != value)
			{
				this.mSortingOrder = value;
				this.UpdateDrawCalls();
			}
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00029940 File Offset: 0x00027B40
	public static int CompareFunc(UIPanel a, UIPanel b)
	{
		if (!(a != b) || !(a != null) || !(b != null))
		{
			return 0;
		}
		if (a.mDepth < b.mDepth)
		{
			return -1;
		}
		if (a.mDepth > b.mDepth)
		{
			return 1;
		}
		return (a.GetInstanceID() >= b.GetInstanceID()) ? 1 : -1;
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000626 RID: 1574 RVA: 0x000299B4 File Offset: 0x00027BB4
	public float width
	{
		get
		{
			return this.GetViewSize().x;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000627 RID: 1575 RVA: 0x000299D0 File Offset: 0x00027BD0
	public float height
	{
		get
		{
			return this.GetViewSize().y;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x000299EC File Offset: 0x00027BEC
	public bool halfPixelOffset
	{
		get
		{
			return this.mHalfPixelOffset;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x000299F4 File Offset: 0x00027BF4
	public bool usedForUI
	{
		get
		{
			return this.mCam != null && this.mCam.isOrthoGraphic;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x0600062A RID: 1578 RVA: 0x00029A18 File Offset: 0x00027C18
	public Vector3 drawCallOffset
	{
		get
		{
			if (this.mHalfPixelOffset && this.mCam != null && this.mCam.isOrthoGraphic)
			{
				float num = 1f / this.GetWindowSize().y / this.mCam.orthographicSize;
				return new Vector3(-num, num);
			}
			return Vector3.zero;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x00029A80 File Offset: 0x00027C80
	// (set) Token: 0x0600062C RID: 1580 RVA: 0x00029A88 File Offset: 0x00027C88
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mResized = true;
				this.mClipping = value;
				this.mMatrixFrame = -1;
			}
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x0600062D RID: 1581 RVA: 0x00029AAC File Offset: 0x00027CAC
	public UIPanel parentPanel
	{
		get
		{
			return this.mParentPanel;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x0600062E RID: 1582 RVA: 0x00029AB4 File Offset: 0x00027CB4
	public int clipCount
	{
		get
		{
			int num = 0;
			UIPanel uipanel = this;
			while (uipanel != null)
			{
				if (uipanel.mClipping == UIDrawCall.Clipping.SoftClip)
				{
					num++;
				}
				uipanel = uipanel.mParentPanel;
			}
			return num;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x0600062F RID: 1583 RVA: 0x00029AF0 File Offset: 0x00027CF0
	public bool hasClipping
	{
		get
		{
			return this.mClipping == UIDrawCall.Clipping.SoftClip;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000630 RID: 1584 RVA: 0x00029AFC File Offset: 0x00027CFC
	public bool hasCumulativeClipping
	{
		get
		{
			return this.clipCount != 0;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000631 RID: 1585 RVA: 0x00029B0C File Offset: 0x00027D0C
	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool clipsChildren
	{
		get
		{
			return this.hasCumulativeClipping;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000632 RID: 1586 RVA: 0x00029B14 File Offset: 0x00027D14
	// (set) Token: 0x06000633 RID: 1587 RVA: 0x00029B1C File Offset: 0x00027D1C
	public Vector2 clipOffset
	{
		get
		{
			return this.mClipOffset;
		}
		set
		{
			if (Mathf.Abs(this.mClipOffset.x - value.x) > 0.001f || Mathf.Abs(this.mClipOffset.y - value.y) > 0.001f)
			{
				this.mResized = true;
				this.mCullTime = ((this.mCullTime != 0f) ? (RealTime.time + 0.15f) : 0.001f);
				this.mClipOffset = value;
				this.mMatrixFrame = -1;
				if (this.onClipMove != null)
				{
					this.onClipMove(this);
				}
			}
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000634 RID: 1588 RVA: 0x00029BC4 File Offset: 0x00027DC4
	// (set) Token: 0x06000635 RID: 1589 RVA: 0x00029BCC File Offset: 0x00027DCC
	[Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
	public Vector4 clipRange
	{
		get
		{
			return this.baseClipRegion;
		}
		set
		{
			this.baseClipRegion = value;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000636 RID: 1590 RVA: 0x00029BD8 File Offset: 0x00027DD8
	// (set) Token: 0x06000637 RID: 1591 RVA: 0x00029BE0 File Offset: 0x00027DE0
	public Vector4 baseClipRegion
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (Mathf.Abs(this.mClipRange.x - value.x) > 0.001f || Mathf.Abs(this.mClipRange.y - value.y) > 0.001f || Mathf.Abs(this.mClipRange.z - value.z) > 0.001f || Mathf.Abs(this.mClipRange.w - value.w) > 0.001f)
			{
				this.mResized = true;
				this.mCullTime = ((this.mCullTime != 0f) ? (RealTime.time + 0.15f) : 0.001f);
				this.mClipRange = value;
				this.mMatrixFrame = -1;
				UIScrollView component = base.GetComponent<UIScrollView>();
				if (component != null)
				{
					component.UpdatePosition();
				}
				if (this.onClipMove != null)
				{
					this.onClipMove(this);
				}
			}
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000638 RID: 1592 RVA: 0x00029CE8 File Offset: 0x00027EE8
	public Vector4 finalClipRegion
	{
		get
		{
			Vector2 viewSize = this.GetViewSize();
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				return new Vector4(this.mClipRange.x + this.mClipOffset.x, this.mClipRange.y + this.mClipOffset.y, viewSize.x, viewSize.y);
			}
			return new Vector4(0f, 0f, viewSize.x, viewSize.y);
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000639 RID: 1593 RVA: 0x00029D68 File Offset: 0x00027F68
	// (set) Token: 0x0600063A RID: 1594 RVA: 0x00029D70 File Offset: 0x00027F70
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
			}
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x0600063B RID: 1595 RVA: 0x00029D8C File Offset: 0x00027F8C
	public override Vector3[] localCorners
	{
		get
		{
			if (this.mClipping == UIDrawCall.Clipping.None)
			{
				Vector2 viewSize = this.GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float num3 = num + viewSize.x;
				float num4 = num2 + viewSize.y;
				Transform transform = (!(this.mCam != null)) ? null : this.mCam.transform;
				if (transform != null)
				{
					UIPanel.mCorners[0] = transform.TransformPoint(num, num2, 0f);
					UIPanel.mCorners[1] = transform.TransformPoint(num, num4, 0f);
					UIPanel.mCorners[2] = transform.TransformPoint(num3, num4, 0f);
					UIPanel.mCorners[3] = transform.TransformPoint(num3, num2, 0f);
					transform = base.cachedTransform;
					for (int i = 0; i < 4; i++)
					{
						UIPanel.mCorners[i] = transform.InverseTransformPoint(UIPanel.mCorners[i]);
					}
				}
				else
				{
					UIPanel.mCorners[0] = new Vector3(num, num2);
					UIPanel.mCorners[1] = new Vector3(num, num4);
					UIPanel.mCorners[2] = new Vector3(num3, num4);
					UIPanel.mCorners[3] = new Vector3(num3, num2);
				}
			}
			else
			{
				float num5 = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
				float num6 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
				float num7 = num5 + this.mClipRange.z;
				float num8 = num6 + this.mClipRange.w;
				UIPanel.mCorners[0] = new Vector3(num5, num6);
				UIPanel.mCorners[1] = new Vector3(num5, num8);
				UIPanel.mCorners[2] = new Vector3(num7, num8);
				UIPanel.mCorners[3] = new Vector3(num7, num6);
			}
			return UIPanel.mCorners;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x0002A018 File Offset: 0x00028218
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.mClipping == UIDrawCall.Clipping.None)
			{
				Vector2 viewSize = this.GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float num3 = num + viewSize.x;
				float num4 = num2 + viewSize.y;
				Transform transform = (!(this.mCam != null)) ? null : this.mCam.transform;
				if (transform != null)
				{
					UIPanel.mCorners[0] = transform.TransformPoint(num, num2, 0f);
					UIPanel.mCorners[1] = transform.TransformPoint(num, num4, 0f);
					UIPanel.mCorners[2] = transform.TransformPoint(num3, num4, 0f);
					UIPanel.mCorners[3] = transform.TransformPoint(num3, num2, 0f);
				}
			}
			else
			{
				float num5 = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
				float num6 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
				float num7 = num5 + this.mClipRange.z;
				float num8 = num6 + this.mClipRange.w;
				Transform cachedTransform = base.cachedTransform;
				UIPanel.mCorners[0] = cachedTransform.TransformPoint(num5, num6, 0f);
				UIPanel.mCorners[1] = cachedTransform.TransformPoint(num5, num8, 0f);
				UIPanel.mCorners[2] = cachedTransform.TransformPoint(num7, num8, 0f);
				UIPanel.mCorners[3] = cachedTransform.TransformPoint(num7, num6, 0f);
			}
			return UIPanel.mCorners;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0002A21C File Offset: 0x0002841C
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.mClipping != UIDrawCall.Clipping.None || this.anchorOffset)
		{
			Vector2 viewSize = this.GetViewSize();
			Vector2 vector = (this.mClipping == UIDrawCall.Clipping.None) ? Vector2.zero : (this.mClipRange + this.mClipOffset);
			float num = vector.x - 0.5f * viewSize.x;
			float num2 = vector.y - 0.5f * viewSize.y;
			float num3 = num + viewSize.x;
			float num4 = num2 + viewSize.y;
			float num5 = (num + num3) * 0.5f;
			float num6 = (num2 + num4) * 0.5f;
			Matrix4x4 localToWorldMatrix = base.cachedTransform.localToWorldMatrix;
			UIPanel.mCorners[0] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num, num6));
			UIPanel.mCorners[1] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num5, num4));
			UIPanel.mCorners[2] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num3, num6));
			UIPanel.mCorners[3] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num5, num2));
			if (relativeTo != null)
			{
				for (int i = 0; i < 4; i++)
				{
					UIPanel.mCorners[i] = relativeTo.InverseTransformPoint(UIPanel.mCorners[i]);
				}
			}
			return UIPanel.mCorners;
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0002A3B0 File Offset: 0x000285B0
	public override void Invalidate(bool includeChildren)
	{
		this.mAlphaFrameID = -1;
		base.Invalidate(includeChildren);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0002A3C0 File Offset: 0x000285C0
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			UIRect parent = base.parent;
			this.finalAlpha = ((!(base.parent != null)) ? this.mAlpha : (parent.CalculateFinalAlpha(frameID) * this.mAlpha));
		}
		return this.finalAlpha;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002A420 File Offset: 0x00028620
	public override void SetRect(float x, float y, float width, float height)
	{
		int num = Mathf.FloorToInt(width + 0.5f);
		int num2 = Mathf.FloorToInt(height + 0.5f);
		num = num >> 1 << 1;
		num2 = num2 >> 1 << 1;
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(x + 0.5f);
		localPosition.y = Mathf.Floor(y + 0.5f);
		if (num < 2)
		{
			num = 2;
		}
		if (num2 < 2)
		{
			num2 = 2;
		}
		this.baseClipRegion = new Vector4(localPosition.x, localPosition.y, (float)num, (float)num2);
		if (base.isAnchored)
		{
			transform = transform.parent;
			if (this.leftAnchor.target)
			{
				this.leftAnchor.SetHorizontal(transform, x);
			}
			if (this.rightAnchor.target)
			{
				this.rightAnchor.SetHorizontal(transform, x + width);
			}
			if (this.bottomAnchor.target)
			{
				this.bottomAnchor.SetVertical(transform, y);
			}
			if (this.topAnchor.target)
			{
				this.topAnchor.SetVertical(transform, y + height);
			}
		}
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002A558 File Offset: 0x00028758
	public bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.worldToLocal.MultiplyPoint3x4(a);
		b = this.worldToLocal.MultiplyPoint3x4(b);
		c = this.worldToLocal.MultiplyPoint3x4(c);
		d = this.worldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0002A690 File Offset: 0x00028890
	public bool IsVisible(Vector3 worldPos)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip)
		{
			return true;
		}
		this.UpdateTransformMatrix();
		Vector3 vector = this.worldToLocal.MultiplyPoint3x4(worldPos);
		return vector.x >= this.mMin.x && vector.y >= this.mMin.y && vector.x <= this.mMax.x && vector.y <= this.mMax.y;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0002A740 File Offset: 0x00028940
	public bool IsVisible(UIWidget w)
	{
		if ((this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip) && !w.hideIfOffScreen && (this.mParentPanel == null || this.clipCount == 0))
		{
			return true;
		}
		UIPanel uipanel = this;
		Vector3[] worldCorners = w.worldCorners;
		while (uipanel != null)
		{
			if (!this.IsVisible(worldCorners[0], worldCorners[1], worldCorners[2], worldCorners[3]))
			{
				return false;
			}
			uipanel = uipanel.mParentPanel;
		}
		return true;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0002A7EC File Offset: 0x000289EC
	public bool Affects(UIWidget w)
	{
		if (w == null)
		{
			return false;
		}
		UIPanel panel = w.panel;
		if (panel == null)
		{
			return false;
		}
		UIPanel uipanel = this;
		while (uipanel != null)
		{
			if (uipanel == panel)
			{
				return true;
			}
			if (!uipanel.hasCumulativeClipping)
			{
				return false;
			}
			uipanel = uipanel.mParentPanel;
		}
		return false;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0002A854 File Offset: 0x00028A54
	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		this.mRebuild = true;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0002A860 File Offset: 0x00028A60
	public void SetDirty()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			this.drawCalls.buffer[i].isDirty = true;
		}
		this.Invalidate(true);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0002A8A4 File Offset: 0x00028AA4
	private void Awake()
	{
		this.mGo = base.gameObject;
		this.mTrans = base.transform;
		this.mHalfPixelOffset = (Application.platform == 2 || Application.platform == 10 || Application.platform == 5 || Application.platform == 7);
		if (this.mHalfPixelOffset)
		{
			this.mHalfPixelOffset = (SystemInfo.graphicsShaderLevel < 40);
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0002A918 File Offset: 0x00028B18
	protected override void OnEnable()
	{
		base.OnEnable();
		Transform parent = base.cachedTransform.parent;
		this.mParentPanel = ((!(parent != null)) ? null : NGUITools.FindInParents<UIPanel>(parent.gameObject));
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0002A95C File Offset: 0x00028B5C
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		Transform parent = base.cachedTransform.parent;
		this.mParentPanel = ((!(parent != null)) ? null : NGUITools.FindInParents<UIPanel>(parent.gameObject));
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0002A9A0 File Offset: 0x00028BA0
	protected override void OnStart()
	{
		this.mLayer = this.mGo.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0002A9F4 File Offset: 0x00028BF4
	protected override void OnInit()
	{
		base.OnInit();
		if (base.rigidbody == null)
		{
			Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		}
		this.mRebuild = true;
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		UIPanel.list.Add(this);
		UIPanel.list.Sort(new BetterList<UIPanel>.CompareFunc(UIPanel.CompareFunc));
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0002AA68 File Offset: 0x00028C68
	protected override void OnDisable()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			if (uidrawCall != null)
			{
				UIDrawCall.Destroy(uidrawCall);
			}
		}
		this.drawCalls.Clear();
		UIPanel.list.Remove(this);
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		if (UIPanel.list.size == 0)
		{
			UIDrawCall.ReleaseAll();
			UIPanel.mUpdateFrame = -1;
		}
		base.OnDisable();
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0002AAF8 File Offset: 0x00028CF8
	private void UpdateTransformMatrix()
	{
		int frameCount = Time.frameCount;
		if (this.mMatrixFrame != frameCount)
		{
			this.mMatrixFrame = frameCount;
			this.worldToLocal = base.cachedTransform.worldToLocalMatrix;
			Vector2 vector = this.GetViewSize() * 0.5f;
			float num = this.mClipOffset.x + this.mClipRange.x;
			float num2 = this.mClipOffset.y + this.mClipRange.y;
			this.mMin.x = num - vector.x;
			this.mMin.y = num2 - vector.y;
			this.mMax.x = num + vector.x;
			this.mMax.y = num2 + vector.y;
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002ABC0 File Offset: 0x00028DC0
	protected override void OnAnchor()
	{
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return;
		}
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector2 viewSize = this.GetViewSize();
		Vector2 vector = cachedTransform.localPosition;
		float num;
		float num2;
		float num3;
		float num4;
		if (this.leftAnchor.target == this.bottomAnchor.target && this.leftAnchor.target == this.rightAnchor.target && this.leftAnchor.target == this.topAnchor.target)
		{
			Vector3[] sides = this.leftAnchor.GetSides(parent);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
			}
			else
			{
				Vector2 vector2 = base.GetLocalPos(this.leftAnchor, parent);
				num = vector2.x + (float)this.leftAnchor.absolute;
				num3 = vector2.y + (float)this.bottomAnchor.absolute;
				num2 = vector2.x + (float)this.rightAnchor.absolute;
				num4 = vector2.y + (float)this.topAnchor.absolute;
			}
		}
		else
		{
			if (this.leftAnchor.target)
			{
				Vector3[] sides2 = this.leftAnchor.GetSides(parent);
				if (sides2 != null)
				{
					num = NGUIMath.Lerp(sides2[0].x, sides2[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				}
				else
				{
					num = base.GetLocalPos(this.leftAnchor, parent).x + (float)this.leftAnchor.absolute;
				}
			}
			else
			{
				num = this.mClipRange.x - 0.5f * viewSize.x;
			}
			if (this.rightAnchor.target)
			{
				Vector3[] sides3 = this.rightAnchor.GetSides(parent);
				if (sides3 != null)
				{
					num2 = NGUIMath.Lerp(sides3[0].x, sides3[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				}
				else
				{
					num2 = base.GetLocalPos(this.rightAnchor, parent).x + (float)this.rightAnchor.absolute;
				}
			}
			else
			{
				num2 = this.mClipRange.x + 0.5f * viewSize.x;
			}
			if (this.bottomAnchor.target)
			{
				Vector3[] sides4 = this.bottomAnchor.GetSides(parent);
				if (sides4 != null)
				{
					num3 = NGUIMath.Lerp(sides4[3].y, sides4[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				}
				else
				{
					num3 = base.GetLocalPos(this.bottomAnchor, parent).y + (float)this.bottomAnchor.absolute;
				}
			}
			else
			{
				num3 = this.mClipRange.y - 0.5f * viewSize.y;
			}
			if (this.topAnchor.target)
			{
				Vector3[] sides5 = this.topAnchor.GetSides(parent);
				if (sides5 != null)
				{
					num4 = NGUIMath.Lerp(sides5[3].y, sides5[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
				}
				else
				{
					num4 = base.GetLocalPos(this.topAnchor, parent).y + (float)this.topAnchor.absolute;
				}
			}
			else
			{
				num4 = this.mClipRange.y + 0.5f * viewSize.y;
			}
		}
		num -= vector.x + this.mClipOffset.x;
		num2 -= vector.x + this.mClipOffset.x;
		num3 -= vector.y + this.mClipOffset.y;
		num4 -= vector.y + this.mClipOffset.y;
		float num5 = Mathf.Lerp(num, num2, 0.5f);
		float num6 = Mathf.Lerp(num3, num4, 0.5f);
		float num7 = num2 - num;
		float num8 = num4 - num3;
		float num9 = Mathf.Max(20f, this.mClipSoftness.x);
		float num10 = Mathf.Max(20f, this.mClipSoftness.y);
		if (num7 < num9)
		{
			num7 = num9;
		}
		if (num8 < num10)
		{
			num8 = num10;
		}
		this.baseClipRegion = new Vector4(num5, num6, num7, num8);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0002B168 File Offset: 0x00029368
	private void LateUpdate()
	{
		if (UIPanel.mUpdateFrame != Time.frameCount)
		{
			UIPanel.mUpdateFrame = Time.frameCount;
			for (int i = 0; i < UIPanel.list.size; i++)
			{
				UIPanel.list[i].UpdateSelf();
			}
			int num = 3000;
			for (int j = 0; j < UIPanel.list.size; j++)
			{
				UIPanel uipanel = UIPanel.list.buffer[j];
				if (uipanel.renderQueue == UIPanel.RenderQueue.Automatic)
				{
					uipanel.startingRenderQueue = num;
					uipanel.UpdateDrawCalls();
					num += uipanel.drawCalls.size;
				}
				else if (uipanel.renderQueue == UIPanel.RenderQueue.StartAt)
				{
					uipanel.UpdateDrawCalls();
					if (uipanel.drawCalls.size != 0)
					{
						num = Mathf.Max(num, uipanel.startingRenderQueue + uipanel.drawCalls.size);
					}
				}
				else
				{
					uipanel.UpdateDrawCalls();
					if (uipanel.drawCalls.size != 0)
					{
						num = Mathf.Max(num, uipanel.startingRenderQueue + 1);
					}
				}
			}
		}
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0002B278 File Offset: 0x00029478
	private void UpdateSelf()
	{
		this.mUpdateTime = RealTime.time;
		this.UpdateTransformMatrix();
		this.UpdateLayers();
		this.UpdateWidgets();
		if (this.mRebuild)
		{
			this.mRebuild = false;
			this.FillAllDrawCalls();
		}
		else
		{
			int i = 0;
			while (i < this.drawCalls.size)
			{
				UIDrawCall uidrawCall = this.drawCalls.buffer[i];
				if (uidrawCall.isDirty && !this.FillDrawCall(uidrawCall))
				{
					UIDrawCall.Destroy(uidrawCall);
					this.drawCalls.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0002B318 File Offset: 0x00029518
	public void SortWidgets()
	{
		this.mSortWidgets = false;
		this.widgets.Sort(new BetterList<UIWidget>.CompareFunc(UIWidget.PanelCompareFunc));
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0002B338 File Offset: 0x00029538
	private void FillAllDrawCalls()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall.Destroy(this.drawCalls.buffer[i]);
		}
		this.drawCalls.Clear();
		Material material = null;
		Texture texture = null;
		Shader shader = null;
		UIDrawCall uidrawCall = null;
		if (this.mSortWidgets)
		{
			this.SortWidgets();
		}
		for (int j = 0; j < this.widgets.size; j++)
		{
			UIWidget uiwidget = this.widgets.buffer[j];
			if (uiwidget.isVisible && uiwidget.hasVertices)
			{
				Material material2 = uiwidget.material;
				Texture mainTexture = uiwidget.mainTexture;
				Shader shader2 = uiwidget.shader;
				if (material != material2 || texture != mainTexture || shader != shader2)
				{
					if (uidrawCall != null && uidrawCall.verts.size != 0)
					{
						this.drawCalls.Add(uidrawCall);
						uidrawCall.UpdateGeometry();
						uidrawCall.onRender = this.mOnRender;
						this.mOnRender = null;
						uidrawCall = null;
					}
					material = material2;
					texture = mainTexture;
					shader = shader2;
				}
				if (material != null || shader != null || texture != null)
				{
					if (uidrawCall == null)
					{
						uidrawCall = UIDrawCall.Create(this, material, texture, shader);
						uidrawCall.depthStart = uiwidget.depth;
						uidrawCall.depthEnd = uidrawCall.depthStart;
						uidrawCall.panel = this;
					}
					else
					{
						int depth = uiwidget.depth;
						if (depth < uidrawCall.depthStart)
						{
							uidrawCall.depthStart = depth;
						}
						if (depth > uidrawCall.depthEnd)
						{
							uidrawCall.depthEnd = depth;
						}
					}
					uiwidget.drawCall = uidrawCall;
					if (this.generateNormals)
					{
						uiwidget.WriteToBuffers(uidrawCall.verts, uidrawCall.uvs, uidrawCall.cols, uidrawCall.norms, uidrawCall.tans);
					}
					else
					{
						uiwidget.WriteToBuffers(uidrawCall.verts, uidrawCall.uvs, uidrawCall.cols, null, null);
					}
					if (uiwidget.mOnRender != null)
					{
						if (this.mOnRender == null)
						{
							this.mOnRender = uiwidget.mOnRender;
						}
						else
						{
							this.mOnRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(this.mOnRender, uiwidget.mOnRender);
						}
					}
				}
			}
			else
			{
				uiwidget.drawCall = null;
			}
		}
		if (uidrawCall != null && uidrawCall.verts.size != 0)
		{
			this.drawCalls.Add(uidrawCall);
			uidrawCall.UpdateGeometry();
			uidrawCall.onRender = this.mOnRender;
			this.mOnRender = null;
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0002B60C File Offset: 0x0002980C
	private bool FillDrawCall(UIDrawCall dc)
	{
		if (dc != null)
		{
			dc.isDirty = false;
			int i = 0;
			while (i < this.widgets.size)
			{
				UIWidget uiwidget = this.widgets[i];
				if (uiwidget == null)
				{
					this.widgets.RemoveAt(i);
				}
				else
				{
					if (uiwidget.drawCall == dc)
					{
						if (uiwidget.isVisible && uiwidget.hasVertices)
						{
							if (this.generateNormals)
							{
								uiwidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, dc.norms, dc.tans);
							}
							else
							{
								uiwidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, null, null);
							}
							if (uiwidget.mOnRender != null)
							{
								if (this.mOnRender == null)
								{
									this.mOnRender = uiwidget.mOnRender;
								}
								else
								{
									this.mOnRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(this.mOnRender, uiwidget.mOnRender);
								}
							}
						}
						else
						{
							uiwidget.drawCall = null;
						}
					}
					i++;
				}
			}
			if (dc.verts.size != 0)
			{
				dc.UpdateGeometry();
				dc.onRender = this.mOnRender;
				this.mOnRender = null;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0002B764 File Offset: 0x00029964
	private void UpdateDrawCalls()
	{
		Transform cachedTransform = base.cachedTransform;
		bool usedForUI = this.usedForUI;
		if (this.clipping != UIDrawCall.Clipping.None)
		{
			this.drawCallClipRange = this.finalClipRegion;
			this.drawCallClipRange.z = this.drawCallClipRange.z * 0.5f;
			this.drawCallClipRange.w = this.drawCallClipRange.w * 0.5f;
		}
		else
		{
			this.drawCallClipRange = Vector4.zero;
		}
		if (this.drawCallClipRange.z == 0f)
		{
			this.drawCallClipRange.z = (float)Screen.width * 0.5f;
		}
		if (this.drawCallClipRange.w == 0f)
		{
			this.drawCallClipRange.w = (float)Screen.height * 0.5f;
		}
		if (this.halfPixelOffset)
		{
			this.drawCallClipRange.x = this.drawCallClipRange.x - 0.5f;
			this.drawCallClipRange.y = this.drawCallClipRange.y + 0.5f;
		}
		Vector3 vector;
		if (usedForUI)
		{
			Transform parent = base.cachedTransform.parent;
			vector = base.cachedTransform.localPosition;
			if (parent != null)
			{
				float num = Mathf.Round(vector.x);
				float num2 = Mathf.Round(vector.y);
				this.drawCallClipRange.x = this.drawCallClipRange.x + (vector.x - num);
				this.drawCallClipRange.y = this.drawCallClipRange.y + (vector.y - num2);
				vector.x = num;
				vector.y = num2;
				vector = parent.TransformPoint(vector);
			}
			vector += this.drawCallOffset;
		}
		else
		{
			vector = cachedTransform.position;
		}
		Quaternion rotation = cachedTransform.rotation;
		Vector3 lossyScale = cachedTransform.lossyScale;
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			Transform cachedTransform2 = uidrawCall.cachedTransform;
			cachedTransform2.position = vector;
			cachedTransform2.rotation = rotation;
			cachedTransform2.localScale = lossyScale;
			uidrawCall.renderQueue = ((this.renderQueue != UIPanel.RenderQueue.Explicit) ? (this.startingRenderQueue + i) : this.startingRenderQueue);
			uidrawCall.alwaysOnScreen = (this.alwaysOnScreen && (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip));
			uidrawCall.sortingOrder = this.mSortingOrder;
		}
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0002B9DC File Offset: 0x00029BDC
	private void UpdateLayers()
	{
		if (this.mLayer != base.cachedGameObject.layer)
		{
			this.mLayer = this.mGo.layer;
			UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
			NGUITools.SetChildLayer(base.cachedTransform, this.mLayer);
			for (int i = 0; i < this.drawCalls.size; i++)
			{
				this.drawCalls.buffer[i].gameObject.layer = this.mLayer;
			}
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0002BA90 File Offset: 0x00029C90
	private void UpdateWidgets()
	{
		bool flag = !this.cullWhileDragging && this.mCullTime > this.mUpdateTime;
		bool flag2 = false;
		if (this.mForced != flag)
		{
			this.mForced = flag;
			this.mResized = true;
		}
		bool hasCumulativeClipping = this.hasCumulativeClipping;
		int i = 0;
		int size = this.widgets.size;
		while (i < size)
		{
			UIWidget uiwidget = this.widgets.buffer[i];
			if (uiwidget.panel == this && uiwidget.enabled)
			{
				int frameCount = Time.frameCount;
				if (uiwidget.UpdateTransform(frameCount) || this.mResized)
				{
					bool visibleByAlpha = flag || uiwidget.CalculateCumulativeAlpha(frameCount) > 0.001f;
					uiwidget.UpdateVisibility(visibleByAlpha, flag || (!hasCumulativeClipping && !uiwidget.hideIfOffScreen) || this.IsVisible(uiwidget));
				}
				if (uiwidget.UpdateGeometry(frameCount))
				{
					flag2 = true;
					if (!this.mRebuild)
					{
						if (uiwidget.drawCall != null)
						{
							uiwidget.drawCall.isDirty = true;
						}
						else
						{
							this.FindDrawCall(uiwidget);
						}
					}
				}
			}
			i++;
		}
		if (flag2 && this.onGeometryUpdated != null)
		{
			this.onGeometryUpdated();
		}
		this.mResized = false;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0002BC08 File Offset: 0x00029E08
	public UIDrawCall FindDrawCall(UIWidget w)
	{
		Material material = w.material;
		Texture mainTexture = w.mainTexture;
		int depth = w.depth;
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			int num = (i != 0) ? (this.drawCalls.buffer[i - 1].depthEnd + 1) : int.MinValue;
			int num2 = (i + 1 != this.drawCalls.size) ? (this.drawCalls.buffer[i + 1].depthStart - 1) : int.MaxValue;
			if (num <= depth && num2 >= depth)
			{
				if (uidrawCall.baseMaterial == material && uidrawCall.mainTexture == mainTexture)
				{
					if (w.isVisible)
					{
						w.drawCall = uidrawCall;
						if (w.hasVertices)
						{
							uidrawCall.isDirty = true;
						}
						return uidrawCall;
					}
				}
				else
				{
					this.mRebuild = true;
				}
				return null;
			}
		}
		this.mRebuild = true;
		return null;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0002BD28 File Offset: 0x00029F28
	public void AddWidget(UIWidget w)
	{
		if (this.widgets.size == 0)
		{
			this.widgets.Add(w);
		}
		else if (this.mSortWidgets)
		{
			this.widgets.Add(w);
			this.SortWidgets();
		}
		else if (UIWidget.PanelCompareFunc(w, this.widgets[0]) == -1)
		{
			this.widgets.Insert(0, w);
		}
		else
		{
			int i = this.widgets.size;
			while (i > 0)
			{
				if (UIWidget.PanelCompareFunc(w, this.widgets[--i]) != -1)
				{
					this.widgets.Insert(i + 1, w);
					break;
				}
			}
		}
		this.FindDrawCall(w);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0002BDF8 File Offset: 0x00029FF8
	public void RemoveWidget(UIWidget w)
	{
		if (this.widgets.Remove(w) && w.drawCall != null)
		{
			int depth = w.depth;
			if (depth == w.drawCall.depthStart || depth == w.drawCall.depthEnd)
			{
				this.mRebuild = true;
			}
			w.drawCall.isDirty = true;
			w.drawCall = null;
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0002BE6C File Offset: 0x0002A06C
	public void Refresh()
	{
		this.mRebuild = true;
		if (UIPanel.list.size > 0)
		{
			UIPanel.list[0].LateUpdate();
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0002BE98 File Offset: 0x0002A098
	public virtual Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		Vector4 finalClipRegion = this.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		Vector2 minRect;
		minRect..ctor(min.x, min.y);
		Vector2 maxRect;
		maxRect..ctor(max.x, max.y);
		Vector2 minArea;
		minArea..ctor(finalClipRegion.x - num, finalClipRegion.y - num2);
		Vector2 maxArea;
		maxArea..ctor(finalClipRegion.x + num, finalClipRegion.y + num2);
		if (this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.clipSoftness.x;
			minArea.y += this.clipSoftness.y;
			maxArea.x -= this.clipSoftness.x;
			maxArea.y -= this.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		Vector3 vector = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (vector.sqrMagnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += vector;
				targetBounds.center += vector;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + vector, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0002C06C File Offset: 0x0002A26C
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0002C090 File Offset: 0x0002A290
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, false, -1);
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002C09C File Offset: 0x0002A29C
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		return UIPanel.Find(trans, createIfMissing, -1);
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
	public static UIPanel Find(Transform trans, bool createIfMissing, int layer)
	{
		UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<UIPanel>();
			if (uipanel != null)
			{
				return uipanel;
			}
			if (trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		return (!createIfMissing) ? null : NGUITools.CreateUI(trans, false, layer);
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0002C11C File Offset: 0x0002A31C
	private Vector2 GetWindowSize()
	{
		UIRoot root = base.root;
		Vector2 vector;
		vector..ctor((float)Screen.width, (float)Screen.height);
		if (root != null)
		{
			vector *= root.GetPixelSizeAdjustment(Screen.height);
		}
		return vector;
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0002C164 File Offset: 0x0002A364
	public Vector2 GetViewSize()
	{
		bool flag = this.mClipping != UIDrawCall.Clipping.None;
		Vector2 vector = (!flag) ? new Vector2((float)Screen.width, (float)Screen.height) : new Vector2(this.mClipRange.z, this.mClipRange.w);
		if (!flag)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				vector *= root.GetPixelSizeAdjustment(Screen.height);
			}
		}
		return vector;
	}

	// Token: 0x04000556 RID: 1366
	public static BetterList<UIPanel> list = new BetterList<UIPanel>();

	// Token: 0x04000557 RID: 1367
	public UIPanel.OnGeometryUpdated onGeometryUpdated;

	// Token: 0x04000558 RID: 1368
	public bool showInPanelTool = true;

	// Token: 0x04000559 RID: 1369
	public bool generateNormals;

	// Token: 0x0400055A RID: 1370
	public bool widgetsAreStatic;

	// Token: 0x0400055B RID: 1371
	public bool cullWhileDragging;

	// Token: 0x0400055C RID: 1372
	public bool alwaysOnScreen;

	// Token: 0x0400055D RID: 1373
	public bool anchorOffset;

	// Token: 0x0400055E RID: 1374
	public UIPanel.RenderQueue renderQueue;

	// Token: 0x0400055F RID: 1375
	public int startingRenderQueue = 3000;

	// Token: 0x04000560 RID: 1376
	[NonSerialized]
	public BetterList<UIWidget> widgets = new BetterList<UIWidget>();

	// Token: 0x04000561 RID: 1377
	[NonSerialized]
	public BetterList<UIDrawCall> drawCalls = new BetterList<UIDrawCall>();

	// Token: 0x04000562 RID: 1378
	[NonSerialized]
	public Matrix4x4 worldToLocal = Matrix4x4.identity;

	// Token: 0x04000563 RID: 1379
	[NonSerialized]
	public Vector4 drawCallClipRange = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x04000564 RID: 1380
	public UIPanel.OnClippingMoved onClipMove;

	// Token: 0x04000565 RID: 1381
	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	// Token: 0x04000566 RID: 1382
	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04000567 RID: 1383
	[HideInInspector]
	[SerializeField]
	private Vector4 mClipRange = new Vector4(0f, 0f, 300f, 200f);

	// Token: 0x04000568 RID: 1384
	[SerializeField]
	[HideInInspector]
	private Vector2 mClipSoftness = new Vector2(4f, 4f);

	// Token: 0x04000569 RID: 1385
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x0400056A RID: 1386
	[SerializeField]
	[HideInInspector]
	private int mSortingOrder;

	// Token: 0x0400056B RID: 1387
	private bool mRebuild;

	// Token: 0x0400056C RID: 1388
	private bool mResized;

	// Token: 0x0400056D RID: 1389
	private Camera mCam;

	// Token: 0x0400056E RID: 1390
	[SerializeField]
	private Vector2 mClipOffset = Vector2.zero;

	// Token: 0x0400056F RID: 1391
	private float mCullTime;

	// Token: 0x04000570 RID: 1392
	private float mUpdateTime;

	// Token: 0x04000571 RID: 1393
	private int mMatrixFrame = -1;

	// Token: 0x04000572 RID: 1394
	private int mAlphaFrameID;

	// Token: 0x04000573 RID: 1395
	private int mLayer = -1;

	// Token: 0x04000574 RID: 1396
	private static float[] mTemp = new float[4];

	// Token: 0x04000575 RID: 1397
	private Vector2 mMin = Vector2.zero;

	// Token: 0x04000576 RID: 1398
	private Vector2 mMax = Vector2.zero;

	// Token: 0x04000577 RID: 1399
	private bool mHalfPixelOffset;

	// Token: 0x04000578 RID: 1400
	private bool mSortWidgets;

	// Token: 0x04000579 RID: 1401
	private UIPanel mParentPanel;

	// Token: 0x0400057A RID: 1402
	private static Vector3[] mCorners = new Vector3[4];

	// Token: 0x0400057B RID: 1403
	private static int mUpdateFrame = -1;

	// Token: 0x0400057C RID: 1404
	private UIDrawCall.OnRenderCallback mOnRender;

	// Token: 0x0400057D RID: 1405
	private bool mForced;

	// Token: 0x020000CA RID: 202
	public enum RenderQueue
	{
		// Token: 0x0400057F RID: 1407
		Automatic,
		// Token: 0x04000580 RID: 1408
		StartAt,
		// Token: 0x04000581 RID: 1409
		Explicit
	}

	// Token: 0x02000AAA RID: 2730
	// (Invoke) Token: 0x06004F31 RID: 20273
	public delegate void OnGeometryUpdated();

	// Token: 0x02000AAB RID: 2731
	// (Invoke) Token: 0x06004F35 RID: 20277
	public delegate void OnClippingMoved(UIPanel panel);
}
