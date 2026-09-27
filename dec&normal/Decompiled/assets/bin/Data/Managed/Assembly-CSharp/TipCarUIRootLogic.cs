using System;
using UnityEngine;

// Token: 0x0200096B RID: 2411
public class TipCarUIRootLogic : SingletonUnity<TipCarUIRootLogic>
{
	// Token: 0x060043D8 RID: 17368 RVA: 0x00150B4C File Offset: 0x0014ED4C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060043D9 RID: 17369 RVA: 0x00150B58 File Offset: 0x0014ED58
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060043DA RID: 17370 RVA: 0x00150B78 File Offset: 0x0014ED78
	private void ResetFakeCarObjRoot()
	{
		FakeCarObjRootLogic instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeCarObjRoot");
			instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		}
		SingletonUnity<FakeCarObjRootLogic>.Instance.EnableFakeObjRoot();
		this.CarModelPic.mainTexture = instance.ModelPic;
	}

	// Token: 0x060043DB RID: 17371 RVA: 0x00150BC4 File Offset: 0x0014EDC4
	private void ResetCarModelVisual(MountData mountData, ColorData colorda)
	{
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, colorda);
	}

	// Token: 0x060043DC RID: 17372 RVA: 0x00150BD4 File Offset: 0x0014EDD4
	public void Reset(string mountId, string messageid = "#{101425}")
	{
		this.ResetFakeCarObjRoot();
		MountData mountDataById = DataManager.GetMountDataById(mountId);
		this.ResetCarModelVisual(mountDataById, DataManager.GetColorDataById(mountDataById.DefaultColorId));
		this.TextLabel.text = StrDictionary.GetDictionaryString(messageid, new object[]
		{
			mountDataById.MCarName
		});
		this.curMountId = mountId;
	}

	// Token: 0x060043DD RID: 17373 RVA: 0x00150C28 File Offset: 0x0014EE28
	public void OnClickCloseBtn()
	{
		this.ClosePage();
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_SHOW_NEXT)
		{
			this.mOnClickTutorialBtn = null;
			TutorialManager.CloseTutorial();
		}
	}

	// Token: 0x060043DE RID: 17374 RVA: 0x00150C48 File Offset: 0x0014EE48
	private void ClosePage()
	{
		if ((!SingletonUnity<PlayerCarRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject)) && SingletonUnity<FakeCarObjRootLogic>.Exists && SingletonUnity<FakeCarObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.DisableFakeObjRoot();
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TipCarUIRoot);
	}

	// Token: 0x060043DF RID: 17375 RVA: 0x00150CA8 File Offset: 0x0014EEA8
	public void OnClickYesBtn()
	{
		this.ClosePage();
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.IsGetSignWeekCar())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerCarRoot, delegate
			{
				SingletonUnity<PlayerCarRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(235, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_mount_info>(null, null);
			}, null);
		}
	}

	// Token: 0x04003085 RID: 12421
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003086 RID: 12422
	public UITexture CarModelPic;

	// Token: 0x04003087 RID: 12423
	public UILabel TextLabel;

	// Token: 0x04003088 RID: 12424
	private Color ambientLight;

	// Token: 0x04003089 RID: 12425
	public UIWidget YesBtn;

	// Token: 0x0400308A RID: 12426
	private string curMountId = string.Empty;
}
