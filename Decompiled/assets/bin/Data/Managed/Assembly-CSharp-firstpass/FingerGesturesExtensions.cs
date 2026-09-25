using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public static class FingerGesturesExtensions
{
	// Token: 0x06000118 RID: 280 RVA: 0x0000522C File Offset: 0x0000342C
	public static string Abreviation(this DistanceUnit unit)
	{
		switch (unit)
		{
		case DistanceUnit.Pixels:
			return "px";
		case DistanceUnit.Inches:
			return "in";
		case DistanceUnit.Centimeters:
			return "cm";
		default:
			return unit.ToString();
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00005270 File Offset: 0x00003470
	public static float Convert(this float value, DistanceUnit fromUnit, DistanceUnit toUnit)
	{
		return FingerGestures.Convert(value, fromUnit, toUnit);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000527C File Offset: 0x0000347C
	public static float In(this float valueInPixels, DistanceUnit toUnit)
	{
		return valueInPixels.Convert(DistanceUnit.Pixels, toUnit);
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00005288 File Offset: 0x00003488
	public static float Centimeters(this float valueInPixels)
	{
		return valueInPixels.In(DistanceUnit.Centimeters);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00005294 File Offset: 0x00003494
	public static float Inches(this float valueInPixels)
	{
		return valueInPixels.In(DistanceUnit.Inches);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000052A0 File Offset: 0x000034A0
	public static Vector2 Convert(this Vector2 v, DistanceUnit fromUnit, DistanceUnit toUnit)
	{
		return FingerGestures.Convert(v, fromUnit, toUnit);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000052AC File Offset: 0x000034AC
	public static Vector2 In(this Vector2 vecInPixels, DistanceUnit toUnit)
	{
		return vecInPixels.Convert(DistanceUnit.Pixels, toUnit);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x000052B8 File Offset: 0x000034B8
	public static Vector2 Centimeters(this Vector2 vecInPixels)
	{
		return vecInPixels.In(DistanceUnit.Centimeters);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000052C4 File Offset: 0x000034C4
	public static Vector2 Inches(this Vector2 vecInPixels)
	{
		return vecInPixels.In(DistanceUnit.Inches);
	}
}
