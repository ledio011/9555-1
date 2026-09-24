using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000077 RID: 119
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
[RequireComponent(typeof(UIToggle))]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x06000267 RID: 615 RVA: 0x00011120 File Offset: 0x0000F320
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

	// Token: 0x06000268 RID: 616 RVA: 0x000111BC File Offset: 0x0000F3BC
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				MonoBehaviour monoBehaviour = this.activate[i];
				monoBehaviour.enabled = UIToggle.current.value;
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				MonoBehaviour monoBehaviour2 = this.deactivate[j];
				monoBehaviour2.enabled = !UIToggle.current.value;
			}
		}
	}

	// Token: 0x040002B5 RID: 693
	public List<MonoBehaviour> activate;

	// Token: 0x040002B6 RID: 694
	public List<MonoBehaviour> deactivate;

	// Token: 0x040002B7 RID: 695
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	// Token: 0x040002B8 RID: 696
	[SerializeField]
	[HideInInspector]
	private bool inverse;
}
