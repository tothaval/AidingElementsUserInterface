/* Aiding Elements User Interface
 *      TXT_Manual class
 * 
 * intermediate solution to serve textual contents to recipient elements
 * 
 * init:        2023|12|01
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Texts
{
    internal class TXT_Manual
    {        
        public string language { get; set; }

        public TXT_Manual(string _language = "english")
        {
            language = _language;
        }
        public StringBuilder tb_LicenseTerms()
        {
            // if(config.language == "english"){ value = "...";}

            //if (language == "english")
            //{
            //    string value =
            //        "\n" +
            //        "license term ideas\n" +
            //        "\n" +
            //        "(if an existing license covers these points, i would choose this license,\n" +
            //        "but i don't know if such an license exists.):\n" +
            //        "\n" +
            //        "1. free in terms of distribution and free to use.\n" +
            //        "2. acknowledgement of authorship and extended authorship\n" +
            //        "3. no warranty at the moment possible, no warranty for free version\n" +
            //        "4. no support at the moment possible, no support for free version\n" +
            //        "5. pay option for enterprises and everyone else once it is finished,\n" +
            //        "\tthe sum should be determineable by the enterprise or person willing to pay,\n" +
            //        "\tregarding the payment redistributing procedure, i have no idea at the moment.\n" +
            //        "6. every element developed must follow the basic design principles:\n" +
            //        "\t-left click to drag and right click to close,\n" +
            //        "\tif it uses the canvas functionality:\n" +
            //        "\t-left clicks are supposed to change color, size, z-index and dimensional orientation\n" +
            //        "\t-middle clicks(or an alternative function) are used to move those containers over the menu surface\n" +
            //        "\t-right clicks are used for deleting objects from a canvas, to close the application\n" +
            //        "\t-F1 help window, F2 screenshots of the scetchboard surface, F3 goldpaletteElement, F5 license terms\n" +
            //        "7. if advertising contains audiovisual contents or any other form of influence method,\n" +
            //        "\tit must be possible to modulate the intensity of the influencing method,\n" +
            //        "\tif advertising is part of a fork or an application concept, it has to follow the following\n" +
            //        "\tdesign principles: -closable, resizeable, (audio control, video control), and moveable\n" +
            //        "8. copies can be altered and redistributed freely under the terms of this license.";

            //    StringBuilder sb = new StringBuilder(value);

            //    return sb;
            //}
            //else
            //{
            //    return new StringBuilder("language selection error");
            //}

            return new StringBuilder("language selection error");
        }

        public StringBuilder tb_NonAlphabetKeys()
        {
            if (language == "english")
            {
                string value =
                    "\n" +
                    "manual:\n" +
                    "\n" +
                    "Key F1:\t\tmanual element\n" +
                    "\n" +
                    "Key F2:\t\tMyNote element\n" +
                    "\n" +
                    "Key F3:\t\tFlatShareCostCalculator element\n";

                StringBuilder sb = new StringBuilder(value);

                return sb;
            }
            else
            {
                return new StringBuilder("language selection error");
            }

        }

        public StringBuilder tb_AlphabetKeys_A_M()
        {
            //if (language == "english")
            //{
            //    string value =
            //        "\n" +
            //        "manual:\n" +
            //        "\n" +
            //        "Key A:\t\tinvoke a binding fileLinkElement onto main canvas,\n" +
            //        "\t\tbuttons and bindings will be saved\n" +
            //        "\n" +
            //        "Key B:\t\tchange main canvas background to color selection\n" +
            //        "\n" +
            //        "Key C:\t\tinvoke coordinates element onto main canvas,\n" +
            //            "\t\tit shows mouse coordinates and it's own coordinates\n" +
            //        "\n" +
            //        "Key D:\t\tinvoke defined drawing element onto main canvas,\n" +
            //        "\t\tit contains controls and buttons to draw different shapes\n" +
            //        "\n" +
            //        "Key E:\t\tinvoke text element onto main canvas\n" +
            //        "\t\tcontents will be saved\n" +
            //        "\n" +
            //        "Key F:\t\tinvoke freehand drawing element onto main canvas,\n" +
            //        "\t\tit contains controls and  buttons to draw per mouse\n" +
            //        "\n" +
            //        "Key G:\t\tswitch canvas background to backgroundimage\n" +
            //        "\n" +
            //        "Key H:\t\thide or show menu elements on the main canvas\n" +
            //        "\n" +
            //        "Key I:\t\tinvoke a link area element onto the main canvas\n" +
            //        "\t\tlinks will be saved\n" +
            //        "\n" +
            //        "Key J:\t\tinvoke a selection binding element onto the main canvas\n" +
            //        "\t\tchoices will be saved\n" +
            //        "\n" +
            //        "Key K:\t\tinvoke a canvas element onto the main canvas\n" +
            //        "\n" +
            //        "Key L:\t\tinvoke a link element onto the main canvas\n" +
            //        "\t\tlinks will be saved\n" +
            //        "\n" +
            //        "Key M:\t\tinvoke the main menu element onto the main canvas\n";

            //    StringBuilder sb = new StringBuilder(value);

            //    return sb;
            //}
            //else
            //{
            //    return new StringBuilder("language selection error");
            //}

            return new StringBuilder("language selection error");

        }

        public StringBuilder tb_AlphabetKeys_N_Z()
        {
            //if (language == "english")
            //{
            //    string value =
            //        "\n" +
            //        "manual:\n" +
            //        "\n" +
            //        "Key N:\t\tinvoke a name element onto the main canvas, you can change\n" +
            //        "\t\tit's text just once, hit [ENTER] to leave writing mode\n" +
            //        "\t\tcontent will be saved\n" +
            //        "\n" +
            //        "Key O:\t\tinvoke a measure element onto the main canvas (unfinished)\n" +
            //        "\n" +
            //        "Key P:\t\tinvoke a file and folder creation element onto the main canvas\n" +
            //        "\n" +
            //        "Key Q:\t\tinvoke a link binding element onto the main canvas\n" +
            //        "\t\tlinks will be saved\n" +
            //        "\n" +
            //        "Key R:\t\tinvoke placement and design module onto the main canvas\n" +
            //        "\n" +
            //        "Key T:\t\tinvoke a timer element onto the main canvas\n" +
            //        "\n" +
            //        "Key W:\t\tinvoke a dice element onto the main canvas\n" +
            //        "\n" +
            //        "Key V:\t\tcopy all elements to background picture, with option picturize\n" +
            //        "\t\tactive(see options element -> F4) all elements will be deleted, else kept\n" +
            //        "\n" +
            //        "Key X:\t\tinvoke a binding fileLinkElement area element onto the main canvas,\n" +
            //        "\t\tbindings will be saved\n" +
            //        "\n" +
            //        "Key Y:\t\tinvoke the color element onto the main canvas\n" +
            //        "\n" +
            //        "Key Z:\t\tinvoke z indexer element onto the main canvas\n";

            //    StringBuilder sb = new StringBuilder(value);

            //    return sb;
            //}
            //else
            //{
            //    return new StringBuilder("language selection error");
            //}

            return new StringBuilder("language selection error");
        }

        public StringBuilder tb_NoticesAndHandlingInstructions()
        {
            if (language == "english")
            {
                string value =
                    "\n" +
                    "have fun\n" +
                    "\n" +
                    "most elements will show tooltips for more informations on how to use YS UI\n" +
                    "and its features, tooltips can be deactivated (not yet implemented for all elements)\n" +
                    "\n" +
                    "\n" +
                    "right mouse click:\n" +
                    "\ta right click onto the main canvas will invoke the right click choice element\n" +
                    "\tonto the main canvas at the mouse click position, click quit fileLinkElement to quit,\n" +
                    "\tclick shutdown fileLinkElement and confirm to shutdown your computer\n" +
                    "\n" +
                    "\ta right click on an element will delete that element\n";
                    //"\n" +
                    //"middle mouse click:\n" +
                    //"\tclick and hold to drag canvas elements over the canvas area\n" +
                    //"\n" +
                    //"left mouse click:\n" +
                    //"\n" +
                    //"\tleft click onto the main canvas and hold to drag the ys ui window\n" +
                    //"\n" +
                    //"\ta left click on an element will change the element. You can...\n" +
                    //"\t... change colors when the color feature is on screen,\n" +
                    //"\t... change size, rotation and z-value if the placement \n" +
                    //"\t\tand design element is on screen\n" +
                    //                    "\n" +
                    //"you can select elements by left clicking them, click on empty canvas surface to\n" +
                    //"loose selection entirely. hit DEL to delete the selected elements\n" +
                    //"\n" +
                    //"if a key command does not work, try [ESC] or right click on canvas and\n" +
                    //"abort quitting or close the element where you inserted text. The issue\n" +
                    //"is due to keyboard focus loss, couldn't fix it yet, no time.\n" +
                    //"\n" +
                    //"drag and drop:\n" +
                    //"\tselect multiple files and drag'n'drop them on...\n" +
                    //"\t\t...the main canvas and a canvas element per file will be\n" +
                    //"\t\t\tgenerated, use the placement and design element to\n" +
                    //"\t\t\tspecify the size, rotation and z-index of the elements.\n" +
                    //"\n" +
                    //"\t\t...either the picture mosaic element or the binding fileLinkElement area element,\n" +
                    //"\t\tthe images or the files will be bound to that element\n" +
                    //"\n" +
                    //"use mousewheel scrolling on image element, picture mosaic element areas or color element\n" +
                    //"to load image. select elements on canvas, on color element choose image brush additionally\n" +
                    //"then click on the image to apply the image as background to all selected elements\n" +
                    //"\n" +
                    //"color element\n" +
                    //"bind color element default colors to the four color selection buttons by using\n" +
                    //"keys 1-4(not numpad) after clicking one of the buttons. or expand slider panel\n" +
                    //"and select color using the sliders, click on one of the buttons(1-4) to apply color\n" +
                    //"to color selection buttons(I-IV).\n" +
                    //"select brushes and click on an element on the canvas to apply the color as element\n" +
                    //"background. select a multitude of elements and use the apply fileLinkElement below the brush\n" +
                    //"options panel to apply color selection to all selected elements\n" +
                    //"\n" +
                    //"change ys ui colors by expanding ys ui color options below slider panel, a click\n" +
                    //"on a fileLinkElement will paint the saved color onto the color display area and will update\n" +
                    //"the sliders as well. click bind fileLinkElement to change into bind mode. alter the color\n" +
                    //"f.e. by using the sliders until you have a pleasent result, then click on the fileLinkElement\n" +
                    //"which represents the desired surface category you want to change, click on bind fileLinkElement\n" +
                    //"to leave bind mode. the new color will be used(but currently not everywhere right from\n" +
                    //"the beginning, in such cases restart ys ui or recreate an element.) you can change any\n" +
                    //"number of ys ui surface colors while bind mode is active. but be careful, in the worst\n" +
                    //"case reset ys ui to default via options element(F4)\n";

                StringBuilder sb = new StringBuilder(value);

                return sb;
            }
            else
            {
                return new StringBuilder("language selection error");
            }
        }

        public StringBuilder exe_Location_StringBuilder()
        {
            string value = $".exe:\t\t{System.IO.Directory.GetCurrentDirectory()}";

            return new StringBuilder(value);
        }

        public StringBuilder tbVersion()
        {
            //if (language == "english")
            //{
            //    string value =
            //        "\n" +
            //        "Version:\t0.9.8.9(alpha)\n" +
            //        ".Net-Core:\t4.6.1\n" +
            //        "\n" +
            //        $"{exe_Location_StringBuilder()}";

            //    StringBuilder sb = new StringBuilder(value);

            //    return sb;
            //}

            //else
            //{
            //    return new StringBuilder("language selection error");
            //}

            return new StringBuilder("language selection error");
        }

    }
}
/*  END OF FILE
 * 
 * 
 */