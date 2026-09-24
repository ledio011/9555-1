using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
[AddComponentMenu("Image Effects/Color Correction")]
[ExecuteInEditMode]
[Serializable]
public class ColorCorrectionCurves : PostEffectsBase
{
	// Token: 0x0600005B RID: 91 RVA: 0x0000562C File Offset: 0x0000382C
	public ColorCorrectionCurves()
	{
		this.selectiveFromColor = Color.white;
		this.selectiveToColor = Color.white;
		this.updateTextures = true;
		this.updateTexturesOnStartup = true;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00005664 File Offset: 0x00003864
	public override void Start()
	{
		base.Start();
		this.updateTexturesOnStartup = true;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00005674 File Offset: 0x00003874
	public virtual void Awake()
	{
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00005678 File Offset: 0x00003878
	public virtual void OnDisable()
	{
		if (this.ccMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.ccMaterial);
		}
		if (this.ccDepthMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.ccDepthMaterial);
		}
		if (this.selectiveCcMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.selectiveCcMaterial);
		}
		if (this.rgbChannelTex)
		{
			UnityEngine.Object.DestroyImmediate(this.rgbChannelTex);
		}
		if (this.rgbDepthChannelTex)
		{
			UnityEngine.Object.DestroyImmediate(this.rgbDepthChannelTex);
		}
		if (this.zCurveTex)
		{
			UnityEngine.Object.DestroyImmediate(this.zCurveTex);
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00005728 File Offset: 0x00003928
	public override bool CheckResources()
	{
		this.CheckSupport(this.mode == ColorCorrectionMode.Advanced);
		this.ccMaterial = this.CheckShaderAndCreateMaterial(this.simpleColorCorrectionCurvesShader, this.ccMaterial);
		this.ccDepthMaterial = this.CheckShaderAndCreateMaterial(this.colorCorrectionCurvesShader, this.ccDepthMaterial);
		this.selectiveCcMaterial = this.CheckShaderAndCreateMaterial(this.colorCorrectionSelectiveShader, this.selectiveCcMaterial);
		if (!this.rgbChannelTex)
		{
			this.rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!this.rgbDepthChannelTex)
		{
			this.rgbDepthChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!this.zCurveTex)
		{
			this.zCurveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
		}
		this.rgbChannelTex.hideFlags = HideFlags.DontSave;
		this.rgbDepthChannelTex.hideFlags = HideFlags.DontSave;
		this.zCurveTex.hideFlags = HideFlags.DontSave;
		this.rgbChannelTex.wrapMode = TextureWrapMode.Clamp;
		this.rgbDepthChannelTex.wrapMode = TextureWrapMode.Clamp;
		this.zCurveTex.wrapMode = TextureWrapMode.Clamp;
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00005858 File Offset: 0x00003A58
	public virtual void UpdateParameters()
	{
		if (this.redChannel != null && this.greenChannel != null && this.blueChannel != null)
		{
			for (float num = (float)0; num <= 1f; num += 0.003921569f)
			{
				float num2 = Mathf.Clamp(this.redChannel.Evaluate(num), (float)0, 1f);
				float num3 = Mathf.Clamp(this.greenChannel.Evaluate(num), (float)0, 1f);
				float num4 = Mathf.Clamp(this.blueChannel.Evaluate(num), (float)0, 1f);
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
				float num5 = Mathf.Clamp(this.zCurve.Evaluate(num), (float)0, 1f);
				this.zCurveTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num5, num5, num5));
				num2 = Mathf.Clamp(this.depthRedChannel.Evaluate(num), (float)0, 1f);
				num3 = Mathf.Clamp(this.depthGreenChannel.Evaluate(num), (float)0, 1f);
				num4 = Mathf.Clamp(this.depthBlueChannel.Evaluate(num), (float)0, 1f);
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
			}
			this.rgbChannelTex.Apply();
			this.rgbDepthChannelTex.Apply();
			this.zCurveTex.Apply();
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00005A68 File Offset: 0x00003C68
	public virtual void UpdateTextures()
	{
		this.UpdateParameters();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00005A70 File Offset: 0x00003C70
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.updateTexturesOnStartup)
			{
				this.UpdateParameters();
				this.updateTexturesOnStartup = false;
			}
			if (this.useDepthCorrection)
			{
				this.camera.depthTextureMode = (this.camera.depthTextureMode | DepthTextureMode.Depth);
			}
			RenderTexture renderTexture = destination;
			if (this.selectiveCc)
			{
				renderTexture = RenderTexture.GetTemporary(source.width, source.height);
			}
			if (this.useDepthCorrection)
			{
				this.ccDepthMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccDepthMaterial.SetTexture("_ZCurve", this.zCurveTex);
				this.ccDepthMaterial.SetTexture("_RgbDepthTex", this.rgbDepthChannelTex);
				Graphics.Blit(source, renderTexture, this.ccDepthMaterial);
			}
			else
			{
				this.ccMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				Graphics.Blit(source, renderTexture, this.ccMaterial);
			}
			if (this.selectiveCc)
			{
				this.selectiveCcMaterial.SetColor("selColor", this.selectiveFromColor);
				this.selectiveCcMaterial.SetColor("targetColor", this.selectiveToColor);
				Graphics.Blit(renderTexture, destination, this.selectiveCcMaterial);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00005BBC File Offset: 0x00003DBC
	public override void Main()
	{
	}

	// Token: 0x040000D5 RID: 213
	public AnimationCurve redChannel;

	// Token: 0x040000D6 RID: 214
	public AnimationCurve greenChannel;

	// Token: 0x040000D7 RID: 215
	public AnimationCurve blueChannel;

	// Token: 0x040000D8 RID: 216
	public bool useDepthCorrection;

	// Token: 0x040000D9 RID: 217
	public AnimationCurve zCurve;

	// Token: 0x040000DA RID: 218
	public AnimationCurve depthRedChannel;

	// Token: 0x040000DB RID: 219
	public AnimationCurve depthGreenChannel;

	// Token: 0x040000DC RID: 220
	public AnimationCurve depthBlueChannel;

	// Token: 0x040000DD RID: 221
	private Material ccMaterial;

	// Token: 0x040000DE RID: 222
	private Material ccDepthMaterial;

	// Token: 0x040000DF RID: 223
	private Material selectiveCcMaterial;

	// Token: 0x040000E0 RID: 224
	private Texture2D rgbChannelTex;

	// Token: 0x040000E1 RID: 225
	private Texture2D rgbDepthChannelTex;

	// Token: 0x040000E2 RID: 226
	private Texture2D zCurveTex;

	// Token: 0x040000E3 RID: 227
	public bool selectiveCc;

	// Token: 0x040000E4 RID: 228
	public Color selectiveFromColor;

	// Token: 0x040000E5 RID: 229
	public Color selectiveToColor;

	// Token: 0x040000E6 RID: 230
	public ColorCorrectionMode mode;

	// Token: 0x040000E7 RID: 231
	public bool updateTextures;

	// Token: 0x040000E8 RID: 232
	public Shader colorCorrectionCurvesShader;

	// Token: 0x040000E9 RID: 233
	public Shader simpleColorCorrectionCurvesShader;

	// Token: 0x040000EA RID: 234
	public Shader colorCorrectionSelectiveShader;

	// Token: 0x040000EB RID: 235
	private bool updateTexturesOnStartup;
}
