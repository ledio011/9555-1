using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005C4 RID: 1476
	public class send_escort_info
	{
		// Token: 0x020005C5 RID: 1477
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A94 RID: 10900 RVA: 0x000B2198 File Offset: 0x000B0398
			public request() : base(send_escort_info.request.max_field_count)
			{
			}

			// Token: 0x06002A95 RID: 10901 RVA: 0x000B21A8 File Offset: 0x000B03A8
			public request(byte[] buffer) : base(send_escort_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C2D RID: 3117
			// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000B21C4 File Offset: 0x000B03C4
			// (set) Token: 0x06002A98 RID: 10904 RVA: 0x000B21CC File Offset: 0x000B03CC
			public long npcid
			{
				get
				{
					return this._npcid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._npcid = value;
				}
			}

			// Token: 0x17000C2E RID: 3118
			// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000B21E4 File Offset: 0x000B03E4
			public bool HasNpcid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C2F RID: 3119
			// (get) Token: 0x06002A9A RID: 10906 RVA: 0x000B21F4 File Offset: 0x000B03F4
			// (set) Token: 0x06002A9B RID: 10907 RVA: 0x000B21FC File Offset: 0x000B03FC
			public long lineIndex
			{
				get
				{
					return this._lineIndex;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._lineIndex = value;
				}
			}

			// Token: 0x17000C30 RID: 3120
			// (get) Token: 0x06002A9C RID: 10908 RVA: 0x000B2214 File Offset: 0x000B0414
			public bool HasLineIndex
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C31 RID: 3121
			// (get) Token: 0x06002A9D RID: 10909 RVA: 0x000B2224 File Offset: 0x000B0424
			// (set) Token: 0x06002A9E RID: 10910 RVA: 0x000B222C File Offset: 0x000B042C
			public string mapInfoId
			{
				get
				{
					return this._mapInfoId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._mapInfoId = value;
				}
			}

			// Token: 0x17000C32 RID: 3122
			// (get) Token: 0x06002A9F RID: 10911 RVA: 0x000B2244 File Offset: 0x000B0444
			public bool HasMapInfoId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002AA0 RID: 10912 RVA: 0x000B2254 File Offset: 0x000B0454
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.npcid = this.deserialize.read_integer();
						break;
					case 1:
						this.lineIndex = this.deserialize.read_integer();
						break;
					case 2:
						this.mapInfoId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002AA1 RID: 10913 RVA: 0x000B22E8 File Offset: 0x000B04E8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.npcid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.lineIndex, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.mapInfoId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E19 RID: 7705
			private static int max_field_count = 3;

			// Token: 0x04001E1A RID: 7706
			private long _npcid;

			// Token: 0x04001E1B RID: 7707
			private long _lineIndex;

			// Token: 0x04001E1C RID: 7708
			private string _mapInfoId;
		}
	}
}
