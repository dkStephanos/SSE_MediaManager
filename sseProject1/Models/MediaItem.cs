using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace sseProject1.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileType { get; set; }

        public int CollectionId { get; set; }
        public MediaCollection Collection { get; set; }

    }
}
