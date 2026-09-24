using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200092D RID: 2349
public class ConsignOnSaleLogic : MonoBehaviour
{
	// Token: 0x06004149 RID: 16713 RVA: 0x001365BC File Offset: 0x001347BC
	public void Reset()
	{
		List<consign_item> saleNowList = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SaleNowList;
		this.currentConsignItemList = saleNowList;
		if (saleNowList != null && saleNowList.Count > 0)
		{
			this.MaxPage = (saleNowList.Count + this.PageCount - 1) / this.PageCount;
			if (this.CurPage >= this.MaxPage)
			{
				this.CurPage = this.MaxPage - 1;
			}
		}
		else
		{
			this.CurPage = 0;
			this.MaxPage = 0;
		}
		this.PageInfoLabel.text = string.Format("{0}/{1}", this.CurPage + 1, this.MaxPage);
		this.RefreshPage(saleNowList, this.CurPage, true);
	}

	// Token: 0x0600414A RID: 16714 RVA: 0x0013667C File Offset: 0x0013487C
	public void RefreshPage(List<consign_item> list, int page, bool isRefresh)
	{
		if (list != null)
		{
			this.PageInfoLabel.text = string.Format("{0}/{1}", page + 1, this.MaxPage);
			int num = Mathf.Min(list.Count, this.PageCount) - this.consignItemLogicList.Count;
			int count = this.consignItemLogicList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.consignItemLogicList[0].gameObject) as GameObject;
					gameObject.name = string.Format("ItemLine_{0}", count + i + 1);
					ConsignItemLogic component = gameObject.GetComponent<ConsignItemLogic>();
					if (component != null)
					{
						component.transform.parent = this.consignItemLogicList[0].transform.parent;
						component.transform.localScale = Vector3.one;
						component.transform.localPosition = Vector3.zero;
						this.consignItemLogicList.Add(component);
					}
				}
			}
			for (int j = 0; j < this.consignItemLogicList.Count; j++)
			{
				if (j + page * this.PageCount < list.Count)
				{
					UnityVersionUtil.SetActiveRecursive(this.consignItemLogicList[j].gameObject, true);
					this.consignItemLogicList[j].Reset(list[j + page * this.PageCount]);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.consignItemLogicList[j].gameObject, false);
				}
			}
		}
		else
		{
			for (int k = 0; k < this.consignItemLogicList.Count; k++)
			{
				UnityVersionUtil.SetActiveRecursive(this.consignItemLogicList[k].gameObject, false);
			}
		}
		if (isRefresh)
		{
			this.grid.Reposition();
			this.scrollView.ResetPosition();
		}
	}

	// Token: 0x0600414B RID: 16715 RVA: 0x00136880 File Offset: 0x00134A80
	public void ClickRightBtn()
	{
		this.CurPage++;
		if (this.CurPage >= this.MaxPage)
		{
			this.CurPage = this.MaxPage - 1;
		}
		else
		{
			this.RefreshPage(this.currentConsignItemList, this.CurPage, true);
		}
	}

	// Token: 0x0600414C RID: 16716 RVA: 0x001368D4 File Offset: 0x00134AD4
	public void ClickLeftBtn()
	{
		this.CurPage--;
		if (this.CurPage < 0)
		{
			this.CurPage = 0;
		}
		else
		{
			this.RefreshPage(this.currentConsignItemList, this.CurPage, true);
		}
	}

	// Token: 0x0600414D RID: 16717 RVA: 0x00136910 File Offset: 0x00134B10
	private void Start()
	{
	}

	// Token: 0x0600414E RID: 16718 RVA: 0x00136914 File Offset: 0x00134B14
	private void Update()
	{
	}

	// Token: 0x04002D0F RID: 11535
	public List<ConsignItemLogic> consignItemLogicList = new List<ConsignItemLogic>();

	// Token: 0x04002D10 RID: 11536
	public UIScrollView scrollView;

	// Token: 0x04002D11 RID: 11537
	public UIGrid grid;

	// Token: 0x04002D12 RID: 11538
	private int PageCount = 10;

	// Token: 0x04002D13 RID: 11539
	private int CurPage;

	// Token: 0x04002D14 RID: 11540
	private int MaxPage;

	// Token: 0x04002D15 RID: 11541
	public UILabel PageInfoLabel;

	// Token: 0x04002D16 RID: 11542
	private List<consign_item> currentConsignItemList;
}
