using ShipCrudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipCrudApp.Services
{
    class MockShipRepository : IShipRepository
    {
        private static readonly List<Ship> shipsDb = new List<Ship>();

        public void AddShip(string name, string code, double length, double width)
        {
            var lastId = shipsDb.Select(s => s.Id).OrderBy(s => s).LastOrDefault();
            shipsDb.Add(new Ship()
            {
                Id = lastId + 1,
                Name = name,
                Code = code,
                Length = length,
                Width = width
            });
        }

        public void DeleteShip(int shipId)
        {
            shipsDb.RemoveAll(s => s.Id == shipId);
        }

        public List<Ship> GetAllShips()
        {
            return shipsDb;
        }

        public Ship GetShipById(int id)
        {
            return shipsDb.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateShip(int shipId, string name, string code, double length, double width)
        {
            var shipToUpdate = shipsDb.FirstOrDefault(s => s.Id == shipId);
            if (shipToUpdate != null)
            {
                shipToUpdate.Name = name;
                shipToUpdate.Code = code;
                shipToUpdate.Length = length;
                shipToUpdate.Width = width;
            }
        }
    }
}
