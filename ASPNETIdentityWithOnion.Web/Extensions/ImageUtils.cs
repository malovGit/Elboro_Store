using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace ASPNETIdentityWithOnion.Web.Extensions
{
    public static class ImageUtils
    {


        //Preview image
        public static Image GetTrumb(string path, string PathToSave, int maxPixels = 220)
        {
            Image image = Image.FromFile(path);
            //получим размер эскиза
            Size thrumbnailSize = GetTrumbnailSize(image, maxPixels);

            Image trumbnail = image.GetThumbnailImage(thrumbnailSize.Width,
                thrumbnailSize.Height, null, IntPtr.Zero);
            //trumbnail.Save(PathToSave);
            return trumbnail;
        }

        private static Size GetTrumbnailSize(Image original, int pixels)
        {
            //Максимальный размер
            //const int maxPixels = 220;

            //Ширина и высота
            int originalWidht = original.Width;
            int originalHeight = original.Height;

            //Рассчитываем фактор для маштабирования изображения на основе оригинального изображения
            double factor;
            if (originalWidht > originalHeight)
            {
                factor = (double)pixels / originalWidht;
            }
            else
            {
                factor = (double)pixels / originalHeight;
            }

            return new Size((int)(originalWidht * factor),
                (int)(originalHeight * factor));
        }

        public static bool ExistImage(string path, string fileName)
        {
            string file_path = HttpContext.Current.Server.MapPath(path+fileName);
            if (System.IO.File.Exists(file_path))
            {
                return true;
            }
            return false;
        }

        const int maxWidth = 220;
        const int maxHeight = 180;


     public static string SaveImage(HttpPostedFileBase hpf, string folder)
     {
         if (hpf != null && hpf.ContentLength != 0 && hpf.ContentLength <= 307200)
         {
             using (System.Drawing.Bitmap originalPic =
                         new System.Drawing.Bitmap(hpf.InputStream, false))
             {
                 // Вычисление новых размеров картинки
                int width = originalPic.Width; //текущая ширина
               int height = originalPic.Height; //текущая высота
                int widthDiff = (width - maxWidth); //разница с допуст. шириной
                int heightDiff = (height - maxHeight); //разница с допуст. высотой
     
                // Определение размеров, которые необходимо изменять
                bool doWidthResize = (maxWidth > 0 && width > maxWidth &&
                                    widthDiff > -1 && widthDiff > heightDiff);
                bool doHeightResize = (maxHeight > 0 && height > maxHeight &&
                        heightDiff > -1 && heightDiff > widthDiff);
    
              // Ресайз картинки
               if (doWidthResize || doHeightResize || (width.Equals(height)
                               && widthDiff.Equals(heightDiff)))
                {
                   int iStart;
                   Decimal divider;
                    if (doWidthResize)
                   {
                        iStart = width;
                       divider = Math.Abs((Decimal)iStart / maxWidth);
                        width = maxWidth;
                        height = (int)Math.Round((height / divider));
                    }
                    else
                    {
                        iStart = height;
                       divider = Math.Abs((Decimal)iStart / maxHeight);
                        height = maxHeight;
                        width = (int)Math.Round((width / divider));
                   }
                }
    
               // Сохраняем файл в папку пользователя
                using (System.Drawing.Bitmap newBMP =
                        new System.Drawing.Bitmap(originalPic, width, height))
                {
                    using (System.Drawing.Graphics oGraphics =
                                System.Drawing.Graphics.FromImage(newBMP))
                    {
                        oGraphics.SmoothingMode = 
                                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        oGraphics.InterpolationMode = 
             System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        oGraphics.DrawImage(originalPic, 0, 0, width, height);
     
                       int idx = hpf.FileName.LastIndexOf('.');
                        string ext =
                         hpf.FileName.Substring(idx, hpf.FileName.Length - idx);

                            // Формируем имя для картинки
                            
                       string imageName = Guid.NewGuid().ToString();

                            string filePath = 
                            HttpContext.Current.Server.MapPath(
                           ConfigurationManager.AppSettings["picPath"] + folder +
                               imageName + ext);
    
                       if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                       newBMP.Save(filePath);
                            return (imageName + ext);
                        }                         
                    }
                }
            }
            return string.Empty;
        }
   }
}