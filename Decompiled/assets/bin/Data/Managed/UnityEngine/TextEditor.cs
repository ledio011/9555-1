using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000116 RID: 278
	public class TextEditor
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x000166A4 File Offset: 0x000148A4
		private void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.m_iAltCursorPos = -1;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000166B4 File Offset: 0x000148B4
		public void OnFocus()
		{
			if (this.multiline)
			{
				this.pos = (this.selectPos = 0);
			}
			else
			{
				this.SelectAll();
			}
			this.m_HasFocus = true;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000166F0 File Offset: 0x000148F0
		public void OnLostFocus()
		{
			this.m_HasFocus = false;
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00016704 File Offset: 0x00014904
		private void GrabGraphicalCursorPos()
		{
			if (!this.hasHorizontalCursorPos)
			{
				this.graphicalCursorPos = this.style.GetCursorPixelPosition(this.position, this.content, this.pos);
				this.graphicalSelectCursorPos = this.style.GetCursorPixelPosition(this.position, this.content, this.selectPos);
				this.hasHorizontalCursorPos = false;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001676C File Offset: 0x0001496C
		public bool HandleKeyEvent(Event e)
		{
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			if (TextEditor.s_Keyactions.ContainsKey(e))
			{
				TextEditor.TextEditOp operation = TextEditor.s_Keyactions[e];
				this.PerformOperation(operation);
				e.modifiers = modifiers;
				this.UpdateScrollOffset();
				return true;
			}
			e.modifiers = modifiers;
			return false;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000167D0 File Offset: 0x000149D0
		public bool DeleteLineBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.pos;
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.content.text[num2] == '\n')
				{
					num = num2 + 1;
					break;
				}
			}
			if (num2 == -1)
			{
				num = 0;
			}
			if (this.pos != num)
			{
				this.content.text = this.content.text.Remove(num, this.pos - num);
				this.selectPos = (this.pos = num);
				return true;
			}
			return false;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00016878 File Offset: 0x00014A78
		public bool DeleteWordBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindEndOfPreviousWord(this.pos);
			if (this.pos != num)
			{
				this.content.text = this.content.text.Remove(num, this.pos - num);
				this.selectPos = (this.pos = num);
				return true;
			}
			return false;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000168EC File Offset: 0x00014AEC
		public bool DeleteWordForward()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindStartOfNextWord(this.pos);
			if (this.pos < this.content.text.Length)
			{
				this.content.text = this.content.text.Remove(this.pos, num - this.pos);
				return true;
			}
			return false;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00016964 File Offset: 0x00014B64
		public bool Delete()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.pos < this.content.text.Length)
			{
				this.content.text = this.content.text.Remove(this.pos, 1);
				return true;
			}
			return false;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000169C8 File Offset: 0x00014BC8
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000169DC File Offset: 0x00014BDC
		public bool Backspace()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.pos > 0)
			{
				this.content.text = this.content.text.Remove(this.pos - 1, 1);
				this.selectPos = --this.pos;
				this.ClearCursorPos();
				return true;
			}
			return false;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00016A50 File Offset: 0x00014C50
		public void SelectAll()
		{
			this.pos = 0;
			this.selectPos = this.content.text.Length;
			this.ClearCursorPos();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00016A78 File Offset: 0x00014C78
		public void SelectNone()
		{
			this.selectPos = this.pos;
			this.ClearCursorPos();
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00016A8C File Offset: 0x00014C8C
		public bool hasSelection
		{
			get
			{
				return this.pos != this.selectPos;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public string SelectedText
		{
			get
			{
				int length = this.content.text.Length;
				if (this.pos > length)
				{
					this.pos = length;
				}
				if (this.selectPos > length)
				{
					this.selectPos = length;
				}
				if (this.pos == this.selectPos)
				{
					return string.Empty;
				}
				if (this.pos < this.selectPos)
				{
					return this.content.text.Substring(this.pos, this.selectPos - this.pos);
				}
				return this.content.text.Substring(this.selectPos, this.pos - this.selectPos);
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00016B54 File Offset: 0x00014D54
		public bool DeleteSelection()
		{
			int length = this.content.text.Length;
			if (this.pos > length)
			{
				this.pos = length;
			}
			if (this.selectPos > length)
			{
				this.selectPos = length;
			}
			if (this.pos == this.selectPos)
			{
				return false;
			}
			if (this.pos < this.selectPos)
			{
				this.content.text = this.content.text.Substring(0, this.pos) + this.content.text.Substring(this.selectPos, this.content.text.Length - this.selectPos);
				this.selectPos = this.pos;
			}
			else
			{
				this.content.text = this.content.text.Substring(0, this.selectPos) + this.content.text.Substring(this.pos, this.content.text.Length - this.pos);
				this.pos = this.selectPos;
			}
			this.ClearCursorPos();
			return true;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00016C88 File Offset: 0x00014E88
		public void ReplaceSelection(string replace)
		{
			this.DeleteSelection();
			this.content.text = this.content.text.Insert(this.pos, replace);
			this.selectPos = (this.pos += replace.Length);
			this.ClearCursorPos();
			this.UpdateScrollOffset();
			this.m_TextHeightPotentiallyChanged = true;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public void Insert(char c)
		{
			this.ReplaceSelection(c.ToString());
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00016D00 File Offset: 0x00014F00
		public void MoveSelectionToAltCursor()
		{
			if (this.m_iAltCursorPos == -1)
			{
				return;
			}
			int iAltCursorPos = this.m_iAltCursorPos;
			string selectedText = this.SelectedText;
			this.content.text = this.content.text.Insert(iAltCursorPos, selectedText);
			if (iAltCursorPos < this.pos)
			{
				this.pos += selectedText.Length;
				this.selectPos += selectedText.Length;
			}
			this.DeleteSelection();
			this.selectPos = (this.pos = iAltCursorPos);
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00016D9C File Offset: 0x00014F9C
		public void MoveRight()
		{
			this.ClearCursorPos();
			if (this.selectPos == this.pos)
			{
				this.pos++;
				this.ClampPos();
				this.selectPos = this.pos;
			}
			else if (this.selectPos > this.pos)
			{
				this.pos = this.selectPos;
			}
			else
			{
				this.selectPos = this.pos;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00016E1C File Offset: 0x0001501C
		public void MoveLeft()
		{
			if (this.selectPos == this.pos)
			{
				this.pos--;
				if (this.pos < 0)
				{
					this.pos = 0;
				}
				this.selectPos = this.pos;
			}
			else if (this.selectPos > this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00016EA8 File Offset: 0x000150A8
		public void MoveUp()
		{
			if (this.selectPos < this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.pos = (this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos));
			if (this.pos <= 0)
			{
				this.ClearCursorPos();
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00016F44 File Offset: 0x00015144
		public void MoveDown()
		{
			if (this.selectPos > this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.pos = (this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos));
			if (this.pos == this.content.text.Length)
			{
				this.ClearCursorPos();
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00016FFC File Offset: 0x000151FC
		public void MoveLineStart()
		{
			int num = (this.selectPos >= this.pos) ? this.pos : this.selectPos;
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.content.text[num2] == '\n')
				{
					this.selectPos = (this.pos = num2 + 1);
					return;
				}
			}
			this.selectPos = (this.pos = 0);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00017080 File Offset: 0x00015280
		public void MoveLineEnd()
		{
			int num = (this.selectPos <= this.pos) ? this.pos : this.selectPos;
			int i = num;
			int length = this.content.text.Length;
			while (i < length)
			{
				if (this.content.text[i] == '\n')
				{
					this.selectPos = (this.pos = i);
					return;
				}
				i++;
			}
			this.selectPos = (this.pos = length);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00017114 File Offset: 0x00015314
		public void MoveGraphicalLineStart()
		{
			this.pos = (this.selectPos = this.GetGraphicalLineStart((this.pos >= this.selectPos) ? this.selectPos : this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00017160 File Offset: 0x00015360
		public void MoveGraphicalLineEnd()
		{
			this.pos = (this.selectPos = this.GetGraphicalLineEnd((this.pos <= this.selectPos) ? this.selectPos : this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000171AC File Offset: 0x000153AC
		public void MoveTextStart()
		{
			this.selectPos = (this.pos = 0);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000171D0 File Offset: 0x000153D0
		public void MoveTextEnd()
		{
			this.selectPos = (this.pos = this.content.text.Length);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00017204 File Offset: 0x00015404
		public void MoveParagraphForward()
		{
			this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
			if (this.pos < this.content.text.Length)
			{
				this.selectPos = (this.pos = this.content.text.IndexOf('\n', this.pos + 1));
				if (this.pos == -1)
				{
					this.selectPos = (this.pos = this.content.text.Length);
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000172B0 File Offset: 0x000154B0
		public void MoveParagraphBackward()
		{
			this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
			if (this.pos > 1)
			{
				this.selectPos = (this.pos = this.content.text.LastIndexOf('\n', this.pos - 2) + 1);
			}
			else
			{
				this.selectPos = (this.pos = 0);
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00017338 File Offset: 0x00015538
		public void MoveCursorToPosition(Vector2 cursorPosition)
		{
			this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			if (!Event.current.shift)
			{
				this.pos = this.selectPos;
			}
			this.ClampPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00017398 File Offset: 0x00015598
		public void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			this.m_iAltCursorPos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			this.ClampPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000173D0 File Offset: 0x000155D0
		public bool IsOverSelection(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			return cursorStringIndex < Mathf.Max(this.pos, this.selectPos) && cursorStringIndex > Mathf.Min(this.pos, this.selectPos);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00017430 File Offset: 0x00015630
		public void SelectToPosition(Vector2 cursorPosition)
		{
			if (!this.m_MouseDragSelectsWholeWords)
			{
				this.pos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			}
			else
			{
				int num = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
				if (this.m_DblClickSnap == TextEditor.DblClickSnapping.WORDS)
				{
					if (num < this.m_DblClickInitPos)
					{
						this.pos = this.FindEndOfClassification(num, -1);
						this.selectPos = this.FindEndOfClassification(this.m_DblClickInitPos, 1);
					}
					else
					{
						if (num >= this.content.text.Length)
						{
							num = this.content.text.Length - 1;
						}
						this.pos = this.FindEndOfClassification(num, 1);
						this.selectPos = this.FindEndOfClassification(this.m_DblClickInitPos - 1, -1);
					}
				}
				else if (num < this.m_DblClickInitPos)
				{
					if (num > 0)
					{
						this.pos = this.content.text.LastIndexOf('\n', num - 2) + 1;
					}
					else
					{
						this.pos = 0;
					}
					this.selectPos = this.content.text.LastIndexOf('\n', this.m_DblClickInitPos);
				}
				else
				{
					if (num < this.content.text.Length)
					{
						this.pos = this.content.text.IndexOf('\n', num + 1) + 1;
						if (this.pos <= 0)
						{
							this.pos = this.content.text.Length;
						}
					}
					else
					{
						this.pos = this.content.text.Length;
					}
					this.selectPos = this.content.text.LastIndexOf('\n', this.m_DblClickInitPos - 2) + 1;
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00017620 File Offset: 0x00015820
		public void SelectLeft()
		{
			if (this.m_bJustSelected && this.pos > this.selectPos)
			{
				int num = this.pos;
				this.pos = this.selectPos;
				this.selectPos = num;
			}
			this.m_bJustSelected = false;
			this.pos--;
			if (this.pos < 0)
			{
				this.pos = 0;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00017694 File Offset: 0x00015894
		public void SelectRight()
		{
			if (this.m_bJustSelected && this.pos < this.selectPos)
			{
				int num = this.pos;
				this.pos = this.selectPos;
				this.selectPos = num;
			}
			this.m_bJustSelected = false;
			this.pos++;
			int length = this.content.text.Length;
			if (this.pos > length)
			{
				this.pos = length;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00017718 File Offset: 0x00015918
		public void SelectUp()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.pos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001776C File Offset: 0x0001596C
		public void SelectDown()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.pos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000177CC File Offset: 0x000159CC
		public void SelectTextEnd()
		{
			this.pos = this.content.text.Length;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000177EC File Offset: 0x000159EC
		public void SelectTextStart()
		{
			this.pos = 0;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000177FC File Offset: 0x000159FC
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPos = this.pos;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00017814 File Offset: 0x00015A14
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.m_DblClickSnap = snapping;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00017820 File Offset: 0x00015A20
		private int GetGraphicalLineStart(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.content, p);
			cursorPixelPosition.x = 0f;
			return this.style.GetCursorStringIndex(this.position, this.content, cursorPixelPosition);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001786C File Offset: 0x00015A6C
		private int GetGraphicalLineEnd(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.content, p);
			cursorPixelPosition.x += 5000f;
			return this.style.GetCursorStringIndex(this.position, this.content, cursorPixelPosition);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000178C0 File Offset: 0x00015AC0
		private int FindNextSeperator(int startPos)
		{
			int length = this.content.text.Length;
			while (startPos < length && !TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos++;
			}
			while (startPos < length && TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos++;
			}
			return startPos;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00017938 File Offset: 0x00015B38
		private static bool isLetterLikeChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '\'';
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00017950 File Offset: 0x00015B50
		private int FindPrevSeperator(int startPos)
		{
			startPos--;
			while (startPos > 0 && !TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos--;
			}
			while (startPos >= 0 && TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos--;
			}
			return startPos + 1;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000179C0 File Offset: 0x00015BC0
		public void MoveWordRight()
		{
			this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
			this.pos = (this.selectPos = this.FindNextSeperator(this.pos));
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00017A1C File Offset: 0x00015C1C
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			if (this.pos != this.selectPos)
			{
				this.MoveRight();
				return;
			}
			this.pos = (this.selectPos = this.FindStartOfNextWord(this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00017A68 File Offset: 0x00015C68
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			if (this.pos != this.selectPos)
			{
				this.MoveLeft();
				return;
			}
			this.pos = (this.selectPos = this.FindEndOfPreviousWord(this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00017AB4 File Offset: 0x00015CB4
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.pos = this.FindStartOfNextWord(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00017AD4 File Offset: 0x00015CD4
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.pos = this.FindEndOfPreviousWord(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00017AF4 File Offset: 0x00015CF4
		private TextEditor.CharacterType ClassifyChar(char c)
		{
			if (char.IsWhiteSpace(c))
			{
				return TextEditor.CharacterType.WhiteSpace;
			}
			if (char.IsLetterOrDigit(c) || c == '\'')
			{
				return TextEditor.CharacterType.LetterLike;
			}
			return TextEditor.CharacterType.Symbol;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00017B1C File Offset: 0x00015D1C
		public int FindStartOfNextWord(int p)
		{
			int length = this.content.text.Length;
			if (p == length)
			{
				return p;
			}
			char c = this.content.text[p];
			TextEditor.CharacterType characterType = this.ClassifyChar(c);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				p++;
				while (p < length && this.ClassifyChar(this.content.text[p]) == characterType)
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p + 1;
			}
			if (p == length)
			{
				return p;
			}
			c = this.content.text[p];
			if (c == ' ')
			{
				while (p < length && char.IsWhiteSpace(this.content.text[p]))
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p;
			}
			return p;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00017C18 File Offset: 0x00015E18
		private int FindEndOfPreviousWord(int p)
		{
			if (p == 0)
			{
				return p;
			}
			p--;
			while (p > 0 && this.content.text[p] == ' ')
			{
				p--;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.content.text[p]);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				while (p > 0 && this.ClassifyChar(this.content.text[p - 1]) == characterType)
				{
					p--;
				}
			}
			return p;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00017CAC File Offset: 0x00015EAC
		public void MoveWordLeft()
		{
			this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
			this.pos = this.FindPrevSeperator(this.pos);
			this.selectPos = this.pos;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00017D08 File Offset: 0x00015F08
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int num = this.selectPos;
			if (this.pos < this.selectPos)
			{
				this.selectPos = this.pos;
				this.MoveWordRight();
				this.selectPos = num;
				this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
				return;
			}
			this.selectPos = this.pos;
			this.MoveWordRight();
			this.selectPos = num;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00017D94 File Offset: 0x00015F94
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int num = this.selectPos;
			if (this.pos > this.selectPos)
			{
				this.selectPos = this.pos;
				this.MoveWordLeft();
				this.selectPos = num;
				this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
				return;
			}
			this.selectPos = this.pos;
			this.MoveWordLeft();
			this.selectPos = num;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00017E20 File Offset: 0x00016020
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			if (this.pos < this.selectPos)
			{
				this.pos = this.GetGraphicalLineStart(this.pos);
			}
			else
			{
				int num = this.pos;
				this.pos = this.GetGraphicalLineStart(this.selectPos);
				this.selectPos = num;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00017E84 File Offset: 0x00016084
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			if (this.pos > this.selectPos)
			{
				this.pos = this.GetGraphicalLineEnd(this.pos);
			}
			else
			{
				int num = this.pos;
				this.pos = this.GetGraphicalLineEnd(this.selectPos);
				this.selectPos = num;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00017EE8 File Offset: 0x000160E8
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.pos = this.GetGraphicalLineStart(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00017F08 File Offset: 0x00016108
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.pos = this.GetGraphicalLineEnd(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00017F28 File Offset: 0x00016128
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool flag = this.pos < this.selectPos;
			if (this.pos < this.content.text.Length)
			{
				this.pos = this.content.text.IndexOf('\n', this.pos + 1);
				if (this.pos == -1)
				{
					this.pos = this.content.text.Length;
				}
				if (flag && this.pos > this.selectPos)
				{
					this.pos = this.selectPos;
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00017FD0 File Offset: 0x000161D0
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool flag = this.pos > this.selectPos;
			if (this.pos > 1)
			{
				this.pos = this.content.text.LastIndexOf('\n', this.pos - 2) + 1;
				if (flag && this.pos < this.selectPos)
				{
					this.pos = this.selectPos;
				}
			}
			else
			{
				this.selectPos = (this.pos = 0);
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00018060 File Offset: 0x00016260
		public void SelectCurrentWord()
		{
			this.ClearCursorPos();
			int length = this.content.text.Length;
			this.selectPos = this.pos;
			if (length == 0)
			{
				return;
			}
			if (this.pos >= length)
			{
				this.pos = length - 1;
			}
			if (this.selectPos >= length)
			{
				this.selectPos--;
			}
			if (this.pos < this.selectPos)
			{
				this.pos = this.FindEndOfClassification(this.pos, -1);
				this.selectPos = this.FindEndOfClassification(this.selectPos, 1);
			}
			else
			{
				this.pos = this.FindEndOfClassification(this.pos, 1);
				this.selectPos = this.FindEndOfClassification(this.selectPos, -1);
			}
			this.m_bJustSelected = true;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00018138 File Offset: 0x00016338
		private int FindEndOfClassification(int p, int dir)
		{
			int length = this.content.text.Length;
			if (p >= length || p < 0)
			{
				return p;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.content.text[p]);
			for (;;)
			{
				p += dir;
				if (p < 0)
				{
					break;
				}
				if (p >= length)
				{
					return length;
				}
				if (this.ClassifyChar(this.content.text[p]) != characterType)
				{
					goto Block_4;
				}
			}
			return 0;
			Block_4:
			if (dir == 1)
			{
				return p;
			}
			return p + 1;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000181C0 File Offset: 0x000163C0
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int length = this.content.text.Length;
			if (this.pos < length)
			{
				this.pos = this.content.text.IndexOf('\n', this.pos);
				if (this.pos == -1)
				{
					this.pos = this.content.text.Length;
				}
				else
				{
					this.pos++;
				}
			}
			if (this.selectPos != 0)
			{
				this.selectPos = this.content.text.LastIndexOf('\n', this.selectPos - 1) + 1;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00018278 File Offset: 0x00016478
		public void UpdateScrollOffsetIfNeeded()
		{
			if (this.m_TextHeightPotentiallyChanged)
			{
				this.UpdateScrollOffset();
				this.m_TextHeightPotentiallyChanged = false;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00018294 File Offset: 0x00016494
		private void UpdateScrollOffset()
		{
			int cursorStringIndex = this.pos;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.content, cursorStringIndex);
			Rect rect = this.style.padding.Remove(this.position);
			Vector2 vector = new Vector2(this.style.CalcSize(this.content).x, this.style.CalcHeight(this.content, this.position.width));
			if (vector.x < this.position.width)
			{
				this.scrollOffset.x = 0f;
			}
			else
			{
				if (this.graphicalCursorPos.x + 1f > this.scrollOffset.x + rect.width)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - rect.width;
				}
				if (this.graphicalCursorPos.x < this.scrollOffset.x + (float)this.style.padding.left)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - (float)this.style.padding.left;
				}
			}
			if (vector.y < rect.height)
			{
				this.scrollOffset.y = 0f;
			}
			else
			{
				if (this.graphicalCursorPos.y + this.style.lineHeight > this.scrollOffset.y + rect.height + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - rect.height - (float)this.style.padding.top + this.style.lineHeight;
				}
				if (this.graphicalCursorPos.y < this.scrollOffset.y + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - (float)this.style.padding.top;
				}
			}
			if (this.scrollOffset.y > 0f && vector.y - this.scrollOffset.y < rect.height)
			{
				this.scrollOffset.y = vector.y - rect.height - (float)this.style.padding.top - (float)this.style.padding.bottom;
			}
			this.scrollOffset.y = ((this.scrollOffset.y >= 0f) ? this.scrollOffset.y : 0f);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000185A4 File Offset: 0x000167A4
		public void DrawCursor(string text)
		{
			string text2 = this.content.text;
			int num = this.pos;
			if (Input.compositionString.Length > 0)
			{
				this.content.text = text.Substring(0, this.pos) + Input.compositionString + text.Substring(this.selectPos);
				num += Input.compositionString.Length;
			}
			else
			{
				this.content.text = text;
			}
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.content, num);
			this.UpdateScrollOffset();
			Vector2 contentOffset = this.style.contentOffset;
			this.style.contentOffset -= this.scrollOffset;
			this.style.Internal_clipOffset = this.scrollOffset;
			Input.compositionCursorPos = this.graphicalCursorPos + new Vector2(this.position.x, this.position.y + this.style.lineHeight) - this.scrollOffset;
			if (Input.compositionString.Length > 0)
			{
				this.style.DrawWithTextSelection(this.position, this.content, this.controlID, this.pos, this.pos + Input.compositionString.Length, true);
			}
			else
			{
				this.style.DrawWithTextSelection(this.position, this.content, this.controlID, this.pos, this.selectPos);
			}
			if (this.m_iAltCursorPos != -1)
			{
				this.style.DrawCursor(this.position, this.content, this.controlID, this.m_iAltCursorPos);
			}
			this.style.contentOffset = contentOffset;
			this.style.Internal_clipOffset = Vector2.zero;
			this.content.text = text2;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000187A8 File Offset: 0x000169A8
		private bool PerformOperation(TextEditor.TextEditOp operation)
		{
			switch (operation)
			{
			case TextEditor.TextEditOp.MoveLeft:
				this.MoveLeft();
				return false;
			case TextEditor.TextEditOp.MoveRight:
				this.MoveRight();
				return false;
			case TextEditor.TextEditOp.MoveUp:
				this.MoveUp();
				return false;
			case TextEditor.TextEditOp.MoveDown:
				this.MoveDown();
				return false;
			case TextEditor.TextEditOp.MoveLineStart:
				this.MoveLineStart();
				return false;
			case TextEditor.TextEditOp.MoveLineEnd:
				this.MoveLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveTextStart:
				this.MoveTextStart();
				return false;
			case TextEditor.TextEditOp.MoveTextEnd:
				this.MoveTextEnd();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineStart:
				this.MoveGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineEnd:
				this.MoveGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveWordLeft:
				this.MoveWordLeft();
				return false;
			case TextEditor.TextEditOp.MoveWordRight:
				this.MoveWordRight();
				return false;
			case TextEditor.TextEditOp.MoveParagraphForward:
				this.MoveParagraphForward();
				return false;
			case TextEditor.TextEditOp.MoveParagraphBackward:
				this.MoveParagraphBackward();
				return false;
			case TextEditor.TextEditOp.MoveToStartOfNextWord:
				this.MoveToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.MoveToEndOfPreviousWord:
				this.MoveToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectLeft:
				this.SelectLeft();
				return false;
			case TextEditor.TextEditOp.SelectRight:
				this.SelectRight();
				return false;
			case TextEditor.TextEditOp.SelectUp:
				this.SelectUp();
				return false;
			case TextEditor.TextEditOp.SelectDown:
				this.SelectDown();
				return false;
			case TextEditor.TextEditOp.SelectTextStart:
				this.SelectTextStart();
				return false;
			case TextEditor.TextEditOp.SelectTextEnd:
				this.SelectTextEnd();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectWordLeft:
				this.SelectWordLeft();
				return false;
			case TextEditor.TextEditOp.SelectWordRight:
				this.SelectWordRight();
				return false;
			case TextEditor.TextEditOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				return false;
			case TextEditor.TextEditOp.SelectParagraphForward:
				this.SelectParagraphForward();
				return false;
			case TextEditor.TextEditOp.Delete:
				return this.Delete();
			case TextEditor.TextEditOp.Backspace:
				return this.Backspace();
			case TextEditor.TextEditOp.DeleteWordBack:
				return this.DeleteWordBack();
			case TextEditor.TextEditOp.DeleteWordForward:
				return this.DeleteWordForward();
			case TextEditor.TextEditOp.DeleteLineBack:
				return this.DeleteLineBack();
			case TextEditor.TextEditOp.Cut:
				return this.Cut();
			case TextEditor.TextEditOp.Copy:
				this.Copy();
				return false;
			case TextEditor.TextEditOp.Paste:
				return this.Paste();
			case TextEditor.TextEditOp.SelectAll:
				this.SelectAll();
				return false;
			case TextEditor.TextEditOp.SelectNone:
				this.SelectNone();
				return false;
			}
			Debug.Log("Unimplemented: " + operation);
			return false;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00018A48 File Offset: 0x00016C48
		public void SaveBackup()
		{
			this.oldText = this.content.text;
			this.oldPos = this.pos;
			this.oldSelectPos = this.selectPos;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00018A74 File Offset: 0x00016C74
		public void Undo()
		{
			this.content.text = this.oldText;
			this.pos = this.oldPos;
			this.selectPos = this.oldSelectPos;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00018AA8 File Offset: 0x00016CA8
		public bool Cut()
		{
			if (this.isPasswordField)
			{
				return false;
			}
			this.Copy();
			return this.DeleteSelection();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00018AC4 File Offset: 0x00016CC4
		public void Copy()
		{
			if (this.selectPos == this.pos)
			{
				return;
			}
			if (this.isPasswordField)
			{
				return;
			}
			string systemCopyBuffer;
			if (this.pos < this.selectPos)
			{
				systemCopyBuffer = this.content.text.Substring(this.pos, this.selectPos - this.pos);
			}
			else
			{
				systemCopyBuffer = this.content.text.Substring(this.selectPos, this.pos - this.selectPos);
			}
			GUIUtility.systemCopyBuffer = systemCopyBuffer;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00018B54 File Offset: 0x00016D54
		public bool Paste()
		{
			string systemCopyBuffer = GUIUtility.systemCopyBuffer;
			if (systemCopyBuffer != string.Empty)
			{
				this.ReplaceSelection(systemCopyBuffer);
				return true;
			}
			return false;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00018B84 File Offset: 0x00016D84
		private static void MapKey(string key, TextEditor.TextEditOp action)
		{
			TextEditor.s_Keyactions[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00018B98 File Offset: 0x00016D98
		private void InitKeyActions()
		{
			if (TextEditor.s_Keyactions != null)
			{
				return;
			}
			TextEditor.s_Keyactions = new Dictionary<Event, TextEditor.TextEditOp>();
			TextEditor.MapKey("left", TextEditor.TextEditOp.MoveLeft);
			TextEditor.MapKey("right", TextEditor.TextEditOp.MoveRight);
			TextEditor.MapKey("up", TextEditor.TextEditOp.MoveUp);
			TextEditor.MapKey("down", TextEditor.TextEditOp.MoveDown);
			TextEditor.MapKey("#left", TextEditor.TextEditOp.SelectLeft);
			TextEditor.MapKey("#right", TextEditor.TextEditOp.SelectRight);
			TextEditor.MapKey("#up", TextEditor.TextEditOp.SelectUp);
			TextEditor.MapKey("#down", TextEditor.TextEditOp.SelectDown);
			TextEditor.MapKey("delete", TextEditor.TextEditOp.Delete);
			TextEditor.MapKey("backspace", TextEditor.TextEditOp.Backspace);
			TextEditor.MapKey("#backspace", TextEditor.TextEditOp.Backspace);
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.OSXDashboardPlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("&left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("&right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("&up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("&down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveTextStart);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveTextEnd);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#&left", TextEditor.TextEditOp.SelectWordLeft);
				TextEditor.MapKey("#&right", TextEditor.TextEditOp.SelectWordRight);
				TextEditor.MapKey("#&up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#&down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#%left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#%right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#%up", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#%down", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("%a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("%x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("%c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("%v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("^d", TextEditor.TextEditOp.Delete);
				TextEditor.MapKey("^h", TextEditor.TextEditOp.Backspace);
				TextEditor.MapKey("^b", TextEditor.TextEditOp.MoveLeft);
				TextEditor.MapKey("^f", TextEditor.TextEditOp.MoveRight);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.MoveLineStart);
				TextEditor.MapKey("^e", TextEditor.TextEditOp.MoveLineEnd);
				TextEditor.MapKey("&delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("&backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
			}
			else
			{
				TextEditor.MapKey("home", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("end", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveToEndOfPreviousWord);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveToStartOfNextWord);
				TextEditor.MapKey("^up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("^down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.SelectToEndOfPreviousWord);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.SelectToStartOfNextWord);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectGraphicalLineStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectGraphicalLineEnd);
				TextEditor.MapKey("^delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("^backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("^x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("^v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("#delete", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^insert", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("#insert", TextEditor.TextEditOp.Paste);
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00018F58 File Offset: 0x00017158
		public void ClampPos()
		{
			if (this.m_HasFocus && this.controlID != GUIUtility.keyboardControl)
			{
				this.OnLostFocus();
			}
			if (!this.m_HasFocus && this.controlID == GUIUtility.keyboardControl)
			{
				this.OnFocus();
			}
			if (this.pos < 0)
			{
				this.pos = 0;
			}
			else if (this.pos > this.content.text.Length)
			{
				this.pos = this.content.text.Length;
			}
			if (this.selectPos < 0)
			{
				this.selectPos = 0;
			}
			else if (this.selectPos > this.content.text.Length)
			{
				this.selectPos = this.content.text.Length;
			}
			if (this.m_iAltCursorPos > this.content.text.Length)
			{
				this.m_iAltCursorPos = this.content.text.Length;
			}
		}

		// Token: 0x0400040E RID: 1038
		public TouchScreenKeyboard keyboardOnScreen;

		// Token: 0x0400040F RID: 1039
		public int pos;

		// Token: 0x04000410 RID: 1040
		public int selectPos;

		// Token: 0x04000411 RID: 1041
		public int controlID;

		// Token: 0x04000412 RID: 1042
		public GUIContent content = new GUIContent();

		// Token: 0x04000413 RID: 1043
		public GUIStyle style = GUIStyle.none;

		// Token: 0x04000414 RID: 1044
		public Rect position;

		// Token: 0x04000415 RID: 1045
		public bool multiline;

		// Token: 0x04000416 RID: 1046
		public bool hasHorizontalCursorPos;

		// Token: 0x04000417 RID: 1047
		public bool isPasswordField;

		// Token: 0x04000418 RID: 1048
		internal bool m_HasFocus;

		// Token: 0x04000419 RID: 1049
		public Vector2 scrollOffset = Vector2.zero;

		// Token: 0x0400041A RID: 1050
		private bool m_TextHeightPotentiallyChanged;

		// Token: 0x0400041B RID: 1051
		public Vector2 graphicalCursorPos;

		// Token: 0x0400041C RID: 1052
		public Vector2 graphicalSelectCursorPos;

		// Token: 0x0400041D RID: 1053
		private bool m_MouseDragSelectsWholeWords;

		// Token: 0x0400041E RID: 1054
		private int m_DblClickInitPos;

		// Token: 0x0400041F RID: 1055
		private TextEditor.DblClickSnapping m_DblClickSnap;

		// Token: 0x04000420 RID: 1056
		private bool m_bJustSelected;

		// Token: 0x04000421 RID: 1057
		private int m_iAltCursorPos = -1;

		// Token: 0x04000422 RID: 1058
		private string oldText;

		// Token: 0x04000423 RID: 1059
		private int oldPos;

		// Token: 0x04000424 RID: 1060
		private int oldSelectPos;

		// Token: 0x04000425 RID: 1061
		private static Dictionary<Event, TextEditor.TextEditOp> s_Keyactions;

		// Token: 0x02000117 RID: 279
		private enum CharacterType
		{
			// Token: 0x04000427 RID: 1063
			LetterLike,
			// Token: 0x04000428 RID: 1064
			Symbol,
			// Token: 0x04000429 RID: 1065
			Symbol2,
			// Token: 0x0400042A RID: 1066
			WhiteSpace
		}

		// Token: 0x02000118 RID: 280
		public enum DblClickSnapping : byte
		{
			// Token: 0x0400042C RID: 1068
			WORDS,
			// Token: 0x0400042D RID: 1069
			PARAGRAPHS
		}

		// Token: 0x02000119 RID: 281
		private enum TextEditOp
		{
			// Token: 0x0400042F RID: 1071
			MoveLeft,
			// Token: 0x04000430 RID: 1072
			MoveRight,
			// Token: 0x04000431 RID: 1073
			MoveUp,
			// Token: 0x04000432 RID: 1074
			MoveDown,
			// Token: 0x04000433 RID: 1075
			MoveLineStart,
			// Token: 0x04000434 RID: 1076
			MoveLineEnd,
			// Token: 0x04000435 RID: 1077
			MoveTextStart,
			// Token: 0x04000436 RID: 1078
			MoveTextEnd,
			// Token: 0x04000437 RID: 1079
			MovePageUp,
			// Token: 0x04000438 RID: 1080
			MovePageDown,
			// Token: 0x04000439 RID: 1081
			MoveGraphicalLineStart,
			// Token: 0x0400043A RID: 1082
			MoveGraphicalLineEnd,
			// Token: 0x0400043B RID: 1083
			MoveWordLeft,
			// Token: 0x0400043C RID: 1084
			MoveWordRight,
			// Token: 0x0400043D RID: 1085
			MoveParagraphForward,
			// Token: 0x0400043E RID: 1086
			MoveParagraphBackward,
			// Token: 0x0400043F RID: 1087
			MoveToStartOfNextWord,
			// Token: 0x04000440 RID: 1088
			MoveToEndOfPreviousWord,
			// Token: 0x04000441 RID: 1089
			SelectLeft,
			// Token: 0x04000442 RID: 1090
			SelectRight,
			// Token: 0x04000443 RID: 1091
			SelectUp,
			// Token: 0x04000444 RID: 1092
			SelectDown,
			// Token: 0x04000445 RID: 1093
			SelectTextStart,
			// Token: 0x04000446 RID: 1094
			SelectTextEnd,
			// Token: 0x04000447 RID: 1095
			SelectPageUp,
			// Token: 0x04000448 RID: 1096
			SelectPageDown,
			// Token: 0x04000449 RID: 1097
			ExpandSelectGraphicalLineStart,
			// Token: 0x0400044A RID: 1098
			ExpandSelectGraphicalLineEnd,
			// Token: 0x0400044B RID: 1099
			SelectGraphicalLineStart,
			// Token: 0x0400044C RID: 1100
			SelectGraphicalLineEnd,
			// Token: 0x0400044D RID: 1101
			SelectWordLeft,
			// Token: 0x0400044E RID: 1102
			SelectWordRight,
			// Token: 0x0400044F RID: 1103
			SelectToEndOfPreviousWord,
			// Token: 0x04000450 RID: 1104
			SelectToStartOfNextWord,
			// Token: 0x04000451 RID: 1105
			SelectParagraphBackward,
			// Token: 0x04000452 RID: 1106
			SelectParagraphForward,
			// Token: 0x04000453 RID: 1107
			Delete,
			// Token: 0x04000454 RID: 1108
			Backspace,
			// Token: 0x04000455 RID: 1109
			DeleteWordBack,
			// Token: 0x04000456 RID: 1110
			DeleteWordForward,
			// Token: 0x04000457 RID: 1111
			DeleteLineBack,
			// Token: 0x04000458 RID: 1112
			Cut,
			// Token: 0x04000459 RID: 1113
			Copy,
			// Token: 0x0400045A RID: 1114
			Paste,
			// Token: 0x0400045B RID: 1115
			SelectAll,
			// Token: 0x0400045C RID: 1116
			SelectNone,
			// Token: 0x0400045D RID: 1117
			ScrollStart,
			// Token: 0x0400045E RID: 1118
			ScrollEnd,
			// Token: 0x0400045F RID: 1119
			ScrollPageUp,
			// Token: 0x04000460 RID: 1120
			ScrollPageDown
		}
	}
}
