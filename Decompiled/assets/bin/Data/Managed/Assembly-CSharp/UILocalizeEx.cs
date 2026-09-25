using System;
using UnityEngine;

// Token: 0x020008C6 RID: 2246
[RequireComponent(typeof(UIWidget))]
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalizeEx : MonoBehaviour
{
	// Token: 0x17000F82 RID: 3970
	// (set) Token: 0x06003C8A RID: 15498 RVA: 0x00109D68 File Offset: 0x00107F68
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

	// Token: 0x06003C8B RID: 15499 RVA: 0x00109DFC File Offset: 0x00107FFC
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnLocalize();
		}
	}

	// Token: 0x06003C8C RID: 15500 RVA: 0x00109E10 File Offset: 0x00108010
	private void Start()
	{
		this.mStarted = true;
		this.OnLocalize();
	}

	// Token: 0x06003C8D RID: 15501 RVA: 0x00109E20 File Offset: 0x00108020
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
			this.value = StrDictionary.GetDictionaryString(string.Format("#{{{0}}}", this.key), new object[]
			{
				this.parm
			});
		}
	}

	// Token: 0x040027C9 RID: 10185
	public string key;

	// Token: 0x040027CA RID: 10186
	public string parm;

	// Token: 0x040027CB RID: 10187
	private bool mStarted;
}
