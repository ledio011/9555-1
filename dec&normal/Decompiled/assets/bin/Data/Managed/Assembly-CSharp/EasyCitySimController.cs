using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class EasyCitySimController : SingletonUnity<EasyCitySimController>
{
	// Token: 0x060010C0 RID: 4288 RVA: 0x0006C914 File Offset: 0x0006AB14
	public EasyCitySimController()
	{
		List<string> list = new List<string>();
		list.Add("201");
		list.Add("202");
		list.Add("204");
		list.Add("206");
		this.mNpcIDList = list;
		this.mLastCreatePointList = new List<int>();
		this.difIndex = 3;
		base..ctor();
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0006C974 File Offset: 0x0006AB74
	private new void Awake()
	{
		base.Awake();
		this.InitPath();
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0006C984 File Offset: 0x0006AB84
	private void InitPath()
	{
		if (this.PointLinkList == null)
		{
			this.PointLinkList = new List<int>[this.PointPosList.Count];
		}
		this.BlockData = new List<int>[(int)this.CitySize.x / this.BlockSize][];
		for (int i = 0; i < this.BlockData.Length; i++)
		{
			this.BlockData[i] = new List<int>[(int)this.CitySize.y / this.BlockSize];
		}
		for (int j = 0; j < this.PointPosList.Count; j++)
		{
			int num;
			int num2;
			this.GetBlockPos(this.PointPosList[j], out num, out num2);
			if (this.BlockData[num][num2] == null)
			{
				this.BlockData[num][num2] = new List<int>();
			}
			this.BlockData[num][num2].Add(j);
		}
		List<int> list = new List<int>();
		for (int k = 0; k < this.PointPosList.Count; k++)
		{
			list.Clear();
			int num3;
			int num4;
			this.GetBlockPos(this.PointPosList[k], out num3, out num4);
			int num5 = num3 - 1;
			int num6 = num3 + 1;
			int num7 = num4 - 1;
			int num8 = num4 + 1;
			for (int l = num5; l <= num6; l++)
			{
				if (l >= 0 && l < this.BlockData.Length)
				{
					for (int m = num7; m <= num8; m++)
					{
						if (m >= 0 && m < this.BlockData[l].Length && this.BlockData[l][m] != null)
						{
							for (int n = 0; n < this.BlockData[l][m].Count; n++)
							{
								list.Add(this.BlockData[l][m][n]);
							}
						}
					}
				}
			}
			if (list.Count > 0 && this.PointLinkList[k] == null)
			{
				this.PointLinkList[k] = new List<int>();
			}
			for (int num9 = 0; num9 < list.Count; num9++)
			{
				if (k != list[num9])
				{
					if (Vector3.SqrMagnitude(this.PointPosList[k] - this.PointPosList[list[num9]]) < 900f)
					{
						this.PointLinkList[k].Add(list[num9]);
					}
				}
			}
		}
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0006CC20 File Offset: 0x0006AE20
	public void GetBlockPos(Vector3 pos, out int x, out int y)
	{
		x = (int)(pos.x - this.CityLeftBottomPos.x) / this.BlockSize;
		y = (int)(pos.z - this.CityLeftBottomPos.y) / this.BlockSize;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0006CC68 File Offset: 0x0006AE68
	public void FreshNPC(Transform targetTrans)
	{
		ObjManager instance = Singleton<ObjManager>.Instance;
		int x;
		int y;
		this.GetBlockPos(targetTrans.position, out x, out y);
		List<int> list = new List<int>();
		list.Clear();
		this.GetGeneRatePoint(x, y, list);
		this.mLastCreatePointList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (this.PointLinkList[list[i]] != null && this.PointLinkList[list[i]].Count != 0)
			{
				Vector3 vector = targetTrans.InverseTransformPoint(this.PointPosList[list[i]]);
				if (vector.z >= 0f && Mathf.Abs(vector.x) <= 50f)
				{
					this.mLastCreatePointList.Add(list[i]);
					NpcData npcDataByID = DataManager.GetNpcDataByID(this.mNpcIDList[Random.Range(0, this.mNpcIDList.Count)]);
					int linkPoint = this.PointLinkList[list[i]][Random.Range(0, this.PointLinkList[list[i]].Count)];
					ObjInitNpcData objInitNpcData = new ObjInitNpcData();
					objInitNpcData.mServerID = UUID.GenUUID();
					objInitNpcData.mPos = this.PointPosList[list[i]];
					objInitNpcData.mDir = (this.PointPosList[linkPoint] - objInitNpcData.mPos).normalized;
					objInitNpcData.HP = npcDataByID.Hp;
					objInitNpcData.MaxHP = npcDataByID.Hp;
					objInitNpcData.npcInfoData = npcDataByID;
					objInitNpcData.mCharacterModelId = npcDataByID.Model;
					instance.GetRagdollNPC(objInitNpcData, delegate(ObjNPC npc)
					{
						if (npc != null)
						{
							if (Random.Range(0, 100) > 50)
							{
								npc.WalkMoveTo(this.PointPosList[linkPoint], 3f, delegate
								{
									npc.MoveTo(this.PointPosList[this.GetNextPoint(linkPoint)], 1f, null);
								});
							}
							else
							{
								npc.MoveTo(this.PointPosList[linkPoint], 3f, delegate(ObjCharacter A_1)
								{
									npc.WalkMoveTo(this.PointPosList[this.GetNextPoint(linkPoint)], 1f, null);
								});
							}
						}
					});
				}
			}
		}
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0006CE60 File Offset: 0x0006B060
	private int GetNextPoint(int curPoint)
	{
		if (this.PointLinkList != null)
		{
			return this.PointLinkList[curPoint][Random.Range(0, this.PointLinkList[curPoint].Count)];
		}
		return 0;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0006CE90 File Offset: 0x0006B090
	private void AddPoint(int x, int y, List<int> pointList)
	{
		if (this.BlockData[x][y] != null)
		{
			for (int i = 0; i < this.BlockData[x][y].Count; i++)
			{
				if (Random.Range(0, 100) > 50 && !this.mLastCreatePointList.Contains(this.BlockData[x][y][i]))
				{
					pointList.Add(this.BlockData[x][y][i]);
				}
			}
		}
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0006CF14 File Offset: 0x0006B114
	private void GetGeneRatePoint(int x, int y, List<int> pointList)
	{
		int num = x - this.difIndex;
		int num2 = x + this.difIndex;
		int num3 = y + this.difIndex;
		int num4 = y - this.difIndex;
		for (int i = num; i < num2 + 1; i++)
		{
			if (i >= 0 && i < this.BlockData.Length)
			{
				for (int j = num4; j < num3 + 1; j++)
				{
					if (j >= 0 && j < this.BlockData[i].Length && (i == num || i == num2 || j == num4 || j == num3))
					{
						this.AddPoint(i, j, pointList);
					}
				}
			}
		}
	}

	// Token: 0x04001443 RID: 5187
	public List<Vector3> PointPosList;

	// Token: 0x04001444 RID: 5188
	public List<int>[] PointLinkList;

	// Token: 0x04001445 RID: 5189
	public Vector2 CityLeftBottomPos;

	// Token: 0x04001446 RID: 5190
	public Vector2 CitySize;

	// Token: 0x04001447 RID: 5191
	public int BlockSize;

	// Token: 0x04001448 RID: 5192
	public List<int>[][] BlockData;

	// Token: 0x04001449 RID: 5193
	public bool runFlag;

	// Token: 0x0400144A RID: 5194
	private List<string> mNpcIDList;

	// Token: 0x0400144B RID: 5195
	private List<int> mLastCreatePointList;

	// Token: 0x0400144C RID: 5196
	private bool flag;

	// Token: 0x0400144D RID: 5197
	private int difIndex;
}
