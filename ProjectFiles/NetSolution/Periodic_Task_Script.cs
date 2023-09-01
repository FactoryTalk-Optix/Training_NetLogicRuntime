#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.SQLiteStore;
using FTOptix.Store;
#endregion

public class Periodic_Task_Script : BaseNetLogic
{
    private PeriodicTask myPeriodicTask;
    private IUAVariable variable1;
    private double dCounter;
    public override void Start()
    {
        variable1 = Project.Current.GetVariable("Model/Variable1");
        myPeriodicTask = new PeriodicTask(IncrementVariable, 250, LogicObject);
        myPeriodicTask.Start();
        dCounter = 0;
    }

    private void IncrementVariable(PeriodicTask task)
    {
        dCounter = dCounter + 0.05;
        variable1.Value = 50 + Math.Sin(dCounter) * 50;
    }
    public override void Stop()
    {
        myPeriodicTask.Dispose();
    }

}
