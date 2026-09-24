using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000070 RID: 112
	public class GUI
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000A51C File Offset: 0x0000871C
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0000A524 File Offset: 0x00008724
		internal static DateTime nextScrollStepTime { get; set; } = DateTime.Now;

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000A52C File Offset: 0x0000872C
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0000A534 File Offset: 0x00008734
		internal static int scrollTroughSide { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000A564 File Offset: 0x00008764
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000A53C File Offset: 0x0000873C
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				if (!value)
				{
					value = GUIUtility.GetDefaultSkin();
				}
				GUI.s_Skin = value;
				value.MakeCurrent();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000A570 File Offset: 0x00008770
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0000A588 File Offset: 0x00008788
		public static Color color
		{
			get
			{
				Color result;
				GUI.INTERNAL_get_color(out result);
				return result;
			}
			set
			{
				GUI.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x060004E8 RID: 1256
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_color(out Color value);

		// Token: 0x060004E9 RID: 1257
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000A594 File Offset: 0x00008794
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0000A5AC File Offset: 0x000087AC
		public static Color backgroundColor
		{
			get
			{
				Color result;
				GUI.INTERNAL_get_backgroundColor(out result);
				return result;
			}
			set
			{
				GUI.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x060004EC RID: 1260
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x060004ED RID: 1261
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000A5B8 File Offset: 0x000087B8
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0000A5D0 File Offset: 0x000087D0
		public static Color contentColor
		{
			get
			{
				Color result;
				GUI.INTERNAL_get_contentColor(out result);
				return result;
			}
			set
			{
				GUI.INTERNAL_set_contentColor(ref value);
			}
		}

		// Token: 0x060004F0 RID: 1264
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_contentColor(out Color value);

		// Token: 0x060004F1 RID: 1265
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_contentColor(ref Color value);

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004F2 RID: 1266
		// (set) Token: 0x060004F3 RID: 1267
		public static extern bool changed { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004F4 RID: 1268
		// (set) Token: 0x060004F5 RID: 1269
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000A5DC File Offset: 0x000087DC
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000A5EC File Offset: 0x000087EC
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000A60C File Offset: 0x0000880C
		public static string tooltip
		{
			get
			{
				string text = GUI.Internal_GetTooltip();
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		// Token: 0x060004FA RID: 1274
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern string Internal_GetTooltip();

		// Token: 0x060004FB RID: 1275
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetTooltip(string value);

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000A614 File Offset: 0x00008814
		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		// Token: 0x060004FD RID: 1277
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern string Internal_GetMouseTooltip();

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000A61C File Offset: 0x0000881C
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0000A624 File Offset: 0x00008824
		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000500 RID: 1280
		// (set) Token: 0x06000501 RID: 1281
		public static extern int depth { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000502 RID: 1282 RVA: 0x0000A62C File Offset: 0x0000882C
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000A644 File Offset: 0x00008844
		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000A65C File Offset: 0x0000885C
		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000A670 File Offset: 0x00008870
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000A680 File Offset: 0x00008880
		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000A690 File Offset: 0x00008890
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUI.DoLabel(position, content, style.m_Ptr);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000A6A0 File Offset: 0x000088A0
		private static void DoLabel(Rect position, GUIContent content, IntPtr style)
		{
			GUI.INTERNAL_CALL_DoLabel(ref position, content, style);
		}

		// Token: 0x06000509 RID: 1289
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DoLabel(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x0600050A RID: 1290
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void InitializeGUIClipTexture();

		// Token: 0x0600050B RID: 1291 RVA: 0x0000A6AC File Offset: 0x000088AC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			float imageAspect = 0f;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000A6CC File Offset: 0x000088CC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			float imageAspect = 0f;
			bool alphaBlend = true;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000A6EC File Offset: 0x000088EC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image)
		{
			float imageAspect = 0f;
			bool alphaBlend = true;
			ScaleMode scaleMode = ScaleMode.StretchToFill;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000A710 File Offset: 0x00008910
		public static void DrawTexture(Rect position, Texture image, [DefaultValue("ScaleMode.StretchToFill")] ScaleMode scaleMode, [DefaultValue("true")] bool alphaBlend, [DefaultValue("0")] float imageAspect)
		{
			if (Event.current.type == EventType.Repaint)
			{
				if (image == null)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
					return;
				}
				if (imageAspect == 0f)
				{
					imageAspect = (float)image.width / (float)image.height;
				}
				Material mat = (!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial;
				float num = position.width / position.height;
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = mat;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					internalDrawTextureArguments.screenRect = position;
					internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
					Graphics.DrawTexture(ref internalDrawTextureArguments);
					break;
				case ScaleMode.ScaleAndCrop:
					if (num > imageAspect)
					{
						float num2 = imageAspect / num;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num3 = num / imageAspect;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				case ScaleMode.ScaleToFit:
					if (num > imageAspect)
					{
						float num4 = imageAspect / num;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num5 = num / imageAspect;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				}
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000A994 File Offset: 0x00008B94
		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool result = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				result = true;
				break;
			case ScaleMode.ScaleAndCrop:
				if (num > imageAspect)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					result = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					result = true;
				}
				break;
			case ScaleMode.ScaleToFit:
				if (num > imageAspect)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				break;
			}
			return result;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000AB30 File Offset: 0x00008D30
		[ExcludeFromDocs]
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			bool alphaBlend = true;
			GUI.DrawTextureWithTexCoords(position, image, texCoords, alphaBlend);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000AB48 File Offset: 0x00008D48
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, [DefaultValue("true")] bool alphaBlend)
		{
			if (Event.current.type == EventType.Repaint)
			{
				Material mat = (!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial;
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = mat;
				internalDrawTextureArguments.screenRect = position;
				internalDrawTextureArguments.sourceRect = texCoords;
				Graphics.DrawTexture(ref internalDrawTextureArguments);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000512 RID: 1298
		private static extern Material blendMaterial { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000513 RID: 1299
		private static extern Material blitMaterial { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000514 RID: 1300 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000AC20 File Offset: 0x00008E20
		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000AC30 File Offset: 0x00008E30
		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000AC40 File Offset: 0x00008E40
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.boxHash, FocusType.Passive);
			if (Event.current.type == EventType.Repaint)
			{
				style.Draw(position, content, controlID);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000AC78 File Offset: 0x00008E78
		public static bool Button(Rect position, string text)
		{
			return GUI.DoButton(position, GUIContent.Temp(text), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000AC98 File Offset: 0x00008E98
		public static bool Button(Rect position, Texture image)
		{
			return GUI.DoButton(position, GUIContent.Temp(image), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		public static bool Button(Rect position, GUIContent content)
		{
			return GUI.DoButton(position, content, GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return GUI.DoButton(position, GUIContent.Temp(text), style.m_Ptr);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoButton(position, GUIContent.Temp(image), style.m_Ptr);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoButton(position, content, style.m_Ptr);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000AD08 File Offset: 0x00008F08
		private static bool DoButton(Rect position, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoButton(ref position, content, style);
		}

		// Token: 0x06000521 RID: 1313
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_DoButton(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x06000522 RID: 1314 RVA: 0x0000AD14 File Offset: 0x00008F14
		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000AD30 File Offset: 0x00008F30
		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000AD60 File Offset: 0x00008F60
		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Native);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000AD70 File Offset: 0x00008F70
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Native);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000AD80 File Offset: 0x00008F80
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Native);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000AD8C File Offset: 0x00008F8C
		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.repeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl == EventType.MouseDown)
			{
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				return false;
			}
			if (typeForControl != EventType.MouseUp)
			{
				if (typeForControl != EventType.Repaint)
				{
					return false;
				}
				style.Draw(position, content, controlID);
				return controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
			}
			else
			{
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					return position.Contains(Event.current.mousePosition);
				}
				return false;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000AE54 File Offset: 0x00009054
		public static string TextField(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, GUI.skin.textField, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000AE8C File Offset: 0x0000908C
		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, GUI.skin.textField, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000AEF4 File Offset: 0x000090F4
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000AF24 File Offset: 0x00009124
		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000AF3C File Offset: 0x0000913C
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000AF54 File Offset: 0x00009154
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000AF60 File Offset: 0x00009160
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent guicontent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), guicontent, false, maxLength, style, password, maskChar);
			text = ((!GUI.changed) ? password : guicontent.text);
			GUI.changed = (GUI.changed || changed);
			return text;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000AFC0 File Offset: 0x000091C0
		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type != EventType.Repaint && Event.current.type != EventType.MouseDown) ? password : string.Empty.PadRight(password.Length, maskChar);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000AFF8 File Offset: 0x000091F8
		public static string TextArea(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, GUI.skin.textArea, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000B030 File Offset: 0x00009230
		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, GUI.skin.textArea, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000B068 File Offset: 0x00009268
		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000B098 File Offset: 0x00009298
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000B0C8 File Offset: 0x000092C8
		private static string TextArea(Rect position, GUIContent content, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(content.text, content.image);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000B100 File Offset: 0x00009300
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText = null, char maskChar = '\0')
		{
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			GUIUtility.CheckOnGUI();
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.content.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.ClampPos();
			if (GUIUtility.keyboardControl == id && Event.current.type != EventType.Layout)
			{
				textEditor.UpdateScrollOffsetIfNeeded();
			}
			GUI.HandleTextFieldEventForTouchscreen(position, id, content, multiline, maxLength, style, secureText, maskChar, textEditor);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000B1C4 File Offset: 0x000093C4
		private static void HandleTextFieldEventForTouchscreen(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar, TextEditor editor)
		{
			Event current = Event.current;
			EventType type = current.type;
			if (type != EventType.MouseDown)
			{
				if (type == EventType.Repaint)
				{
					if (editor.keyboardOnScreen != null)
					{
						content.text = editor.keyboardOnScreen.text;
						if (maxLength >= 0 && content.text.Length > maxLength)
						{
							content.text = content.text.Substring(0, maxLength);
						}
						if (editor.keyboardOnScreen.done)
						{
							editor.keyboardOnScreen = null;
							GUI.changed = true;
						}
					}
					string text = content.text;
					if (secureText != null)
					{
						content.text = GUI.PasswordFieldGetStrToShow(text, maskChar);
					}
					style.Draw(position, content, id, false);
					content.text = text;
				}
			}
			else if (position.Contains(current.mousePosition))
			{
				GUIUtility.hotControl = id;
				if (GUI.hotTextField != -1 && GUI.hotTextField != id)
				{
					TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.hotTextField);
					textEditor.keyboardOnScreen = null;
				}
				GUI.hotTextField = id;
				if (GUIUtility.keyboardControl != id)
				{
					GUIUtility.keyboardControl = id;
				}
				editor.keyboardOnScreen = TouchScreenKeyboard.Open((secureText == null) ? content.text : secureText, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
				current.Use();
			}
		}

		// Token: 0x06000539 RID: 1337
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetNextControlName(string name);

		// Token: 0x0600053A RID: 1338
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetNameOfFocusedControl();

		// Token: 0x0600053B RID: 1339
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void FocusControl(string name);

		// Token: 0x0600053C RID: 1340 RVA: 0x0000B32C File Offset: 0x0000952C
		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000B348 File Offset: 0x00009548
		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000B364 File Offset: 0x00009564
		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000B378 File Offset: 0x00009578
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000B388 File Offset: 0x00009588
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000B398 File Offset: 0x00009598
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.toggleHash, FocusType.Native, position), value, content, style.m_Ptr);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoToggle(position, id, value, content, style.m_Ptr);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000B3C8 File Offset: 0x000095C8
		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoToggle(ref position, id, value, content, style);
		}

		// Token: 0x06000544 RID: 1348
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_DoToggle(ref Rect position, int id, bool value, GUIContent content, IntPtr style);

		// Token: 0x06000545 RID: 1349 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000B410 File Offset: 0x00009610
		public static int Toolbar(Rect position, int selected, GUIContent[] content)
		{
			return GUI.Toolbar(position, selected, content, GUI.s_Skin.button);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000B424 File Offset: 0x00009624
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000B434 File Offset: 0x00009634
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000B444 File Offset: 0x00009644
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			GUIStyle firstStyle;
			GUIStyle midStyle;
			GUIStyle lastStyle;
			GUI.FindStyles(ref style, out firstStyle, out midStyle, out lastStyle, "left", "mid", "right");
			return GUI.DoButtonGrid(position, selected, contents, contents.Length, style, firstStyle, midStyle, lastStyle);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000B47C File Offset: 0x0000967C
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000B490 File Offset: 0x00009690
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000B4A4 File Offset: 0x000096A4
		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000B4C4 File Offset: 0x000096C4
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			if (style == null)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, xCount, style, style, style, style);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000B50C File Offset: 0x0000970C
		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			if (style == null)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = GUI.skin.FindStyle(name + mid);
			if (midStyle == null)
			{
				midStyle = style;
			}
			firstStyle = GUI.skin.FindStyle(name + first);
			if (firstStyle == null)
			{
				firstStyle = midStyle;
			}
			lastStyle = GUI.skin.FindStyle(name + last);
			if (lastStyle == null)
			{
				lastStyle = midStyle;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000B594 File Offset: 0x00009794
		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			if (xCount < 2)
			{
				return 0;
			}
			if (xCount == 2)
			{
				return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
			}
			int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
			return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000B628 File Offset: 0x00009828
		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			if (num == 0)
			{
				return selected;
			}
			if (xCount <= 0)
			{
				Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
				return selected;
			}
			int controlID = GUIUtility.GetControlID(GUI.buttonGridHash, FocusType.Native, position);
			int num2 = num / xCount;
			if (num % xCount != 0)
			{
				num2++;
			}
			float num3 = (float)GUI.CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
			float num4 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1));
			float elemWidth = (position.width - num3) / (float)xCount;
			float elemHeight = (position.height - num4) / (float)num2;
			if (style.fixedWidth != 0f)
			{
				elemWidth = style.fixedWidth;
			}
			if (style.fixedHeight != 0f)
			{
				elemHeight = style.fixedHeight;
			}
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.MouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					if (GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true) != -1)
					{
						GUIUtility.hotControl = controlID;
						Event.current.Use();
					}
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true);
					GUI.changed = true;
					return buttonGridMouseSelection;
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
			{
				GUIStyle guistyle = null;
				GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
				position = new Rect(0f, 0f, position.width, position.height);
				Rect[] array = GUI.CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
				int buttonGridMouseSelection2 = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, controlID == GUIUtility.hotControl);
				bool flag = position.Contains(Event.current.mousePosition);
				GUIUtility.mouseUsed = (GUIUtility.mouseUsed || flag);
				for (int i = 0; i < num; i++)
				{
					GUIStyle guistyle2;
					if (i != 0)
					{
						guistyle2 = midStyle;
					}
					else
					{
						guistyle2 = firstStyle;
					}
					if (i == num - 1)
					{
						guistyle2 = lastStyle;
					}
					if (num == 1)
					{
						guistyle2 = style;
					}
					if (i != selected)
					{
						guistyle2.Draw(array[i], contents[i], i == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl && GUI.enabled, false, false);
					}
					else
					{
						guistyle = guistyle2;
					}
				}
				if (selected < num && selected > -1)
				{
					guistyle.Draw(array[selected], contents[selected], selected == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl, true, false);
				}
				if (buttonGridMouseSelection2 >= 0)
				{
					GUI.tooltip = contents[buttonGridMouseSelection2].tooltip;
				}
				GUIClip.Pop();
				break;
			}
			}
			return selected;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		private static Rect[] CalcMouseRects(Rect position, int count, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders)
		{
			int num = 0;
			int num2 = 0;
			float num3 = position.xMin;
			float num4 = position.yMin;
			GUIStyle guistyle = style;
			Rect[] array = new Rect[count];
			if (count > 1)
			{
				guistyle = firstStyle;
			}
			for (int i = 0; i < count; i++)
			{
				if (!addBorders)
				{
					array[i] = new Rect(num3, num4, elemWidth, elemHeight);
				}
				else
				{
					array[i] = guistyle.margin.Add(new Rect(num3, num4, elemWidth, elemHeight));
				}
				array[i].width = Mathf.Round(array[i].xMax) - Mathf.Round(array[i].x);
				array[i].x = Mathf.Round(array[i].x);
				GUIStyle guistyle2 = midStyle;
				if (i == count - 2)
				{
					guistyle2 = lastStyle;
				}
				num3 += elemWidth + (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				num2++;
				if (num2 >= xCount)
				{
					num++;
					num2 = 0;
					num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					num3 = position.xMin;
				}
			}
			return array;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000BB24 File Offset: 0x00009D24
		private static int GetButtonGridMouseSelection(Rect[] buttonRects, Vector2 mousePos, bool findNearest)
		{
			for (int i = 0; i < buttonRects.Length; i++)
			{
				if (buttonRects[i].Contains(mousePos))
				{
					return i;
				}
			}
			if (!findNearest)
			{
				return -1;
			}
			float num = 10000000f;
			int result = -1;
			for (int j = 0; j < buttonRects.Length; j++)
			{
				Rect rect = buttonRects[j];
				Vector2 b = new Vector2(Mathf.Clamp(mousePos.x, rect.xMin, rect.xMax), Mathf.Clamp(mousePos.y, rect.yMin, rect.yMax));
				float sqrMagnitude = (mousePos - b).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = j;
					num = sqrMagnitude;
				}
			}
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000BC24 File Offset: 0x00009E24
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000BC50 File Offset: 0x00009E50
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000BC8C File Offset: 0x00009E8C
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			GUIUtility.CheckOnGUI();
			SliderHandler sliderHandler = new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id);
			return sliderHandler.Handle();
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600055B RID: 1371
		internal static extern bool usePageScrollbars { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600055C RID: 1372 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		// Token: 0x0600055E RID: 1374
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void InternalRepaintEditorWindow();

		// Token: 0x0600055F RID: 1375 RVA: 0x0000BD98 File Offset: 0x00009F98
		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool result = false;
			if (GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive))
			{
				bool flag = GUI.scrollControlID != scrollerID;
				GUI.scrollControlID = scrollerID;
				if (flag)
				{
					result = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else if (DateTime.Now >= GUI.nextScrollStepTime)
				{
					result = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
				}
				if (Event.current.type == EventType.Repaint)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000BE3C File Offset: 0x0000A03C
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000BE80 File Offset: 0x0000A080
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		private static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive, position);
			Rect position2;
			Rect rect;
			Rect rect2;
			if (horiz)
			{
				position2 = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect2 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				position2 = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect2 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(position2, value, size, leftValue, rightValue, slider, thumb, horiz, controlID);
			bool flag = false;
			if (Event.current.type == EventType.MouseUp)
			{
				flag = true;
			}
			if (GUI.ScrollerRepeatButton(controlID, rect, leftButton))
			{
				value -= GUI.scrollStepSize * ((leftValue >= rightValue) ? -1f : 1f);
			}
			if (GUI.ScrollerRepeatButton(controlID, rect2, rightButton))
			{
				value += GUI.scrollStepSize * ((leftValue >= rightValue) ? -1f : 1f);
			}
			if (flag && Event.current.type == EventType.Used)
			{
				GUI.scrollControlID = 0;
			}
			if (leftValue < rightValue)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000C10C File Offset: 0x0000A30C
		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000C120 File Offset: 0x0000A320
		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000C134 File Offset: 0x0000A334
		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000C144 File Offset: 0x0000A344
		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000C154 File Offset: 0x0000A354
		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000C164 File Offset: 0x0000A364
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000C174 File Offset: 0x0000A374
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.beginGroupHash, FocusType.Passive);
			if (content != GUIContent.none || style != GUIStyle.none)
			{
				EventType type = Event.current.type;
				if (type != EventType.Repaint)
				{
					if (position.Contains(Event.current.mousePosition))
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000C200 File Offset: 0x0000A400
		public static void EndGroup()
		{
			GUIClip.Pop();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000C208 File Offset: 0x0000A408
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000C240 File Offset: 0x0000A440
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000C278 File Offset: 0x0000A478
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000C29C File Offset: 0x0000A49C
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, null);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.scrollviewHash, FocusType.Passive);
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUIUtility.GetStateObject(typeof(GUI.ScrollViewState), controlID);
			if (scrollViewState.apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.s_ScrollViewStates.Push(scrollViewState);
			Rect screenRect = new Rect(position);
			EventType type = Event.current.type;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					bool flag = alwaysShowVertical;
					bool flag2 = alwaysShowHorizontal;
					if (flag2 || viewRect.width > screenRect.width)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
					if (flag || viewRect.height > screenRect.height)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						screenRect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag = true;
						if (!flag2 && viewRect.width > screenRect.width)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag2 = true;
						}
					}
					if (Event.current.type == EventType.Repaint && background != GUIStyle.none)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
					}
					if (flag2 && horizontalScrollbar != GUIStyle.none)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, screenRect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, screenRect.width, 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						if (horizontalScrollbar != GUIStyle.none)
						{
							scrollPosition.x = 0f;
						}
						else
						{
							scrollPosition.x = Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f));
						}
					}
					if (flag && verticalScrollbar != GUIStyle.none)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(screenRect.xMax + (float)verticalScrollbar.margin.left, screenRect.y, verticalScrollbar.fixedWidth, screenRect.height), scrollPosition.y, screenRect.height, 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						if (verticalScrollbar != GUIStyle.none)
						{
							scrollPosition.y = 0f;
						}
						else
						{
							scrollPosition.y = Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f));
						}
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(screenRect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000C770 File Offset: 0x0000A970
		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000C778 File Offset: 0x0000A978
		public static void EndScrollView(bool handleScrollWheel)
		{
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
			GUI.s_ScrollViewStates.Pop();
			if (handleScrollWheel && Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
				scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
				scrollViewState.apply = true;
				Event.current.Use();
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000C88C File Offset: 0x0000AA8C
		internal static GUI.ScrollViewState GetTopScrollView()
		{
			if (GUI.s_ScrollViewStates.Count != 0)
			{
				return (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			}
			return null;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public static void ScrollTo(Rect position)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			return topScrollView != null && topScrollView.ScrollTowards(position, maxDelta);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000C914 File Offset: 0x0000AB14
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000C934 File Offset: 0x0000AB34
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000C950 File Offset: 0x0000AB50
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000C968 File Offset: 0x0000AB68
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000C980 File Offset: 0x0000AB80
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000C994 File Offset: 0x0000AB94
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000CA34 File Offset: 0x0000AC34
		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			return GUI.INTERNAL_CALL_DoModalWindow(id, ref clientRect, func, content, style, skin);
		}

		// Token: 0x06000584 RID: 1412
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_DoModalWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin);

		// Token: 0x06000585 RID: 1413 RVA: 0x0000CA44 File Offset: 0x0000AC44
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			if (Event.current.type == EventType.Layout)
			{
				if (forceRect != 0)
				{
					GUILayoutOption[] options = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, options);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			GUI.skin = _skin;
			func(id);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			return GUI.INTERNAL_CALL_DoWindow(id, ref clientRect, func, title, style, skin, forceRectOnLayout);
		}

		// Token: 0x06000587 RID: 1415
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_DoWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout);

		// Token: 0x06000588 RID: 1416 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public static void DragWindow(Rect position)
		{
			GUI.INTERNAL_CALL_DragWindow(ref position);
		}

		// Token: 0x06000589 RID: 1417
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DragWindow(ref Rect position);

		// Token: 0x0600058A RID: 1418 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x0600058B RID: 1419
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void BringWindowToFront(int windowID);

		// Token: 0x0600058C RID: 1420
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void BringWindowToBack(int windowID);

		// Token: 0x0600058D RID: 1421
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void FocusWindow(int windowID);

		// Token: 0x0600058E RID: 1422
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void UnfocusWindow();

		// Token: 0x0600058F RID: 1423 RVA: 0x0000CB10 File Offset: 0x0000AD10
		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x06000590 RID: 1424
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_BeginWindows();

		// Token: 0x06000591 RID: 1425 RVA: 0x0000CB70 File Offset: 0x0000AD70
		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x06000592 RID: 1426
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_EndWindows();

		// Token: 0x04000100 RID: 256
		private static float scrollStepSize = 10f;

		// Token: 0x04000101 RID: 257
		private static int scrollControlID;

		// Token: 0x04000102 RID: 258
		private static int hotTextField = -1;

		// Token: 0x04000103 RID: 259
		private static GUISkin s_Skin;

		// Token: 0x04000104 RID: 260
		internal static Rect s_ToolTipRect;

		// Token: 0x04000105 RID: 261
		private static int boxHash = "Box".GetHashCode();

		// Token: 0x04000106 RID: 262
		private static int repeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x04000107 RID: 263
		private static int toggleHash = "Toggle".GetHashCode();

		// Token: 0x04000108 RID: 264
		private static int buttonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x04000109 RID: 265
		private static int sliderHash = "Slider".GetHashCode();

		// Token: 0x0400010A RID: 266
		private static int beginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x0400010B RID: 267
		private static int scrollviewHash = "scrollView".GetHashCode();

		// Token: 0x0400010C RID: 268
		private static GenericStack s_ScrollViewStates = new GenericStack();

		// Token: 0x02000071 RID: 113
		internal sealed class ScrollViewState
		{
			// Token: 0x06000594 RID: 1428 RVA: 0x0000CBCC File Offset: 0x0000ADCC
			internal void ScrollTo(Rect position)
			{
				this.ScrollTowards(position, float.PositiveInfinity);
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x0000CBDC File Offset: 0x0000ADDC
			internal bool ScrollTowards(Rect position, float maxDelta)
			{
				Vector2 b = this.ScrollNeeded(position);
				if (b.sqrMagnitude < 0.0001f)
				{
					return false;
				}
				if (maxDelta == 0f)
				{
					return true;
				}
				if (b.magnitude > maxDelta)
				{
					b = b.normalized * maxDelta;
				}
				this.scrollPosition += b;
				this.apply = true;
				return true;
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0000CC48 File Offset: 0x0000AE48
			internal Vector2 ScrollNeeded(Rect position)
			{
				Rect rect = this.visibleRect;
				rect.x += this.scrollPosition.x;
				rect.y += this.scrollPosition.y;
				float num = position.width - this.visibleRect.width;
				if (num > 0f)
				{
					position.width -= num;
					position.x += num * 0.5f;
				}
				num = position.height - this.visibleRect.height;
				if (num > 0f)
				{
					position.height -= num;
					position.y += num * 0.5f;
				}
				Vector2 zero = Vector2.zero;
				if (position.xMax > rect.xMax)
				{
					zero.x += position.xMax - rect.xMax;
				}
				else if (position.xMin < rect.xMin)
				{
					zero.x -= rect.xMin - position.xMin;
				}
				if (position.yMax > rect.yMax)
				{
					zero.y += position.yMax - rect.yMax;
				}
				else if (position.yMin < rect.yMin)
				{
					zero.y -= rect.yMin - position.yMin;
				}
				Rect rect2 = this.viewRect;
				rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
				rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
				zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
				zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
				return zero;
			}

			// Token: 0x0400010F RID: 271
			public Rect position;

			// Token: 0x04000110 RID: 272
			public Rect visibleRect;

			// Token: 0x04000111 RID: 273
			public Rect viewRect;

			// Token: 0x04000112 RID: 274
			public Vector2 scrollPosition;

			// Token: 0x04000113 RID: 275
			public bool apply;

			// Token: 0x04000114 RID: 276
			public bool hasScrollTo;
		}

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x06000598 RID: 1432
		public delegate void WindowFunction(int id);
	}
}
