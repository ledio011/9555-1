using System;
using UnityEngine;

// Token: 0x020008ED RID: 2285
public class CopyDrugItemLogic : MonoBehaviour
{
	// Token: 0x06003DD8 RID: 15832 RVA: 0x00117698 File Offset: 0x00115898
	public void Reset(GameItem item, float time)
	{
		this.curItem = item;
		this.count = this.curItem.StackNum;
		ItemData itemData = item.ItemData;
		this.SpriteIcon.spriteName = itemData.BackPackIcon + "_Min";
		this.DrugLabel.text = item.StackNum.ToString();
		this.RemainLabel.text = string.Empty;
		this.curTime = time;
		this.UpdateTime(time);
		if (itemData.BackPackIcon.Contains("Red"))
		{
			this.GuangTiaoSprite.color = this.RedColor;
			this.GuangQuanSprite.color = this.RedColor;
		}
		else if (itemData.BackPackIcon.Contains("Blue"))
		{
			this.GuangTiaoSprite.color = this.BlueColor;
			this.GuangQuanSprite.color = this.BlueColor;
		}
		else
		{
			this.GuangTiaoSprite.color = this.YellowColor;
			this.GuangQuanSprite.color = this.YellowColor;
		}
	}

	// Token: 0x06003DD9 RID: 15833 RVA: 0x001177B0 File Offset: 0x001159B0
	public void UpdateTime(float time)
	{
		this.curTime = time;
		NGUITools.SetActive(this.TweenerObj, this.curTime > Time.realtimeSinceStartup);
		if (this.curTime <= Time.realtimeSinceStartup)
		{
			this.RemainLabel.text = string.Empty;
		}
		if (this.curItem.StackNum <= 0 && !UnityVersionUtil.IsActive(this.TweenerObj.gameObject))
		{
			NGUITools.SetActive(base.gameObject, false);
			SingletonUnity<CopyDrugUseUIRoot>.Instance.OnResetPosition(this.curItem);
		}
	}

	// Token: 0x06003DDA RID: 15834 RVA: 0x00117844 File Offset: 0x00115A44
	private void UpdateRemainTime()
	{
		if (this.curTime > Time.realtimeSinceStartup)
		{
			this.RemainLabel.text = string.Format("{0}", (int)(this.curTime - Time.realtimeSinceStartup + 0.5f));
		}
		else
		{
			this.RemainLabel.text = string.Empty;
		}
	}

	// Token: 0x06003DDB RID: 15835 RVA: 0x001178A4 File Offset: 0x00115AA4
	public void OnClickItem()
	{
		if (this.curItem.StackNum <= 0)
		{
			NoticeLogic.AddNotifyData2Client(false, "#{100145}", true, new object[]
			{
				this.curItem.ItemData.MName
			});
			return;
		}
		SingletonUnity<CopyDrugUseUIRoot>.Instance.UseItem(this, this.curItem);
	}

	// Token: 0x06003DDC RID: 15836 RVA: 0x001178FC File Offset: 0x00115AFC
	private void Update()
	{
		if (this.curTime >= 0f)
		{
			this.UpdateRemainTime();
			if (this.curTime < Time.realtimeSinceStartup)
			{
				this.curTime = -1f;
				this.UpdateTime(this.curTime);
			}
		}
	}

	// Token: 0x04002977 RID: 10615
	public UISprite SpriteIcon;

	// Token: 0x04002978 RID: 10616
	public UILabel DrugLabel;

	// Token: 0x04002979 RID: 10617
	public UILabel RemainLabel;

	// Token: 0x0400297A RID: 10618
	public GameObject TweenerObj;

	// Token: 0x0400297B RID: 10619
	private float curTime = -1f;

	// Token: 0x0400297C RID: 10620
	private GameItem curItem;

	// Token: 0x0400297D RID: 10621
	private int count;

	// Token: 0x0400297E RID: 10622
	private Color RedColor = new Color(1f, 0.20392157f, 0.1882353f, 1f);

	// Token: 0x0400297F RID: 10623
	private Color BlueColor = new Color(0.1882353f, 0.7607843f, 1f, 1f);

	// Token: 0x04002980 RID: 10624
	private Color YellowColor = new Color(1f, 0.9529412f, 0.1254902f, 1f);

	// Token: 0x04002981 RID: 10625
	public UISprite GuangTiaoSprite;

	// Token: 0x04002982 RID: 10626
	public UISprite GuangQuanSprite;
}
