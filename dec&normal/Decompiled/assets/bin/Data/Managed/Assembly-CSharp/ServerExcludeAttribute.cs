using System;

// Token: 0x020001A5 RID: 421
public class ServerExcludeAttribute : Attribute
{
	// Token: 0x06000FF0 RID: 4080 RVA: 0x00065438 File Offset: 0x00063638
	public ServerExcludeAttribute(string msg)
	{
		this.Msg = msg;
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00065448 File Offset: 0x00063648
	// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00065450 File Offset: 0x00063650
	public string Msg { get; set; }
}
