using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[AddComponentMenu("Image Effects/Contrast Stretch")]
[ExecuteInEditMode]
public class ContrastStretchEffect : MonoBehaviour
{
	// Token: 0x1700007D RID: 125
	// (get) Token: 0x0600023D RID: 573 RVA: 0x000096B4 File Offset: 0x000078B4
	protected Material materialLum
	{
		get
		{
			if (this.m_materialLum == null)
			{
				this.m_materialLum = new Material(this.shaderLum);
				this.m_materialLum.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialLum;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x0600023E RID: 574 RVA: 0x000096EC File Offset: 0x000078EC
	protected Material materialReduce
	{
		get
		{
			if (this.m_materialReduce == null)
			{
				this.m_materialReduce = new Material(this.shaderReduce);
				this.m_materialReduce.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialReduce;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600023F RID: 575 RVA: 0x00009724 File Offset: 0x00007924
	protected Material materialAdapt
	{
		get
		{
			if (this.m_materialAdapt == null)
			{
				this.m_materialAdapt = new Material(this.shaderAdapt);
				this.m_materialAdapt.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialAdapt;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000240 RID: 576 RVA: 0x0000975C File Offset: 0x0000795C
	protected Material materialApply
	{
		get
		{
			if (this.m_materialApply == null)
			{
				this.m_materialApply = new Material(this.shaderApply);
				this.m_materialApply.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialApply;
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00009794 File Offset: 0x00007994
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shaderAdapt.isSupported || !this.shaderApply.isSupported || !this.shaderLum.isSupported || !this.shaderReduce.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000242 RID: 578 RVA: 0x000097FC File Offset: 0x000079FC
	private void OnEnable()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!this.adaptRenderTex[i])
			{
				this.adaptRenderTex[i] = new RenderTexture(1, 1, 32);
				this.adaptRenderTex[i].hideFlags = HideFlags.HideAndDontSave;
			}
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00009850 File Offset: 0x00007A50
	private void OnDisable()
	{
		for (int i = 0; i < 2; i++)
		{
			UnityEngine.Object.DestroyImmediate(this.adaptRenderTex[i]);
			this.adaptRenderTex[i] = null;
		}
		if (this.m_materialLum)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialLum);
		}
		if (this.m_materialReduce)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialReduce);
		}
		if (this.m_materialAdapt)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialAdapt);
		}
		if (this.m_materialApply)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialApply);
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x000098F4 File Offset: 0x00007AF4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / 1, source.height / 1);
		Graphics.Blit(source, renderTexture, this.materialLum);
		while (renderTexture.width > 1 || renderTexture.height > 1)
		{
			int num = renderTexture.width / 2;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = renderTexture.height / 2;
			if (num2 < 1)
			{
				num2 = 1;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
			Graphics.Blit(renderTexture, temporary, this.materialReduce);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		this.CalculateAdaptation(renderTexture);
		this.materialApply.SetTexture("_AdaptTex", this.adaptRenderTex[this.curAdaptIndex]);
		Graphics.Blit(source, destination, this.materialApply);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x000099C4 File Offset: 0x00007BC4
	private void CalculateAdaptation(Texture curTexture)
	{
		int num = this.curAdaptIndex;
		this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
		float num2 = 1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime);
		num2 = Mathf.Clamp(num2, 0.01f, 1f);
		this.materialAdapt.SetTexture("_CurTex", curTexture);
		this.materialAdapt.SetVector("_AdaptParams", new Vector4(num2, this.limitMinimum, this.limitMaximum, 0f));
		Graphics.Blit(this.adaptRenderTex[num], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
	}

	// Token: 0x040001AD RID: 429
	public float adaptationSpeed = 0.02f;

	// Token: 0x040001AE RID: 430
	public float limitMinimum = 0.2f;

	// Token: 0x040001AF RID: 431
	public float limitMaximum = 0.6f;

	// Token: 0x040001B0 RID: 432
	private RenderTexture[] adaptRenderTex = new RenderTexture[2];

	// Token: 0x040001B1 RID: 433
	private int curAdaptIndex;

	// Token: 0x040001B2 RID: 434
	public Shader shaderLum;

	// Token: 0x040001B3 RID: 435
	private Material m_materialLum;

	// Token: 0x040001B4 RID: 436
	public Shader shaderReduce;

	// Token: 0x040001B5 RID: 437
	private Material m_materialReduce;

	// Token: 0x040001B6 RID: 438
	public Shader shaderAdapt;

	// Token: 0x040001B7 RID: 439
	private Material m_materialAdapt;

	// Token: 0x040001B8 RID: 440
	public Shader shaderApply;

	// Token: 0x040001B9 RID: 441
	private Material m_materialApply;
}
