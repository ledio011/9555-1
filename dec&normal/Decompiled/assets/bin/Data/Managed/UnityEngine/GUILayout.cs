using System;

namespace UnityEngine
{
	// Token: 0x02000077 RID: 119
	public sealed class GUILayout
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000D140 File Offset: 0x0000B340
		public static void Label(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000D158 File Offset: 0x0000B358
		public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000D168 File Offset: 0x0000B368
		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000D17C File Offset: 0x0000B37C
		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000D194 File Offset: 0x0000B394
		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public static string TextArea(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, GUI.skin.textArea, options);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
			GUIContent guicontent = GUIContent.Temp(text);
			if (GUIUtility.keyboardControl != controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			else
			{
				guicontent = GUIContent.Temp(text + Input.compositionString);
			}
			Rect rect = GUILayoutUtility.GetRect(guicontent, style, options);
			if (GUIUtility.keyboardControl == controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			GUI.DoTextField(rect, controlID, guicontent, multiline, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000D24C File Offset: 0x0000B44C
		public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000D268 File Offset: 0x0000B468
		private static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000D27C File Offset: 0x0000B47C
		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000D29C File Offset: 0x0000B49C
		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, new GUILayoutOption[]
				{
					GUILayout.Height(pixels)
				});
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[]
				{
					GUILayout.Width(pixels)
				});
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000D328 File Offset: 0x0000B528
		public static void FlexibleSpace()
		{
			GUIUtility.CheckOnGUI();
			GUILayoutOption guilayoutOption;
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				guilayoutOption = GUILayout.ExpandHeight(true);
			}
			else
			{
				guilayoutOption = GUILayout.ExpandWidth(true);
			}
			guilayoutOption.value = 10000;
			GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[]
			{
				guilayoutOption
			});
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000D390 File Offset: 0x0000B590
		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = false;
			if (style != GUIStyle.none || content != GUIContent.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		public static void EndHorizontal()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndHorizontal");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000D404 File Offset: 0x0000B604
		public static void BeginVertical(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000D418 File Offset: 0x0000B618
		public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = true;
			if (style != GUIStyle.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000D458 File Offset: 0x0000B658
		public static void EndVertical()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndVertical");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000D46C File Offset: 0x0000B66C
		public static void BeginArea(Rect screenRect)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000D480 File Offset: 0x0000B680
		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			if (Event.current.type == EventType.Layout)
			{
				guilayoutGroup.resetCoords = true;
				guilayoutGroup.minWidth = (guilayoutGroup.maxWidth = screenRect.width);
				guilayoutGroup.minHeight = (guilayoutGroup.maxHeight = screenRect.height);
				guilayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, guilayoutGroup.rect.xMax, guilayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(guilayoutGroup.rect, content, style);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000D524 File Offset: 0x0000B724
		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.Used)
			{
				return;
			}
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
			GUI.EndGroup();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000D57C File Offset: 0x0000B77C
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000D58C File Offset: 0x0000B78C
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000D59C File Offset: 0x0000B79C
		public static GUILayoutOption ExpandWidth(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, (!expand) ? 0 : 1);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public static GUILayoutOption ExpandHeight(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, (!expand) ? 0 : 1);
		}
	}
}
