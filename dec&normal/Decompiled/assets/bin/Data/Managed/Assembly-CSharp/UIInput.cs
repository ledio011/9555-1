using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000BE RID: 190
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600058B RID: 1419 RVA: 0x00026904 File Offset: 0x00024B04
	// (set) Token: 0x0600058C RID: 1420 RVA: 0x0002690C File Offset: 0x00024B0C
	public string defaultText
	{
		get
		{
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x0600058D RID: 1421 RVA: 0x0002692C File Offset: 0x00024B2C
	// (set) Token: 0x0600058E RID: 1422 RVA: 0x00026934 File Offset: 0x00024B34
	[Obsolete("Use UIInput.value instead")]
	public string text
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

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x0600058F RID: 1423 RVA: 0x00026940 File Offset: 0x00024B40
	// (set) Token: 0x06000590 RID: 1424 RVA: 0x0002695C File Offset: 0x00024B5C
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			UIInput.mDrawStart = 0;
			if (Application.platform == 22)
			{
				value = value.Replace("\\b", "\b");
			}
			value = this.Validate(value);
			if (this.isSelected && UIInput.mKeyboard != null && this.mCached != value)
			{
				UIInput.mKeyboard.text = value;
				this.mCached = value;
			}
			if (this.mValue != value)
			{
				this.mValue = value;
				this.mLoadSavedValue = false;
				if (!this.isSelected)
				{
					this.SaveToPlayerPrefs(value);
				}
				this.UpdateLabel();
				this.ExecuteOnChange();
			}
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000591 RID: 1425 RVA: 0x00026A1C File Offset: 0x00024C1C
	// (set) Token: 0x06000592 RID: 1426 RVA: 0x00026A24 File Offset: 0x00024C24
	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000593 RID: 1427 RVA: 0x00026A30 File Offset: 0x00024C30
	// (set) Token: 0x06000594 RID: 1428 RVA: 0x00026A40 File Offset: 0x00024C40
	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000595 RID: 1429 RVA: 0x00026A6C File Offset: 0x00024C6C
	// (set) Token: 0x06000596 RID: 1430 RVA: 0x00026A7C File Offset: 0x00024C7C
	public int cursorPosition
	{
		get
		{
			return this.value.Length;
		}
		set
		{
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000597 RID: 1431 RVA: 0x00026A80 File Offset: 0x00024C80
	// (set) Token: 0x06000598 RID: 1432 RVA: 0x00026A90 File Offset: 0x00024C90
	public int selectionStart
	{
		get
		{
			return this.value.Length;
		}
		set
		{
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000599 RID: 1433 RVA: 0x00026A94 File Offset: 0x00024C94
	// (set) Token: 0x0600059A RID: 1434 RVA: 0x00026AA4 File Offset: 0x00024CA4
	public int selectionEnd
	{
		get
		{
			return this.value.Length;
		}
		set
		{
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x0600059B RID: 1435 RVA: 0x00026AA8 File Offset: 0x00024CA8
	public UITexture caret
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00026AAC File Offset: 0x00024CAC
	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		for (int i = 0; i < val.Length; i++)
		{
			char c = val.get_Chars(i);
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00026B7C File Offset: 0x00024D7C
	private void Start()
	{
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
		}
		else
		{
			this.value = this.mValue.Replace("\\n", "\n");
		}
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00026BCC File Offset: 0x00024DCC
	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.label.supportEncoding = false;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mPivot = this.label.pivot;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00026C88 File Offset: 0x00024E88
	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
			}
			else
			{
				PlayerPrefs.SetString(this.savedAs, val);
			}
		}
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00026CC4 File Offset: 0x00024EC4
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			this.OnSelectEvent();
		}
		else
		{
			this.OnDeselectEvent();
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00026CE0 File Offset: 0x00024EE0
	protected void OnSelectEvent()
	{
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.label.color = this.activeTextColor;
			if (Application.platform == 8 || Application.platform == 11)
			{
				UIInput.mKeyboard = ((this.inputType != UIInput.InputType.Password) ? TouchScreenKeyboard.Open(this.mValue, this.keyboardType, this.inputType == UIInput.InputType.AutoCorrect, this.label.multiLine, false, false, this.defaultText) : TouchScreenKeyboard.Open(this.mValue, 0, false, false, true));
			}
			else
			{
				Vector2 compositionCursorPos = (!(UICamera.current != null) || !(UICamera.current.cachedCamera != null)) ? this.label.worldCorners[0] : UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]);
				compositionCursorPos.y = (float)Screen.height - compositionCursorPos.y;
				Input.imeCompositionMode = 1;
				Input.compositionCursorPos = compositionCursorPos;
				UIInput.mDrawStart = 0;
			}
			this.UpdateLabel();
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00026E38 File Offset: 0x00025038
	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.mKeyboard != null)
			{
				UIInput.mKeyboard.active = false;
				UIInput.mKeyboard = null;
			}
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = 0;
			this.RestoreLabelPivot();
		}
		UIInput.selection = null;
		this.UpdateLabel();
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00026EFC File Offset: 0x000250FC
	private void Update()
	{
		if (UIInput.mKeyboard != null && this.isSelected)
		{
			string text = UIInput.mKeyboard.text;
			if (this.mCached != text)
			{
				this.mCached = text;
				this.value = text;
			}
			if (UIInput.mKeyboard.done || !UIInput.mKeyboard.active)
			{
				if (!UIInput.mKeyboard.wasCanceled)
				{
					this.Submit();
				}
				UIInput.mKeyboard = null;
				this.isSelected = false;
				this.mCached = string.Empty;
			}
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00026F94 File Offset: 0x00025194
	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00026FEC File Offset: 0x000251EC
	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((!flag || isSelected) ? this.activeTextColor : this.mDefaultColor);
			string text;
			if (flag)
			{
				text = ((!isSelected) ? this.mDefaultText : string.Empty);
				this.RestoreLabelPivot();
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = string.Empty;
					string text2 = "*";
					if (this.label.bitmapFont != null && this.label.bitmapFont.bmFont != null && this.label.bitmapFont.bmFont.GetGlyph(42) == null)
					{
						text2 = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += text2;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = (!isSelected) ? 0 : Mathf.Min(text.Length, this.cursorPosition);
				string text3 = text.Substring(0, num);
				if (isSelected)
				{
					text3 += Input.compositionString;
				}
				text = text3 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.RestoreLabelPivot();
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.SetPivotToLeft();
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.SetPivotToLeft();
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.SetPivotToRight();
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.RestoreLabelPivot();
				}
			}
			this.label.text = text;
		}
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00027268 File Offset: 0x00025468
	protected void SetPivotToLeft()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 0f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x000272A0 File Offset: 0x000254A0
	protected void SetPivotToRight()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 1f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x000272D8 File Offset: 0x000254D8
	protected void RestoreLabelPivot()
	{
		if (this.label != null && this.label.pivot != this.mPivot)
		{
			this.label.pivot = this.mPivot;
		}
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00027320 File Offset: 0x00025520
	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text.get_Chars(Mathf.Clamp(pos, 0, text.Length - 1));
			char c2 = (text.Length <= 0) ? '\n' : text.get_Chars(Mathf.Clamp(pos + 1, 0, text.Length - 1));
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0002758C File Offset: 0x0002578C
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x000275C8 File Offset: 0x000257C8
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000275D4 File Offset: 0x000257D4
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x000275E4 File Offset: 0x000257E4
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs) && PlayerPrefs.HasKey(this.savedAs))
		{
			this.value = PlayerPrefs.GetString(this.savedAs);
		}
	}

	// Token: 0x040004EC RID: 1260
	public static UIInput current;

	// Token: 0x040004ED RID: 1261
	public static UIInput selection;

	// Token: 0x040004EE RID: 1262
	public UILabel label;

	// Token: 0x040004EF RID: 1263
	public UIInput.InputType inputType;

	// Token: 0x040004F0 RID: 1264
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x040004F1 RID: 1265
	public UIInput.KeyboardType keyboardType;

	// Token: 0x040004F2 RID: 1266
	public UIInput.Validation validation;

	// Token: 0x040004F3 RID: 1267
	public int characterLimit;

	// Token: 0x040004F4 RID: 1268
	public string savedAs;

	// Token: 0x040004F5 RID: 1269
	public GameObject selectOnTab;

	// Token: 0x040004F6 RID: 1270
	public Color activeTextColor = Color.white;

	// Token: 0x040004F7 RID: 1271
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x040004F8 RID: 1272
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x040004F9 RID: 1273
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x040004FA RID: 1274
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040004FB RID: 1275
	public UIInput.OnValidate onValidate;

	// Token: 0x040004FC RID: 1276
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x040004FD RID: 1277
	protected string mDefaultText = string.Empty;

	// Token: 0x040004FE RID: 1278
	protected Color mDefaultColor = Color.white;

	// Token: 0x040004FF RID: 1279
	protected float mPosition;

	// Token: 0x04000500 RID: 1280
	protected bool mDoInit = true;

	// Token: 0x04000501 RID: 1281
	protected UIWidget.Pivot mPivot;

	// Token: 0x04000502 RID: 1282
	protected bool mLoadSavedValue = true;

	// Token: 0x04000503 RID: 1283
	protected static int mDrawStart;

	// Token: 0x04000504 RID: 1284
	protected static TouchScreenKeyboard mKeyboard;

	// Token: 0x04000505 RID: 1285
	private string mCached = string.Empty;

	// Token: 0x020000BF RID: 191
	public enum InputType
	{
		// Token: 0x04000507 RID: 1287
		Standard,
		// Token: 0x04000508 RID: 1288
		AutoCorrect,
		// Token: 0x04000509 RID: 1289
		Password
	}

	// Token: 0x020000C0 RID: 192
	public enum Validation
	{
		// Token: 0x0400050B RID: 1291
		None,
		// Token: 0x0400050C RID: 1292
		Integer,
		// Token: 0x0400050D RID: 1293
		Float,
		// Token: 0x0400050E RID: 1294
		Alphanumeric,
		// Token: 0x0400050F RID: 1295
		Username,
		// Token: 0x04000510 RID: 1296
		Name
	}

	// Token: 0x020000C1 RID: 193
	public enum KeyboardType
	{
		// Token: 0x04000512 RID: 1298
		Default,
		// Token: 0x04000513 RID: 1299
		ASCIICapable,
		// Token: 0x04000514 RID: 1300
		NumbersAndPunctuation,
		// Token: 0x04000515 RID: 1301
		URL,
		// Token: 0x04000516 RID: 1302
		NumberPad,
		// Token: 0x04000517 RID: 1303
		PhonePad,
		// Token: 0x04000518 RID: 1304
		NamePhonePad,
		// Token: 0x04000519 RID: 1305
		EmailAddress
	}

	// Token: 0x020000C2 RID: 194
	public enum OnReturnKey
	{
		// Token: 0x0400051B RID: 1307
		Default,
		// Token: 0x0400051C RID: 1308
		Submit,
		// Token: 0x0400051D RID: 1309
		NewLine
	}

	// Token: 0x02000AA9 RID: 2729
	// (Invoke) Token: 0x06004F2D RID: 20269
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
