using System;
using System.Collections.Generic;
using System.Text;
using Valkyrie.Graphics;
using Valkyrie.GL;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel
    {
        //============================================================

        /*--------------------------------------
         * 
         * We do quite a lot of work with the 
         * Screen and Sprite classes here
         * 
         * ------------------------------------*/

        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get
            {
                return deviceScreen_;
            }
        }

        //============================================================

        /*-----------------------------------
         * 
         * Navigation Bar becomes visible 
         * when pause button is pressed
         * 
         * ---------------------------------*/

        internal bool displayNavigationBar_ = false;
        public bool DisplayNavigationBar
        {
            get
            {
                return displayNavigationBar_;
            }
            set
            {
                displayNavigationBar_ = value;
            }
        }

        //===========================================================

        /*------------------------------------
         * 
         * Constructor
         * 
         * ----------------------------------*/
        public GamePageViewModel()
        {
            deviceScreen_ = new Screen();
        }
    }
}
