using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
	// Token: 0x0600015B RID: 347 RVA: 0x000096D8 File Offset: 0x000078D8
	private void OnDragStart()
	{
		if (this.target != null)
		{
			Vector3[] worldCorners = this.target.worldCorners;
			this.mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
			Ray currentRay = UICamera.currentRay;
			float num;
			if (this.mPlane.Raycast(currentRay, ref num))
			{
				this.mRayPos = currentRay.GetPoint(num);
				this.mLocalPos = this.target.cachedTransform.localPosition;
				this.mWidth = this.target.width;
				this.mHeight = this.target.height;
				this.mDragging = true;
			}
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00009798 File Offset: 0x00007998
	private void OnDrag(Vector2 delta)
	{
		if (this.mDragging && this.target != null)
		{
			Ray currentRay = UICamera.currentRay;
			float num;
			if (this.mPlane.Raycast(currentRay, ref num))
			{
				Transform cachedTransform = this.target.cachedTransform;
				cachedTransform.localPosition = this.mLocalPos;
				this.target.width = this.mWidth;
				this.target.height = this.mHeight;
				Vector3 vector = currentRay.GetPoint(num) - this.mRayPos;
				cachedTransform.position += vector;
				Vector3 vector2 = Quaternion.Inverse(cachedTransform.localRotation) * (cachedTransform.localPosition - this.mLocalPos);
				cachedTransform.localPosition = this.mLocalPos;
				NGUIMath.ResizeWidget(this.target, this.pivot, vector2.x, vector2.y, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight);
			}
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x000098A0 File Offset: 0x00007AA0
	private void OnDragEnd()
	{
		this.mDragging = false;
	}

	// Token: 0x0400017E RID: 382
	public UIWidget target;

	// Token: 0x0400017F RID: 383
	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	// Token: 0x04000180 RID: 384
	public int minWidth = 100;

	// Token: 0x04000181 RID: 385
	public int minHeight = 100;

	// Token: 0x04000182 RID: 386
	public int maxWidth = 100000;

	// Token: 0x04000183 RID: 387
	public int maxHeight = 100000;

	// Token: 0x04000184 RID: 388
	private Plane mPlane;

	// Token: 0x04000185 RID: 389
	private Vector3 mRayPos;

	// Token: 0x04000186 RID: 390
	private Vector3 mLocalPos;

	// Token: 0x04000187 RID: 391
	private int mWidth;

	// Token: 0x04000188 RID: 392
	private int mHeight;

	// Token: 0x04000189 RID: 393
	private bool mDragging;
}
