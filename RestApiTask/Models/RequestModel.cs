namespace RestApiTask.Models
{
    public class RequestModel
    {
        public string Path { get; set; }

        public string StatusCode { get; set; }
    }


    public class Rootobject
    {
        public Request1[] Request1 { get; set; }
        public Request2[] Request2 { get; set; }
        public Request3[] Request3 { get; set; }
        public Request4[] Request4 { get; set; }
        public Request5[] Request5 { get; set; }
        public Request6[] Request6 { get; set; }
    }

    public class Request1
    {
        public string Path { get; set; }
        public string StatusCode { get; set; }
    }

    public class Request2
    {
        public string Path { get; set; }
        public string StatusCode { get; set; }
    }

    public class Request3
    {
        public string Path { get; set; }
        public string StatusCode { get; set; }
    }

    public class Request4
    {
        public string Path { get; set; }
        public string StatusCode { get; set; }
    }

    public class Request5
    {
        public string Path { get; set; }
        public string StatusCode { get; set; }
    }

    public class Request6
    {
        public string Path { get; set; }
        public string StatusCode { get; set; }
    }

}
