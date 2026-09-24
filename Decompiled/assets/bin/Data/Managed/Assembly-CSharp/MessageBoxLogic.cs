using System;
using UnityEngine;

// Token: 0x020009FD RID: 2557
public class MessageBoxLogic : SingletonUnity<MessageBoxLogic>
{
	// Token: 0x06004938 RID: 18744 RVA: 0x0017A120 File Offset: 0x00178320
	public void Clear()
	{
		this.mDealyTime = -1f;
		this.mDealyTime = -1f;
		this.onYesClick = null;
		this.onCancelClick = null;
		this.onWaitTimeOut = null;
	}

	// Token: 0x06004939 RID: 18745 RVA: 0x0017A150 File Offset: 0x00178350
	public void MessageBoxOkOnClick()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MessageBoxUI);
		if (this.onYesClick != null)
		{
			this.onYesClick();
		}
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
	}

	// Token: 0x0600493A RID: 18746 RVA: 0x0017A1AC File Offset: 0x001783AC
	public void MessageBoxCancelOnClick()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MessageBoxUI);
		if (this.onCancelClick != null)
		{
			this.onCancelClick();
		}
	}

	// Token: 0x0600493B RID: 18747 RVA: 0x0017A1D4 File Offset: 0x001783D4
	public void ShowMessageBox(string title, string text, MessageBoxLogic.MESSAGEBOXTYPE messageType, string yesStr = null, string noStr = null)
	{
		this.mTitleLabel.text = StrDictionary.GetDictionaryString(title, new object[0]);
		this.mTextLabel.text = StrDictionary.GetDictionaryString(text, new object[0]);
		if (!string.IsNullOrEmpty(yesStr))
		{
			this.mYesLabel.text = StrDictionary.GetDictionaryString(yesStr, new object[0]);
		}
		else
		{
			this.mYesLabel.text = StrDictionary.GetDictionaryString("#{100242}", new object[0]);
		}
		if (!string.IsNullOrEmpty(noStr))
		{
			this.mNoLabel.text = StrDictionary.GetDictionaryString(noStr, new object[0]);
		}
		else
		{
			this.mNoLabel.text = StrDictionary.GetDictionaryString("#{100243}", new object[0]);
		}
		this.ShowBox();
		switch (messageType)
		{
		case MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OKCANCEL:
			this.mMessageBoxOkButtonObj.transform.localPosition = this.rightPos;
			this.mMessageBoxCancelButtonObj.transform.localPosition = this.leftPos;
			NGUITools.SetActive(this.mMessageBoxOkButtonObj.gameObject, true);
			NGUITools.SetActive(this.mMessageBoxCancelButtonObj, true);
			NGUITools.SetActive(this.mTimeLabel.gameObject, false);
			NGUITools.SetActive(this.mCloseBtnObj, false);
			break;
		case MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OK:
			this.mMessageBoxOkButtonObj.transform.localPosition = this.midPos;
			NGUITools.SetActive(this.mMessageBoxOkButtonObj.gameObject, true);
			NGUITools.SetActive(this.mMessageBoxCancelButtonObj, false);
			NGUITools.SetActive(this.mTimeLabel.gameObject, false);
			NGUITools.SetActive(this.mCloseBtnObj, false);
			break;
		case MessageBoxLogic.MESSAGEBOXTYPE.TYPE_WAIT:
			NGUITools.SetActive(this.mMessageBoxOkButtonObj.gameObject, false);
			NGUITools.SetActive(this.mMessageBoxCancelButtonObj, false);
			NGUITools.SetActive(this.mTimeLabel.gameObject, true);
			NGUITools.SetActive(this.mCloseBtnObj, false);
			break;
		case MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OKCANCEL_WAIT:
			this.mMessageBoxOkButtonObj.transform.localPosition = this.rightPos;
			this.mMessageBoxCancelButtonObj.transform.localPosition = this.leftPos;
			NGUITools.SetActive(this.mMessageBoxOkButtonObj.gameObject, true);
			NGUITools.SetActive(this.mMessageBoxCancelButtonObj, true);
			NGUITools.SetActive(this.mTimeLabel.gameObject, true);
			NGUITools.SetActive(this.mCloseBtnObj, false);
			break;
		case MessageBoxLogic.MESSAGEBOXTYPE.TYPE_CANCEL_WAIT:
			this.mMessageBoxCancelButtonObj.transform.localPosition = this.midPos;
			NGUITools.SetActive(this.mMessageBoxOkButtonObj.gameObject, false);
			NGUITools.SetActive(this.mMessageBoxCancelButtonObj, true);
			NGUITools.SetActive(this.mTimeLabel.gameObject, true);
			NGUITools.SetActive(this.mCloseBtnObj, false);
			break;
		case MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OK_ONE:
			this.mMessageBoxOkButtonObj.transform.localPosition = this.midPos;
			NGUITools.SetActive(this.mMessageBoxOkButtonObj.gameObject, true);
			NGUITools.SetActive(this.mMessageBoxCancelButtonObj, false);
			NGUITools.SetActive(this.mTimeLabel.gameObject, false);
			NGUITools.SetActive(this.mCloseBtnObj, false);
			break;
		}
	}

	// Token: 0x0600493C RID: 18748 RVA: 0x0017A4D8 File Offset: 0x001786D8
	public void HideBox()
	{
		UnityVersionUtil.SetActiveRecursive(this.mDetailRootObj, false);
	}

	// Token: 0x0600493D RID: 18749 RVA: 0x0017A4E8 File Offset: 0x001786E8
	public void ShowBox()
	{
		UnityVersionUtil.SetActiveRecursive(this.mDetailRootObj, true);
	}

	// Token: 0x0600493E RID: 18750 RVA: 0x0017A4F8 File Offset: 0x001786F8
	public static void OpenOKCancelBox(string text, string title, MessageBoxLogic.OnYesClick delOnYesClick = null, MessageBoxLogic.OnCancelClick delOnCancelClick = null, string yesStr = null, string noStr = null)
	{
		MessageBoxLogic.OKCancelInfo param = new MessageBoxLogic.OKCancelInfo(text, title, delOnYesClick, delOnCancelClick, yesStr, noStr);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MessageBoxUI, new UIManager.OnOpenUIDelegate(MessageBoxLogic.OnOpenOKCancelBox), param);
	}

	// Token: 0x0600493F RID: 18751 RVA: 0x0017A55C File Offset: 0x0017875C
	private static void OnOpenOKCancelBox(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		MessageBoxLogic.OKCancelInfo okcancelInfo = obj as MessageBoxLogic.OKCancelInfo;
		if (SingletonUnity<MessageBoxLogic>.Exists)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
			SingletonUnity<MessageBoxLogic>.Instance.onYesClick = okcancelInfo.mdelOnYesClick;
			SingletonUnity<MessageBoxLogic>.Instance.onCancelClick = okcancelInfo.mdelOnCancelClick;
			SingletonUnity<MessageBoxLogic>.Instance.ShowMessageBox(okcancelInfo.mTitle, okcancelInfo.mText, MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OKCANCEL, okcancelInfo.mYesStr, okcancelInfo.mNoStr);
		}
	}

	// Token: 0x06004940 RID: 18752 RVA: 0x0017A5D0 File Offset: 0x001787D0
	public static void OpenOKBox(string text, string title, MessageBoxLogic.OnYesClick delOnYesClick = null)
	{
		MessageBoxLogic.OKCancelInfo param = new MessageBoxLogic.OKCancelInfo(text, title, delOnYesClick, null, null, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MessageBoxUI, new UIManager.OnOpenUIDelegate(MessageBoxLogic.OnOpenOKBox), param);
	}

	// Token: 0x06004941 RID: 18753 RVA: 0x0017A634 File Offset: 0x00178834
	private static void OnOpenOKBox(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		MessageBoxLogic.OKCancelInfo okcancelInfo = obj as MessageBoxLogic.OKCancelInfo;
		if (SingletonUnity<MessageBoxLogic>.Exists)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
			SingletonUnity<MessageBoxLogic>.Instance.onYesClick = okcancelInfo.mdelOnYesClick;
			SingletonUnity<MessageBoxLogic>.Instance.ShowMessageBox(okcancelInfo.mTitle, okcancelInfo.mText, MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OK, null, null);
		}
	}

	// Token: 0x06004942 RID: 18754 RVA: 0x0017A68C File Offset: 0x0017888C
	public static void OpenOkBox_One(string text, string title, MessageBoxLogic.OnYesClick delOnYesClick = null)
	{
		MessageBoxLogic.OKCancelInfo param = new MessageBoxLogic.OKCancelInfo(text, title, delOnYesClick, null, null, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MessageBoxUI, new UIManager.OnOpenUIDelegate(MessageBoxLogic.OnOpenOKBox_One), param);
	}

	// Token: 0x06004943 RID: 18755 RVA: 0x0017A6F0 File Offset: 0x001788F0
	private static void OnOpenOKBox_One(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		MessageBoxLogic.OKCancelInfo okcancelInfo = obj as MessageBoxLogic.OKCancelInfo;
		if (SingletonUnity<MessageBoxLogic>.Exists)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
			SingletonUnity<MessageBoxLogic>.Instance.onYesClick = okcancelInfo.mdelOnYesClick;
			SingletonUnity<MessageBoxLogic>.Instance.ShowMessageBox(okcancelInfo.mTitle, okcancelInfo.mText, MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OK_ONE, null, null);
		}
	}

	// Token: 0x06004944 RID: 18756 RVA: 0x0017A748 File Offset: 0x00178948
	public static void OpenWaitBox(string text, string tile, float duration = 0f, float delay = 0f, MessageBoxLogic.OnWaitTimeOut delWaitout = null)
	{
		MessageBoxLogic.WaitBoxInfo param = new MessageBoxLogic.WaitBoxInfo(text, tile, delay, duration, delWaitout);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MessageBoxUI, new UIManager.OnOpenUIDelegate(MessageBoxLogic.OnOpenWaitBox), param);
	}

	// Token: 0x06004945 RID: 18757 RVA: 0x0017A7AC File Offset: 0x001789AC
	private static void OnOpenWaitBox(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		MessageBoxLogic.WaitBoxInfo waitBoxInfo = obj as MessageBoxLogic.WaitBoxInfo;
		if (waitBoxInfo != null)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
			SingletonUnity<MessageBoxLogic>.Instance.onWaitTimeOut = waitBoxInfo.mDelWaitOut;
			SingletonUnity<MessageBoxLogic>.Instance.mWaitTime = waitBoxInfo.mDuration;
			SingletonUnity<MessageBoxLogic>.Instance.mDealyTime = waitBoxInfo.mDelay;
			SingletonUnity<MessageBoxLogic>.Instance.ShowMessageBox(waitBoxInfo.mTitle, waitBoxInfo.mText, MessageBoxLogic.MESSAGEBOXTYPE.TYPE_WAIT, null, null);
			if (waitBoxInfo.mDelay > 0f)
			{
				SingletonUnity<MessageBoxLogic>.Instance.HideBox();
			}
		}
	}

	// Token: 0x06004946 RID: 18758 RVA: 0x0017A83C File Offset: 0x00178A3C
	public static void OpenCancelWaitBox(string text, string tile, float waitTime = 0f, MessageBoxLogic.OnCancelClick delOnCancelClick = null, MessageBoxLogic.OnWaitTimeOut delWaitout = null)
	{
		MessageBoxLogic.OKCancelWaitInfo param = new MessageBoxLogic.OKCancelWaitInfo(text, tile, waitTime, null, delOnCancelClick, delWaitout, null, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MessageBoxUI, new UIManager.OnOpenUIDelegate(MessageBoxLogic.OnOpenCancelWaitBox), param);
	}

	// Token: 0x06004947 RID: 18759 RVA: 0x0017A8A4 File Offset: 0x00178AA4
	private static void OnOpenCancelWaitBox(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		MessageBoxLogic.OKCancelWaitInfo okcancelWaitInfo = obj as MessageBoxLogic.OKCancelWaitInfo;
		if (okcancelWaitInfo != null)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
			SingletonUnity<MessageBoxLogic>.Instance.onWaitTimeOut = okcancelWaitInfo.mdelOnWaitTimeOut;
			SingletonUnity<MessageBoxLogic>.Instance.mWaitTime = okcancelWaitInfo.mWaitTIme;
			SingletonUnity<MessageBoxLogic>.Instance.onYesClick = okcancelWaitInfo.mdelOnYesClick;
			SingletonUnity<MessageBoxLogic>.Instance.onCancelClick = okcancelWaitInfo.mdelOnCancelClick;
			SingletonUnity<MessageBoxLogic>.Instance.ShowMessageBox(okcancelWaitInfo.mTitle, okcancelWaitInfo.mText, MessageBoxLogic.MESSAGEBOXTYPE.TYPE_CANCEL_WAIT, null, null);
		}
	}

	// Token: 0x06004948 RID: 18760 RVA: 0x0017A928 File Offset: 0x00178B28
	public static void OpenOKCancelWaitBox(string text, string title, float waitTime = 0f, MessageBoxLogic.OnYesClick delOnYesClick = null, MessageBoxLogic.OnCancelClick delOnCancelClick = null, MessageBoxLogic.OnWaitTimeOut delWaitout = null, string yesStr = null, string noStr = null)
	{
		MessageBoxLogic.OKCancelWaitInfo param = new MessageBoxLogic.OKCancelWaitInfo(text, title, waitTime, delOnYesClick, delOnCancelClick, delWaitout, yesStr, noStr);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MessageBoxUI, new UIManager.OnOpenUIDelegate(MessageBoxLogic.OnOpenOKCancelWaitBox), param);
	}

	// Token: 0x06004949 RID: 18761 RVA: 0x0017A990 File Offset: 0x00178B90
	private static void OnOpenOKCancelWaitBox(bool isSucess, object obj)
	{
		if (!isSucess)
		{
			return;
		}
		MessageBoxLogic.OKCancelWaitInfo okcancelWaitInfo = obj as MessageBoxLogic.OKCancelWaitInfo;
		if (okcancelWaitInfo != null)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
			SingletonUnity<MessageBoxLogic>.Instance.onWaitTimeOut = okcancelWaitInfo.mdelOnWaitTimeOut;
			SingletonUnity<MessageBoxLogic>.Instance.mWaitTime = okcancelWaitInfo.mWaitTIme;
			SingletonUnity<MessageBoxLogic>.Instance.onYesClick = okcancelWaitInfo.mdelOnYesClick;
			SingletonUnity<MessageBoxLogic>.Instance.onCancelClick = okcancelWaitInfo.mdelOnCancelClick;
			SingletonUnity<MessageBoxLogic>.Instance.ShowMessageBox(okcancelWaitInfo.mTitle, okcancelWaitInfo.mText, MessageBoxLogic.MESSAGEBOXTYPE.TYPE_OKCANCEL_WAIT, okcancelWaitInfo.mYesStr, okcancelWaitInfo.mNoStr);
		}
	}

	// Token: 0x0600494A RID: 18762 RVA: 0x0017AA20 File Offset: 0x00178C20
	public static void CloseBox()
	{
		if (SingletonUnity<MessageBoxLogic>.Exists)
		{
			SingletonUnity<MessageBoxLogic>.Instance.Clear();
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MessageBoxUI);
	}

	// Token: 0x0600494B RID: 18763 RVA: 0x0017AA48 File Offset: 0x00178C48
	private void Update()
	{
		if (this.mDealyTime > 0f)
		{
			this.mDealyTime -= Time.deltaTime;
			if (this.mDealyTime <= 0f)
			{
				this.ShowBox();
			}
			return;
		}
		if (this.mWaitTime > 0f)
		{
			this.mWaitTime -= Time.deltaTime;
			this.mTimeLabel.text = ((int)this.mWaitTime).ToString();
			if (this.mWaitTime <= 0f)
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MessageBoxUI);
				if (this.onWaitTimeOut != null)
				{
					this.onWaitTimeOut();
				}
			}
		}
	}

	// Token: 0x0400366B RID: 13931
	private MessageBoxLogic.OnYesClick onYesClick;

	// Token: 0x0400366C RID: 13932
	private MessageBoxLogic.OnCancelClick onCancelClick;

	// Token: 0x0400366D RID: 13933
	private MessageBoxLogic.OnWaitTimeOut onWaitTimeOut;

	// Token: 0x0400366E RID: 13934
	public UILabel mTitleLabel;

	// Token: 0x0400366F RID: 13935
	public UILabel mTextLabel;

	// Token: 0x04003670 RID: 13936
	public UILabel mTimeLabel;

	// Token: 0x04003671 RID: 13937
	public UILabel mYesLabel;

	// Token: 0x04003672 RID: 13938
	public UILabel mNoLabel;

	// Token: 0x04003673 RID: 13939
	public float mWaitTime = -1f;

	// Token: 0x04003674 RID: 13940
	public float mDealyTime = -1f;

	// Token: 0x04003675 RID: 13941
	public UIWidget mMessageBoxOkButtonObj;

	// Token: 0x04003676 RID: 13942
	public GameObject mMessageBoxCancelButtonObj;

	// Token: 0x04003677 RID: 13943
	public GameObject mDetailRootObj;

	// Token: 0x04003678 RID: 13944
	public GameObject mCloseBtnObj;

	// Token: 0x04003679 RID: 13945
	private Vector3 leftPos = new Vector3(-110f, -115f, 0f);

	// Token: 0x0400367A RID: 13946
	private Vector3 midPos = new Vector3(0f, -115f, 0f);

	// Token: 0x0400367B RID: 13947
	private Vector3 rightPos = new Vector3(110f, -115f, 0f);

	// Token: 0x020009FE RID: 2558
	public enum MESSAGEBOXTYPE
	{
		// Token: 0x0400367D RID: 13949
		TYPE_OKCANCEL,
		// Token: 0x0400367E RID: 13950
		TYPE_OK,
		// Token: 0x0400367F RID: 13951
		TYPE_WAIT,
		// Token: 0x04003680 RID: 13952
		TYPE_OKCANCEL_WAIT,
		// Token: 0x04003681 RID: 13953
		TYPE_CANCEL_WAIT,
		// Token: 0x04003682 RID: 13954
		TYPE_OK_ONE
	}

	// Token: 0x020009FF RID: 2559
	private class OKCancelInfo
	{
		// Token: 0x0600494C RID: 18764 RVA: 0x0017AB00 File Offset: 0x00178D00
		public OKCancelInfo(string text, string title = null, MessageBoxLogic.OnYesClick delOnYesClick = null, MessageBoxLogic.OnCancelClick delOnCancelClick = null, string yesStr = null, string noStr = null)
		{
			this.mText = text;
			this.mTitle = title;
			this.mdelOnYesClick = delOnYesClick;
			this.mdelOnCancelClick = delOnCancelClick;
			this.mYesStr = yesStr;
			this.mNoStr = noStr;
		}

		// Token: 0x04003683 RID: 13955
		public string mText;

		// Token: 0x04003684 RID: 13956
		public string mTitle;

		// Token: 0x04003685 RID: 13957
		public MessageBoxLogic.OnYesClick mdelOnYesClick;

		// Token: 0x04003686 RID: 13958
		public MessageBoxLogic.OnCancelClick mdelOnCancelClick;

		// Token: 0x04003687 RID: 13959
		public string mYesStr;

		// Token: 0x04003688 RID: 13960
		public string mNoStr;
	}

	// Token: 0x02000A00 RID: 2560
	private class WaitBoxInfo
	{
		// Token: 0x0600494D RID: 18765 RVA: 0x0017AB38 File Offset: 0x00178D38
		public WaitBoxInfo(string text, string title, float delay, float duration, MessageBoxLogic.OnWaitTimeOut delWaitOut)
		{
			this.mTitle = title;
			this.mText = text;
			this.mDelay = delay;
			this.mDuration = duration;
			this.mDelWaitOut = delWaitOut;
		}

		// Token: 0x04003689 RID: 13961
		public string mTitle;

		// Token: 0x0400368A RID: 13962
		public string mText;

		// Token: 0x0400368B RID: 13963
		public float mDelay;

		// Token: 0x0400368C RID: 13964
		public float mDuration;

		// Token: 0x0400368D RID: 13965
		public MessageBoxLogic.OnWaitTimeOut mDelWaitOut;
	}

	// Token: 0x02000A01 RID: 2561
	private class OKCancelWaitInfo
	{
		// Token: 0x0600494E RID: 18766 RVA: 0x0017AB68 File Offset: 0x00178D68
		public OKCancelWaitInfo(string text, string title = null, float waitTime = 0f, MessageBoxLogic.OnYesClick delOnYesClick = null, MessageBoxLogic.OnCancelClick delOnCancelClick = null, MessageBoxLogic.OnWaitTimeOut waitTimeOut = null, string yesStr = null, string noStr = null)
		{
			this.mText = text;
			this.mTitle = title;
			this.mWaitTIme = waitTime;
			this.mdelOnYesClick = delOnYesClick;
			this.mdelOnCancelClick = delOnCancelClick;
			this.mdelOnWaitTimeOut = waitTimeOut;
			this.mYesStr = yesStr;
			this.mNoStr = noStr;
		}

		// Token: 0x0400368E RID: 13966
		public string mText;

		// Token: 0x0400368F RID: 13967
		public string mTitle;

		// Token: 0x04003690 RID: 13968
		public float mWaitTIme;

		// Token: 0x04003691 RID: 13969
		public MessageBoxLogic.OnYesClick mdelOnYesClick;

		// Token: 0x04003692 RID: 13970
		public MessageBoxLogic.OnCancelClick mdelOnCancelClick;

		// Token: 0x04003693 RID: 13971
		public MessageBoxLogic.OnWaitTimeOut mdelOnWaitTimeOut;

		// Token: 0x04003694 RID: 13972
		public string mYesStr;

		// Token: 0x04003695 RID: 13973
		public string mNoStr;
	}

	// Token: 0x02000AFE RID: 2814
	// (Invoke) Token: 0x06005081 RID: 20609
	public delegate void OnYesClick();

	// Token: 0x02000AFF RID: 2815
	// (Invoke) Token: 0x06005085 RID: 20613
	public delegate void OnCancelClick();

	// Token: 0x02000B00 RID: 2816
	// (Invoke) Token: 0x06005089 RID: 20617
	public delegate void OnWaitTimeOut();
}
