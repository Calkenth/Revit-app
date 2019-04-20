using System;
using System.Reflection;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace FamilyParamTransfer
{
    class RibbonTab : IExternalApplication
    {
        static void AddRibbonPanel(UIControlledApplication application)
        {
            String tabName = "Useful things";
            application.CreateRibbonTab(tabName);

            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Tools");

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData PB1Data = new PushButtonData("Button", "Create and Set params", thisAssemblyPath, "FamilyParamTransfer.CreateSetParam");

            PushButton PB1 = ribbonPanel.AddItem(PB1Data) as PushButton;
            PB1.ToolTip = "Just click it";
            //BitmapImage PB1image = new BitmapImage(new Uri("pack://application:,,,/FamilyParamTranfer;component/Resources/tool.png.ico"));
            BitmapImage PB1image = new BitmapImage(new Uri(@"C:\Users\Plucio\source\repos\FamilyParamTransfer\FamilyParamTransfer\Resources\tool.png.ico"));
            PB1.LargeImage = PB1image;

            PushButtonData PB2Data = new PushButtonData("Button2", "Read and Save params", thisAssemblyPath, "FamilyParamTransfer.ReadSaveParam");

            PushButton PB2 = ribbonPanel.AddItem(PB2Data) as PushButton;
            PB2.ToolTip = "Just click it";
            //BitmapImage PB1image = new BitmapImage(new Uri("pack://application:,,,/FamilyParamTranfer;component/Resources/tool.png.ico"));
            BitmapImage PB2image = new BitmapImage(new Uri(@"C:\Users\Plucio\source\repos\FamilyParamTransfer\FamilyParamTransfer\Resources\tree.png.ico"));
            PB2.LargeImage = PB2image;


        }
        public Result OnShutdown(UIControlledApplication application)
        {
            // do nothing
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            // call our method that will load up our toolbar
            AddRibbonPanel(application);
            return Result.Succeeded;
        }
    }
    
}
