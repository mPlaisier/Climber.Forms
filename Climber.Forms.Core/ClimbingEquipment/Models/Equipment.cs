using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class Equipment
    {
        #region Properties

        public int Id { get; set; }

        public DateTime DatePurchase { get; set; }

        public string LblDate => DatePurchase.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public Action ActionClicked { get; set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        public Equipment(DbEquipment equipment)
        {
            Id = equipment.Id;

            DatePurchase = equipment.DatePurchase;
            Description = equipment.Description;

            Price = equipment.Price;
            IsActive = equipment.IsActive;
        }

        public Equipment(DateTime datePurchase, string description, decimal price, bool isActive)
        {
            DatePurchase = datePurchase;
            Description = description;
            Price = price;
            IsActive = isActive;
        }

        #endregion

        #region Static

        public static explicit operator DbEquipment(Equipment equipment)
        {
            var dbEquipment = new DbEquipment(equipment);
            return dbEquipment;
        }

        #endregion
    }
}
