using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x060000EA RID: 234 RVA: 0x00006AA0 File Offset: 0x00004CA0
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		if (Localization.knownLanguages != null)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00006B30 File Offset: 0x00004D30
	private void OnChange()
	{
		Localization.language = UIPopupList.current.value;
	}

	// Token: 0x04000107 RID: 263
	private UIPopupList mList;
}
