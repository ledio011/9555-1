using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200008A RID: 138
public static class NGUIText
{
	// Token: 0x06000328 RID: 808 RVA: 0x0001639C File Offset: 0x0001459C
	public static void Update()
	{
		NGUIText.Update(true);
	}

	// Token: 0x06000329 RID: 809 RVA: 0x000163A4 File Offset: 0x000145A4
	public static void Update(bool request)
	{
		NGUIText.finalSize = Mathf.RoundToInt((float)NGUIText.fontSize / NGUIText.pixelDensity);
		NGUIText.finalSpacingX = NGUIText.spacingX * NGUIText.fontScale;
		NGUIText.finalLineHeight = ((float)NGUIText.fontSize + NGUIText.spacingY) * NGUIText.fontScale;
		NGUIText.useSymbols = (NGUIText.bitmapFont != null && NGUIText.bitmapFont.hasSymbols && NGUIText.encoding && NGUIText.symbolStyle != NGUIText.SymbolStyle.None);
		if (NGUIText.dynamicFont != null && request)
		{
			NGUIText.dynamicFont.RequestCharactersInTexture(")_-", NGUIText.finalSize, NGUIText.fontStyle);
			if (!NGUIText.dynamicFont.GetCharacterInfo(')', ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
			{
				NGUIText.dynamicFont.RequestCharactersInTexture("A", NGUIText.finalSize, NGUIText.fontStyle);
				if (!NGUIText.dynamicFont.GetCharacterInfo('A', ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
				{
					NGUIText.baseline = 0f;
					return;
				}
			}
			float yMax = NGUIText.mTempChar.vert.yMax;
			float yMin = NGUIText.mTempChar.vert.yMin;
			NGUIText.baseline = Mathf.Round(yMax + ((float)NGUIText.finalSize - yMax + yMin) * 0.5f);
		}
	}

	// Token: 0x0600032A RID: 810 RVA: 0x000164FC File Offset: 0x000146FC
	public static void Prepare(string text)
	{
		if (NGUIText.dynamicFont != null)
		{
			NGUIText.dynamicFont.RequestCharactersInTexture(text, NGUIText.finalSize, NGUIText.fontStyle);
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00016524 File Offset: 0x00014724
	public static BMSymbol GetSymbol(string text, int index, int textLength)
	{
		return (!(NGUIText.bitmapFont != null)) ? null : NGUIText.bitmapFont.MatchSymbol(text, index, textLength);
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001654C File Offset: 0x0001474C
	public static float GetGlyphWidth(int ch, int prev)
	{
		if (NGUIText.bitmapFont != null)
		{
			BMGlyph bmglyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
			if (bmglyph != null)
			{
				return NGUIText.fontScale * (float)((prev == 0) ? bmglyph.advance : (bmglyph.advance + bmglyph.GetKerning(prev)));
			}
		}
		else if (NGUIText.dynamicFont != null && NGUIText.dynamicFont.GetCharacterInfo((char)ch, ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
		{
			return NGUIText.mTempChar.width * NGUIText.fontScale * NGUIText.pixelDensity;
		}
		return 0f;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x000165F8 File Offset: 0x000147F8
	public static NGUIText.GlyphInfo GetGlyph(int ch, int prev)
	{
		if (NGUIText.bitmapFont != null)
		{
			BMGlyph bmglyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
			if (bmglyph != null)
			{
				int num = (prev == 0) ? 0 : bmglyph.GetKerning(prev);
				NGUIText.glyph.v0.x = (float)((prev == 0) ? bmglyph.offsetX : (bmglyph.offsetX + num));
				NGUIText.glyph.v1.y = (float)(-(float)bmglyph.offsetY);
				NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + (float)bmglyph.width;
				NGUIText.glyph.v0.y = NGUIText.glyph.v1.y - (float)bmglyph.height;
				NGUIText.glyph.u0.x = (float)bmglyph.x;
				NGUIText.glyph.u0.y = (float)(bmglyph.y + bmglyph.height);
				NGUIText.glyph.u1.x = (float)(bmglyph.x + bmglyph.width);
				NGUIText.glyph.u1.y = (float)bmglyph.y;
				NGUIText.glyph.advance = (float)(bmglyph.advance + num);
				NGUIText.glyph.channel = bmglyph.channel;
				NGUIText.glyph.rotatedUVs = false;
				if (NGUIText.fontScale != 1f)
				{
					NGUIText.glyph.v0 *= NGUIText.fontScale;
					NGUIText.glyph.v1 *= NGUIText.fontScale;
					NGUIText.glyph.advance *= NGUIText.fontScale;
				}
				return NGUIText.glyph;
			}
		}
		else if (NGUIText.dynamicFont != null && NGUIText.dynamicFont.GetCharacterInfo((char)ch, ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
		{
			NGUIText.glyph.v0.x = NGUIText.mTempChar.vert.xMin;
			NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + NGUIText.mTempChar.vert.width;
			NGUIText.glyph.v0.y = NGUIText.mTempChar.vert.yMax - NGUIText.baseline;
			NGUIText.glyph.v1.y = NGUIText.glyph.v0.y - NGUIText.mTempChar.vert.height;
			NGUIText.glyph.u0.x = NGUIText.mTempChar.uv.xMin;
			NGUIText.glyph.u0.y = NGUIText.mTempChar.uv.yMin;
			NGUIText.glyph.u1.x = NGUIText.mTempChar.uv.xMax;
			NGUIText.glyph.u1.y = NGUIText.mTempChar.uv.yMax;
			NGUIText.glyph.advance = NGUIText.mTempChar.width;
			NGUIText.glyph.channel = 0;
			NGUIText.glyph.rotatedUVs = NGUIText.mTempChar.flipped;
			float num2 = NGUIText.fontScale * NGUIText.pixelDensity;
			if (num2 != 1f)
			{
				NGUIText.glyph.v0 *= num2;
				NGUIText.glyph.v1 *= num2;
				NGUIText.glyph.advance *= num2;
			}
			return NGUIText.glyph;
		}
		return null;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001699C File Offset: 0x00014B9C
	public static Color ParseColor(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text.get_Chars(offset)) << 4 | NGUIMath.HexToDecimal(text.get_Chars(offset + 1));
		int num2 = NGUIMath.HexToDecimal(text.get_Chars(offset + 2)) << 4 | NGUIMath.HexToDecimal(text.get_Chars(offset + 3));
		int num3 = NGUIMath.HexToDecimal(text.get_Chars(offset + 4)) << 4 | NGUIMath.HexToDecimal(text.get_Chars(offset + 5));
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00016A20 File Offset: 0x00014C20
	public static string EncodeColor(Color c)
	{
		int num = 16777215 & NGUIMath.ColorToInt(c) >> 8;
		return NGUIMath.DecimalToHex(num);
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00016A44 File Offset: 0x00014C44
	public static bool ParseSymbol(string text, ref int index)
	{
		int num = 1;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		return NGUIText.ParseSymbol(text, ref index, null, false, ref num, ref flag, ref flag2, ref flag3, ref flag4);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00016A70 File Offset: 0x00014C70
	public static bool ParseSymbol(string text, ref int index, BetterList<Color> colors, bool premultiply, ref int sub, ref bool bold, ref bool italic, ref bool underline, ref bool strike)
	{
		int length = text.Length;
		if (index + 3 > length || text.get_Chars(index) != '[')
		{
			return false;
		}
		int num;
		if (text.get_Chars(index + 2) == ']')
		{
			if (text.get_Chars(index + 1) == '-')
			{
				if (colors != null && colors.size > 1)
				{
					colors.RemoveAt(colors.size - 1);
				}
				index += 3;
				return true;
			}
			string text2 = text.Substring(index, 3);
			string text3 = text2;
			if (text3 != null)
			{
				if (NGUIText.<>f__switch$map1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
					dictionary.Add("[b]", 0);
					dictionary.Add("[i]", 1);
					dictionary.Add("[u]", 2);
					dictionary.Add("[s]", 3);
					NGUIText.<>f__switch$map1 = dictionary;
				}
				if (NGUIText.<>f__switch$map1.TryGetValue(text3, ref num))
				{
					switch (num)
					{
					case 0:
						bold = true;
						index += 3;
						return true;
					case 1:
						italic = true;
						index += 3;
						return true;
					case 2:
						underline = true;
						index += 3;
						return true;
					case 3:
						strike = true;
						index += 3;
						return true;
					}
				}
			}
		}
		if (index + 4 > length)
		{
			return false;
		}
		if (text.get_Chars(index + 3) == ']')
		{
			string text4 = text.Substring(index, 4);
			string text3 = text4;
			if (text3 != null)
			{
				if (NGUIText.<>f__switch$map2 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
					dictionary.Add("[/b]", 0);
					dictionary.Add("[/i]", 1);
					dictionary.Add("[/u]", 2);
					dictionary.Add("[/s]", 3);
					NGUIText.<>f__switch$map2 = dictionary;
				}
				if (NGUIText.<>f__switch$map2.TryGetValue(text3, ref num))
				{
					switch (num)
					{
					case 0:
						bold = false;
						index += 4;
						return true;
					case 1:
						italic = false;
						index += 4;
						return true;
					case 2:
						underline = false;
						index += 4;
						return true;
					case 3:
						strike = false;
						index += 4;
						return true;
					}
				}
			}
		}
		if (index + 5 > length)
		{
			return false;
		}
		if (text.get_Chars(index + 4) == ']')
		{
			string text5 = text.Substring(index, 5);
			string text3 = text5;
			if (text3 != null)
			{
				if (NGUIText.<>f__switch$map3 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
					dictionary.Add("[sub]", 0);
					dictionary.Add("[sup]", 1);
					NGUIText.<>f__switch$map3 = dictionary;
				}
				if (NGUIText.<>f__switch$map3.TryGetValue(text3, ref num))
				{
					if (num == 0)
					{
						sub = 1;
						index += 5;
						return true;
					}
					if (num == 1)
					{
						sub = 2;
						index += 5;
						return true;
					}
				}
			}
		}
		if (index + 6 > length)
		{
			return false;
		}
		if (text.get_Chars(index + 5) == ']')
		{
			string text6 = text.Substring(index, 6);
			string text3 = text6;
			if (text3 != null)
			{
				if (NGUIText.<>f__switch$map4 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("[/sub]", 0);
					dictionary.Add("[/sup]", 1);
					dictionary.Add("[/url]", 2);
					NGUIText.<>f__switch$map4 = dictionary;
				}
				if (NGUIText.<>f__switch$map4.TryGetValue(text3, ref num))
				{
					switch (num)
					{
					case 0:
						sub = 0;
						index += 6;
						return true;
					case 1:
						sub = 0;
						index += 6;
						return true;
					case 2:
						index += 6;
						return true;
					}
				}
			}
		}
		if (text.get_Chars(index + 1) == 'u' && text.get_Chars(index + 2) == 'r' && text.get_Chars(index + 3) == 'l' && text.get_Chars(index + 4) == '=')
		{
			int num2 = text.IndexOf(']', index + 4);
			if (num2 != -1)
			{
				index = num2 + 1;
				return true;
			}
		}
		if (index + 8 > length)
		{
			return false;
		}
		if (text.get_Chars(index + 7) != ']')
		{
			return false;
		}
		Color color = NGUIText.ParseColor(text, index + 1);
		if (NGUIText.EncodeColor(color) != text.Substring(index + 1, 6).ToUpper())
		{
			return false;
		}
		if (colors != null)
		{
			color.a = colors[colors.size - 1].a;
			if (premultiply && color.a != 1f)
			{
				color = Color.Lerp(NGUIText.mInvisible, color, color.a);
			}
			colors.Add(color);
		}
		index += 8;
		return true;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00016EF8 File Offset: 0x000150F8
	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				char c = text.get_Chars(i);
				if (c == '[')
				{
					int num = 0;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num2 = i;
					if (NGUIText.ParseSymbol(text, ref num2, null, false, ref num, ref flag, ref flag2, ref flag3, ref flag4))
					{
						text = text.Remove(i, num2 - i);
						length = text.Length;
						continue;
					}
				}
				i++;
			}
		}
		return text;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00016F78 File Offset: 0x00015178
	public static void Align(BetterList<Vector3> verts, int indexOffset, float printedWidth)
	{
		switch (NGUIText.alignment)
		{
		case NGUIText.Alignment.Center:
		{
			float num = ((float)NGUIText.rectWidth - printedWidth) * 0.5f;
			if (num < 0f)
			{
				return;
			}
			int num2 = Mathf.RoundToInt((float)NGUIText.rectWidth - printedWidth);
			int num3 = Mathf.RoundToInt((float)NGUIText.rectWidth);
			bool flag = (num2 & 1) == 1;
			bool flag2 = (num3 & 1) == 1;
			if ((flag && !flag2) || (!flag && flag2))
			{
				num += 0.5f * NGUIText.fontScale;
			}
			for (int i = indexOffset; i < verts.size; i++)
			{
				Vector3[] buffer = verts.buffer;
				int num4 = i;
				buffer[num4].x = buffer[num4].x + num;
			}
			break;
		}
		case NGUIText.Alignment.Right:
		{
			float num5 = (float)NGUIText.rectWidth - printedWidth;
			if (num5 < 0f)
			{
				return;
			}
			for (int j = indexOffset; j < verts.size; j++)
			{
				Vector3[] buffer2 = verts.buffer;
				int num6 = j;
				buffer2[num6].x = buffer2[num6].x + num5;
			}
			break;
		}
		case NGUIText.Alignment.Justified:
		{
			if (printedWidth < (float)NGUIText.rectWidth * 0.65f)
			{
				return;
			}
			float num7 = ((float)NGUIText.rectWidth - printedWidth) * 0.5f;
			if (num7 < 1f)
			{
				return;
			}
			int num8 = (verts.size - indexOffset) / 4;
			if (num8 < 1)
			{
				return;
			}
			float num9 = 1f / (float)(num8 - 1);
			float num10 = (float)NGUIText.rectWidth / printedWidth;
			int k = indexOffset + 4;
			int num11 = 1;
			while (k < verts.size)
			{
				float num12 = verts.buffer[k].x;
				float num13 = verts.buffer[k + 2].x;
				float num14 = num13 - num12;
				float num15 = num12 * num10;
				float num16 = num15 + num14;
				float num17 = num13 * num10;
				float num18 = num17 - num14;
				float num19 = (float)num11 * num9;
				num12 = Mathf.Lerp(num15, num18, num19);
				num13 = Mathf.Lerp(num16, num17, num19);
				num12 = Mathf.Round(num12);
				num13 = Mathf.Round(num13);
				verts.buffer[k++].x = num12;
				verts.buffer[k++].x = num12;
				verts.buffer[k++].x = num13;
				verts.buffer[k++].x = num13;
				num11++;
			}
			break;
		}
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00017204 File Offset: 0x00015404
	public static int GetClosestCharacter(BetterList<Vector3> verts, Vector2 pos)
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		int result = 0;
		for (int i = 0; i < verts.size; i++)
		{
			float num3 = Mathf.Abs(pos.y - verts[i].y);
			if (num3 <= num2)
			{
				float num4 = Mathf.Abs(pos.x - verts[i].x);
				if (num3 < num2)
				{
					num2 = num3;
					num = num4;
					result = i;
				}
				else if (num4 < num)
				{
					num = num4;
					result = i;
				}
			}
		}
		return result;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x000172A4 File Offset: 0x000154A4
	public static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s.get_Chars(num) == ' ')
		{
			s.set_Chars(num, '\n');
		}
		else
		{
			s.Append('\n');
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x000172EC File Offset: 0x000154EC
	private static void ReplaceSpaceWithNewline(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s.get_Chars(num) == ' ')
		{
			s.set_Chars(num, '\n');
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00017324 File Offset: 0x00015524
	public static Vector2 CalculatePrintedSize(string text)
	{
		Vector2 zero = Vector2.zero;
		if (!string.IsNullOrEmpty(text))
		{
			if (NGUIText.encoding)
			{
				text = NGUIText.StripSymbols(text);
			}
			NGUIText.Prepare(text);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int length = text.Length;
			int prev = 0;
			for (int i = 0; i < length; i++)
			{
				int num4 = (int)text.get_Chars(i);
				if (num4 == 10)
				{
					if (num > num3)
					{
						num3 = num;
					}
					num = 0f;
					num2 += NGUIText.finalLineHeight;
				}
				else if (num4 >= 32)
				{
					BMSymbol bmsymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
					if (bmsymbol == null)
					{
						float num5 = NGUIText.GetGlyphWidth(num4, prev);
						if (num5 != 0f)
						{
							num5 += NGUIText.finalSpacingX;
							if (Mathf.RoundToInt(num + num5) > NGUIText.rectWidth)
							{
								if (num > num3)
								{
									num3 = num - NGUIText.finalSpacingX;
								}
								num = num5;
								num2 += NGUIText.finalLineHeight;
							}
							else
							{
								num += num5;
							}
							prev = num4;
						}
					}
					else
					{
						float num6 = NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale;
						if (Mathf.RoundToInt(num + num6) > NGUIText.rectWidth)
						{
							if (num > num3)
							{
								num3 = num - NGUIText.finalSpacingX;
							}
							num = num6;
							num2 += NGUIText.finalLineHeight;
						}
						else
						{
							num += num6;
						}
						i += bmsymbol.sequence.Length - 1;
						prev = 0;
					}
				}
			}
			zero.x = ((num <= num3) ? num3 : (num - NGUIText.finalSpacingX));
			zero.y = num2 + NGUIText.finalLineHeight;
		}
		return zero;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x000174E0 File Offset: 0x000156E0
	public static int CalculateOffsetToFit(string text)
	{
		if (string.IsNullOrEmpty(text) || NGUIText.rectWidth < 1)
		{
			return 0;
		}
		NGUIText.Prepare(text);
		int length = text.Length;
		int prev = 0;
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			BMSymbol bmsymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
			if (bmsymbol == null)
			{
				int num = (int)text.get_Chars(i);
				float glyphWidth = NGUIText.GetGlyphWidth(num, prev);
				if (glyphWidth != 0f)
				{
					NGUIText.mSizes.Add(NGUIText.finalSpacingX + glyphWidth);
				}
				prev = num;
			}
			else
			{
				NGUIText.mSizes.Add(NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale);
				int j = 0;
				int num2 = bmsymbol.sequence.Length - 1;
				while (j < num2)
				{
					NGUIText.mSizes.Add(0f);
					j++;
				}
				i += bmsymbol.sequence.Length - 1;
				prev = 0;
			}
			i++;
		}
		float num3 = (float)NGUIText.rectWidth;
		int num4 = NGUIText.mSizes.size;
		while (num4 > 0 && num3 > 0f)
		{
			num3 -= NGUIText.mSizes[--num4];
		}
		NGUIText.mSizes.Clear();
		if (num3 < 0f)
		{
			num4++;
		}
		return num4;
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00017650 File Offset: 0x00015850
	public static string GetEndOfLineThatFits(string text)
	{
		int length = text.Length;
		int num = NGUIText.CalculateOffsetToFit(text);
		return text.Substring(num, length - num);
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00017678 File Offset: 0x00015878
	public static bool WrapText(string text, out string finalText)
	{
		return NGUIText.WrapText(text, out finalText, false);
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00017684 File Offset: 0x00015884
	public static bool WrapText(string text, out string finalText, bool keepCharCount)
	{
		if (NGUIText.rectWidth < 1 || NGUIText.rectHeight < 1 || NGUIText.finalLineHeight < 1f)
		{
			finalText = string.Empty;
			return false;
		}
		float num = (NGUIText.maxLines <= 0) ? ((float)NGUIText.rectHeight) : Mathf.Min((float)NGUIText.rectHeight, NGUIText.finalLineHeight * (float)NGUIText.maxLines);
		int num2 = (NGUIText.maxLines <= 0) ? 1000000 : NGUIText.maxLines;
		num2 = Mathf.FloorToInt(Mathf.Min((float)num2, num / NGUIText.finalLineHeight) + 0.01f);
		if (num2 == 0)
		{
			finalText = string.Empty;
			return false;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		float num3 = (float)NGUIText.rectWidth;
		int num4 = 0;
		int i = 0;
		int num5 = 1;
		int num6 = 0;
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		while (i < length)
		{
			char c = text.get_Chars(i);
			if (c > '⿿')
			{
				flag3 = true;
			}
			if (c == '\n')
			{
				if (num5 == num2)
				{
					break;
				}
				num3 = (float)NGUIText.rectWidth;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				num6 = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bmsymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				float num7;
				if (bmsymbol == null)
				{
					float glyphWidth = NGUIText.GetGlyphWidth((int)c, num6);
					if (glyphWidth == 0f)
					{
						goto IL_396;
					}
					num7 = NGUIText.finalSpacingX + glyphWidth;
				}
				else
				{
					num7 = NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale;
				}
				num3 -= num7;
				if (c == ' ' && !flag3)
				{
					if (num6 == 32)
					{
						stringBuilder.Append(' ');
						num4 = i;
					}
					else if (num6 != 32 && num4 < i)
					{
						int num8 = i - num4 + 1;
						if (num5 == num2 && num3 <= 0f && i < length && text.get_Chars(i) <= ' ')
						{
							num8--;
						}
						stringBuilder.Append(text.Substring(num4, num8));
						flag = false;
						num4 = i + 1;
					}
				}
				if (Mathf.RoundToInt(num3) < 0)
				{
					if (flag || num5 == num2)
					{
						stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
						if (c != ' ' && !flag3)
						{
							flag2 = false;
						}
						if (num5++ == num2)
						{
							num4 = i;
							break;
						}
						if (keepCharCount)
						{
							NGUIText.ReplaceSpaceWithNewline(ref stringBuilder);
						}
						else
						{
							NGUIText.EndLine(ref stringBuilder);
						}
						flag = true;
						if (c == ' ')
						{
							num4 = i + 1;
							num3 = (float)NGUIText.rectWidth;
						}
						else
						{
							num4 = i;
							num3 = (float)NGUIText.rectWidth - num7;
						}
						num6 = 0;
					}
					else
					{
						flag = true;
						num3 = (float)NGUIText.rectWidth;
						i = num4 - 1;
						num6 = 0;
						if (num5++ == num2)
						{
							break;
						}
						if (keepCharCount)
						{
							NGUIText.ReplaceSpaceWithNewline(ref stringBuilder);
						}
						else
						{
							NGUIText.EndLine(ref stringBuilder);
						}
						goto IL_396;
					}
				}
				else
				{
					num6 = (int)c;
				}
				if (bmsymbol != null)
				{
					i += bmsymbol.length - 1;
					num6 = 0;
				}
			}
			IL_396:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		finalText = stringBuilder.ToString();
		return flag2 && (i == length || num5 <= Mathf.Min(NGUIText.maxLines, num2));
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00017A84 File Offset: 0x00015C84
	public static void Print(string text, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		int size = verts.size;
		NGUIText.Prepare(text);
		NGUIText.mColors.Add(Color.white);
		int prev = 0;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)NGUIText.finalSize;
		Color color = NGUIText.tint * NGUIText.gradientBottom;
		Color color2 = NGUIText.tint * NGUIText.gradientTop;
		Color32 color3 = NGUIText.tint;
		int length = text.Length;
		Rect rect = default(Rect);
		float num5 = 0f;
		float num6 = 0f;
		float num7 = num4 * NGUIText.pixelDensity;
		bool flag = false;
		int num8 = 0;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		if (NGUIText.bitmapFont != null)
		{
			rect = NGUIText.bitmapFont.uvRect;
			num5 = rect.width / (float)NGUIText.bitmapFont.texWidth;
			num6 = rect.height / (float)NGUIText.bitmapFont.texHeight;
		}
		for (int i = 0; i < length; i++)
		{
			int num9 = (int)text.get_Chars(i);
			float num10 = num;
			if (num9 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (NGUIText.alignment != NGUIText.Alignment.Left)
				{
					NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num9 < 32)
			{
				prev = num9;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i, NGUIText.mColors, NGUIText.premultiply, ref num8, ref flag2, ref flag3, ref flag4, ref flag5))
			{
				Color color4 = NGUIText.tint * NGUIText.mColors[NGUIText.mColors.size - 1];
				color3 = color4;
				if (NGUIText.gradient)
				{
					color = NGUIText.gradientBottom * color4;
					color2 = NGUIText.gradientTop * color4;
				}
				i--;
			}
			else
			{
				BMSymbol bmsymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				if (bmsymbol != null)
				{
					float num11 = num + (float)bmsymbol.offsetX * NGUIText.fontScale;
					float num12 = num11 + (float)bmsymbol.width * NGUIText.fontScale;
					float num13 = -(num2 + (float)bmsymbol.offsetY * NGUIText.fontScale);
					float num14 = num13 - (float)bmsymbol.height * NGUIText.fontScale;
					if (Mathf.RoundToInt(num + (float)bmsymbol.advance * NGUIText.fontScale) > NGUIText.rectWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
						{
							NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
							size = verts.size;
						}
						num11 -= num;
						num12 -= num;
						num14 -= NGUIText.finalLineHeight;
						num13 -= NGUIText.finalLineHeight;
						num = 0f;
						num2 += NGUIText.finalLineHeight;
					}
					verts.Add(new Vector3(num11, num14));
					verts.Add(new Vector3(num11, num13));
					verts.Add(new Vector3(num12, num13));
					verts.Add(new Vector3(num12, num14));
					num += NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale;
					i += bmsymbol.length - 1;
					prev = 0;
					if (uvs != null)
					{
						Rect uvRect = bmsymbol.uvRect;
						float xMin = uvRect.xMin;
						float yMin = uvRect.yMin;
						float xMax = uvRect.xMax;
						float yMax = uvRect.yMax;
						uvs.Add(new Vector2(xMin, yMin));
						uvs.Add(new Vector2(xMin, yMax));
						uvs.Add(new Vector2(xMax, yMax));
						uvs.Add(new Vector2(xMax, yMin));
					}
					if (cols != null)
					{
						if (NGUIText.symbolStyle == NGUIText.SymbolStyle.Colored)
						{
							for (int j = 0; j < 4; j++)
							{
								cols.Add(color3);
							}
						}
						else
						{
							Color32 item = Color.white;
							item.a = color3.a;
							for (int k = 0; k < 4; k++)
							{
								cols.Add(item);
							}
						}
					}
				}
				else
				{
					NGUIText.GlyphInfo glyphInfo = NGUIText.GetGlyph(num9, prev);
					if (glyphInfo != null)
					{
						prev = num9;
						if (num8 != 0)
						{
							NGUIText.GlyphInfo glyphInfo2 = glyphInfo;
							glyphInfo2.v0.x = glyphInfo2.v0.x * 0.75f;
							NGUIText.GlyphInfo glyphInfo3 = glyphInfo;
							glyphInfo3.v0.y = glyphInfo3.v0.y * 0.75f;
							NGUIText.GlyphInfo glyphInfo4 = glyphInfo;
							glyphInfo4.v1.x = glyphInfo4.v1.x * 0.75f;
							NGUIText.GlyphInfo glyphInfo5 = glyphInfo;
							glyphInfo5.v1.y = glyphInfo5.v1.y * 0.75f;
							if (num8 == 1)
							{
								NGUIText.GlyphInfo glyphInfo6 = glyphInfo;
								glyphInfo6.v0.y = glyphInfo6.v0.y - NGUIText.fontScale * (float)NGUIText.fontSize * 0.4f;
								NGUIText.GlyphInfo glyphInfo7 = glyphInfo;
								glyphInfo7.v1.y = glyphInfo7.v1.y - NGUIText.fontScale * (float)NGUIText.fontSize * 0.4f;
							}
							else
							{
								NGUIText.GlyphInfo glyphInfo8 = glyphInfo;
								glyphInfo8.v0.y = glyphInfo8.v0.y + NGUIText.fontScale * (float)NGUIText.fontSize * 0.05f;
								NGUIText.GlyphInfo glyphInfo9 = glyphInfo;
								glyphInfo9.v1.y = glyphInfo9.v1.y + NGUIText.fontScale * (float)NGUIText.fontSize * 0.05f;
							}
						}
						float y = glyphInfo.v0.y;
						float y2 = glyphInfo.v1.y;
						float num11 = glyphInfo.v0.x + num;
						float num14 = glyphInfo.v0.y - num2;
						float num12 = glyphInfo.v1.x + num;
						float num13 = glyphInfo.v1.y - num2;
						float num15 = glyphInfo.advance;
						if (NGUIText.finalSpacingX < 0f)
						{
							num15 += NGUIText.finalSpacingX;
						}
						if (Mathf.RoundToInt(num + num15) > NGUIText.rectWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
							{
								NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
								size = verts.size;
							}
							num11 -= num;
							num12 -= num;
							num14 -= NGUIText.finalLineHeight;
							num13 -= NGUIText.finalLineHeight;
							num = 0f;
							num2 += NGUIText.finalLineHeight;
							num10 = 0f;
						}
						if (num9 == 32)
						{
							if (flag4)
							{
								num9 = 95;
							}
							else if (flag5)
							{
								num9 = 45;
							}
						}
						num += ((num8 != 0) ? ((NGUIText.finalSpacingX + glyphInfo.advance) * 0.75f) : (NGUIText.finalSpacingX + glyphInfo.advance));
						if (num9 != 32)
						{
							if (uvs != null)
							{
								if (NGUIText.bitmapFont != null)
								{
									glyphInfo.u0.x = rect.xMin + num5 * glyphInfo.u0.x;
									glyphInfo.u1.x = rect.xMin + num5 * glyphInfo.u1.x;
									glyphInfo.u0.y = rect.yMax - num6 * glyphInfo.u0.y;
									glyphInfo.u1.y = rect.yMax - num6 * glyphInfo.u1.y;
								}
								int l = 0;
								int num16 = (!flag2) ? 1 : 4;
								while (l < num16)
								{
									if (glyphInfo.rotatedUVs)
									{
										uvs.Add(glyphInfo.u0);
										uvs.Add(new Vector2(glyphInfo.u1.x, glyphInfo.u0.y));
										uvs.Add(glyphInfo.u1);
										uvs.Add(new Vector2(glyphInfo.u0.x, glyphInfo.u1.y));
									}
									else
									{
										uvs.Add(glyphInfo.u0);
										uvs.Add(new Vector2(glyphInfo.u0.x, glyphInfo.u1.y));
										uvs.Add(glyphInfo.u1);
										uvs.Add(new Vector2(glyphInfo.u1.x, glyphInfo.u0.y));
									}
									l++;
								}
							}
							if (cols != null)
							{
								if (glyphInfo.channel == 0 || glyphInfo.channel == 15)
								{
									if (NGUIText.gradient)
									{
										float num17 = num7 + y / NGUIText.fontScale;
										float num18 = num7 + y2 / NGUIText.fontScale;
										num17 /= num7;
										num18 /= num7;
										NGUIText.s_c0 = Color.Lerp(color, color2, num17);
										NGUIText.s_c1 = Color.Lerp(color, color2, num18);
										int m = 0;
										int num19 = (!flag2) ? 1 : 4;
										while (m < num19)
										{
											cols.Add(NGUIText.s_c0);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c0);
											m++;
										}
									}
									else
									{
										int n = 0;
										int num20 = (!flag2) ? 4 : 16;
										while (n < num20)
										{
											cols.Add(color3);
											n++;
										}
									}
								}
								else
								{
									Color color5 = color3;
									color5 *= 0.49f;
									switch (glyphInfo.channel)
									{
									case 1:
										color5.b += 0.51f;
										break;
									case 2:
										color5.g += 0.51f;
										break;
									case 4:
										color5.r += 0.51f;
										break;
									case 8:
										color5.a += 0.51f;
										break;
									}
									Color32 item2 = color5;
									int num21 = 0;
									int num22 = (!flag2) ? 4 : 16;
									while (num21 < num22)
									{
										cols.Add(item2);
										num21++;
									}
								}
							}
							if (!flag2)
							{
								if (!flag3)
								{
									verts.Add(new Vector3(num11, num14));
									verts.Add(new Vector3(num11, num13));
									verts.Add(new Vector3(num12, num13));
									verts.Add(new Vector3(num12, num14));
								}
								else
								{
									float num23 = (float)NGUIText.fontSize * 0.1f * ((num13 - num14) / (float)NGUIText.fontSize);
									verts.Add(new Vector3(num11 - num23, num14));
									verts.Add(new Vector3(num11 + num23, num13));
									verts.Add(new Vector3(num12 + num23, num13));
									verts.Add(new Vector3(num12 - num23, num14));
								}
							}
							else
							{
								for (int num24 = 0; num24 < 4; num24++)
								{
									float num25 = NGUIText.mBoldOffset[num24 * 2];
									float num26 = NGUIText.mBoldOffset[num24 * 2 + 1];
									float num27 = num25 + ((!flag3) ? 0f : ((float)NGUIText.fontSize * 0.1f * ((num13 - num14) / (float)NGUIText.fontSize)));
									verts.Add(new Vector3(num11 - num27, num14 + num26));
									verts.Add(new Vector3(num11 + num27, num13 + num26));
									verts.Add(new Vector3(num12 + num27, num13 + num26));
									verts.Add(new Vector3(num12 - num27, num14 + num26));
								}
							}
							if (flag4 || flag5)
							{
								NGUIText.GlyphInfo glyphInfo10 = NGUIText.GetGlyph((!flag5) ? 95 : 45, prev);
								if (glyphInfo10 != null)
								{
									if (uvs != null)
									{
										if (NGUIText.bitmapFont != null)
										{
											glyphInfo10.u0.x = rect.xMin + num5 * glyphInfo10.u0.x;
											glyphInfo10.u1.x = rect.xMin + num5 * glyphInfo10.u1.x;
											glyphInfo10.u0.y = rect.yMax - num6 * glyphInfo10.u0.y;
											glyphInfo10.u1.y = rect.yMax - num6 * glyphInfo10.u1.y;
										}
										float num28 = (glyphInfo10.u0.x + glyphInfo10.u1.x) * 0.5f;
										float num29 = (glyphInfo10.u0.y + glyphInfo10.u1.y) * 0.5f;
										uvs.Add(new Vector2(num28, num29));
										uvs.Add(new Vector2(num28, num29));
										uvs.Add(new Vector2(num28, num29));
										uvs.Add(new Vector2(num28, num29));
									}
									if (flag && flag5)
									{
										num14 = (-num2 + glyphInfo10.v0.y) * 0.75f;
										num13 = (-num2 + glyphInfo10.v1.y) * 0.75f;
									}
									else
									{
										num14 = -num2 + glyphInfo10.v0.y;
										num13 = -num2 + glyphInfo10.v1.y;
									}
									verts.Add(new Vector3(num10, num14));
									verts.Add(new Vector3(num10, num13));
									verts.Add(new Vector3(num, num13));
									verts.Add(new Vector3(num, num14));
									Color color6 = color3;
									if (flag5)
									{
										color6.r *= 0.5f;
										color6.g *= 0.5f;
										color6.b *= 0.5f;
									}
									color6.a *= 0.75f;
									Color32 item3 = color6;
									cols.Add(item3);
									cols.Add(color3);
									cols.Add(color3);
									cols.Add(item3);
								}
							}
						}
					}
				}
			}
		}
		if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
		{
			NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
			size = verts.size;
		}
		NGUIText.mColors.Clear();
	}

	// Token: 0x0600033D RID: 829 RVA: 0x000188F4 File Offset: 0x00016AF4
	public static void PrintCharacterPositions(string text, BetterList<Vector3> verts, BetterList<int> indices)
	{
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)NGUIText.fontSize * NGUIText.fontScale * 0.5f;
		int length = text.Length;
		int size = verts.size;
		int prev = 0;
		for (int i = 0; i < length; i++)
		{
			int num5 = (int)text.get_Chars(i);
			verts.Add(new Vector3(num, -num2 - num4));
			indices.Add(i);
			if (num5 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (NGUIText.alignment != NGUIText.Alignment.Left)
				{
					NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num5 < 32)
			{
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bmsymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				if (bmsymbol == null)
				{
					float num6 = NGUIText.GetGlyphWidth(num5, prev);
					if (num6 != 0f)
					{
						num6 += NGUIText.finalSpacingX;
						if (Mathf.RoundToInt(num + num6) > NGUIText.rectWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
							{
								NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
								size = verts.size;
							}
							num = num6;
							num2 += NGUIText.finalLineHeight;
						}
						else
						{
							num += num6;
						}
						verts.Add(new Vector3(num, -num2 - num4));
						indices.Add(i + 1);
						prev = num5;
					}
				}
				else
				{
					float num7 = (float)bmsymbol.advance * NGUIText.fontScale + NGUIText.finalSpacingX;
					if (Mathf.RoundToInt(num + num7) > NGUIText.rectWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
						{
							NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
							size = verts.size;
						}
						num = num7;
						num2 += NGUIText.finalLineHeight;
					}
					else
					{
						num += num7;
					}
					verts.Add(new Vector3(num, -num2 - num4));
					indices.Add(i + 1);
					i += bmsymbol.sequence.Length - 1;
					prev = 0;
				}
			}
		}
		if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
		{
			NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00018BA4 File Offset: 0x00016DA4
	public static void PrintCaretAndSelection(string text, int start, int end, BetterList<Vector3> caret, BetterList<Vector3> highlight)
	{
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		int num = end;
		if (start > end)
		{
			end = start;
			start = num;
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = (float)NGUIText.fontSize * NGUIText.fontScale;
		int indexOffset = (caret == null) ? 0 : caret.size;
		int num6 = (highlight == null) ? 0 : highlight.size;
		int length = text.Length;
		int i = 0;
		int prev = 0;
		bool flag = false;
		bool flag2 = false;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		while (i < length)
		{
			if (caret != null && !flag2 && num <= i)
			{
				flag2 = true;
				caret.Add(new Vector3(num2 - 1f, -num3 - num5));
				caret.Add(new Vector3(num2 - 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3 - num5));
			}
			int num7 = (int)text.get_Chars(i);
			if (num7 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (caret != null && flag2)
				{
					if (NGUIText.alignment != NGUIText.Alignment.Left)
					{
						NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
					}
					caret = null;
				}
				if (highlight != null)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(zero2);
						highlight.Add(zero);
					}
					else if (start <= i && end > i)
					{
						highlight.Add(new Vector3(num2, -num3 - num5));
						highlight.Add(new Vector3(num2, -num3));
						highlight.Add(new Vector3(num2 + 2f, -num3));
						highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
					}
					if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
					{
						NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
						num6 = highlight.size;
					}
				}
				num2 = 0f;
				num3 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num7 < 32)
			{
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bmsymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				float num8 = (bmsymbol == null) ? NGUIText.GetGlyphWidth(num7, prev) : ((float)bmsymbol.advance * NGUIText.fontScale);
				if (num8 != 0f)
				{
					float num9 = num2;
					float num10 = num2 + num8;
					float num11 = -num3 - num5;
					float num12 = -num3;
					if (Mathf.RoundToInt(num10 + NGUIText.finalSpacingX) > NGUIText.rectWidth)
					{
						if (num2 == 0f)
						{
							return;
						}
						if (num2 > num4)
						{
							num4 = num2;
						}
						if (caret != null && flag2)
						{
							if (NGUIText.alignment != NGUIText.Alignment.Left)
							{
								NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
							}
							caret = null;
						}
						if (highlight != null)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(zero2);
								highlight.Add(zero);
							}
							else if (start <= i && end > i)
							{
								highlight.Add(new Vector3(num2, -num3 - num5));
								highlight.Add(new Vector3(num2, -num3));
								highlight.Add(new Vector3(num2 + 2f, -num3));
								highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
							{
								Debug.Log("Aligning");
								NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
								num6 = highlight.size;
							}
						}
						num9 -= num2;
						num10 -= num2;
						num11 -= NGUIText.finalLineHeight;
						num12 -= NGUIText.finalLineHeight;
						num2 = 0f;
						num3 += NGUIText.finalLineHeight;
					}
					num2 += num8 + NGUIText.finalSpacingX;
					if (highlight != null)
					{
						if (start > i || end <= i)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(zero2);
								highlight.Add(zero);
							}
						}
						else if (!flag)
						{
							flag = true;
							highlight.Add(new Vector3(num9, num11));
							highlight.Add(new Vector3(num9, num12));
						}
					}
					zero..ctor(num10, num11);
					zero2..ctor(num10, num12);
					prev = num7;
				}
			}
			i++;
		}
		if (caret != null)
		{
			if (!flag2)
			{
				caret.Add(new Vector3(num2 - 1f, -num3 - num5));
				caret.Add(new Vector3(num2 - 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3 - num5));
			}
			if (NGUIText.alignment != NGUIText.Alignment.Left)
			{
				NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
			}
		}
		if (highlight != null)
		{
			if (flag)
			{
				highlight.Add(zero2);
				highlight.Add(zero);
			}
			else if (start < i && end == i)
			{
				highlight.Add(new Vector3(num2, -num3 - num5));
				highlight.Add(new Vector3(num2, -num3));
				highlight.Add(new Vector3(num2 + 2f, -num3));
				highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
			}
			if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
			{
				NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
			}
		}
	}

	// Token: 0x04000322 RID: 802
	public static UIFont bitmapFont;

	// Token: 0x04000323 RID: 803
	public static Font dynamicFont;

	// Token: 0x04000324 RID: 804
	public static NGUIText.GlyphInfo glyph = new NGUIText.GlyphInfo();

	// Token: 0x04000325 RID: 805
	public static int fontSize = 16;

	// Token: 0x04000326 RID: 806
	public static float fontScale = 1f;

	// Token: 0x04000327 RID: 807
	public static float pixelDensity = 1f;

	// Token: 0x04000328 RID: 808
	public static FontStyle fontStyle = 0;

	// Token: 0x04000329 RID: 809
	public static NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	// Token: 0x0400032A RID: 810
	public static Color tint = Color.white;

	// Token: 0x0400032B RID: 811
	public static int rectWidth = 1000000;

	// Token: 0x0400032C RID: 812
	public static int rectHeight = 1000000;

	// Token: 0x0400032D RID: 813
	public static int maxLines = 0;

	// Token: 0x0400032E RID: 814
	public static bool gradient = false;

	// Token: 0x0400032F RID: 815
	public static Color gradientBottom = Color.white;

	// Token: 0x04000330 RID: 816
	public static Color gradientTop = Color.white;

	// Token: 0x04000331 RID: 817
	public static bool encoding = false;

	// Token: 0x04000332 RID: 818
	public static float spacingX = 0f;

	// Token: 0x04000333 RID: 819
	public static float spacingY = 0f;

	// Token: 0x04000334 RID: 820
	public static bool premultiply = false;

	// Token: 0x04000335 RID: 821
	public static NGUIText.SymbolStyle symbolStyle;

	// Token: 0x04000336 RID: 822
	public static int finalSize = 0;

	// Token: 0x04000337 RID: 823
	public static float finalSpacingX = 0f;

	// Token: 0x04000338 RID: 824
	public static float finalLineHeight = 0f;

	// Token: 0x04000339 RID: 825
	public static float baseline = 0f;

	// Token: 0x0400033A RID: 826
	public static bool useSymbols = false;

	// Token: 0x0400033B RID: 827
	private static Color mInvisible = new Color(0f, 0f, 0f, 0f);

	// Token: 0x0400033C RID: 828
	private static BetterList<Color> mColors = new BetterList<Color>();

	// Token: 0x0400033D RID: 829
	private static CharacterInfo mTempChar;

	// Token: 0x0400033E RID: 830
	private static BetterList<float> mSizes = new BetterList<float>();

	// Token: 0x0400033F RID: 831
	private static Color32 s_c0;

	// Token: 0x04000340 RID: 832
	private static Color32 s_c1;

	// Token: 0x04000341 RID: 833
	private static float[] mBoldOffset = new float[]
	{
		-0.5f,
		0f,
		0.5f,
		0f,
		0f,
		-0.5f,
		0f,
		0.5f
	};

	// Token: 0x0200008B RID: 139
	public enum Alignment
	{
		// Token: 0x04000347 RID: 839
		Automatic,
		// Token: 0x04000348 RID: 840
		Left,
		// Token: 0x04000349 RID: 841
		Center,
		// Token: 0x0400034A RID: 842
		Right,
		// Token: 0x0400034B RID: 843
		Justified
	}

	// Token: 0x0200008C RID: 140
	public enum SymbolStyle
	{
		// Token: 0x0400034D RID: 845
		None,
		// Token: 0x0400034E RID: 846
		Normal,
		// Token: 0x0400034F RID: 847
		Colored
	}

	// Token: 0x0200008D RID: 141
	public class GlyphInfo
	{
		// Token: 0x04000350 RID: 848
		public Vector2 v0;

		// Token: 0x04000351 RID: 849
		public Vector2 v1;

		// Token: 0x04000352 RID: 850
		public Vector2 u0;

		// Token: 0x04000353 RID: 851
		public Vector2 u1;

		// Token: 0x04000354 RID: 852
		public float advance;

		// Token: 0x04000355 RID: 853
		public int channel;

		// Token: 0x04000356 RID: 854
		public bool rotatedUVs;
	}
}
