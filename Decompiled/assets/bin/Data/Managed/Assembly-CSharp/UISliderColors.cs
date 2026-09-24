using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
[AddComponentMenu("NGUI/Examples/Slider Colors")]
[RequireComponent(typeof(UIProgressBar))]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x060000E0 RID: 224 RVA: 0x000067D4 File Offset: 0x000049D4
	private void Start()
	{
		this.mBar = base.GetComponent<UIProgressBar>();
		this.Update();
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x000067E8 File Offset: 0x000049E8
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = this.mBar.value;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 < this.colors.Length)
			{
				float num3 = num - (float)num2;
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], num3);
			}
			else if (num2 < this.colors.Length)
			{
				color = this.colors[num2];
			}
			else
			{
				color = this.colors[this.colors.Length - 1];
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x040000FB RID: 251
	public UISprite sprite;

	// Token: 0x040000FC RID: 252
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040000FD RID: 253
	private UIProgressBar mBar;
}
