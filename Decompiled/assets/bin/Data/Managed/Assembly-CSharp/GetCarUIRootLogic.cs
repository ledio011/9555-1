using System;
using UnityEngine;

// Token: 0x02000968 RID: 2408
public class GetCarUIRootLogic : SingletonUnity<GetCarUIRootLogic>
{
	// Token: 0x0600439F RID: 17311 RVA: 0x0014ECF4 File Offset: 0x0014CEF4
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060043A0 RID: 17312 RVA: 0x0014ED00 File Offset: 0x0014CF00
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060043A1 RID: 17313 RVA: 0x0014ED20 File Offset: 0x0014CF20
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

	// Token: 0x060043A2 RID: 17314 RVA: 0x0014ED6C File Offset: 0x0014CF6C
	private void ResetCarModelVisual(MountData mountData, ColorData colorda)
	{
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, colorda);
	}

	// Token: 0x060043A3 RID: 17315 RVA: 0x0014ED7C File Offset: 0x0014CF7C
	public void Reset(string mountId, string messageid = "#{101425}")
	{
		this.ResetFakeCarObjRoot();
		MountData mountDataById = DataManager.GetMountDataById(mountId);
		this.ResetCarModelVisual(mountDataById, DataManager.GetColorDataById(mountDataById.DefaultColorId));
		this.TextLabel.text = StrDictionary.GetDictionaryString(messageid, new object[]
		{
			mountDataById.MCarName
		});
	}

	// Token: 0x060043A4 RID: 17316 RVA: 0x0014EDC8 File Offset: 0x0014CFC8
	public void OnClickCloseBtn()
	{
		if ((!SingletonUnity<PlayerCarRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<PlayerCarRootLogic>.Instance.gameObject)) && SingletonUnity<FakeCarObjRootLogic>.Exists && SingletonUnity<FakeCarObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.DisableFakeObjRoot();
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GetCarUIRoot);
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_CLICK_OK)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x04003042 RID: 12354
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003043 RID: 12355
	public UITexture CarModelPic;

	// Token: 0x04003044 RID: 12356
	public UILabel TextLabel;

	// Token: 0x04003045 RID: 12357
	private Color ambientLight;

	// Token: 0x04003046 RID: 12358
	public UIWidget YesBtn;
}
