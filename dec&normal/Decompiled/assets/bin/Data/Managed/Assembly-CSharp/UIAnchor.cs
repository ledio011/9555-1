using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
	// Token: 0x06000516 RID: 1302 RVA: 0x0002209C File Offset: 0x0002029C
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mAnim = base.animation;
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x000220E4 File Offset: 0x000202E4
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00022114 File Offset: 0x00020314
	public void ScreenSizeChanged()
	{
		if (this.mStarted && this.runOnlyOnce)
		{
			this.count = 0;
			base.enabled = true;
			this.Update();
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0002214C File Offset: 0x0002034C
	private void Start()
	{
		if (this.container == null)
		{
			UIPanel uipanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
			if (uipanel != null)
			{
				this.container = uipanel.gameObject;
			}
		}
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.Update();
		this.mStarted = true;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00022210 File Offset: 0x00020410
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.enabled && this.mAnim.isPlaying)
		{
			return;
		}
		bool flag = false;
		UIWidget uiwidget = (!(this.container == null)) ? this.container.GetComponent<UIWidget>() : null;
		UIPanel uipanel = (!(this.container == null) || !(uiwidget == null)) ? this.container.GetComponent<UIPanel>() : null;
		if (uiwidget != null)
		{
			Bounds bounds = uiwidget.CalculateBounds(this.container.transform.parent);
			this.mRect.x = bounds.min.x;
			this.mRect.y = bounds.min.y;
			this.mRect.width = bounds.size.x;
			this.mRect.height = bounds.size.y;
		}
		else if (uipanel != null)
		{
			if (uipanel.clipping == UIDrawCall.Clipping.None)
			{
				float num = (!(this.mRoot != null)) ? 0.5f : ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f);
				this.mRect.xMin = (float)(-(float)Screen.width) * num;
				this.mRect.yMin = (float)(-(float)Screen.height) * num;
				this.mRect.xMax = -this.mRect.xMin;
				this.mRect.yMax = -this.mRect.yMin;
			}
			else
			{
				Vector4 finalClipRegion = uipanel.finalClipRegion;
				this.mRect.x = finalClipRegion.x - finalClipRegion.z * 0.5f;
				this.mRect.y = finalClipRegion.y - finalClipRegion.w * 0.5f;
				this.mRect.width = finalClipRegion.z;
				this.mRect.height = finalClipRegion.w;
			}
		}
		else if (this.container != null)
		{
			Transform parent = this.container.transform.parent;
			Bounds bounds2 = (!(parent != null)) ? NGUIMath.CalculateRelativeWidgetBounds(this.container.transform) : NGUIMath.CalculateRelativeWidgetBounds(parent, this.container.transform);
			this.mRect.x = bounds2.min.x;
			this.mRect.y = bounds2.min.y;
			this.mRect.width = bounds2.size.x;
			this.mRect.height = bounds2.size.y;
		}
		else
		{
			if (!(this.uiCamera != null))
			{
				return;
			}
			flag = true;
			this.mRect = this.uiCamera.pixelRect;
		}
		float num2 = (this.mRect.xMin + this.mRect.xMax) * 0.5f;
		float num3 = (this.mRect.yMin + this.mRect.yMax) * 0.5f;
		Vector3 vector;
		vector..ctor(num2, num3, 0f);
		if (this.side != UIAnchor.Side.Center)
		{
			if (this.side == UIAnchor.Side.Right || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.BottomRight)
			{
				vector.x = this.mRect.xMax;
			}
			else if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Bottom)
			{
				vector.x = num2;
			}
			else
			{
				vector.x = this.mRect.xMin;
			}
			if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.TopLeft)
			{
				vector.y = this.mRect.yMax;
			}
			else if (this.side == UIAnchor.Side.Left || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Right)
			{
				vector.y = num3;
			}
			else
			{
				vector.y = this.mRect.yMin;
			}
		}
		float width = this.mRect.width;
		float height = this.mRect.height;
		vector.x += this.pixelOffset.x + this.relativeOffset.x * width;
		vector.y += this.pixelOffset.y + this.relativeOffset.y * height;
		if (flag)
		{
			if (this.uiCamera.orthographic)
			{
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
			}
			vector.z = this.uiCamera.WorldToScreenPoint(this.mTrans.position).z;
			vector = this.uiCamera.ScreenToWorldPoint(vector);
		}
		else
		{
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			if (uipanel != null)
			{
				vector = uipanel.cachedTransform.TransformPoint(vector);
			}
			else if (this.container != null)
			{
				Transform parent2 = this.container.transform.parent;
				if (parent2 != null)
				{
					vector = parent2.TransformPoint(vector);
				}
			}
			vector.z = this.mTrans.position.z;
		}
		if (this.mTrans.position != vector)
		{
			this.mTrans.position = vector;
		}
		this.count++;
		if (this.runOnlyOnce && Application.isPlaying && this.count > 1)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0400045F RID: 1119
	public Camera uiCamera;

	// Token: 0x04000460 RID: 1120
	public GameObject container;

	// Token: 0x04000461 RID: 1121
	public UIAnchor.Side side = UIAnchor.Side.Center;

	// Token: 0x04000462 RID: 1122
	public bool runOnlyOnce = true;

	// Token: 0x04000463 RID: 1123
	public Vector2 relativeOffset = Vector2.zero;

	// Token: 0x04000464 RID: 1124
	public Vector2 pixelOffset = Vector2.zero;

	// Token: 0x04000465 RID: 1125
	private int count;

	// Token: 0x04000466 RID: 1126
	[SerializeField]
	[HideInInspector]
	private UIWidget widgetContainer;

	// Token: 0x04000467 RID: 1127
	private Transform mTrans;

	// Token: 0x04000468 RID: 1128
	private Animation mAnim;

	// Token: 0x04000469 RID: 1129
	private Rect mRect = default(Rect);

	// Token: 0x0400046A RID: 1130
	private UIRoot mRoot;

	// Token: 0x0400046B RID: 1131
	private bool mStarted;

	// Token: 0x020000B3 RID: 179
	public enum Side
	{
		// Token: 0x0400046D RID: 1133
		BottomLeft,
		// Token: 0x0400046E RID: 1134
		Left,
		// Token: 0x0400046F RID: 1135
		TopLeft,
		// Token: 0x04000470 RID: 1136
		Top,
		// Token: 0x04000471 RID: 1137
		TopRight,
		// Token: 0x04000472 RID: 1138
		Right,
		// Token: 0x04000473 RID: 1139
		BottomRight,
		// Token: 0x04000474 RID: 1140
		Bottom,
		// Token: 0x04000475 RID: 1141
		Center
	}
}
