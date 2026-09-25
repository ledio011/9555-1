using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000393 RID: 915
	public class enter_survive_batttle
	{
		// Token: 0x02000394 RID: 916
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B78 RID: 7032 RVA: 0x00094360 File Offset: 0x00092560
			public request() : base(enter_survive_batttle.request.max_field_count)
			{
			}

			// Token: 0x06001B79 RID: 7033 RVA: 0x00094370 File Offset: 0x00092570
			public request(byte[] buffer) : base(enter_survive_batttle.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0009438C File Offset: 0x0009258C
			// (set) Token: 0x06001B7C RID: 7036 RVA: 0x00094394 File Offset: 0x00092594
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000943AC File Offset: 0x000925AC
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06001B7E RID: 7038 RVA: 0x000943BC File Offset: 0x000925BC
			// (set) Token: 0x06001B7F RID: 7039 RVA: 0x000943C4 File Offset: 0x000925C4
			public long floor
			{
				get
				{
					return this._floor;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._floor = value;
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06001B80 RID: 7040 RVA: 0x000943DC File Offset: 0x000925DC
			public bool HasFloor
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000943EC File Offset: 0x000925EC
			// (set) Token: 0x06001B82 RID: 7042 RVA: 0x000943F4 File Offset: 0x000925F4
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._type = value;
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0009440C File Offset: 0x0009260C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001B84 RID: 7044 RVA: 0x0009441C File Offset: 0x0009261C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.id = this.deserialize.read_string();
						break;
					case 1:
						this.floor = this.deserialize.read_integer();
						break;
					case 2:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001B85 RID: 7045 RVA: 0x000944B0 File Offset: 0x000926B0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.floor, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.type, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A12 RID: 6674
			private static int max_field_count = 3;

			// Token: 0x04001A13 RID: 6675
			private string _id;

			// Token: 0x04001A14 RID: 6676
			private long _floor;

			// Token: 0x04001A15 RID: 6677
			private long _type;
		}
	}
}
