using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using WebApp.Data;
using WebApp.Models.Factories;
using WebApp.Models.FileManagement;

namespace WebApp.Models
{
   public interface IPdfCreator
        {
        MemoryStream CreatePdf(TripDetails vm, string id);
        }


    public class PdfCreator : IPdfCreator
    {
        private IFileManager pngFileManager;
        public PdfCreator(IFileManagerFactory fileManagerFactory)
        {
            pngFileManager = fileManagerFactory.GetManager(FileType.Png);
        }
        public MemoryStream CreatePdf(TripDetails vm,string generatepdf)
        {
            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page to the document.
            PdfPage page = doc.Pages.Add();
            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;
            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
            //Draw the text
            graphics.DrawString("Destination Address City:", font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.DestinationAddress.City, font, PdfBrushes.Black, new PointF(0, 20));

            graphics.DrawString("Destination Address Street:", font, PdfBrushes.Black, new PointF(0, 40));
            graphics.DrawString(vm.DestinationAddress.Street, font, PdfBrushes.Black, new PointF(0, 60));

            graphics.DrawString("Starting Address City:", font, PdfBrushes.Black, new PointF(0, 80));
            graphics.DrawString(vm.StartingAddress.City, font, PdfBrushes.Black, new PointF(0, 100));

            graphics.DrawString("Starting Address Street:", font, PdfBrushes.Black, new PointF(0, 120));
            graphics.DrawString(vm.StartingAddress.Street, font, PdfBrushes.Black, new PointF(0, 140));

            graphics.DrawString("Date Start:", font, PdfBrushes.Black, new PointF(0, 160));
            graphics.DrawString(vm.Date.ToString(), font, PdfBrushes.Black, new PointF(0, 180));

            graphics.DrawString("Date End:", font, PdfBrushes.Black, new PointF(0, 200));
            graphics.DrawString(vm.DateEnd.ToString(), font, PdfBrushes.Black, new PointF(0, 220));

            graphics.DrawString("Cost:", font, PdfBrushes.Black, new PointF(0, 240));
            graphics.DrawString(vm.Cost.ToString(), font, PdfBrushes.Black, new PointF(0, 260));

            graphics.DrawString("Vehicle Model:", font, PdfBrushes.Black, new PointF(0, 280));
            graphics.DrawString(vm.VechicleModel, font, PdfBrushes.Black, new PointF(0, 300));

            graphics.DrawString("Size:", font, PdfBrushes.Black, new PointF(0, 320));
            graphics.DrawString(vm.Size.ToString(), font, PdfBrushes.Black, new PointF(0, 340));

            graphics.DrawString("Is smoking allowed:", font, PdfBrushes.Black, new PointF(0, 360));
            graphics.DrawString(vm.IsSmokingAllowed.ToString(), font, PdfBrushes.Black, new PointF(0, 380));

            graphics.DrawString("Description:", font, PdfBrushes.Black, new PointF(0, 400));
            graphics.DrawString(vm.Description, font, PdfBrushes.Black, new PointF(0, 420));

            if (vm.MapId != null)
            {
                var id = pngFileManager.SaveFile(generatepdf, "wwwroot/images/maps/");
                //Load the image as stream.
                FileStream imageStream = new FileStream($"wwwroot/images/maps/{id}.png", FileMode.Open, FileAccess.Read);
                PdfBitmap image = new PdfBitmap(imageStream);
                //Draw the image
                RectangleF bounds = new RectangleF(0, 20, 500, 500);
                page = doc.Pages.Add();
                page.Graphics.DrawString("Map:", font, PdfBrushes.Black, new PointF(50, 0));
                page.Graphics.DrawImage(image, bounds);
                pngFileManager.RemoveFile(id, "wwwroot/images/maps/");
            }
            //Save the PDF document to stream
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);

            return stream;
        }

    }
}
