using System;
using UnityEngine;

// Token: 0x02000959 RID: 2393
public class MissionLineLogic : MonoBehaviour
{
	// Token: 0x060042F8 RID: 17144 RVA: 0x00148C48 File Offset: 0x00146E48
	public void Reset(MissionData data)
	{
		this.CurMissionData = data;
		this.MissionNameLabel.text = this.CurMissionData.MTipDescribeID;
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x00148C68 File Offset: 0x00146E68
	public void OnClickMissionLine()
	{
		SingletonUnity<MissionPageRootLogic>.Instance.ResetMissionDataPage(this);
	}

	// Token: 0x04002F5B RID: 12123
	public UILabel MissionNameLabel;

	// Token: 0x04002F5C RID: 12124
	public MissionData CurMissionData;
}
