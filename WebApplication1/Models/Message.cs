using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Service;

namespace MegaStore.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string SenderId {  get; set; }
        [Required]
        public string ReceiverId { get; set; }
        [MaxLength(2000)]
        public string Content {  get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; }
    }
}
