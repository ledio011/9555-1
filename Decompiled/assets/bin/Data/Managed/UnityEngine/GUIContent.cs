using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIContent
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		public GUIContent()
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000CF18 File Offset: 0x0000B118
		public GUIContent(string text)
		{
			this.m_Text = text;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000CF40 File Offset: 0x0000B140
		public GUIContent(Texture image)
		{
			this.m_Image = image;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000CF68 File Offset: 0x0000B168
		public GUIContent(GUIContent src)
		{
			this.m_Text = src.m_Text;
			this.m_Image = src.m_Image;
			this.m_Tooltip = src.m_Tooltip;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000D004 File Offset: 0x0000B204
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000D00C File Offset: 0x0000B20C
		internal int hash
		{
			get
			{
				int result = 0;
				if (this.m_Text != null && this.m_Text != string.Empty)
				{
					result = this.m_Text.GetHashCode() * 37;
				}
				return result;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000D04C File Offset: 0x0000B24C
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			return GUIContent.s_Text;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000D060 File Offset: 0x0000B260
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			return GUIContent.s_Image;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000D074 File Offset: 0x0000B274
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000D094 File Offset: 0x0000B294
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x04000115 RID: 277
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x04000116 RID: 278
		[SerializeField]
		private Texture m_Image;

		// Token: 0x04000117 RID: 279
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x04000118 RID: 280
		public static GUIContent none = new GUIContent(string.Empty);

		// Token: 0x04000119 RID: 281
		private static GUIContent s_Text = new GUIContent();

		// Token: 0x0400011A RID: 282
		private static GUIContent s_Image = new GUIContent();

		// Token: 0x0400011B RID: 283
		private static GUIContent s_TextImage = new GUIContent();
	}
}
