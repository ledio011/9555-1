using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008F5 RID: 2293
public class DailyCopySubLineLogic : MonoBehaviour
{
	// Token: 0x17000F85 RID: 3973
	// (get) Token: 0x06003E49 RID: 15945 RVA: 0x0011C204 File Offset: 0x0011A404
	public string Key
	{
		get
		{
			return this.mKey;
		}
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x0011C20C File Offset: 0x0011A40C
	public void ResetItem(int index, copyscene_info info, DelegateDefine.StringGameObjectDelegate clickFunc)
	{
		this.enableLineFlag = true;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mKey = info.ID;
		this.onClickItem = clickFunc;
		CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(info.ID);
		int num = (int)info.CurNum;
		if (num < 0)
		{
			num = 0;
		}
		NGUITools.SetActive(this.DurationLabel.gameObject, false);
		if (copySceneDataById.SubType == 16)
		{
			this.TimesLabel.enabled = false;
		}
		else
		{
			this.TimesLabel.enabled = true;
			this.TimesLabel.text = string.Format("{0}  {1}", StrDictionary.GetDictionaryString("#{101636}", new object[0]), TimeTools.GetMinuteSecondStr(num));
		}
		this.IconSprite.spriteName = copySceneDataById.Icon;
		if (copySceneDataById.SubType != 1 && num <= 0)
		{
			this.enableLineFlag = false;
		}
		this.NameLabel.text = StrDictionary.GetDictionaryString(copySceneDataById.Name, new object[0]);
		this.LevelLabel.text = string.Format("Lv {0}", copySceneDataById.MinLevel);
		if (!this.CheckLevel(copySceneDataById.MinLevel))
		{
			this.SetLabelWarining(this.LevelLabel, true);
			this.enableLineFlag = false;
		}
		else
		{
			this.SetLabelWarining(this.LevelLabel, false);
		}
		this.RefreshSelect(string.Empty);
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x0011C374 File Offset: 0x0011A574
	public void OnClikcItemBtn()
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(this.mKey, base.gameObject);
		}
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x0011C3A4 File Offset: 0x0011A5A4
	public bool CheckLevel(int minLevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel);
	}

	// Token: 0x06003E4D RID: 15949 RVA: 0x0011C3B8 File Offset: 0x0011A5B8
	public bool RefreshSelect(string key)
	{
		if (this.enableLineFlag)
		{
			this.SelectBkSprite.spriteName = "CZ_wuPinYanSe_3";
		}
		else
		{
			this.SelectBkSprite.spriteName = "CZ_tongYongDi_zhuYao_4_1";
		}
		if (!this.mKey.Equals(key))
		{
			UnityVersionUtil.SetActiveRecursive(this.SelectUpSprite, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.SelectUpSprite, true);
		}
		return this.mKey.Equals(key);
	}

	// Token: 0x06003E4E RID: 15950 RVA: 0x0011C430 File Offset: 0x0011A630
	private void SetLabelWarining(UILabel label, bool needWarning)
	{
		if (needWarning)
		{
			label.color = Color.red;
		}
		else
		{
			label.color = Color.white;
		}
	}

	// Token: 0x040029EE RID: 10734
	public UILabel NameLabel;

	// Token: 0x040029EF RID: 10735
	public UISprite IconSprite;

	// Token: 0x040029F0 RID: 10736
	public UILabel TimesLabel;

	// Token: 0x040029F1 RID: 10737
	public UILabel DurationLabel;

	// Token: 0x040029F2 RID: 10738
	public UILabel LevelLabel;

	// Token: 0x040029F3 RID: 10739
	public UISprite SelectBkSprite;

	// Token: 0x040029F4 RID: 10740
	public GameObject SelectUpSprite;

	// Token: 0x040029F5 RID: 10741
	public DelegateDefine.StringGameObjectDelegate onClickItem;

	// Token: 0x040029F6 RID: 10742
	private string mKey = string.Empty;

	// Token: 0x040029F7 RID: 10743
	public bool enableLineFlag;
}
