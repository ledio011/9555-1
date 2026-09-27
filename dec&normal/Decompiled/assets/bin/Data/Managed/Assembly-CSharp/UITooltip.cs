using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00031130 File Offset: 0x0002F330
	public static bool isVisible
	{
		get
		{
			return UITooltip.mInstance != null && UITooltip.mInstance.mTarget == 1f;
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00031164 File Offset: 0x0002F364
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0003116C File Offset: 0x0002F36C
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00031174 File Offset: 0x0002F374
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x000311DC File Offset: 0x0002F3DC
	protected virtual void Update()
	{
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, RealTime.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 vector = this.mSize * 0.25f;
				vector.y = -vector.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - vector, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x000312D8 File Offset: 0x0002F4D8
	protected virtual void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00031320 File Offset: 0x0002F520
	protected virtual void SetText(string tooltipText)
	{
		if (this.text != null && !string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 1f;
			if (this.text != null)
			{
				this.text.text = tooltipText;
			}
			this.mPos = Input.mousePosition;
			Transform transform = this.text.transform;
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			this.mSize = this.text.printedSize;
			this.mSize.x = this.mSize.x * localScale.x;
			this.mSize.y = this.mSize.y * localScale.y;
			if (this.background != null)
			{
				Vector4 border = this.background.border;
				this.mSize.x = this.mSize.x + (border.x + border.z + (localPosition.x - border.x) * 2f);
				this.mSize.y = this.mSize.y + (border.y + border.w + (-localPosition.y - border.y) * 2f);
				this.background.width = Mathf.RoundToInt(this.mSize.x);
				this.background.height = Mathf.RoundToInt(this.mSize.y);
			}
			if (this.uiCamera != null)
			{
				this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
				this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
				float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector;
				vector..ctor(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
				this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
				this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
				this.mPos = this.mTrans.localPosition;
				this.mPos.x = Mathf.Round(this.mPos.x);
				this.mPos.y = Mathf.Round(this.mPos.y);
				this.mTrans.localPosition = this.mPos;
			}
			else
			{
				if (this.mPos.x + this.mSize.x > (float)Screen.width)
				{
					this.mPos.x = (float)Screen.width - this.mSize.x;
				}
				if (this.mPos.y - this.mSize.y < 0f)
				{
					this.mPos.y = this.mSize.y;
				}
				this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
				this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
			}
		}
		else
		{
			this.mTarget = 0f;
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x000316E4 File Offset: 0x0002F8E4
	public static void ShowText(string tooltipText)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x040005FE RID: 1534
	protected static UITooltip mInstance;

	// Token: 0x040005FF RID: 1535
	public Camera uiCamera;

	// Token: 0x04000600 RID: 1536
	public UILabel text;

	// Token: 0x04000601 RID: 1537
	public UISprite background;

	// Token: 0x04000602 RID: 1538
	public float appearSpeed = 10f;

	// Token: 0x04000603 RID: 1539
	public bool scalingTransitions = true;

	// Token: 0x04000604 RID: 1540
	protected Transform mTrans;

	// Token: 0x04000605 RID: 1541
	protected float mTarget;

	// Token: 0x04000606 RID: 1542
	protected float mCurrent;

	// Token: 0x04000607 RID: 1543
	protected Vector3 mPos;

	// Token: 0x04000608 RID: 1544
	protected Vector3 mSize = Vector3.zero;

	// Token: 0x04000609 RID: 1545
	protected UIWidget[] mWidgets;
}
