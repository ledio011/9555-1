using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A0A RID: 2570
public class MysteryShopRootLogic : SingletonUnity<MysteryShopRootLogic>
{
	// Token: 0x060049BF RID: 18879 RVA: 0x0017E08C File Offset: 0x0017C28C
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> leftBtnInfo = new List<MenuTabBtnInfo>();
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(leftBtnInfo, new DelegateDefine.NoParamDelegate(this.OnClickBackBtn), true, StrDictionary.GetDictionaryString("#{100161}", new object[0]));
		}, null);
		UIEventListener uieventListener = this.uidragEvent;
		uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragMoveBtn));
		this.TargetID = string.Empty;
	}

	// Token: 0x060049C0 RID: 18880 RVA: 0x0017E0E8 File Offset: 0x0017C2E8
	public void EnableReset()
	{
		UnityVersionUtil.SetActiveRecursive(this.MainPage.gameObject, true);
		UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, false);
		this.MainPage.EnableReset();
		this.TempPage.EnableReset();
	}

	// Token: 0x060049C1 RID: 18881 RVA: 0x0017E130 File Offset: 0x0017C330
	public void Reset(ret_special_big_pack.request request)
	{
		this.SpePackList.Clear();
		this.SpePackList = new List<special_big_pack>(request.special_big_packs.Values);
		for (int i = this.SpePackList.Count - 1; i >= 0; i--)
		{
			BigPackageData bigPackageDataById = DataManager.GetBigPackageDataById(this.SpePackList[i].ID);
			if (bigPackageDataById != null)
			{
				if (bigPackageDataById.SellType != 1 || this.SpePackList[i].state != 0L)
				{
					this.SpePackList.RemoveAt(i);
				}
			}
			else
			{
				this.SpePackList.RemoveAt(i);
			}
		}
		if (this.SpePackList.Count == 0)
		{
			this.OnClickBackBtn();
			return;
		}
		this.SpePackList.Sort(delegate(special_big_pack x, special_big_pack y)
		{
			BigPackageData bigPackageDataById2 = DataManager.GetBigPackageDataById(x.ID);
			BigPackageData bigPackageDataById3 = DataManager.GetBigPackageDataById(y.ID);
			if (bigPackageDataById2.sortID == bigPackageDataById3.sortID)
			{
				return int.Parse(x.ID) - int.Parse(y.ID);
			}
			return bigPackageDataById2.sortID - bigPackageDataById3.sortID;
		});
		this.ResetPoint();
		this.CurShowIndex = 0;
		if (!string.IsNullOrEmpty(this.TargetID))
		{
			for (int j = 0; j < this.SpePackList.Count; j++)
			{
				if (this.SpePackList[j].ID.Equals(this.TargetID))
				{
					this.CurShowIndex = j;
					break;
				}
			}
			this.TargetID = string.Empty;
		}
		UnityVersionUtil.SetActiveRecursive(this.MainPage.gameObject, true);
		UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, false);
		this.MainPage.RefershInfo(this.SpePackList[this.CurShowIndex]);
		this.SelectPoint();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Mystery", "open");
	}

	// Token: 0x060049C2 RID: 18882 RVA: 0x0017E2E8 File Offset: 0x0017C4E8
	public void UpdateInfo(string id)
	{
		bool flag = true;
		for (int i = 0; i < this.SpePackList.Count; i++)
		{
			if (this.SpePackList[i].ID.Equals(id))
			{
				this.SpePackList[i].remain_times += 1L;
				this.SpePackList[i].state = 2L;
				this.MainPage.UpdateInfo(this.SpePackList[i]);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Mystery", string.Format("require_{0}", id));
			}
			if (this.SpePackList[i].state == 0L)
			{
				flag = false;
			}
		}
		if (flag && SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.HideMysterySaleBtn();
		}
	}

	// Token: 0x060049C3 RID: 18883 RVA: 0x0017E3D8 File Offset: 0x0017C5D8
	public void OnDragMoveBtn(GameObject btn, Vector2 delta)
	{
		if (this.isMoveFlag)
		{
			return;
		}
		if (delta.x > 20f)
		{
			this.OnClickLeftBtn();
		}
		else if (delta.x < -20f)
		{
			this.OnClickRightBtn();
		}
	}

	// Token: 0x060049C4 RID: 18884 RVA: 0x0017E424 File Offset: 0x0017C624
	public void OnClickLeftBtn()
	{
		if (this.CurShowIndex > 0)
		{
			this.isMoveFlag = true;
			this.CurShowIndex--;
			this.MainAnima.from = this.MainAnima.transform.localPosition;
			this.MainAnima.to = this.rightPos;
			this.MainAnima.duration = this.MoveTime;
			this.MainAnima.ResetToBeginning();
			this.TempAnima.from = this.LeftPos;
			this.TempAnima.to = Vector3.zero;
			this.TempAnima.duration = this.MoveTime;
			this.TempAnima.ResetToBeginning();
			this.MainAnima.PlayForward();
			this.TempAnima.PlayForward();
			UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, true);
			this.TempPage.RefershInfo(this.SpePackList[this.CurShowIndex]);
			this.SwapPage();
			this.SelectPoint();
		}
	}

	// Token: 0x060049C5 RID: 18885 RVA: 0x0017E528 File Offset: 0x0017C728
	public void OnClickRightBtn()
	{
		if (this.CurShowIndex < this.SpePackList.Count - 1)
		{
			this.isMoveFlag = true;
			this.CurShowIndex++;
			this.MainAnima.from = this.MainAnima.transform.localPosition;
			this.MainAnima.to = this.LeftPos;
			this.MainAnima.duration = this.MoveTime;
			this.MainAnima.ResetToBeginning();
			this.TempAnima.from = this.rightPos;
			this.TempAnima.to = Vector3.zero;
			this.TempAnima.duration = this.MoveTime;
			this.TempAnima.ResetToBeginning();
			this.MainAnima.PlayForward();
			this.TempAnima.PlayForward();
			UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, true);
			this.TempPage.RefershInfo(this.SpePackList[this.CurShowIndex]);
			this.SwapPage();
			this.SelectPoint();
		}
	}

	// Token: 0x060049C6 RID: 18886 RVA: 0x0017E638 File Offset: 0x0017C838
	private void Update()
	{
		if (this.isMoveFlag)
		{
			this.temptime += Time.deltaTime;
			if (this.temptime >= this.MoveTime)
			{
				this.temptime -= this.MoveTime;
				this.isMoveFlag = false;
			}
		}
	}

	// Token: 0x060049C7 RID: 18887 RVA: 0x0017E690 File Offset: 0x0017C890
	public void OnClickBackBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MysteryShopRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x060049C8 RID: 18888 RVA: 0x0017E6E4 File Offset: 0x0017C8E4
	public void SelectPoint()
	{
		for (int i = 0; i < this.PointList.Count; i++)
		{
			if (i == this.CurShowIndex)
			{
				this.PointList[i].SetDimensions(13, 13);
				this.PointList[i].color = Color.white;
			}
			else
			{
				this.PointList[i].SetDimensions(10, 10);
				this.PointList[i].color = Color.gray;
			}
		}
		if (this.CurShowIndex == 0)
		{
			this.LeftSp.alpha = 0f;
		}
		else
		{
			this.LeftSp.alpha = 1f;
		}
		if (this.CurShowIndex == this.SpePackList.Count - 1)
		{
			this.RightSp.alpha = 0f;
		}
		else
		{
			this.RightSp.alpha = 1f;
		}
	}

	// Token: 0x060049C9 RID: 18889 RVA: 0x0017E7E4 File Offset: 0x0017C9E4
	public void ResetPoint()
	{
		int num = this.SpePackList.Count - this.PointList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.PointList[0].gameObject) as GameObject;
				UISprite component = gameObject.GetComponent<UISprite>();
				gameObject.name = string.Format("point{0:D2}", this.PointList.Count);
				gameObject.transform.parent = this.PointGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.PointList.Add(component);
			}
		}
		for (int j = 0; j < this.PointList.Count; j++)
		{
			if (j < this.SpePackList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.PointList[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.PointList[j].gameObject, false);
			}
		}
		this.PointGrid.Reposition();
		if (this.SpePackList.Count <= 1)
		{
			UnityVersionUtil.SetActiveRecursive(this.PointGrid.gameObject, false);
		}
	}

	// Token: 0x060049CA RID: 18890 RVA: 0x0017E92C File Offset: 0x0017CB2C
	public void SwapPage()
	{
		TweenPosition mainAnima = this.MainAnima;
		this.MainAnima = this.TempAnima;
		this.TempAnima = mainAnima;
		MysteryShopPageLogic mainPage = this.MainPage;
		this.MainPage = this.TempPage;
		this.TempPage = mainPage;
	}

	// Token: 0x040036FE RID: 14078
	public TweenPosition MainAnima;

	// Token: 0x040036FF RID: 14079
	public TweenPosition TempAnima;

	// Token: 0x04003700 RID: 14080
	public MysteryShopPageLogic MainPage;

	// Token: 0x04003701 RID: 14081
	public MysteryShopPageLogic TempPage;

	// Token: 0x04003702 RID: 14082
	public UISprite LeftSp;

	// Token: 0x04003703 RID: 14083
	public UISprite RightSp;

	// Token: 0x04003704 RID: 14084
	public UIGrid PointGrid;

	// Token: 0x04003705 RID: 14085
	public List<UISprite> PointList;

	// Token: 0x04003706 RID: 14086
	public UIEventListener uidragEvent;

	// Token: 0x04003707 RID: 14087
	private List<special_big_pack> SpePackList = new List<special_big_pack>();

	// Token: 0x04003708 RID: 14088
	private int CurShowIndex;

	// Token: 0x04003709 RID: 14089
	private Vector3 LeftPos = new Vector3(-1000f, 0f, 0f);

	// Token: 0x0400370A RID: 14090
	private Vector3 rightPos = new Vector3(1000f, 0f, 0f);

	// Token: 0x0400370B RID: 14091
	private float MoveTime = 0.5f;

	// Token: 0x0400370C RID: 14092
	private float temptime;

	// Token: 0x0400370D RID: 14093
	private bool isMoveFlag;

	// Token: 0x0400370E RID: 14094
	public string TargetID = string.Empty;
}
