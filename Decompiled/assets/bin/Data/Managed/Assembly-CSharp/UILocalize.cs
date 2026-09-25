using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
[AddComponentMenu("NGUI/UI/Localize")]
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class UILocalize : MonoBehaviour
{
	// Token: 0x17000117 RID: 279
	// (set) Token: 0x06000614 RID: 1556 RVA: 0x00029554 File Offset: 0x00027754
	public string value
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				UIWidget component = base.GetComponent<UIWidget>();
				UILabel uilabel = component as UILabel;
				UISprite uisprite = component as UISprite;
				if (uilabel != null)
				{
					UIInput uiinput = NGUITools.FindInParents<UIInput>(uilabel.gameObject);
					if (uiinput != null && uiinput.label == uilabel)
					{
						uiinput.defaultText = value;
					}
					else
					{
						uilabel.text = value;
					}
				}
				else if (uisprite != null)
				{
					uisprite.spriteName = value;
					uisprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000295E8 File Offset: 0x000277E8
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnLocalize();
		}
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000295FC File Offset: 0x000277FC
	private void Start()
	{
		this.mStarted = true;
		this.OnLocalize();
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002960C File Offset: 0x0002780C
	private void OnLocalize()
	{
		if (string.IsNullOrEmpty(this.key))
		{
			UILabel component = base.GetComponent<UILabel>();
			if (component != null)
			{
				this.key = component.text;
			}
		}
		if (!string.IsNullOrEmpty(this.key))
		{
			this.value = LocalizationManager.Get(this.key);
		}
	}

	// Token: 0x04000552 RID: 1362
	public string key;

	// Token: 0x04000553 RID: 1363
	private bool mStarted;
}
