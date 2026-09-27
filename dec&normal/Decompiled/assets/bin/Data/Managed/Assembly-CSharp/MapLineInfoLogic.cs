using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000952 RID: 2386
public class MapLineInfoLogic : SingletonUnity<MapLineInfoLogic>
{
	// Token: 0x060042BF RID: 17087 RVA: 0x00146DEC File Offset: 0x00144FEC
	private new void Awake()
	{
		base.Awake();
		this.UIWrapContent.onInitializeItem = new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem);
	}

	// Token: 0x060042C0 RID: 17088 RVA: 0x00146E0C File Offset: 0x0014500C
	public void PreReset()
	{
		this.UpdateItems();
	}

	// Token: 0x060042C1 RID: 17089 RVA: 0x00146E14 File Offset: 0x00145014
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		MapLineItemLogic itemLogic = this.MapLineItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x060042C2 RID: 17090 RVA: 0x00146E3C File Offset: 0x0014503C
	private void ResetItemLine(MapLineItemLogic itemLogic, int idx)
	{
		if (idx < this.curLineCount)
		{
			int num = idx * 2;
			int num2 = idx * 2 + 1;
			if (num2 >= this.curRealCount)
			{
				num2 = -2;
			}
			int state = -2;
			if (this.curLineStates.Count > num)
			{
				state = (int)this.curLineStates[num];
			}
			int state2 = -2;
			if (this.curLineStates.Count > num2 && num2 >= 0)
			{
				state2 = (int)this.curLineStates[num2];
			}
			itemLogic.UpdateLineState(num, state, num2, state2, idx);
		}
	}

	// Token: 0x060042C3 RID: 17091 RVA: 0x00146EC4 File Offset: 0x001450C4
	public void UpdateItems()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.curRealCount = playerData.LineCount;
		if (playerData.LineStates != null)
		{
			this.curLineStates = playerData.LineStates;
		}
		int num = (this.curRealCount + 1) / 2;
		this.curLineCount = num;
		int num2 = Mathf.Min(num, this.LineCount) - this.MapLineItems.Count;
		for (int i = 0; i < num2; i++)
		{
			GameObject gameObject = Object.Instantiate(this.MapLineItems[0].gameObject) as GameObject;
			gameObject.name = string.Format("LineItem_{0}", this.MapLineItems.Count);
			gameObject.transform.parent = this.MapLineItems[0].transform.parent;
			gameObject.transform.localScale = Vector3.one;
			this.MapLineItems.Add(gameObject.GetComponent<MapLineItemLogic>());
		}
		for (int j = 0; j < this.MapLineItems.Count; j++)
		{
			NGUITools.SetActive(this.MapLineItems[j].gameObject, j < num);
			if (j < num)
			{
				this.ResetItemLine(this.MapLineItems[j], j);
			}
		}
		this.UIWrapContent.maxIndex = 0;
		this.UIWrapContent.minIndex = 1 - num;
		this.WrapContentBottomWidget.height = num * this.UIWrapContent.itemSize;
		this.ScrollView.ResetPosition();
		this.UIWrapContent.SortBasedOnScrollMovement();
	}

	// Token: 0x060042C4 RID: 17092 RVA: 0x00147060 File Offset: 0x00145260
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MapLineInfoLogic);
	}

	// Token: 0x04002F11 RID: 12049
	public UIWrapContentNew UIWrapContent;

	// Token: 0x04002F12 RID: 12050
	public UIScrollView ScrollView;

	// Token: 0x04002F13 RID: 12051
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04002F14 RID: 12052
	public List<MapLineItemLogic> MapLineItems = new List<MapLineItemLogic>();

	// Token: 0x04002F15 RID: 12053
	public List<long> curLineStates = new List<long>();

	// Token: 0x04002F16 RID: 12054
	public int LineCount = 7;

	// Token: 0x04002F17 RID: 12055
	private int curLineCount;

	// Token: 0x04002F18 RID: 12056
	private int curRealCount;
}
