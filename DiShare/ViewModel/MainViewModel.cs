using System.Web.UI.WebControls;
using DiShare.Logic.Max2018Detector;
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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                Title = "TEST";
            }
            else
            {
                // Code runs "for real"
                Title = "UNDERTEST";
            }

            MaxBadVersionDetector maxBadVersionDetector = new MaxBadVersionDetector(new RegistryProvider());
            if (maxBadVersionDetector.Detect().Value)
            {
                Title = "Version of 3ds Max is Bad";
            }
            else
            {
                Title = "Version of  3ds Max is Good, and under 2018 (3dsMax 2019-2021)";
            }


        }
    }
}