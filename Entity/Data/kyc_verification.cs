using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKYCWebhook.Entity.Data
{
    public class kyc_verification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        [Column("verification_id", TypeName = "varchar(100)")]
        public string? verification_id { get; set; }

        [Column("id_card_number", TypeName = "varchar(20)")]
        public string? id_card_number { get; set; }

        [Column("title_th", TypeName = "varchar(50)")]
        public string? title_th { get; set; }

        [Column("first_name_th", TypeName = "varchar(100)")]
        public string? first_name_th { get; set; }

        [Column("last_name_th", TypeName = "varchar(100)")]
        public string? last_name_th { get; set; }

        [Column("first_name_en", TypeName = "varchar(100)")]
        public string? first_name_en { get; set; }

        [Column("last_name_en", TypeName = "varchar(100)")]
        public string? last_name_en { get; set; }

        [Column("birth_date", TypeName = "datetime")]
        public DateTime? birth_date { get; set; }

        [Column("address", TypeName = "nvarchar(500)")]
        public string? address { get; set; }

        [Column("card_issue_date", TypeName = "datetime")]
        public DateTime? card_issue_date { get; set; }

        [Column("card_expire_date", TypeName = "datetime")]
        public DateTime? card_expire_date { get; set; }

        [Column("created_at", TypeName = "datetime")]
        public DateTime? created_at { get; set; }
    }
}
