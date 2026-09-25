using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class UIGeometry
{
	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
	public bool hasVertices
	{
		get
		{
			return this.verts.size > 0;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.size > 0 && this.mRtpVerts.size == this.verts.size;
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0001CB20 File Offset: 0x0001AD20
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0001CB5C File Offset: 0x0001AD5C
	public void ApplyTransform(Matrix4x4 widgetToPanel)
	{
		if (this.verts.size > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int size = this.verts.size;
			while (i < size)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
			Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
			this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
		}
		else
		{
			this.mRtpVerts.Clear();
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0001CC28 File Offset: 0x0001AE28
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.size > 0)
		{
			if (n == null)
			{
				for (int i = 0; i < this.mRtpVerts.size; i++)
				{
					v.Add(this.mRtpVerts.buffer[i]);
					u.Add(this.uvs.buffer[i]);
					c.Add(this.cols.buffer[i]);
				}
			}
			else
			{
				for (int j = 0; j < this.mRtpVerts.size; j++)
				{
					v.Add(this.mRtpVerts.buffer[j]);
					u.Add(this.uvs.buffer[j]);
					c.Add(this.cols.buffer[j]);
					n.Add(this.mRtpNormal);
					t.Add(this.mRtpTan);
				}
			}
		}
	}

	// Token: 0x040003A8 RID: 936
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x040003A9 RID: 937
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x040003AA RID: 938
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x040003AB RID: 939
	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	// Token: 0x040003AC RID: 940
	private Vector3 mRtpNormal;

	// Token: 0x040003AD RID: 941
	private Vector4 mRtpTan;
}
