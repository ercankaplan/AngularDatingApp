using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }


    //To create not null Foreign Key relationship between AppUser and Photo
    public AppUser AppUser { get; set; }
    public int AppUserId { get; set; }

}