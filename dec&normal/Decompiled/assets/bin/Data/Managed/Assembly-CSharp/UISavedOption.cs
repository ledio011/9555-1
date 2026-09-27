using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000213 RID: 531 RVA: 0x0000E240 File Offset: 0x0000C440
	private string key
	{
		get
		{
			return (!string.IsNullOrEmpty(this.keyName)) ? this.keyName : ("NGUI State: " + base.name);
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000E270 File Offset: 0x0000C470
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.mCheck = base.GetComponent<UIToggle>();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000E28C File Offset: 0x0000C48C
	private void OnEnable()
	{
		if (this.mList != null)
		{
			EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
		}
		if (this.mCheck != null)
		{
			EventDelegate.Add(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
		}
		if (this.mList != null)
		{
			string @string = PlayerPrefs.GetString(this.key);
			if (!string.IsNullOrEmpty(@string))
			{
				this.mList.value = @string;
			}
			return;
		}
		if (this.mCheck != null)
		{
			this.mCheck.value = (PlayerPrefs.GetInt(this.key, 1) != 0);
		}
		else
		{
			string string2 = PlayerPrefs.GetString(this.key);
			UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIToggle uitoggle = componentsInChildren[i];
				uitoggle.value = (uitoggle.name == string2);
				i++;
			}
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
	private void OnDisable()
	{
		if (this.mCheck != null)
		{
			EventDelegate.Remove(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
		}
		if (this.mList != null)
		{
			EventDelegate.Remove(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
		}
		if (this.mCheck == null && this.mList == null)
		{
			UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIToggle uitoggle = componentsInChildren[i];
				if (uitoggle.value)
				{
					PlayerPrefs.SetString(this.key, uitoggle.name);
					break;
				}
				i++;
			}
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000E470 File Offset: 0x0000C670
	public void SaveSelection()
	{
		PlayerPrefs.SetString(this.key, UIPopupList.current.value);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000E488 File Offset: 0x0000C688
	public void SaveState()
	{
		PlayerPrefs.SetInt(this.key, (!UIToggle.current.value) ? 0 : 1);
	}

	// Token: 0x04000251 RID: 593
	public string keyName;

	// Token: 0x04000252 RID: 594
	private UIPopupList mList;

	// Token: 0x04000253 RID: 595
	private UIToggle mCheck;
}
