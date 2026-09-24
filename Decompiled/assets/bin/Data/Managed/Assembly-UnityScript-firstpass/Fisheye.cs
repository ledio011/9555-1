using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Fisheye")]
[Serializable]
public class Fisheye : PostEffectsBase
{
	// Token: 0x0600008A RID: 138 RVA: 0x00007868 File Offset: 0x00005A68
	public Fisheye()
	{
		this.strengthX = 0.05f;
		this.strengthY = 0.05f;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00007888 File Offset: 0x00005A88
	public virtual void OnDisable()
	{
		if (this.fisheyeMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.fisheyeMaterial);
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000078A8 File Offset: 0x00005AA8
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.fisheyeMaterial = this.CheckShaderAndCreateMaterial(this.fishEyeShader, this.fisheyeMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000078E4 File Offset: 0x00005AE4
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = 0.15625f;
			float num2 = (float)source.width * 1f / ((float)source.height * 1f);
			this.fisheyeMaterial.SetVector("intensity", new Vector4(this.strengthX * num2 * num, this.strengthY * num, this.strengthX * num2 * num, this.strengthY * num));
			Graphics.Blit(source, destination, this.fisheyeMaterial);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00007970 File Offset: 0x00005B70
	public override void Main()
	{
	}

	// Token: 0x04000153 RID: 339
	public float strengthX;

	// Token: 0x04000154 RID: 340
	public float strengthY;

	// Token: 0x04000155 RID: 341
	public Shader fishEyeShader;

	// Token: 0x04000156 RID: 342
	private Material fisheyeMaterial;
}
