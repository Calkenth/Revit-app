using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

namespace FamilyParamTransfer
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class ReadSaveParam : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            DocumentSet Doc = app.Documents;            
            Document doc = uidoc.Document;
            var selection = uidoc.Selection.PickObject(ObjectType.Element);
            Element elem = doc.GetElement(selection);
            
            string parName = string.Empty;
            string parValue = string.Empty;
            string parameterID = string.Empty;
            string typeName = string.Empty;
            bool isIstance;

            BuiltInParameterGroup parBuiltInGroup;
            StorageType paramStorageType;
            ParameterType parType;

            ElementClassFilter elementClassFilter = new ElementClassFilter(typeof(Family));
            
            FamilyManager manager = doc.FamilyManager;
            TaskDialog.Show("test", manager.Types.ToString());

            /*FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> elems = collector.OfClass(typeof(FamilyInstance)).ToElements();
            Element el1 = elems.Where(item => item.Name = 1);
            string name3 = elems.First().Name;
            string name4 = elems.Last().Name;
            TaskDialog.Show("test", name3 + "0" + name4);
            
            ElementId paramId;
            List<FamilyInstance> allFamilies = new List<FamilyInstance>();*/

            TaskDialog.Show("test", doc.Title);
            TaskDialog.Show("family test", elem.Name);
            
            /*string name1 = allFamilies.First().Title.ToString();
            string name2 = allFamilies.Last().Title.ToString();
            TaskDialog.Show("test", name1 + " cos " + name2);
            Dictionary<string, FamilyParameter> paramDict = new Dictionary<string, FamilyParameter>();*/
            
            //Document doc1 = allFamilies.Find(item => item.Title == "1");
            //Document doc1 = allFamilies.First();
            /*foreach (Document doc1 in allFamilies)
            {
                TaskDialog.Show("thing", doc1.Title);

                FamilyManager FM = doc1.FamilyManager;
                FamilyParameterSet familyParameterSet = FM.Parameters;
                FamilyTypeSet familyTypeSet = FM.Types;
                foreach (FamilyParameter P in familyParameterSet)
                {
                    parName = P.Definition.Name;
                    paramDict.Add(parName, P);
                }
                List<string> paramKeys = new List<string>(paramDict.Keys);
                paramKeys.Sort();

                foreach (FamilyType familyType in familyTypeSet)
                {
                    string TName = familyType.Name;

                    foreach (string key in paramKeys)
                    {
                        FamilyParameter familyParameter = paramDict[key];
                        if (familyType.HasValue(familyParameter))
                        {
                            isIstance = familyParameter.IsInstance;
                            paramStorageType = FamilyParamStorageType(familyParameter);
                            parType = familyParameter.Definition.ParameterType;
                            parBuiltInGroup = familyParameter.Definition.ParameterGroup;
                          /*  if (paramStorageType == StorageType.ElementId)
                            {
                                paramId = FamilyParamElementId(familyType, familyParameter);
                                parameterID = paramId.ToString();
                            }
                            else if (paramStorageType == StorageType.String || paramStorageType == StorageType.Double || paramStorageType == StorageType.Integer)
                            {
                                parValue = FamilyParamValueString(familyType, familyParameter, doc1);
                                if (parType == ParameterType.YesNo)
                                {
                                    bool YesNo = FamilyParamYesNoType(familyType, familyParameter);
                                    if (YesNo == true) { parValue = "1"; }
                                    else { parValue = "0"; }
                                }
                            }*/
                            //TaskDialog.Show("things", key + " = " + parValue + " storage type = " + paramStorageType + " id value = " + parameterID);
                            /*Document doc2 = allFamilies.Find(item => item.Title == "2");
                            {
                                FamilyManager FM2 = doc2.FamilyManager;
                                Transaction t = new Transaction(doc2, "Creating params");
                                t.Start();
                                if (FM2.CurrentType.Name != TName)
                                {
                                    FM2.NewType(TName);
                                    FamilyParameter familyParameter2 = FM2.AddParameter(key, parBuiltInGroup, parType, isIstance);
                                    if(paramStorageType == StorageType.String) { FM2.Set(familyParameter2, parValue); }
                                    else if (paramStorageType == StorageType.Double) { FM2.Set(familyParameter2, Convert.ToDouble(parValue)); }
                                    else if (paramStorageType == StorageType.Integer) { FM2.Set(familyParameter2, Convert.ToInt16(parValue)); }
                                    else if (paramStorageType == StorageType.ElementId) { FM2.Set(familyParameter2, paramId); }
                                }
                                else
                                {
                                    FamilyParameter familyParameter2 = FM2.AddParameter(key, parBuiltInGroup, parType, isIstance);
                                    if (paramStorageType == StorageType.String) { FM2.Set(familyParameter2, parValue); }
                                    else if (paramStorageType == StorageType.Double) { FM2.Set(familyParameter2, Convert.ToDouble(parValue)); }
                                    else if (paramStorageType == StorageType.Integer) { FM2.Set(familyParameter2, Convert.ToInt16(parValue)); }
                                    else if (paramStorageType == StorageType.ElementId) { FM2.Set(familyParameter2, paramId); }
                                }
                                t.Commit();
                            }
                        }
                    }
                }
            }*/
            return Result.Succeeded;
        }
        public static StorageType FamilyParamStorageType(FamilyParameter fp)
        {
            StorageType storageType = fp.StorageType;
            return storageType;
        }
        public static ElementId FamilyParamElementId(FamilyType t, FamilyParameter fp)
        {
            ElementId id = t.AsElementId(fp);
            return id;
        }
        public static string FamilyParamValueString(FamilyType t, FamilyParameter fp, Document doc)
        {
            string value = t.AsValueString(fp);
            switch (fp.StorageType)
            {
                case StorageType.Double:
                    value = t.AsValueString(fp);
                    break;

               /* case StorageType.ElementId:
                    ElementId id = t.AsElementId(fp);
                    Element e = doc.GetElement(id);
                    value = id.IntegerValue.ToString() + " (" + e.Name + ")";
                    break;*/

                case StorageType.Integer:
                    value = t.AsInteger(fp).ToString();
                    break;

                case StorageType.String:
                    value = t.AsString(fp);
                    break;
            }
            return value;
        }
        public static bool FamilyParamYesNoType(FamilyType t,FamilyParameter fp)
        {
            bool value;
            if (t.AsInteger(fp) == 0)
            {
                value = false;
            }
            else
            {
                value = true;
            }
            return value;
        }
    }
    
}
