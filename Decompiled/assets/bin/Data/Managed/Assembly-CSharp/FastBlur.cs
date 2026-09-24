using System;
using UnityEngine;

// Token: 0x020008B1 RID: 2225
public class FastBlur : MonoBehaviour
{
	// Token: 0x06003BEC RID: 15340 RVA: 0x001057B4 File Offset: 0x001039B4
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || GameSettingData.IsLowPhone)
		{
			base.enabled = false;
			return;
		}
		if (!this.blurShader)
		{
			base.enabled = false;
			return;
		}
		this.blurMaterial = new Material(this.blurShader);
	}

	// Token: 0x06003BED RID: 15341 RVA: 0x00105808 File Offset: 0x00103A08
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		float num = 1f / (1f * (float)(1 << this.downsample));
		source.filterMode = 1;
		int num2 = source.width >> this.downsample;
		int num3 = source.height >> this.downsample;
		RenderTexture renderTexture = RenderTexture.GetTemporary(num2, num3, 0, source.format);
		renderTexture.filterMode = 1;
		Graphics.Blit(source, renderTexture);
		for (int i = 0; i < this.blurIterations; i++)
		{
			float num4 = (float)i * 1f;
			this.blurMaterial.SetFloat("_Parameter", this.blurSize * num + num4);
			RenderTexture temporary = RenderTexture.GetTemporary(num2, num3, 0, source.format);
			temporary.filterMode = 1;
			Graphics.Blit(renderTexture, temporary, this.blurMaterial, 0);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
			temporary = RenderTexture.GetTemporary(num2, num3, 0, source.format);
			temporary.filterMode = 1;
			Graphics.Blit(renderTexture, temporary, this.blurMaterial, 1);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		Graphics.Blit(renderTexture, destination);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x0400272C RID: 10028
	public int downsample = 2;

	// Token: 0x0400272D RID: 10029
	public float blurSize = 3.5f;

	// Token: 0x0400272E RID: 10030
	public int blurIterations = 1;

	// Token: 0x0400272F RID: 10031
	public Shader blurShader;

	// Token: 0x04002730 RID: 10032
	private Material blurMaterial;

	// Token: 0x04002731 RID: 10033
	private float widthOffset;

	// Token: 0x04002732 RID: 10034
	private float heightOffset;
}
