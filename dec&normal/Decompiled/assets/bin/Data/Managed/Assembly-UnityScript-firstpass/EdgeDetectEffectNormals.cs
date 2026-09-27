using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Edge Detection (Geometry)")]
[Serializable]
public class EdgeDetectEffectNormals : PostEffectsBase
{
	// Token: 0x06000085 RID: 133 RVA: 0x00007700 File Offset: 0x00005900
	public EdgeDetectEffectNormals()
	{
		this.mode = EdgeDetectMode.Thin;
		this.sensitivityDepth = 1f;
		this.sensitivityNormals = 1f;
		this.edgesOnlyBgColor = Color.white;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000773C File Offset: 0x0000593C
	public virtual void OnDisable()
	{
		if (this.edgeDetectMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.edgeDetectMaterial);
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000775C File Offset: 0x0000595C
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.edgeDetectMaterial = this.CheckShaderAndCreateMaterial(this.edgeDetectShader, this.edgeDetectMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00007798 File Offset: 0x00005998
	[ImageEffectOpaque]
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			Vector2 vector = new Vector2(this.sensitivityDepth, this.sensitivityNormals);
			source.filterMode = FilterMode.Point;
			this.edgeDetectMaterial.SetVector("sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
			this.edgeDetectMaterial.SetFloat("_BgFade", this.edgesOnly);
			Vector4 vector2 = this.edgesOnlyBgColor;
			this.edgeDetectMaterial.SetVector("_BgColor", vector2);
			if (this.mode == EdgeDetectMode.Thin)
			{
				Graphics.Blit(source, destination, this.edgeDetectMaterial, 0);
			}
			else
			{
				Graphics.Blit(source, destination, this.edgeDetectMaterial, 1);
			}
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00007864 File Offset: 0x00005A64
	public override void Main()
	{
	}

	// Token: 0x0400014C RID: 332
	public EdgeDetectMode mode;

	// Token: 0x0400014D RID: 333
	public float sensitivityDepth;

	// Token: 0x0400014E RID: 334
	public float sensitivityNormals;

	// Token: 0x0400014F RID: 335
	public float edgesOnly;

	// Token: 0x04000150 RID: 336
	public Color edgesOnlyBgColor;

	// Token: 0x04000151 RID: 337
	public Shader edgeDetectShader;

	// Token: 0x04000152 RID: 338
	private Material edgeDetectMaterial;
}
