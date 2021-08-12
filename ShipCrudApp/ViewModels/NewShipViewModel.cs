//using ShipCrudApp.Helpers;
//using ShipCrudApp.Models;
//using ShipCrudApp.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace ShipCrudApp.ViewModels
//{
//    public class NewShipViewModel: ViewModelBase
//    {
//        private Ship newShip;
//        private readonly DataService dataService;

//		private RelayCommand cancelCommand;
//		private RelayCommand saveCommand;

//		public NewShipViewModel()
//        {
//            newShip = new Ship();
//            dataService = new DataService();
//            cancelCommand = new RelayCommand(param => Reset(), null);
//            saveCommand = new RelayCommand(param => SaveNewShip(), null);
//        }

//        public void Initialize()
//        {
//        }

//        public Ship NewShip
//        {
//            get { return newShip; }
//            set
//            {
//                newShip = value;
//                OnPropertyChanged();
//            }
//        }

//		public ICommand CancelCommand
//		{
//			get
//			{
//				return cancelCommand;
//			}
//		}

//		public ICommand SaveCommand
//		{
//			get
//			{
//				return saveCommand;
//			}
//		}

//		private void SaveNewShip()
//        {
//            this.dataService.AddNewShip(newShip);
//            // display message box
//            this.Reset();
//        }

//		private void Reset()
//        {
//            this.NewShip = new Ship();
//        }
//	}
//}
