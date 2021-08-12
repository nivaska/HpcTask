//using ShipCrudApp.Helpers;
//using ShipCrudApp.Models;
//using ShipCrudApp.Services;
//using System.Collections.ObjectModel;

//namespace ShipCrudApp.ViewModels
//{
//    public class AllShipsViewModel : ViewModelBase
//    {
//        private ObservableCollection<Ship> ships;
//        private readonly DataService dataService;

//        public AllShipsViewModel()
//        {
//            dataService = new DataService();
//        }

//        public void Initialize()
//        {
//            ships = new ObservableCollection<Ship>(dataService.GetAllShips());
//        }

//        public ObservableCollection<Ship> Ships
//        {
//            get { return ships; }
//            set
//            {
//                ships = value;
//                OnPropertyChanged();
//            }
//        }
//    }
//}
