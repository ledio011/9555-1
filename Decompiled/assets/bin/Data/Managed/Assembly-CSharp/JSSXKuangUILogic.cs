using System;
using UnityEngine;

// Token: 0x0200094C RID: 2380
public class JSSXKuangUILogic : MonoBehaviour
{
	// Token: 0x06004291 RID: 17041 RVA: 0x00145614 File Offset: 0x00143814
	public void Reset(CharacterAttributeData data)
	{
		this.HPValLabel.text = data.MaxHP.ToString();
		this.ATKValLabel.text = data.ATK.ToString();
		this.CRIValLabel.text = data.CRI.ToString();
		this.HITValLabel.text = data.HIT.ToString();
		this.DEFValLabel.text = data.DEF.ToString();
		this.DGEValLabel.text = data.DGE.ToString();
		this.EXDValLabel.text = string.Format("{0:P1}", data.EXD);
		this.EXRValLabel.text = string.Format("{0:P1}", data.EXR);
		this.RESValLabel.text = data.RES.ToString();
		this.ATKLabel.text = GameDefine.GetAttributeName(1001);
		this.HPLabel.text = GameDefine.GetAttributeName(1002);
		this.DEFLabel.text = GameDefine.GetAttributeName(1003);
		this.HITLabel.text = GameDefine.GetAttributeName(1004);
		this.DGELabel.text = GameDefine.GetAttributeName(1005);
		this.CRILabel.text = GameDefine.GetAttributeName(1006);
		this.RESLabel.text = GameDefine.GetAttributeName(1007);
		this.EXDLabel.text = GameDefine.GetAttributeName(1008);
		this.EXRLabel.text = GameDefine.GetAttributeName(1009);
		this.ATKsp.spriteName = GameDefine.GetAttributeIcon(1001);
		this.HPsp.spriteName = GameDefine.GetAttributeIcon(1002);
		this.DEFsp.spriteName = GameDefine.GetAttributeIcon(1003);
		this.HITsp.spriteName = GameDefine.GetAttributeIcon(1004);
		this.DGEsp.spriteName = GameDefine.GetAttributeIcon(1005);
		this.CRIsp.spriteName = GameDefine.GetAttributeIcon(1006);
		this.RESsp.spriteName = GameDefine.GetAttributeIcon(1007);
		this.EXDsp.spriteName = GameDefine.GetAttributeIcon(1008);
		this.EXRsp.spriteName = GameDefine.GetAttributeIcon(1009);
	}

	// Token: 0x06004292 RID: 17042 RVA: 0x00145890 File Offset: 0x00143A90
	public void Show(CharacterAttributeData data)
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		this.Reset(data);
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x001458A8 File Offset: 0x00143AA8
	public void Show()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x001458B8 File Offset: 0x00143AB8
	public void Hide()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x001458C8 File Offset: 0x00143AC8
	public void OnClickTipBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate(bool bSuccess, object param)
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{100633}", "#{100634}", null, new object[0]);
		}, null);
	}

	// Token: 0x04002EA3 RID: 11939
	public UISprite ATKsp;

	// Token: 0x04002EA4 RID: 11940
	public UISprite HPsp;

	// Token: 0x04002EA5 RID: 11941
	public UISprite DEFsp;

	// Token: 0x04002EA6 RID: 11942
	public UISprite HITsp;

	// Token: 0x04002EA7 RID: 11943
	public UISprite DGEsp;

	// Token: 0x04002EA8 RID: 11944
	public UISprite CRIsp;

	// Token: 0x04002EA9 RID: 11945
	public UISprite RESsp;

	// Token: 0x04002EAA RID: 11946
	public UISprite EXDsp;

	// Token: 0x04002EAB RID: 11947
	public UISprite EXRsp;

	// Token: 0x04002EAC RID: 11948
	public UILabel ATKValLabel;

	// Token: 0x04002EAD RID: 11949
	public UILabel HPValLabel;

	// Token: 0x04002EAE RID: 11950
	public UILabel DEFValLabel;

	// Token: 0x04002EAF RID: 11951
	public UILabel HITValLabel;

	// Token: 0x04002EB0 RID: 11952
	public UILabel DGEValLabel;

	// Token: 0x04002EB1 RID: 11953
	public UILabel CRIValLabel;

	// Token: 0x04002EB2 RID: 11954
	public UILabel RESValLabel;

	// Token: 0x04002EB3 RID: 11955
	public UILabel EXDValLabel;

	// Token: 0x04002EB4 RID: 11956
	public UILabel EXRValLabel;

	// Token: 0x04002EB5 RID: 11957
	public UILabel ATKLabel;

	// Token: 0x04002EB6 RID: 11958
	public UILabel HPLabel;

	// Token: 0x04002EB7 RID: 11959
	public UILabel DEFLabel;

	// Token: 0x04002EB8 RID: 11960
	public UILabel HITLabel;

	// Token: 0x04002EB9 RID: 11961
	public UILabel DGELabel;

	// Token: 0x04002EBA RID: 11962
	public UILabel CRILabel;

	// Token: 0x04002EBB RID: 11963
	public UILabel RESLabel;

	// Token: 0x04002EBC RID: 11964
	public UILabel EXDLabel;

	// Token: 0x04002EBD RID: 11965
	public UILabel EXRLabel;
}
