using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A04 RID: 2564
public class DanceTipLogic : MonoBehaviour
{
	// Token: 0x06004958 RID: 18776 RVA: 0x0017B128 File Offset: 0x00179328
	private void Awake()
	{
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06004959 RID: 18777 RVA: 0x0017B160 File Offset: 0x00179360
	public void Reset()
	{
		this.mPlayerdata = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CurDanceList = new List<dance_state_info>(this.mPlayerdata.ActivityData.CurDanceStateDic.Values);
		if (this.CurDanceList != null || this.CurDanceList.Count > 0)
		{
			int num = Mathf.Min(this.CurDanceList.Count, this.lineMinCount) - this.DanceList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.DanceList[0].gameObject) as GameObject;
					DanceTipLineLogic component = gameObject.GetComponent<DanceTipLineLogic>();
					gameObject.name = string.Format("{0:D2}", this.DanceList.Count);
					gameObject.transform.parent = this.uiWrapContent.transform;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localPosition = Vector3.zero;
					this.DanceList.Add(component);
				}
			}
			for (int j = 0; j < this.DanceList.Count; j++)
			{
				if (j < this.CurDanceList.Count)
				{
					NGUITools.SetActive(this.DanceList[j].gameObject, true);
					this.DanceList[j].Reset(this.CurDanceList[j]);
				}
				else
				{
					NGUITools.SetActive(this.DanceList[j].gameObject, false);
				}
			}
		}
		else
		{
			for (int k = 0; k < this.DanceList.Count; k++)
			{
				NGUITools.SetActive(this.DanceList[k].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.CurDanceList.Count;
		this.WrapContentBottomWidget.height = this.CurDanceList.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
	}

	// Token: 0x0600495A RID: 18778 RVA: 0x0017B39C File Offset: 0x0017959C
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		DanceTipLineLogic itemLogic = this.DanceList[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x0600495B RID: 18779 RVA: 0x0017B3C4 File Offset: 0x001795C4
	private void ResetItemLine(DanceTipLineLogic itemLogic, int idx)
	{
		if (idx < this.CurDanceList.Count)
		{
			itemLogic.Reset(this.CurDanceList[idx]);
		}
	}

	// Token: 0x040036A6 RID: 13990
	public List<DanceTipLineLogic> DanceList;

	// Token: 0x040036A7 RID: 13991
	private PlayerData mPlayerdata;

	// Token: 0x040036A8 RID: 13992
	private List<dance_state_info> CurDanceList;

	// Token: 0x040036A9 RID: 13993
	public UIWrapContentNew uiWrapContent;

	// Token: 0x040036AA RID: 13994
	private int lineMinCount = 6;

	// Token: 0x040036AB RID: 13995
	public UIWidget WrapContentBottomWidget;

	// Token: 0x040036AC RID: 13996
	public UIScrollView uiScrollView;
}
