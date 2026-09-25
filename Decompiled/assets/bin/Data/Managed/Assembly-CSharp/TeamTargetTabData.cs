using System;
using System.Collections.Generic;

// Token: 0x020009C5 RID: 2501
public class TeamTargetTabData
{
	// Token: 0x06004735 RID: 18229 RVA: 0x0016B0EC File Offset: 0x001692EC
	public void Reset(string title, List<string> subTitle, string key, List<string> subKey)
	{
		this.Title = title;
		this.SubTitle = subTitle;
		this.Key = key;
		this.SubKey = subKey;
	}

	// Token: 0x0400345E RID: 13406
	public string Title;

	// Token: 0x0400345F RID: 13407
	public string Key;

	// Token: 0x04003460 RID: 13408
	public List<string> SubTitle;

	// Token: 0x04003461 RID: 13409
	public List<string> SubKey;
}
