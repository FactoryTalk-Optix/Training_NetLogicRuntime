#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.NetLogic;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.SQLiteStore;
using FTOptix.Store;
#endregion

public class QueryDB_NetLogic : BaseNetLogic
{
    private Store myStore;
    private Table myTable;
    private object[,] resultSet;
    private string[] header;
    
    public override void Start()
    {
        myStore = Project.Current.Get<Store>("DataStores/EmbeddedDatabase1");
        myTable = myStore.Tables.Get<Table>("Table1");
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void Insert(string Lot, string Prod, uint Quantity)
    {
        object[,] rawValues = new object[1, 3];     // insert 1 row with 3 columns
        rawValues[0, 0] = Lot;
        rawValues[0, 1] = Prod;
        rawValues[0, 2] = Quantity;
        string[] columns = new string[3] { "Lot", "Product", "Quantity" };
        myTable.Insert(columns, rawValues);
    }

    [ExportMethod]
    public void Delete(string Lot)
    {
        myStore.Query($"DELETE FROM Table1 WHERE Lot='{Lot}'", out header, out resultSet);
    }

    [ExportMethod]
    public void Update(string Lot, uint Quantity)
    {
        myStore.Query($"UPDATE Table1 SET Quantity='{Quantity}' WHERE Lot='{Lot}'", out header, out resultSet);
    }

    [ExportMethod]
    public void Select(string Lot)
    {
        myStore.Query($"SELECT Product, QUantity FROM Table1 WHERE Lot='{Lot}'", out header, out resultSet);
        for (int i = 0; i < resultSet.GetLength(0); i++)
        {
            Log.Info(LogicObject.BrowseName, $"Product = '{resultSet[i, 0]}' - Quantity = '{resultSet[i, 1]}'");
        }
    }


}
