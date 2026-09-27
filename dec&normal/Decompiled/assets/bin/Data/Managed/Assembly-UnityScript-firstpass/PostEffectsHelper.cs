using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[Serializable]
public class PostEffectsHelper : MonoBehaviour
{
	// Token: 0x060000AA RID: 170 RVA: 0x0000877C File Offset: 0x0000697C
	public virtual void Start()
	{
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00008780 File Offset: 0x00006980
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Debug.Log("OnRenderImage in Helper called ...");
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000878C File Offset: 0x0000698C
	public static void DrawLowLevelPlaneAlignedWithCamera(float dist, RenderTexture source, RenderTexture dest, Material material, Camera cameraForProjectionMatrix)
	{
		RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		bool flag = true;
		GL.PushMatrix();
		GL.LoadIdentity();
		GL.LoadProjectionMatrix(cameraForProjectionMatrix.projectionMatrix);
		float f = cameraForProjectionMatrix.fieldOfView * 0.5f * 0.017453292f;
		float num = Mathf.Cos(f) / Mathf.Sin(f);
		float aspect = cameraForProjectionMatrix.aspect;
		float num2 = aspect / -num;
		float num3 = aspect / num;
		float num4 = 1f / -num;
		float num5 = 1f / num;
		float num6 = 1f;
		num2 *= dist * num6;
		num3 *= dist * num6;
		num4 *= dist * num6;
		num5 *= dist * num6;
		float z = -dist;
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			GL.Begin(7);
			float y = 0f;
			float y2 = 0f;
			if (flag)
			{
				y = 1f;
				y2 = (float)0;
			}
			else
			{
				y = (float)0;
				y2 = 1f;
			}
			GL.TexCoord2((float)0, y);
			GL.Vertex3(num2, num4, z);
			GL.TexCoord2(1f, y);
			GL.Vertex3(num3, num4, z);
			GL.TexCoord2(1f, y2);
			GL.Vertex3(num3, num5, z);
			GL.TexCoord2((float)0, y2);
			GL.Vertex3(num2, num5, z);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00008900 File Offset: 0x00006B00
	public static void DrawBorder(RenderTexture dest, Material material)
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

	// Token: 0x060000AE RID: 174 RVA: 0x00008BA8 File Offset: 0x00006DA8
	public static void DrawLowLevelQuad(float x1, float x2, float y1, float y2, RenderTexture source, RenderTexture dest, Material material)
	{
		RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			GL.Begin(7);
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
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x1, y1, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y1, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x1, y2, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00008C9C File Offset: 0x00006E9C
	public virtual void Main()
	{
	}
}
