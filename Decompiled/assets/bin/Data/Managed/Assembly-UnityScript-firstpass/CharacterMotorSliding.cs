using System;

// Token: 0x02000006 RID: 6
[Serializable]
public class CharacterMotorSliding
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002230 File Offset: 0x00000430
	public CharacterMotorSliding()
	{
		this.enabled = true;
		this.slidingSpeed = (float)15;
		this.sidewaysControl = 1f;
		this.speedControl = 0.4f;
	}

	// Token: 0x04000028 RID: 40
	public bool enabled;

	// Token: 0x04000029 RID: 41
	public float slidingSpeed;

	// Token: 0x0400002A RID: 42
	public float sidewaysControl;

	// Token: 0x0400002B RID: 43
	public float speedControl;
}
