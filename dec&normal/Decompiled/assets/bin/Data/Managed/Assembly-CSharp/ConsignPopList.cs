using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200092F RID: 2351
public class ConsignPopList : MonoBehaviour
{
	// Token: 0x06004153 RID: 16723 RVA: 0x001369B0 File Offset: 0x00134BB0
	public void Init(List<int> itemIndexList, List<string> itemTxtList, ConsignPopItem.OnClickItemDelegate func)
	{
		int count = itemIndexList.Count;
		int num = count - this.PopItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.PopItemList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.ItemRoot;
				gameObject.transform.localScale = Vector3.one;
				this.PopItemList.Add(gameObject.GetComponent<ConsignPopItem>());
			}
		}
		else if (num < 0)
		{
			for (int j = 0; j > num; j--)
			{
				GameObject gameObject2 = this.PopItemList[this.PopItemList.Count - 1].gameObject;
				this.PopItemList.RemoveAt(this.PopItemList.Count - 1);
				Object.Destroy(gameObject2);
			}
		}
		for (int k = 0; k < count; k++)
		{
			this.PopItemList[k].Reset(itemIndexList[k], itemTxtList[k], new ConsignPopItem.OnClickItemDelegate(this.OnClickItem));
			this.PopItemList[k].transform.localPosition = new Vector3(0f, (float)(15 + (count - k - 1) * 30), 0f);
		}
		this.BottomPic.height = 30 * count;
		this.onClickItem = func;
	}

	// Token: 0x06004154 RID: 16724 RVA: 0x00136B28 File Offset: 0x00134D28
	public void OnClickItem(int index, string val)
	{
		this.tweenObj.PlayReverse();
		this.textLabel.text = val;
		if (this.onClickItem != null)
		{
			this.onClickItem(index, val);
		}
	}

	// Token: 0x06004155 RID: 16725 RVA: 0x00136B5C File Offset: 0x00134D5C
	private void OnDisable()
	{
		this.tweenObj.ResetToBeginning();
	}

	// Token: 0x06004156 RID: 16726 RVA: 0x00136B6C File Offset: 0x00134D6C
	public void CloseAnima()
	{
		this.tweenObj.ResetToBeginning();
	}

	// Token: 0x04002D1A RID: 11546
	public List<ConsignPopItem> PopItemList = new List<ConsignPopItem>();

	// Token: 0x04002D1B RID: 11547
	public Transform ItemRoot;

	// Token: 0x04002D1C RID: 11548
	public UISprite BottomPic;

	// Token: 0x04002D1D RID: 11549
	public TweenScale tweenObj;

	// Token: 0x04002D1E RID: 11550
	public UILabel textLabel;

	// Token: 0x04002D1F RID: 11551
	public ConsignPopItem.OnClickItemDelegate onClickItem;
}
