using System;
using UnityEngine;

// Token: 0x02000A7A RID: 2682
public class UIMyCenterOnChild : MonoBehaviour
{
	// Token: 0x06004E08 RID: 19976 RVA: 0x001AA858 File Offset: 0x001A8A58
	private void CenterOnNow(Transform target, Vector3 panelCenter)
	{
		if (target != null && this.mScrollView != null && this.mScrollView.panel != null)
		{
			Transform cachedTransform = this.mScrollView.panel.cachedTransform;
			Vector3 vector = cachedTransform.InverseTransformPoint(target.position);
			Vector3 vector2 = cachedTransform.InverseTransformPoint(panelCenter);
			Vector3 vector3 = vector - vector2;
			if (!this.mScrollView.canMoveHorizontally)
			{
				vector3.x = 0f;
			}
			if (!this.mScrollView.canMoveVertically)
			{
				vector3.y = 0f;
			}
			vector3.z = 0f;
			Vector3 localPosition = this.mScrollView.panel.cachedGameObject.transform.localPosition;
			Vector3 vector4 = cachedTransform.localPosition - vector3;
			this.mScrollView.panel.cachedGameObject.transform.localPosition = vector4;
			Vector3 vector5 = vector4 - localPosition;
			Vector2 clipOffset = this.mScrollView.panel.clipOffset;
			clipOffset.x -= vector5.x;
			clipOffset.y -= vector5.y;
			this.mScrollView.panel.clipOffset = clipOffset;
			if (this.mScrollView != null)
			{
				this.mScrollView.UpdateScrollbars(false);
			}
		}
	}

	// Token: 0x06004E09 RID: 19977 RVA: 0x001AA9C8 File Offset: 0x001A8BC8
	public void CenterOn(Transform target)
	{
		if (this.mScrollView == null)
		{
			this.mScrollView = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
		if (this.mScrollView != null && this.mScrollView.panel != null)
		{
			Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
			Vector3 vector = Vector3.zero;
			switch (this.CenterPos)
			{
			case CENTERONCHILD_CENTERPOS.CENTER:
				vector = (worldCorners[2] + worldCorners[0]) * 0.5f;
				break;
			case CENTERONCHILD_CENTERPOS.LEFT:
				vector = (worldCorners[1] + worldCorners[0]) * 0.5f;
				break;
			case CENTERONCHILD_CENTERPOS.RIGHT:
				vector = (worldCorners[2] + worldCorners[3]) * 0.5f;
				break;
			case CENTERONCHILD_CENTERPOS.TOP:
				vector = (worldCorners[2] + worldCorners[1]) * 0.5f;
				break;
			case CENTERONCHILD_CENTERPOS.BOTTOM:
				vector = (worldCorners[3] + worldCorners[0]) * 0.5f;
				break;
			}
			Vector3 panelCenter = vector + new Vector3(this.CenterOffset.x * base.transform.lossyScale.x, this.CenterOffset.y * base.transform.lossyScale.y, this.CenterOffset.z * base.transform.lossyScale.z);
			this.CenterOn(target, panelCenter);
		}
	}

	// Token: 0x06004E0A RID: 19978 RVA: 0x001AABB0 File Offset: 0x001A8DB0
	private void CenterOn(Transform target, Vector3 panelCenter)
	{
		if (target != null && this.mScrollView != null && this.mScrollView.panel != null)
		{
			Transform cachedTransform = this.mScrollView.panel.cachedTransform;
			Vector3 vector = cachedTransform.InverseTransformPoint(target.position);
			Vector3 vector2 = cachedTransform.InverseTransformPoint(panelCenter);
			Vector3 vector3 = vector - vector2;
			if (!this.mScrollView.canMoveHorizontally)
			{
				vector3.x = 0f;
			}
			if (!this.mScrollView.canMoveVertically)
			{
				vector3.y = 0f;
			}
			vector3.z = 0f;
			SpringPanel.Begin(this.mScrollView.panel.cachedGameObject, cachedTransform.localPosition - vector3, this.springStrength).onFinished = this.onFinished;
		}
	}

	// Token: 0x04003C96 RID: 15510
	public CENTERONCHILD_CENTERPOS CenterPos;

	// Token: 0x04003C97 RID: 15511
	public Vector3 CenterOffset = Vector3.zero;

	// Token: 0x04003C98 RID: 15512
	private UIScrollView mScrollView;

	// Token: 0x04003C99 RID: 15513
	public float springStrength = 8f;

	// Token: 0x04003C9A RID: 15514
	public SpringPanel.OnFinished onFinished;
}
