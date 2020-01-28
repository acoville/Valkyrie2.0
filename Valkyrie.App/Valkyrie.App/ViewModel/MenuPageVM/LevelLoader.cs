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
using System.Xml;
using System.Reflection;
using Valkryie.GL;

namespace Valkyrie.App.ViewModel
{
    public class LevelLoader
    {
        internal List<String> levelNames_;
        public List<String> LevelNames => levelNames_;

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

            levelNames_ = new List<String>();

            LoadManifest();

            if(levelNames_.Count > 0)
            {
                currentLevel_ = levelNames_[0];
            }
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
            XmlDocument manifest = new XmlDocument();
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

            manifest.LoadXml(content);

            //------------ we should now be able to populate the level list

            XmlNode root = manifest.DocumentElement;

            XmlNodeList levels = root.SelectNodes("Level"); 
            foreach (XmlNode level in levels)
            {
                levelNames_.Add(level.Attributes[0].Value.ToString());
            }
        }

        //======================================================================

        /*---------------------------------------
         * 
         * Loads a level into memory, 
         * the index references levelList_[index]
         * 
         * -------------------------------------*/

        internal Level LoadLevel(int index)
        {
            // determine which of the level names from the manifest
            // we are fetching

            string mapname = levelNames_[index];

            XmlDocument map = new XmlDocument();
            string content;

            // open the map 

            var assembly = Assembly.GetExecutingAssembly();
            var fileName = "Valkyrie.App.Model.Maps." + mapname;

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                }
            }

            // read map's content

            map.LoadXml(content);

            // XML document is now loaded, pass it to the Level constructor

            return new Level(map);
        }

        //======================================================================

        public Level LoadFirstLevel()
        {
            return LoadLevel(0);
        }

        //======================================================================

        /*

        public Level LoadNextLevel()
        {

        }
         */

        //=======================================================================

        /*------------------------------------------
         * 
         * Load a Saved State
         * (selected by the LoadPage)
         * 
         * --------------------------------------*/

        public Level LoadSavedState(string levelName)
        {
            XmlDocument level = new XmlDocument();

            //--- if this is resuming the saved game, load that

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), levelName);
            string fileContents = File.ReadAllText(fileName);

            level.LoadXml(fileContents);

            return new Level(level);
        }
    }
}
