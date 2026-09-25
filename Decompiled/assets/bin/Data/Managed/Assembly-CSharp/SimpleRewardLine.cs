using System;
using UnityEngine;

// Token: 0x02000992 RID: 2450
public class SimpleRewardLine : MonoBehaviour
{
	// Token: 0x06004564 RID: 17764 RVA: 0x0015C57C File Offset: 0x0015A77C
	public void Reset(ItemData curItem, int count, EQUIP_QUALITY quality, SimpleRewardLine.OnFinished func)
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		string label = string.Empty;
		switch (curItem.Type)
		{
		case GameDefine.ITEM_TYPE.EQUIP:
			label = StrDictionary.GetDictionaryString("#{100139}", new object[]
			{
				curItem.MName
			}) + string.Format("[ffff00]+{0}[-]", count);
			goto IL_111;
		case GameDefine.ITEM_TYPE.ADD_COIN:
			label = GameMoneyHelper.GetMoneyPre(GameDefine.MONEY_TYPE.CASH) + string.Format(" [ffff00]+{0}[-]", count);
			goto IL_111;
		case GameDefine.ITEM_TYPE.ADD_GOLD:
			label = GameMoneyHelper.GetMoneyPre(GameDefine.MONEY_TYPE.GOLD) + string.Format(" [ffff00]+{0}[-]", count);
			goto IL_111;
		case GameDefine.ITEM_TYPE.ADD_DIAMOND:
			label = GameMoneyHelper.GetMoneyPre(GameDefine.MONEY_TYPE.DIAMOND) + string.Format(" [ffff00]+{0}[-]", count);
			goto IL_111;
		}
		label = StrDictionary.GetDictionaryString("#{100139}", new object[]
		{
			curItem.MName
		}) + string.Format("[ffff00]+{0}[-]", count);
		IL_111:
		this.onFinished = func;
		this.Reset(label);
	}

	// Token: 0x06004565 RID: 17765 RVA: 0x0015C6AC File Offset: 0x0015A8AC
	private void Reset(string label)
	{
		this.NumLabel.text = label;
		this.TwPosition.ResetToBeginning();
		this.TwAlph.ResetToBeginning();
		this.TwPosition.PlayForward();
		this.TwAlph.PlayForward();
	}

	// Token: 0x06004566 RID: 17766 RVA: 0x0015C6F4 File Offset: 0x0015A8F4
	public void OnFlyFinished()
	{
		if (this.onFinished != null)
		{
			this.onFinished(this);
		}
	}

	// Token: 0x04003244 RID: 12868
	public UILabel NumLabel;

	// Token: 0x04003245 RID: 12869
	public UISprite IconSprite;

	// Token: 0x04003246 RID: 12870
	public UISprite QualitySprite;

	// Token: 0x04003247 RID: 12871
	public TweenPosition TwPosition;

	// Token: 0x04003248 RID: 12872
	public TweenAlpha TwAlph;

	// Token: 0x04003249 RID: 12873
	public SimpleRewardLine.OnFinished onFinished;

	// Token: 0x02000AFA RID: 2810
	// (Invoke) Token: 0x06005071 RID: 20593
	public delegate void OnFinished(SimpleRewardLine obj);
}
