using System;

// Token: 0x020008CB RID: 2251
public class AccountBindUIRoot : SingletonUnity<AccountBindUIRoot>
{
	// Token: 0x06003CA4 RID: 15524 RVA: 0x0010AE94 File Offset: 0x00109094
	public void Init()
	{
	}

	// Token: 0x06003CA5 RID: 15525 RVA: 0x0010AE98 File Offset: 0x00109098
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06003CA6 RID: 15526 RVA: 0x0010AEA0 File Offset: 0x001090A0
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.AccountBindUIRoot);
	}

	// Token: 0x06003CA7 RID: 15527 RVA: 0x0010AEB4 File Offset: 0x001090B4
	public void OnClickFb()
	{
		if (PlayerData.FaceBookBind == 200L || PlayerData.FaceBookBind < 0L)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SignInFacebook();
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SignOutFacebook();
		}
		this.OnClickClose();
	}

	// Token: 0x06003CA8 RID: 15528 RVA: 0x0010AEF4 File Offset: 0x001090F4
	public void OnClickG()
	{
		if (PlayerData.FaceBookBind == 100L || PlayerData.FaceBookBind < 0L)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SignInGoogle();
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.SignOutGoogle();
		}
		this.OnClickClose();
	}

	// Token: 0x06003CA9 RID: 15529 RVA: 0x0010AF3C File Offset: 0x0010913C
	private void Start()
	{
	}

	// Token: 0x06003CAA RID: 15530 RVA: 0x0010AF40 File Offset: 0x00109140
	private void Update()
	{
	}

	// Token: 0x040027E5 RID: 10213
	public UILabel FbLabel;

	// Token: 0x040027E6 RID: 10214
	public UILabel Glabel;
}
