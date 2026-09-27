using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x0600046F RID: 1135 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
	private void Start()
	{
		this.mTrans = base.transform;
		if (this.updateScrollView)
		{
			this.mSv = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).sqrMagnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).sqrMagnitude)
			{
				this.mTrans.position = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).sqrMagnitude * 1E-05f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).sqrMagnitude)
			{
				this.mTrans.localPosition = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		if (this.mSv != null)
		{
			this.mSv.UpdateScrollbars(true);
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0001FD80 File Offset: 0x0001DF80
	private void NotifyListeners()
	{
		SpringPosition.current = this;
		if (this.onFinished != null)
		{
			this.onFinished();
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
		{
			this.eventReceiver.SendMessage(this.callWhenFinished, this, 1);
		}
		SpringPosition.current = null;
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.mThreshold = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x040003FA RID: 1018
	public static SpringPosition current;

	// Token: 0x040003FB RID: 1019
	public Vector3 target = Vector3.zero;

	// Token: 0x040003FC RID: 1020
	public float strength = 10f;

	// Token: 0x040003FD RID: 1021
	public bool worldSpace;

	// Token: 0x040003FE RID: 1022
	public bool ignoreTimeScale;

	// Token: 0x040003FF RID: 1023
	public bool updateScrollView;

	// Token: 0x04000400 RID: 1024
	public SpringPosition.OnFinished onFinished;

	// Token: 0x04000401 RID: 1025
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x04000402 RID: 1026
	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000403 RID: 1027
	private Transform mTrans;

	// Token: 0x04000404 RID: 1028
	private float mThreshold;

	// Token: 0x04000405 RID: 1029
	private UIScrollView mSv;

	// Token: 0x02000AA6 RID: 2726
	// (Invoke) Token: 0x06004F21 RID: 20257
	public delegate void OnFinished();
}
