using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class Skidmarks : SingletonUnity<Skidmarks>
{
	// Token: 0x06000AB6 RID: 2742 RVA: 0x0004FAF8 File Offset: 0x0004DCF8
	protected override void Awake()
	{
		base.Awake();
		this.maxSegmentCount = this.maxMarks;
		this.InitSkidmarks();
		this.minDistanceSq = this.minDistance * this.minDistance;
		this.markAlphaDec = 5f / (float)this.maxSegmentCount;
		this.vertices = new Vector3[this.maxSegmentCount * 4];
		this.normals = new Vector3[this.maxSegmentCount * 4];
		this.tangents = new Vector4[this.maxSegmentCount * 4];
		this.colors = new Color[this.maxSegmentCount * 4];
		this.uvs = new Vector2[this.maxSegmentCount * 4];
		this.triangles = new int[this.maxSegmentCount * 6];
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
	private void InitSkidmarks()
	{
		this.skidmarks = new Skidmarks.MarkSection[this.maxMarks];
		for (int i = 0; i < this.maxMarks; i++)
		{
			this.skidmarks[i] = new Skidmarks.MarkSection();
		}
		this.mesh = this.meshFilter.mesh;
		if (this.mesh == null)
		{
			this.mesh = new Mesh();
			this.meshFilter.mesh = this.mesh;
		}
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0004FC38 File Offset: 0x0004DE38
	public void InitMark()
	{
		this.numMarks = 0;
		this.mesh.Clear();
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0004FC4C File Offset: 0x0004DE4C
	public int AddSkidMark(Vector3 pos, Vector3 normal, float intensity, int lastIndex, float width)
	{
		if (intensity > 1f)
		{
			intensity = 1f;
		}
		if (intensity < 0f)
		{
			return -1;
		}
		Skidmarks.MarkSection markSection = null;
		Vector3 vector = default(Vector3);
		if (lastIndex != -1)
		{
			markSection = this.skidmarks[lastIndex % this.maxMarks];
			vector = pos - markSection.pos;
			if (vector.sqrMagnitude < this.minDistanceSq)
			{
				return lastIndex;
			}
		}
		if (this.skidmarks == null)
		{
			this.InitSkidmarks();
		}
		if (intensity > 1f)
		{
			intensity = 1f;
		}
		else if (intensity <= 0f)
		{
			return -1;
		}
		Skidmarks.MarkSection markSection2 = this.skidmarks[this.numMarks % this.maxMarks];
		markSection2.pos = pos + normal * this.groundOffset;
		markSection2.normal = normal;
		markSection2.intensity = intensity;
		markSection2.lastIndex = lastIndex;
		if (lastIndex != -1)
		{
			Vector3 normalized = Vector3.Cross(vector, normal).normalized;
			markSection2.posl = markSection2.pos + normalized * width * 0.5f;
			markSection2.posr = markSection2.pos - normalized * width * 0.5f;
			markSection2.tangent = new Vector4(normalized.x, normalized.y, normalized.z, 1f);
			if (markSection.lastIndex == -1)
			{
				markSection.tangent = markSection2.tangent;
				markSection.posl = markSection.pos + normalized * width * 0.5f;
				markSection.posr = markSection.pos - normalized * width * 0.5f;
			}
		}
		this.updated = true;
		return this.numMarks++;
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0004FE34 File Offset: 0x0004E034
	public void SetMaterial(bool isSand)
	{
		if (isSand)
		{
			if (this.mat_sand == null)
			{
				this.mat_sand = (Object.Instantiate(Resources.Load("Cars/Skidmarks_sand")) as Material);
			}
			this.mat = base.renderer.material;
			base.renderer.material = this.mat_sand;
		}
		else
		{
			base.renderer.material = this.mat;
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0004FEAC File Offset: 0x0004E0AC
	private void LateUpdate()
	{
		if (!this.updated)
		{
			return;
		}
		this.updated = false;
		this.mesh.Clear();
		int num = 0;
		int num2 = 0;
		while (num < this.maxMarks && num2 < this.maxSegmentCount && num < this.numMarks)
		{
			int num3 = (this.numMarks - 1 - num + (this.maxMarks - 1)) % this.maxMarks;
			if (this.skidmarks[num3].lastIndex != -1 && this.skidmarks[num3].lastIndex > this.numMarks - this.maxMarks)
			{
				Skidmarks.MarkSection markSection = this.skidmarks[num3];
				Skidmarks.MarkSection markSection2 = this.skidmarks[markSection.lastIndex % this.maxMarks];
				this.vertices[num2 * 4] = markSection2.posl;
				this.vertices[num2 * 4 + 1] = markSection2.posr;
				this.vertices[num2 * 4 + 2] = markSection.posl;
				this.vertices[num2 * 4 + 3] = markSection.posr;
				this.normals[num2 * 4] = markSection2.normal;
				this.normals[num2 * 4 + 1] = markSection2.normal;
				this.normals[num2 * 4 + 2] = markSection.normal;
				this.normals[num2 * 4 + 3] = markSection.normal;
				this.tangents[num2 * 4] = markSection2.tangent;
				this.tangents[num2 * 4 + 1] = markSection2.tangent;
				this.tangents[num2 * 4 + 2] = markSection.tangent;
				this.tangents[num2 * 4 + 3] = markSection.tangent;
				markSection2.intensity -= this.markAlphaDec;
				if (markSection2.intensity < 0f)
				{
					markSection2.intensity = 0f;
				}
				this.colors[num2 * 4] = new Color(0f, 0f, 0f, markSection2.intensity);
				this.colors[num2 * 4 + 1] = new Color(0f, 0f, 0f, markSection2.intensity);
				this.colors[num2 * 4 + 2] = new Color(0f, 0f, 0f, markSection.intensity);
				this.colors[num2 * 4 + 3] = new Color(0f, 0f, 0f, markSection.intensity);
				this.uvs[num2 * 4] = new Vector2(0f, 0f);
				this.uvs[num2 * 4 + 1] = new Vector2(1f, 0f);
				this.uvs[num2 * 4 + 2] = new Vector2(0f, 1f);
				this.uvs[num2 * 4 + 3] = new Vector2(1f, 1f);
				this.triangles[num2 * 6] = num2 * 4;
				this.triangles[num2 * 6 + 2] = num2 * 4 + 1;
				this.triangles[num2 * 6 + 1] = num2 * 4 + 2;
				this.triangles[num2 * 6 + 3] = num2 * 4 + 2;
				this.triangles[num2 * 6 + 5] = num2 * 4 + 1;
				this.triangles[num2 * 6 + 4] = num2 * 4 + 3;
				num2++;
			}
			num++;
		}
		this.mesh.vertices = this.vertices;
		this.mesh.normals = this.normals;
		this.mesh.tangents = this.tangents;
		this.mesh.triangles = this.triangles;
		this.mesh.colors = this.colors;
		this.mesh.uv = this.uvs;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00050300 File Offset: 0x0004E500
	public void Clear()
	{
		this.numMarks = 0;
		this.updated = true;
		for (int i = 0; i < this.skidmarks.Length; i++)
		{
			this.skidmarks[i].Clear();
		}
		Array.Clear(this.normals, 0, this.normals.Length);
		Array.Clear(this.vertices, 0, this.vertices.Length);
		Array.Clear(this.tangents, 0, this.tangents.Length);
		Array.Clear(this.triangles, 0, this.triangles.Length);
		Array.Clear(this.colors, 0, this.colors.Length);
		Array.Clear(this.uvs, 0, this.uvs.Length);
	}

	// Token: 0x040009B5 RID: 2485
	public MeshFilter meshFilter;

	// Token: 0x040009B6 RID: 2486
	public int maxMarks = 128;

	// Token: 0x040009B7 RID: 2487
	[HideInInspector]
	public float markWidth = 0.275f;

	// Token: 0x040009B8 RID: 2488
	public float groundOffset = 0.02f;

	// Token: 0x040009B9 RID: 2489
	public float minDistance = 0.1f;

	// Token: 0x040009BA RID: 2490
	private float minDistanceSq;

	// Token: 0x040009BB RID: 2491
	private int numMarks;

	// Token: 0x040009BC RID: 2492
	private int maxSegmentCount;

	// Token: 0x040009BD RID: 2493
	private float markAlphaDec;

	// Token: 0x040009BE RID: 2494
	private Mesh mesh;

	// Token: 0x040009BF RID: 2495
	private Vector3[] vertices;

	// Token: 0x040009C0 RID: 2496
	private Vector3[] normals;

	// Token: 0x040009C1 RID: 2497
	private Vector4[] tangents;

	// Token: 0x040009C2 RID: 2498
	private Color[] colors;

	// Token: 0x040009C3 RID: 2499
	private Vector2[] uvs;

	// Token: 0x040009C4 RID: 2500
	private int[] triangles;

	// Token: 0x040009C5 RID: 2501
	private Material mat;

	// Token: 0x040009C6 RID: 2502
	private Material mat_sand;

	// Token: 0x040009C7 RID: 2503
	private Skidmarks.MarkSection[] skidmarks;

	// Token: 0x040009C8 RID: 2504
	private bool updated;

	// Token: 0x02000124 RID: 292
	private class MarkSection
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x00050410 File Offset: 0x0004E610
		public void Clear()
		{
			this.pos = Vector3.zero;
			this.normal = Vector3.zero;
			this.tangent = Vector3.zero;
			this.posl = Vector3.zero;
			this.posr = Vector3.zero;
			this.intensity = 0f;
			this.lastIndex = -1;
		}

		// Token: 0x040009C9 RID: 2505
		public Vector3 pos = Vector3.zero;

		// Token: 0x040009CA RID: 2506
		public Vector3 normal = Vector3.zero;

		// Token: 0x040009CB RID: 2507
		public Vector4 tangent = Vector4.zero;

		// Token: 0x040009CC RID: 2508
		public Vector3 posl = Vector3.zero;

		// Token: 0x040009CD RID: 2509
		public Vector3 posr = Vector3.zero;

		// Token: 0x040009CE RID: 2510
		public float intensity;

		// Token: 0x040009CF RID: 2511
		public int lastIndex = -1;
	}
}
