using System;
using System.Collections.Generic;
using System.Threading;

namespace HotelBooking
{
    public interface IBookingManager
    {

        bool IsRoomAvailable(int room, DateTime date);
 
        void AddBooking(string guest, int room, DateTime date);

        IEnumerable<int> getAvailableRooms(DateTime date);
    }

    class BookingManager : IBookingManager
    {
        int[] HotelRooms = {101,102,201,203};   // Define the rooms in your hotel here.
        IDictionary<KeyValuePair<int, DateTime>, int> BookedRooms = new Dictionary<KeyValuePair<int, DateTime>, int>();


        int bookingcount = 0;
        public void AddBooking(string guest, int room, DateTime date)
        {
            KeyValuePair<int, DateTime> roomdata = new KeyValuePair<int, DateTime>(room, date);
            try
            {
                BookedRooms.Add(roomdata, bookingcount++);
                Console.WriteLine("Enjoy Your Stay!");
            } catch
            {
                Console.WriteLine("Room already booked!");
            }
        }

        public IEnumerable<int> getAvailableRooms(DateTime date)
        {
            List<int> availRooms = new List<int>(HotelRooms);

            foreach (var room in BookedRooms)
            {
                if (room.Key.Value != date)
                {
                    availRooms.Add(room.Key.Key);
                }
                else if (room.Key.Value == date)
                {
                    availRooms.Remove(room.Key.Key);
                }
            }

            return availRooms;
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            KeyValuePair<int, DateTime> roomdata = new KeyValuePair<int, DateTime>(room, date);

            if (!BookedRooms.ContainsKey(roomdata))
                return true;
            else
                return false;
        }
    }

    class Program 
    {
        static void Main(string[] args)
        {
            Thread obj = new Thread(Booking);
            obj.Start();

        }

        static void Booking()
        {
            BookingManager bm = new BookingManager();
            var today = new DateTime(2012, 3, 28);
            Console.WriteLine(bm.IsRoomAvailable(101, today));
            bm.AddBooking("Patel", 101, today);
            Console.WriteLine(bm.IsRoomAvailable(101, today));
            var rooms = bm.getAvailableRooms(today);   // this variable contains your available rooms.
            bm.AddBooking("Li", 101, today);
        }

    }
}



