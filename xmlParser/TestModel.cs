namespace xmlParser
{
    public class TestModel
    {
        public string Name { get; set; }
        public string mail{ get; set; }
        public int age { get; set; }

        public InnerTestModel InnerTestModel { get; set; }
    }

    public class InnerTestModel
    {
        public string name { get; set; }
        public string mail{ get; set; }
    }
}