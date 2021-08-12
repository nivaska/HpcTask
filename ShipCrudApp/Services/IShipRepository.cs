using ShipCrudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipCrudApp.Services
{
    interface IShipRepository
    {
        void AddShip(string name, string code, double length, double width);
        Ship GetShipById(int id);
        List<Ship> GetAllShips();
        void UpdateShip(int shipId, string name, string code, double length, double width);
        void DeleteShip(int shipId);
    }
}
