using System;
using UnityEngine;

// Token: 0x02000A70 RID: 2672
public class MathUtil
{
	// Token: 0x06004DD4 RID: 19924 RVA: 0x001A9C50 File Offset: 0x001A7E50
	public static float WrapDegrees(float angle)
	{
		while (angle > 180f)
		{
			angle -= 360f;
		}
		while (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}

	// Token: 0x06004DD5 RID: 19925 RVA: 0x001A9C88 File Offset: 0x001A7E88
	public static float RotationDir(float a, float b)
	{
		return MathUtil.WrapDegrees(b - a);
	}

	// Token: 0x06004DD6 RID: 19926 RVA: 0x001A9C94 File Offset: 0x001A7E94
	public static float Heading(Vector3 dir)
	{
		if (dir == Vector3.zero)
		{
			return 0f;
		}
		return Mathf.Atan2(dir.x, dir.z) * 57.29578f;
	}

	// Token: 0x06004DD7 RID: 19927 RVA: 0x001A9CC8 File Offset: 0x001A7EC8
	public static float Pitch(Vector3 dir)
	{
		if (dir == Vector3.zero)
		{
			return 0f;
		}
		return Mathf.Asin(Mathf.Clamp(dir.y / dir.magnitude, -1f, 1f)) * 57.29578f;
	}

	// Token: 0x06004DD8 RID: 19928 RVA: 0x001A9D14 File Offset: 0x001A7F14
	public static Vector3 HeadingToVector3(float heading)
	{
		float num = Mathf.Sin(heading * 0.017453292f);
		float num2 = Mathf.Cos(heading * 0.017453292f);
		return new Vector3(num, 0f, num2);
	}

	// Token: 0x06004DD9 RID: 19929 RVA: 0x001A9D48 File Offset: 0x001A7F48
	public static float ClampScale(float f, float c1, float c2, float s1, float s2)
	{
		if (c2 == c1)
		{
			return s2;
		}
		if (c2 > c1)
		{
			return (Mathf.Clamp(f, c1, c2) - c1) / (c2 - c1) * (s2 - s1) + s1;
		}
		return (-Mathf.Clamp(-f, -c1, -c2) - c1) / (c2 - c1) * (s2 - s1) + s1;
	}

	// Token: 0x06004DDA RID: 19930 RVA: 0x001A9D98 File Offset: 0x001A7F98
	public static float SmoothLerp(float fo, float to, float t)
	{
		t = Mathf.Clamp01(t);
		if (t <= 0.5f)
		{
			t = 2f * t * t;
		}
		else
		{
			t = 1f - t;
			t = 1f - 2f * t * t;
		}
		return Mathf.Lerp(fo, to, t);
	}

	// Token: 0x06004DDB RID: 19931 RVA: 0x001A9DEC File Offset: 0x001A7FEC
	public static float SmoothLerpForm(float fo, float to, float t)
	{
		return MathUtil.SmoothLerp(fo, 2f * to - fo, t * 0.5f);
	}

	// Token: 0x06004DDC RID: 19932 RVA: 0x001A9E04 File Offset: 0x001A8004
	public static float SmoothLerpTo(float fo, float to, float t)
	{
		return fo + to - MathUtil.SmoothLerpForm(fo, to, 1f - t);
	}

	// Token: 0x06004DDD RID: 19933 RVA: 0x001A9E18 File Offset: 0x001A8018
	public static Vector3 SmoothStep(Vector3 current, Vector3 dest, float time)
	{
		float num = Mathf.SmoothStep(current.x, dest.x, time);
		float num2 = Mathf.SmoothStep(current.y, dest.y, time);
		float num3 = Mathf.SmoothStep(current.z, dest.z, time);
		return new Vector3(num, num2, num3);
	}
}
