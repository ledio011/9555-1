using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class ToolboxFingerEventsSample : SampleBase
{
	// Token: 0x06000043 RID: 67 RVA: 0x00003760 File Offset: 0x00001960
	private void ToggleLight1()
	{
		this.light1.enabled = !this.light1.enabled;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000377C File Offset: 0x0000197C
	private void ToggleLight2()
	{
		this.light2.enabled = !this.light2.enabled;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003798 File Offset: 0x00001998
	protected override string GetHelpText()
	{
		return "This sample demonstrates the use of the toolbox scripts TBFingerDown and TBFingerUp. It also shows how you can use the message target property to turn the light on & off.";
	}

	// Token: 0x0400003C RID: 60
	public Light light1;

	// Token: 0x0400003D RID: 61
	public Light light2;
}
