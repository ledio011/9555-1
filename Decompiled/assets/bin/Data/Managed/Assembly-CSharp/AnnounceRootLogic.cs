using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200091C RID: 2332
public class AnnounceRootLogic : SingletonUnity<AnnounceRootLogic>
{
	// Token: 0x060040C7 RID: 16583 RVA: 0x001324E4 File Offset: 0x001306E4
	public void Reset()
	{
		this.Refresh();
	}

	// Token: 0x060040C8 RID: 16584 RVA: 0x001324EC File Offset: 0x001306EC
	private void OnEnable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Refresh));
	}

	// Token: 0x060040C9 RID: 16585 RVA: 0x0013251C File Offset: 0x0013071C
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Refresh));
	}

	// Token: 0x060040CA RID: 16586 RVA: 0x0013254C File Offset: 0x0013074C
	private void Refresh()
	{
		this.mAnnounceData = null;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		List<AnnounceData> announceDataList = DataManager.GetAnnounceDataList();
		for (int i = 0; i < announceDataList.Count; i++)
		{
			if (announceDataList[i].StartLevel <= level && announceDataList[i].EndLevel > level)
			{
				this.mAnnounceData = announceDataList[i];
				break;
			}
		}
		if (this.mAnnounceData != null)
		{
			NGUITools.SetActive(this.ObjRoot, true);
			if (this.mAnnounceData.Type == 0)
			{
				NGUITools.SetActive(this.ItemSprite.gameObject, false);
				this.FuncIconSprite.spriteName = this.mAnnounceData.ICON;
				this.NameLabel1.text = this.mAnnounceData.MName1;
				this.NameLabel2.text = this.mAnnounceData.MName2;
				this.FuncIconSprite.width = 40;
				this.FuncIconSprite.height = 40;
			}
			else if (this.mAnnounceData.Type == 1)
			{
				NGUITools.SetActive(this.ItemSprite.gameObject, false);
				this.FuncIconSprite.spriteName = this.mAnnounceData.ICON;
				this.NameLabel1.text = this.mAnnounceData.MName1;
				this.NameLabel2.text = this.mAnnounceData.MName2;
				this.FuncIconSprite.width = 36;
				this.FuncIconSprite.height = 36;
			}
			else
			{
				if (string.IsNullOrEmpty(this.mAnnounceData.ItemId))
				{
					NGUITools.SetActive(this.ObjRoot, false);
					return;
				}
				NGUITools.SetActive(this.FuncIconSprite.gameObject, false);
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mAnnounceData.ItemId);
				if (itemDataByID != null)
				{
					this.ItemSprite.spriteName = itemDataByID.BackPackIcon;
					this.ItemQualitySprite.spriteName = ((EQUIP_QUALITY)this.mAnnounceData.quality).ToString();
				}
				this.NameLabel1.text = this.mAnnounceData.MName1;
				this.NameLabel2.text = this.mAnnounceData.MName2;
			}
		}
		else
		{
			NGUITools.SetActive(this.ObjRoot, false);
		}
	}

	// Token: 0x04002C5E RID: 11358
	public GameObject ObjRoot;

	// Token: 0x04002C5F RID: 11359
	public UILabel NameLabel1;

	// Token: 0x04002C60 RID: 11360
	public UILabel NameLabel2;

	// Token: 0x04002C61 RID: 11361
	public UISprite ItemSprite;

	// Token: 0x04002C62 RID: 11362
	public UISprite ItemQualitySprite;

	// Token: 0x04002C63 RID: 11363
	public UISprite FuncIconSprite;

	// Token: 0x04002C64 RID: 11364
	private AnnounceData mAnnounceData;
}
