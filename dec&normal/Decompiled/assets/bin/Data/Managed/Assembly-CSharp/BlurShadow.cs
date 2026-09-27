using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class BlurShadow : MonoBehaviour
{
	// Token: 0x170003AA RID: 938
	// (get) Token: 0x06001168 RID: 4456 RVA: 0x00070DFC File Offset: 0x0006EFFC
	public Material blurMaterial
	{
		get
		{
			if (this.mBlurMaterial == null)
			{
				this.mBlurMaterial = new Material(this.blurShader);
				this.mBlurMaterial.hideFlags = 13;
			}
			return this.mBlurMaterial;
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x06001169 RID: 4457 RVA: 0x00070E34 File Offset: 0x0006F034
	private Material shadowMaterial
	{
		get
		{
			if (this.m_ShadowMaterial == null)
			{
				this.m_ShadowMaterial = new Material(BlurShadow.shadowMatString);
				this.m_ShadowMaterial.shader.hideFlags = 13;
				this.m_ShadowMaterial.hideFlags = 13;
			}
			return this.m_ShadowMaterial;
		}
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x00070E88 File Offset: 0x0006F088
	private void Start()
	{
		this.blurShader = Shader.Find("Hidden/OutterLineShapeBlur");
		base.camera.SetReplacementShader(this.shadowMaterial.shader, null);
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x00070EBC File Offset: 0x0006F0BC
	private void OnDisable()
	{
		if (this.blurMaterial)
		{
			Object.DestroyImmediate(this.blurMaterial);
		}
		if (this.shadowMaterial)
		{
			Object.DestroyImmediate(this.shadowMaterial);
		}
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x00070F00 File Offset: 0x0006F100
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.Spread;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(num, num),
			new Vector2(-num, num),
			new Vector2(num, -num),
			new Vector2(-num, -num)
		});
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x00070F80 File Offset: 0x0006F180
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x00070FF8 File Offset: 0x0006F1F8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.Iterations = Mathf.Clamp(this.Iterations, 0, 15);
		this.Spread = Mathf.Clamp(this.Spread, 0.5f, 6f);
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
		bool flag = true;
		Graphics.Blit(source, temporary);
		for (int i = 0; i < this.Iterations; i++)
		{
			if (flag)
			{
				this.FourTapCone(temporary, temporary2, i);
			}
			else
			{
				this.FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		if (flag)
		{
			Graphics.Blit(temporary, destination);
		}
		else
		{
			Graphics.Blit(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04001743 RID: 5955
	public int Iterations = 2;

	// Token: 0x04001744 RID: 5956
	public float Spread = 0.7f;

	// Token: 0x04001745 RID: 5957
	private Material mBlurMaterial;

	// Token: 0x04001746 RID: 5958
	public Shader blurShader;

	// Token: 0x04001747 RID: 5959
	private static string shadowMatString = "Shader \"Hidden/ShadowMat\" {\n\tProperties {\n\t\t_ShadowLightness1 (\"_ShadowLightnesszz\", Color) = (0.5,0.5,0.5,0.1)\n\t}\n\tSubShader {\n\t\tPass {\n\t\t\tColor(0.12,0.12,0.12,0)    \n\t\t}\n\t}\n\tFallback off\n}";

	// Token: 0x04001748 RID: 5960
	private Material m_ShadowMaterial;
}
