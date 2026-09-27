using System;
using System.Text.RegularExpressions;
using Sproto;
using SprotoType;

// Token: 0x02000981 RID: 2433
public class ReNameRootLogic : SingletonUnity<ReNameRootLogic>
{
	// Token: 0x060044CA RID: 17610 RVA: 0x00157BFC File Offset: 0x00155DFC
	private new void Awake()
	{
		this.NameTipsLabel.text = StrDictionary.GetDictionaryString("#{200056}", new object[]
		{
			this.NameMinLength,
			this.NameMaxLength
		});
		this.curitem.UpdateItem("5024", EQUIP_QUALITY.KUANG_BLUE, 1, 0);
	}

	// Token: 0x060044CB RID: 17611 RVA: 0x00157C54 File Offset: 0x00155E54
	public void OnClickRandomNameBtn()
	{
		this.CharacterGetRandomName();
	}

	// Token: 0x060044CC RID: 17612 RVA: 0x00157C5C File Offset: 0x00155E5C
	public void OnClickCreateBtn()
	{
		if (!string.IsNullOrEmpty(this.NameInput.value))
		{
			this.NameInput.value = this.NameInput.value.Trim();
		}
		if (!string.IsNullOrEmpty(this.NameInput.value))
		{
			if (this.NameInput.value.Length < this.NameMinLength || this.NameInput.value.Length > this.NameMaxLength)
			{
				MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{200060}", new object[]
				{
					this.NameMinLength,
					this.NameMaxLength
				}), "#{100127}", null);
				return;
			}
			if (!this.CheckNameIsRight(this.NameInput.value))
			{
				MessageBoxLogic.OpenOKBox("#{200058}", "#{100127}", null);
				return;
			}
			this.OnClickCloseBtn();
			WaitResponseUIRootLogic.OpenWaitBox(301, 10f, 0f, null);
			re_name.request request = new re_name.request();
			request.name = this.NameInput.value;
			NetLogic.GetInstance().Send<Protocol.re_name>(request, null);
		}
		else
		{
			MessageBoxLogic.OpenOKBox("#{200057}", "#{100127}", null);
		}
	}

	// Token: 0x060044CD RID: 17613 RVA: 0x00157D98 File Offset: 0x00155F98
	private bool CheckNameIsRight(string namestr)
	{
		string text = "^[a-zA-Z0-9]{1}([a-zA-Z0-9]|[ _]){4,19}$";
		return Regex.IsMatch(namestr, text);
	}

	// Token: 0x060044CE RID: 17614 RVA: 0x00157DBC File Offset: 0x00155FBC
	private void CharacterGetRandomName()
	{
		long type = 0L;
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession == PROFESSION_TYPE.NQS)
		{
			type = 1L;
		}
		request_random_name.request request = new request_random_name.request();
		request.type = type;
		NetLogic.GetInstance().Send<Protocol.request_random_name>(request, new RpcRspHandler(this.CharacterGetRandomNameResponse));
	}

	// Token: 0x060044CF RID: 17615 RVA: 0x00157E08 File Offset: 0x00156008
	private void CharacterGetRandomNameResponse(SprotoTypeBase req)
	{
		request_random_name.response response = req as request_random_name.response;
		if (response != null)
		{
			this.NameInput.value = response.name;
		}
	}

	// Token: 0x060044D0 RID: 17616 RVA: 0x00157E34 File Offset: 0x00156034
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RenameRoot);
	}

	// Token: 0x04003180 RID: 12672
	public UIInput NameInput;

	// Token: 0x04003181 RID: 12673
	private int NameMinLength = 5;

	// Token: 0x04003182 RID: 12674
	private int NameMaxLength = 20;

	// Token: 0x04003183 RID: 12675
	public UILabel NameTipsLabel;

	// Token: 0x04003184 RID: 12676
	public RewardItem curitem;
}
