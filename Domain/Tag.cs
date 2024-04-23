namespace UBB_SE_2024_Team_42.Domain
{
    public class Tag(long newTagId, string newTagName)
    {
        public long TagID { get; } = newTagId;
        public string TagName { get; set; } = newTagName;

        public override string ToString()
        {
            return $"Tag(tagID: {TagID}, tagName: {TagName}) \n";
        }
    }
}
