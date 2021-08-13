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
    enum ShipFormMode
    {
        NewShip,
        EditShip
    }

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
        private RelayCommand editCommand;
        private RelayCommand newCommand;
        private RelayCommand refreshCommand;
        private ShipFormMode formMode;
        private int shipEditId;

        public MainViewModel() : base()
        {
            dataService = new DataService();
            this.ConfigureCommands();
            this.ConfigureValidationRules();
            formMode = ShipFormMode.NewShip;
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

        public string ShipFormTitle
        {
            get {
                return this.formMode == ShipFormMode.NewShip ?
                    "New Ship" :
                    "Edit Ship";
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

        public ICommand EditCommand
        {
            get
            {
                return editCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                return newCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return refreshCommand;
            }
        }

        private void SaveShip()
        {
            if (this.formMode == ShipFormMode.NewShip)
            {
                this.dataService.AddNewShip(newShipName, newShipCode, newShipLength, newShipWidth);
            } else
            {
                this.dataService.UpdateShip(shipEditId, newShipName, newShipCode, newShipLength, newShipWidth);
            }

            // display message box
            this.RefreshAllShips();
            this.ResetSave();
        }

        private bool CanSaveShip()
        {
            return !string.IsNullOrEmpty(this.NewShipName)
                && !string.IsNullOrEmpty(this.NewShipCode)
                && !this.HasErrors;
        }

        private void ResetSave()
        {
            this.SetFormMode(ShipFormMode.NewShip);
            this.NewShipName = string.Empty;
            this.NewShipCode = string.Empty;
            this.NewShipLength = 0;
            this.NewShipWidth = 0;
            this.ClearAllErrors();
        }

        private void DeleteShip(int shipId)
        {
            this.dataService.DeleteShip(shipId);
            this.RefreshAllShips();
        }

        private void EditShip(int shipId)
        {
            this.SetFormMode(ShipFormMode.EditShip);
            shipEditId = shipId;
            var shipToEdit = allShips.FirstOrDefault(x => x.Id == shipId);
            this.NewShipName = shipToEdit?.Name;
            this.NewShipCode = shipToEdit?.Code;
            this.NewShipLength = shipToEdit?.Length ?? 0;
            this.NewShipWidth = shipToEdit?.Width ?? 0;
        }

        private void AddNewShip()
        {
            this.ResetSave();
        }

        private void RefreshAllShips()
        {
            AllShips = new ObservableCollection<Ship>(dataService.GetAllShips());
        }

        #endregion

        #region Methods

        private void ConfigureCommands()
        {
            cancelCommand = new RelayCommand(param => ResetSave(), null);
            saveCommand = new RelayCommand(param => SaveShip(), param => CanSaveShip());
            deleteCommand = new RelayCommand(param => DeleteShip((int)param), null);
            editCommand = new RelayCommand(param => EditShip((int)param), null);
            newCommand = new RelayCommand(param => AddNewShip(), null);
            refreshCommand = new RelayCommand(param => RefreshAllShips(), null);
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

        private void SetFormMode(ShipFormMode mode)
        {
            this.formMode = mode;
            OnPropertyChanged(nameof(ShipFormTitle));
        }
        #endregion
    }
}
