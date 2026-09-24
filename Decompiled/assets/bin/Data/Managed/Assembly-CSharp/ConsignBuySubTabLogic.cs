using System;
using UnityEngine;

// Token: 0x0200092A RID: 2346
public class ConsignBuySubTabLogic : MonoBehaviour
{
	// Token: 0x17000F9C RID: 3996
	// (get) Token: 0x06004137 RID: 16695 RVA: 0x00135F54 File Offset: 0x00134154
	public int ItemType
	{
		get
		{
			return this.mItemType;
		}
	}

	// Token: 0x17000F9D RID: 3997
	// (get) Token: 0x06004138 RID: 16696 RVA: 0x00135F5C File Offset: 0x0013415C
	public int SubType
	{
		get
		{
			return this.mSubType;
		}
	}

	// Token: 0x06004139 RID: 16697 RVA: 0x00135F64 File Offset: 0x00134164
	public void Reset(int itemType, int subType, string title, ConsignBuySubTabLogic.OnClickTabDelegate func, int index)
	{
		this.mItemType = itemType;
		this.mSubType = subType;
		this.TabLabel.text = StrDictionary.GetDictionaryString(title, new object[0]);
		this.onClickTabDel = func;
		this.mIndex = index;
	}

	// Token: 0x0600413A RID: 16698 RVA: 0x00135F9C File Offset: 0x0013419C
	public void OnClickTab()
	{
		if (this.onClickTabDel != null)
		{
			this.onClickTabDel(this.mItemType, this.mSubType, this.mIndex);
		}
	}

	// Token: 0x04002CF8 RID: 11512
	public UILabel TabLabel;

	// Token: 0x04002CF9 RID: 11513
	private int mIndex;

	// Token: 0x04002CFA RID: 11514
	private int mItemType;

	// Token: 0x04002CFB RID: 11515
	private int mSubType;

	// Token: 0x04002CFC RID: 11516
	private ConsignBuySubTabLogic.OnClickTabDelegate onClickTabDel;

	// Token: 0x02000AEE RID: 2798
	// (Invoke) Token: 0x06005041 RID: 20545
	public delegate void OnClickTabDelegate(int itemType, int subType, int index);
}
