using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract22.Model
{
    public partial class Склады
    {

        public string PhotoFull
        {
            get
            {
                if (this.Photo == null)
                {
                    return null;
                }
                else
                {
                    string namePhoto = Directory.GetCurrentDirectory() + "\\image\\" + Photo;
                    return namePhoto;
                }
            }
        }

    }
}
