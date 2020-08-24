using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageStoreAPI.Models
{
    public class StoredImage
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] ImageByteArray { get; set; }
    }
}