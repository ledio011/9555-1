using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class RectangleCreate : MeshCreate
{
	// Token: 0x0600127D RID: 4733 RVA: 0x00078FC8 File Offset: 0x000771C8
	public override Mesh Create(float distance, float parm1)
	{
		this.distancex = distance;
		this.distancey = parm1;
		return this.CreateInternal();
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x00078FE0 File Offset: 0x000771E0
	private Mesh CreateInternal()
	{
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(-this.distancex / 2f, 0f, 0f),
			new Vector3(this.distancex / 2f, 0f, 0f),
			new Vector3(this.distancex / 2f, 0f, this.distancey),
			new Vector3(-this.distancex / 2f, 0f, this.distancey)
		};
		int[] triangles = new int[]
		{
			2,
			1,
			0,
			2,
			0,
			3
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(1f, 1f),
			new Vector2(0f, 1f)
		};
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		return mesh;
	}

	// Token: 0x040017BA RID: 6074
	private float distancex;

	// Token: 0x040017BB RID: 6075
	private float distancey;

	// Token: 0x040017BC RID: 6076
	private float mAngel;

	// Token: 0x040017BD RID: 6077
	private float angelDiff = 1f;

	// Token: 0x040017BE RID: 6078
	private float mSegments;
}
