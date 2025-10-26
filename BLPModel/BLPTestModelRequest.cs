namespace BLPModel
{
    public enum TestType { read, write, setlevel};
    public class BLPTestModelRequest
    {
       public  TestType testType {  get; set; }
        public string subjectName { get; set; }
        public string ObjectName { get; set; }
    }
}
