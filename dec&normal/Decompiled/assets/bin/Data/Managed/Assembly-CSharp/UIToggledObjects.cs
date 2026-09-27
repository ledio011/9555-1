using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000078 RID: 120
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x0600026A RID: 618 RVA: 0x00011254 File Offset: 0x0000F454
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.activate.Count == 0 && this.deactivate.Count == 0)
			{
				if (this.inverse)
				{
					this.deactivate.Add(this.target);
				}
				else
				{
					this.activate.Add(this.target);
				}
			}
			else
			{
				this.target = null;
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.Toggle));
	}

	// Token: 0x0600026B RID: 619 RVA: 0x000112F0 File Offset: 0x0000F4F0
	public void Toggle()
	{
		bool value = UIToggle.current.value;
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.Set(this.activate[i], value);
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.Set(this.deactivate[j], !value);
			}
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00011374 File Offset: 0x0000F574
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x040002B9 RID: 697
	public List<GameObject> activate;

	// Token: 0x040002BA RID: 698
	public List<GameObject> deactivate;

	// Token: 0x040002BB RID: 699
	[SerializeField]
	[HideInInspector]
	private GameObject target;

	// Token: 0x040002BC RID: 700
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
