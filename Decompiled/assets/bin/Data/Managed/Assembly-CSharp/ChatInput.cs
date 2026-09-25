using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[AddComponentMenu("NGUI/Examples/Chat Input")]
[RequireComponent(typeof(UIInput))]
public class ChatInput : MonoBehaviour
{
	// Token: 0x060000B0 RID: 176 RVA: 0x0000581C File Offset: 0x00003A1C
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		this.mInput.label.maxLineCount = 1;
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000058C0 File Offset: 0x00003AC0
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUIText.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = string.Empty;
				this.mInput.isSelected = false;
			}
		}
	}

	// Token: 0x040000C4 RID: 196
	public UITextList textList;

	// Token: 0x040000C5 RID: 197
	public bool fillWithDummyData;

	// Token: 0x040000C6 RID: 198
	private UIInput mInput;
}
