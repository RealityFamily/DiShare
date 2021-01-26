using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Logic.ScriptGenerators;
using MahApps.Metro.Controls;

namespace DiShare
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private IScriptGenerator scriptGenerator { get; set; }

        private TryResult<string> _scriptPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            //ModelScript
            scriptGenerator = new ScriptGenerator();
            ModelItem modelToLoad = new ModelItem();
            modelToLoad.Category = "Model.ms";
            modelToLoad.Path = "W:\\Projects\\workup\\DiShare\\DiShare\\bin\\Debug\\TestDB\\ModelTest1"; // TODO: Change to relative path


            _scriptPath = scriptGenerator.Generate(modelToLoad);

            Image img = (Image)sender;

            string sourceFile = _scriptPath.Value;
            DataObject dataObject = new DataObject();
            dataObject.SetFileDropList(new StringCollection()
            {
                sourceFile
            });

            int num = (int)DragDrop.DoDragDrop((DependencyObject)img, (object)dataObject, DragDropEffects.Copy);
            

        }
    }
}
