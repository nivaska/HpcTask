using ShipCrudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipCrudApp.Services
{
    /// <summary>
    /// DataService provides access to the repositories to the ViewModels
    /// </summary>
    public class DataService
    {
        readonly IShipRepository shipRepository;

        public DataService()
        {
            shipRepository = new MockShipRepository();
        }

        public void AddNewShip(string name, string code, double length, double width)
        {
            shipRepository.AddShip(name, code, length, width);
        }

        public void DeleteShip(int shipId)
        {
            shipRepository.DeleteShip(shipId);
        }

        public void UpdateShip(int shipId, string name, string code, double length, double width)
        {
            shipRepository.UpdateShip(shipId, name, code, length, width);
        }

        public Ship GetShipById(int shipId)
        {
            return shipRepository.GetShipById(shipId);
        }

        public List<Ship> GetAllShips()
        {
            return shipRepository.GetAllShips();
        }
    }
}
