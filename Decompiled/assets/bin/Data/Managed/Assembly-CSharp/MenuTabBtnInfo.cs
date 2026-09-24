using System;

// Token: 0x020009FB RID: 2555
public class MenuTabBtnInfo
{
	// Token: 0x0600492C RID: 18732 RVA: 0x001799C4 File Offset: 0x00177BC4
	public MenuTabBtnInfo(DelegateDefine.NoParamDelegate onClickFunc, bool isIcon, string btnName, string pageName, FUNCTION_TYPE functionType, DelegateDefine.NoParamReturnDelegate tipsfun = null)
	{
		this.onClickBtn = onClickFunc;
		this.IsIconBtn = isIcon;
		this.BtnName = btnName;
		this.PageName = pageName;
		this.TipsFun = tipsfun;
		this.FunctionType = functionType;
	}

	// Token: 0x0400365B RID: 13915
	public bool IsIconBtn;

	// Token: 0x0400365C RID: 13916
	public string BtnName = string.Empty;

	// Token: 0x0400365D RID: 13917
	public DelegateDefine.NoParamDelegate onClickBtn;

	// Token: 0x0400365E RID: 13918
	public string PageName = string.Empty;

	// Token: 0x0400365F RID: 13919
	public DelegateDefine.NoParamReturnDelegate TipsFun;

	// Token: 0x04003660 RID: 13920
	public FUNCTION_TYPE FunctionType;
}
