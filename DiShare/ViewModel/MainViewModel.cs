using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Logic.Max2018Detector;
using DiShare.Logic.ScriptGenerators;
using DiShare.OS.Registry;
using GalaSoft.MvvmLight;

namespace DiShare.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public string Title { get; set; }
        public string maxVersionStatus { get; set; }
        /*private IScriptGenerator scriptGenerator { get; set; }
        
        private FrameworkElement _draggedElement;
        private TryResult<string> _scriptPath;*/


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                
            }
            else
            {
                // Code runs "for real"
            }

            Title = "VER 1.0 alpha";

            // Detecting Bad Max Version
            MaxBadVersionDetector maxBadVersionDetector = new MaxBadVersionDetector(new RegistryProvider());
            if (maxBadVersionDetector.Detect().Value)
            {
                maxVersionStatus = "Version of 3ds Max is Bad";
            }
            else
            {
                maxVersionStatus = "Version of  3ds Max is Good, and under 2018 (3dsMax 2019-2021)";
            }


            /*//ModelScript
            scriptGenerator = new ScriptGenerator(); 
            ModelItem modelToLoad = new ModelItem();
            modelToLoad.Category = "Model.ms";
            modelToLoad.Path = "W:\\Projects\\workup\\DiShare\\DiShare\\bin\\Debug\\TestDB\\ModelTest1";


            _scriptPath = scriptGenerator.Generate(modelToLoad);*/

            



        }

    /*    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image) sender;

            string sourceFile = _scriptPath.Value;
            DataObject dataObject = new DataObject();
            dataObject.SetFileDropList(new StringCollection()
            {
                sourceFile
            });

            int num = (int)DragDrop.DoDragDrop((DependencyObject)this._draggedElement, (object)dataObject, DragDropEffects.Copy);

        }*/
    }
}