public interface IDialogLine
{
    public string Speaker { get; set; }
    public int UID { get; set; }
    public int SequenceID { get; set; }
    public string Dialog { get; set; }
}