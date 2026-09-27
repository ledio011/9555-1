using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000937 RID: 2359
public class DanceChooseRootLogic : SingletonUnity<DanceChooseRootLogic>
{
	// Token: 0x06004199 RID: 16793 RVA: 0x00138620 File Offset: 0x00136820
	private void OnEnable()
	{
		this.Reset();
	}

	// Token: 0x0600419A RID: 16794 RVA: 0x00138628 File Offset: 0x00136828
	public void OnClickFreeDanceBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsInGatherArea(Singleton<ObjManager>.Instance.MainPlayer.Position))
		{
			this.OnClickCloseBtn();
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		use_dance.request request = new use_dance.request();
		request.id = playerData.PlayerDanceData.CurNormalDanceData.ID;
		NetLogic.GetInstance().Send<Protocol.use_dance>(request, null);
		this.OnClickCloseBtn();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "dance", string.Format("usedance_{0}", request.id));
	}

	// Token: 0x0600419B RID: 16795 RVA: 0x001386BC File Offset: 0x001368BC
	public void OnClickSpecialDanceBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsInGatherArea(Singleton<ObjManager>.Instance.MainPlayer.Position))
		{
			this.OnClickCloseBtn();
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerDanceData.IsSpecialDanceDataEnable())
		{
			Singleton<ObjManager>.Instance.MainPlayer.StopDance();
			use_dance.request request = new use_dance.request();
			request.id = playerData.PlayerDanceData.CurSpecialDanceData.ID;
			NetLogic.GetInstance().Send<Protocol.use_dance>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "dance", string.Format("usedance_{0}", request.id));
		}
		else if (GameMoneyHelper.BeforeCheckBuy(playerData.PlayerDanceData.CurSpecialDanceData.PriceType, playerData.PlayerDanceData.CurSpecialDanceData.Price))
		{
			Singleton<ObjManager>.Instance.MainPlayer.StopDance();
			use_dance.request request2 = new use_dance.request();
			request2.id = playerData.PlayerDanceData.CurSpecialDanceData.ID;
			NetLogic.GetInstance().Send<Protocol.use_dance>(request2, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "dance", string.Format("usedance_{0}", request2.id));
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "dance", string.Format("buydance_{0}", request2.id));
		}
		this.OnClickCloseBtn();
	}

	// Token: 0x0600419C RID: 16796 RVA: 0x00138820 File Offset: 0x00136A20
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DanceChooseRoot);
	}

	// Token: 0x0600419D RID: 16797 RVA: 0x00138834 File Offset: 0x00136A34
	public void Reset()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerDanceData.IsSpecialDanceDataEnable())
		{
			NGUITools.SetActive(this.CostLabel.gameObject, false);
			this.DanceNameLabel.transform.localPosition = Vector3.zero;
		}
		else
		{
			NGUITools.SetActive(this.CostLabel.gameObject, true);
			GameDefine.MONEY_TYPE priceType = (GameDefine.MONEY_TYPE)playerData.PlayerDanceData.CurSpecialDanceData.PriceType;
			this.CostLabel.text = GameMoneyHelper.GetMoneyValStr(playerData.PlayerDanceData.CurSpecialDanceData.Price, priceType);
			this.DanceNameLabel.transform.localPosition = Vector3.up * 8f;
		}
	}

	// Token: 0x04002D77 RID: 11639
	public UILabel CostLabel;

	// Token: 0x04002D78 RID: 11640
	public UILabel DanceNameLabel;
}
