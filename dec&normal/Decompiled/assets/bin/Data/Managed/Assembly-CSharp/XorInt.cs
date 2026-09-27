using System;
using UnityEngine;

// Token: 0x02000A80 RID: 2688
[Serializable]
public class XorInt
{
	// Token: 0x06004E41 RID: 20033 RVA: 0x001AB45C File Offset: 0x001A965C
	public XorInt() : this(0)
	{
	}

	// Token: 0x06004E42 RID: 20034 RVA: 0x001AB468 File Offset: 0x001A9668
	public XorInt(int value)
	{
		this.GenerateKey();
		this.realValue = value;
		this.rawValue = this.Xor(value);
	}

	// Token: 0x06004E43 RID: 20035 RVA: 0x001AB498 File Offset: 0x001A9698
	private void GenerateKey()
	{
		this.key = Random.Range(0, 65535);
	}

	// Token: 0x17000FE1 RID: 4065
	// (get) Token: 0x06004E44 RID: 20036 RVA: 0x001AB4AC File Offset: 0x001A96AC
	// (set) Token: 0x06004E45 RID: 20037 RVA: 0x001AB4B4 File Offset: 0x001A96B4
	public int rawValue { get; private set; }

	// Token: 0x17000FE2 RID: 4066
	// (get) Token: 0x06004E46 RID: 20038 RVA: 0x001AB4C0 File Offset: 0x001A96C0
	// (set) Token: 0x06004E47 RID: 20039 RVA: 0x001AB4FC File Offset: 0x001A96FC
	public int value
	{
		get
		{
			if ((this.rawValue ^ this.key) != this.realValue)
			{
				SingletonDontDestoryUnity<NetManager>.Instance.GameCheck();
			}
			return this.Xor(this.rawValue);
		}
		set
		{
			this.GenerateKey();
			this.realValue = value;
			this.rawValue = this.Xor(value);
		}
	}

	// Token: 0x06004E48 RID: 20040 RVA: 0x001AB518 File Offset: 0x001A9718
	private int Xor(int x)
	{
		return x ^ this.key;
	}

	// Token: 0x06004E49 RID: 20041 RVA: 0x001AB524 File Offset: 0x001A9724
	public override string ToString()
	{
		return this.value.ToString();
	}

	// Token: 0x06004E4A RID: 20042 RVA: 0x001AB540 File Offset: 0x001A9740
	public string ToString(string format)
	{
		return this.value.ToString(format);
	}

	// Token: 0x06004E4B RID: 20043 RVA: 0x001AB55C File Offset: 0x001A975C
	public string ToString(IFormatProvider provider)
	{
		return this.value.ToString(provider);
	}

	// Token: 0x06004E4C RID: 20044 RVA: 0x001AB578 File Offset: 0x001A9778
	public string ToString(string format, IFormatProvider provider)
	{
		return this.value.ToString(format, provider);
	}

	// Token: 0x06004E4D RID: 20045 RVA: 0x001AB598 File Offset: 0x001A9798
	public static implicit operator int(XorInt xor)
	{
		if (xor == null)
		{
			return 0;
		}
		return xor.value;
	}

	// Token: 0x06004E4E RID: 20046 RVA: 0x001AB5A8 File Offset: 0x001A97A8
	public static implicit operator XorInt(int val)
	{
		return new XorInt(val);
	}

	// Token: 0x06004E4F RID: 20047 RVA: 0x001AB5B0 File Offset: 0x001A97B0
	public static XorInt operator ++(XorInt val)
	{
		val++;
		return val;
	}

	// Token: 0x06004E50 RID: 20048 RVA: 0x001AB5C4 File Offset: 0x001A97C4
	public static XorInt operator --(XorInt val)
	{
		val--;
		return val;
	}

	// Token: 0x04003CA9 RID: 15529
	private int key;

	// Token: 0x04003CAA RID: 15530
	private int realValue;
}
