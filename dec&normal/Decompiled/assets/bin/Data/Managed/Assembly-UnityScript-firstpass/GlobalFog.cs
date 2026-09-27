using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Global Fog")]
[Serializable]
public class GlobalFog : PostEffectsBase
{
	// Token: 0x0600008F RID: 143 RVA: 0x00007974 File Offset: 0x00005B74
	public GlobalFog()
	{
		this.fogMode = GlobalFog.FogMode.AbsoluteYAndDistance;
		this.CAMERA_NEAR = 0.5f;
		this.CAMERA_FAR = 50f;
		this.CAMERA_FOV = 60f;
		this.CAMERA_ASPECT_RATIO = 1.333333f;
		this.startDistance = 200f;
		this.globalDensity = 1f;
		this.heightScale = 100f;
		this.globalFogColor = Color.grey;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000079E8 File Offset: 0x00005BE8
	public virtual void OnDisable()
	{
		if (this.fogMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.fogMaterial);
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00007A08 File Offset: 0x00005C08
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.fogMaterial = this.CheckShaderAndCreateMaterial(this.fogShader, this.fogMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00007A44 File Offset: 0x00005C44
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.CAMERA_NEAR = this.camera.nearClipPlane;
			this.CAMERA_FAR = this.camera.farClipPlane;
			this.CAMERA_FOV = this.camera.fieldOfView;
			this.CAMERA_ASPECT_RATIO = this.camera.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			Vector4 vector = default(Vector4);
			Vector3 vector2 = default(Vector3);
			float num = this.CAMERA_FOV * 0.5f;
			Vector3 b = this.camera.transform.right * this.CAMERA_NEAR * Mathf.Tan(num * 0.017453292f) * this.CAMERA_ASPECT_RATIO;
			Vector3 b2 = this.camera.transform.up * this.CAMERA_NEAR * Mathf.Tan(num * 0.017453292f);
			Vector3 vector3 = this.camera.transform.forward * this.CAMERA_NEAR - b + b2;
			float num2 = vector3.magnitude * this.CAMERA_FAR / this.CAMERA_NEAR;
			vector3.Normalize();
			vector3 *= num2;
			Vector3 vector4 = this.camera.transform.forward * this.CAMERA_NEAR + b + b2;
			vector4.Normalize();
			vector4 *= num2;
			Vector3 vector5 = this.camera.transform.forward * this.CAMERA_NEAR + b - b2;
			vector5.Normalize();
			vector5 *= num2;
			Vector3 vector6 = this.camera.transform.forward * this.CAMERA_NEAR - b - b2;
			vector6.Normalize();
			vector6 *= num2;
			identity.SetRow(0, vector3);
			identity.SetRow(1, vector4);
			identity.SetRow(2, vector5);
			identity.SetRow(3, vector6);
			this.fogMaterial.SetMatrix("_FrustumCornersWS", identity);
			this.fogMaterial.SetVector("_CameraWS", this.camera.transform.position);
			this.fogMaterial.SetVector("_StartDistance", new Vector4(1f / this.startDistance, num2 - this.startDistance));
			this.fogMaterial.SetVector("_Y", new Vector4(this.height, 1f / this.heightScale));
			this.fogMaterial.SetFloat("_GlobalDensity", this.globalDensity * 0.01f);
			this.fogMaterial.SetColor("_FogColor", this.globalFogColor);
			GlobalFog.CustomGraphicsBlit(source, destination, this.fogMaterial, (int)this.fogMode);
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00007D4C File Offset: 0x00005F4C
	public static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
	{
		RenderTexture.active = dest;
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		GL.MultiTexCoord2(0, (float)0, (float)0);
		GL.Vertex3((float)0, (float)0, 3f);
		GL.MultiTexCoord2(0, 1f, (float)0);
		GL.Vertex3(1f, (float)0, 2f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 1f);
		GL.MultiTexCoord2(0, (float)0, 1f);
		GL.Vertex3((float)0, 1f, (float)0);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00007E04 File Offset: 0x00006004
	public override void Main()
	{
	}

	// Token: 0x04000157 RID: 343
	public GlobalFog.FogMode fogMode;

	// Token: 0x04000158 RID: 344
	private float CAMERA_NEAR;

	// Token: 0x04000159 RID: 345
	private float CAMERA_FAR;

	// Token: 0x0400015A RID: 346
	private float CAMERA_FOV;

	// Token: 0x0400015B RID: 347
	private float CAMERA_ASPECT_RATIO;

	// Token: 0x0400015C RID: 348
	public float startDistance;

	// Token: 0x0400015D RID: 349
	public float globalDensity;

	// Token: 0x0400015E RID: 350
	public float heightScale;

	// Token: 0x0400015F RID: 351
	public float height;

	// Token: 0x04000160 RID: 352
	public Color globalFogColor;

	// Token: 0x04000161 RID: 353
	public Shader fogShader;

	// Token: 0x04000162 RID: 354
	private Material fogMaterial;

	// Token: 0x02000027 RID: 39
	[Serializable]
	public enum FogMode
	{
		// Token: 0x04000164 RID: 356
		AbsoluteYAndDistance,
		// Token: 0x04000165 RID: 357
		AbsoluteY,
		// Token: 0x04000166 RID: 358
		Distance,
		// Token: 0x04000167 RID: 359
		RelativeYAndDistance
	}
}
