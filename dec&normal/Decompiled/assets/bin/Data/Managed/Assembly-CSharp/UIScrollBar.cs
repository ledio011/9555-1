using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
[AddComponentMenu("NGUI/Interaction/NGUI Scroll Bar")]
[ExecuteInEditMode]
public class UIScrollBar : UISlider
{
	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600021A RID: 538 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
	// (set) Token: 0x0600021B RID: 539 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
	[Obsolete("Use 'value' instead")]
	public float scrollValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600021C RID: 540 RVA: 0x0000E4DC File Offset: 0x0000C6DC
	// (set) Token: 0x0600021D RID: 541 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
	public float barSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mSize != num)
			{
				this.mSize = num;
				this.mIsDirty = true;
				if (NGUITools.GetActive(this))
				{
					if (UIProgressBar.current == null && this.onChange != null)
					{
						UIProgressBar.current = this;
						EventDelegate.Execute(this.onChange);
						UIProgressBar.current = null;
					}
					this.ForceUpdate();
				}
			}
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000E558 File Offset: 0x0000C758
	protected override void Upgrade()
	{
		if (this.mDir != UIScrollBar.Direction.Upgraded)
		{
			this.mValue = this.mScroll;
			if (this.mDir == UIScrollBar.Direction.Horizontal)
			{
				this.mFill = ((!this.mInverted) ? UIProgressBar.FillDirection.LeftToRight : UIProgressBar.FillDirection.RightToLeft);
			}
			else
			{
				this.mFill = ((!this.mInverted) ? UIProgressBar.FillDirection.TopToBottom : UIProgressBar.FillDirection.BottomToTop);
			}
			this.mDir = UIScrollBar.Direction.Upgraded;
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mFG != null && this.mFG.collider != null && this.mFG.gameObject != base.gameObject)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.mFG.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(base.OnPressForeground));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(base.OnDragForeground));
			this.mFG.autoResizeBoxCollider = true;
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000E67C File Offset: 0x0000C87C
	protected override float LocalToValue(Vector2 localPos)
	{
		if (!(this.mFG != null))
		{
			return base.LocalToValue(localPos);
		}
		float num = Mathf.Clamp01(this.mSize) * 0.5f;
		float num2 = num;
		float num3 = 1f - num;
		Vector3[] localCorners = this.mFG.localCorners;
		if (base.isHorizontal)
		{
			num2 = Mathf.Lerp(localCorners[0].x, localCorners[2].x, num2);
			num3 = Mathf.Lerp(localCorners[0].x, localCorners[2].x, num3);
			float num4 = num3 - num2;
			if (num4 == 0f)
			{
				return base.value;
			}
			return (!base.isInverted) ? ((localPos.x - num2) / num4) : ((num3 - localPos.x) / num4);
		}
		else
		{
			num2 = Mathf.Lerp(localCorners[0].y, localCorners[1].y, num2);
			num3 = Mathf.Lerp(localCorners[3].y, localCorners[2].y, num3);
			float num5 = num3 - num2;
			if (num5 == 0f)
			{
				return base.value;
			}
			return (!base.isInverted) ? ((localPos.y - num2) / num5) : ((num3 - localPos.y) / num5);
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
	public override void ForceUpdate()
	{
		if (this.mFG != null)
		{
			this.mIsDirty = false;
			float num = Mathf.Clamp01(this.mSize) * 0.5f;
			float num2 = Mathf.Lerp(num, 1f - num, base.value);
			float num3 = num2 - num;
			float num4 = num2 + num;
			if (base.isHorizontal)
			{
				this.mFG.drawRegion = ((!base.isInverted) ? new Vector4(num3, 0f, num4, 1f) : new Vector4(1f - num4, 0f, 1f - num3, 1f));
			}
			else
			{
				this.mFG.drawRegion = ((!base.isInverted) ? new Vector4(0f, num3, 1f, num4) : new Vector4(0f, 1f - num4, 1f, 1f - num3));
			}
			if (this.thumb != null)
			{
				Vector4 drawingDimensions = this.mFG.drawingDimensions;
				Vector3 vector;
				vector..ctor(Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, 0.5f), Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, 0.5f));
				base.SetThumbPosition(this.mFG.cachedTransform.TransformPoint(vector));
			}
		}
		else
		{
			base.ForceUpdate();
		}
	}

	// Token: 0x04000254 RID: 596
	[HideInInspector]
	[SerializeField]
	protected float mSize = 1f;

	// Token: 0x04000255 RID: 597
	[HideInInspector]
	[SerializeField]
	private float mScroll;

	// Token: 0x04000256 RID: 598
	[HideInInspector]
	[SerializeField]
	private UIScrollBar.Direction mDir = UIScrollBar.Direction.Upgraded;

	// Token: 0x0200006B RID: 107
	private enum Direction
	{
		// Token: 0x04000258 RID: 600
		Horizontal,
		// Token: 0x04000259 RID: 601
		Vertical,
		// Token: 0x0400025A RID: 602
		Upgraded
	}
}
