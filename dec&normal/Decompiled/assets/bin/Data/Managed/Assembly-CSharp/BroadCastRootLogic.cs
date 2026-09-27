using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008D0 RID: 2256
public class BroadCastRootLogic : SingletonUnity<BroadCastRootLogic>
{
	// Token: 0x06003CC2 RID: 15554 RVA: 0x0010BC94 File Offset: 0x00109E94
	private void OnEnable()
	{
		if (this.EnableLabelList.Count > 0)
		{
			for (int i = 0; i < this.EnableLabelList.Count; i++)
			{
				NGUITools.SetActive(this.EnableLabelList[i].gameObject, false);
			}
		}
		if (this.DisableLabelList.Count > 0)
		{
			for (int j = 0; j < this.DisableLabelList.Count; j++)
			{
				NGUITools.SetActive(this.DisableLabelList[j].gameObject, false);
			}
		}
		this.CirclePic.alpha = 0f;
	}

	// Token: 0x06003CC3 RID: 15555 RVA: 0x0010BD3C File Offset: 0x00109F3C
	public static void AddMessage(string str)
	{
		if (SingletonUnity<BroadCastRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<BroadCastRootLogic>.Instance.gameObject))
		{
			SingletonUnity<BroadCastRootLogic>.Instance.AddMSG(str);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BroadCastRoot, delegate
			{
				SingletonUnity<BroadCastRootLogic>.Instance.AddMSG(str);
			}, null);
		}
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x0010BDA8 File Offset: 0x00109FA8
	public void AddMSG(string str)
	{
		BroadCastRootLogic.mMessageList.Add(str);
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x0010BDB8 File Offset: 0x00109FB8
	private UILabel GetCurEnableLabel()
	{
		if (this.DisableLabelList.Count > 0)
		{
			UILabel result = this.DisableLabelList[0];
			this.DisableLabelList.RemoveAt(0);
			this.CirclePic.alpha = 1f;
			return result;
		}
		return null;
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x0010BE04 File Offset: 0x0010A004
	private void RecycleLabel(UILabel curLabel)
	{
		this.EnableLabelList.Remove(curLabel);
		this.DisableLabelList.Add(curLabel);
		UnityVersionUtil.SetActiveRecursive(curLabel.gameObject, false);
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x0010BE38 File Offset: 0x0010A038
	private void Update()
	{
		if (this.EnableLabelList.Count > 0)
		{
			for (int i = this.EnableLabelList.Count - 1; i >= 0; i--)
			{
				this.EnableLabelList[i].transform.localPosition = this.EnableLabelList[i].transform.localPosition + Vector3.left * this.MOVE_SPEED * Time.deltaTime;
				if (this.EnableLabelList[i].transform.localPosition.x < (float)(-(float)this.CirclePic.width / 2 - this.EnableLabelList[i].width))
				{
					this.RecycleLabel(this.EnableLabelList[i]);
				}
			}
		}
		if (BroadCastRootLogic.mMessageList.Count > 0)
		{
			UILabel curEnableLabel = this.GetCurEnableLabel();
			if (curEnableLabel != null)
			{
				curEnableLabel.text = BroadCastRootLogic.mMessageList[0];
				BroadCastRootLogic.mMessageList.RemoveAt(0);
				UnityVersionUtil.SetActiveRecursive(curEnableLabel.gameObject, true);
				if (this.EnableLabelList.Count > 0)
				{
					curEnableLabel.transform.localPosition = Vector3.right * Mathf.Max(this.EnableLabelList[this.EnableLabelList.Count - 1].transform.localPosition.x + (float)this.EnableLabelList[this.EnableLabelList.Count - 1].width + 30f, (float)(this.CirclePic.width / 2));
				}
				else
				{
					curEnableLabel.transform.localPosition = Vector3.right * (float)this.CirclePic.width / 2f;
				}
				this.EnableLabelList.Add(curEnableLabel);
			}
		}
		if (this.EnableLabelList.Count <= 0)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BroadCastRoot);
		}
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x0010C044 File Offset: 0x0010A244
	private void OnDisable()
	{
		if (this.EnableLabelList != null && this.EnableLabelList.Count > 0)
		{
			for (int i = 0; i < this.EnableLabelList.Count; i++)
			{
				if (this.EnableLabelList[i] != null && this.EnableLabelList[i].transform.localPosition.x > 0f)
				{
					this.AddMSG(this.EnableLabelList[i].text);
				}
			}
		}
	}

	// Token: 0x0400281C RID: 10268
	private static List<string> mMessageList = new List<string>();

	// Token: 0x0400281D RID: 10269
	public List<UILabel> DisableLabelList;

	// Token: 0x0400281E RID: 10270
	private List<UILabel> EnableLabelList = new List<UILabel>();

	// Token: 0x0400281F RID: 10271
	public UISprite CirclePic;

	// Token: 0x04002820 RID: 10272
	private int curLabelIndex;

	// Token: 0x04002821 RID: 10273
	private float MOVE_SPEED = 50f;
}
