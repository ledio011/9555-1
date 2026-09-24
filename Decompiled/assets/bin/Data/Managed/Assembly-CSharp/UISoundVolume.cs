using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x0600024F RID: 591 RVA: 0x000105E4 File Offset: 0x0000E7E4
	private void Awake()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.mSlider.value = NGUITools.soundVolume;
		EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0001062C File Offset: 0x0000E82C
	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}

	// Token: 0x0400028F RID: 655
	private UISlider mSlider;
}
