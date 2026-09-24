using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200009C RID: 156
[AddComponentMenu("NGUI/UI/NGUI Widget")]
[ExecuteInEditMode]
public class UIWidget : UIRect
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001DB04 File Offset: 0x0001BD04
	// (set) Token: 0x06000423 RID: 1059 RVA: 0x0001DB0C File Offset: 0x0001BD0C
	public UIDrawCall.OnRenderCallback onRender
	{
		get
		{
			return this.mOnRender;
		}
		set
		{
			if (this.mOnRender != value)
			{
				if (this.drawCall != null && this.drawCall.onRender != null && this.mOnRender != null)
				{
					UIDrawCall uidrawCall = this.drawCall;
					uidrawCall.onRender = (UIDrawCall.OnRenderCallback)Delegate.Remove(uidrawCall.onRender, this.mOnRender);
				}
				this.mOnRender = value;
				if (this.drawCall != null)
				{
					UIDrawCall uidrawCall2 = this.drawCall;
					uidrawCall2.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uidrawCall2.onRender, value);
				}
			}
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001DBAC File Offset: 0x0001BDAC
	// (set) Token: 0x06000425 RID: 1061 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
	public Vector4 drawRegion
	{
		get
		{
			return this.mDrawRegion;
		}
		set
		{
			if (this.mDrawRegion != value)
			{
				this.mDrawRegion = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
	public Vector2 pivotOffset
	{
		get
		{
			return NGUIMath.GetPivotOffset(this.pivot);
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
	// (set) Token: 0x06000428 RID: 1064 RVA: 0x0001DC00 File Offset: 0x0001BE00
	public int width
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			int minWidth = this.minWidth;
			if (value < minWidth)
			{
				value = minWidth;
			}
			if (this.mWidth != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnHeight)
			{
				if (this.isAnchoredHorizontally)
				{
					if (this.leftAnchor.target != null && this.rightAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Left || this.mPivot == UIWidget.Pivot.TopLeft)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
						}
						else if (this.mPivot == UIWidget.Pivot.BottomRight || this.mPivot == UIWidget.Pivot.Right || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
						}
						else
						{
							int num = value - this.mWidth;
							num -= (num & 1);
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f, 0f);
							}
						}
					}
					else if (this.leftAnchor.target != null)
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
					}
					else
					{
						NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
					}
				}
				else
				{
					this.SetDimensions(value, this.mHeight);
				}
			}
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
	// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
	public int height
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			int minHeight = this.minHeight;
			if (value < minHeight)
			{
				value = minHeight;
			}
			if (this.mHeight != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnWidth)
			{
				if (this.isAnchoredVertically)
				{
					if (this.bottomAnchor.target != null && this.topAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Bottom || this.mPivot == UIWidget.Pivot.BottomRight)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
						}
						else if (this.mPivot == UIWidget.Pivot.TopLeft || this.mPivot == UIWidget.Pivot.Top || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
						}
						else
						{
							int num = value - this.mHeight;
							num -= (num & 1);
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, 0f, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f);
							}
						}
					}
					else if (this.bottomAnchor.target != null)
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
					}
					else
					{
						NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
					}
				}
				else
				{
					this.SetDimensions(this.mWidth, value);
				}
			}
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001DF48 File Offset: 0x0001C148
	// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001DF50 File Offset: 0x0001C150
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				bool includeChildren = this.mColor.a != value.a;
				this.mColor = value;
				this.Invalidate(includeChildren);
			}
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001DF94 File Offset: 0x0001C194
	// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
	public override float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			if (this.mColor.a != value)
			{
				this.mColor.a = value;
				this.Invalidate(true);
			}
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
	public bool isVisible
	{
		get
		{
			return this.mIsVisibleByPanel && this.mIsVisibleByAlpha && this.mIsInFront && this.finalAlpha > 0.001f && NGUITools.GetActive(this);
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001E020 File Offset: 0x0001C220
	public bool hasVertices
	{
		get
		{
			return this.geometry != null && this.geometry.hasVertices;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001E03C File Offset: 0x0001C23C
	// (set) Token: 0x06000432 RID: 1074 RVA: 0x0001E044 File Offset: 0x0001C244
	public UIWidget.Pivot rawPivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001E07C File Offset: 0x0001C27C
	// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001E084 File Offset: 0x0001C284
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				Vector3 vector = this.worldCorners[0];
				this.mPivot = value;
				this.mChanged = true;
				Vector3 vector2 = this.worldCorners[0];
				Transform cachedTransform = base.cachedTransform;
				Vector3 vector3 = cachedTransform.position;
				float z = cachedTransform.localPosition.z;
				vector3.x += vector.x - vector2.x;
				vector3.y += vector.y - vector2.y;
				base.cachedTransform.position = vector3;
				vector3 = base.cachedTransform.localPosition;
				vector3.x = Mathf.Round(vector3.x);
				vector3.y = Mathf.Round(vector3.y);
				vector3.z = z;
				base.cachedTransform.localPosition = vector3;
			}
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001E17C File Offset: 0x0001C37C
	// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001E184 File Offset: 0x0001C384
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
				if (this.panel != null)
				{
					this.panel.RemoveWidget(this);
				}
				this.mDepth = value;
				if (this.panel != null)
				{
					this.panel.AddWidget(this);
					if (!Application.isPlaying)
					{
						this.panel.SortWidgets();
						this.panel.RebuildAllDrawCalls();
					}
				}
			}
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001E200 File Offset: 0x0001C400
	public int raycastDepth
	{
		get
		{
			if (this.panel == null)
			{
				this.CreatePanel();
			}
			return (!(this.panel != null)) ? this.mDepth : (this.mDepth + this.panel.depth * 1000);
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001E25C File Offset: 0x0001C45C
	public override Vector3[] localCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			this.mCorners[0] = new Vector3(num, num2);
			this.mCorners[1] = new Vector3(num, num4);
			this.mCorners[2] = new Vector3(num3, num4);
			this.mCorners[3] = new Vector3(num3, num2);
			return this.mCorners;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001E310 File Offset: 0x0001C510
	public virtual Vector2 localSize
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return localCorners[2] - localCorners[0];
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001E348 File Offset: 0x0001C548
	public override Vector3[] worldCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			Transform cachedTransform = base.cachedTransform;
			this.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			this.mCorners[1] = cachedTransform.TransformPoint(num, num4, 0f);
			this.mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0f);
			this.mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001E420 File Offset: 0x0001C620
	public virtual Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			return new Vector4((this.mDrawRegion.x != 0f) ? Mathf.Lerp(num, num3, this.mDrawRegion.x) : num, (this.mDrawRegion.y != 0f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.y) : num2, (this.mDrawRegion.z != 1f) ? Mathf.Lerp(num, num3, this.mDrawRegion.z) : num3, (this.mDrawRegion.w != 1f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.w) : num4);
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001E528 File Offset: 0x0001C728
	// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001E52C File Offset: 0x0001C72C
	public virtual Material material
	{
		get
		{
			return null;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no material setter");
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001E544 File Offset: 0x0001C744
	// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001E570 File Offset: 0x0001C770
	public virtual Texture mainTexture
	{
		get
		{
			Material material = this.material;
			return (!(material != null)) ? null : material.mainTexture;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no mainTexture setter");
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001E588 File Offset: 0x0001C788
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
	public virtual Shader shader
	{
		get
		{
			Material material = this.material;
			return (!(material != null)) ? null : material.shader;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no shader setter");
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001E5CC File Offset: 0x0001C7CC
	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
	public bool hasBoxCollider
	{
		get
		{
			BoxCollider boxCollider = base.collider as BoxCollider;
			return boxCollider != null;
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
	public void SetDimensions(int w, int h)
	{
		if (this.mWidth != w || this.mHeight != h)
		{
			this.mWidth = w;
			this.mHeight = h;
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnWidth)
			{
				this.mHeight = Mathf.RoundToInt((float)this.mWidth / this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				this.mWidth = Mathf.RoundToInt((float)this.mHeight * this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.Free)
			{
				this.aspectRatio = (float)this.mWidth / (float)this.mHeight;
			}
			this.mMoved = true;
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0001E6BC File Offset: 0x0001C8BC
	public override Vector3[] GetSides(Transform relativeTo)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = -pivotOffset.x * (float)this.mWidth;
		float num2 = -pivotOffset.y * (float)this.mHeight;
		float num3 = num + (float)this.mWidth;
		float num4 = num2 + (float)this.mHeight;
		float num5 = (num + num3) * 0.5f;
		float num6 = (num2 + num4) * 0.5f;
		Transform cachedTransform = base.cachedTransform;
		this.mCorners[0] = cachedTransform.TransformPoint(num, num6, 0f);
		this.mCorners[1] = cachedTransform.TransformPoint(num5, num4, 0f);
		this.mCorners[2] = cachedTransform.TransformPoint(num3, num6, 0f);
		this.mCorners[3] = cachedTransform.TransformPoint(num5, num2, 0f);
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				this.mCorners[i] = relativeTo.InverseTransformPoint(this.mCorners[i]);
			}
		}
		return this.mCorners;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0001E7FC File Offset: 0x0001C9FC
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			this.UpdateFinalAlpha(frameID);
		}
		return this.finalAlpha;
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0001E82C File Offset: 0x0001CA2C
	protected void UpdateFinalAlpha(int frameID)
	{
		if (!this.mIsVisibleByAlpha || !this.mIsInFront)
		{
			this.finalAlpha = 0f;
		}
		else
		{
			UIRect parent = base.parent;
			this.finalAlpha = ((!(base.parent != null)) ? this.mColor.a : (parent.CalculateFinalAlpha(frameID) * this.mColor.a));
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
	public override void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		this.mAlphaFrameID = -1;
		if (this.panel != null)
		{
			bool visibleByPanel = (!this.hideIfOffScreen && !this.panel.hasCumulativeClipping) || this.panel.IsVisible(this);
			this.UpdateVisibility(this.CalculateCumulativeAlpha(Time.frameCount) > 0.001f, visibleByPanel);
			this.UpdateFinalAlpha(Time.frameCount);
			if (includeChildren)
			{
				base.Invalidate(true);
			}
		}
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0001E92C File Offset: 0x0001CB2C
	public float CalculateCumulativeAlpha(int frameID)
	{
		UIRect parent = base.parent;
		return (!(parent != null)) ? this.mColor.a : (parent.CalculateFinalAlpha(frameID) * this.mColor.a);
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0001E970 File Offset: 0x0001CB70
	public override void SetRect(float x, float y, float width, float height)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = Mathf.Lerp(x, x + width, pivotOffset.x);
		float num2 = Mathf.Lerp(y, y + height, pivotOffset.y);
		int num3 = Mathf.FloorToInt(width + 0.5f);
		int num4 = Mathf.FloorToInt(height + 0.5f);
		if (pivotOffset.x == 0.5f)
		{
			num3 = num3 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f)
		{
			num4 = num4 >> 1 << 1;
		}
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(num + 0.5f);
		localPosition.y = Mathf.Floor(num2 + 0.5f);
		if (num3 < this.minWidth)
		{
			num3 = this.minWidth;
		}
		if (num4 < this.minHeight)
		{
			num4 = this.minHeight;
		}
		transform.localPosition = localPosition;
		this.width = num3;
		this.height = num4;
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

	// Token: 0x0600044B RID: 1099 RVA: 0x0001EB10 File Offset: 0x0001CD10
	public void ResizeCollider()
	{
		if (NGUITools.GetActive(this))
		{
			BoxCollider boxCollider = base.collider as BoxCollider;
			if (boxCollider != null)
			{
				NGUITools.UpdateWidgetCollider(boxCollider, true);
			}
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0001EB48 File Offset: 0x0001CD48
	[DebuggerStepThrough]
	[DebuggerHidden]
	public static int FullCompareFunc(UIWidget left, UIWidget right)
	{
		int num = UIPanel.CompareFunc(left.panel, right.panel);
		return (num != 0) ? num : UIWidget.PanelCompareFunc(left, right);
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001EB7C File Offset: 0x0001CD7C
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int PanelCompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		Material material = left.material;
		Material material2 = right.material;
		if (material == material2)
		{
			return 0;
		}
		if (material != null)
		{
			return -1;
		}
		if (material2 != null)
		{
			return 1;
		}
		return (material.GetInstanceID() >= material2.GetInstanceID()) ? 1 : -1;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0001EC00 File Offset: 0x0001CE00
	public Bounds CalculateBounds()
	{
		return this.CalculateBounds(null);
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0001EC0C File Offset: 0x0001CE0C
	public Bounds CalculateBounds(Transform relativeParent)
	{
		if (relativeParent == null)
		{
			Vector3[] localCorners = this.localCorners;
			Bounds result;
			result..ctor(localCorners[0], Vector3.zero);
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(localCorners[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = relativeParent.worldToLocalMatrix;
		Vector3[] worldCorners = this.worldCorners;
		Bounds result2;
		result2..ctor(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[0]), Vector3.zero);
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[j]));
		}
		return result2;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0001ECD0 File Offset: 0x0001CED0
	public void SetDirty()
	{
		if (this.drawCall != null)
		{
			this.drawCall.isDirty = true;
		}
		else if (this.isVisible && this.hasVertices)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0001ED1C File Offset: 0x0001CF1C
	protected void RemoveFromPanel()
	{
		if (this.panel != null)
		{
			this.panel.RemoveWidget(this);
			this.panel = null;
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001ED50 File Offset: 0x0001CF50
	public virtual void MarkAsChanged()
	{
		if (NGUITools.GetActive(this))
		{
			this.mChanged = true;
			if (this.panel != null && base.enabled && NGUITools.GetActive(base.gameObject) && !this.mPlayMode)
			{
				this.SetDirty();
				this.CheckLayer();
			}
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
	public UIPanel CreatePanel()
	{
		if (this.mStarted && this.panel == null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.panel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != null)
			{
				this.mParentFound = false;
				this.panel.AddWidget(this);
				this.CheckLayer();
				this.Invalidate(true);
			}
		}
		return this.panel;
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0001EE4C File Offset: 0x0001D04C
	public void CheckLayer()
	{
		if (this.panel != null && this.panel.gameObject.layer != base.gameObject.layer)
		{
			Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.panel.gameObject.layer;
		}
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		if (this.panel != null)
		{
			UIPanel uipanel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != uipanel)
			{
				this.RemoveFromPanel();
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001EF0C File Offset: 0x0001D10C
	protected virtual void Awake()
	{
		this.mGo = base.gameObject;
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001EF28 File Offset: 0x0001D128
	protected override void OnInit()
	{
		base.OnInit();
		this.RemoveFromPanel();
		this.mMoved = true;
		if (this.mWidth == 100 && this.mHeight == 100 && base.cachedTransform.localScale.magnitude > 8f)
		{
			this.UpgradeFrom265();
			base.cachedTransform.localScale = Vector3.one;
		}
		base.Update();
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001EF9C File Offset: 0x0001D19C
	protected virtual void UpgradeFrom265()
	{
		Vector3 localScale = base.cachedTransform.localScale;
		this.mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		this.mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		if (base.GetComponent<BoxCollider>() != null)
		{
			NGUITools.AddWidgetCollider(base.gameObject, true);
		}
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0001F004 File Offset: 0x0001D204
	protected override void OnStart()
	{
		this.CreatePanel();
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0001F010 File Offset: 0x0001D210
	protected override void OnAnchor()
	{
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector3 localPosition = cachedTransform.localPosition;
		Vector2 pivotOffset = this.pivotOffset;
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
				this.mIsInFront = true;
			}
			else
			{
				Vector3 localPos = base.GetLocalPos(this.leftAnchor, parent);
				num = localPos.x + (float)this.leftAnchor.absolute;
				num3 = localPos.y + (float)this.bottomAnchor.absolute;
				num2 = localPos.x + (float)this.rightAnchor.absolute;
				num4 = localPos.y + (float)this.topAnchor.absolute;
				this.mIsInFront = (!this.hideIfOffScreen || localPos.z >= 0f);
			}
		}
		else
		{
			this.mIsInFront = true;
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
				num = localPosition.x - pivotOffset.x * (float)this.mWidth;
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
				num2 = localPosition.x - pivotOffset.x * (float)this.mWidth + (float)this.mWidth;
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
				num3 = localPosition.y - pivotOffset.y * (float)this.mHeight;
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
				num4 = localPosition.y - pivotOffset.y * (float)this.mHeight + (float)this.mHeight;
			}
		}
		Vector3 vector;
		vector..ctor(Mathf.Lerp(num, num2, pivotOffset.x), Mathf.Lerp(num3, num4, pivotOffset.y), localPosition.z);
		int num5 = Mathf.FloorToInt(num2 - num + 0.5f);
		int num6 = Mathf.FloorToInt(num4 - num3 + 0.5f);
		if (this.keepAspectRatio != UIWidget.AspectRatioSource.Free && this.aspectRatio != 0f)
		{
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				num5 = Mathf.RoundToInt((float)num6 * this.aspectRatio);
			}
			else
			{
				num6 = Mathf.RoundToInt((float)num5 / this.aspectRatio);
			}
		}
		if (num5 < this.minWidth)
		{
			num5 = this.minWidth;
		}
		if (num6 < this.minHeight)
		{
			num6 = this.minHeight;
		}
		if (Vector3.SqrMagnitude(localPosition - vector) > 0.001f)
		{
			base.cachedTransform.localPosition = vector;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
		}
		if (this.mWidth != num5 || this.mHeight != num6)
		{
			this.mWidth = num5;
			this.mHeight = num6;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0001F634 File Offset: 0x0001D834
	protected override void OnUpdate()
	{
		if (this.panel == null)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0001F650 File Offset: 0x0001D850
	private void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			this.MarkAsChanged();
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0001F660 File Offset: 0x0001D860
	protected override void OnDisable()
	{
		this.RemoveFromPanel();
		base.OnDisable();
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0001F670 File Offset: 0x0001D870
	private void OnDestroy()
	{
		this.RemoveFromPanel();
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001F678 File Offset: 0x0001D878
	public bool UpdateVisibility(bool visibleByAlpha, bool visibleByPanel)
	{
		if (this.mIsVisibleByAlpha != visibleByAlpha || this.mIsVisibleByPanel != visibleByPanel)
		{
			this.mChanged = true;
			this.mIsVisibleByAlpha = visibleByAlpha;
			this.mIsVisibleByPanel = visibleByPanel;
			return true;
		}
		return false;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
	public bool UpdateTransform(int frame)
	{
		if (!this.mMoved && !this.panel.widgetsAreStatic && base.cachedTransform.hasChanged)
		{
			this.mTrans.hasChanged = false;
			this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
			this.mMatrixFrame = frame;
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			Transform cachedTransform = base.cachedTransform;
			Vector3 vector = cachedTransform.TransformPoint(num, num2, 0f);
			Vector3 vector2 = cachedTransform.TransformPoint(num3, num4, 0f);
			vector = this.panel.worldToLocal.MultiplyPoint3x4(vector);
			vector2 = this.panel.worldToLocal.MultiplyPoint3x4(vector2);
			if (Vector3.SqrMagnitude(this.mOldV0 - vector) > 1E-06f || Vector3.SqrMagnitude(this.mOldV1 - vector2) > 1E-06f)
			{
				this.mMoved = true;
				this.mOldV0 = vector;
				this.mOldV1 = vector2;
			}
		}
		if (this.mMoved && this.onChange != null)
		{
			this.onChange();
		}
		return this.mMoved || this.mChanged;
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001F838 File Offset: 0x0001DA38
	public bool UpdateGeometry(int frame)
	{
		float num = this.CalculateFinalAlpha(frame);
		if (this.mIsVisibleByAlpha && this.mLastAlpha != num)
		{
			this.mChanged = true;
		}
		this.mLastAlpha = num;
		if (this.mChanged)
		{
			this.mChanged = false;
			if (this.mIsVisibleByAlpha && num > 0.001f && this.shader != null)
			{
				bool hasVertices = this.geometry.hasVertices;
				if (this.fillGeometry)
				{
					this.geometry.Clear();
					this.OnFill(this.geometry.verts, this.geometry.uvs, this.geometry.cols);
				}
				if (this.geometry.hasVertices)
				{
					if (this.mMatrixFrame != frame)
					{
						this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
						this.mMatrixFrame = frame;
					}
					this.geometry.ApplyTransform(this.mLocalToPanel);
					this.mMoved = false;
					return true;
				}
				return hasVertices;
			}
			else if (this.geometry.hasVertices)
			{
				if (this.fillGeometry)
				{
					this.geometry.Clear();
				}
				this.mMoved = false;
				return true;
			}
		}
		else if (this.mMoved && this.geometry.hasVertices)
		{
			if (this.mMatrixFrame != frame)
			{
				this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
				this.mMatrixFrame = frame;
			}
			this.geometry.ApplyTransform(this.mLocalToPanel);
			this.mMoved = false;
			return true;
		}
		this.mMoved = false;
		return false;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001F9FC File Offset: 0x0001DBFC
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		this.geometry.WriteToBuffers(v, u, c, n, t);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0001FA10 File Offset: 0x0001DC10
	public virtual void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		base.cachedTransform.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
	public virtual int minWidth
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001FAAC File Offset: 0x0001DCAC
	public virtual int minHeight
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
	public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
	}

	// Token: 0x040003CA RID: 970
	[HideInInspector]
	[SerializeField]
	protected Color mColor = Color.white;

	// Token: 0x040003CB RID: 971
	[SerializeField]
	[HideInInspector]
	protected UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x040003CC RID: 972
	[HideInInspector]
	[SerializeField]
	protected int mWidth = 100;

	// Token: 0x040003CD RID: 973
	[HideInInspector]
	[SerializeField]
	protected int mHeight = 100;

	// Token: 0x040003CE RID: 974
	[HideInInspector]
	[SerializeField]
	protected int mDepth;

	// Token: 0x040003CF RID: 975
	public UIWidget.OnDimensionsChanged onChange;

	// Token: 0x040003D0 RID: 976
	public bool autoResizeBoxCollider;

	// Token: 0x040003D1 RID: 977
	public bool hideIfOffScreen;

	// Token: 0x040003D2 RID: 978
	public UIDrawCall.OnRenderCallback mOnRender;

	// Token: 0x040003D3 RID: 979
	public UIWidget.AspectRatioSource keepAspectRatio;

	// Token: 0x040003D4 RID: 980
	public float aspectRatio = 1f;

	// Token: 0x040003D5 RID: 981
	public UIWidget.HitCheck hitCheck;

	// Token: 0x040003D6 RID: 982
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x040003D7 RID: 983
	[NonSerialized]
	public UIGeometry geometry = new UIGeometry();

	// Token: 0x040003D8 RID: 984
	[NonSerialized]
	public bool fillGeometry = true;

	// Token: 0x040003D9 RID: 985
	protected bool mPlayMode = true;

	// Token: 0x040003DA RID: 986
	protected Vector4 mDrawRegion = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x040003DB RID: 987
	private Matrix4x4 mLocalToPanel;

	// Token: 0x040003DC RID: 988
	private bool mIsVisibleByAlpha = true;

	// Token: 0x040003DD RID: 989
	private bool mIsVisibleByPanel = true;

	// Token: 0x040003DE RID: 990
	private bool mIsInFront = true;

	// Token: 0x040003DF RID: 991
	private float mLastAlpha;

	// Token: 0x040003E0 RID: 992
	private bool mMoved;

	// Token: 0x040003E1 RID: 993
	[HideInInspector]
	[NonSerialized]
	public UIDrawCall drawCall;

	// Token: 0x040003E2 RID: 994
	protected Vector3[] mCorners = new Vector3[4];

	// Token: 0x040003E3 RID: 995
	private int mAlphaFrameID = -1;

	// Token: 0x040003E4 RID: 996
	private int mMatrixFrame = -1;

	// Token: 0x040003E5 RID: 997
	private Vector3 mOldV0;

	// Token: 0x040003E6 RID: 998
	private Vector3 mOldV1;

	// Token: 0x0200009D RID: 157
	public enum Pivot
	{
		// Token: 0x040003E8 RID: 1000
		TopLeft,
		// Token: 0x040003E9 RID: 1001
		Top,
		// Token: 0x040003EA RID: 1002
		TopRight,
		// Token: 0x040003EB RID: 1003
		Left,
		// Token: 0x040003EC RID: 1004
		Center,
		// Token: 0x040003ED RID: 1005
		Right,
		// Token: 0x040003EE RID: 1006
		BottomLeft,
		// Token: 0x040003EF RID: 1007
		Bottom,
		// Token: 0x040003F0 RID: 1008
		BottomRight
	}

	// Token: 0x0200009E RID: 158
	public enum AspectRatioSource
	{
		// Token: 0x040003F2 RID: 1010
		Free,
		// Token: 0x040003F3 RID: 1011
		BasedOnWidth,
		// Token: 0x040003F4 RID: 1012
		BasedOnHeight
	}

	// Token: 0x02000AA4 RID: 2724
	// (Invoke) Token: 0x06004F19 RID: 20249
	public delegate void OnDimensionsChanged();

	// Token: 0x02000AA5 RID: 2725
	// (Invoke) Token: 0x06004F1D RID: 20253
	public delegate bool HitCheck(Vector3 worldPos);
}
