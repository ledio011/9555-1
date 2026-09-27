using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild : MonoBehaviour
{
	// Token: 0x0600012F RID: 303 RVA: 0x00007EF4 File Offset: 0x000060F4
	public void RegisterCenterOnEvent(UICenterOnChild.CenterOnChildDelegate func)
	{
		this.OnCenterOnEvent = (UICenterOnChild.CenterOnChildDelegate)Delegate.Combine(this.OnCenterOnEvent, func);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00007F10 File Offset: 0x00006110
	public void DeregisterCenterOnEvent(UICenterOnChild.CenterOnChildDelegate func)
	{
		if (this.OnCenterOnEvent != null)
		{
			this.OnCenterOnEvent = (UICenterOnChild.CenterOnChildDelegate)Delegate.Remove(this.OnCenterOnEvent, func);
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000131 RID: 305 RVA: 0x00007F40 File Offset: 0x00006140
	public GameObject centeredObject
	{
		get
		{
			return this.mCenteredObject;
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00007F48 File Offset: 0x00006148
	private void OnEnable()
	{
		this.Recenter();
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00007F50 File Offset: 0x00006150
	private void OnDragFinished()
	{
		if (base.enabled)
		{
			this.Recenter();
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00007F64 File Offset: 0x00006164
	private void OnValidate()
	{
		this.nextPageThreshold = Mathf.Abs(this.nextPageThreshold);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00007F78 File Offset: 0x00006178
	public void Recenter()
	{
		Transform transform = base.transform;
		if (transform.childCount == 0)
		{
			return;
		}
		if (this.mScrollView == null)
		{
			this.mScrollView = NGUITools.FindInParents<UIScrollView>(base.gameObject);
			if (this.mScrollView == null)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					base.GetType(),
					" requires ",
					typeof(UIScrollView),
					" on a parent object in order to work"
				}), this);
				base.enabled = false;
				return;
			}
			this.mScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.OnDragFinished);
			if (this.mScrollView.horizontalScrollBar != null)
			{
				this.mScrollView.horizontalScrollBar.onDragFinished = new UIProgressBar.OnDragFinished(this.OnDragFinished);
			}
			if (this.mScrollView.verticalScrollBar != null)
			{
				this.mScrollView.verticalScrollBar.onDragFinished = new UIProgressBar.OnDragFinished(this.OnDragFinished);
			}
		}
		if (this.mScrollView.panel == null)
		{
			return;
		}
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
		Vector3 vector2 = vector + new Vector3(this.CenterOffset.x * base.transform.lossyScale.x, this.CenterOffset.y * base.transform.lossyScale.y, this.CenterOffset.z * base.transform.lossyScale.z);
		Vector3 vector3 = vector2 - this.mScrollView.currentMomentum * (this.mScrollView.momentumAmount * 0.1f);
		this.mScrollView.currentMomentum = Vector3.zero;
		float num = float.MaxValue;
		Transform target = null;
		int num2 = 0;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			Transform child = transform.GetChild(i);
			float num3 = Vector3.SqrMagnitude(child.position - vector3);
			if (num3 < num)
			{
				num = num3;
				target = child;
				num2 = i;
			}
			i++;
		}
		if (this.nextPageThreshold > 0f && UICamera.currentTouch != null && this.mCenteredObject != null && this.mCenteredObject.transform == transform.GetChild(num2))
		{
			Vector2 totalDelta = UICamera.currentTouch.totalDelta;
			UIScrollView.Movement movement = this.mScrollView.movement;
			float num4;
			if (movement != UIScrollView.Movement.Horizontal)
			{
				if (movement != UIScrollView.Movement.Vertical)
				{
					num4 = totalDelta.magnitude;
				}
				else
				{
					num4 = totalDelta.y;
				}
			}
			else
			{
				num4 = totalDelta.x;
			}
			if (num4 > this.nextPageThreshold)
			{
				if (num2 > 0)
				{
					target = transform.GetChild(num2 - 1);
				}
			}
			else if (num4 < -this.nextPageThreshold && num2 < transform.childCount - 1)
			{
				target = transform.GetChild(num2 + 1);
			}
		}
		this.CenterOn(target, vector2);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000083C0 File Offset: 0x000065C0
	private void CenterOn(Transform target, Vector3 panelCenter)
	{
		if (target != null && this.mScrollView != null && this.mScrollView.panel != null)
		{
			Transform cachedTransform = this.mScrollView.panel.cachedTransform;
			this.mCenteredObject = target.gameObject;
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
			if (this.OnCenterOnEvent != null)
			{
				this.OnCenterOnEvent(target);
			}
		}
		else
		{
			this.mCenteredObject = null;
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000084D4 File Offset: 0x000066D4
	public void CenterOn(Transform target)
	{
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

	// Token: 0x04000145 RID: 325
	public UICenterOnChild.CenterOnChildDelegate OnCenterOnEvent;

	// Token: 0x04000146 RID: 326
	public float springStrength = 8f;

	// Token: 0x04000147 RID: 327
	public float nextPageThreshold;

	// Token: 0x04000148 RID: 328
	public CENTERONCHILD_CENTERPOS CenterPos;

	// Token: 0x04000149 RID: 329
	public Vector3 CenterOffset = Vector3.zero;

	// Token: 0x0400014A RID: 330
	public SpringPanel.OnFinished onFinished;

	// Token: 0x0400014B RID: 331
	private UIScrollView mScrollView;

	// Token: 0x0400014C RID: 332
	private GameObject mCenteredObject;

	// Token: 0x02000A94 RID: 2708
	// (Invoke) Token: 0x06004ED9 RID: 20185
	public delegate void CenterOnChildDelegate(Transform obj);
}
