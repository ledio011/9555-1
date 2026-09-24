using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200011B RID: 283
	[StructLayout(0)]
	public sealed class TextGenerator : IDisposable
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x0001924C File Offset: 0x0001744C
		public TextGenerator() : this(50)
		{
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00019258 File Offset: 0x00017458
		public TextGenerator(int initialCapacity)
		{
			this.m_Verts = new List<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new List<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new List<UILineInfo>(20);
			this.Init();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00019294 File Offset: 0x00017494
		void IDisposable.Dispose()
		{
			this.Dispose_cpp();
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001929C File Offset: 0x0001749C
		~TextGenerator()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000192CC File Offset: 0x000174CC
		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			if (settings.font != null && settings.font.dynamic)
			{
				return settings;
			}
			if (settings.fontSize != 0 || settings.fontStyle != FontStyle.Normal)
			{
				Debug.LogWarning("Font size and style overrides are only supported for dynamic fonts.");
				settings.fontSize = 0;
				settings.fontStyle = FontStyle.Normal;
			}
			if (settings.resizeTextForBestFit)
			{
				Debug.LogWarning("BestFit is only suppoerted for dynamic fonts.");
				settings.resizeTextForBestFit = false;
			}
			return settings;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00019350 File Offset: 0x00017550
		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001935C File Offset: 0x0001755C
		public void GetCharacters(List<UICharInfo> characters)
		{
			this.GetCharactersInternal(characters);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00019368 File Offset: 0x00017568
		public void GetLines(List<UILineInfo> lines)
		{
			this.GetLinesInternal(lines);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00019374 File Offset: 0x00017574
		public void GetVertices(List<UIVertex> vertices)
		{
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00019380 File Offset: 0x00017580
		public float GetPreferredWidth(string str, TextGenerationSettings settings)
		{
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.width;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000193BC File Offset: 0x000175BC
		public float GetPreferredHeight(string str, TextGenerationSettings settings)
		{
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.height;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000193F0 File Offset: 0x000175F0
		public bool Populate(string str, TextGenerationSettings settings)
		{
			if (this.m_HasGenerated && str == this.m_LastString && settings.Equals(this.m_LastSettings))
			{
				return this.m_LastValid;
			}
			return this.PopulateAlways(str, settings);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00019430 File Offset: 0x00017630
		private bool PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings textGenerationSettings = this.ValidatedSettings(settings);
			this.m_LastValid = this.Populate_Internal(str, textGenerationSettings.font, textGenerationSettings.color, textGenerationSettings.fontSize, textGenerationSettings.scaleFactor, textGenerationSettings.lineSpacing, textGenerationSettings.fontStyle, textGenerationSettings.richText, textGenerationSettings.resizeTextForBestFit, textGenerationSettings.resizeTextMinSize, textGenerationSettings.resizeTextMaxSize, textGenerationSettings.verticalOverflow, textGenerationSettings.horizontalOverflow, textGenerationSettings.updateBounds, textGenerationSettings.textAnchor, textGenerationSettings.generationExtents, textGenerationSettings.pivot, textGenerationSettings.generateOutOfBounds);
			return this.m_LastValid;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000194FC File Offset: 0x000176FC
		public IList<UIVertex> verts
		{
			get
			{
				if (!this.m_CachedVerts)
				{
					this.GetVertices(this.m_Verts);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00019524 File Offset: 0x00017724
		public IList<UICharInfo> characters
		{
			get
			{
				if (!this.m_CachedCharacters)
				{
					this.GetCharacters(this.m_Characters);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001954C File Offset: 0x0001774C
		public IList<UILineInfo> lines
		{
			get
			{
				if (!this.m_CachedLines)
				{
					this.GetLines(this.m_Lines);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		// Token: 0x06000A55 RID: 2645
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000A56 RID: 2646
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Dispose_cpp();

		// Token: 0x06000A57 RID: 2647 RVA: 0x00019574 File Offset: 0x00017774
		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, VerticalWrapMode verticalOverFlow, HorizontalWrapMode horizontalOverflow, bool updateBounds, TextAnchor anchor, Vector2 extents, Vector2 pivot, bool generateOutOfBounds)
		{
			return this.Populate_Internal_cpp(str, font, color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, (int)verticalOverFlow, (int)horizontalOverflow, updateBounds, anchor, extents.x, extents.y, pivot.x, pivot.y, generateOutOfBounds);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000195C0 File Offset: 0x000177C0
		internal bool Populate_Internal_cpp(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds)
		{
			return TextGenerator.INTERNAL_CALL_Populate_Internal_cpp(this, str, font, ref color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, verticalOverFlow, horizontalOverflow, updateBounds, anchor, extentsX, extentsY, pivotX, pivotY, generateOutOfBounds);
		}

		// Token: 0x06000A59 RID: 2649
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Populate_Internal_cpp(TextGenerator self, string str, Font font, ref Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds);

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x000195FC File Offset: 0x000177FC
		public Rect rectExtents
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rectExtents(out result);
				return result;
			}
		}

		// Token: 0x06000A5B RID: 2651
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rectExtents(out Rect value);

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A5C RID: 2652
		public extern int vertexCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000A5D RID: 2653
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void GetVerticesInternal(object vertices);

		// Token: 0x06000A5E RID: 2654
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern UIVertex[] GetVerticesArray();

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A5F RID: 2655
		public extern int characterCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00019614 File Offset: 0x00017814
		public int characterCountVisible
		{
			get
			{
				return (!string.IsNullOrEmpty(this.m_LastString)) ? Mathf.Min(this.m_LastString.Length, Mathf.Max(0, (this.vertexCount - 4) / 4)) : 0;
			}
		}

		// Token: 0x06000A61 RID: 2657
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void GetCharactersInternal(object characters);

		// Token: 0x06000A62 RID: 2658
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern UICharInfo[] GetCharactersArray();

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A63 RID: 2659
		public extern int lineCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000A64 RID: 2660
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void GetLinesInternal(object lines);

		// Token: 0x06000A65 RID: 2661
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern UILineInfo[] GetLinesArray();

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A66 RID: 2662
		public extern int fontSizeUsedForBestFit { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x04000472 RID: 1138
		internal IntPtr m_Ptr;

		// Token: 0x04000473 RID: 1139
		private string m_LastString;

		// Token: 0x04000474 RID: 1140
		private TextGenerationSettings m_LastSettings;

		// Token: 0x04000475 RID: 1141
		private bool m_HasGenerated;

		// Token: 0x04000476 RID: 1142
		private bool m_LastValid;

		// Token: 0x04000477 RID: 1143
		private readonly List<UIVertex> m_Verts;

		// Token: 0x04000478 RID: 1144
		private readonly List<UICharInfo> m_Characters;

		// Token: 0x04000479 RID: 1145
		private readonly List<UILineInfo> m_Lines;

		// Token: 0x0400047A RID: 1146
		private bool m_CachedVerts;

		// Token: 0x0400047B RID: 1147
		private bool m_CachedCharacters;

		// Token: 0x0400047C RID: 1148
		private bool m_CachedLines;
	}
}
