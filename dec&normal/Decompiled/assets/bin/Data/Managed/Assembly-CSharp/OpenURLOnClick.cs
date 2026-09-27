using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x060000C7 RID: 199 RVA: 0x00005E58 File Offset: 0x00004058
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.lastHit.point);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
