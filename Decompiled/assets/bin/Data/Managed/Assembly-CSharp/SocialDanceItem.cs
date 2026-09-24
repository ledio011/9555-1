using System;
using UnityEngine;

// Token: 0x02000A49 RID: 2633
public class SocialDanceItem : MonoBehaviour
{
	// Token: 0x06004CBC RID: 19644 RVA: 0x001A0ACC File Offset: 0x0019ECCC
	private void OnEnable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Reset));
	}

	// Token: 0x06004CBD RID: 19645 RVA: 0x001A0AFC File Offset: 0x0019ECFC
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Reset));
	}

	// Token: 0x06004CBE RID: 19646 RVA: 0x001A0B2C File Offset: 0x0019ED2C
	public void Init(SocialDanceData data)
	{
		this.mData = data;
		this.Reset();
	}

	// Token: 0x06004CBF RID: 19647 RVA: 0x001A0B3C File Offset: 0x0019ED3C
	private void Reset()
	{
		this.iconSprite.spriteName = this.mData.ICON;
		this.NameLabel.text = StrDictionary.GetDictionaryString(this.mData.Name, new object[0]);
		this.UnlockState = false;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		if (level < this.mData.Level)
		{
			this.UnlockState = true;
			this.cdSprite.fillAmount = 1f;
			this.NameLabel.text = string.Format("Lv.{0}", this.mData.Level);
			return;
		}
		this.UpdateCd();
	}

	// Token: 0x06004CC0 RID: 19648 RVA: 0x001A0BEC File Offset: 0x0019EDEC
	private void UpdateCd()
	{
		if (this.UnlockState)
		{
			return;
		}
		if (this.cdSprite.fillAmount != SocialDanceUIRoot.progress)
		{
			this.cdSprite.fillAmount = SocialDanceUIRoot.progress;
		}
	}

	// Token: 0x06004CC1 RID: 19649 RVA: 0x001A0C20 File Offset: 0x0019EE20
	private void Update()
	{
		this.UpdateCd();
	}

	// Token: 0x06004CC2 RID: 19650 RVA: 0x001A0C28 File Offset: 0x0019EE28
	public void OnClickSocial()
	{
		if (this.UnlockState)
		{
			NoticeLogic.AddNotifyData2Client(false, "#{100154}", false, new object[]
			{
				this.mData.Level
			});
			return;
		}
		if (SocialDanceUIRoot.progress <= 0f)
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SocialDanceRoot);
			if (mainPlayer != null)
			{
				if (mainPlayer.PlayeSocialDance(this.mData.ID))
				{
					SocialDanceUIRoot.CDTime = 5f + Time.realtimeSinceStartup;
				}
				else
				{
					NoticeLogic.AddNotifyData("#{200061}", true, false);
				}
			}
		}
	}

	// Token: 0x04003A5D RID: 14941
	private SocialDanceData mData;

	// Token: 0x04003A5E RID: 14942
	public UISprite iconSprite;

	// Token: 0x04003A5F RID: 14943
	public UISprite cdSprite;

	// Token: 0x04003A60 RID: 14944
	public UILabel NameLabel;

	// Token: 0x04003A61 RID: 14945
	private bool UnlockState;
}
