using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000065 RID: 101
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
	// (set) Token: 0x060001DC RID: 476 RVA: 0x0000C628 File Offset: 0x0000A828
	public Object ambigiousFont
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.trueTypeFont;
			}
			if (this.bitmapFont != null)
			{
				return this.bitmapFont;
			}
			return this.font;
		}
		set
		{
			if (value is Font)
			{
				this.trueTypeFont = (value as Font);
				this.bitmapFont = null;
				this.font = null;
			}
			else if (value is UIFont)
			{
				this.bitmapFont = (value as UIFont);
				this.trueTypeFont = null;
				this.font = null;
			}
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060001DD RID: 477 RVA: 0x0000C684 File Offset: 0x0000A884
	// (set) Token: 0x060001DE RID: 478 RVA: 0x0000C68C File Offset: 0x0000A88C
	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public UIPopupList.LegacyEvent onSelectionChange
	{
		get
		{
			return this.mLegacyEvent;
		}
		set
		{
			this.mLegacyEvent = value;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060001DF RID: 479 RVA: 0x0000C698 File Offset: 0x0000A898
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
	// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
	public string value
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			this.mSelectedItem = value;
			if (this.mSelectedItem == null)
			{
				return;
			}
			if (this.mSelectedItem != null)
			{
				this.TriggerCallbacks();
			}
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
	// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000C6EC File Offset: 0x0000A8EC
	[Obsolete("Use 'value' instead")]
	public string selection
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
	// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000C724 File Offset: 0x0000A924
	private bool handleEvents
	{
		get
		{
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			return component == null || !component.enabled;
		}
		set
		{
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000C750 File Offset: 0x0000A950
	private bool isValid
	{
		get
		{
			return this.bitmapFont != null || this.trueTypeFont != null;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000C780 File Offset: 0x0000A980
	private int activeFontSize
	{
		get
		{
			return (!(this.trueTypeFont != null) && !(this.bitmapFont == null)) ? this.bitmapFont.defaultSize : this.fontSize;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
	private float activeFontScale
	{
		get
		{
			return (!(this.trueTypeFont != null) && !(this.bitmapFont == null)) ? ((float)this.fontSize / (float)this.bitmapFont.defaultSize) : 1f;
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000C818 File Offset: 0x0000AA18
	protected void TriggerCallbacks()
	{
		if (UIPopupList.current != this)
		{
			UIPopupList uipopupList = UIPopupList.current;
			UIPopupList.current = this;
			if (this.mLegacyEvent != null)
			{
				this.mLegacyEvent(this.mSelectedItem);
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				EventDelegate.Execute(this.onChange);
			}
			else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, 1);
			}
			UIPopupList.current = uipopupList;
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000C8BC File Offset: 0x0000AABC
	private void OnEnable()
	{
		if (EventDelegate.IsValid(this.onChange))
		{
			this.eventReceiver = null;
			this.functionName = null;
		}
		if (this.font != null)
		{
			if (this.font.isDynamic)
			{
				this.trueTypeFont = this.font.dynamicFont;
				this.fontStyle = this.font.dynamicFontStyle;
				this.mUseDynamicFont = true;
			}
			else if (this.bitmapFont == null)
			{
				this.bitmapFont = this.font;
				this.mUseDynamicFont = false;
			}
			this.font = null;
		}
		if (this.textScale != 0f)
		{
			this.fontSize = ((!(this.bitmapFont != null)) ? 16 : Mathf.RoundToInt((float)this.bitmapFont.defaultSize * this.textScale));
			this.textScale = 0f;
		}
		if (this.trueTypeFont == null && this.bitmapFont != null && this.bitmapFont.isDynamic)
		{
			this.trueTypeFont = this.bitmapFont.dynamicFont;
			this.bitmapFont = null;
		}
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000C9FC File Offset: 0x0000ABFC
	private void OnValidate()
	{
		Font font = this.trueTypeFont;
		UIFont uifont = this.bitmapFont;
		this.bitmapFont = null;
		this.trueTypeFont = null;
		if (font != null && (uifont == null || !this.mUseDynamicFont))
		{
			this.bitmapFont = null;
			this.trueTypeFont = font;
			this.mUseDynamicFont = true;
		}
		else if (uifont != null)
		{
			if (uifont.isDynamic)
			{
				this.trueTypeFont = uifont.dynamicFont;
				this.fontStyle = uifont.dynamicFontStyle;
				this.fontSize = uifont.defaultSize;
				this.mUseDynamicFont = true;
			}
			else
			{
				this.bitmapFont = uifont;
				this.mUseDynamicFont = false;
			}
		}
		else
		{
			this.trueTypeFont = font;
			this.mUseDynamicFont = true;
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000CACC File Offset: 0x0000ACCC
	private void Start()
	{
		if (this.textLabel != null)
		{
			EventDelegate.Add(this.onChange, new EventDelegate.Callback(this.textLabel.SetCurrentSelection));
			this.textLabel = null;
		}
		if (Application.isPlaying)
		{
			if (string.IsNullOrEmpty(this.mSelectedItem))
			{
				if (this.items.Count > 0)
				{
					this.value = this.items[0];
				}
			}
			else
			{
				string value = this.mSelectedItem;
				this.mSelectedItem = null;
				this.value = value;
			}
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000CB64 File Offset: 0x0000AD64
	private void OnLocalize()
	{
		if (this.isLocalized)
		{
			this.TriggerCallbacks();
		}
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000CB78 File Offset: 0x0000AD78
	private void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			TweenPosition component = lbl.GetComponent<TweenPosition>();
			if (component != null && component.enabled)
			{
				return;
			}
			this.mHighlightedLabel = lbl;
			UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float pixelSize = this.atlas.pixelSize;
			float num = (float)atlasSprite.borderLeft * pixelSize;
			float num2 = (float)atlasSprite.borderTop * pixelSize;
			Vector3 vector = lbl.cachedTransform.localPosition + new Vector3(-num, num2, 1f);
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = vector;
			}
			else
			{
				TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, vector).method = UITweener.Method.EaseOut;
			}
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000CC58 File Offset: 0x0000AE58
	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000CC7C File Offset: 0x0000AE7C
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		UIEventListener component = lbl.gameObject.GetComponent<UIEventListener>();
		this.value = (component.parameter as string);
		UIPlaySound[] components = base.GetComponents<UIPlaySound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIPlaySound uiplaySound = components[i];
			if (uiplaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uiplaySound.audioClip, uiplaySound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000CD10 File Offset: 0x0000AF10
	private void OnKey(KeyCode key)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.handleEvents)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (num == -1)
			{
				num = 0;
			}
			if (key == 273)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
				}
			}
			else if (key == 274)
			{
				if (num + 1 < this.mLabelList.Count)
				{
					this.Select(this.mLabelList[num + 1], false);
				}
			}
			else if (key == 27)
			{
				this.OnSelect(false);
			}
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
	private void OnSelect(bool isSelected)
	{
		if (!isSelected)
		{
			this.Close();
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
	public void Close()
	{
		if (this.mChild != null)
		{
			this.mLabelList.Clear();
			this.handleEvents = false;
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				Collider[] componentsInChildren2 = this.mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				int num2 = componentsInChildren2.Length;
				while (j < num2)
				{
					componentsInChildren2[j].enabled = false;
					j++;
				}
				Object.Destroy(this.mChild, 0.15f);
			}
			else
			{
				Object.Destroy(this.mChild);
			}
			this.mBackground = null;
			this.mHighlight = null;
			this.mChild = null;
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000CF30 File Offset: 0x0000B130
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.activeFontSize * this.activeFontScale + this.mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(1f, num / (float)widget.height, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000D05C File Offset: 0x0000B25C
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000D070 File Offset: 0x0000B270
	private void OnClick()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mChild == null && this.atlas != null && this.isValid && this.items.Count > 0)
		{
			this.mLabelList.Clear();
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform);
				if (this.mPanel == null)
				{
					return;
				}
			}
			this.handleEvents = true;
			Transform transform = base.transform;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = bounds.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.depth = NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = (float)atlasSprite.borderTop;
			float num2 = (float)this.activeFontSize;
			float activeFontScale = this.activeFontScale;
			float num3 = num2 * activeFontScale;
			float num4 = 0f;
			float num5 = -this.padding.y;
			int num6 = (!(this.bitmapFont != null)) ? this.fontSize : this.bitmapFont.defaultSize;
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = NGUITools.AddWidget<UILabel>(this.mChild);
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.bitmapFont = this.bitmapFont;
				uilabel.trueTypeFont = this.trueTypeFont;
				uilabel.fontSize = num6;
				uilabel.fontStyle = this.fontStyle;
				uilabel.text = ((!this.isLocalized) ? text : Localization.Get(text));
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x + this.padding.x, num5, -1f);
				uilabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uilabel.MakePixelPerfect();
				if (activeFontScale != 1f)
				{
					uilabel.cachedTransform.localScale = Vector3.one * activeFontScale;
				}
				list.Add(uilabel);
				num5 -= num3;
				num5 -= this.padding.y;
				num4 = Mathf.Max(num4, uilabel.printedSize.x);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(this.mSelectedItem)))
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num4 = Mathf.Max(num4, bounds.size.x * activeFontScale - (border.x + this.padding.x) * 2f);
			float num7 = num4 / activeFontScale;
			Vector3 center;
			center..ctor(num7 * 0.5f, -num2 * 0.5f, 0f);
			Vector3 size;
			size..ctor(num7, (num3 + this.padding.y) / activeFontScale, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				UILabel uilabel2 = list[j];
				BoxCollider boxCollider = NGUITools.AddWidgetCollider(uilabel2.gameObject);
				center.z = boxCollider.center.z;
				boxCollider.center = center;
				boxCollider.size = size;
				j++;
			}
			num4 += (border.x + this.padding.x) * 2f;
			num5 -= border.y;
			this.mBackground.width = Mathf.RoundToInt(num4);
			this.mBackground.height = Mathf.RoundToInt(-num5 + border.y);
			float num8 = 2f * this.atlas.pixelSize;
			float num9 = num4 - (border.x + this.padding.x) * 2f + (float)atlasSprite.borderLeft * num8;
			float num10 = num3 + num * num8;
			this.mHighlight.width = Mathf.RoundToInt(num9);
			this.mHighlight.height = Mathf.RoundToInt(num10);
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f);
				}
			}
			if (this.isAnimated)
			{
				float bottom = num5 + num3;
				this.Animate(this.mHighlight, flag, bottom);
				int k = 0;
				int count3 = list.Count;
				while (k < count3)
				{
					this.Animate(list[k], flag, bottom);
					k++;
				}
				this.AnimateColor(this.mBackground);
				this.AnimateScale(this.mBackground, flag, bottom);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(bounds.min.x, bounds.max.y - num5 - border.y, bounds.min.z);
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x0400021A RID: 538
	private const float animSpeed = 0.15f;

	// Token: 0x0400021B RID: 539
	public static UIPopupList current;

	// Token: 0x0400021C RID: 540
	public UIAtlas atlas;

	// Token: 0x0400021D RID: 541
	public UIFont bitmapFont;

	// Token: 0x0400021E RID: 542
	public Font trueTypeFont;

	// Token: 0x0400021F RID: 543
	public int fontSize = 16;

	// Token: 0x04000220 RID: 544
	public FontStyle fontStyle;

	// Token: 0x04000221 RID: 545
	public string backgroundSprite;

	// Token: 0x04000222 RID: 546
	public string highlightSprite;

	// Token: 0x04000223 RID: 547
	public UIPopupList.Position position;

	// Token: 0x04000224 RID: 548
	public List<string> items = new List<string>();

	// Token: 0x04000225 RID: 549
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x04000226 RID: 550
	public Color textColor = Color.white;

	// Token: 0x04000227 RID: 551
	public Color backgroundColor = Color.white;

	// Token: 0x04000228 RID: 552
	public Color highlightColor = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x04000229 RID: 553
	public bool isAnimated = true;

	// Token: 0x0400022A RID: 554
	public bool isLocalized;

	// Token: 0x0400022B RID: 555
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x0400022C RID: 556
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x0400022D RID: 557
	private UIPanel mPanel;

	// Token: 0x0400022E RID: 558
	private GameObject mChild;

	// Token: 0x0400022F RID: 559
	private UISprite mBackground;

	// Token: 0x04000230 RID: 560
	private UISprite mHighlight;

	// Token: 0x04000231 RID: 561
	private UILabel mHighlightedLabel;

	// Token: 0x04000232 RID: 562
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x04000233 RID: 563
	private float mBgBorder;

	// Token: 0x04000234 RID: 564
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000235 RID: 565
	[SerializeField]
	[HideInInspector]
	private string functionName = "OnSelectionChange";

	// Token: 0x04000236 RID: 566
	[HideInInspector]
	[SerializeField]
	private float textScale;

	// Token: 0x04000237 RID: 567
	[HideInInspector]
	[SerializeField]
	private UIFont font;

	// Token: 0x04000238 RID: 568
	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	// Token: 0x04000239 RID: 569
	private UIPopupList.LegacyEvent mLegacyEvent;

	// Token: 0x0400023A RID: 570
	private bool mUseDynamicFont;

	// Token: 0x02000066 RID: 102
	public enum Position
	{
		// Token: 0x0400023C RID: 572
		Auto,
		// Token: 0x0400023D RID: 573
		Above,
		// Token: 0x0400023E RID: 574
		Below
	}

	// Token: 0x02000A96 RID: 2710
	// (Invoke) Token: 0x06004EE1 RID: 20193
	public delegate void LegacyEvent(string val);
}
