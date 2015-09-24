using System;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.InteropServices;

namespace SigknowBarcode
{
    public class TSCLIB_DLL
    {
        bool resolution300dpi = false;
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);


        // canned functions..................
        private static void printer_precheck()
        {
            if (Utils.Printer == null)
            {
                throw new Exception("Printer has not been selected.");
            }
        }

        public static bool Is300DPI()
        {
            printer_precheck();
            if (Utils.Printer.ToString().Contains("345"))
            {
                return true;
            }
            else if (Utils.Printer.ToString().Contains("644"))
            {
                return false;
            }
            else
            {
                throw new Exception("unknown printer type.");
            }
        }
        public static void PrintBarcodeWithText(string txtbarcode)
        {
            printer_precheck();
            TSCLIB_DLL.openport(Utils.Printer);
            TSCLIB_DLL.clearbuffer();
            if (Is300DPI())
            {
                TSCLIB_DLL.setup("42.5", "20", "3.0", "5", "0", "3", "0");
                TSCLIB_DLL.barcode("52", "60", "128", "67", "0", "0", "3", "3", txtbarcode);
                TSCLIB_DLL.printerfont("52", "140", "2", "0", "2", "2", txtbarcode);
            }
            else
            {
                TSCLIB_DLL.setup("40", "20", "3.0", "5", "0", "3", "0");
                TSCLIB_DLL.barcode("70", "100", "128", "100", "0", "0", "5", "5", txtbarcode);
                TSCLIB_DLL.printerfont("70", "210", "4", "0", "2", "2", txtbarcode);
            }
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
        }
        public static void PrintSNAndExpDate(string txtbarcode, string expiredate)
        {
            printer_precheck();
            TSCLIB_DLL.openport(Utils.Printer);                                           //Open specified printer driver
            TSCLIB_DLL.setup("40", "20", "3.0", "5", "0", "3", "0");                            //Setup the media size and sensor type info
            TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
            TSCLIB_DLL.printerfont("80", "100", "5", "0", "1", "1", "SN : " + txtbarcode);        //Drawing printer font
            //TSCLIB_DLL.printerfont("200", "200", "5", "0", "1", "1", "Apply By:");        //Drawing printer font
            TSCLIB_DLL.windowsfont(80, 180, 128, 0, 0, 0, "標楷體", "使用期限");
            TSCLIB_DLL.printerfont("220", "330", "4", "0", "2", "2", expiredate);        //Drawing printer font
            TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
            TSCLIB_DLL.closeport();    
        }
        public static void PrintPCBAWithText(string txtbarcode)
        {
            printer_precheck();
            TSCLIB_DLL.openport(Utils.Printer);
            // print barcode and human readable font
            TSCLIB_DLL.setup("40", "20", "3.0", "5", "0", "3", "0");    // wty --- length and width of paper needs to be changed.
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.barcode("70", "100", "128", "100", "0", "0", "5", "5", txtbarcode);
            TSCLIB_DLL.printerfont("70", "210", "4", "0", "2", "2", txtbarcode);
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
        }
        public static void PrintBarcodeWithTextForPreOperation(string txtbarcode)
        {
            printer_precheck();
            TSCLIB_DLL.openport(Utils.Printer);
            TSCLIB_DLL.clearbuffer();
            if (Is300DPI())
            {
                TSCLIB_DLL.setup("25", "10", "3.0", "5", "0", "3", "0");  // 小紙張 25mm x 10mm 
                TSCLIB_DLL.barcode("52", "60", "128", "67", "0", "0", "3", "3", txtbarcode);  // 300 dpi -- ?? mm x 12
                TSCLIB_DLL.printerfont("52", "140", "2", "0", "2", "2", txtbarcode);
            }
            else
            {
                TSCLIB_DLL.setup("28", "10", "3.0", "5", "0", "3", "0");
                TSCLIB_DLL.barcode("18", "24", "128", "100", "0", "0", "4", "4", txtbarcode); //600 dpi  -  ?? mm x 24  
                TSCLIB_DLL.printerfont("28", "130", "2", "0", "2", "2", txtbarcode);
            }
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
        }
    }
}
