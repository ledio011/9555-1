using System;
using System.Collections.Generic;

// Token: 0x02000A5B RID: 2651
public class UIPathData
{
	// Token: 0x06004D3B RID: 19771 RVA: 0x001A48DC File Offset: 0x001A2ADC
	public UIPathData(string path1, UIPathData.UIType uiType1, bool HideBase = false, bool isDestroy = true, bool addback = false, bool closestate = false, bool reshow = false)
	{
		this.path = path1;
		this.uiType = uiType1;
		int num = path1.LastIndexOf('/');
		if (num > 0)
		{
			this.name = path1.Substring(num + 1);
		}
		else
		{
			this.name = path1;
		}
		this.isDestoryOnUnload = isDestroy;
		UIPathData.UINameDic.Add(this.name, this);
		this.hideBase = HideBase;
		this.backFlag = addback;
		this.needSelfClose = closestate;
		this.ReShowFlag = reshow;
	}

	// Token: 0x04003B04 RID: 15108
	public string path;

	// Token: 0x04003B05 RID: 15109
	public string name;

	// Token: 0x04003B06 RID: 15110
	public UIPathData.UIType uiType;

	// Token: 0x04003B07 RID: 15111
	public bool isDestoryOnUnload;

	// Token: 0x04003B08 RID: 15112
	public bool hideBase;

	// Token: 0x04003B09 RID: 15113
	public bool backFlag;

	// Token: 0x04003B0A RID: 15114
	public bool needSelfClose;

	// Token: 0x04003B0B RID: 15115
	public bool ReShowFlag;

	// Token: 0x04003B0C RID: 15116
	public static Dictionary<string, UIPathData> UINameDic = new Dictionary<string, UIPathData>();

	// Token: 0x02000A5C RID: 2652
	public enum UIType
	{
		// Token: 0x04003B0E RID: 15118
		TYPE_BASE,
		// Token: 0x04003B0F RID: 15119
		TYPE_POP,
		// Token: 0x04003B10 RID: 15120
		TYPE_MENU_POP,
		// Token: 0x04003B11 RID: 15121
		TYPE_MENU_TOP,
		// Token: 0x04003B12 RID: 15122
		TYPE_MEMU_TOP_2,
		// Token: 0x04003B13 RID: 15123
		TYPE_MEMU_TOP_3,
		// Token: 0x04003B14 RID: 15124
		TYPE_MESSAGE,
		// Token: 0x04003B15 RID: 15125
		TYPE_ITEM
	}
}
