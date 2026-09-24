using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class PostEffectsBase : MonoBehaviour
{
	// Token: 0x0600009B RID: 155 RVA: 0x0000819C File Offset: 0x0000639C
	public PostEffectsBase()
	{
		this.supportHDRTextures = true;
		this.isSupported = true;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x000081B4 File Offset: 0x000063B4
	public virtual Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
	{
		Material result;
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			this.enabled = false;
			result = null;
		}
		else if (s.isSupported && m2Create && m2Create.shader == s)
		{
			result = m2Create;
		}
		else if (!s.isSupported)
		{
			this.NotSupported();
			Debug.LogError("The shader " + s.ToString() + " on effect " + this.ToString() + " is not supported on this platform!");
			result = null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			result = ((!m2Create) ? null : m2Create);
		}
		return result;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00008290 File Offset: 0x00006490
	public virtual Material CreateMaterial(Shader s, Material m2Create)
	{
		Material result;
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			result = null;
		}
		else if (m2Create && m2Create.shader == s && s.isSupported)
		{
			result = m2Create;
		}
		else if (!s.isSupported)
		{
			result = null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			result = ((!m2Create) ? null : m2Create);
		}
		return result;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000832C File Offset: 0x0000652C
	public virtual void OnEnable()
	{
		this.isSupported = true;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00008338 File Offset: 0x00006538
	public virtual bool CheckSupport()
	{
		return this.CheckSupport(false);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00008344 File Offset: 0x00006544
	public virtual bool CheckResources()
	{
		Debug.LogWarning("CheckResources () for " + this.ToString() + " should be overwritten.");
		return this.isSupported;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000836C File Offset: 0x0000656C
	public virtual void Start()
	{
		this.CheckResources();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00008378 File Offset: 0x00006578
	public virtual bool CheckSupport(bool needDepth)
	{
		this.isSupported = true;
		this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		bool result;
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
		{
			this.NotSupported();
			result = false;
		}
		else if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.NotSupported();
			result = false;
		}
		else
		{
			if (needDepth)
			{
				this.camera.depthTextureMode = (this.camera.depthTextureMode | DepthTextureMode.Depth);
			}
			result = true;
		}
		return result;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000083F4 File Offset: 0x000065F4
	public virtual bool CheckSupport(bool needDepth, bool needHdr)
	{
		bool result;
		if (!this.CheckSupport(needDepth))
		{
			result = false;
		}
		else if (needHdr && !this.supportHDRTextures)
		{
			this.NotSupported();
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00008434 File Offset: 0x00006634
	public virtual void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00008458 File Offset: 0x00006658
	public virtual bool CheckShader(Shader s)
	{
		Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
		bool result;
		if (!s.isSupported)
		{
			this.NotSupported();
			result = false;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000084B8 File Offset: 0x000066B8
	public virtual void NotSupported()
	{
		this.enabled = false;
		this.isSupported = false;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000084C8 File Offset: 0x000066C8
	public virtual void DrawBorder(RenderTexture dest, Material material)
	{
		float x = 0f;
		float x2 = 0f;
		float y = 0f;
		float y2 = 0f;
		RenderTexture.active = dest;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float y3 = 0f;
			float y4 = 0f;
			if (flag)
			{
				y3 = 1f;
				y4 = (float)0;
			}
			else
			{
				y3 = (float)0;
				y4 = 1f;
			}
			x = (float)0;
			x2 = (float)0 + 1f / ((float)dest.width * 1f);
			y = (float)0;
			y2 = 1f;
			GL.Begin(7);
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			x = 1f - 1f / ((float)dest.width * 1f);
			x2 = 1f;
			y = (float)0;
			y2 = 1f;
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			x = (float)0;
			x2 = 1f;
			y = (float)0;
			y2 = (float)0 + 1f / ((float)dest.height * 1f);
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			x = (float)0;
			x2 = 1f;
			y = 1f - 1f / ((float)dest.height * 1f);
			y2 = 1f;
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00008770 File Offset: 0x00006970
	public virtual void Main()
	{
	}

	// Token: 0x04000175 RID: 373
	protected bool supportHDRTextures;

	// Token: 0x04000176 RID: 374
	protected bool isSupported;
}
