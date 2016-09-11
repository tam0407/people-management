using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeopleManagement.Model;

namespace PeopleManagement.Manager
{
    public class PrintManager
    {
        private static PrintManager _currentObject = null;
        public static PrintManager getInstance()
        {
            if (_currentObject == null)
                _currentObject = new PrintManager();
            return _currentObject;
        }

        private PrintPreviewDialog printPreviewDialog1;
        private PrintDocument printDocument1;
        private Bitmap memoryImage;
        private PeopleManager ppManager = PeopleManager.getInstance();

        private Font DefaultFont;

        private PrintManager()
        {
            InitStyle();

            printPreviewDialog1 = new PrintPreviewDialog();
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += PrintDocument1_PrintPage;
        }

        private void InitStyle()
        {
            DefaultFont = SystemFonts.DefaultFont;
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(memoryImage, 0, 0);

            //e.Graphics.MeasureString("Biên bản mất phiếu giữ xe", DefaultFont, e.MarginBounds.Size,
            //    StringFormat.GenericTypographic);

            e.Graphics.DrawString("Biên bản mất phiếu giữ xe", DefaultFont, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);
        }

        public void Print(Graphics graphics, Size size, Point point)
        {
            CaptureScreen(graphics, new Size(595, 842), new Point(0,0));
            printDocument1.Print();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        public void Print(int ppID, int bikeID)
        {
            Person person = ppManager.GetPersonById(ppID);
            Motobike bike = ppManager.GetBikeList(ppID).First(b => b.Id == bikeID);

            printDocument1.Print();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void CaptureScreen(Graphics graphics, Size size, Point point)
        {
            memoryImage = new Bitmap(size.Width, size.Height, graphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(point.X, point.Y, 0, 0, size);
        }
    }
}
