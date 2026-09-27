using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A69 RID: 2665
public class CreateEquilateralPic : MonoBehaviour
{
	// Token: 0x06004DB1 RID: 19889 RVA: 0x001A8D20 File Offset: 0x001A6F20
	private void Awake()
	{
		this.DrawPic(this.LineNum);
	}

	// Token: 0x06004DB2 RID: 19890 RVA: 0x001A8D30 File Offset: 0x001A6F30
	public void DrawPic(int lineNum)
	{
		if (lineNum == 0)
		{
			return;
		}
		this.meshFilter = base.GetComponent<MeshFilter>();
		float num = 6.2831855f / (float)lineNum;
		for (int i = 0; i < this.PosDisList.Count; i++)
		{
			Vector3 vector;
			vector..ctor(Mathf.Cos(num * (float)i) * this.PosDisList[i], Mathf.Sin(num * (float)i) * this.PosDisList[i], 0f);
			this.pointList.Add(vector);
		}
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[lineNum + 1];
		array[0] = Vector3.zero;
		for (int j = 0; j < lineNum; j++)
		{
			array[j + 1] = this.pointList[j];
		}
		int[] array2 = new int[3 * lineNum];
		for (int k = 0; k < lineNum; k++)
		{
			array2[k * 3] = 0;
			array2[k * 3 + 1] = k + 1;
			array2[k * 3 + 2] = (k + 2) % (lineNum + 1);
			array2[k * 3 + 2] = ((array2[k * 3 + 2] != 0) ? array2[k * 3 + 2] : 1);
		}
		Vector2[] array3 = new Vector2[array.Length];
		array3[0] = new Vector2(0f, 0f);
		for (int l = 1; l < array3.Length; l++)
		{
			array3[l] = new Vector2((float)((l + 1) % 2), (float)(l % 2));
		}
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.uv = array3;
		this.meshFilter.mesh = mesh;
	}

	// Token: 0x06004DB3 RID: 19891 RVA: 0x001A8F00 File Offset: 0x001A7100
	public void UpdatePos(float[] posDisList)
	{
		float num = 6.2831855f / (float)this.LineNum;
		this.pointList.Clear();
		for (int i = 0; i < posDisList.Length; i++)
		{
			Vector3 vector;
			vector..ctor(Mathf.Cos(num * (float)i) * posDisList[i], Mathf.Sin(num * (float)i) * posDisList[i], 0f);
			this.pointList.Add(vector);
		}
		Vector3[] array = new Vector3[this.LineNum + 1];
		array[0] = Vector3.zero;
		for (int j = 0; j < this.LineNum; j++)
		{
			array[j + 1] = this.pointList[j];
		}
		this.meshFilter.mesh.vertices = array;
	}

	// Token: 0x04003C68 RID: 15464
	public int LineNum;

	// Token: 0x04003C69 RID: 15465
	public List<float> PosDisList;

	// Token: 0x04003C6A RID: 15466
	private MeshFilter meshFilter;

	// Token: 0x04003C6B RID: 15467
	private List<Vector3> pointList = new List<Vector3>();
}
