using ShipCrudApp.Helpers;
using ShipCrudApp.Helpers.ValidationRules;
using ShipCrudApp.Models;
using ShipCrudApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShipCrudApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DataService dataService;
        private ObservableCollection<Ship> allShips;
        private string newShipName;
        private string newShipCode;
        private double newShipWidth;
        private double newShipLength;
        private RelayCommand cancelCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;


        public MainViewModel() : base()
        {
            dataService = new DataService();
            cancelCommand = new RelayCommand(param => ResetSave(), null);
            saveCommand = new RelayCommand(param => SaveNewShip(), param => CanSaveNewShip());
            deleteCommand = new RelayCommand(param => DeleteShip((int)param), null);
            this.ConfigureValidationRules();
        }

        public void Initialize()
        {
            this.RefreshAllShips();
        }

        #region Properties

        public string NewShipName
        {
            get { return newShipName; }
            set
            {
                ValidateProperty(value);
                newShipName = value;
                OnPropertyChanged();

            }
        }

        public string NewShipCode
        {
            get { return newShipCode; }
            set
            {
                ValidateProperty(value);
                newShipCode = value;
                OnPropertyChanged();
            }
        }

        public double NewShipLength
        {
            get { return newShipLength; }
            set
            {
                ValidateProperty(value);
                newShipLength = value;
                OnPropertyChanged();
            }
        }

        public double NewShipWidth
        {
            get { return newShipWidth; }
            set
            {
                ValidateProperty(value);
                newShipWidth = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Ship> AllShips
        {
            get { return allShips; }
            set
            {
                allShips = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
        }

        private void SaveNewShip()
        {
            this.dataService.AddNewShip(newShipName, newShipCode, newShipLength, newShipWidth);
            // display message box
            this.RefreshAllShips();
            this.ResetSave();
        }

        private bool CanSaveNewShip()
        {
            return !this.HasErrors;
        }

        private void ResetSave()
        {
            this.NewShipName = string.Empty;
            this.NewShipCode = string.Empty;
            this.NewShipLength = 0;
            this.NewShipWidth = 0;
        }

        private void DeleteShip(int shipId)
        {
            this.dataService.DeleteShip(shipId);
            this.RefreshAllShips();
        }

        #endregion

        #region Methods

        private void RefreshAllShips()
        {
            AllShips = new ObservableCollection<Ship>(dataService.GetAllShips());
        }

        private void ConfigureValidationRules()
        {
            this.ValidationRules.Add(nameof(this.NewShipName),
               new List<ValidationRule>() { new UserInputValidationRule() });
            this.ValidationRules.Add(nameof(this.NewShipCode),
                new List<ValidationRule>() { new UserInputValidationRule(), new CodeInputValidationRule() });
            this.ValidationRules.Add(nameof(this.NewShipLength),
                new List<ValidationRule>() { new NumberValidationRule() });
            this.ValidationRules.Add(nameof(this.NewShipWidth),
                new List<ValidationRule>() { new NumberValidationRule() });
        }
        #endregion
    }
}
