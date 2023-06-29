namespace Cookapp_API.DataAccess.DTO.AllInOneDTO.PostDTO
{
    public class CreatePostDTO
    {

        public string Title { get; set; } = null!;

        public string RefTag { get; set; } = null!;

        public string Content { get; set; } = null!;


        public string RefCategory { get; set; } = null!;

        public string RefAccount { get; set; } = null!;

        public Byte[] Image { get; set; }
        public int preptime { get; set; }
        public int addtime { get; set; }
        public int cooktime { get; set; }


    }
}
