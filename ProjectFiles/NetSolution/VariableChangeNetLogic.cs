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

public class VariableChangeNetLogic : BaseNetLogic
{
    private IUAVariable addend1;
    private IUAVariable addend2;
    private RemoteVariableSynchronizer variableSynchronizer;

    public override void Start()
    {
        addend1 = Project.Current.GetVariable("Model/Variable1");
        addend2 = Project.Current.GetVariable("Model/Variable2");
        addend1.VariableChange += addend1_VariableChange;
        addend2.VariableChange += addend2_VariableChange;

        //the following three rows are necessary when variables are exchanged with external device (e.g. PLC) and they are not in use
        //because not linked to any object in page
        variableSynchronizer = new RemoteVariableSynchronizer();
        variableSynchronizer.Add(addend1);
        variableSynchronizer.Add(addend2);

    }
    private void addend1_VariableChange(object sender, VariableChangeEventArgs e)
    {
        Project.Current.GetVariable("Model/Variable3").Value = (Int32)e.NewValue + (Int32)addend2.Value;
    }
    private void addend2_VariableChange(object sender, VariableChangeEventArgs e)
    {
        Project.Current.GetVariable("Model/Variable3").Value = (Int32)addend1.Value + (Int32)e.NewValue;
    }
    public override void Stop()
    {
        addend1.VariableChange -= addend1_VariableChange;
        addend2.VariableChange -= addend2_VariableChange;
    }

}
