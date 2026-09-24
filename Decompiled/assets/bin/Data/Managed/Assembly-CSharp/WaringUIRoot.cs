using System;
using UnityEngine;

// Token: 0x02000A52 RID: 2642
public class WaringUIRoot : SingletonUnity<WaringUIRoot>
{
	// Token: 0x06004D01 RID: 19713 RVA: 0x001A320C File Offset: 0x001A140C
	public void Reset(string waringStr, float duration)
	{
		this.WaringLabel.text = waringStr;
		this.mStartTime = Time.time;
		this.mDuration = duration;
	}

	// Token: 0x06004D02 RID: 19714 RVA: 0x001A322C File Offset: 0x001A142C
	private void Update()
	{
		if (Time.time - this.mStartTime >= this.mDuration)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WaringUIRoot);
		}
	}

	// Token: 0x04003AA2 RID: 15010
	public UILabel WaringLabel;

	// Token: 0x04003AA3 RID: 15011
	private float mStartTime;

	// Token: 0x04003AA4 RID: 15012
	private float mDuration;
}
