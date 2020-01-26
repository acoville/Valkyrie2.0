/*================================================================
 * 
 * Valkyrie 2.0 
 * Menu Page View Model
 * Level Loader class
 * 
 * this class has the single responsibility of locating 
 * map files matching the ones in the campaign manifest
 * and loading them into memory. 
 * 
 * The level object (the resulting output of this loader) then
 * has to get to the Menu Page's GamePage object. 
 * The GamePage View Model has a public CurrentLevel property, 
 * brilliant.
 * 
 * =============================================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Valkyrie.GL;
using System.Reflection;

namespace Valkyrie.App.ViewModel
{
    public class LevelLoader
    {
        internal XmlDocument manifest_;
        internal List<String> levelNames_;
        internal String currentLevel_;

        //========================================================================

        /*---------------------------------------
         * 
         * Consructor Attempts to open a 
         * manifest of levels included in the
         * build
         * 
         * -------------------------------------*/

        public LevelLoader()
        {
            // attempt to open manifest
        }

        //=========================================================================

        /*-----------------------------------
         * 
         * Helper Function to load the level
         * manifest
         * 
         * --------------------------------*/
        internal void LoadManifest()
        {
            string content;

            var assembly = Assembly.GetExecutingAssembly();
            var fileName = "Valkyrie.App.Model.Maps.LevelManifest.xml";

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                }
            }

            manifest_.LoadXml(content);
        }

        //======================================================================

        /*

        public Level LoadNextLevel()
        {

        }

        //=======================================================================

        public Level LoadSavedState(string SaveStateName)
        {

        }
         */
    }
}
