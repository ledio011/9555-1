using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[AddComponentMenu("NGUI/Internal/Spring Panel")]
[RequireComponent(typeof(UIPanel))]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x060003B9 RID: 953 RVA: 0x0001B47C File Offset: 0x0001967C
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIScrollView>();
		this.mTrans = base.transform;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0001B4B0 File Offset: 0x000196B0
	private void Update()
	{
		this.AdvanceTowardsPosition();
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0001B4B8 File Offset: 0x000196B8
	protected virtual void AdvanceTowardsPosition()
	{
		float deltaTime = RealTime.deltaTime;
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		if ((vector - this.target).sqrMagnitude < 0.01f)
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= vector2.x;
		clipOffset.y -= vector2.y;
		this.mPanel.clipOffset = clipOffset;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			SpringPanel.current = this;
			this.onFinished();
			SpringPanel.current = null;
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001B5C4 File Offset: 0x000197C4
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		springPanel.enabled = true;
		return springPanel;
	}

	// Token: 0x04000372 RID: 882
	public static SpringPanel current;

	// Token: 0x04000373 RID: 883
	public Vector3 target = Vector3.zero;

	// Token: 0x04000374 RID: 884
	public float strength = 10f;

	// Token: 0x04000375 RID: 885
	public SpringPanel.OnFinished onFinished;

	// Token: 0x04000376 RID: 886
	private UIPanel mPanel;

	// Token: 0x04000377 RID: 887
	private Transform mTrans;

	// Token: 0x04000378 RID: 888
	private UIScrollView mDrag;

	// Token: 0x02000A9C RID: 2716
	// (Invoke) Token: 0x06004EF9 RID: 20217
	public delegate void OnFinished();
}
