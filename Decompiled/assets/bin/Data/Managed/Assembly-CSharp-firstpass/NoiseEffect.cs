using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Noise")]
public class NoiseEffect : MonoBehaviour
{
	// Token: 0x06000261 RID: 609 RVA: 0x0000A2BC File Offset: 0x000084BC
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.shaderRGB == null || this.shaderYUV == null)
		{
			Debug.Log("Noise shaders are not set up! Disabling noise effect.");
			base.enabled = false;
		}
		else if (!this.shaderRGB.isSupported)
		{
			base.enabled = false;
		}
		else if (!this.shaderYUV.isSupported)
		{
			this.rgbFallback = true;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A348 File Offset: 0x00008548
	protected Material material
	{
		get
		{
			if (this.m_MaterialRGB == null)
			{
				this.m_MaterialRGB = new Material(this.shaderRGB);
				this.m_MaterialRGB.hideFlags = HideFlags.HideAndDontSave;
			}
			if (this.m_MaterialYUV == null && !this.rgbFallback)
			{
				this.m_MaterialYUV = new Material(this.shaderYUV);
				this.m_MaterialYUV.hideFlags = HideFlags.HideAndDontSave;
			}
			return (this.rgbFallback || this.monochrome) ? this.m_MaterialRGB : this.m_MaterialYUV;
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000A3E8 File Offset: 0x000085E8
	protected void OnDisable()
	{
		if (this.m_MaterialRGB)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialRGB);
		}
		if (this.m_MaterialYUV)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialYUV);
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000A42C File Offset: 0x0000862C
	private void SanitizeParameters()
	{
		this.grainIntensityMin = Mathf.Clamp(this.grainIntensityMin, 0f, 5f);
		this.grainIntensityMax = Mathf.Clamp(this.grainIntensityMax, 0f, 5f);
		this.scratchIntensityMin = Mathf.Clamp(this.scratchIntensityMin, 0f, 5f);
		this.scratchIntensityMax = Mathf.Clamp(this.scratchIntensityMax, 0f, 5f);
		this.scratchFPS = Mathf.Clamp(this.scratchFPS, 1f, 30f);
		this.scratchJitter = Mathf.Clamp(this.scratchJitter, 0f, 1f);
		this.grainSize = Mathf.Clamp(this.grainSize, 0.1f, 50f);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000A4F8 File Offset: 0x000086F8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SanitizeParameters();
		if (this.scratchTimeLeft <= 0f)
		{
			this.scratchTimeLeft = UnityEngine.Random.value * 2f / this.scratchFPS;
			this.scratchX = UnityEngine.Random.value;
			this.scratchY = UnityEngine.Random.value;
		}
		this.scratchTimeLeft -= Time.deltaTime;
		Material material = this.material;
		material.SetTexture("_GrainTex", this.grainTexture);
		material.SetTexture("_ScratchTex", this.scratchTexture);
		float num = 1f / this.grainSize;
		material.SetVector("_GrainOffsetScale", new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, (float)Screen.width / (float)this.grainTexture.width * num, (float)Screen.height / (float)this.grainTexture.height * num));
		material.SetVector("_ScratchOffsetScale", new Vector4(this.scratchX + UnityEngine.Random.value * this.scratchJitter, this.scratchY + UnityEngine.Random.value * this.scratchJitter, (float)Screen.width / (float)this.scratchTexture.width, (float)Screen.height / (float)this.scratchTexture.height));
		material.SetVector("_Intensity", new Vector4(UnityEngine.Random.Range(this.grainIntensityMin, this.grainIntensityMax), UnityEngine.Random.Range(this.scratchIntensityMin, this.scratchIntensityMax), 0f, 0f));
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x040001CC RID: 460
	public bool monochrome = true;

	// Token: 0x040001CD RID: 461
	private bool rgbFallback;

	// Token: 0x040001CE RID: 462
	public float grainIntensityMin = 0.1f;

	// Token: 0x040001CF RID: 463
	public float grainIntensityMax = 0.2f;

	// Token: 0x040001D0 RID: 464
	public float grainSize = 2f;

	// Token: 0x040001D1 RID: 465
	public float scratchIntensityMin = 0.05f;

	// Token: 0x040001D2 RID: 466
	public float scratchIntensityMax = 0.25f;

	// Token: 0x040001D3 RID: 467
	public float scratchFPS = 10f;

	// Token: 0x040001D4 RID: 468
	public float scratchJitter = 0.01f;

	// Token: 0x040001D5 RID: 469
	public Texture grainTexture;

	// Token: 0x040001D6 RID: 470
	public Texture scratchTexture;

	// Token: 0x040001D7 RID: 471
	public Shader shaderRGB;

	// Token: 0x040001D8 RID: 472
	public Shader shaderYUV;

	// Token: 0x040001D9 RID: 473
	private Material m_MaterialRGB;

	// Token: 0x040001DA RID: 474
	private Material m_MaterialYUV;

	// Token: 0x040001DB RID: 475
	private float scratchTimeLeft;

	// Token: 0x040001DC RID: 476
	private float scratchX;

	// Token: 0x040001DD RID: 477
	private float scratchY;
}
