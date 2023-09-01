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

public class SumVar1Var2 : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void Sum()
    {
        var addend1 = (Int32)Project.Current.GetVariable("Model/Variable1").Value;
        var addend2 = (Int32)Project.Current.GetVariable("Model/Variable2").Value;
        Project.Current.GetVariable("Model/Variable3").Value = addend1 + addend2;
    }

}
