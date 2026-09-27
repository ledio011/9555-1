using System;

// Token: 0x0200000B RID: 11
public enum GestureRecognitionState
{
	// Token: 0x04000019 RID: 25
	Ready,
	// Token: 0x0400001A RID: 26
	Started,
	// Token: 0x0400001B RID: 27
	InProgress,
	// Token: 0x0400001C RID: 28
	Failed,
	// Token: 0x0400001D RID: 29
	Ended,
	// Token: 0x0400001E RID: 30
	Recognized = 4,
	// Token: 0x0400001F RID: 31
	FailAndRetry
}
