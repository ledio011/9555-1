using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200099F RID: 2463
public class SlotRuleRootLogic : SingletonUnity<SlotRuleRootLogic>
{
	// Token: 0x060045C4 RID: 17860 RVA: 0x0015FADC File Offset: 0x0015DCDC
	public void Reset(List<slot_data> curlist)
	{
		this.InfoLabel.text = StrDictionary.GetDictionaryString("#{101632}", new object[0]);
		this.SlotDataList = curlist;
		this.SlotDataList.Sort(delegate(slot_data x, slot_data y)
		{
			if (x.ID.Length != y.ID.Length)
			{
				return x.ID.Length - y.ID.Length;
			}
			return x.ID.CompareTo(y.ID);
		});
		int num = this.SlotDataList.Count - this.slotItemsList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.slotItemsList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.parentGrid.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				SlotItemLogic component = gameObject.GetComponent<SlotItemLogic>();
				this.slotItemsList.Add(component);
				gameObject.gameObject.name = string.Format("slotitem{0:D2}", this.slotItemsList.Count - 1);
			}
		}
		for (int j = 0; j < this.slotItemsList.Count; j++)
		{
			if (j < this.SlotDataList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.slotItemsList[j].gameObject, true);
				this.slotItemsList[j].Reset(this.SlotDataList[j]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.slotItemsList[j].gameObject, false);
			}
		}
		this.parentGrid.Reposition();
	}

	// Token: 0x060045C5 RID: 17861 RVA: 0x0015FC84 File Offset: 0x0015DE84
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SlotRuleRoot);
	}

	// Token: 0x040032C8 RID: 13000
	public UILabel InfoLabel;

	// Token: 0x040032C9 RID: 13001
	public List<SlotItemLogic> slotItemsList;

	// Token: 0x040032CA RID: 13002
	public UIGrid parentGrid;

	// Token: 0x040032CB RID: 13003
	private List<slot_data> SlotDataList;
}
