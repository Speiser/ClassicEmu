using Classic.Common;
using Classic.Data;
using System;

namespace Classic.World.Messages
{
    public class SMSG_UPDATE_OBJECT : ServerMessageBase<Opcode>
    {
        private readonly Character character;
        private readonly bool create;

        public SMSG_UPDATE_OBJECT(Character character, bool create = true) : base(Opcode.SMSG_UPDATE_OBJECT)
        {
            this.character = character;
            this.create = create;
        }

        public override byte[] Get()
        {
            // Parts from https://subversion.assembla.com/svn/rift-net-reloaded/src/OpCodes/WS.Handlers.World.vb
            this.Writer
                .WriteUInt32(1) // ObjectCount
                .WriteUInt8((byte)(create ? 3 : 2))
                .WriteUInt64(this.character.ID) // ObjectGuid
                .WriteUInt8(4)  // ObjectType, 4 = Player

                .WriteUInt64(0) // TransportGuid
                .WriteFloat(0) // TransportX
                .WriteFloat(0) // TransportY
                .WriteFloat(0) // TransportZ
                .WriteFloat(0) // TransportW (TransportO)

                .WriteFloat(this.character.Map.X)   // x
                .WriteFloat(this.character.Map.Y)   // y
                .WriteFloat(this.character.Map.Z)   // z
                .WriteFloat(this.character.Map.Orientation) // o
                .WriteFloat(0)         // Pitch
                .WriteUInt32(0x8000000) // MovementFlagMask
                .WriteUInt32(0)         // FallTime
                .WriteFloat(2.5f)       // WalkSpeed
                .WriteFloat(7.0f)       // RunSpeed
                .WriteFloat(4.7222f)    // SwimSpeed
                .WriteFloat(3.14f)      // TurnSpeed
                .WriteUInt32(1)         // Flags, 1 - SelfUpdate
                .WriteUInt32(1)         // AttackCycle
                .WriteUInt32(0)         // TimerId
                .WriteUInt64(0)         // VictimGuid

                // FillInPartialObjectData
                .WriteUInt8(0x14);

            // UpdateMaskBlocks, 20
            for (var i = 0; i < 20; i++)
                this.Writer.WriteUInt32(0xFFFFFFFFU);

            this.Writer
                // ObjectFields
                .WriteUInt64(this.character.ID)
                .WriteUInt32(0x19)
                // UpdateType, 0x19 - Player (Player + Unit + Object)
                .WriteUInt32(0)
                .WriteFloat(1)
                .WriteUInt32(0);

            // UnitFields
            for (var i = 0; i < 16; i++)
                this.Writer.WriteUInt32(0);

            // TODO: As Character Properties
            var Health = (uint)100;
            var Mana = (uint)100;
            var Rage = (uint)100;
            var Focus = (uint)100;
            var Energy = (uint)100;
            var Strength = (uint)100;
            var Agility = (uint)100;
            var Stamina = (uint)100;
            var Intellect = (uint)100;
            var Spirit = (uint)100;

            this.Writer
                .WriteUInt32(Health)
                .WriteUInt32(Mana)
                .WriteUInt32(Rage)
                .WriteUInt32(Focus)
                .WriteUInt32(Energy)

                // Max values...
                .WriteUInt32(Health)
                .WriteUInt32(Mana)
                .WriteUInt32(Rage)
                .WriteUInt32(Focus)
                .WriteUInt32(Energy)
                .WriteUInt32(this.character.Level)
                .WriteUInt32(5)
                .WriteUInt8((byte)this.character.Race)
                .WriteUInt8((byte)this.character.Class)
                .WriteUInt8((byte)this.character.Gender)
                .WriteUInt8(1)

                .WriteUInt32(Strength)
                .WriteUInt32(Agility)
                .WriteUInt32(Stamina)
                .WriteUInt32(Intellect)
                .WriteUInt32(Spirit)

                // BastStats, copy ...
                .WriteUInt32(Strength)
                .WriteUInt32(Agility)
                .WriteUInt32(Stamina)
                .WriteUInt32(Intellect)
                .WriteUInt32(Spirit);

            for (var i = 0; i < 10; i++)
                this.Writer.WriteUInt32(0);

            // Money
            this.Writer.WriteUInt32(0x00); // char.Money

            for (var i = 0; i < 56; i++)
                this.Writer.WriteUInt32(0);
            for (var i = 0; i < 39; i++)
                this.Writer.WriteUInt32(0);

            // DisplayId
            this.Writer.WriteUInt32(0x31); // Human, Male
            // Select Case this.character.Race
            //     Case GlobalConstants.Races.RACE_HUMAN
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x31, 0x32)))
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_ORC
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x33, 0x34)))
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_DWARF
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x35, 0x36)))
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_NIGHT_ELF
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x37, 0x38)))
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_UNDEAD
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x39, 0x40))) 'bug: female undead cube model
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_TAUREN
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x3B, 0x3C)))
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_GNOME
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x61B, 0x61C)))
            //         Exit Select
            //     Case GlobalConstants.Races.RACE_TROLL
            //         .WriteUInt32(CUInt(If(this.character.Gender = 0, 0x5C6, 0x5C7)))
            //         Exit Select
            // End Select

            for (var i = 0; i < 32; i++)
                this.Writer.WriteUInt32(0);
            // PlayerFields
            for (var i = 0; i < 46; i++)
                this.Writer.WriteUInt32(0);
            for (var i = 0; i < 32; i++)
                this.Writer.WriteUInt32(0);
            for (var i = 0; i < 48; i++)
                this.Writer.WriteUInt32(0);
            for (var i = 0; i < 12; i++)
                this.Writer.WriteUInt32(0);

            this.Writer
                .WriteUInt32(0)
                .WriteUInt32(0)

                .WriteUInt32(0)
                .WriteUInt32(0)

                .WriteUInt32(0)
                .WriteUInt32(0)

                // InventarSlots
                .WriteUInt32(14)

                .WriteUInt32(0)
                .WriteUInt32(0)

                // PLAYER_BYTES (Skin, Face, HairStyle, HairColor)
                .WriteUInt8(this.character.Skin)
                .WriteUInt8(this.character.Face)
                .WriteUInt8(this.character.HairStyle)
                .WriteUInt8(this.character.HairColor)

                // XP
                .WriteUInt32(0)

                // NextLevel
                .WriteUInt32(1);

            // SkillInfo
            for (var i = 0; i < 192; i++)
                this.Writer.WriteUInt32(0);

            // PLAYER_BYTES_2 (FacialHair, Unknown, BankBagSlotCount, RestState)
            this.Writer
                .WriteUInt8(this.character.FacialHair)
                .WriteUInt8(0)
                .WriteUInt8(0)
                .WriteUInt8(1);

            // QuestInfo
            for (var i = 0; i < 96; i++)
                this.Writer.WriteUInt32(0);

            this.Writer
                .WriteUInt32(0)
                .WriteUInt32(0)
                .WriteUInt32(0)
                .WriteUInt32(0)
                .WriteUInt32(0)
                .WriteUInt32(0)
                .WriteUInt32(0)
                .WriteUInt32(0)

                // BaseMana
                .WriteUInt32(20)
                .WriteUInt32(0)

                // unknown
                .WriteUInt8(0)
                .WriteUInt8(0)
                .WriteUInt8(0);

            return this.Writer.Build();
        }
    }
}