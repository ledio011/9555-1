using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000035 RID: 53
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Set Color on Selection")]
[ExecuteInEditMode]
public class SetColorOnSelection : MonoBehaviour
{
	// Token: 0x060000CF RID: 207 RVA: 0x00006240 File Offset: 0x00004440
	public void SetSpriteBySelection()
	{
		if (UIPopupList.current == null)
		{
			return;
		}
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		string value = UIPopupList.current.value;
		if (value != null)
		{
			if (SetColorOnSelection.<>f__switch$map0 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
				dictionary.Add("White", 0);
				dictionary.Add("Red", 1);
				dictionary.Add("Green", 2);
				dictionary.Add("Blue", 3);
				dictionary.Add("Yellow", 4);
				dictionary.Add("Cyan", 5);
				dictionary.Add("Magenta", 6);
				SetColorOnSelection.<>f__switch$map0 = dictionary;
			}
			int num;
			if (SetColorOnSelection.<>f__switch$map0.TryGetValue(value, ref num))
			{
				switch (num)
				{
				case 0:
					this.mWidget.color = Color.white;
					break;
				case 1:
					this.mWidget.color = Color.red;
					break;
				case 2:
					this.mWidget.color = Color.green;
					break;
				case 3:
					this.mWidget.color = Color.blue;
					break;
				case 4:
					this.mWidget.color = Color.yellow;
					break;
				case 5:
					this.mWidget.color = Color.cyan;
					break;
				case 6:
					this.mWidget.color = Color.magenta;
					break;
				}
			}
		}
	}

	// Token: 0x040000EB RID: 235
	private UIWidget mWidget;
}
