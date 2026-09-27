using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200099C RID: 2460
public class SlotLineLogic : MonoBehaviour
{
	// Token: 0x060045B5 RID: 17845 RVA: 0x0015F230 File Offset: 0x0015D430
	public void Reset(List<SlotIconData> iconlist, int line)
	{
		if (this.mTransform == null)
		{
			this.mTransform = base.transform;
		}
		this.lineIndex = line;
		this.slotIconList = this.ListRandom(iconlist);
		int num = this.slotIconList.Count - this.lineitems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.lineitems[0].gameObject) as GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				SlotLineItemLogic component = gameObject.GetComponent<SlotLineItemLogic>();
				this.lineitems.Add(component);
				gameObject.gameObject.name = string.Format("icon{0:D2}", this.lineitems.Count - 1);
			}
		}
		for (int j = 0; j < this.lineitems.Count; j++)
		{
			if (j < this.slotIconList.Count)
			{
				this.lineitems[j].Reset(this.slotIconList[j], j);
			}
			else
			{
				NGUITools.SetActive(this.lineitems[j].gameObject, false);
			}
		}
		this.mTransform.localPosition = new Vector3((float)(this.lineIndex * 128 + -128), 0f, 0f);
		this.isRolling = false;
		this.backfun = null;
	}

	// Token: 0x060045B6 RID: 17846 RVA: 0x0015F3D0 File Offset: 0x0015D5D0
	public void StopRolling(string des)
	{
		this.isRolling = false;
		this.DesStr = des;
		int num = -1;
		for (int i = 0; i < this.lineitems.Count; i++)
		{
			if (this.lineitems[i].curiconinfo.ID.Equals(this.DesStr))
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			this.ChangeSortCells(num);
			this.mTransform.localPosition = new Vector3((float)(this.lineIndex * 128 + -128), 0f, 0f);
			for (int j = 0; j < this.lineitems.Count; j++)
			{
				this.lineitems[j].ResetPos(j);
			}
		}
		if (this.backfun != null)
		{
			this.backfun();
		}
	}

	// Token: 0x060045B7 RID: 17847 RVA: 0x0015F4B4 File Offset: 0x0015D6B4
	private void FixedUpdate()
	{
		if (this.isRolling)
		{
			this.mTransform.localPosition = new Vector3((float)(this.lineIndex * 128 + -128), this.startY, 0f);
			this.startY += this.RollSpeed * Time.deltaTime;
			this.delRollCount = (int)(Mathf.Abs(this.mTransform.localPosition.y) / 90f) - this.RollCount;
			if (this.delRollCount >= 0)
			{
				this.ChangeCells(this.delRollCount);
				this.RollCount += this.delRollCount + 1;
			}
		}
	}

	// Token: 0x060045B8 RID: 17848 RVA: 0x0015F56C File Offset: 0x0015D76C
	public void ChangeCells(int delcount)
	{
		delcount++;
		List<SlotLineItemLogic> list = new List<SlotLineItemLogic>();
		int num = 0;
		int cellsitemNum = this.CellsitemNum;
		int num2 = this.CellsitemNum - delcount;
		for (int i = num2; i < this.CellsitemNum; i++)
		{
			list.Add(this.lineitems[i]);
			delcount = (this.lineitems[i].idx = delcount - 1);
			this.lineitems[i].SetMoveToUp(this.RollCount - delcount);
			num++;
		}
		for (int j = this.CellsitemNum - 1; j >= num2; j--)
		{
		}
		for (int k = 0; k < num2; k++)
		{
			list.Add(this.lineitems[k]);
			this.lineitems[k].idx = num++;
		}
		this.lineitems = list;
	}

	// Token: 0x060045B9 RID: 17849 RVA: 0x0015F660 File Offset: 0x0015D860
	public void ChangeSortCells(int desindex)
	{
		List<SlotLineItemLogic> list = new List<SlotLineItemLogic>();
		int num = desindex - 4;
		if (num < 0)
		{
			num += this.CellsitemNum;
		}
		for (int i = 0; i < this.CellsitemNum; i++)
		{
			list.Add(this.lineitems[(i + num) % this.CellsitemNum]);
		}
		this.lineitems = list;
	}

	// Token: 0x060045BA RID: 17850 RVA: 0x0015F6C0 File Offset: 0x0015D8C0
	public void StartSpin(DelegateDefine.NoParamDelegate stopfun)
	{
		this.backfun = stopfun;
		this.isRolling = true;
		this.startY = this.mTransform.localPosition.y;
		this.RollCount = (int)Mathf.Abs(this.mTransform.localPosition.y) / 90 + 1;
		this.MaxRollSpeed = -3000f;
		this.RollSpeed = this.MaxRollSpeed;
	}

	// Token: 0x060045BB RID: 17851 RVA: 0x0015F730 File Offset: 0x0015D930
	private List<SlotIconData> ListRandom(List<SlotIconData> myList)
	{
		List<SlotIconData> list = new List<SlotIconData>();
		for (int i = 0; i < myList.Count; i++)
		{
			int num = Random.Range(0, myList.Count - 1);
			if (num != i)
			{
				SlotIconData slotIconData = myList[i];
				myList[i] = myList[num];
				myList[num] = slotIconData;
			}
		}
		return myList;
	}

	// Token: 0x040032AD RID: 12973
	private Transform mTransform;

	// Token: 0x040032AE RID: 12974
	public int lineIndex;

	// Token: 0x040032AF RID: 12975
	public List<SlotLineItemLogic> lineitems;

	// Token: 0x040032B0 RID: 12976
	private List<SlotIconData> slotIconList;

	// Token: 0x040032B1 RID: 12977
	private int CellsitemNum = 10;

	// Token: 0x040032B2 RID: 12978
	public float RollSpeed;

	// Token: 0x040032B3 RID: 12979
	public float MaxRollSpeed;

	// Token: 0x040032B4 RID: 12980
	private float startY;

	// Token: 0x040032B5 RID: 12981
	public int RollCount;

	// Token: 0x040032B6 RID: 12982
	private int delRollCount;

	// Token: 0x040032B7 RID: 12983
	public bool isRolling;

	// Token: 0x040032B8 RID: 12984
	private DelegateDefine.NoParamDelegate backfun;

	// Token: 0x040032B9 RID: 12985
	public string DesStr;
}
