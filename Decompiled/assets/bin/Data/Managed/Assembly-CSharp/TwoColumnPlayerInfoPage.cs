using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009CA RID: 2506
public class TwoColumnPlayerInfoPage : MonoBehaviour
{
	// Token: 0x06004760 RID: 18272 RVA: 0x0016BD90 File Offset: 0x00169F90
	private void Awake()
	{
		this.WrapContent.onInitializeItem = new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem);
	}

	// Token: 0x06004761 RID: 18273 RVA: 0x0016BDAC File Offset: 0x00169FAC
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		SmallPlayerInfoLine smallPlayerInfoLine = this.mPlayerInfoLineList[index];
		PlayerInfoItemData leftData = null;
		PlayerInfoItemData rightData = null;
		realIndex = Mathf.Abs(realIndex);
		if (this.mCurPageDataList != null)
		{
			if (this.mCurPageDataList.Count > realIndex * 2)
			{
				leftData = this.mCurPageDataList[realIndex * 2];
			}
			if (this.mCurPageDataList.Count > realIndex * 2 + 1)
			{
				rightData = this.mCurPageDataList[realIndex * 2 + 1];
			}
		}
		smallPlayerInfoLine.Reset(leftData, rightData, this.mBtnName, this.mDisableBtnName, new DelegateDefine.OneLongParamDelegate(this.OnClickBtn));
	}

	// Token: 0x06004762 RID: 18274 RVA: 0x0016BE48 File Offset: 0x0016A048
	public void Reset(List<PlayerInfoItemData> dataList, string btnName, string disableBtnName, DelegateDefine.OneLongParamDelegate func)
	{
		if (dataList == null || dataList.Count == 0)
		{
			for (int i = 0; i < this.mPlayerInfoLineList.Count; i++)
			{
				NGUITools.SetActive(this.mPlayerInfoLineList[i].gameObject, false);
			}
			return;
		}
		this.onClickBtn = func;
		this.mBtnName = btnName;
		this.mDisableBtnName = disableBtnName;
		this.mCurPageDataList = dataList;
		if (this.mPlayerInfoLineList.Count < this.MaxLineCount && this.mPlayerInfoLineList.Count < (dataList.Count + 1) / 2)
		{
			int num = Mathf.Min(this.MaxLineCount - this.mPlayerInfoLineList.Count, (dataList.Count + 1) / 2 - this.mPlayerInfoLineList.Count);
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.mPlayerInfoLineList[0].gameObject) as GameObject;
				gameObject.name = string.Format("{0}", this.mPlayerInfoLineList.Count);
				SmallPlayerInfoLine component = gameObject.GetComponent<SmallPlayerInfoLine>();
				this.mPlayerInfoLineList.Add(component);
				gameObject.transform.parent = this.mPlayerInfoLineList[0].transform.parent;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = new Vector3(0f, (float)(-(float)this.mPlayerInfoLineList.Count * this.WrapContent.itemSize), 0f);
			}
		}
		for (int k = 0; k < this.mPlayerInfoLineList.Count; k++)
		{
			NGUITools.SetActive(this.mPlayerInfoLineList[k].gameObject, k < this.MaxLineCount && k < (dataList.Count + 1) / 2);
		}
		int maxIndex = 0;
		int num2 = -((dataList.Count + 1) / 2 - 1);
		this.BottomWidget.height = (Mathf.Abs(num2) + 1) * this.WrapContent.itemSize;
		this.WrapContent.maxIndex = maxIndex;
		this.WrapContent.minIndex = num2;
		this.WrapContent.SortBasedOnScrollMovement();
		this.ScrollView.ResetPosition();
		this.ScrollBar.value = 0f;
	}

	// Token: 0x06004763 RID: 18275 RVA: 0x0016C0A4 File Offset: 0x0016A2A4
	private void OnClickBtn(long key)
	{
		if (this.onClickBtn != null)
		{
			this.onClickBtn(key);
		}
	}

	// Token: 0x04003489 RID: 13449
	public UIGrid GrideRoot;

	// Token: 0x0400348A RID: 13450
	public UIScrollBar ScrollBar;

	// Token: 0x0400348B RID: 13451
	public UIScrollView ScrollView;

	// Token: 0x0400348C RID: 13452
	public UIWrapContentNew WrapContent;

	// Token: 0x0400348D RID: 13453
	public List<SmallPlayerInfoLine> mPlayerInfoLineList;

	// Token: 0x0400348E RID: 13454
	public UIWidget BottomWidget;

	// Token: 0x0400348F RID: 13455
	public int MaxLineCount = 6;

	// Token: 0x04003490 RID: 13456
	private List<PlayerInfoItemData> mCurPageDataList;

	// Token: 0x04003491 RID: 13457
	private DelegateDefine.OneLongParamDelegate onClickBtn;

	// Token: 0x04003492 RID: 13458
	private string mBtnName;

	// Token: 0x04003493 RID: 13459
	private string mDisableBtnName;
}
