using System;
using UnityEngine;

// Token: 0x0200093D RID: 2365
public class FakeMissionTutorial : SingletonUnity<FakeMissionTutorial>
{
	// Token: 0x060041CE RID: 16846 RVA: 0x0013A63C File Offset: 0x0013883C
	private void OnEnable()
	{
		this.StartTime = Time.time;
		this.isHaveHand = true;
	}

	// Token: 0x060041CF RID: 16847 RVA: 0x0013A650 File Offset: 0x00138850
	public void OnClickMissionBtn()
	{
		(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager).MissionMoveToPoint();
		this.StartTime = Time.time;
		UnityVersionUtil.SetActiveRecursive(this.Handobj.gameObject, false);
		this.isHaveHand = false;
	}

	// Token: 0x060041D0 RID: 16848 RVA: 0x0013A694 File Offset: 0x00138894
	private void Update()
	{
		if (!this.isHaveHand && Time.time - this.StartTime > 10f)
		{
			UnityVersionUtil.SetActiveRecursive(this.Handobj.gameObject, true);
			this.isHaveHand = true;
		}
	}

	// Token: 0x04002DBB RID: 11707
	private float StartTime;

	// Token: 0x04002DBC RID: 11708
	private bool isHaveHand;

	// Token: 0x04002DBD RID: 11709
	public GameObject Handobj;
}
