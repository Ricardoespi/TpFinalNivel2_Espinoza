using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace negocio
{
    public class Helper
    {
        public static void cargarImg(string imagen, PictureBox pbx)
        {
            try
            {
                pbx.Load(imagen);
            }
            catch (Exception ex)
            {
                pbx.Load("https://images.squarespace-cdn.com/content/v1/5a79de08aeb625f12ad4f85a/1527015265032-KYY1AQ4NCW6NB7BK1NDH/placeholder-image-vertical.png");
            }
        }
    }

}
