using System;

// Token: 0x02000A3F RID: 2623
public class RateRootLogic : SingletonUnity<RateRootLogic>
{
	// Token: 0x06004C8A RID: 19594 RVA: 0x0019F120 File Offset: 0x0019D320
	public void OnEnable()
	{
		this.InfoLabel.text = StrDictionary.GetDictionaryString("#{100160}", new object[0]);
		LocalDataSaveManager.SetRateFlag(0);
	}

	// Token: 0x06004C8B RID: 19595 RVA: 0x0019F144 File Offset: 0x0019D344
	public void OnClickYesBtn()
	{
		this.CloseRate();
		SingletonDontDestoryUnity<GameManager>.Instance.Rating();
	}

	// Token: 0x06004C8C RID: 19596 RVA: 0x0019F158 File Offset: 0x0019D358
	public void CloseRate()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RateRoot);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x04003A34 RID: 14900
	public UILabel InfoLabel;
}
