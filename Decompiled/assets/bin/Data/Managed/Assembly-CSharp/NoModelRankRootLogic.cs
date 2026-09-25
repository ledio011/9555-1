using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000978 RID: 2424
public class NoModelRankRootLogic : MonoBehaviour
{
	// Token: 0x06004477 RID: 17527 RVA: 0x001556AC File Offset: 0x001538AC
	private void Start()
	{
		if (!this.mInitFlag)
		{
			for (int i = 0; i < this.RankItemList.Count; i++)
			{
				this.RankItemList[i].Init(new RankItemLogic.onClickBtn(this.OnClickRankNumBtn), i);
			}
			this.mInitFlag = true;
		}
	}

	// Token: 0x06004478 RID: 17528 RVA: 0x00155708 File Offset: 0x00153908
	public void Reset(List<sort_item> ranklist, RANK_TYPE curtype)
	{
		this.mCurRankList = ranklist;
		if (ranklist.Count % 10 != 0)
		{
			this.MaxPageNum = ranklist.Count / 10 + 1;
		}
		else
		{
			this.MaxPageNum = ranklist.Count / 10;
			if (this.MaxPageNum == 0)
			{
				this.MaxPageNum = 1;
			}
		}
		this.CurRankType = curtype;
		this.curPagenum = 1;
		this.SelfInfoShow();
		this.mCurRankNum = -1;
		this.PageInfoLabel.text = this.curPagenum + "/" + this.MaxPageNum;
		this.ResetItemList((this.curPagenum - 1) * 10);
		this.OnClickRankNum(0, 0);
		this.RankScrollView.ResetPosition();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Rank", string.Format("rank_{0}", (int)curtype), "opentimes");
	}

	// Token: 0x06004479 RID: 17529 RVA: 0x001557F0 File Offset: 0x001539F0
	public void ResetItemList(int startRanknum)
	{
		for (int i = 0; i < this.RankItemList.Count; i++)
		{
			if (startRanknum + i < this.mCurRankList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.RankItemList[i].gameObject, true);
				this.RankItemList[i].Reset(startRanknum + i, this.mCurRankList[startRanknum + i], this.CurRankType);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.RankItemList[i].gameObject, false);
			}
		}
	}

	// Token: 0x0600447A RID: 17530 RVA: 0x00155888 File Offset: 0x00153A88
	public void OnClickLeftpageBtn()
	{
		if (this.curPagenum > 1)
		{
			this.curPagenum--;
			this.PageInfoLabel.text = this.curPagenum + "/" + this.MaxPageNum;
			this.ResetItemList((this.curPagenum - 1) * 10);
			this.RankScrollView.ResetPosition();
			this.RankItemList[0].OnClickBtn();
		}
	}

	// Token: 0x0600447B RID: 17531 RVA: 0x00155908 File Offset: 0x00153B08
	public void OnClickRightpageBtn()
	{
		if (this.curPagenum < this.MaxPageNum)
		{
			this.curPagenum++;
			this.PageInfoLabel.text = this.curPagenum + "/" + this.MaxPageNum;
			this.ResetItemList((this.curPagenum - 1) * 10);
			this.RankScrollView.ResetPosition();
			this.RankItemList[0].OnClickBtn();
		}
	}

	// Token: 0x0600447C RID: 17532 RVA: 0x0015598C File Offset: 0x00153B8C
	public void OnClickRankNumBtn(int clickNum, int objIndex)
	{
		this.OnClickRankNum(clickNum, objIndex);
	}

	// Token: 0x0600447D RID: 17533 RVA: 0x00155998 File Offset: 0x00153B98
	public void OnClickRankNum(int rankNum, int itemIndex)
	{
		if (this.mCurRankNum == rankNum)
		{
			return;
		}
		if (rankNum >= this.mCurRankList.Count)
		{
			return;
		}
		this.mCurRankNum = rankNum;
		this.mCurRankObjIndex = itemIndex;
		this.selectitemPic.transform.parent = this.RankItemList[this.mCurRankObjIndex].transform;
		this.selectitemPic.transform.localPosition = Vector3.zero;
		if (!UnityVersionUtil.IsActive(this.selectitemPic.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.selectitemPic.gameObject, true);
		}
	}

	// Token: 0x0600447E RID: 17534 RVA: 0x00155A34 File Offset: 0x00153C34
	public void SelfInfoShow()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		RANK_TYPE curRankType = this.CurRankType;
		if (curRankType == RANK_TYPE.GUILD)
		{
			this.lvLabel.text = StrDictionary.GetDictionaryString("#{101312}", new object[0]);
			this.powerlabel.text = StrDictionary.GetDictionaryString("#{101313}", new object[0]);
			this.mannamelabel.text = StrDictionary.GetDictionaryString("#{100720}", new object[0]);
			this.guildnamelabel.text = StrDictionary.GetDictionaryString("#{100734}", new object[0]);
			this.menberLabel.text = StrDictionary.GetDictionaryString("#{100703}", new object[0]);
			this.CityLabel.text = StrDictionary.GetDictionaryString("#{106028}", new object[0]);
			if (playerData.IsHaveGuild())
			{
				this.SelfRankLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString("#{101322}", new object[0]));
				this.SelfNameLabel.enabled = false;
				this.SelfValLabel.enabled = false;
				for (int i = 0; i < this.mCurRankList.Count; i++)
				{
					if (this.mCurRankList[i].id == playerData.PlayerGuild.ServerId)
					{
						this.SelfRankLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101309}", new object[0]), i + 1);
						string[] array = this.mCurRankList[i].name.Split(new char[]
						{
							'#'
						});
						this.SelfNameLabel.text = array[0];
						this.SelfValLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101313}", new object[0]), this.mCurRankList[i].score >> 32);
						this.SelfNameLabel.enabled = true;
						this.SelfValLabel.enabled = true;
						break;
					}
				}
			}
			else
			{
				this.SelfRankLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString("#{100240}", new object[0]));
				this.SelfNameLabel.enabled = false;
				this.SelfValLabel.enabled = false;
			}
		}
	}

	// Token: 0x04003116 RID: 12566
	public RANK_TYPE CurRankType;

	// Token: 0x04003117 RID: 12567
	private List<sort_item> mCurRankList;

	// Token: 0x04003118 RID: 12568
	public UILabel SelfRankLabel;

	// Token: 0x04003119 RID: 12569
	public UILabel SelfNameLabel;

	// Token: 0x0400311A RID: 12570
	public UILabel SelfValLabel;

	// Token: 0x0400311B RID: 12571
	private bool mInitFlag;

	// Token: 0x0400311C RID: 12572
	public List<RankItemLogic> RankItemList;

	// Token: 0x0400311D RID: 12573
	public UISprite selectitemPic;

	// Token: 0x0400311E RID: 12574
	private int mCurRankNum;

	// Token: 0x0400311F RID: 12575
	private int mCurRankObjIndex;

	// Token: 0x04003120 RID: 12576
	public UILabel PageInfoLabel;

	// Token: 0x04003121 RID: 12577
	private int curPagenum;

	// Token: 0x04003122 RID: 12578
	private int MaxPageNum = 10;

	// Token: 0x04003123 RID: 12579
	public UIScrollView RankScrollView;

	// Token: 0x04003124 RID: 12580
	public UILabel lvLabel;

	// Token: 0x04003125 RID: 12581
	public UILabel powerlabel;

	// Token: 0x04003126 RID: 12582
	public UILabel mannamelabel;

	// Token: 0x04003127 RID: 12583
	public UILabel guildnamelabel;

	// Token: 0x04003128 RID: 12584
	public UILabel menberLabel;

	// Token: 0x04003129 RID: 12585
	public UILabel CityLabel;
}
