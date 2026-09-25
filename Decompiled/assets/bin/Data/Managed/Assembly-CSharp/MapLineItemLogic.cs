using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000953 RID: 2387
public class MapLineItemLogic : MonoBehaviour
{
	// Token: 0x060042C6 RID: 17094 RVA: 0x00147094 File Offset: 0x00145294
	public string GetStrState(int state)
	{
		if (state == 0)
		{
			return StrDictionary.GetDictionaryString("#{101810}", new object[0]);
		}
		if (state == 1)
		{
			return StrDictionary.GetDictionaryString("#{101811}", new object[0]);
		}
		return StrDictionary.GetDictionaryString("#{101804}", new object[0]);
	}

	// Token: 0x060042C7 RID: 17095 RVA: 0x001470E0 File Offset: 0x001452E0
	public void UpdateLineState(int index1, int state1, int index2, int state2, int realIndex)
	{
		this.Line1Label.text = string.Format("{0}{1}", StrDictionary.GetDictionaryString("#{101802}", new object[0]), index1 + 1);
		if (state1 > -1)
		{
			this.Line1StateLable.text = this.GetStrState(state1);
		}
		else
		{
			this.Line1StateLable.text = string.Empty;
		}
		this.LindeIndex1 = index1;
		this.LindeIndex2 = index2;
		if (index2 > -1)
		{
			if (state2 > -1)
			{
				this.Line2StateLable.text = this.GetStrState(state2);
			}
			else
			{
				this.Line2StateLable.text = string.Empty;
			}
			this.Line2Label.text = string.Format("{0}{1}", StrDictionary.GetDictionaryString("#{101802}", new object[0]), index2 + 1);
		}
		this.mRealIndex = realIndex;
		int num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CurLineIndex - 1;
		if (index1 != num && state1 < 2)
		{
			this.Btn1Sp.alpha = 1f;
		}
		else
		{
			this.Btn1Sp.alpha = 0f;
		}
		if (index2 != num && state2 < 2)
		{
			this.Btn2Sp.alpha = 1f;
		}
		else
		{
			this.Btn2Sp.alpha = 0f;
		}
		NGUITools.SetActive(this.RightObj, index2 > -1);
	}

	// Token: 0x060042C8 RID: 17096 RVA: 0x0014724C File Offset: 0x0014544C
	public void OnClickChangeLineIndex1()
	{
		change_scene_line.request request = new change_scene_line.request();
		request.line_index = (long)(this.LindeIndex1 + 1);
		NetLogic.GetInstance().Send<Protocol.change_scene_line>(request, null);
	}

	// Token: 0x060042C9 RID: 17097 RVA: 0x0014727C File Offset: 0x0014547C
	public void OnClikcChangeLineIndex2()
	{
		change_scene_line.request request = new change_scene_line.request();
		request.line_index = (long)(this.LindeIndex2 + 1);
		NetLogic.GetInstance().Send<Protocol.change_scene_line>(request, null);
	}

	// Token: 0x04002F19 RID: 12057
	public UILabel Line1Label;

	// Token: 0x04002F1A RID: 12058
	public UILabel Line2Label;

	// Token: 0x04002F1B RID: 12059
	public UILabel Line1StateLable;

	// Token: 0x04002F1C RID: 12060
	public UILabel Line2StateLable;

	// Token: 0x04002F1D RID: 12061
	public UISprite Btn1Sp;

	// Token: 0x04002F1E RID: 12062
	public UISprite Btn2Sp;

	// Token: 0x04002F1F RID: 12063
	public GameObject RightObj;

	// Token: 0x04002F20 RID: 12064
	private int mRealIndex = -1;

	// Token: 0x04002F21 RID: 12065
	private int LindeIndex1 = -1;

	// Token: 0x04002F22 RID: 12066
	private int LindeIndex2 = -1;
}
