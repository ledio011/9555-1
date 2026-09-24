using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Screen Space Ambient Occlusion")]
[RequireComponent(typeof(Camera))]
public class SSAOEffect : MonoBehaviour
{
	// Token: 0x06000267 RID: 615 RVA: 0x0000A6CC File Offset: 0x000088CC
	private static Material CreateMaterial(Shader shader)
	{
		if (!shader)
		{
			return null;
		}
		return new Material(shader)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000A6F8 File Offset: 0x000088F8
	private static void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000A710 File Offset: 0x00008910
	private void OnDisable()
	{
		SSAOEffect.DestroyMaterial(this.m_SSAOMaterial);
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000A720 File Offset: 0x00008920
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.m_Supported = false;
			base.enabled = false;
			return;
		}
		this.CreateMaterials();
		if (!this.m_SSAOMaterial || this.m_SSAOMaterial.passCount != 5)
		{
			this.m_Supported = false;
			base.enabled = false;
			return;
		}
		this.m_Supported = true;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000A790 File Offset: 0x00008990
	private void OnEnable()
	{
		base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000A7A8 File Offset: 0x000089A8
	private void CreateMaterials()
	{
		if (!this.m_SSAOMaterial && this.m_SSAOShader.isSupported)
		{
			this.m_SSAOMaterial = SSAOEffect.CreateMaterial(this.m_SSAOShader);
			this.m_SSAOMaterial.SetTexture("_RandomTexture", this.m_RandomTexture);
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000A7FC File Offset: 0x000089FC
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.m_Supported || !this.m_SSAOShader.isSupported)
		{
			base.enabled = false;
			return;
		}
		this.CreateMaterials();
		this.m_Downsampling = Mathf.Clamp(this.m_Downsampling, 1, 6);
		this.m_Radius = Mathf.Clamp(this.m_Radius, 0.05f, 1f);
		this.m_MinZ = Mathf.Clamp(this.m_MinZ, 1E-05f, 0.5f);
		this.m_OcclusionIntensity = Mathf.Clamp(this.m_OcclusionIntensity, 0.5f, 4f);
		this.m_OcclusionAttenuation = Mathf.Clamp(this.m_OcclusionAttenuation, 0.2f, 2f);
		this.m_Blur = Mathf.Clamp(this.m_Blur, 0, 4);
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / this.m_Downsampling, source.height / this.m_Downsampling, 0);
		float fieldOfView = base.camera.fieldOfView;
		float farClipPlane = base.camera.farClipPlane;
		float num = Mathf.Tan(fieldOfView * 0.017453292f * 0.5f) * farClipPlane;
		float x = num * base.camera.aspect;
		this.m_SSAOMaterial.SetVector("_FarCorner", new Vector3(x, num, farClipPlane));
		int num2;
		int num3;
		if (this.m_RandomTexture)
		{
			num2 = this.m_RandomTexture.width;
			num3 = this.m_RandomTexture.height;
		}
		else
		{
			num2 = 1;
			num3 = 1;
		}
		this.m_SSAOMaterial.SetVector("_NoiseScale", new Vector3((float)renderTexture.width / (float)num2, (float)renderTexture.height / (float)num3, 0f));
		this.m_SSAOMaterial.SetVector("_Params", new Vector4(this.m_Radius, this.m_MinZ, 1f / this.m_OcclusionAttenuation, this.m_OcclusionIntensity));
		bool flag = this.m_Blur > 0;
		Graphics.Blit((!flag) ? source : null, renderTexture, this.m_SSAOMaterial, (int)this.m_SampleCount);
		if (flag)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4((float)this.m_Blur / (float)source.width, 0f, 0f, 0f));
			this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(null, temporary, this.m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(renderTexture);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
			this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4(0f, (float)this.m_Blur / (float)source.height, 0f, 0f));
			this.m_SSAOMaterial.SetTexture("_SSAO", temporary);
			Graphics.Blit(source, temporary2, this.m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(temporary);
			renderTexture = temporary2;
		}
		this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
		Graphics.Blit(source, destination, this.m_SSAOMaterial, 4);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x040001DE RID: 478
	public float m_Radius = 0.4f;

	// Token: 0x040001DF RID: 479
	public SSAOEffect.SSAOSamples m_SampleCount = SSAOEffect.SSAOSamples.Medium;

	// Token: 0x040001E0 RID: 480
	public float m_OcclusionIntensity = 1.5f;

	// Token: 0x040001E1 RID: 481
	public int m_Blur = 2;

	// Token: 0x040001E2 RID: 482
	public int m_Downsampling = 2;

	// Token: 0x040001E3 RID: 483
	public float m_OcclusionAttenuation = 1f;

	// Token: 0x040001E4 RID: 484
	public float m_MinZ = 0.01f;

	// Token: 0x040001E5 RID: 485
	public Shader m_SSAOShader;

	// Token: 0x040001E6 RID: 486
	private Material m_SSAOMaterial;

	// Token: 0x040001E7 RID: 487
	public Texture2D m_RandomTexture;

	// Token: 0x040001E8 RID: 488
	private bool m_Supported;

	// Token: 0x0200005C RID: 92
	public enum SSAOSamples
	{
		// Token: 0x040001EA RID: 490
		Low,
		// Token: 0x040001EB RID: 491
		Medium,
		// Token: 0x040001EC RID: 492
		High
	}
}
