namespace LLDProblems.StandardProblems
{
    public class ParkingLotManager
    {
        Dictionary<Location, ParkingLot> locatioinSpecificParking = new Dictionary<Location, ParkingLot>();
        public void AddParkingLot(int floors, int capacity, Location location)
        {
            locatioinSpecificParking.Add(location, new ParkingLot(floors, capacity, new DefaultSlotStrategy(), new PerHourPricingStrategy()));
        }

        public Ticket ParkVehicle(Color vehicleColor, string registrationNum, Location location, SlotType slotType)
        {
            var parkLot = locatioinSpecificParking[location];
            return parkLot.CheckInVechile(vehicleColor, registrationNum, slotType);
        }

        public void ExitVehicle(Ticket ticket, Location location)
        {
            var parkLot = locatioinSpecificParking[location];
            parkLot.CheckOutVehicle(ticket);
        }
    }

    public class ParkingLot
    {
        Slot[][] slots;
        public readonly ISlotStrategy _slotStrategy;
        public readonly IPricingStrategy _pricingStrategy;

        public ParkingLot(int floors, int capacity, ISlotStrategy slotStrategy, IPricingStrategy pricingStrategy)
        {
            _slotStrategy = slotStrategy;
            _pricingStrategy = pricingStrategy;

            slots = new Slot[floors][];
            for (int i = 0; i < floors; i++)
                slots[i] = new Slot[capacity];

            for (int i = 0; i < capacity; i++)
            {
                slots[0][i] = new Slot() { SlotType = SlotType.FourWheelr };
                slots[1][i] = new Slot() { SlotType = SlotType.TwoWheeler };
            }
                
        }

        public Ticket CheckInVechile(Color color, string regNumber, SlotType slotType)
        {
            Vehicle vehicleObj = null;
            if (slotType == SlotType.TwoWheeler)
                vehicleObj = new TwoWheeler();
            else if (slotType == SlotType.FourWheelr)
                vehicleObj = new FourWheeler();

            vehicleObj.Color = color;
            vehicleObj.RegistrationNumber = regNumber;

            var availableSlot = _slotStrategy.FetchSlot(slots, slotType);
            if (availableSlot is not null)
            {
                availableSlot.EntryTime = DateTime.Now;
                availableSlot.Vehicle = vehicleObj;
                return new Ticket() { AssignedSlot = availableSlot, VechileDetails = vehicleObj, EntryTime = DateTime.Now };
            }

            throw new Exception("Slots not available");
        }
        public bool CheckOutVehicle(Ticket ticket)
        {
            int billToPay = _pricingStrategy.CalculatePrice(ticket);
            ticket.AssignedSlot = null;
            return true;
        }
    }

    public class Slot
    {
        public SlotType SlotType { get; set; } = SlotType.TwoWheeler;
        public Vehicle Vehicle { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    }

    public abstract class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public Color Color { get;set; }
    }

    public class TwoWheeler : Vehicle
    {

    }

    public class FourWheeler : Vehicle
    {

    }

    public enum SlotType
    {
        TwoWheeler,
        FourWheelr
    }

    public enum Color
    {
        Red,
        Blue,
        Yellow,
        Green,
        White,
        Black
    }

    public class Ticket
    {
        public Slot AssignedSlot { get; set; }
        public Vehicle VechileDetails { get; set; }
        public DateTime EntryTime { get; set; }
    }

    public interface ISlotStrategy
    {
        public Slot FetchSlot(Slot[][] allSlots, SlotType slotType);
    }

    public class NearestToGateSlotStrategy : ISlotStrategy
    {
        public Slot FetchSlot(Slot[][] allSlots, SlotType slotType)
        {
            throw new NotImplementedException();
        }
    }

    public class DefaultSlotStrategy : ISlotStrategy
    {
        public Slot FetchSlot(Slot[][] allSlots, SlotType slotType)
        {
            // Check if slot available or not
            for (int i = 0; i < allSlots.Length; i++)
            {
                for (int j = 0; j < allSlots[0].Length; j++)
                {
                    if (allSlots[i][j].SlotType == slotType && allSlots[i][j].Vehicle is null)
                        return allSlots[i][j];
                }
            }

            return null;
        }
    }

    public interface IPricingStrategy
    {
        public int CalculatePrice(Ticket ticket);
    }

    public class PerMinutePricingStrategy : IPricingStrategy
    {
        public int CalculatePrice(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }

    public class PerHourPricingStrategy : IPricingStrategy
    {
        public int CalculatePrice(Ticket ticket)
        {
            int price = 0;
            int perHourPrice = 10;
            var currSlotType = ticket.AssignedSlot.SlotType;
            int hoursDiff = (int)(DateTime.Now - ticket.EntryTime).TotalHours;

            if (currSlotType == SlotType.FourWheelr)
            {
                price += 30;
                perHourPrice = 20;
            }
            else if (currSlotType == SlotType.TwoWheeler)
            {
                price += 10;
                perHourPrice = 5;
            }

            price += perHourPrice * hoursDiff;

            return price;
        }
    }

    public enum Location
    {
        South_Delhi,
        Noida,
        Ghaziabad,
        Meeruth
    }
}
