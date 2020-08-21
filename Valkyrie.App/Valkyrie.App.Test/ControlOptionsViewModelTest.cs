using NUnit.Framework;
using Valkyrie.App.ViewModel;

namespace Valkyrie.App.Test
{
    public class ControlOptionsViewModelTest
    {
        ControlOptionsViewModel covm_;

        //==============================================================

        [SetUp]
        public void Setup()
        {
            covm_ = new ControlOptionsViewModel();
        }

        //===============================================================

        /*--------------------------------------------
         * 
         * Running on my Windows laptop localhost, 
         * this test should return true.
         * 

        [Test]
        [Category("HardwareDetection")]
        public void KeyboardDetectionTest()
        {
            //bool kbPresent = covm_.KeyboardPresent;
            //Assert.IsTrue(kbPresent);
        }
         * ------------------------------------------*/
    }
}
