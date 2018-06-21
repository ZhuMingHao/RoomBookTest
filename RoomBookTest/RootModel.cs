using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookTest
{
    public class Room
    {
        public string name { get; set; }
        public string id { get; set; }
        public int seats { get; set; }
        public object availableFrom { get; set; }
        public object availableTo { get; set; }
        public List<object> roomAttributes { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public int code { get; set; }
        public DateTime timeFrom { get; set; }
        public DateTime timeTo { get; set; }
        public object note { get; set; }
        public DateTime createdDate { get; set; }
        public Room room { get; set; }
    }
}
