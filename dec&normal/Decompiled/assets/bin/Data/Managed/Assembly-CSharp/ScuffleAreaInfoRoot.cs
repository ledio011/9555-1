using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000986 RID: 2438
public class ScuffleAreaInfoRoot : SingletonUnity<ScuffleAreaInfoRoot>
{
	// Token: 0x060044F5 RID: 17653 RVA: 0x001591A0 File Offset: 0x001573A0
	public void Reset(int floorid, ScuffleData data)
	{
		this.curData = data;
		this.floorId = floorid;
		if (floorid == 0)
		{
			this.TypeLabel.text = StrDictionary.GetDictionaryString("#{102019}", new object[0]);
			this.BtnPic.spriteName = "CZ_tuBiao_Up";
			this.BtnPic.color = new Color(1f, 0.85882354f, 0f, 1f);
			this.TwPosition.enabled = true;
		}
		else
		{
			this.TypeLabel.text = StrDictionary.GetDictionaryString("#{102020}", new object[0]);
			this.BtnPic.spriteName = "CZ_tuBiao_Down";
			this.BtnPic.color = Color.white;
			this.TwPosition.enabled = false;
		}
	}

	// Token: 0x060044F6 RID: 17654 RVA: 0x00159268 File Offset: 0x00157468
	public void OnClickBtn()
	{
		if (this.floorId == 0)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
			enter_scuffle_batttle.request request = new enter_scuffle_batttle.request();
			request.ID = this.curData.ID;
			request.floor = 1L;
			NetLogic.GetInstance().Send<Protocol.enter_scuffle_batttle>(request, null);
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
			enter_scuffle_batttle.request request2 = new enter_scuffle_batttle.request();
			request2.ID = this.curData.ID;
			request2.floor = 0L;
			NetLogic.GetInstance().Send<Protocol.enter_scuffle_batttle>(request2, null);
		}
	}

	// Token: 0x040031C0 RID: 12736
	public UILabel TypeLabel;

	// Token: 0x040031C1 RID: 12737
	public UISprite BtnPic;

	// Token: 0x040031C2 RID: 12738
	private int floorId;

	// Token: 0x040031C3 RID: 12739
	private ScuffleData curData;

	// Token: 0x040031C4 RID: 12740
	public TweenPosition TwPosition;
}
