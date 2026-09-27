using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class ColorData
{
	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000619F0 File Offset: 0x0005FBF0
	public Color CShaderColor
	{
		get
		{
			return NGUIText.ParseColor(this.ShaderColor, 0);
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06000EED RID: 3821 RVA: 0x00061A00 File Offset: 0x0005FC00
	public Color CShaderRimColor
	{
		get
		{
			return NGUIText.ParseColor(this.ShaderRimColor, 0);
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00061A10 File Offset: 0x0005FC10
	public Color CUIColor
	{
		get
		{
			return NGUIText.ParseColor(this.UIColor, 0);
		}
	}

	// Token: 0x04000D77 RID: 3447
	public string ID;

	// Token: 0x04000D78 RID: 3448
	public string Name;

	// Token: 0x04000D79 RID: 3449
	public int PriceType;

	// Token: 0x04000D7A RID: 3450
	public int PriceCost;

	// Token: 0x04000D7B RID: 3451
	[ServerExclude("ServerNoUse")]
	public string ShaderColor;

	// Token: 0x04000D7C RID: 3452
	[ServerExclude("ServerNoUse")]
	public string ShaderRimColor;

	// Token: 0x04000D7D RID: 3453
	[ServerExclude("ServerNoUse")]
	public float ShaderReflAmount;

	// Token: 0x04000D7E RID: 3454
	[ServerExclude("ServerNoUse")]
	public float ShaderRimPower;

	// Token: 0x04000D7F RID: 3455
	[ServerExclude("ServerNoUse")]
	public string UIColor;
}
