using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CB RID: 203
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000665 RID: 1637 RVA: 0x0002C21C File Offset: 0x0002A41C
	public int activeHeight
	{
		get
		{
			int height = Screen.height;
			int num = Mathf.Max(2, height);
			if (this.scalingStyle == UIRoot.Scaling.FixedSize)
			{
				return this.manualHeight;
			}
			int width = Screen.width;
			if (this.scalingStyle == UIRoot.Scaling.FixedSizeOnMobiles)
			{
				return this.manualHeight;
			}
			if (num < this.minimumHeight)
			{
				num = this.minimumHeight;
			}
			if (num > this.maximumHeight)
			{
				num = this.maximumHeight;
			}
			if (this.shrinkPortraitUI && height > width)
			{
				num = Mathf.RoundToInt((float)num * ((float)height / (float)width));
			}
			return (!this.adjustByDPI) ? num : NGUIMath.AdjustByDPI((float)num);
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000666 RID: 1638 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
	public float pixelSizeAdjustment
	{
		get
		{
			return this.GetPixelSizeAdjustment(Screen.height);
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0002C2D4 File Offset: 0x0002A4D4
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uiroot != null)) ? 1f : uiroot.pixelSizeAdjustment;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0002C304 File Offset: 0x0002A504
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.scalingStyle == UIRoot.Scaling.FixedSize)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (this.scalingStyle == UIRoot.Scaling.FixedSizeOnMobiles)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0002C37C File Offset: 0x0002A57C
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0002C38C File Offset: 0x0002A58C
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0002C39C File Offset: 0x0002A59C
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0002C3AC File Offset: 0x0002A5AC
	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			this.Update();
		}
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0002C40C File Offset: 0x0002A60C
	private void Update()
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1E-45f || Mathf.Abs(localScale.y - num2) > 1E-45f || Mathf.Abs(localScale.z - num2) > 1E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
				}
			}
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0002C4AC File Offset: 0x0002A6AC
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, 1);
			}
			i++;
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = UIRoot.list.Count;
			while (i < count)
			{
				UIRoot uiroot = UIRoot.list[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, 1);
				}
				i++;
			}
		}
	}

	// Token: 0x04000582 RID: 1410
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x04000583 RID: 1411
	public UIRoot.Scaling scalingStyle;

	// Token: 0x04000584 RID: 1412
	public int manualHeight = 720;

	// Token: 0x04000585 RID: 1413
	public int minimumHeight = 320;

	// Token: 0x04000586 RID: 1414
	public int maximumHeight = 1536;

	// Token: 0x04000587 RID: 1415
	public bool adjustByDPI;

	// Token: 0x04000588 RID: 1416
	public bool shrinkPortraitUI;

	// Token: 0x04000589 RID: 1417
	private Transform mTrans;

	// Token: 0x020000CC RID: 204
	public enum Scaling
	{
		// Token: 0x0400058B RID: 1419
		PixelPerfect,
		// Token: 0x0400058C RID: 1420
		FixedSize,
		// Token: 0x0400058D RID: 1421
		FixedSizeOnMobiles
	}
}
