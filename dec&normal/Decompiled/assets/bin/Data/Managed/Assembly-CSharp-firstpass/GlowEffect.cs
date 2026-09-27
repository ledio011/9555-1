using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Glow")]
[ExecuteInEditMode]
public class GlowEffect : MonoBehaviour
{
	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000249 RID: 585 RVA: 0x00009B10 File Offset: 0x00007D10
	protected Material compositeMaterial
	{
		get
		{
			if (this.m_CompositeMaterial == null)
			{
				this.m_CompositeMaterial = new Material(this.compositeShader);
				this.m_CompositeMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_CompositeMaterial;
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600024A RID: 586 RVA: 0x00009B48 File Offset: 0x00007D48
	protected Material blurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				this.m_BlurMaterial = new Material(this.blurShader);
				this.m_BlurMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_BlurMaterial;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x0600024B RID: 587 RVA: 0x00009B80 File Offset: 0x00007D80
	protected Material downsampleMaterial
	{
		get
		{
			if (this.m_DownsampleMaterial == null)
			{
				this.m_DownsampleMaterial = new Material(this.downsampleShader);
				this.m_DownsampleMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_DownsampleMaterial;
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00009BB8 File Offset: 0x00007DB8
	protected void OnDisable()
	{
		if (this.m_CompositeMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.m_CompositeMaterial);
		}
		if (this.m_BlurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.m_BlurMaterial);
		}
		if (this.m_DownsampleMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.m_DownsampleMaterial);
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00009C18 File Offset: 0x00007E18
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.downsampleShader == null)
		{
			Debug.Log("No downsample shader assigned! Disabling glow.");
			base.enabled = false;
		}
		else
		{
			if (!this.blurMaterial.shader.isSupported)
			{
				base.enabled = false;
			}
			if (!this.compositeMaterial.shader.isSupported)
			{
				base.enabled = false;
			}
			if (!this.downsampleMaterial.shader.isSupported)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00009CB4 File Offset: 0x00007EB4
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(num, num),
			new Vector2(-num, num),
			new Vector2(num, -num),
			new Vector2(-num, -num)
		});
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00009D34 File Offset: 0x00007F34
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		this.downsampleMaterial.color = new Color(this.glowTint.r, this.glowTint.g, this.glowTint.b, this.glowTint.a / 4f);
		Graphics.Blit(source, dest, this.downsampleMaterial);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00009D90 File Offset: 0x00007F90
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.glowIntensity = Mathf.Clamp(this.glowIntensity, 0f, 10f);
		this.blurIterations = Mathf.Clamp(this.blurIterations, 0, 30);
		this.blurSpread = Mathf.Clamp(this.blurSpread, 0.5f, 1f);
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(source, temporary);
		float num = Mathf.Clamp01((this.glowIntensity - 1f) / 4f);
		this.blurMaterial.color = new Color(1f, 1f, 1f, 0.25f + num);
		bool flag = true;
		for (int i = 0; i < this.blurIterations; i++)
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
		Graphics.Blit(source, destination);
		if (flag)
		{
			this.BlitGlow(temporary, destination);
		}
		else
		{
			this.BlitGlow(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00009ECC File Offset: 0x000080CC
	public void BlitGlow(RenderTexture source, RenderTexture dest)
	{
		this.compositeMaterial.color = new Color(1f, 1f, 1f, Mathf.Clamp01(this.glowIntensity));
		Graphics.Blit(source, dest, this.compositeMaterial);
	}

	// Token: 0x040001BB RID: 443
	public float glowIntensity = 1.5f;

	// Token: 0x040001BC RID: 444
	public int blurIterations = 3;

	// Token: 0x040001BD RID: 445
	public float blurSpread = 0.7f;

	// Token: 0x040001BE RID: 446
	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	// Token: 0x040001BF RID: 447
	public Shader compositeShader;

	// Token: 0x040001C0 RID: 448
	private Material m_CompositeMaterial;

	// Token: 0x040001C1 RID: 449
	public Shader blurShader;

	// Token: 0x040001C2 RID: 450
	private Material m_BlurMaterial;

	// Token: 0x040001C3 RID: 451
	public Shader downsampleShader;

	// Token: 0x040001C4 RID: 452
	private Material m_DownsampleMaterial;
}
