using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Tonemapping")]
[Serializable]
public class Tonemapping : PostEffectsBase
{
	// Token: 0x060000C7 RID: 199 RVA: 0x00009ADC File Offset: 0x00007CDC
	public Tonemapping()
	{
		this.type = Tonemapping.TonemapperType.SimpleReinhard;
		this.adaptiveTextureSize = Tonemapping.AdaptiveTexSize.Square256;
		this.exposureAdjustment = 1.5f;
		this.middleGrey = 0.4f;
		this.white = 2f;
		this.adaptionSpeed = 1.5f;
		this.validRenderTextureFormat = true;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00009B34 File Offset: 0x00007D34
	public virtual void OnDisable()
	{
		if (this.tonemapMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.tonemapMaterial);
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00009B54 File Offset: 0x00007D54
	public override bool CheckResources()
	{
		this.CheckSupport(false, true);
		this.tonemapMaterial = this.CheckShaderAndCreateMaterial(this.tonemapper, this.tonemapMaterial);
		if (!this.curveTex && this.type == Tonemapping.TonemapperType.UserCurve)
		{
			this.curveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
			this.curveTex.filterMode = FilterMode.Bilinear;
			this.curveTex.wrapMode = TextureWrapMode.Clamp;
			this.curveTex.hideFlags = HideFlags.DontSave;
		}
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00009BF0 File Offset: 0x00007DF0
	public virtual float UpdateCurve()
	{
		float num = 1f;
		if (this.remapCurve == null)
		{
			this.remapCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe((float)0, (float)0),
				new Keyframe((float)2, (float)1)
			});
		}
		if (this.remapCurve != null)
		{
			if (this.remapCurve.length != 0)
			{
				num = this.remapCurve[this.remapCurve.length - 1].time;
			}
			for (float num2 = (float)0; num2 <= 1f; num2 += 0.003921569f)
			{
				float num3 = this.remapCurve.Evaluate(num2 * 1f * num);
				this.curveTex.SetPixel((int)Mathf.Floor(num2 * 255f), 0, new Color(num3, num3, num3));
			}
			this.curveTex.Apply();
		}
		return 1f / num;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00009CE8 File Offset: 0x00007EE8
	public virtual bool CreateInternalRenderTexture()
	{
		bool result;
		if (this.rt)
		{
			result = false;
		}
		else
		{
			this.rt = new RenderTexture(1, 1, 0, RenderTextureFormat.ARGBHalf);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = this.rt;
			GL.Clear(false, true, Color.white);
			this.rt.hideFlags = HideFlags.DontSave;
			RenderTexture.active = active;
			result = true;
		}
		return result;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00009D4C File Offset: 0x00007F4C
	[ImageEffectTransformsToLDR]
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.exposureAdjustment = ((this.exposureAdjustment >= 0.001f) ? this.exposureAdjustment : 0.001f);
			if (this.type == Tonemapping.TonemapperType.UserCurve)
			{
				float value = this.UpdateCurve();
				this.tonemapMaterial.SetFloat("_RangeScale", value);
				this.tonemapMaterial.SetTexture("_Curve", this.curveTex);
				Graphics.Blit(source, destination, this.tonemapMaterial, 4);
			}
			else if (this.type == Tonemapping.TonemapperType.SimpleReinhard)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 6);
			}
			else if (this.type == Tonemapping.TonemapperType.Hable)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 5);
			}
			else if (this.type == Tonemapping.TonemapperType.Photographic)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 8);
			}
			else if (this.type == Tonemapping.TonemapperType.OptimizedHejiDawson)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", 0.5f * this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 7);
			}
			else
			{
				bool flag = this.CreateInternalRenderTexture();
				RenderTexture temporary = RenderTexture.GetTemporary((int)this.adaptiveTextureSize, (int)this.adaptiveTextureSize, 0, RenderTextureFormat.ARGBHalf);
				Graphics.Blit(source, temporary);
				int num = (int)Mathf.Log((float)temporary.width * 1f, (float)2);
				int num2 = 2;
				RenderTexture[] array = new RenderTexture[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = RenderTexture.GetTemporary(temporary.width / num2, temporary.width / num2, 0, RenderTextureFormat.ARGBHalf);
					num2 *= 2;
				}
				float num3 = (float)source.width * 1f / ((float)source.height * 1f);
				RenderTexture source2 = array[num - 1];
				Graphics.Blit(temporary, array[0], this.tonemapMaterial, 1);
				if (this.type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
				{
					for (int i = 0; i < num - 1; i++)
					{
						Graphics.Blit(array[i], array[i + 1], this.tonemapMaterial, 9);
						source2 = array[i + 1];
					}
				}
				else if (this.type == Tonemapping.TonemapperType.AdaptiveReinhard)
				{
					for (int i = 0; i < num - 1; i++)
					{
						Graphics.Blit(array[i], array[i + 1]);
						source2 = array[i + 1];
					}
				}
				this.adaptionSpeed = ((this.adaptionSpeed >= 0.001f) ? this.adaptionSpeed : 0.001f);
				this.tonemapMaterial.SetFloat("_AdaptionSpeed", this.adaptionSpeed);
				Graphics.Blit(source2, this.rt, this.tonemapMaterial, 2);
				this.middleGrey = ((this.middleGrey >= 0.001f) ? this.middleGrey : 0.001f);
				this.tonemapMaterial.SetVector("_HdrParams", new Vector4(this.middleGrey, this.middleGrey, this.middleGrey, this.white * this.white));
				this.tonemapMaterial.SetTexture("_SmallTex", this.rt);
				if (this.type == Tonemapping.TonemapperType.AdaptiveReinhard)
				{
					Graphics.Blit(source, destination, this.tonemapMaterial, 0);
				}
				else if (this.type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
				{
					Graphics.Blit(source, destination, this.tonemapMaterial, 10);
				}
				else
				{
					Debug.LogError("No valid adaptive tonemapper type found!");
					Graphics.Blit(source, destination);
				}
				for (int i = 0; i < num; i++)
				{
					RenderTexture.ReleaseTemporary(array[i]);
				}
				RenderTexture.ReleaseTemporary(temporary);
			}
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000A130 File Offset: 0x00008330
	public override void Main()
	{
	}

	// Token: 0x040001A7 RID: 423
	public Tonemapping.TonemapperType type;

	// Token: 0x040001A8 RID: 424
	public Tonemapping.AdaptiveTexSize adaptiveTextureSize;

	// Token: 0x040001A9 RID: 425
	public AnimationCurve remapCurve;

	// Token: 0x040001AA RID: 426
	private Texture2D curveTex;

	// Token: 0x040001AB RID: 427
	public float exposureAdjustment;

	// Token: 0x040001AC RID: 428
	public float middleGrey;

	// Token: 0x040001AD RID: 429
	public float white;

	// Token: 0x040001AE RID: 430
	public float adaptionSpeed;

	// Token: 0x040001AF RID: 431
	public Shader tonemapper;

	// Token: 0x040001B0 RID: 432
	public bool validRenderTextureFormat;

	// Token: 0x040001B1 RID: 433
	private Material tonemapMaterial;

	// Token: 0x040001B2 RID: 434
	private RenderTexture rt;

	// Token: 0x02000033 RID: 51
	[Serializable]
	public enum TonemapperType
	{
		// Token: 0x040001B4 RID: 436
		SimpleReinhard,
		// Token: 0x040001B5 RID: 437
		UserCurve,
		// Token: 0x040001B6 RID: 438
		Hable,
		// Token: 0x040001B7 RID: 439
		Photographic,
		// Token: 0x040001B8 RID: 440
		OptimizedHejiDawson,
		// Token: 0x040001B9 RID: 441
		AdaptiveReinhard,
		// Token: 0x040001BA RID: 442
		AdaptiveReinhardAutoWhite
	}

	// Token: 0x02000034 RID: 52
	[Serializable]
	public enum AdaptiveTexSize
	{
		// Token: 0x040001BC RID: 444
		Square16 = 16,
		// Token: 0x040001BD RID: 445
		Square32 = 32,
		// Token: 0x040001BE RID: 446
		Square64 = 64,
		// Token: 0x040001BF RID: 447
		Square128 = 128,
		// Token: 0x040001C0 RID: 448
		Square256 = 256,
		// Token: 0x040001C1 RID: 449
		Square512 = 512,
		// Token: 0x040001C2 RID: 450
		Square1024 = 1024
	}
}
