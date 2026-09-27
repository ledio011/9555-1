using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000065 RID: 101
	public sealed class Font : Object
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00009DF0 File Offset: 0x00007FF0
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00009E00 File Offset: 0x00008000
		public Font(string name)
		{
			Font.Internal_CreateFont(this, name);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000440 RID: 1088 RVA: 0x00009E10 File Offset: 0x00008010
		// (remove) Token: 0x06000441 RID: 1089 RVA: 0x00009E28 File Offset: 0x00008028
		public static event Action<Font> textureRebuilt;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000442 RID: 1090 RVA: 0x00009E40 File Offset: 0x00008040
		// (remove) Token: 0x06000443 RID: 1091 RVA: 0x00009E5C File Offset: 0x0000805C
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x06000444 RID: 1092
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateFont([Writable] Font _font, string name);

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000445 RID: 1093
		// (set) Token: 0x06000446 RID: 1094
		public extern Material material { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000447 RID: 1095
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool HasCharacter(char c);

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000448 RID: 1096
		// (set) Token: 0x06000449 RID: 1097
		public extern string[] fontNames { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600044A RID: 1098
		// (set) Token: 0x0600044B RID: 1099
		public extern CharacterInfo[] characterInfo { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600044C RID: 1100
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RequestCharactersInTexture(string characters, [UnityEngine.Internal.DefaultValue("0")] int size, [UnityEngine.Internal.DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x0600044D RID: 1101 RVA: 0x00009E78 File Offset: 0x00008078
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			FontStyle style = FontStyle.Normal;
			this.RequestCharactersInTexture(characters, size, style);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00009E90 File Offset: 0x00008090
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			FontStyle style = FontStyle.Normal;
			int size = 0;
			this.RequestCharactersInTexture(characters, size, style);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00009EAC File Offset: 0x000080AC
		private static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action(font);
			}
			if (font.m_FontTextureRebuildCallback != null)
			{
				font.m_FontTextureRebuildCallback();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00009EE4 File Offset: 0x000080E4
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00009EEC File Offset: 0x000080EC
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00009EF8 File Offset: 0x000080F8
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x06000453 RID: 1107
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool GetCharacterInfo(char ch, out CharacterInfo info, [UnityEngine.Internal.DefaultValue("0")] int size, [UnityEngine.Internal.DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x06000454 RID: 1108 RVA: 0x00009F04 File Offset: 0x00008104
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			FontStyle style = FontStyle.Normal;
			return this.GetCharacterInfo(ch, out info, size, style);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00009F20 File Offset: 0x00008120
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			FontStyle style = FontStyle.Normal;
			int size = 0;
			return this.GetCharacterInfo(ch, out info, size, style);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000456 RID: 1110
		public extern bool dynamic { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000457 RID: 1111
		public extern int fontSize { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x06000459 RID: 1113
		[EditorBrowsable(EditorBrowsableState.Never)]
		public delegate void FontTextureRebuildCallback();
	}
}
