using System;
using UnityEngine;

// Token: 0x02000A3D RID: 2621
public class PotionItem : MonoBehaviour
{
	// Token: 0x06004C73 RID: 19571 RVA: 0x0019EA0C File Offset: 0x0019CC0C
	private void Start()
	{
	}

	// Token: 0x06004C74 RID: 19572 RVA: 0x0019EA10 File Offset: 0x0019CC10
	public void Init(GameItem gameItem)
	{
		this.useItem = gameItem;
		ItemData itemDataByID = DataManager.GetItemDataByID(this.useItem.ItemId);
		this.IconSprite.spriteName = itemDataByID.BackPackIcon + "_Min";
		this.NumLabel.text = gameItem.StackNum.ToString();
	}

	// Token: 0x06004C75 RID: 19573 RVA: 0x0019EA6C File Offset: 0x0019CC6C
	public void ClickUseItem()
	{
		Debug.Log("ClickUseItem");
		if (SingletonUnity<PotionLogic>.Exists)
		{
			SingletonUnity<PotionLogic>.Instance.ClickSelectItem(this.useItem);
		}
	}

	// Token: 0x04003A24 RID: 14884
	public UISprite IconSprite;

	// Token: 0x04003A25 RID: 14885
	public UILabel NumLabel;

	// Token: 0x04003A26 RID: 14886
	private GameItem useItem;
}
