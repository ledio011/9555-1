using System;

// Token: 0x02000010 RID: 16
public class BasicSample : SampleBase
{
	// Token: 0x06000049 RID: 73 RVA: 0x000037D0 File Offset: 0x000019D0
	protected override string GetHelpText()
	{
		return this.helpText;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000037D8 File Offset: 0x000019D8
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = this.statusText;
	}

	// Token: 0x0400003E RID: 62
	public string helpText = "Help text here";

	// Token: 0x0400003F RID: 63
	public string statusText = string.Empty;
}
