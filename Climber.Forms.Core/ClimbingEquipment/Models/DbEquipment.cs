using System;
using SQLite;

namespace Climber.Forms.Core
{
    public class DbEquipment : IWithId
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime DatePurchase { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        #endregion

        #region Constructor

        public DbEquipment()
        {
        }

        public DbEquipment(Equipment equipment)
        {
            Id = equipment.Id;

            DatePurchase = equipment.DatePurchase;
            Description = equipment.Description;

            Price = equipment.Price;
            IsActive = equipment.IsActive;
        }

        public DbEquipment(int id, DateTime datePurchase, string description, decimal price)
            : this(id, datePurchase, description, price, true)
        {
        }

        public DbEquipment(int id, DateTime datePurchase, string description, decimal price, bool isActive)
        {
            Id = id;

            DatePurchase = datePurchase;
            Description = description;

            Price = price;
            IsActive = isActive;
        }

        #endregion

        #region Static

        public static explicit operator Equipment(DbEquipment dbEquipment)
        {
            var equipment = new Equipment(dbEquipment);
            return equipment;
        }

        #endregion
    }
}