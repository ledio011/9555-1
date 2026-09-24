using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009C4 RID: 2500
public class TeamTargetTab : MonoBehaviour
{
	// Token: 0x06004730 RID: 18224 RVA: 0x0016AD5C File Offset: 0x00168F5C
	public void Reset(List<TeamTargetTabData> targetData, DelegateDefine.OneStringParamDelegate onClick)
	{
		TeamTargetTabData teamTargetTabData = null;
		for (int i = targetData.Count - 1; i >= 0; i--)
		{
			if (targetData[i].Key.Equals("1"))
			{
				teamTargetTabData = targetData[i];
				targetData.RemoveAt(i);
				break;
			}
		}
		for (int j = targetData.Count - 1; j >= 0; j--)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(targetData[j].Key);
			if (!this.CheckLevel(copySceneDataById.MinLevel, copySceneDataById.MaxLevel))
			{
				targetData.RemoveAt(j);
			}
		}
		targetData.Sort((TeamTargetTabData x, TeamTargetTabData y) => x.Key.CompareTo(y.Key));
		if (teamTargetTabData != null)
		{
			targetData.Add(teamTargetTabData);
		}
		this.onClickTab = onClick;
		int num = targetData.Count - this.TargetTabLineList.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.TargetTabLineList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.LineRoot.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				TeamTargetTabLine component = gameObject.GetComponent<TeamTargetTabLine>();
				this.TargetTabLineList.Add(component);
			}
		}
		for (int l = 0; l < this.TargetTabLineList.Count; l++)
		{
			this.TargetTabLineList[l].gameObject.name = l.ToString();
			if (l < targetData.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.TargetTabLineList[l].gameObject, true);
				this.TargetTabLineList[l].Reset(targetData[l].Title, targetData[l].SubTitle, targetData[l].Key, targetData[l].SubKey, new DelegateDefine.StringGameObjectDelegate(this.OnClickTab));
				this.TargetTabLineList[l].transform.localPosition = new Vector3(0f, (float)(l * -46), 0f);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.TargetTabLineList[l].gameObject, false);
			}
		}
		this.LineRoot.Reposition();
		this.mCurChooseLineID = string.Empty;
	}

	// Token: 0x06004731 RID: 18225 RVA: 0x0016AFE8 File Offset: 0x001691E8
	public bool CheckLevel(int minLevel, int maxLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxLevel);
	}

	// Token: 0x06004732 RID: 18226 RVA: 0x0016AFFC File Offset: 0x001691FC
	private void OnClickTab(string key, GameObject obj)
	{
		if (!this.mCurChooseLineID.Equals(key))
		{
			this.mCurChooseLineID = key;
			if (this.mPreChoosedPic != null)
			{
				if (this.mIsPreIsSub)
				{
					this.mPreChoosedPic.spriteName = "CZ_huaDongBG_2";
				}
				else
				{
					this.mPreChoosedPic.spriteName = "CZ_huaDongBG";
				}
			}
			this.mPreChoosedPic = obj.GetComponent<UISprite>();
			if (obj.transform.childCount > 2)
			{
				this.mIsPreIsSub = false;
				this.mPreChoosedPic.spriteName = "CZ_huaDongBG_1";
			}
			else
			{
				this.mIsPreIsSub = true;
				this.mPreChoosedPic.spriteName = "CZ_huaDongBG_2_Xuan";
			}
			if (this.onClickTab != null)
			{
				this.onClickTab(this.mCurChooseLineID);
			}
		}
	}

	// Token: 0x04003457 RID: 13399
	public UITable LineRoot;

	// Token: 0x04003458 RID: 13400
	public List<TeamTargetTabLine> TargetTabLineList;

	// Token: 0x04003459 RID: 13401
	private string mCurChooseLineID = string.Empty;

	// Token: 0x0400345A RID: 13402
	private DelegateDefine.OneStringParamDelegate onClickTab;

	// Token: 0x0400345B RID: 13403
	private UISprite mPreChoosedPic;

	// Token: 0x0400345C RID: 13404
	private bool mIsPreIsSub;
}
