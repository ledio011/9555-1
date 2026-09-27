using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A4A RID: 2634
public class SocialDanceUIRoot : SingletonUnity<SocialDanceUIRoot>
{
	// Token: 0x06004CC5 RID: 19653 RVA: 0x001A0CF4 File Offset: 0x0019EEF4
	public void Reset()
	{
		List<SocialDanceData> list = new List<SocialDanceData>(DataManager.GetAllSocialDanceData().Values);
		SocialDanceUIRoot.progress = Mathf.Clamp01((SocialDanceUIRoot.CDTime - Time.realtimeSinceStartup) / 5f);
		if (list == null || list.Count == 0)
		{
			return;
		}
		int num = list.Count - this.SocailDanceItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.SocailDanceItemList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.SocailDanceItemList[0].transform.parent;
				gameObject.name = string.Format("{0:D2}", this.SocailDanceItemList.Count);
				gameObject.transform.localScale = Vector3.one;
				this.SocailDanceItemList.Add(gameObject.GetComponent<SocialDanceItem>());
			}
		}
		for (int j = 0; j < this.SocailDanceItemList.Count; j++)
		{
			if (j < list.Count)
			{
				this.SocailDanceItemList[j].Init(list[j]);
			}
			else
			{
				NGUITools.SetActive(this.SocailDanceItemList[j].gameObject, false);
			}
		}
		this.grid.Reposition();
	}

	// Token: 0x06004CC6 RID: 19654 RVA: 0x001A0E58 File Offset: 0x0019F058
	public void OnClickSocial()
	{
		if (SingletonUnity<SocialDanceUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SocialDanceUIRoot>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SocialDanceRoot);
		}
	}

	// Token: 0x06004CC7 RID: 19655 RVA: 0x001A0E94 File Offset: 0x0019F094
	private void Update()
	{
		if (SocialDanceUIRoot.CDTime > 0f)
		{
			SocialDanceUIRoot.progress = Mathf.Clamp01((SocialDanceUIRoot.CDTime - Time.realtimeSinceStartup) / 5f);
			if (SocialDanceUIRoot.progress <= 0f)
			{
				SocialDanceUIRoot.CDTime = -1f;
			}
		}
	}

	// Token: 0x04003A62 RID: 14946
	public const int MAX_CD = 5;

	// Token: 0x04003A63 RID: 14947
	public List<SocialDanceItem> SocailDanceItemList = new List<SocialDanceItem>();

	// Token: 0x04003A64 RID: 14948
	public UIGrid grid;

	// Token: 0x04003A65 RID: 14949
	public static float CDTime = -1f;

	// Token: 0x04003A66 RID: 14950
	public static float progress;
}
