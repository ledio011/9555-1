using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class RecentSpeakerBtnLogic : MonoBehaviour
{
	// Token: 0x06000B7E RID: 2942 RVA: 0x0005408C File Offset: 0x0005228C
	public void Reset(RecentSpeaker speakerInfo)
	{
		this.NameLabel.text = speakerInfo.Name;
		this.BottomSprite.width = this.NameLabel.width + 20;
		this.SpeakerInfo = speakerInfo;
		this.Collider.size = new Vector3((float)this.BottomSprite.width, (float)this.BottomSprite.height, 1f);
		this.SelfTipPic.transform.localPosition = new Vector3((float)(this.NameLabel.width / 2), (float)(this.BottomSprite.height / 2), 0f);
		if (speakerInfo.NewMessageFlag)
		{
			UnityVersionUtil.SetActiveRecursive(this.TipPic.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.SelfTipPic.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TipPic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.SelfTipPic.gameObject, false);
		}
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00054184 File Offset: 0x00052384
	public void OnClickBtn()
	{
		if (SingletonUnity<ChatUIRootLogic>.Exists)
		{
			SingletonUnity<ChatUIRootLogic>.Instance.OnClickRecentSpeaker(this.SpeakerInfo);
		}
	}

	// Token: 0x04000A52 RID: 2642
	public UILabel NameLabel;

	// Token: 0x04000A53 RID: 2643
	public UIWidget BottomSprite;

	// Token: 0x04000A54 RID: 2644
	public RecentSpeaker SpeakerInfo;

	// Token: 0x04000A55 RID: 2645
	public BoxCollider Collider;

	// Token: 0x04000A56 RID: 2646
	public UISprite TipPic;

	// Token: 0x04000A57 RID: 2647
	public UISprite SelfTipPic;
}
