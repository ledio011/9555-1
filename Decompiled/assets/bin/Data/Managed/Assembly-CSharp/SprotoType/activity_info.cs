using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002E7 RID: 743
	public class activity_info : SprotoTypeBase
	{
		// Token: 0x060014D9 RID: 5337 RVA: 0x00086738 File Offset: 0x00084938
		public activity_info() : base(activity_info.max_field_count)
		{
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00086748 File Offset: 0x00084948
		public activity_info(byte[] buffer) : base(activity_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00086768 File Offset: 0x00084968
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x00086770 File Offset: 0x00084970
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._ID = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00086788 File Offset: 0x00084988
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00086798 File Offset: 0x00084998
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x000867A0 File Offset: 0x000849A0
		public long CurNum
		{
			get
			{
				return this._CurNum;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._CurNum = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000867B8 File Offset: 0x000849B8
		public bool HasCurNum
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000867C8 File Offset: 0x000849C8
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x000867D0 File Offset: 0x000849D0
		public long Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._Type = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x000867E8 File Offset: 0x000849E8
		public bool HasType
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000867F8 File Offset: 0x000849F8
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00086800 File Offset: 0x00084A00
		public long State
		{
			get
			{
				return this._State;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._State = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00086818 File Offset: 0x00084A18
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00086828 File Offset: 0x00084A28
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00086830 File Offset: 0x00084A30
		public long Parm
		{
			get
			{
				return this._Parm;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._Parm = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00086848 File Offset: 0x00084A48
		public bool HasParm
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00086858 File Offset: 0x00084A58
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00086860 File Offset: 0x00084A60
		public string Parmstr
		{
			get
			{
				return this._Parmstr;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._Parmstr = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00086878 File Offset: 0x00084A78
		public bool HasParmstr
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00086888 File Offset: 0x00084A88
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00086890 File Offset: 0x00084A90
		public long sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._sign = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000868A8 File Offset: 0x00084AA8
		public bool HasSign
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000868B8 File Offset: 0x00084AB8
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x000868C0 File Offset: 0x00084AC0
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._time = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000868D8 File Offset: 0x00084AD8
		public bool HasTime
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000868E8 File Offset: 0x00084AE8
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x000868F0 File Offset: 0x00084AF0
		public long next
		{
			get
			{
				return this._next;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._next = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00086908 File Offset: 0x00084B08
		public bool HasNext
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00086918 File Offset: 0x00084B18
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.ID = this.deserialize.read_string();
					break;
				case 1:
					this.CurNum = this.deserialize.read_integer();
					break;
				case 2:
					this.Type = this.deserialize.read_integer();
					break;
				case 3:
					this.State = this.deserialize.read_integer();
					break;
				case 4:
					this.Parm = this.deserialize.read_integer();
					break;
				case 5:
					this.Parmstr = this.deserialize.read_string();
					break;
				case 6:
					this.sign = this.deserialize.read_integer();
					break;
				case 7:
					this.time = this.deserialize.read_integer();
					break;
				case 8:
					this.next = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00086A48 File Offset: 0x00084C48
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.CurNum, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.Type, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.State, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.Parm, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.Parmstr, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.sign, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.time, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.next, 8);
			}
			return this.serialize.close();
		}

		// Token: 0x04001832 RID: 6194
		private static int max_field_count = 9;

		// Token: 0x04001833 RID: 6195
		private string _ID;

		// Token: 0x04001834 RID: 6196
		private long _CurNum;

		// Token: 0x04001835 RID: 6197
		private long _Type;

		// Token: 0x04001836 RID: 6198
		private long _State;

		// Token: 0x04001837 RID: 6199
		private long _Parm;

		// Token: 0x04001838 RID: 6200
		private string _Parmstr;

		// Token: 0x04001839 RID: 6201
		private long _sign;

		// Token: 0x0400183A RID: 6202
		private long _time;

		// Token: 0x0400183B RID: 6203
		private long _next;
	}
}
