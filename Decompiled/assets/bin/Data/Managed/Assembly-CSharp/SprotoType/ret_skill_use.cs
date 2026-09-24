using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000597 RID: 1431
	public class ret_skill_use
	{
		// Token: 0x02000598 RID: 1432
		public class request : SprotoTypeBase
		{
			// Token: 0x06002969 RID: 10601 RVA: 0x000AFC7C File Offset: 0x000ADE7C
			public request() : base(ret_skill_use.request.max_field_count)
			{
			}

			// Token: 0x0600296A RID: 10602 RVA: 0x000AFC8C File Offset: 0x000ADE8C
			public request(byte[] buffer) : base(ret_skill_use.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BC7 RID: 3015
			// (get) Token: 0x0600296C RID: 10604 RVA: 0x000AFCA8 File Offset: 0x000ADEA8
			// (set) Token: 0x0600296D RID: 10605 RVA: 0x000AFCB0 File Offset: 0x000ADEB0
			public long sendderId
			{
				get
				{
					return this._sendderId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._sendderId = value;
				}
			}

			// Token: 0x17000BC8 RID: 3016
			// (get) Token: 0x0600296E RID: 10606 RVA: 0x000AFCC8 File Offset: 0x000ADEC8
			public bool HasSendderId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BC9 RID: 3017
			// (get) Token: 0x0600296F RID: 10607 RVA: 0x000AFCD8 File Offset: 0x000ADED8
			// (set) Token: 0x06002970 RID: 10608 RVA: 0x000AFCE0 File Offset: 0x000ADEE0
			public long targetId
			{
				get
				{
					return this._targetId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._targetId = value;
				}
			}

			// Token: 0x17000BCA RID: 3018
			// (get) Token: 0x06002971 RID: 10609 RVA: 0x000AFCF8 File Offset: 0x000ADEF8
			public bool HasTargetId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000BCB RID: 3019
			// (get) Token: 0x06002972 RID: 10610 RVA: 0x000AFD08 File Offset: 0x000ADF08
			// (set) Token: 0x06002973 RID: 10611 RVA: 0x000AFD10 File Offset: 0x000ADF10
			public string skillId
			{
				get
				{
					return this._skillId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._skillId = value;
				}
			}

			// Token: 0x17000BCC RID: 3020
			// (get) Token: 0x06002974 RID: 10612 RVA: 0x000AFD28 File Offset: 0x000ADF28
			public bool HasSkillId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000BCD RID: 3021
			// (get) Token: 0x06002975 RID: 10613 RVA: 0x000AFD38 File Offset: 0x000ADF38
			// (set) Token: 0x06002976 RID: 10614 RVA: 0x000AFD40 File Offset: 0x000ADF40
			public List<attack_list> attack_list
			{
				get
				{
					return this._attack_list;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._attack_list = value;
				}
			}

			// Token: 0x17000BCE RID: 3022
			// (get) Token: 0x06002977 RID: 10615 RVA: 0x000AFD58 File Offset: 0x000ADF58
			public bool HasAttack_list
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002978 RID: 10616 RVA: 0x000AFD68 File Offset: 0x000ADF68
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.sendderId = this.deserialize.read_integer();
						break;
					case 1:
						this.targetId = this.deserialize.read_integer();
						break;
					case 2:
						this.skillId = this.deserialize.read_string();
						break;
					case 3:
						this.attack_list = this.deserialize.read_obj_list<attack_list>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002979 RID: 10617 RVA: 0x000AFE14 File Offset: 0x000AE014
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.sendderId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.targetId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.skillId, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj<attack_list>(this.attack_list, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DC9 RID: 7625
			private static int max_field_count = 4;

			// Token: 0x04001DCA RID: 7626
			private long _sendderId;

			// Token: 0x04001DCB RID: 7627
			private long _targetId;

			// Token: 0x04001DCC RID: 7628
			private string _skillId;

			// Token: 0x04001DCD RID: 7629
			private List<attack_list> _attack_list;
		}
	}
}
