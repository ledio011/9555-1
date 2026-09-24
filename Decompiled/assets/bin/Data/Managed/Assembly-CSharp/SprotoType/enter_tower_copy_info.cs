using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000397 RID: 919
	public class enter_tower_copy_info
	{
		// Token: 0x02000398 RID: 920
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B99 RID: 7065 RVA: 0x00094798 File Offset: 0x00092998
			public request() : base(enter_tower_copy_info.request.max_field_count)
			{
			}

			// Token: 0x06001B9A RID: 7066 RVA: 0x000947A8 File Offset: 0x000929A8
			public request(byte[] buffer) : base(enter_tower_copy_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000947C4 File Offset: 0x000929C4
			// (set) Token: 0x06001B9D RID: 7069 RVA: 0x000947CC File Offset: 0x000929CC
			public long floorID
			{
				get
				{
					return this._floorID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._floorID = value;
				}
			}

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000947E4 File Offset: 0x000929E4
			public bool HasFloorID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B9F RID: 7071 RVA: 0x000947F4 File Offset: 0x000929F4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.floorID = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001BA0 RID: 7072 RVA: 0x00094850 File Offset: 0x00092A50
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.floorID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A1B RID: 6683
			private static int max_field_count = 1;

			// Token: 0x04001A1C RID: 6684
			private long _floorID;
		}
	}
}
