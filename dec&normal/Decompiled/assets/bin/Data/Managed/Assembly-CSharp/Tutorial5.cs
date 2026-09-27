using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x060000DB RID: 219 RVA: 0x00006600 File Offset: 0x00004800
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		foreach (UITweener uitweener in componentsInChildren)
		{
			uitweener.duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
