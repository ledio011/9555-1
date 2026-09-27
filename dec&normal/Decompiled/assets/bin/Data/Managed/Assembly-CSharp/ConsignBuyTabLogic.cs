using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200092B RID: 2347
public class ConsignBuyTabLogic : MonoBehaviour
{
	// Token: 0x0600413C RID: 16700 RVA: 0x00135FDC File Offset: 0x001341DC
	public void SetMinusPic(bool isMinus)
	{
		if (isMinus)
		{
			this.RootPic.spriteName = string.Format("CZ_paiMai_-", new object[0]);
			this.RootPic.MakePixelPerfect();
		}
		else
		{
			this.RootPic.spriteName = string.Format("CZ_paiMai_+", new object[0]);
			this.RootPic.MakePixelPerfect();
		}
		this.mIsOpen = isMinus;
	}

	// Token: 0x0600413D RID: 16701 RVA: 0x00136048 File Offset: 0x00134248
	public void Reset(List<ConsignBuyTabData> curTabDataList, ConsignBuyTabLogic.OnClickTabDelegate func, int index, ConsignBuyTabLogic.OnClickTopTabDelegate clickTopTabFunc)
	{
		this.SetMinusPic(false);
		this.TypeLabel.text = StrDictionary.GetDictionaryString(curTabDataList[0].TopTabName, new object[0]);
		int num = curTabDataList.Count - this.SubTabList.Count;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = Object.Instantiate(this.SubTabList[0].gameObject) as GameObject;
			ConsignBuySubTabLogic component = gameObject.GetComponent<ConsignBuySubTabLogic>();
			gameObject.transform.parent = this.SubTabList[0].transform.parent;
			gameObject.transform.localPosition = new Vector3(0f, (float)(-16 - 32 * (i + 1)), 0f);
			this.SubTabList.Add(component);
		}
		for (int j = 0; j < curTabDataList.Count; j++)
		{
			this.SubTabList[j].Reset(curTabDataList[j].ItemTypeId, curTabDataList[j].SubTypeId, curTabDataList[j].SubTypeName, new ConsignBuySubTabLogic.OnClickTabDelegate(this.OnClickSubTab), j);
		}
		this.SubTabList[0].transform.parent.localScale = new Vector3(1f, 0.001f, 1f);
		this.onClickTabDelegate = func;
		this.onClickTopTabDelegate = clickTopTabFunc;
		this.mIndex = index;
	}

	// Token: 0x0600413E RID: 16702 RVA: 0x001361C0 File Offset: 0x001343C0
	public void OnClickTab()
	{
		if (this.mIsOpen)
		{
			this.SetMinusPic(false);
		}
		else
		{
			this.SetMinusPic(true);
			if (this.onClickTopTabDelegate != null)
			{
				this.onClickTopTabDelegate(this.mIndex);
			}
		}
	}

	// Token: 0x0600413F RID: 16703 RVA: 0x00136208 File Offset: 0x00134408
	private void OnClickSubTab(int itemType, int subType, int index)
	{
		if (this.onClickTabDelegate != null)
		{
			this.onClickTabDelegate(itemType, subType, this.mIndex, index);
		}
		this.preClickIndex = index;
	}

	// Token: 0x04002CFD RID: 11517
	private ConsignBuyTabLogic.OnClickTabDelegate onClickTabDelegate;

	// Token: 0x04002CFE RID: 11518
	private ConsignBuyTabLogic.OnClickTopTabDelegate onClickTopTabDelegate;

	// Token: 0x04002CFF RID: 11519
	public TweenScale SubTweenRoot;

	// Token: 0x04002D00 RID: 11520
	public UILabel TypeLabel;

	// Token: 0x04002D01 RID: 11521
	public UISprite RootPic;

	// Token: 0x04002D02 RID: 11522
	private int mIndex;

	// Token: 0x04002D03 RID: 11523
	public List<ConsignBuySubTabLogic> SubTabList;

	// Token: 0x04002D04 RID: 11524
	private int preClickIndex;

	// Token: 0x04002D05 RID: 11525
	private bool mIsOpen;

	// Token: 0x02000AEF RID: 2799
	// (Invoke) Token: 0x06005045 RID: 20549
	public delegate void OnClickTabDelegate(int itemType, int subType, int tabIndex, int subIndex);

	// Token: 0x02000AF0 RID: 2800
	// (Invoke) Token: 0x06005049 RID: 20553
	public delegate void OnClickTopTabDelegate(int index);
}
