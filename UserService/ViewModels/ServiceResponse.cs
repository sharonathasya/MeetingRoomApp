

namespace UserService.ViewModels
{
    public class ServiceResponse<T>
    {
        public int CODE { get; set; }
        public string MESSAGE { get; set; }
        public IEnumerable<T> DATA { get; set; }
    }

    public class ServiceResponseSingle<T>
    {
        public int CODE { get; set; }
        public string MESSAGE { get; set; }
        public T DATA { get; set; }
    }

  


}
