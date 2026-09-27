using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000974 RID: 2420
public class PopTipsRoot : SingletonUnity<PopTipsRoot>
{
	// Token: 0x06004457 RID: 17495 RVA: 0x00154568 File Offset: 0x00152768
	public static void ShowPop(int type, object p)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.CurrentMapInofData != null && sceneManager.CurrentMapInofData.MapType == MAPTYPE.BAR_FIGHT_COPY)
		{
			return;
		}
		PopTipsRoot.typeList.Add(type);
		PopTipsRoot.typeParmList.Add(p);
		if (!SingletonUnity<PopTipsRoot>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<PopTipsRoot>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopTipsRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<PopTipsRoot>.Instance.PlayNext();
			}, null);
		}
	}

	// Token: 0x06004458 RID: 17496 RVA: 0x00154600 File Offset: 0x00152800
	public void PlayNext()
	{
		if (PopTipsRoot.typeList.Count > 0)
		{
			this.curType = PopTipsRoot.typeList[0];
		}
		this.tweener.ResetToBeginning();
		this.tweener.PlayForward();
		this.contentLabel.text = StrDictionary.GetDictionaryString("#{102052}", new object[0]);
	}

	// Token: 0x06004459 RID: 17497 RVA: 0x00154660 File Offset: 0x00152860
	public void PlayeFinish()
	{
		PopTipsRoot.typeList.RemoveAt(0);
		PopTipsRoot.typeParmList.RemoveAt(0);
		this.curType = -1;
		if (PopTipsRoot.typeList.Count <= 0)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTipsRoot);
		}
		else
		{
			this.PlayNext();
		}
	}

	// Token: 0x0600445A RID: 17498 RVA: 0x001546B4 File Offset: 0x001528B4
	public void OnClickNo()
	{
		this.PlayeFinish();
	}

	// Token: 0x0600445B RID: 17499 RVA: 0x001546BC File Offset: 0x001528BC
	public void OnClickYes()
	{
		if (this.curType == 0)
		{
			string id = (string)PopTipsRoot.typeParmList[0];
			enter_bar_fight.request request = new enter_bar_fight.request();
			request.ID = id;
			WaitResponseUIRootLogic.OpenWaitBox(207, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_bar_fight>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_4", "start");
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTipsRoot);
	}

	// Token: 0x040030EA RID: 12522
	private static List<int> typeList = new List<int>();

	// Token: 0x040030EB RID: 12523
	private static List<object> typeParmList = new List<object>();

	// Token: 0x040030EC RID: 12524
	public UILabel contentLabel;

	// Token: 0x040030ED RID: 12525
	public int curType;

	// Token: 0x040030EE RID: 12526
	public UITweener tweener;
}
