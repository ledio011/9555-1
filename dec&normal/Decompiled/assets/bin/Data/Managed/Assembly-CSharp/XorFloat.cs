using System;

// Token: 0x02000A7F RID: 2687
[Serializable]
public class XorFloat
{
	// Token: 0x06004E34 RID: 20020 RVA: 0x001AB23C File Offset: 0x001A943C
	public XorFloat() : this(0f)
	{
	}

	// Token: 0x06004E35 RID: 20021 RVA: 0x001AB24C File Offset: 0x001A944C
	public XorFloat(float value)
	{
		this.GenerateKey();
		this.realValue = value;
		this.Xor(value);
	}

	// Token: 0x06004E36 RID: 20022 RVA: 0x001AB28C File Offset: 0x001A948C
	private void GenerateKey()
	{
		new Random().NextBytes(this.key);
	}

	// Token: 0x17000FE0 RID: 4064
	// (get) Token: 0x06004E37 RID: 20023 RVA: 0x001AB2A0 File Offset: 0x001A94A0
	// (set) Token: 0x06004E38 RID: 20024 RVA: 0x001AB2A8 File Offset: 0x001A94A8
	public float value
	{
		get
		{
			return this.Xor();
		}
		set
		{
			this.realValue = value;
			this.GenerateKey();
			this.Xor(value);
		}
	}

	// Token: 0x06004E39 RID: 20025 RVA: 0x001AB2C0 File Offset: 0x001A94C0
	private void Xor(float x)
	{
		this.bytes = BitConverter.GetBytes(x);
		byte[] array = this.bytes;
		int num = 0;
		array[num] ^= this.key[0];
		byte[] array2 = this.bytes;
		int num2 = 1;
		array2[num2] ^= this.key[1];
		byte[] array3 = this.bytes;
		int num3 = 2;
		array3[num3] ^= this.key[2];
		byte[] array4 = this.bytes;
		int num4 = 3;
		array4[num4] ^= this.key[3];
	}

	// Token: 0x06004E3A RID: 20026 RVA: 0x001AB340 File Offset: 0x001A9540
	private float Xor()
	{
		byte[] array = new byte[]
		{
			this.bytes[0] ^ this.key[0],
			this.bytes[1] ^ this.key[1],
			this.bytes[2] ^ this.key[2],
			this.bytes[3] ^ this.key[3]
		};
		if (this.realValue != BitConverter.ToSingle(array, 0))
		{
			SingletonDontDestoryUnity<NetManager>.Instance.GameCheck();
		}
		return BitConverter.ToSingle(array, 0);
	}

	// Token: 0x06004E3B RID: 20027 RVA: 0x001AB3CC File Offset: 0x001A95CC
	public override string ToString()
	{
		return this.value.ToString();
	}

	// Token: 0x06004E3C RID: 20028 RVA: 0x001AB3E8 File Offset: 0x001A95E8
	public string ToString(string format)
	{
		return this.value.ToString(format);
	}

	// Token: 0x06004E3D RID: 20029 RVA: 0x001AB404 File Offset: 0x001A9604
	public string ToString(IFormatProvider provider)
	{
		return this.value.ToString(provider);
	}

	// Token: 0x06004E3E RID: 20030 RVA: 0x001AB420 File Offset: 0x001A9620
	public string ToString(string format, IFormatProvider provider)
	{
		return this.value.ToString(format, provider);
	}

	// Token: 0x06004E3F RID: 20031 RVA: 0x001AB440 File Offset: 0x001A9640
	public static implicit operator float(XorFloat xor)
	{
		if (xor == null)
		{
			return 0f;
		}
		return xor.value;
	}

	// Token: 0x06004E40 RID: 20032 RVA: 0x001AB454 File Offset: 0x001A9654
	public static implicit operator XorFloat(float val)
	{
		return new XorFloat(val);
	}

	// Token: 0x04003CA6 RID: 15526
	private byte[] key = new byte[4];

	// Token: 0x04003CA7 RID: 15527
	private byte[] bytes = new byte[4];

	// Token: 0x04003CA8 RID: 15528
	private float realValue;
}
