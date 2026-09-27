using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Motion Blur (Color Accumulation)")]
public class MotionBlur : ImageEffectBase
{
	// Token: 0x0600025D RID: 605 RVA: 0x0000A0F0 File Offset: 0x000082F0
	protected override void Start()
	{
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
			return;
		}
		base.Start();
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000A10C File Offset: 0x0000830C
	protected override void OnDisable()
	{
		base.OnDisable();
		UnityEngine.Object.DestroyImmediate(this.accumTexture);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000A120 File Offset: 0x00008320
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.accumTexture == null || this.accumTexture.width != source.width || this.accumTexture.height != source.height)
		{
			UnityEngine.Object.DestroyImmediate(this.accumTexture);
			this.accumTexture = new RenderTexture(source.width, source.height, 0);
			this.accumTexture.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit(source, this.accumTexture);
		}
		if (this.extraBlur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
			Graphics.Blit(this.accumTexture, temporary);
			Graphics.Blit(temporary, this.accumTexture);
			RenderTexture.ReleaseTemporary(temporary);
		}
		this.blurAmount = Mathf.Clamp(this.blurAmount, 0f, 0.92f);
		base.material.SetTexture("_MainTex", this.accumTexture);
		base.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
		Graphics.Blit(source, this.accumTexture, base.material);
		Graphics.Blit(this.accumTexture, destination);
	}

	// Token: 0x040001C9 RID: 457
	public float blurAmount = 0.8f;

	// Token: 0x040001CA RID: 458
	public bool extraBlur;

	// Token: 0x040001CB RID: 459
	private RenderTexture accumTexture;
}
