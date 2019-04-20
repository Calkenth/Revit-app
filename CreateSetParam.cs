using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace FamilyParamTransfer
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateSetParam : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document CurDoc = commandData.Application.ActiveUIDocument.Document;
            FamilyManager FM = CurDoc.FamilyManager;
            Reference reference = commandData.Application.ActiveUIDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            Document Doc = commandData.Application.ActiveUIDocument.Document;
            Element e = Doc.GetElement(reference);

            BuiltInParameterGroup pGroup = BuiltInParameterGroup.PG_CONSTRUCTION;
            string pName = "Depth";
            ParameterType pType = ParameterType.Length;
            bool isIstance = false;
            string[] types = { "typ1", "typ2", "typ3", "typ4", "typ5" };

            Transaction t = new Transaction(CurDoc,"doing things");
            t.Start();

            FamilyParameter FP = FM.AddParameter(pName, pGroup, pType, isIstance);
            
           switch (FP.StorageType)
            {
                case StorageType.Double:
                FM.Set(FP, 15.5); // wartość w STOPACH
                    break;
                case StorageType.Integer:
                    FM.Set(FP, 16);
                    break;
                case StorageType.String:
                    FM.Set(FP, "15.5");
                    break;
                case StorageType.ElementId:
                    break;
                case StorageType.None:
                    break;
            }
           /* foreach (string n in types)
            {
                double x = 15.5;
                FM.NewType(n);
                FM.Set(FP, x);
                x += 0.75;
            }*/
            t.Commit();
            return Result.Succeeded;
        }
    }
}
