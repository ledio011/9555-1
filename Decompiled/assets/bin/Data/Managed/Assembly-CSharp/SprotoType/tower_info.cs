using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000623 RID: 1571
	public class tower_info : SprotoTypeBase
	{
		// Token: 0x06002DBE RID: 11710 RVA: 0x000B8934 File Offset: 0x000B6B34
		public tower_info() : base(tower_info.max_field_count)
		{
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000B8944 File Offset: 0x000B6B44
		public tower_info(byte[] buffer) : base(tower_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000B8960 File Offset: 0x000B6B60
		// (set) Token: 0x06002DC2 RID: 11714 RVA: 0x000B8968 File Offset: 0x000B6B68
		public long floor
		{
			get
			{
				return this._floor;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._floor = value;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000B8980 File Offset: 0x000B6B80
		public bool HasFloor
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000B8990 File Offset: 0x000B6B90
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x000B8998 File Offset: 0x000B6B98
		public long cur_floor
		{
			get
			{
				return this._cur_floor;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._cur_floor = value;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000B89B0 File Offset: 0x000B6BB0
		public bool HasCur_floor
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x000B89C0 File Offset: 0x000B6BC0
		// (set) Token: 0x06002DC8 RID: 11720 RVA: 0x000B89C8 File Offset: 0x000B6BC8
		public long times
		{
			get
			{
				return this._times;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._times = value;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x000B89E0 File Offset: 0x000B6BE0
		public bool HasTimes
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000B89F0 File Offset: 0x000B6BF0
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000B89F8 File Offset: 0x000B6BF8
		public long sum_time
		{
			get
			{
				return this._sum_time;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._sum_time = value;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000B8A10 File Offset: 0x000B6C10
		public bool HasSum_time
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000B8A20 File Offset: 0x000B6C20
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x000B8A28 File Offset: 0x000B6C28
		public long wipe_out_state
		{
			get
			{
				return this._wipe_out_state;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._wipe_out_state = value;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000B8A40 File Offset: 0x000B6C40
		public bool HasWipe_out_state
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B8A50 File Offset: 0x000B6C50
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000B8A58 File Offset: 0x000B6C58
		public long wipe_time
		{
			get
			{
				return this._wipe_time;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._wipe_time = value;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000B8A70 File Offset: 0x000B6C70
		public bool HasWipe_time
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000B8A80 File Offset: 0x000B6C80
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x000B8A88 File Offset: 0x000B6C88
		public long max_floor
		{
			get
			{
				return this._max_floor;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._max_floor = value;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x000B8AA0 File Offset: 0x000B6CA0
		public bool HasMax_floor
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000B8AB0 File Offset: 0x000B6CB0
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.floor = this.deserialize.read_integer();
					break;
				case 1:
					this.cur_floor = this.deserialize.read_integer();
					break;
				case 2:
					this.times = this.deserialize.read_integer();
					break;
				case 3:
					this.sum_time = this.deserialize.read_integer();
					break;
				case 4:
					this.wipe_out_state = this.deserialize.read_integer();
					break;
				case 5:
					this.wipe_time = this.deserialize.read_integer();
					break;
				case 6:
					this.max_floor = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000B8BAC File Offset: 0x000B6DAC
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.floor, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.cur_floor, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.times, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.sum_time, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.wipe_out_state, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.wipe_time, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.max_floor, 6);
			}
			return this.serialize.close();
		}

		// Token: 0x04001EFE RID: 7934
		private static int max_field_count = 7;

		// Token: 0x04001EFF RID: 7935
		private long _floor;

		// Token: 0x04001F00 RID: 7936
		private long _cur_floor;

		// Token: 0x04001F01 RID: 7937
		private long _times;

		// Token: 0x04001F02 RID: 7938
		private long _sum_time;

		// Token: 0x04001F03 RID: 7939
		private long _wipe_out_state;

		// Token: 0x04001F04 RID: 7940
		private long _wipe_time;

		// Token: 0x04001F05 RID: 7941
		private long _max_floor;
	}
}
