using System;
using SprotoType;
using UnityEngine;

// Token: 0x0200097E RID: 2430
public class RankPVPPlayerLogic : MonoBehaviour
{
	// Token: 0x060044A6 RID: 17574 RVA: 0x00156FD4 File Offset: 0x001551D4
	public void UpdateInfo(character_look lookvalue, int rank, RankPVPPlayerLogic.clickfightfun clickfun = null, DelegateDefine.NoParamDelegate loadfun = null)
	{
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragModelBtn);
		this.clickfight = clickfun;
		this.loadfinish = loadfun;
		PROFESSION_TYPE profession_TYPE = (PROFESSION_TYPE)lookvalue.general.profession;
		string modelName = string.Empty;
		if (profession_TYPE == PROFESSION_TYPE.XD)
		{
			modelName = "baiRen";
		}
		else if (profession_TYPE == PROFESSION_TYPE.QJ)
		{
			modelName = "heiRen";
		}
		else
		{
			modelName = "nvRen";
		}
		if (this.OtherPlayerFakeObjs != null)
		{
			if (!this.mPreProfession.Equals(profession_TYPE))
			{
				this.OtherPlayerFakeObjs.DestroyFakeObj();
			}
		}
		else
		{
			this.OtherPlayerFakeObjs = new FakeObjLogic();
		}
		this.CurFakeObjRoot.EnableFakeObjRoot();
		this.CurModelPic.mainTexture = this.CurFakeObjRoot.ModelPic;
		general general = lookvalue.general;
		characterVisual visual = lookvalue.visual;
		this.OtherPlayerFakeObjs.InitFakeObject(visual, profession_TYPE.ToString(), modelName, this.CurFakeObjRoot.MeshRoot, new FakeObjLogic.OnLoadFinishedDel(this.OnInitFakeObjDone), "FakeObj");
		this.OtherPlayerRankLabel.text = string.Format("{0}: {1}", StrDictionary.GetDictionaryString("#{100754}", new object[0]), rank);
		this.OtherPlayerNameLabel.text = general.name;
		this.OtherPlayerComboValueLabel.text = lookvalue.attribute_other.combValue.ToString();
		this.OtherPlayerIconSprite.spriteName = GameDefine.Profession_PicName[(int)lookvalue.general.profession];
		this.OtherPlayerIndex = lookvalue.id;
		this.mPreProfession = profession_TYPE;
		this.OtherPlayerLevelLabel.text = string.Format("Lv.{0}", lookvalue.attribute_other.level);
	}

	// Token: 0x060044A7 RID: 17575 RVA: 0x00157198 File Offset: 0x00155398
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.OtherPlayerFakeObjs != null && this.OtherPlayerFakeObjs.FakeObj != null)
		{
			this.OtherPlayerFakeObjs.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x060044A8 RID: 17576 RVA: 0x001571F8 File Offset: 0x001553F8
	private void OnInitFakeObjDone(FakeObjLogic obj)
	{
		if (this.loadfinish != null)
		{
			this.loadfinish();
		}
	}

	// Token: 0x060044A9 RID: 17577 RVA: 0x00157210 File Offset: 0x00155410
	public void OnClickFightBtn()
	{
		if (this.clickfight != null)
		{
			this.clickfight(this.OtherPlayerIndex);
		}
	}

	// Token: 0x060044AA RID: 17578 RVA: 0x00157230 File Offset: 0x00155430
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x060044AB RID: 17579 RVA: 0x00157238 File Offset: 0x00155438
	public void UnLoadFakeObj()
	{
		if (this.OtherPlayerFakeObjs != null)
		{
			this.OtherPlayerFakeObjs.DestroyFakeObj();
			this.OtherPlayerFakeObjs = null;
		}
		else
		{
			Debug.Log("mPlayerModelVisual == null");
		}
		if (this.CurFakeObjRoot != null)
		{
			this.CurFakeObjRoot.DisableFakeObjRoot();
		}
	}

	// Token: 0x04003157 RID: 12631
	public UIEventListener RotateModelBtnListener;

	// Token: 0x04003158 RID: 12632
	public UILabel OtherPlayerNameLabel;

	// Token: 0x04003159 RID: 12633
	public UILabel OtherPlayerComboValueLabel;

	// Token: 0x0400315A RID: 12634
	public UILabel OtherPlayerLevelLabel;

	// Token: 0x0400315B RID: 12635
	public UILabel OtherPlayerRankLabel;

	// Token: 0x0400315C RID: 12636
	public UISprite OtherPlayerIconSprite;

	// Token: 0x0400315D RID: 12637
	private FakeObjLogic OtherPlayerFakeObjs = new FakeObjLogic();

	// Token: 0x0400315E RID: 12638
	private long OtherPlayerIndex;

	// Token: 0x0400315F RID: 12639
	private PROFESSION_TYPE mPreProfession = PROFESSION_TYPE.INVALID;

	// Token: 0x04003160 RID: 12640
	public UITexture CurModelPic;

	// Token: 0x04003161 RID: 12641
	public RankPVPFakeObj CurFakeObjRoot;

	// Token: 0x04003162 RID: 12642
	private RankPVPPlayerLogic.clickfightfun clickfight;

	// Token: 0x04003163 RID: 12643
	private DelegateDefine.NoParamDelegate loadfinish;

	// Token: 0x02000AF8 RID: 2808
	// (Invoke) Token: 0x06005069 RID: 20585
	public delegate void clickfightfun(long id);
}
