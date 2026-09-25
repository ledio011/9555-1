using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200084E RID: 2126
public class GuildApplyListLogic : SingletonUnity<GuildApplyListLogic>
{
	// Token: 0x06003705 RID: 14085 RVA: 0x000E20E0 File Offset: 0x000E02E0
	public void UpdataApplyList(List<GuildMember> list)
	{
		int num = list.Count - this.ApplyList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.ApplyList[0].gameObject) as GameObject;
				this.ApplyGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.ApplyList.Add(gameObject.GetComponent<GuildApplyListItemLogic>());
			}
		}
		for (int j = 0; j < this.ApplyList.Count; j++)
		{
			NGUITools.SetActive(this.ApplyList[j].gameObject, j < list.Count);
		}
		this.ApplyGrid.Reposition();
		int num2 = 0;
		for (int k = 0; k < list.Count; k++)
		{
			this.ApplyList[num2++].InitApplyListItem(list[k]);
		}
	}

	// Token: 0x06003706 RID: 14086 RVA: 0x000E21EC File Offset: 0x000E03EC
	public void AllAgree()
	{
		for (int i = this.ApplyList.Count - 1; i >= 0; i--)
		{
			this.ApplyList[i].Agree();
		}
	}

	// Token: 0x06003707 RID: 14087 RVA: 0x000E2228 File Offset: 0x000E0428
	public void DelAll()
	{
		for (int i = this.ApplyList.Count - 1; i >= 0; i--)
		{
			this.ApplyList[i].DisAgree();
		}
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x000E2264 File Offset: 0x000E0464
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildApplyListLogicRoot);
	}

	// Token: 0x0400242D RID: 9261
	public List<GuildApplyListItemLogic> ApplyList = new List<GuildApplyListItemLogic>();

	// Token: 0x0400242E RID: 9262
	public UIGrid ApplyGrid;
}
