using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009AD RID: 2477
public class MailUIRootLogic : SingletonUnity<MailUIRootLogic>
{
	// Token: 0x06004645 RID: 17989 RVA: 0x00163860 File Offset: 0x00161A60
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06004646 RID: 17990 RVA: 0x00163868 File Offset: 0x00161A68
	private void OnEnable()
	{
		this.curPage = 1;
		this.curMailid = -1L;
		this.InitList();
		this.curMail = null;
	}

	// Token: 0x06004647 RID: 17991 RVA: 0x00163888 File Offset: 0x00161A88
	private void InitList()
	{
		for (int i = 0; i < this.MailItems.Count; i++)
		{
			this.MailItems[i].InitSelct();
			NGUITools.SetActive(this.MailItems[i].gameObject, false);
		}
	}

	// Token: 0x06004648 RID: 17992 RVA: 0x001638DC File Offset: 0x00161ADC
	public void Reset()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		playerData.FriendInfo.SortMailList();
		this.UpdateMailList();
		this.uiScrollView.ResetPosition();
	}

	// Token: 0x06004649 RID: 17993 RVA: 0x00163910 File Offset: 0x00161B10
	private void UpdateSelectItem(long mailid)
	{
		if (mailid == -1L || this.curMailList.Count == 0)
		{
			NGUITools.SetActive(this.SelectMailObj, false);
			NGUITools.SetActive(this.NoSelectMailObj, true);
			for (int i = 0; i < this.MailItems.Count; i++)
			{
				this.MailItems[i].InitSelct();
			}
			this.uiScrollView.ResetPosition();
			return;
		}
		for (int j = 0; j < this.MailItems.Count; j++)
		{
			this.MailItems[j].UpdateSelect(mailid);
		}
		NGUITools.SetActive(this.SelectMailObj, true);
		NGUITools.SetActive(this.NoSelectMailObj, false);
		for (int k = 0; k < this.curMailList.Count; k++)
		{
			if (mailid == this.curMailList[k].key)
			{
				this.curMail = this.curMailList[k];
			}
		}
		this.MailTitleLabel.text = StrDictionary.GetServerDictionaryString(this.curMail.title);
		string serverDictionaryString = StrDictionary.GetServerDictionaryString(this.curMail.text);
		if (!string.IsNullOrEmpty(serverDictionaryString))
		{
			this.MailTextLabel.text = serverDictionaryString.Replace("#r", "\n");
		}
		else
		{
			this.MailTextLabel.text = string.Empty;
		}
		DateTime dateTime = DateTimeTool.LongToDateTimeLocal(this.curMail.time);
		this.TimeLabel.text = string.Format("{0}-{1:D2}-{2:D2}", dateTime.Year, dateTime.Month, dateTime.Day);
		this.OpenMail();
		this.ShowRewardItemScript.ShowRewards(this.curMail.items);
		this.rewardview.ResetPosition();
		if (this.curMail.IsGetItem())
		{
			this.ItemGetLabel.text = StrDictionary.GetDictionaryString("#{100238}", new object[0]);
			this.GetItemSP.spriteName = "CZ_anNiu_2+";
			this.ReceiveFlag.enabled = true;
		}
		else
		{
			this.ItemGetLabel.text = StrDictionary.GetDictionaryString("#{100237}", new object[0]);
			this.GetItemSP.spriteName = "CZ_anNiu_1";
			this.ReceiveFlag.enabled = false;
		}
		NGUITools.SetActive(this.GetItemBtnObj, this.curMail.IsHaveItem());
	}

	// Token: 0x0600464A RID: 17994 RVA: 0x00163B84 File Offset: 0x00161D84
	public void UpdateMailList()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<FriendInfo.Mail> userMailList = playerData.FriendInfo.UserMailList;
		this.maxPage = (userMailList.Count + this.LineCount - 1) / this.LineCount;
		this.curMailList.Clear();
		int num = (this.curPage - 1) * this.LineCount;
		while (num < this.curPage * this.LineCount && num < userMailList.Count)
		{
			this.curMailList.Add(userMailList[num]);
			num++;
		}
		int num2 = Mathf.Min(this.curMailList.Count, this.LineCount) - this.MailItems.Count;
		int count = this.MailItems.Count;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.MailItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:d2}", count + i);
				MailItemLogic component = gameObject.GetComponent<MailItemLogic>();
				if (component != null)
				{
					component.transform.parent = this.MailItems[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.MailItems.Add(component);
				}
			}
		}
		for (int j = 0; j < this.MailItems.Count; j++)
		{
			NGUITools.SetActive(this.MailItems[j].gameObject, j < this.curMailList.Count);
			if (j < this.curMailList.Count)
			{
				this.MailItems[j].UpdateMail(this.curMailList[j], new MailItemLogic.OnClick(this.OnClickMail));
			}
		}
		this.PageLabel.text = string.Format("{0}/{1}", this.curPage, this.maxPage);
		this.uiWrapContent.Reposition();
		this.UpdateSelectItem(this.curMailid);
	}

	// Token: 0x0600464B RID: 17995 RVA: 0x00163DCC File Offset: 0x00161FCC
	public void OnClickDelBtn()
	{
		if (this.curMail.IsHaveItem() && !this.curMail.IsGetItem())
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100245}", new object[0]), StrDictionary.GetDictionaryString("#{100244}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnYesClick), null, null, null);
		}
		else
		{
			this.OnYesClick();
		}
	}

	// Token: 0x0600464C RID: 17996 RVA: 0x00163E38 File Offset: 0x00162038
	public void OnYesClick()
	{
		this.curMailid = -1L;
		mail_operation.request request = new mail_operation.request();
		request.mailId = this.curMail.key;
		request.operation = 1L;
		NetLogic.GetInstance().Send<Protocol.mail_operation>(request, null);
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		friendInfo.DelMail(this.curMail.key);
		this.curMail = null;
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x00163EA0 File Offset: 0x001620A0
	public void OnClickGetBtn()
	{
		if (this.curMail.IsGetItem())
		{
			return;
		}
		mail_operation.request request = new mail_operation.request();
		request.mailId = this.curMail.key;
		request.operation = 2L;
		NetLogic.GetInstance().Send<Protocol.mail_operation>(request, null);
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x00163EEC File Offset: 0x001620EC
	private void OpenMail()
	{
		if (this.curMail.read)
		{
			return;
		}
		mail_operation.request request = new mail_operation.request();
		request.mailId = this.curMail.key;
		request.operation = 0L;
		NetLogic.GetInstance().Send<Protocol.mail_operation>(request, null);
		this.curMail.read = true;
		if (this.curMail.mailstate == 0)
		{
			this.curMail.mailstate = 1;
		}
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		friendInfo.ReadMail(this.curMail.key);
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x00163F80 File Offset: 0x00162180
	public void OnClickMail(long id)
	{
		this.curMailid = id;
		this.UpdateSelectItem(this.curMailid);
	}

	// Token: 0x06004650 RID: 18000 RVA: 0x00163F98 File Offset: 0x00162198
	public void OnClickLeft()
	{
		if (this.curPage > 1)
		{
			this.curPage--;
			this.UpdateMailList();
			this.uiScrollView.ResetPosition();
		}
	}

	// Token: 0x06004651 RID: 18001 RVA: 0x00163FC8 File Offset: 0x001621C8
	public void OnClickRight()
	{
		if (this.curPage < this.maxPage)
		{
			this.curPage++;
			this.UpdateMailList();
			this.uiScrollView.ResetPosition();
		}
	}

	// Token: 0x06004652 RID: 18002 RVA: 0x00164008 File Offset: 0x00162208
	public void OnClikeDeletAll()
	{
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (!friendInfo.IsHaveMail())
		{
			return;
		}
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100246}", new object[0]), StrDictionary.GetDictionaryString("#{100244}", new object[0]), delegate
		{
			mail_operation.request request = new mail_operation.request();
			request.operation = 4L;
			NetLogic.GetInstance().Send<Protocol.mail_operation>(request, null);
			friendInfo.ClearMailData();
		}, null, null, null);
	}

	// Token: 0x06004653 RID: 18003 RVA: 0x00164078 File Offset: 0x00162278
	public void OnClickGetAll()
	{
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (!friendInfo.IsHaveMail())
		{
			return;
		}
		mail_operation.request request = new mail_operation.request();
		request.operation = 3L;
		NetLogic.GetInstance().Send<Protocol.mail_operation>(request, null);
	}

	// Token: 0x04003352 RID: 13138
	public List<MailItemLogic> MailItems = new List<MailItemLogic>();

	// Token: 0x04003353 RID: 13139
	public UIGrid uiWrapContent;

	// Token: 0x04003354 RID: 13140
	public UIScrollView uiScrollView;

	// Token: 0x04003355 RID: 13141
	public ShowRewardItems ShowRewardItemScript;

	// Token: 0x04003356 RID: 13142
	public UILabel MailTitleLabel;

	// Token: 0x04003357 RID: 13143
	public UILabel MailTextLabel;

	// Token: 0x04003358 RID: 13144
	public GameObject NoSelectMailObj;

	// Token: 0x04003359 RID: 13145
	public GameObject SelectMailObj;

	// Token: 0x0400335A RID: 13146
	public UILabel ItemGetLabel;

	// Token: 0x0400335B RID: 13147
	public UILabel PageLabel;

	// Token: 0x0400335C RID: 13148
	public int LineCount = 10;

	// Token: 0x0400335D RID: 13149
	private List<FriendInfo.Mail> curMailList = new List<FriendInfo.Mail>();

	// Token: 0x0400335E RID: 13150
	private int curPage = 1;

	// Token: 0x0400335F RID: 13151
	private int maxPage = 1;

	// Token: 0x04003360 RID: 13152
	private long curMailid = -1L;

	// Token: 0x04003361 RID: 13153
	private FriendInfo.Mail curMail;

	// Token: 0x04003362 RID: 13154
	public GameObject GetItemBtnObj;

	// Token: 0x04003363 RID: 13155
	public UISprite GetItemSP;

	// Token: 0x04003364 RID: 13156
	public UISprite ReceiveFlag;

	// Token: 0x04003365 RID: 13157
	public UILabel TimeLabel;

	// Token: 0x04003366 RID: 13158
	public UIScrollView rewardview;
}
