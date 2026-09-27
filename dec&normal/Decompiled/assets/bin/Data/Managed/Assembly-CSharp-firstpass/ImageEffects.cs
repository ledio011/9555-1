using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[AddComponentMenu("")]
public class ImageEffects
{
	// Token: 0x06000259 RID: 601 RVA: 0x0000A010 File Offset: 0x00008210
	public static void RenderDistortion(Material material, RenderTexture source, RenderTexture destination, float angle, Vector2 center, Vector2 radius)
	{
		bool flag = source.texelSize.y < 0f;
		if (flag)
		{
			center.y = 1f - center.y;
			angle = -angle;
		}
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, angle), Vector3.one);
		material.SetMatrix("_RotationMatrix", matrix);
		material.SetVector("_CenterRadius", new Vector4(center.x, center.y, radius.x, radius.y));
		material.SetFloat("_Angle", angle * 0.017453292f);
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000A0C4 File Offset: 0x000082C4
	[Obsolete("Use Graphics.Blit(source,dest) instead")]
	public static void Blit(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest);
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000A0D0 File Offset: 0x000082D0
	[Obsolete("Use Graphics.Blit(source, destination, material) instead")]
	public static void BlitWithMaterial(Material material, RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest, material);
	}
}
