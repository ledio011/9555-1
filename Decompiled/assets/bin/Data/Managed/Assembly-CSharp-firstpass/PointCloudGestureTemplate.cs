using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class PointCloudGestureTemplate : ScriptableObject
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000141 RID: 321 RVA: 0x00005828 File Offset: 0x00003A28
	public Vector2 Size
	{
		get
		{
			return this.size;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000142 RID: 322 RVA: 0x00005830 File Offset: 0x00003A30
	public float Width
	{
		get
		{
			return this.size.x;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000143 RID: 323 RVA: 0x00005840 File Offset: 0x00003A40
	public float Height
	{
		get
		{
			return this.size.y;
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00005850 File Offset: 0x00003A50
	private void Awake()
	{
		if (this.positions == null)
		{
			this.positions = new List<Vector2>();
		}
		if (this.strokeIds == null)
		{
			this.strokeIds = new List<int>();
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000588C File Offset: 0x00003A8C
	public void BeginPoints()
	{
		this.positions.Clear();
		this.strokeIds.Clear();
		this.strokeCount = 0;
		this.size = Vector2.zero;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000058C4 File Offset: 0x00003AC4
	public void AddPoint(int stroke, Vector2 p)
	{
		this.strokeIds.Add(stroke);
		this.positions.Add(p);
	}

	// Token: 0x06000147 RID: 327 RVA: 0x000058E0 File Offset: 0x00003AE0
	public void AddPoint(int stroke, float x, float y)
	{
		this.AddPoint(stroke, new Vector2(x, y));
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000058F0 File Offset: 0x00003AF0
	public void EndPoints()
	{
		this.Normalize();
		List<int> list = new List<int>();
		for (int i = 0; i < this.strokeIds.Count; i++)
		{
			int item = this.strokeIds[i];
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
		this.strokeCount = list.Count;
		this.MakeDirty();
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00005958 File Offset: 0x00003B58
	public Vector2 GetPosition(int pointIndex)
	{
		return this.positions[pointIndex];
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00005968 File Offset: 0x00003B68
	public int GetStrokeId(int pointIndex)
	{
		return this.strokeIds[pointIndex];
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600014B RID: 331 RVA: 0x00005978 File Offset: 0x00003B78
	public int PointCount
	{
		get
		{
			return this.positions.Count;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600014C RID: 332 RVA: 0x00005988 File Offset: 0x00003B88
	public int StrokeCount
	{
		get
		{
			return this.strokeCount;
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00005990 File Offset: 0x00003B90
	public void Normalize()
	{
		Vector2 b = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
		Vector2 vector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		for (int i = 0; i < this.positions.Count; i++)
		{
			Vector2 vector2 = this.positions[i];
			b.x = Mathf.Min(b.x, vector2.x);
			b.y = Mathf.Min(b.y, vector2.y);
			vector.x = Mathf.Max(vector.x, vector2.x);
			vector.y = Mathf.Max(vector.y, vector2.y);
		}
		float num = vector.x - b.x;
		float num2 = vector.y - b.y;
		float num3 = Mathf.Max(num, num2);
		float num4 = 1f / num3;
		this.size.x = num * num4;
		this.size.y = num2 * num4;
		Vector2 b2 = -0.5f * this.size;
		for (int j = 0; j < this.positions.Count; j++)
		{
			this.positions[j] = (this.positions[j] - b) * num4 + b2;
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00005B08 File Offset: 0x00003D08
	private void MakeDirty()
	{
	}

	// Token: 0x040000C9 RID: 201
	[SerializeField]
	private List<int> strokeIds;

	// Token: 0x040000CA RID: 202
	[SerializeField]
	private List<Vector2> positions;

	// Token: 0x040000CB RID: 203
	[SerializeField]
	private int strokeCount;

	// Token: 0x040000CC RID: 204
	[SerializeField]
	private Vector2 size = Vector2.zero;
}
