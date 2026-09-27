using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000340 RID: 832
	public class character_aoi_move : SprotoTypeBase
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x0008D924 File Offset: 0x0008BB24
		public character_aoi_move() : base(character_aoi_move.max_field_count)
		{
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008D934 File Offset: 0x0008BB34
		public character_aoi_move(byte[] buffer) : base(character_aoi_move.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0008D950 File Offset: 0x0008BB50
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x0008D958 File Offset: 0x0008BB58
		public long id
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0008D970 File Offset: 0x0008BB70
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0008D980 File Offset: 0x0008BB80
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x0008D988 File Offset: 0x0008BB88
		public movement movement
		{
			get
			{
				return this._movement;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._movement = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0008D9A0 File Offset: 0x0008BBA0
		public bool HasMovement
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0008D9B0 File Offset: 0x0008BBB0
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x0008D9B8 File Offset: 0x0008BBB8
		public bool walk
		{
			get
			{
				return this._walk;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._walk = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0008D9D0 File Offset: 0x0008BBD0
		public bool HasWalk
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008D9E0 File Offset: 0x0008BBE0
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.movement = this.deserialize.read_obj<movement>();
					break;
				case 2:
					this.walk = this.deserialize.read_boolean();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0008DA74 File Offset: 0x0008BC74
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.movement, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_boolean(this.walk, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x0400192A RID: 6442
		private static int max_field_count = 3;

		// Token: 0x0400192B RID: 6443
		private long _id;

		// Token: 0x0400192C RID: 6444
		private movement _movement;

		// Token: 0x0400192D RID: 6445
		private bool _walk;
	}
}
