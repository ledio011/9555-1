using System;
using UnityEngine;

// Token: 0x020008F8 RID: 2296
public class TowerFloorInfoLogic : MonoBehaviour
{
	// Token: 0x17000F86 RID: 3974
	// (get) Token: 0x06003E7D RID: 15997 RVA: 0x0011E8AC File Offset: 0x0011CAAC
	public int CurFloorIndex
	{
		get
		{
			return this.mCurFloorIndex;
		}
	}

	// Token: 0x06003E7E RID: 15998 RVA: 0x0011E8B4 File Offset: 0x0011CAB4
	public void Init(DelegateDefine.TwoIntParamDelegate func, int selfIndex)
	{
		this.RegisterClickEvent(func);
		this.mSelfIndex = selfIndex;
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x0011E8C4 File Offset: 0x0011CAC4
	public void RegisterClickEvent(DelegateDefine.TwoIntParamDelegate func)
	{
		this.onClickItem = (DelegateDefine.TwoIntParamDelegate)Delegate.Combine(this.onClickItem, func);
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x0011E8E0 File Offset: 0x0011CAE0
	public void DeRegisterClickEvent(DelegateDefine.TwoIntParamDelegate func)
	{
		if (this.onClickItem != null)
		{
			this.onClickItem = (DelegateDefine.TwoIntParamDelegate)Delegate.Remove(this.onClickItem, func);
		}
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x0011E910 File Offset: 0x0011CB10
	public void Refresh(int selectFloor, int completetFloor)
	{
		if (this.mCurFloorIndex == selectFloor)
		{
			this.BtnPic.spriteName = "CZ_huaDongBG_1";
		}
		else if (this.mCurFloorIndex > completetFloor)
		{
			this.BtnPic.spriteName = "CZ_huaDongBG_2";
		}
		else
		{
			this.BtnPic.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x06003E82 RID: 16002 RVA: 0x0011E970 File Offset: 0x0011CB70
	public void Reset(int floorIndex, bool isSelect, int completetFloor, bool isShow, bool isEnable)
	{
		this.mCurFloorIndex = floorIndex;
		this.mCurCompleteFloor = completetFloor;
		this.FloorLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{101526}", new object[0]), this.mCurFloorIndex + 1);
		this.isShowReward = isShow;
		this.isShowRewardEnable = isEnable;
		if ((this.mCurFloorIndex + 1) % 5 == 0 && this.mCurFloorIndex > 0)
		{
			this.IconSprite.spriteName = "CZ_paTa_PVE";
		}
		else
		{
			this.IconSprite.spriteName = "CZ_jingJiChang_PVP";
			this.isShowReward = false;
			this.isShowRewardEnable = false;
		}
		NGUITools.SetActive(this.CompletePic.gameObject, false);
		if (isSelect)
		{
			this.BtnPic.spriteName = "CZ_huaDongBG_1";
		}
		else if (floorIndex > completetFloor)
		{
			this.BtnPic.spriteName = "CZ_huaDongBG_2";
		}
		else
		{
			this.BtnPic.spriteName = "CZ_huaDongBG";
			if (this.isShowReward)
			{
				NGUITools.SetActive(this.CompletePic.gameObject, false);
			}
			else
			{
				NGUITools.SetActive(this.CompletePic.gameObject, true);
			}
		}
		if (this.isShowReward)
		{
			this.ShowReward();
			if (this.isShowRewardEnable)
			{
				this.EnableReward();
			}
			else
			{
				this.DisableReward();
			}
		}
		else
		{
			this.HideReward();
		}
	}

	// Token: 0x06003E83 RID: 16003 RVA: 0x0011EAE0 File Offset: 0x0011CCE0
	public void OnClickBtn()
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(this.mCurFloorIndex, this.mSelfIndex);
		}
	}

	// Token: 0x06003E84 RID: 16004 RVA: 0x0011EB10 File Offset: 0x0011CD10
	public void EnableReward()
	{
		for (int i = 0; i < this.RewardTweenerList.Length; i++)
		{
			this.RewardTweenerList[i].enabled = true;
		}
	}

	// Token: 0x06003E85 RID: 16005 RVA: 0x0011EB44 File Offset: 0x0011CD44
	public void DisableReward()
	{
		for (int i = 0; i < this.RewardTweenerList.Length; i++)
		{
			this.RewardTweenerList[i].enabled = false;
		}
	}

	// Token: 0x06003E86 RID: 16006 RVA: 0x0011EB78 File Offset: 0x0011CD78
	public void ShowReward()
	{
		NGUITools.SetActive(this.RewardRoot, true);
		for (int i = 0; i < this.RewardTweenerList.Length; i++)
		{
			this.RewardTweenerList[i].ResetToBeginning();
		}
	}

	// Token: 0x06003E87 RID: 16007 RVA: 0x0011EBB8 File Offset: 0x0011CDB8
	public void HideReward()
	{
		NGUITools.SetActive(this.RewardRoot, false);
	}

	// Token: 0x06003E88 RID: 16008 RVA: 0x0011EBC8 File Offset: 0x0011CDC8
	public void OnClickReward()
	{
		if (this.isShowReward)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerWipeOutRootLogic, delegate
			{
				SingletonUnity<TowerWipeOutRootLogic>.Instance.ResetGetSpecialRewardPage(this.CurFloorIndex, this.isShowRewardEnable);
			}, null);
		}
	}

	// Token: 0x04002A27 RID: 10791
	private int mCurFloorIndex;

	// Token: 0x04002A28 RID: 10792
	private int mSelfIndex;

	// Token: 0x04002A29 RID: 10793
	private int mCurCompleteFloor;

	// Token: 0x04002A2A RID: 10794
	public UISprite BtnPic;

	// Token: 0x04002A2B RID: 10795
	public UILabel FloorLabel;

	// Token: 0x04002A2C RID: 10796
	public UISprite CompletePic;

	// Token: 0x04002A2D RID: 10797
	private bool mIsEnable;

	// Token: 0x04002A2E RID: 10798
	private DelegateDefine.TwoIntParamDelegate onClickItem;

	// Token: 0x04002A2F RID: 10799
	public UISprite IconSprite;

	// Token: 0x04002A30 RID: 10800
	public GameObject RewardRoot;

	// Token: 0x04002A31 RID: 10801
	public UITweener[] RewardTweenerList;

	// Token: 0x04002A32 RID: 10802
	private bool isShowReward;

	// Token: 0x04002A33 RID: 10803
	private bool isShowRewardEnable;
}
