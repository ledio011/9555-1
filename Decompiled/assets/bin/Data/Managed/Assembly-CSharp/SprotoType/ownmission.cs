using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000440 RID: 1088
	public class ownmission : SprotoTypeBase
	{
		// Token: 0x060021E5 RID: 8677 RVA: 0x000A1840 File Offset: 0x0009FA40
		public ownmission() : base(ownmission.max_field_count)
		{
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000A1850 File Offset: 0x0009FA50
		public ownmission(byte[] buffer) : base(ownmission.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000A186C File Offset: 0x0009FA6C
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000A1874 File Offset: 0x0009FA74
		public string missionId
		{
			get
			{
				return this._missionId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._missionId = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000A188C File Offset: 0x0009FA8C
		public bool HasMissionId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000A189C File Offset: 0x0009FA9C
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x000A18A4 File Offset: 0x0009FAA4
		public long missionstate
		{
			get
			{
				return this._missionstate;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._missionstate = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000A18BC File Offset: 0x0009FABC
		public bool HasMissionstate
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000A18CC File Offset: 0x0009FACC
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x000A18D4 File Offset: 0x0009FAD4
		public long missionquality
		{
			get
			{
				return this._missionquality;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._missionquality = value;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000A18EC File Offset: 0x0009FAEC
		public bool HasMissionquality
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000A18FC File Offset: 0x0009FAFC
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x000A1904 File Offset: 0x0009FB04
		public List<long> parm
		{
			get
			{
				return this._parm;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._parm = value;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000A191C File Offset: 0x0009FB1C
		public bool HasParm
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000A192C File Offset: 0x0009FB2C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.missionId = this.deserialize.read_string();
					break;
				case 1:
					this.missionstate = this.deserialize.read_integer();
					break;
				case 2:
					this.missionquality = this.deserialize.read_integer();
					break;
				case 3:
					this.parm = this.deserialize.read_integer_list();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000A19D8 File Offset: 0x0009FBD8
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.missionId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.missionstate, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.missionquality, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.parm, 3);
			}
			return this.serialize.close();
		}

		// Token: 0x04001BDF RID: 7135
		private static int max_field_count = 4;

		// Token: 0x04001BE0 RID: 7136
		private string _missionId;

		// Token: 0x04001BE1 RID: 7137
		private long _missionstate;

		// Token: 0x04001BE2 RID: 7138
		private long _missionquality;

		// Token: 0x04001BE3 RID: 7139
		private List<long> _parm;
	}
}
