using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class HitHyperlink : MonoBehaviour
{
	// Token: 0x06000B52 RID: 2898 RVA: 0x00053970 File Offset: 0x00051B70
	private void Start()
	{
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00053974 File Offset: 0x00051B74
	private void Update()
	{
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00053978 File Offset: 0x00051B78
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		string urlAtPosition = component.GetUrlAtPosition(UICamera.lastHit.point);
		if (urlAtPosition == null)
		{
			return;
		}
		Debug.Log("Hit->" + urlAtPosition.Substring(1));
		string text = urlAtPosition.Substring(1);
		if (text != null)
		{
			if (HitHyperlink.<>f__switch$map5 == null)
			{
				HitHyperlink.<>f__switch$map5 = new Dictionary<string, int>(0);
			}
			int num;
			if (HitHyperlink.<>f__switch$map5.TryGetValue(text, ref num))
			{
			}
		}
	}
}
