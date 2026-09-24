using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003F6 RID: 1014
	public class hit_action
	{
		// Token: 0x020003F7 RID: 1015
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F69 RID: 8041 RVA: 0x0009C6B8 File Offset: 0x0009A8B8
			public request() : base(hit_action.request.max_field_count)
			{
			}

			// Token: 0x06001F6A RID: 8042 RVA: 0x0009C6C8 File Offset: 0x0009A8C8
			public request(byte[] buffer) : base(hit_action.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0009C6E4 File Offset: 0x0009A8E4
			// (set) Token: 0x06001F6D RID: 8045 RVA: 0x0009C6EC File Offset: 0x0009A8EC
			public long targetid
			{
				get
				{
					return this._targetid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._targetid = value;
				}
			}

			// Token: 0x1700088A RID: 2186
			// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0009C704 File Offset: 0x0009A904
			public bool HasTargetid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700088B RID: 2187
			// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0009C714 File Offset: 0x0009A914
			// (set) Token: 0x06001F70 RID: 8048 RVA: 0x0009C71C File Offset: 0x0009A91C
			public long senderId
			{
				get
				{
					return this._senderId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._senderId = value;
				}
			}

			// Token: 0x1700088C RID: 2188
			// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0009C734 File Offset: 0x0009A934
			public bool HasSenderId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0009C744 File Offset: 0x0009A944
			// (set) Token: 0x06001F73 RID: 8051 RVA: 0x0009C74C File Offset: 0x0009A94C
			public string effinfoId
			{
				get
				{
					return this._effinfoId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._effinfoId = value;
				}
			}

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0009C764 File Offset: 0x0009A964
			public bool HasEffinfoId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001F75 RID: 8053 RVA: 0x0009C774 File Offset: 0x0009A974
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.targetid = this.deserialize.read_integer();
						break;
					case 1:
						this.senderId = this.deserialize.read_integer();
						break;
					case 2:
						this.effinfoId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001F76 RID: 8054 RVA: 0x0009C808 File Offset: 0x0009AA08
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.targetid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.senderId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.effinfoId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B30 RID: 6960
			private static int max_field_count = 3;

			// Token: 0x04001B31 RID: 6961
			private long _targetid;

			// Token: 0x04001B32 RID: 6962
			private long _senderId;

			// Token: 0x04001B33 RID: 6963
			private string _effinfoId;
		}
	}
}
