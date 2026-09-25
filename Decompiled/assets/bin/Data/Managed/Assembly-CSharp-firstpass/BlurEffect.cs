using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[AddComponentMenu("Image Effects/Blur")]
[ExecuteInEditMode]
public class BlurEffect : MonoBehaviour
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000234 RID: 564 RVA: 0x00009400 File Offset: 0x00007600
	protected Material material
	{
		get
		{
			if (BlurEffect.m_Material == null)
			{
				BlurEffect.m_Material = new Material(this.blurShader);
				BlurEffect.m_Material.hideFlags = HideFlags.DontSave;
			}
			return BlurEffect.m_Material;
		}
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00009440 File Offset: 0x00007640
	protected void OnDisable()
	{
		if (BlurEffect.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(BlurEffect.m_Material);
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000945C File Offset: 0x0000765C
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x000094A8 File Offset: 0x000076A8
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00009528 File Offset: 0x00007728
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000095A0 File Offset: 0x000077A0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(source, temporary);
		bool flag = true;
		for (int i = 0; i < this.iterations; i++)
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

	// Token: 0x040001A8 RID: 424
	public int iterations = 3;

	// Token: 0x040001A9 RID: 425
	public float blurSpread = 0.6f;

	// Token: 0x040001AA RID: 426
	public Shader blurShader;

	// Token: 0x040001AB RID: 427
	private static Material m_Material;
}
