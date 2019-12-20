using System;
using System.Collections.Generic;
using System.Text;

namespace DNDTest
{
    class WorldMap
    {
        public List<ILocation> MapGrid { get; set; } = new List<ILocation>();

        public WorldMap(uint seed)
        {
            PseudoRandom prng = new PseudoRandom(seed);
            var rowLength = prng.IRandom(3, 6);
            int mapSize = rowLength * rowLength;
            int tmpIndex;
            ILocation tmpLoc, lastLoc = null;

            List<ILocation> tmpRow, lastRow = new List<ILocation>();
            var locTypes = (LocationType[])Enum.GetValues(typeof(LocationType));


            for(int i = 0; i < mapSize; i++)//Generate map first, worry about associating the links later
            {
                tmpIndex = prng.IRandom(0, locTypes.Length - 1);
                tmpLoc = (ILocation)Activator.CreateInstance(Type.GetType("DNDTest." + locTypes[tmpIndex].ToString()));
                MapGrid.Add(tmpLoc);
            }

            //Make sure every map location has a directional link
            for (int row = 0; row < Math.Ceiling((decimal)mapSize / rowLength); row++)
            {
                for (int i = row * rowLength; i < rowLength * (row + 1); i++)
                {
                    //North
                    if (i < rowLength)
                        MapGrid[i].North = MapGrid[rowLength * (rowLength - 1) + i];
                    else
                        MapGrid[i].North = MapGrid[i - rowLength];

                    //South
                    if (i >= rowLength * (rowLength - 1))
                        MapGrid[i].South = MapGrid[i - (rowLength * (rowLength - 1))];
                    else
                        MapGrid[i].South = MapGrid[i + rowLength];

                    //East
                    if ((i + 1) % rowLength == 0) //If evenly divisible, it is end of row, loop to front of row
                        MapGrid[i].East = MapGrid[i - rowLength + 1];
                    else
                        MapGrid[i].East = MapGrid[i + 1];
                    //West
                    if (i % rowLength == 0)
                        MapGrid[i].West = MapGrid[i + rowLength - 1];
                    else
                        MapGrid[i].West = MapGrid[i - 1];
                }
            }

            Console.WriteLine();
        }
    }

    public enum LocationType
    {
        Church,
        Tavern,
        Shop,
        Town,
        Dungeon,
        Farm,
        Forest,
        Desert,
        Swamp,
        Lake,
        Ocean,
        Mountain,
    }

    public interface ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; }
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Building : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; }
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Church : Building
    {
        public Church()
        {
            LocationType = LocationType.Church;
        }
    }

    public class Tavern : Building
    {
        public Tavern()
        {
            LocationType = LocationType.Tavern;
        }
    }

    public class Shop : Building
    {
        public Shop()
        {
            LocationType = LocationType.Shop;
        }
    }

    public class Town : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Town;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Dungeon : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Dungeon;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Farm : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Farm;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Forest : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Forest;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }
    public class Desert : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Desert;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }
    public class Swamp : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Swamp;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Lake : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Lake;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }

    public class Ocean : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Ocean;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }
    public class Mountain : ILocation
    {
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; } = LocationType.Mountain;
        public ILocation North { get; set; }
        public ILocation South { get; set; }
        public ILocation East { get; set; }
        public ILocation West { get; set; }
    }
}
