using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000087 RID: 135
	public class GUIUtility
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x00010D60 File Offset: 0x0000EF60
		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		// Token: 0x060006A9 RID: 1705
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetControlID(int hint, FocusType focus);

		// Token: 0x060006AA RID: 1706 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00010D7C File Offset: 0x0000EF7C
		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(0, focus, position);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00010D88 File Offset: 0x0000EF88
		public static int GetControlID(int hint, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(hint, focus, position);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00010D94 File Offset: 0x0000EF94
		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(contents.hash, focus, position);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		private static int Internal_GetNextControlID2(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.INTERNAL_CALL_Internal_GetNextControlID2(hint, focusType, ref rect);
		}

		// Token: 0x060006AF RID: 1711
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_Internal_GetNextControlID2(int hint, FocusType focusType, ref Rect rect);

		// Token: 0x060006B0 RID: 1712 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00010DBC File Offset: 0x0000EFBC
		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		// Token: 0x060006B2 RID: 1714
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int GetPermanentControlID();

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x060006B5 RID: 1717
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetHotControl();

		// Token: 0x060006B6 RID: 1718
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x060006B7 RID: 1719
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void UpdateUndoName();

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006B8 RID: 1720
		// (set) Token: 0x060006B9 RID: 1721
		public static extern int keyboardControl { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060006BA RID: 1722 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x060006BB RID: 1723
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006BC RID: 1724
		// (set) Token: 0x060006BD RID: 1725
		internal static extern string systemCopyBuffer { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060006BE RID: 1726 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode);
		}

		// Token: 0x060006BF RID: 1727
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern GUISkin Internal_GetDefaultSkin(int skinMode);

		// Token: 0x060006C0 RID: 1728
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Object Internal_GetBuiltinSkin(int skin);

		// Token: 0x060006C1 RID: 1729 RVA: 0x00010DEC File Offset: 0x0000EFEC
		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00010DFC File Offset: 0x0000EFFC
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUI.skin = null;
			if (useGUILayout != 0)
			{
				GUILayoutUtility.SelectIDList(instanceID, false);
				GUILayoutUtility.Begin(instanceID);
			}
			GUI.changed = false;
		}

		// Token: 0x060006C3 RID: 1731
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_ExitGUI();

		// Token: 0x060006C4 RID: 1732 RVA: 0x00010E2C File Offset: 0x0000F02C
		internal static void EndGUI(int layoutType)
		{
			try
			{
				if (Event.current.type == EventType.Layout)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		internal static bool EndGUIFromException(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			if (!(exception is ExitGUIException) && !(exception.InnerException is ExitGUIException))
			{
				return false;
			}
			GUIUtility.Internal_ExitGUI();
			return true;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		internal static void CheckOnGUI()
		{
			if (GUIUtility.Internal_GetGUIDepth() <= 0)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x060006C7 RID: 1735
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int Internal_GetGUIDepth();

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006C8 RID: 1736
		// (set) Token: 0x060006C9 RID: 1737
		internal static extern bool mouseUsed { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060006CA RID: 1738 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIClip.Unclip(guiPoint) + GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00010F04 File Offset: 0x0000F104
		internal static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00010F48 File Offset: 0x0000F148
		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.Clip(screenPoint) - GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00010F5C File Offset: 0x0000F15C
		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 lhs = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = lhs * matrix;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00011010 File Offset: 0x0000F210
		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 lhs = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = lhs * matrix;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006D0 RID: 1744
		public static extern bool hasModalWindow { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006D1 RID: 1745
		// (set) Token: 0x060006D2 RID: 1746
		internal static extern bool textFieldInput { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x04000190 RID: 400
		[NotRenamed]
		internal static int s_SkinMode;

		// Token: 0x04000191 RID: 401
		[NotRenamed]
		internal static int s_OriginalID;

		// Token: 0x04000192 RID: 402
		internal static Vector2 s_EditorScreenPointOffset = Vector2.zero;

		// Token: 0x04000193 RID: 403
		internal static bool s_HasKeyboardFocus = false;
	}
}
