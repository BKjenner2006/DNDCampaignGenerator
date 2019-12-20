using System;
using System.Collections.Generic;
using System.Text;

namespace DNDTest
{
    public class Character : IRace, IClass
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public IRace Race { get; set; }
        public string RaceName { get; set; }
        public IClass Class { get; set; }
        public string ClassName { get; set; }
        public int Level { get; set; }
        public int HitPoints { get; set; }
        public int HitDie { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public Character(IRace race, IClass @class)
        {
            Race = race;
            RaceName = race.RaceName;
            Class = @class;
            ClassName = @class.ClassName;
            HitDie = @class.HitDie;
        }
    }

    public interface IRace
    {
        public string RaceName { get; set; }
    }

    public interface IClass
    {
        public string ClassName { get; set; }
        public int HitDie { get; set; }
    }
    #region Races
    public enum RaceList
    {
        Dwarf,
        Elf,
        Halfling,
        Human,
        Dragonborn,
        Gnome,
        Half_Elf,
        Half_Orc,
        Tiefling
    }
    public class Dwarf : IRace
    {
        public string RaceName { get; set; } = "Dwarf";
    }
    public class Elf : IRace
    {
        public string RaceName { get; set; } = "Elf";
    }
    public class Halfling : IRace
    {
        public string RaceName { get; set; } = "Halfling";
    }
    public class Human : IRace
    {
        public string RaceName { get; set; } = "Human";
    }
    public class Dragonborn : IRace
    {
        public string RaceName { get; set; } = "Dragonborn";
    }
    public class Gnome : IRace
    {
        public string RaceName { get; set; } = "Gnome";
    }
    public class HalfElf : IRace
    {
        public string RaceName { get; set; } = "Half-Elf";
    }
    public class HalfOrc : IRace
    {
        public string RaceName { get; set; } = "Half-Orc";
    }
    public class Tiefling : IRace
    {
        public string RaceName { get; set; } = "Tiefling";
    }
    #endregion
    #region Classes
    public enum ClassList
    {
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Paladin,
        Ranger,
        Rogue,
        Sorceror,
        Warlock,
        Wizard
    }
    public class Barbarian : IClass
    {
        public string ClassName { get; set; } = "Barbarian";
        public int HitDie { get; set; } = 12;
    }
    public class Bard : IClass
    {
        public string ClassName { get; set; } = "Bard";
        public int HitDie { get; set; } = 8;
    }
    public class Cleric : IClass
    {
        public string ClassName { get; set; } = "Cleric";
        public int HitDie { get; set; } = 8;
    }
    public class Druid : IClass
    {
        public string ClassName { get; set; } = "Druid";
        public int HitDie { get; set; } = 8;
    }
    public class Fighter : IClass
    {
        public string ClassName { get; set; } = "Fighter";
        public int HitDie { get; set; } = 10;
    }
    public class Monk : IClass
    {
        public string ClassName { get; set; } = "Monk";
        public int HitDie { get; set; } = 8;
    }
    public class Paladin : IClass
    {
        public string ClassName { get; set; } = "Paladin";
        public int HitDie { get; set; } = 10;
    }
    public class Ranger : IClass
    {
        public string ClassName { get; set; } = "Ranger";
        public int HitDie { get; set; } = 10;
    }
    public class Rogue : IClass
    {
        public string ClassName { get; set; } = "Rogue";
        public int HitDie { get; set; } = 8;
    }
    public class Sorceror : IClass
    {
        public string ClassName { get; set; } = "Sorceror";
        public int HitDie { get; set; } = 6;
    }
    public class Warlock : IClass
    {
        public string ClassName { get; set; } = "Warlock";
        public int HitDie { get; set; } = 8;
    }
    public class Wizard : IClass
    {
        public string ClassName { get; set; } = "Wizard";
        public int HitDie { get; set; } = 6;
    }
    #endregion
}
