using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000618 RID: 1560
	public class team : SprotoTypeBase
	{
		// Token: 0x06002D27 RID: 11559 RVA: 0x000B7518 File Offset: 0x000B5718
		public team() : base(team.max_field_count)
		{
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000B7528 File Offset: 0x000B5728
		public team(byte[] buffer) : base(team.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000B7548 File Offset: 0x000B5748
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x000B7550 File Offset: 0x000B5750
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

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000B7568 File Offset: 0x000B5768
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000B7578 File Offset: 0x000B5778
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x000B7580 File Offset: 0x000B5780
		public teammember teamleader
		{
			get
			{
				return this._teamleader;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._teamleader = value;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000B7598 File Offset: 0x000B5798
		public bool HasTeamleader
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000B75A8 File Offset: 0x000B57A8
		// (set) Token: 0x06002D31 RID: 11569 RVA: 0x000B75B0 File Offset: 0x000B57B0
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._count = value;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x000B75C8 File Offset: 0x000B57C8
		public bool HasCount
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000B75D8 File Offset: 0x000B57D8
		// (set) Token: 0x06002D34 RID: 11572 RVA: 0x000B75E0 File Offset: 0x000B57E0
		public long isVerfiy
		{
			get
			{
				return this._isVerfiy;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._isVerfiy = value;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000B75F8 File Offset: 0x000B57F8
		public bool HasIsVerfiy
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000B7608 File Offset: 0x000B5808
		// (set) Token: 0x06002D37 RID: 11575 RVA: 0x000B7610 File Offset: 0x000B5810
		public Dictionary<long, teammember> teammembers
		{
			get
			{
				return this._teammembers;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._teammembers = value;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000B7628 File Offset: 0x000B5828
		public bool HasTeammembers
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000B7638 File Offset: 0x000B5838
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000B7640 File Offset: 0x000B5840
		public string goalId
		{
			get
			{
				return this._goalId;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._goalId = value;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000B7658 File Offset: 0x000B5858
		public bool HasGoalId
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000B7668 File Offset: 0x000B5868
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x000B7670 File Offset: 0x000B5870
		public long minLevel
		{
			get
			{
				return this._minLevel;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._minLevel = value;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000B7688 File Offset: 0x000B5888
		public bool HasMinLevel
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000B7698 File Offset: 0x000B5898
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x000B76A0 File Offset: 0x000B58A0
		public long maxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._maxLevel = value;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000B76B8 File Offset: 0x000B58B8
		public bool HasMaxLevel
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x000B76C8 File Offset: 0x000B58C8
		// (set) Token: 0x06002D43 RID: 11587 RVA: 0x000B76D0 File Offset: 0x000B58D0
		public long recruit
		{
			get
			{
				return this._recruit;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._recruit = value;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000B76E8 File Offset: 0x000B58E8
		public bool HasRecruit
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000B76F8 File Offset: 0x000B58F8
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
					this.teamleader = this.deserialize.read_obj<teammember>();
					break;
				case 2:
					this.count = this.deserialize.read_integer();
					break;
				case 3:
					this.isVerfiy = this.deserialize.read_integer();
					break;
				case 4:
					this.teammembers = this.deserialize.read_map<long, teammember>((teammember v) => v.id);
					break;
				case 5:
					this.goalId = this.deserialize.read_string();
					break;
				case 6:
					this.minLevel = this.deserialize.read_integer();
					break;
				case 7:
					this.maxLevel = this.deserialize.read_integer();
					break;
				case 8:
					this.recruit = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000B7844 File Offset: 0x000B5A44
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.teamleader, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.count, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.isVerfiy, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_obj<long, teammember>(this.teammembers, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.goalId, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.minLevel, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.maxLevel, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.recruit, 8);
			}
			return this.serialize.close();
		}

		// Token: 0x04001ED1 RID: 7889
		private static int max_field_count = 9;

		// Token: 0x04001ED2 RID: 7890
		private long _id;

		// Token: 0x04001ED3 RID: 7891
		private teammember _teamleader;

		// Token: 0x04001ED4 RID: 7892
		private long _count;

		// Token: 0x04001ED5 RID: 7893
		private long _isVerfiy;

		// Token: 0x04001ED6 RID: 7894
		private Dictionary<long, teammember> _teammembers;

		// Token: 0x04001ED7 RID: 7895
		private string _goalId;

		// Token: 0x04001ED8 RID: 7896
		private long _minLevel;

		// Token: 0x04001ED9 RID: 7897
		private long _maxLevel;

		// Token: 0x04001EDA RID: 7898
		private long _recruit;
	}
}
