using System;

// Token: 0x02000871 RID: 2161
public class AnimationEditorManager : SceneManager
{
	// Token: 0x06003955 RID: 14677 RVA: 0x000F514C File Offset: 0x000F334C
	public override void Init(string id)
	{
		base.Init(id);
		SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnMainPlayerCreate");
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x000F516C File Offset: 0x000F336C
	public void OnMainPlayerCreate()
	{
		UIManager instance = SingletonUnity<UIManager>.Instance;
		instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		instance.ShowUI(UIInfo.AnimationEditor, delegate
		{
			SingletonUnity<AnimaEditorUIRootLogic>.Instance.Init();
		}, null);
	}

	// Token: 0x06003957 RID: 14679 RVA: 0x000F51B8 File Offset: 0x000F33B8
	public override void Update()
	{
	}
}
