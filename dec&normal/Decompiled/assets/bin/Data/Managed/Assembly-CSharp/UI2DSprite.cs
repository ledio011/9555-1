using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Unity2D Sprite")]
public class UI2DSprite : UIWidget
{
	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000505 RID: 1285 RVA: 0x000218E4 File Offset: 0x0001FAE4
	// (set) Token: 0x06000506 RID: 1286 RVA: 0x000218EC File Offset: 0x0001FAEC
	public Sprite sprite2D
	{
		get
		{
			return this.mSprite;
		}
		set
		{
			if (this.mSprite != value)
			{
				base.RemoveFromPanel();
				this.mSprite = value;
				this.nextSprite = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000507 RID: 1287 RVA: 0x0002191C File Offset: 0x0001FB1C
	// (set) Token: 0x06000508 RID: 1288 RVA: 0x00021924 File Offset: 0x0001FB24
	public override Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				base.RemoveFromPanel();
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000509 RID: 1289 RVA: 0x00021954 File Offset: 0x0001FB54
	// (set) Token: 0x0600050A RID: 1290 RVA: 0x000219A8 File Offset: 0x0001FBA8
	public override Shader shader
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat.shader;
			}
			if (this.mShader == null)
			{
				this.mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				base.RemoveFromPanel();
				this.mShader = value;
				if (this.mMat == null)
				{
					this.mPMA = -1;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600050B RID: 1291 RVA: 0x000219F4 File Offset: 0x0001FBF4
	public override Texture mainTexture
	{
		get
		{
			if (this.mSprite != null)
			{
				return this.mSprite.texture;
			}
			if (this.mMat != null)
			{
				return this.mMat.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x0600050C RID: 1292 RVA: 0x00021A3C File Offset: 0x0001FC3C
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Shader shader = this.shader;
				this.mPMA = ((!(shader != null) || !shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600050D RID: 1293 RVA: 0x00021A94 File Offset: 0x0001FC94
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			int num5 = (!(this.mSprite != null)) ? this.mWidth : Mathf.RoundToInt(this.mSprite.textureRect.width);
			int num6 = (!(this.mSprite != null)) ? this.mHeight : Mathf.RoundToInt(this.mSprite.textureRect.height);
			if ((num5 & 1) != 0)
			{
				num3 -= 1f / (float)num5 * (float)this.mWidth;
			}
			if ((num6 & 1) != 0)
			{
				num4 -= 1f / (float)num6 * (float)this.mHeight;
			}
			return new Vector4((this.mDrawRegion.x != 0f) ? Mathf.Lerp(num, num3, this.mDrawRegion.x) : num, (this.mDrawRegion.y != 0f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.y) : num2, (this.mDrawRegion.z != 1f) ? Mathf.Lerp(num, num3, this.mDrawRegion.z) : num3, (this.mDrawRegion.w != 1f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.w) : num4);
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x0600050E RID: 1294 RVA: 0x00021C48 File Offset: 0x0001FE48
	public Rect uvRect
	{
		get
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				Rect textureRect = this.mSprite.textureRect;
				textureRect.xMin /= (float)mainTexture.width;
				textureRect.xMax /= (float)mainTexture.width;
				textureRect.yMin /= (float)mainTexture.height;
				textureRect.yMax /= (float)mainTexture.height;
				return textureRect;
			}
			return new Rect(0f, 0f, 1f, 1f);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00021CE4 File Offset: 0x0001FEE4
	protected override void OnUpdate()
	{
		if (this.nextSprite != null)
		{
			if (this.nextSprite != this.mSprite)
			{
				this.sprite2D = this.nextSprite;
			}
			this.nextSprite = null;
		}
		base.OnUpdate();
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00021D34 File Offset: 0x0001FF34
	public override void MakePixelPerfect()
	{
		if (this.mSprite != null)
		{
			Rect textureRect = this.mSprite.textureRect;
			int num = Mathf.RoundToInt(textureRect.width);
			int num2 = Mathf.RoundToInt(textureRect.height);
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00021DA8 File Offset: 0x0001FFA8
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Color color = base.color;
		color.a = this.finalAlpha;
		Color32 item = (!this.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		Vector4 drawingDimensions = this.drawingDimensions;
		Rect uvRect = this.uvRect;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		uvs.Add(new Vector2(uvRect.xMin, uvRect.yMin));
		uvs.Add(new Vector2(uvRect.xMin, uvRect.yMax));
		uvs.Add(new Vector2(uvRect.xMax, uvRect.yMax));
		uvs.Add(new Vector2(uvRect.xMax, uvRect.yMin));
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
	}

	// Token: 0x04000453 RID: 1107
	[SerializeField]
	[HideInInspector]
	private Sprite mSprite;

	// Token: 0x04000454 RID: 1108
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x04000455 RID: 1109
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x04000456 RID: 1110
	public Sprite nextSprite;

	// Token: 0x04000457 RID: 1111
	private int mPMA = -1;
}
