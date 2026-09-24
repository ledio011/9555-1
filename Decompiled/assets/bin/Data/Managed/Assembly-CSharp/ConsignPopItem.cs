using System;
using UnityEngine;

// Token: 0x0200092E RID: 2350
public class ConsignPopItem : MonoBehaviour
{
	// Token: 0x06004150 RID: 16720 RVA: 0x00136920 File Offset: 0x00134B20
	public void Reset(int index, string str, ConsignPopItem.OnClickItemDelegate func)
	{
		string text = StrDictionary.GetDictionaryString(str, new object[0]);
		int num = text.LastIndexOf(' ');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		this.ItemLabel.text = text;
		this.mItemIndex = index;
		this.onClickItem = func;
	}

	// Token: 0x06004151 RID: 16721 RVA: 0x00136970 File Offset: 0x00134B70
	public void OnClickItem()
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(this.mItemIndex, this.ItemLabel.text);
		}
	}

	// Token: 0x04002D17 RID: 11543
	private int mItemIndex;

	// Token: 0x04002D18 RID: 11544
	public UILabel ItemLabel;

	// Token: 0x04002D19 RID: 11545
	private ConsignPopItem.OnClickItemDelegate onClickItem;

	// Token: 0x02000AF1 RID: 2801
	// (Invoke) Token: 0x0600504D RID: 20557
	public delegate void OnClickItemDelegate(int index, string val);
}
