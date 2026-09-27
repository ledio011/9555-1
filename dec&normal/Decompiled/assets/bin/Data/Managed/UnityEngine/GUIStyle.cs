using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyle
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x000105E8 File Offset: 0x0000E7E8
		public GUIStyle()
		{
			this.Init();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public GUIStyle(GUIStyle other)
		{
			this.InitCopy(other);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00010610 File Offset: 0x0000E810
		~GUIStyle()
		{
			this.Cleanup();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00010640 File Offset: 0x0000E840
		internal void CreateObjectReferences()
		{
			this.m_FontInternal = this.GetFontInternal();
			this.normal.RefreshAssetReference();
			this.hover.RefreshAssetReference();
			this.active.RefreshAssetReference();
			this.focused.RefreshAssetReference();
			this.onNormal.RefreshAssetReference();
			this.onHover.RefreshAssetReference();
			this.onActive.RefreshAssetReference();
			this.onFocused.RefreshAssetReference();
		}

		// Token: 0x06000650 RID: 1616
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000651 RID: 1617
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InitCopy(GUIStyle other);

		// Token: 0x06000652 RID: 1618
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000653 RID: 1619
		// (set) Token: 0x06000654 RID: 1620
		public extern string name { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000106B4 File Offset: 0x0000E8B4
		public GUIStyleState normal
		{
			get
			{
				if (this.m_Normal == null)
				{
					this.m_Normal = new GUIStyleState(this, this.GetStyleStatePtr(0));
				}
				return this.m_Normal;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000106DC File Offset: 0x0000E8DC
		public GUIStyleState hover
		{
			get
			{
				if (this.m_Hover == null)
				{
					this.m_Hover = new GUIStyleState(this, this.GetStyleStatePtr(1));
				}
				return this.m_Hover;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00010704 File Offset: 0x0000E904
		public GUIStyleState active
		{
			get
			{
				if (this.m_Active == null)
				{
					this.m_Active = new GUIStyleState(this, this.GetStyleStatePtr(2));
				}
				return this.m_Active;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001072C File Offset: 0x0000E92C
		public GUIStyleState onNormal
		{
			get
			{
				if (this.m_OnNormal == null)
				{
					this.m_OnNormal = new GUIStyleState(this, this.GetStyleStatePtr(4));
				}
				return this.m_OnNormal;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00010754 File Offset: 0x0000E954
		public GUIStyleState onHover
		{
			get
			{
				if (this.m_OnHover == null)
				{
					this.m_OnHover = new GUIStyleState(this, this.GetStyleStatePtr(5));
				}
				return this.m_OnHover;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001077C File Offset: 0x0000E97C
		public GUIStyleState onActive
		{
			get
			{
				if (this.m_OnActive == null)
				{
					this.m_OnActive = new GUIStyleState(this, this.GetStyleStatePtr(6));
				}
				return this.m_OnActive;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public GUIStyleState focused
		{
			get
			{
				if (this.m_Focused == null)
				{
					this.m_Focused = new GUIStyleState(this, this.GetStyleStatePtr(3));
				}
				return this.m_Focused;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000107CC File Offset: 0x0000E9CC
		public GUIStyleState onFocused
		{
			get
			{
				if (this.m_OnFocused == null)
				{
					this.m_OnFocused = new GUIStyleState(this, this.GetStyleStatePtr(7));
				}
				return this.m_OnFocused;
			}
		}

		// Token: 0x0600065D RID: 1629
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public RectOffset margin
		{
			get
			{
				if (this.m_Margin == null)
				{
					this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1));
				}
				return this.m_Margin;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001081C File Offset: 0x0000EA1C
		public RectOffset padding
		{
			get
			{
				if (this.m_Padding == null)
				{
					this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2));
				}
				return this.m_Padding;
			}
		}

		// Token: 0x06000660 RID: 1632
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000661 RID: 1633
		public extern ImagePosition imagePosition { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000154 RID: 340
		// (set) Token: 0x06000662 RID: 1634
		public extern TextAnchor alignment { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000663 RID: 1635
		public extern bool wordWrap { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00010844 File Offset: 0x0000EA44
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001085C File Offset: 0x0000EA5C
		public Vector2 contentOffset
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_contentOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_contentOffset(ref value);
			}
		}

		// Token: 0x06000666 RID: 1638
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_contentOffset(out Vector2 value);

		// Token: 0x06000667 RID: 1639
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_contentOffset(ref Vector2 value);

		// Token: 0x17000157 RID: 343
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00010868 File Offset: 0x0000EA68
		internal Vector2 Internal_clipOffset
		{
			set
			{
				this.INTERNAL_set_Internal_clipOffset(ref value);
			}
		}

		// Token: 0x06000669 RID: 1641
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_Internal_clipOffset(ref Vector2 value);

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600066A RID: 1642
		public extern float fixedWidth { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600066B RID: 1643
		public extern float fixedHeight { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600066C RID: 1644
		// (set) Token: 0x0600066D RID: 1645
		public extern bool stretchWidth { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600066E RID: 1646
		// (set) Token: 0x0600066F RID: 1647
		public extern bool stretchHeight { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000670 RID: 1648
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x06000671 RID: 1649
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Font GetFontInternal();

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00010874 File Offset: 0x0000EA74
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00010888 File Offset: 0x0000EA88
		private static void Internal_Draw(IntPtr target, Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			Internal_DrawArguments internal_DrawArguments = default(Internal_DrawArguments);
			internal_DrawArguments.target = target;
			internal_DrawArguments.position = position;
			internal_DrawArguments.isHover = ((!isHover) ? 0 : 1);
			internal_DrawArguments.isActive = ((!isActive) ? 0 : 1);
			internal_DrawArguments.on = ((!on) ? 0 : 1);
			internal_DrawArguments.hasKeyboardFocus = ((!hasKeyboardFocus) ? 0 : 1);
			GUIStyle.Internal_Draw(content, ref internal_DrawArguments);
		}

		// Token: 0x06000674 RID: 1652
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Draw(GUIContent content, ref Internal_DrawArguments arguments);

		// Token: 0x06000675 RID: 1653 RVA: 0x00010908 File Offset: 0x0000EB08
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.none, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00010924 File Offset: 0x0000EB24
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001093C File Offset: 0x0000EB3C
		[ExcludeFromDocs]
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			bool on = false;
			this.Draw(position, content, controlID, on);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00010958 File Offset: 0x0000EB58
		public void Draw(Rect position, GUIContent content, int controlID, [DefaultValue("false")] bool on)
		{
			if (content != null)
			{
				GUIStyle.Internal_Draw2(this.m_Ptr, position, content, controlID, on);
			}
			else
			{
				Debug.LogError("Style.Draw may not be called with GUIContent that is null.");
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00010980 File Offset: 0x0000EB80
		private static void Internal_Draw2(IntPtr style, Rect position, GUIContent content, int controlID, bool on)
		{
			GUIStyle.INTERNAL_CALL_Internal_Draw2(style, ref position, content, controlID, on);
		}

		// Token: 0x0600067A RID: 1658
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_Draw2(IntPtr style, ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x0600067B RID: 1659
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x0600067C RID: 1660 RVA: 0x00010990 File Offset: 0x0000EB90
		private static void Internal_DrawCursor(IntPtr target, Rect position, GUIContent content, int pos, Color cursorColor)
		{
			GUIStyle.INTERNAL_CALL_Internal_DrawCursor(target, ref position, content, pos, ref cursorColor);
		}

		// Token: 0x0600067D RID: 1661
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_DrawCursor(IntPtr target, ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x0600067E RID: 1662 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public void DrawCursor(Rect position, GUIContent content, int controlID, int Character)
		{
			Event current = Event.current;
			if (current.type == EventType.Repaint)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				if (cursorFlashSpeed == 0f || num < 0.5f)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				GUIStyle.Internal_DrawCursor(this.m_Ptr, position, content, Character, cursorColor);
			}
		}

		// Token: 0x0600067F RID: 1663
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_DrawWithTextSelection(GUIContent content, ref Internal_DrawWithTextSelectionArguments arguments);

		// Token: 0x06000680 RID: 1664 RVA: 0x00010A30 File Offset: 0x0000EC30
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			Event current = Event.current;
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			if (cursorFlashSpeed == 0f || num < 0.5f)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			Internal_DrawWithTextSelectionArguments internal_DrawWithTextSelectionArguments = default(Internal_DrawWithTextSelectionArguments);
			internal_DrawWithTextSelectionArguments.target = this.m_Ptr;
			internal_DrawWithTextSelectionArguments.position = position;
			internal_DrawWithTextSelectionArguments.firstPos = firstSelectedCharacter;
			internal_DrawWithTextSelectionArguments.lastPos = lastSelectedCharacter;
			internal_DrawWithTextSelectionArguments.cursorColor = cursorColor;
			internal_DrawWithTextSelectionArguments.selectionColor = GUI.skin.settings.selectionColor;
			internal_DrawWithTextSelectionArguments.isHover = ((!position.Contains(current.mousePosition)) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.isActive = ((controlID != GUIUtility.hotControl) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.on = 0;
			internal_DrawWithTextSelectionArguments.hasKeyboardFocus = ((controlID != GUIUtility.keyboardControl || !GUIStyle.showKeyboardFocus) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.drawSelectionAsComposition = ((!drawSelectionAsComposition) ? 0 : 1);
			GUIStyle.Internal_DrawWithTextSelection(content, ref internal_DrawWithTextSelectionArguments);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00010B74 File Offset: 0x0000ED74
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x06000682 RID: 1666
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00010B84 File Offset: 0x0000ED84
		public static GUIStyle none
		{
			get
			{
				if (GUIStyle.s_None == null)
				{
					GUIStyle.s_None = new GUIStyle();
				}
				return GUIStyle.s_None;
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 result;
			GUIStyle.Internal_GetCursorPixelPosition(this.m_Ptr, position, content, cursorStringIndex, out result);
			return result;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		internal static void Internal_GetCursorPixelPosition(IntPtr target, Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret)
		{
			GUIStyle.INTERNAL_CALL_Internal_GetCursorPixelPosition(target, ref position, content, cursorStringIndex, out ret);
		}

		// Token: 0x06000686 RID: 1670
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_GetCursorPixelPosition(IntPtr target, ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x06000687 RID: 1671 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.Internal_GetCursorStringIndex(this.m_Ptr, position, content, cursorPixelPosition);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		internal static int Internal_GetCursorStringIndex(IntPtr target, Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.INTERNAL_CALL_Internal_GetCursorStringIndex(target, ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x06000689 RID: 1673
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_Internal_GetCursorStringIndex(IntPtr target, ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x0600068A RID: 1674 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public Vector2 CalcSize(GUIContent content)
		{
			Vector2 result;
			GUIStyle.Internal_CalcSize(this.m_Ptr, content, out result);
			return result;
		}

		// Token: 0x0600068B RID: 1675
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void Internal_CalcSize(IntPtr target, GUIContent content, out Vector2 ret);

		// Token: 0x0600068C RID: 1676 RVA: 0x00010C0C File Offset: 0x0000EE0C
		public float CalcHeight(GUIContent content, float width)
		{
			return GUIStyle.Internal_CalcHeight(this.m_Ptr, content, width);
		}

		// Token: 0x0600068D RID: 1677
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_CalcHeight(IntPtr target, GUIContent content, float width);

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00010C1C File Offset: 0x0000EE1C
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			GUIStyle.Internal_CalcMinMaxWidth(this.m_Ptr, content, out minWidth, out maxWidth);
		}

		// Token: 0x06000690 RID: 1680
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CalcMinMaxWidth(IntPtr target, GUIContent content, out float minWidth, out float maxWidth);

		// Token: 0x06000691 RID: 1681 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[]
			{
				this.name
			});
		}

		// Token: 0x0400017D RID: 381
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400017E RID: 382
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x0400017F RID: 383
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x04000180 RID: 384
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x04000181 RID: 385
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x04000182 RID: 386
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x04000183 RID: 387
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x04000184 RID: 388
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x04000185 RID: 389
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x04000186 RID: 390
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x04000187 RID: 391
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x04000188 RID: 392
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x04000189 RID: 393
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x0400018A RID: 394
		[NonSerialized]
		private Font m_FontInternal;

		// Token: 0x0400018B RID: 395
		internal static bool showKeyboardFocus = true;

		// Token: 0x0400018C RID: 396
		private static GUIStyle s_None;
	}
}
