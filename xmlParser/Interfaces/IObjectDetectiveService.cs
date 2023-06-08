namespace xmlParser.Interfaces
{
    public interface IObjectDetectiveService<T> where T : class
    {
        List<T> GetListObjectElements();
        IEnumerable<BaseElement> GetListSatisfactedElements();
    }
}