using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class SectorMeshCreate : MeshCreate
{
	// Token: 0x06001280 RID: 4736 RVA: 0x0007914C File Offset: 0x0007734C
	public override Mesh Create(float distance, float parm1)
	{
		this.mRadius = distance;
		this.mAngel = parm1;
		this.mSegments = this.mAngel / this.angelDiff;
		return this.CreateInternal();
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x00079178 File Offset: 0x00077378
	private Mesh CreateInternal()
	{
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[(int)this.mSegments + 3 - 1];
		array[0] = Vector3.zero;
		float num = 0.017453292f * this.mAngel;
		float num2 = num / 2f;
		float num3 = num / this.mSegments;
		int i;
		for (i = 1; i < array.Length; i++)
		{
			array[i] = new Vector3(Mathf.Sin(num2) * this.mRadius, 0f, Mathf.Cos(num2) * this.mRadius);
			num2 -= num3;
		}
		int[] array2 = new int[(int)this.mSegments * 3];
		i = 0;
		int num4 = 1;
		while (i < array2.Length)
		{
			array2[i] = 0;
			array2[i + 1] = num4 + 1;
			array2[i + 2] = num4;
			i += 3;
			num4++;
		}
		Vector2[] array3 = new Vector2[array.Length];
		for (i = 0; i < array3.Length; i++)
		{
			array3[i] = new Vector2(Mathf.Abs(array[i].x) / this.mRadius, Mathf.Abs(array[i].z) / this.mRadius);
		}
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.uv = array3;
		return mesh;
	}

	// Token: 0x040017BF RID: 6079
	private float mRadius;

	// Token: 0x040017C0 RID: 6080
	private float mAngel;

	// Token: 0x040017C1 RID: 6081
	private float angelDiff = 1f;

	// Token: 0x040017C2 RID: 6082
	private float mSegments;
}
