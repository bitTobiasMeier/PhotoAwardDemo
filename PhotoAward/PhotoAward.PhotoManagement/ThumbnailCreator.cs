using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PhotoAward.PhotoManagement
{
    public interface IThumbnailCreator
    {
        Task<byte[]> GetThumbnail(byte[] data);
        Task<byte[]> GetThumbnail(Image img);
    }

    public class ThumbnailCreator : IThumbnailCreator
    {

        public  async Task<byte[]> GetThumbnail(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var bmp = new Bitmap(ms);
                var thumbnailTask = await GetThumbnail(bmp);
                return thumbnailTask;
            }
        }

        public   async Task<byte[]> GetThumbnail(Image img)
        {
            return await Task.Run(() =>
            {
                using (Bitmap srcBmp = new Bitmap(img))
                {
                    float ratio = srcBmp.Width / srcBmp.Height;
                    float ratio2 = srcBmp.Height / srcBmp.Width;
                    var width = 500;
                    var height = 500;
                    if (srcBmp.Width > srcBmp.Height)
                    {
                        height = Convert.ToInt32(width * ratio);
                    }
                    else
                    {
                        width = Convert.ToInt32(height * ratio2);
                    }

                    SizeF newSize = new SizeF(width, height);
                    using (Bitmap target = new Bitmap((int)newSize.Width, (int)newSize.Height))
                    {

                        using (Graphics graphics = Graphics.FromImage(target))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.DrawImage(srcBmp, 0, 0, newSize.Width, newSize.Height);
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                target.Save(memoryStream, ImageFormat.Jpeg);
                            }
                        }
                        return ImageToByteArray(target);
                    }
                }
            });

        }


        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}