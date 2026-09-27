using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000061 RID: 97
	[AddComponentMenu("UI/Input Field", 31)]
	public class InputField : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler, IPointerClickHandler, ISubmitHandler, IUpdateSelectedHandler, ICanvasElement
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000ACDC File Offset: 0x00008EDC
		protected InputField()
		{
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000AD80 File Offset: 0x00008F80
		protected TextGenerator cachedInputTextGenerator
		{
			get
			{
				if (this.m_InputTextCache == null)
				{
					this.m_InputTextCache = new TextGenerator();
				}
				return this.m_InputTextCache;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				switch (platform)
				{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
					break;
				default:
					if (platform != RuntimePlatform.BB10Player)
					{
						return true;
					}
					break;
				}
				return this.m_HideMobileInput;
			}
			set
			{
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000AE4C File Offset: 0x0000904C
		public string text
		{
			get
			{
				if (InputField.m_Keyboard != null && InputField.m_Keyboard.active && !this.InPlaceEditing() && EventSystem.current.currentSelectedGameObject == base.gameObject)
				{
					return InputField.m_Keyboard.text;
				}
				return this.m_Text;
			}
			set
			{
				if (this.text == value)
				{
					return;
				}
				this.m_Text = value;
				if (InputField.m_Keyboard != null)
				{
					InputField.m_Keyboard.text = this.m_Text;
				}
				if (this.m_CaretPosition > this.m_Text.Length)
				{
					this.m_CaretPosition = (this.m_CaretSelectPosition = this.m_Text.Length);
				}
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000AECC File Offset: 0x000090CC
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000AED4 File Offset: 0x000090D4
		public float caretBlinkRate
		{
			get
			{
				return this.m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_CaretBlinkRate, value) && this.m_AllowInput)
				{
					this.SetCaretActive();
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AEF8 File Offset: 0x000090F8
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000AF00 File Offset: 0x00009100
		public Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				SetPropertyUtility.SetClass<Text>(ref this.m_TextComponent, value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000AF10 File Offset: 0x00009110
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000AF18 File Offset: 0x00009118
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass<Graphic>(ref this.m_Placeholder, value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000AF28 File Offset: 0x00009128
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000AF30 File Offset: 0x00009130
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				SetPropertyUtility.SetColor(ref this.m_SelectionColor, value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000AF40 File Offset: 0x00009140
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000AF48 File Offset: 0x00009148
		public InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_EndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.SubmitEvent>(ref this.m_EndEdit, value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000AF58 File Offset: 0x00009158
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000AF60 File Offset: 0x00009160
		public InputField.OnChangeEvent onValueChange
		{
			get
			{
				return this.m_OnValueChange;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnChangeEvent>(ref this.m_OnValueChange, value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000AF70 File Offset: 0x00009170
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000AF78 File Offset: 0x00009178
		public InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000AF88 File Offset: 0x00009188
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000AF90 File Offset: 0x00009190
		public int characterLimit
		{
			get
			{
				return this.m_CharacterLimit;
			}
			set
			{
				SetPropertyUtility.SetStruct<int>(ref this.m_CharacterLimit, value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AFA0 File Offset: 0x000091A0
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AFC4 File Offset: 0x000091C4
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000AFCC File Offset: 0x000091CC
		public InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new InputField.ContentType[]
					{
						InputField.ContentType.Standard,
						InputField.ContentType.Autocorrected
					});
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AFF0 File Offset: 0x000091F0
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000AFF8 File Offset: 0x000091F8
		public InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B014 File Offset: 0x00009214
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B01C File Offset: 0x0000921C
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TouchScreenKeyboardType>(ref this.m_KeyboardType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B038 File Offset: 0x00009238
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B040 File Offset: 0x00009240
		public InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B05C File Offset: 0x0000925C
		public bool multiLine
		{
			get
			{
				return this.m_LineType == InputField.LineType.MultiLineNewline || this.lineType == InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B078 File Offset: 0x00009278
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000B080 File Offset: 0x00009280
		public char asteriskChar
		{
			get
			{
				return this.m_AsteriskChar;
			}
			set
			{
				SetPropertyUtility.SetStruct<char>(ref this.m_AsteriskChar, value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000B090 File Offset: 0x00009290
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B098 File Offset: 0x00009298
		protected void ClampPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
			}
			else if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B0CC File Offset: 0x000092CC
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000B0E0 File Offset: 0x000092E0
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + Input.compositionString.Length;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B0F8 File Offset: 0x000092F8
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000B10C File Offset: 0x0000930C
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + Input.compositionString.Length;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B124 File Offset: 0x00009324
		private bool hasSelection
		{
			get
			{
				return this.caretPositionInternal != this.caretSelectPositionInternal;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B138 File Offset: 0x00009338
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000B14C File Offset: 0x0000934C
		public int caretPosition
		{
			get
			{
				return this.m_CaretSelectPosition + Input.compositionString.Length;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B15C File Offset: 0x0000935C
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000B170 File Offset: 0x00009370
		public int selectionAnchorPosition
		{
			get
			{
				return this.m_CaretPosition + Input.compositionString.Length;
			}
			set
			{
				if (Input.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B198 File Offset: 0x00009398
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B1AC File Offset: 0x000093AC
		public int selectionFocusPosition
		{
			get
			{
				return this.m_CaretSelectPosition + Input.compositionString.Length;
			}
			set
			{
				if (Input.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B1D4 File Offset: 0x000093D4
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_DrawStart = 0;
			this.m_DrawEnd = this.m_Text.Length;
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.UpdateLabel();
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B25C File Offset: 0x0000945C
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer)
			{
				this.m_CachedInputRenderer.SetVertices(null, 0);
			}
			base.OnDisable();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B2E0 File Offset: 0x000094E0
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while (this.isFocused && this.m_CaretBlinkRate > 0f)
			{
				float blinkPeriod = 1f / this.m_CaretBlinkRate;
				bool blinkState = (Time.unscaledTime - this.m_BlinkStartTime) % blinkPeriod < blinkPeriod / 2f;
				if (this.m_CaretVisible != blinkState)
				{
					this.m_CaretVisible = blinkState;
					this.UpdateGeometry();
				}
				yield return null;
			}
			this.m_BlinkCoroutine = null;
			yield break;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B2FC File Offset: 0x000094FC
		private void SetCaretVisible()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_CaretVisible = true;
			this.m_BlinkStartTime = Time.unscaledTime;
			this.SetCaretActive();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B324 File Offset: 0x00009524
		private void SetCaretActive()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			if (this.m_CaretBlinkRate > 0f)
			{
				if (this.m_BlinkCoroutine == null)
				{
					this.m_BlinkCoroutine = base.StartCoroutine(this.CaretBlink());
				}
			}
			else
			{
				this.m_CaretVisible = true;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B378 File Offset: 0x00009578
		protected void OnFocus()
		{
			this.SelectAll();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B380 File Offset: 0x00009580
		protected void SelectAll()
		{
			this.caretPositionInternal = this.text.Length;
			this.caretSelectPositionInternal = 0;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B39C File Offset: 0x0000959C
		public void MoveTextEnd(bool shift)
		{
			int length = this.text.Length;
			if (shift)
			{
				this.caretSelectPositionInternal = length;
			}
			else
			{
				this.caretPositionInternal = length;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public void MoveTextStart(bool shift)
		{
			int num = 0;
			if (shift)
			{
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B41C File Offset: 0x0000961C
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000B444 File Offset: 0x00009644
		private static string clipboard
		{
			get
			{
				TextEditor textEditor = new TextEditor();
				textEditor.Paste();
				return textEditor.content.text;
			}
			set
			{
				TextEditor textEditor = new TextEditor();
				textEditor.content = new GUIContent(value);
				textEditor.OnFocus();
				textEditor.Copy();
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B470 File Offset: 0x00009670
		private bool InPlaceEditing()
		{
			return !TouchScreenKeyboard.isSupported;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B47C File Offset: 0x0000967C
		protected virtual void LateUpdate()
		{
			if (this.m_ShouldActivateNextUpdate)
			{
				if (!this.isFocused)
				{
					this.ActivateInputFieldInternal();
					this.m_ShouldActivateNextUpdate = false;
					return;
				}
				this.m_ShouldActivateNextUpdate = false;
			}
			if (this.InPlaceEditing() || !this.isFocused)
			{
				return;
			}
			this.AssignPositioningIfNeeded();
			if (InputField.m_Keyboard == null || !InputField.m_Keyboard.active)
			{
				if (InputField.m_Keyboard != null && InputField.m_Keyboard.wasCanceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
				return;
			}
			string text = InputField.m_Keyboard.text;
			if (this.m_Text != text)
			{
				this.m_Text = string.Empty;
				foreach (char c in text)
				{
					if (c == '\r' || c == '\u0003')
					{
						c = '\n';
					}
					if (this.onValidateInput != null)
					{
						c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
					}
					else if (this.characterValidation != InputField.CharacterValidation.None)
					{
						c = this.Validate(this.m_Text, this.m_Text.Length, c);
					}
					if (this.lineType == InputField.LineType.MultiLineSubmit && c == '\n')
					{
						InputField.m_Keyboard.text = this.m_Text;
						this.OnDeselect(null);
						return;
					}
					if (c != '\0')
					{
						this.m_Text += c;
					}
				}
				if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
				{
					this.m_Text = this.m_Text.Substring(0, this.characterLimit);
				}
				int length = this.m_Text.Length;
				this.caretSelectPositionInternal = length;
				this.caretPositionInternal = length;
				if (this.m_Text != text)
				{
					InputField.m_Keyboard.text = this.m_Text;
				}
				this.SendOnValueChangedAndUpdateLabel();
			}
			if (InputField.m_Keyboard.done)
			{
				if (InputField.m_Keyboard.wasCanceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B6AC File Offset: 0x000098AC
		public Vector2 ScreenToLocal(Vector2 screen)
		{
			Canvas canvas = this.m_TextComponent.canvas;
			if (canvas == null)
			{
				return screen;
			}
			Vector3 vector = Vector3.zero;
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				vector = this.m_TextComponent.transform.InverseTransformPoint(screen);
			}
			else if (canvas.worldCamera != null)
			{
				Ray ray = canvas.worldCamera.ScreenPointToRay(screen);
				Plane plane = new Plane(this.m_TextComponent.transform.forward, this.m_TextComponent.transform.position);
				float distance;
				plane.Raycast(ray, out distance);
				vector = this.m_TextComponent.transform.InverseTransformPoint(ray.GetPoint(distance));
			}
			return new Vector2(vector.x, vector.y);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B784 File Offset: 0x00009984
		private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			float num = this.m_TextComponent.rectTransform.rect.yMax;
			if (pos.y > num)
			{
				return -1;
			}
			for (int i = 0; i < generator.lineCount; i++)
			{
				float num2 = (float)generator.lines[i].height / this.m_TextComponent.pixelsPerUnit;
				if (pos.y <= num && pos.y > num - num2)
				{
					return i;
				}
				num -= num2;
			}
			return generator.lineCount;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B828 File Offset: 0x00009A28
		protected int GetCharacterIndexFromPosition(Vector2 pos)
		{
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount == 0)
			{
				return 0;
			}
			int unclampedCharacterLineFromPosition = this.GetUnclampedCharacterLineFromPosition(pos, cachedTextGenerator);
			if (unclampedCharacterLineFromPosition < 0)
			{
				return 0;
			}
			if (unclampedCharacterLineFromPosition >= cachedTextGenerator.lineCount)
			{
				return cachedTextGenerator.characterCountVisible;
			}
			int startCharIdx = cachedTextGenerator.lines[unclampedCharacterLineFromPosition].startCharIdx;
			int lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, unclampedCharacterLineFromPosition);
			for (int i = startCharIdx; i < lineEndPosition; i++)
			{
				if (i >= cachedTextGenerator.characterCountVisible)
				{
					break;
				}
				UICharInfo uicharInfo = cachedTextGenerator.characters[i];
				Vector2 vector = uicharInfo.cursorPos / this.m_TextComponent.pixelsPerUnit;
				float num = pos.x - vector.x;
				float num2 = vector.x + uicharInfo.charWidth / this.m_TextComponent.pixelsPerUnit - pos.x;
				if (num < num2)
				{
					return i;
				}
			}
			return lineEndPosition;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B924 File Offset: 0x00009B24
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && InputField.m_Keyboard == null;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B964 File Offset: 0x00009B64
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B97C File Offset: 0x00009B7C
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera, out pos);
			this.caretSelectPositionInternal = this.GetCharacterIndexFromPosition(pos) + this.m_DrawStart;
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BA24 File Offset: 0x00009C24
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 localMousePos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera, out localMousePos);
				Rect rect = this.textComponent.rectTransform.rect;
				if (this.multiLine)
				{
					if (localMousePos.y > rect.yMax)
					{
						this.MoveUp(true, true);
					}
					else if (localMousePos.y < rect.yMin)
					{
						this.MoveDown(true, true);
					}
				}
				else if (localMousePos.x < rect.xMin)
				{
					this.MoveLeft(true, false);
				}
				else if (localMousePos.x > rect.xMax)
				{
					this.MoveRight(true, false);
				}
				this.UpdateLabel();
				float delay = (!this.multiLine) ? 0.05f : 0.1f;
				yield return new WaitForSeconds(delay);
			}
			this.m_DragCoroutine = null;
			yield break;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BA50 File Offset: 0x00009C50
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000BA68 File Offset: 0x00009C68
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (InputField.m_Keyboard == null || !InputField.m_Keyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			if (allowInput)
			{
				Vector2 pos = this.ScreenToLocal(eventData.position);
				int num = this.GetCharacterIndexFromPosition(pos) + this.m_DrawStart;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
			this.UpdateLabel();
			eventData.Use();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BB0C File Offset: 0x00009D0C
		protected InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			RuntimePlatform platform = Application.platform;
			bool flag = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer || platform == RuntimePlatform.OSXWebPlayer;
			bool flag2 = (!flag) ? ((modifiers & EventModifiers.Control) != EventModifiers.None) : ((modifiers & EventModifiers.Command) != EventModifiers.None);
			bool flag3 = (modifiers & EventModifiers.Shift) != EventModifiers.None;
			bool flag4 = (modifiers & EventModifiers.Alt) != EventModifiers.None;
			bool flag5 = flag2 && !flag4 && !flag3;
			KeyCode keyCode = evt.keyCode;
			switch (keyCode)
			{
			case KeyCode.KeypadEnter:
				break;
			default:
				switch (keyCode)
				{
				case KeyCode.A:
					if (flag5)
					{
						this.SelectAll();
						return InputField.EditState.Continue;
					}
					goto IL_1CF;
				default:
					switch (keyCode)
					{
					case KeyCode.V:
						if (flag5)
						{
							this.Append(InputField.clipboard);
							return InputField.EditState.Continue;
						}
						goto IL_1CF;
					default:
						if (keyCode == KeyCode.Backspace)
						{
							this.Backspace();
							return InputField.EditState.Continue;
						}
						if (keyCode != KeyCode.Return)
						{
							if (keyCode == KeyCode.Escape)
							{
								this.m_WasCanceled = true;
								return InputField.EditState.Finish;
							}
							if (keyCode != KeyCode.Delete)
							{
								goto IL_1CF;
							}
							this.ForwardSpace();
							return InputField.EditState.Continue;
						}
						break;
					case KeyCode.X:
						if (flag5)
						{
							InputField.clipboard = this.GetSelectedString();
							this.Delete();
							this.SendOnValueChangedAndUpdateLabel();
							return InputField.EditState.Continue;
						}
						goto IL_1CF;
					}
					break;
				case KeyCode.C:
					if (flag5)
					{
						InputField.clipboard = this.GetSelectedString();
						return InputField.EditState.Continue;
					}
					goto IL_1CF;
				}
				break;
			case KeyCode.UpArrow:
				this.MoveUp(flag3);
				return InputField.EditState.Continue;
			case KeyCode.DownArrow:
				this.MoveDown(flag3);
				return InputField.EditState.Continue;
			case KeyCode.RightArrow:
				this.MoveRight(flag3, flag2);
				return InputField.EditState.Continue;
			case KeyCode.LeftArrow:
				this.MoveLeft(flag3, flag2);
				return InputField.EditState.Continue;
			case KeyCode.Home:
				this.MoveTextStart(flag3);
				return InputField.EditState.Continue;
			case KeyCode.End:
				this.MoveTextEnd(flag3);
				return InputField.EditState.Continue;
			}
			if (this.lineType != InputField.LineType.MultiLineNewline)
			{
				return InputField.EditState.Finish;
			}
			IL_1CF:
			if (!this.multiLine && evt.character == '\t')
			{
				return InputField.EditState.Continue;
			}
			char c = evt.character;
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && Input.compositionString.Length > 0)
			{
				this.UpdateLabel();
			}
			return InputField.EditState.Continue;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000BD54 File Offset: 0x00009F54
		private bool IsValidChar(char c)
		{
			return c != '\u007f' && (c == '\t' || c == '\n' || this.m_TextComponent.font.HasCharacter(c));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000BD84 File Offset: 0x00009F84
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BD90 File Offset: 0x00009F90
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				if (this.m_ProcessingEvent.rawType == EventType.KeyDown)
				{
					flag = true;
					InputField.EditState editState = this.KeyPressed(this.m_ProcessingEvent);
					if (editState == InputField.EditState.Finish)
					{
						this.DeactivateInputField();
						break;
					}
				}
			}
			if (flag)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BE04 File Offset: 0x0000A004
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return string.Empty;
			}
			int num = this.caretPositionInternal;
			int num2 = this.caretSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = num; i < num2; i++)
			{
				stringBuilder.Append(this.text[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BE74 File Offset: 0x0000A074
		private int FindtNextWordBegin()
		{
			if (this.caretSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal + 1);
			if (num == -1)
			{
				num = this.text.Length;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
				return;
			}
			int num2;
			if (ctrl)
			{
				num2 = this.FindtNextWordBegin();
			}
			else
			{
				num2 = this.caretSelectPositionInternal + 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BF58 File Offset: 0x0000A158
		private int FindtPrevWordBegin()
		{
			if (this.caretSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal - 2);
			if (num == -1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
				return;
			}
			int num2;
			if (ctrl)
			{
				num2 = this.FindtPrevWordBegin();
			}
			else
			{
				num2 = this.caretSelectPositionInternal - 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C020 File Offset: 0x0000A220
		private int DetermineCharacterLine(int charPos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			for (int i = 0; i < generator.lineCount - 1; i++)
			{
				if (generator.lines[i + 1].startCharIdx > charPos)
				{
					return i;
				}
			}
			return generator.lineCount - 1;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C07C File Offset: 0x0000A27C
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return 0;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num - 1 < 0)
			{
				return (!goToFirstChar) ? originalPos : 0;
			}
			int num2 = this.cachedInputTextGenerator.lines[num].startCharIdx - 1;
			for (int i = this.cachedInputTextGenerator.lines[num - 1].startCharIdx; i < num2; i++)
			{
				if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
				{
					return i;
				}
			}
			return num2;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C150 File Offset: 0x0000A350
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return this.text.Length;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num + 1 >= this.cachedInputTextGenerator.lineCount)
			{
				return (!goToLastChar) ? originalPos : this.text.Length;
			}
			int lineEndPosition = InputField.GetLineEndPosition(this.cachedInputTextGenerator, num + 1);
			for (int i = this.cachedInputTextGenerator.lines[num + 1].startCharIdx; i < lineEndPosition; i++)
			{
				if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
				{
					return i;
				}
			}
			return lineEndPosition;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000C234 File Offset: 0x0000A434
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C240 File Offset: 0x0000A440
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
			}
			int num2 = (!this.multiLine) ? this.text.Length : this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar);
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		private void MoveUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				int num = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = num;
				this.caretPositionInternal = num;
			}
			int num2 = (!this.multiLine) ? 0 : this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar);
			if (shift)
			{
				this.caretSelectPositionInternal = num2;
			}
			else
			{
				int num = num2;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C358 File Offset: 0x0000A558
		private void Delete()
		{
			if (this.caretPositionInternal == this.caretSelectPositionInternal)
			{
				return;
			}
			if (this.caretPositionInternal < this.caretSelectPositionInternal)
			{
				this.m_Text = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			else
			{
				this.m_Text = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
				this.caretPositionInternal = this.caretSelectPositionInternal;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C428 File Offset: 0x0000A628
		private void ForwardSpace()
		{
			if (this.hasSelection)
			{
				this.Delete();
				this.SendOnValueChangedAndUpdateLabel();
			}
			else if (this.caretPositionInternal < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal, 1);
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C488 File Offset: 0x0000A688
		private void Backspace()
		{
			if (this.hasSelection)
			{
				this.Delete();
				this.SendOnValueChangedAndUpdateLabel();
			}
			else if (this.caretPositionInternal > 0)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal - 1, 1);
				int num = this.caretPositionInternal - 1;
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = num;
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		private void Insert(char c)
		{
			string text = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_CaretPosition, text);
			this.caretSelectPositionInternal = (this.caretPositionInternal += text.Length);
			this.SendOnValueChanged();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C56C File Offset: 0x0000A76C
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.SendOnValueChanged();
			this.UpdateLabel();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C57C File Offset: 0x0000A77C
		private void SendOnValueChanged()
		{
			if (this.onValueChange != null)
			{
				this.onValueChange.Invoke(this.text);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C59C File Offset: 0x0000A79C
		protected void SendOnSubmit()
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		protected virtual void Append(string input)
		{
			if (!this.InPlaceEditing())
			{
				return;
			}
			int i = 0;
			int length = input.Length;
			while (i < length)
			{
				char c = input[i];
				if (c >= ' ')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C608 File Offset: 0x0000A808
		protected virtual void Append(char input)
		{
			if (!this.InPlaceEditing())
			{
				return;
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(this.text, this.caretPositionInternal, input);
			}
			else if (this.characterValidation != InputField.CharacterValidation.None)
			{
				input = this.Validate(this.text, this.caretPositionInternal, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C67C File Offset: 0x0000A87C
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventFontCallback)
			{
				string text;
				if (Input.compositionString.Length > 0)
				{
					text = this.text.Substring(0, this.m_CaretPosition) + Input.compositionString + this.text.Substring(this.m_CaretPosition);
				}
				else
				{
					text = this.text;
				}
				string text2;
				if (this.inputType == InputField.InputType.Password)
				{
					text2 = new string(this.asteriskChar, text.Length);
				}
				else
				{
					text2 = text;
				}
				bool flag = string.IsNullOrEmpty(text);
				if (this.m_Placeholder != null)
				{
					this.m_Placeholder.enabled = flag;
				}
				if (!this.m_AllowInput)
				{
					this.m_DrawStart = 0;
					this.m_DrawEnd = this.m_Text.Length;
				}
				if (!flag)
				{
					Vector2 size = this.m_TextComponent.rectTransform.rect.size;
					TextGenerationSettings generationSettings = this.m_TextComponent.GetGenerationSettings(size);
					generationSettings.generateOutOfBounds = true;
					this.m_PreventFontCallback = true;
					this.cachedInputTextGenerator.Populate(text2, generationSettings);
					this.m_PreventFontCallback = false;
					this.SetDrawRangeToContainCaretPosition(this.cachedInputTextGenerator, this.caretSelectPositionInternal, ref this.m_DrawStart, ref this.m_DrawEnd);
					text2 = text2.Substring(this.m_DrawStart, Mathf.Min(this.m_DrawEnd, text2.Length) - this.m_DrawStart);
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2;
				this.MarkGeometryAsDirty();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C820 File Offset: 0x0000AA20
		private bool IsSelectionVisible()
		{
			return this.m_DrawStart <= this.caretPositionInternal && this.m_DrawStart <= this.caretSelectPositionInternal && this.m_DrawEnd >= this.caretPositionInternal && this.m_DrawEnd >= this.caretSelectPositionInternal;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C878 File Offset: 0x0000AA78
		private static int GetLineStartPosition(TextGenerator gen, int line)
		{
			line = Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		private static int GetLineEndPosition(TextGenerator gen, int line)
		{
			line = Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx;
			}
			return gen.characterCountVisible;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		private void SetDrawRangeToContainCaretPosition(TextGenerator gen, int caretPos, ref int drawStart, ref int drawEnd)
		{
			Vector2 size = gen.rectExtents.size;
			if (this.multiLine)
			{
				IList<UILineInfo> lines = gen.lines;
				int num = this.DetermineCharacterLine(caretPos, gen);
				int num2 = (int)size.y;
				if (drawEnd <= caretPos)
				{
					drawEnd = InputField.GetLineEndPosition(gen, num);
					int num3 = num;
					while (num3 >= 0 && num3 < lines.Count)
					{
						num2 -= lines[num3].height;
						if (num2 < 0)
						{
							break;
						}
						drawStart = InputField.GetLineStartPosition(gen, num3);
						num3--;
					}
				}
				else
				{
					if (drawStart > caretPos)
					{
						drawStart = InputField.GetLineStartPosition(gen, num);
					}
					int num4 = this.DetermineCharacterLine(drawStart, gen);
					int num5 = num4;
					drawEnd = InputField.GetLineEndPosition(gen, num5);
					num2 -= lines[num5].height;
					for (;;)
					{
						if (num5 < lines.Count - 1)
						{
							num5++;
							if (num2 < lines[num5].height)
							{
								break;
							}
							drawEnd = InputField.GetLineEndPosition(gen, num5);
							num2 -= lines[num5].height;
						}
						else
						{
							if (num4 <= 0)
							{
								break;
							}
							num4--;
							if (num2 < lines[num4].height)
							{
								break;
							}
							drawStart = InputField.GetLineStartPosition(gen, num4);
							num2 -= lines[num4].height;
						}
					}
				}
			}
			else
			{
				float num6 = size.x;
				IList<UICharInfo> characters = gen.characters;
				if (drawEnd <= caretPos)
				{
					drawEnd = Mathf.Min(caretPos, gen.characterCountVisible);
					drawStart = 0;
					for (int i = drawEnd; i > 0; i--)
					{
						num6 -= characters[i - 1].charWidth;
						if (num6 < 0f)
						{
							drawStart = i;
							break;
						}
					}
				}
				else
				{
					if (drawStart > caretPos)
					{
						drawStart = caretPos;
					}
					drawEnd = gen.characterCountVisible;
					for (int j = drawStart; j < gen.characterCountVisible; j++)
					{
						num6 -= characters[j].charWidth;
						if (num6 < 0f)
						{
							drawEnd = j;
							break;
						}
					}
				}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CB64 File Offset: 0x0000AD64
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000CB94 File Offset: 0x0000AD94
		private void UpdateGeometry()
		{
			if (!this.shouldHideMobileInput)
			{
				return;
			}
			if (this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject(base.transform.name + " Input Caret");
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.AddComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.AddComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, null);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.m_Vbo);
			if (this.m_Vbo.Count == 0)
			{
				this.m_CachedInputRenderer.SetVertices(null, 0);
			}
			else
			{
				this.m_CachedInputRenderer.SetVertices(this.m_Vbo.ToArray(), this.m_Vbo.Count);
			}
			this.m_Vbo.Clear();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		private void AssignPositioningIfNeeded()
		{
			if (this.m_TextComponent != null && this.caretRectTrans != null && (this.caretRectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition || this.caretRectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation || this.caretRectTrans.localScale != this.m_TextComponent.rectTransform.localScale || this.caretRectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin || this.caretRectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax || this.caretRectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition || this.caretRectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta || this.caretRectTrans.pivot != this.m_TextComponent.rectTransform.pivot))
			{
				this.caretRectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
				this.caretRectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
				this.caretRectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
				this.caretRectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
				this.caretRectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
				this.caretRectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
				this.caretRectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
				this.caretRectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000CF00 File Offset: 0x0000B100
		private void OnFillVBO(List<UIVertex> vbo)
		{
			if (!this.isFocused)
			{
				return;
			}
			Rect rect = this.m_TextComponent.rectTransform.rect;
			Vector2 size = rect.size;
			Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_TextComponent.alignment);
			Vector2 zero = Vector2.zero;
			zero.x = Mathf.Lerp(rect.xMin, rect.xMax, textAnchorPivot.x);
			zero.y = Mathf.Lerp(rect.yMin, rect.yMax, textAnchorPivot.y);
			Vector2 a = this.m_TextComponent.PixelAdjustPoint(zero);
			Vector2 roundingOffset = a - zero + Vector2.Scale(size, textAnchorPivot);
			roundingOffset.x -= Mathf.Floor(0.5f + roundingOffset.x);
			roundingOffset.y -= Mathf.Floor(0.5f + roundingOffset.y);
			if (!this.hasSelection)
			{
				this.GenerateCursor(vbo, roundingOffset);
			}
			else
			{
				this.GenerateHightlight(vbo, roundingOffset);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D014 File Offset: 0x0000B214
		private void GenerateCursor(List<UIVertex> vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			float num = 1f;
			float num2 = (float)this.m_TextComponent.fontSize;
			int num3 = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator == null)
			{
				return;
			}
			if (this.m_TextComponent.resizeTextForBestFit)
			{
				num2 = (float)cachedTextGenerator.fontSizeUsedForBestFit / this.m_TextComponent.pixelsPerUnit;
			}
			Vector2 zero = Vector2.zero;
			if (cachedTextGenerator.characterCountVisible + 1 > num3 || num3 == 0)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num3];
				zero.x = uicharInfo.cursorPos.x;
				zero.y = uicharInfo.cursorPos.y;
			}
			zero.x /= this.m_TextComponent.pixelsPerUnit;
			if (zero.x > this.m_TextComponent.rectTransform.rect.xMax)
			{
				zero.x = this.m_TextComponent.rectTransform.rect.xMax;
			}
			this.m_CursorVerts[0].position = new Vector3(zero.x, zero.y - num2, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x + num, zero.y - num2, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num, zero.y, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x, zero.y, 0f);
			if (roundingOffset != Vector2.zero)
			{
				for (int i = 0; i < this.m_CursorVerts.Length; i++)
				{
					UIVertex item = this.m_CursorVerts[i];
					item.position.x = item.position.x + roundingOffset.x;
					item.position.y = item.position.y + roundingOffset.y;
					vbo.Add(item);
				}
			}
			else
			{
				for (int j = 0; j < this.m_CursorVerts.Length; j++)
				{
					vbo.Add(this.m_CursorVerts[j]);
				}
			}
			zero.y = (float)Screen.height - zero.y;
			Input.compositionCursorPos = zero;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].color = this.m_TextComponent.color;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D348 File Offset: 0x0000B548
		private float SumLineHeights(int endLine, TextGenerator generator)
		{
			float num = 0f;
			for (int i = 0; i < endLine; i++)
			{
				num += (float)generator.lines[i].height;
			}
			return num;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D388 File Offset: 0x0000B588
		private void GenerateHightlight(List<UIVertex> vbo, Vector2 roundingOffset)
		{
			int num = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			int num2 = Mathf.Max(0, this.caretSelectPositionInternal - this.m_DrawStart);
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			num2--;
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			int num4 = this.DetermineCharacterLine(num, cachedTextGenerator);
			float num5 = (float)this.m_TextComponent.fontSize;
			if (this.m_TextComponent.resizeTextForBestFit)
			{
				num5 = (float)cachedTextGenerator.fontSizeUsedForBestFit / this.m_TextComponent.pixelsPerUnit;
			}
			if (this.cachedInputTextGenerator != null && this.cachedInputTextGenerator.lines.Count > 0)
			{
				num5 = (float)this.cachedInputTextGenerator.lines[0].height;
			}
			if (this.m_TextComponent.resizeTextForBestFit && this.cachedInputTextGenerator != null)
			{
				num5 = (float)this.cachedInputTextGenerator.fontSizeUsedForBestFit;
			}
			int lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, num4);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num6 = num;
			while (num6 <= num2 && num6 < cachedTextGenerator.characterCountVisible)
			{
				if (num6 + 1 == lineEndPosition || num6 == num2)
				{
					UICharInfo uicharInfo = cachedTextGenerator.characters[num];
					UICharInfo uicharInfo2 = cachedTextGenerator.characters[num6];
					Vector2 vector = new Vector2(uicharInfo.cursorPos.x / this.m_TextComponent.pixelsPerUnit, uicharInfo.cursorPos.y);
					Vector2 vector2 = new Vector2((uicharInfo2.cursorPos.x + uicharInfo2.charWidth) / this.m_TextComponent.pixelsPerUnit, vector.y - num5 / this.m_TextComponent.pixelsPerUnit);
					if (vector2.x > this.m_TextComponent.rectTransform.rect.xMax || vector2.x < this.m_TextComponent.rectTransform.rect.xMin)
					{
						vector2.x = this.m_TextComponent.rectTransform.rect.xMax;
					}
					simpleVert.position = new Vector3(vector.x, vector2.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					simpleVert.position = new Vector3(vector.x, vector.y, 0f) + roundingOffset;
					vbo.Add(simpleVert);
					num = num6 + 1;
					num4++;
					lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, num4);
				}
				num6++;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == InputField.CharacterValidation.Integer || this.characterValidation == InputField.CharacterValidation.Decimal)
			{
				if (pos != 0 || text.Length <= 0 || text[0] != '-')
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && pos == 0)
					{
						return ch;
					}
					if (ch == '.' && this.characterValidation == InputField.CharacterValidation.Decimal && !text.Contains("."))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Alphanumeric)
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
			else if (this.characterValidation == InputField.CharacterValidation.Name)
			{
				char c = (text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)];
				char c2 = (text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)];
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && c == ' ')
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && c != ' ' && c != '\'')
					{
						return char.ToLower(ch);
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
			else if (this.characterValidation == InputField.CharacterValidation.EmailAddress)
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
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					char c3 = (text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)];
					char c4 = (text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)];
					if (c3 != '.' && c4 != '.')
					{
						return ch;
					}
				}
			}
			return '\0';
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D998 File Offset: 0x0000BB98
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && InputField.m_Keyboard != null && !InputField.m_Keyboard.active)
			{
				InputField.m_Keyboard.active = true;
				InputField.m_Keyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			if (TouchScreenKeyboard.isSupported)
			{
				if (Input.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				InputField.m_Keyboard = ((this.inputType != InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == InputField.InputType.AutoCorrect, this.multiLine) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true));
			}
			else
			{
				Input.imeCompositionMode = IMECompositionMode.On;
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.ActivateInputField();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public void DeactivateInputField()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_HasDoneFocusTransition = false;
			this.m_AllowInput = false;
			if (this.m_TextComponent != null && this.IsInteractable())
			{
				if (this.m_WasCanceled)
				{
					this.text = this.m_OriginalText;
				}
				if (InputField.m_Keyboard != null)
				{
					InputField.m_Keyboard.active = false;
					InputField.m_Keyboard = null;
				}
				this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
				this.SendOnSubmit();
				Input.imeCompositionMode = IMECompositionMode.Auto;
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField();
			base.OnDeselect(eventData);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000DC00 File Offset: 0x0000BE00
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case InputField.ContentType.Standard:
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				return;
			case InputField.ContentType.Autocorrected:
				this.m_InputType = InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				return;
			case InputField.ContentType.IntegerNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				return;
			case InputField.ContentType.DecimalNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = InputField.CharacterValidation.Decimal;
				return;
			case InputField.ContentType.Alphanumeric:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = InputField.CharacterValidation.Alphanumeric;
				return;
			case InputField.ContentType.Name:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.Name;
				return;
			case InputField.ContentType.EmailAddress:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = InputField.CharacterValidation.EmailAddress;
				return;
			case InputField.ContentType.Password:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				return;
			case InputField.ContentType.Pin:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		private void SetToCustomIfContentTypeIsNot(params InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (this.contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000DD84 File Offset: 0x0000BF84
		private void SetToCustom()
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Highlighted;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400014C RID: 332
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x0400014D RID: 333
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x0400014E RID: 334
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x0400014F RID: 335
		protected static TouchScreenKeyboard m_Keyboard;

		// Token: 0x04000150 RID: 336
		private static readonly char[] kSeparators = new char[]
		{
			' ',
			'.',
			','
		};

		// Token: 0x04000151 RID: 337
		[SerializeField]
		[FormerlySerializedAs("text")]
		protected Text m_TextComponent;

		// Token: 0x04000152 RID: 338
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x04000153 RID: 339
		[SerializeField]
		private InputField.ContentType m_ContentType;

		// Token: 0x04000154 RID: 340
		[SerializeField]
		[FormerlySerializedAs("inputType")]
		private InputField.InputType m_InputType;

		// Token: 0x04000155 RID: 341
		[SerializeField]
		[FormerlySerializedAs("asteriskChar")]
		private char m_AsteriskChar = '*';

		// Token: 0x04000156 RID: 342
		[SerializeField]
		[FormerlySerializedAs("keyboardType")]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x04000157 RID: 343
		[SerializeField]
		private InputField.LineType m_LineType;

		// Token: 0x04000158 RID: 344
		[FormerlySerializedAs("hideMobileInput")]
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x04000159 RID: 345
		[SerializeField]
		[FormerlySerializedAs("validation")]
		private InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x0400015A RID: 346
		[FormerlySerializedAs("characterLimit")]
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x0400015B RID: 347
		[FormerlySerializedAs("onSubmit")]
		[SerializeField]
		[FormerlySerializedAs("m_OnSubmit")]
		private InputField.SubmitEvent m_EndEdit = new InputField.SubmitEvent();

		// Token: 0x0400015C RID: 348
		[SerializeField]
		[FormerlySerializedAs("onValueChange")]
		private InputField.OnChangeEvent m_OnValueChange = new InputField.OnChangeEvent();

		// Token: 0x0400015D RID: 349
		[FormerlySerializedAs("onValidateInput")]
		[SerializeField]
		private InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		[FormerlySerializedAs("selectionColor")]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x0400015F RID: 351
		[SerializeField]
		[FormerlySerializedAs("mValue")]
		protected string m_Text = string.Empty;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		[Range(0f, 8f)]
		private float m_CaretBlinkRate = 1.7f;

		// Token: 0x04000161 RID: 353
		protected int m_CaretPosition;

		// Token: 0x04000162 RID: 354
		protected int m_CaretSelectPosition;

		// Token: 0x04000163 RID: 355
		private RectTransform caretRectTrans;

		// Token: 0x04000164 RID: 356
		protected UIVertex[] m_CursorVerts;

		// Token: 0x04000165 RID: 357
		private TextGenerator m_InputTextCache;

		// Token: 0x04000166 RID: 358
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x04000167 RID: 359
		private bool m_PreventFontCallback;

		// Token: 0x04000168 RID: 360
		private readonly List<UIVertex> m_Vbo = new List<UIVertex>();

		// Token: 0x04000169 RID: 361
		private bool m_AllowInput;

		// Token: 0x0400016A RID: 362
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x0400016B RID: 363
		private bool m_UpdateDrag;

		// Token: 0x0400016C RID: 364
		private bool m_DragPositionOutOfBounds;

		// Token: 0x0400016D RID: 365
		protected bool m_CaretVisible;

		// Token: 0x0400016E RID: 366
		private Coroutine m_BlinkCoroutine;

		// Token: 0x0400016F RID: 367
		private float m_BlinkStartTime;

		// Token: 0x04000170 RID: 368
		protected int m_DrawStart;

		// Token: 0x04000171 RID: 369
		protected int m_DrawEnd;

		// Token: 0x04000172 RID: 370
		private Coroutine m_DragCoroutine;

		// Token: 0x04000173 RID: 371
		private string m_OriginalText = string.Empty;

		// Token: 0x04000174 RID: 372
		private bool m_WasCanceled;

		// Token: 0x04000175 RID: 373
		private bool m_HasDoneFocusTransition;

		// Token: 0x04000176 RID: 374
		private Event m_ProcessingEvent = new Event();

		// Token: 0x02000064 RID: 100
		public enum CharacterValidation
		{
			// Token: 0x04000185 RID: 389
			None,
			// Token: 0x04000186 RID: 390
			Integer,
			// Token: 0x04000187 RID: 391
			Decimal,
			// Token: 0x04000188 RID: 392
			Alphanumeric,
			// Token: 0x04000189 RID: 393
			Name,
			// Token: 0x0400018A RID: 394
			EmailAddress
		}

		// Token: 0x02000065 RID: 101
		public enum ContentType
		{
			// Token: 0x0400018C RID: 396
			Standard,
			// Token: 0x0400018D RID: 397
			Autocorrected,
			// Token: 0x0400018E RID: 398
			IntegerNumber,
			// Token: 0x0400018F RID: 399
			DecimalNumber,
			// Token: 0x04000190 RID: 400
			Alphanumeric,
			// Token: 0x04000191 RID: 401
			Name,
			// Token: 0x04000192 RID: 402
			EmailAddress,
			// Token: 0x04000193 RID: 403
			Password,
			// Token: 0x04000194 RID: 404
			Pin,
			// Token: 0x04000195 RID: 405
			Custom
		}

		// Token: 0x02000066 RID: 102
		protected enum EditState
		{
			// Token: 0x04000197 RID: 407
			Continue,
			// Token: 0x04000198 RID: 408
			Finish
		}

		// Token: 0x02000067 RID: 103
		public enum InputType
		{
			// Token: 0x0400019A RID: 410
			Standard,
			// Token: 0x0400019B RID: 411
			AutoCorrect,
			// Token: 0x0400019C RID: 412
			Password
		}

		// Token: 0x02000068 RID: 104
		public enum LineType
		{
			// Token: 0x0400019E RID: 414
			SingleLine,
			// Token: 0x0400019F RID: 415
			MultiLineSubmit,
			// Token: 0x040001A0 RID: 416
			MultiLineNewline
		}

		// Token: 0x02000069 RID: 105
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x06000316 RID: 790
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x0200006B RID: 107
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}
	}
}
