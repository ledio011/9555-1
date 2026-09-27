using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class DelegateDefine
{
	// Token: 0x02000AC9 RID: 2761
	// (Invoke) Token: 0x06004FAD RID: 20397
	public delegate void OneIntParamDelegate(int val);

	// Token: 0x02000ACA RID: 2762
	// (Invoke) Token: 0x06004FB1 RID: 20401
	public delegate void TwoIntParamDelegate(int val, int val2);

	// Token: 0x02000ACB RID: 2763
	// (Invoke) Token: 0x06004FB5 RID: 20405
	public delegate void ThreeParamDelegate(int val, int val2, string val3, bool val4);

	// Token: 0x02000ACC RID: 2764
	// (Invoke) Token: 0x06004FB9 RID: 20409
	public delegate void ThirdIntParamDelegate(int val, int val2, int val3);

	// Token: 0x02000ACD RID: 2765
	// (Invoke) Token: 0x06004FBD RID: 20413
	public delegate void OneLongParamDelegate(long val);

	// Token: 0x02000ACE RID: 2766
	// (Invoke) Token: 0x06004FC1 RID: 20417
	public delegate void NoParamDelegate();

	// Token: 0x02000ACF RID: 2767
	// (Invoke) Token: 0x06004FC5 RID: 20421
	public delegate bool NoParamReturnDelegate();

	// Token: 0x02000AD0 RID: 2768
	// (Invoke) Token: 0x06004FC9 RID: 20425
	public delegate void TwoParamDelegate(int val, bool istrue);

	// Token: 0x02000AD1 RID: 2769
	// (Invoke) Token: 0x06004FCD RID: 20429
	public delegate void OneStringParamDelegate(string val);

	// Token: 0x02000AD2 RID: 2770
	// (Invoke) Token: 0x06004FD1 RID: 20433
	public delegate void StringGameObjectDelegate(string val, GameObject obj);

	// Token: 0x02000AD3 RID: 2771
	// (Invoke) Token: 0x06004FD5 RID: 20437
	public delegate void OneGameItemParamDelegate(GameItem item);

	// Token: 0x02000AD4 RID: 2772
	// (Invoke) Token: 0x06004FD9 RID: 20441
	public delegate void GameObjectDelegate(GameObject obj);
}
