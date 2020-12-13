using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Enums;
using Bakery.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Bakery
{
    public class Controller : IController
    {
        private ICollection<IBakedFood> bakedFoods;
        private ICollection<IDrink> drinks;
        private ICollection<ITable> tables;

        private decimal totalIncome;

        public Controller()
        {
            this.bakedFoods = new List<IBakedFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<ITable>();
        }
        
        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink targetDrink = null;

            if (Enum.TryParse(type, out DrinkType drinkTypeEnum))
            {
                switch (drinkTypeEnum)
                {
                    case DrinkType.Tea:
                        targetDrink = new Tea(name, portion, brand);
                        break;
                    case DrinkType.Water:
                        targetDrink = new Water(name, portion, brand);
                        break;
                }

                this.drinks.Add(targetDrink);

                return string.Format(OutputMessages.DrinkAdded, name, brand);
            }

            return null;
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood currentFood = null;

            if (Enum.TryParse(type, out BakedFoodType bakedFoodEnum))
            {
                switch (bakedFoodEnum)
                {
                    case BakedFoodType.Bread:
                        currentFood = new Bread(name, price);
                        break;
                    case BakedFoodType.Cake:
                        currentFood = new Cake(name, price);
                        break;
                }

                this.bakedFoods.Add(currentFood);

                return string.Format(OutputMessages.FoodAdded, name, type);
            }

            return null;
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable targetTable = null;

            if (Enum.TryParse(type, out TableType tableTypeEnum))
            {
                switch (tableTypeEnum)
                {
                    case TableType.InsideTable:
                        targetTable = new InsideTable(tableNumber, capacity);
                        break;
                    case TableType.OutsideTable:
                        targetTable = new OutsideTable(tableNumber, capacity);
                        break;
                }

                this.tables.Add(targetTable);

                return string.Format(OutputMessages.TableAdded, tableNumber);
            }

            return null;
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var table in this.tables.Where(x => x.IsReserved == false))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {this.totalIncome:f2}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            ITable targetTable = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            decimal tableBill = targetTable.GetBill();

            this.totalIncome += tableBill;

            targetTable.Clear();

            return $"Table: {tableNumber}" + Environment.NewLine + $"Bill: {tableBill:f2}";
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable targetTable = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            IDrink targetDrink = this.drinks.FirstOrDefault(x => x.Name == drinkName && x.Brand == drinkBrand);

            if (targetTable == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            if (targetDrink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            targetTable.OrderDrink(targetDrink);

            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable targetTable = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            IBakedFood targetFood = this.bakedFoods.FirstOrDefault(x => x.Name == foodName);

            if (targetTable == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            if (targetFood == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            targetTable.OrderFood(targetFood);

            return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable targetTable = this.tables.FirstOrDefault(x => x.IsReserved == false && x.Capacity >= numberOfPeople);

            if (targetTable == null)
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }
            else
            {
                targetTable.Reserve(numberOfPeople);

                return string.Format(OutputMessages.TableReserved, targetTable.TableNumber, numberOfPeople);
            }

        }
    }
}
