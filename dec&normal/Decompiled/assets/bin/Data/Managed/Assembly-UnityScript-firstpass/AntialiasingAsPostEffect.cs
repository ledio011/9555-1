using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Antialiasing (Fullscreen)")]
[ExecuteInEditMode]
[Serializable]
public class AntialiasingAsPostEffect : PostEffectsBase
{
	// Token: 0x0600004A RID: 74 RVA: 0x000046BC File Offset: 0x000028BC
	public AntialiasingAsPostEffect()
	{
		this.mode = AAMode.FXAA3Console;
		this.offsetScale = 0.2f;
		this.blurRadius = 18f;
		this.edgeThresholdMin = 0.05f;
		this.edgeThreshold = 0.2f;
		this.edgeSharpness = 4f;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004710 File Offset: 0x00002910
	public virtual Material CurrentAAMaterial()
	{
		AAMode aamode = this.mode;
		Material result;
		if (aamode == AAMode.FXAA3Console)
		{
			result = this.materialFXAAIII;
		}
		else if (aamode == AAMode.FXAA2)
		{
			result = this.materialFXAAII;
		}
		else if (aamode == AAMode.FXAA1PresetA)
		{
			result = this.materialFXAAPreset2;
		}
		else if (aamode == AAMode.FXAA1PresetB)
		{
			result = this.materialFXAAPreset3;
		}
		else if (aamode == AAMode.NFAA)
		{
			result = this.nfaa;
		}
		else if (aamode == AAMode.SSAA)
		{
			result = this.ssaa;
		}
		else if (aamode == AAMode.DLAA)
		{
			result = this.dlaa;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000047B4 File Offset: 0x000029B4
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.materialFXAAPreset2 = this.CreateMaterial(this.shaderFXAAPreset2, this.materialFXAAPreset2);
		this.materialFXAAPreset3 = this.CreateMaterial(this.shaderFXAAPreset3, this.materialFXAAPreset3);
		this.materialFXAAII = this.CreateMaterial(this.shaderFXAAII, this.materialFXAAII);
		this.materialFXAAIII = this.CreateMaterial(this.shaderFXAAIII, this.materialFXAAIII);
		this.nfaa = this.CreateMaterial(this.nfaaShader, this.nfaa);
		this.ssaa = this.CreateMaterial(this.ssaaShader, this.ssaa);
		this.dlaa = this.CreateMaterial(this.dlaaShader, this.dlaa);
		if (!this.ssaaShader.isSupported)
		{
			this.NotSupported();
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00004894 File Offset: 0x00002A94
	public virtual void OnDisable()
	{
		if (this.materialFXAAPreset2)
		{
			UnityEngine.Object.Destroy(this.materialFXAAPreset2);
		}
		if (this.materialFXAAPreset3)
		{
			UnityEngine.Object.Destroy(this.materialFXAAPreset3);
		}
		if (this.materialFXAAII)
		{
			UnityEngine.Object.Destroy(this.materialFXAAII);
		}
		if (this.materialFXAAIII)
		{
			UnityEngine.Object.Destroy(this.materialFXAAIII);
		}
		if (this.nfaa)
		{
			UnityEngine.Object.Destroy(this.nfaa);
		}
		if (this.ssaa)
		{
			UnityEngine.Object.Destroy(this.ssaa);
		}
		if (this.dlaa)
		{
			UnityEngine.Object.Destroy(this.dlaa);
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00004960 File Offset: 0x00002B60
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else if (this.mode == AAMode.FXAA3Console && this.materialFXAAIII != null)
		{
			this.materialFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
			this.materialFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
			this.materialFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
			Graphics.Blit(source, destination, this.materialFXAAIII);
		}
		else if (this.mode == AAMode.FXAA1PresetB && this.materialFXAAPreset3 != null)
		{
			Graphics.Blit(source, destination, this.materialFXAAPreset3);
		}
		else if (this.mode == AAMode.FXAA1PresetA && this.materialFXAAPreset2 != null)
		{
			source.anisoLevel = 4;
			Graphics.Blit(source, destination, this.materialFXAAPreset2);
			source.anisoLevel = 0;
		}
		else if (this.mode == AAMode.FXAA2 && this.materialFXAAII != null)
		{
			Graphics.Blit(source, destination, this.materialFXAAII);
		}
		else if (this.mode == AAMode.SSAA && this.ssaa != null)
		{
			Graphics.Blit(source, destination, this.ssaa);
		}
		else if (this.mode == AAMode.DLAA && this.dlaa != null)
		{
			source.anisoLevel = 0;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
			Graphics.Blit(source, temporary, this.dlaa, 0);
			Graphics.Blit(temporary, destination, this.dlaa, (!this.dlaaSharp) ? 1 : 2);
			RenderTexture.ReleaseTemporary(temporary);
		}
		else if (this.mode == AAMode.NFAA && this.nfaa != null)
		{
			source.anisoLevel = 0;
			this.nfaa.SetFloat("_OffsetScale", this.offsetScale);
			this.nfaa.SetFloat("_BlurRadius", this.blurRadius);
			Graphics.Blit(source, destination, this.nfaa, (!this.showGeneratedNormals) ? 0 : 1);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00004BA8 File Offset: 0x00002DA8
	public override void Main()
	{
	}

	// Token: 0x04000088 RID: 136
	public AAMode mode;

	// Token: 0x04000089 RID: 137
	public bool showGeneratedNormals;

	// Token: 0x0400008A RID: 138
	public float offsetScale;

	// Token: 0x0400008B RID: 139
	public float blurRadius;

	// Token: 0x0400008C RID: 140
	public float edgeThresholdMin;

	// Token: 0x0400008D RID: 141
	public float edgeThreshold;

	// Token: 0x0400008E RID: 142
	public float edgeSharpness;

	// Token: 0x0400008F RID: 143
	public bool dlaaSharp;

	// Token: 0x04000090 RID: 144
	public Shader ssaaShader;

	// Token: 0x04000091 RID: 145
	private Material ssaa;

	// Token: 0x04000092 RID: 146
	public Shader dlaaShader;

	// Token: 0x04000093 RID: 147
	private Material dlaa;

	// Token: 0x04000094 RID: 148
	public Shader nfaaShader;

	// Token: 0x04000095 RID: 149
	private Material nfaa;

	// Token: 0x04000096 RID: 150
	public Shader shaderFXAAPreset2;

	// Token: 0x04000097 RID: 151
	private Material materialFXAAPreset2;

	// Token: 0x04000098 RID: 152
	public Shader shaderFXAAPreset3;

	// Token: 0x04000099 RID: 153
	private Material materialFXAAPreset3;

	// Token: 0x0400009A RID: 154
	public Shader shaderFXAAII;

	// Token: 0x0400009B RID: 155
	private Material materialFXAAII;

	// Token: 0x0400009C RID: 156
	public Shader shaderFXAAIII;

	// Token: 0x0400009D RID: 157
	private Material materialFXAAIII;
}
