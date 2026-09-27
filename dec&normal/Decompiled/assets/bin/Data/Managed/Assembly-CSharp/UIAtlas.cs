using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x0600051C RID: 1308 RVA: 0x000228B0 File Offset: 0x00020AB0
	// (set) Token: 0x0600051D RID: 1309 RVA: 0x000228DC File Offset: 0x00020ADC
	public Material spriteMaterial
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.material : this.mReplacement.spriteMaterial;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteMaterial = value;
			}
			else if (this.material == null)
			{
				this.mPMA = 0;
				this.material = value;
			}
			else
			{
				this.MarkAsChanged();
				this.mPMA = -1;
				this.material = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x0600051E RID: 1310 RVA: 0x0002294C File Offset: 0x00020B4C
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material spriteMaterial = this.spriteMaterial;
				this.mPMA = ((!(spriteMaterial != null) || !(spriteMaterial.shader != null) || !spriteMaterial.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600051F RID: 1311 RVA: 0x000229D8 File Offset: 0x00020BD8
	// (set) Token: 0x06000520 RID: 1312 RVA: 0x00022A20 File Offset: 0x00020C20
	public List<UISpriteData> spriteList
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.spriteList;
			}
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			return this.mSprites;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteList = value;
			}
			else
			{
				this.mSprites = value;
			}
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000521 RID: 1313 RVA: 0x00022A4C File Offset: 0x00020C4C
	public Texture texture
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!(this.material != null)) ? null : this.material.mainTexture) : this.mReplacement.texture;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x00022A9C File Offset: 0x00020C9C
	// (set) Token: 0x06000523 RID: 1315 RVA: 0x00022AC8 File Offset: 0x00020CC8
	public float pixelSize
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mPixelSize : this.mReplacement.pixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
			}
			else
			{
				float num = Mathf.Clamp(value, 0.25f, 4f);
				if (this.mPixelSize != num)
				{
					this.mPixelSize = num;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x00022B24 File Offset: 0x00020D24
	// (set) Token: 0x06000525 RID: 1317 RVA: 0x00022B2C File Offset: 0x00020D2C
	public UIAtlas replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIAtlas uiatlas = value;
			if (uiatlas == this)
			{
				uiatlas = null;
			}
			if (this.mReplacement != uiatlas)
			{
				if (uiatlas != null && uiatlas.replacement == this)
				{
					uiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsChanged();
				}
				this.mReplacement = uiatlas;
				if (uiatlas != null)
				{
					this.material = null;
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00022BB8 File Offset: 0x00020DB8
	public UISpriteData GetSprite(string name)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			int i = 0;
			int count = this.mSprites.Count;
			while (i < count)
			{
				UISpriteData uispriteData = this.mSprites[i];
				if (!string.IsNullOrEmpty(uispriteData.name) && name == uispriteData.name)
				{
					return uispriteData;
				}
				i++;
			}
		}
		return null;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00022C54 File Offset: 0x00020E54
	public void SortAlphabetically()
	{
		this.mSprites.Sort((UISpriteData s1, UISpriteData s2) => s1.name.CompareTo(s2.name));
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00022C8C File Offset: 0x00020E8C
	public BetterList<string> GetListOfSprites()
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name))
			{
				betterList.Add(uispriteData.name);
			}
			i++;
		}
		return betterList;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00022D24 File Offset: 0x00020F24
	public BetterList<string> GetListOfSprites(string match)
	{
		if (this.mReplacement)
		{
			return this.mReplacement.GetListOfSprites(match);
		}
		if (string.IsNullOrEmpty(match))
		{
			return this.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name) && string.Equals(match, uispriteData.name, 5))
			{
				betterList.Add(uispriteData.name);
				return betterList;
			}
			i++;
		}
		string[] array = match.Split(new char[]
		{
			' '
		}, 1);
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = array[j].ToLower();
		}
		int k = 0;
		int count2 = this.mSprites.Count;
		while (k < count2)
		{
			UISpriteData uispriteData2 = this.mSprites[k];
			if (uispriteData2 != null && !string.IsNullOrEmpty(uispriteData2.name))
			{
				string text = uispriteData2.name.ToLower();
				int num = 0;
				for (int l = 0; l < array.Length; l++)
				{
					if (text.Contains(array[l]))
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					betterList.Add(uispriteData2.name);
				}
			}
			k++;
		}
		return betterList;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00022EC0 File Offset: 0x000210C0
	private bool References(UIAtlas atlas)
	{
		return !(atlas == null) && (atlas == this || (this.mReplacement != null && this.mReplacement.References(atlas)));
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00022F0C File Offset: 0x0002110C
	public static bool CheckIfRelated(UIAtlas a, UIAtlas b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00022F58 File Offset: 0x00021158
	public void MarkAsChanged()
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.MarkAsChanged();
		}
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (UIAtlas.CheckIfRelated(this, uisprite.atlas))
			{
				UIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		UIFont[] array2 = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UIFont uifont = array2[j];
			if (UIAtlas.CheckIfRelated(this, uifont.atlas))
			{
				UIAtlas atlas2 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas2;
			}
			j++;
		}
		UILabel[] array3 = NGUITools.FindActive<UILabel>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UILabel uilabel = array3[k];
			if (uilabel.bitmapFont != null && UIAtlas.CheckIfRelated(this, uilabel.bitmapFont.atlas))
			{
				UIFont bitmapFont = uilabel.bitmapFont;
				uilabel.bitmapFont = null;
				uilabel.bitmapFont = bitmapFont;
			}
			k++;
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000230A0 File Offset: 0x000212A0
	private bool Upgrade()
	{
		if (this.mReplacement)
		{
			return this.mReplacement.Upgrade();
		}
		if (this.mSprites.Count == 0 && this.sprites.Count > 0 && this.material)
		{
			Texture mainTexture = this.material.mainTexture;
			int width = (!(mainTexture != null)) ? 512 : mainTexture.width;
			int height = (!(mainTexture != null)) ? 512 : mainTexture.height;
			for (int i = 0; i < this.sprites.Count; i++)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				Rect outer = sprite.outer;
				Rect inner = sprite.inner;
				if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
				{
					NGUIMath.ConvertToPixels(outer, width, height, true);
					NGUIMath.ConvertToPixels(inner, width, height, true);
				}
				UISpriteData uispriteData = new UISpriteData();
				uispriteData.name = sprite.name;
				uispriteData.x = Mathf.RoundToInt(outer.xMin);
				uispriteData.y = Mathf.RoundToInt(outer.yMin);
				uispriteData.width = Mathf.RoundToInt(outer.width);
				uispriteData.height = Mathf.RoundToInt(outer.height);
				uispriteData.paddingLeft = Mathf.RoundToInt(sprite.paddingLeft * outer.width);
				uispriteData.paddingRight = Mathf.RoundToInt(sprite.paddingRight * outer.width);
				uispriteData.paddingBottom = Mathf.RoundToInt(sprite.paddingBottom * outer.height);
				uispriteData.paddingTop = Mathf.RoundToInt(sprite.paddingTop * outer.height);
				uispriteData.borderLeft = Mathf.RoundToInt(inner.xMin - outer.xMin);
				uispriteData.borderRight = Mathf.RoundToInt(outer.xMax - inner.xMax);
				uispriteData.borderBottom = Mathf.RoundToInt(outer.yMax - inner.yMax);
				uispriteData.borderTop = Mathf.RoundToInt(inner.yMin - outer.yMin);
				this.mSprites.Add(uispriteData);
			}
			this.sprites.Clear();
			return true;
		}
		return false;
	}

	// Token: 0x04000476 RID: 1142
	[SerializeField]
	[HideInInspector]
	private Material material;

	// Token: 0x04000477 RID: 1143
	[HideInInspector]
	[SerializeField]
	private List<UISpriteData> mSprites = new List<UISpriteData>();

	// Token: 0x04000478 RID: 1144
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x04000479 RID: 1145
	[HideInInspector]
	[SerializeField]
	private UIAtlas mReplacement;

	// Token: 0x0400047A RID: 1146
	[SerializeField]
	[HideInInspector]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x0400047B RID: 1147
	[SerializeField]
	[HideInInspector]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x0400047C RID: 1148
	private int mPMA = -1;

	// Token: 0x020000B5 RID: 181
	[Serializable]
	private class Sprite
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00023364 File Offset: 0x00021564
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x0400047E RID: 1150
		public string name = "Unity Bug";

		// Token: 0x0400047F RID: 1151
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000480 RID: 1152
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000481 RID: 1153
		public bool rotated;

		// Token: 0x04000482 RID: 1154
		public float paddingLeft;

		// Token: 0x04000483 RID: 1155
		public float paddingRight;

		// Token: 0x04000484 RID: 1156
		public float paddingTop;

		// Token: 0x04000485 RID: 1157
		public float paddingBottom;
	}

	// Token: 0x020000B6 RID: 182
	private enum Coordinates
	{
		// Token: 0x04000487 RID: 1159
		Pixels,
		// Token: 0x04000488 RID: 1160
		TexCoords
	}
}
