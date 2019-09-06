using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sseProject1.Models
{
    public class MediaCollection
    {
        public int Id { get; set; }
        public string BorrowRequests { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<MediaItem> MediaItems { get; set; }
    }
}
