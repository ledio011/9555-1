using System;

// Token: 0x02000164 RID: 356
public class DailyExpData
{
	// Token: 0x06000F08 RID: 3848 RVA: 0x00061CB8 File Offset: 0x0005FEB8
	public int GetWaveEnemyNum(int index)
	{
		if (this.WaveNums == null)
		{
			string[] array = this.GroupNpcCount.Split(new char[]
			{
				'#'
			});
			this.WaveNums = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.WaveNums[i] = int.Parse(array[i]);
			}
		}
		if (index < this.WaveNums.Length)
		{
			return this.WaveNums[index];
		}
		return this.WaveNums[0];
	}

	// Token: 0x04000DD4 RID: 3540
	public string ID;

	// Token: 0x04000DD5 RID: 3541
	public int WaveCount;

	// Token: 0x04000DD6 RID: 3542
	public int GroupCount;

	// Token: 0x04000DD7 RID: 3543
	public int WaveTime;

	// Token: 0x04000DD8 RID: 3544
	public int GroupTime;

	// Token: 0x04000DD9 RID: 3545
	public string Monsters;

	// Token: 0x04000DDA RID: 3546
	public string GroupNpcCount;

	// Token: 0x04000DDB RID: 3547
	private int[] WaveNums;
}
